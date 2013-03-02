
using System.Collections.Generic;
using System;
using AxWFICALib;
using System.Drawing;
using System.Diagnostics;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections;
using System.Windows.Forms;
using My;




namespace mRemoteNC
{
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]public partial class frmPassword : System.Windows.Forms.Form
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
		private System.ComponentModel.Container components = null;
		
		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.Load += new System.EventHandler(frmPassword_Load);
			this.txtPassword.GotFocus += new System.EventHandler(this.txtPassword_GotFocus);
			this.txtVerify = new System.Windows.Forms.TextBox();
			this.txtVerify.GotFocus += new System.EventHandler(this.txtVerify_GotFocus);
			this.lblPassword = new System.Windows.Forms.Label();
			this.lblVerify = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.lblStatus = new System.Windows.Forms.Label();
			this.pbLock = new System.Windows.Forms.PictureBox();
			this.pnlImage = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize) this.pbLock).BeginInit();
			this.pnlImage.SuspendLayout();
			this.SuspendLayout();
			//
			//txtPassword
			//
			this.txtPassword.Anchor = (System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtPassword.Location = new System.Drawing.Point(95, 22);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Size = new System.Drawing.Size(184, 20);
			this.txtPassword.TabIndex = 0;
			this.txtPassword.UseSystemPasswordChar = true;
			//
			//txtVerify
			//
			this.txtVerify.Anchor = (System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.txtVerify.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtVerify.Location = new System.Drawing.Point(95, 61);
			this.txtVerify.Name = "txtVerify";
			this.txtVerify.Size = new System.Drawing.Size(184, 20);
			this.txtVerify.TabIndex = 1;
			this.txtVerify.UseSystemPasswordChar = true;
			//
			//lblPassword
			//
			this.lblPassword.AutoSize = true;
			this.lblPassword.Location = new System.Drawing.Point(87, 6);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(56, 13);
			this.lblPassword.TabIndex = 2;
			this.lblPassword.Text = Language.strLabelPassword;
			//
			//lblVerify
			//
			this.lblVerify.AutoSize = true;
			this.lblVerify.Location = new System.Drawing.Point(87, 45);
			this.lblVerify.Name = "lblVerify";
			this.lblVerify.Size = new System.Drawing.Size(36, 13);
			this.lblVerify.TabIndex = 3;
			this.lblVerify.Text = Language.strLabelVerify;
			//
			//btnOK
			//
			this.btnOK.Anchor = (System.Windows.Forms.AnchorStyles) (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOK.Location = new System.Drawing.Point(210, 101);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(69, 23);
			this.btnOK.TabIndex = 4;
			this.btnOK.Text = Language.strButtonOK;
			this.btnOK.UseVisualStyleBackColor = true;
			//
			//btnCancel
			//
			this.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles) (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCancel.Location = new System.Drawing.Point(135, 101);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(69, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = Language.strButtonCancel;
			this.btnCancel.UseVisualStyleBackColor = true;
			//
			//lblStatus
			//
			this.lblStatus.Anchor = (System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.lblStatus.ForeColor = System.Drawing.Color.OrangeRed;
			this.lblStatus.Location = new System.Drawing.Point(90, 84);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(189, 14);
			this.lblStatus.TabIndex = 6;
			this.lblStatus.Text = "Status";
			this.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.lblStatus.Visible = false;
			//
			//pbLock
			//
			this.pbLock.Image = global::My.Resources.Resources.Lock;
			this.pbLock.Location = new System.Drawing.Point(7, 8);
			this.pbLock.Name = "pbLock";
			this.pbLock.Size = new System.Drawing.Size(64, 64);
			this.pbLock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pbLock.TabIndex = 7;
			this.pbLock.TabStop = false;
			//
			//pnlImage
			//
			this.pnlImage.Controls.Add(this.pbLock);
			this.pnlImage.Location = new System.Drawing.Point(9, 6);
			this.pnlImage.Name = "pnlImage";
			this.pnlImage.Size = new System.Drawing.Size(100, 100);
			this.pnlImage.TabIndex = 8;
			//
			//frmPassword
			//
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF((float) (6.0F), (float) (13.0F));
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(289, 133);
			this.ControlBox = false;
			this.Controls.Add(this.txtVerify);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.lblVerify);
			this.Controls.Add(this.lblPassword);
			this.Controls.Add(this.pnlImage);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmPassword";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = Language.strTitlePassword;
			((System.ComponentModel.ISupportInitialize) this.pbLock).EndInit();
			this.pnlImage.ResumeLayout(false);
			this.pnlImage.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
			
		}
		internal System.Windows.Forms.TextBox txtPassword;
		internal System.Windows.Forms.TextBox txtVerify;
		internal System.Windows.Forms.Label lblPassword;
		internal System.Windows.Forms.Label lblVerify;
		internal System.Windows.Forms.Button btnOK;
		internal System.Windows.Forms.Button btnCancel;
		internal System.Windows.Forms.Label lblStatus;
		internal System.Windows.Forms.PictureBox pbLock;
		internal System.Windows.Forms.Panel pnlImage;
	}
	
}
