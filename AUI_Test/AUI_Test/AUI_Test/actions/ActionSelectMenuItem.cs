using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AUI_Test.ABT.Model;
using AUI_Test.ABT.Auto;
namespace AUI_Test.actions
{
    class ActionSelectMenuItem : UIAAction
    {
        /// <summary>
        /// contructor
        /// </summary>
        public ActionSelectMenuItem()
        {
            Name = @"select menu item";
        }

        /// <summary>
        /// the path to the menu item, delimeter is '->'
        /// </summary>
        private string MenuPath { get; set; }

        /// <summary>
        /// set parameters to action
        /// </summary>
        public override Dictionary<string, string> Params
        {
            set
            {
                base.Params = value;

                if (Params.ContainsKey(@"item"))
                    MenuPath = Params[@"item"];
            }
        }

        /// <summary>
        /// check whether the parameters are valid
        /// </summary>
        /// <returns>true - if params are valid</returns>
        public override bool IsValid()
        {
            if (Window == null)
                throw new Exception(Constants.Messages.Error_Matching_Window_NotFound);

            // the coordinate is specified
            if (MenuPath == null)
                return false;

            return true;
        }

        /// <summary>
        /// executing the action
        /// </summary>
        /// <returns>true - if click success</returns>
        public override int Execute()
        {
            string[] itemTexts = MenuPath.Split(@"->".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            TestStack.White.UIItems.MenuItems.Menu item = null;

            try
            {
                TestStack.White.UIItems.WindowStripControls.MenuBar menuBar = Window.MenuBar;
                item = menuBar.MenuItem(itemTexts);
            }
            catch
            { }

            if (item == null)
            {
                Result = ActionResult.WARNING;
                MoreDetailAboutResult = @"Menu item does not exist.";
            }
            else
            {
                item.Click();
            }

            return 0;
        }

        /// <summary>
        /// reset action after executing
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            MenuPath = null;
        }
    }
}
