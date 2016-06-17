using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Automation;

using TestStack.White.Factory;
using TestStack.White.UIItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.Finders;



namespace AUI_Test
{
    public delegate void SelectedItemChangedHandler(IUIItem item, string name);
    public delegate void WindowsRefreshedHandler(Dictionary<string, IUIItem> matchedControls);

    public partial class WindowsViewer : UserControl
    {
        class TreeNodeGroup : TreeNode
        {
            public TreeNodeGroup(string text) : base(text) { }
        }

        class WindowTreeNode : TreeNode
        {
            public WindowTreeNode(string text) : base(text) { }

            public string MatchedName { get; set; }
        }

        class ControlTreeNode : TreeNode
        {
            public ControlTreeNode(string text) : base(text) { }

            public string MatchedName { get; set; }
        }

        public event SelectedItemChangedHandler SelectedItemChanged;
        public event WindowsRefreshedHandler WindowsRefreshed;

        public WindowsViewer()
        {
            InitializeComponent();
            
        }

        private void treeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (!(e.Node.Tag is Window))
                return;

            if (e.Node.IsExpanded)
                ShowControl(e.Node);
        }

        private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (!(e.Node.Tag is Window))
                return;

            e.Node.Nodes.Clear();
            e.Node.Nodes.Add(@"Scanning...");
        }

        private void treeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {

        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node is ControlTreeNode && SelectedItemChanged != null)
                SelectedItemChanged(e.Node.Tag as UIItem, (e.Node as ControlTreeNode).MatchedName);
        }

        public UIAActionManager ActionManager { get; set; }
        public ABT.Model.IInterface CurrentInterface { get; set; }
        Dictionary<string, IUIItem> matchedControls = new Dictionary<string, IUIItem>();

        TreeNode MatchedWindowNode { get; set; }

        public void RefreshWindows()
        {
            MatchedWindowNode = ShowWindow();
            bool bFound = FindMatchControls(MatchedWindowNode);
            if (bFound && MatchedWindowNode != null)
                MatchedWindowNode.Expand();

            if (WindowsRefreshed != null)
                WindowsRefreshed(matchedControls);
        }

        private bool FindMatchControls(TreeNode matchedWindowNode)
        {
            matchedControls.Clear();

            if (CurrentInterface == null)
                return false;

            bool bFound = false;
            if (matchedWindowNode != null)
            {
                Window window = matchedWindowNode.Tag as Window;
                foreach (string name in CurrentInterface.Controls.Keys)
                {
                    IUIItem item = ActionManager.FindControl(window, CurrentInterface.Controls[name]);
                    if (item != null)
                    {
                        matchedControls[name] = item;
                        bFound = true;
                    }
                    else
                        matchedControls[name] = null;
                }
            }

            return bFound;
        }

        private TreeNode ShowWindow()
        {
            treeView.Nodes.Clear();

            List<Window> windows = WindowFactory.Desktop.DesktopWindows();
            System.Diagnostics.ProcessStartInfo si = new System.Diagnostics.ProcessStartInfo();
            si.FileName = @"C:\Windows\explorer.exe";
            TestStack.White.Application appExplorer = TestStack.White.Application.AttachOrLaunch(si);
            System.Diagnostics.Process appCurrent = System.Diagnostics.Process.GetCurrentProcess();

            TreeNode matchedWindowNode = null;
            foreach (Window window in windows)
            {
                if (window.IsOffScreen || !window.Visible || window.Name.Length == 0)
                    continue;

                if (window.AutomationElement.Current.ProcessId == appCurrent.Id)
                    continue;

                if (window.Title == @"Program Manager" && window.AutomationElement.Current.ProcessId == appExplorer.Process.Id)
                    continue;

                ControlTreeNode node = new ControlTreeNode(window.Name);
                node.Tag = window;
                node.Nodes.Add("");
                if (matchedWindowNode == null && CurrentInterface != null && ActionManager.MatchWindow(window, CurrentInterface.Properties))
                {
                    matchedWindowNode = node;
                    node.ForeColor = Color.Green;
                    node.NodeFont = new Font(SystemFonts.DefaultFont, FontStyle.Bold);
                    node.Text = node.Text;
                    node.MatchedName = CurrentInterface.Name;
                    treeView.SelectedNode = node;
                }
                treeView.Nodes.Add(node);
            }

            return matchedWindowNode;
        }

        private bool SelectControlByName(TreeNode parent, string name)
        {
            foreach (TreeNode node in parent.Nodes)
            {
                if (node is ControlTreeNode && (node as ControlTreeNode).MatchedName == name)
                {
                    treeView.Focus();
                    node.EnsureVisible();
                    treeView.SelectedNode = node;
                    return true;
                }
                else if (SelectControlByName(node, name))
                    return true;
            }
            return false;
        }
        public void SelectControlByName(string name)
        {
            SelectControlByName(MatchedWindowNode, name);
        }

        private int CompareItem(IUIItem item1, IUIItem item2)
        {
            if (item1 == null)
                return -1;
            else if (item2 == null)
                return 1;
            else
                return item1.AutomationElement.Current.AutomationId.CompareTo(item2.AutomationElement.Current.AutomationId);
        }

        private void ShowGroupControl(TreeNode node, string groupName, IUIItem[] items)
        {
            if (items.Length == 0)
                return;

            treeView.BeginUpdate();
            TreeNode group = new TreeNodeGroup(groupName);
            node.Nodes.Add(group);
            group.NodeFont = new Font(SystemFonts.DefaultFont, FontStyle.Italic);
            group.ForeColor = Color.Gray;
            foreach (IUIItem item in items)
            {
                string name = item.Name.Trim();
                if (name.Length == 0) name = "{No name}";
                ControlTreeNode itemnode = new ControlTreeNode(name);
                itemnode.Tag = item;

                foreach (string ctrlName in matchedControls.Keys)
                {
                    if (CompareItem(matchedControls[ctrlName], item) == 0)
                    {
                        itemnode.NodeFont = new Font(SystemFonts.DefaultFont, FontStyle.Bold);
                        itemnode.ForeColor = Color.Green;
                        itemnode.Text = itemnode.Text;
                        itemnode.MatchedName = ctrlName;
                    }
                }
                group.Nodes.Add(itemnode);
            }
            treeView.EndUpdate();
        }

        private void ShowControl(TreeNode node)
        {
            Window window = node.Tag as Window;
            if (window == null)
                return;

            Cursor = Cursors.WaitCursor;

            // search for children
            SearchCriteria crit = SearchCriteria.ByControlType(ControlType.Button);
            IUIItem[] buttons = window.GetMultiple(crit);
            crit = SearchCriteria.ByControlType(ControlType.Text);
            IUIItem[] edits = window.GetMultiple(crit);
            crit = SearchCriteria.ByControlType(ControlType.CheckBox);
            IUIItem[] checkboxes = window.GetMultiple(crit);
            crit = SearchCriteria.ByControlType(ControlType.RadioButton);
            IUIItem[] Radiobutton = window.GetMultiple(crit);
            crit = SearchCriteria.ByControlType(ControlType.ComboBox);
            IUIItem[] ComboBox = window.GetMultiple(crit);
            crit = SearchCriteria.ByControlType(ControlType.MenuItem);
            IUIItem[] MenuItem = window.GetMultiple(crit);
            crit = SearchCriteria.ByControlType(ControlType.ToolTip);
            IUIItem[] ToolTip = window.GetMultiple(crit);
            crit = SearchCriteria.ByControlType(ControlType.DataGrid);
            IUIItem[] DataGrid = window.GetMultiple(crit);


            node.Nodes.Clear();

            // add child nodes
            ShowGroupControl(node, "[Buttons]", buttons);
            ShowGroupControl(node, "[Textboxes]", edits);
            ShowGroupControl(node, "[Checkboxes]", checkboxes);
            ShowGroupControl(node, "[Radiobutton]", Radiobutton);
            ShowGroupControl(node, "[ComboBox]", ComboBox);
            ShowGroupControl(node, "[MenuItem]", MenuItem);
            ShowGroupControl(node, "[ToolTip]", ToolTip);
            ShowGroupControl(node, "[DataGrid]", DataGrid);

            Cursor = Cursors.Arrow;
        }



    }
}
