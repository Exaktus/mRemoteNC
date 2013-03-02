using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using WeifenLuo.WinFormsUI.Docking;
using mRemoteNC.App;
using mRemoteNC.Connection;
using mRemoteNC.Forms;
using mRemoteNC.Forms.Importer;
using mRemoteNC.Protocol;
using mRemoteNC.Tree;
using My;
using PSTaskDialog;
using Type = mRemoteNC.UI.Window.Type;

namespace mRemoteNC
{
    public partial class frmMain
    {
        private frmMain()
        {
            IsClosing = false;
            InitializeComponent();

            //Added to support default instance behavour in C#
            if (defaultInstance == null)
                defaultInstance = this;
            fullscreenManager = new Tools.Misc.Fullscreen(this);
            Runtime.Windows.Show(Type.Connection);
        }

        #region Default Instance

        public static frmMain defaultInstance;

        public static frmMain Default
        {
            get
            {
                if (defaultInstance == null)
                {
                    defaultInstance = new frmMain();
                    defaultInstance.FormClosed += defaultInstance_FormClosed;
                }

                return defaultInstance;
            }
        }

        private static void defaultInstance_FormClosed(object sender, FormClosedEventArgs e)
        {
            defaultInstance = null;
        }

        #endregion Default Instance

        public Tools.Misc.Fullscreen fullscreenManager;


        public FormWindowState PreviousWindowState { get; set; }

        public delegate void clipboardchangeEventHandler();

        private static clipboardchangeEventHandler clipboardchangeEvent;

        public static event clipboardchangeEventHandler clipboardchange
        {
            add { clipboardchangeEvent = (clipboardchangeEventHandler)Delegate.Combine(clipboardchangeEvent, value); }
            remove { clipboardchangeEvent = (clipboardchangeEventHandler)Delegate.Remove(clipboardchangeEvent, value); }
        }

        private IntPtr fpChainedWindowHandle;

        #region Properties

        public bool IsClosing { get; private set; }

        #endregion Properties

        #region Startup & Shutdown

        private string GetToolStripParentName(ToolStrip toolStrip)
        {
            var panel = toolStrip.Parent as ToolStripPanel;
            var defaultName = String.Empty;

            if (panel == null)
            {
                return defaultName;
            }

            var container = panel.Parent as ToolStripContainer;

            if (container == null)
            {
                return defaultName;
            }

            if (panel == container.LeftToolStripPanel)
            {
                return "LeftToolStripPanel";
            }

            if (panel == container.RightToolStripPanel)
            {
                return "RightToolStripPanel";
            }

            if (panel == container.TopToolStripPanel)
            {
                return "TopToolStripPanel";
            }

            if (panel == container.BottomToolStripPanel)
            {
                return "BottomToolStripPanel";
            }

            return defaultName;
        }

        private ToolStripPanel GetToolStripParentByName(ToolStripContainer container, string parentName)
        {
            if (parentName == "LeftToolStripPanel")
            {
                return container.LeftToolStripPanel;
            }

            if (parentName == "RightToolStripPanel")
            {
                return container.RightToolStripPanel;
            }

            if (parentName == "TopToolStripPanel")
            {
                return container.TopToolStripPanel;
            }

            if (parentName == "BottomToolStripPanel")
            {
                return container.BottomToolStripPanel;
            }

            return null;
        }

        private static DockPanelSkin CreateAEGIS()
        {
            DockPanelSkin skin = new DockPanelSkin();

            skin.AutoHideStripSkin.DockStripGradient.StartColor = Color.FromArgb(255, 233, 236, 250);
            skin.AutoHideStripSkin.DockStripGradient.EndColor = SystemColors.ControlLight;
            skin.AutoHideStripSkin.TabGradient.TextColor = SystemColors.ControlDarkDark;

            skin.DockPaneStripSkin.DocumentGradient.DockStripGradient.StartColor = SystemColors.Control;
            skin.DockPaneStripSkin.DocumentGradient.DockStripGradient.EndColor = SystemColors.Control;
            skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient.StartColor = SystemColors.ControlLightLight;
            skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient.EndColor = SystemColors.ControlLightLight;
            skin.DockPaneStripSkin.DocumentGradient.InactiveTabGradient.StartColor = SystemColors.ControlLight;
            skin.DockPaneStripSkin.DocumentGradient.InactiveTabGradient.EndColor = SystemColors.ControlLight;

            skin.DockPaneStripSkin.ToolWindowGradient.DockStripGradient.StartColor = SystemColors.ControlLight;
            skin.DockPaneStripSkin.ToolWindowGradient.DockStripGradient.EndColor = SystemColors.ControlLight;

            skin.DockPaneStripSkin.ToolWindowGradient.ActiveTabGradient.StartColor = SystemColors.Control;
            skin.DockPaneStripSkin.ToolWindowGradient.ActiveTabGradient.EndColor = SystemColors.Control;

            skin.DockPaneStripSkin.ToolWindowGradient.InactiveTabGradient.StartColor = Color.Transparent;
            skin.DockPaneStripSkin.ToolWindowGradient.InactiveTabGradient.EndColor = Color.Transparent;
            skin.DockPaneStripSkin.ToolWindowGradient.InactiveTabGradient.TextColor = SystemColors.ControlDarkDark;

            skin.DockPaneStripSkin.ToolWindowGradient.ActiveCaptionGradient.StartColor = SystemColors.GradientActiveCaption;
            skin.DockPaneStripSkin.ToolWindowGradient.ActiveCaptionGradient.EndColor = SystemColors.ActiveCaption;
            skin.DockPaneStripSkin.ToolWindowGradient.ActiveCaptionGradient.LinearGradientMode = LinearGradientMode.Vertical;
            skin.DockPaneStripSkin.ToolWindowGradient.ActiveCaptionGradient.TextColor = SystemColors.ActiveCaptionText;

            skin.DockPaneStripSkin.ToolWindowGradient.InactiveCaptionGradient.StartColor = SystemColors.GradientInactiveCaption;
            skin.DockPaneStripSkin.ToolWindowGradient.InactiveCaptionGradient.EndColor = SystemColors.InactiveCaption;
            skin.DockPaneStripSkin.ToolWindowGradient.InactiveCaptionGradient.LinearGradientMode = LinearGradientMode.Vertical;
            skin.DockPaneStripSkin.ToolWindowGradient.InactiveCaptionGradient.TextColor = SystemColors.InactiveCaptionText;

            return skin;
        }

