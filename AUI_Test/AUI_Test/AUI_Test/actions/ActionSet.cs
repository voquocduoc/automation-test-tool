using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AUI_Test.ABT.Auto;
namespace AUI_Test.actions
{
    class ActionSet : UIAAction
    {
        private bool Checked { get; set; }

        public ActionSet()
        {
            Name = @"set";
            Checked = true;
        }

        /// <summary>
        /// set parameters to action
        /// </summary>
        public override Dictionary<string, string> Params
        {
            set
            {
                base.Params = value;
                if (Params.ContainsKey(@"checked"))
                    Checked = Params[@"checked"].Equals(@"true", StringComparison.CurrentCultureIgnoreCase);
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

            if (!(Control is TestStack.White.UIItems.CheckBox) && !(Control is TestStack.White.UIItems.RadioButton))
                return false;

            return base.IsValid();
        }

        /// <summary>
        /// reset action after executing
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            Checked = true;
        }

        /// <summary>
        /// executing the action
        /// </summary>
        /// <returns>true - if click success</returns>
        public override int Execute()
        {
            if (Control is TestStack.White.UIItems.CheckBox)
                (Control as TestStack.White.UIItems.CheckBox).Checked = Checked;
            else if (Control is TestStack.White.UIItems.RadioButton)
                (Control as TestStack.White.UIItems.RadioButton).IsSelected = Checked;

            return 0;
        }
    }
}
