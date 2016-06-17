using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using Telerik;
using System.Drawing;
using Excel = Microsoft.Office.Interop.Excel;
//-------------------------------------------
using QiHe.CodeLib;
using ExcelLibrary.CompoundDocumentFormat;
using ExcelLibrary.BinaryDrawingFormat;
using ExcelLibrary.BinaryFileFormat;
using ExcelLibrary.SpreadSheet;
//-------------------------------------------

using White.Core.Factory;
using White.Core.UIItems.Finders;
using White.Core.InputDevices;


//-------------------------------------------

namespace AUI_Test
{


    public class DirectoryTreeview : TreeView
    {



        //Tao Envent Mo
        public event SelectOpenHandler SelectOpen;

        public string path;
        public string duongdanfulladd;
        public string duongdanfullselectopen;
        public string fullpathadd
        {
            set
            {
                duongdanfulladd = value;
            }
        }

        public string fullpathselectopen
        {
            set
            {
                duongdanfullselectopen = value;
            }
        }

        public string PathTree
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

        //---------------------------------------

        //Ten cua file can add
        public string _nameaddfile;
        public string Nameaddfile
        {
            set
            {
                _nameaddfile = value;
            }
        }

        //Duong dan cua file can add

        public string pathaddfile;
        public string pathfileadd
        {
            set
            {
                pathaddfile = value;
            }
        }

        // Name Project

        public string _nameproject;
        public string NameProject
        {
            set
            {
                _nameproject = value;
            }
        }





        //---- Tao Mot Context menu

        public myContextMenuStrip myMenu;


        public DirectoryTreeview()
        {
            Nodes.Clear();
            System.Windows.Forms.ImageList myImageList = new System.Windows.Forms.ImageList();
            myImageList.Images.Add(Properties.Resources.iconsubtree);
            myImageList.Images.Add(Properties.Resources.icontree);


            // Assign the ImageList to the TreeView.
            ImageList = myImageList;

            // Set the TreeView control's default image and selected image indexes.
            ImageIndex = 0;
            SelectedImageIndex = 1;
            ExpandAll();
            Dock = DockStyle.Fill;   

            this.DoubleClick += DirectoryTreeview_DoubleClick;
        }

        void DirectoryTreeview_DoubleClick(object sender, EventArgs e)
        {
            TreeView tree = sender as TreeView;
            if (tree.SelectedNode.Level == 2)
            {
                string path_duoble = tree.SelectedNode.FullPath;
                path_duoble = path_duoble.Remove(0, 4);
                path_duoble = path + path_duoble;

                int index = path_duoble.LastIndexOf("\\");
                string nameopen = path_duoble.Substring(index + 1);
                if (this.SelectOpen != null)
                {
                    SelectOpen(path_duoble, nameopen);
                }
            }
           
        }


        // chạy lại một lần nữa khi mình tạo new với đường dẫn mới.

        public void Runother()
        {
            try
            {
                Nodes.Clear();
                string drives = Path.Combine(Environment.CurrentDirectory, path);
                string RootNameDirection = _nameproject;
                TreeNode node = new TreeNode();
                node.Text = RootNameDirection;
                Nodes.Add(node);
                FillDirectory(drives, node, 0);

                System.Windows.Forms.ImageList myImageList = new System.Windows.Forms.ImageList();
                myImageList.Images.Add(Properties.Resources.iconsubtree);
                myImageList.Images.Add(Properties.Resources.icontree);


                // Assign the ImageList to the TreeView.
                ImageList = myImageList;

                // Set the TreeView control's default image and selected image indexes.
                ImageIndex = 0;
                SelectedImageIndex = 1;
                Dock = DockStyle.Fill;
                //--------------------------------------

                //---------Dua Con Text Menu Vao -----------------------------------
                myMenu = new myContextMenuStrip();
                ToolStripMenuItem ADD = new ToolStripMenuItem("Add");
                ADD.Image = Properties.Resources.Add;
                ToolStripMenuItem Del = new ToolStripMenuItem("Delete");
                Del.Image = Properties.Resources.delete;
                ToolStripMenuItem Open = new ToolStripMenuItem("Open");
                Open.Image = Properties.Resources.openfile;
                myMenu.Items.AddRange(new ToolStripItem[] { Open, ADD, Del });
                ContextMenuStrip = myMenu;
                Open.Click += Open_Click;
                this.ExpandAll();
            }
            catch
            {
            }

        }

        public void FillDirectory(string drv, TreeNode parent, int level)
        {
            try
            {

                level++;
                if (level > 4)
                    return;
                DirectoryInfo dir = new DirectoryInfo(drv);
                if (!dir.Exists)
                    throw new DirectoryNotFoundException
                        ("directory does not exist:" + drv);
                this.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.rightclick);
                foreach (DirectoryInfo di in dir.GetDirectories())
                {
                    TreeNode child = new TreeNode();
                    child.Text = di.Name;
                    parent.Nodes.Add(child);
                    foreach (FileInfo fi in di.GetFiles())
                    {
                        child.Nodes.Add(fi.Name);
                    }

                    FillDirectory(child.FullPath, child, level);

                }



            }

