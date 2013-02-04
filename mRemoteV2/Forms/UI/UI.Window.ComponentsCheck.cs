using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using AxWFICALib;
using WFICALib;
using mRemoteNC.App;
using My;
using WeifenLuo.WinFormsUI.Docking;
using mRemoteNC.Tools;

namespace mRemoteNC
{
    namespace UI
    {
        namespace Window
        {
            public class ComponentsCheck : Base
            {
                #region Form Stuff

                internal System.Windows.Forms.PictureBox pbCheck1;
                internal System.Windows.Forms.Label lblCheck1;
                internal System.Windows.Forms.Panel pnlCheck2;
                internal System.Windows.Forms.Label lblCheck2;
                internal System.Windows.Forms.PictureBox pbCheck2;
                internal System.Windows.Forms.Panel pnlCheck3;
                internal System.Windows.Forms.Label lblCheck3;
                internal System.Windows.Forms.PictureBox pbCheck3;
                internal System.Windows.Forms.Panel pnlCheck4;
                internal System.Windows.Forms.Label lblCheck4;
                internal System.Windows.Forms.PictureBox pbCheck4;
                internal System.Windows.Forms.Panel pnlCheck5;
                internal System.Windows.Forms.Label lblCheck5;
                internal System.Windows.Forms.PictureBox pbCheck5;
                internal System.Windows.Forms.Button btnCheckAgain;
                internal System.Windows.Forms.TextBox txtCheck1;
                internal System.Windows.Forms.TextBox txtCheck2;
                internal System.Windows.Forms.TextBox txtCheck3;
                internal System.Windows.Forms.TextBox txtCheck4;
                internal System.Windows.Forms.TextBox txtCheck5;
                internal System.Windows.Forms.CheckBox chkAlwaysShow;
                internal System.Windows.Forms.Panel pnlChecks;
                internal System.Windows.Forms.Panel pnlCheck6;
                internal System.Windows.Forms.TextBox txtCheck6;
                internal System.Windows.Forms.Label lblCheck6;
                internal System.Windows.Forms.PictureBox pbCheck6;
                internal System.Windows.Forms.Panel pnlCheck7;
                internal System.Windows.Forms.TextBox txtCheck7;
                internal System.Windows.Forms.Label lblCheck7;
                internal System.Windows.Forms.PictureBox pbCheck7;
                internal TextBox txtCheck8;
                internal Panel pnlCheck8;
                internal Label lblCheck8;
                internal PictureBox pbCheck8;
                internal System.Windows.Forms.Panel pnlCheck1;

