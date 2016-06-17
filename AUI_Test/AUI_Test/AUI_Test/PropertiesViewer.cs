using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using TestStack.White.UIItems;
using System.Reflection;
using System.Windows.Automation;

namespace AUI_Test
{
    public delegate void InvalidItemHandler();

    public partial class PropertiesViewer : UserControl
    {
        private IUIItem m_Object;

        /// <summary>
        /// constructor
        /// </summary>
        public PropertiesViewer()
        {
            InitializeComponent();
            CurrentName = null;
        }

        /// <summary>
        /// event - current "Object" is not valid any more
        /// </summary>
        public event InvalidItemHandler InvalidItemSelected;

        /// <summary>
        /// current Object's name
        /// </summary>
        public string CurrentName { get; set; }

        private string CandidateName { get; set; }

        /// <summary>
        /// current ActionManager - used to match controls
        /// </summary>
        public UIAActionManager ActionManager { get; set; }

        /// <summary>
        /// current Interface used for matching
        /// </summary>
        public ABT.Model.IInterface CurrentInterface { get; set; }

        /// <summary>
        /// the Object to show properties, it may be a window, or a control
        /// </summary>
        public IUIItem Object
        {
            get { return m_Object; }
            set
            {
                m_Object = value;
                Reset();

                if (m_Object != null)
                {
                    if (ShowProperties() == false)
                    {
                        Reset();

                        // if cannot show property, we consider the Object is invalid
                        if (InvalidItemSelected != null)
                            InvalidItemSelected();
                    }
                }
            }
        }

        /// <summary>
        /// reset the property list
        /// </summary>
        public void Reset()
        {
            listView.Items.Clear();
        }

        /// <summary>
        /// show properties of an item - use Reflection technology
        /// </summary>
        /// <returns></returns>
        private bool ShowProperties()
        {
            AutomationElement.AutomationElementInformation source = Object.AutomationElement.Current;
            //(Object as TestStack.White.UIItems.WindowItems.Window).
            // get all property methods of Object - use Reflection
            PropertyInfo[] properties = source.GetType().GetProperties();
            bool ret = true;

            try
            {
                // currently stop updating the list
                listView.BeginUpdate();

                // if current object is not matched
                if (CurrentInterface == null || CurrentName == null)
                {
                    CandidateName = "";

                    // loop for each property
                    foreach (PropertyInfo prop in properties)
                    {
                        // get property name and value
                        string strName = prop.Name;
                        object objVal = prop.GetValue(source);

                        // create a corresponding item in the list
                        if (objVal != null)
                        {
                            ListViewItem item = new ListViewItem(strName);
                            string strVal = objVal.ToString();
                            if (objVal is System.Windows.Automation.ControlType)
                            {
                                System.Windows.Automation.ControlType c = objVal as System.Windows.Automation.ControlType;
                                strVal = c.ProgrammaticName.Substring(c.ProgrammaticName.IndexOf('.') + 1);
                            }
                            item.SubItems.Add(strVal);
                            listView.Items.Add(item);

                            if (strName.Equals(ABT.Auto.Constants.PropertyNames.AutomationId, StringComparison.CurrentCultureIgnoreCase))
                                CandidateName = CandidateName + strVal;
                            else if (strName.Equals(ABT.Auto.Constants.PropertyNames.ControlType, StringComparison.CurrentCultureIgnoreCase))
                                CandidateName = strVal + CandidateName;

                        }
                    }
                }
                else if (Object is TestStack.White.UIItems.WindowItems.Window)
                {
                    // loop for each property
                    foreach (PropertyInfo prop in properties)
                    {
                        // get property name and value
                        string strName = prop.Name;
                        object objVal = prop.GetValue(source);

                        // create a corresponding item in the list
                        if (objVal != null)
                        {
                            ListViewItem item = new ListViewItem(strName);
                            string strVal = objVal.ToString();
                            if (objVal is System.Windows.Automation.ControlType)
                            {
                                System.Windows.Automation.ControlType c = objVal as System.Windows.Automation.ControlType;
                                strVal = c.ProgrammaticName.Substring(c.ProgrammaticName.IndexOf('.') + 1);
                            }
                            item.SubItems.Add(strVal);

                            if (Object is TestStack.White.UIItems.WindowItems.Window &&
                                CurrentInterface.Name.Equals(CurrentName, StringComparison.CurrentCultureIgnoreCase) &&
                                CurrentInterface.Properties.ContainsKey(strName.ToLower()))
                            {
                                item.Checked = true;
                                item.Font = new Font(SystemFonts.DefaultFont, FontStyle.Bold);
                                listView.Items.Insert(0, item);
                            }
                            else // just show the property
                                listView.Items.Add(item);
                        }
                    }
                }
                else
                {
                    // loop for each property
                    foreach (PropertyInfo prop in properties)
                    {
                        // get property name and value
                        string strName = prop.Name;
                        object objVal = prop.GetValue(source);

                        // create a corresponding item in the list
                        if (objVal != null)
                        {
                            ListViewItem item = new ListViewItem(strName);
                            string strVal = objVal.ToString();
                            if (objVal is System.Windows.Automation.ControlType)
                            {
                                System.Windows.Automation.ControlType c = objVal as System.Windows.Automation.ControlType;
                                strVal = c.ProgrammaticName.Substring(c.ProgrammaticName.IndexOf('.') + 1);
                            }
                            item.SubItems.Add(strVal);

                            if (CurrentInterface.Controls.ContainsKey(CurrentName) &&
                                CurrentInterface.Controls[CurrentName].ContainsKey(strName.ToLower()))
                            {
                                item.Checked = true;
                                item.Font = new Font(SystemFonts.DefaultFont, FontStyle.Bold);
                                listView.Items.Insert(0, item);
                            }
                            else // just show the property
                                listView.Items.Add(item);
                        }
                    }
                }
            }
            catch (InvalidCastException)
            {
                // some error occured, we cannot show the properties, so the Object is considered as invalid
                ret = false;
            }
            finally
            {
                // resume updating the list
                listView.EndUpdate();
            }

            return ret;
        }

        private void listView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (CurrentInterface != null)
            {
                if (CurrentName != null)
                {
                    if (Object is TestStack.White.UIItems.WindowItems.Window)
                    {
                        if (e.Item.Checked)
                            CurrentInterface.Properties[e.Item.Text.ToLower()] = e.Item.SubItems[1].Text;
                        else
                            CurrentInterface.Properties.Remove(e.Item.Text.ToLower());
                    }
                    else
                    {
                        if (e.Item.Checked)
                            CurrentInterface.Controls[CurrentName][e.Item.Text.ToLower()] = e.Item.SubItems[1].Text;
                        else
                            CurrentInterface.Controls[CurrentName].Remove(e.Item.Text.ToLower());
                    }
                }
                else
                {
                    if (e.Item.Checked)
                    {
                        CurrentName = CandidateName;
                        Dictionary<string, string> properties = new Dictionary<string, string>();
                        properties[e.Item.Text.ToLower()] = e.Item.SubItems[1].Text;
                        CurrentInterface.Controls[CurrentName] = properties;
                    }
                }
            }
        }

        private void listView_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (CurrentName != null)
            {
                if (listView.CheckedIndices.Count == 1 && listView.CheckedIndices.Contains(e.Index))
                    e.NewValue = CheckState.Checked;
            }
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
