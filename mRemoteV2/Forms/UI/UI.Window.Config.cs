using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using AxMSTSCLib;
using AxWFICALib;
using Microsoft.VisualBasic;
using mRemoteNC;
using mRemoteNC.App;
using mRemoteNC.Connection;
using My;
using WeifenLuo.WinFormsUI.Docking;
using Icon = System.Drawing.Icon;

//using mRemoteNC.Runtime;

namespace mRemoteNC
{
    namespace UI
    {
        namespace Window
        {
            public class Config : Base
            {
                #region Form Init

                internal System.Windows.Forms.ToolStripButton btnShowProperties;
                internal System.Windows.Forms.ToolStripButton btnShowDefaultProperties;
                internal System.Windows.Forms.ToolStripButton btnShowInheritance;
                internal System.Windows.Forms.ToolStripButton btnShowDefaultInheritance;
                internal System.Windows.Forms.ToolStripButton btnIcon;
                internal System.Windows.Forms.ToolStripButton btnHostStatus;
                internal System.Windows.Forms.ContextMenuStrip cMenIcons;
                private System.ComponentModel.Container components = null;
                internal Azuria.Common.Controls.FilteredPropertyGrid pGrid;

                private void InitializeComponent()
                {
                    this.components = new System.ComponentModel.Container();
                    this.Load += new System.EventHandler(this.Config_Load);
                    this.pGrid = new Azuria.Common.Controls.FilteredPropertyGrid();
                    this.pGrid.PropertyValueChanged +=
                        new System.Windows.Forms.PropertyValueChangedEventHandler(this.pGrid_PropertyValueChanged);
                    this.btnShowInheritance = new System.Windows.Forms.ToolStripButton();
                    this.btnShowInheritance.Click += new System.EventHandler(this.btnShowInheritance_Click);
                    this.btnShowDefaultInheritance = new System.Windows.Forms.ToolStripButton();
                    this.btnShowDefaultInheritance.Click += new System.EventHandler(this.btnShowDefaultInheritance_Click);
                    this.btnShowProperties = new System.Windows.Forms.ToolStripButton();
                    this.btnShowProperties.Click += new System.EventHandler(this.btnShowProperties_Click);
                    this.btnShowDefaultProperties = new System.Windows.Forms.ToolStripButton();
                    this.btnShowDefaultProperties.Click += new System.EventHandler(this.btnShowDefaultProperties_Click);
                    this.btnIcon = new System.Windows.Forms.ToolStripButton();
                    this.btnIcon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnIcon_Click);
                    this.btnHostStatus = new System.Windows.Forms.ToolStripButton();
                    this.btnHostStatus.Click += new System.EventHandler(this.btnHostStatus_Click);
                    this.cMenIcons = new System.Windows.Forms.ContextMenuStrip(this.components);
                    this.SuspendLayout();
                    //
                    //pGrid
                    //
                    this.pGrid.Anchor =
                        (System.Windows.Forms.AnchorStyles)
                        (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) |
                          System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
                    this.pGrid.BrowsableProperties = null;
                    this.pGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", (float)(8.25F),
                                                              System.Drawing.FontStyle.Regular,
                                                              System.Drawing.GraphicsUnit.Point, (byte)(0));
                    this.pGrid.HiddenAttributes = null;
                    this.pGrid.HiddenProperties = null;
                    this.pGrid.Location = new System.Drawing.Point(0, 0);
                    this.pGrid.Name = "pGrid";
                    this.pGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
                    this.pGrid.Size = new System.Drawing.Size(226, 530);
                    this.pGrid.TabIndex = 0;
                    this.pGrid.UseCompatibleTextRendering = true;
                    //
                    //btnShowInheritance
                    //
                    this.btnShowInheritance.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
                    this.btnShowInheritance.Image = global::My.Resources.Resources.Inheritance;
                    this.btnShowInheritance.ImageTransparentColor = System.Drawing.Color.Magenta;
                    this.btnShowInheritance.Name = "btnShowInheritance";
                    this.btnShowInheritance.Size = new System.Drawing.Size(23, 22);
                    this.btnShowInheritance.Text = "Inheritance";
                    //
                    //btnShowDefaultInheritance
                    //
                    this.btnShowDefaultInheritance.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
                    this.btnShowDefaultInheritance.Image = global::My.Resources.Resources.Inheritance_Default;
                    this.btnShowDefaultInheritance.ImageTransparentColor = System.Drawing.Color.Magenta;
                    this.btnShowDefaultInheritance.Name = "btnShowDefaultInheritance";
                    this.btnShowDefaultInheritance.Size = new System.Drawing.Size(23, 22);
                    this.btnShowDefaultInheritance.Text = "Default Inheritance";
                    //
                    //btnShowProperties
                    //
                    this.btnShowProperties.Checked = true;
                    this.btnShowProperties.CheckState = System.Windows.Forms.CheckState.Checked;
                    this.btnShowProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
                    this.btnShowProperties.Image = global::My.Resources.Resources.Properties;
                    this.btnShowProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
                    this.btnShowProperties.Name = "btnShowProperties";
                    this.btnShowProperties.Size = new System.Drawing.Size(23, 22);
                    this.btnShowProperties.Text = "Properties";
                    //
                    //btnShowDefaultProperties
                    //
                    this.btnShowDefaultProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
                    this.btnShowDefaultProperties.Image = global::My.Resources.Resources.Properties_Default;
                    this.btnShowDefaultProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
                    this.btnShowDefaultProperties.Name = "btnShowDefaultProperties";
                    this.btnShowDefaultProperties.Size = new System.Drawing.Size(23, 22);
                    this.btnShowDefaultProperties.Text = "Default Properties";
                    //
                    //btnIcon
                    //
                    this.btnIcon.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
                    this.btnIcon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
                    this.btnIcon.ImageTransparentColor = System.Drawing.Color.Magenta;
                    this.btnIcon.Name = "btnIcon";
                    this.btnIcon.Size = new System.Drawing.Size(23, 22);
                    this.btnIcon.Text = "Icon";
                    //
                    //btnHostStatus
                    //
                    this.btnHostStatus.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
                    this.btnHostStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
                    this.btnHostStatus.Image = global::My.Resources.Resources.HostStatus_Check;
                    this.btnHostStatus.ImageTransparentColor = System.Drawing.Color.Magenta;
                    this.btnHostStatus.Name = "btnHostStatus";
                    this.btnHostStatus.Size = new System.Drawing.Size(23, 22);
                    this.btnHostStatus.Tag = "checking";
                    this.btnHostStatus.Text = "Status";
                    //
                    //cMenIcons
                    //
                    this.cMenIcons.Name = "cMenIcons";
                    this.cMenIcons.Size = new System.Drawing.Size(61, 4);
                    //
                    //Config
                    //
                    this.ClientSize = new System.Drawing.Size(226, 530);
                    this.Controls.Add(this.pGrid);
                    this.Font = new System.Drawing.Font("Microsoft Sans Serif", (float)(8.25F),
                                                        System.Drawing.FontStyle.Regular,
                                                        System.Drawing.GraphicsUnit.Point, (byte)(0));
                    this.HideOnClose = true;
                    this.Icon = global::My.Resources.Resources.Config_Icon;
                    this.Name = "Config";
                    this.TabText = "Config";
                    this.Text = "Config";
                    this.ResumeLayout(false);
                }

                #endregion Form Init

