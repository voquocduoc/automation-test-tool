using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AUI_Test
{
    public delegate void ControlSelectedHandler(string name);

    public partial class MatchesViewer : UserControl
    {
        public MatchesViewer()
        {
            InitializeComponent();
        }

        public void Reset()
        {
            listView.Items.Clear();
        }

        public event ControlSelectedHandler ControlSelected;

        Dictionary<string, TestStack.White.UIItems.IUIItem> matchedControls;
        public Dictionary<string, TestStack.White.UIItems.IUIItem> MatchedControls
        {
            set
            {
                listView.Items.Clear();
                matchedControls = value;

                foreach (string name in value.Keys)
                {
                    ListViewItem item = listView.Items.Add(name);
                    if (value[name] == null)
                    {
                        item.ForeColor = Color.Red;
                        item.Font = new Font(SystemFonts.DefaultFont, FontStyle.Bold);
                    }
                    else
                        item.ForeColor = Color.Green;
                }
            }
        }

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ControlSelected != null && listView.SelectedItems.Count > 0)
            {
                ListViewItem item = listView.SelectedItems[0];
                if (matchedControls[item.Text] != null)
                    ControlSelected(item.Text);
            }
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
