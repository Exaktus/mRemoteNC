using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using AxMSTSCLib;
using AxWFICALib;
using Microsoft.VisualBasic;
using mRemoteNC.App;
using mRemoteNC.Tools;
using My;
using WeifenLuo.WinFormsUI.Docking;

//using mRemoteNC.Runtime;

namespace mRemoteNC
{
    namespace UI
    {
        namespace Window
        {
            public class ExternalApps : Base
            {
                #region Form Init

                internal System.Windows.Forms.ContextMenuStrip cMenApps;
                private System.ComponentModel.IContainer components;
                internal System.Windows.Forms.ToolStripMenuItem cMenAppsAdd;
                internal System.Windows.Forms.OpenFileDialog dlgOpenFile;
                internal System.Windows.Forms.ToolStripMenuItem cMenAppsRemove;
                internal System.Windows.Forms.ToolStripSeparator cMenAppsSep1;
                internal ColumnHeader clmDisplayName;
                internal ColumnHeader clmFilename;
                internal ColumnHeader clmArguments;
                internal ColumnHeader clmWaitForExit;
                internal ColumnHeader clmTryIntegrate;
                internal ListView lvApps;
                internal Label Label1;
                internal Label Label2;
                internal Label Label3;
                internal TextBox txtDisplayName;
                internal TextBox txtFilename;
                internal TextBox txtArguments;
                internal Button btnBrowse;
                internal CheckBox chkWaitForExit;
                internal Label Label4;
                internal CheckBox chkTryIntegrate;
                internal GroupBox grpEditor;
                internal System.Windows.Forms.ToolStripMenuItem cMenAppsStart;