                #region Private Properties

                private bool ConfigLoading;

                #endregion Private Properties

                #region Public Properties

                public bool PropertiesVisible
                {
                    get
                    {
                        if (this.btnShowProperties.Checked)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    set
                    {
                        this.btnShowProperties.Checked = value;

                        if (value == true)
                        {
                            this.btnShowInheritance.Checked = false;
                            this.btnShowDefaultInheritance.Checked = false;
                            this.btnShowDefaultProperties.Checked = false;
                        }
                    }
                }

                public bool InheritanceVisible
                {
                    get
                    {
                        if (this.btnShowInheritance.Checked)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    set
                    {
                        this.btnShowInheritance.Checked = value;

                        if (value == true)
                        {
                            this.btnShowProperties.Checked = false;
                            this.btnShowDefaultInheritance.Checked = false;
                            this.btnShowDefaultProperties.Checked = false;
                        }
                    }
                }

                public bool DefaultPropertiesVisible
                {
                    get
                    {
                        if (this.btnShowDefaultProperties.Checked)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    set
                    {
                        this.btnShowDefaultProperties.Checked = value;

                        if (value == true)
                        {
                            this.btnShowProperties.Checked = false;
                            this.btnShowDefaultInheritance.Checked = false;
                            this.btnShowInheritance.Checked = false;
                        }
                    }
                }

                public bool DefaultInheritanceVisible
                {
                    get
                    {
                        if (this.btnShowDefaultInheritance.Checked)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    set
                    {
                        this.btnShowDefaultInheritance.Checked = value;

                        if (value == true)
                        {
                            this.btnShowProperties.Checked = false;
                            this.btnShowDefaultProperties.Checked = false;
                            this.btnShowInheritance.Checked = false;
                        }
                    }
                }

                #endregion Public Properties

                #region Public Methods

                public Config(DockContent Panel)
                {
                    this.WindowType = Type.Config;
                    this.DockPnl = Panel;
                    this.InitializeComponent();
                }

                // Main form handle command key events
                protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg,
                                                      System.Windows.Forms.Keys keyData)
                {
#if Config
					Debug.Print("key: " + keyData.ToString());
					Debug.Print("msg: " + msg.Msg);
					Debug.Print("hwnd: " + msg.HWnd.ToString());
					Debug.Print("lparam: " + msg.LParam.ToString());
					Debug.Print("wparam: " + msg.WParam.ToString());
					Debug.Print("result: " + msg.Result.ToString());
#endif
                    if (keyData == Keys.Tab)
                    {
                        string curGridItemLabel = pGrid.SelectedGridItem.Label;
                        int gridItemIndex;

                        for (gridItemIndex = 0;
                             gridItemIndex <= pGrid.SelectedGridItem.Parent.GridItems.Count;
                             gridItemIndex++)
                        {
                            if (pGrid.SelectedGridItem.Parent.GridItems[gridItemIndex].Label == curGridItemLabel)
                            {
                                break;
                            }
                        }

                        if (pGrid.SelectedGridItem.Parent.GridItems.Count > gridItemIndex + 1)
                        {
                            pGrid.SelectedGridItem.Parent.GridItems[gridItemIndex + 1].Select();
                        }
                        else
                        {
                            pGrid.SelectedGridItem.Parent.GridItems[0].Select();
                        }
                    }
                    if (keyData == (Keys.Tab) || keyData == Keys.Shift)
                    {
                        string curGridItemLabel = pGrid.SelectedGridItem.Label;
                        int gridItemIndex;

                        for (gridItemIndex = 0;
                             gridItemIndex <= pGrid.SelectedGridItem.Parent.GridItems.Count;
                             gridItemIndex++)
                        {
                            if (pGrid.SelectedGridItem.Parent.GridItems[gridItemIndex].Label == curGridItemLabel)
                            {
                                break;
                            }
                        }

                        if (gridItemIndex - 1 >= 0)
                        {
                            pGrid.SelectedGridItem.Parent.GridItems[gridItemIndex - 1].Select();
                        }
                        else
                        {
                            pGrid.SelectedGridItem.Parent.GridItems[pGrid.SelectedGridItem.Parent.GridItems.Count - 1].
                                Select();
                        }
                    }

                    return base.ProcessCmdKey(ref msg, keyData);
                }

                public void SetPropertyGridObject(object Obj)
                {
                    try
                    {
                        this.btnShowProperties.Enabled = false;
                        this.btnShowInheritance.Enabled = false;
                        this.btnShowDefaultProperties.Enabled = false;
                        this.btnShowDefaultInheritance.Enabled = false;
                        this.btnIcon.Enabled = false;
                        this.btnHostStatus.Enabled = false;

                        this.btnIcon.Image = null;

                        if (Obj is Info) //CONNECTION INFO
                        {
                            if ((Obj as Info).IsContainer == false) //NO CONTAINER
                            {
                                if (this.PropertiesVisible) //Properties selected
                                {
                                    this.pGrid.SelectedObject = Obj;

                                    this.btnShowProperties.Enabled = true;
                                    if ((Obj as Info).Parent != null)
                                    {
                                        this.btnShowInheritance.Enabled = true;
                                    }
                                    else
                                    {
                                        this.btnShowInheritance.Enabled = false;
                                    }
                                    this.btnShowDefaultProperties.Enabled = false;
                                    this.btnShowDefaultInheritance.Enabled = false;
                                    this.btnIcon.Enabled = true;
                                    this.btnHostStatus.Enabled = true;
                                }
                                else if (this.DefaultPropertiesVisible) //Defaults selected
                                {
                                    this.pGrid.SelectedObject = Obj;

                                    if ((Obj as Info).IsDefault) //Is the default connection
                                    {
                                        this.btnShowProperties.Enabled = true;
                                        this.btnShowInheritance.Enabled = false;
                                        this.btnShowDefaultProperties.Enabled = true;
                                        this.btnShowDefaultInheritance.Enabled = true;
                                        this.btnIcon.Enabled = true;
                                        this.btnHostStatus.Enabled = false;
                                    }
                                    else //is not the default connection
                                    {
                                        this.btnShowProperties.Enabled = true;
                                        this.btnShowInheritance.Enabled = true;
                                        this.btnShowDefaultProperties.Enabled = false;
                                        this.btnShowDefaultInheritance.Enabled = false;
                                        this.btnIcon.Enabled = true;
                                        this.btnHostStatus.Enabled = true;

                                        this.PropertiesVisible = true;
                                    }
                                }
                                else if (this.InheritanceVisible) //Inheritance selected
                                {
                                    this.pGrid.SelectedObject = (Obj as Info).Inherit;

                                    this.btnShowProperties.Enabled = true;
                                    this.btnShowInheritance.Enabled = true;
                                    this.btnShowDefaultProperties.Enabled = false;
                                    this.btnShowDefaultInheritance.Enabled = false;
                                    this.btnIcon.Enabled = true;
                                    this.btnHostStatus.Enabled = true;
                                }
                                else if (this.DefaultInheritanceVisible) //Default Inhertiance selected
                                {
                                    this.btnShowProperties.Enabled = true;
                                    this.btnShowInheritance.Enabled = true;
                                    this.btnShowDefaultProperties.Enabled = false;
                                    this.btnShowDefaultInheritance.Enabled = false;
                                    this.btnIcon.Enabled = true;
                                    this.btnHostStatus.Enabled = true;

                                    this.PropertiesVisible = true;
                                }
                            }
                            else if ((Obj as Info).IsContainer) //CONTAINER
                            {
                                this.pGrid.SelectedObject = Obj;

                                this.btnShowProperties.Enabled = true;
                                if (((Obj as Info).Parent as Container.Info).Parent != null)
                                {
                                    this.btnShowInheritance.Enabled = true;
                                }
                                else
                                {
                                    this.btnShowInheritance.Enabled = false;
                                }
                                this.btnShowDefaultProperties.Enabled = false;
                                this.btnShowDefaultInheritance.Enabled = false;
                                this.btnIcon.Enabled = true;
                                this.btnHostStatus.Enabled = false;

                                this.PropertiesVisible = true;
                            }

                            Icon conIcon = mRemoteNC.Connection.Icon.FromString((Obj as Info).Icon);
                            if (conIcon != null)
                            {
                                this.btnIcon.Image = conIcon.ToBitmap();
                            }
                        }
                        else if (Obj is Root.Info) //ROOT
                        {
                            this.PropertiesVisible = true;
                            this.DefaultPropertiesVisible = false;
                            this.btnShowProperties.Enabled = true;
                            this.btnShowInheritance.Enabled = false;
                            this.btnShowDefaultProperties.Enabled = true;
                            this.btnShowDefaultInheritance.Enabled = true;
                            this.btnIcon.Enabled = false;
                            this.btnHostStatus.Enabled = false;

                            this.pGrid.SelectedObject = Obj;
                        }
                        else if (Obj is Info.Inheritance) //INHERITANCE
                        {
                            this.pGrid.SelectedObject = Obj;

                            if (this.InheritanceVisible)
                            {
                                this.InheritanceVisible = true;
                                this.btnShowProperties.Enabled = true;
                                this.btnShowInheritance.Enabled = true;
                                this.btnShowDefaultProperties.Enabled = false;
                                this.btnShowDefaultInheritance.Enabled = false;
                                this.btnIcon.Enabled = true;
                                this.btnHostStatus.Enabled =
                                    System.Convert.ToBoolean(!((Obj as Info.Inheritance).Parent as Info).IsContainer);

                                this.InheritanceVisible = true;

                                Icon conIcon =
                                    mRemoteNC.Connection.Icon.FromString(((Obj as Info.Inheritance).Parent as Info).Icon);
                                if (conIcon != null)
                                {
                                    this.btnIcon.Image = conIcon.ToBitmap();
                                }
                            }
                            else if (this.DefaultInheritanceVisible)
                            {
                                this.btnShowProperties.Enabled = true;
                                this.btnShowInheritance.Enabled = false;
                                this.btnShowDefaultProperties.Enabled = true;
                                this.btnShowDefaultInheritance.Enabled = true;
                                this.btnIcon.Enabled = false;
                                this.btnHostStatus.Enabled = false;

                                this.DefaultInheritanceVisible = true;
                            }
                        }

                        this.ShowHideGridItems();
                        this.SetHostStatus(Obj);
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            Language.strConfigPropertyGridObjectFailed +
                                                            Constants.vbNewLine + ex.Message, true);
                    }
                }

                public void pGrid_SelectedObjectChanged()
                {
                    this.ShowHideGridItems();
                }

                #endregion Public Methods

                #region Private Methods

                private ToolStrip tsCustom = null;

                private void Config_Load(object sender, System.EventArgs e)
                {
                    ApplyLanguage();

                    try
                    {
                        //Show PropertyGrid Toolbar buttons
                        tsCustom = new ToolStrip();
                        tsCustom.Items.Add(btnShowProperties);
                        tsCustom.Items.Add(btnShowInheritance);
                        tsCustom.Items.Add(btnShowDefaultProperties);
                        tsCustom.Items.Add(btnShowDefaultInheritance);
                        tsCustom.Items.Add(btnHostStatus);
                        tsCustom.Items.Add(btnIcon);
                        tsCustom.Show();

                        ToolStrip tsDefault = new ToolStrip();

                        foreach (System.Windows.Forms.Control ctrl in pGrid.Controls)
                        {
                            ToolStrip tStrip = ctrl as ToolStrip;

                            if (tStrip != null)
                            {
                                tsDefault = tStrip;
                                break;
                            }
                        }

                        tsDefault.AllowMerge = true;
                        tsDefault.Items[tsDefault.Items.Count - 1].Visible = false;
                        ToolStripManager.Merge(tsCustom, tsDefault);
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            Language.strConfigUiLoadFailed + Constants.vbNewLine +
                                                            ex.Message, true);
                    }
                }

                private void ApplyLanguage()
                {
                    btnShowInheritance.Text = Language.strButtonInheritance;
                    btnShowDefaultInheritance.Text = Language.strButtonDefaultInheritance;
                    btnShowProperties.Text = Language.strButtonProperties;
                    btnShowDefaultProperties.Text = Language.strButtonDefaultProperties;
                    btnIcon.Text = Language.strButtonIcon;
                    btnHostStatus.Text = Language.strStatus;
                    Text = Language.strMenuConfig;
                    TabText = Language.strMenuConfig;
                }

                private void pGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
                {
                    try
                    {
                        if (this.pGrid.SelectedObject is Info)
                        {
                            if (e.ChangedItem.Label == Language.strPropertyNameProtocol)
                            {
                                (this.pGrid.SelectedObject as Info).SetDefaultPort();
                            }
                            else if (e.ChangedItem.Label == Language.strPropertyNameName)
                            {
                                Runtime.Windows.treeForm.tvConnections.SelectedNode.Text =
                                    ((Info)pGrid.SelectedObject).Name;
                                if (Settings.Default.SetHostnameLikeDisplayName && this.pGrid.SelectedObject is Info)
                                {
                                    Info connectionInfo = (Info)this.pGrid.SelectedObject;
                                    if (!string.IsNullOrEmpty((string)connectionInfo.Name))
                                    {
                                        connectionInfo.Hostname = connectionInfo.Name;
                                    }
                                }
                            }
                            else if (e.ChangedItem.Label == Language.strPropertyNameIcon)
                            {
                                Icon conIcon =
                                    mRemoteNC.Connection.Icon.FromString((this.pGrid.SelectedObject as Info).Icon);
                                if (conIcon != null)
                                {
                                    this.btnIcon.Image = conIcon.ToBitmap();
                                }
                            }
                            else if (e.ChangedItem.Label == Language.strPropertyNamePuttySession)
                            {
                                PuttySession.PuttySessions = mRemoteNC.Connection.PuttyBase.GetSessions();
                            }
                            else if (e.ChangedItem.Label == Language.strPropertyNameAddress)
                            {
                                this.SetHostStatus(this.pGrid.SelectedObject);
                            }

                            if ((this.pGrid.SelectedObject as Info).IsDefault)
                            {
                                Runtime.DefaultConnectionToSettings();
                            }
                        }

                        if (this.pGrid.SelectedObject is Root.Info)
                        {
                            Root.Info rInfo = (Root.Info)this.pGrid.SelectedObject;

                            if (e.ChangedItem.Label == Language.strPasswordProtect)
                            {
                                if (rInfo.Password == true)
                                {
                                    string pw = (string)Tools.Misc.PasswordDialog();

                                    if (pw != "")
                                    {
                                        rInfo.PasswordString = pw;
                                    }
                                    else
                                    {
                                        rInfo.Password = false;
                                    }
                                }
                            }
                            else if (e.ChangedItem.Label == Language.strPropertyNameName)
                            {
                                Runtime.Windows.treeForm.tvConnections.SelectedNode.Text =
                                    ((Root.Info)pGrid.SelectedObject).Name;
                            }
                        }

                        if (this.pGrid.SelectedObject is Info.Inheritance)
                        {
                            if ((this.pGrid.SelectedObject as Info.Inheritance).IsDefault)
                            {
                                Runtime.DefaultInheritanceToSettings();
                            }
                        }

                        this.ShowHideGridItems();
                        Runtime.SaveConnectionsBG();
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            Language.strConfigPropertyGridValueFailed +
                                                            Constants.vbNewLine + ex.Message, true);
                    }
                }

