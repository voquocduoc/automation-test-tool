using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.AccessControl;
using TestStack.White;
using AUI_Test.actions;
namespace AUI_Test
{
    internal class ActionStartProgram : UIAAction
    {
        /// <summary>
        /// the path to the launching program
        /// </summary>
        private string ProgramPath { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public ActionStartProgram()
        {
            Name = "start program";
        }

        /// <summary>
        /// set parameters to action
        /// </summary>
        public override Dictionary<string, string> Params
        {
            set
            {
                base.Params = value;

                if (Params.ContainsKey(@"path"))
                    ProgramPath = Params[@"path"];
            }
        }

        /// <summary>
        /// check whether the parameters are valid
        /// </summary>
        /// <returns>true - if params are valid</returns>
        public override bool IsValid()
        {
            // check the program path is valid
            if (ProgramPath == null || ProgramPath.Length == 0)
                return false;

            // check the program file exist
            FileInfo file = new FileInfo(ProgramPath);
            if (!file.Exists)
                return false;

            return true;
        }

        /// <summary>
        /// executing the action
        /// </summary>
        /// <returns>true - if start program successfully</returns>
        public override int Execute()
        {
            // launch the program
            Application.Launch(ProgramPath);

            return 0;
        }

        /// <summary>
        /// override the reset from base class
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            ProgramPath = null;
        }
    }
}
