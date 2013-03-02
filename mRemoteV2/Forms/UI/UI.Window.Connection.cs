using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using mRemoteNC.App;
using mRemoteNC.Connection;
using mRemoteNC.Protocol;
using My;
using PSTaskDialog;
using WeifenLuo.WinFormsUI.Docking;
using mRemoteNC.Tools;
using Icon = System.Drawing.Icon;

//using mRemoteNC.Runtime;

namespace mRemoteNC
{
    namespace UI
    {
        namespace Window
        {
            public class Connection : Base
            {
                #region Form Init

                internal System.Windows.Forms.ContextMenuStrip cmenTab;
                private System.ComponentModel.IContainer components;
                internal System.Windows.Forms.ToolStripMenuItem cmenTabFullscreen;
                internal System.Windows.Forms.ToolStripMenuItem cmenTabScreenshot;
                internal System.Windows.Forms.ToolStripMenuItem cmenTabTransferFile;
                internal System.Windows.Forms.ToolStripMenuItem cmenTabSendSpecialKeys;
                internal System.Windows.Forms.ToolStripSeparator cmenTabSep1;
                internal System.Windows.Forms.ToolStripMenuItem cmenTabRenameTab;
                internal System.Windows.Forms.ToolStripMenuItem cmenTabDuplicateTab;
                internal System.Windows.Forms.ToolStripMenuItem cmenTabDisconnect;
                internal System.Windows.Forms.ToolStripMenuItem cmenTabSmartSize;
                internal System.Windows.Forms.ToolStripMenuItem cmenTabSendSpecialKeysCtrlAltDel;
                internal System.Windows.Forms.ToolStripMenuItem cmenTabSendSpecialKeysCtrlEsc;
                internal System.Windows.Forms.ToolStripMenuItem cmenTabViewOnly;
                internal System.Windows.Forms.ToolStripMenuItem cmenTabReconnect;
                internal System.Windows.Forms.ToolStripMenuItem cmenTabExternalApps;
                internal System.Windows.Forms.ToolStripMenuItem cmenTabStartChat;
                internal System.Windows.Forms.ToolStripMenuItem cmenTabRefreshScreen;
                internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
                internal System.Windows.Forms.ToolStripMenuItem cmenTabPuttySettings;
                private ToolStripMenuItem cmenShowPuTTYMenu;

                public Crownwood.Magic.Controls.TabControl TabController;

