using System;
using System.Collections.Generic;
using System.Diagnostics;

using System.Windows.Automation;
using TestStack.White.Factory;
using TestStack.White.UIItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.Finders;

using AUI_Test.ABT.Model;
using AUI_Test.ABT.Auto;
using AUI_Test.actions;
using AUI_Test;

namespace AUI_Test
{
    public class UIAActionManager : ActionManager
    {
        /// <summary>
        /// construct an ActionManager
        /// </summary>
        /// <param name="parent">the Automation object</param>
        public UIAActionManager(IAutomation parent)
            : base(parent)
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
        }

        /// <summary>
        /// create an action for the line
        /// </summary>
        /// <param name="actLine">the action line</param>
        /// <returns>the action</returns>
        public override IAction getAction(ActionLine actLine)
        {
            Window targetWindow = null;
            IUIItem targetControl = null;

            if (!Actions.ContainsKey(actLine.ActionName))
                return null;
            if (actLine.WindowName != null && !Parent.Interfaces.ContainsKey(actLine.WindowName))
                throw new Exception(Constants.Messages.Error_Matching_Window_NoDefinition);

            // search for the target control
            if (actLine.WindowName != null)
            {
                targetWindow = FindWindow(Parent.Interfaces[actLine.WindowName].Properties);
                if (targetWindow != null && actLine.ControlName != null)
                {
                    if (!Parent.Interfaces[actLine.WindowName].Controls.ContainsKey(actLine.ControlName))
                        throw new Exception(Constants.Messages.Error_Matching_Control_NoDefinition);

                    targetControl = FindControl(targetWindow, Parent.Interfaces[actLine.WindowName].Controls[actLine.ControlName]);
                }
            }

            // prepare the action
            UIAAction action = Actions[actLine.ActionName] as UIAAction;
            action.Window = targetWindow;
            action.Control = targetControl;
            action.Params = actLine.Arguments;

            return action;
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
            if (criteria.Count == 0)
                return false;

            //window.AutomationElement.c
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

                try
                {
                    List<Window> windows = WindowFactory.Desktop.DesktopWindows();

                    // loop all windows on the desktop
                    foreach (Window window in windows)
                    {
                        if (MatchWindow(window, criteria))
                            foundWindows.Add(window);
                    }
                }
                catch { }

                if (foundWindows.Count > 0)
                    break;

                sw.Stop();
                wait.Subtract(sw.Elapsed);
            }

            // check for error
            if (foundWindows.Count != 1)
                return null;

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
                switch (key.ToLower())
                {
                    //case Constants.PropertyNames.ControlType:
                    //    crit = crit.AndControlType(GetTypeByName(criteria[key]));
                    //    break;
                    case Constants.PropertyNames.AutomationId:
                        crit = crit.AndAutomationId(criteria[key]);
                        break;
                    case Constants.PropertyNames.Text:
                        crit = crit.AndByText(criteria[key]);
                        break;
                    case Constants.PropertyNames.ClassName:
                        crit = crit.AndByClassName(criteria[key]);
                        break;
                    case Constants.PropertyNames.Index:
                        crit = crit.AndIndex(int.Parse(criteria[key]));
                        break;
                    default:
                        {
                            bool bNativeFound = false;
                            AutomationProperty[] props = window.AutomationElement.GetSupportedProperties();
                            foreach (AutomationProperty prop in props)
                            {
                                string propName = prop.ProgrammaticName.Substring(prop.ProgrammaticName.IndexOf('.') + 1);
                                propName = propName.Substring(0, propName.IndexOf("Property"));
                                if (propName.Equals(key, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    crit.AndNativeProperty(prop, criteria[key]);
                                    bNativeFound = true;
                                    break;
                                }
                            }
                            if (bNativeFound)
                                break; ;
                        }
                        return null;
                };
            }

            try
            {
                // search for control with 'crit'
                IUIItem item = window.Get(crit, WaitTime);

                // return the found control
                return item;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
