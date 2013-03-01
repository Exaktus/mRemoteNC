using EXControls;

namespace mRemoteNC.Forms.Importer
{
    partial class ImportForm
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.lvFoundConections = new EXControls.EXListView();
            this.chEmpty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chToImport = ((EXControls.EXBoolColumnHeader)(new EXControls.EXBoolColumnHeader()));
            this.chIcon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chImported = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chProtocol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chHost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chUsername = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Location = new System.Drawing.Point(423, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(94, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search again";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.Location = new System.Drawing.Point(423, 41);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(94, 64);
            this.btnImport.TabIndex = 2;
            this.btnImport.Text = "Import selected";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // lvFoundConections
            // 
            this.lvFoundConections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvFoundConections.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvFoundConections.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chEmpty,
            this.chToImport,
            this.chIcon,
            this.chImported,
            this.chProtocol,
            this.chHost,
            this.chPort,
            this.chUsername});
            this.lvFoundConections.ControlPadding = 4;
            this.lvFoundConections.FullRowSelect = true;
            this.lvFoundConections.GridLines = true;
            this.lvFoundConections.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvFoundConections.Location = new System.Drawing.Point(12, 111);
            this.lvFoundConections.Name = "lvFoundConections";
            this.lvFoundConections.OwnerDraw = true;
            this.lvFoundConections.Size = new System.Drawing.Size(505, 176);
            this.lvFoundConections.TabIndex = 0;
            this.lvFoundConections.UseCompatibleStateImageBehavior = false;
            this.lvFoundConections.View = System.Windows.Forms.View.Details;
            // 
            // chEmpty
            // 
            this.chEmpty.Text = "";
            this.chEmpty.Width = 0;
            // 
            // chToImport
            // 
            this.chToImport.Editable = true;
            this.chToImport.FalseImage = global::My.Resources.Resources.Delete;
            this.chToImport.Text = "";
            this.chToImport.TrueImage = global::My.Resources.Resources.Expand;
            this.chToImport.Width = 27;
            // 
            // chIcon
            // 
            this.chIcon.Text = " ";
            this.chIcon.Width = 27;
            // 
            // chImported
            // 
            this.chImported.Text = "Imported from";
            this.chImported.Width = 111;
            // 
            // chProtocol
            // 
            this.chProtocol.Text = "Protocol";
            this.chProtocol.Width = 87;
            // 
            // chHost
            // 
            this.chHost.Text = "Host";
            this.chHost.Width = 85;
            // 
            // chPort
            // 
            this.chPort.Text = "Port";
            // 
            // chUsername
            // 
            this.chUsername.Text = "User";
            this.chUsername.Width = 86;
            // 
            // ImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 299);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lvFoundConections);
            this.Name = "ImportForm";
            this.Text = "Import";
            this.Load += new System.EventHandler(this.Import_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private EXControls.EXListView lvFoundConections;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.ColumnHeader chIcon;
        private System.Windows.Forms.ColumnHeader chProtocol;
        private System.Windows.Forms.ColumnHeader chHost;
        private System.Windows.Forms.ColumnHeader chUsername;
        private System.Windows.Forms.ColumnHeader chImported;
        private System.Windows.Forms.ColumnHeader chPort;
        private EXBoolColumnHeader chToImport;
        private System.Windows.Forms.ColumnHeader chEmpty;
    }
}