namespace mRemoteNC.Forms
{
    partial class ConnectionStatusForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionStatusForm));
            this.lstStatus = new System.Windows.Forms.ListView();
            this.chConName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chHost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tslTestType = new System.Windows.Forms.ToolStripLabel();
            this.tscbTestType = new System.Windows.Forms.ToolStripComboBox();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbAutoupdate = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstStatus
            // 
            this.lstStatus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chConName,
            this.chHost,
            this.chStatus});
            this.lstStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstStatus.FullRowSelect = true;
            this.lstStatus.GridLines = true;
            this.lstStatus.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstStatus.Location = new System.Drawing.Point(0, 25);
            this.lstStatus.MultiSelect = false;
            this.lstStatus.Name = "lstStatus";
            this.lstStatus.Size = new System.Drawing.Size(319, 333);
            this.lstStatus.TabIndex = 5;
            this.lstStatus.UseCompatibleStateImageBehavior = false;
            this.lstStatus.View = System.Windows.Forms.View.Details;
            this.lstStatus.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstStatus_MouseDoubleClick);
            // 
            // chConName
            // 
            this.chConName.Text = "Connection";
            this.chConName.Width = 114;
            // 
            // chHost
            // 
            this.chHost.Text = "Host";
            this.chHost.Width = 116;
            // 
            // chStatus
            // 
            this.chStatus.Text = "Status";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslTestType,
            this.tscbTestType,
            this.tsbRefresh,
            this.tsbAutoupdate});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(319, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tslTestType
            // 
            this.tslTestType.Name = "tslTestType";
            this.tslTestType.Size = new System.Drawing.Size(58, 22);
            this.tslTestType.Text = "Test type:";
            // 
            // tscbTestType
            // 
            this.tscbTestType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbTestType.Items.AddRange(new object[] {
            "Ping",
            "Connect",
            "Both"});
            this.tscbTestType.Name = "tscbTestType";
            this.tscbTestType.Size = new System.Drawing.Size(121, 25);
            this.tscbTestType.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            this.tscbTestType.Click += new System.EventHandler(this.toolStripComboBox1_Click);
            this.tscbTestType.TextChanged += new System.EventHandler(this.toolStripComboBox1_TextChanged);
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRefresh.Image = global::My.Resources.Resources.Refresh;
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(23, 22);
            this.tsbRefresh.Text = "toolStripButton1";
            this.tsbRefresh.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tsbAutoupdate
            // 
            this.tsbAutoupdate.Checked = true;
            this.tsbAutoupdate.CheckOnClick = true;
            this.tsbAutoupdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbAutoupdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbAutoupdate.Image = ((System.Drawing.Image)(resources.GetObject("tsbAutoupdate.Image")));
            this.tsbAutoupdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAutoupdate.Name = "tsbAutoupdate";
            this.tsbAutoupdate.Size = new System.Drawing.Size(74, 22);
            this.tsbAutoupdate.Text = "Autoupdate";
            this.tsbAutoupdate.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // ConnectionStatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 358);
            this.Controls.Add(this.lstStatus);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConnectionStatusForm";
            this.Text = "Connections status";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConnectionStatusForm_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstStatus;
        private System.Windows.Forms.ColumnHeader chConName;
        private System.Windows.Forms.ColumnHeader chHost;
        private System.Windows.Forms.ColumnHeader chStatus;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel tslTestType;
        private System.Windows.Forms.ToolStripComboBox tscbTestType;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        private System.Windows.Forms.ToolStripButton tsbAutoupdate;

    }
}