                private void ShowHideGridItems()
                {
                    try
                    {
                        List<string> strHide = new List<string>();

                        if (this.pGrid.SelectedObject is Info)
                        {
                            Info conI = (Info)pGrid.SelectedObject;

                            switch (conI.Protocol)
                            {
                                case Protocols.TeamViewer:
                                    strHide.Add("ICAEncryption");
                                    strHide.Add("PuttySession");
                                    strHide.Add("RenderingEngine");
                                    strHide.Add("VNCAuthMode");
                                    strHide.Add("VNCColors");
                                    strHide.Add("VNCCompression");
                                    strHide.Add("VNCEncoding");
                                    strHide.Add("VNCProxyIP");
                                    strHide.Add("VNCProxyPassword");
                                    strHide.Add("VNCProxyPort");
                                    strHide.Add("VNCProxyType");
                                    strHide.Add("VNCProxyUsername");
                                    strHide.Add("VNCSmartSizeMode");
                                    strHide.Add("VNCViewOnly");
                                    strHide.Add("RDGatewayDomain");
                                    strHide.Add("RDGatewayHostname");
                                    strHide.Add("RDGatewayPassword");
                                    strHide.Add("RDGatewayUseConnectionCredentials");
                                    strHide.Add("RDGatewayUsername");
                                    strHide.Add("CacheBitmaps");
                                    strHide.Add("Colors");
                                    strHide.Add("DisplayThemes");
                                    strHide.Add("DisplayWallpaper");
                                    strHide.Add("EnableFontSmoothing");
                                    strHide.Add("EnableDesktopComposition");
                                    strHide.Add("Domain");
                                    strHide.Add("ExtApp");
                                    strHide.Add("ICAEncryption");
                                    strHide.Add("RDGatewayDomain");
                                    strHide.Add("RDGatewayHostname");
                                    strHide.Add("RDGatewayPassword");
                                    strHide.Add("RDGatewayUsageMethod");
                                    strHide.Add("RDGatewayUseConnectionCredentials");
                                    strHide.Add("RDGatewayUsername");
                                    strHide.Add("RDPAuthenticationLevel");
                                    strHide.Add("RedirectDiskDrives");
                                    strHide.Add("RedirectKeys");
                                    strHide.Add("RedirectPorts");
                                    strHide.Add("RedirectPrinters");
                                    strHide.Add("RedirectSmartCards");
                                    strHide.Add("RedirectSound");
                                    strHide.Add("RenderingEngine");
                                    strHide.Add("Resolution");
                                    strHide.Add("UseConsoleSession");
                                    strHide.Add("UseCredSsp");
                                    strHide.Add("Username");
                                    strHide.Add("Port");
                                    break;

                                case Protocols.RDP:
                                    strHide.Add("ExtApp");
                                    strHide.Add("ICAEncryption");
                                    strHide.Add("PuttySession");
                                    strHide.Add("RenderingEngine");
                                    strHide.Add("VNCAuthMode");
                                    strHide.Add("VNCColors");
                                    strHide.Add("VNCCompression");
                                    strHide.Add("VNCEncoding");
                                    strHide.Add("VNCProxyIP");
                                    strHide.Add("VNCProxyPassword");
                                    strHide.Add("VNCProxyPort");
                                    strHide.Add("VNCProxyType");
                                    strHide.Add("VNCProxyUsername");
                                    strHide.Add("VNCSmartSizeMode");
                                    strHide.Add("VNCViewOnly");
                                    if (conI.RDGatewayUsageMethod == RDP.RDGatewayUsageMethod.Never)
                                    {
                                        strHide.Add("RDGatewayDomain");
                                        strHide.Add("RDGatewayHostname");
                                        strHide.Add("RDGatewayPassword");
                                        strHide.Add("RDGatewayUseConnectionCredentials");
                                        strHide.Add("RDGatewayUsername");
                                    }
                                    else if (conI.RDGatewayUseConnectionCredentials ==
                                             RDP.RDGatewayUseConnectionCredentials.Yes) //FIXME
                                    {
                                        strHide.Add("RDGatewayDomain");
                                        strHide.Add("RDGatewayPassword");
                                        strHide.Add("RDGatewayUsername");
                                    }
                                    break;
                                case Protocols.VNC:
                                    strHide.Add("CacheBitmaps");
                                    strHide.Add("Colors");
                                    strHide.Add("DisplayThemes");
                                    strHide.Add("DisplayWallpaper");
                                    strHide.Add("EnableFontSmoothing");
                                    strHide.Add("EnableDesktopComposition");
                                    strHide.Add("ExtApp");
                                    strHide.Add("ICAEncryption");
                                    strHide.Add("PuttySession");
                                    strHide.Add("RDGatewayDomain");
                                    strHide.Add("RDGatewayHostname");
                                    strHide.Add("RDGatewayPassword");
                                    strHide.Add("RDGatewayUsageMethod");
                                    strHide.Add("RDGatewayUseConnectionCredentials");
                                    strHide.Add("RDGatewayUsername");
                                    strHide.Add("RDPAuthenticationLevel");
                                    strHide.Add("RedirectDiskDrives");
                                    strHide.Add("RedirectKeys");
                                    strHide.Add("RedirectPorts");
                                    strHide.Add("RedirectPrinters");
                                    strHide.Add("RedirectSmartCards");
                                    strHide.Add("RedirectSound");
                                    strHide.Add("RenderingEngine");
                                    strHide.Add("Resolution");
                                    strHide.Add("UseConsoleSession");
                                    strHide.Add("UseCredSsp");
                                    if (conI.VNCAuthMode == VNC.AuthMode.AuthVNC)
                                    {
                                        strHide.Add("Username;Domain");
                                    }
                                    if (conI.VNCProxyType == VNC.ProxyType.ProxyNone)
                                    {
                                        strHide.Add("VNCProxyIP");
                                        strHide.Add("VNCProxyPassword");
                                        strHide.Add("VNCProxyPort");
                                        strHide.Add("VNCProxyUsername");
                                    }
                                    break;
                                case Protocols.SSH1:
                                    strHide.Add("CacheBitmaps");
                                    strHide.Add("Colors");
                                    strHide.Add("DisplayThemes");
                                    strHide.Add("DisplayWallpaper");
                                    strHide.Add("EnableFontSmoothing");
                                    strHide.Add("EnableDesktopComposition");
                                    strHide.Add("Domain");
                                    strHide.Add("ExtApp");
                                    strHide.Add("ICAEncryption");
                                    strHide.Add("RDGatewayDomain");
                                    strHide.Add("RDGatewayHostname");
                                    strHide.Add("RDGatewayPassword");
                                    strHide.Add("RDGatewayUsageMethod");
                                    strHide.Add("RDGatewayUseConnectionCredentials");
                                    strHide.Add("RDGatewayUsername");
                                    strHide.Add("RDPAuthenticationLevel");
                                    strHide.Add("RedirectDiskDrives");
                                    strHide.Add("RedirectKeys");
                                    strHide.Add("RedirectPorts");
                                    strHide.Add("RedirectPrinters");
                                    strHide.Add("RedirectSmartCards");
                                    strHide.Add("RedirectSound");
                                    strHide.Add("RenderingEngine");
                                    strHide.Add("Resolution");
                                    strHide.Add("UseConsoleSession");
                                    strHide.Add("UseCredSsp");
                                    strHide.Add("VNCAuthMode");
                                    strHide.Add("VNCColors");
                                    strHide.Add("VNCCompression");
                                    strHide.Add("VNCEncoding");
                                    strHide.Add("VNCProxyIP");
                                    strHide.Add("VNCProxyPassword");
                                    strHide.Add("VNCProxyPort");
                                    strHide.Add("VNCProxyType");
                                    strHide.Add("VNCProxyUsername");
                                    strHide.Add("VNCSmartSizeMode");
                                    strHide.Add("VNCViewOnly");
                                    break;
                                case Protocols.SSH2:
                                    strHide.Add("CacheBitmaps");
                                    strHide.Add("Colors");
                                    strHide.Add("DisplayThemes");
                                    strHide.Add("DisplayWallpaper");
                                    strHide.Add("EnableFontSmoothing");
                                    strHide.Add("EnableDesktopComposition");
                                    strHide.Add("Domain");
                                    strHide.Add("ExtApp");
                                    strHide.Add("ICAEncryption");
                                    strHide.Add("RDGatewayDomain");
                                    strHide.Add("RDGatewayHostname");
                                    strHide.Add("RDGatewayPassword");
                                    strHide.Add("RDGatewayUsageMethod");
                                    strHide.Add("RDGatewayUseConnectionCredentials");
                                    strHide.Add("RDGatewayUsername");
                                    strHide.Add("RDPAuthenticationLevel");
                                    strHide.Add("RedirectDiskDrives");
                                    strHide.Add("RedirectKeys");
                                    strHide.Add("RedirectPorts");
                                    strHide.Add("RedirectPrinters");
                                    strHide.Add("RedirectSmartCards");
                                    strHide.Add("RedirectSound");
                                    strHide.Add("RenderingEngine");
                                    strHide.Add("Resolution");
                                    strHide.Add("UseConsoleSession");
                                    strHide.Add("UseCredSsp");
                                    strHide.Add("VNCAuthMode");
                                    strHide.Add("VNCColors");
                                    strHide.Add("VNCCompression");
                                    strHide.Add("VNCEncoding");
                                    strHide.Add("VNCProxyIP");
                                    strHide.Add("VNCProxyPassword");
                                    strHide.Add("VNCProxyPort");
                                    strHide.Add("VNCProxyType");
                                    strHide.Add("VNCProxyUsername");
                                    strHide.Add("VNCSmartSizeMode");
                                    strHide.Add("VNCViewOnly");
                                    break;
                                case Protocols.Telnet:
                                    strHide.Add("CacheBitmaps");
                                    strHide.Add("Colors");
                                    strHide.Add("DisplayThemes");
                                    strHide.Add("DisplayWallpaper");
                                    strHide.Add("EnableFontSmoothing");
                                    strHide.Add("EnableDesktopComposition");
                                    strHide.Add("Domain");
                                    strHide.Add("ExtApp");
                                    strHide.Add("ICAEncryption");
                                    strHide.Add("Password");
                                    strHide.Add("RDGatewayDomain");
                                    strHide.Add("RDGatewayHostname");
                                    strHide.Add("RDGatewayPassword");
                                    strHide.Add("RDGatewayUsageMethod");
                                    strHide.Add("RDGatewayUseConnectionCredentials");
                                    strHide.Add("RDGatewayUsername");
                                    strHide.Add("RDPAuthenticationLevel");
                                    strHide.Add("RedirectDiskDrives");
                                    strHide.Add("RedirectKeys");
                                    strHide.Add("RedirectPorts");
                                    strHide.Add("RedirectPrinters");
                                    strHide.Add("RedirectSmartCards");
                                    strHide.Add("RedirectSound");
                                    strHide.Add("RenderingEngine");
                                    strHide.Add("Resolution");
                                    strHide.Add("UseConsoleSession");
                                    strHide.Add("UseCredSsp");
                                    strHide.Add("Username");
                                    strHide.Add("VNCAuthMode");
                                    strHide.Add("VNCColors");
                                    strHide.Add("VNCCompression");
                                    strHide.Add("VNCEncoding");
                                    strHide.Add("VNCProxyIP");
                                    strHide.Add("VNCProxyPassword");
                                    strHide.Add("VNCProxyPort");
                                    strHide.Add("VNCProxyType");
                                    strHide.Add("VNCProxyUsername");
                                    strHide.Add("VNCSmartSizeMode");
                                    strHide.Add("VNCViewOnly");
                                    break;
                                case Protocols.Rlogin:
                                    strHide.Add("CacheBitmaps");
                                    strHide.Add("Colors");
                                    strHide.Add("DisplayThemes");
                                    strHide.Add("DisplayWallpaper");
                                    strHide.Add("EnableFontSmoothing");
                                    strHide.Add("EnableDesktopComposition");
                                    strHide.Add("Domain");
                                    strHide.Add("ExtApp");
                                    strHide.Add("ICAEncryption");
                                    strHide.Add("Password");
                                    strHide.Add("RDGatewayDomain");
                                    strHide.Add("RDGatewayHostname");
                                    strHide.Add("RDGatewayPassword");
                                    strHide.Add("RDGatewayUsageMethod");
                                    strHide.Add("RDGatewayUseConnectionCredentials");
                                    strHide.Add("RDGatewayUsername");
                                    strHide.Add("RDPAuthenticationLevel");
                                    strHide.Add("RedirectDiskDrives");
                                    strHide.Add("RedirectKeys");
                                    strHide.Add("RedirectPorts");
                                    strHide.Add("RedirectPrinters");
                                    strHide.Add("RedirectSmartCards");
                                    strHide.Add("RedirectSound");
                                    strHide.Add("RenderingEngine");
                                    strHide.Add("Resolution");
                                    strHide.Add("UseConsoleSession");
                                    strHide.Add("UseCredSsp");
                                    strHide.Add("Username");
                                    strHide.Add("VNCAuthMode");
                                    strHide.Add("VNCColors");
                                    strHide.Add("VNCCompression");
                                    strHide.Add("VNCEncoding");
                                    strHide.Add("VNCProxyIP");
                                    strHide.Add("VNCProxyPassword");
                                    strHide.Add("VNCProxyPort");
                                    strHide.Add("VNCProxyType");
                                    strHide.Add("VNCProxyUsername");
                                    strHide.Add("VNCSmartSizeMode");
                                    strHide.Add("VNCViewOnly");
                                    break;
                                case Protocols.RAW:
                                    strHide.Add("CacheBitmaps");
                                    strHide.Add("Colors");
                                    strHide.Add("DisplayThemes");
                                    strHide.Add("DisplayWallpaper");
                                    strHide.Add("EnableFontSmoothing");
                                    strHide.Add("EnableDesktopComposition");
                                    strHide.Add("Domain");
                                    strHide.Add("ExtApp");
                                    strHide.Add("ICAEncryption");
                                    strHide.Add("Password");
                                    strHide.Add("RDGatewayDomain");
                                    strHide.Add("RDGatewayHostname");
                                    strHide.Add("RDGatewayPassword");
                                    strHide.Add("RDGatewayUsageMethod");
                                    strHide.Add("RDGatewayUseConnectionCredentials");
                                    strHide.Add("RDGatewayUsername");
                                    strHide.Add("RDPAuthenticationLevel");
                                    strHide.Add("RedirectDiskDrives");
                                    strHide.Add("RedirectKeys");
                                    strHide.Add("RedirectPorts");
                                    strHide.Add("RedirectPrinters");
                                    strHide.Add("RedirectSmartCards");
                                    strHide.Add("RedirectSound");
                                    strHide.Add("RenderingEngine");
                                    strHide.Add("Resolution");
                                    strHide.Add("UseConsoleSession");
                                    strHide.Add("UseCredSsp");
                                    strHide.Add("Username");
                                    strHide.Add("VNCAuthMode");
                                    strHide.Add("VNCColors");
                                    strHide.Add("VNCCompression");
                                    strHide.Add("VNCEncoding");
                                    strHide.Add("VNCProxyIP");
                                    strHide.Add("VNCProxyPassword");
                                    strHide.Add("VNCProxyPort");
                                    strHide.Add("VNCProxyType");
                                    strHide.Add("VNCProxyUsername");
                                    strHide.Add("VNCSmartSizeMode");
                                    strHide.Add("VNCViewOnly");
                                    break;
                                case Protocols.HTTP:
                                    strHide.Add("CacheBitmaps");
                                    strHide.Add("Colors");
                                    strHide.Add("DisplayThemes");
                                    strHide.Add("DisplayWallpaper");
                                    strHide.Add("EnableFontSmoothing");
                                    strHide.Add("EnableDesktopComposition");
                                    strHide.Add("Domain");
                                    strHide.Add("ExtApp");
                                    strHide.Add("ICAEncryption");
                                    strHide.Add("PuttySession");
                                    strHide.Add("RDGatewayDomain");
                                    strHide.Add("RDGatewayHostname");
                                    strHide.Add("RDGatewayPassword");
                                    strHide.Add("RDGatewayUsageMethod");
                                    strHide.Add("RDGatewayUseConnectionCredentials");
                                    strHide.Add("RDGatewayUsername");
                                    strHide.Add("RDPAuthenticationLevel");
                                    strHide.Add("RedirectDiskDrives");
                                    strHide.Add("RedirectKeys");
                                    strHide.Add("RedirectPorts");
                                    strHide.Add("RedirectPrinters");
                                    strHide.Add("RedirectSmartCards");
                                    strHide.Add("RedirectSound");
                                    strHide.Add("Resolution");
                                    strHide.Add("UseConsoleSession");
                                    strHide.Add("UseCredSsp");
                                    strHide.Add("VNCAuthMode");
                                    strHide.Add("VNCColors");
                                    strHide.Add("VNCCompression");
                                    strHide.Add("VNCEncoding");
                                    strHide.Add("VNCProxyIP");
                                    strHide.Add("VNCProxyPassword");
                                    strHide.Add("VNCProxyPort");
                                    strHide.Add("VNCProxyType");
                                    strHide.Add("VNCProxyUsername");
                                    strHide.Add("VNCSmartSizeMode");
                                    strHide.Add("VNCViewOnly");
                                    break;
                                case Protocols.HTTPS:
                                    strHide.Add("CacheBitmaps");
                                    strHide.Add("Colors");
                                    strHide.Add("DisplayThemes");
                                    strHide.Add("DisplayWallpaper");
                                    strHide.Add("EnableFontSmoothing");
                                    strHide.Add("EnableDesktopComposition");
                                    strHide.Add("Domain");
                                    strHide.Add("ExtApp");
                                    strHide.Add("ICAEncryption");
                                    strHide.Add("PuttySession");
                                    strHide.Add("RDGatewayDomain");
                                    strHide.Add("RDGatewayHostname");
                                    strHide.Add("RDGatewayPassword");
                                    strHide.Add("RDGatewayUsageMethod");
                                    strHide.Add("RDGatewayUseConnectionCredentials");
                                    strHide.Add("RDGatewayUsername");
                                    strHide.Add("RDPAuthenticationLevel");
                                    strHide.Add("RedirectDiskDrives");
                                    strHide.Add("RedirectKeys");
                                    strHide.Add("RedirectPorts");
                                    strHide.Add("RedirectPrinters");
                                    strHide.Add("RedirectSmartCards");
                                    strHide.Add("RedirectSound;Resolution");
                                    strHide.Add("UseConsoleSession");
                                    strHide.Add("UseCredSsp");
                                    strHide.Add("VNCAuthMode");
                                    strHide.Add("VNCColors");
                                    strHide.Add("VNCCompression");
                                    strHide.Add("VNCEncoding");
                                    strHide.Add("VNCProxyIP");
                                    strHide.Add("VNCProxyPassword");
                                    strHide.Add("VNCProxyPort");
                                    strHide.Add("VNCProxyType");
                                    strHide.Add("VNCProxyUsername");
                                    strHide.Add("VNCSmartSizeMode");
                                    strHide.Add("VNCViewOnly");
                                    break;
                                case Protocols.ICA:
                                    strHide.Add("DisplayThemes");
                                    strHide.Add("DisplayWallpaper");
                                    strHide.Add("EnableFontSmoothing");
                                    strHide.Add("EnableDesktopComposition");
                                    strHide.Add("ExtApp");
                                    strHide.Add("Port");
                                    strHide.Add("PuttySession");
                                    strHide.Add("RDGatewayDomain");
                                    strHide.Add("RDGatewayHostname");
                                    strHide.Add("RDGatewayPassword");
                                    strHide.Add("RDGatewayUsageMethod");
                                    strHide.Add("RDGatewayUseConnectionCredentials");
                                    strHide.Add("RDGatewayUsername");
                                    strHide.Add("RDPAuthenticationLevel");
                                    strHide.Add("RedirectDiskDrives");
                                    strHide.Add("RedirectKeys");
                                    strHide.Add("RedirectPorts");
                                    strHide.Add("RedirectPrinters");
                                    strHide.Add("RedirectSmartCards");
                                    strHide.Add("RedirectSound");
                                    strHide.Add("RenderingEngine");
                                    strHide.Add("UseConsoleSession");
                                    strHide.Add("UseCredSsp");
                                    strHide.Add("VNCAuthMode");
                                    strHide.Add("VNCColors");
                                    strHide.Add("VNCCompression");
                                    strHide.Add("VNCEncoding");
                                    strHide.Add("VNCProxyIP");
                                    strHide.Add("VNCProxyPassword");
                                    strHide.Add("VNCProxyPort");
                                    strHide.Add("VNCProxyType");
                                    strHide.Add("VNCProxyUsername");
                                    strHide.Add("VNCSmartSizeMode");
                                    strHide.Add("VNCViewOnly");
                                    break;
                                case Protocols.IntApp:
                                    strHide.Add("CacheBitmaps");
                                    strHide.Add("Colors");
                                    strHide.Add("DisplayThemes");
                                    strHide.Add("DisplayWallpaper");
                                    strHide.Add("EnableFontSmoothing");
                                    strHide.Add("EnableDesktopComposition");
                                    strHide.Add("Domain");
                                    strHide.Add("ICAEncryption");
                                    strHide.Add("PuttySession");
                                    strHide.Add("RDGatewayDomain");
                                    strHide.Add("RDGatewayHostname");
                                    strHide.Add("RDGatewayPassword");
                                    strHide.Add("RDGatewayUsageMethod");
                                    strHide.Add("RDGatewayUseConnectionCredentials");
                                    strHide.Add("RDGatewayUsername");
                                    strHide.Add("RDPAuthenticationLevel");
                                    strHide.Add("RedirectDiskDrives");
                                    strHide.Add("RedirectKeys");
                                    strHide.Add("RedirectPorts");
                                    strHide.Add("RedirectPrinters");
                                    strHide.Add("RedirectSmartCards");
                                    strHide.Add("RedirectSound");
                                    strHide.Add("RenderingEngine");
                                    strHide.Add("Resolution");
                                    strHide.Add("UseConsoleSession");
                                    strHide.Add("UseCredSsp");
                                    strHide.Add("VNCAuthMode");
                                    strHide.Add("VNCColors");
                                    strHide.Add("VNCCompression");
                                    strHide.Add("VNCEncoding");
                                    strHide.Add("VNCProxyIP");
                                    strHide.Add("VNCProxyPassword");
                                    strHide.Add("VNCProxyPort");
                                    strHide.Add("VNCProxyType");
                                    strHide.Add("VNCProxyUsername");
                                    strHide.Add("VNCSmartSizeMode");
                                    strHide.Add("VNCViewOnly");
                                    break;
                            }

                            if (conI.IsDefault == false)
                            {
                                var with_1 = conI.Inherit;
                                if (with_1.CacheBitmaps)
                                {
                                    strHide.Add("CacheBitmaps");
                                }

                                if (with_1.Colors)
                                {
                                    strHide.Add("Colors");
                                }

                                if (with_1.Description)
                                {
                                    strHide.Add("Description");
                                }

                                if (with_1.DisplayThemes)
                                {
                                    strHide.Add("DisplayThemes");
                                }

                                if (with_1.DisplayWallpaper)
                                {
                                    strHide.Add("DisplayWallpaper");
                                }

                                if (with_1.EnableFontSmoothing)
                                {
                                    strHide.Add("EnableFontSmoothing");
                                }

                                if (with_1.EnableDesktopComposition)
                                {
                                    strHide.Add("EnableDesktopComposition");
                                }

                                if (with_1.Domain)
                                {
                                    strHide.Add("Domain");
                                }

                                if (with_1.Icon)
                                {
                                    strHide.Add("Icon");
                                }

                                if (with_1.Password)
                                {
                                    strHide.Add("Password");
                                }

                                if (with_1.Port)
                                {
                                    strHide.Add("Port");
                                }

                                if (with_1.Protocol)
                                {
                                    strHide.Add("Protocol");
                                }

                                if (with_1.PuttySession)
                                {
                                    strHide.Add("PuttySession");
                                }

                                if (with_1.RedirectDiskDrives)
                                {
                                    strHide.Add("RedirectDiskDrives");
                                }

                                if (with_1.RedirectKeys)
                                {
                                    strHide.Add("RedirectKeys");
                                }

                                if (with_1.RedirectPorts)
                                {
                                    strHide.Add("RedirectPorts");
                                }

                                if (with_1.RedirectPrinters)
                                {
                                    strHide.Add("RedirectPrinters");
                                }

                                if (with_1.RedirectSmartCards)
                                {
                                    strHide.Add("RedirectSmartCards");
                                }

                                if (with_1.RedirectSound)
                                {
                                    strHide.Add("RedirectSound");
                                }

                                if (with_1.Resolution)
                                {
                                    strHide.Add("Resolution");
                                }

                                if (with_1.UseConsoleSession)
                                {
                                    strHide.Add("UseConsoleSession");
                                }

                                if (with_1.UseCredSsp)
                                {
                                    strHide.Add("UseCredSsp");
                                }

                                if (with_1.RenderingEngine)
                                {
                                    strHide.Add("RenderingEngine");
                                }

                                if (with_1.ICAEncryption)
                                {
                                    strHide.Add("ICAEncryption");
                                }

                                if (with_1.RDPAuthenticationLevel)
                                {
                                    strHide.Add("RDPAuthenticationLevel");
                                }

                                if (with_1.Username)
                                {
                                    strHide.Add("Username");
                                }

                                if (with_1.Panel)
                                {
                                    strHide.Add("Panel");
                                }

                                if (conI.IsContainer)
                                {
                                    strHide.Add("Hostname");
                                }

                                if (with_1.PreExtApp)
                                {
                                    strHide.Add("PreExtApp");
                                }

                                if (with_1.PostExtApp)
                                {
                                    strHide.Add("PostExtApp");
                                }

                                if (with_1.MacAddress)
                                {
                                    strHide.Add("MacAddress");
                                }

                                if (with_1.UserField)
                                {
                                    strHide.Add("UserField");
                                }

                                if (with_1.VNCAuthMode)
                                {
                                    strHide.Add("VNCAuthMode");
                                }

                                if (with_1.VNCColors)
                                {
                                    strHide.Add("VNCColors");
                                }

                                if (with_1.VNCCompression)
                                {
                                    strHide.Add("VNCCompression");
                                }

                                if (with_1.VNCEncoding)
                                {
                                    strHide.Add("VNCEncoding");
                                }

                                if (with_1.VNCProxyIP)
                                {
                                    strHide.Add("VNCProxyIP");
                                }

                                if (with_1.VNCProxyPassword)
                                {
                                    strHide.Add("VNCProxyPassword");
                                }

                                if (with_1.VNCProxyPort)
                                {
                                    strHide.Add("VNCProxyPort");
                                }

                                if (with_1.VNCProxyType)
                                {
                                    strHide.Add("VNCProxyType");
                                }

                                if (with_1.VNCProxyUsername)
                                {
                                    strHide.Add("VNCProxyUsername");
                                }

                                if (with_1.VNCViewOnly)
                                {
                                    strHide.Add("VNCViewOnly");
                                }

                                if (with_1.VNCSmartSizeMode)
                                {
                                    strHide.Add("VNCSmartSizeMode");
                                }

                                if (with_1.ExtApp)
                                {
                                    strHide.Add("ExtApp");
                                }

                                if (with_1.RDGatewayUsageMethod)
                                {
                                    strHide.Add("RDGatewayUsageMethod");
                                }

                                if (with_1.RDGatewayHostname)
                                {
                                    strHide.Add("RDGatewayHostname");
                                }

                                if (with_1.RDGatewayUsername)
                                {
                                    strHide.Add("RDGatewayUsername");
                                }

                                if (with_1.RDGatewayPassword)
                                {
                                    strHide.Add("RDGatewayPassword");
                                }

                                if (with_1.RDGatewayDomain)
                                {
                                    strHide.Add("RDGatewayDomain");
                                }

                                if (with_1.RDGatewayUseConnectionCredentials)
                                {
                                    strHide.Add("RDGatewayUseConnectionCredentials");
                                }

                                if (with_1.RDGatewayHostname)
                                {
                                    strHide.Add("RDGatewayHostname");
                                }
                            }
                            else
                            {
                                strHide.Add("Hostname");
                                strHide.Add("Name");
                            }
                        }
                        else if (this.pGrid.SelectedObject is Root.Info)
                        {
                            strHide.Add("TreeNode");
                        }

                        this.pGrid.HiddenProperties = strHide.ToArray();

                        this.pGrid.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            Language.strConfigPropertyGridHideItemsFailed +
                                                            Constants.vbNewLine + ex.Message, true);
                    }
                }

