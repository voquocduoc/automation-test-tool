using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems;

using AUI_Test.ABT.Model;
using AUI_Test.ABT.Auto;

namespace AUI_Test.actions
{
    public class UIAAction : ABT.Auto.Action
    {
        /// <summary>
        /// the UIA control to be automated
        /// </summary>
        public Window Window { get; set; }

        /// <summary>
        /// the UIA control to be automated
        /// </summary>
        public IUIItem Control { get; set; }

        /// <summary>
        /// reset action after executing
        /// </summary>
        public override void Reset()
        {
            base.Reset();

            Window = null;
            Control = null;
        }
    }
}
