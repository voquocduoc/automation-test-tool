using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AUI_Test.ABT.Model;
namespace AUI_Test.ABT.Auto
{
    public class Script : SourceFile, ABT.Model.IScript
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="parser">the file parser</param>
        public Script(IFileParser parser)  : base(parser)
        {
            CurrentLineNumber = 0;
            ActionLines = new List<ActionLine>();

            Parser.WorkingDir = Parser.WorkingDir + Constants.Directory.ScriptDir;
        }

        /// <summary>
        /// process script data from parser
        /// </summary>
        protected override void ProcessLoadedData()
        {
            base.ProcessLoadedData();

            try
            {
                // process each source line: convert it to action line
                foreach (SourceLine line in Parser.Lines)
                {
                    if (line.ColumnCount > 0)
                    {
                        ActionLine actLine = new ActionLine();
                        actLine.ActionName = line.Columns[0].ToLower();
                        for (int i = 1; i < line.ColumnCount; i++)
                        {
                            string[] pairs = line.Columns[i].Split(Constants.PropertyDelimeter.ToCharArray(), 2);
                            if (pairs.Length != 2)
                                throw new FormatException(Constants.Messages.Error_Parsing_Script);
                            if (Constants.Keywords.KeywordWindow.Equals(pairs[0], StringComparison.CurrentCultureIgnoreCase))
                                actLine.WindowName = pairs[1].ToLower();
                            else if (Constants.Keywords.KeywordControl.Equals(pairs[0], StringComparison.CurrentCultureIgnoreCase))
                                actLine.ControlName = pairs[1].ToLower();
                            else
                                actLine.Arguments[pairs[0].ToLower()] = pairs[1];
                        }
                        ActionLines.Add(actLine);
                    }
                }
            }
            catch
            {
                ActionLines.Clear();

                throw;
            }
        }

        /// <summary>
        /// list of action lines
        /// </summary>
        private List<ActionLine> ActionLines { get; set; }

        /// <summary>
        /// check if there is any action lines are waiting for executing
        /// </summary>
        public bool HasNextLine
        {
            get { return CurrentLineNumber < ActionLines.Count; }
        }

        /// <summary>
        /// current executing line number
        /// </summary>
        public int CurrentLineNumber { get; private set; }

        /// <summary>
        /// go to next action line
        /// </summary>
        /// <returns>null - if no action line left</returns>
        public ActionLine Next()
        {
            if (CurrentLineNumber < ActionLines.Count)
            {
                return ActionLines[CurrentLineNumber++];
            }

            return null;
        }

        /// <summary>
        /// restart the script
        /// </summary>
        public void Restart()
        {
            CurrentLineNumber = 0;
        }
    }
}
