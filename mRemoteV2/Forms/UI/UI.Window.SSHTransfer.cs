using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using EXControls;
using Microsoft.VisualBasic;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using mRemoteNC.App;
using System.Linq;
using My;
using WeifenLuo.WinFormsUI.Docking;

//using mRemoteNC.Runtime;

namespace mRemoteNC
{
    namespace UI
    {
        namespace Window
        {
            public class SSHTransfer : Base
            {
                #region Form Init

                internal System.Windows.Forms.ProgressBar pbStatus;
                internal System.Windows.Forms.Button btnTransfer;
                internal System.Windows.Forms.TextBox txtUser;
                internal System.Windows.Forms.TextBox txtPassword;
                internal System.Windows.Forms.TextBox txtHost;
                internal System.Windows.Forms.TextBox txtPort;
                internal System.Windows.Forms.Label lblHost;
                internal System.Windows.Forms.Label lblPort;
                internal System.Windows.Forms.Label lblUser;
                internal System.Windows.Forms.Label lblPassword;
                internal System.Windows.Forms.Label lblProtocol;
                internal System.Windows.Forms.RadioButton radProtSCP;
                internal System.Windows.Forms.RadioButton radProtSFTP;
                internal System.Windows.Forms.GroupBox grpConnection;
                internal System.Windows.Forms.Button btnBrowse;
                internal System.Windows.Forms.Label lblRemoteFile;
                internal System.Windows.Forms.TextBox txtRemoteFile;
                internal System.Windows.Forms.TextBox txtLocalFile;
                internal System.Windows.Forms.Label lblLocalFile;
                private TabControl tabControl1;
                private TabPage tpSimple;
                private TabPage tpFull;
                private SplitContainer splitContainer1;
                private SplitContainer splitContainer2;
                private Button btnLocalBrowserGo;
                private TextBox txtLocalBrowserPath;
                private SplitContainer splitContainer3;
                internal EXListView lvLocalBrowser;
                private ColumnHeader clmLFBIcon;
                private ColumnHeader clmName;
                private ColumnHeader clSize;
                private Button button1;
                private TextBox txtRemoteFolderPath;
                internal EXListView lvSSHFileBrowser;
                private ColumnHeader columnHeader1;
                private ColumnHeader columnHeader2;
                private ColumnHeader columnHeader3;
                internal System.Windows.Forms.GroupBox grpFiles;

