using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUI_Test
{
    public class SourceFile
    {
        // Muc dich cua cai nay dung de doc du lieu INF, DATA, SCRIPT lên 
        // cái Process data là trong moi cai no doc len va co cai gi va dua vao bo du lieu


        // Contructor
        public SourceFile(IFileParser parser)
        {
            Parser = parser;
            ProcessData();
        }

        // --------------------------------------------------------------------------
        

        // duong dan cua một file 
        string pathfilesl;
        public string PathFile
        {
            get { return pathfilesl; }
            set
            {
                pathfilesl = value;
            }
        }

        // xac dinh cái nay là interface ha script ha data
        public string namesl;
        public string Name
        {
            get
            {
                return namesl;
            }


            set
            {
                namesl = value;
            }
        }

        public string NameWindow { get; protected set; }

        // Lây ra bo du lieu tung dong
        public List<SourceLine> Lines { get; private set; }

        //Parser
        public IFileParser Parser { get; private set; }


        //--------------------------------------------------------------------------
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

        //--------------------------------------------------------------------------

        // Tien trinh lay du lieu
        protected virtual void ProcessData()
        {

        }
    }

}