        public void frmMain_Load(object sender, System.EventArgs e)
        {
            My.MyApplication.MyApplication_Startup();
            
            Runtime.Startup.CheckCompatibility();

            Runtime.Startup.CreateLogger();

            // Create gui config load and save objects
            var SettingsLoad = new Config.SettingsManager.Load(this);

            // Load GUI Configuration
            SettingsLoad.Load_Settings();

            Debug.Print("---------------------------" + Constants.vbNewLine + "[START] - " + DateTime.Now);

            Runtime.Startup.ParseCommandLineArgs();

            Runtime.Startup.FirstTimeRun();

            ApplyLanguage();

            fpChainedWindowHandle = Native.SetClipboardViewer(this.Handle);

            Runtime.MessageCollector = new Messages.Collector(Runtime.Windows.errorsForm);

            RDP.Resolutions.AddResolutions();
            PuttyBase.BorderSize = new Size(SystemInformation.FrameBorderSize.Width,
                                                       SystemInformation.CaptionHeight +
                                                       SystemInformation.FrameBorderSize.Height);
            //Size.Subtract(Me.Size, Me.ClientSize)

            Runtime.WindowList = new UI.Window.List();
            
            Runtime.Startup.GetConnectionIcons();
            Runtime.Startup.GetPuttySessions();
            Runtime.GetExtApps();
            Runtime.Windows.treePanel.Focus();

            Node.TreeView = Runtime.Windows.treeForm.tvConnections;

            //LoadCredentials()
            Runtime.LoadConnections();

            if (Settings.Default.StartupComponentsCheck || Settings.Default.FirstStart)
            {
                Runtime.Windows.Show(Type.ComponentsCheck);
            }

            if (!Settings.Default.CheckForUpdatesAsked)
            {
                var CommandButtons = new string[]
                                              {
                                                  Language.strAskUpdatesCommandRecommended,
                                                  Language.strAskUpdatesCommandCustom,
                                                  Language.strAskUpdatesCommandAskLater
                                              };
                cTaskDialog.ShowTaskDialogBox(this,
                                              (new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase
                                                  ()).Info.ProductName, Language.strAskUpdatesMainInstruction,
                                              string.Format(Language.strAskUpdatesContent,
                                                            (new Microsoft.VisualBasic.ApplicationServices.
                                                                WindowsFormsApplicationBase()).Info.ProductName), "", "",
                                              "", "", string.Join("|", CommandButtons), eTaskDialogButtons.None,
                                              eSysIcons.Question, eSysIcons.Question);
                if (cTaskDialog.CommandButtonResult == 0 || cTaskDialog.CommandButtonResult == 1)
                {
                    Settings.Default.CheckForUpdatesAsked = true;
                }
                if (cTaskDialog.CommandButtonResult == 1)
                {
                    Runtime.Windows.ShowUpdatesTab();
                }
            }

            Runtime.Startup.UpdateCheck();
            Runtime.Startup.AnnouncementCheck();


            Runtime.Startup.CreateSQLUpdateHandlerAndStartTimer();

            AddSysMenuItems();
            Microsoft.Win32.SystemEvents.DisplaySettingsChanged += DisplayChanged;
            AddQuickTextsToToolBar();
            AddExternalToolsToToolBar();
            this.Opacity = 1;
            ToolStrip1.Visible = false;
            quickTextToolbarToolStripMenuItem.Checked = tsQuickTexts.Visible;

            importToolStripMenuItem.Visible =
#if DEBUG
                true;
#else
            false;
#endif

            ChangeToolStripLockState();
            foreach (Info con in Runtime.ConnectionList.Cast<Info>().Where(con => con.ConnectOnStartup))
            {
                Runtime.OpenConnection(con, Info.Force.None);
            }
            Focus();
            BringToFront();
        }
        

        private void frmMain_ResizeBegin(object sender, EventArgs e)
        {
            _inSizeMove = true;
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if ((WindowState == FormWindowState.Minimized))
            {
                if (Settings.Default.MinimizeToTray)
                {
                    if ((Runtime.NotificationAreaIcon == null))
                    {
                        Runtime.NotificationAreaIcon = new Tools.Controls.NotificationAreaIcon();
                    }
                    Hide();
                }
            }
            else
            {
                PreviousWindowState = WindowState;
            }
        }

        private void frmMain_ResizeEnd(object sender, EventArgs e)
        {
            _inSizeMove = false;
            ActivateConnection();
        }