                private void InitializeComponent()
                {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SSHTransfer));
            this.grpFiles = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpSimple = new System.Windows.Forms.TabPage();
            this.lblLocalFile = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtLocalFile = new System.Windows.Forms.TextBox();
            this.lblRemoteFile = new System.Windows.Forms.Label();
            this.txtRemoteFile = new System.Windows.Forms.TextBox();
            this.tpFull = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnLocalBrowserGo = new System.Windows.Forms.Button();
            this.txtLocalBrowserPath = new System.Windows.Forms.TextBox();
            this.lvLocalBrowser = new EXControls.EXListView();
            this.clmLFBIcon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.button1 = new System.Windows.Forms.Button();
            this.txtRemoteFolderPath = new System.Windows.Forms.TextBox();
            this.lvSSHFileBrowser = new EXControls.EXListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpConnection = new System.Windows.Forms.GroupBox();
            this.radProtSFTP = new System.Windows.Forms.RadioButton();
            this.radProtSCP = new System.Windows.Forms.RadioButton();
            this.lblProtocol = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblHost = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.pbStatus = new System.Windows.Forms.ProgressBar();
            this.grpFiles.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpSimple.SuspendLayout();
            this.tpFull.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.grpConnection.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpFiles
            // 
            this.grpFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFiles.Controls.Add(this.tabControl1);
            this.grpFiles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpFiles.Location = new System.Drawing.Point(12, 153);
            this.grpFiles.Name = "grpFiles";
            this.grpFiles.Size = new System.Drawing.Size(976, 480);
            this.grpFiles.TabIndex = 2000;
            this.grpFiles.TabStop = false;
            this.grpFiles.Text = "Files";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpSimple);
            this.tabControl1.Controls.Add(this.tpFull);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 16);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(970, 461);
            this.tabControl1.TabIndex = 51;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tpSimple
            // 
            this.tpSimple.Controls.Add(this.lblLocalFile);
            this.tpSimple.Controls.Add(this.btnBrowse);
            this.tpSimple.Controls.Add(this.txtLocalFile);
            this.tpSimple.Controls.Add(this.lblRemoteFile);
            this.tpSimple.Controls.Add(this.txtRemoteFile);
            this.tpSimple.Location = new System.Drawing.Point(4, 22);
            this.tpSimple.Name = "tpSimple";
            this.tpSimple.Padding = new System.Windows.Forms.Padding(3);
            this.tpSimple.Size = new System.Drawing.Size(962, 435);
            this.tpSimple.TabIndex = 0;
            this.tpSimple.Text = "Simple";
            this.tpSimple.UseVisualStyleBackColor = true;
            // 
            // lblLocalFile
            // 
            this.lblLocalFile.AutoSize = true;
            this.lblLocalFile.Location = new System.Drawing.Point(6, 28);
            this.lblLocalFile.Name = "lblLocalFile";
            this.lblLocalFile.Size = new System.Drawing.Size(52, 13);
            this.lblLocalFile.TabIndex = 10;
            this.lblLocalFile.Text = "Local file:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Location = new System.Drawing.Point(800, 24);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(81, 23);
            this.btnBrowse.TabIndex = 30;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtLocalFile
            // 
            this.txtLocalFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLocalFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLocalFile.Location = new System.Drawing.Point(91, 26);
            this.txtLocalFile.Name = "txtLocalFile";
            this.txtLocalFile.Size = new System.Drawing.Size(703, 20);
            this.txtLocalFile.TabIndex = 20;
            // 
            // lblRemoteFile
            // 
            this.lblRemoteFile.AutoSize = true;
            this.lblRemoteFile.Location = new System.Drawing.Point(6, 54);
            this.lblRemoteFile.Name = "lblRemoteFile";
            this.lblRemoteFile.Size = new System.Drawing.Size(63, 13);
            this.lblRemoteFile.TabIndex = 40;
            this.lblRemoteFile.Text = "Remote file:";
            // 
            // txtRemoteFile
            // 
            this.txtRemoteFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRemoteFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemoteFile.Location = new System.Drawing.Point(91, 52);
            this.txtRemoteFile.Name = "txtRemoteFile";
            this.txtRemoteFile.Size = new System.Drawing.Size(790, 20);
            this.txtRemoteFile.TabIndex = 50;
            // 
            // tpFull
            // 
            this.tpFull.Controls.Add(this.splitContainer1);
            this.tpFull.Location = new System.Drawing.Point(4, 22);
            this.tpFull.Name = "tpFull";
            this.tpFull.Padding = new System.Windows.Forms.Padding(3);
            this.tpFull.Size = new System.Drawing.Size(962, 435);
            this.tpFull.TabIndex = 1;
            this.tpFull.Text = "Full";
            this.tpFull.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(956, 429);
            this.splitContainer1.SplitterDistance = 455;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.btnLocalBrowserGo);
            this.splitContainer2.Panel1.Controls.Add(this.txtLocalBrowserPath);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lvLocalBrowser);
            this.splitContainer2.Size = new System.Drawing.Size(455, 429);
            this.splitContainer2.SplitterDistance = 31;
            this.splitContainer2.TabIndex = 0;
            // 
            // btnLocalBrowserGo
            // 
            this.btnLocalBrowserGo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnLocalBrowserGo.Location = new System.Drawing.Point(384, 2);
            this.btnLocalBrowserGo.Name = "btnLocalBrowserGo";
            this.btnLocalBrowserGo.Size = new System.Drawing.Size(68, 27);
            this.btnLocalBrowserGo.TabIndex = 1;
            this.btnLocalBrowserGo.Text = "List";
            this.btnLocalBrowserGo.UseVisualStyleBackColor = true;
            this.btnLocalBrowserGo.Click += new System.EventHandler(this.btnLocalBrowserGo_Click);
            // 
            // txtLocalBrowserPath
            // 
            this.txtLocalBrowserPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLocalBrowserPath.Location = new System.Drawing.Point(3, 6);
            this.txtLocalBrowserPath.Name = "txtLocalBrowserPath";
            this.txtLocalBrowserPath.Size = new System.Drawing.Size(375, 20);
            this.txtLocalBrowserPath.TabIndex = 0;
            // 
            // lvLocalBrowser
            // 
            this.lvLocalBrowser.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmLFBIcon,
            this.clmName,
            this.clSize});
            this.lvLocalBrowser.ControlPadding = 4;
            this.lvLocalBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLocalBrowser.FullRowSelect = true;
            this.lvLocalBrowser.Location = new System.Drawing.Point(0, 0);
            this.lvLocalBrowser.Name = "lvLocalBrowser";
            this.lvLocalBrowser.OwnerDraw = true;
            this.lvLocalBrowser.Size = new System.Drawing.Size(455, 394);
            this.lvLocalBrowser.TabIndex = 0;
            this.lvLocalBrowser.UseCompatibleStateImageBehavior = false;
            this.lvLocalBrowser.View = System.Windows.Forms.View.Details;
            this.lvLocalBrowser.ItemActivate += new System.EventHandler(this.lvLocalBrowser_ItemActivate);
            this.lvLocalBrowser.SelectedIndexChanged += new System.EventHandler(this.lvLocalBrowser_SelectedIndexChanged);
            this.lvLocalBrowser.Click += new System.EventHandler(this.lvLocalBrowser_Click);
            this.lvLocalBrowser.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvLocalBrowser_MouseClick);
            this.lvLocalBrowser.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvLocalBrowser_MouseDoubleClick);
            // 
            // clmLFBIcon
            // 
            this.clmLFBIcon.Text = "";
            this.clmLFBIcon.Width = 20;
            // 
            // clmName
            // 
            this.clmName.Text = "Name";
            // 
            // clSize
            // 
            this.clSize.Text = "Size";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.button1);
            this.splitContainer3.Panel1.Controls.Add(this.txtRemoteFolderPath);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.lvSSHFileBrowser);
            this.splitContainer3.Size = new System.Drawing.Size(497, 429);
            this.splitContainer3.SplitterDistance = 32;
            this.splitContainer3.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button1.Location = new System.Drawing.Point(426, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(68, 27);
            this.button1.TabIndex = 3;
            this.button1.Text = "List";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtRemoteFolderPath
            // 
            this.txtRemoteFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRemoteFolderPath.Location = new System.Drawing.Point(3, 6);
            this.txtRemoteFolderPath.Name = "txtRemoteFolderPath";
            this.txtRemoteFolderPath.Size = new System.Drawing.Size(417, 20);
            this.txtRemoteFolderPath.TabIndex = 2;
            this.txtRemoteFolderPath.Text = "/";
            // 
            // lvSSHFileBrowser
            // 
            this.lvSSHFileBrowser.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvSSHFileBrowser.ControlPadding = 4;
            this.lvSSHFileBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSSHFileBrowser.FullRowSelect = true;
            this.lvSSHFileBrowser.Location = new System.Drawing.Point(0, 0);
            this.lvSSHFileBrowser.Name = "lvSSHFileBrowser";
            this.lvSSHFileBrowser.OwnerDraw = true;
            this.lvSSHFileBrowser.Size = new System.Drawing.Size(497, 393);
            this.lvSSHFileBrowser.TabIndex = 1;
            this.lvSSHFileBrowser.UseCompatibleStateImageBehavior = false;
            this.lvSSHFileBrowser.View = System.Windows.Forms.View.Details;
            this.lvSSHFileBrowser.ItemActivate += new System.EventHandler(this.lvSSHFileBrowser_ItemActivate);
            this.lvSSHFileBrowser.Click += new System.EventHandler(this.lvSSHFileBrowser_Click);
            this.lvSSHFileBrowser.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvSSHFileBrowser_MouseClick_1);
            this.lvSSHFileBrowser.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvSSHFileBrowser_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 20;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Size";
            // 
            // grpConnection
            // 
            this.grpConnection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpConnection.Controls.Add(this.radProtSFTP);
            this.grpConnection.Controls.Add(this.radProtSCP);
            this.grpConnection.Controls.Add(this.lblProtocol);
            this.grpConnection.Controls.Add(this.lblPassword);
            this.grpConnection.Controls.Add(this.lblUser);
            this.grpConnection.Controls.Add(this.lblPort);
            this.grpConnection.Controls.Add(this.lblHost);
            this.grpConnection.Controls.Add(this.txtPort);
            this.grpConnection.Controls.Add(this.txtHost);
            this.grpConnection.Controls.Add(this.txtPassword);
            this.grpConnection.Controls.Add(this.txtUser);
            this.grpConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpConnection.Location = new System.Drawing.Point(12, 12);
            this.grpConnection.Name = "grpConnection";
            this.grpConnection.Size = new System.Drawing.Size(976, 135);
            this.grpConnection.TabIndex = 1000;
            this.grpConnection.TabStop = false;
            this.grpConnection.Text = "Connection";
            // 
            // radProtSFTP
            // 
            this.radProtSFTP.AutoSize = true;
            this.radProtSFTP.Checked = true;
            this.radProtSFTP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radProtSFTP.Location = new System.Drawing.Point(153, 103);
            this.radProtSFTP.Name = "radProtSFTP";
            this.radProtSFTP.Size = new System.Drawing.Size(51, 17);
            this.radProtSFTP.TabIndex = 110;
            this.radProtSFTP.TabStop = true;
            this.radProtSFTP.Text = "SFTP";
            this.radProtSFTP.UseVisualStyleBackColor = true;
            this.radProtSFTP.CheckedChanged += new System.EventHandler(this.radProtSFTP_CheckedChanged);
            // 
            // radProtSCP
            // 
            this.radProtSCP.AutoSize = true;
            this.radProtSCP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radProtSCP.Location = new System.Drawing.Point(92, 103);
            this.radProtSCP.Name = "radProtSCP";
            this.radProtSCP.Size = new System.Drawing.Size(45, 17);
            this.radProtSCP.TabIndex = 100;
            this.radProtSCP.Text = "SCP";
            this.radProtSCP.UseVisualStyleBackColor = true;
            // 
            // lblProtocol
            // 
            this.lblProtocol.AutoSize = true;
            this.lblProtocol.Location = new System.Drawing.Point(20, 105);
            this.lblProtocol.Name = "lblProtocol";
            this.lblProtocol.Size = new System.Drawing.Size(49, 13);
            this.lblProtocol.TabIndex = 90;
            this.lblProtocol.Text = "Protocol:";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(20, 79);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 70;
            this.lblPassword.Text = "Password:";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(20, 53);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(32, 13);
            this.lblUser.TabIndex = 50;
            this.lblUser.Text = "User:";
            // 
            // lblPort
            // 
            this.lblPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(890, 27);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(29, 13);
            this.lblPort.TabIndex = 30;
            this.lblPort.Text = "Port:";
            // 
            // lblHost
            // 
            this.lblHost.AutoSize = true;
            this.lblHost.Location = new System.Drawing.Point(20, 27);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(32, 13);
            this.lblHost.TabIndex = 10;
            this.lblHost.Text = "Host:";
            // 
            // txtPort
            // 
            this.txtPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPort.Location = new System.Drawing.Point(925, 25);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(30, 20);
            this.txtPort.TabIndex = 40;
            // 
            // txtHost
            // 
            this.txtHost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHost.Location = new System.Drawing.Point(105, 25);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(779, 20);
            this.txtHost.TabIndex = 20;
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Location = new System.Drawing.Point(105, 77);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(779, 20);
            this.txtPassword.TabIndex = 80;
            // 
            // txtUser
            // 
            this.txtUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUser.Location = new System.Drawing.Point(105, 51);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(779, 20);
            this.txtUser.TabIndex = 60;
            // 
            // btnTransfer
            // 
            this.btnTransfer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTransfer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransfer.Image = global::My.Resources.Resources.SSHTransfer;
            this.btnTransfer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTransfer.Location = new System.Drawing.Point(905, 668);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(83, 29);
            this.btnTransfer.TabIndex = 10000;
            this.btnTransfer.Text = "Transfer";
            this.btnTransfer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTransfer.UseVisualStyleBackColor = true;
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // pbStatus
            // 
            this.pbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbStatus.Location = new System.Drawing.Point(12, 639);
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(976, 23);
            this.pbStatus.TabIndex = 3000;
            // 
            // SSHTransfer
            // 
            this.ClientSize = new System.Drawing.Size(1000, 709);
            this.Controls.Add(this.grpFiles);
            this.Controls.Add(this.grpConnection);
            this.Controls.Add(this.btnTransfer);
            this.Controls.Add(this.pbStatus);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SSHTransfer";
            this.TabText = "SSH File Transfer";
            this.Text = "SSH File Transfer";
            this.Load += new System.EventHandler(this.SSHTransfer_Load);
            this.grpFiles.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpSimple.ResumeLayout(false);
            this.tpSimple.PerformLayout();
            this.tpFull.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.grpConnection.ResumeLayout(false);
            this.grpConnection.PerformLayout();
            this.ResumeLayout(false);

                }

                #endregion Form Init

                #region Private Properties
                
                private OpenFileDialog oDlg;

                #endregion Private Properties

                #region Public Properties

                public string Hostname
                {
                    get { return this.txtHost.Text; }
                    set { this.txtHost.Text = value; }
                }

                public string Port
                {
                    get { return this.txtPort.Text; }
                    set { this.txtPort.Text = value; }
                }

                public string Username
                {
                    get { return this.txtUser.Text; }
                    set { this.txtUser.Text = value; }
                }

                public string Password
                {
                    get { return this.txtPassword.Text; }
                    set { this.txtPassword.Text = value; }
                }

                #endregion Public Properties

                #region Form Stuff

                private void SSHTransfer_Load(object sender, System.EventArgs e)
                {
                    ApplyLanguage();
                    txtLocalBrowserPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    btnLocalBrowserGo_Click(null,null);
                }

                private void ApplyLanguage()
                {
                    grpFiles.Text = Language.strGroupboxFiles;
                    lblLocalFile.Text = Language.strLocalFile + ":";
                    lblRemoteFile.Text = Language.strRemoteFile + ":";
                    btnBrowse.Text = Language.strButtonBrowse;
                    grpConnection.Text = Language.strGroupboxConnection;
                    lblProtocol.Text = Language.strLabelProtocol;
                    lblPassword.Text = Language.strLabelPassword;
                    lblUser.Text = Language.strUser + ":";
                    lblPort.Text = Language.strLabelPort;
                    lblHost.Text = Language.strHost + ":";
                    btnTransfer.Text = Language.strTransfer;
                    TabText = Language.strMenuSSHFileTransfer;
                    Text = Language.strMenuSSHFileTransfer;
                }

                #endregion Form Stuff

                #region Private Methods

                private void SetProgressStatus(long cur, long max)
                {
                    Action del = () =>
                    {
                        pbStatus.Maximum = 100;
                        pbStatus.Value = (int)((cur * 100) / (max));
                        if (cur == max)
                        {
                            pbStatus.Maximum = 100;
                            pbStatus.Value = 100;
                            EnableButtons();
                        }
                    };
                    if (pbStatus.InvokeRequired)
                    {
                        pbStatus.Invoke(del);
                    }
                    else
                    {
                        del();
                    }
                }
                private void StartTransfer(SSHTransferProtocol Protocol)
                {
                    if (AllFieldsSet() == false)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            Language.strPleaseFillAllFields);
                        return;
                    }

                    if (File.Exists(this.txtLocalFile.Text) == false)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                            Language.strLocalFileDoesNotExist);
                        return;
                    }

                    try
                    {
                        if (Protocol == SSHTransferProtocol.SCP)
                        {
                            var ssh = new ScpClient(txtHost.Text, int.Parse(this.txtPort.Text), txtUser.Text, txtPassword.Text);
                            ssh.Uploading+=(sender, e) => SetProgressStatus(e.Uploaded, e.Size);
                            DisableButtons();
                            ssh.Connect();
                            ssh.Upload(new FileStream(txtLocalFile.Text,FileMode.Open), txtRemoteFile.Text);
                        }
                        else if (Protocol == SSHTransferProtocol.SFTP)
                        {
                            var ssh = new SftpClient(txtHost.Text, int.Parse(txtPort.Text),txtUser.Text, txtPassword.Text);
                            var s = new FileStream(txtLocalFile.Text, FileMode.Open);
                            ssh.Connect();
                            var i = ssh.BeginUploadFile(s, txtRemoteFile.Text) as SftpUploadAsyncResult;
                            ThreadPool.QueueUserWorkItem(state => 
                            {
                                while (!i.IsCompleted)
                                {
                                    SetProgressStatus((long)i.UploadedBytes, s.Length);
                                }
                                MessageBox.Show("Upload completed.");
                                EnableButtons();
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            Language.strSSHTransferFailed + Constants.vbNewLine +
                                                            ex.Message);
                        EnableButtons();
                    }
                }



                private bool AllFieldsSet()
                {
                    if (this.txtHost.Text != "" && this.txtPort.Text != "" && this.txtUser.Text != "" &&
                        this.txtLocalFile.Text != "" && this.txtRemoteFile.Text != "")
                    {
                        if (this.txtPassword.Text == "")
                        {
                            if (
                                Interaction.MsgBox(Language.strEmptyPasswordContinue,
                                                   MsgBoxStyle.Question | MsgBoxStyle.YesNo, null) == MsgBoxResult.No)
                            {
                                return false;
                            }
                        }

                        if (this.txtRemoteFile.Text.EndsWith("/") || this.txtRemoteFile.Text.EndsWith("\\"))
                        {
                            this.txtRemoteFile.Text +=
                                this.txtLocalFile.Text.Substring(
                                    System.Convert.ToInt32(this.txtLocalFile.Text.LastIndexOf("\\") + 1));
                        }

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }


                private delegate void EnableButtonsCB();

                private void EnableButtons()
                {
                    if (btnTransfer.InvokeRequired)
                    {
                        EnableButtonsCB d = new EnableButtonsCB(EnableButtons);
                        this.btnTransfer.Invoke(d);
                    }
                    else
                    {
                        btnTransfer.Enabled = true;
                    }
                }

                private delegate void DisableButtonsCB();

                private void DisableButtons()
                {
                    if (btnTransfer.InvokeRequired)
                    {
                        DisableButtonsCB d = new DisableButtonsCB(DisableButtons);
                        this.btnTransfer.Invoke(d);
                    }
                    else
                    {
                        btnTransfer.Enabled = false;
                    }
                }

                #endregion Private Methods

                #region Public Methods

                public SSHTransfer(DockContent Panel)
                {
                    this.WindowType = Type.SSHTransfer;
                    this.DockPnl = Panel;
                    this.InitializeComponent();

                    this.oDlg = new OpenFileDialog();
                    this.oDlg.Filter = "All Files (*.*)|*.*";
                    this.oDlg.CheckFileExists = true;
                }

                #endregion Public Methods

                #region Form Stuff

                private void btnBrowse_Click(System.Object sender, System.EventArgs e)
                {
                    if (this.oDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (this.oDlg.FileName != "")
                        {
                            this.txtLocalFile.Text = this.oDlg.FileName;
                        }
                    }
                }

                private void btnTransfer_Click(System.Object sender, System.EventArgs e)
                {
                    if (tabControl1.SelectedTab==tpSimple)
                    {
                        if (this.radProtSCP.Checked)
                        {
                            this.StartTransfer(SSHTransferProtocol.SCP);
                        }
                        else if (this.radProtSFTP.Checked)
                        {
                            this.StartTransfer(SSHTransferProtocol.SFTP);
                        }
                    }
                    else
                    {
                        if (Upload)
                        {
                            UploadFilesFromFilemanager();
                        }
                        else
                        {
                            DownloadFilesFromServer();
                        }
                        
                    }
                    
                }

                private void DownloadFilesFromServer()
                {
                    try
                    {
                        var filelist = new Dictionary<SftpFile, string>();
                        var remfold = txtRemoteFolderPath.Text.EndsWith("/") ? txtRemoteFolderPath.Text : txtRemoteFolderPath.Text += "/";
                        var ssh = new SftpClient(txtHost.Text, int.Parse(txtPort.Text), txtUser.Text, txtPassword.Text);
                        ssh.Connect();
                        foreach (var i in lvSSHFileBrowser.SelectedItems.Cast<EXImageListViewItem>().Select(item => item.Tag as SftpFile))
                        {
                            if (i.IsRegularFile)
                            {
                                filelist.Add(i, Path.Combine(txtLocalBrowserPath.Text, i.Name));
                            }
                            if (i.IsDirectory)
                            {
                                foreach (var file in GetFilesRecur(ssh, i))
                                {
                                    var i1 = file.FullName.Replace(remfold, "");
                                    var i2 = i1.Replace('/', '\\');
                                    var i3 = Path.Combine(txtLocalBrowserPath.Text, i2);
                                    filelist.Add(file, i3);
                                }
                            }
                        }
                        long totalsize = filelist.Sum(pair => pair.Key.Length);
                        var result = MessageBox.Show(string.Format("Download {0} in {1} files?\r\nDestination directory:{2}", Tools.Misc.LengthToHumanReadable(totalsize), filelist.Count, txtLocalBrowserPath.Text), "Downloading", MessageBoxButtons.YesNoCancel);
                        if (result!=DialogResult.Yes)
                        {
                            EnableButtons();
                            return;
                        }
                        long totaluploaded = 0;
                        ThreadPool.QueueUserWorkItem(state =>
                            {
                                foreach (var file in filelist)
                                {
                                    if (!Directory.Exists(Path.GetDirectoryName(file.Value)))
                                    {
                                        Tools.Misc.ebfFolderCreate(Path.GetDirectoryName(file.Value));
                                    }
                                    var asynch = ssh.BeginDownloadFile(file.Key.FullName,
                                                                       new FileStream(file.Value, FileMode.Create));
                                    var sftpAsynch = asynch as SftpDownloadAsyncResult;

                                    while (!sftpAsynch.IsCompleted)
                                    {
                                        SetProgressStatus(totaluploaded + (long)sftpAsynch.DownloadedBytes, totalsize);
                                    }
                                    totaluploaded += file.Key.Length;
                                    ssh.EndDownloadFile(asynch);
                                }
                                EnableButtons();
                                ssh.Disconnect();
                                MessageBox.Show("Download finished");
                            });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }


                private IEnumerable<SftpFile> GetFilesRecur(SftpClient ssh, SftpFile sftpDir)
                {
                    if (!sftpDir.IsDirectory)
                    {
                        return new[] {sftpDir};
                    }
                    var fl = new List<SftpFile>();
                    foreach (var sftpFile in ssh.ListDirectory(sftpDir.FullName))
                    {
                        if (sftpFile.IsRegularFile)
                        {
                            fl.Add(sftpFile);
                        }
                        else if (sftpFile.IsDirectory && sftpFile.Name != "." && sftpFile.Name != "..")
                        {
                            fl.AddRange(GetFilesRecur(ssh, sftpFile));
                        }
                    }
                    return fl;
                }

                private bool _upload = true;
                private bool Upload { get { return _upload; } 
                        set
                        {
                            _upload = value;
                            btnTransfer.Text = tabControl1.SelectedIndex==1? _upload ? "Upload" : "Download":"Transfer";
                            btnTransfer.Image = tabControl1.SelectedIndex == 1 ? !_upload ? Tools.Misc.RotateFlip(RotateFlipType.RotateNoneFlipX, global::My.Resources.Resources.SSHTransfer) : global::My.Resources.Resources.SSHTransfer : global::My.Resources.Resources.SSHTransfer;
                        } 
                    }

                private void UploadFilesFromFilemanager()
                {
                    string destpathroot = txtRemoteFolderPath.Text;
                    if (!destpathroot.EndsWith("/"))
                    {
                        destpathroot += "/";
                    }
                    var filelist = new Dictionary<string, string>();
                    foreach (var item in lvLocalBrowser.SelectedItems.Cast<EXImageListViewItem>().Where(item => (string) item.Tag != "[..]"))
                    {
                        if ((string)item.Tag == "File")
                        {
                            filelist.Add(item.MyValue, destpathroot + Path.GetFileName(item.MyValue));
                        }
                        if ((string)item.Tag == "Folder")
                        {
                            string folder = Path.GetDirectoryName(item.MyValue);
                            folder = folder.EndsWith("\\") ? folder : folder + "\\";
                            string[] files = Directory.GetFiles(item.MyValue,
                                                                "*.*",
                                                                SearchOption.AllDirectories);

                            // Display all the files.
                            foreach (string file in files)
                            {
                                filelist.Add(Path.GetFullPath(file), destpathroot + Path.GetFullPath(file).Replace(folder,"").Replace("\\","/"));
                            }
                        }
                    }
                    long fulllength = filelist.Sum(file => new FileInfo(file.Key).Length);
                    MessageBox.Show(Tools.Misc.LengthToHumanReadable(fulllength));
                    var ssh = new SftpClient(txtHost.Text, int.Parse(this.txtPort.Text), txtUser.Text, txtPassword.Text);
                    ssh.Connect();
                    
                    ThreadPool.QueueUserWorkItem(state =>
                        {
                            long totaluploaded = 0;
                            foreach (var file in filelist)
                            {
                                CreatSSHDir(ssh, file.Value);
                                var s = new FileStream(file.Key, FileMode.Open);
                                var i = ssh.BeginUploadFile(s, file.Value) as SftpUploadAsyncResult;
                                while (!i.IsCompleted)
                                {
                                    SetProgressStatus(totaluploaded + (long)i.UploadedBytes, fulllength);
                                }
                                ssh.EndUploadFile(i);
                                totaluploaded += s.Length;
                            }
                            MessageBox.Show("Upload completed.");
                            EnableButtons();
                            ssh.Disconnect();
                        });
                }

                private static void CreatSSHDir(SftpClient ssh, string file)
                {
                    var newDir = Tools.Misc.GetUnixDirecoryOfFile(file);
                    if (ssh.Exists(newDir)) return;
                    string[] dirs = new string[newDir.ToCharArray().Count(x => x == '/')];
                    dirs[0] = newDir;
                    for (int i = 1; i < dirs.Count(); i++)
                    {
                        dirs[i] = Tools.Misc.GetUnixDirecoryOfFile(dirs[i - 1]);
                    }
                    for (int i = dirs.Count()-1; i >= 0; i--)
                    {
                        if (ssh.Exists(dirs[i])) continue;
                        ssh.CreateDirectory(dirs[i]);
                    }
                }

                #endregion Form Stuff

                private enum SSHTransferProtocol
                {
                    SCP = 0,
                    SFTP = 1
                }

                private void btnLocalBrowserGo_Click(object sender, EventArgs e)
                {
                    try
                    {
                        var l = new List<EXImageListViewItem>();
                        lvLocalBrowser.Items.Clear();
                        if (txtLocalBrowserPath.Text == "")
                        {
                            foreach (string str in Directory.GetLogicalDrives())
                            {
                                EXImageListViewItem imglstvitem = new EXImageListViewItem();
                                imglstvitem.MyImage = global::My.Resources.Resources.Folder;
                                imglstvitem.MyValue = str;
                                imglstvitem.Tag = "Folder";
                                imglstvitem.SubItems.Add(str);
                                imglstvitem.SubItems.Add("");
                                lvLocalBrowser.Items.Add(imglstvitem);
                            }
                            return;
                        }
                        EXImageListViewItem li = new EXImageListViewItem();
                        li.MyValue = txtLocalBrowserPath.Text;
                        li.Tag = "[..]";
                        li.SubItems.Add("[..]");
                        li.SubItems.Add("");
                        l.Add(li);
                        var d = Directory.GetDirectories(txtLocalBrowserPath.Text);
                        foreach (var s1 in d)
                        {
                            EXImageListViewItem imglstvitem = new EXImageListViewItem();
                            imglstvitem.MyImage = global::My.Resources.Resources.Folder;
                            imglstvitem.MyValue = s1;
                            imglstvitem.Tag = "Folder";
                            imglstvitem.SubItems.Add(Path.GetFileName(s1));
                            imglstvitem.SubItems.Add("");
                            l.Add(imglstvitem);
                        }
                        var s = Directory.GetFiles(txtLocalBrowserPath.Text);
                        
                        foreach (var s1 in s)
                        {
                            EXImageListViewItem imglstvitem = new EXImageListViewItem();
                            imglstvitem.MyImage = global::My.Resources.Resources.File;
                            imglstvitem.MyValue = s1;
                            imglstvitem.Tag = "File";
                            imglstvitem.SubItems.Add(Path.GetFileName(s1));
                            imglstvitem.SubItems.Add(Tools.Misc.LengthToHumanReadable(new FileInfo(s1).Length));
                            /*li.SubItems.Add()*/
                            l.Add(imglstvitem);
                        }

                        lvLocalBrowser.Items.AddRange(l.ToArray());
                        lvLocalBrowser.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                        lvLocalBrowser.Refresh();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }

                private void lvLocalBrowser_SelectedIndexChanged(object sender, EventArgs e)
                {

                }

                private void lvLocalBrowser_MouseDoubleClick(object sender, MouseEventArgs e)
                {
                    ListViewHitTestInfo hit = lvLocalBrowser.HitTest(e.Location);
                    if (hit.Item != null)
                    {
                        var i = (hit.Item as EXImageListViewItem);
                        if (i.SubItems[1].Text == "[..]")
                        {
                            txtLocalBrowserPath.Text = Path.GetDirectoryName(i.MyValue);
                            btnLocalBrowserGo_Click(null,null);
                        }
                        else if ((string) i.Tag=="Folder")
                        {
                            txtLocalBrowserPath.Text = i.MyValue;
                            btnLocalBrowserGo_Click(null, null);
                        }
                    }
                }

                private void button1_Click(object sender, EventArgs e)
                {
                    try
                    {
                        /*if (AllFieldsSet() == false)
                        {
                            Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                                Language.strPleaseFillAllFields);
                            return;
                        }*/

                        lvSSHFileBrowser.Items.Clear();
                        var ssh = new SftpClient(txtHost.Text, int.Parse(this.txtPort.Text), txtUser.Text, txtPassword.Text);
                        ssh.Connect();
                        var res = ssh.ListDirectory(txtRemoteFolderPath.Text).ToList().OrderBy(file => !file.IsDirectory).ThenBy(file => file.Name);
                        var l = new List<EXImageListViewItem>();
                        foreach (var item in res)
                        {
                            if (item.Name==".")
                            {
                                continue;
                            }
                            EXImageListViewItem imglstvitem = new EXImageListViewItem();
                            imglstvitem.MyImage = item.IsDirectory ? global::My.Resources.Resources.Folder : global::My.Resources.Resources.File;
                            imglstvitem.MyValue = item.FullName;
                            imglstvitem.Tag = item;
                            imglstvitem.SubItems.Add(item.Name);
                            imglstvitem.SubItems.Add(item.IsDirectory ? "" : Tools.Misc.LengthToHumanReadable(item.Length));
                            l.Add(imglstvitem);
                        }
                        ssh.Disconnect();
                        lvSSHFileBrowser.Items.AddRange(l.ToArray());
                        lvSSHFileBrowser.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                private void lvSSHFileBrowser_MouseClick(object sender, MouseEventArgs e)
                {
                    ListViewHitTestInfo hit = lvSSHFileBrowser.HitTest(e.Location);
                    if (hit.Item != null)
                    {
                        var i = ((hit.Item as EXImageListViewItem).Tag as SftpFile);
                        if (i.IsDirectory )
                        {
                            txtRemoteFolderPath.Text = Tools.Misc.GetUnixPathParent(i.FullName);
                            button1_Click(null, null);
                        }
                    }
                }

                private void radProtSFTP_CheckedChanged(object sender, EventArgs e)
                {
                   tpFull.Parent = radProtSFTP.Checked?tabControl1:null;
                }

                private void lvLocalBrowser_MouseClick(object sender, MouseEventArgs e)
                {
                    Upload = true;
                }

                private void lvSSHFileBrowser_MouseClick_1(object sender, MouseEventArgs e)
                {
                    Upload = false;
                }

                private void lvSSHFileBrowser_Click(object sender, EventArgs e)
                {
                    Upload = false;
                }

                private void lvLocalBrowser_Click(object sender, EventArgs e)
                {
                    Upload = true;
                }

                private void lvLocalBrowser_ItemActivate(object sender, EventArgs e)
                {
                    Upload = true;
                }

                private void lvSSHFileBrowser_ItemActivate(object sender, EventArgs e)
                {
                    Upload = false;
                }

                private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
                {
                    //ToDo: Make normal code
                    Upload = Upload;//Magic :(
                }


            }
        }
    }
}