                private void btnShowProperties_Click(object sender, System.EventArgs e)
                {
                    if (this.pGrid.SelectedObject is Info.Inheritance)
                    {
                        if ((this.pGrid.SelectedObject as Info.Inheritance).IsDefault)
                        {
                            this.PropertiesVisible = true;
                            this.InheritanceVisible = false;
                            this.DefaultPropertiesVisible = false;
                            this.DefaultInheritanceVisible = false;
                            this.SetPropertyGridObject(
                                Runtime.Windows.treeForm.tvConnections.SelectedNode.Tag as Root.Info);
                        }
                        else
                        {
                            this.PropertiesVisible = true;
                            this.InheritanceVisible = false;
                            this.DefaultPropertiesVisible = false;
                            this.DefaultInheritanceVisible = false;
                            this.SetPropertyGridObject((this.pGrid.SelectedObject as Info.Inheritance).Parent);
                        }
                    }
                    else if (this.pGrid.SelectedObject is Info)
                    {
                        if ((this.pGrid.SelectedObject as Info).IsDefault)
                        {
                            this.PropertiesVisible = true;
                            this.InheritanceVisible = false;
                            this.DefaultPropertiesVisible = false;
                            this.DefaultInheritanceVisible = false;
                            this.SetPropertyGridObject(
                                Runtime.Windows.treeForm.tvConnections.SelectedNode.Tag as Root.Info);
                        }
                    }
                }

