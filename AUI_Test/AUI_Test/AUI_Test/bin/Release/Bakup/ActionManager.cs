using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUI_Test
{
    public abstract class ActionManager
    {
        /// <summary>
        /// the parent - an Automation engine
        /// </summary>
        protected Automation Parent { get; set; }

        /// <summary>
        /// list of automation actions
        /// </summary>
        protected Dictionary<string, Action> Actions { get; set; }

        /// <summary>
        /// construct an ActionManager
        /// </summary>
        /// <param name="parent">the Automation object</param>
        public ActionManager(Automation parent)
        {
            Parent = parent;
            parent.ActionManagers.Add(this);
            Actions = new Dictionary<string, Action>();
        }

        /// <summary>
        /// register an action to ActionManager
        /// </summary>
        /// <param name="action">the action to be registered</param>
        public void RegisterAction(Action action)
        {
            Actions[action.Name] = action;
        }

        /// <summary>
        /// create an action for the line
        /// </summary>
        /// <param name="actLine">the action line</param>
        /// <returns>the action</returns>
        /// 
        public abstract Action getAction(ActionLine actLine);

    }
}
