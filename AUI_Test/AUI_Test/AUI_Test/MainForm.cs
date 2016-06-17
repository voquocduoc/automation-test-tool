using AUI_Test.ABT.Auto;
using AUI_Test.ABT.Model;
using ExcelLibrary.BinaryDrawingFormat;
using ExcelLibrary.BinaryFileFormat;
using ExcelLibrary.CompoundDocumentFormat;
using ExcelLibrary.SpreadSheet;
//-------------------------------------------
using QiHe.CodeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
//-------------------------------------------
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI.Docking;
using White.Core.Factory;
using White.Core.InputDevices;
using White.Core.UIItems.Finders;
using System.Windows.Input;
//-------------------------------------------

using UITimer = System.Windows.Forms.Timer;
using Telerik.WinControls.UI;

namespace AUI_Test
{
    public partial class MainForm : Telerik.WinControls.UI.RadForm
    {
        public MainForm()
        {
            InitializeComponent();

            DirTreeview.SelectOpen += treeView_SelectOpen;

            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            this.KeyPreview = true;
            this.KeyPress += MainForm_KeyPress;
            this.KeyDown += MainForm_KeyDown;
           
            
        }

        void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.I)
            {
                this.insertLine();
            }

            if (e.Control && e.KeyCode == Keys.O)
            {
                this.Open_File();
            }
            if (e.Control && e.KeyCode == Keys.D)
            {
                this.deleteLine();
            }
            if (e.Control && e.KeyCode == Keys.S)
            {
                this.save();
            }

        }

        void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {          
        }

        public void insertLine()
        {
            TabPage selectedTab = tabControl.SelectedTab;
            int selectedIndex = tabControl.SelectedIndex;
            int chilcontrol = tabControl.TabPages[selectedIndex].Controls.Count;



            string nameoftab = selectedTab.Text;
            string _pt = tabControl.duongdan[nameoftab];

            DataGridView dgv = tabControl.TabPages[selectedIndex].Controls[0] as DataGridView;

            int rowCurrent = dgv.CurrentCell.RowIndex;

            dgv.Rows.Insert(rowCurrent,1);           
            
        }

        public void deleteLine()
        {
            TabPage selectedTab = tabControl.SelectedTab;
            int selectedIndex = tabControl.SelectedIndex;
            int chilcontrol = tabControl.TabPages[selectedIndex].Controls.Count;



            string nameoftab = selectedTab.Text;
            string _pt = tabControl.duongdan[nameoftab];

            DataGridView dgv = tabControl.TabPages[selectedIndex].Controls[0] as DataGridView;

            int rowCurrent = dgv.CurrentCell.RowIndex;

            dgv.Rows.RemoveAt(rowCurrent);  
        }

        bool ispause;

        public delegate void EventhandlerPathDir(string path);
        public event EventhandlerPathDir PathProject;

        private void treeView_SelectOpen(string duongdan, string name)
        {
            duongdantabcontrol = duongdan;
            nametabdir = name;
            RunTabConTrol();
        }

        public int counttabpage()
        {
            int i = tabControl.TabPages.Count;
            return i;
            
        }


        // name  of tab
        public string nametabofdir;
        public string nametabdir
        {
            set
            {
                nametabofdir = value;
            }
        }
        // Khi bam open ben tree vi no truyen duong dan qua va thuc hien runtabcontrolben nay
        public string duongdan;
        public string duongdantabcontrol
        {
            set
            {
                duongdan = value;

            }
        }

        //DUONG DAN PROJECT
        public string duongdanproject;
        public string pathproject
        {

            set
            {
                duongdanproject = value;
            }
        }

        // cHAY TAB
        public void RunTabConTrol()
        {
            
                DirectoryTabcontrol RanString = tabControl as DirectoryTabcontrol;
                RanString.Dir = duongdan;
                RanString.nameoftab = nametabofdir;
                try
                {
                    RanString.duongdan.Add(nametabofdir, duongdan);
                    RanString.Run();
                    counttabpage();
                }
                catch
                {
                    RanString.Run();
                    counttabpage();
                }
                
            
            
            
        }



        

        private void MainForm_Load(object sender, EventArgs e)
        {

            
        }

       

        
        private void Open_File_Click(object sender, EventArgs e)
        {
                string file = FileSelector.BrowseFile(FileType.Excel97);
                DirectoryTabcontrol RanString = tabControl as DirectoryTabcontrol;
                int name = file.LastIndexOf("\\");
                string _nametab = file.Substring(name + 1);
                RanString.duongdan.Add(_nametab, file);
                RanString.nameoftab = _nametab;
                RanString.Dir = file;
                RanString.Run();
                counttabpage();
            

        }

        public void Open_File()
        {
            try
            {
                string file = FileSelector.BrowseFile(FileType.Excel97);
                DirectoryTabcontrol RanString = tabControl as DirectoryTabcontrol;
                int name = file.LastIndexOf("\\");
                string _nametab = file.Substring(name + 1);
                try
                {
                    RanString.duongdan.Add(_nametab, file);
                    RanString.nameoftab = _nametab;
                    RanString.Dir = file;
                    RanString.Run();
                }
                catch
                {
                    RanString.nameoftab = _nametab;
                    RanString.Dir = file;
                    RanString.Run();
                }                
               
                counttabpage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void radSplitContainerChinh_Click(object sender, EventArgs e)
        {

        }

        private void Automation_Bar_Click(object sender, EventArgs e)
        {

        }

        private void documentWindow3_Click(object sender, EventArgs e)
        {
        }

        private void radMenuItem23_Click(object sender, EventArgs e)
        {

        }

        //---- New Project
        public void newproject()
        {
            //Show Form
            NewProject _newproject = new NewProject();
            _newproject.ShowDialog();

            // Dua Vao CAY
            DirectoryTreeview NewDir = DirTreeview as DirectoryTreeview;
            //
            NewDir.PathTree = _newproject.duongdanproject;
            pathproject = _newproject.duongdanproject;
            string Pathproject = duongdanproject;
            NewDir.NameProject = _newproject._nameproject;

            DirectoryTreeview Tree = DirTreeview as DirectoryTreeview;
            Tree.PathTree = Pathproject;
            Tree.Runother();

            ExcelFileParser WorkDir = new ExcelFileParser();
            WorkDir.WorkingDir = duongdanproject;

        }

        //--- Open Project 
        private void radMenuItemOpenProject_Click(object sender, EventArgs e)
        {
            string duongdan;
            FolderBrowserDialog Chonduongdan = new FolderBrowserDialog();
            Chonduongdan.RootFolder = Environment.SpecialFolder.MyComputer;
            if (Chonduongdan.ShowDialog() == DialogResult.OK)
            {
                duongdan = Chonduongdan.SelectedPath;
                pathproject = duongdan;
                int namprojectpost = duongdan.LastIndexOf("\\");
                string valuenameproject = duongdan.Substring(namprojectpost+1);
                DirectoryTreeview NewTree = DirTreeview as DirectoryTreeview;
                NewTree.PathTree = duongdan;
                NewTree.NameProject = valuenameproject;
                duongdanproject = duongdan;
                NewTree.Runother();
                counttabpage();
            }
        }

        // RUn PRoject again

        public void RunProJectAgain()
        {
            try
            {
                duongdan = duongdanproject;
                pathproject = duongdan;
                int namprojectpost = duongdan.LastIndexOf("\\");
                string valuenameproject = duongdan.Substring(namprojectpost + 1);
                DirectoryTreeview NewTree = DirTreeview as DirectoryTreeview;
                NewTree.PathTree = duongdan;
                NewTree.NameProject = valuenameproject;
                duongdanproject = duongdan;
                NewTree.Runother();
            }
            catch
            {
            }
        }

        private void commandBarButtonPause_Click(object sender, EventArgs e)
        {

        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
        private void _Run_Click(object sender, EventArgs e)
        {
            RunScript _Run = new RunScript();
            _Run._pathproject = this.duongdanproject;
            _Run.Rundata(duongdanproject);            
            _Run.Runscript(duongdanproject);
            _Run.ShowDialog();
            _Run.ispause = ispause;
            this.RunProJectAgain();
            
            
            
        }
        public delegate void ChosseStoptHandler();
        public event ChosseStoptHandler EventStopt;

        private void _Pause_Click(object sender, EventArgs e)
        {
            Keyboard.Instance.HoldKey(White.Core.WindowsAPI.KeyboardInput.SpecialKeys.CONTROL);
            Keyboard.Instance.Enter("P");          
        
        }

        private void _Stop_Click(object sender, EventArgs e)
        {
            Keyboard.Instance.HoldKey(White.Core.WindowsAPI.KeyboardInput.SpecialKeys.CONTROL);
            Keyboard.Instance.Enter("R");          
        
        }

        private void _Save_Click(object sender, EventArgs e)
        {
            try
            {
                TabPage selectedTab = tabControl.SelectedTab;
                int selectedIndex = tabControl.SelectedIndex;
                int chilcontrol = tabControl.TabPages[selectedIndex].Controls.Count;



                string nameoftab = selectedTab.Text;
                string _pt = tabControl.duongdan[nameoftab];

                DataGridView dgv = tabControl.TabPages[selectedIndex].Controls[0] as DataGridView;

                //ExcelFileParser Dongcot = new ExcelFileParser();
                //int cot = Dongcot.returnlastcol(_pt)+1;
                // int dong = Dongcot.returnlastrow(_pt)+1;
                int cot = dgv.ColumnCount;
                int dong = dgv.RowCount;

                Workbook workbook = new Workbook();
                Worksheet worksheet = new Worksheet("First Sheet");

                for (int i = 0; i < dong; i++)
                {
                    for (int j = 0; i < cot; j++)
                    {
                        try
                        {
                            string s = dgv.Rows[i].Cells[j].Value.ToString();
                            worksheet.Cells[i, j] = new Cell(s);
                        }
                        catch
                        {
                            worksheet.Cells[i, j] = new Cell("");
                            break;
                        }

                    }
                }

                string file = _pt;
                worksheet.Cells.ColumnWidth[0, 1] = 3000;
                workbook.Worksheets.Add(worksheet);
                workbook.Save(file);

            }
            catch
            {
            }



        }

        private void _OpenProject_Click(object sender, EventArgs e)
        {

            Open_File();
            
        }

        private void _NewProject_Click(object sender, EventArgs e)
        {

        }

        private void _Spy_Click(object sender, EventArgs e)
        {
            Spy _newspy = new Spy();
            _newspy.Show();
        }

        private void radMenuItem7_Click(object sender, EventArgs e)
        {

        }

        private void radMenuItem15_Click(object sender, EventArgs e)
        {
            RunScript _Run = new RunScript();
            _Run._pathproject = this.duongdanproject;
            _Run.Rundata(duongdanproject);
            _Run.Runscript(duongdanproject);
            _Run.ShowDialog();
            _Run.ispause = ispause;
            this.RunProJectAgain();
        }

        private void _M_Save_Click(object sender, EventArgs e)
        {
            save();
        }

        public void save()
        {
            try
            {
                TabPage selectedTab = tabControl.SelectedTab;
                int selectedIndex = tabControl.SelectedIndex;
                int chilcontrol = tabControl.TabPages[selectedIndex].Controls.Count;



                string nameoftab = selectedTab.Text;
                string _pt = tabControl.duongdan[nameoftab];

                DataGridView dgv = tabControl.TabPages[selectedIndex].Controls[0] as DataGridView;

                //ExcelFileParser Dongcot = new ExcelFileParser();
                //int cot = Dongcot.returnlastcol(_pt)+1;
                // int dong = Dongcot.returnlastrow(_pt)+1;
                int cot = dgv.ColumnCount;
                int dong = dgv.RowCount;

                Workbook workbook = new Workbook();
                Worksheet worksheet = new Worksheet("First Sheet");

                for (int i = 0; i < dong; i++)
                {
                    for (int j = 0; i < cot; j++)
                    {
                        try
                        {
                            string s = dgv.Rows[i].Cells[j].Value.ToString();
                            worksheet.Cells[i, j] = new Cell(s);
                        }
                        catch
                        {
                            worksheet.Cells[i, j] = new Cell("");
                            break;
                        }

                    }
                }

                string file = _pt;
                worksheet.Cells.ColumnWidth[0, 1] = 3000;
                workbook.Worksheets.Add(worksheet);
                workbook.Save(file);

            }
            catch
            {
            }
        }

        private void radMenuItem8_Click(object sender, EventArgs e)
        {
            
        }

        private void radMenuItem3_Click(object sender, EventArgs e)
        {
            newproject();
        }

        private void radMenuItem5_Click(object sender, EventArgs e)
        {
            Open_File();
        }

        private void _openproject_Click_1(object sender, EventArgs e)
        {
            string duongdan;

            PropertyGridBrowseEditor editor = new PropertyGridBrowseEditor();
            RadBrowseEditorElement element = editor.EditorElement as RadBrowseEditorElement;
            element.DialogType = BrowseEditorDialogType.FolderBrowseDialog;

            FolderBrowserDialog Chonduongdan = new FolderBrowserDialog();
            Chonduongdan.RootFolder = Environment.SpecialFolder.MyComputer;
            if (Chonduongdan.ShowDialog() == DialogResult.OK)
            {
                duongdan = Chonduongdan.SelectedPath;
                pathproject = duongdan;
                int namprojectpost = duongdan.LastIndexOf("\\");
                string valuenameproject = duongdan.Substring(namprojectpost + 1);
                DirectoryTreeview NewTree = DirTreeview as DirectoryTreeview;
                NewTree.PathTree = duongdan;
                NewTree.NameProject = valuenameproject;
                duongdanproject = duongdan;
                NewTree.Runother();
                counttabpage();
            }
        }

        private void _M_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radMenuItem16_Click(object sender, EventArgs e)
        {

        }

        private void radMenuItem17_Click(object sender, EventArgs e)
        {

        }
        
    }
}