                private void btnShowDefaultProperties_Click(object sender, System.EventArgs e)
                {
                    if (this.pGrid.SelectedObject is Root.Info || this.pGrid.SelectedObject is Info.Inheritance)
                    {
                        this.PropertiesVisible = false;
                        this.InheritanceVisible = false;
                        this.DefaultPropertiesVisible = true;
                        this.DefaultInheritanceVisible = false;
                        this.SetPropertyGridObject(Runtime.DefaultConnectionFromSettings());
                    }
                }

                private void btnShowInheritance_Click(object sender, System.EventArgs e)
                {
                    if (this.pGrid.SelectedObject is Info)
                    {
                        this.PropertiesVisible = false;
                        this.InheritanceVisible = true;
                        this.DefaultPropertiesVisible = false;
                        this.DefaultInheritanceVisible = false;
                        this.SetPropertyGridObject((this.pGrid.SelectedObject as Info).Inherit);
                    }
                }

                private void btnShowDefaultInheritance_Click(object sender, System.EventArgs e)
                {
                    if (this.pGrid.SelectedObject is Root.Info || this.pGrid.SelectedObject is Info)
                    {
                        this.PropertiesVisible = false;
                        this.InheritanceVisible = false;
                        this.DefaultPropertiesVisible = false;
                        this.DefaultInheritanceVisible = true;
                        this.SetPropertyGridObject(Runtime.DefaultInheritanceFromSettings());
                    }
                }

