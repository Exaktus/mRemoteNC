using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using mRemoteNC.App;
using mRemoteNC.Tools;
using My;
using WeifenLuo.WinFormsUI.Docking;
using Type = mRemoteNC.UI.Window.Type;

namespace mRemoteNC.Forms
{
    public partial class QuickTextEdit : UI.Window.Base
    {
        public QuickTextEdit()
        {
            InitializeComponent();
        }

        public QuickTextEdit(DockContent Panel)
        {
            this.WindowType = Type.QuickText;
            this.DockPnl = Panel;
            this.InitializeComponent();
        }

        private void QuickTextEdit_Load(object sender, EventArgs e)
        {
            txtDisplayName.LostFocus += new EventHandler(txtDisplayName_LostFocus);
            txtFilename.LostFocus += new EventHandler(txtDisplayName_LostFocus);
            LoadQT();
        }

        private void txtDisplayName_LostFocus(object sender, EventArgs e)
        {
            SetAppProperties(_SelApp);
        }

        private void cMenAppsAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Tools.QuickText nExtA = new Tools.QuickText();
                Runtime.QuickTexts.Add(nExtA);
                LoadQT();
                lvApps.SelectedItems.Clear();

                foreach (ListViewItem lvItem in lvApps.Items)
                {
                    if (lvItem.Tag == nExtA)
                    {
                        lvItem.Selected = true;
                        txtDisplayName.Focus();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    (string)
                                                    ("AddNewApp failed (UI.Window.ExternalApps)" +
                                                        Constants.vbNewLine + ex.Message), true);
            }
        }

        private void RemoveApps()
        {
            try
            {
                string Prompt = "";
                if (lvApps.SelectedItems.Count < 1)
                {
                    return;
                }
                else if (lvApps.SelectedItems.Count == 1)
                {
                    Prompt = string.Format(Language.strConfirmDeleteExternalTool, lvApps.SelectedItems[0].Text);
                }
                else if (lvApps.SelectedItems.Count > 1)
                {
                    Prompt = string.Format(Language.strConfirmDeleteExternalToolMultiple,
                                           lvApps.SelectedItems.Count);
                }

                if (Interaction.MsgBox(Prompt, MsgBoxStyle.Question | MsgBoxStyle.YesNo, null) ==
                    MsgBoxResult.Yes)
                {
                    foreach (ListViewItem lvItem in lvApps.SelectedItems)
                    {
                        Runtime.QuickTexts.Remove((QuickText)lvItem.Tag);
                        lvItem.Tag = null;
                        lvItem.Remove();
                    }

                    RefreshToolbar();
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    (string)
                                                    ("RemoveApps failed (UI.Window.ExternalApps)" +
                                                     Constants.vbNewLine + ex.Message), true);
            }
        }

        private Tools.QuickText _SelApp;

        private void RefreshToolbar()
        {
            frmMain.Default.AddQuickTextsToToolBar();
        }

        private void LoadQT()
        {
            try
            {
                lvApps.Items.Clear();

                foreach (Tools.QuickText extA in Runtime.QuickTexts)
                {
                    ListViewItem lvItem = new ListViewItem();
                    lvItem.Text = (string)extA.DisplayName;
                    lvItem.SubItems.Add(extA.Text);
                    lvItem.Tag = extA;

                    lvApps.Items.Add(lvItem);

                    if (_SelApp != null)
                    {
                        if (extA == _SelApp)
                        {
                            lvItem.Selected = true;
                        }
                    }
                }

                RefreshToolbar();
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    (string)
                                                    ("LoadApps failed (UI.Window.ExternalApps)" +
                                                     Constants.vbNewLine + ex.Message), true);
            }
        }

        private void GetAppProperties(Tools.QuickText SelApp)
        {
            try
            {
                if (SelApp != null)
                {
                    txtDisplayName.Text = (string)SelApp.DisplayName;
                    txtFilename.Text = (string)SelApp.Text;
                    _SelApp = SelApp;
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    (string)
                                                    ("GetAppProperties failed (UI.Window.ExternalApps)" +
                                                     Constants.vbNewLine + ex.Message), true);
            }
        }

        private void SetAppProperties(Tools.QuickText SelApp)
        {
            try
            {
                if (SelApp != null)
                {
                    SelApp.DisplayName = txtDisplayName.Text;
                    SelApp.Text = txtFilename.Text;

                    LoadQT();
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    (string)
                                                    ("SetAppProperties failed (UI.Window.QT)" +
                                                     Constants.vbNewLine + ex.Message), true);
            }
        }

        private void cMenAppsRemove_Click(object sender, EventArgs e)
        {
            RemoveApps();
        }

        private void lvApps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvApps.SelectedItems.Count == 1)
            {
                this.grpEditor.Enabled = true;
                GetAppProperties((QuickText)lvApps.SelectedItems[0].Tag);
            }
            else
            {
                this.grpEditor.Enabled = false;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                var cm = new ContextMenu();
                var ls = new List<string>(){"%Password%", "%Login%", "%AltSpace%"};
                foreach (var l in ls)
                {
                    cm.MenuItems.Add(new MenuItem(l, (o, args) => { txtFilename.Text += ((MenuItem)o).Text;
                                                                      txtDisplayName_LostFocus(null,null);
                    }));
                }
                Point pos = this.PointToClient(Cursor.Position);
                cm.Show(this, pos, LeftRightAlignment.Right);

            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    (string)
                                                    ("btnBrowse_Click failed (UI.Window.QT)" +
                                                     Constants.vbNewLine + ex.Message), true);
            }
        }
    }
}