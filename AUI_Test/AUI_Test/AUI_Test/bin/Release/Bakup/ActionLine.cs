using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUI_Test
{
    public class ActionLine
    {
        /// <summary>
        /// constructor
        /// </summary>
        public ActionLine()
        {
            Arguments = new Dictionary<string, string>();
        }

        /// <summary>
        /// name of the action
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// name of the window
        /// </summary>
        public string WindowName { get; set; }

        /// <summary>
        /// name of the control
        /// </summary>
        public string ControlName { get; set; }

        /// <summary>
        /// additional arguments for executing action
        /// </summary>
        public Dictionary<string, string> Arguments { get; private set; }
    }
}
