using My;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuickHostScanner));
            this.lblPorts = new System.Windows.Forms.Label();
            this.txtPorts = new System.Windows.Forms.TextBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.btnScan = new System.Windows.Forms.Button();
            this.lstStatus = new System.Windows.Forms.ListView();
            this.chPort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chProtocol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblHost = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblPorts
            // 
            resources.ApplyResources(this.lblPorts, "lblPorts");
            this.lblPorts.Name = "lblPorts";
            // 
            // txtPorts
            // 
            resources.ApplyResources(this.txtPorts, "txtPorts");
            this.txtPorts.Name = "txtPorts";
            // 
            // txtIP
            // 
            resources.ApplyResources(this.txtIP, "txtIP");
            this.txtIP.Name = "txtIP";
            // 
            // btnScan
            // 
            resources.ApplyResources(this.btnScan, "btnScan");
            this.btnScan.Name = "btnScan";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.button1_Click);
            // 
            // lstStatus
            // 
            this.lstStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstStatus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chPort,
            this.chProtocol,
            this.chStatus});
            resources.ApplyResources(this.lstStatus, "lstStatus");
            this.lstStatus.FullRowSelect = true;
            this.lstStatus.GridLines = true;
            this.lstStatus.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstStatus.MultiSelect = false;
            this.lstStatus.Name = "lstStatus";
            this.lstStatus.UseCompatibleStateImageBehavior = false;
            this.lstStatus.View = System.Windows.Forms.View.Details;
            this.lstStatus.SelectedIndexChanged += new System.EventHandler(this.lstStatus_SelectedIndexChanged);
            this.lstStatus.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstStatus_MouseClick);
            this.lstStatus.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstStatus_MouseDoubleClick);
            // 
            // chPort
            // 
            resources.ApplyResources(this.chPort, "chPort");
            // 
            // chProtocol
            // 
            resources.ApplyResources(this.chProtocol, "chProtocol");
            // 
            // chStatus
            // 
            resources.ApplyResources(this.chStatus, "chStatus");
            // 
            // lblHost
            // 
            resources.ApplyResources(this.lblHost, "lblHost");
            this.lblHost.Name = "lblHost";
            // 
            // QuickHostScanner
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblHost);
            this.Controls.Add(this.lstStatus);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.txtPorts);
            this.Controls.Add(this.lblPorts);
            this.Name = "QuickHostScanner";
            this.Load += new System.EventHandler(this.QuickHostScanner_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPorts;
        private System.Windows.Forms.Button btnScan;
        internal System.Windows.Forms.TextBox txtPorts;
        internal System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.ListView lstStatus;
        private System.Windows.Forms.ColumnHeader chPort;
        private System.Windows.Forms.ColumnHeader chProtocol;
        private System.Windows.Forms.ColumnHeader chStatus;
        private System.Windows.Forms.Label lblHost;

    }
}