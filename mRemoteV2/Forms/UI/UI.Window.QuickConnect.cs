using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using AxWFICALib;
using Microsoft.VisualBasic;
using mRemoteNC;
using mRemoteNC.App;
using mRemoteNC.Connection;
using My;
using WeifenLuo.WinFormsUI.Docking;

//using mRemoteNC.Runtime;

namespace mRemoteNC
{
    namespace UI
    {
        namespace Window
        {
            public class QuickConnect : Base
            {
                #region Form Init

                internal System.Windows.Forms.Button btnCancel;
                internal System.Windows.Forms.FlowLayoutPanel flpProtocols;

                private void InitializeComponent()
                {
                    this.flpProtocols = new System.Windows.Forms.FlowLayoutPanel();
                    this.Load += new System.EventHandler(this.QuickConnect_Load);
                    this.btnCancel = new System.Windows.Forms.Button();
                    this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
                    this.SuspendLayout();
                    //
                    //flpProtocols
                    //
                    this.flpProtocols.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.flpProtocols.Location = new System.Drawing.Point(0, 0);
                    this.flpProtocols.Name = "flpProtocols";
                    this.flpProtocols.Size = new System.Drawing.Size(271, 155);
                    this.flpProtocols.TabIndex = 10;
                    //
                    //btnCancel
                    //
                    this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    this.btnCancel.Location = new System.Drawing.Point(-200, -200);
                    this.btnCancel.Name = "btnCancel";
                    this.btnCancel.Size = new System.Drawing.Size(75, 23);
                    this.btnCancel.TabIndex = 20;
                    this.btnCancel.TabStop = false;
                    this.btnCancel.Text = Language.strButtonCancel;
                    this.btnCancel.UseVisualStyleBackColor = true;
                    //
                    //QuickConnect
                    //
                    this.CancelButton = this.btnCancel;
                    this.ClientSize = new System.Drawing.Size(271, 155);
                    this.Controls.Add(this.btnCancel);
                    this.Controls.Add(this.flpProtocols);
                    this.HideOnClose = true;
                    this.Icon = global::My.Resources.Resources.Play_Quick_Icon;
                    this.Name = "QuickConnect";
                    this.TabText = Language.strQuickConnect;
                    this.Text = Language.strQuickConnect;
                    this.ResumeLayout(false);
                }

                #endregion Form Init

                #region Public Methods

                public QuickConnect(DockContent Panel)
                {
                    this.WindowType = Type.Connection;
                    this.DockPnl = Panel;
                    this.InitializeComponent();
                }

                #endregion Public Methods

                #region Public Properties

                private Info _ConnectionInfo;

                public Info ConnectionInfo
                {
                    get { return this._ConnectionInfo; }
                    set { this._ConnectionInfo = value; }
                }

                #endregion Public Properties

                private void btnCancel_Click(System.Object sender, System.EventArgs e)
                {
                    this.Hide();
                }

                private void QuickConnect_Load(object sender, System.EventArgs e)
                {
                    ApplyLanguage();

                    this.CreateButtons();

                    this.flpProtocols.Controls[0].Focus();
                }

                private void ApplyLanguage()
                {
                    btnCancel.Text = Language.strButtonCancel;
                    TabText = Language.strQuickConnect;
                    Text = Language.strQuickConnect;
                }

                private void CreateButtons()
                {
                    try
                    {
                        foreach (FieldInfo fI in typeof(Protocols).GetFields())
                        {
                            if (fI.Name != "value__" && fI.Name != "NONE" && fI.Name != "IntApp")
                            {
                                Button nBtn = new Button();
                                nBtn.Text = fI.Name;
                                nBtn.FlatStyle = FlatStyle.Flat;
                                nBtn.Size = new Size(60, 40);
                                nBtn.Parent = this.flpProtocols;
                                nBtn.Click += new System.EventHandler(ProtocolButton_Click);
                                nBtn.Show();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("CreateButtons (UI.Window.QuickConnect) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void ProtocolButton_Click(object sender, System.EventArgs e)
                {
                    try
                    {
                        this._ConnectionInfo.Protocol =
                            (Protocols)Tools.Misc.StringToEnum(typeof(Protocols), ((Control)sender).Text);

                        if (this._ConnectionInfo.Port == 0)
                        {
                            this._ConnectionInfo.SetDefaultPort();

                            if (
                                mRemoteNC.QuickConnect.History.Exists(this._ConnectionInfo.Hostname) ==
                                false)
                            {
                                mRemoteNC.QuickConnect.History.Add(this._ConnectionInfo.Hostname);
                            }
                        }
                        else
                        {
                            if (
                                mRemoteNC.QuickConnect.History.Exists(this._ConnectionInfo.Hostname) ==
                                false)
                            {
                                mRemoteNC.QuickConnect.History.Add(this._ConnectionInfo.Hostname +
                                                                                       ":" + this._ConnectionInfo.Port);
                            }
                        }

                        Runtime.OpenConnection(this._ConnectionInfo, Info.Force.DoNotJump);

                        this.Hide();
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("ProtocolButton_Click (UI.Window.QuickConnect) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }
            }
        }
    }
}