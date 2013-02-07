namespace mRemoteNC.Forms
{
    partial class QuickHostScanner
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPorts = new System.Windows.Forms.TextBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lstStatus = new System.Windows.Forms.ListView();
            this.chPort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chProtocol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Host:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Posrts:";
            // 
            // txtPorts
            // 
            this.txtPorts.Location = new System.Drawing.Point(48, 35);
            this.txtPorts.Name = "txtPorts";
            this.txtPorts.Size = new System.Drawing.Size(221, 20);
            this.txtPorts.TabIndex = 14;
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(48, 6);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(221, 20);
            this.txtIP.TabIndex = 15;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(275, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 46);
            this.button1.TabIndex = 16;
            this.button1.Text = "Scan";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lstStatus
            // 
            this.lstStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstStatus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chPort,
            this.chProtocol,
            this.chStatus});
            this.lstStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lstStatus.FullRowSelect = true;
            this.lstStatus.GridLines = true;
            this.lstStatus.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstStatus.Location = new System.Drawing.Point(0, 68);
            this.lstStatus.MultiSelect = false;
            this.lstStatus.Name = "lstStatus";
            this.lstStatus.Size = new System.Drawing.Size(370, 210);
            this.lstStatus.TabIndex = 17;
            this.lstStatus.UseCompatibleStateImageBehavior = false;
            this.lstStatus.View = System.Windows.Forms.View.Details;
            this.lstStatus.SelectedIndexChanged += new System.EventHandler(this.lstStatus_SelectedIndexChanged);
            this.lstStatus.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstStatus_MouseClick);
            this.lstStatus.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstStatus_MouseDoubleClick);
            // 
            // chPort
            // 
            this.chPort.Text = "Port";
            this.chPort.Width = 114;
            // 
            // chProtocol
            // 
            this.chProtocol.Text = "Protocol";
            this.chProtocol.Width = 116;
            // 
            // chStatus
            // 
            this.chStatus.Text = "Status";
            // 
            // QuickHostScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 278);
            this.Controls.Add(this.lstStatus);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.txtPorts);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "QuickHostScanner";
            this.Text = "QuickHostScanner";
            this.Load += new System.EventHandler(this.QuickHostScanner_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        internal System.Windows.Forms.TextBox txtPorts;
        internal System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.ListView lstStatus;
        private System.Windows.Forms.ColumnHeader chPort;
        private System.Windows.Forms.ColumnHeader chProtocol;
        private System.Windows.Forms.ColumnHeader chStatus;

    }
}