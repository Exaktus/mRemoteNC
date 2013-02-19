using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Drawing;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;
using My;
using WeifenLuo.WinFormsUI.Docking;
using mRemoteNC.App;
using mRemoteNC.Connection;
using mRemoteNC.Forms;
using mRemoteNC.Messages;
using mRemoteNC.Tools;
using mRemoteNC.Tree;

//using mRemoteNC.Runtime;

namespace mRemoteNC
{
    namespace UI
    {
        namespace Window
        {
            public class Tree : Base
            {
                private System.ComponentModel.IContainer components;

                #region Form Init

                internal System.Windows.Forms.TextBox txtSearch;
                internal System.Windows.Forms.Panel pnlConnections;
                internal System.Windows.Forms.ImageList imgListTree;
                internal System.Windows.Forms.MenuStrip msMain;
                internal System.Windows.Forms.ToolStripMenuItem mMenView;
                internal System.Windows.Forms.ToolStripMenuItem mMenViewExpandAllFolders;
                internal System.Windows.Forms.ToolStripMenuItem mMenViewCollapseAllFolders;
                internal System.Windows.Forms.ContextMenuStrip cMenTree;
                internal System.Windows.Forms.ToolStripMenuItem cMenTreeAddConnection;
                internal System.Windows.Forms.ToolStripMenuItem cMenTreeAddFolder;
                internal System.Windows.Forms.ToolStripSeparator cMenTreeSep1;
                internal System.Windows.Forms.ToolStripMenuItem cMenTreeConnect;
                internal System.Windows.Forms.ToolStripMenuItem cMenTreeConnectWithOptions;
                internal System.Windows.Forms.ToolStripMenuItem cMenTreeConnectWithOptionsConnectToConsoleSession;
                internal System.Windows.Forms.ToolStripMenuItem cMenTreeConnectWithOptionsConnectInFullscreen;
                internal System.Windows.Forms.ToolStripMenuItem cMenTreeDisconnect;
                internal System.Windows.Forms.ToolStripSeparator cMenTreeSep2;
                internal System.Windows.Forms.ToolStripMenuItem cMenTreeToolsTransferFile;
                internal System.Windows.Forms.ToolStripMenuItem cMenTreeToolsImportExport;
                internal System.Windows.Forms.ToolStripMenuItem cMenTreeToolsImportExportExportmRemoteXML;
                internal System.Windows.Forms.ToolStripMenuItem cMenTreeToolsImportExportImportmRemoteXML;
                internal System.Windows.Forms.ToolStripSeparator cMenTreeToolsImportExportSep1;
                internal System.Windows.Forms.ToolStripMenuItem cMenTreeToolsImportExportImportFromAD;
                internal System.Windows.Forms.ToolStripMenuItem cMenTreeToolsSort;
                internal System.Windows.Forms.ToolStripMenuItem cMenTreeToolsSortAscending;
                internal System.Windows.Forms.ToolStripMenuItem cMenTreeToolsSortDescending;
                internal System.Windows.Forms.ToolStripSeparator cMenTreeSep3;
                internal System.Windows.Forms.ToolStripMenuItem cMenTreeRename;
                internal System.Windows.Forms.ToolStripMenuItem cMenTreeDelete;
                internal System.Windows.Forms.ToolStripSeparator cMenTreeSep4;
                internal System.Windows.Forms.ToolStripMenuItem cMenTreeMoveUp;
                internal System.Windows.Forms.ToolStripMenuItem cMenTreeMoveDown;
                internal System.Windows.Forms.PictureBox PictureBox1;
                internal System.Windows.Forms.ToolStripMenuItem cMenTreeToolsImportExportImportFromRDPFiles;
                internal System.Windows.Forms.ToolStripMenuItem cMenTreeToolsExternalApps;
                internal System.Windows.Forms.ToolStripMenuItem cMenTreeDuplicate;
                internal System.Windows.Forms.ToolStripMenuItem cMenTreeToolsImportExportImportFromPortScan;
                internal System.Windows.Forms.ToolStripMenuItem cMenTreeConnectWithOptionsChoosePanelBeforeConnecting;
                internal System.Windows.Forms.ToolStripMenuItem cMenTreeConnectWithOptionsDontConnectToConsoleSession;
                internal System.Windows.Forms.ToolStripMenuItem mMenSortAscending;
                internal System.Windows.Forms.ToolStripMenuItem mMenAddConnection;
                internal System.Windows.Forms.ToolStripMenuItem mMenAddFolder;
                private ToolStripMenuItem checkTypicalPPortsToolStripMenuItem;
                public System.Windows.Forms.TreeView tvConnections;

