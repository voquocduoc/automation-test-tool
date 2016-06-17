using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White.Factory;
using TestStack.White.UIItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.Finders;
using System.Windows.Automation;
using System.Diagnostics;
using AUI_Test.actions;

namespace AUI_Test
{
    public class UIAActionManager : ActionManager
    {
        /// <summary>
        /// wait time of finding windows and controls
        /// </summary>
        public TimeSpan WaitTime { get; set; }

        /// <summary>
        /// construct an ActionManager with an Automation engine
        /// </summary>
        /// <param name="automation">the Automation engine</param>
        public UIAActionManager(Automation automation)  : base(automation)
        {
            RegisterAction(new ActionClick());
            RegisterAction(new ActionEnter());
            RegisterAction(new ActionSet());
            RegisterAction(new ActionStartProgram());
            RegisterAction(new ActionCloseWindow());
            RegisterAction(new ActionCheckWindowProperty());
            RegisterAction(new ActionCheckWindowExist());
            RegisterAction(new ActionCheckControlProperty());
            RegisterAction(new ActionCheckControlExist());
            RegisterAction(new ActionSelectMenuItem());
            WaitTime = new TimeSpan(0, 0, 30);
        }

        

        /// <summary>
        /// get control type from a string name
        /// </summary>
        /// <param name="typeName">the type name</param>
        /// <returns>the control type</returns>
        private ControlType GetTypeByName(string typeName)
        {
            switch (typeName)
            {
                case Constants.ControlTypeNames.ControlTypeButton:
                    return ControlType.Button;
                case Constants.ControlTypeNames.ControlTypeTextBox:
                    return ControlType.Text;
                default:
                    return null;
            };
        }

        /// <summary>
        /// check if a window is matched with given criteria
        /// </summary>
        /// <param name="window">the window to be checked</param>
        /// <param name="criteria">the criteria</param>
        /// <returns>true - if matched</returns>
        public bool MatchWindow(Window window, Dictionary<string, string> criteria)
        {
            // for each critera, check the property of window
            foreach (string key in criteria.Keys)
            {
                switch (key.ToLower())
                {
                    case Constants.PropertyNames.Title:
                        if (window.Title != criteria[key])
                            return false;
                        break;
                    case Constants.PropertyNames.Name:
                        if (window.Name != criteria[key])
                            return false;
                        break;
                    case Constants.PropertyNames.Id:
                        if (window.Id != criteria[key])
                            return false;
                        break;
                    default:
                        return false;
                };
            }

            // all criteria are matched
            return true;
        }

        /// <summary>
        /// find a window with given criteria
        /// </summary>
        /// <param name="criteria">the criteria to find</param>
        /// <returns>the found window</returns>
        private Window FindWindow(Dictionary<string, string> criteria)
        {
            List<Window> foundWindows = new List<Window>();

            TimeSpan wait = WaitTime;
            while (wait.TotalMilliseconds > 0)
            {
                Stopwatch sw = Stopwatch.StartNew();

                List<Window> windows = WindowFactory.Desktop.DesktopWindows();

                // loop all windows on the desktop
                foreach (Window window in windows)
                {
                    if (MatchWindow(window, criteria))
                        foundWindows.Add(window);
                }

                if (foundWindows.Count > 0)
                    break;

                sw.Stop();
                wait.Subtract(sw.Elapsed);
            }

            // check for error
            if (foundWindows.Count > 1)
                throw new Exception(Constants.Messages.Error_Matching_Window_NoUniqueWindow);
            else if (foundWindows.Count == 0)
                throw new Exception(Constants.Messages.Error_Matching_Window_NotFound);

            // found the window
            return foundWindows[0];
        }

        /// <summary>
        /// given a criteria, find a control within a window
        /// </summary>
        /// <param name="window">the containing window</param>
        /// <param name="criteria">the criteria to find the control</param>
        /// <returns>the found control. null - if not found</returns>
        public IUIItem FindControl(Window window, Dictionary<string, string> criteria)
        {
            // the "all" condition
            SearchCriteria crit = SearchCriteria.All;

            // for each criteria, AND with a new condition
            foreach (string key in criteria.Keys)
            {
                switch (key)
                {
                    case Constants.PropertyNames.ControlType:
                        crit = crit.AndControlType(GetTypeByName(criteria[key]));
                        break;
                    case Constants.PropertyNames.AutomationId:
                        crit = crit.AndAutomationId(criteria[key]);
                        break;
                    case Constants.PropertyNames.Text:
                        crit = crit.AndByText(criteria[key]);
                        break;
                    default:
                        return null;
                };
            }

            // search for control with 'crit'
            IUIItem item = window.Get(crit, WaitTime);

            // return the found control
            return item;
        }

        /// <summary>
        /// get an UIA action for the line
        /// </summary>
        /// <param name="actLine">the action line</param>
        /// <returns>the UIA action</returns>
        public override Action getAction(ActionLine actLine)
        {
            Window targetWindow = null;
            IUIItem targetControl = null;

            if (!Actions.ContainsKey(actLine.ActionName))
                throw new Exception(Constants.Messages.Error_Executing_NoAction);
            if (actLine.WindowName != null && !Parent.Interfaces.ContainsKey(actLine.WindowName))

                throw new Exception(Constants.Messages.Error_Matching_Window_NoDefinition);
            //if (actLine.WindowName != null )
              //  throw new Exception(Constants.Messages.Error_Matching_Window_NoDefinition);

            // search for the target control
            //Dictionary<string, string> Key = Parent.Interfaces.Values;
            if (actLine.WindowName != null)
                targetWindow = FindWindow(Parent.Interfaces[actLine.WindowName].Properties);
            if (actLine.ControlName != null)
                targetControl = FindControl(targetWindow, Parent.Interfaces[actLine.WindowName].Controls[actLine.ControlName]);
            // prepare the action
            UIAAction action = Actions[actLine.ActionName] as UIAAction;
            action.Window = targetWindow;
            action.Control = targetControl;
            action.Params = actLine.Arguments;

            return action;
        }
    }
}