                private void btnHostStatus_Click(object sender, System.EventArgs e)
                {
                    SetHostStatus(this.pGrid.SelectedObject);
                }

                private void btnIcon_Click(object sender, System.Windows.Forms.MouseEventArgs e)
                {
                    try
                    {
                        if (this.pGrid.SelectedObject is Info)
                        {
                            this.cMenIcons.Items.Clear();

                            foreach (string iStr in mRemoteNC.Connection.Icon.Icons)
                            {
                                ToolStripMenuItem tI = new ToolStripMenuItem();
                                tI.Text = iStr;
                                tI.Image = mRemoteNC.Connection.Icon.FromString(iStr).ToBitmap();
                                tI.Click += new System.EventHandler(IconMenu_Click);

                                this.cMenIcons.Items.Add(tI);
                            }

                            Point mPos = PointToScreen(new Point(e.Location.X + this.pGrid.Width - 100, e.Location.Y));
                            this.cMenIcons.Show(mPos);
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            Language.strConfigPropertyGridButtonIconClickFailed +
                                                            Constants.vbNewLine + ex.Message, true);
                    }
                }

                private void IconMenu_Click(object sender, System.EventArgs e)
                {
                    try
                    {
                        if (this.pGrid.SelectedObject is Info)
                        {
                            (this.pGrid.SelectedObject as Info).Icon = (sender as ToolStripMenuItem).Text;
                            Icon conIcon = mRemoteNC.Connection.Icon.FromString((this.pGrid.SelectedObject as Info).Icon);
                            if (conIcon != null)
                            {
                                this.btnIcon.Image = conIcon.ToBitmap();
                            }

                            Runtime.SaveConnectionsBG();
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            Language.strConfigPropertyGridMenuClickFailed +
                                                            Constants.vbNewLine + ex.Message, true);
                    }
                }

