using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AUI_Test.ABT.Model;
namespace AUI_Test.ABT.Auto
{
    public class ActionManager : ABT.Model.IActionManager
    {
        /// <summary>
        /// the parent - an Automation engine
        /// </summary>
        protected IAutomation Parent { get; set; }

        /// <summary>
        /// list of automation actions
        /// </summary>
        protected Dictionary<string, IAction> Actions { get; set; }

        /// <summary>
        /// construct an ActionManager
        /// </summary>
        /// <param name="parent">the Automation object</param>
        public ActionManager(IAutomation parent)
        {
            Parent = parent;
            parent.ActionManagers.Add(this);
            Actions = new Dictionary<string, IAction>();

            WaitTime = new TimeSpan(0, 0, 30);
        }

        /// <summary>
        /// register an action to ActionManager
        /// </summary>
        /// <param name="action">the action to be registered</param>
        public void RegisterAction(IAction action)
        {
            Actions[action.Name] = action;
        }

        /// <summary>
        /// create an action for the line
        /// </summary>
        /// <param name="actLine">the action line</param>
        /// <returns>the action</returns>
        public virtual IAction getAction(ActionLine actLine)
        {
            return null;
        }

        /// <summary>
        /// wait time of finding windows and controls
        /// </summary>
        public virtual TimeSpan WaitTime { get; set; }

    }
}