                private void InitializeComponent()
                {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Connections");
            this.tvConnections = new System.Windows.Forms.TreeView();
            this.cMenTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cMenTreeAddConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenTreeAddFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenTreeSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.cMenTreeConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenTreeConnectWithOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenTreeConnectWithOptionsConnectToConsoleSession = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenTreeConnectWithOptionsDontConnectToConsoleSession = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenTreeConnectWithOptionsConnectInFullscreen = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenTreeConnectWithOptionsChoosePanelBeforeConnecting = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenTreeDisconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenTreeSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.cMenTreeToolsTransferFile = new System.Windows.Forms.ToolStripMenuItem();
            this.checkTypicalPPortsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenTreeToolsImportExport = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenTreeToolsImportExportExportmRemoteXML = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenTreeToolsImportExportImportmRemoteXML = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenTreeToolsImportExportSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.cMenTreeToolsImportExportImportFromAD = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenTreeToolsImportExportImportFromRDPFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenTreeToolsImportExportImportFromPortScan = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenTreeToolsSort = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenTreeToolsSortAscending = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenTreeToolsSortDescending = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenTreeToolsExternalApps = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenTreeSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.cMenTreeDuplicate = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenTreeRename = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenTreeDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenTreeSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.cMenTreeMoveUp = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenTreeMoveDown = new System.Windows.Forms.ToolStripMenuItem();
            this.imgListTree = new System.Windows.Forms.ImageList(this.components);
            this.pnlConnections = new System.Windows.Forms.Panel();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.mMenAddConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenAddFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenView = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenViewExpandAllFolders = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenViewCollapseAllFolders = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenSortAscending = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenTree.SuspendLayout();
            this.pnlConnections.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.msMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvConnections
            // 
            this.tvConnections.AllowDrop = true;
            this.tvConnections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvConnections.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvConnections.ContextMenuStrip = this.cMenTree;
            this.tvConnections.HideSelection = false;
            this.tvConnections.ImageIndex = 0;
            this.tvConnections.ImageList = this.imgListTree;
            this.tvConnections.LabelEdit = true;
            this.tvConnections.Location = new System.Drawing.Point(0, 0);
            this.tvConnections.Name = "tvConnections";
            treeNode1.Name = "nodeRoot";
            treeNode1.Text = "Connections";
            this.tvConnections.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.tvConnections.SelectedImageIndex = 0;
            this.tvConnections.Size = new System.Drawing.Size(210, 407);
            this.tvConnections.TabIndex = 20;
            this.tvConnections.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvConnections_BeforeLabelEdit);
            this.tvConnections.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvConnections_AfterLabelEdit);
            this.tvConnections.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvConnections_ItemDrag);
            this.tvConnections.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvConnections_AfterSelect);
            this.tvConnections.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvConnections_NodeMouseClick);
            this.tvConnections.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvConnections_NodeMouseDoubleClick);
            this.tvConnections.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvConnections_DragDrop);
            this.tvConnections.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvConnections_DragEnter);
            this.tvConnections.DragOver += new System.Windows.Forms.DragEventHandler(this.tvConnections_DragOver);
            this.tvConnections.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvConnections_KeyDown);
            this.tvConnections.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tvConnections_KeyPress);
            this.tvConnections.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tvConnections_MouseMove);
            // 
            // cMenTree
            // 
            this.cMenTree.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cMenTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cMenTreeAddConnection,
            this.cMenTreeAddFolder,
            this.cMenTreeSep1,
            this.cMenTreeConnect,
            this.cMenTreeConnectWithOptions,
            this.cMenTreeDisconnect,
            this.cMenTreeSep2,
            this.cMenTreeToolsTransferFile,
            this.checkTypicalPPortsToolStripMenuItem,
            this.cMenTreeToolsImportExport,
            this.cMenTreeToolsSort,
            this.cMenTreeToolsExternalApps,
            this.cMenTreeSep3,
            this.cMenTreeDuplicate,
            this.cMenTreeRename,
            this.cMenTreeDelete,
            this.cMenTreeSep4,
            this.cMenTreeMoveUp,
            this.cMenTreeMoveDown});
            this.cMenTree.Name = "cMenTree";
            this.cMenTree.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.cMenTree.Size = new System.Drawing.Size(187, 358);
            this.cMenTree.Opening += new System.ComponentModel.CancelEventHandler(this.cMenTree_DropDownOpening);
            // 
            // cMenTreeAddConnection
            // 
            this.cMenTreeAddConnection.Image = global::My.Resources.Resources.Connection_Add;
            this.cMenTreeAddConnection.Name = "cMenTreeAddConnection";
            this.cMenTreeAddConnection.Size = new System.Drawing.Size(186, 22);
            this.cMenTreeAddConnection.Text = "New Connection";
            this.cMenTreeAddConnection.Click += new System.EventHandler(this.cMenTreeAddConnection_Click);
            // 
            // cMenTreeAddFolder
            // 
            this.cMenTreeAddFolder.Image = global::My.Resources.Resources.Folder_Add;
            this.cMenTreeAddFolder.Name = "cMenTreeAddFolder";
            this.cMenTreeAddFolder.Size = new System.Drawing.Size(186, 22);
            this.cMenTreeAddFolder.Text = "New Folder";
            this.cMenTreeAddFolder.Click += new System.EventHandler(this.cMenTreeAddFolder_Click);
            // 
            // cMenTreeSep1
            // 
            this.cMenTreeSep1.Name = "cMenTreeSep1";
            this.cMenTreeSep1.Size = new System.Drawing.Size(183, 6);
            // 
            // cMenTreeConnect
            // 
            this.cMenTreeConnect.Image = global::My.Resources.Resources.Play;
            this.cMenTreeConnect.Name = "cMenTreeConnect";
            this.cMenTreeConnect.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.cMenTreeConnect.Size = new System.Drawing.Size(186, 22);
            this.cMenTreeConnect.Text = "Connect";
            this.cMenTreeConnect.Click += new System.EventHandler(this.cMenTreeConnect_Click);
            // 
            // cMenTreeConnectWithOptions
            // 
            this.cMenTreeConnectWithOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cMenTreeConnectWithOptionsConnectToConsoleSession,
            this.cMenTreeConnectWithOptionsDontConnectToConsoleSession,
            this.cMenTreeConnectWithOptionsConnectInFullscreen,
            this.cMenTreeConnectWithOptionsChoosePanelBeforeConnecting});
            this.cMenTreeConnectWithOptions.Name = "cMenTreeConnectWithOptions";
            this.cMenTreeConnectWithOptions.Size = new System.Drawing.Size(186, 22);
            this.cMenTreeConnectWithOptions.Text = "Connect (with options)";
            // 
            // cMenTreeConnectWithOptionsConnectToConsoleSession
            // 
            this.cMenTreeConnectWithOptionsConnectToConsoleSession.Name = "cMenTreeConnectWithOptionsConnectToConsoleSession";
            this.cMenTreeConnectWithOptionsConnectToConsoleSession.Size = new System.Drawing.Size(231, 22);
            this.cMenTreeConnectWithOptionsConnectToConsoleSession.Text = "Connect to console session";
            this.cMenTreeConnectWithOptionsConnectToConsoleSession.Click += new System.EventHandler(this.cMenTreeConnectWithOptionsConnectToConsoleSession_Click);
            // 
            // cMenTreeConnectWithOptionsDontConnectToConsoleSession
            // 
            this.cMenTreeConnectWithOptionsDontConnectToConsoleSession.Name = "cMenTreeConnectWithOptionsDontConnectToConsoleSession";
            this.cMenTreeConnectWithOptionsDontConnectToConsoleSession.Size = new System.Drawing.Size(231, 22);
            this.cMenTreeConnectWithOptionsDontConnectToConsoleSession.Text = "Don\'t connect to console session";
            this.cMenTreeConnectWithOptionsDontConnectToConsoleSession.Visible = false;
            this.cMenTreeConnectWithOptionsDontConnectToConsoleSession.Click += new System.EventHandler(this.cMenTreeConnectWithOptionsDontConnectToConsoleSession_Click);
            // 
            // cMenTreeConnectWithOptionsConnectInFullscreen
            // 
            this.cMenTreeConnectWithOptionsConnectInFullscreen.Image = global::My.Resources.Resources.Fullscreen;
            this.cMenTreeConnectWithOptionsConnectInFullscreen.Name = "cMenTreeConnectWithOptionsConnectInFullscreen";
            this.cMenTreeConnectWithOptionsConnectInFullscreen.Size = new System.Drawing.Size(231, 22);
            this.cMenTreeConnectWithOptionsConnectInFullscreen.Text = "Connect in fullscreen";
            this.cMenTreeConnectWithOptionsConnectInFullscreen.Click += new System.EventHandler(this.cMenTreeConnectWithOptionsConnectInFullscreen_Click);
            // 
            // cMenTreeConnectWithOptionsChoosePanelBeforeConnecting
            // 
            this.cMenTreeConnectWithOptionsChoosePanelBeforeConnecting.Image = global::My.Resources.Resources.Panels;
            this.cMenTreeConnectWithOptionsChoosePanelBeforeConnecting.Name = "cMenTreeConnectWithOptionsChoosePanelBeforeConnecting";
            this.cMenTreeConnectWithOptionsChoosePanelBeforeConnecting.Size = new System.Drawing.Size(231, 22);
            this.cMenTreeConnectWithOptionsChoosePanelBeforeConnecting.Text = "Choose panel before connecting";
            this.cMenTreeConnectWithOptionsChoosePanelBeforeConnecting.Click += new System.EventHandler(this.cMenTreeConnectWithOptionsChoosePanelBeforeConnecting_Click);
            // 
            // cMenTreeDisconnect
            // 
            this.cMenTreeDisconnect.Image = global::My.Resources.Resources.Pause;
            this.cMenTreeDisconnect.Name = "cMenTreeDisconnect";
            this.cMenTreeDisconnect.Size = new System.Drawing.Size(186, 22);
            this.cMenTreeDisconnect.Text = "Disconnect";
            this.cMenTreeDisconnect.Click += new System.EventHandler(this.cMenTreeDisconnect_Click);
            // 
            // cMenTreeSep2
            // 
            this.cMenTreeSep2.Name = "cMenTreeSep2";
            this.cMenTreeSep2.Size = new System.Drawing.Size(183, 6);
            // 
            // cMenTreeToolsTransferFile
            // 
            this.cMenTreeToolsTransferFile.Image = global::My.Resources.Resources.SSHTransfer;
            this.cMenTreeToolsTransferFile.Name = "cMenTreeToolsTransferFile";
            this.cMenTreeToolsTransferFile.Size = new System.Drawing.Size(186, 22);
            this.cMenTreeToolsTransferFile.Text = "Transfer File (SSH)";
            this.cMenTreeToolsTransferFile.Click += new System.EventHandler(this.cMenTreeToolsTransferFile_Click);
            // 
            // checkTypicalPPortsToolStripMenuItem
            // 
            this.checkTypicalPPortsToolStripMenuItem.Image = global::My.Resources.Resources.HostStatus_Check;
            this.checkTypicalPPortsToolStripMenuItem.Name = "checkTypicalPPortsToolStripMenuItem";
            this.checkTypicalPPortsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.checkTypicalPPortsToolStripMenuItem.Text = "Check typical ports";
            this.checkTypicalPPortsToolStripMenuItem.Click += new System.EventHandler(this.checkTypicalPPortsToolStripMenuItem_Click);
            // 
            // cMenTreeToolsImportExport
            // 
            this.cMenTreeToolsImportExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cMenTreeToolsImportExportExportmRemoteXML,
            this.cMenTreeToolsImportExportImportmRemoteXML,
            this.cMenTreeToolsImportExportSep1,
            this.cMenTreeToolsImportExportImportFromAD,
            this.cMenTreeToolsImportExportImportFromRDPFiles,
            this.cMenTreeToolsImportExportImportFromPortScan});
            this.cMenTreeToolsImportExport.Name = "cMenTreeToolsImportExport";
            this.cMenTreeToolsImportExport.Size = new System.Drawing.Size(186, 22);
            this.cMenTreeToolsImportExport.Text = "Import/Export";
            // 
            // cMenTreeToolsImportExportExportmRemoteXML
            // 
            this.cMenTreeToolsImportExportExportmRemoteXML.Image = global::My.Resources.Resources.Connections_SaveAs;
            this.cMenTreeToolsImportExportExportmRemoteXML.Name = "cMenTreeToolsImportExportExportmRemoteXML";
            this.cMenTreeToolsImportExportExportmRemoteXML.Size = new System.Drawing.Size(204, 22);
            this.cMenTreeToolsImportExportExportmRemoteXML.Text = "Export mRemote XML";
            this.cMenTreeToolsImportExportExportmRemoteXML.Click += new System.EventHandler(this.cMenTreeToolsImportExportExportmRemoteXML_Click);
            // 
            // cMenTreeToolsImportExportImportmRemoteXML
            // 
            this.cMenTreeToolsImportExportImportmRemoteXML.Image = global::My.Resources.Resources.Connections_Load;
            this.cMenTreeToolsImportExportImportmRemoteXML.Name = "cMenTreeToolsImportExportImportmRemoteXML";
            this.cMenTreeToolsImportExportImportmRemoteXML.Size = new System.Drawing.Size(204, 22);
            this.cMenTreeToolsImportExportImportmRemoteXML.Text = "Import mRemote XML";
            this.cMenTreeToolsImportExportImportmRemoteXML.Click += new System.EventHandler(this.cMenTreeToolsImportExportImportmRemoteXML_Click);
            // 
            // cMenTreeToolsImportExportSep1
            // 
            this.cMenTreeToolsImportExportSep1.Name = "cMenTreeToolsImportExportSep1";
            this.cMenTreeToolsImportExportSep1.Size = new System.Drawing.Size(201, 6);
            // 
            // cMenTreeToolsImportExportImportFromAD
            // 
            this.cMenTreeToolsImportExportImportFromAD.Image = global::My.Resources.Resources.ActiveDirectory;
            this.cMenTreeToolsImportExportImportFromAD.Name = "cMenTreeToolsImportExportImportFromAD";
            this.cMenTreeToolsImportExportImportFromAD.Size = new System.Drawing.Size(204, 22);
            this.cMenTreeToolsImportExportImportFromAD.Text = "Import from Active Directory";
            this.cMenTreeToolsImportExportImportFromAD.Click += new System.EventHandler(this.cMenTreeToolsImportExportImportFromAD_Click);
            // 
            // cMenTreeToolsImportExportImportFromRDPFiles
            // 
            this.cMenTreeToolsImportExportImportFromRDPFiles.Image = global::My.Resources.Resources.RDP;
            this.cMenTreeToolsImportExportImportFromRDPFiles.Name = "cMenTreeToolsImportExportImportFromRDPFiles";
            this.cMenTreeToolsImportExportImportFromRDPFiles.Size = new System.Drawing.Size(204, 22);
            this.cMenTreeToolsImportExportImportFromRDPFiles.Text = "Import from .RDP file(s)";
            this.cMenTreeToolsImportExportImportFromRDPFiles.Click += new System.EventHandler(this.cMenTreeToolsImportExportImportFromRDPFiles_Click);
            // 
            // cMenTreeToolsImportExportImportFromPortScan
            // 
            this.cMenTreeToolsImportExportImportFromPortScan.Image = global::My.Resources.Resources.PortScan;
            this.cMenTreeToolsImportExportImportFromPortScan.Name = "cMenTreeToolsImportExportImportFromPortScan";
            this.cMenTreeToolsImportExportImportFromPortScan.Size = new System.Drawing.Size(204, 22);
            this.cMenTreeToolsImportExportImportFromPortScan.Text = "Import from Port Scan";
            this.cMenTreeToolsImportExportImportFromPortScan.Click += new System.EventHandler(this.cMenTreeToolsImportExportImportFromPortScan_Click);
            // 
            // cMenTreeToolsSort
            // 
            this.cMenTreeToolsSort.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cMenTreeToolsSortAscending,
            this.cMenTreeToolsSortDescending});
            this.cMenTreeToolsSort.Name = "cMenTreeToolsSort";
            this.cMenTreeToolsSort.Size = new System.Drawing.Size(186, 22);
            this.cMenTreeToolsSort.Text = "Sort";
            // 
            // cMenTreeToolsSortAscending
            // 
            this.cMenTreeToolsSortAscending.Image = global::My.Resources.Resources.Sort_AZ;
            this.cMenTreeToolsSortAscending.Name = "cMenTreeToolsSortAscending";
            this.cMenTreeToolsSortAscending.Size = new System.Drawing.Size(157, 22);
            this.cMenTreeToolsSortAscending.Text = "Ascending (A-Z)";
            this.cMenTreeToolsSortAscending.Click += new System.EventHandler(this.cMenTreeToolsSortAscending_Click);
            // 
            // cMenTreeToolsSortDescending
            // 
            this.cMenTreeToolsSortDescending.Image = global::My.Resources.Resources.Sort_ZA;
            this.cMenTreeToolsSortDescending.Name = "cMenTreeToolsSortDescending";
            this.cMenTreeToolsSortDescending.Size = new System.Drawing.Size(157, 22);
            this.cMenTreeToolsSortDescending.Text = "Descending (Z-A)";
            this.cMenTreeToolsSortDescending.Click += new System.EventHandler(this.cMenTreeToolsSortDescending_Click);
            // 
            // cMenTreeToolsExternalApps
            // 
            this.cMenTreeToolsExternalApps.Image = global::My.Resources.Resources.ExtApp;
            this.cMenTreeToolsExternalApps.Name = "cMenTreeToolsExternalApps";
            this.cMenTreeToolsExternalApps.Size = new System.Drawing.Size(186, 22);
            this.cMenTreeToolsExternalApps.Text = "External Applications";
            this.cMenTreeToolsExternalApps.Click += new System.EventHandler(this.cMenTreeToolsExternalApps_Click);
            // 
            // cMenTreeSep3
            // 
            this.cMenTreeSep3.Name = "cMenTreeSep3";
            this.cMenTreeSep3.Size = new System.Drawing.Size(183, 6);
            // 
            // cMenTreeDuplicate
            // 
            this.cMenTreeDuplicate.Image = global::My.Resources.Resources.Connection_Duplicate;
            this.cMenTreeDuplicate.Name = "cMenTreeDuplicate";
            this.cMenTreeDuplicate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.cMenTreeDuplicate.Size = new System.Drawing.Size(186, 22);
            this.cMenTreeDuplicate.Text = "Duplicate";
            this.cMenTreeDuplicate.Click += new System.EventHandler(this.cMenTreeDuplicate_Click);
            // 
            // cMenTreeRename
            // 
            this.cMenTreeRename.Image = global::My.Resources.Resources.Rename;
            this.cMenTreeRename.Name = "cMenTreeRename";
            this.cMenTreeRename.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.cMenTreeRename.Size = new System.Drawing.Size(186, 22);
            this.cMenTreeRename.Text = "Rename";
            this.cMenTreeRename.Click += new System.EventHandler(this.cMenTreeRename_Click);
            // 
            // cMenTreeDelete
            // 
            this.cMenTreeDelete.Image = global::My.Resources.Resources.Delete;
            this.cMenTreeDelete.Name = "cMenTreeDelete";
            this.cMenTreeDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.cMenTreeDelete.Size = new System.Drawing.Size(186, 22);
            this.cMenTreeDelete.Text = "Delete";
            this.cMenTreeDelete.Click += new System.EventHandler(this.cMenTreeDelete_Click);
            // 
            // cMenTreeSep4
            // 
            this.cMenTreeSep4.Name = "cMenTreeSep4";
            this.cMenTreeSep4.Size = new System.Drawing.Size(183, 6);
            // 
            // cMenTreeMoveUp
            // 
            this.cMenTreeMoveUp.Image = global::My.Resources.Resources.Arrow_Up;
            this.cMenTreeMoveUp.Name = "cMenTreeMoveUp";
            this.cMenTreeMoveUp.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.cMenTreeMoveUp.Size = new System.Drawing.Size(186, 22);
            this.cMenTreeMoveUp.Text = "Move up";
            this.cMenTreeMoveUp.Click += new System.EventHandler(this.cMenTreeMoveUp_Click);
            // 
            // cMenTreeMoveDown
            // 
            this.cMenTreeMoveDown.Image = global::My.Resources.Resources.Arrow_Down;
            this.cMenTreeMoveDown.Name = "cMenTreeMoveDown";
            this.cMenTreeMoveDown.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.cMenTreeMoveDown.Size = new System.Drawing.Size(186, 22);
            this.cMenTreeMoveDown.Text = "Move down";
            this.cMenTreeMoveDown.Click += new System.EventHandler(this.cMenTreeMoveDown_Click);
            // 
            // imgListTree
            // 
            this.imgListTree.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imgListTree.ImageSize = new System.Drawing.Size(16, 16);
            this.imgListTree.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pnlConnections
            // 
            this.pnlConnections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlConnections.Controls.Add(this.PictureBox1);
            this.pnlConnections.Controls.Add(this.txtSearch);
            this.pnlConnections.Controls.Add(this.tvConnections);
            this.pnlConnections.Location = new System.Drawing.Point(0, 25);
            this.pnlConnections.Name = "pnlConnections";
            this.pnlConnections.Size = new System.Drawing.Size(210, 428);
            this.pnlConnections.TabIndex = 9;
            // 
            // PictureBox1
            // 
            this.PictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox1.Image = global::My.Resources.Resources.Search;
            this.PictureBox1.Location = new System.Drawing.Point(0, 411);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(16, 16);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PictureBox1.TabIndex = 1;
            this.PictureBox1.TabStop = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Location = new System.Drawing.Point(21, 412);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(187, 13);
            this.txtSearch.TabIndex = 30;
            this.txtSearch.TabStop = false;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // msMain
            // 
            this.msMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mMenAddConnection,
            this.mMenAddFolder,
            this.mMenView,
            this.mMenSortAscending});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.msMain.ShowItemToolTips = true;
            this.msMain.Size = new System.Drawing.Size(210, 24);
            this.msMain.TabIndex = 10;
            this.msMain.Text = "MenuStrip1";
            this.msMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.msMain_ItemClicked);
            // 
            // mMenAddConnection
            // 
            this.mMenAddConnection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mMenAddConnection.Image = global::My.Resources.Resources.Connection_Add;
            this.mMenAddConnection.Name = "mMenAddConnection";
            this.mMenAddConnection.Size = new System.Drawing.Size(28, 20);
            this.mMenAddConnection.Click += new System.EventHandler(this.cMenTreeAddConnection_Click);
            // 
            // mMenAddFolder
            // 
            this.mMenAddFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mMenAddFolder.Image = global::My.Resources.Resources.Folder_Add;
            this.mMenAddFolder.Name = "mMenAddFolder";
            this.mMenAddFolder.Size = new System.Drawing.Size(28, 20);
            this.mMenAddFolder.Click += new System.EventHandler(this.cMenTreeAddFolder_Click);
            // 
            // mMenView
            // 
            this.mMenView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mMenView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mMenViewExpandAllFolders,
            this.mMenViewCollapseAllFolders});
            this.mMenView.Image = global::My.Resources.Resources.View;
            this.mMenView.Name = "mMenView";
            this.mMenView.Size = new System.Drawing.Size(28, 20);
            this.mMenView.Text = "&View";
            // 
            // mMenViewExpandAllFolders
            // 
            this.mMenViewExpandAllFolders.Image = global::My.Resources.Resources.Expand;
            this.mMenViewExpandAllFolders.Name = "mMenViewExpandAllFolders";
            this.mMenViewExpandAllFolders.Size = new System.Drawing.Size(161, 22);
            this.mMenViewExpandAllFolders.Text = "Expand all folders";
            this.mMenViewExpandAllFolders.Click += new System.EventHandler(this.mMenViewExpandAllFolders_Click);
            // 
            // mMenViewCollapseAllFolders
            // 
            this.mMenViewCollapseAllFolders.Image = global::My.Resources.Resources.Collapse;
            this.mMenViewCollapseAllFolders.Name = "mMenViewCollapseAllFolders";
            this.mMenViewCollapseAllFolders.Size = new System.Drawing.Size(161, 22);
            this.mMenViewCollapseAllFolders.Text = "Collapse all folders";
            this.mMenViewCollapseAllFolders.Click += new System.EventHandler(this.mMenViewCollapseAllFolders_Click);
            // 
            // mMenSortAscending
            // 
            this.mMenSortAscending.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mMenSortAscending.Image = global::My.Resources.Resources.Sort_AZ;
            this.mMenSortAscending.Name = "mMenSortAscending";
            this.mMenSortAscending.Size = new System.Drawing.Size(28, 20);
            this.mMenSortAscending.Visible = false;
            this.mMenSortAscending.Click += new System.EventHandler(this.mMenSortAscending_Click);
            // 
            // Tree
            // 
            this.ClientSize = new System.Drawing.Size(210, 453);
            this.Controls.Add(this.msMain);
            this.Controls.Add(this.pnlConnections);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideOnClose = true;
            this.Icon = global::My.Resources.Resources.Root_Icon;
            this.Name = "Tree";
            this.TabText = "Connections";
            this.Text = "Connections";
            this.Load += new System.EventHandler(this.Tree_Load);
            this.cMenTree.ResumeLayout(false);
            this.pnlConnections.ResumeLayout(false);
            this.pnlConnections.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

                }

                #endregion

                #region Form Stuff

                private void Tree_Load(object sender, System.EventArgs e)
                {
                    ApplyLanguage();
                }

                private void ApplyLanguage()
                {
                    cMenTreeAddConnection.Text = Language.strAddConnection;
                    cMenTreeAddFolder.Text = Language.strAddFolder;
                    cMenTreeConnect.Text = Language.strConnect;
                    cMenTreeConnectWithOptions.Text = Language.strConnectWithOptions;
                    cMenTreeConnectWithOptionsConnectToConsoleSession.Text = Language.strConnectToConsoleSession;
                    cMenTreeConnectWithOptionsConnectInFullscreen.Text = Language.strConnectInFullscreen;
                    cMenTreeConnectWithOptionsChoosePanelBeforeConnecting.Text = Language.strChoosePanelBeforeConnecting;
                    cMenTreeDisconnect.Text = Language.strMenuDisconnect;
                    cMenTreeToolsTransferFile.Text = Language.strMenuTransferFile;
                    cMenTreeToolsImportExport.Text = Language.strImportExport;
                    cMenTreeToolsImportExportExportmRemoteXML.Text = Language.strExportmRemoteXML;
                    cMenTreeToolsImportExportImportmRemoteXML.Text = Language.strImportmRemoteXML;
                    cMenTreeToolsImportExportImportFromAD.Text = Language.strImportAD;
                    cMenTreeToolsImportExportImportFromRDPFiles.Text = Language.strImportRDPFiles;
                    cMenTreeToolsImportExportImportFromPortScan.Text = Language.strImportPortScan;
                    cMenTreeToolsSort.Text = Language.strSort;
                    cMenTreeToolsSortAscending.Text = Language.strSortAsc;
                    cMenTreeToolsSortDescending.Text = Language.strSortDesc;
                    cMenTreeToolsExternalApps.Text = Language.strMenuExternalTools;
                    cMenTreeDuplicate.Text = Language.strDuplicate;
                    cMenTreeRename.Text = Language.strRename;
                    cMenTreeDelete.Text = Language.strMenuDelete;
                    cMenTreeMoveUp.Text = Language.strMoveUp;
                    cMenTreeMoveDown.Text = Language.strMoveDown;
                    mMenAddConnection.ToolTipText = Language.strAddConnection;
                    mMenAddFolder.ToolTipText = Language.strAddFolder;
                    mMenView.ToolTipText = Language.strMenuView.Replace("&", "");
                    mMenViewExpandAllFolders.Text = Language.strExpandAllFolders;
                    mMenViewCollapseAllFolders.Text = Language.strCollapseAllFolders;
                    mMenSortAscending.ToolTipText = Language.strSortAsc;
                    TabText = Language.strConnections;
                    Text = Language.strConnections;
                }

                #endregion

                #region Public Methods

                public Tree(DockContent Panel)
                {
                    this.WindowType = Type.Tree;
                    this.DockPnl = Panel;
                    this.InitializeComponent();
                    this.FillImageList();

                    _DescriptionTooltip = new ToolTip();
                    _DescriptionTooltip.InitialDelay = 300;
                    _DescriptionTooltip.ReshowDelay = 0;
                }

                public void InitialRefresh()
                {
                    this.tvConnections_AfterSelect(this.tvConnections,
                                                   new TreeViewEventArgs(this.tvConnections.SelectedNode,
                                                                         TreeViewAction.ByMouse));
                }

                #endregion

                #region Public Properties

                private ToolTip _DescriptionTooltip;

                public ToolTip DescriptionTooltip
                {
                    get { return _DescriptionTooltip; }
                    set { _DescriptionTooltip = value; }
                }

                #endregion

                #region Private Methods

                private void FillImageList()
                {
                    try
                    {
                        this.imgListTree.Images.Add(global::My.Resources.Resources.Root);
                        this.imgListTree.Images.Add(global::My.Resources.Resources.Folder);
                        this.imgListTree.Images.Add(global::My.Resources.Resources.Play);
                        this.imgListTree.Images.Add(global::My.Resources.Resources.Pause);
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("FillImageList (UI.Window.Tree) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void tvConnections_BeforeLabelEdit(object sender, System.Windows.Forms.NodeLabelEditEventArgs e)
                {
                    cMenTreeDelete.ShortcutKeys = Keys.None;
                }

                private void tvConnections_AfterLabelEdit(object sender, System.Windows.Forms.NodeLabelEditEventArgs e)
                {
                    try
                    {
                        cMenTreeDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
                        if (Settings.Default.SetHostnameLikeDisplayName)
                        {
                            if (e.Node.Tag is Info)
                            {
                                ((Info) e.Node.Tag).Hostname = e.Label;
                            }
                        }

                        Node.FinishRenameSelectedNode(e.Label);
                        Runtime.Windows.configForm.pGrid_SelectedObjectChanged();
                        this.ShowHideTreeContextMenuItems(e.Node);
                        Runtime.SaveConnectionsBG();
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("tvConnections_AfterLabelEdit (UI.Window.Tree) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void tvConnections_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
                {
                    try
                    {
                        if (Node.GetNodeType(e.Node) == Node.Type.Connection)
                        {
                            Runtime.Windows.configForm.SetPropertyGridObject(e.Node.Tag);
                            Runtime.Windows.sessionsForm.CurrentHost = (e.Node.Tag as Info).Hostname;
                        }
                        else if (Node.GetNodeType(e.Node) == Node.Type.Container)
                        {
                            Runtime.Windows.configForm.SetPropertyGridObject(
                                (e.Node.Tag as Container.Info).ConnectionInfo);
                        }
                        else if (Node.GetNodeType(e.Node) == Node.Type.Root)
                        {
                            Runtime.Windows.configForm.SetPropertyGridObject(e.Node.Tag);
                        }
                        else
                        {
                            return;
                        }

                        Runtime.Windows.configForm.pGrid_SelectedObjectChanged();
                        this.ShowHideTreeContextMenuItems(e.Node);
                        Runtime.Windows.sessionsForm.GetSessionsAuto();

                        Runtime.LastSelected = Node.GetConstantID(e.Node);
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("tvConnections_AfterSelect (UI.Window.Tree) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void tvConnections_NodeMouseClick(object sender,
                                                          System.Windows.Forms.TreeNodeMouseClickEventArgs e)
                {
                    try
                    {
                        this.ShowHideTreeContextMenuItems(this.tvConnections.SelectedNode);
                        this.tvConnections.SelectedNode = e.Node;

                        if (e.Button == System.Windows.Forms.MouseButtons.Left)
                        {
                            if (Settings.Default.SingleClickOnConnectionOpensIt &&
                                Node.GetNodeType(e.Node) == Node.Type.Connection)
                            {
                                Runtime.OpenConnection();
                            }

                            if (Settings.Default.SingleClickSwitchesToOpenConnection &&
                                Node.GetNodeType(e.Node) == Node.Type.Connection)
                            {
                                Runtime.SwitchToOpenConnection((Info) e.Node.Tag);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("tvConnections_NodeMouseClick (UI.Window.Tree) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void tvConnections_NodeMouseDoubleClick(object sender,
                                                                System.Windows.Forms.TreeNodeMouseClickEventArgs e)
                {
                    if (Node.GetNodeType(Node.SelectedNode) == Node.Type.Connection)
                    {
                        Runtime.OpenConnection();
                    }
                }

                private void tvConnections_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
                {
                    try
                    {
                        Node.SetNodeToolTip(e, this.DescriptionTooltip);
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("tvConnections_MouseMove (UI.Window.Tree) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private bool IsConnectionOpen(Info[] ConnectionInfos)
                {
                    try
                    {
                        if (ConnectionInfos != null)
                        {
                            if (ConnectionInfos.Any(conI => conI.OpenConnections.Count > 0))
                            {
                                return true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("IsConnectionOpen (UI.Window.Tree) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }

                    return false;
                }

                private void ShowHideTreeContextMenuItems(TreeNode tNode)
                {
                    try
                    {
                        this.cMenTree.Enabled = true;

                        if (tNode == null)
                        {
                            return;
                        }

                        if (Node.GetNodeType(tNode) == Node.Type.Connection)
                        {
                            Info conI = (Info) tNode.Tag;

                            this.cMenTreeConnect.Enabled = true;
                            this.cMenTreeConnectWithOptions.Enabled = true;

                            if ((tNode.Tag as Info).OpenConnections.Count > 0)
                            {
                                this.cMenTreeDisconnect.Enabled = true;
                            }
                            else
                            {
                                this.cMenTreeDisconnect.Enabled = false;
                            }

                            if (conI.Protocol == Protocols.SSH1 || conI.Protocol == Protocols.SSH2)
                            {
                                this.cMenTreeToolsTransferFile.Enabled = true;
                            }
                            else
                            {
                                this.cMenTreeToolsTransferFile.Enabled = false;
                            }

                            if (conI.Protocol == Protocols.RDP)
                            {
                                this.cMenTreeConnectWithOptionsConnectInFullscreen.Enabled = true;
                                this.cMenTreeConnectWithOptionsConnectToConsoleSession.Enabled = true;
                            }
                            else if (conI.Protocol == Protocols.ICA)
                            {
                                this.cMenTreeConnectWithOptionsConnectInFullscreen.Enabled = true;
                                this.cMenTreeConnectWithOptionsConnectToConsoleSession.Enabled = false;
                            }
                            else
                            {
                                this.cMenTreeConnectWithOptionsConnectInFullscreen.Enabled = false;
                                this.cMenTreeConnectWithOptionsConnectToConsoleSession.Enabled = false;
                            }

                            this.cMenTreeConnectWithOptionsChoosePanelBeforeConnecting.Enabled = true;

                            this.cMenTreeToolsImportExport.Enabled = false;

                            this.cMenTreeToolsExternalApps.Enabled = true;

                            this.cMenTreeDuplicate.Enabled = true;
                            this.cMenTreeDelete.Enabled = true;

                            this.cMenTreeMoveUp.Enabled = true;
                            this.cMenTreeMoveDown.Enabled = true;
                        }
                        else if (Node.GetNodeType(tNode) == Node.Type.Container)
                        {
                            this.cMenTreeConnect.Enabled = true;
                            this.cMenTreeConnectWithOptions.Enabled = true;
                            this.cMenTreeConnectWithOptionsConnectInFullscreen.Enabled = false;
                            this.cMenTreeConnectWithOptionsConnectToConsoleSession.Enabled = false;
                            this.cMenTreeConnectWithOptionsChoosePanelBeforeConnecting.Enabled = true;
                            this.cMenTreeDisconnect.Enabled = false;

                            foreach (TreeNode n in tNode.Nodes)
                            {
                                if (n.Tag is Info)
                                {
                                    Info cI = (Info) n.Tag;
                                    if (cI.OpenConnections.Count > 0)
                                    {
                                        this.cMenTreeDisconnect.Enabled = true;
                                        break;
                                    }
                                }
                            }

                            this.cMenTreeToolsTransferFile.Enabled = false;

                            this.cMenTreeToolsImportExport.Enabled = true;
                            this.cMenTreeToolsImportExportExportmRemoteXML.Enabled = true;
                            this.cMenTreeToolsImportExportImportFromAD.Enabled = true;
                            this.cMenTreeToolsImportExportImportmRemoteXML.Enabled = true;

                            this.cMenTreeToolsExternalApps.Enabled = false;

                            this.cMenTreeDuplicate.Enabled = true;
                            this.cMenTreeDelete.Enabled = true;

                            this.cMenTreeMoveUp.Enabled = true;
                            this.cMenTreeMoveDown.Enabled = true;
                        }
                        else if (Node.GetNodeType(tNode) == Node.Type.Root)
                        {
                            this.cMenTreeConnect.Enabled = false;
                            this.cMenTreeConnectWithOptions.Enabled = false;
                            this.cMenTreeConnectWithOptionsConnectInFullscreen.Enabled = false;
                            this.cMenTreeConnectWithOptionsConnectToConsoleSession.Enabled = false;
                            this.cMenTreeConnectWithOptionsChoosePanelBeforeConnecting.Enabled = false;
                            this.cMenTreeDisconnect.Enabled = false;

                            this.cMenTreeToolsTransferFile.Enabled = false;

                            this.cMenTreeToolsImportExport.Enabled = true;
                            this.cMenTreeToolsImportExportExportmRemoteXML.Enabled = false;
                            this.cMenTreeToolsImportExportImportFromAD.Enabled = true;
                            this.cMenTreeToolsImportExportImportmRemoteXML.Enabled = true;

                            this.cMenTreeToolsExternalApps.Enabled = false;

                            this.cMenTreeDuplicate.Enabled = false;
                            this.cMenTreeDelete.Enabled = false;

                            this.cMenTreeMoveUp.Enabled = false;
                            this.cMenTreeMoveDown.Enabled = false;
                        }
                        else
                        {
                            this.cMenTree.Enabled = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("ShowHideTreeContextMenuItems (UI.Window.Tree) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                #endregion

                #region Drag and Drop

                private void tvConnections_DragDrop(object sender, DragEventArgs e)
                {
                    try
                    {
                        if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", true))
                        {
                            TreeView selectedTreeview = (TreeView) sender;
                            TreeNode dropNode = (TreeNode) e.Data.GetData("System.Windows.Forms.TreeNode");
                            TreeNode targetNode = selectedTreeview.SelectedNode;
                            if (targetNode==null)
                            {
                                return;
                            }
                            if (((dropNode != targetNode) && (Node.GetNodeType(dropNode) != Node.Type.Root)) &&
                                (dropNode != targetNode.Parent))
                            {
                                dropNode.Remove();
                                if ((Node.GetNodeType(targetNode) == Node.Type.Root) |
                                    (Node.GetNodeType(targetNode) == Node.Type.Container))
                                {
                                    targetNode.Nodes.Insert(0, dropNode);
                                }
                                else
                                {
                                    targetNode.Parent.Nodes.Insert(targetNode.Index + 1, dropNode);
                                }
                                if ((Node.GetNodeType(dropNode) == Node.Type.Connection) |
                                    (Node.GetNodeType(dropNode) == Node.Type.Container))
                                {
                                    if (Node.GetNodeType(dropNode.Parent) == Node.Type.Container)
                                    {
                                        NewLateBinding.LateSetComplex(dropNode.Tag, null, "Parent",
                                                                      new object[]
                                                                          {
                                                                              RuntimeHelpers.GetObjectValue(
                                                                                  dropNode.Parent.Tag)
                                                                          }, null, null,
                                                                      false, true);
                                    }
                                    else if (Node.GetNodeType(dropNode.Parent) == Node.Type.Root)
                                    {
                                        NewLateBinding.LateSetComplex(dropNode.Tag, null, "Parent", new object[] {null},
                                                                      null, null, false, true);
                                        if (Node.GetNodeType(dropNode) == Node.Type.Connection)
                                        {
                                            NewLateBinding.LateSetComplex(dropNode.Tag, null, "Inherit",
                                                                          new object[]
                                                                              {
                                                                                  new Info.Inheritance(
                                                                                      RuntimeHelpers.GetObjectValue(
                                                                                          dropNode.Tag), false)
                                                                              }, null,
                                                                          null, false, true);
                                        }
                                        else if (Node.GetNodeType(dropNode) == Node.Type.Container)
                                        {
                                            NewLateBinding.LateSetComplex(
                                                NewLateBinding.LateGet(dropNode.Tag, null, "ConnectionInfo",
                                                                       new object[0], null, null, null), null, "Inherit",
                                                new object[]
                                                    {
                                                        new Info.Inheritance(
                                                            RuntimeHelpers.GetObjectValue(
                                                                NewLateBinding.LateGet(dropNode.Tag, null,
                                                                                       "ConnectionInfo", new object[0],
                                                                                       null, null, null)), false)
                                                    }, null,
                                                null, false, true);
                                        }
                                    }
                                }
                                dropNode.EnsureVisible();
                                selectedTreeview.SelectedNode = dropNode;
                                Runtime.SaveConnectionsBG();
                            }
                        }
                    }
                    catch (Exception exception1)
                    {
                        ProjectData.SetProjectError(exception1);
                        Exception ex = exception1;
                        Runtime.MessageCollector.AddMessage(MessageClass.ErrorMsg,
                                                            "tvConnections_DragDrop (UI.Window.Tree) failed\r\n" +
                                                            ex.Message, true);
                        ProjectData.ClearProjectError();
                    }
                }


                private void tvConnections_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
                {
                    try
                    {
                        //See if there is a TreeNode being dragged
                        if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", true))
                        {
                            //TreeNode found allow move effect
                            e.Effect = DragDropEffects.Move;
                        }
                        else
                        {
                            //No TreeNode found, prevent move
                            e.Effect = DragDropEffects.None;
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("tvConnections_DragEnter (UI.Window.Tree) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void tvConnections_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
                {
                    try
                    {
                        //Check that there is a TreeNode being dragged
                        if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", true) == false)
                        {
                            return;
                        }

                        //Get the TreeView raising the event (incase multiple on form)
                        TreeView selectedTreeview = (TreeView) sender;

                        //As the mouse moves over nodes, provide feedback to
                        //the user by highlighting the node that is the
                        //current drop target
                        Point pt = ((TreeView) sender).PointToClient(new Point(e.X, e.Y));
                        TreeNode targetNode = selectedTreeview.GetNodeAt(pt);

                        //See if the targetNode is currently selected,
                        //if so no need to validate again
                        if (!(selectedTreeview.SelectedNode == targetNode))
                        {
                            //Select the node currently under the cursor
                            selectedTreeview.SelectedNode = targetNode;

                            //Check that the selected node is not the dropNode and
                            //also that it is not a child of the dropNode and
                            //therefore an invalid target
                            TreeNode dropNode =
                                (System.Windows.Forms.TreeNode) (e.Data.GetData("System.Windows.Forms.TreeNode"));

                            while (!(targetNode == null))
                            {
                                if (targetNode == dropNode)
                                {
                                    e.Effect = DragDropEffects.None;
                                    return;
                                }

                                targetNode = targetNode.Parent;
                            }
                        }

                        //Currently selected node is a suitable target
                        e.Effect = DragDropEffects.Move;
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("tvConnections_DragOver (UI.Window.Tree) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void tvConnections_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
                {
                    try
                    {
                        //Set the drag node and initiate the DragDrop
                        DoDragDrop(e.Item, DragDropEffects.Move);
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("tvConnections_ItemDrag (UI.Window.Tree) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                #endregion

                #region Tree Context Menu

                private void cMenTreeAddConnection_Click(System.Object sender, System.EventArgs e)
                {
                    AddConnection();
                    Runtime.SaveConnectionsBG();
                }

                private void cMenTreeAddFolder_Click(System.Object sender, System.EventArgs e)
                {
                    this.AddFolder();
                    Runtime.SaveConnectionsBG();
                }

                private void cMenTreeConnect_Click(System.Object sender, System.EventArgs e)
                {
                    Runtime.OpenConnection(Info.Force.DoNotJump);
                }

                private void cMenTreeConnectWithOptionsConnectToConsoleSession_Click(System.Object sender,
                                                                                     System.EventArgs e)
                {
                    Runtime.OpenConnection(Info.Force.UseConsoleSession | Info.Force.DoNotJump);
                }

                private void cMenTreeConnectWithOptionsDontConnectToConsoleSession_Click(System.Object sender,
                                                                                         System.EventArgs e)
                {
                    Runtime.OpenConnection(Info.Force.DontUseConsoleSession | Info.Force.DoNotJump);
                }

                private void cMenTreeConnectWithOptionsConnectInFullscreen_Click(System.Object sender,
                                                                                 System.EventArgs e)
                {
                    Runtime.OpenConnection(Info.Force.Fullscreen | Info.Force.DoNotJump);
                }

                private void cMenTreeConnectWithOptionsChoosePanelBeforeConnecting_Click(System.Object sender,
                                                                                         System.EventArgs e)
                {
                    Runtime.OpenConnection(Info.Force.OverridePanel | Info.Force.DoNotJump);
                }

                private void cMenTreeDisconnect_Click(System.Object sender, System.EventArgs e)
                {
                    this.DisconnectConnection();
                }

                private void cMenTreeToolsTransferFile_Click(System.Object sender, System.EventArgs e)
                {
                    this.SSHTransferFile();
                }

                private void cMenTreeToolsImportExportExportmRemoteXML_Click(System.Object sender, System.EventArgs e)
                {
                    this.ExportXML();
                }

                private void cMenTreeToolsImportExportImportmRemoteXML_Click(System.Object sender, System.EventArgs e)
                {
                    this.ImportXML();
                }

                private void cMenTreeToolsImportExportImportFromAD_Click(System.Object sender, System.EventArgs e)
                {
                    this.ImportFromAD();
                }

                private void cMenTreeToolsImportExportImportFromRDPFiles_Click(System.Object sender, System.EventArgs e)
                {
                    this.ImportFromRDPFiles();
                }

                private void cMenTreeToolsImportExportImportFromPortScan_Click(System.Object sender, System.EventArgs e)
                {
                    this.ImportFromPortScan();
                }

                private void mMenSortAscending_Click(System.Object sender, System.EventArgs e)
                {
                    this.tvConnections.BeginUpdate();
                    Node.Sort(this.tvConnections.Nodes[0], Tools.Controls.TreeNodeSorter.SortType.Ascending);
                    this.tvConnections.EndUpdate();
                    Runtime.SaveConnectionsBG();
                }

                private void cMenTreeToolsSortAscending_Click(System.Object sender, System.EventArgs e)
                {
                    this.tvConnections.BeginUpdate();
                    Node.Sort(this.tvConnections.SelectedNode, Tools.Controls.TreeNodeSorter.SortType.Ascending);
                    this.tvConnections.EndUpdate();
                    Runtime.SaveConnectionsBG();
                }

                private void cMenTreeToolsSortDescending_Click(System.Object sender, System.EventArgs e)
                {
                    this.tvConnections.BeginUpdate();
                    Node.Sort(this.tvConnections.SelectedNode, Tools.Controls.TreeNodeSorter.SortType.Descending);
                    this.tvConnections.EndUpdate();
                    Runtime.SaveConnectionsBG();
                }

                private void cMenTree_DropDownOpening(object sender, System.EventArgs e)
                {
                    AddExternalApps();
                }

                private void cMenTreeToolsExternalAppsEntry_Click(object sender, System.EventArgs e)
                {
                    StartExternalApp((ExternalTool) ((System.Windows.Forms.ToolStripMenuItem) sender).Tag);
                }

                private void cMenTreeDuplicate_Click(System.Object sender, System.EventArgs e)
                {
                    Node.CloneNode(tvConnections.SelectedNode);
                    Runtime.SaveConnectionsBG();
                }

                private void cMenTreeRename_Click(System.Object sender, System.EventArgs e)
                {
                    Node.StartRenameSelectedNode();
                    Runtime.SaveConnectionsBG();
                }

                private void cMenTreeDelete_Click(System.Object sender, System.EventArgs e)
                {
                    Node.DeleteSelectedNode();
                    Runtime.SaveConnectionsBG();
                }

                private void cMenTreeMoveUp_Click(System.Object sender, System.EventArgs e)
                {
                    Node.MoveNodeUp();
                    Runtime.SaveConnectionsBG();
                }

                private void cMenTreeMoveDown_Click(System.Object sender, System.EventArgs e)
                {
                    Node.MoveNodeDown();
                    Runtime.SaveConnectionsBG();
                }

                #endregion

                #region Context Menu Actions

                public void AddConnection(string host,int port, Protocols prot)
                {
                    try
                    {
                        TreeNode nNode = Node.AddNode(Node.Type.Connection, host + " - " + prot);

                        if (nNode != null)
                        {
                            Info nConI = new Info();


                            if (tvConnections.Nodes.Count < 1)
                            {
                                System.Windows.Forms.TreeNode treeNode1 =
                                    new System.Windows.Forms.TreeNode("Connections");
                                treeNode1.Name = "nodeRoot";
                                treeNode1.Text = "Connections";
                                this.tvConnections.Nodes.AddRange(new[] { treeNode1 });
                            }

                            if (this.tvConnections.SelectedNode == null)
                            {
                                this.tvConnections.SelectedNode = this.tvConnections.Nodes[0];
                            }
                            if (this.tvConnections.SelectedNode.Tag is Container.Info)
                            {
                                nConI.Parent = this.tvConnections.SelectedNode.Tag;
                            }
                            else
                            {
                                nConI.Inherit.TurnOffInheritanceCompletely();
                            }

                            nConI.Name = host + " - " + port;
                            nConI.Protocol = prot;
                            nConI.Hostname = host;
                            nConI.Port = port;
                            nConI.TreeNode = nNode;
                            nNode.Tag = nConI;
                            Runtime.ConnectionList.Add(nConI);

                            if (Node.GetNodeType(this.tvConnections.SelectedNode) == Node.Type.Connection)
                            {
                                this.tvConnections.SelectedNode.Parent.Nodes.Add(nNode);
                            }
                            else
                            {
                                this.tvConnections.SelectedNode.Nodes.Add(nNode);
                            }

                            this.tvConnections.SelectedNode = nNode;
                            //this.tvConnections.SelectedNode.BeginEdit();
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("AddConnectionEx (UI.Window.Tree) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                public void AddConnection()
                {
                    try
                    {
                        TreeNode nNode = Node.AddNode(Node.Type.Connection);

                        if (nNode != null)
                        {
                            Info nConI = new Info();


                            if (tvConnections.Nodes.Count < 1)
                            {
                                System.Windows.Forms.TreeNode treeNode1 =
                                    new System.Windows.Forms.TreeNode("Connections");
                                treeNode1.Name = "nodeRoot";
                                treeNode1.Text = "Connections";
                                this.tvConnections.Nodes.AddRange(new[] {treeNode1});
                            }

                            if (this.tvConnections.SelectedNode == null)
                            {
                                this.tvConnections.SelectedNode = this.tvConnections.Nodes[0];
                            }
                            if (this.tvConnections.SelectedNode.Tag is Container.Info)
                            {
                                nConI.Parent = this.tvConnections.SelectedNode.Tag;
                            }
                            else
                            {
                                nConI.Inherit.TurnOffInheritanceCompletely();
                            }

                            nConI.TreeNode = nNode;

                            nNode.Tag = nConI;
                            Runtime.ConnectionList.Add(nConI);

                            if (Node.GetNodeType(this.tvConnections.SelectedNode) == Node.Type.Connection)
                            {
                                this.tvConnections.SelectedNode.Parent.Nodes.Add(nNode);
                            }
                            else
                            {
                                this.tvConnections.SelectedNode.Nodes.Add(nNode);
                            }

                            this.tvConnections.SelectedNode = nNode;
                            this.tvConnections.SelectedNode.BeginEdit();
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("AddConnection (UI.Window.Tree) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                public void AddFolder()
                {
                    try
                    {
                        TreeNode newNode = Node.AddNode(Node.Type.Container);
                        Container.Info newContainerInfo = new Container.Info();
                        newNode.Tag = newContainerInfo;
                        newContainerInfo.TreeNode = newNode;

                        if (tvConnections.Nodes.Count < 1)
                        {
                            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Connections");
                            treeNode1.Name = "nodeRoot";
                            treeNode1.Text = "Connections";
                            this.tvConnections.Nodes.AddRange(new[] {treeNode1});
                        }
                        TreeNode selectedNode = Node.SelectedNode;
                        TreeNode parentNode;
                        if (selectedNode == null)
                        {
                            parentNode = tvConnections.Nodes[0];
                        }
                        else
                        {
                            if (Node.GetNodeType(selectedNode) == Node.Type.Connection)
                            {
                                parentNode = selectedNode.Parent;
                            }
                            else
                            {
                                parentNode = selectedNode;
                            }
                        }

                        // We can only inherit from a container node, not the root node or connection nodes
                        if (Node.GetNodeType(parentNode) == Node.Type.Container)
                        {
                            newContainerInfo.Parent = parentNode.Tag;
                        }
                        else
                        {
                            if (newContainerInfo.ConnectionInfo.Inherit == null)
                            {
                                newContainerInfo.ConnectionInfo.Inherit = new Info.Inheritance(parentNode);
                            }
                            newContainerInfo.ConnectionInfo.Inherit.TurnOffInheritanceCompletely();
                        }

                        newContainerInfo.ConnectionInfo = new Info(newContainerInfo);

                        Runtime.ContainerList.Add(newContainerInfo);
                        parentNode.Nodes.Add(newNode);

                        this.tvConnections.SelectedNode = newNode;
                        this.tvConnections.SelectedNode.BeginEdit();
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            string.Format(Language.strErrorAddFolderFailed, ex.Message),
                                                            true);
                    }
                }

                private void DisconnectConnection()
                {
                    try
                    {
                        if (this.tvConnections.SelectedNode != null)
                        {
                            if (this.tvConnections.SelectedNode.Tag is Info)
                            {
                                Info conI = (Info) this.tvConnections.SelectedNode.Tag;
                                for (int i = 0; i <= conI.OpenConnections.Count - 1; i++)
                                {
                                    conI.OpenConnections[i].Disconnect();
                                }
                            }

                            if (this.tvConnections.SelectedNode.Tag is Container.Info)
                            {
                                foreach (TreeNode n in this.tvConnections.SelectedNode.Nodes)
                                {
                                    if (n.Tag is Info)
                                    {
                                        Info conI = (Info) n.Tag;
                                        for (int i = 0; i <= conI.OpenConnections.Count - 1; i++)
                                        {
                                            conI.OpenConnections[i].Disconnect();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("DisconnectConnection (UI.Window.Tree) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void SSHTransferFile()
                {
                    try
                    {
                        Runtime.Windows.Show(Type.SSHTransfer);

                        Info conI = (Info) Node.SelectedNode.Tag;

                        Runtime.Windows.sshtransferForm.Hostname = conI.Hostname;
                        Runtime.Windows.sshtransferForm.Username = conI.Username;
                        Runtime.Windows.sshtransferForm.Password = conI.Password;
                        Runtime.Windows.sshtransferForm.Port = conI.Port.ToString();
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("SSHTransferFile (UI.Window.Tree) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void ExportXML()
                {
                    try
                    {
                        if (this.tvConnections.SelectedNode != null)
                        {
                            Runtime.Windows.saveasForm = new UI.Window.SaveAs(Runtime.Windows.saveasPanel, true,
                                                                              this.tvConnections.SelectedNode);
                            Runtime.Windows.saveasPanel = Runtime.Windows.saveasForm;

                            Runtime.Windows.saveasForm.Show(frmMain.Default.pnlDock);
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("ExportXml (UI.Window.Tree) failed" + Constants.vbNewLine +
                                                             ex.Message), true);
                    }
                }

                private void ImportXML()
                {
                    try
                    {
                        if (this.tvConnections.SelectedNode != null)
                        {
                            Runtime.ImportConnections();
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("ImportXML (UI.Window.Tree) failed" + Constants.vbNewLine +
                                                             ex.Message), true);
                    }
                }

                private void ImportFromAD()
                {
                    try
                    {
                        if (this.tvConnections.SelectedNode != null)
                        {
                            Runtime.Windows.Show(Type.ADImport);
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("ImportFromAD (UI.Window.Tree) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void ImportFromRDPFiles()
                {
                    try
                    {
                        Runtime.ImportConnectionsFromRDPFiles();
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("ImportFromRDPFiles (UI.Window.Tree) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void ImportFromPortScan()
                {
                    try
                    {
                        if (this.tvConnections.SelectedNode != null)
                        {
                            Runtime.Windows.Show(Type.PortScan, Tools.PortScan.PortScanMode.Import);
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("ImportFromPortScan (UI.Window.Tree) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void AddExternalApps()
                {
                    try
                    {
                        //clean up
                        cMenTreeToolsExternalApps.DropDownItems.Clear();

                        //add ext apps
                        foreach (Tools.ExternalTool extA in Runtime.ExternalTools)
                        {
                            ToolStripMenuItem nItem = new ToolStripMenuItem();
                            nItem.Text = (string) extA.DisplayName;
                            nItem.Tag = extA;

                            nItem.Image = extA.Image;

                            nItem.Click += new System.EventHandler(cMenTreeToolsExternalAppsEntry_Click);

                            cMenTreeToolsExternalApps.DropDownItems.Add(nItem);
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
                        if (Node.GetNodeType(Node.SelectedNode) == Node.Type.Connection)
                        {
                            ExtA.Start((Info) Node.SelectedNode.Tag);
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("cMenTreeToolsExternalAppsEntry_Click failed (UI.Window.Tree)" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                #endregion

                #region Menu

                private void mMenViewExpandAllFolders_Click(System.Object sender, System.EventArgs e)
                {
                    Node.ExpandAllNodes();
                }

                private void mMenViewCollapseAllFolders_Click(System.Object sender, System.EventArgs e)
                {
                    if (tvConnections.SelectedNode!=null&&this.tvConnections.SelectedNode.IsEditing)
                    {
                        this.tvConnections.SelectedNode.EndEdit(false);
                    }
                    Node.CollapseAllNodes();
                }

                #endregion

                #region Search

                private void txtSearch_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
                {
                    try
                    {
                        if (e.KeyCode == Keys.Escape)
                        {
                            e.Handled = true;
                            this.tvConnections.Focus();
                        }
                        else if (e.KeyCode == Keys.Up)
                        {
                            this.tvConnections.SelectedNode = this.tvConnections.SelectedNode.PrevVisibleNode;
                        }
                        else if (e.KeyCode == Keys.Down)
                        {
                            this.tvConnections.SelectedNode = this.tvConnections.SelectedNode.NextVisibleNode;
                        }
                        else
                        {
                            this.tvConnections_KeyDown(sender, e);
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("txtSearch_KeyDown (UI.Window.Tree) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void txtSearch_TextChanged(System.Object sender, System.EventArgs e)
                {
                    this.tvConnections.SelectedNode = Node.Find(this.tvConnections.Nodes[0], this.txtSearch.Text);
                }

                private void tvConnections_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
                {
                    try
                    {
                        if (char.IsLetterOrDigit(e.KeyChar))
                        {
                            this.txtSearch.Text = "" + e.KeyChar;

                            this.txtSearch.Focus();
                            this.txtSearch.SelectionStart = this.txtSearch.TextLength;
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("tvConnections_KeyPress (UI.Window.Tree) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void tvConnections_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
                {
                    try
                    {
                        if (e.KeyCode == Keys.Enter)
                        {
                            if (this.tvConnections.SelectedNode.Tag is Info)
                            {
                                e.Handled = true;
                                Runtime.OpenConnection();
                            }
                            else
                            {
                                if (this.tvConnections.SelectedNode.IsExpanded)
                                {
                                    this.tvConnections.SelectedNode.Collapse(true);
                                }
                                else
                                {
                                    this.tvConnections.SelectedNode.Expand();
                                }
                            }
                        }
                        else if (e.KeyCode == Keys.Escape ^ e.KeyCode == Keys.Control || e.KeyCode == Keys.F)
                        {
                            this.txtSearch.Focus();
                            this.txtSearch.SelectionStart = this.txtSearch.TextLength;
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("tvConnections_KeyDown (UI.Window.Tree) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                #endregion

                private void checkTypicalPPortsToolStripMenuItem_Click(object sender, EventArgs e)
                {
                    if (Runtime.Windows.treeForm.tvConnections.SelectedNode.Tag == null)
                    {
                        return;
                    }

                    if (Node.GetNodeType(Node.SelectedNode) == Node.Type.Connection)
                    {
                        var qhs = new QuickHostScanner((Info)Runtime.Windows.treeForm.tvConnections.SelectedNode.Tag);
                        qhs.txtIP.Text=((Info)Runtime.Windows.treeForm.tvConnections.SelectedNode.Tag).Hostname;
                        qhs.ShowDialog(frmMain.defaultInstance);
                    }
                }

                private void cMenTreeToolsExternalApps_Click(object sender, EventArgs e)
                {

                }

                private void msMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
                {

                }

                IEnumerable<TreeNode> GetAllNodes(TreeNode nodes)
                {
                    var allNodes = new List<TreeNode>();
                    allNodes.Add(nodes);
                    foreach (TreeNode tn in nodes.Nodes)
                    {
                        allNodes.AddRange(GetAllNodes(tn));
                    }
                    return allNodes;
                }

                internal void ChangeConProp(Info info, int p, Protocols protocols)
                {
                    try
                    {
                        var allNodes = new List<TreeNode>();
                        foreach (TreeNode t in tvConnections.Nodes)
                        {
                            allNodes.AddRange(GetAllNodes(t));
                        }
                        foreach (var treeNode in allNodes)
                        {
                            var il = treeNode.Tag as Info;
                            if (il != null && il == info)
                            {
                                il.Protocol = protocols;
                                il.Port = p;
                                tvConnections.SelectedNode = treeNode;
                                InitialRefresh();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                    (string)
                                    ("ChangeConProp (UI.Window.Tree) failed" +
                                        Constants.vbNewLine + ex.Message), true);
                    }
                }
            }
        }
    }
}