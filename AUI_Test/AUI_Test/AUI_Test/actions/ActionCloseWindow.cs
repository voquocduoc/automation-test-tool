using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AUI_Test
{
    internal class ActionCloseWindow : actions.UIAAction
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public ActionCloseWindow()
        {
            Name = "close window";
        }

        /// <summary>
        /// check whether the parameters are valid
        /// </summary>
        /// <returns>true - if params are valid</returns>
        public override bool IsValid()
        {
            if (Window == null)
                return false;

            return true;
        }

        /// <summary>
        /// executing the action
        /// </summary>
        /// <returns>true - if close window successfully</returns>
        public override int Execute()
        {
            Window.Close();

            return 0;
        }
    }
}