            catch (Exception ex)
            {
                ex.ToString();
            }

        }



        private void rightclick(object sender, TreeNodeMouseClickEventArgs e)
        {
                
            if (e.Button == MouseButtons.Right)
            {

                myMenu = new myContextMenuStrip();
                ToolStripMenuItem ADD = new ToolStripMenuItem("Add");
                ADD.Image = Properties.Resources.Add;
                ToolStripMenuItem Del = new ToolStripMenuItem("Delete");
                Del.Image = Properties.Resources.delete;
                ToolStripMenuItem Open = new ToolStripMenuItem("Open");
                Open.Image = Properties.Resources.openfile;
                myMenu.Items.AddRange(new ToolStripItem[] { Open, ADD, Del });
                ContextMenuStrip = myMenu;
                Open.Click += Open_Click;
                ADD.Click += ADD_Click;
                Del.Click += Del_Click;

                if (e.Node.Level == 0)
                {
                    ADD.Visible = false;
                    Open.Visible = false;
                    Del.Visible = false;
                }
                else if (e.Node.Level == 1)
                {
                    ADD.Visible = true;
                    Open.Visible = false;
                    Del.Visible = false;
                    //Xử Lý Duoc Duong Dan 

                    string duongdanselecadd = e.Node.FullPath.ToString();
                    pathfileadd = duongdanselecadd;

                    if (pathaddfile.Contains(_nameproject))
                    {
                        int index = pathaddfile.LastIndexOf("\\");
                        string s = pathaddfile.Substring(index);
                        //pathaddfile = pathaddfile.Remove(0, 4);
                        pathaddfile = path + s;
                    }

                    

                }
                else if (e.Node.Level == 2)
                {
                    ADD.Visible = false;
                    Open.Visible = true;
                    Del.Visible = true;

                    // Minh xu ly duong dan 

                    string duongdanselectopen = e.Node.FullPath.ToString();

                    fullpathselectopen = duongdanselectopen;
                    if (duongdanfullselectopen.Contains(_nameproject))
                    {
                        duongdanfullselectopen = duongdanfullselectopen.Remove(0, 4);
                        duongdanfullselectopen = path + duongdanfullselectopen;
                    }
                    // double click
                    
                }

            }



        }

        

        void Del_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do You Want Delete File ?", "The Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                int index = duongdanfullselectopen.LastIndexOf(@"\");
                if (index != -1)
                {

                    string nodecantim = duongdanfullselectopen.Substring(index + 1);
                    int countlengthnameproject = _nameproject.Length;
                    int vitrisubstringafter = duongdanfullselectopen.IndexOf(_nameproject );
                    string subtring = duongdanfullselectopen.Substring(vitrisubstringafter + countlengthnameproject + 1);
                    // cat mang

                    string [] nodetrotoi = subtring.Split('\\');
                    string nodechuottrotoi = nodetrotoi[0];
                    int countnode = Nodes[0].Nodes.Count;
                    for (int i = 0; i < countnode; i++)
                    {
                        if (Nodes[0].Nodes[i].Text == nodechuottrotoi)
                        {

                            int coutnchil = Nodes[0].Nodes[i].Nodes.Count;

                            for (int j = 0; j < coutnchil; j++)
                            {
                                if (Nodes[0].Nodes[i].Nodes[j].Text == nodecantim)
                                {
                                    Nodes[0].Nodes[i].Nodes[j].Remove();
                                    File.Delete(duongdanfullselectopen);
                                    break;

                                }
                            }
                        }
                    }
                    

                }
               
            }
        }

        void Open_Click(object sender, EventArgs e)
        {
            // phai xu ly duoc dua cai ten file qua ben kia luon
            int index = duongdanfullselectopen.LastIndexOf("\\");
            string nameopen = duongdanfullselectopen.Substring(index+1);
            if (this.SelectOpen != null)
            {
                SelectOpen(duongdanfullselectopen, nameopen);
            }
        }

        void ADD_Click(object sender, EventArgs e)
        {
            NewFile _newfile = new NewFile();
            _newfile.ShowDialog();
            Nameaddfile = _newfile.name;
            fullpathadd = pathaddfile + @"\" + _nameaddfile+".xls";
            //---- xu ly coi no o node nao moi dc

            int index = pathaddfile.LastIndexOf(@"\");
            if (index != -1)
            {

               string nodecantim =  pathaddfile.Substring(index+1);
               int countnode = Nodes[0].Nodes.Count;
               for (int i = 0; i < countnode; i++)
               {
                   if (Nodes[0].Nodes[i].Text == nodecantim)
                   {
                       Nodes[0].Nodes[i].Nodes.Add(_nameaddfile+".xls");
                   }
               }
                   
            }

            //create new xls file
            string file = duongdanfulladd;
            Workbook workbook = new Workbook();
            Worksheet worksheet = new Worksheet(_nameaddfile);
            worksheet.Cells.ColumnWidth[0, 1] = 3000;
            workbook.Worksheets.Add(worksheet);
            workbook.Save(file);
            
        }







        public delegate void SelectOpenHandler(string duongdan, string name);


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