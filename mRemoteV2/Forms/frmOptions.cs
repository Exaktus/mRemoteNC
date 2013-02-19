using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using mRemoteNC.App;
using mRemoteNC.App.Info;
using mRemoteNC.Security;
using My;
using WeifenLuo.WinFormsUI.Docking;
using mRemoteNC.Tools;
using Settings = My.Settings;

namespace mRemoteNC
{
    public class frmOptions : System.Windows.Forms.Form
    {
        #region Form Init

        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.Button btnOK;
        internal System.Windows.Forms.ListView lvPages;
        internal System.Windows.Forms.ImageList imgListPages;
        internal System.Windows.Forms.CheckBox chkWriteLogFile;
        internal System.Windows.Forms.CheckBox chkAutomaticallyGetSessionInfo;
        internal System.Windows.Forms.Label lblXulRunnerPath;
        internal System.Windows.Forms.CheckBox chkEncryptCompleteFile;
        internal System.Windows.Forms.Button btnBrowseXulRunnerPath;
        internal System.Windows.Forms.CheckBox chkUseCustomPuttyPath;
        internal System.Windows.Forms.TextBox txtCustomPuttyPath;
        internal System.Windows.Forms.Button btnBrowseCustomPuttyPath;
        internal System.Windows.Forms.Label lblSeconds;
        internal System.Windows.Forms.Button btnLaunchPutty;
        internal System.Windows.Forms.Label lblConfigurePuttySessions;
        internal System.Windows.Forms.NumericUpDown numPuttyWaitTime;
        internal System.Windows.Forms.CheckBox chkAutomaticReconnect;
        internal System.Windows.Forms.Label lblMaximumPuttyWaitTime;
        internal System.Windows.Forms.Label lblUpdatesExplanation;
        internal System.Windows.Forms.Panel pnlUpdateCheck;
        internal System.Windows.Forms.Button btnUpdateCheckNow;
        internal System.Windows.Forms.CheckBox chkCheckForUpdatesOnStartup;
        internal System.Windows.Forms.ComboBox cboUpdateCheckFrequency;
        internal System.Windows.Forms.Panel pnlProxy;
        internal System.Windows.Forms.Panel pnlProxyBasic;
        internal System.Windows.Forms.Label lblProxyAddress;
        internal System.Windows.Forms.TextBox txtProxyAddress;
        internal System.Windows.Forms.Label lblProxyPort;
        internal System.Windows.Forms.NumericUpDown numProxyPort;
        internal System.Windows.Forms.CheckBox chkUseProxyForAutomaticUpdates;
        internal System.Windows.Forms.CheckBox chkUseProxyAuthentication;
        internal System.Windows.Forms.Panel pnlProxyAuthentication;
        internal System.Windows.Forms.Label lblProxyUsername;
        internal System.Windows.Forms.TextBox txtProxyUsername;
        internal System.Windows.Forms.Label lblProxyPassword;
        internal System.Windows.Forms.TextBox txtProxyPassword;
        internal System.Windows.Forms.Button btnTestProxy;
        internal System.Windows.Forms.Panel pnlRdpReconnectionCount;
        internal System.Windows.Forms.Label lblRdpReconnectionCount;
        internal System.Windows.Forms.NumericUpDown numRdpReconnectionCount;
        internal System.Windows.Forms.CheckBox chkSingleClickOnConnectionOpensIt;
        internal System.Windows.Forms.CheckBox chkSingleClickOnOpenedConnectionSwitchesToIt;
        internal System.Windows.Forms.Panel pnlAutoSave;
        internal System.Windows.Forms.Label lblAutoSave1;
        internal System.Windows.Forms.NumericUpDown numAutoSave;
        internal System.Windows.Forms.Label lblAutoSave2;
        internal System.Windows.Forms.CheckBox chkHostnameLikeDisplayName;
        internal System.Windows.Forms.CheckBox chkUseOnlyErrorsAndInfosPanel;
        internal System.Windows.Forms.Label lblSwitchToErrorsAndInfos;
        internal System.Windows.Forms.CheckBox chkMCInformation;
        internal System.Windows.Forms.CheckBox chkMCErrors;
        internal System.Windows.Forms.CheckBox chkMCWarnings;
        internal System.Windows.Forms.CheckBox chkOpenNewTabRightOfSelected;
        internal System.Windows.Forms.CheckBox chkShowProtocolOnTabs;
        internal System.Windows.Forms.CheckBox chkDoubleClickClosesTab;
        internal System.Windows.Forms.CheckBox chkShowLogonInfoOnTabs;
        internal System.Windows.Forms.CheckBox chkAlwaysShowPanelSelectionDlg;
        internal System.Windows.Forms.Label lblLanguageRestartRequired;
        internal System.Windows.Forms.ComboBox cboLanguage;
        internal System.Windows.Forms.Label lblLanguage;
        internal System.Windows.Forms.CheckBox chkShowDescriptionTooltipsInTree;
        internal System.Windows.Forms.CheckBox chkMinimizeToSystemTray;
        internal System.Windows.Forms.CheckBox chkShowSystemTrayIcon;
        internal System.Windows.Forms.CheckBox chkShowFullConnectionsFilePathInTitle;
        internal System.Windows.Forms.CheckBox chkConfirmCloseConnection;
        internal System.Windows.Forms.CheckBox chkSaveConsOnExit;
        internal System.Windows.Forms.CheckBox chkProperInstallationOfComponentsAtStartup;
        internal System.Windows.Forms.CheckBox chkConfirmExit;
        internal System.Windows.Forms.CheckBox chkSingleInstance;
        internal System.Windows.Forms.CheckBox chkReconnectOnStart;
        internal System.Windows.Forms.TabControl tcTabControl;
        internal System.Windows.Forms.TabPage tabStartupExit;
        internal System.Windows.Forms.TabPage tabAppearance;
        internal System.Windows.Forms.TabPage tabTabsAndPanels;
        internal System.Windows.Forms.TabPage tabConnections;
        internal System.Windows.Forms.TabPage tabUpdates;
        internal System.Windows.Forms.TabPage tabAdvanced;
        internal System.Windows.Forms.TabPage tabSQLServer;
        internal System.Windows.Forms.Panel pnlDefaultCredentials;
        internal System.Windows.Forms.RadioButton radCredentialsCustom;
        internal System.Windows.Forms.Label lblDefaultCredentials;
        internal System.Windows.Forms.RadioButton radCredentialsNoInfo;
        internal System.Windows.Forms.RadioButton radCredentialsWindows;
        internal System.Windows.Forms.TextBox txtCredentialsDomain;
        internal System.Windows.Forms.Label lblCredentialsUsername;
        internal System.Windows.Forms.TextBox txtCredentialsPassword;
        internal System.Windows.Forms.Label lblCredentialsPassword;
        internal System.Windows.Forms.TextBox txtCredentialsUsername;
        internal System.Windows.Forms.Label lblCredentialsDomain;
        internal System.Windows.Forms.CheckBox chkUseSQLServer;
        internal System.Windows.Forms.Label lblSQLInfo;
        internal System.Windows.Forms.Label lblSQLUsername;
        internal System.Windows.Forms.TextBox txtSQLPassword;
        internal System.Windows.Forms.Label lblSQLServer;
        internal System.Windows.Forms.TextBox txtSQLUsername;
        internal System.Windows.Forms.Label lblSQLPassword;
        internal System.Windows.Forms.TextBox txtSQLServer;
        internal System.Windows.Forms.Label lblExperimental;
        internal System.Windows.Forms.Label lblSQLDatabaseName;
        internal System.Windows.Forms.TextBox txtSQLDatabaseName;
        internal CheckBox chkDoubleClickOpensNewConnection;
        internal Button btnTVPathBrowse;
        private ComboBox txtTVPath;
        private Label lblTVPath;
        internal NumericUpDown numUVNCSCPort;
        internal Label lblUVNCSCPort;
        internal Button btnRAminPathBrowse;
        private ComboBox txtRAdminPath;
        private Label lblRAdminPath;
        private ComboBox txtXULrunnerPath;
        private System.ComponentModel.IContainer components;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("Startup/Exit", 0);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("Appearance", 1);
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("Tabs & Panels", 2);
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem("Connections", 3);
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem("SQL Server", 4);
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem("Updates", 5);
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem("Advanced", 6);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOptions));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lvPages = new System.Windows.Forms.ListView();
            this.imgListPages = new System.Windows.Forms.ImageList(this.components);
            this.lblMaximumPuttyWaitTime = new System.Windows.Forms.Label();
            this.chkAutomaticReconnect = new System.Windows.Forms.CheckBox();
            this.numPuttyWaitTime = new System.Windows.Forms.NumericUpDown();
            this.lblConfigurePuttySessions = new System.Windows.Forms.Label();
            this.btnLaunchPutty = new System.Windows.Forms.Button();
            this.lblSeconds = new System.Windows.Forms.Label();
            this.btnBrowseCustomPuttyPath = new System.Windows.Forms.Button();
            this.txtCustomPuttyPath = new System.Windows.Forms.TextBox();
            this.chkUseCustomPuttyPath = new System.Windows.Forms.CheckBox();
            this.btnBrowseXulRunnerPath = new System.Windows.Forms.Button();
            this.chkEncryptCompleteFile = new System.Windows.Forms.CheckBox();
            this.lblXulRunnerPath = new System.Windows.Forms.Label();
            this.chkAutomaticallyGetSessionInfo = new System.Windows.Forms.CheckBox();
            this.chkWriteLogFile = new System.Windows.Forms.CheckBox();
            this.pnlProxy = new System.Windows.Forms.Panel();
            this.pnlProxyBasic = new System.Windows.Forms.Panel();
            this.lblProxyAddress = new System.Windows.Forms.Label();
            this.txtProxyAddress = new System.Windows.Forms.TextBox();
            this.lblProxyPort = new System.Windows.Forms.Label();
            this.numProxyPort = new System.Windows.Forms.NumericUpDown();
            this.chkUseProxyForAutomaticUpdates = new System.Windows.Forms.CheckBox();
            this.chkUseProxyAuthentication = new System.Windows.Forms.CheckBox();
            this.pnlProxyAuthentication = new System.Windows.Forms.Panel();
            this.lblProxyUsername = new System.Windows.Forms.Label();
            this.txtProxyUsername = new System.Windows.Forms.TextBox();
            this.lblProxyPassword = new System.Windows.Forms.Label();
            this.txtProxyPassword = new System.Windows.Forms.TextBox();
            this.btnTestProxy = new System.Windows.Forms.Button();
            this.pnlUpdateCheck = new System.Windows.Forms.Panel();
            this.btnUpdateCheckNow = new System.Windows.Forms.Button();
            this.chkCheckForUpdatesOnStartup = new System.Windows.Forms.CheckBox();
            this.cboUpdateCheckFrequency = new System.Windows.Forms.ComboBox();
            this.lblUpdatesExplanation = new System.Windows.Forms.Label();
            this.chkHostnameLikeDisplayName = new System.Windows.Forms.CheckBox();
            this.pnlAutoSave = new System.Windows.Forms.Panel();
            this.lblAutoSave1 = new System.Windows.Forms.Label();
            this.numAutoSave = new System.Windows.Forms.NumericUpDown();
            this.lblAutoSave2 = new System.Windows.Forms.Label();
            this.chkSingleClickOnOpenedConnectionSwitchesToIt = new System.Windows.Forms.CheckBox();
            this.chkSingleClickOnConnectionOpensIt = new System.Windows.Forms.CheckBox();
            this.pnlRdpReconnectionCount = new System.Windows.Forms.Panel();
            this.lblRdpReconnectionCount = new System.Windows.Forms.Label();
            this.numRdpReconnectionCount = new System.Windows.Forms.NumericUpDown();
            this.chkAlwaysShowPanelSelectionDlg = new System.Windows.Forms.CheckBox();
            this.chkShowLogonInfoOnTabs = new System.Windows.Forms.CheckBox();
            this.chkDoubleClickClosesTab = new System.Windows.Forms.CheckBox();
            this.chkShowProtocolOnTabs = new System.Windows.Forms.CheckBox();
            this.chkOpenNewTabRightOfSelected = new System.Windows.Forms.CheckBox();
            this.chkMCWarnings = new System.Windows.Forms.CheckBox();
            this.chkMCErrors = new System.Windows.Forms.CheckBox();
            this.chkMCInformation = new System.Windows.Forms.CheckBox();
            this.lblSwitchToErrorsAndInfos = new System.Windows.Forms.Label();
            this.chkUseOnlyErrorsAndInfosPanel = new System.Windows.Forms.CheckBox();
            this.chkShowFullConnectionsFilePathInTitle = new System.Windows.Forms.CheckBox();
            this.chkShowSystemTrayIcon = new System.Windows.Forms.CheckBox();
            this.chkMinimizeToSystemTray = new System.Windows.Forms.CheckBox();
            this.chkShowDescriptionTooltipsInTree = new System.Windows.Forms.CheckBox();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.cboLanguage = new System.Windows.Forms.ComboBox();
            this.lblLanguageRestartRequired = new System.Windows.Forms.Label();
            this.chkReconnectOnStart = new System.Windows.Forms.CheckBox();
            this.chkSingleInstance = new System.Windows.Forms.CheckBox();
            this.chkConfirmExit = new System.Windows.Forms.CheckBox();
            this.chkProperInstallationOfComponentsAtStartup = new System.Windows.Forms.CheckBox();
            this.chkSaveConsOnExit = new System.Windows.Forms.CheckBox();
            this.chkConfirmCloseConnection = new System.Windows.Forms.CheckBox();
            this.tcTabControl = new System.Windows.Forms.TabControl();
            this.tabStartupExit = new System.Windows.Forms.TabPage();
            this.tabAppearance = new System.Windows.Forms.TabPage();
            this.tabTabsAndPanels = new System.Windows.Forms.TabPage();
            this.tabConnections = new System.Windows.Forms.TabPage();
            this.chkDoubleClickOpensNewConnection = new System.Windows.Forms.CheckBox();
            this.pnlDefaultCredentials = new System.Windows.Forms.Panel();
            this.radCredentialsCustom = new System.Windows.Forms.RadioButton();
            this.lblDefaultCredentials = new System.Windows.Forms.Label();
            this.radCredentialsNoInfo = new System.Windows.Forms.RadioButton();
            this.radCredentialsWindows = new System.Windows.Forms.RadioButton();
            this.txtCredentialsDomain = new System.Windows.Forms.TextBox();
            this.lblCredentialsUsername = new System.Windows.Forms.Label();
            this.txtCredentialsPassword = new System.Windows.Forms.TextBox();
            this.lblCredentialsPassword = new System.Windows.Forms.Label();
            this.txtCredentialsUsername = new System.Windows.Forms.TextBox();
            this.lblCredentialsDomain = new System.Windows.Forms.Label();
            this.tabSQLServer = new System.Windows.Forms.TabPage();
            this.lblSQLDatabaseName = new System.Windows.Forms.Label();
            this.txtSQLDatabaseName = new System.Windows.Forms.TextBox();
            this.lblExperimental = new System.Windows.Forms.Label();
            this.chkUseSQLServer = new System.Windows.Forms.CheckBox();
            this.lblSQLUsername = new System.Windows.Forms.Label();
            this.txtSQLPassword = new System.Windows.Forms.TextBox();
            this.lblSQLInfo = new System.Windows.Forms.Label();
            this.lblSQLServer = new System.Windows.Forms.Label();
            this.txtSQLUsername = new System.Windows.Forms.TextBox();
            this.txtSQLServer = new System.Windows.Forms.TextBox();
            this.lblSQLPassword = new System.Windows.Forms.Label();
            this.tabUpdates = new System.Windows.Forms.TabPage();
            this.tabAdvanced = new System.Windows.Forms.TabPage();
            this.numUVNCSCPort = new System.Windows.Forms.NumericUpDown();
            this.lblUVNCSCPort = new System.Windows.Forms.Label();
            this.btnRAminPathBrowse = new System.Windows.Forms.Button();
            this.txtRAdminPath = new System.Windows.Forms.ComboBox();
            this.lblRAdminPath = new System.Windows.Forms.Label();
            this.txtXULrunnerPath = new System.Windows.Forms.ComboBox();
            this.btnTVPathBrowse = new System.Windows.Forms.Button();
            this.txtTVPath = new System.Windows.Forms.ComboBox();
            this.lblTVPath = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numPuttyWaitTime)).BeginInit();
            this.pnlProxy.SuspendLayout();
            this.pnlProxyBasic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numProxyPort)).BeginInit();
            this.pnlProxyAuthentication.SuspendLayout();
            this.pnlUpdateCheck.SuspendLayout();
            this.pnlAutoSave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAutoSave)).BeginInit();
            this.pnlRdpReconnectionCount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRdpReconnectionCount)).BeginInit();
            this.tcTabControl.SuspendLayout();
            this.tabStartupExit.SuspendLayout();
            this.tabAppearance.SuspendLayout();
            this.tabTabsAndPanels.SuspendLayout();
            this.tabConnections.SuspendLayout();
            this.pnlDefaultCredentials.SuspendLayout();
            this.tabSQLServer.SuspendLayout();
            this.tabUpdates.SuspendLayout();
            this.tabAdvanced.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUVNCSCPort)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(626, 507);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(707, 507);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lvPages
            // 
            this.lvPages.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvPages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvPages.FullRowSelect = true;
            this.lvPages.HideSelection = false;
            this.lvPages.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12,
            listViewItem13,
            listViewItem14});
            this.lvPages.LabelWrap = false;
            this.lvPages.LargeImageList = this.imgListPages;
            this.lvPages.Location = new System.Drawing.Point(12, 12);
            this.lvPages.MultiSelect = false;
            this.lvPages.Name = "lvPages";
            this.lvPages.Scrollable = false;
            this.lvPages.Size = new System.Drawing.Size(154, 489);
            this.lvPages.TabIndex = 0;
            this.lvPages.TileSize = new System.Drawing.Size(154, 30);
            this.lvPages.UseCompatibleStateImageBehavior = false;
            this.lvPages.View = System.Windows.Forms.View.Tile;
            this.lvPages.SelectedIndexChanged += new System.EventHandler(this.lvPages_SelectedIndexChanged);
            this.lvPages.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvPages_MouseUp);
            // 
            // imgListPages
            // 
            this.imgListPages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListPages.ImageStream")));
            this.imgListPages.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imgListPages.Images.SetKeyName(0, "StartupExit_Icon.ico");
            this.imgListPages.Images.SetKeyName(1, "Appearance_Icon.ico");
            this.imgListPages.Images.SetKeyName(2, "Tab_Icon.ico");
            this.imgListPages.Images.SetKeyName(3, "Root_Icon.ico");
            this.imgListPages.Images.SetKeyName(4, "database.bmp");
            this.imgListPages.Images.SetKeyName(5, "Update_Icon.ico");
            this.imgListPages.Images.SetKeyName(6, "Config_Icon.ico");
            // 
            // lblMaximumPuttyWaitTime
            // 
            this.lblMaximumPuttyWaitTime.Location = new System.Drawing.Point(3, 188);
            this.lblMaximumPuttyWaitTime.Name = "lblMaximumPuttyWaitTime";
            this.lblMaximumPuttyWaitTime.Size = new System.Drawing.Size(364, 13);
            this.lblMaximumPuttyWaitTime.TabIndex = 9;
            this.lblMaximumPuttyWaitTime.Text = "Maximum PuTTY wait time:";
            this.lblMaximumPuttyWaitTime.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // chkAutomaticReconnect
            // 
            this.chkAutomaticReconnect.AutoSize = true;
            this.chkAutomaticReconnect.Location = new System.Drawing.Point(3, 72);
            this.chkAutomaticReconnect.Name = "chkAutomaticReconnect";
            this.chkAutomaticReconnect.Size = new System.Drawing.Size(399, 17);
            this.chkAutomaticReconnect.TabIndex = 3;
            this.chkAutomaticReconnect.Text = "Automatically try to reconnect when disconnected from server (RDP && ICA only)";
            this.chkAutomaticReconnect.UseVisualStyleBackColor = true;
            // 
            // numPuttyWaitTime
            // 
            this.numPuttyWaitTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numPuttyWaitTime.Location = new System.Drawing.Point(373, 186);
            this.numPuttyWaitTime.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numPuttyWaitTime.Name = "numPuttyWaitTime";
            this.numPuttyWaitTime.Size = new System.Drawing.Size(49, 20);
            this.numPuttyWaitTime.TabIndex = 10;
            this.numPuttyWaitTime.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lblConfigurePuttySessions
            // 
            this.lblConfigurePuttySessions.Location = new System.Drawing.Point(3, 157);
            this.lblConfigurePuttySessions.Name = "lblConfigurePuttySessions";
            this.lblConfigurePuttySessions.Size = new System.Drawing.Size(364, 13);
            this.lblConfigurePuttySessions.TabIndex = 7;
            this.lblConfigurePuttySessions.Text = "To configure PuTTY sessions click this button:";
            this.lblConfigurePuttySessions.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnLaunchPutty
            // 
            this.btnLaunchPutty.Image = global::My.Resources.Resources.PuttyConfig;
            this.btnLaunchPutty.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLaunchPutty.Location = new System.Drawing.Point(373, 152);
            this.btnLaunchPutty.Name = "btnLaunchPutty";
            this.btnLaunchPutty.Size = new System.Drawing.Size(110, 23);
            this.btnLaunchPutty.TabIndex = 8;
            this.btnLaunchPutty.Text = "Launch PuTTY";
            this.btnLaunchPutty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLaunchPutty.UseVisualStyleBackColor = true;
            this.btnLaunchPutty.Click += new System.EventHandler(this.btnLaunchPutty_Click);
            // 
            // lblSeconds
            // 
            this.lblSeconds.AutoSize = true;
            this.lblSeconds.Location = new System.Drawing.Point(428, 188);
            this.lblSeconds.Name = "lblSeconds";
            this.lblSeconds.Size = new System.Drawing.Size(47, 13);
            this.lblSeconds.TabIndex = 11;
            this.lblSeconds.Text = "seconds";
            // 
            // btnBrowseCustomPuttyPath
            // 
            this.btnBrowseCustomPuttyPath.Enabled = false;
            this.btnBrowseCustomPuttyPath.Location = new System.Drawing.Point(373, 116);
            this.btnBrowseCustomPuttyPath.Name = "btnBrowseCustomPuttyPath";
            this.btnBrowseCustomPuttyPath.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseCustomPuttyPath.TabIndex = 6;
            this.btnBrowseCustomPuttyPath.Text = "Browse...";
            this.btnBrowseCustomPuttyPath.UseVisualStyleBackColor = true;
            this.btnBrowseCustomPuttyPath.Click += new System.EventHandler(this.btnBrowseCustomPuttyPath_Click);
            // 
            // txtCustomPuttyPath
            // 
            this.txtCustomPuttyPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustomPuttyPath.Enabled = false;
            this.txtCustomPuttyPath.Location = new System.Drawing.Point(21, 118);
            this.txtCustomPuttyPath.Name = "txtCustomPuttyPath";
            this.txtCustomPuttyPath.Size = new System.Drawing.Size(346, 20);
            this.txtCustomPuttyPath.TabIndex = 5;
            // 
            // chkUseCustomPuttyPath
            // 
            this.chkUseCustomPuttyPath.AutoSize = true;
            this.chkUseCustomPuttyPath.Location = new System.Drawing.Point(3, 95);
            this.chkUseCustomPuttyPath.Name = "chkUseCustomPuttyPath";
            this.chkUseCustomPuttyPath.Size = new System.Drawing.Size(146, 17);
            this.chkUseCustomPuttyPath.TabIndex = 4;
            this.chkUseCustomPuttyPath.Text = "Use custom PuTTY path:";
            this.chkUseCustomPuttyPath.UseVisualStyleBackColor = true;
            this.chkUseCustomPuttyPath.CheckedChanged += new System.EventHandler(this.chkUseCustomPuttyPath_CheckedChanged);
            // 
            // btnBrowseXulRunnerPath
            // 
            this.btnBrowseXulRunnerPath.Location = new System.Drawing.Point(373, 274);
            this.btnBrowseXulRunnerPath.Name = "btnBrowseXulRunnerPath";
            this.btnBrowseXulRunnerPath.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseXulRunnerPath.TabIndex = 14;
            this.btnBrowseXulRunnerPath.Text = "Browse...";
            this.btnBrowseXulRunnerPath.UseVisualStyleBackColor = true;
            this.btnBrowseXulRunnerPath.Click += new System.EventHandler(this.btnBrowseXulRunnerPath_Click);
            // 
            // chkEncryptCompleteFile
            // 
            this.chkEncryptCompleteFile.AutoSize = true;
            this.chkEncryptCompleteFile.Location = new System.Drawing.Point(2, 26);
            this.chkEncryptCompleteFile.Name = "chkEncryptCompleteFile";
            this.chkEncryptCompleteFile.Size = new System.Drawing.Size(180, 17);
            this.chkEncryptCompleteFile.TabIndex = 1;
            this.chkEncryptCompleteFile.Text = "Encrypt complete connection file";
            this.chkEncryptCompleteFile.UseVisualStyleBackColor = true;
            // 
            // lblXulRunnerPath
            // 
            this.lblXulRunnerPath.AutoSize = true;
            this.lblXulRunnerPath.Location = new System.Drawing.Point(3, 246);
            this.lblXulRunnerPath.Name = "lblXulRunnerPath";
            this.lblXulRunnerPath.Size = new System.Drawing.Size(85, 13);
            this.lblXulRunnerPath.TabIndex = 12;
            this.lblXulRunnerPath.Text = "XULrunner path:";
            // 
            // chkAutomaticallyGetSessionInfo
            // 
            this.chkAutomaticallyGetSessionInfo.AutoSize = true;
            this.chkAutomaticallyGetSessionInfo.Location = new System.Drawing.Point(3, 49);
            this.chkAutomaticallyGetSessionInfo.Name = "chkAutomaticallyGetSessionInfo";
            this.chkAutomaticallyGetSessionInfo.Size = new System.Drawing.Size(198, 17);
            this.chkAutomaticallyGetSessionInfo.TabIndex = 2;
            this.chkAutomaticallyGetSessionInfo.Text = "Automatically get session information";
            this.chkAutomaticallyGetSessionInfo.UseVisualStyleBackColor = true;
            // 
            // chkWriteLogFile
            // 
            this.chkWriteLogFile.AutoSize = true;
            this.chkWriteLogFile.Location = new System.Drawing.Point(3, 3);
            this.chkWriteLogFile.Name = "chkWriteLogFile";
            this.chkWriteLogFile.Size = new System.Drawing.Size(170, 17);
            this.chkWriteLogFile.TabIndex = 0;
            this.chkWriteLogFile.Text = "Write log file (mRemoteNC.log)";
            this.chkWriteLogFile.UseVisualStyleBackColor = true;
            // 
            // pnlProxy
            // 
            this.pnlProxy.Controls.Add(this.pnlProxyBasic);
            this.pnlProxy.Controls.Add(this.chkUseProxyForAutomaticUpdates);
            this.pnlProxy.Controls.Add(this.chkUseProxyAuthentication);
            this.pnlProxy.Controls.Add(this.pnlProxyAuthentication);
            this.pnlProxy.Controls.Add(this.btnTestProxy);
            this.pnlProxy.Location = new System.Drawing.Point(3, 200);
            this.pnlProxy.Name = "pnlProxy";
            this.pnlProxy.Size = new System.Drawing.Size(536, 224);
            this.pnlProxy.TabIndex = 2;
            // 
            // pnlProxyBasic
            // 
            this.pnlProxyBasic.Controls.Add(this.lblProxyAddress);
            this.pnlProxyBasic.Controls.Add(this.txtProxyAddress);
            this.pnlProxyBasic.Controls.Add(this.lblProxyPort);
            this.pnlProxyBasic.Controls.Add(this.numProxyPort);
            this.pnlProxyBasic.Enabled = false;
            this.pnlProxyBasic.Location = new System.Drawing.Point(8, 32);
            this.pnlProxyBasic.Name = "pnlProxyBasic";
            this.pnlProxyBasic.Size = new System.Drawing.Size(512, 40);
            this.pnlProxyBasic.TabIndex = 1;
            // 
            // lblProxyAddress
            // 
            this.lblProxyAddress.Location = new System.Drawing.Point(8, 4);
            this.lblProxyAddress.Name = "lblProxyAddress";
            this.lblProxyAddress.Size = new System.Drawing.Size(96, 24);
            this.lblProxyAddress.TabIndex = 0;
            this.lblProxyAddress.Text = "Address:";
            this.lblProxyAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProxyAddress
            // 
            this.txtProxyAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProxyAddress.Location = new System.Drawing.Point(104, 8);
            this.txtProxyAddress.Name = "txtProxyAddress";
            this.txtProxyAddress.Size = new System.Drawing.Size(240, 20);
            this.txtProxyAddress.TabIndex = 1;
            // 
            // lblProxyPort
            // 
            this.lblProxyPort.Location = new System.Drawing.Point(350, 5);
            this.lblProxyPort.Name = "lblProxyPort";
            this.lblProxyPort.Size = new System.Drawing.Size(64, 23);
            this.lblProxyPort.TabIndex = 2;
            this.lblProxyPort.Text = "Port:";
            this.lblProxyPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblProxyPort.Click += new System.EventHandler(this.lblProxyPort_Click);
            // 
            // numProxyPort
            // 
            this.numProxyPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numProxyPort.Location = new System.Drawing.Point(420, 8);
            this.numProxyPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numProxyPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numProxyPort.Name = "numProxyPort";
            this.numProxyPort.Size = new System.Drawing.Size(64, 20);
            this.numProxyPort.TabIndex = 3;
            this.numProxyPort.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            // 
            // chkUseProxyForAutomaticUpdates
            // 
            this.chkUseProxyForAutomaticUpdates.AutoSize = true;
            this.chkUseProxyForAutomaticUpdates.Location = new System.Drawing.Point(8, 8);
            this.chkUseProxyForAutomaticUpdates.Name = "chkUseProxyForAutomaticUpdates";
            this.chkUseProxyForAutomaticUpdates.Size = new System.Drawing.Size(168, 17);
            this.chkUseProxyForAutomaticUpdates.TabIndex = 0;
            this.chkUseProxyForAutomaticUpdates.Text = "Use a proxy server to connect";
            this.chkUseProxyForAutomaticUpdates.UseVisualStyleBackColor = true;
            this.chkUseProxyForAutomaticUpdates.CheckedChanged += new System.EventHandler(this.chkUseProxyForAutomaticUpdates_CheckedChanged);
            // 
            // chkUseProxyAuthentication
            // 
            this.chkUseProxyAuthentication.AutoSize = true;
            this.chkUseProxyAuthentication.Enabled = false;
            this.chkUseProxyAuthentication.Location = new System.Drawing.Point(32, 80);
            this.chkUseProxyAuthentication.Name = "chkUseProxyAuthentication";
            this.chkUseProxyAuthentication.Size = new System.Drawing.Size(216, 17);
            this.chkUseProxyAuthentication.TabIndex = 2;
            this.chkUseProxyAuthentication.Text = "This proxy server requires authentication";
            this.chkUseProxyAuthentication.UseVisualStyleBackColor = true;
            this.chkUseProxyAuthentication.CheckedChanged += new System.EventHandler(this.chkUseProxyAuthentication_CheckedChanged);
            // 
            // pnlProxyAuthentication
            // 
            this.pnlProxyAuthentication.Controls.Add(this.lblProxyUsername);
            this.pnlProxyAuthentication.Controls.Add(this.txtProxyUsername);
            this.pnlProxyAuthentication.Controls.Add(this.lblProxyPassword);
            this.pnlProxyAuthentication.Controls.Add(this.txtProxyPassword);
            this.pnlProxyAuthentication.Enabled = false;
            this.pnlProxyAuthentication.Location = new System.Drawing.Point(8, 104);
            this.pnlProxyAuthentication.Name = "pnlProxyAuthentication";
            this.pnlProxyAuthentication.Size = new System.Drawing.Size(512, 72);
            this.pnlProxyAuthentication.TabIndex = 3;
            // 
            // lblProxyUsername
            // 
            this.lblProxyUsername.Location = new System.Drawing.Point(8, 4);
            this.lblProxyUsername.Name = "lblProxyUsername";
            this.lblProxyUsername.Size = new System.Drawing.Size(96, 24);
            this.lblProxyUsername.TabIndex = 0;
            this.lblProxyUsername.Text = "Username:";
            this.lblProxyUsername.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProxyUsername
            // 
            this.txtProxyUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProxyUsername.Location = new System.Drawing.Point(104, 8);
            this.txtProxyUsername.Name = "txtProxyUsername";
            this.txtProxyUsername.Size = new System.Drawing.Size(240, 20);
            this.txtProxyUsername.TabIndex = 1;
            // 
            // lblProxyPassword
            // 
            this.lblProxyPassword.Location = new System.Drawing.Point(8, 36);
            this.lblProxyPassword.Name = "lblProxyPassword";
            this.lblProxyPassword.Size = new System.Drawing.Size(96, 24);
            this.lblProxyPassword.TabIndex = 2;
            this.lblProxyPassword.Text = "Password:";
            this.lblProxyPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProxyPassword
            // 
            this.txtProxyPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProxyPassword.Location = new System.Drawing.Point(104, 40);
            this.txtProxyPassword.Name = "txtProxyPassword";
            this.txtProxyPassword.Size = new System.Drawing.Size(240, 20);
            this.txtProxyPassword.TabIndex = 3;
            this.txtProxyPassword.UseSystemPasswordChar = true;
            // 
            // btnTestProxy
            // 
            this.btnTestProxy.Location = new System.Drawing.Point(8, 184);
            this.btnTestProxy.Name = "btnTestProxy";
            this.btnTestProxy.Size = new System.Drawing.Size(120, 32);
            this.btnTestProxy.TabIndex = 4;
            this.btnTestProxy.Text = "Test Proxy";
            this.btnTestProxy.UseVisualStyleBackColor = true;
            this.btnTestProxy.Click += new System.EventHandler(this.btnTestProxy_Click);
            // 
            // pnlUpdateCheck
            // 
            this.pnlUpdateCheck.Controls.Add(this.btnUpdateCheckNow);
            this.pnlUpdateCheck.Controls.Add(this.chkCheckForUpdatesOnStartup);
            this.pnlUpdateCheck.Controls.Add(this.cboUpdateCheckFrequency);
            this.pnlUpdateCheck.Location = new System.Drawing.Point(3, 48);
            this.pnlUpdateCheck.Name = "pnlUpdateCheck";
            this.pnlUpdateCheck.Size = new System.Drawing.Size(536, 120);
            this.pnlUpdateCheck.TabIndex = 1;
            // 
            // btnUpdateCheckNow
            // 
            this.btnUpdateCheckNow.Location = new System.Drawing.Point(8, 80);
            this.btnUpdateCheckNow.Name = "btnUpdateCheckNow";
            this.btnUpdateCheckNow.Size = new System.Drawing.Size(120, 32);
            this.btnUpdateCheckNow.TabIndex = 2;
            this.btnUpdateCheckNow.Text = "Check Now";
            this.btnUpdateCheckNow.UseVisualStyleBackColor = true;
            this.btnUpdateCheckNow.Click += new System.EventHandler(this.btnUpdateCheckNow_Click);
            // 
            // chkCheckForUpdatesOnStartup
            // 
            this.chkCheckForUpdatesOnStartup.AutoSize = true;
            this.chkCheckForUpdatesOnStartup.Location = new System.Drawing.Point(8, 8);
            this.chkCheckForUpdatesOnStartup.Name = "chkCheckForUpdatesOnStartup";
            this.chkCheckForUpdatesOnStartup.Size = new System.Drawing.Size(213, 17);
            this.chkCheckForUpdatesOnStartup.TabIndex = 0;
            this.chkCheckForUpdatesOnStartup.Text = "Check for updates and announcements";
            this.chkCheckForUpdatesOnStartup.UseVisualStyleBackColor = true;
            this.chkCheckForUpdatesOnStartup.CheckedChanged += new System.EventHandler(this.chkCheckForUpdatesOnStartup_CheckedChanged);
            // 
            // cboUpdateCheckFrequency
            // 
            this.cboUpdateCheckFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUpdateCheckFrequency.FormattingEnabled = true;
            this.cboUpdateCheckFrequency.Location = new System.Drawing.Point(48, 40);
            this.cboUpdateCheckFrequency.Name = "cboUpdateCheckFrequency";
            this.cboUpdateCheckFrequency.Size = new System.Drawing.Size(128, 21);
            this.cboUpdateCheckFrequency.TabIndex = 1;
            // 
            // lblUpdatesExplanation
            // 
            this.lblUpdatesExplanation.Location = new System.Drawing.Point(3, 0);
            this.lblUpdatesExplanation.Name = "lblUpdatesExplanation";
            this.lblUpdatesExplanation.Size = new System.Drawing.Size(536, 40);
            this.lblUpdatesExplanation.TabIndex = 0;
            this.lblUpdatesExplanation.Text = "mRemoteNC can periodically connect to the mRemoteNC website to check for updates " +
    "and product announcements.";
            // 
            // chkHostnameLikeDisplayName
            // 
            this.chkHostnameLikeDisplayName.AutoSize = true;
            this.chkHostnameLikeDisplayName.Location = new System.Drawing.Point(3, 72);
            this.chkHostnameLikeDisplayName.Name = "chkHostnameLikeDisplayName";
            this.chkHostnameLikeDisplayName.Size = new System.Drawing.Size(328, 17);
            this.chkHostnameLikeDisplayName.TabIndex = 2;
            this.chkHostnameLikeDisplayName.Text = "Set hostname like display name when creating new connections";
            this.chkHostnameLikeDisplayName.UseVisualStyleBackColor = true;
            // 
            // pnlAutoSave
            // 
            this.pnlAutoSave.Controls.Add(this.lblAutoSave1);
            this.pnlAutoSave.Controls.Add(this.numAutoSave);
            this.pnlAutoSave.Controls.Add(this.lblAutoSave2);
            this.pnlAutoSave.Location = new System.Drawing.Point(3, 130);
            this.pnlAutoSave.Name = "pnlAutoSave";
            this.pnlAutoSave.Size = new System.Drawing.Size(500, 29);
            this.pnlAutoSave.TabIndex = 4;
            // 
            // lblAutoSave1
            // 
            this.lblAutoSave1.Location = new System.Drawing.Point(6, 9);
            this.lblAutoSave1.Name = "lblAutoSave1";
            this.lblAutoSave1.Size = new System.Drawing.Size(288, 13);
            this.lblAutoSave1.TabIndex = 0;
            this.lblAutoSave1.Text = "Auto Save every:";
            this.lblAutoSave1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // numAutoSave
            // 
            this.numAutoSave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numAutoSave.Location = new System.Drawing.Point(300, 7);
            this.numAutoSave.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numAutoSave.Name = "numAutoSave";
            this.numAutoSave.Size = new System.Drawing.Size(53, 20);
            this.numAutoSave.TabIndex = 1;
            // 
            // lblAutoSave2
            // 
            this.lblAutoSave2.AutoSize = true;
            this.lblAutoSave2.Location = new System.Drawing.Point(359, 9);
            this.lblAutoSave2.Name = "lblAutoSave2";
            this.lblAutoSave2.Size = new System.Drawing.Size(135, 13);
            this.lblAutoSave2.TabIndex = 2;
            this.lblAutoSave2.Text = "Minutes (0 means disabled)";
            // 
            // chkSingleClickOnOpenedConnectionSwitchesToIt
            // 
            this.chkSingleClickOnOpenedConnectionSwitchesToIt.AutoSize = true;
            this.chkSingleClickOnOpenedConnectionSwitchesToIt.Location = new System.Drawing.Point(3, 49);
            this.chkSingleClickOnOpenedConnectionSwitchesToIt.Name = "chkSingleClickOnOpenedConnectionSwitchesToIt";
            this.chkSingleClickOnOpenedConnectionSwitchesToIt.Size = new System.Drawing.Size(254, 17);
            this.chkSingleClickOnOpenedConnectionSwitchesToIt.TabIndex = 1;
            this.chkSingleClickOnOpenedConnectionSwitchesToIt.Text = "Single click on opened connection switches to it";
            this.chkSingleClickOnOpenedConnectionSwitchesToIt.UseVisualStyleBackColor = true;
            // 
            // chkSingleClickOnConnectionOpensIt
            // 
            this.chkSingleClickOnConnectionOpensIt.AutoSize = true;
            this.chkSingleClickOnConnectionOpensIt.Location = new System.Drawing.Point(3, 3);
            this.chkSingleClickOnConnectionOpensIt.Name = "chkSingleClickOnConnectionOpensIt";
            this.chkSingleClickOnConnectionOpensIt.Size = new System.Drawing.Size(191, 17);
            this.chkSingleClickOnConnectionOpensIt.TabIndex = 0;
            this.chkSingleClickOnConnectionOpensIt.Text = "Single click on connection opens it";
            this.chkSingleClickOnConnectionOpensIt.UseVisualStyleBackColor = true;
            // 
            // pnlRdpReconnectionCount
            // 
            this.pnlRdpReconnectionCount.Controls.Add(this.lblRdpReconnectionCount);
            this.pnlRdpReconnectionCount.Controls.Add(this.numRdpReconnectionCount);
            this.pnlRdpReconnectionCount.Location = new System.Drawing.Point(3, 95);
            this.pnlRdpReconnectionCount.Name = "pnlRdpReconnectionCount";
            this.pnlRdpReconnectionCount.Size = new System.Drawing.Size(500, 29);
            this.pnlRdpReconnectionCount.TabIndex = 3;
            // 
            // lblRdpReconnectionCount
            // 
            this.lblRdpReconnectionCount.Location = new System.Drawing.Point(6, 9);
            this.lblRdpReconnectionCount.Name = "lblRdpReconnectionCount";
            this.lblRdpReconnectionCount.Size = new System.Drawing.Size(288, 13);
            this.lblRdpReconnectionCount.TabIndex = 0;
            this.lblRdpReconnectionCount.Text = "RDP Reconnection Count";
            this.lblRdpReconnectionCount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // numRdpReconnectionCount
            // 
            this.numRdpReconnectionCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numRdpReconnectionCount.Location = new System.Drawing.Point(300, 7);
            this.numRdpReconnectionCount.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numRdpReconnectionCount.Name = "numRdpReconnectionCount";
            this.numRdpReconnectionCount.Size = new System.Drawing.Size(53, 20);
            this.numRdpReconnectionCount.TabIndex = 1;
            this.numRdpReconnectionCount.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // chkAlwaysShowPanelSelectionDlg
            // 
            this.chkAlwaysShowPanelSelectionDlg.AutoSize = true;
            this.chkAlwaysShowPanelSelectionDlg.Location = new System.Drawing.Point(3, 95);
            this.chkAlwaysShowPanelSelectionDlg.Name = "chkAlwaysShowPanelSelectionDlg";
            this.chkAlwaysShowPanelSelectionDlg.Size = new System.Drawing.Size(317, 17);
            this.chkAlwaysShowPanelSelectionDlg.TabIndex = 4;
            this.chkAlwaysShowPanelSelectionDlg.Text = "Always show panel selection dialog when opening connectins";
            this.chkAlwaysShowPanelSelectionDlg.UseVisualStyleBackColor = true;
            // 
            // chkShowLogonInfoOnTabs
            // 
            this.chkShowLogonInfoOnTabs.AutoSize = true;
            this.chkShowLogonInfoOnTabs.Location = new System.Drawing.Point(3, 26);
            this.chkShowLogonInfoOnTabs.Name = "chkShowLogonInfoOnTabs";
            this.chkShowLogonInfoOnTabs.Size = new System.Drawing.Size(203, 17);
            this.chkShowLogonInfoOnTabs.TabIndex = 1;
            this.chkShowLogonInfoOnTabs.Text = "Show logon information on tab names";
            this.chkShowLogonInfoOnTabs.UseVisualStyleBackColor = true;
            // 
            // chkDoubleClickClosesTab
            // 
            this.chkDoubleClickClosesTab.AutoSize = true;
            this.chkDoubleClickClosesTab.Location = new System.Drawing.Point(3, 72);
            this.chkDoubleClickClosesTab.Name = "chkDoubleClickClosesTab";
            this.chkDoubleClickClosesTab.Size = new System.Drawing.Size(159, 17);
            this.chkDoubleClickClosesTab.TabIndex = 3;
            this.chkDoubleClickClosesTab.Text = "Double click on tab closes it";
            this.chkDoubleClickClosesTab.UseVisualStyleBackColor = true;
            // 
            // chkShowProtocolOnTabs
            // 
            this.chkShowProtocolOnTabs.AutoSize = true;
            this.chkShowProtocolOnTabs.Location = new System.Drawing.Point(3, 49);
            this.chkShowProtocolOnTabs.Name = "chkShowProtocolOnTabs";
            this.chkShowProtocolOnTabs.Size = new System.Drawing.Size(166, 17);
            this.chkShowProtocolOnTabs.TabIndex = 2;
            this.chkShowProtocolOnTabs.Text = "Show protocols on tab names";
            this.chkShowProtocolOnTabs.UseVisualStyleBackColor = true;
            // 
            // chkOpenNewTabRightOfSelected
            // 
            this.chkOpenNewTabRightOfSelected.AutoSize = true;
            this.chkOpenNewTabRightOfSelected.Location = new System.Drawing.Point(3, 3);
            this.chkOpenNewTabRightOfSelected.Name = "chkOpenNewTabRightOfSelected";
            this.chkOpenNewTabRightOfSelected.Size = new System.Drawing.Size(280, 17);
            this.chkOpenNewTabRightOfSelected.TabIndex = 0;
            this.chkOpenNewTabRightOfSelected.Text = "Open new tab to the right of the currently selected tab";
            this.chkOpenNewTabRightOfSelected.UseVisualStyleBackColor = true;
            // 
            // chkMCWarnings
            // 
            this.chkMCWarnings.AutoSize = true;
            this.chkMCWarnings.Enabled = false;
            this.chkMCWarnings.Location = new System.Drawing.Point(19, 214);
            this.chkMCWarnings.Name = "chkMCWarnings";
            this.chkMCWarnings.Size = new System.Drawing.Size(71, 17);
            this.chkMCWarnings.TabIndex = 8;
            this.chkMCWarnings.Text = "Warnings";
            this.chkMCWarnings.UseVisualStyleBackColor = true;
            // 
            // chkMCErrors
            // 
            this.chkMCErrors.AutoSize = true;
            this.chkMCErrors.Enabled = false;
            this.chkMCErrors.Location = new System.Drawing.Point(19, 237);
            this.chkMCErrors.Name = "chkMCErrors";
            this.chkMCErrors.Size = new System.Drawing.Size(53, 17);
            this.chkMCErrors.TabIndex = 9;
            this.chkMCErrors.Text = "Errors";
            this.chkMCErrors.UseVisualStyleBackColor = true;
            // 
            // chkMCInformation
            // 
            this.chkMCInformation.AutoSize = true;
            this.chkMCInformation.Enabled = false;
            this.chkMCInformation.Location = new System.Drawing.Point(19, 191);
            this.chkMCInformation.Name = "chkMCInformation";
            this.chkMCInformation.Size = new System.Drawing.Size(83, 17);
            this.chkMCInformation.TabIndex = 7;
            this.chkMCInformation.Text = "Informations";
            this.chkMCInformation.UseVisualStyleBackColor = true;
            // 
            // lblSwitchToErrorsAndInfos
            // 
            this.lblSwitchToErrorsAndInfos.AutoSize = true;
            this.lblSwitchToErrorsAndInfos.Location = new System.Drawing.Point(3, 171);
            this.lblSwitchToErrorsAndInfos.Name = "lblSwitchToErrorsAndInfos";
            this.lblSwitchToErrorsAndInfos.Size = new System.Drawing.Size(159, 13);
            this.lblSwitchToErrorsAndInfos.TabIndex = 6;
            this.lblSwitchToErrorsAndInfos.Text = "Switch to Notifications panel on:";
            // 
            // chkUseOnlyErrorsAndInfosPanel
            // 
            this.chkUseOnlyErrorsAndInfosPanel.AutoSize = true;
            this.chkUseOnlyErrorsAndInfosPanel.Location = new System.Drawing.Point(3, 146);
            this.chkUseOnlyErrorsAndInfosPanel.Name = "chkUseOnlyErrorsAndInfosPanel";
            this.chkUseOnlyErrorsAndInfosPanel.Size = new System.Drawing.Size(278, 17);
            this.chkUseOnlyErrorsAndInfosPanel.TabIndex = 5;
            this.chkUseOnlyErrorsAndInfosPanel.Text = "Use only Notifications panel (no messagebox popups)";
            this.chkUseOnlyErrorsAndInfosPanel.UseVisualStyleBackColor = true;
            this.chkUseOnlyErrorsAndInfosPanel.CheckedChanged += new System.EventHandler(this.chkUseOnlyErrorsAndInfosPanel_CheckedChanged);
            // 
            // chkShowFullConnectionsFilePathInTitle
            // 
            this.chkShowFullConnectionsFilePathInTitle.AutoSize = true;
            this.chkShowFullConnectionsFilePathInTitle.Location = new System.Drawing.Point(3, 128);
            this.chkShowFullConnectionsFilePathInTitle.Name = "chkShowFullConnectionsFilePathInTitle";
            this.chkShowFullConnectionsFilePathInTitle.Size = new System.Drawing.Size(239, 17);
            this.chkShowFullConnectionsFilePathInTitle.TabIndex = 4;
            this.chkShowFullConnectionsFilePathInTitle.Text = "Show full connections file path in window title";
            this.chkShowFullConnectionsFilePathInTitle.UseVisualStyleBackColor = true;
            // 
            // chkShowSystemTrayIcon
            // 
            this.chkShowSystemTrayIcon.AutoSize = true;
            this.chkShowSystemTrayIcon.Location = new System.Drawing.Point(3, 176);
            this.chkShowSystemTrayIcon.Name = "chkShowSystemTrayIcon";
            this.chkShowSystemTrayIcon.Size = new System.Drawing.Size(172, 17);
            this.chkShowSystemTrayIcon.TabIndex = 5;
            this.chkShowSystemTrayIcon.Text = "Always show System Tray Icon";
            this.chkShowSystemTrayIcon.UseVisualStyleBackColor = true;
            // 
            // chkMinimizeToSystemTray
            // 
            this.chkMinimizeToSystemTray.AutoSize = true;
            this.chkMinimizeToSystemTray.Location = new System.Drawing.Point(3, 200);
            this.chkMinimizeToSystemTray.Name = "chkMinimizeToSystemTray";
            this.chkMinimizeToSystemTray.Size = new System.Drawing.Size(139, 17);
            this.chkMinimizeToSystemTray.TabIndex = 6;
            this.chkMinimizeToSystemTray.Text = "Minimize to System Tray";
            this.chkMinimizeToSystemTray.UseVisualStyleBackColor = true;
            // 
            // chkShowDescriptionTooltipsInTree
            // 
            this.chkShowDescriptionTooltipsInTree.AutoSize = true;
            this.chkShowDescriptionTooltipsInTree.Location = new System.Drawing.Point(3, 104);
            this.chkShowDescriptionTooltipsInTree.Name = "chkShowDescriptionTooltipsInTree";
            this.chkShowDescriptionTooltipsInTree.Size = new System.Drawing.Size(231, 17);
            this.chkShowDescriptionTooltipsInTree.TabIndex = 3;
            this.chkShowDescriptionTooltipsInTree.Text = "Show description tooltips in connection tree";
            this.chkShowDescriptionTooltipsInTree.UseVisualStyleBackColor = true;
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Location = new System.Drawing.Point(3, 0);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(55, 13);
            this.lblLanguage.TabIndex = 0;
            this.lblLanguage.Text = "Language";
            // 
            // cboLanguage
            // 
            this.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLanguage.FormattingEnabled = true;
            this.cboLanguage.Location = new System.Drawing.Point(3, 24);
            this.cboLanguage.Name = "cboLanguage";
            this.cboLanguage.Size = new System.Drawing.Size(304, 21);
            this.cboLanguage.Sorted = true;
            this.cboLanguage.TabIndex = 1;
            // 
            // lblLanguageRestartRequired
            // 
            this.lblLanguageRestartRequired.AutoSize = true;
            this.lblLanguageRestartRequired.Location = new System.Drawing.Point(3, 56);
            this.lblLanguageRestartRequired.Name = "lblLanguageRestartRequired";
            this.lblLanguageRestartRequired.Size = new System.Drawing.Size(379, 13);
            this.lblLanguageRestartRequired.TabIndex = 2;
            this.lblLanguageRestartRequired.Text = "mRemoteNC must be restarted before changes to the language will take effect.";
            // 
            // chkReconnectOnStart
            // 
            this.chkReconnectOnStart.AutoSize = true;
            this.chkReconnectOnStart.Location = new System.Drawing.Point(3, 75);
            this.chkReconnectOnStart.Name = "chkReconnectOnStart";
            this.chkReconnectOnStart.Size = new System.Drawing.Size(273, 17);
            this.chkReconnectOnStart.TabIndex = 3;
            this.chkReconnectOnStart.Text = "Reconnect to previously opened sessions on startup";
            this.chkReconnectOnStart.UseVisualStyleBackColor = true;
            // 
            // chkSingleInstance
            // 
            this.chkSingleInstance.AutoSize = true;
            this.chkSingleInstance.Location = new System.Drawing.Point(3, 99);
            this.chkSingleInstance.Name = "chkSingleInstance";
            this.chkSingleInstance.Size = new System.Drawing.Size(366, 17);
            this.chkSingleInstance.TabIndex = 4;
            this.chkSingleInstance.Text = "Allow only a single instance of the application (mRemote restart required)";
            this.chkSingleInstance.UseVisualStyleBackColor = true;
            // 
            // chkConfirmExit
            // 
            this.chkConfirmExit.AutoSize = true;
            this.chkConfirmExit.Location = new System.Drawing.Point(3, 27);
            this.chkConfirmExit.Name = "chkConfirmExit";
            this.chkConfirmExit.Size = new System.Drawing.Size(221, 17);
            this.chkConfirmExit.TabIndex = 1;
            this.chkConfirmExit.Text = "Confirm exit if there are open connections";
            this.chkConfirmExit.UseVisualStyleBackColor = true;
            // 
            // chkProperInstallationOfComponentsAtStartup
            // 
            this.chkProperInstallationOfComponentsAtStartup.AutoSize = true;
            this.chkProperInstallationOfComponentsAtStartup.Location = new System.Drawing.Point(3, 123);
            this.chkProperInstallationOfComponentsAtStartup.Name = "chkProperInstallationOfComponentsAtStartup";
            this.chkProperInstallationOfComponentsAtStartup.Size = new System.Drawing.Size(262, 17);
            this.chkProperInstallationOfComponentsAtStartup.TabIndex = 5;
            this.chkProperInstallationOfComponentsAtStartup.Text = "Check proper installation of components at startup";
            this.chkProperInstallationOfComponentsAtStartup.UseVisualStyleBackColor = true;
            // 
            // chkSaveConsOnExit
            // 
            this.chkSaveConsOnExit.AutoSize = true;
            this.chkSaveConsOnExit.Location = new System.Drawing.Point(3, 51);
            this.chkSaveConsOnExit.Name = "chkSaveConsOnExit";
            this.chkSaveConsOnExit.Size = new System.Drawing.Size(146, 17);
            this.chkSaveConsOnExit.TabIndex = 2;
            this.chkSaveConsOnExit.Text = "Save connections on exit";
            this.chkSaveConsOnExit.UseVisualStyleBackColor = true;
            // 
            // chkConfirmCloseConnection
            // 
            this.chkConfirmCloseConnection.AutoSize = true;
            this.chkConfirmCloseConnection.Location = new System.Drawing.Point(3, 3);
            this.chkConfirmCloseConnection.Name = "chkConfirmCloseConnection";
            this.chkConfirmCloseConnection.Size = new System.Drawing.Size(176, 17);
            this.chkConfirmCloseConnection.TabIndex = 0;
            this.chkConfirmCloseConnection.Text = "Confirm closing connection tabs";
            this.chkConfirmCloseConnection.UseVisualStyleBackColor = true;
            // 
            // tcTabControl
            // 
            this.tcTabControl.Controls.Add(this.tabStartupExit);
            this.tcTabControl.Controls.Add(this.tabAppearance);
            this.tcTabControl.Controls.Add(this.tabTabsAndPanels);
            this.tcTabControl.Controls.Add(this.tabConnections);
            this.tcTabControl.Controls.Add(this.tabSQLServer);
            this.tcTabControl.Controls.Add(this.tabUpdates);
            this.tcTabControl.Controls.Add(this.tabAdvanced);
            this.tcTabControl.Location = new System.Drawing.Point(172, 12);
            this.tcTabControl.Name = "tcTabControl";
            this.tcTabControl.SelectedIndex = 0;
            this.tcTabControl.Size = new System.Drawing.Size(610, 489);
            this.tcTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcTabControl.TabIndex = 1;
            this.tcTabControl.TabStop = false;
            // 
            // tabStartupExit
            // 
            this.tabStartupExit.Controls.Add(this.chkConfirmCloseConnection);
            this.tabStartupExit.Controls.Add(this.chkReconnectOnStart);
            this.tabStartupExit.Controls.Add(this.chkSaveConsOnExit);
            this.tabStartupExit.Controls.Add(this.chkSingleInstance);
            this.tabStartupExit.Controls.Add(this.chkProperInstallationOfComponentsAtStartup);
            this.tabStartupExit.Controls.Add(this.chkConfirmExit);
            this.tabStartupExit.Location = new System.Drawing.Point(4, 22);
            this.tabStartupExit.Name = "tabStartupExit";
            this.tabStartupExit.Size = new System.Drawing.Size(602, 463);
            this.tabStartupExit.TabIndex = 0;
            this.tabStartupExit.Text = "Startup/Exit";
            this.tabStartupExit.UseVisualStyleBackColor = true;
            // 
            // tabAppearance
            // 
            this.tabAppearance.Controls.Add(this.lblLanguageRestartRequired);
            this.tabAppearance.Controls.Add(this.cboLanguage);
            this.tabAppearance.Controls.Add(this.lblLanguage);
            this.tabAppearance.Controls.Add(this.chkShowFullConnectionsFilePathInTitle);
            this.tabAppearance.Controls.Add(this.chkShowDescriptionTooltipsInTree);
            this.tabAppearance.Controls.Add(this.chkShowSystemTrayIcon);
            this.tabAppearance.Controls.Add(this.chkMinimizeToSystemTray);
            this.tabAppearance.Location = new System.Drawing.Point(4, 22);
            this.tabAppearance.Name = "tabAppearance";
            this.tabAppearance.Size = new System.Drawing.Size(602, 463);
            this.tabAppearance.TabIndex = 1;
            this.tabAppearance.Text = "Appearance";
            this.tabAppearance.UseVisualStyleBackColor = true;
            // 
            // tabTabsAndPanels
            // 
            this.tabTabsAndPanels.Controls.Add(this.chkUseOnlyErrorsAndInfosPanel);
            this.tabTabsAndPanels.Controls.Add(this.chkOpenNewTabRightOfSelected);
            this.tabTabsAndPanels.Controls.Add(this.lblSwitchToErrorsAndInfos);
            this.tabTabsAndPanels.Controls.Add(this.chkAlwaysShowPanelSelectionDlg);
            this.tabTabsAndPanels.Controls.Add(this.chkMCInformation);
            this.tabTabsAndPanels.Controls.Add(this.chkShowLogonInfoOnTabs);
            this.tabTabsAndPanels.Controls.Add(this.chkMCErrors);
            this.tabTabsAndPanels.Controls.Add(this.chkDoubleClickClosesTab);
            this.tabTabsAndPanels.Controls.Add(this.chkMCWarnings);
            this.tabTabsAndPanels.Controls.Add(this.chkShowProtocolOnTabs);
            this.tabTabsAndPanels.Location = new System.Drawing.Point(4, 22);
            this.tabTabsAndPanels.Name = "tabTabsAndPanels";
            this.tabTabsAndPanels.Size = new System.Drawing.Size(602, 463);
            this.tabTabsAndPanels.TabIndex = 2;
            this.tabTabsAndPanels.Text = "Tabs & Panels";
            this.tabTabsAndPanels.UseVisualStyleBackColor = true;
            // 
            // tabConnections
            // 
            this.tabConnections.Controls.Add(this.chkDoubleClickOpensNewConnection);
            this.tabConnections.Controls.Add(this.pnlRdpReconnectionCount);
            this.tabConnections.Controls.Add(this.chkSingleClickOnConnectionOpensIt);
            this.tabConnections.Controls.Add(this.chkHostnameLikeDisplayName);
            this.tabConnections.Controls.Add(this.pnlDefaultCredentials);
            this.tabConnections.Controls.Add(this.chkSingleClickOnOpenedConnectionSwitchesToIt);
            this.tabConnections.Controls.Add(this.pnlAutoSave);
            this.tabConnections.Location = new System.Drawing.Point(4, 22);
            this.tabConnections.Name = "tabConnections";
            this.tabConnections.Size = new System.Drawing.Size(602, 463);
            this.tabConnections.TabIndex = 3;
            this.tabConnections.Text = "Connections";
            this.tabConnections.UseVisualStyleBackColor = true;
            // 
            // chkDoubleClickOpensNewConnection
            // 
            this.chkDoubleClickOpensNewConnection.AutoSize = true;
            this.chkDoubleClickOpensNewConnection.Location = new System.Drawing.Point(3, 26);
            this.chkDoubleClickOpensNewConnection.Name = "chkDoubleClickOpensNewConnection";
            this.chkDoubleClickOpensNewConnection.Size = new System.Drawing.Size(267, 17);
            this.chkDoubleClickOpensNewConnection.TabIndex = 6;
            this.chkDoubleClickOpensNewConnection.Text = "Double click on connection opens new connection";
            this.chkDoubleClickOpensNewConnection.UseVisualStyleBackColor = true;
            // 
            // pnlDefaultCredentials
            // 
            this.pnlDefaultCredentials.Controls.Add(this.radCredentialsCustom);
            this.pnlDefaultCredentials.Controls.Add(this.lblDefaultCredentials);
            this.pnlDefaultCredentials.Controls.Add(this.radCredentialsNoInfo);
            this.pnlDefaultCredentials.Controls.Add(this.radCredentialsWindows);
            this.pnlDefaultCredentials.Controls.Add(this.txtCredentialsDomain);
            this.pnlDefaultCredentials.Controls.Add(this.lblCredentialsUsername);
            this.pnlDefaultCredentials.Controls.Add(this.txtCredentialsPassword);
            this.pnlDefaultCredentials.Controls.Add(this.lblCredentialsPassword);
            this.pnlDefaultCredentials.Controls.Add(this.txtCredentialsUsername);
            this.pnlDefaultCredentials.Controls.Add(this.lblCredentialsDomain);
            this.pnlDefaultCredentials.Location = new System.Drawing.Point(3, 165);
            this.pnlDefaultCredentials.Name = "pnlDefaultCredentials";
            this.pnlDefaultCredentials.Size = new System.Drawing.Size(596, 175);
            this.pnlDefaultCredentials.TabIndex = 5;
            // 
            // radCredentialsCustom
            // 
            this.radCredentialsCustom.AutoSize = true;
            this.radCredentialsCustom.Location = new System.Drawing.Point(16, 69);
            this.radCredentialsCustom.Name = "radCredentialsCustom";
            this.radCredentialsCustom.Size = new System.Drawing.Size(87, 17);
            this.radCredentialsCustom.TabIndex = 3;
            this.radCredentialsCustom.Text = "the following:";
            this.radCredentialsCustom.UseVisualStyleBackColor = true;
            this.radCredentialsCustom.CheckedChanged += new System.EventHandler(this.radCredentialsCustom_CheckedChanged);
            // 
            // lblDefaultCredentials
            // 
            this.lblDefaultCredentials.AutoSize = true;
            this.lblDefaultCredentials.Location = new System.Drawing.Point(3, 9);
            this.lblDefaultCredentials.Name = "lblDefaultCredentials";
            this.lblDefaultCredentials.Size = new System.Drawing.Size(257, 13);
            this.lblDefaultCredentials.TabIndex = 0;
            this.lblDefaultCredentials.Text = "For empty Username, Password or Domain fields use:";
            // 
            // radCredentialsNoInfo
            // 
            this.radCredentialsNoInfo.AutoSize = true;
            this.radCredentialsNoInfo.Checked = true;
            this.radCredentialsNoInfo.Location = new System.Drawing.Point(16, 31);
            this.radCredentialsNoInfo.Name = "radCredentialsNoInfo";
            this.radCredentialsNoInfo.Size = new System.Drawing.Size(91, 17);
            this.radCredentialsNoInfo.TabIndex = 1;
            this.radCredentialsNoInfo.TabStop = true;
            this.radCredentialsNoInfo.Text = "no information";
            this.radCredentialsNoInfo.UseVisualStyleBackColor = true;
            // 
            // radCredentialsWindows
            // 
            this.radCredentialsWindows.AutoSize = true;
            this.radCredentialsWindows.Location = new System.Drawing.Point(16, 50);
            this.radCredentialsWindows.Name = "radCredentialsWindows";
            this.radCredentialsWindows.Size = new System.Drawing.Size(227, 17);
            this.radCredentialsWindows.TabIndex = 2;
            this.radCredentialsWindows.Text = "my current credentials (windows logon info)";
            this.radCredentialsWindows.UseVisualStyleBackColor = true;
            // 
            // txtCredentialsDomain
            // 
            this.txtCredentialsDomain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCredentialsDomain.Enabled = false;
            this.txtCredentialsDomain.Location = new System.Drawing.Point(140, 147);
            this.txtCredentialsDomain.Name = "txtCredentialsDomain";
            this.txtCredentialsDomain.Size = new System.Drawing.Size(150, 20);
            this.txtCredentialsDomain.TabIndex = 9;
            // 
            // lblCredentialsUsername
            // 
            this.lblCredentialsUsername.Enabled = false;
            this.lblCredentialsUsername.Location = new System.Drawing.Point(37, 95);
            this.lblCredentialsUsername.Name = "lblCredentialsUsername";
            this.lblCredentialsUsername.Size = new System.Drawing.Size(97, 13);
            this.lblCredentialsUsername.TabIndex = 4;
            this.lblCredentialsUsername.Text = "Username:";
            this.lblCredentialsUsername.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtCredentialsPassword
            // 
            this.txtCredentialsPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCredentialsPassword.Enabled = false;
            this.txtCredentialsPassword.Location = new System.Drawing.Point(140, 120);
            this.txtCredentialsPassword.Name = "txtCredentialsPassword";
            this.txtCredentialsPassword.Size = new System.Drawing.Size(150, 20);
            this.txtCredentialsPassword.TabIndex = 7;
            this.txtCredentialsPassword.UseSystemPasswordChar = true;
            // 
            // lblCredentialsPassword
            // 
            this.lblCredentialsPassword.Enabled = false;
            this.lblCredentialsPassword.Location = new System.Drawing.Point(34, 123);
            this.lblCredentialsPassword.Name = "lblCredentialsPassword";
            this.lblCredentialsPassword.Size = new System.Drawing.Size(100, 13);
            this.lblCredentialsPassword.TabIndex = 6;
            this.lblCredentialsPassword.Text = "Password:";
            this.lblCredentialsPassword.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtCredentialsUsername
            // 
            this.txtCredentialsUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCredentialsUsername.Enabled = false;
            this.txtCredentialsUsername.Location = new System.Drawing.Point(140, 93);
            this.txtCredentialsUsername.Name = "txtCredentialsUsername";
            this.txtCredentialsUsername.Size = new System.Drawing.Size(150, 20);
            this.txtCredentialsUsername.TabIndex = 5;
            // 
            // lblCredentialsDomain
            // 
            this.lblCredentialsDomain.Enabled = false;
            this.lblCredentialsDomain.Location = new System.Drawing.Point(34, 150);
            this.lblCredentialsDomain.Name = "lblCredentialsDomain";
            this.lblCredentialsDomain.Size = new System.Drawing.Size(100, 13);
            this.lblCredentialsDomain.TabIndex = 8;
            this.lblCredentialsDomain.Text = "Domain:";
            this.lblCredentialsDomain.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tabSQLServer
            // 
            this.tabSQLServer.Controls.Add(this.lblSQLDatabaseName);
            this.tabSQLServer.Controls.Add(this.txtSQLDatabaseName);
            this.tabSQLServer.Controls.Add(this.lblExperimental);
            this.tabSQLServer.Controls.Add(this.chkUseSQLServer);
            this.tabSQLServer.Controls.Add(this.lblSQLUsername);
            this.tabSQLServer.Controls.Add(this.txtSQLPassword);
            this.tabSQLServer.Controls.Add(this.lblSQLInfo);
            this.tabSQLServer.Controls.Add(this.lblSQLServer);
            this.tabSQLServer.Controls.Add(this.txtSQLUsername);
            this.tabSQLServer.Controls.Add(this.txtSQLServer);
            this.tabSQLServer.Controls.Add(this.lblSQLPassword);
            this.tabSQLServer.Location = new System.Drawing.Point(4, 22);
            this.tabSQLServer.Name = "tabSQLServer";
            this.tabSQLServer.Size = new System.Drawing.Size(602, 463);
            this.tabSQLServer.TabIndex = 6;
            this.tabSQLServer.Text = "SQL Server";
            this.tabSQLServer.UseVisualStyleBackColor = true;
            // 
            // lblSQLDatabaseName
            // 
            this.lblSQLDatabaseName.Enabled = false;
            this.lblSQLDatabaseName.Location = new System.Drawing.Point(23, 132);
            this.lblSQLDatabaseName.Name = "lblSQLDatabaseName";
            this.lblSQLDatabaseName.Size = new System.Drawing.Size(111, 13);
            this.lblSQLDatabaseName.TabIndex = 5;
            this.lblSQLDatabaseName.Text = "Database:";
            this.lblSQLDatabaseName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtSQLDatabaseName
            // 
            this.txtSQLDatabaseName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSQLDatabaseName.Enabled = false;
            this.txtSQLDatabaseName.Location = new System.Drawing.Point(140, 130);
            this.txtSQLDatabaseName.Name = "txtSQLDatabaseName";
            this.txtSQLDatabaseName.Size = new System.Drawing.Size(153, 20);
            this.txtSQLDatabaseName.TabIndex = 6;
            // 
            // lblExperimental
            // 
            this.lblExperimental.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExperimental.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
            this.lblExperimental.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblExperimental.Location = new System.Drawing.Point(3, 0);
            this.lblExperimental.Name = "lblExperimental";
            this.lblExperimental.Size = new System.Drawing.Size(596, 25);
            this.lblExperimental.TabIndex = 0;
            this.lblExperimental.Text = "EXPERIMENTAL";
            this.lblExperimental.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkUseSQLServer
            // 
            this.chkUseSQLServer.AutoSize = true;
            this.chkUseSQLServer.Location = new System.Drawing.Point(3, 76);
            this.chkUseSQLServer.Name = "chkUseSQLServer";
            this.chkUseSQLServer.Size = new System.Drawing.Size(234, 17);
            this.chkUseSQLServer.TabIndex = 2;
            this.chkUseSQLServer.Text = "Use SQL Server to load && save connections";
            this.chkUseSQLServer.UseVisualStyleBackColor = true;
            this.chkUseSQLServer.CheckedChanged += new System.EventHandler(this.chkUseSQLServer_CheckedChanged);
            // 
            // lblSQLUsername
            // 
            this.lblSQLUsername.Enabled = false;
            this.lblSQLUsername.Location = new System.Drawing.Point(23, 158);
            this.lblSQLUsername.Name = "lblSQLUsername";
            this.lblSQLUsername.Size = new System.Drawing.Size(111, 13);
            this.lblSQLUsername.TabIndex = 7;
            this.lblSQLUsername.Text = "Username:";
            this.lblSQLUsername.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtSQLPassword
            // 
            this.txtSQLPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSQLPassword.Enabled = false;
            this.txtSQLPassword.Location = new System.Drawing.Point(140, 182);
            this.txtSQLPassword.Name = "txtSQLPassword";
            this.txtSQLPassword.Size = new System.Drawing.Size(153, 20);
            this.txtSQLPassword.TabIndex = 10;
            this.txtSQLPassword.UseSystemPasswordChar = true;
            // 
            // lblSQLInfo
            // 
            this.lblSQLInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSQLInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
            this.lblSQLInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblSQLInfo.Location = new System.Drawing.Point(3, 25);
            this.lblSQLInfo.Name = "lblSQLInfo";
            this.lblSQLInfo.Size = new System.Drawing.Size(596, 25);
            this.lblSQLInfo.TabIndex = 1;
            this.lblSQLInfo.Text = "Please see Help - Getting started - SQL Configuration for more Info!";
            this.lblSQLInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSQLServer
            // 
            this.lblSQLServer.Enabled = false;
            this.lblSQLServer.Location = new System.Drawing.Point(23, 106);
            this.lblSQLServer.Name = "lblSQLServer";
            this.lblSQLServer.Size = new System.Drawing.Size(111, 13);
            this.lblSQLServer.TabIndex = 3;
            this.lblSQLServer.Text = "SQL Server:";
            this.lblSQLServer.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtSQLUsername
            // 
            this.txtSQLUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSQLUsername.Enabled = false;
            this.txtSQLUsername.Location = new System.Drawing.Point(140, 156);
            this.txtSQLUsername.Name = "txtSQLUsername";
            this.txtSQLUsername.Size = new System.Drawing.Size(153, 20);
            this.txtSQLUsername.TabIndex = 8;
            // 
            // txtSQLServer
            // 
            this.txtSQLServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSQLServer.Enabled = false;
            this.txtSQLServer.Location = new System.Drawing.Point(140, 103);
            this.txtSQLServer.Name = "txtSQLServer";
            this.txtSQLServer.Size = new System.Drawing.Size(153, 20);
            this.txtSQLServer.TabIndex = 4;
            // 
            // lblSQLPassword
            // 
            this.lblSQLPassword.Enabled = false;
            this.lblSQLPassword.Location = new System.Drawing.Point(23, 184);
            this.lblSQLPassword.Name = "lblSQLPassword";
            this.lblSQLPassword.Size = new System.Drawing.Size(111, 13);
            this.lblSQLPassword.TabIndex = 9;
            this.lblSQLPassword.Text = "Password:";
            this.lblSQLPassword.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tabUpdates
            // 
            this.tabUpdates.Controls.Add(this.lblUpdatesExplanation);
            this.tabUpdates.Controls.Add(this.pnlUpdateCheck);
            this.tabUpdates.Controls.Add(this.pnlProxy);
            this.tabUpdates.Location = new System.Drawing.Point(4, 22);
            this.tabUpdates.Name = "tabUpdates";
            this.tabUpdates.Size = new System.Drawing.Size(602, 463);
            this.tabUpdates.TabIndex = 4;
            this.tabUpdates.Text = "Updates";
            this.tabUpdates.UseVisualStyleBackColor = true;
            // 
            // tabAdvanced
            // 
            this.tabAdvanced.Controls.Add(this.numUVNCSCPort);
            this.tabAdvanced.Controls.Add(this.lblUVNCSCPort);
            this.tabAdvanced.Controls.Add(this.btnRAminPathBrowse);
            this.tabAdvanced.Controls.Add(this.txtRAdminPath);
            this.tabAdvanced.Controls.Add(this.lblRAdminPath);
            this.tabAdvanced.Controls.Add(this.txtXULrunnerPath);
            this.tabAdvanced.Controls.Add(this.btnTVPathBrowse);
            this.tabAdvanced.Controls.Add(this.txtTVPath);
            this.tabAdvanced.Controls.Add(this.lblTVPath);
            this.tabAdvanced.Controls.Add(this.chkWriteLogFile);
            this.tabAdvanced.Controls.Add(this.chkAutomaticallyGetSessionInfo);
            this.tabAdvanced.Controls.Add(this.lblXulRunnerPath);
            this.tabAdvanced.Controls.Add(this.lblMaximumPuttyWaitTime);
            this.tabAdvanced.Controls.Add(this.chkEncryptCompleteFile);
            this.tabAdvanced.Controls.Add(this.chkAutomaticReconnect);
            this.tabAdvanced.Controls.Add(this.btnBrowseXulRunnerPath);
            this.tabAdvanced.Controls.Add(this.numPuttyWaitTime);
            this.tabAdvanced.Controls.Add(this.chkUseCustomPuttyPath);
            this.tabAdvanced.Controls.Add(this.lblConfigurePuttySessions);
            this.tabAdvanced.Controls.Add(this.txtCustomPuttyPath);
            this.tabAdvanced.Controls.Add(this.btnLaunchPutty);
            this.tabAdvanced.Controls.Add(this.lblSeconds);
            this.tabAdvanced.Controls.Add(this.btnBrowseCustomPuttyPath);
            this.tabAdvanced.Location = new System.Drawing.Point(4, 22);
            this.tabAdvanced.Name = "tabAdvanced";
            this.tabAdvanced.Size = new System.Drawing.Size(602, 463);
            this.tabAdvanced.TabIndex = 5;
            this.tabAdvanced.Text = "Advanced";
            this.tabAdvanced.UseVisualStyleBackColor = true;
            // 
            // numUVNCSCPort
            // 
            this.numUVNCSCPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numUVNCSCPort.Location = new System.Drawing.Point(373, 218);
            this.numUVNCSCPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numUVNCSCPort.Name = "numUVNCSCPort";
            this.numUVNCSCPort.Size = new System.Drawing.Size(72, 20);
            this.numUVNCSCPort.TabIndex = 26;
            this.numUVNCSCPort.Value = new decimal(new int[] {
            5500,
            0,
            0,
            0});
            // 
            // lblUVNCSCPort
            // 
            this.lblUVNCSCPort.Location = new System.Drawing.Point(3, 220);
            this.lblUVNCSCPort.Name = "lblUVNCSCPort";
            this.lblUVNCSCPort.Size = new System.Drawing.Size(364, 13);
            this.lblUVNCSCPort.TabIndex = 25;
            this.lblUVNCSCPort.Text = "UltraVNC SingleClick Listening Port:";
            this.lblUVNCSCPort.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnRAminPathBrowse
            // 
            this.btnRAminPathBrowse.Location = new System.Drawing.Point(373, 420);
            this.btnRAminPathBrowse.Name = "btnRAminPathBrowse";
            this.btnRAminPathBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnRAminPathBrowse.TabIndex = 24;
            this.btnRAminPathBrowse.Text = "Browse...";
            this.btnRAminPathBrowse.UseVisualStyleBackColor = true;
            this.btnRAminPathBrowse.Click += new System.EventHandler(this.btnRAminPathBrowse_Click);
            // 
            // txtRAdminPath
            // 
            this.txtRAdminPath.FormattingEnabled = true;
            this.txtRAdminPath.Location = new System.Drawing.Point(21, 422);
            this.txtRAdminPath.Name = "txtRAdminPath";
            this.txtRAdminPath.Size = new System.Drawing.Size(346, 21);
            this.txtRAdminPath.TabIndex = 23;
            // 
            // lblRAdminPath
            // 
            this.lblRAdminPath.AutoSize = true;
            this.lblRAdminPath.Location = new System.Drawing.Point(3, 392);
            this.lblRAdminPath.Name = "lblRAdminPath";
            this.lblRAdminPath.Size = new System.Drawing.Size(72, 13);
            this.lblRAdminPath.TabIndex = 22;
            this.lblRAdminPath.Text = "RAdmin Path:";
            // 
            // txtXULrunnerPath
            // 
            this.txtXULrunnerPath.FormattingEnabled = true;
            this.txtXULrunnerPath.Location = new System.Drawing.Point(21, 274);
            this.txtXULrunnerPath.Name = "txtXULrunnerPath";
            this.txtXULrunnerPath.Size = new System.Drawing.Size(346, 21);
            this.txtXULrunnerPath.TabIndex = 20;
            // 
            // btnTVPathBrowse
            // 
            this.btnTVPathBrowse.Location = new System.Drawing.Point(373, 344);
            this.btnTVPathBrowse.Name = "btnTVPathBrowse";
            this.btnTVPathBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnTVPathBrowse.TabIndex = 19;
            this.btnTVPathBrowse.Text = "Browse...";
            this.btnTVPathBrowse.UseVisualStyleBackColor = true;
            this.btnTVPathBrowse.Click += new System.EventHandler(this.btnTVPathBrowse_Click);
            // 
            // txtTVPath
            // 
            this.txtTVPath.FormattingEnabled = true;
            this.txtTVPath.Location = new System.Drawing.Point(21, 346);
            this.txtTVPath.Name = "txtTVPath";
            this.txtTVPath.Size = new System.Drawing.Size(346, 21);
            this.txtTVPath.TabIndex = 18;
            // 
            // lblTVPath
            // 
            this.lblTVPath.AutoSize = true;
            this.lblTVPath.Location = new System.Drawing.Point(3, 316);
            this.lblTVPath.Name = "lblTVPath";
            this.lblTVPath.Size = new System.Drawing.Size(97, 13);
            this.lblTVPath.TabIndex = 17;
            this.lblTVPath.Text = "Team Viewer Path:";
            // 
            // frmOptions
            // 
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(794, 542);
            this.Controls.Add(this.tcTabControl);
            this.Controls.Add(this.lvPages);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = global::My.Resources.Resources.Options_Icon;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOptions";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.Options_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numPuttyWaitTime)).EndInit();
            this.pnlProxy.ResumeLayout(false);
            this.pnlProxy.PerformLayout();
            this.pnlProxyBasic.ResumeLayout(false);
            this.pnlProxyBasic.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numProxyPort)).EndInit();
            this.pnlProxyAuthentication.ResumeLayout(false);
            this.pnlProxyAuthentication.PerformLayout();
            this.pnlUpdateCheck.ResumeLayout(false);
            this.pnlUpdateCheck.PerformLayout();
            this.pnlAutoSave.ResumeLayout(false);
            this.pnlAutoSave.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAutoSave)).EndInit();
            this.pnlRdpReconnectionCount.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numRdpReconnectionCount)).EndInit();
            this.tcTabControl.ResumeLayout(false);
            this.tabStartupExit.ResumeLayout(false);
            this.tabStartupExit.PerformLayout();
            this.tabAppearance.ResumeLayout(false);
            this.tabAppearance.PerformLayout();
            this.tabTabsAndPanels.ResumeLayout(false);
            this.tabTabsAndPanels.PerformLayout();
            this.tabConnections.ResumeLayout(false);
            this.tabConnections.PerformLayout();
            this.pnlDefaultCredentials.ResumeLayout(false);
            this.pnlDefaultCredentials.PerformLayout();
            this.tabSQLServer.ResumeLayout(false);
            this.tabSQLServer.PerformLayout();
            this.tabUpdates.ResumeLayout(false);
            this.tabAdvanced.ResumeLayout(false);
            this.tabAdvanced.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUVNCSCPort)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion Form Init

        #region Private Methods

        private void UpdatePaths()
        {
            txtTVPath.DropDownStyle = ComboBoxStyle.DropDown;
            txtTVPath.Items.AddRange(Misc.FindTvPaths().ToArray());
            txtXULrunnerPath.DropDownStyle = ComboBoxStyle.DropDown;
            txtXULrunnerPath.Items.AddRange(Misc.FindGeckoPaths().ToArray());
            txtRAdminPath.DropDownStyle = ComboBoxStyle.DropDown;
            txtRAdminPath.Items.AddRange(Misc.FindRAdminPaths().ToArray());
        }

        private void LoadOptions()
        {
            try
            {
                UpdatePaths();
                Settings.Default.Reload();
                txtRAdminPath.Text = Settings.Default.RAdminPath;
                txtTVPath.Text = Settings.Default.TeamViewerPath;
                chkDoubleClickOpensNewConnection.Checked = System.Convert.ToBoolean(Settings.Default.DoubleClickStartsNewConnection);
                this.chkSaveConsOnExit.Checked = System.Convert.ToBoolean(Settings.Default.SaveConsOnExit);
                this.chkConfirmCloseConnection.Checked =
                    System.Convert.ToBoolean(Settings.Default.ConfirmCloseConnection);
                this.chkConfirmExit.Checked = System.Convert.ToBoolean(Settings.Default.ConfirmExit);
                this.chkReconnectOnStart.Checked = System.Convert.ToBoolean(Settings.Default.OpenConsFromLastSession);
                this.chkProperInstallationOfComponentsAtStartup.Checked =
                    System.Convert.ToBoolean(Settings.Default.StartupComponentsCheck);

                this.cboLanguage.Items.Clear();
                this.cboLanguage.Items.Add(Language.strLanguageDefault);

                foreach (string CultureNativeName in mRemoteNC.SupportedCultures.CultureNativeNames)
                {
                    this.cboLanguage.Items.Add(CultureNativeName);
                }
                if (Settings.Default.OverrideUICulture != "" &&
                    mRemoteNC.SupportedCultures.IsNameSupported((string)Settings.Default.OverrideUICulture))
                {
                    this.cboLanguage.SelectedItem =
                        mRemoteNC.SupportedCultures.CultureNativeName((string)Settings.Default.OverrideUICulture);
                }
                if (this.cboLanguage.SelectedIndex == -1)
                {
                    this.cboLanguage.SelectedIndex = 0;
                }

                this.chkShowDescriptionTooltipsInTree.Checked =
                    System.Convert.ToBoolean(Settings.Default.ShowDescriptionTooltipsInTree);
                this.chkShowSystemTrayIcon.Checked = System.Convert.ToBoolean(Settings.Default.ShowSystemTrayIcon);
                this.chkMinimizeToSystemTray.Checked = System.Convert.ToBoolean(Settings.Default.MinimizeToTray);

                this.chkOpenNewTabRightOfSelected.Checked =
                    System.Convert.ToBoolean(Settings.Default.OpenTabsRightOfSelected);
                this.chkShowLogonInfoOnTabs.Checked = System.Convert.ToBoolean(Settings.Default.ShowLogonInfoOnTabs);
                this.chkShowProtocolOnTabs.Checked = System.Convert.ToBoolean(Settings.Default.ShowProtocolOnTabs);
                this.chkShowFullConnectionsFilePathInTitle.Checked =
                    System.Convert.ToBoolean(Settings.Default.ShowCompleteConsPathInTitle);
                this.chkDoubleClickClosesTab.Checked =
                    System.Convert.ToBoolean(Settings.Default.DoubleClickOnTabClosesIt);
                this.chkAlwaysShowPanelSelectionDlg.Checked =
                    System.Convert.ToBoolean(Settings.Default.AlwaysShowPanelSelectionDlg);

                this.chkSingleClickOnConnectionOpensIt.Checked =
                    System.Convert.ToBoolean(Settings.Default.SingleClickOnConnectionOpensIt);
                this.chkSingleClickOnOpenedConnectionSwitchesToIt.Checked =
                    System.Convert.ToBoolean(Settings.Default.SingleClickSwitchesToOpenConnection);
                this.chkHostnameLikeDisplayName.Checked =
                    System.Convert.ToBoolean(Settings.Default.SetHostnameLikeDisplayName);
                this.numRdpReconnectionCount.Value = System.Convert.ToDecimal(Settings.Default.RdpReconnectionCount);
                this.numAutoSave.Value = System.Convert.ToDecimal(Settings.Default.AutoSaveEveryMinutes);

                this.chkUseSQLServer.Checked = System.Convert.ToBoolean(Settings.Default.UseSQLServer);
                this.txtSQLServer.Text = (string)Settings.Default.SQLHost;
                this.txtSQLDatabaseName.Text = (string)Settings.Default.SQLDatabaseName;
                this.txtSQLUsername.Text = (string)Settings.Default.SQLUser;
                this.txtSQLPassword.Text = Crypt.Decrypt((string)Settings.Default.SQLPass,
                                                         (string)General.EncryptionKey);

                if ((string)Settings.Default.EmptyCredentials == "noinfo")
                {
                    this.radCredentialsNoInfo.Checked = true;
                }
                else if ((string)Settings.Default.EmptyCredentials == "windows")
                {
                    this.radCredentialsWindows.Checked = true;
                }
                else if ((string)Settings.Default.EmptyCredentials == "custom")
                {
                    this.radCredentialsCustom.Checked = true;
                }

                this.txtCredentialsUsername.Text = (string)Settings.Default.DefaultUsername;
                this.txtCredentialsPassword.Text = Crypt.Decrypt((string)Settings.Default.DefaultPassword,
                                                                 (string)General.EncryptionKey);
                this.txtCredentialsDomain.Text = (string)Settings.Default.DefaultDomain;

                this.chkUseOnlyErrorsAndInfosPanel.Checked =
                    System.Convert.ToBoolean(Settings.Default.ShowNoMessageBoxes);
                this.chkMCInformation.Checked = System.Convert.ToBoolean(Settings.Default.SwitchToMCOnInformation);
                this.chkMCWarnings.Checked = System.Convert.ToBoolean(Settings.Default.SwitchToMCOnWarning);
                this.chkMCErrors.Checked = System.Convert.ToBoolean(Settings.Default.SwitchToMCOnError);

                chkCheckForUpdatesOnStartup.Checked = System.Convert.ToBoolean(Settings.Default.CheckForUpdatesOnStartup);
                cboUpdateCheckFrequency.Enabled = chkCheckForUpdatesOnStartup.Checked;
                cboUpdateCheckFrequency.Items.Clear();
                int nDaily = cboUpdateCheckFrequency.Items.Add(Language.strUpdateFrequencyDaily);
                int nWeekly = cboUpdateCheckFrequency.Items.Add(Language.strUpdateFrequencyWeekly);
                int nMonthly = cboUpdateCheckFrequency.Items.Add(Language.strUpdateFrequencyMonthly);
                if (Settings.Default.CheckForUpdatesFrequencyDays < 1)
                {
                    chkCheckForUpdatesOnStartup.Checked = false;
                    cboUpdateCheckFrequency.SelectedIndex = nDaily;
                } // Daily
                else if (Settings.Default.CheckForUpdatesFrequencyDays == 1)
                {
                    cboUpdateCheckFrequency.SelectedIndex = nDaily;
                } // Weekly
                else if (Settings.Default.CheckForUpdatesFrequencyDays == 7)
                {
                    cboUpdateCheckFrequency.SelectedIndex = nWeekly;
                } // Monthly
                else if (Settings.Default.CheckForUpdatesFrequencyDays == 31)
                {
                    cboUpdateCheckFrequency.SelectedIndex = nMonthly;
                }
                else
                {
                    int nCustom =
                        cboUpdateCheckFrequency.Items.Add(string.Format(Language.strUpdateFrequencyCustom,
                                                                        Settings.Default.CheckForUpdatesFrequencyDays));
                    cboUpdateCheckFrequency.SelectedIndex = nCustom;
                }

                this.chkWriteLogFile.Checked = System.Convert.ToBoolean(Settings.Default.WriteLogFile);
                this.chkEncryptCompleteFile.Checked =
                    System.Convert.ToBoolean(Settings.Default.EncryptCompleteConnectionsFile);
                this.chkAutomaticallyGetSessionInfo.Checked =
                    System.Convert.ToBoolean(Settings.Default.AutomaticallyGetSessionInfo);
                this.chkAutomaticReconnect.Checked = System.Convert.ToBoolean(Settings.Default.ReconnectOnDisconnect);
                this.chkSingleInstance.Checked = System.Convert.ToBoolean(Settings.Default.SingleInstance);
                this.chkUseCustomPuttyPath.Checked = System.Convert.ToBoolean(Settings.Default.UseCustomPuttyPath);
                this.txtCustomPuttyPath.Text = (string)Settings.Default.CustomPuttyPath;
                this.numPuttyWaitTime.Value = System.Convert.ToDecimal(Settings.Default.MaxPuttyWaitTime);

                this.chkUseProxyForAutomaticUpdates.Checked = System.Convert.ToBoolean(Settings.Default.UpdateUseProxy);
                this.btnTestProxy.Enabled = System.Convert.ToBoolean(Settings.Default.UpdateUseProxy);
                this.pnlProxyBasic.Enabled = System.Convert.ToBoolean(Settings.Default.UpdateUseProxy);

                this.txtProxyAddress.Text = (string)Settings.Default.UpdateProxyAddress;
                this.numProxyPort.Value = System.Convert.ToDecimal(Settings.Default.UpdateProxyPort);

                this.chkUseProxyAuthentication.Checked =
                    System.Convert.ToBoolean(Settings.Default.UpdateProxyUseAuthentication);
                this.pnlProxyAuthentication.Enabled =
                    System.Convert.ToBoolean(Settings.Default.UpdateProxyUseAuthentication);

                this.txtProxyUsername.Text = (string)Settings.Default.UpdateProxyAuthUser;
                this.txtProxyPassword.Text = Security.Crypt.Decrypt((string)Settings.Default.UpdateProxyAuthPass,
                                                                    (string)General.EncryptionKey);

                this.numUVNCSCPort.Value = System.Convert.ToDecimal(Settings.Default.UVNCSCPort);

                this.txtXULrunnerPath.Text = (string)Settings.Default.XULRunnerPath;
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    (string)
                                                    ("LoadOptions (UI.Window.Options) failed" + Constants.vbNewLine +
                                                     ex.Message), true);
            }
        }

        private void SaveOptions()
        {
            try
            {
                Settings.Default.RAdminPath = txtRAdminPath.Text;
                Settings.Default.TeamViewerPath = txtTVPath.Text;
                Settings.Default.DoubleClickStartsNewConnection = chkDoubleClickOpensNewConnection.Checked;
                Settings.Default.SaveConsOnExit = this.chkSaveConsOnExit.Checked;
                Settings.Default.ConfirmCloseConnection = this.chkConfirmCloseConnection.Checked;
                Settings.Default.ConfirmExit = this.chkConfirmExit.Checked;
                Settings.Default.OpenConsFromLastSession = this.chkReconnectOnStart.Checked;
                Settings.Default.StartupComponentsCheck = this.chkProperInstallationOfComponentsAtStartup.Checked;

                if (this.cboLanguage.SelectedIndex > 0 &&
                    mRemoteNC.SupportedCultures.IsNativeNameSupported((string)this.cboLanguage.SelectedItem))
                {
                    Settings.Default.OverrideUICulture =
                        mRemoteNC.SupportedCultures.CultureName((string)this.cboLanguage.SelectedItem);
                }
                else
                {
                    Settings.Default.OverrideUICulture = "";
                }

                Settings.Default.ShowDescriptionTooltipsInTree = this.chkShowDescriptionTooltipsInTree.Checked;
                Settings.Default.ShowSystemTrayIcon = this.chkShowSystemTrayIcon.Checked;
                Settings.Default.MinimizeToTray = this.chkMinimizeToSystemTray.Checked;

                if (Settings.Default.ShowSystemTrayIcon)
                {
                    if (Runtime.NotificationAreaIcon == null)
                    {
                        Runtime.NotificationAreaIcon = new Tools.Controls.NotificationAreaIcon();
                    }
                }
                else
                {
                    if (Runtime.NotificationAreaIcon != null)
                    {
                        Runtime.NotificationAreaIcon.Dispose();
                        Runtime.NotificationAreaIcon = null;
                    }
                }

                Settings.Default.ShowCompleteConsPathInTitle = this.chkShowFullConnectionsFilePathInTitle.Checked;

                Settings.Default.OpenTabsRightOfSelected = this.chkOpenNewTabRightOfSelected.Checked;
                Settings.Default.ShowLogonInfoOnTabs = this.chkShowLogonInfoOnTabs.Checked;
                Settings.Default.ShowProtocolOnTabs = this.chkShowProtocolOnTabs.Checked;
                Settings.Default.DoubleClickOnTabClosesIt = this.chkDoubleClickClosesTab.Checked;
                Settings.Default.AlwaysShowPanelSelectionDlg = this.chkAlwaysShowPanelSelectionDlg.Checked;

                Settings.Default.SingleClickOnConnectionOpensIt = this.chkSingleClickOnConnectionOpensIt.Checked;
                Settings.Default.SingleClickSwitchesToOpenConnection =
                    this.chkSingleClickOnOpenedConnectionSwitchesToIt.Checked;
                Settings.Default.SetHostnameLikeDisplayName = this.chkHostnameLikeDisplayName.Checked;
                Settings.Default.RdpReconnectionCount = (int)this.numRdpReconnectionCount.Value;
                Settings.Default.AutoSaveEveryMinutes = (int)this.numAutoSave.Value;

                if (Settings.Default.AutoSaveEveryMinutes > 0)
                {
                    frmMain.Default.tmrAutoSave.Interval =
                        System.Convert.ToInt32(Settings.Default.AutoSaveEveryMinutes * 60000);
                    frmMain.Default.tmrAutoSave.Enabled = true;
                }
                else
                {
                    frmMain.Default.tmrAutoSave.Enabled = false;
                }

                Settings.Default.UseSQLServer = this.chkUseSQLServer.Checked;
                Settings.Default.SQLHost = this.txtSQLServer.Text;
                Settings.Default.SQLDatabaseName = this.txtSQLDatabaseName.Text;
                Settings.Default.SQLUser = this.txtSQLUsername.Text;
                Settings.Default.SQLPass = Crypt.Encrypt(this.txtSQLPassword.Text, (string)General.EncryptionKey);

                if (this.radCredentialsNoInfo.Checked)
                {
                    Settings.Default.EmptyCredentials = "noinfo";
                }
                else if (this.radCredentialsWindows.Checked)
                {
                    Settings.Default.EmptyCredentials = "windows";
                }
                else if (this.radCredentialsCustom.Checked)
                {
                    Settings.Default.EmptyCredentials = "custom";
                }

                Settings.Default.DefaultUsername = this.txtCredentialsUsername.Text;
                Settings.Default.DefaultPassword = Crypt.Encrypt(this.txtCredentialsPassword.Text,
                                                                 (string)General.EncryptionKey);
                Settings.Default.DefaultDomain = this.txtCredentialsDomain.Text;

                Settings.Default.ShowNoMessageBoxes = this.chkUseOnlyErrorsAndInfosPanel.Checked;
                Settings.Default.SwitchToMCOnInformation = this.chkMCInformation.Checked;
                Settings.Default.SwitchToMCOnWarning = this.chkMCWarnings.Checked;
                Settings.Default.SwitchToMCOnError = this.chkMCErrors.Checked;

                Settings.Default.CheckForUpdatesOnStartup = chkCheckForUpdatesOnStartup.Checked;
                if (cboUpdateCheckFrequency.SelectedItem.ToString() == Language.strUpdateFrequencyDaily)
                {
                    Settings.Default.CheckForUpdatesFrequencyDays = 1;
                }
                else if (cboUpdateCheckFrequency.SelectedItem.ToString() == Language.strUpdateFrequencyWeekly)
                {
                    Settings.Default.CheckForUpdatesFrequencyDays = 7;
                }
                else if (cboUpdateCheckFrequency.SelectedItem.ToString() == Language.strUpdateFrequencyMonthly)
                {
                    Settings.Default.CheckForUpdatesFrequencyDays = 31;
                }
                else
                {
                }

                Settings.Default.WriteLogFile = this.chkWriteLogFile.Checked;
                Settings.Default.EncryptCompleteConnectionsFile = this.chkEncryptCompleteFile.Checked;
                Settings.Default.AutomaticallyGetSessionInfo = this.chkAutomaticallyGetSessionInfo.Checked;
                Settings.Default.ReconnectOnDisconnect = this.chkAutomaticReconnect.Checked;
                Settings.Default.SingleInstance = this.chkSingleInstance.Checked;
                Settings.Default.UseCustomPuttyPath = this.chkUseCustomPuttyPath.Checked;
                Settings.Default.CustomPuttyPath = this.txtCustomPuttyPath.Text;

                if (Settings.Default.UseCustomPuttyPath)
                {
                    Connection.PuttyBase.PuttyPath = Settings.Default.CustomPuttyPath;
                }
                else
                {
                    mRemoteNC.Connection.PuttyBase.PuttyPath =
                        (new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase()).Info.DirectoryPath +
                        "\\PuTTYNG.exe";
                }

                Settings.Default.MaxPuttyWaitTime = (int)this.numPuttyWaitTime.Value;

                Settings.Default.UpdateUseProxy = this.chkUseProxyForAutomaticUpdates.Checked;

                Settings.Default.UpdateProxyAddress = this.txtProxyAddress.Text;
                Settings.Default.UpdateProxyPort = (int)this.numProxyPort.Value;

                Settings.Default.UpdateProxyUseAuthentication = this.chkUseProxyAuthentication.Checked;

                Settings.Default.UpdateProxyAuthUser = this.txtProxyUsername.Text;
                Settings.Default.UpdateProxyAuthPass = Security.Crypt.Encrypt(this.txtProxyPassword.Text,
                                                                              (string)General.EncryptionKey);

                Settings.Default.UVNCSCPort = (int)this.numUVNCSCPort.Value;

                Settings.Default.XULRunnerPath = this.txtXULrunnerPath.Text;

                if (Settings.Default.LoadConsFromCustomLocation == false)
                {
                    Runtime.SetMainFormText(Connections.DefaultConnectionsPath + "\\" +
                                            Connections.DefaultConnectionsFile);
                }
                else
                {
                    Runtime.SetMainFormText(Settings.Default.CustomConsPath);
                }

                Runtime.Startup.DestroySQLUpdateHandlerAndStopTimer();

                if (Settings.Default.UseSQLServer == true)
                {
                    Runtime.SetMainFormText("SQL Server");
                    Runtime.Startup.CreateSQLUpdateHandlerAndStartTimer();
                }
                Settings.Default.Save();
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    (string)
                                                    ("SaveOptions (UI.Window.Options) failed" + Constants.vbNewLine +
                                                     ex.Message), true);
            }
        }

        #endregion Private Methods

        #region Private Variables

        private int _initialTab = 0;

        #endregion Private Variables

        #region Public Methods

        public frmOptions(DockContent panel)
        {
            this.InitializeComponent();
            Runtime.FontOverride(this);
        }

        #endregion Public Methods

        #region Form Stuff

        private void Options_Load(object sender, System.EventArgs e)
        {
            ApplyLanguage();

            // Hide the tabs
            tcTabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            tcTabControl.Padding = new Point(0, 0);
            tcTabControl.ItemSize = new Size(0, 1);

            // Switch to the _initialTab
            tcTabControl.SelectedIndex = _initialTab;
            lvPages.Items[_initialTab].Selected = true;

#if PORTABLE
            foreach (Control Control in tcTabControl.TabPages[5].Controls)
            {
                if (Control != lblUpdatesExplanation)
                {
                    Control.Visible = false;
                }
            }
#endif
        }

        private void ApplyLanguage()
        {
            lvPages.Items[0].Text = Language.strStartupExit;
            lvPages.Items[1].Text = Language.strTabAppearance;
            lvPages.Items[2].Text = Language.strTabsAndPanels.Replace("&&", "&");
            lvPages.Items[3].Text = Language.strConnections;
            lvPages.Items[4].Text = Language.strSQLServer;
            lvPages.Items[5].Text = Language.strTabUpdates;
            lvPages.Items[6].Text = Language.strTabAdvanced;
#if !PORTABLE
            lblUpdatesExplanation.Text = Language.strUpdateCheck;
#else
            lblUpdatesExplanation.Text = Language.strUpdateCheckPortableEdition;
#endif
            btnTestProxy.Text = Language.strButtonTestProxy;
            lblSeconds.Text = Language.strLabelSeconds;
            lblMaximumPuttyWaitTime.Text = Language.strLabelPuttyTimeout;
            chkAutomaticReconnect.Text = Language.strCheckboxAutomaticReconnect;
            lblProxyAddress.Text = Language.strLabelAddress;
            lblProxyPort.Text = Language.strLabelPort;
            lblProxyUsername.Text = Language.strLabelUsername;
            lblProxyPassword.Text = Language.strLabelPassword;
            chkUseProxyAuthentication.Text = Language.strCheckboxProxyAuthentication;
            chkUseProxyForAutomaticUpdates.Text = Language.strCheckboxUpdateUseProxy;
            lblConfigurePuttySessions.Text = Language.strLabelPuttySessionsConfig;
            btnLaunchPutty.Text = Language.strButtonLaunchPutty;
            btnBrowseCustomPuttyPath.Text = Language.strButtonBrowse;
            chkUseCustomPuttyPath.Text = Language.strCheckboxPuttyPath;
            chkAutomaticallyGetSessionInfo.Text = Language.strAutomaticallyGetSessionInfo;
            chkWriteLogFile.Text = Language.strWriteLogFile;
            chkSingleInstance.Text = Language.strAllowOnlySingleInstance;
            chkReconnectOnStart.Text = Language.strReconnectAtStartup;
            chkCheckForUpdatesOnStartup.Text = Language.strCheckForUpdatesOnStartup;
            chkConfirmCloseConnection.Text = Language.strConfirmCloseConnection;
            chkConfirmExit.Text = Language.strConfirmExit;
            chkSaveConsOnExit.Text = Language.strSaveConsOnExit;
            chkMinimizeToSystemTray.Text = Language.strMinimizeToSysTray;
            chkShowFullConnectionsFilePathInTitle.Text = Language.strShowFullConsFilePath;
            chkShowSystemTrayIcon.Text = Language.strAlwaysShowSysTrayIcon;
            chkShowDescriptionTooltipsInTree.Text = Language.strShowDescriptionTooltips;
            chkShowProtocolOnTabs.Text = Language.strShowProtocolOnTabs;
            chkShowLogonInfoOnTabs.Text = Language.strShowLogonInfoOnTabs;
            chkOpenNewTabRightOfSelected.Text = Language.strOpenNewTabRight;
            chkAlwaysShowPanelSelectionDlg.Text = Language.strAlwaysShowPanelSelection;
            chkDoubleClickClosesTab.Text = Language.strDoubleClickTabClosesIt;
            chkHostnameLikeDisplayName.Text = Language.strSetHostnameLikeDisplayName;
            lblExperimental.Text = Language.strExperimental.ToUpper();
            chkUseSQLServer.Text = Language.strUseSQLServer;
            lblSQLInfo.Text = Language.strSQLInfo;
            lblSQLUsername.Text = Language.strLabelUsername;
            lblSQLServer.Text = Language.strLabelHostname;
            lblSQLDatabaseName.Text = Language.strLabelSQLServerDatabaseName;
            lblSQLPassword.Text = Language.strLabelPassword;
            lblRdpReconnectionCount.Text = Language.strRdpReconnectCount;
            lblAutoSave2.Text = Language.strAutoSaveMins;
            lblAutoSave1.Text = Language.strAutoSaveEvery;
            lblCredentialsDomain.Text = Language.strLabelDomain;
            lblCredentialsPassword.Text = Language.strLabelPassword;
            lblCredentialsUsername.Text = Language.strLabelUsername;
            radCredentialsCustom.Text = Language.strTheFollowing;
            radCredentialsWindows.Text = Language.strMyCurrentWindowsCreds;
            radCredentialsNoInfo.Text = Language.strNoInformation;
            lblDefaultCredentials.Text = Language.strEmptyUsernamePasswordDomainFields;
            chkSingleClickOnOpenedConnectionSwitchesToIt.Text = Language.strSingleClickOnOpenConnectionSwitchesToIt;
            chkSingleClickOnConnectionOpensIt.Text = Language.strSingleClickOnConnectionOpensIt;
            lblSwitchToErrorsAndInfos.Text = Language.strSwitchToErrorsAndInfos;
            chkMCErrors.Text = Language.strErrors;
            chkMCWarnings.Text = Language.strWarnings;
            chkMCInformation.Text = Language.strInformations;
            chkUseOnlyErrorsAndInfosPanel.Text = Language.strUseOnlyErrorsAndInfosPanel;
            btnOK.Text = Language.strButtonOK;
            btnCancel.Text = Language.strButtonCancel;
            btnUpdateCheckNow.Text = Language.strCheckNow;
            Text = Language.strMenuOptions;
            lblUVNCSCPort.Text = Language.strUltraVNCSCListeningPort;
            chkProperInstallationOfComponentsAtStartup.Text = Language.strCheckProperInstallationOfComponentsAtStartup;
            lblXulRunnerPath.Text = Language.strXULrunnerPath;
            btnBrowseXulRunnerPath.Text = Language.strButtonBrowse;
            chkEncryptCompleteFile.Text = Language.strEncryptCompleteConnectionFile;
            lblLanguage.Text = Language.strLanguage;
            lblLanguageRestartRequired.Text = string.Format(Language.strLanguageRestartRequired,
                                                            (new Microsoft.VisualBasic.ApplicationServices.
                                                                WindowsFormsApplicationBase()).Info.ProductName);
            //TODO
            chkDoubleClickOpensNewConnection.Text = Language.strOptionDoubleClickToOpenConnection;
            lblTVPath.Text = Language.strTVPath;
            btnTVPathBrowse.Text = Language.strButtonBrowse;
        }

        public void Show(DockPanel dockPanel, int initialTab = 0)
        {
            Runtime.Windows.optionsForm.LoadOptions();

            _initialTab = initialTab;
            base.ShowDialog(frmMain.defaultInstance);
        }

        private void btnCancel_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(System.Object sender, System.EventArgs e)
        {
            this.SaveOptions();
            this.Close();
        }

        public void lvPages_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            ListView listView = (ListView)sender;
            if (listView.SelectedIndices.Count != 0)
            {
                tcTabControl.SelectedIndex = listView.SelectedIndices[0];
            }
        }

        private void lvPages_MouseUp(System.Object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ListView listView = (ListView)sender;
            if (listView.SelectedIndices.Count == 0)
            {
                listView.Items[tcTabControl.SelectedIndex].Selected = true;
            }
        }

        private void radCredentialsCustom_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            this.lblCredentialsUsername.Enabled = this.radCredentialsCustom.Checked;
            this.lblCredentialsPassword.Enabled = this.radCredentialsCustom.Checked;
            this.lblCredentialsDomain.Enabled = this.radCredentialsCustom.Checked;
            this.txtCredentialsUsername.Enabled = this.radCredentialsCustom.Checked;
            this.txtCredentialsPassword.Enabled = this.radCredentialsCustom.Checked;
            this.txtCredentialsDomain.Enabled = this.radCredentialsCustom.Checked;
        }

        private void chkUseSQLServer_CheckedChanged(object sender, System.EventArgs e)
        {
            this.lblSQLServer.Enabled = chkUseSQLServer.Checked;
            this.lblSQLDatabaseName.Enabled = chkUseSQLServer.Checked;
            this.lblSQLUsername.Enabled = chkUseSQLServer.Checked;
            this.lblSQLPassword.Enabled = chkUseSQLServer.Checked;
            this.txtSQLServer.Enabled = chkUseSQLServer.Checked;
            this.txtSQLDatabaseName.Enabled = chkUseSQLServer.Checked;
            this.txtSQLUsername.Enabled = chkUseSQLServer.Checked;
            this.txtSQLPassword.Enabled = chkUseSQLServer.Checked;
        }

        private void chkUseOnlyErrorsAndInfosPanel_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            this.chkMCInformation.Enabled = this.chkUseOnlyErrorsAndInfosPanel.Checked;
            this.chkMCWarnings.Enabled = this.chkUseOnlyErrorsAndInfosPanel.Checked;
            this.chkMCErrors.Enabled = this.chkUseOnlyErrorsAndInfosPanel.Checked;
        }

        private void chkUseCustomPuttyPath_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            this.txtCustomPuttyPath.Enabled = this.chkUseCustomPuttyPath.Checked;
            this.btnBrowseCustomPuttyPath.Enabled = this.chkUseCustomPuttyPath.Checked;
        }

        private void btnLaunchPutty_Click(System.Object sender, System.EventArgs e)
        {
            mRemoteNC.Connection.PuttyBase.StartPutty();
        }

        private void btnBrowseCustomPuttyPath_Click(System.Object sender, System.EventArgs e)
        {
            OpenFileDialog oDlg = new OpenFileDialog();
            oDlg.Filter = Language.strFilterApplication + "|*.exe|" + Language.strFilterAll + "|*.*";
            oDlg.FileName = "PuTTYNG.exe";
            oDlg.CheckFileExists = true;
            oDlg.Multiselect = false;

            if (oDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtCustomPuttyPath.Text = oDlg.FileName;
            }

            oDlg.Dispose();
        }

        private void btnBrowseXulRunnerPath_Click(System.Object sender, System.EventArgs e)
        {
            FolderBrowserDialog oDlg = new FolderBrowserDialog();
            oDlg.ShowNewFolderButton = false;

            if (oDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtXULrunnerPath.Text = oDlg.SelectedPath;
            }

            oDlg.Dispose();
        }

        private void chkCheckForUpdatesOnStartup_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            cboUpdateCheckFrequency.Enabled = chkCheckForUpdatesOnStartup.Checked;
        }

        private void btnUpdateCheckNow_Click(System.Object sender, System.EventArgs e)
        {
            Runtime.Windows.Show(UI.Window.Type.Update);
        }

        private void chkUseProxyForAutomaticUpdates_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            this.pnlProxyBasic.Enabled = this.chkUseProxyForAutomaticUpdates.Checked;
            this.btnTestProxy.Enabled = this.chkUseProxyForAutomaticUpdates.Checked;

            if (this.chkUseProxyForAutomaticUpdates.Checked)
            {
                this.chkUseProxyAuthentication.Enabled = true;

                if (this.chkUseProxyAuthentication.Checked)
                {
                    this.pnlProxyAuthentication.Enabled = true;
                }
            }
            else
            {
                this.chkUseProxyAuthentication.Enabled = false;
                this.pnlProxyAuthentication.Enabled = false;
            }
        }

        private void btnTestProxy_Click(System.Object sender, System.EventArgs e)
        {
            SaveOptions();
            var ud = new Update();

            if (ud.IsProxyOK())
            {
                Interaction.MsgBox(Language.strProxyTestSucceeded, MsgBoxStyle.Information, null);
            }
            else
            {
                Interaction.MsgBox(Language.strProxyTestFailed, MsgBoxStyle.Exclamation, null);
            }
        }

        private void chkUseProxyAuthentication_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            if (this.chkUseProxyForAutomaticUpdates.Checked)
            {
                if (this.chkUseProxyAuthentication.Checked)
                {
                    this.pnlProxyAuthentication.Enabled = true;
                }
                else
                {
                    this.pnlProxyAuthentication.Enabled = false;
                }
            }
        }

        #endregion Form Stuff

        private void lblProxyPort_Click(System.Object sender, System.EventArgs e)
        {
        }

        private void btnTVPathBrowse_Click(object sender, EventArgs e)
        {
            using (var oDlg = new OpenFileDialog())
            {
                oDlg.Filter = "TeamViewer.exe|*.exe";
                if (oDlg.ShowDialog() == DialogResult.OK)
                {
                    txtTVPath.Text = oDlg.FileName;
                }
            }
        }

        private void btnRAminPathBrowse_Click(object sender, EventArgs e)
        {
            using (var oDlg = new OpenFileDialog())
            {
                oDlg.Filter = "Radmin.exe|*.exe";
                if (oDlg.ShowDialog() == DialogResult.OK)
                {
                    txtRAdminPath.Text = oDlg.FileName;
                }
            }
        }
    }
}