        private void ApplyLanguage()
        {
            mMenFile.Text = Language.strMenuFile;
            mMenFileNew.Text = Language.strMenuNewConnectionFile;
            mMenFileNewConnection.Text = Language.strNewConnection;
            mMenFileNewFolder.Text = Language.strNewFolder;
            mMenFileLoad.Text = Language.strMenuOpenConnectionFile;
            mMenFileSave.Text = Language.strMenuSaveConnectionFile;
            mMenFileSaveAs.Text = Language.strMenuSaveConnectionFileAs;
            mMenFileImportExport.Text = Language.strImportExport;
            ImportFromActiveDirectoryToolStripMenuItem.Text = Language.strImportAD;
            ImportFromPortScanToolStripMenuItem.Text = Language.strImportPortScan;
            ImportFromRDPFileToolStripMenuItem.Text = Language.strImportRDPFiles;
            ImportFromXMLFileToolStripMenuItem.Text = Language.strImportmRemoteXML;
            ExportToXMLFileToolStripMenuItem.Text = Language.strExportmRemoteXML;
            mMenFileExit.Text = Language.strMenuExit;
            mConStatus.Text = Language.frmMain_Connections_status;
            quickTextToolbarToolStripMenuItem.Text = Language.frmMain_ApplyLanguage_Quick_Text_Toolbar;
            mQuickText.Text = Language.frmMain_ApplyLanguage_Quick_Text;
            lockToolStripMenuItem.Text = Language.frmMain_ApplyLanguage_Lock_toolbar;

            mMenView.Text = Language.strMenuView;
            mMenViewAddConnectionPanel.Text = Language.strMenuAddConnectionPanel;
            mMenViewConnectionPanels.Text = Language.strMenuConnectionPanels;
            mMenViewConnections.Text = Language.strMenuConnections;
            mMenViewConfig.Text = Language.strMenuConfig;
            mMenViewSessions.Text = Language.strMenuSessions;
            mMenViewErrorsAndInfos.Text = Language.strMenuNotifications;
            mMenViewScreenshotManager.Text = Language.strMenuScreenshotManager;
            mMenViewJumpTo.Text = Language.strMenuJumpTo;
            mMenViewJumpToConnectionsConfig.Text = Language.strMenuConnectionsAndConfig;
            mMenViewJumpToSessionsScreenshots.Text = Language.strMenuSessionsAndScreenshots;
            mMenViewJumpToErrorsInfos.Text = Language.strMenuNotifications;
            mMenViewResetLayout.Text = Language.strMenuResetLayout;
            mMenViewQuickConnectToolbar.Text = Language.strMenuQuickConnectToolbar;
            mMenViewExtAppsToolbar.Text = Language.strMenuExternalToolsToolbar;
            mMenViewFullscreen.Text = Language.strMenuFullScreen;

            mMenTools.Text = Language.strMenuTools;
            mMenToolsSSHTransfer.Text = Language.strMenuSSHFileTransfer;
            mMenToolsExternalApps.Text = Language.strMenuExternalTools;
            mMenToolsPortScan.Text = Language.strMenuPortScan;
            mMenToolsComponentsCheck.Text = Language.strComponentsCheck;
            mMenToolsUpdate.Text = Language.strMenuCheckForUpdates;
            mMenToolsOptions.Text = Language.strMenuOptions;

            mMenInfo.Text = Language.strMenuHelp;
            mMenInfoHelp.Text = Language.strMenuHelpContents;
            mMenInfoForum.Text = Language.strMenuSupportForum;
            mMenInfoBugReport.Text = Language.strMenuReportBug;
            mMenInfoDonate.Text = Language.strMenuDonate;
            mMenInfoWebsite.Text = Language.strMenuWebsite;
            mMenInfoAbout.Text = Language.strMenuAbout;
            mMenInfoAnnouncements.Text = Language.strMenuAnnouncements;

            lblQuickConnect.Text = Language.strLabelConnect;
            btnQuickyPlay.Text = Language.strMenuConnect;
            mMenQuickyCon.Text = Language.strMenuConnections;

            cMenToolbarShowText.Text = Language.strMenuShowText;

            ToolStripButton1.Text = Language.strConnect;
            ToolStripButton2.Text = Language.strScreenshot;
            ToolStripButton3.Text = Language.strRefresh;

            ToolStripSplitButton1.Text = Language.strSpecialKeys;
            ToolStripMenuItem1.Text = Language.strKeysCtrlAltDel;
            ToolStripMenuItem2.Text = Language.strKeysCtrlEsc;
        }

        public Control GetControlByName(string ControlName)
        {
            return Controls.Cast<Control>().FirstOrDefault(c => c.Name == ControlName);
        }

