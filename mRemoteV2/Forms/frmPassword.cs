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
    public partial class frmPassword
    {
        public string Username
        {
            get { return txtPassword.Text; }
        }

        public string Password
        {
            get
            {
                if (_Verify == true)
                {
                    return txtVerify.Text;
                }
                else
                {
                    return txtPassword.Text;
                }
            }
        }

        private bool _Verify = true;

        public bool Verify
        {
            get { return _Verify; }
            set { _Verify = value; }
        }

        private bool _UserAndPass = false;


        public frmPassword(bool UserAndPass = false, string title = "Security")
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            // Add any initialization after the InitializeComponent() call.
            this.Text = title;

            if (UserAndPass == true)
            {
                _UserAndPass = true;
                lblPassword.Text = "Username:";
                lblVerify.Text = "Password:";
                txtPassword.UseSystemPasswordChar = false;
                txtVerify.UseSystemPasswordChar = true;
            }
        }

        public void btnCancel_Click(System.Object sender, System.EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        public void btnOK_Click(System.Object sender, System.EventArgs e)
        {
            if (Verify == true && _UserAndPass == false)
            {
                if (VerifyOK())
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private bool VerifyOK()
        {
            if (txtPassword.Text.Length >= 3)
            {
                if (txtPassword.Text == txtVerify.Text)
                {
                    return true;
                }
                else
                {
                    lblStatus.Visible = true;
                    lblStatus.Text = "Passwords don\'t match!";
                    return false;
                }
            }
            else
            {
                lblStatus.Visible = true;
                lblStatus.Text = "3 characters is minimum!";
                return false;
            }
        }

        public void frmPassword_Load(object sender, System.EventArgs e)
        {
            ApplyLanguage();

            if (Verify == false)
            {
                this.Height = 124;
                this.lblVerify.Visible = false;
                this.txtVerify.Visible = false;
            }
        }

        private void ApplyLanguage()
        {
            lblPassword.Text = Language.strLabelPassword;
            lblVerify.Text = Language.strLabelVerify;
            btnOK.Text = Language.strButtonOK;
            btnCancel.Text = Language.strButtonCancel;
            lblStatus.Text = "Status";
            Text = Language.strTitlePassword;
        }

        public void txtPassword_GotFocus(object sender, System.EventArgs e)
        {
            if (this.txtPassword.TextLength > 0)
            {
                this.txtPassword.SelectionStart = 0;
                this.txtPassword.SelectionLength = this.txtPassword.TextLength;
            }
        }

        public void txtVerify_GotFocus(object sender, System.EventArgs e)
        {
            if (this.txtVerify.TextLength > 0)
            {
                this.txtVerify.SelectionStart = 0;
                this.txtVerify.SelectionLength = this.txtVerify.TextLength;
            }
        }
    }
}