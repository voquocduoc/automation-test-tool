using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using AUI_Test.ABT.Auto;
using AUI_Test.ABT.Model;
namespace AUI_Test.actions
{
    class ActionCheckControlProperty : UIAAction
    {
        /// <summary>
        /// name of property to be checked
        /// </summary>
        private string PropertyName { get; set; }

        /// <summary>
        /// value of property to be checked
        /// </summary>
        private string PropertyValue { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        public ActionCheckControlProperty()
        {
            Name = @"check control property";
        }

        /// <summary>
        /// set parameters to action
        /// </summary>
        public override Dictionary<string, string> Params
        {
            set
            {
                base.Params = value;

                if (Params.ContainsKey(@"name"))
                    PropertyName = Params[@"name"];
                if (Params.ContainsKey(@"value"))
                    PropertyValue = Params[@"value"];
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

            if (Control == null)
                throw new Exception(Constants.Messages.Error_Matching_Control_NotFound);

            // the coordinate is specified
            if (PropertyName == null || PropertyValue == null)
                return false;

            return true;
        }

        /// <summary>
        /// executing the action
        /// </summary>
        /// <returns>true - if click success</returns>
        public override int Execute()
        {
            Result = ActionResult.ERROR;

            if (Constants.PropertyNames.AutomationId.Equals(PropertyName, StringComparison.CurrentCultureIgnoreCase))
                Result = Control.AutomationElement.Current.AutomationId == PropertyValue ? ActionResult.PASSED : ActionResult.FAILED;
            else if (Constants.PropertyNames.Id.Equals(PropertyName, StringComparison.CurrentCultureIgnoreCase))
                Result = Control.Id == PropertyValue ? ActionResult.PASSED : ActionResult.FAILED;
            else if (Constants.PropertyNames.Name.Equals(PropertyName, StringComparison.CurrentCultureIgnoreCase))
                Result = Control.Name == PropertyValue ? ActionResult.PASSED : ActionResult.FAILED;
            else if (Constants.PropertyNames.Text.Equals(PropertyName, StringComparison.CurrentCultureIgnoreCase))
                Result = Control.Name == PropertyValue ? ActionResult.PASSED : ActionResult.FAILED;
            else if (Constants.PropertyNames.Title.Equals(PropertyName, StringComparison.CurrentCultureIgnoreCase))
                Result = Control.Name == PropertyValue ? ActionResult.PASSED : ActionResult.FAILED;
            else
            {
                AutomationProperty[] props = Control.AutomationElement.GetSupportedProperties();
                foreach (AutomationProperty prop in props)
                {
                    string propName = prop.ProgrammaticName.Substring(prop.ProgrammaticName.IndexOf('.') + 1);
                    propName = propName.Substring(0, propName.IndexOf("Property"));
                    if (propName.Equals(PropertyName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (Control.AutomationElement.GetCurrentPropertyValue(prop).ToString().Equals(PropertyValue, StringComparison.CurrentCultureIgnoreCase))
                            Result = ActionResult.PASSED;
                        break;
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// reset action after executing
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            PropertyName = PropertyValue = null;
        }

        
    }
}
