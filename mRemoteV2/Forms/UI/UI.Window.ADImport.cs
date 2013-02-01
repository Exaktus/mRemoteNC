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
using mRemoteNC.Tree;
using My;
using WeifenLuo.WinFormsUI.Docking;

namespace mRemoteNC
{
    namespace UI
    {
        namespace Window
        {
            public class ADImport : Base
            {
                #region Form Init

                internal System.Windows.Forms.Button btnCancel;
                internal System.Windows.Forms.Button btnOK;
                internal System.Windows.Forms.TextBox txtDomain;
                internal System.Windows.Forms.Label lblDomain;
                internal System.Windows.Forms.Button btnChangeDomain;
                internal ADTree.ADtree AD;

                private void InitializeComponent()
                {
                    this.btnOK = new System.Windows.Forms.Button();
                    this.Load += new System.EventHandler(this.ADImport_Load);
                    this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
                    this.btnCancel = new System.Windows.Forms.Button();
                    this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
                    this.txtDomain = new System.Windows.Forms.TextBox();
                    this.lblDomain = new System.Windows.Forms.Label();
                    this.btnChangeDomain = new System.Windows.Forms.Button();
                    this.btnChangeDomain.Click += new System.EventHandler(this.btnChangeDomain_Click);
                    this.AD = new ADTree.ADtree();
                    this.SuspendLayout();
                    //
                    //btnOK
                    //
                    this.btnOK.Anchor =
                        (System.Windows.Forms.AnchorStyles)
                        (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
                    this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    this.btnOK.Location = new System.Drawing.Point(372, 347);
                    this.btnOK.Name = "btnOK";
                    this.btnOK.Size = new System.Drawing.Size(75, 23);
                    this.btnOK.TabIndex = 100;
                    this.btnOK.Text = "OK";
                    this.btnOK.UseVisualStyleBackColor = true;
                    //
                    //btnCancel
                    //
                    this.btnCancel.Anchor =
                        (System.Windows.Forms.AnchorStyles)
                        (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
                    this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    this.btnCancel.Location = new System.Drawing.Point(453, 347);
                    this.btnCancel.Name = "btnCancel";
                    this.btnCancel.Size = new System.Drawing.Size(75, 23);
                    this.btnCancel.TabIndex = 110;
                    this.btnCancel.Text = "Cancel";
                    this.btnCancel.UseVisualStyleBackColor = true;
                    //
                    //txtDomain
                    //
                    this.txtDomain.Anchor =
                        (System.Windows.Forms.AnchorStyles)
                        (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
                    this.txtDomain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    this.txtDomain.Location = new System.Drawing.Point(57, 348);
                    this.txtDomain.Name = "txtDomain";
                    this.txtDomain.Size = new System.Drawing.Size(100, 20);
                    this.txtDomain.TabIndex = 30;
                    //
                    //lblDomain
                    //
                    this.lblDomain.Anchor =
                        (System.Windows.Forms.AnchorStyles)
                        (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
                    this.lblDomain.AutoSize = true;
                    this.lblDomain.Location = new System.Drawing.Point(5, 351);
                    this.lblDomain.Name = "lblDomain";
                    this.lblDomain.Size = new System.Drawing.Size(46, 13);
                    this.lblDomain.TabIndex = 20;
                    this.lblDomain.Text = "Domain:";
                    //
                    //btnChangeDomain
                    //
                    this.btnChangeDomain.Anchor =
                        (System.Windows.Forms.AnchorStyles)
                        (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
                    this.btnChangeDomain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    this.btnChangeDomain.Location = new System.Drawing.Point(163, 347);
                    this.btnChangeDomain.Name = "btnChangeDomain";
                    this.btnChangeDomain.Size = new System.Drawing.Size(75, 23);
                    this.btnChangeDomain.TabIndex = 40;
                    this.btnChangeDomain.Text = "Change";
                    this.btnChangeDomain.UseVisualStyleBackColor = true;
                    //
                    //AD
                    //
                    this.AD.ADPath = null;
                    this.AD.Anchor =
                        (System.Windows.Forms.AnchorStyles)
                        (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) |
                          System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
                    this.AD.Domain = "akane";
                    this.AD.Location = new System.Drawing.Point(0, 0);
                    this.AD.Name = "AD";
                    this.AD.SelectedNode = null;
                    this.AD.Size = new System.Drawing.Size(530, 341);
                    this.AD.TabIndex = 10;
                    //
                    //ADImport
                    //
                    this.AcceptButton = this.btnOK;
                    this.CancelButton = this.btnCancel;
                    this.ClientSize = new System.Drawing.Size(530, 373);
                    this.Controls.Add(this.AD);
                    this.Controls.Add(this.lblDomain);
                    this.Controls.Add(this.txtDomain);
                    this.Controls.Add(this.btnChangeDomain);
                    this.Controls.Add(this.btnCancel);
                    this.Controls.Add(this.btnOK);
                    this.Font = new System.Drawing.Font("Microsoft Sans Serif", (float)(8.25F),
                                                        System.Drawing.FontStyle.Regular,
                                                        System.Drawing.GraphicsUnit.Point, (byte)(0));
                    this.Icon = global::My.Resources.Resources.ActiveDirectory_Icon;
                    this.Name = "ADImport";
                    this.TabText = "Active Directory Import";
                    this.Text = "Active Directory Import";
                    this.ResumeLayout(false);
                    this.PerformLayout();
                }

                #endregion Form Init

                #region Public Methods

                public ADImport(DockContent Panel)
                {
                    this.WindowType = Type.ADImport;
                    this.DockPnl = Panel;
                    this.InitializeComponent();
                }

                #endregion Public Methods

                #region Form Stuff

                private void ADImport_Load(object sender, System.EventArgs e)
                {
                    ApplyLanguage();
                }

                private void ApplyLanguage()
                {
                    btnOK.Text = Language.strButtonOK;
                    btnCancel.Text = Language.strButtonCancel;
                    lblDomain.Text = Language.strLabelDomain;
                    btnChangeDomain.Text = Language.strButtonChange;
                }

                #endregion Form Stuff

                #region Public Properties

                private string _ADPath;

                public string ADPath
                {
                    get { return _ADPath; }
                    set { _ADPath = value; }
                }

                #endregion Public Properties

                private void btnOK_Click(System.Object sender, System.EventArgs e)
                {
                    this._ADPath = this.AD.ADPath;
                    Node.AddADNodes(this._ADPath);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }

                private void btnCancel_Click(System.Object sender, System.EventArgs e)
                {
                    this._ADPath = "";
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.Close();
                }

                private void btnChangeDomain_Click(System.Object sender, System.EventArgs e)
                {
                    this.AD.Domain = txtDomain.Text;
                    this.AD.Refresh();
                }
            }
        }
    }
}