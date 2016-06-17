using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AUI_Test.ABT.Model;
using System.Threading;
namespace AUI_Test.ABT.Auto
{
    public class Automation : ABT.Model.IAutomation
    {
        /// <summary>
        /// construct a default Automation engine
        /// </summary>
        /// <param name="workingDir">the working directory</param>
        /// 
        public Automation(string workingDir = "")
        {
            Interfaces = new Dictionary<string, IInterface>();
            Scripts = new Stack<IScript>();
            ActionManagers = new List<IActionManager>();

            WorkingDir = workingDir;
            Speed = 10;
            Name = Constants.DefaultName;
        }

        /// <summary>
        /// construct an Automation Engine
        /// </summary>
        /// <param name="parser">the file parser</param>
        /// <param name="workingDir">the working directory</param>
        public Automation(IFileParser parser, IReporter reporter, string workingDir = "")       : this(workingDir)
        {
            Parser = parser;
            Parser.WorkingDir = WorkingDir;

            Reporter = reporter;
            Reporter.WorkingDir = WorkingDir;
        }

        /// <summary>
        /// get corresponding Action object from all Action Managers
        /// </summary>
        /// <param name="actLine">the action line</param>
        /// <returns>the found action</returns>
        private IAction getAction(ActionLine actLine)
        {
            int i = 0;
            IAction action = null;
            while (action == null && i < ActionManagers.Count)
                action = ActionManagers[i++].getAction(actLine);

            return action;
        }

        #region Run Automation

        /// <summary>
        /// check if the automation is in 'paused' state
        /// </summary>
        private void CheckPaused()
        {
            if (IsPausing)
            {
                IsPausing = false;
                IsPaused = true;
                if (Paused != null)
                    Paused(this);
            }
            while (IsPaused && !IsStopping)
                Thread.Sleep(100);

            CheckResumed();
        }

        /// <summary>
        /// check if the automation is being resumed
        /// </summary>
        private void CheckResumed()
        {
            if (IsResuming)
            {
                IsResuming = false;
                IsResumed = true;

                if (Resumed != null)
                    Resumed(this);
            }
        }

        /// <summary>
        /// check if the automation is in 'stopped' state
        /// </summary>
        private bool CheckStopped()
        {
            if (IsStopping)
            {
                IsStopping = false;
                IsStopped = true;

                return true;
            }

            return false;
        }

        /// <summary>
        /// run all automation
        /// </summary>
        private void RunAll()
        {
            if (Data != null)
            {
                // create new report
                IReporter reporter = Reporter.NewInstance;
                reporter.BeginReport(Name, Data.Name);

                while (Data.MoveNext() && !IsStopped)
                {
                    reporter.BeginDataRow(Data.CurrentRowId);

                    StartScript.Restart();
                    Scripts.Push(StartScript);
                    
                    Run(reporter);

                    reporter.EndDataRow(Data.CurrentRowId);
                }

                reporter.EndReport();
            }
            else
            {
                // create new report
                IReporter reporter = Reporter.NewInstance;
                reporter.BeginReport(Name + Constants.ReportText.ReportNameSuffix, null);

                StartScript.Restart();
                Scripts.Push(StartScript);
                Run(reporter);

                reporter.EndReport();
            }

            if (IsStopped && Interupted != null)
                Interupted(this);

            if (Ended != null)
                Ended(this);
        }

