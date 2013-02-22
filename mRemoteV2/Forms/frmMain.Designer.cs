
using System.Collections.Generic;
using System;
using AxWFICALib;
using System.Drawing;
using System.Diagnostics;
using System.Data;
using AxMSTSCLib;
using Microsoft.VisualBasic;
using System.Collections;
using System.Windows.Forms;
using My;


namespace mRemoteNC
{
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]public partial class frmMain : System.Windows.Forms.Form
	{
		
		//Form overrides dispose to clean up the component list.
		[System.Diagnostics.DebuggerNonUserCode()]protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && components != null)
				{
					components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

        //Required by the Windows Form Designer
		
		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            WeifenLuo.WinFormsUI.Docking.DockPanelSkin dockPanelSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPanelSkin();
            WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin autoHideStripSkin1 = new WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient1 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin dockPaneStripSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient dockPaneStripGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient2 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient3 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient4 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient5 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient3 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient6 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient7 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.pnlDock = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.mMenSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsContainer = new System.Windows.Forms.ToolStripContainer();
            this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ToolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.ToolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.ToolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSplitButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsPanelMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsQuickTexts = new System.Windows.Forms.ToolStrip();
            this.cMenExtAppsToolbar = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cMenToolbarShowText = new System.Windows.Forms.ToolStripMenuItem();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.mMenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenFileNewConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenFileNewFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mMenFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenFileLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mMenFileImportExport = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportFromXMLFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportFromRDPFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportFromActiveDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportFromPortScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.ExportToXMLFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenFileSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.mMenFileDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenFileRename = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenFileDuplicate = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenFileSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.mMenFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenView = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenViewAddConnectionPanel = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenViewConnectionPanels = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mMenViewConnections = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenViewConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenViewSessions = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenViewErrorsAndInfos = new System.Windows.Forms.ToolStripMenuItem();
            this.mConStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenViewScreenshotManager = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mMenViewJumpTo = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenViewJumpToConnectionsConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenViewJumpToSessionsScreenshots = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenViewJumpToErrorsInfos = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenViewResetLayout = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenViewSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.quickTextToolbarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenViewQuickConnectToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenViewExtAppsToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenViewSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.mMenViewFullscreen = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenToolsSSHTransfer = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenToolsUVNCSC = new System.Windows.Forms.ToolStripMenuItem();
            this.mQuickText = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenToolsExternalApps = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenToolsPortScan = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenToolsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mMenToolsComponentsCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenToolsOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenInfoHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenInfoSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mMenInfoWebsite = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenInfoDonate = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenInfoForum = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenInfoBugReport = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mMenInfoAnnouncements = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenToolsUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenInfoSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mMenInfoAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsExternalTools = new System.Windows.Forms.ToolStrip();
            this.tsQuickConnect = new System.Windows.Forms.ToolStrip();
            this.lblQuickConnect = new System.Windows.Forms.ToolStripLabel();
            this.cmbQuickConnect = new System.Windows.Forms.ToolStripComboBox();
            this.btnQuickyPlay = new System.Windows.Forms.ToolStripSplitButton();
            this.mMenQuickyCon = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrShowUpdate = new System.Windows.Forms.Timer(this.components);
            this.tmrAutoSave = new System.Windows.Forms.Timer(this.components);
            this.tsContainer.ContentPanel.SuspendLayout();
            this.tsContainer.TopToolStripPanel.SuspendLayout();
            this.tsContainer.SuspendLayout();
            this.ToolStrip1.SuspendLayout();
            this.cmsPanelMenu.SuspendLayout();
            this.cMenExtAppsToolbar.SuspendLayout();
            this.msMain.SuspendLayout();
            this.tsQuickConnect.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDock
            // 
            this.pnlDock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDock.DockBackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pnlDock.DockLeftPortion = 230D;
            this.pnlDock.DockRightPortion = 230D;
            this.pnlDock.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.pnlDock.Location = new System.Drawing.Point(0, 0);
            this.pnlDock.Name = "pnlDock";
            this.pnlDock.Size = new System.Drawing.Size(1258, 617);
            dockPanelGradient1.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient1.StartColor = System.Drawing.SystemColors.ControlLight;
            autoHideStripSkin1.DockStripGradient = dockPanelGradient1;
            tabGradient1.EndColor = System.Drawing.SystemColors.Control;
            tabGradient1.StartColor = System.Drawing.SystemColors.Control;
            tabGradient1.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            autoHideStripSkin1.TabGradient = tabGradient1;
            autoHideStripSkin1.TextFont = new System.Drawing.Font("Segoe UI", 9F);
            dockPanelSkin1.AutoHideStripSkin = autoHideStripSkin1;
            tabGradient2.EndColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.StartColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.ActiveTabGradient = tabGradient2;
            dockPanelGradient2.EndColor = System.Drawing.SystemColors.Control;
            dockPanelGradient2.StartColor = System.Drawing.SystemColors.Control;
            dockPaneStripGradient1.DockStripGradient = dockPanelGradient2;
            tabGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.InactiveTabGradient = tabGradient3;
            dockPaneStripSkin1.DocumentGradient = dockPaneStripGradient1;
            dockPaneStripSkin1.TextFont = new System.Drawing.Font("Segoe UI", 9F);
            tabGradient4.EndColor = System.Drawing.SystemColors.ActiveCaption;
            tabGradient4.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient4.StartColor = System.Drawing.SystemColors.GradientActiveCaption;
            tabGradient4.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
            dockPaneStripToolWindowGradient1.ActiveCaptionGradient = tabGradient4;
            tabGradient5.EndColor = System.Drawing.SystemColors.Control;
            tabGradient5.StartColor = System.Drawing.SystemColors.Control;
            tabGradient5.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient1.ActiveTabGradient = tabGradient5;
            dockPanelGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            dockPaneStripToolWindowGradient1.DockStripGradient = dockPanelGradient3;
            tabGradient6.EndColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient6.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient6.StartColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient6.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient1.InactiveCaptionGradient = tabGradient6;
            tabGradient7.EndColor = System.Drawing.Color.Transparent;
            tabGradient7.StartColor = System.Drawing.Color.Transparent;
            tabGradient7.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            dockPaneStripToolWindowGradient1.InactiveTabGradient = tabGradient7;
            dockPaneStripSkin1.ToolWindowGradient = dockPaneStripToolWindowGradient1;
            dockPanelSkin1.DockPaneStripSkin = dockPaneStripSkin1;
            this.pnlDock.Skin = dockPanelSkin1;
            this.pnlDock.SupportDeeplyNestedContent = true;
            this.pnlDock.TabIndex = 13;
            this.pnlDock.ActiveDocumentChanged += new System.EventHandler(this.pnlDock_ActiveDocumentChanged);
            // 
            // mMenSep3
            // 
            this.mMenSep3.Name = "mMenSep3";
            this.mMenSep3.Size = new System.Drawing.Size(211, 6);
            // 
            // tsContainer
            // 
            // 
            // tsContainer.BottomToolStripPanel
            // 
            this.tsContainer.BottomToolStripPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            // 
            // tsContainer.ContentPanel
            // 
            this.tsContainer.ContentPanel.Controls.Add(this.pnlDock);
            this.tsContainer.ContentPanel.Controls.Add(this.ToolStrip1);
            this.tsContainer.ContentPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.tsContainer.ContentPanel.Size = new System.Drawing.Size(1258, 617);
            this.tsContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // tsContainer.LeftToolStripPanel
            // 
            this.tsContainer.LeftToolStripPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.tsContainer.Location = new System.Drawing.Point(0, 0);
            this.tsContainer.Name = "tsContainer";
            // 
            // tsContainer.RightToolStripPanel
            // 
            this.tsContainer.RightToolStripPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.tsContainer.Size = new System.Drawing.Size(1258, 691);
            this.tsContainer.TabIndex = 17;
            this.tsContainer.Text = "ToolStripContainer1";
            // 
            // tsContainer.TopToolStripPanel
            // 
            this.tsContainer.TopToolStripPanel.BackColor = System.Drawing.SystemColors.Control;
            this.tsContainer.TopToolStripPanel.ContextMenuStrip = this.cmsPanelMenu;
            this.tsContainer.TopToolStripPanel.Controls.Add(this.tsQuickTexts);
            this.tsContainer.TopToolStripPanel.Controls.Add(this.msMain);
            this.tsContainer.TopToolStripPanel.Controls.Add(this.tsExternalTools);
            this.tsContainer.TopToolStripPanel.Controls.Add(this.tsQuickConnect);
            this.tsContainer.TopToolStripPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsContainer.TopToolStripPanel.Click += new System.EventHandler(this.tsContainer_TopToolStripPanel_Click);
            // 
            // ToolStrip1
            // 
            this.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripButton1,
            this.ToolStripButton2,
            this.ToolStripButton3,
            this.ToolStripSplitButton1});
            this.ToolStrip1.Location = new System.Drawing.Point(701, 641);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Size = new System.Drawing.Size(264, 25);
            this.ToolStrip1.TabIndex = 19;
            this.ToolStrip1.Visible = false;
            // 
            // ToolStripButton1
            // 
            this.ToolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButton1.Image")));
            this.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButton1.Name = "ToolStripButton1";
            this.ToolStripButton1.Size = new System.Drawing.Size(72, 22);
            this.ToolStripButton1.Text = "Connect";
            // 
            // ToolStripButton2
            // 
            this.ToolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButton2.Image")));
            this.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButton2.Name = "ToolStripButton2";
            this.ToolStripButton2.Size = new System.Drawing.Size(85, 22);
            this.ToolStripButton2.Text = "Screenshot";
            // 
            // ToolStripButton3
            // 
            this.ToolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButton3.Image")));
            this.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButton3.Name = "ToolStripButton3";
            this.ToolStripButton3.Size = new System.Drawing.Size(66, 22);
            this.ToolStripButton3.Text = "Refresh";
            // 
            // ToolStripSplitButton1
            // 
            this.ToolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem1,
            this.ToolStripMenuItem2});
            this.ToolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripSplitButton1.Image")));
            this.ToolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripSplitButton1.Name = "ToolStripSplitButton1";
            this.ToolStripSplitButton1.Size = new System.Drawing.Size(29, 22);
            this.ToolStripSplitButton1.Text = "Special Keys";
            // 
            // ToolStripMenuItem1
            // 
            this.ToolStripMenuItem1.Name = "ToolStripMenuItem1";
            this.ToolStripMenuItem1.Size = new System.Drawing.Size(135, 22);
            this.ToolStripMenuItem1.Text = "Ctrl-Alt-Del";
            // 
            // ToolStripMenuItem2
            // 
            this.ToolStripMenuItem2.Name = "ToolStripMenuItem2";
            this.ToolStripMenuItem2.Size = new System.Drawing.Size(135, 22);
            this.ToolStripMenuItem2.Text = "Ctrl-Esc";
            // 
            // cmsPanelMenu
            // 
            this.cmsPanelMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lockToolStripMenuItem});
            this.cmsPanelMenu.Name = "cmsPanelMenu";
            this.cmsPanelMenu.Size = new System.Drawing.Size(141, 26);
            // 
            // lockToolStripMenuItem
            // 
            this.lockToolStripMenuItem.Checked = global::My.Settings.Default.ToolStripsIsLocked;
            this.lockToolStripMenuItem.CheckOnClick = true;
            this.lockToolStripMenuItem.Name = "lockToolStripMenuItem";
            this.lockToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.lockToolStripMenuItem.Text = "Lock toolbar";
            this.lockToolStripMenuItem.Click += new System.EventHandler(this.lockToolStripMenuItem_Click);
            // 
            // tsQuickTexts
            // 
            this.tsQuickTexts.BackColor = System.Drawing.SystemColors.Control;
            this.tsQuickTexts.ContextMenuStrip = this.cMenExtAppsToolbar;
            this.tsQuickTexts.Dock = System.Windows.Forms.DockStyle.None;
            this.tsQuickTexts.Location = new System.Drawing.Point(39, 0);
            this.tsQuickTexts.Name = "tsQuickTexts";
            this.tsQuickTexts.Size = new System.Drawing.Size(111, 25);
            this.tsQuickTexts.TabIndex = 20;
            this.tsQuickTexts.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tsQuickTexts_ItemClicked);
            // 
            // cMenExtAppsToolbar
            // 
            this.cMenExtAppsToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cMenToolbarShowText});
            this.cMenExtAppsToolbar.Name = "cMenToolbar";
            this.cMenExtAppsToolbar.Size = new System.Drawing.Size(129, 26);
            // 
            // cMenToolbarShowText
            // 
            this.cMenToolbarShowText.Checked = true;
            this.cMenToolbarShowText.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cMenToolbarShowText.Name = "cMenToolbarShowText";
            this.cMenToolbarShowText.Size = new System.Drawing.Size(128, 22);
            this.cMenToolbarShowText.Text = "Show Text";
            this.cMenToolbarShowText.Click += new System.EventHandler(this.cMenToolbarShowText_Click);
            // 
            // msMain
            // 
            this.msMain.BackColor = System.Drawing.SystemColors.Control;
            this.msMain.Dock = System.Windows.Forms.DockStyle.None;
            this.msMain.GripMargin = new System.Windows.Forms.Padding(0);
            this.msMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mMenFile,
            this.mMenView,
            this.mMenTools,
            this.mMenInfo});
            this.msMain.Location = new System.Drawing.Point(3, 25);
            this.msMain.Name = "msMain";
            this.msMain.Padding = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.msMain.Size = new System.Drawing.Size(182, 24);
            this.msMain.Stretch = false;
            this.msMain.TabIndex = 16;
            this.msMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.msMain_ItemClicked);
            // 
            // mMenFile
            // 
            this.mMenFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mMenFileNewConnection,
            this.mMenFileNewFolder,
            this.mMenFileSep1,
            this.mMenFileNew,
            this.mMenFileLoad,
            this.mMenFileSave,
            this.mMenFileSaveAs,
            this.mMenFileSep2,
            this.mMenFileImportExport,
            this.mMenFileSep3,
            this.mMenFileDelete,
            this.mMenFileRename,
            this.mMenFileDuplicate,
            this.mMenFileSep4,
            this.mMenFileExit});
            this.mMenFile.Name = "mMenFile";
            this.mMenFile.Size = new System.Drawing.Size(37, 20);
            this.mMenFile.Text = "&File";
            this.mMenFile.DropDownOpening += new System.EventHandler(this.mMenFile_DropDownOpening);
            // 
            // mMenFileNewConnection
            // 
            this.mMenFileNewConnection.Image = ((System.Drawing.Image)(resources.GetObject("mMenFileNewConnection.Image")));
            this.mMenFileNewConnection.Name = "mMenFileNewConnection";
            this.mMenFileNewConnection.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F4)));
            this.mMenFileNewConnection.Size = new System.Drawing.Size(281, 22);
            this.mMenFileNewConnection.Text = "New Connection";
            this.mMenFileNewConnection.Click += new System.EventHandler(this.mMenFileNewConnection_Click);
            // 
            // mMenFileNewFolder
            // 
            this.mMenFileNewFolder.Image = ((System.Drawing.Image)(resources.GetObject("mMenFileNewFolder.Image")));
            this.mMenFileNewFolder.Name = "mMenFileNewFolder";
            this.mMenFileNewFolder.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.mMenFileNewFolder.Size = new System.Drawing.Size(281, 22);
            this.mMenFileNewFolder.Text = "New Folder";
            this.mMenFileNewFolder.Click += new System.EventHandler(this.mMenFileNewFolder_Click);
            // 
            // mMenFileSep1
            // 
            this.mMenFileSep1.Name = "mMenFileSep1";
            this.mMenFileSep1.Size = new System.Drawing.Size(278, 6);
            // 
            // mMenFileNew
            // 
            this.mMenFileNew.Image = ((System.Drawing.Image)(resources.GetObject("mMenFileNew.Image")));
            this.mMenFileNew.Name = "mMenFileNew";
            this.mMenFileNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mMenFileNew.Size = new System.Drawing.Size(281, 22);
            this.mMenFileNew.Text = "New Connection File";
            this.mMenFileNew.Click += new System.EventHandler(this.mMenFileNew_Click);
            // 
            // mMenFileLoad
            // 
            this.mMenFileLoad.Image = ((System.Drawing.Image)(resources.GetObject("mMenFileLoad.Image")));
            this.mMenFileLoad.Name = "mMenFileLoad";
            this.mMenFileLoad.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mMenFileLoad.Size = new System.Drawing.Size(281, 22);
            this.mMenFileLoad.Text = "Open Connection File...";
            this.mMenFileLoad.Click += new System.EventHandler(this.mMenFileLoad_Click);
            // 
            // mMenFileSave
            // 
            this.mMenFileSave.Image = ((System.Drawing.Image)(resources.GetObject("mMenFileSave.Image")));
            this.mMenFileSave.Name = "mMenFileSave";
            this.mMenFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mMenFileSave.Size = new System.Drawing.Size(281, 22);
            this.mMenFileSave.Text = "Save Connection File";
            this.mMenFileSave.Click += new System.EventHandler(this.mMenFileSave_Click);
            // 
            // mMenFileSaveAs
            // 
            this.mMenFileSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("mMenFileSaveAs.Image")));
            this.mMenFileSaveAs.Name = "mMenFileSaveAs";
            this.mMenFileSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.mMenFileSaveAs.Size = new System.Drawing.Size(281, 22);
            this.mMenFileSaveAs.Text = "Save Connection File As...";
            this.mMenFileSaveAs.Click += new System.EventHandler(this.mMenFileSaveAs_Click);
            // 
            // mMenFileSep2
            // 
            this.mMenFileSep2.Name = "mMenFileSep2";
            this.mMenFileSep2.Size = new System.Drawing.Size(278, 6);
            // 
            // mMenFileImportExport
            // 
            this.mMenFileImportExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImportFromXMLFileToolStripMenuItem,
            this.ImportFromRDPFileToolStripMenuItem,
            this.ImportFromActiveDirectoryToolStripMenuItem,
            this.ImportFromPortScanToolStripMenuItem,
            this.ToolStripSeparator6,
            this.ExportToXMLFileToolStripMenuItem});
            this.mMenFileImportExport.Name = "mMenFileImportExport";
            this.mMenFileImportExport.Size = new System.Drawing.Size(281, 22);
            this.mMenFileImportExport.Text = "Import/Export Folder";
            this.mMenFileImportExport.Visible = false;
            // 
            // ImportFromXMLFileToolStripMenuItem
            // 
            this.ImportFromXMLFileToolStripMenuItem.Name = "ImportFromXMLFileToolStripMenuItem";
            this.ImportFromXMLFileToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.ImportFromXMLFileToolStripMenuItem.Text = "Import from XML File...";
            // 
            // ImportFromRDPFileToolStripMenuItem
            // 
            this.ImportFromRDPFileToolStripMenuItem.Name = "ImportFromRDPFileToolStripMenuItem";
            this.ImportFromRDPFileToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.ImportFromRDPFileToolStripMenuItem.Text = "Import from RDP File...";
            // 
            // ImportFromActiveDirectoryToolStripMenuItem
            // 
            this.ImportFromActiveDirectoryToolStripMenuItem.Name = "ImportFromActiveDirectoryToolStripMenuItem";
            this.ImportFromActiveDirectoryToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.ImportFromActiveDirectoryToolStripMenuItem.Text = "Import from Active Directory...";
            // 
            // ImportFromPortScanToolStripMenuItem
            // 
            this.ImportFromPortScanToolStripMenuItem.Name = "ImportFromPortScanToolStripMenuItem";
            this.ImportFromPortScanToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.ImportFromPortScanToolStripMenuItem.Text = "Import from Port Scan...";
            // 
            // ToolStripSeparator6
            // 
            this.ToolStripSeparator6.Name = "ToolStripSeparator6";
            this.ToolStripSeparator6.Size = new System.Drawing.Size(232, 6);
            // 
            // ExportToXMLFileToolStripMenuItem
            // 
            this.ExportToXMLFileToolStripMenuItem.Name = "ExportToXMLFileToolStripMenuItem";
            this.ExportToXMLFileToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.ExportToXMLFileToolStripMenuItem.Text = "Export to XML File...";
            // 
            // mMenFileSep3
            // 
            this.mMenFileSep3.Name = "mMenFileSep3";
            this.mMenFileSep3.Size = new System.Drawing.Size(278, 6);
            this.mMenFileSep3.Visible = false;
            // 
            // mMenFileDelete
            // 
            this.mMenFileDelete.Image = ((System.Drawing.Image)(resources.GetObject("mMenFileDelete.Image")));
            this.mMenFileDelete.Name = "mMenFileDelete";
            this.mMenFileDelete.Size = new System.Drawing.Size(281, 22);
            this.mMenFileDelete.Text = "Delete...";
            this.mMenFileDelete.Click += new System.EventHandler(this.mMenFileDelete_Click);
            // 
            // mMenFileRename
            // 
            this.mMenFileRename.Image = ((System.Drawing.Image)(resources.GetObject("mMenFileRename.Image")));
            this.mMenFileRename.Name = "mMenFileRename";
            this.mMenFileRename.Size = new System.Drawing.Size(281, 22);
            this.mMenFileRename.Text = "Rename";
            this.mMenFileRename.Click += new System.EventHandler(this.mMenFileRename_Click);
            // 
            // mMenFileDuplicate
            // 
            this.mMenFileDuplicate.Image = ((System.Drawing.Image)(resources.GetObject("mMenFileDuplicate.Image")));
            this.mMenFileDuplicate.Name = "mMenFileDuplicate";
            this.mMenFileDuplicate.Size = new System.Drawing.Size(281, 22);
            this.mMenFileDuplicate.Text = "Duplicate";
            this.mMenFileDuplicate.Click += new System.EventHandler(this.mMenFileDuplicate_Click);
            // 
            // mMenFileSep4
            // 
            this.mMenFileSep4.Name = "mMenFileSep4";
            this.mMenFileSep4.Size = new System.Drawing.Size(278, 6);
            // 
            // mMenFileExit
            // 
            this.mMenFileExit.Image = ((System.Drawing.Image)(resources.GetObject("mMenFileExit.Image")));
            this.mMenFileExit.Name = "mMenFileExit";
            this.mMenFileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.mMenFileExit.Size = new System.Drawing.Size(281, 22);
            this.mMenFileExit.Text = "Exit";
            this.mMenFileExit.Click += new System.EventHandler(this.mMenFileExit_Click);
            // 
            // mMenView
            // 
            this.mMenView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mMenViewAddConnectionPanel,
            this.mMenViewConnectionPanels,
            this.mMenViewSep1,
            this.mMenViewConnections,
            this.mMenViewConfig,
            this.mMenViewSessions,
            this.mMenViewErrorsAndInfos,
            this.mConStatus,
            this.mMenViewScreenshotManager,
            this.ToolStripSeparator1,
            this.mMenViewJumpTo,
            this.mMenViewResetLayout,
            this.mMenViewSep2,
            this.quickTextToolbarToolStripMenuItem,
            this.mMenViewQuickConnectToolbar,
            this.mMenViewExtAppsToolbar,
            this.mMenViewSep3,
            this.mMenViewFullscreen});
            this.mMenView.Name = "mMenView";
            this.mMenView.Size = new System.Drawing.Size(44, 20);
            this.mMenView.Text = "&View";
            this.mMenView.DropDownOpening += new System.EventHandler(this.mMenView_DropDownOpening);
            this.mMenView.Click += new System.EventHandler(this.mMenView_Click);
            // 
            // mMenViewAddConnectionPanel
            // 
            this.mMenViewAddConnectionPanel.Image = ((System.Drawing.Image)(resources.GetObject("mMenViewAddConnectionPanel.Image")));
            this.mMenViewAddConnectionPanel.Name = "mMenViewAddConnectionPanel";
            this.mMenViewAddConnectionPanel.Size = new System.Drawing.Size(228, 22);
            this.mMenViewAddConnectionPanel.Text = "Add Connection Panel";
            this.mMenViewAddConnectionPanel.Click += new System.EventHandler(this.mMenViewAddConnectionPanel_Click);
            // 
            // mMenViewConnectionPanels
            // 
            this.mMenViewConnectionPanels.Image = ((System.Drawing.Image)(resources.GetObject("mMenViewConnectionPanels.Image")));
            this.mMenViewConnectionPanels.Name = "mMenViewConnectionPanels";
            this.mMenViewConnectionPanels.Size = new System.Drawing.Size(228, 22);
            this.mMenViewConnectionPanels.Text = "Connection Panels";
            // 
            // mMenViewSep1
            // 
            this.mMenViewSep1.Name = "mMenViewSep1";
            this.mMenViewSep1.Size = new System.Drawing.Size(225, 6);
            // 
            // mMenViewConnections
            // 
            this.mMenViewConnections.Checked = true;
            this.mMenViewConnections.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mMenViewConnections.Image = ((System.Drawing.Image)(resources.GetObject("mMenViewConnections.Image")));
            this.mMenViewConnections.Name = "mMenViewConnections";
            this.mMenViewConnections.Size = new System.Drawing.Size(228, 22);
            this.mMenViewConnections.Text = "Connections";
            this.mMenViewConnections.Click += new System.EventHandler(this.mMenViewConnections_Click);
            // 
            // mMenViewConfig
            // 
            this.mMenViewConfig.Checked = true;
            this.mMenViewConfig.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mMenViewConfig.Image = ((System.Drawing.Image)(resources.GetObject("mMenViewConfig.Image")));
            this.mMenViewConfig.Name = "mMenViewConfig";
            this.mMenViewConfig.Size = new System.Drawing.Size(228, 22);
            this.mMenViewConfig.Text = "Config";
            this.mMenViewConfig.Click += new System.EventHandler(this.mMenViewConfig_Click);
            // 
            // mMenViewSessions
            // 
            this.mMenViewSessions.Checked = true;
            this.mMenViewSessions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mMenViewSessions.Image = ((System.Drawing.Image)(resources.GetObject("mMenViewSessions.Image")));
            this.mMenViewSessions.Name = "mMenViewSessions";
            this.mMenViewSessions.Size = new System.Drawing.Size(228, 22);
            this.mMenViewSessions.Text = "Sessions";
            this.mMenViewSessions.Click += new System.EventHandler(this.mMenViewSessions_Click);
            // 
            // mMenViewErrorsAndInfos
            // 
            this.mMenViewErrorsAndInfos.Checked = true;
            this.mMenViewErrorsAndInfos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mMenViewErrorsAndInfos.Image = ((System.Drawing.Image)(resources.GetObject("mMenViewErrorsAndInfos.Image")));
            this.mMenViewErrorsAndInfos.Name = "mMenViewErrorsAndInfos";
            this.mMenViewErrorsAndInfos.Size = new System.Drawing.Size(228, 22);
            this.mMenViewErrorsAndInfos.Text = "Errors and Infos";
            this.mMenViewErrorsAndInfos.Click += new System.EventHandler(this.mMenViewErrorsAndInfos_Click);
            // 
            // mConStatus
            // 
            this.mConStatus.Image = ((System.Drawing.Image)(resources.GetObject("mConStatus.Image")));
            this.mConStatus.Name = "mConStatus";
            this.mConStatus.Size = new System.Drawing.Size(228, 22);
            this.mConStatus.Text = "Connections status";
            this.mConStatus.Click += new System.EventHandler(this.statusToolStripMenuItem_Click_1);
            // 
            // mMenViewScreenshotManager
            // 
            this.mMenViewScreenshotManager.Image = ((System.Drawing.Image)(resources.GetObject("mMenViewScreenshotManager.Image")));
            this.mMenViewScreenshotManager.Name = "mMenViewScreenshotManager";
            this.mMenViewScreenshotManager.Size = new System.Drawing.Size(228, 22);
            this.mMenViewScreenshotManager.Text = "Screenshot Manager";
            this.mMenViewScreenshotManager.Click += new System.EventHandler(this.mMenViewScreenshotManager_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(225, 6);
            // 
            // mMenViewJumpTo
            // 
            this.mMenViewJumpTo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mMenViewJumpToConnectionsConfig,
            this.mMenViewJumpToSessionsScreenshots,
            this.mMenViewJumpToErrorsInfos});
            this.mMenViewJumpTo.Image = ((System.Drawing.Image)(resources.GetObject("mMenViewJumpTo.Image")));
            this.mMenViewJumpTo.Name = "mMenViewJumpTo";
            this.mMenViewJumpTo.Size = new System.Drawing.Size(228, 22);
            this.mMenViewJumpTo.Text = "Jump To";
            // 
            // mMenViewJumpToConnectionsConfig
            // 
            this.mMenViewJumpToConnectionsConfig.Name = "mMenViewJumpToConnectionsConfig";
            this.mMenViewJumpToConnectionsConfig.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.C)));
            this.mMenViewJumpToConnectionsConfig.Size = new System.Drawing.Size(260, 22);
            this.mMenViewJumpToConnectionsConfig.Text = "Connections && Config";
            this.mMenViewJumpToConnectionsConfig.Click += new System.EventHandler(this.mMenViewJumpToConnectionsConfig_Click);
            // 
            // mMenViewJumpToSessionsScreenshots
            // 
            this.mMenViewJumpToSessionsScreenshots.Name = "mMenViewJumpToSessionsScreenshots";
            this.mMenViewJumpToSessionsScreenshots.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
            this.mMenViewJumpToSessionsScreenshots.Size = new System.Drawing.Size(260, 22);
            this.mMenViewJumpToSessionsScreenshots.Text = "Sessions && Screenshots";
            this.mMenViewJumpToSessionsScreenshots.Click += new System.EventHandler(this.mMenViewJumpToSessionsScreenshots_Click);
            // 
            // mMenViewJumpToErrorsInfos
            // 
            this.mMenViewJumpToErrorsInfos.Name = "mMenViewJumpToErrorsInfos";
            this.mMenViewJumpToErrorsInfos.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.E)));
            this.mMenViewJumpToErrorsInfos.Size = new System.Drawing.Size(260, 22);
            this.mMenViewJumpToErrorsInfos.Text = "Errors && Infos";
            this.mMenViewJumpToErrorsInfos.Click += new System.EventHandler(this.mMenViewJumpToErrorsInfos_Click);
            // 
            // mMenViewResetLayout
            // 
            this.mMenViewResetLayout.Name = "mMenViewResetLayout";
            this.mMenViewResetLayout.Size = new System.Drawing.Size(228, 22);
            this.mMenViewResetLayout.Text = "Reset Layout";
            this.mMenViewResetLayout.Click += new System.EventHandler(this.mMenViewResetLayout_Click);
            // 
            // mMenViewSep2
            // 
            this.mMenViewSep2.Name = "mMenViewSep2";
            this.mMenViewSep2.Size = new System.Drawing.Size(225, 6);
            // 
            // quickTextToolbarToolStripMenuItem
            // 
            this.quickTextToolbarToolStripMenuItem.Checked = true;
            this.quickTextToolbarToolStripMenuItem.CheckOnClick = true;
            this.quickTextToolbarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.quickTextToolbarToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("quickTextToolbarToolStripMenuItem.Image")));
            this.quickTextToolbarToolStripMenuItem.Name = "quickTextToolbarToolStripMenuItem";
            this.quickTextToolbarToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.quickTextToolbarToolStripMenuItem.Text = "Quick Text Toolbar";
            this.quickTextToolbarToolStripMenuItem.Click += new System.EventHandler(this.quickTextToolbarToolStripMenuItem_Click);
            // 
            // mMenViewQuickConnectToolbar
            // 
            this.mMenViewQuickConnectToolbar.Image = ((System.Drawing.Image)(resources.GetObject("mMenViewQuickConnectToolbar.Image")));
            this.mMenViewQuickConnectToolbar.Name = "mMenViewQuickConnectToolbar";
            this.mMenViewQuickConnectToolbar.Size = new System.Drawing.Size(228, 22);
            this.mMenViewQuickConnectToolbar.Text = "Quick Connect Toolbar";
            this.mMenViewQuickConnectToolbar.Click += new System.EventHandler(this.mMenViewQuickConnectToolbar_Click);
            // 
            // mMenViewExtAppsToolbar
            // 
            this.mMenViewExtAppsToolbar.Image = ((System.Drawing.Image)(resources.GetObject("mMenViewExtAppsToolbar.Image")));
            this.mMenViewExtAppsToolbar.Name = "mMenViewExtAppsToolbar";
            this.mMenViewExtAppsToolbar.Size = new System.Drawing.Size(228, 22);
            this.mMenViewExtAppsToolbar.Text = "External Applications Toolbar";
            this.mMenViewExtAppsToolbar.Click += new System.EventHandler(this.mMenViewExtAppsToolbar_Click);
            // 
            // mMenViewSep3
            // 
            this.mMenViewSep3.Name = "mMenViewSep3";
            this.mMenViewSep3.Size = new System.Drawing.Size(225, 6);
            // 
            // mMenViewFullscreen
            // 
            this.mMenViewFullscreen.Image = ((System.Drawing.Image)(resources.GetObject("mMenViewFullscreen.Image")));
            this.mMenViewFullscreen.Name = "mMenViewFullscreen";
            this.mMenViewFullscreen.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.mMenViewFullscreen.Size = new System.Drawing.Size(228, 22);
            this.mMenViewFullscreen.Text = "Full Screen";
            this.mMenViewFullscreen.Click += new System.EventHandler(this.mMenViewFullscreen_Click);
            // 
            // mMenTools
            // 
            this.mMenTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mMenToolsSSHTransfer,
            this.mMenToolsUVNCSC,
            this.mQuickText,
            this.mMenToolsExternalApps,
            this.mMenToolsPortScan,
            this.mMenToolsSep1,
            this.mMenToolsComponentsCheck,
            this.mMenToolsOptions});
            this.mMenTools.Name = "mMenTools";
            this.mMenTools.Size = new System.Drawing.Size(48, 20);
            this.mMenTools.Text = "&Tools";
            // 
            // mMenToolsSSHTransfer
            // 
            this.mMenToolsSSHTransfer.Image = ((System.Drawing.Image)(resources.GetObject("mMenToolsSSHTransfer.Image")));
            this.mMenToolsSSHTransfer.Name = "mMenToolsSSHTransfer";
            this.mMenToolsSSHTransfer.Size = new System.Drawing.Size(184, 22);
            this.mMenToolsSSHTransfer.Text = "SSH File Transfer";
            this.mMenToolsSSHTransfer.Click += new System.EventHandler(this.mMenToolsSSHTransfer_Click);
            // 
            // mMenToolsUVNCSC
            // 
            this.mMenToolsUVNCSC.Image = ((System.Drawing.Image)(resources.GetObject("mMenToolsUVNCSC.Image")));
            this.mMenToolsUVNCSC.Name = "mMenToolsUVNCSC";
            this.mMenToolsUVNCSC.Size = new System.Drawing.Size(184, 22);
            this.mMenToolsUVNCSC.Text = "UltraVNC SingleClick";
            this.mMenToolsUVNCSC.Visible = false;
            this.mMenToolsUVNCSC.Click += new System.EventHandler(this.mMenToolsUVNCSC_Click);
            // 
            // mQuickText
            // 
            this.mQuickText.Image = ((System.Drawing.Image)(resources.GetObject("mQuickText.Image")));
            this.mQuickText.Name = "mQuickText";
            this.mQuickText.Size = new System.Drawing.Size(184, 22);
            this.mQuickText.Text = "Quick Text";
            this.mQuickText.Click += new System.EventHandler(this.mQuickText_Click);
            // 
            // mMenToolsExternalApps
            // 
            this.mMenToolsExternalApps.Image = ((System.Drawing.Image)(resources.GetObject("mMenToolsExternalApps.Image")));
            this.mMenToolsExternalApps.Name = "mMenToolsExternalApps";
            this.mMenToolsExternalApps.Size = new System.Drawing.Size(184, 22);
            this.mMenToolsExternalApps.Text = "External Applications";
            this.mMenToolsExternalApps.Click += new System.EventHandler(this.mMenToolsExternalApps_Click);
            // 
            // mMenToolsPortScan
            // 
            this.mMenToolsPortScan.Image = ((System.Drawing.Image)(resources.GetObject("mMenToolsPortScan.Image")));
            this.mMenToolsPortScan.Name = "mMenToolsPortScan";
            this.mMenToolsPortScan.Size = new System.Drawing.Size(184, 22);
            this.mMenToolsPortScan.Text = "Port Scan";
            this.mMenToolsPortScan.Click += new System.EventHandler(this.mMenToolsPortScan_Click);
            // 
            // mMenToolsSep1
            // 
            this.mMenToolsSep1.Name = "mMenToolsSep1";
            this.mMenToolsSep1.Size = new System.Drawing.Size(181, 6);
            // 
            // mMenToolsComponentsCheck
            // 
            this.mMenToolsComponentsCheck.Image = ((System.Drawing.Image)(resources.GetObject("mMenToolsComponentsCheck.Image")));
            this.mMenToolsComponentsCheck.Name = "mMenToolsComponentsCheck";
            this.mMenToolsComponentsCheck.Size = new System.Drawing.Size(184, 22);
            this.mMenToolsComponentsCheck.Text = "Components Check";
            this.mMenToolsComponentsCheck.Click += new System.EventHandler(this.mMenToolsComponentsCheck_Click);
            // 
            // mMenToolsOptions
            // 
            this.mMenToolsOptions.Image = ((System.Drawing.Image)(resources.GetObject("mMenToolsOptions.Image")));
            this.mMenToolsOptions.Name = "mMenToolsOptions";
            this.mMenToolsOptions.Size = new System.Drawing.Size(184, 22);
            this.mMenToolsOptions.Text = "Options";
            this.mMenToolsOptions.Click += new System.EventHandler(this.mMenToolsOptions_Click);
            // 
            // mMenInfo
            // 
            this.mMenInfo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mMenInfoHelp,
            this.mMenInfoSep1,
            this.mMenInfoWebsite,
            this.mMenInfoDonate,
            this.mMenInfoForum,
            this.mMenInfoBugReport,
            this.ToolStripSeparator2,
            this.mMenInfoAnnouncements,
            this.mMenToolsUpdate,
            this.mMenInfoSep2,
            this.mMenInfoAbout});
            this.mMenInfo.Name = "mMenInfo";
            this.mMenInfo.Size = new System.Drawing.Size(44, 20);
            this.mMenInfo.Text = "&Help";
            this.mMenInfo.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // mMenInfoHelp
            // 
            this.mMenInfoHelp.Image = ((System.Drawing.Image)(resources.GetObject("mMenInfoHelp.Image")));
            this.mMenInfoHelp.Name = "mMenInfoHelp";
            this.mMenInfoHelp.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.mMenInfoHelp.Size = new System.Drawing.Size(179, 22);
            this.mMenInfoHelp.Text = "mRemote Help";
            this.mMenInfoHelp.Click += new System.EventHandler(this.mMenInfoHelp_Click);
            // 
            // mMenInfoSep1
            // 
            this.mMenInfoSep1.Name = "mMenInfoSep1";
            this.mMenInfoSep1.Size = new System.Drawing.Size(176, 6);
            // 
            // mMenInfoWebsite
            // 
            this.mMenInfoWebsite.Image = ((System.Drawing.Image)(resources.GetObject("mMenInfoWebsite.Image")));
            this.mMenInfoWebsite.Name = "mMenInfoWebsite";
            this.mMenInfoWebsite.Size = new System.Drawing.Size(179, 22);
            this.mMenInfoWebsite.Text = "Website";
            this.mMenInfoWebsite.Click += new System.EventHandler(this.mMenInfoWebsite_Click);
            // 
            // mMenInfoDonate
            // 
            this.mMenInfoDonate.Image = ((System.Drawing.Image)(resources.GetObject("mMenInfoDonate.Image")));
            this.mMenInfoDonate.Name = "mMenInfoDonate";
            this.mMenInfoDonate.Size = new System.Drawing.Size(179, 22);
            this.mMenInfoDonate.Text = "Donate";
            this.mMenInfoDonate.Visible = false;
            this.mMenInfoDonate.Click += new System.EventHandler(this.mMenInfoDonate_Click);
            // 
            // mMenInfoForum
            // 
            this.mMenInfoForum.Name = "mMenInfoForum";
            this.mMenInfoForum.Size = new System.Drawing.Size(179, 22);
            this.mMenInfoForum.Text = "Support Forum";
            this.mMenInfoForum.Visible = false;
            this.mMenInfoForum.Click += new System.EventHandler(this.mMenInfoForum_Click);
            // 
            // mMenInfoBugReport
            // 
            this.mMenInfoBugReport.Image = ((System.Drawing.Image)(resources.GetObject("mMenInfoBugReport.Image")));
            this.mMenInfoBugReport.Name = "mMenInfoBugReport";
            this.mMenInfoBugReport.Size = new System.Drawing.Size(179, 22);
            this.mMenInfoBugReport.Text = "Report a Bug";
            this.mMenInfoBugReport.Click += new System.EventHandler(this.mMenInfoBugReport_Click);
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(176, 6);
            // 
            // mMenInfoAnnouncements
            // 
            this.mMenInfoAnnouncements.Image = ((System.Drawing.Image)(resources.GetObject("mMenInfoAnnouncements.Image")));
            this.mMenInfoAnnouncements.Name = "mMenInfoAnnouncements";
            this.mMenInfoAnnouncements.Size = new System.Drawing.Size(179, 22);
            this.mMenInfoAnnouncements.Text = "Announcements";
            this.mMenInfoAnnouncements.Visible = false;
            this.mMenInfoAnnouncements.Click += new System.EventHandler(this.mMenInfoAnnouncements_Click);
            // 
            // mMenToolsUpdate
            // 
            this.mMenToolsUpdate.Image = ((System.Drawing.Image)(resources.GetObject("mMenToolsUpdate.Image")));
            this.mMenToolsUpdate.Name = "mMenToolsUpdate";
            this.mMenToolsUpdate.Size = new System.Drawing.Size(179, 22);
            this.mMenToolsUpdate.Text = "Check for Updates";
            this.mMenToolsUpdate.Click += new System.EventHandler(this.mMenToolsUpdate_Click);
            // 
            // mMenInfoSep2
            // 
            this.mMenInfoSep2.Name = "mMenInfoSep2";
            this.mMenInfoSep2.Size = new System.Drawing.Size(176, 6);
            this.mMenInfoSep2.Visible = false;
            // 
            // mMenInfoAbout
            // 
            this.mMenInfoAbout.Image = global::My.Resources.Resources.mRemoteNC;
            this.mMenInfoAbout.Name = "mMenInfoAbout";
            this.mMenInfoAbout.Size = new System.Drawing.Size(179, 22);
            this.mMenInfoAbout.Text = "About mRemoteNC";
            this.mMenInfoAbout.Click += new System.EventHandler(this.mMenInfoAbout_Click);
            // 
            // tsExternalTools
            // 
            this.tsExternalTools.BackColor = System.Drawing.SystemColors.Control;
            this.tsExternalTools.Dock = System.Windows.Forms.DockStyle.None;
            this.tsExternalTools.Location = new System.Drawing.Point(3, 49);
            this.tsExternalTools.Name = "tsExternalTools";
            this.tsExternalTools.Size = new System.Drawing.Size(111, 25);
            this.tsExternalTools.TabIndex = 17;
            this.tsExternalTools.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tsExternalTools_ItemClicked);
            // 
            // tsQuickConnect
            // 
            this.tsQuickConnect.BackColor = System.Drawing.SystemColors.Control;
            this.tsQuickConnect.Dock = System.Windows.Forms.DockStyle.None;
            this.tsQuickConnect.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblQuickConnect,
            this.cmbQuickConnect,
            this.btnQuickyPlay,
            this.mMenQuickyCon});
            this.tsQuickConnect.Location = new System.Drawing.Point(114, 49);
            this.tsQuickConnect.Name = "tsQuickConnect";
            this.tsQuickConnect.Size = new System.Drawing.Size(331, 25);
            this.tsQuickConnect.TabIndex = 18;
            // 
            // lblQuickConnect
            // 
            this.lblQuickConnect.BackColor = System.Drawing.SystemColors.Control;
            this.lblQuickConnect.Name = "lblQuickConnect";
            this.lblQuickConnect.Size = new System.Drawing.Size(55, 22);
            this.lblQuickConnect.Text = "&Connect:";
            this.lblQuickConnect.Click += new System.EventHandler(this.lblQuickConnect_Click);
            // 
            // cmbQuickConnect
            // 
            this.cmbQuickConnect.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbQuickConnect.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbQuickConnect.Name = "cmbQuickConnect";
            this.cmbQuickConnect.Size = new System.Drawing.Size(200, 25);
            this.cmbQuickConnect.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbQuickConnect_KeyDown);
            this.cmbQuickConnect.Click += new System.EventHandler(this.cmbQuickConnect_Click);
            // 
            // btnQuickyPlay
            // 
            this.btnQuickyPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnQuickyPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnQuickyPlay.Image")));
            this.btnQuickyPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnQuickyPlay.Name = "btnQuickyPlay";
            this.btnQuickyPlay.Size = new System.Drawing.Size(32, 22);
            this.btnQuickyPlay.Text = "Connect";
            this.btnQuickyPlay.ButtonClick += new System.EventHandler(this.btnQuickyPlay_ButtonClick);
            this.btnQuickyPlay.DropDownOpening += new System.EventHandler(this.btnQuickyPlay_DropDownOpening);
            // 
            // mMenQuickyCon
            // 
            this.mMenQuickyCon.AutoSize = false;
            this.mMenQuickyCon.AutoToolTip = true;
            this.mMenQuickyCon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mMenQuickyCon.Image = ((System.Drawing.Image)(resources.GetObject("mMenQuickyCon.Image")));
            this.mMenQuickyCon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mMenQuickyCon.Name = "mMenQuickyCon";
            this.mMenQuickyCon.Size = new System.Drawing.Size(30, 22);
            this.mMenQuickyCon.Text = "Connections";
            this.mMenQuickyCon.DropDownOpening += new System.EventHandler(this.mMenQuickyCon_DropDownOpening);
            // 
            // tmrShowUpdate
            // 
            this.tmrShowUpdate.Enabled = true;
            this.tmrShowUpdate.Interval = 5000;
            this.tmrShowUpdate.Tick += new System.EventHandler(this.tmrShowUpdate_Tick);
            // 
            // tmrAutoSave
            // 
            this.tmrAutoSave.Interval = 10000;
            this.tmrAutoSave.Tick += new System.EventHandler(this.tmrAutoSave_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1258, 691);
            this.Controls.Add(this.tsContainer);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::My.Settings.Default, "MainFormLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = global::My.Settings.Default.MainFormLocation;
            this.MainMenuStrip = this.msMain;
            this.Name = "frmMain";
            this.Opacity = 0D;
            this.Text = "mRemoteNC";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tsContainer.ContentPanel.ResumeLayout(false);
            this.tsContainer.ContentPanel.PerformLayout();
            this.tsContainer.TopToolStripPanel.ResumeLayout(false);
            this.tsContainer.TopToolStripPanel.PerformLayout();
            this.tsContainer.ResumeLayout(false);
            this.tsContainer.PerformLayout();
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            this.cmsPanelMenu.ResumeLayout(false);
            this.cMenExtAppsToolbar.ResumeLayout(false);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.tsQuickConnect.ResumeLayout(false);
            this.tsQuickConnect.PerformLayout();
            this.ResumeLayout(false);

		}
		internal WeifenLuo.WinFormsUI.Docking.DockPanel pnlDock;
		internal System.Windows.Forms.MenuStrip msMain;
		internal System.Windows.Forms.ToolStripMenuItem mMenFile;
		internal System.Windows.Forms.ToolStripMenuItem mMenView;
		internal System.Windows.Forms.ToolStripMenuItem mMenTools;
		internal System.Windows.Forms.ToolStripLabel lblQuickConnect;
		internal System.Windows.Forms.ToolStripMenuItem mMenInfo;
		internal System.Windows.Forms.ToolStripMenuItem mMenFileNew;
		internal System.Windows.Forms.ToolStripMenuItem mMenFileLoad;
		internal System.Windows.Forms.ToolStripMenuItem mMenFileSave;
		internal System.Windows.Forms.ToolStripMenuItem mMenFileSaveAs;
		internal System.Windows.Forms.ToolStripSeparator mMenFileSep1;
		internal System.Windows.Forms.ToolStripMenuItem mMenFileExit;
		internal System.Windows.Forms.ToolStripSeparator mMenToolsSep1;
		internal System.Windows.Forms.ToolStripMenuItem mMenToolsOptions;
		internal System.Windows.Forms.ToolStripMenuItem mMenInfoHelp;
		internal System.Windows.Forms.ToolStripMenuItem mMenInfoWebsite;
		internal System.Windows.Forms.ToolStripSeparator mMenInfoSep1;
		internal System.Windows.Forms.ToolStripMenuItem mMenInfoAbout;
		internal System.Windows.Forms.ToolStripMenuItem mMenViewConnectionPanels;
		internal System.Windows.Forms.ToolStripSeparator mMenViewSep1;
		internal System.Windows.Forms.ToolStripMenuItem mMenViewSessions;
		internal System.Windows.Forms.ToolStripMenuItem mMenViewConnections;
		internal System.Windows.Forms.ToolStripMenuItem mMenViewConfig;
		internal System.Windows.Forms.ToolStripMenuItem mMenViewErrorsAndInfos;
		internal System.Windows.Forms.ToolStripMenuItem mMenViewScreenshotManager;
		internal System.Windows.Forms.ToolStripMenuItem mMenViewAddConnectionPanel;
		internal System.Windows.Forms.ToolStripComboBox cmbQuickConnect;
		internal System.Windows.Forms.ToolStripSeparator mMenViewSep2;
		internal System.Windows.Forms.ToolStripMenuItem mMenViewFullscreen;
        internal System.Windows.Forms.ToolStripMenuItem mMenToolsSSHTransfer;
		internal System.Windows.Forms.Timer tmrShowUpdate;
		internal System.Windows.Forms.ToolStripMenuItem mMenToolsExternalApps;
		internal System.Windows.Forms.Timer tmrAutoSave;
		internal System.Windows.Forms.ToolStrip tsExternalTools;
		internal System.Windows.Forms.ToolStripMenuItem mMenViewExtAppsToolbar;
		internal System.Windows.Forms.ContextMenuStrip cMenExtAppsToolbar;
		internal System.Windows.Forms.ToolStripMenuItem cMenToolbarShowText;
		internal System.Windows.Forms.ToolStripMenuItem mMenToolsPortScan;
		internal System.Windows.Forms.ToolStrip tsQuickConnect;
		internal System.Windows.Forms.ToolStripMenuItem mMenViewQuickConnectToolbar;
		internal System.Windows.Forms.ToolStripSeparator mMenSep3;
		internal System.Windows.Forms.ToolStripMenuItem mMenInfoDonate;
		internal System.Windows.Forms.ToolStripSeparator mMenViewSep3;
		internal System.Windows.Forms.ToolStripSplitButton btnQuickyPlay;
		internal System.Windows.Forms.ToolStripMenuItem mMenQuickyCon;
		internal System.Windows.Forms.ToolStripMenuItem mMenViewJumpTo;
		internal System.Windows.Forms.ToolStripMenuItem mMenViewJumpToConnectionsConfig;
		internal System.Windows.Forms.ToolStripMenuItem mMenViewJumpToSessionsScreenshots;
		internal System.Windows.Forms.ToolStripMenuItem mMenViewJumpToErrorsInfos;
		internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
		internal System.Windows.Forms.ToolStripMenuItem mMenToolsUVNCSC;
		internal System.Windows.Forms.ToolStripMenuItem mMenToolsComponentsCheck;
		internal System.Windows.Forms.ToolStripMenuItem mMenInfoAnnouncements;
		internal System.Windows.Forms.ToolStripSeparator mMenInfoSep2;
		internal System.Windows.Forms.ToolStripMenuItem mMenInfoBugReport;
		internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
		internal System.Windows.Forms.ToolStripMenuItem mMenInfoForum;
		internal System.Windows.Forms.ToolStripMenuItem mMenToolsUpdate;
		internal System.Windows.Forms.ToolStripMenuItem mMenViewResetLayout;
		internal System.Windows.Forms.ToolStripMenuItem mMenFileDuplicate;
		internal System.Windows.Forms.ToolStripSeparator mMenFileSep2;
		internal System.Windows.Forms.ToolStripMenuItem mMenFileNewConnection;
		internal System.Windows.Forms.ToolStripMenuItem mMenFileNewFolder;
		internal System.Windows.Forms.ToolStripMenuItem mMenFileImportExport;
		internal System.Windows.Forms.ToolStripMenuItem ImportFromXMLFileToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem ImportFromRDPFileToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem ImportFromActiveDirectoryToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem ImportFromPortScanToolStripMenuItem;
		internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator6;
		internal System.Windows.Forms.ToolStripMenuItem ExportToXMLFileToolStripMenuItem;
		internal System.Windows.Forms.ToolStripSeparator mMenFileSep3;
		internal System.Windows.Forms.ToolStripMenuItem mMenFileDelete;
		internal System.Windows.Forms.ToolStripMenuItem mMenFileRename;
		internal System.Windows.Forms.ToolStripSeparator mMenFileSep4;
		internal System.Windows.Forms.ToolStrip ToolStrip1;
		internal System.Windows.Forms.ToolStripButton ToolStripButton1;
		internal System.Windows.Forms.ToolStripButton ToolStripButton2;
		internal System.Windows.Forms.ToolStripButton ToolStripButton3;
		internal System.Windows.Forms.ToolStripDropDownButton ToolStripSplitButton1;
		internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem1;
		internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem2;
        private System.ComponentModel.IContainer components;
        internal ToolStripMenuItem mQuickText;
        internal ToolStrip tsQuickTexts;
        private ToolStripMenuItem quickTextToolbarToolStripMenuItem;
        public ToolStripContainer tsContainer;
        private ContextMenuStrip cmsPanelMenu;
        private ToolStripMenuItem lockToolStripMenuItem;
        internal ToolStripMenuItem mConStatus;
	}
	
}
