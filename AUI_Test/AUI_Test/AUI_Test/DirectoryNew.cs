using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace AUI_Test
{
    class DirectoryNew
    {


        public DirectoryNew()
        {

        }

        // Minh phai co duong dan
        public string path;
        public string PathProject
        {
            get
            {
                return path;
            }
            set
            {
                path = value;
            }
        }
        // Ten Project
        string name;
        public string NameProject
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public string CreateDir()
        {
            try
            {
                string ProjectPath = PathProject + @"\" + NameProject;
                string filePath = PathProject + @"\" + NameProject + @"\Script";
                string filePath1 = PathProject + @"\" + NameProject + @"\Data";
                string filePath2 = PathProject + @"\" + NameProject + @"\Interface";
                System.IO.Directory.CreateDirectory(filePath);
                System.IO.Directory.CreateDirectory(filePath1);
                System.IO.Directory.CreateDirectory(filePath2);
                return ProjectPath;
            }
            catch
            {
                MessageBox.Show("Chua Tao Duoc");
                return "";
            }
        }
    }
}
