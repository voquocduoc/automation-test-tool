using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUI_Test.ABT.Model
{
    public delegate void StartedHandler(IAutomation at);
    public delegate void EndedHandler(IAutomation at);
    public delegate void InteruptedHandler(IAutomation at);
    public delegate void PausedHandler(IAutomation at);
    public delegate void ResumedHandler(IAutomation at);
    public delegate void ActionPerfomingNotificationHandler(string summary);

    public interface IAutomation
    {
        /// <summary>
        /// name of this running
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// starting script
        /// </summary>
        IScript StartScript { get; set; }

        /// <summary>
        /// executing script
        /// </summary>
        IScript CurrentScript { get; }

        /// <summary>
        /// current data table
        /// </summary>
        IData Data { get; set; }

        /// <summary>
        /// the action manager
        /// </summary>
        List<IActionManager> ActionManagers { get; }

        /// <summary>
        /// current list of loaded interfaces
        /// </summary>
        Dictionary<string, IInterface> Interfaces { get; }

        /// <summary>
        /// the file parser use for all source file
        /// </summary>
        IFileParser Parser { get; }

        /// <summary>
        /// the reporter of the automation
        /// </summary>
        IReporter Reporter { get; }

        /// <summary>
        /// the working directory
        /// </summary>
        string WorkingDir { get; set; }

        /// <summary>
        /// speed of running automation, from 0 to 10
        /// </summary>
        int Speed { get; set; }

        /// <summary>
        /// error message in running Automation
        /// </summary>
        string ErrorMessage { get; }

        /// <summary>
        /// the automation has just started
        /// </summary>
        event StartedHandler Started;

        /// <summary>
        /// the automation has just ended
        /// </summary>
        event EndedHandler Ended;

        /// <summary>
        /// the automation has just interupted
        /// </summary>
        event InteruptedHandler Interupted;

        /// <summary>
        /// the automation has just paused
        /// </summary>
        event PausedHandler Paused;

        /// <summary>
        /// the automation has just resumed
        /// </summary>
        event ResumedHandler Resumed;

        /// <summary>
        /// the automation is going to perform an action
        /// </summary>
        event ActionPerfomingNotificationHandler ActionPerforming;

        /// <summary>
        /// start running automation
        /// </summary>
        void Start();

        /// <summary>
        /// interupt current automation
        /// </summary>
        void Interupt();

        /// <summary>
        /// temporary pause the automation
        /// </summary>
        void Pause();

        /// <summary>
        /// resume the paused automation
        /// </summary>
        void Resume();

    }
}