                private void InitializeComponent()
                {
            this.pnlCheck1 = new System.Windows.Forms.Panel();
            this.txtCheck1 = new System.Windows.Forms.TextBox();
            this.lblCheck1 = new System.Windows.Forms.Label();
            this.pbCheck1 = new System.Windows.Forms.PictureBox();
            this.pnlCheck2 = new System.Windows.Forms.Panel();
            this.txtCheck2 = new System.Windows.Forms.TextBox();
            this.lblCheck2 = new System.Windows.Forms.Label();
            this.pbCheck2 = new System.Windows.Forms.PictureBox();
            this.pnlCheck3 = new System.Windows.Forms.Panel();
            this.txtCheck3 = new System.Windows.Forms.TextBox();
            this.lblCheck3 = new System.Windows.Forms.Label();
            this.pbCheck3 = new System.Windows.Forms.PictureBox();
            this.pnlCheck4 = new System.Windows.Forms.Panel();
            this.txtCheck4 = new System.Windows.Forms.TextBox();
            this.lblCheck4 = new System.Windows.Forms.Label();
            this.pbCheck4 = new System.Windows.Forms.PictureBox();
            this.pnlCheck5 = new System.Windows.Forms.Panel();
            this.txtCheck5 = new System.Windows.Forms.TextBox();
            this.lblCheck5 = new System.Windows.Forms.Label();
            this.pbCheck5 = new System.Windows.Forms.PictureBox();
            this.btnCheckAgain = new System.Windows.Forms.Button();
            this.chkAlwaysShow = new System.Windows.Forms.CheckBox();
            this.pnlChecks = new System.Windows.Forms.Panel();
            this.pnlCheck7 = new System.Windows.Forms.Panel();
            this.txtCheck7 = new System.Windows.Forms.TextBox();
            this.lblCheck7 = new System.Windows.Forms.Label();
            this.pbCheck7 = new System.Windows.Forms.PictureBox();
            this.pnlCheck6 = new System.Windows.Forms.Panel();
            this.txtCheck6 = new System.Windows.Forms.TextBox();
            this.lblCheck6 = new System.Windows.Forms.Label();
            this.pbCheck6 = new System.Windows.Forms.PictureBox();
            this.txtCheck8 = new System.Windows.Forms.TextBox();
            this.pnlCheck8 = new System.Windows.Forms.Panel();
            this.lblCheck8 = new System.Windows.Forms.Label();
            this.pbCheck8 = new System.Windows.Forms.PictureBox();
            this.pnlCheck1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCheck1)).BeginInit();
            this.pnlCheck2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCheck2)).BeginInit();
            this.pnlCheck3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCheck3)).BeginInit();
            this.pnlCheck4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCheck4)).BeginInit();
            this.pnlCheck5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCheck5)).BeginInit();
            this.pnlChecks.SuspendLayout();
            this.pnlCheck7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCheck7)).BeginInit();
            this.pnlCheck6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCheck6)).BeginInit();
            this.pnlCheck8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCheck8)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCheck1
            // 
            this.pnlCheck1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCheck1.Controls.Add(this.txtCheck1);
            this.pnlCheck1.Controls.Add(this.lblCheck1);
            this.pnlCheck1.Controls.Add(this.pbCheck1);
            this.pnlCheck1.Location = new System.Drawing.Point(3, 3);
            this.pnlCheck1.Name = "pnlCheck1";
            this.pnlCheck1.Size = new System.Drawing.Size(562, 100);
            this.pnlCheck1.TabIndex = 10;
            this.pnlCheck1.Visible = false;
            // 
            // txtCheck1
            // 
            this.txtCheck1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCheck1.BackColor = System.Drawing.SystemColors.Control;
            this.txtCheck1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCheck1.Location = new System.Drawing.Point(129, 29);
            this.txtCheck1.Multiline = true;
            this.txtCheck1.Name = "txtCheck1";
            this.txtCheck1.ReadOnly = true;
            this.txtCheck1.Size = new System.Drawing.Size(430, 67);
            this.txtCheck1.TabIndex = 2;
            // 
            // lblCheck1
            // 
            this.lblCheck1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCheck1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheck1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCheck1.Location = new System.Drawing.Point(108, 3);
            this.lblCheck1.Name = "lblCheck1";
            this.lblCheck1.Size = new System.Drawing.Size(451, 23);
            this.lblCheck1.TabIndex = 1;
            this.lblCheck1.Text = "RDP check succeeded!";
            // 
            // pbCheck1
            // 
            this.pbCheck1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pbCheck1.Location = new System.Drawing.Point(6, 4);
            this.pbCheck1.Name = "pbCheck1";
            this.pbCheck1.Size = new System.Drawing.Size(72, 93);
            this.pbCheck1.TabIndex = 0;
            this.pbCheck1.TabStop = false;
            // 
            // pnlCheck2
            // 
            this.pnlCheck2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCheck2.Controls.Add(this.txtCheck2);
            this.pnlCheck2.Controls.Add(this.lblCheck2);
            this.pnlCheck2.Controls.Add(this.pbCheck2);
            this.pnlCheck2.Location = new System.Drawing.Point(6, 109);
            this.pnlCheck2.Name = "pnlCheck2";
            this.pnlCheck2.Size = new System.Drawing.Size(562, 100);
            this.pnlCheck2.TabIndex = 20;
            this.pnlCheck2.Visible = false;
            // 
            // txtCheck2
            // 
            this.txtCheck2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCheck2.BackColor = System.Drawing.SystemColors.Control;
            this.txtCheck2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCheck2.Location = new System.Drawing.Point(129, 29);
            this.txtCheck2.Multiline = true;
            this.txtCheck2.Name = "txtCheck2";
            this.txtCheck2.ReadOnly = true;
            this.txtCheck2.Size = new System.Drawing.Size(430, 67);
            this.txtCheck2.TabIndex = 2;
            // 
            // lblCheck2
            // 
            this.lblCheck2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCheck2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheck2.Location = new System.Drawing.Point(112, 3);
            this.lblCheck2.Name = "lblCheck2";
            this.lblCheck2.Size = new System.Drawing.Size(447, 23);
            this.lblCheck2.TabIndex = 1;
            this.lblCheck2.Text = "RDP check succeeded!";
            // 
            // pbCheck2
            // 
            this.pbCheck2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pbCheck2.Location = new System.Drawing.Point(3, 3);
            this.pbCheck2.Name = "pbCheck2";
            this.pbCheck2.Size = new System.Drawing.Size(72, 93);
            this.pbCheck2.TabIndex = 0;
            this.pbCheck2.TabStop = false;
            // 
            // pnlCheck3
            // 
            this.pnlCheck3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCheck3.Controls.Add(this.txtCheck3);
            this.pnlCheck3.Controls.Add(this.lblCheck3);
            this.pnlCheck3.Controls.Add(this.pbCheck3);
            this.pnlCheck3.Location = new System.Drawing.Point(6, 215);
            this.pnlCheck3.Name = "pnlCheck3";
            this.pnlCheck3.Size = new System.Drawing.Size(562, 100);
            this.pnlCheck3.TabIndex = 30;
            this.pnlCheck3.Visible = false;
            // 
            // txtCheck3
            // 
            this.txtCheck3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCheck3.BackColor = System.Drawing.SystemColors.Control;
            this.txtCheck3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCheck3.Location = new System.Drawing.Point(129, 29);
            this.txtCheck3.Multiline = true;
            this.txtCheck3.Name = "txtCheck3";
            this.txtCheck3.ReadOnly = true;
            this.txtCheck3.Size = new System.Drawing.Size(430, 67);
            this.txtCheck3.TabIndex = 2;
            // 
            // lblCheck3
            // 
            this.lblCheck3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCheck3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheck3.Location = new System.Drawing.Point(112, 3);
            this.lblCheck3.Name = "lblCheck3";
            this.lblCheck3.Size = new System.Drawing.Size(447, 23);
            this.lblCheck3.TabIndex = 1;
            this.lblCheck3.Text = "RDP check succeeded!";
            // 
            // pbCheck3
            // 
            this.pbCheck3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pbCheck3.Location = new System.Drawing.Point(3, 3);
            this.pbCheck3.Name = "pbCheck3";
            this.pbCheck3.Size = new System.Drawing.Size(72, 93);
            this.pbCheck3.TabIndex = 0;
            this.pbCheck3.TabStop = false;
            this.pbCheck3.Click += new System.EventHandler(this.pbCheck3_Click);
            // 
            // pnlCheck4
            // 
            this.pnlCheck4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCheck4.Controls.Add(this.txtCheck4);
            this.pnlCheck4.Controls.Add(this.lblCheck4);
            this.pnlCheck4.Controls.Add(this.pbCheck4);
            this.pnlCheck4.Location = new System.Drawing.Point(3, 321);
            this.pnlCheck4.Name = "pnlCheck4";
            this.pnlCheck4.Size = new System.Drawing.Size(562, 100);
            this.pnlCheck4.TabIndex = 40;
            this.pnlCheck4.Visible = false;
            // 
            // txtCheck4
            // 
            this.txtCheck4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCheck4.BackColor = System.Drawing.SystemColors.Control;
            this.txtCheck4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCheck4.Location = new System.Drawing.Point(129, 30);
            this.txtCheck4.Multiline = true;
            this.txtCheck4.Name = "txtCheck4";
            this.txtCheck4.ReadOnly = true;
            this.txtCheck4.Size = new System.Drawing.Size(430, 67);
            this.txtCheck4.TabIndex = 2;
            // 
            // lblCheck4
            // 
            this.lblCheck4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCheck4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheck4.Location = new System.Drawing.Point(112, 3);
            this.lblCheck4.Name = "lblCheck4";
            this.lblCheck4.Size = new System.Drawing.Size(447, 23);
            this.lblCheck4.TabIndex = 1;
            this.lblCheck4.Text = "RDP check succeeded!";
            // 
            // pbCheck4
            // 
            this.pbCheck4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pbCheck4.Location = new System.Drawing.Point(3, 3);
            this.pbCheck4.Name = "pbCheck4";
            this.pbCheck4.Size = new System.Drawing.Size(72, 93);
            this.pbCheck4.TabIndex = 0;
            this.pbCheck4.TabStop = false;
            // 
            // pnlCheck5
            // 
            this.pnlCheck5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCheck5.Controls.Add(this.txtCheck5);
            this.pnlCheck5.Controls.Add(this.lblCheck5);
            this.pnlCheck5.Controls.Add(this.pbCheck5);
            this.pnlCheck5.Location = new System.Drawing.Point(3, 424);
            this.pnlCheck5.Name = "pnlCheck5";
            this.pnlCheck5.Size = new System.Drawing.Size(562, 100);
            this.pnlCheck5.TabIndex = 50;
            this.pnlCheck5.Visible = false;
            // 
            // txtCheck5
            // 
            this.txtCheck5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCheck5.BackColor = System.Drawing.SystemColors.Control;
            this.txtCheck5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCheck5.Location = new System.Drawing.Point(129, 29);
            this.txtCheck5.Multiline = true;
            this.txtCheck5.Name = "txtCheck5";
            this.txtCheck5.ReadOnly = true;
            this.txtCheck5.Size = new System.Drawing.Size(430, 67);
            this.txtCheck5.TabIndex = 2;
            // 
            // lblCheck5
            // 
            this.lblCheck5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCheck5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheck5.Location = new System.Drawing.Point(112, 3);
            this.lblCheck5.Name = "lblCheck5";
            this.lblCheck5.Size = new System.Drawing.Size(447, 23);
            this.lblCheck5.TabIndex = 1;
            this.lblCheck5.Text = "RDP check succeeded!";
            // 
            // pbCheck5
            // 
            this.pbCheck5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pbCheck5.Location = new System.Drawing.Point(3, 3);
            this.pbCheck5.Name = "pbCheck5";
            this.pbCheck5.Size = new System.Drawing.Size(72, 93);
            this.pbCheck5.TabIndex = 0;
            this.pbCheck5.TabStop = false;
            this.pbCheck5.Click += new System.EventHandler(this.pbCheck5_Click);
            // 
            // btnCheckAgain
            // 
            this.btnCheckAgain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheckAgain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckAgain.Location = new System.Drawing.Point(476, 895);
            this.btnCheckAgain.Name = "btnCheckAgain";
            this.btnCheckAgain.Size = new System.Drawing.Size(104, 23);
            this.btnCheckAgain.TabIndex = 0;
            this.btnCheckAgain.Text = "Check again";
            this.btnCheckAgain.UseVisualStyleBackColor = true;
            this.btnCheckAgain.Click += new System.EventHandler(this.btnCheckAgain_Click);
            // 
            // chkAlwaysShow
            // 
            this.chkAlwaysShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAlwaysShow.AutoSize = true;
            this.chkAlwaysShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAlwaysShow.Location = new System.Drawing.Point(12, 899);
            this.chkAlwaysShow.Name = "chkAlwaysShow";
            this.chkAlwaysShow.Size = new System.Drawing.Size(185, 17);
            this.chkAlwaysShow.TabIndex = 51;
            this.chkAlwaysShow.Text = "Always show this screen at startup";
            this.chkAlwaysShow.UseVisualStyleBackColor = true;
            this.chkAlwaysShow.CheckedChanged += new System.EventHandler(this.chkAlwaysShow_CheckedChanged);
            // 
            // pnlChecks
            // 
            this.pnlChecks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlChecks.AutoScroll = true;
            this.pnlChecks.Controls.Add(this.pnlCheck1);
            this.pnlChecks.Controls.Add(this.pnlCheck7);
            this.pnlChecks.Controls.Add(this.pnlCheck2);
            this.pnlChecks.Controls.Add(this.pnlCheck3);
            this.pnlChecks.Controls.Add(this.pnlCheck6);
            this.pnlChecks.Controls.Add(this.pnlCheck5);
            this.pnlChecks.Controls.Add(this.pnlCheck4);
            this.pnlChecks.Location = new System.Drawing.Point(12, 12);
            this.pnlChecks.Name = "pnlChecks";
            this.pnlChecks.Size = new System.Drawing.Size(568, 877);
            this.pnlChecks.TabIndex = 52;
            // 
            // pnlCheck7
            // 
            this.pnlCheck7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCheck7.Controls.Add(this.txtCheck7);
            this.pnlCheck7.Controls.Add(this.lblCheck7);
            this.pnlCheck7.Controls.Add(this.pbCheck7);
            this.pnlCheck7.Location = new System.Drawing.Point(0, 636);
            this.pnlCheck7.Name = "pnlCheck7";
            this.pnlCheck7.Size = new System.Drawing.Size(562, 100);
            this.pnlCheck7.TabIndex = 51;
            // 
            // txtCheck7
            // 
            this.txtCheck7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCheck7.BackColor = System.Drawing.SystemColors.Control;
            this.txtCheck7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCheck7.Location = new System.Drawing.Point(129, 29);
            this.txtCheck7.Multiline = true;
            this.txtCheck7.Name = "txtCheck7";
            this.txtCheck7.ReadOnly = true;
            this.txtCheck7.Size = new System.Drawing.Size(430, 67);
            this.txtCheck7.TabIndex = 2;
            // 
            // lblCheck7
            // 
            this.lblCheck7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCheck7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheck7.Location = new System.Drawing.Point(112, 3);
            this.lblCheck7.Name = "lblCheck7";
            this.lblCheck7.Size = new System.Drawing.Size(447, 23);
            this.lblCheck7.TabIndex = 1;
            this.lblCheck7.Text = "RDP check succeeded!";
            // 
            // pbCheck7
            // 
            this.pbCheck7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pbCheck7.Location = new System.Drawing.Point(3, 3);
            this.pbCheck7.Name = "pbCheck7";
            this.pbCheck7.Size = new System.Drawing.Size(72, 93);
            this.pbCheck7.TabIndex = 0;
            this.pbCheck7.TabStop = false;
            this.pbCheck7.Click += new System.EventHandler(this.pbCheck7_Click);
            // 
            // pnlCheck6
            // 
            this.pnlCheck6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCheck6.Controls.Add(this.txtCheck6);
            this.pnlCheck6.Controls.Add(this.lblCheck6);
            this.pnlCheck6.Controls.Add(this.pbCheck6);
            this.pnlCheck6.Location = new System.Drawing.Point(3, 530);
            this.pnlCheck6.Name = "pnlCheck6";
            this.pnlCheck6.Size = new System.Drawing.Size(562, 100);
            this.pnlCheck6.TabIndex = 50;
            this.pnlCheck6.Visible = false;
            // 
            // txtCheck6
            // 
            this.txtCheck6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCheck6.BackColor = System.Drawing.SystemColors.Control;
            this.txtCheck6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCheck6.Location = new System.Drawing.Point(129, 29);
            this.txtCheck6.Multiline = true;
            this.txtCheck6.Name = "txtCheck6";
            this.txtCheck6.ReadOnly = true;
            this.txtCheck6.Size = new System.Drawing.Size(430, 67);
            this.txtCheck6.TabIndex = 2;
            // 
            // lblCheck6
            // 
            this.lblCheck6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCheck6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheck6.Location = new System.Drawing.Point(112, 3);
            this.lblCheck6.Name = "lblCheck6";
            this.lblCheck6.Size = new System.Drawing.Size(447, 23);
            this.lblCheck6.TabIndex = 1;
            this.lblCheck6.Text = "RDP check succeeded!";
            // 
            // pbCheck6
            // 
            this.pbCheck6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pbCheck6.Location = new System.Drawing.Point(3, 7);
            this.pbCheck6.Name = "pbCheck6";
            this.pbCheck6.Size = new System.Drawing.Size(72, 93);
            this.pbCheck6.TabIndex = 0;
            this.pbCheck6.TabStop = false;
            this.pbCheck6.Click += new System.EventHandler(this.pbCheck6_Click);
            // 
            // txtCheck8
            // 
            this.txtCheck8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCheck8.BackColor = System.Drawing.SystemColors.Control;
            this.txtCheck8.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCheck8.Location = new System.Drawing.Point(129, 29);
            this.txtCheck8.Multiline = true;
            this.txtCheck8.Name = "txtCheck8";
            this.txtCheck8.ReadOnly = true;
            this.txtCheck8.Size = new System.Drawing.Size(430, 67);
            this.txtCheck8.TabIndex = 2;
            // 
            // pnlCheck8
            // 
            this.pnlCheck8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCheck8.Controls.Add(this.txtCheck8);
            this.pnlCheck8.Controls.Add(this.lblCheck8);
            this.pnlCheck8.Controls.Add(this.pbCheck8);
            this.pnlCheck8.Location = new System.Drawing.Point(12, 754);
            this.pnlCheck8.Name = "pnlCheck8";
            this.pnlCheck8.Size = new System.Drawing.Size(562, 100);
            this.pnlCheck8.TabIndex = 52;
            // 
            // lblCheck8
            // 
            this.lblCheck8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCheck8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheck8.Location = new System.Drawing.Point(112, 3);
            this.lblCheck8.Name = "lblCheck8";
            this.lblCheck8.Size = new System.Drawing.Size(447, 23);
            this.lblCheck8.TabIndex = 1;
            this.lblCheck8.Text = "RDP check succeeded!";
            // 
            // pbCheck8
            // 
            this.pbCheck8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pbCheck8.Location = new System.Drawing.Point(3, 3);
            this.pbCheck8.Name = "pbCheck8";
            this.pbCheck8.Size = new System.Drawing.Size(72, 93);
            this.pbCheck8.TabIndex = 0;
            this.pbCheck8.TabStop = false;
            this.pbCheck8.Click += new System.EventHandler(this.pbCheck8_Click);
            // 
            // ComponentsCheck
            // 
            this.ClientSize = new System.Drawing.Size(592, 930);
            this.Controls.Add(this.pnlCheck8);
            this.Controls.Add(this.pnlChecks);
            this.Controls.Add(this.chkAlwaysShow);
            this.Controls.Add(this.btnCheckAgain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = global::My.Resources.Resources.ComponentsCheck_Icon;
            this.Name = "ComponentsCheck";
            this.TabText = "Components Check";
            this.Text = "Components Check";
            this.Load += new System.EventHandler(this.ComponentsCheck_Load);
            this.pnlCheck1.ResumeLayout(false);
            this.pnlCheck1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCheck1)).EndInit();
            this.pnlCheck2.ResumeLayout(false);
            this.pnlCheck2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCheck2)).EndInit();
            this.pnlCheck3.ResumeLayout(false);
            this.pnlCheck3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCheck3)).EndInit();
            this.pnlCheck4.ResumeLayout(false);
            this.pnlCheck4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCheck4)).EndInit();
            this.pnlCheck5.ResumeLayout(false);
            this.pnlCheck5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCheck5)).EndInit();
            this.pnlChecks.ResumeLayout(false);
            this.pnlCheck7.ResumeLayout(false);
            this.pnlCheck7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCheck7)).EndInit();
            this.pnlCheck6.ResumeLayout(false);
            this.pnlCheck6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCheck6)).EndInit();
            this.pnlCheck8.ResumeLayout(false);
            this.pnlCheck8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCheck8)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

                }

                #endregion Form Stuff

                #region Public Methods

                public ComponentsCheck(DockContent Panel)
                {
                    this.WindowType = Type.ComponentsCheck;
                    this.DockPnl = Panel;
                    this.InitializeComponent();
                }

                #endregion Public Methods

                #region Form Stuff

                private void ComponentsCheck_Load(object sender, System.EventArgs e)
                {
                    ApplyLanguage();

                    chkAlwaysShow.Checked = System.Convert.ToBoolean(Settings.Default.StartupComponentsCheck);
                    CheckComponents();
                }

                private void ApplyLanguage()
                {
                    TabText = Language.strComponentsCheck;
                    Text = Language.strComponentsCheck;
                    chkAlwaysShow.Text = Language.strCcAlwaysShowScreen;
                    btnCheckAgain.Text = Language.strCcCheckAgain;
                }

                private void btnCheckAgain_Click(System.Object sender, System.EventArgs e)
                {
                    CheckComponents();
                }

                private void chkAlwaysShow_CheckedChanged(System.Object sender, System.EventArgs e)
                {
                    Settings.Default.StartupComponentsCheck = chkAlwaysShow.Checked;
                    Settings.Default.Save();
                }

                #endregion Form Stuff

                #region Private Methods

                private void CheckComponents()
                {
                    string errorMsg = Language.strCcNotInstalledProperly;

                    pnlCheck1.Visible = true;
                    pnlCheck2.Visible = true;
                    pnlCheck3.Visible = true;
                    pnlCheck4.Visible = true;
                    pnlCheck5.Visible = true;
                    pnlCheck6.Visible = true;
                    pnlCheck7.Visible = true;
                    pnlCheck8.Visible = true;

                    if (ProblemFixer.IsRDPOk())
                    {
                        pbCheck1.Image = global::My.Resources.Resources.Good_Symbol;
                        lblCheck1.ForeColor = Color.DarkOliveGreen;
                        lblCheck1.Text = "RDP (Remote Desktop) " + Language.strCcCheckSucceeded;
                        txtCheck1.Text = string.Format(Language.strCcRDPOK, ProblemFixer.RDPVer);
                    }
                    else
                    {
                        pbCheck1.Image = global::My.Resources.Resources.Bad_Symbol;
                        lblCheck1.ForeColor = Color.Firebrick;
                        lblCheck1.Text = "RDP (Remote Desktop) " + Language.strCcCheckFailed;
                        txtCheck1.Text = Language.strCcRDPFailed;

                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg, "RDP " + errorMsg, true);
                    }
                    
                    

                    if (ProblemFixer.IsVNCOk())
                    {
                        pbCheck2.Image = global::My.Resources.Resources.Good_Symbol;
                        lblCheck2.ForeColor = Color.DarkOliveGreen;
                        lblCheck2.Text = "VNC (Virtual Network Computing) " + Language.strCcCheckSucceeded;
                        txtCheck2.Text = string.Format(Language.strCcVNCOK,ProblemFixer.VNCVer);
                    }
                    else
                    {
                        pbCheck2.Image = global::My.Resources.Resources.Bad_Symbol;
                        lblCheck2.ForeColor = Color.Firebrick;
                        lblCheck2.Text = "VNC (Virtual Network Computing) " + Language.strCcCheckFailed;
                        txtCheck2.Text = Language.strCcVNCFailed;

                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg, "VNC " + errorMsg, true);
                    }

                    
                    if (ProblemFixer.IsPuTTYOk())
                    {
                        pbCheck3.Image = global::My.Resources.Resources.Good_Symbol;
                        lblCheck3.ForeColor = Color.DarkOliveGreen;
                        lblCheck3.Text = "PuTTY (SSH/Telnet/Rlogin/RAW) " + Language.strCcCheckSucceeded;
                        txtCheck3.Text = Language.strCcPuttyOK;
                    }
                    else
                    {
                        pbCheck3.Image = global::My.Resources.Resources.Bad_Symbol;
                        lblCheck3.ForeColor = Color.Firebrick;
                        lblCheck3.Text = "PuTTY (SSH/Telnet/Rlogin/RAW) " + Language.strCcCheckFailed;
                        txtCheck3.Text = Language.strCcClickOnSign;

                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg, "PuTTY " + errorMsg, true);
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            "File does not exist.", true);
                    }

                    if (ProblemFixer.IsICAOk())
                    {

                        pbCheck4.Image = global::My.Resources.Resources.Good_Symbol;
                        lblCheck4.ForeColor = Color.DarkOliveGreen;
                        lblCheck4.Text = "ICA (Citrix ICA) " + Language.strCcCheckSucceeded;
                        txtCheck4.Text = string.Format(Language.strCcICAOK, "FixMe");
                        throw new Exception();
                    }
                    else
                    {
                        pbCheck4.Image = global::My.Resources.Resources.Bad_Symbol;
                        lblCheck4.ForeColor = Color.Firebrick;
                        lblCheck4.Text = "ICA (Citrix ICA) " + Language.strCcCheckFailed;
                        txtCheck4.Text = Language.strCcICAFailed;

                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg, "ICA " + errorMsg, true);
                    }

                    if (ProblemFixer.IsGeckoOk())
                    {
                        pbCheck5.Image = global::My.Resources.Resources.Good_Symbol;
                        lblCheck5.ForeColor = Color.DarkOliveGreen;
                        lblCheck5.Text = "Gecko (Firefox) Rendering Engine (HTTP/S) " + Language.strCcCheckSucceeded;
                        txtCheck5.Text = Language.strCcGeckoOK + " Gecko version: " + ProblemFixer.XulVer;
                    }
                    else
                    {
                        pbCheck5.Image = global::My.Resources.Resources.Bad_Symbol;
                        lblCheck5.ForeColor = Color.Firebrick;
                        lblCheck5.Text = "Gecko (Firefox) Rendering Engine (HTTP/S) " + Language.strCcCheckFailed;
                        txtCheck5.Text = Language.strCcClickOnSign;

                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg, "Gecko " + errorMsg, true);
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            "XULrunner was not found in " +
                                                            Settings.Default.XULRunnerPath, true);
                    }

                    if (ProblemFixer.IsRDPSessionsOk())
                    {
                        pbCheck6.Image = global::My.Resources.Resources.Good_Symbol;
                        lblCheck6.ForeColor = Color.DarkOliveGreen;
                        lblCheck6.Text = "(RDP) Sessions " + Language.strCcCheckSucceeded;
                        txtCheck6.Text = Language.strCcEOLOK;
                    }
                    else
                    {
                        pbCheck6.Image = global::My.Resources.Resources.Bad_Symbol;
                        lblCheck6.ForeColor = Color.Firebrick;
                        lblCheck6.Text = "(RDP) Sessions " + Language.strCcCheckFailed;
                        txtCheck6.Text = Language.strCcClickOnSign;

                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg, "EOLWTSCOM " + errorMsg,
                                                            true);
                    }

                    if (ProblemFixer.IsTeamViewerOk())
                    {
                        pbCheck7.Image = global::My.Resources.Resources.Good_Symbol;
                        lblCheck7.ForeColor = Color.DarkOliveGreen;
                        lblCheck7.Text = "TeamViewer " + Language.strCcCheckSucceeded;
                        txtCheck7.Text = "TeamViewer Version: "+ProblemFixer.TVVer;
                    }
                    else
                    {
                        pbCheck7.Image = global::My.Resources.Resources.Bad_Symbol;
                        lblCheck7.ForeColor = Color.Firebrick;
                        lblCheck7.Text = "TeamViewer " + Language.strCcCheckFailed;
                        txtCheck7.Text = Language.strCcClickOnSign;
                    }

                    if (ProblemFixer.IsRAdminOk())
                    {
                        pbCheck8.Image = global::My.Resources.Resources.Good_Symbol;
                        lblCheck8.ForeColor = Color.DarkOliveGreen;
                        lblCheck8.Text = "RAdmin " + Language.strCcCheckSucceeded;
                        txtCheck8.Text = "RAdmin Version: " + ProblemFixer.RAVer;
                    }
                    else
                    {
                        pbCheck8.Image = global::My.Resources.Resources.Bad_Symbol;
                        lblCheck8.ForeColor = Color.Firebrick;
                        lblCheck8.Text = "RAdmin " + Language.strCcCheckFailed;
                        txtCheck8.Text = Language.strCcClickOnSign;
                    }
                }

                #endregion Private Methods

                private void pbCheck7_Click(object sender, EventArgs e)
                {
                    if (!ProblemFixer.IsTeamViewerOk())
                    {
                        ProblemFixer.FixTVProblem();
                        CheckComponents();
                    }
                }

                private void pbCheck5_Click(object sender, EventArgs e)
                {
                    if (!ProblemFixer.IsGeckoOk())
                    {
                        ProblemFixer.FixGeckoProblem();
                        CheckComponents();
                    }
                }

                private void pbCheck3_Click(object sender, EventArgs e)
                {
                    if (!ProblemFixer.IsPuTTYOk())
                    {
                        ProblemFixer.FixPuTTYProblem();
                        CheckComponents();
                    }
                }

                private void pbCheck6_Click(object sender, EventArgs e)
                {
                    if (!ProblemFixer.IsRDPSessionsOk())
                    {
                        ProblemFixer.FixEolProblem();
                        CheckComponents();
                    }
                }

                private void pbCheck8_Click(object sender, EventArgs e)
                {
                    if (!ProblemFixer.IsRAdminOk())
                    {
                        ProblemFixer.FixRAdminProblem();
                        CheckComponents();
                    }
                }
            }
        }
    }
}