                private void InitializeComponent()
                {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Connection));
            this.TabController = new Crownwood.Magic.Controls.TabControl();
            this.cmenTab = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmenTabFullscreen = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenTabSmartSize = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenTabViewOnly = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmenTabScreenshot = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenTabStartChat = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenTabTransferFile = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenTabRefreshScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenTabSendSpecialKeys = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenTabSendSpecialKeysCtrlAltDel = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenTabSendSpecialKeysCtrlEsc = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenShowPuTTYMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenTabPuttySettings = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenTabExternalApps = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenTabSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmenTabRenameTab = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenTabDuplicateTab = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenTabReconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenTabDisconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabController
            // 
            this.TabController.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabController.Appearance = Crownwood.Magic.Controls.TabControl.VisualAppearance.MultiDocument;
            this.TabController.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TabController.DragFromControl = false;
            this.TabController.IDEPixelArea = true;
            this.TabController.IDEPixelBorder = false;
            this.TabController.Location = new System.Drawing.Point(0, -1);
            this.TabController.Name = "TabController";
            this.TabController.Size = new System.Drawing.Size(632, 454);
            this.TabController.TabIndex = 0;
            this.TabController.ClosePressed += new System.EventHandler(this.TabController_ClosePressed);
            this.TabController.SelectionChanged += new System.EventHandler(this.TabController_SelectionChanged);
            this.TabController.PageDragStart += new System.Windows.Forms.MouseEventHandler(this.TabController_PageDragStart);
            this.TabController.PageDragMove += new System.Windows.Forms.MouseEventHandler(this.TabController_PageDragMove);
            this.TabController.PageDragEnd += new System.Windows.Forms.MouseEventHandler(this.TabController_PageDragEnd);
            this.TabController.PageDragQuit += new System.Windows.Forms.MouseEventHandler(this.TabController_PageDragEnd);
            this.TabController.DoubleClickTab += new Crownwood.Magic.Controls.TabControl.DoubleClickTabHandler(this.TabController_DoubleClickTab);
            this.TabController.DragDrop += new System.Windows.Forms.DragEventHandler(this.TabController_DragDrop);
            this.TabController.DragEnter += new System.Windows.Forms.DragEventHandler(this.TabController_DragEnter);
            this.TabController.DragOver += new System.Windows.Forms.DragEventHandler(this.TabController_DragOver);
            this.TabController.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TabController_MouseUp);
            this.TabController.Resize += new System.EventHandler(this.TabController_Resize);
            // 
            // cmenTab
            // 
            this.cmenTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmenTabFullscreen,
            this.cmenTabSmartSize,
            this.cmenTabViewOnly,
            this.ToolStripSeparator1,
            this.cmenTabScreenshot,
            this.cmenTabStartChat,
            this.cmenTabTransferFile,
            this.cmenTabRefreshScreen,
            this.cmenTabSendSpecialKeys,
            this.cmenShowPuTTYMenu,
            this.cmenTabPuttySettings,
            this.cmenTabExternalApps,
            this.cmenTabSep1,
            this.cmenTabRenameTab,
            this.cmenTabDuplicateTab,
            this.cmenTabReconnect,
            this.cmenTabDisconnect});
            this.cmenTab.Name = "cmenTab";
            this.cmenTab.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.cmenTab.Size = new System.Drawing.Size(202, 346);
            // 
            // cmenTabFullscreen
            // 
            this.cmenTabFullscreen.Image = global::My.Resources.Resources.Fullscreen;
            this.cmenTabFullscreen.Name = "cmenTabFullscreen";
            this.cmenTabFullscreen.Size = new System.Drawing.Size(201, 22);
            this.cmenTabFullscreen.Text = "Fullscreen (RDP)";
            this.cmenTabFullscreen.Click += new System.EventHandler(this.cmenTabFullscreen_Click);
            // 
            // cmenTabSmartSize
            // 
            this.cmenTabSmartSize.Image = global::My.Resources.Resources.SmartSize;
            this.cmenTabSmartSize.Name = "cmenTabSmartSize";
            this.cmenTabSmartSize.Size = new System.Drawing.Size(201, 22);
            this.cmenTabSmartSize.Text = "SmartSize (RDP/VNC)";
            this.cmenTabSmartSize.Click += new System.EventHandler(this.cmenTabSmartSize_Click);
            // 
            // cmenTabViewOnly
            // 
            this.cmenTabViewOnly.Name = "cmenTabViewOnly";
            this.cmenTabViewOnly.Size = new System.Drawing.Size(201, 22);
            this.cmenTabViewOnly.Text = "View Only (VNC)";
            this.cmenTabViewOnly.Click += new System.EventHandler(this.cmenTabViewOnly_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(198, 6);
            // 
            // cmenTabScreenshot
            // 
            this.cmenTabScreenshot.Image = global::My.Resources.Resources.Screenshot_Add;
            this.cmenTabScreenshot.Name = "cmenTabScreenshot";
            this.cmenTabScreenshot.Size = new System.Drawing.Size(201, 22);
            this.cmenTabScreenshot.Text = "Screenshot";
            this.cmenTabScreenshot.Click += new System.EventHandler(this.cmenTabScreenshot_Click);
            // 
            // cmenTabStartChat
            // 
            this.cmenTabStartChat.Image = global::My.Resources.Resources.Chat;
            this.cmenTabStartChat.Name = "cmenTabStartChat";
            this.cmenTabStartChat.Size = new System.Drawing.Size(201, 22);
            this.cmenTabStartChat.Text = "Start Chat (VNC)";
            this.cmenTabStartChat.Visible = false;
            this.cmenTabStartChat.Click += new System.EventHandler(this.cmenTabStartChat_Click);
            // 
            // cmenTabTransferFile
            // 
            this.cmenTabTransferFile.Image = global::My.Resources.Resources.SSHTransfer;
            this.cmenTabTransferFile.Name = "cmenTabTransferFile";
            this.cmenTabTransferFile.Size = new System.Drawing.Size(201, 22);
            this.cmenTabTransferFile.Text = "Transfer File (SSH)";
            this.cmenTabTransferFile.Click += new System.EventHandler(this.cmenTabTransferFile_Click);
            // 
            // cmenTabRefreshScreen
            // 
            this.cmenTabRefreshScreen.Image = global::My.Resources.Resources.Refresh;
            this.cmenTabRefreshScreen.Name = "cmenTabRefreshScreen";
            this.cmenTabRefreshScreen.Size = new System.Drawing.Size(201, 22);
            this.cmenTabRefreshScreen.Text = "Refresh Screen (VNC)";
            this.cmenTabRefreshScreen.Click += new System.EventHandler(this.cmenTabRefreshScreen_Click);
            // 
            // cmenTabSendSpecialKeys
            // 
            this.cmenTabSendSpecialKeys.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmenTabSendSpecialKeysCtrlAltDel,
            this.cmenTabSendSpecialKeysCtrlEsc});
            this.cmenTabSendSpecialKeys.Image = global::My.Resources.Resources.Keyboard;
            this.cmenTabSendSpecialKeys.Name = "cmenTabSendSpecialKeys";
            this.cmenTabSendSpecialKeys.Size = new System.Drawing.Size(201, 22);
            this.cmenTabSendSpecialKeys.Text = "Send special Keys (VNC)";
            // 
            // cmenTabSendSpecialKeysCtrlAltDel
            // 
            this.cmenTabSendSpecialKeysCtrlAltDel.Name = "cmenTabSendSpecialKeysCtrlAltDel";
            this.cmenTabSendSpecialKeysCtrlAltDel.Size = new System.Drawing.Size(141, 22);
            this.cmenTabSendSpecialKeysCtrlAltDel.Text = "Ctrl+Alt+Del";
            this.cmenTabSendSpecialKeysCtrlAltDel.Click += new System.EventHandler(this.cmenTabSendSpecialKeysCtrlAltDel_Click);
            // 
            // cmenTabSendSpecialKeysCtrlEsc
            // 
            this.cmenTabSendSpecialKeysCtrlEsc.Name = "cmenTabSendSpecialKeysCtrlEsc";
            this.cmenTabSendSpecialKeysCtrlEsc.Size = new System.Drawing.Size(141, 22);
            this.cmenTabSendSpecialKeysCtrlEsc.Text = "Ctrl+Esc";
            this.cmenTabSendSpecialKeysCtrlEsc.Click += new System.EventHandler(this.cmenTabSendSpecialKeysCtrlEsc_Click);
            // 
            // cmenShowPuTTYMenu
            // 
            this.cmenShowPuTTYMenu.Name = "cmenShowPuTTYMenu";
            this.cmenShowPuTTYMenu.Size = new System.Drawing.Size(201, 22);
            this.cmenShowPuTTYMenu.Text = "Show PuTTY Menu";
            this.cmenShowPuTTYMenu.Click += new System.EventHandler(this.cmenShowPuTTYMenu_Click);
            // 
            // cmenTabPuttySettings
            // 
            this.cmenTabPuttySettings.Name = "cmenTabPuttySettings";
            this.cmenTabPuttySettings.Size = new System.Drawing.Size(201, 22);
            this.cmenTabPuttySettings.Text = "PuTTY Settings";
            this.cmenTabPuttySettings.Click += new System.EventHandler(this.cmenTabPuttySettings_Click);
            // 
            // cmenTabExternalApps
            // 
            this.cmenTabExternalApps.Image = ((System.Drawing.Image)(resources.GetObject("cmenTabExternalApps.Image")));
            this.cmenTabExternalApps.Name = "cmenTabExternalApps";
            this.cmenTabExternalApps.Size = new System.Drawing.Size(201, 22);
            this.cmenTabExternalApps.Text = "External Applications";
            // 
            // cmenTabSep1
            // 
            this.cmenTabSep1.Name = "cmenTabSep1";
            this.cmenTabSep1.Size = new System.Drawing.Size(198, 6);
            // 
            // cmenTabRenameTab
            // 
            this.cmenTabRenameTab.Image = global::My.Resources.Resources.Rename;
            this.cmenTabRenameTab.Name = "cmenTabRenameTab";
            this.cmenTabRenameTab.Size = new System.Drawing.Size(201, 22);
            this.cmenTabRenameTab.Text = "Rename Tab";
            this.cmenTabRenameTab.Click += new System.EventHandler(this.cmenTabRenameTab_Click);
            // 
            // cmenTabDuplicateTab
            // 
            this.cmenTabDuplicateTab.Name = "cmenTabDuplicateTab";
            this.cmenTabDuplicateTab.Size = new System.Drawing.Size(201, 22);
            this.cmenTabDuplicateTab.Text = "Duplicate Tab";
            this.cmenTabDuplicateTab.Click += new System.EventHandler(this.cmenTabDuplicateTab_Click);
            // 
            // cmenTabReconnect
            // 
            this.cmenTabReconnect.Image = ((System.Drawing.Image)(resources.GetObject("cmenTabReconnect.Image")));
            this.cmenTabReconnect.Name = "cmenTabReconnect";
            this.cmenTabReconnect.Size = new System.Drawing.Size(201, 22);
            this.cmenTabReconnect.Text = "Reconnect";
            this.cmenTabReconnect.Click += new System.EventHandler(this.cmenTabReconnect_Click);
            // 
            // cmenTabDisconnect
            // 
            this.cmenTabDisconnect.Image = global::My.Resources.Resources.Pause;
            this.cmenTabDisconnect.Name = "cmenTabDisconnect";
            this.cmenTabDisconnect.Size = new System.Drawing.Size(201, 22);
            this.cmenTabDisconnect.Text = "Disconnect";
            this.cmenTabDisconnect.Click += new System.EventHandler(this.cmenTabDisconnect_Click);
            // 
            // Connection
            // 
            this.ClientSize = new System.Drawing.Size(632, 453);
            this.Controls.Add(this.TabController);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Connection";
            this.TabText = "UI.Window.Connection";
            this.Text = "UI.Window.Connection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Connection_FormClosing);
            this.Load += new System.EventHandler(this.Connection_Load);
            this.DockStateChanged += new System.EventHandler(this.Connection_DockStateChanged);
            this.cmenTab.ResumeLayout(false);
            this.ResumeLayout(false);

                }

                #endregion Form Init

                #region Public Methods

                private Tools.Misc.Fullscreen fullscreenManager;

                public Connection(DockContent Panel, string FormText = "")
                {
                    if (FormText == "")
                    {
                        FormText = Language.strNewPanel;
                    }

                    this.WindowType = Type.Connection;
                    this.DockPnl = Panel;
                    this.InitializeComponent();
                    this.Text = FormText;
                    this.TabText = FormText;
                    fullscreenManager=new Misc.Fullscreen(this);
                }

                public Crownwood.Magic.Controls.TabPage AddConnectionTab(Info conI)
                {
                    try
                    {
                        Crownwood.Magic.Controls.TabPage nTab = new Crownwood.Magic.Controls.TabPage();
                        nTab.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

                        if (Settings.Default.ShowProtocolOnTabs)
                        {
                            nTab.Title = conI.Protocol.ToString() + ": ";
                        }
                        else
                        {
                            nTab.Title = "";
                        }

                        nTab.Title += (string)conI.Name;

                        if (Settings.Default.ShowLogonInfoOnTabs)
                        {
                            nTab.Title += " (";

                            if (conI.Domain != "")
                            {
                                nTab.Title += (string)conI.Domain;
                            }

                            if (conI.Username != "")
                            {
                                if (conI.Domain != "")
                                {
                                    nTab.Title += "\\";
                                }

                                nTab.Title += (string)conI.Username;
                            }

                            nTab.Title += ")";
                        }
                        nTab.Title = nTab.Title.Replace("&", "&&");
                        Icon conIcon = mRemoteNC.Connection.Icon.FromString(conI.Icon);
                        if (conIcon != null)
                        {
                            nTab.Icon = conIcon;
                        }

                        if (Settings.Default.OpenTabsRightOfSelected)
                        {
                            this.TabController.TabPages.Insert(this.TabController.SelectedIndex + 1, nTab);
                        }
                        else
                        {
                            this.TabController.TabPages.Add(nTab);
                        }

                        nTab.Selected = true;
                        _ignoreChangeSelectedTabClick = false;
                        return nTab;
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("AddConnectionTab (UI.Window.Connections) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }

                    return null;
                }

                private bool _ignoreChangeSelectedTabClick = false;

                #endregion Public Methods

                #region Form

                private void Connection_Load(object sender, System.EventArgs e)
                {
                    ApplyLanguage();
                }

                private bool _documentHandlersAdded = false;
                private bool _floatHandlersAdded = false;
                private void Connection_DockStateChanged(System.Object sender, EventArgs e)
                {
                    if (DockState == DockState.Float)
                    {
                        if (_documentHandlersAdded)
                        {
                            frmMain.Default.ResizeBegin -= Connection_ResizeBegin;
                            frmMain.Default.ResizeEnd -= Connection_ResizeEnd;
                            _documentHandlersAdded = false;
                        }
                        DockHandler.FloatPane.FloatWindow.ResizeBegin += Connection_ResizeBegin;
                        DockHandler.FloatPane.FloatWindow.ResizeEnd += Connection_ResizeEnd;
                        _floatHandlersAdded = true;
                    }
                    else if (DockState == DockState.Document)
                    {
                        if (_floatHandlersAdded)
                        {
                            DockHandler.FloatPane.FloatWindow.ResizeBegin -= Connection_ResizeBegin;
                            DockHandler.FloatPane.FloatWindow.ResizeEnd -= Connection_ResizeEnd;
                            _floatHandlersAdded = false;
                        }
                        frmMain.Default.ResizeBegin += Connection_ResizeBegin;
                        frmMain.Default.ResizeEnd += Connection_ResizeEnd;
                        _documentHandlersAdded = true;
                    }
                }

                public new event EventHandler ResizeBegin;
                private void Connection_ResizeBegin(System.Object sender, EventArgs e)
                {
                    if (ResizeBegin != null)
                    {
                        ResizeBegin(this, e);
                    }
                }

                public new event EventHandler ResizeEnd;
                public void Connection_ResizeEnd(System.Object sender, EventArgs e)
                {
                    if (ResizeEnd != null)
                    {
                        ResizeEnd(sender, e);
                    }
                }

                private void ApplyLanguage()
                {
                    cmenTabFullscreen.Text = Language.strMenuFullScreenRDP;
                    cmenTabSmartSize.Text = Language.strMenuSmartSize;
                    cmenTabViewOnly.Text = Language.strMenuViewOnly;
                    cmenTabScreenshot.Text = Language.strMenuScreenshot;
                    cmenTabStartChat.Text = Language.strMenuStartChat;
                    cmenTabTransferFile.Text = Language.strMenuTransferFile;
                    cmenTabRefreshScreen.Text = Language.strMenuRefreshScreen;
                    cmenTabSendSpecialKeys.Text = Language.strMenuSendSpecialKeys;
                    cmenTabSendSpecialKeysCtrlAltDel.Text = Language.strMenuCtrlAltDel;
                    cmenTabSendSpecialKeysCtrlEsc.Text = Language.strMenuCtrlEsc;
                    cmenTabExternalApps.Text = Language.strMenuExternalTools;
                    cmenTabRenameTab.Text = Language.strMenuRenameTab;
                    cmenTabDuplicateTab.Text = Language.strMenuDuplicateTab;
                    cmenTabReconnect.Text = Language.strMenuReconnect;
                    cmenTabDisconnect.Text = Language.strMenuDisconnect;
                    cmenTabPuttySettings.Text = Language.strPuttySettings;
                }

                private void Connection_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
                {
                    if (!frmMain.Default.IsClosing && Settings.Default.ConfirmCloseConnection &&
                        TabController.TabPages.Count > 0)
                    {
                        DialogResult Result = cTaskDialog.MessageBox(this,
                                                                     (new Microsoft.VisualBasic.ApplicationServices.
                                                                         WindowsFormsApplicationBase()).Info.ProductName,
                                                                     string.Format(
                                                                         Language.strConfirmCloseConnectionPanelMainInstruction,
                                                                         this.Text), "", "", "",
                                                                     Language.strCheckboxDoNotShowThisMessageAgain,
                                                                     eTaskDialogButtons.YesNo, eSysIcons.Question,
                                                                     eSysIcons.Information);
                        if (cTaskDialog.VerificationChecked)
                        {
                            Settings.Default.ConfirmCloseConnection = false;
                        }
                        if (Result == DialogResult.No)
                        {
                            e.Cancel = true;
                            return;
                        }
                    }

                    try
                    {
                        foreach (Crownwood.Magic.Controls.TabPage tabP in this.TabController.TabPages)
                        {
                            if (tabP.Tag != null)
                            {
                                InterfaceControl IC = (InterfaceControl)tabP.Tag;
                                IC.Protocol.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("Connection_FormClosing (UI.Window.Connections) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                /*private void Connection_Resize(System.Object sender, System.EventArgs e)
                {
                    try
                    {
                        foreach (Crownwood.Magic.Controls.TabPage tabP in this.TabController.TabPages)
                        {
                            if (tabP.Tag != null)
                            {
                                if (tabP.Tag is InterfaceControl)
                                {
                                    InterfaceControl IC = (InterfaceControl)tabP.Tag;

                                    if (IC.Info.Protocol == Protocols.VNC)
                                    {
                                        (IC.Protocol as VNC).RefreshScreen();
                                    }

                                    //TODO
                                    if (IC.Protocol as PuttyBase != null && Width > 200)
                                    {
                                        (IC.Protocol as PuttyBase).Resize();
                                    }

                                    //TODO
                                    if (IC.Protocol as RDP != null && Width > 200)
                                    {
                                        (IC.Protocol as RDP).Resize();
                                    }

                                    //TODO
                                    if (IC.Protocol as TeamViewer != null && Width > 200)
                                    {
                                        (IC.Protocol as TeamViewer).Resize();
                                    }

                                    //TODO
                                    if (IC.Protocol as RAdmin != null && Width > 200)
                                    {
                                        (IC.Protocol as RAdmin).Resize();
                                    }

                                    //TODO
                                    if (IC.Protocol as IntApp != null )
                                    {
                                        (IC.Protocol as IntApp).Resize();
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("Connection_Resize (UI.Window.Connections) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }*/

                #endregion Form

                #region TabController

                private void TabController_ClosePressed(object sender, System.EventArgs e)
                {
                    if (this.TabController.SelectedTab == null)
                    {
                        return;
                    }

                    this.CloseConnectionTab();
                }

                private void CloseConnectionTab()
                {
                    Crownwood.Magic.Controls.TabPage SelectedTab = this.TabController.SelectedTab;
                    if (Settings.Default.ConfirmCloseConnection)
                    {
                        DialogResult Result = cTaskDialog.MessageBox(this,
                                                                     (new Microsoft.VisualBasic.ApplicationServices.
                                                                         WindowsFormsApplicationBase()).Info.ProductName,
                                                                     string.Format(
                                                                         Language.strConfirmCloseConnectionMainInstruction,
                                                                         SelectedTab.Title), "", "", "",
                                                                     Language.strCheckboxDoNotShowThisMessageAgain,
                                                                     eTaskDialogButtons.YesNo, eSysIcons.Question,
                                                                     eSysIcons.Information);
                        if (cTaskDialog.VerificationChecked)
                        {
                            Settings.Default.ConfirmCloseConnection = false;
                        }
                        if (Result == DialogResult.No)
                        {
                            return;
                        }
                    }

                    try
                    {
                        if (SelectedTab.Tag != null)
                        {
                            var IC = (InterfaceControl)SelectedTab.Tag;
                            IC.Protocol.Close();
                            CloseTab(SelectedTab);
                        }
                        else
                        {
                            this.CloseTab(SelectedTab);
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("CloseConnectionTab (UI.Window.Connections) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private int _lastMouseUp = 0;
                private void TabController_DoubleClickTab(Crownwood.Magic.Controls.TabControl sender,
                                                          Crownwood.Magic.Controls.TabPage page)
                {
                    _lastMouseUp = 0;
                    if (Settings.Default.DoubleClickOnTabClosesIt)
                    {
                        this.CloseConnectionTab();
                    }
                }

                #region Drag and Drop

                private void TabController_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
                {
                    if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", true))
                    {
                        Runtime.OpenConnection(
                            (Info)((Control)e.Data.GetData("System.Windows.Forms.TreeNode", true)).Tag, this,
                            Info.Force.DoNotJump);
                    }
                }

                private void TabController_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
                {
                    if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", true))
                    {
                        e.Effect = DragDropEffects.Move;
                    }
                }

                private void TabController_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
                {
                    e.Effect = DragDropEffects.Move;
                }

                #endregion Drag and Drop

                #endregion TabController

                #region Tab Menu

                private void ShowHideMenuButtons()
                {
                    try
                    {
                        if (this.TabController.SelectedTab == null)
                        {
                            return;
                        }

                        InterfaceControl IC = (InterfaceControl)this.TabController.SelectedTab.Tag;

                        if (IC == null)
                        {
                            return;
                        }

                        if (IC.Info.Protocol == Protocols.RDP)
                        {
                            this.cmenTabFullscreen.Visible = true;
                            this.cmenTabSmartSize.Visible = true;

                            RDP rdp = (RDP)IC.Protocol;
                            this.cmenTabSmartSize.Checked = System.Convert.ToBoolean(rdp.SmartSize);
                            this.cmenTabFullscreen.Checked = System.Convert.ToBoolean(rdp.Fullscreen);
                        }
                        else
                        {
                            this.cmenTabFullscreen.Visible = false;
                            this.cmenTabSmartSize.Visible = false;
                        }

                        if (IC.Info.Protocol == Protocols.VNC)
                        {
                            this.cmenTabSendSpecialKeys.Visible = true;
                            this.cmenTabViewOnly.Visible = true;

                            this.cmenTabSmartSize.Visible = true;
                            this.cmenTabStartChat.Visible = true;
                            this.cmenTabRefreshScreen.Visible = true;
                            this.cmenTabTransferFile.Visible = false;

                            VNC vnc = (VNC)IC.Protocol;
                            this.cmenTabSmartSize.Visible = System.Convert.ToBoolean(vnc.SmartSize);
                            this.cmenTabViewOnly.Visible = System.Convert.ToBoolean(vnc.ViewOnly);
                        }
                        else
                        {
                            this.cmenTabSendSpecialKeys.Visible = false;
                            this.cmenTabViewOnly.Visible = false;
                            this.cmenTabStartChat.Visible = false;
                            this.cmenTabRefreshScreen.Visible = false;
                            this.cmenTabTransferFile.Visible = false;
                        }

                        if (IC.Info.Protocol == Protocols.SSH1 || IC.Info.Protocol == Protocols.SSH2)
                        {
                            this.cmenTabTransferFile.Enabled = true;
                        }

                        if (IC.Protocol is PuttyBase)
                        {
                            cmenTabFullscreen.Visible = true;
                            cmenShowPuTTYMenu.Visible = false;
                            this.cmenTabPuttySettings.Visible = true;
                        }
                        else
                        {
                            cmenShowPuTTYMenu.Visible = false;
                            this.cmenTabPuttySettings.Visible = false;
                        }

                        AddExternalApps();
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("ShowHideMenuButtons (UI.Window.Connections) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void cmenTabScreenshot_Click(System.Object sender, System.EventArgs e)
                {
                    cmenTab.Close();
                    Application.DoEvents();
                    Runtime.Windows.screenshotForm.AddScreenshot(Tools.Misc.TakeScreenshot(this));
                }

                private void cmenTabSmartSize_Click(System.Object sender, System.EventArgs e)
                {
                    this.ToggleSmartSize();
                }

                private void cmenTabReconnect_Click(System.Object sender, System.EventArgs e)
                {
                    this.Reconnect();
                }

                private void cmenTabTransferFile_Click(System.Object sender, System.EventArgs e)
                {
                    this.TransferFile();
                }

                private void cmenTabViewOnly_Click(System.Object sender, System.EventArgs e)
                {
                    this.ToggleViewOnly();
                }

                private void cmenTabStartChat_Click(object sender, System.EventArgs e)
                {
                    this.StartChat();
                }

                private void cmenTabRefreshScreen_Click(object sender, System.EventArgs e)
                {
                    this.RefreshScreen();
                }

                private void cmenTabSendSpecialKeysCtrlAltDel_Click(System.Object sender, System.EventArgs e)
                {
                    this.SendSpecialKeys(VNC.SpecialKeys.CtrlAltDel);
                }

                private void cmenTabSendSpecialKeysCtrlEsc_Click(System.Object sender, System.EventArgs e)
                {
                    this.SendSpecialKeys(VNC.SpecialKeys.CtrlEsc);
                }

                private void cmenTabFullscreen_Click(System.Object sender, System.EventArgs e)
                {
                    this.ToggleFullscreen();
                }

                private void cmenTabPuttySettings_Click(System.Object sender, System.EventArgs e)
                {
                    this.ShowPuttySettingsDialog();
                }

                private void cmenTabExternalAppsEntry_Click(object sender, System.EventArgs e)
                {
                    StartExternalApp((Tools.ExternalTool)((Control)sender).Tag);
                }

                private void cmenTabDisconnect_Click(System.Object sender, System.EventArgs e)
                {
                    this.CloseTabMenu();
                }

                private void cmenTabDuplicateTab_Click(System.Object sender, System.EventArgs e)
                {
                    this.DuplicateTab();
                }

                private void cmenTabRenameTab_Click(System.Object sender, System.EventArgs e)
                {
                    this.RenameTab();
                }

                #endregion Tab Menu

                #region Tab Actions

                private void ToggleSmartSize()
                {
                    try
                    {
                        if (this.TabController.SelectedTab != null)
                        {
                            if (this.TabController.SelectedTab.Tag is InterfaceControl)
                            {
                                InterfaceControl IC = (InterfaceControl)this.TabController.SelectedTab.Tag;

                                if (IC.Protocol is RDP)
                                {
                                    RDP rdp = (RDP)IC.Protocol;
                                    rdp.ToggleSmartSize();
                                }
                                else if (IC.Protocol is VNC)
                                {
                                    VNC vnc = (VNC)IC.Protocol;
                                    vnc.ToggleSmartSize();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("ToggleSmartSize (UI.Window.Connections) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void TransferFile()
                {
                    try
                    {
                        if (this.TabController.SelectedTab != null)
                        {
                            if (this.TabController.SelectedTab.Tag is InterfaceControl)
                            {
                                InterfaceControl IC = (InterfaceControl)this.TabController.SelectedTab.Tag;

                                if (IC.Info.Protocol == Protocols.SSH1 || IC.Info.Protocol == Protocols.SSH2)
                                {
                                    SSHTransferFile();
                                }
                                else if (IC.Info.Protocol == Protocols.VNC)
                                {
                                    VNCTransferFile();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("TransferFile (UI.Window.Connections) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void SSHTransferFile()
                {
                    try
                    {
                        InterfaceControl IC = (InterfaceControl)this.TabController.SelectedTab.Tag;

                        Runtime.Windows.Show(Type.SSHTransfer);

                        Info conI = IC.Info;

                        Runtime.Windows.sshtransferForm.Hostname = conI.Hostname;
                        Runtime.Windows.sshtransferForm.Username = conI.Username;
                        Runtime.Windows.sshtransferForm.Password = conI.Password;
                        Runtime.Windows.sshtransferForm.Port = conI.Port.ToString();
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("SSHTransferFile (UI.Window.Connections) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void VNCTransferFile()
                {
                    try
                    {
                        InterfaceControl IC = (InterfaceControl)this.TabController.SelectedTab.Tag;
                        VNC vnc = (VNC)IC.Protocol;
                        vnc.StartFileTransfer();
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("VNCTransferFile (UI.Window.Connections) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void ToggleViewOnly()
                {
                    try
                    {
                        if (this.TabController.SelectedTab != null)
                        {
                            if (this.TabController.SelectedTab.Tag is InterfaceControl)
                            {
                                InterfaceControl IC = (InterfaceControl)this.TabController.SelectedTab.Tag;

                                if (IC.Protocol is VNC)
                                {
                                    cmenTabViewOnly.Checked = System.Convert.ToBoolean(!cmenTabViewOnly.Checked);

                                    VNC vnc = (VNC)IC.Protocol;
                                    vnc.ToggleViewOnly();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("ToggleViewOnly (UI.Window.Connections) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void StartChat()
                {
                    try
                    {
                        if (this.TabController.SelectedTab != null)
                        {
                            if (this.TabController.SelectedTab.Tag is InterfaceControl)
                            {
                                InterfaceControl IC = (InterfaceControl)this.TabController.SelectedTab.Tag;

                                if (IC.Protocol is VNC)
                                {
                                    VNC vnc = (VNC)IC.Protocol;
                                    vnc.StartChat();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("StartChat (UI.Window.Connections) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void RefreshScreen()
                {
                    try
                    {
                        if (this.TabController.SelectedTab != null)
                        {
                            if (this.TabController.SelectedTab.Tag is InterfaceControl)
                            {
                                InterfaceControl IC = (InterfaceControl)this.TabController.SelectedTab.Tag;

                                var protocol = IC.Protocol as VNC;
                                if (protocol != null)
                                {
                                    protocol.RefreshScreen();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("RefreshScreen (UI.Window.Connections) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void SendSpecialKeys(mRemoteNC.VNC.SpecialKeys Keys)
                {
                    try
                    {
                        if (this.TabController.SelectedTab != null)
                        {
                            if (this.TabController.SelectedTab.Tag is InterfaceControl)
                            {
                                InterfaceControl IC = (InterfaceControl)this.TabController.SelectedTab.Tag;

                                if (IC.Protocol is VNC)
                                {
                                    VNC vnc = (VNC)IC.Protocol;
                                    vnc.SendSpecialKeys(Keys);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("SendSpecialKeys (UI.Window.Connections) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void ToggleFullscreen()
                {
                    try
                    {
                        if (this.TabController.SelectedTab != null)
                        {
                            if (this.TabController.SelectedTab.Tag is InterfaceControl)
                            {
                                InterfaceControl IC = (InterfaceControl)this.TabController.SelectedTab.Tag;

                                if (IC.Protocol is RDP)
                                {
                                    RDP rdp = (RDP)IC.Protocol;
                                    rdp.ToggleFullscreen();
                                }

                                if (IC.Protocol is PuttyBase)
                                {
                                    fullscreenManager.EnterFullscreen();
                                    Native.SetForegroundWindow((IC.Protocol as PuttyBase).PuttyHandle);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("ToggleFullscreen (UI.Window.Connections) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void ShowPuttySettingsDialog()
                {
                    try
                    {
                        if (this.TabController.SelectedTab != null)
                        {
                            if (this.TabController.SelectedTab.Tag is InterfaceControl)
                            {
                                InterfaceControl objInterfaceControl =
                                    (InterfaceControl)this.TabController.SelectedTab.Tag;

                                if (objInterfaceControl.Protocol is mRemoteNC.Connection.PuttyBase)
                                {
                                    mRemoteNC.Connection.PuttyBase objPuttyBase =
                                        (PuttyBase)objInterfaceControl.Protocol;

                                    objPuttyBase.ShowSettingsDialog();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("ShowPuttySettingsDialog (UI.Window.Connections) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void AddExternalApps()
                {
                    try
                    {
                        //clean up
                        cmenTabExternalApps.DropDownItems.Clear();

                        //add ext apps
                        foreach (Tools.ExternalTool extA in Runtime.ExternalTools)
                        {
                            ToolStripMenuItem nItem = new ToolStripMenuItem();
                            nItem.Text = (string)extA.DisplayName;
                            nItem.Tag = extA;

                            nItem.Image = extA.Image;

                            nItem.Click += new System.EventHandler(cmenTabExternalAppsEntry_Click);

                            cmenTabExternalApps.DropDownItems.Add(nItem);
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("cMenTreeTools_DropDownOpening failed (UI.Window.Tree)" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void StartExternalApp(Tools.ExternalTool ExtA)
                {
                    try
                    {
                        if (this.TabController.SelectedTab != null)
                        {
                            if (this.TabController.SelectedTab.Tag is InterfaceControl)
                            {
                                InterfaceControl IC = (InterfaceControl)this.TabController.SelectedTab.Tag;

                                ExtA.Start(IC.Info);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("cmenTabExternalAppsEntry_Click failed (UI.Window.Tree)" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void CloseTabMenu()
                {
                    try
                    {
                        if (this.TabController.SelectedTab != null)
                        {
                            if (this.TabController.SelectedTab.Tag is InterfaceControl)
                            {
                                InterfaceControl IC = (InterfaceControl)this.TabController.SelectedTab.Tag;

                                IC.Protocol.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("CloseTabMenu (UI.Window.Connections) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void DuplicateTab()
                {
                    try
                    {
                        if (this.TabController.SelectedTab != null)
                        {
                            if (this.TabController.SelectedTab.Tag is InterfaceControl)
                            {
                                InterfaceControl IC = (InterfaceControl)this.TabController.SelectedTab.Tag;

                                Runtime.OpenConnection(IC.Info, Info.Force.DoNotJump);
                                _ignoreChangeSelectedTabClick = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("DuplicateTab (UI.Window.Connections) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void Reconnect()
                {
                    try
                    {
                        if (this.TabController.SelectedTab != null)
                        {
                            if (this.TabController.SelectedTab.Tag is InterfaceControl)
                            {
                                InterfaceControl IC = (InterfaceControl)this.TabController.SelectedTab.Tag;
                                Info conI = IC.Info;

                                IC.Protocol.Close();

                                Runtime.OpenConnection(conI, Info.Force.DoNotJump);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("Reconnect (UI.Window.Connections) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void RenameTab()
                {
                    try
                    {
                        string nTitle = Interaction.InputBox(Language.strNewTitle + ":", "",
                                                             this.TabController.SelectedTab.Title.Replace("&&", "&"), -1,
                                                             -1);

                        if (nTitle != "")
                        {
                            this.TabController.SelectedTab.Title = nTitle.Replace("&", "&&");
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("RenameTab (UI.Window.Connections) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                #endregion Tab Actions

                #region Protocols

                public void Prot_Event_Closed(object sender)
                {
                    mRemoteNC.Base Prot = (mRemoteNC.Base)sender;
                    CloseTab((Crownwood.Magic.Controls.TabPage)Prot.InterfaceControl.Parent);
                }

                #endregion Protocols

                #region Tabs

                private delegate void CloseTabCB(Crownwood.Magic.Controls.TabPage TabToBeClosed);

                private void CloseTab(Crownwood.Magic.Controls.TabPage TabToBeClosed)
                {
                    try
                    {
                        if (this.TabController.InvokeRequired)
                        {
                            CloseTabCB s = CloseTab;

                            try
                            {
                                this.TabController.Invoke(s, TabToBeClosed);
                            }
                            catch (System.Runtime.InteropServices.COMException)
                            {
                                this.TabController.Invoke(s, TabToBeClosed);
                            }
                            catch (Exception ex)
                            {
                                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                                    "Couldn\'t close tab" + Constants.vbNewLine +
                                                                    ex.Message, true);
                            }
                        }
                        else
                        {
                            try
                            {
                                if (TabController.TabPages.Contains(TabToBeClosed))
                                    TabController.TabPages.Remove(TabToBeClosed);
                                _ignoreChangeSelectedTabClick = false;
                            }
                            catch (Exception ex)
                            {
                                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                                    "Couldn\'t close tab" + Constants.vbNewLine +
                                                                    ex.Message, true);
                            }

                            if (this.TabController.TabPages.Count == 0)
                            {
                                this.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("CloseTab failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private bool _selectedTabChanged=false;

                private void TabController_SelectionChanged(object sender, System.EventArgs e)
                {
                    _ignoreChangeSelectedTabClick = true;
                    _selectedTabChanged = true;
                    this.FocusIC();
                    this.RefreshIC();
                }

                private void FocusIC()
                {
                    try
                    {
                        if (this.TabController.SelectedTab != null)
                        {
                            if (this.TabController.SelectedTab.Tag is InterfaceControl)
                            {
                                InterfaceControl IC = (InterfaceControl)this.TabController.SelectedTab.Tag;
                                IC.Protocol.Focus();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("FocusIC (UI.Window.Connections) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                public void RefreshIC()
                {
                    try
                    {
                        if (this.TabController.SelectedTab != null)
                        {
                            if (this.TabController.SelectedTab.Tag is InterfaceControl)
                            {
                                InterfaceControl IC = (InterfaceControl)this.TabController.SelectedTab.Tag;

                                if (IC.Info.Protocol == Protocols.VNC)
                                {
                                    (IC.Protocol as VNC).RefreshScreen();
                                }

                                //TODO
                                if (IC.Protocol as IntApp != null)
                                {
                                    (IC.Protocol as IntApp).Refresh();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("RefreshIC (UI.Window.Connection) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                #endregion Tabs

                #region Window Overrides

                protected override void WndProc(ref Message WndMsg)
                {
                    try
                    {
                        if (WndMsg.Msg == Native.WM_MOUSEACTIVATE)
                        {
                            Crownwood.Magic.Controls.TabPage curTab = this.TabController.SelectedTab;
                            Rectangle curRect = curTab.RectangleToScreen(curTab.ClientRectangle);

                            if (curRect.Contains(Form.MousePosition))
                            {
                                if (curTab != null)
                                {
                                    InterfaceControl IC = (InterfaceControl)this.TabController.SelectedTab.Tag;

                                    if (IC != null && IC.Info.Protocol == Protocols.RDP)
                                    {
                                        IC.Protocol.Focus();
                                    }
                                }
                            }
                            return; // Do not pass to base class
                        }
                    }
                    catch (Exception)
                    {
                    }

                    base.WndProc(ref WndMsg);
                }

                #endregion Window Overrides

                #region Tab drag and drop

                private void TabController_PageDragStart(object sender, MouseEventArgs e)
                {
                    Cursor = Cursors.SizeWE;
                }

                private void TabController_PageDragEnd(object sender, MouseEventArgs e)
                {
                    Cursor = Cursors.Default;
                }

                private void TabController_PageDragMove(object sender, MouseEventArgs e)
                {
                    Crownwood.Magic.Controls.TabPage sourceTab = TabController.SelectedTab;
                    Crownwood.Magic.Controls.TabPage destinationTab = TabController.TabPageFromPoint(e.Location);

                    if (!TabController.TabPages.Contains(destinationTab) || sourceTab == destinationTab)
                    {
                        return;
                    }

                    int targetIndex = TabController.TabPages.IndexOf(destinationTab);

                    TabController.TabPages.SuspendEvents();
                    TabController.TabPages.Remove(sourceTab);
                    TabController.TabPages.Insert(targetIndex, sourceTab);
                    TabController.SelectedTab = sourceTab;
                    TabController.TabPages.ResumeEvents();
                }

                private void TabController_MouseUp(object sender, MouseEventArgs e)
                {
                    try
                    {
                    Debug.Print("UI.Window.Connection.TabController_MouseUp()");
                    Debug.Print("_ignoreChangeSelectedTabClick = {0}", _ignoreChangeSelectedTabClick);
                        if (!(Native.GetForegroundWindow() == frmMain.Default.Handle) && !_ignoreChangeSelectedTabClick)
                        {
                            var clickedTab = TabController.TabPageFromPoint(e.Location);
                            if (clickedTab != null & !object.ReferenceEquals(TabController.SelectedTab, clickedTab))
                            {
                                Native.SetForegroundWindow(Handle);
                                TabController.SelectedTab = clickedTab;
                            }
                        }
                        _selectedTabChanged = false;
                        _ignoreChangeSelectedTabClick = false;
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                FocusIC();
                                int currentTicks = Environment.TickCount;
                                int elapsedTicks = currentTicks - _lastMouseUp;
                                if (elapsedTicks > SystemInformation.DoubleClickTime) 
                                {
	                                _lastMouseUp = currentTicks;
	                                FocusIC();
                                } 
                                else 
                                {
	                                TabController.OnDoubleClickTab(TabController.SelectedTab);
                                }
                                break;
                            case MouseButtons.Middle:
                                CloseConnectionTab();
                                break;
                            case MouseButtons.Right:
                                ShowHideMenuButtons();
                                Native.SetForegroundWindow(Handle);
                                cmenTab.Show(TabController, e.Location);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg, "TabController_MouseUp (UI.Window.Connections) failed" + Constants.vbNewLine + ex.Message, true);
                    }
                }

                /*private void TabController_MouseUp(object sender, MouseEventArgs e)
                {
                    Debug.Print("UI.Window.Connection.TabController_MouseUp()");
                    Debug.Print("_ignoreChangeSelectedTabClick = {0}", _ignoreChangeSelectedTabClick);
                    try
                    {
                        var clickedTab = TabController.TabPageFromPoint(e.Location);
                        if (clickedTab != null & !ReferenceEquals(TabController.SelectedTab, clickedTab))
                        {

                            TabController.SelectedTab = clickedTab;
                            return;
                        }
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                TabController.SelectedTab = TabController.TabPageFromPoint(e.Location);
                                FocusIC();
                                RefreshIC();
                                break;
                            case MouseButtons.Middle:
                                TabController.SelectedTab = TabController.TabPageFromPoint(e.Location);
                                CloseConnectionTab();
                                break;
                            case MouseButtons.Right:
                                ShowHideMenuButtons();
                                cmenTab.Show(TabController, e.Location);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("TabController_MouseUp (UI.Window.Connections) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }*/

                #endregion Tab drag and drop

                private void TabController_Resize(object sender, EventArgs e)
                {
                    RefreshIC();
                }



                private void cmenShowPuTTYMenu_Click(object sender, EventArgs e)
                {
                    try
                    {
                        if (this.TabController.SelectedTab != null)
                        {
                            if (this.TabController.SelectedTab.Tag is InterfaceControl)
                            {
                                InterfaceControl objInterfaceControl =
                                    (InterfaceControl)this.TabController.SelectedTab.Tag;

                                if (objInterfaceControl.Protocol is mRemoteNC.Connection.PuttyBase)
                                {
                                    var objPuttyBase =
                                        (PuttyBase)objInterfaceControl.Protocol;

                                    objPuttyBase.ShowSystemMenu();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("cmenShowPuTTYMenu_Click (UI.Window.Connections) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                public void Prot_Event_Connected(object sender)
                {
                    FocusIC();
                    RefreshIC();
                }
            }
        }
    }
}