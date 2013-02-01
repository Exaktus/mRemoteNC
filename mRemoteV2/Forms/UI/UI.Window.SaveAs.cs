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
using My;
using WeifenLuo.WinFormsUI.Docking;

//using mRemoteNC.Runtime;

namespace mRemoteNC
{
    namespace UI
    {
        namespace Window
        {
            public class SaveAs : Base
            {
                #region Form Init

                internal System.Windows.Forms.Button btnCancel;
                internal System.Windows.Forms.ListView lvSecurity;
                internal System.Windows.Forms.ColumnHeader ColumnHeader1;
                internal System.Windows.Forms.Button btnOK;
                internal System.Windows.Forms.Label lblMremoteXMLOnly;
                internal System.Windows.Forms.PictureBox PictureBox1;
                internal System.Windows.Forms.Label Label1;

                private void InitializeComponent()
                {
                    System.Windows.Forms.ListViewItem ListViewItem1 = new System.Windows.Forms.ListViewItem("Username");
                    System.Windows.Forms.ListViewItem ListViewItem2 = new System.Windows.Forms.ListViewItem("Password");
                    System.Windows.Forms.ListViewItem ListViewItem3 = new System.Windows.Forms.ListViewItem("Domain");
                    System.Windows.Forms.ListViewItem ListViewItem4 =
                        new System.Windows.Forms.ListViewItem("Inheritance");
                    this.btnCancel = new System.Windows.Forms.Button();
                    this.Load += new System.EventHandler(this.SaveAs_Load);
                    this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
                    this.btnOK = new System.Windows.Forms.Button();
                    this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
                    this.lvSecurity = new System.Windows.Forms.ListView();
                    this.ColumnHeader1 = new System.Windows.Forms.ColumnHeader();
                    this.Label1 = new System.Windows.Forms.Label();
                    this.lblMremoteXMLOnly = new System.Windows.Forms.Label();
                    this.PictureBox1 = new System.Windows.Forms.PictureBox();
                    ((System.ComponentModel.ISupportInitialize)this.PictureBox1).BeginInit();
                    this.SuspendLayout();
                    //
                    //btnCancel
                    //
                    this.btnCancel.Anchor =
                        (System.Windows.Forms.AnchorStyles)
                        (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
                    this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    this.btnCancel.Location = new System.Drawing.Point(457, 370);
                    this.btnCancel.Name = "btnCancel";
                    this.btnCancel.Size = new System.Drawing.Size(75, 23);
                    this.btnCancel.TabIndex = 110;
                    this.btnCancel.Text = "&Cancel";
                    this.btnCancel.UseVisualStyleBackColor = true;
                    //
                    //btnOK
                    //
                    this.btnOK.Anchor =
                        (System.Windows.Forms.AnchorStyles)
                        (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
                    this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    this.btnOK.Location = new System.Drawing.Point(376, 370);
                    this.btnOK.Name = "btnOK";
                    this.btnOK.Size = new System.Drawing.Size(75, 23);
                    this.btnOK.TabIndex = 100;
                    this.btnOK.Text = "&OK";
                    this.btnOK.UseVisualStyleBackColor = true;
                    //
                    //lvSecurity
                    //
                    this.lvSecurity.Anchor =
                        (System.Windows.Forms.AnchorStyles)
                        (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) |
                          System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
                    this.lvSecurity.BorderStyle = System.Windows.Forms.BorderStyle.None;
                    this.lvSecurity.CheckBoxes = true;
                    this.lvSecurity.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.ColumnHeader1 });
                    this.lvSecurity.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
                    ListViewItem1.Checked = true;
                    ListViewItem1.StateImageIndex = 1;
                    ListViewItem2.Checked = true;
                    ListViewItem2.StateImageIndex = 1;
                    ListViewItem3.Checked = true;
                    ListViewItem3.StateImageIndex = 1;
                    ListViewItem4.Checked = true;
                    ListViewItem4.StateImageIndex = 1;
                    this.lvSecurity.Items.AddRange(new System.Windows.Forms.ListViewItem[] { ListViewItem1, ListViewItem2, ListViewItem3, ListViewItem4 });
                    this.lvSecurity.Location = new System.Drawing.Point(0, 55);
                    this.lvSecurity.MultiSelect = false;
                    this.lvSecurity.Name = "lvSecurity";
                    this.lvSecurity.Scrollable = false;
                    this.lvSecurity.Size = new System.Drawing.Size(532, 309);
                    this.lvSecurity.TabIndex = 20;
                    this.lvSecurity.UseCompatibleStateImageBehavior = false;
                    this.lvSecurity.View = System.Windows.Forms.View.Details;
                    //
                    //ColumnHeader1
                    //
                    this.ColumnHeader1.Width = 110;
                    //
                    //Label1
                    //
                    this.Label1.AutoSize = true;
                    this.Label1.Location = new System.Drawing.Point(12, 11);
                    this.Label1.Name = "Label1";
                    this.Label1.Size = new System.Drawing.Size(244, 13);
                    this.Label1.TabIndex = 10;
                    this.Label1.Text = "Uncheck the properties you want not to be saved!";
                    //
                    //lblMremoteXMLOnly
                    //
                    this.lblMremoteXMLOnly.AutoSize = true;
                    this.lblMremoteXMLOnly.ForeColor = System.Drawing.Color.DarkRed;
                    this.lblMremoteXMLOnly.Location = new System.Drawing.Point(37, 33);
                    this.lblMremoteXMLOnly.Name = "lblMremoteXMLOnly";
                    this.lblMremoteXMLOnly.Size = new System.Drawing.Size(345, 13);
                    this.lblMremoteXMLOnly.TabIndex = 111;
                    this.lblMremoteXMLOnly.Text =
                        "(These properties will only be saved if you select a mRemote file format!)";
                    //
                    //PictureBox1
                    //
                    this.PictureBox1.Image = global::My.Resources.Resources.WarningSmall;
                    this.PictureBox1.Location = new System.Drawing.Point(15, 31);
                    this.PictureBox1.Name = "PictureBox1";
                    this.PictureBox1.Size = new System.Drawing.Size(16, 16);
                    this.PictureBox1.TabIndex = 112;
                    this.PictureBox1.TabStop = false;
                    //
                    //SaveAs
                    //
                    this.AcceptButton = this.btnOK;
                    this.CancelButton = this.btnCancel;
                    this.ClientSize = new System.Drawing.Size(534, 396);
                    this.Controls.Add(this.PictureBox1);
                    this.Controls.Add(this.lblMremoteXMLOnly);
                    this.Controls.Add(this.Label1);
                    this.Controls.Add(this.lvSecurity);
                    this.Controls.Add(this.btnCancel);
                    this.Controls.Add(this.btnOK);
                    this.Icon = global::My.Resources.Resources.Connections_SaveAs_Icon;
                    this.Name = "SaveAs";
                    this.TabText = "Save Connections As";
                    this.Text = "Save Connections As";
                    ((System.ComponentModel.ISupportInitialize)this.PictureBox1).EndInit();
                    this.ResumeLayout(false);
                    this.PerformLayout();
                }

