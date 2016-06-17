using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AUI_Test.ABT.Auto;
using AUI_Test.ABT.Auto;
using AUI_Test.ABT.Model;
namespace AUI_Test.actions
{
    class ActionEnter : UIAAction
    {
        private string Text { get; set; }

        public ActionEnter()
        {
            Name = @"enter";
        }

        /// <summary>
        /// set parameters to action
        /// </summary>
        public override Dictionary<string, string> Params
        {
            set
            {
                base.Params = value;
                if (Params.ContainsKey(@"text"))
                    Text = Params[@"text"];
            }
        }

        /// <summary>
        /// check if the parameters are valid for executing
        /// derived class should re-implement this function
        /// </summary>
        /// <returns>true - if parameters are ready for executing</returns>
        public override bool IsValid()
        {
            if (Window == null)
                throw new Exception(Constants.Messages.Error_Matching_Window_NotFound);

            if (Control == null)
                throw new Exception(Constants.Messages.Error_Matching_Control_NotFound);

            if (Text == null)
                return false;

            if (!(Control is TestStack.White.UIItems.TextBox))
                return false;

            return base.IsValid();
        }

        /// <summary>
        /// reset action after executing
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            Text = null;
        }

        /// <summary>
        /// executing the action
        /// </summary>
        /// <returns>true - if click success</returns>
        public override int Execute()
        {
            TestStack.White.UIItems.TextBox txt = Control as TestStack.White.UIItems.TextBox;
            if (txt.IsReadOnly)
            {
                Result = ActionResult.WARNING;
                MoreDetailAboutResult = Constants.WarningMessages.Warning_ReadOnly_TextBox;
            }

            txt.Text = Text;
            return 0;
        }
    }
}