        public void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Settings.Default.ConfirmExit & !(Runtime.WindowList == null || Runtime.WindowList.Count == 0))
            {
                var openConnections = Runtime.WindowList.OfType<UI.Window.Connection>()
                    .Aggregate(0, (current, connectionWindow) => current + connectionWindow.TabController.TabPages.Count);

                if (openConnections > 0)
                {
                    DialogResult result = cTaskDialog.MessageBox(this, Application.ProductName, Language.strConfirmExitMainInstruction, "", "", "", Language.strCheckboxDoNotShowThisMessageAgain, eTaskDialogButtons.YesNo, eSysIcons.Question, eSysIcons.Question);
                    if (cTaskDialog.VerificationChecked)
                    {
                        Settings.Default.ConfirmExit = false;
                    }
                    if (result == DialogResult.No)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
            
            Runtime.Shutdown.BeforeQuit();
            My.MyApplication.MyApplication_Shutdown();

            IsClosing = true;

            if (Runtime.WindowList != null)
            {
                foreach (UI.Window.Base Window in Runtime.WindowList)
                {
                    Window.Close();
                }
            }

            Debug.Print("[END] - " + DateTime.Now);
        }

        #endregion Startup & Shutdown

        #region Timer

        private int tmrRuns = 0;

        public void tmrShowUpdate_Tick(System.Object sender, System.EventArgs e)
        {
            if (tmrRuns == 5)
            {
                this.tmrShowUpdate.Enabled = false;
            }

            if (Runtime.IsUpdateAvailable)
            {
                Runtime.Windows.Show(UI.Window.Type.Update);
                this.tmrShowUpdate.Enabled = false;
            }

            if (Runtime.IsAnnouncementAvailable)
            {
                Runtime.Windows.Show(UI.Window.Type.Announcement);
                this.tmrShowUpdate.Enabled = false;
            }

            tmrRuns++;
        }

        public void tmrAutoSave_Tick(System.Object sender, System.EventArgs e)
        {
            Runtime.MessageCollector.AddMessage(Messages.MessageClass.InformationMsg, "Doing AutoSave", true);
            Runtime.SaveConnections();
        }

        #endregion Timer

        #region Ext Apps Toolbar

        public void cMenToolbarShowText_Click(System.Object sender, System.EventArgs e)
        {
            SwitchToolBarText(Convert.ToBoolean(!cMenToolbarShowText.Checked));
        }

        public void AddExternalToolsToToolBar()
        {
            try
            {
                tsExternalTools.Items.Clear();

                foreach (Tools.ExternalTool tool in Runtime.ExternalTools)
                {
                    var button = tsExternalTools.Items.Add((string)tool.DisplayName, tool.Image, tsExtAppEntry_Click);

                    button.DisplayStyle = cMenToolbarShowText.Checked
                                              ? ToolStripItemDisplayStyle.ImageAndText
                                              : (button.Image != null
                                                     ? ToolStripItemDisplayStyle.Image
                                                     : ToolStripItemDisplayStyle.ImageAndText);

                    button.Tag = tool;
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    string.Format(Language.strErrorAddExternalToolsToToolBarFailed,
                                                                  ex.Message), true);
            }
        }

        private void tsExtAppEntry_Click(System.Object sender, System.EventArgs e)
        {
            Tools.ExternalTool extA = (Tools.ExternalTool)((System.Windows.Forms.ToolStripButton)sender).Tag;

            if (Node.GetNodeType(Tree.Node.SelectedNode) == Node.Type.Connection)
            {
                extA.Start((Info)Node.SelectedNode.Tag);
            }
            else
            {
                extA.Start();
            }
        }

        public void SwitchToolBarText(bool show)
        {
            foreach (ToolStripButton tItem in tsExternalTools.Items)
            {
                tItem.DisplayStyle = show
                                         ? ToolStripItemDisplayStyle.ImageAndText
                                         : (tItem.Image != null
                                                ? ToolStripItemDisplayStyle.Image
                                                : ToolStripItemDisplayStyle.ImageAndText);
            }

            cMenToolbarShowText.Checked = show;
        }

        #endregion Ext Apps Toolbar

        #region Menu

        #region File

        public void mMenFile_DropDownOpening(System.Object sender, System.EventArgs e)
        {
            if (Tree.Node.GetNodeType(Node.SelectedNode) == Tree.Node.Type.Root)
            {
                mMenFileImportExport.Enabled = true;
                mMenFileDelete.Enabled = false;
                mMenFileRename.Enabled = true;
                mMenFileDuplicate.Enabled = false;
                mMenFileDelete.Text = Language.strMenuDelete;
                mMenFileRename.Text = Language.strMenuRenameFolder;
                mMenFileDuplicate.Text = Language.strMenuDuplicate;
            }
            else if (Tree.Node.GetNodeType(Node.SelectedNode) == Tree.Node.Type.Container)
            {
                mMenFileImportExport.Enabled = true;
                mMenFileDelete.Enabled = true;
                mMenFileRename.Enabled = true;
                mMenFileDuplicate.Enabled = true;
                mMenFileDelete.Text = Language.strMenuDeleteFolder;
                mMenFileRename.Text = Language.strMenuRenameFolder;
                mMenFileDuplicate.Text = Language.strMenuDuplicateFolder;
            }
            else if (Tree.Node.GetNodeType(Node.SelectedNode) == Tree.Node.Type.Connection)
            {
                mMenFileImportExport.Enabled = false;
                mMenFileDelete.Enabled = true;
                mMenFileRename.Enabled = true;
                mMenFileDuplicate.Enabled = true;
                mMenFileDelete.Text = Language.strMenuDeleteConnection;
                mMenFileRename.Text = Language.strMenuRenameConnection;
                mMenFileDuplicate.Text = Language.strMenuDuplicateConnection;
            }
            else
            {
                mMenFileImportExport.Enabled = false;
                mMenFileDelete.Enabled = false;
                mMenFileRename.Enabled = false;
                mMenFileDuplicate.Enabled = false;
                mMenFileDelete.Text = Language.strMenuDelete;
                mMenFileRename.Text = Language.strMenuRename;
                mMenFileDuplicate.Text = Language.strMenuDuplicate;
            }
        }

        public void mMenFileNewConnection_Click(System.Object sender, System.EventArgs e)
        {
            Runtime.Windows.treeForm.AddConnection();
            Runtime.SaveConnectionsBG();
        }

        public void mMenFileNewFolder_Click(System.Object sender, System.EventArgs e)
        {
            Runtime.Windows.treeForm.AddFolder();
            Runtime.SaveConnectionsBG();
        }

        public void mMenFileNew_Click(System.Object sender, System.EventArgs e)
        {
            SaveFileDialog lD = Tools.Controls.ConnectionsSaveAsDialog();
            if (lD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Runtime.NewConnections(lD.FileName);
            }
            else
            {
                return;
            }
        }

        public void mMenFileLoad_Click(System.Object sender, System.EventArgs e)
        {
            if (Runtime.IsConnectionsFileLoaded)
            {
                switch (
                    Interaction.MsgBox(Language.strSaveConnectionsFileBeforeOpeningAnother,
                                       MsgBoxStyle.YesNoCancel | MsgBoxStyle.Question, null))
                {
                    case MsgBoxResult.Yes:
                        Runtime.SaveConnections();
                        break;
                    case MsgBoxResult.Cancel:
                        return;
                }
            }

            Runtime.LoadConnections(true);
        }

        public void mMenFileSave_Click(System.Object sender, System.EventArgs e)
        {
            Runtime.SaveConnections();
        }

        public void mMenFileSaveAs_Click(System.Object sender, System.EventArgs e)
        {
            Runtime.Windows.Show(UI.Window.Type.SaveAs);
        }

        public void mMenFileExit_Click(System.Object sender, System.EventArgs e)
        {
            Settings.Default.Save();
            Runtime.Shutdown.Quit();
        }

        public void mMenFileDelete_Click(System.Object sender, System.EventArgs e)
        {
            Tree.Node.DeleteSelectedNode();
            Runtime.SaveConnectionsBG();
        }

        public void mMenFileRename_Click(System.Object sender, System.EventArgs e)
        {
            Tree.Node.StartRenameSelectedNode();
            Runtime.SaveConnectionsBG();
        }

        public void mMenFileDuplicate_Click(System.Object sender, System.EventArgs e)
        {
            Tree.Node.CloneNode(Tree.Node.SelectedNode);
            Runtime.SaveConnectionsBG();
        }

        #endregion File

        #region View

        public void mMenView_DropDownOpening(object sender, System.EventArgs e)
        {
            this.mMenViewConnections.Checked = System.Convert.ToBoolean(!Runtime.Windows.treeForm.IsHidden);
            this.mMenViewConfig.Checked = System.Convert.ToBoolean(!Runtime.Windows.configForm.IsHidden);
            this.mMenViewErrorsAndInfos.Checked = System.Convert.ToBoolean(!Runtime.Windows.errorsForm.IsHidden);
            this.mMenViewSessions.Checked = (Runtime.Windows.sessionsForm == null) ||
                                            (!Runtime.Windows.sessionsForm.IsHidden);
            this.mMenViewScreenshotManager.Checked = System.Convert.ToBoolean(!Runtime.Windows.screenshotForm.IsHidden);
            mConStatus.Checked = System.Convert.ToBoolean(!Runtime.Windows.connectionStatusForm.IsHidden);

            this.mMenViewExtAppsToolbar.Checked = tsExternalTools.Visible;
            this.mMenViewQuickConnectToolbar.Checked = tsQuickConnect.Visible;

            this.mMenViewConnectionPanels.DropDownItems.Clear();

            for (int i = 0; i <= Runtime.WindowList.Count - 1; i++)
            {
                ToolStripMenuItem tItem = new ToolStripMenuItem((string)(Runtime.WindowList[i].Text),
                                                                Runtime.WindowList[i].Icon.ToBitmap(),
                                                                ConnectionPanelMenuItem_Click) { Tag = Runtime.WindowList[i] };

                this.mMenViewConnectionPanels.DropDownItems.Add(tItem);
            }

            this.mMenViewConnectionPanels.Enabled = this.mMenViewConnectionPanels.DropDownItems.Count > 0;
        }

        private void ConnectionPanelMenuItem_Click(object sender, System.EventArgs e)
        {
            (((Control)sender).Tag as UI.Window.Base).Show(this.pnlDock);
            (((Control)sender).Tag as UI.Window.Base).Focus();
        }

        public void mMenViewSessions_Click(object sender, System.EventArgs e)
        {
            if (this.mMenViewSessions.Checked == false)
            {
                Runtime.Windows.sessionsPanel.Show(this.pnlDock);
                this.mMenViewSessions.Checked = true;
            }
            else
            {
                Runtime.Windows.sessionsPanel.Hide();
                this.mMenViewSessions.Checked = false;
            }
        }

        public void mMenViewConnections_Click(System.Object sender, System.EventArgs e)
        {
            if (this.mMenViewConnections.Checked == false)
            {
                Runtime.Windows.treePanel.Show(this.pnlDock);
                this.mMenViewConnections.Checked = true;
            }
            else
            {
                Runtime.Windows.treePanel.Hide();
                this.mMenViewConnections.Checked = false;
            }
        }

        public void mMenViewConfig_Click(System.Object sender, System.EventArgs e)
        {
            if (this.mMenViewConfig.Checked == false)
            {
                Runtime.Windows.configPanel.Show(this.pnlDock);
                this.mMenViewConfig.Checked = true;
            }
            else
            {
                Runtime.Windows.configPanel.Hide();
                this.mMenViewConfig.Checked = false;
            }
        }

        public void mMenViewErrorsAndInfos_Click(System.Object sender, System.EventArgs e)
        {
            if (this.mMenViewErrorsAndInfos.Checked == false)
            {
                Runtime.Windows.errorsPanel.Show(this.pnlDock);
                this.mMenViewErrorsAndInfos.Checked = true;
            }
            else
            {
                Runtime.Windows.errorsPanel.Hide();
                this.mMenViewErrorsAndInfos.Checked = false;
            }
        }

        public void mMenViewScreenshotManager_Click(System.Object sender, System.EventArgs e)
        {
            if (this.mMenViewScreenshotManager.Checked == false)
            {
                Runtime.Windows.screenshotPanel.Show(this.pnlDock);
                this.mMenViewScreenshotManager.Checked = true;
            }
            else
            {
                Runtime.Windows.screenshotPanel.Hide();
                this.mMenViewScreenshotManager.Checked = false;
            }
        }

        public void mMenViewJumpToConnectionsConfig_Click(object sender, System.EventArgs e)
        {
            if (pnlDock.ActiveContent == Runtime.Windows.treePanel)
            {
                Runtime.Windows.configForm.Activate();
            }
            else
            {
                Runtime.Windows.treeForm.Activate();
            }
        }

        public void mMenViewJumpToSessionsScreenshots_Click(object sender, System.EventArgs e)
        {
            if (pnlDock.ActiveContent == Runtime.Windows.sessionsPanel)
            {
                Runtime.Windows.screenshotForm.Activate();
            }
            else
            {
                Runtime.Windows.sessionsForm.Activate();
            }
        }

        public void mMenViewJumpToErrorsInfos_Click(object sender, System.EventArgs e)
        {
            Runtime.Windows.errorsForm.Activate();
        }

        public void mMenViewResetLayout_Click(System.Object sender, System.EventArgs e)
        {
            if (Interaction.MsgBox(Language.strConfirmResetLayout, MsgBoxStyle.Question | MsgBoxStyle.YesNo, null) ==
                MsgBoxResult.Yes)
            {
                Runtime.Startup.SetDefaultLayout();
            }
        }

        public void mMenViewAddConnectionPanel_Click(System.Object sender, System.EventArgs e)
        {
            Runtime.AddPanel();
        }

        public void mMenViewExtAppsToolbar_Click(System.Object sender, System.EventArgs e)
        {
            if (mMenViewExtAppsToolbar.Checked == false)
            {
                tsExternalTools.Visible = true;
                mMenViewExtAppsToolbar.Checked = true;
            }
            else
            {
                tsExternalTools.Visible = false;
                mMenViewExtAppsToolbar.Checked = false;
            }
        }

        public void mMenViewQuickConnectToolbar_Click(System.Object sender, System.EventArgs e)
        {
            if (mMenViewQuickConnectToolbar.Checked == false)
            {
                tsQuickConnect.Visible = true;
                mMenViewQuickConnectToolbar.Checked = true;
            }
            else
            {
                tsQuickConnect.Visible = false;
                mMenViewQuickConnectToolbar.Checked = false;
            }
        }

        public void mMenViewFullscreen_Click(System.Object sender, System.EventArgs e)
        {
            if (frmMain.defaultInstance.fullscreenManager.FullscreenActive)
            {
                frmMain.defaultInstance.fullscreenManager.ExitFullscreen();
                this.mMenViewFullscreen.Checked = false;
            }
            else
            {
                frmMain.defaultInstance.fullscreenManager.EnterFullscreen();
                this.mMenViewFullscreen.Checked = true;
            }
        }

        #endregion View

        #region Tools

        public void mMenToolsUpdate_Click(System.Object sender, System.EventArgs e)
        {
            Runtime.Windows.Show(UI.Window.Type.Update);
        }

        public void mMenToolsSSHTransfer_Click(System.Object sender, System.EventArgs e)
        {
            Runtime.Windows.Show(UI.Window.Type.SSHTransfer);
        }

        public void mMenToolsUVNCSC_Click(System.Object sender, System.EventArgs e)
        {
            Runtime.Windows.Show(UI.Window.Type.UltraVNCSC);
        }

        public void mMenToolsExternalApps_Click(System.Object sender, System.EventArgs e)
        {
            Runtime.Windows.Show(UI.Window.Type.ExternalApps);
        }

        public void mMenToolsPortScan_Click(System.Object sender, System.EventArgs e)
        {
            Runtime.Windows.Show(UI.Window.Type.PortScan, Tools.PortScan.PortScanMode.Normal);
        }

        public void mMenToolsComponentsCheck_Click(System.Object sender, System.EventArgs e)
        {
            Runtime.Windows.Show(UI.Window.Type.ComponentsCheck);
        }

        public void mMenToolsOptions_Click(System.Object sender, System.EventArgs e)
        {
            Runtime.Windows.Show(UI.Window.Type.Options);
        }

        private void mQuickText_Click(object sender, EventArgs e)
        {
            Runtime.Windows.Show(UI.Window.Type.QuickText);
        }

        #endregion Tools

        #region Quick Connect

        public void btnQuickyPlay_ButtonClick(object sender, System.EventArgs e)
        {
            Runtime.CreateQuicky(QuickyText());
        }

        public void btnQuickyPlay_DropDownOpening(object sender, System.EventArgs e)
        {
            CreateQuickyButtons();
        }

        private void CreateQuickyButtons()
        {
            try
            {
                btnQuickyPlay.DropDownItems.Clear();

                foreach (ToolStripMenuItem nBtn in
                    typeof(Protocols).GetFields().Where(fI => fI.Name != "value__" && fI.Name != "NONE" && fI.Name != "IntApp")
                                                    .Select(fI => new ToolStripMenuItem { Text = fI.Name }))
                {
                    btnQuickyPlay.DropDownItems.Add(nBtn);
                    nBtn.Click += QuickyProtocolButton_Click;
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    (string)
                                                    ("CreateButtons (frmMain) failed" + Constants.vbNewLine + ex.Message),
                                                    true);
            }
        }

        private void QuickyProtocolButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                Connection.Info conI = Runtime.CreateQuicky(QuickyText(),
                                                            (Protocols)
                                                            Tools.Misc.StringToEnum(typeof(Protocols),
                                                                                    ((Control)sender).Text));

                if (conI.Port == 0)
                {
                    conI.SetDefaultPort();

                    if (mRemoteNC.QuickConnect.History.Exists(conI.Hostname) == false)
                    {
                        QuickConnect.History.Add(conI.Hostname);
                    }
                }
                else
                {
                    if (QuickConnect.History.Exists(conI.Hostname) == false)
                    {
                        QuickConnect.History.Add(conI.Hostname + ":" + conI.Port);
                    }
                }

                Runtime.OpenConnection(conI, Info.Force.DoNotJump);
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    (string)
                                                    ("QuickyProtocolButton_Click (frmMain) failed" + Constants.vbNewLine +
                                                     ex.Message), true);
            }
        }

        public void cmbQuickConnect_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Runtime.CreateQuicky(QuickyText());
            }
        }

        public void lblQuickConnect_Click(System.Object sender, System.EventArgs e)
        {
            this.cmbQuickConnect.Focus();
        }

        private string QuickyText()
        {
            return cmbQuickConnect.Text.Trim();
        }

        #endregion Quick Connect

        #region Info

        public void mMenInfoHelp_Click(System.Object sender, System.EventArgs e)
        {
            Runtime.Windows.Show(UI.Window.Type.Help);
        }

        public void mMenInfoForum_Click(System.Object sender, System.EventArgs e)
        {
            Runtime.GoToForum();
        }

        public void mMenInfoBugReport_Click(System.Object sender, System.EventArgs e)
        {
            Runtime.GoToBugs();
        }

        public void mMenInfoWebsite_Click(System.Object sender, System.EventArgs e)
        {
            Runtime.GoToWebsite();
        }

        public void mMenInfoDonate_Click(System.Object sender, System.EventArgs e)
        {
            Runtime.GoToDonate();
        }

        public void mMenInfoAnnouncements_Click(System.Object sender, System.EventArgs e)
        {
            Runtime.Windows.Show(UI.Window.Type.Announcement);
        }

        public void mMenInfoAbout_Click(System.Object sender, System.EventArgs e)
        {
            Runtime.Windows.Show(UI.Window.Type.About);
        }

        #endregion Info

        #endregion Menu

        #region Connections DropDown

        public void mMenQuickyCon_DropDownOpening(object sender, System.EventArgs e)
        {
            mMenQuickyCon.DropDownItems.Clear();

            foreach (TreeNode tNode in Runtime.Windows.treeForm.tvConnections.Nodes)
            {
                AddNodeToMenu(tNode.Nodes, mMenQuickyCon);
            }
        }

        private void AddNodeToMenu(TreeNodeCollection tnc, ToolStripMenuItem menToolStrip)
        {
            try
            {
                foreach (TreeNode tNode in tnc)
                {
                    var tMenItem = new ToolStripMenuItem {Text = tNode.Text, Tag = tNode};

                    switch (Node.GetNodeType(tNode))
                    {
                        case Node.Type.Container:
                            tMenItem.Image = global::My.Resources.Resources.Folder;
                            tMenItem.Tag = tNode.Tag;
                            menToolStrip.DropDownItems.Add(tMenItem);
                            AddNodeToMenu(tNode.Nodes, tMenItem);
                            break;
                        case Node.Type.Connection:
                            tMenItem.Image = Runtime.Windows.treeForm.imgListTree.Images[tNode.ImageIndex];
                            tMenItem.Tag = tNode.Tag;
                            menToolStrip.DropDownItems.Add(tMenItem);
                            break;
                    }

                    tMenItem.MouseDown += ConMenItem_MouseDown;
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    "AddNodeToMenu failed" + Constants.vbNewLine + ex.Message,
                                                    true);
            }
        }

        private void ConMenItem_MouseDown(Object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var tag = ((Control)sender).Tag as Info;
                if (tag != null)
                {
                    Runtime.OpenConnection(tag);
                }
            }
        }

        #endregion Connections DropDown

        #region Window Overrides and DockPanel Stuff
        private bool _inMouseActivate = false;
        private bool _inSizeMove = false;

        protected override void WndProc(ref Message m)
        {
            try
            {
#if Config
    //Debug.Print(m.Msg)
#endif

                switch (m.Msg)
                {
                    case Native.WM_MOUSEACTIVATE:
                        _inMouseActivate = true;
                        break;
                    case Native.WM_ACTIVATEAPP:
                        _inMouseActivate = false;
                        break;
                    case Native.WM_ACTIVATE:
                        // Ingore this message if it wasn't triggered by a click
                        if (Native.LOWORD(m.WParam) != Native.WA_CLICKACTIVE)
                        {
                            break;
                        }

                        Control control = FromChildHandle(Native.WindowFromPoint(MousePosition));
                        if (control != null)
                        {
                            // Let ComboBoxes get focus but don't simulate a mouse event
                            if (control is ComboBox||control is TreeView)
                            {
                                break;
                            }

                            if (control.CanSelect || control is MenuStrip || control is ToolStrip ||
                                control is Crownwood.Magic.Controls.InertButton)
                            {
                                // Simulate a mouse event since one wasn't generated by Windows
                                Point clientMousePosition = control.PointToClient(MousePosition);
                                Native.SendMessage(control.Handle, System.Convert.ToInt32(Native.WM_LBUTTONDOWN),
                                                   System.Convert.ToInt32(Native.MK_LBUTTON),
                                                   Native.MAKELPARAM(clientMousePosition.X, clientMousePosition.Y));

                                control.Focus();
                                break;
                            }
                        }

                        // This handles activations from clicks that did not start a size/move operation
                        ActivateConnection();
                        break;
                    case Native.WM_WINDOWPOSCHANGED:
                        // Ignore this message if the window wasn't activated
                        var windowPos =
                            (Native.WINDOWPOS)Marshal.PtrToStructure(m.LParam, typeof(Native.WINDOWPOS));
                        if ((windowPos.flags & Native.SWP_NOACTIVATE) != 0)
                        {
                            break;
                        }

                        // This handles all other activations
                        if (!_inMouseActivate && !_inSizeMove)
                        {
                            ActivateConnection();
                        }
                        break;
                    case Native.WM_SYSCOMMAND:
                        for (int i = 0; i <= SysMenSubItems.Length - 1; i++)
                        {
                            if (SysMenSubItems[i] == (int)m.WParam)
                            {
                                Runtime.Screens.SendFormToScreen(Screen.AllScreens[i]);
                                break;
                            }
                        }
                        break;
                    case Native.WM_DRAWCLIPBOARD:
                        Native.SendMessage(fpChainedWindowHandle, m.Msg, (int)m.LParam, (int)m.WParam);
                        if (clipboardchangeEvent != null)
                            clipboardchangeEvent();
                        break;
                    case Native.WM_CHANGECBCHAIN:
                        //Send to the next window
                        Native.SendMessage(fpChainedWindowHandle, m.Msg, (int)m.LParam, (int)m.WParam);
                        fpChainedWindowHandle = m.LParam;
                        break;
                }
                base.WndProc(ref m);
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg, ex.ToString(), true);
            }
        }

        private void ActivateConnection()
        {
            if (this.pnlDock.ActiveDocument is UI.Window.Connection)
            {
                UI.Window.Connection cW = (UI.Window.Connection)this.pnlDock.ActiveDocument;
                if (cW.TabController.SelectedTab != null)
                {
                    InterfaceControl ifc = cW.TabController.SelectedTab.Tag as InterfaceControl;
                    if (ifc != null)
                    {
                        ifc.Protocol.Focus();
                        (ifc.FindForm() as UI.Window.Connection).RefreshIC();
                    }
                }
            }
        }

        public void pnlDock_ActiveDocumentChanged(object sender, System.EventArgs e)
        {
            ActivateConnection();
        }

        #endregion Window Overrides and DockPanel Stuff

        #region Screen Stuff

        private void DisplayChanged(object sender, System.EventArgs e)
        {
            ResetSysMenuItems();
            AddSysMenuItems();
        }

        private int[] SysMenSubItems = new int[51];

        private static void ResetSysMenuItems()
        {
            Runtime.SystemMenu.Reset();
        }

        private void AddSysMenuItems()
        {
            Runtime.SystemMenu = new Tools.SystemMenu(this.Handle);
            IntPtr popMen = Runtime.SystemMenu.CreatePopupMenuItem();

            for (int i = 0; i <= Screen.AllScreens.Length - 1; i++)
            {
                SysMenSubItems[i] = System.Convert.ToInt32(200 + i);
                Runtime.SystemMenu.AppendMenuItem(popMen, Tools.SystemMenu.Flags.MF_STRING, SysMenSubItems[i],
                                                  Language.strScreen + " " + (i.ToString() + 1));
            }

            Runtime.SystemMenu.InsertMenuItem(Runtime.SystemMenu.SystemMenuHandle, 0,
                                              Tools.SystemMenu.Flags.MF_POPUP | Tools.SystemMenu.Flags.MF_BYPOSITION,
                                              popMen, Language.strSendTo);
            Runtime.SystemMenu.InsertMenuItem(Runtime.SystemMenu.SystemMenuHandle, 1,
                                              Tools.SystemMenu.Flags.MF_SEPARATOR, IntPtr.Zero, null);
        }

        #endregion Screen Stuff

        private void statusToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (this.mConStatus.Checked == false)
                {
                    //Runtime.Windows.Show(Type.ConnectionStatus);
                    Runtime.Windows.connectionStatusForm = new ConnectionStatusForm(Runtime.Windows.connectionStatusPanel);
                    Runtime.Windows.connectionStatusPanel = Runtime.Windows.connectionStatusForm;
                    Runtime.Windows.connectionStatusPanel.Show(this.pnlDock);
                    this.mConStatus.Checked = true;
                }
                else
                {
                    if (Runtime.Windows.connectionStatusForm.DockPanel != null)
                    {
                        Runtime.Windows.connectionStatusPanel.Hide();
                    }
                    this.mConStatus.Checked = false;
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                    (string)("statusToolStripMenuItem_Click failed" + Constants.vbNewLine + ex.Message),
                                    true);
            }
        }

        private void mMenView_Click(object sender, EventArgs e)
        {
            
        }

        private void tsContainer_TopToolStripPanel_Click(object sender, EventArgs e)
        {
        }

        private void tsQuickTextsEntry_Click(Object sender, EventArgs e)
        {
            var extA = (Tools.QuickText)((ToolStripButton)sender).Tag;
            var cW = pnlDock.ActiveDocument as UI.Window.Connection;
            if (cW != null)
            {
                if (cW.TabController.SelectedTab != null)
                {
                    var ifc = cW.TabController.SelectedTab.Tag as InterfaceControl;
                    if (ifc != null)
                    {
                        ifc.Protocol.Focus();
                        var connection = ifc.FindForm() as UI.Window.Connection;
                        if (connection != null)
                            connection.RefreshIC();
                        SendKeys.Send(extA.ToCommand(ifc.Protocol.InterfaceControl.Info));
                    }
                }
            }
        }

        public void AddQuickTextsToToolBar()
        {
            try
            {
                tsQuickTexts.Items.Clear();

                foreach (Tools.QuickText tool in Runtime.QuickTexts)
                {
                    var button = tsQuickTexts.Items.Add(tool.DisplayName, null, tsQuickTextsEntry_Click);

                    button.DisplayStyle = ToolStripItemDisplayStyle.Text;

                    button.Tag = tool;
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    string.Format(Language.strErrorAddExternalToolsToToolBarFailed,
                                                                  ex.Message), true);
            }
        }

        private void tsExternalTools_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void quickTextToolbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsQuickTexts.Visible = quickTextToolbarToolStripMenuItem.Checked;
        }

        private void tsQuickTexts_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void msMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void cmbQuickConnect_Click(object sender, EventArgs e)
        {

        }

        private void lockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.ToolStripsIsLocked = lockToolStripMenuItem.Checked;
            ChangeToolStripLockState();
        }

        private void ChangeToolStripLockState()
        {
            try
            {
                foreach (var ts in new[] { tsContainer.TopToolStripPanel, 
                                        tsContainer.BottomToolStripPanel, 
                                        tsContainer.LeftToolStripPanel, 
                                        tsContainer.RightToolStripPanel }
                                    .SelectMany(panel => panel.Controls.OfType<ToolStrip>()))
                {
                    ts.GripStyle = Settings.Default.ToolStripsIsLocked ? ToolStripGripStyle.Hidden : ToolStripGripStyle.Visible;
                }
            }
            catch (Exception ex)
            {
                 Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,ex.Message, false);
            }
        }

        private void ImportFromRDPFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ImportFromXMLFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ImportFromActiveDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ExportToXMLFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void importToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            new ImportForm().ShowDialog();
        }
    }
}