namespace mRemoteNC.Forms
{
    partial class QuickTextEdit
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuickTextEdit));
            this.grpEditor = new System.Windows.Forms.GroupBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.lvApps = new System.Windows.Forms.ListView();
            this.clmDisplayName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cMenText = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cMenAppsAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenAppsRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.grpEditor.SuspendLayout();
            this.cMenText.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpEditor
            // 
            this.grpEditor.Controls.Add(this.btnBrowse);
            this.grpEditor.Controls.Add(this.txtFilename);
            this.grpEditor.Controls.Add(this.txtDisplayName);
            this.grpEditor.Controls.Add(this.Label2);
            this.grpEditor.Controls.Add(this.Label1);
            this.grpEditor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpEditor.Enabled = false;
            this.grpEditor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpEditor.Location = new System.Drawing.Point(0, 453);
            this.grpEditor.Name = "grpEditor";
            this.grpEditor.Size = new System.Drawing.Size(677, 81);
            this.grpEditor.TabIndex = 12;
            this.grpEditor.TabStop = false;
            this.grpEditor.Text = "Application Editor";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Location = new System.Drawing.Point(570, 48);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(95, 23);
            this.btnBrowse.TabIndex = 41;
            this.btnBrowse.Text = "Iinsert Variable";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtFilename
            // 
            this.txtFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilename.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilename.Location = new System.Drawing.Point(106, 48);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(458, 20);
            this.txtFilename.TabIndex = 30;
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDisplayName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDisplayName.Location = new System.Drawing.Point(106, 22);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(561, 20);
            this.txtDisplayName.TabIndex = 10;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(17, 50);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(31, 13);
            this.Label2.TabIndex = 20;
            this.Label2.Text = "Text:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(17, 25);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(75, 13);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Display Name:";
            // 
            // lvApps
            // 
            this.lvApps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvApps.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvApps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmDisplayName,
            this.clmText});
            this.lvApps.ContextMenuStrip = this.cMenText;
            this.lvApps.FullRowSelect = true;
            this.lvApps.GridLines = true;
            this.lvApps.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvApps.HideSelection = false;
            this.lvApps.Location = new System.Drawing.Point(0, 1);
            this.lvApps.Name = "lvApps";
            this.lvApps.Size = new System.Drawing.Size(677, 446);
            this.lvApps.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvApps.TabIndex = 11;
            this.lvApps.UseCompatibleStateImageBehavior = false;
            this.lvApps.View = System.Windows.Forms.View.Details;
            this.lvApps.SelectedIndexChanged += new System.EventHandler(this.lvApps_SelectedIndexChanged);
            // 
            // clmDisplayName
            // 
            this.clmDisplayName.Text = "Display Name";
            this.clmDisplayName.Width = 130;
            // 
            // clmText
            // 
            this.clmText.Text = "Text";
            this.clmText.Width = 308;
            // 
            // cMenText
            // 
            this.cMenText.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cMenAppsAdd,
            this.cMenAppsRemove});
            this.cMenText.Name = "cMenApps";
            this.cMenText.Size = new System.Drawing.Size(148, 48);
            // 
            // cMenAppsAdd
            // 
            this.cMenAppsAdd.Image = global::My.Resources.Resources.ExtApp_Add;
            this.cMenAppsAdd.Name = "cMenAppsAdd";
            this.cMenAppsAdd.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F4)));
            this.cMenAppsAdd.Size = new System.Drawing.Size(147, 22);
            this.cMenAppsAdd.Text = "Add";
            this.cMenAppsAdd.Click += new System.EventHandler(this.cMenAppsAdd_Click);
            // 
            // cMenAppsRemove
            // 
            this.cMenAppsRemove.Image = global::My.Resources.Resources.ExtApp_Delete;
            this.cMenAppsRemove.Name = "cMenAppsRemove";
            this.cMenAppsRemove.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.cMenAppsRemove.Size = new System.Drawing.Size(147, 22);
            this.cMenAppsRemove.Text = "Remove";
            this.cMenAppsRemove.Click += new System.EventHandler(this.cMenAppsRemove_Click);
            // 
            // QuickTextEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 534);
            this.Controls.Add(this.grpEditor);
            this.Controls.Add(this.lvApps);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "QuickTextEdit";
            this.Text = "QuickTextEdit";
            this.Load += new System.EventHandler(this.QuickTextEdit_Load);
            this.grpEditor.ResumeLayout(false);
            this.grpEditor.PerformLayout();
            this.cMenText.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox grpEditor;
        internal System.Windows.Forms.TextBox txtFilename;
        internal System.Windows.Forms.TextBox txtDisplayName;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.ListView lvApps;
        internal System.Windows.Forms.ColumnHeader clmDisplayName;
        private System.Windows.Forms.ColumnHeader clmText;
        internal System.Windows.Forms.Button btnBrowse;
        internal System.Windows.Forms.ContextMenuStrip cMenText;
        internal System.Windows.Forms.ToolStripMenuItem cMenAppsAdd;
        internal System.Windows.Forms.ToolStripMenuItem cMenAppsRemove;
    }
}