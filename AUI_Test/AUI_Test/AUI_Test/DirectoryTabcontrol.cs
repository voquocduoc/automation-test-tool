using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QiHe.CodeLib;
using ExcelLibrary.CompoundDocumentFormat;
using ExcelLibrary.BinaryDrawingFormat;
using ExcelLibrary.BinaryFileFormat;
using ExcelLibrary.SpreadSheet;
using System.IO;
using System.Drawing;
using System.Threading;
using Telerik.WinControls.UI.Docking;
using AUI_Test.ABT.Model;
using AUI_Test.ABT.Auto;
namespace AUI_Test
{
    public class DirectoryTabcontrol : TabControl
    {

       ExcelFileParser  Parser = new ExcelFileParser();

       public Dictionary<string, string> duongdan = new Dictionary<string, string>();

        //name of tab
        public string nametab;
        public string nameoftab
        {
            set
            {
                nametab = value;
            }

        }

        string PathDir;
        public string Dir
        {
            set
            {
                PathDir = value;

            }

            get
            {

                return PathDir;
            }
        }





        public void Run()
        {
            if (Dir == null)
                return;
            Parser.getopen(Dir);
            Parser.loadexcel();
            TabPage sheetPage = new TabPage(nametab);
            sheetPage.Controls.Add(Parser.loadexcel());
            TabPages.Add(sheetPage);
            sheetPage.Select();
        }


        public myContextMenuStrip myMenu;

        public DirectoryTabcontrol()
        {
           Run();
           Dock = DockStyle.Fill;
           this.MouseClick += DirectoryTabcontrol_MouseClick;
        }

        void DirectoryTabcontrol_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                myMenu = new myContextMenuStrip();
                
                ToolStripMenuItem Close = new ToolStripMenuItem("Close");

                Close.Image = Properties.Resources.delete;
                
                myMenu.Items.AddRange(new ToolStripItem[] { Close });

                ContextMenuStrip = myMenu;

                Close.Click += Close_Click;
               
            }
        }

        void Close_Click(object sender, EventArgs e)
        {
            TabPage selectedTab = SelectedTab;
            TabPages.Remove(selectedTab);           

        }


        //Contextmenu
        public class myContextMenuStrip : ContextMenuStrip
        {

            public TreeNode tn;
            public myContextMenuStrip()
            {

            }
        }
        


    }
}