                #endregion Form Init

                #region Public Properties

                private bool _Export;

                public bool Export
                {
                    get { return _Export; }
                    set { _Export = value; }
                }

                private TreeNode _TreeNode;

                public TreeNode TreeNode
                {
                    get { return _TreeNode; }
                    set { _TreeNode = value; }
                }

                #endregion Public Properties

                #region Public Methods

                public SaveAs(DockContent Panel)
                    : this(Panel, false, null)
                {
                }

                public SaveAs(DockContent Panel, bool Export, TreeNode TreeNode)
                {
                    this.WindowType = Type.SaveAs;
                    this.DockPnl = Panel;
                    this.InitializeComponent();

                    if (Export)
                    {
                        this.SetFormText(Language.strExport);
                    }
                    else
                    {
                        this.SetFormText(Language.strMenuSaveConnectionFileAs);
                    }

                    this._Export = Export;
                    this._TreeNode = TreeNode;
                }

                #endregion Public Methods

                #region Form Stuff

                private void SaveAs_Load(object sender, System.EventArgs e)
                {
                    ApplyLanguage();
                }

                private void ApplyLanguage()
                {
                    lvSecurity.Items[0].Text = Language.strCheckboxUsername;
                    lvSecurity.Items[1].Text = Language.strCheckboxPassword;
                    lvSecurity.Items[2].Text = Language.strCheckboxDomain;
                    lvSecurity.Items[3].Text = Language.strCheckboxInheritance;
                    btnCancel.Text = Language.strButtonCancel;
                    btnOK.Text = Language.strButtonOK;
                    Label1.Text = Language.strUncheckProperties;
                    lblMremoteXMLOnly.Text = Language.strPropertiesWillOnlyBeSavedMRemoteXML;
                    TabText = Language.strMenuSaveConnectionFileAs;
                    Text = Language.strMenuSaveConnectionFileAs;
                }

                private void btnCancel_Click(System.Object sender, System.EventArgs e)
                {
                    this.Close();
                }

                private void btnOK_Click(System.Object sender, System.EventArgs e)
                {
                    try
                    {
                        if (_TreeNode == null)
                        {
                            _TreeNode = Runtime.Windows.treeForm.tvConnections.Nodes[0];
                        }

                        Security.Save sS = new Security.Save();

                        sS.Username = this.lvSecurity.Items[0].Checked;
                        sS.Password = this.lvSecurity.Items[1].Checked;
                        sS.Domain = this.lvSecurity.Items[2].Checked;
                        sS.Inheritance = this.lvSecurity.Items[3].Checked;

                        Runtime.SaveConnectionsAs(sS, _TreeNode);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("btnOK_Click (UI.Window.SaveAs) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                #endregion Form Stuff
            }
        }
    }
}