                #endregion Private Methods

                #region Host Status (Ping)

                private string HostName;
                private System.Threading.Thread pThread;

                private void CheckHostAlive()
                {
                    Ping pingSender = new Ping();
                    PingReply pReply;

                    try
                    {
                        pReply = pingSender.Send(HostName);

                        if (pReply.Status == IPStatus.Success)
                        {
                            if ((string)this.btnHostStatus.Tag == "checking")
                            {
                                ShowStatusImage(global::My.Resources.Resources.HostStatus_On);
                            }
                        }
                        else
                        {
                            if ((string)this.btnHostStatus.Tag == "checking")
                            {
                                ShowStatusImage(global::My.Resources.Resources.HostStatus_Off);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        if ((string)this.btnHostStatus.Tag == "checking")
                        {
                            ShowStatusImage(global::My.Resources.Resources.HostStatus_Off);
                        }
                    }
                }

                private delegate void ShowStatusImageCB(Image Image);

                private void ShowStatusImage(Image Image)
                {
                    if (this.pGrid.InvokeRequired)
                    {
                        ShowStatusImageCB d = new ShowStatusImageCB(ShowStatusImage);
                        this.pGrid.Invoke(d, new object[] { Image });
                    }
                    else
                    {
                        this.btnHostStatus.Image = Image;
                        this.btnHostStatus.Tag = "checkfinished";
                    }
                }

                public void SetHostStatus(object ConnectionInfo)
                {
                    try
                    {
                        this.btnHostStatus.Image = global::My.Resources.Resources.HostStatus_Check;

                        // To check status, ConnectionInfo must be an mRemoteNC.Connection.Info that is not a container
                        if (ConnectionInfo is Info)
                        {
                            if ((ConnectionInfo as Info).IsContainer)
                            {
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }

                        this.btnHostStatus.Tag = "checking";
                        HostName = (ConnectionInfo as Info).Hostname;
                        pThread = new System.Threading.Thread(new System.Threading.ThreadStart(CheckHostAlive));
                        pThread.SetApartmentState(System.Threading.ApartmentState.STA);
                        pThread.IsBackground = true;
                        pThread.Start();
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            Language.strConfigPropertyGridSetHostStatusFailed +
                                                            Constants.vbNewLine + ex.Message, true);
                    }
                }

                #endregion Host Status (Ping)
            }
        }
    }
}