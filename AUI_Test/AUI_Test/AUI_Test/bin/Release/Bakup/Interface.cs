using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AUI_Test
{
    public class Interface : SourceFile
    {

        public Interface(IFileParser parser) : base(parser)
        {


        }



        public Dictionary<string, string> Properties { get; set; }


        public Dictionary<string, Dictionary<string, string>> Controls { get; set; }


        // minh phai dc duoc script len coi su dung interface nao roi moi fare len 
        protected override void ProcessData()
        {
            base.ProcessData();
            // Lay Dc Windown - Control
            // minh phải cho no den cua interface de doc
            this.Name = "Calculator";
            string duongdan = Parser.pathworkingdir + Constants.Directory.InterfaceDir;

            string path = duongdan + Name + Parser.FileExtension;
            this.PathFile = path;
            Name.ToLower();
            //--------minh diem cai interface co bao nhieu cot

            SourceLine _Sourline = new SourceLine();
            Parser.getopen(path);
            int cot = Parser.ColumnCount;
            //int cot = _Sourline.CountColmsv(path);
            Parser.pathlines = path;
            int dong = Parser.CountRow(path);


            Controls = new Dictionary<string, Dictionary<string, string>>();
            Properties = new Dictionary<string, string>();
            for (int j = 1; j <= dong; j++)
            {

                if (_Sourline.CountColmsv(path) > 0)
                {
                    string window = Parser.ValueCell(path, j - 1, 0);
                    string control = Parser.ValueCell(path, j - 1, 0);
                    if (Constants.KeywordWindow == window)
                    {
                        this.NameWindow = Parser.ValueCell(path, j - 1, 1).ToLower();
                        for (int i = 2; i < _Sourline.CountColmsv(path); i++)
                        {
                            string[] pairs = Parser.ValueCell(path, j - 1, i).Split(Constants.PropertyDelimeter.ToCharArray(), 2);

                            if (pairs.Length != 2)
                                throw new FormatException(Constants.Messages.Error_Parsing_Interface_InvalidWindow);

                            Properties[pairs[0]] = pairs[1];


                        }
                    }

                    else if (Constants.KeywordControl == control)
                    {
                        string controlName = Parser.ValueCell(path, j - 1, 1).ToLower();
                        for (int i = 2; i < _Sourline.CountColmsv(path); i++)
                        {
                            string[] pairs = Parser.ValueCell(path, j - 1, i).Split(Constants.PropertyDelimeter.ToCharArray(), 2);
                            if (pairs.Length != 2)
                                throw new FormatException(Constants.Messages.Error_Parsing_Interface_InvalidControl);

                            Controls.Add(controlName, new Dictionary<string, string>());
                            Controls[controlName][pairs[0]] = pairs[1];

                        }
                    }
                }
            }



        }


    }
}
