using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUI_Test
{
    public class Script : SourceFile
    {

        public Script(IFileParser parser)    : base(parser)
        {
            CurrentLineNumber = 0;
        }


        protected override void ProcessData()
        {
            base.ProcessData();

            this.Name = "Script.xls";
            string duongdan = Parser.pathworkingdir + Constants.Directory.ScriptDir;

            string path = duongdan + Name;
            this.PathFile = path;

            //--------minh diem cai interface co bao nhieu cot

            SourceLine _SourlineScript = new SourceLine();
            Parser.getopen(path);
            int cot = Parser.ColumnCount;
            Parser.pathlines = path;
            int dong = Parser.CountRow(path);

            //----------------------------------------------------


            ActionLines = new List<ActionLine>();
            for (int j = 1; j <= dong; j++)
            {
                if (_SourlineScript.CountColmsv(path) > 0)
                {
                    ActionLine actLine = new ActionLine();
                    actLine.ActionName = Parser.ValueCell(path, j - 1, 0).ToLower();

                    for (int i = 1; i < cot; i++)
                    {

                        string[] pairs = Parser.ValueCell(path, j - 1, i).Split(Constants.PropertyDelimeter.ToCharArray(), 2);
                        if (pairs.Length != 2)
                        {
                            continue;
                        }
                        if (Constants.KeywordWindow.Equals(pairs[0], StringComparison.CurrentCultureIgnoreCase))
                            actLine.WindowName = pairs[1];
                        else if (Constants.KeywordControl.Equals(pairs[0], StringComparison.CurrentCultureIgnoreCase))
                            actLine.ControlName = pairs[1].ToLower();
                        else
                            actLine.Arguments[pairs[0]] = pairs[1];

                    }

                    ActionLines.Add(actLine);
                }
            }
        }


        private List<ActionLine> ActionLines { get; set; }


        public bool HasNextLine
        {
            get { return CurrentLineNumber < ActionLines.Count; }
        }


        public int CurrentLineNumber { get; private set; }


        public ActionLine Next()
        {
            if (CurrentLineNumber < ActionLines.Count)
            {
                return ActionLines[CurrentLineNumber++];
            }

            return null;
        }

        public void Restart()
        {
            CurrentLineNumber = 0;
        }
    }
}