        /// <summary>
        /// execute the script
        /// </summary>
        private void Run(IReporter reporter)
        {
            while (Scripts.Count > 0 && !IsStopped)
            {
                // pop a script from stack
                CurrentScript = Scripts.Pop();

                // begin new section in report
                if (CurrentScript.CurrentLineNumber == 0)
                    reporter.BeginScript(CurrentScript.Name);

                // loop for each line of current script
                while (CurrentScript.HasNextLine && !IsStopped)
                {
                    // get a action line from script
                    ActionLine actLineRaw = CurrentScript.Next();

                    // manipulate action line with DataSet
                    ActionLine actLine = ManipulateData(actLineRaw);

                    // notify about action that is going to be performed
                    if (ActionPerforming != null)
                    {
                        string summary = actLine.ActionName;
                        if (actLine.WindowName != null)
                            summary += "\t[window:" + actLine.WindowName + "]";
                        if (actLine.ControlName != null)
                            summary += "\t[control:" + actLine.ControlName + "]";
                        foreach (string key in actLine.Arguments.Keys)
                            summary += "\t[" + key + ":" + actLine.Arguments[key] + "]";
                        ActionPerforming(summary);
                    }

                    // the action is 'use interface'
                    if (actLine.ActionName == Constants.Keywords.ActionUseInterface)
                    {
                        if (!Interfaces.ContainsKey(actLine.Arguments[Constants.Keywords.KeywordInterface].ToLower()))
                        {
                            IInterface newInterface = new Interface(Parser.NewInstance);
                            newInterface.FileName = actLine.Arguments[Constants.Keywords.KeywordInterface] + Parser.FileExtension;
                            Interfaces.Add(newInterface.Name, newInterface);
                        }

                    }
                    // the action is 'run script'
                    else if (actLine.ActionName == Constants.Keywords.ActionStartScript)
                    {
                        Script newScript = new Script(Parser.NewInstance);
                        newScript.FileName = actLine.Arguments[Constants.Keywords.KeywordScript] + Parser.FileExtension;

                        // push current script to stack and run new script
                        Scripts.Push(CurrentScript);
                        CurrentScript = newScript;

                        // begin new section in report
                        reporter.BeginScript(CurrentScript.Name);
                    }
                    else
                    {
                        try
                        {
                            IAction action = getAction(actLine);
                            if (action == null)
                                throw new Exception(Constants.Messages.Error_Executing_NoAction);

                            if (!action.IsValid())
                                throw new Exception(Constants.Messages.Error_Executing_InvalidArg);

                            // execute the action
                            int ret = action.Execute();

                            // write result of executing to report
                            reporter.WriteLine(actLine, action.Result);

                            // reset the action state
                            action.Reset();
                        }
                        catch (Exception e)
                        {
                            reporter.WriteError(actLine, e.Message);
                            InteruptWithError(e.Message);
                        }
                    }

                    // if the automation is paused, just sleep
                    CheckPaused();

                    // check if user interupt the automation
                    if (!CheckStopped())
                        ProcessSpeed();
                }

                // if the automation is paused, just sleep
                CheckPaused();

                // check if user interupt the automation
                CheckStopped();

                // end section in report
                if (CurrentScript.CurrentLineNumber > 0)
                    reporter.EndScript(CurrentScript.Name);
            }
        }

        /// <summary>
        /// check for string 'input' if it contains a variable, then replace the variables with DataSet
        /// </summary>
        /// <param name="input">the input string</param>
        /// <returns>the result string</returns>
        private string CheckForVariable(string input)
        {
            if (input == null)
                return null;

            while (true)
            {
                int idx = input.IndexOf(Constants.Keywords.VariableNamePrefix);
                if (idx < 0)
                    return input;
                int idx2 = input.IndexOf(Constants.Keywords.VariableNameSuffix, idx);
                if (idx2 < 0)
                    return input;

                string variable = input.Substring(idx, idx2 - idx + 1);
                string varName = input.Substring(idx + 2, idx2 - idx - 2);

                input = input.Replace(variable, Data[varName]);
            }
        }

        /// <summary>
        /// manipulate action line with data from DataSet
        /// </summary>
        /// <param name="actLine">the raw action line</param>
        /// <returns>the manipulated action line</returns>
        private ActionLine ManipulateData(ActionLine actLine)
        {
            ActionLine newLine = new ActionLine();
            newLine.ActionName = actLine.ActionName;
            newLine.WindowName = CheckForVariable(actLine.WindowName);
            newLine.ControlName = CheckForVariable(actLine.ControlName);

            foreach (string key in actLine.Arguments.Keys)
                newLine.Arguments[key] = CheckForVariable(actLine.Arguments[key]);

            return newLine;
        }

        #endregion Run Automation

        #region Controlling the Automation