                private void InitializeComponent()
                {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExternalApps));
            this.cMenApps = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cMenAppsAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenAppsRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenAppsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.cMenAppsStart = new System.Windows.Forms.ToolStripMenuItem();
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.clmDisplayName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmArguments = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmWaitForExit = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmTryIntegrate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvApps = new System.Windows.Forms.ListView();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.txtArguments = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.chkWaitForExit = new System.Windows.Forms.CheckBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.chkTryIntegrate = new System.Windows.Forms.CheckBox();
            this.grpEditor = new System.Windows.Forms.GroupBox();
            this.cMenApps.SuspendLayout();
            this.grpEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // cMenApps
            // 
            this.cMenApps.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cMenAppsAdd,
            this.cMenAppsRemove,
            this.cMenAppsSep1,
            this.cMenAppsStart});
            this.cMenApps.Name = "cMenApps";
            this.cMenApps.Size = new System.Drawing.Size(148, 76);
            // 
            // cMenAppsAdd
            // 
            this.cMenAppsAdd.Image = global::My.Resources.Resources.ExtApp_Add;
            this.cMenAppsAdd.Name = "cMenAppsAdd";
            this.cMenAppsAdd.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F4)));
            this.cMenAppsAdd.Size = new System.Drawing.Size(147, 22);
            this.cMenAppsAdd.Text = "Add";
            this.cMenAppsAdd.Click += new System.EventHandler(this.cMenAppsAddApp_Click);
            // 
            // cMenAppsRemove
            // 
            this.cMenAppsRemove.Image = global::My.Resources.Resources.ExtApp_Delete;
            this.cMenAppsRemove.Name = "cMenAppsRemove";
            this.cMenAppsRemove.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.cMenAppsRemove.Size = new System.Drawing.Size(147, 22);
            this.cMenAppsRemove.Text = "Remove";
            this.cMenAppsRemove.Click += new System.EventHandler(this.cMenAppsRemove_Click);
            // 
            // cMenAppsSep1
            // 
            this.cMenAppsSep1.Name = "cMenAppsSep1";
            this.cMenAppsSep1.Size = new System.Drawing.Size(144, 6);
            // 
            // cMenAppsStart
            // 
            this.cMenAppsStart.Image = global::My.Resources.Resources.ExtApp_Start;
            this.cMenAppsStart.Name = "cMenAppsStart";
            this.cMenAppsStart.Size = new System.Drawing.Size(147, 22);
            this.cMenAppsStart.Text = "Start";
            this.cMenAppsStart.Click += new System.EventHandler(this.cMenAppsStart_Click);
            // 
            // dlgOpenFile
            // 
            this.dlgOpenFile.Filter = "All Files (*.*)|*.*";
            // 
            // clmDisplayName
            // 
            this.clmDisplayName.Text = "Display Name";
            this.clmDisplayName.Width = 130;
            // 
            // clmFilename
            // 
            this.clmFilename.Text = "Filename";
            this.clmFilename.Width = 200;
            // 
            // clmArguments
            // 
            this.clmArguments.Text = "Arguments";
            this.clmArguments.Width = 160;
            // 
            // clmWaitForExit
            // 
            this.clmWaitForExit.Text = "Wait for exit";
            this.clmWaitForExit.Width = 75;
            // 
            // clmTryIntegrate
            // 
            this.clmTryIntegrate.Text = "Try To Integrate";
            this.clmTryIntegrate.Width = 95;
            // 
            // lvApps
            // 
            this.lvApps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvApps.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvApps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmDisplayName,
            this.clmFilename,
            this.clmArguments,
            this.clmWaitForExit,
            this.clmTryIntegrate});
            this.lvApps.ContextMenuStrip = this.cMenApps;
            this.lvApps.FullRowSelect = true;
            this.lvApps.GridLines = true;
            this.lvApps.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvApps.HideSelection = false;
            this.lvApps.Location = new System.Drawing.Point(0, 1);
            this.lvApps.Name = "lvApps";
            this.lvApps.Size = new System.Drawing.Size(684, 193);
            this.lvApps.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvApps.TabIndex = 0;
            this.lvApps.UseCompatibleStateImageBehavior = false;
            this.lvApps.View = System.Windows.Forms.View.Details;
            this.lvApps.SelectedIndexChanged += new System.EventHandler(this.lvApps_SelectedIndexChanged);
            this.lvApps.DoubleClick += new System.EventHandler(this.lvApps_DoubleClick);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(15, 19);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(75, 13);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Display Name:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(15, 44);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(52, 13);
            this.Label2.TabIndex = 20;
            this.Label2.Text = "Filename:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(15, 70);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(60, 13);
            this.Label3.TabIndex = 50;
            this.Label3.Text = "Arguments:";
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDisplayName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDisplayName.Location = new System.Drawing.Point(104, 16);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(567, 20);
            this.txtDisplayName.TabIndex = 10;
            this.txtDisplayName.LostFocus += new System.EventHandler(this.ApplicationEditorField_ChangedOrLostFocus);
            // 
            // txtFilename
            // 
            this.txtFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilename.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilename.Location = new System.Drawing.Point(104, 42);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(464, 20);
            this.txtFilename.TabIndex = 30;
            this.txtFilename.LostFocus += new System.EventHandler(this.ApplicationEditorField_ChangedOrLostFocus);
            // 
            // txtArguments
            // 
            this.txtArguments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtArguments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtArguments.Location = new System.Drawing.Point(104, 68);
            this.txtArguments.Name = "txtArguments";
            this.txtArguments.Size = new System.Drawing.Size(567, 20);
            this.txtArguments.TabIndex = 60;
            this.txtArguments.LostFocus += new System.EventHandler(this.ApplicationEditorField_ChangedOrLostFocus);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Location = new System.Drawing.Point(576, 40);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(95, 23);
            this.btnBrowse.TabIndex = 40;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            this.btnBrowse.LostFocus += new System.EventHandler(this.ApplicationEditorField_ChangedOrLostFocus);
            // 
            // chkWaitForExit
            // 
            this.chkWaitForExit.AutoSize = true;
            this.chkWaitForExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkWaitForExit.Location = new System.Drawing.Point(104, 96);
            this.chkWaitForExit.Name = "chkWaitForExit";
            this.chkWaitForExit.Size = new System.Drawing.Size(79, 17);
            this.chkWaitForExit.TabIndex = 70;
            this.chkWaitForExit.Text = "Wait for exit";
            this.chkWaitForExit.UseVisualStyleBackColor = true;
            this.chkWaitForExit.Click += new System.EventHandler(this.ApplicationEditorField_ChangedOrLostFocus);
            this.chkWaitForExit.LostFocus += new System.EventHandler(this.ApplicationEditorField_ChangedOrLostFocus);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(15, 98);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(46, 13);
            this.Label4.TabIndex = 62;
            this.Label4.Text = "Options:";
            // 
            // chkTryIntegrate
            // 
            this.chkTryIntegrate.AutoSize = true;
            this.chkTryIntegrate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkTryIntegrate.Location = new System.Drawing.Point(243, 97);
            this.chkTryIntegrate.Name = "chkTryIntegrate";
            this.chkTryIntegrate.Size = new System.Drawing.Size(94, 17);
            this.chkTryIntegrate.TabIndex = 71;
            this.chkTryIntegrate.Text = "Try to integrate";
            this.chkTryIntegrate.UseVisualStyleBackColor = true;
            this.chkTryIntegrate.CheckedChanged += new System.EventHandler(this.chkTryIntegrate_CheckedChanged);
            this.chkTryIntegrate.Click += new System.EventHandler(this.ApplicationEditorField_ChangedOrLostFocus);
            // 
            // grpEditor
            // 
            this.grpEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpEditor.Controls.Add(this.chkTryIntegrate);
            this.grpEditor.Controls.Add(this.Label4);
            this.grpEditor.Controls.Add(this.chkWaitForExit);
            this.grpEditor.Controls.Add(this.btnBrowse);
            this.grpEditor.Controls.Add(this.txtArguments);
            this.grpEditor.Controls.Add(this.txtFilename);
            this.grpEditor.Controls.Add(this.txtDisplayName);
            this.grpEditor.Controls.Add(this.Label3);
            this.grpEditor.Controls.Add(this.Label2);
            this.grpEditor.Controls.Add(this.Label1);
            this.grpEditor.Enabled = false;
            this.grpEditor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpEditor.Location = new System.Drawing.Point(1, 200);
            this.grpEditor.Name = "grpEditor";
            this.grpEditor.Size = new System.Drawing.Size(683, 123);
            this.grpEditor.TabIndex = 10;
            this.grpEditor.TabStop = false;
            this.grpEditor.Text = "Application Editor";
            // 
            // ExternalApps
            // 
            this.ClientSize = new System.Drawing.Size(684, 323);
            this.Controls.Add(this.grpEditor);
            this.Controls.Add(this.lvApps);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ExternalApps";
            this.TabText = "External Applications";
            this.Text = "External Applications";
            this.Load += new System.EventHandler(this.ExternalApps_Load);
            this.cMenApps.ResumeLayout(false);
            this.grpEditor.ResumeLayout(false);
            this.grpEditor.PerformLayout();
            this.ResumeLayout(false);

                }

                #endregion Form Init

                #region Public Methods

                public ExternalApps(DockContent Panel)
                {
                    this.WindowType = Type.ExternalApps;
                    this.DockPnl = Panel;
                    this.InitializeComponent();
                }

                #endregion Public Methods

                #region Private Properties

                private Tools.ExternalTool _SelApp;

                #endregion Private Properties

                #region Private Methods

                private void LoadApps()
                {
                    try
                    {
                        lvApps.Items.Clear();

                        foreach (Tools.ExternalTool extA in Runtime.ExternalTools)
                        {
                            ListViewItem lvItem = new ListViewItem();
                            lvItem.Text = (string)extA.DisplayName;
                            lvItem.SubItems.Add(extA.FileName);
                            lvItem.SubItems.Add(extA.Arguments);
                            lvItem.SubItems.Add(extA.WaitForExit.ToString());
                            lvItem.SubItems.Add(extA.TryIntegrate.ToString());
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

                private void GetAppProperties(Tools.ExternalTool SelApp)
                {
                    try
                    {
                        if (SelApp != null)
                        {
                            txtDisplayName.Text = (string)SelApp.DisplayName;
                            txtFilename.Text = (string)SelApp.FileName;
                            txtArguments.Text = (string)SelApp.Arguments;
                            chkWaitForExit.Checked = System.Convert.ToBoolean(SelApp.WaitForExit);
                            chkTryIntegrate.Checked = System.Convert.ToBoolean(SelApp.TryIntegrate);
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

                private void SetAppProperties(Tools.ExternalTool SelApp)
                {
                    try
                    {
                        if (SelApp != null)
                        {
                            SelApp.DisplayName = txtDisplayName.Text;
                            SelApp.FileName = txtFilename.Text;
                            SelApp.Arguments = txtArguments.Text;
                            SelApp.WaitForExit = chkWaitForExit.Checked;
                            SelApp.TryIntegrate = chkTryIntegrate.Checked;

                            LoadApps();
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("SetAppProperties failed (UI.Window.ExternalApps)" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void AddNewApp()
                {
                    try
                    {
                        Tools.ExternalTool nExtA = new Tools.ExternalTool("New Application");
                        Runtime.ExternalTools.Add(nExtA);
                        LoadApps();
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
                                Runtime.ExternalTools.Remove(lvItem.Tag);
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

                private void StartApp()
                {
                    try
                    {
                        foreach (ListViewItem lvItem in lvApps.SelectedItems)
                        {
                            (lvItem.Tag as ExternalTool).Start();
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                            (string)
                                                            ("StartApp failed (UI.Window.ExternalApps" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void RefreshToolbar()
                {
                    frmMain.Default.AddExternalToolsToToolBar();
                    Runtime.GetExtApps();
                }

                #endregion Private Methods

                #region Form Stuff

                private void ExternalApps_Load(object sender, System.EventArgs e)
                {
                    ApplyLanguage();
                    LoadApps();
                }

                private void ApplyLanguage()
                {
                    clmDisplayName.Text = Language.strColumnDisplayName;
                    clmFilename.Text = Language.strColumnFilename;
                    clmArguments.Text = Language.strColumnArguments;
                    clmWaitForExit.Text = Language.strColumnWaitForExit;
                    cMenAppsAdd.Text = Language.strMenuNewExternalTool;
                    cMenAppsRemove.Text = Language.strMenuDeleteExternalTool;
                    cMenAppsStart.Text = Language.strMenuLaunchExternalTool;
                    grpEditor.Text = Language.strGroupboxExternalToolProperties;
                    Label4.Text = Language.strLabelOptions;
                    chkWaitForExit.Text = Language.strCheckboxWaitForExit;
                    chkTryIntegrate.Text = Language.strTryIntegrate;
                    btnBrowse.Text = Language.strButtonBrowse;
                    Label3.Text = Language.strLabelArguments;
                    Label2.Text = Language.strLabelFilename;
                    Label1.Text = Language.strLabelDisplayName;
                    dlgOpenFile.Filter = Language.strFilterApplication + "|*.exe|" + Language.strFilterAll + "|*.*";
                    TabText = Language.strMenuExternalTools;
                    Text = Language.strMenuExternalTools;
                }

                private void lvApps_DoubleClick(object sender, System.EventArgs e)
                {
                    if (lvApps.SelectedItems.Count > 0)
                    {
                        StartApp();
                    }
                }

                private void lvApps_SelectedIndexChanged(System.Object sender, System.EventArgs e)
                {
                    if (lvApps.SelectedItems.Count == 1)
                    {
                        this.grpEditor.Enabled = true;
                        GetAppProperties((ExternalTool)lvApps.SelectedItems[0].Tag);
                    }
                    else
                    {
                        this.grpEditor.Enabled = false;
                    }
                }

                private void btnBrowse_Click(System.Object sender, System.EventArgs e)
                {
                    if (dlgOpenFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        txtFilename.Text = dlgOpenFile.FileName;
                    }
                }

                private void ApplicationEditorField_ChangedOrLostFocus(object sender, System.EventArgs e)
                {
                    SetAppProperties(_SelApp);
                }

                private void chkTryIntegrate_CheckedChanged(System.Object sender, System.EventArgs e)
                {
                    if (chkTryIntegrate.Checked)
                    {
                        chkWaitForExit.Checked = false;
                        chkWaitForExit.Enabled = false;
                    }
                    else
                    {
                        chkWaitForExit.Enabled = true;
                    }
                }

                #endregion Form Stuff

                #region Menu

                private void cMenAppsAddApp_Click(System.Object sender, System.EventArgs e)
                {
                    AddNewApp();
                }

                private void cMenAppsRemove_Click(System.Object sender, System.EventArgs e)
                {
                    RemoveApps();
                }

                private void cMenAppsStart_Click(System.Object sender, System.EventArgs e)
                {
                    StartApp();
                }

                #endregion Menu
            }
        }
    }
}