        /// <summary>
        /// process delaying based on the automation speed
        /// </summary>
        private void ProcessSpeed()
        {
            if (Speed < 0) Speed = 0;
            if (Speed > 10) Speed = 10;

            Thread.Sleep((10 - Speed) * 200);
        }

        /// <summary>
        /// start the automation
        /// </summary>
        public void Start()
        {
            if (CurrentThread != null && CurrentThread.IsAlive)
                return;

            ErrorMessage = null;
            CurrentThread = new Thread(RunAll);
            CurrentThread.Start();

            if (Started != null)
                Started(this);
        }

        /// <summary>
        /// interupt the automation
        /// </summary>
        public void Interupt()
        {
            IsStopping = true;
        }

        /// <summary>
        /// interupt the automation with an error
        /// </summary>
        /// <param name="errorMsg"></param>
        public void InteruptWithError(string errorMsg)
        {
            if (ErrorMessage == null)
                ErrorMessage = errorMsg;
            else
                ErrorMessage += "\n" + errorMsg;

            Interupt();
        }

        /// <summary>
        /// pause the automation
        /// </summary>
        public void Pause()
        {
            IsPausing = true;
        }

        /// <summary>
        /// resume the automation
        /// </summary>
        public void Resume()
        {
            IsResuming = true;
            IsPaused = false;
        }

        #endregion Controlling the Automation

        #region Properties

        /// <summary>
        /// name of this run
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// the starting script, used with data set
        /// </summary>
        public IScript StartScript { get; set; }

        /// <summary>
        /// executing script
        /// </summary>
        public IScript CurrentScript { get; private set; }

        /// <summary>
        /// executing scripts stack
        /// </summary>
        public Stack<IScript> Scripts { get; private set; }

        /// <summary>
        /// current data table
        /// </summary>
        public IData Data { get; set; }

        /// <summary>
        /// the action manager
        /// </summary>
        public List<IActionManager> ActionManagers { get; private set; }

        /// <summary>
        /// current list of loaded interfaces
        /// </summary>
        public Dictionary<string, IInterface> Interfaces { get; private set; }

        /// <summary>
        /// the file parser use for all source file
        /// </summary>
        public IFileParser Parser { get; private set; }

        /// <summary>
        /// the reporter of the automation
        /// </summary>
        public IReporter Reporter { get; private set; }

        /// <summary>
        /// the working directory
        /// </summary>
        public string WorkingDir { get; set; }

        /// <summary>
        /// speed of running automation, from 1 to 10
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// error message in running Automation
        /// </summary>
        public string ErrorMessage { get; protected set; }

        /// <summary>
        /// the automation has just started
        /// </summary>
        public event StartedHandler Started;

        /// <summary>
        /// the automation has just ended
        /// </summary>
        public event EndedHandler Ended;

        /// <summary>
        /// the automation has just interupted
        /// </summary>
        public event InteruptedHandler Interupted;

        /// <summary>
        /// the automation has just paused
        /// </summary>
        public event PausedHandler Paused;

        /// <summary>
        /// the automation has just resumed
        /// </summary>
        public event ResumedHandler Resumed;

        /// <summary>
        /// the automation is going to perform an action
        /// </summary>
        public event ActionPerfomingNotificationHandler ActionPerforming;

        /// <summary>
        /// current automation thread
        /// </summary>
        protected Thread CurrentThread { get; set; }

        /// <summary>
        /// the paused state of the automation
        /// </summary>
        private bool IsPaused { get; set; }

        /// <summary>
        /// the automation is being paused
        /// </summary>
        private bool IsPausing { get; set; }

        /// <summary>
        /// the resumed state of the automation
        /// </summary>
        private bool IsResumed { get; set; }

        /// <summary>
        /// the automation is being resumed
        /// </summary>
        private bool IsResuming { get; set; }

        /// <summary>
        /// the ended state of the automation
        /// </summary>
        private bool IsStopped { get; set; }

        /// <summary>
        /// the automation is being stopped
        /// </summary>
        private bool IsStopping { get; set; }

        #endregion
    }
}
