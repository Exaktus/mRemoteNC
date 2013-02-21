using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using mRemoteNC.App;
using My;
using WeifenLuo.WinFormsUI.Docking;
using My.Resources;

namespace mRemoteNC
{
    namespace UI
    {
        namespace Window
        {
            public class Update : Base
            {
                #region Form Init

                internal System.Windows.Forms.Label lblStatus;
                internal System.Windows.Forms.TextBox txtChangeLog;
                internal System.Windows.Forms.ProgressBar prgbDownload;
                internal System.Windows.Forms.Button btnDownload;
                internal System.Windows.Forms.Label lblChangeLogLabel;
                internal System.Windows.Forms.Panel pnlUp;
                internal System.Windows.Forms.Label lblCurrentVersionLabel;
                internal System.Windows.Forms.Label lblInstalledVersionLabel;
                internal System.Windows.Forms.Label lblAvailableVersion;
                internal System.Windows.Forms.Label lblCurrentVersion;
                internal System.Windows.Forms.PictureBox pbUpdateImage;
                internal System.Windows.Forms.Button btnCheckForUpdate;

                private void InitializeComponent()
                {
                    this.btnCheckForUpdate = new System.Windows.Forms.Button();
                    this.Load += new System.EventHandler(this.Update_Load);
                    this.btnCheckForUpdate.Click += new System.EventHandler(this.btnCheckForUpdate_Click);
                    this.pnlUp = new System.Windows.Forms.Panel();
                    this.lblChangeLogLabel = new System.Windows.Forms.Label();
                    this.btnDownload = new System.Windows.Forms.Button();
                    this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
                    this.prgbDownload = new System.Windows.Forms.ProgressBar();
                    this.txtChangeLog = new System.Windows.Forms.TextBox();
                    this.lblStatus = new System.Windows.Forms.Label();
                    this.lblCurrentVersionLabel = new System.Windows.Forms.Label();
                    this.lblInstalledVersionLabel = new System.Windows.Forms.Label();
                    this.lblAvailableVersion = new System.Windows.Forms.Label();
                    this.lblCurrentVersion = new System.Windows.Forms.Label();
                    this.pbUpdateImage = new System.Windows.Forms.PictureBox();
                    this.pnlUp.SuspendLayout();
                    ((System.ComponentModel.ISupportInitialize)this.pbUpdateImage).BeginInit();
                    this.SuspendLayout();
                    //
                    //btnCheckForUpdate
                    //
                    this.btnCheckForUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    this.btnCheckForUpdate.Location = new System.Drawing.Point(16, 104);
                    this.btnCheckForUpdate.Name = "btnCheckForUpdate";
                    this.btnCheckForUpdate.Size = new System.Drawing.Size(104, 32);
                    this.btnCheckForUpdate.TabIndex = 5;
                    this.btnCheckForUpdate.Text = "Check Again";
                    this.btnCheckForUpdate.UseVisualStyleBackColor = true;
                    //
                    //pnlUp
                    //
                    this.pnlUp.Anchor =
                        (System.Windows.Forms.AnchorStyles)
                        (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) |
                          System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
                    this.pnlUp.Controls.Add(this.lblChangeLogLabel);
                    this.pnlUp.Controls.Add(this.btnDownload);
                    this.pnlUp.Controls.Add(this.prgbDownload);
                    this.pnlUp.Controls.Add(this.txtChangeLog);
                    this.pnlUp.Location = new System.Drawing.Point(16, 152);
                    this.pnlUp.Name = "pnlUp";
                    this.pnlUp.Size = new System.Drawing.Size(718, 248);
                    this.pnlUp.TabIndex = 6;
                    this.pnlUp.Visible = false;
                    //
                    //lblChangeLogLabel
                    //
                    this.lblChangeLogLabel.AutoSize = true;
                    this.lblChangeLogLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", (float)(8.25F),
                                                                          System.Drawing.FontStyle.Bold,
                                                                          System.Drawing.GraphicsUnit.Point, (byte)(0));
                    this.lblChangeLogLabel.Location = new System.Drawing.Point(0, 0);
                    this.lblChangeLogLabel.Name = "lblChangeLogLabel";
                    this.lblChangeLogLabel.Size = new System.Drawing.Size(79, 13);
                    this.lblChangeLogLabel.TabIndex = 0;
                    this.lblChangeLogLabel.Text = "Change Log:";
                    //
                    //btnDownload
                    //
                    this.btnDownload.Anchor =
                        (System.Windows.Forms.AnchorStyles)
                        (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
                    this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    this.btnDownload.Location = new System.Drawing.Point(0, 216);
                    this.btnDownload.Name = "btnDownload";
                    this.btnDownload.Size = new System.Drawing.Size(144, 32);
                    this.btnDownload.TabIndex = 2;
                    this.btnDownload.Text = "Download and Install";
                    this.btnDownload.UseVisualStyleBackColor = true;
                    //
                    //prgbDownload
                    //
                    this.prgbDownload.Anchor =
                        (System.Windows.Forms.AnchorStyles)
                        ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) |
                         System.Windows.Forms.AnchorStyles.Right);
                    this.prgbDownload.Location = new System.Drawing.Point(160, 224);
                    this.prgbDownload.Name = "prgbDownload";
                    this.prgbDownload.Size = new System.Drawing.Size(542, 23);
                    this.prgbDownload.TabIndex = 3;
                    //
                    //txtChangeLog
                    //
                    this.txtChangeLog.Anchor =
                        (System.Windows.Forms.AnchorStyles)
                        (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) |
                          System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
                    this.txtChangeLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
                    this.txtChangeLog.Cursor = System.Windows.Forms.Cursors.Arrow;
                    this.txtChangeLog.Location = new System.Drawing.Point(16, 24);
                    this.txtChangeLog.Multiline = true;
                    this.txtChangeLog.Name = "txtChangeLog";
                    this.txtChangeLog.ReadOnly = true;
                    this.txtChangeLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
                    this.txtChangeLog.Size = new System.Drawing.Size(699, 181);
                    this.txtChangeLog.TabIndex = 1;
                    this.txtChangeLog.TabStop = false;
                    //
                    //lblStatus
                    //
                    this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", (float)(12.0F),
                                                                  System.Drawing.FontStyle.Bold,
                                                                  System.Drawing.GraphicsUnit.Point, (byte)(0));
                    this.lblStatus.ForeColor = System.Drawing.SystemColors.ControlText;
                    this.lblStatus.Location = new System.Drawing.Point(12, 16);
                    this.lblStatus.Name = "lblStatus";
                    this.lblStatus.Size = new System.Drawing.Size(660, 23);
                    this.lblStatus.TabIndex = 0;
                    this.lblStatus.Text = "Status";
                    this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                    //
                    //lblCurrentVersionLabel
                    //
                    this.lblCurrentVersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", (float)(8.25F),
                                                                               System.Drawing.FontStyle.Bold,
                                                                               System.Drawing.GraphicsUnit.Point,
                                                                               (byte)(0));
                    this.lblCurrentVersionLabel.Location = new System.Drawing.Point(16, 72);
                    this.lblCurrentVersionLabel.Name = "lblCurrentVersionLabel";
                    this.lblCurrentVersionLabel.Size = new System.Drawing.Size(120, 16);
                    this.lblCurrentVersionLabel.TabIndex = 3;
                    this.lblCurrentVersionLabel.Text = "Current version:";
                    this.lblCurrentVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                    //
                    //lblInstalledVersionLabel
                    //
                    this.lblInstalledVersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", (float)(8.25F),
                                                                                 System.Drawing.FontStyle.Bold,
                                                                                 System.Drawing.GraphicsUnit.Point,
                                                                                 (byte)(0));
                    this.lblInstalledVersionLabel.Location = new System.Drawing.Point(16, 48);
                    this.lblInstalledVersionLabel.Name = "lblInstalledVersionLabel";
                    this.lblInstalledVersionLabel.Size = new System.Drawing.Size(120, 16);
                    this.lblInstalledVersionLabel.TabIndex = 1;
                    this.lblInstalledVersionLabel.Text = "Installed version:";
                    this.lblInstalledVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                    //
                    //lblAvailableVersion
                    //
                    this.lblAvailableVersion.Location = new System.Drawing.Point(136, 72);
                    this.lblAvailableVersion.Name = "lblAvailableVersion";
                    this.lblAvailableVersion.Size = new System.Drawing.Size(104, 16);
                    this.lblAvailableVersion.TabIndex = 4;
                    this.lblAvailableVersion.Text = "Version";
                    this.lblAvailableVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                    //
                    //lblCurrentVersion
                    //
                    this.lblCurrentVersion.Location = new System.Drawing.Point(136, 48);
                    this.lblCurrentVersion.Name = "lblCurrentVersion";
                    this.lblCurrentVersion.Size = new System.Drawing.Size(104, 16);
                    this.lblCurrentVersion.TabIndex = 2;
                    this.lblCurrentVersion.Text = "Version";
                    this.lblCurrentVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                    //
                    //pbUpdateImage
                    //
                    this.pbUpdateImage.Anchor =
                        (System.Windows.Forms.AnchorStyles)
                        (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
                    this.pbUpdateImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    this.pbUpdateImage.Cursor = System.Windows.Forms.Cursors.Hand;
                    this.pbUpdateImage.Location = new System.Drawing.Point(246, 48);
                    this.pbUpdateImage.Name = "pbUpdateImage";
                    this.pbUpdateImage.Size = new System.Drawing.Size(468, 60);
                    this.pbUpdateImage.TabIndex = 45;
                    this.pbUpdateImage.TabStop = false;
                    this.pbUpdateImage.Visible = false;
                    //
                    //Update
                    //
                    this.ClientSize = new System.Drawing.Size(734, 418);
                    this.Controls.Add(this.pbUpdateImage);
                    this.Controls.Add(this.lblCurrentVersionLabel);
                    this.Controls.Add(this.lblInstalledVersionLabel);
                    this.Controls.Add(this.lblAvailableVersion);
                    this.Controls.Add(this.btnCheckForUpdate);
                    this.Controls.Add(this.lblCurrentVersion);
                    this.Controls.Add(this.pnlUp);
                    this.Controls.Add(this.lblStatus);
                    this.Icon = global::My.Resources.Resources.Update_Icon;
                    this.Name = "Update";
                    this.TabText = "Update";
                    this.Text = "Update";
                    this.pnlUp.ResumeLayout(false);
                    this.pnlUp.PerformLayout();
                    ((System.ComponentModel.ISupportInitialize)this.pbUpdateImage).EndInit();
                    this.ResumeLayout(false);
                }

                #endregion Form Init

                #region Private Properties

                private UpdateManager uD;

                private bool IsUpdateCheckHandlerDeclared = false;
                private bool IsUpdateDownloadHandlerDeclared = false;

                #endregion Private Properties

                #region Public Events

                public delegate void UpdateCheckCompletedEventHandler(bool UpdateAvailable);

                private UpdateCheckCompletedEventHandler UpdateCheckCompletedEvent;

                public event UpdateCheckCompletedEventHandler UpdateCheckCompleted
                {
                    add
                    {
                        UpdateCheckCompletedEvent =
                            (UpdateCheckCompletedEventHandler)System.Delegate.Combine(UpdateCheckCompletedEvent, value);
                    }
                    remove
                    {
                        UpdateCheckCompletedEvent =
                            (UpdateCheckCompletedEventHandler)System.Delegate.Remove(UpdateCheckCompletedEvent, value);
                    }
                }

                #endregion Public Events

                #region Public Methods

                public Update(DockContent Panel)
                {
                    this.WindowType = Type.Update;
                    this.DockPnl = Panel;
                    this.InitializeComponent();
                    Runtime.FontOverride(this);
                }

                void SetUpdateButtonState(bool active)
                {
                    try
                    {
                        if (btnCheckForUpdate.InvokeRequired)
                        {
                            btnCheckForUpdate.Invoke(
                                new MethodInvoker(() =>
                                {
                                    btnCheckForUpdate.Enabled = active;
                                }));
                        }
                        else
                        {
                            btnCheckForUpdate.Enabled = active;
                        }
                    }
                    catch (Exception ex)
                    {
                         Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            ex.Message, true);
                    }
                }

                public void CheckForUpdate()
                {
                    try
                    {
                        SetCurrentVersionText(Application.ProductVersion);
                        SetUpdateButtonState(false);
                        if (IsUpdateCheckHandlerDeclared == false)
                        {
                            UpdateCheckCompleted += UpdateCheckComplete;
                            IsUpdateCheckHandlerDeclared = true;
                        }
                        ThreadPool.QueueUserWorkItem(state =>
                                                         {
                                                             try
                                                             {
                                                                 uD = new App.UpdateManager();
                                                                 if (UpdateCheckCompletedEvent != null)
                                                                     UpdateCheckCompletedEvent(uD.IsUpdateAvailable());
                                                             }
                                                             catch (Exception ex)
                                                             {
                                                                 Runtime.MessageCollector.AddMessage(
                                                                     Messages.MessageClass.ErrorMsg,
                                                                     Language.strUpdateCheckFailed + Constants.vbNewLine +
                                                                     ex.Message, true);
                                                             }
                                                             finally
                                                             {
                                                                 SetUpdateButtonState(true);
                                                             }
                                                         });
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            Language.strUpdateCheckFailed + Constants.vbNewLine +
                                                            ex.Message, true);
                    }
                }

                #endregion Public Methods

                #region Private Methods



                private void UpdateCheckComplete(bool UpdateAvailable)
                {
                    try
                    {
                        Settings.Default.CheckForUpdatesLastCheck = DateTime.Now;

                        if (UpdateAvailable)
                        {
                            Settings.Default.UpdatePending = true;

                            SetStatus(Color.OrangeRed, Language.strUpdateAvailable);
                            SetVisible(pnlUp, true);

                            var uI = uD.GetUpdateInfo();
                            SetAvailableVersionText(uI.Version.ToString());
                            SetChangeLogText(uI.ChangeLog);

                            if (!string.IsNullOrWhiteSpace(uI.ImageURL))
                            {
                                SetImageURL(uI.ImageURL);

                                if (!string.IsNullOrWhiteSpace(uI.ImageURLLink))
                                {
                                    pbUpdateImage.Tag = uI.ImageURLLink;
                                }

                                SetVisible(pbUpdateImage, true);
                            }
                            else
                            {
                                SetVisible(pbUpdateImage, false);
                            }

                            FocusDownloadButton();
                        }
                        else
                        {
                            Settings.Default.UpdatePending = false;

                            SetStatus(Color.ForestGreen, Language.strNoUpdateAvailable);
                            SetVisible(pnlUp, false);

                            var uI = uD.GetUpdateInfo();
                            SetAvailableVersionText(uI.Version.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            Language.strUpdateCheckCompleteFailed + Constants.vbNewLine +
                                                            ex.Message, true);
                    }
                }

                #endregion Private Methods

                #region Threading Callbacks

                private delegate void SetImageURLCB(string img);

                private void SetImageURL(string img)
                {
                    if (this.pbUpdateImage.InvokeRequired == true)
                    {
                        SetImageURLCB d = new SetImageURLCB(SetImageURL);
                        this.pbUpdateImage.Invoke(d, new object[] { img });
                    }
                    else
                    {
                        this.pbUpdateImage.ImageLocation = img;
                    }
                }

                private delegate void SetStatusCB(Color Color, string Text);

                private void SetStatus(Color Color, string Text)
                {
                    if (this.lblStatus.InvokeRequired == true)
                    {
                        SetStatusCB d = new SetStatusCB(SetStatus);
                        this.lblStatus.Invoke(d, new object[] { Color, Text });
                    }
                    else
                    {
                        this.lblStatus.ForeColor = Color;
                        this.lblStatus.Text = Text;
                    }
                }

                private delegate void SetVisibleCB(Control ctrl, bool visible);

                private void SetVisible(Control ctrl, bool visible)
                {
                    if (ctrl.InvokeRequired)
                    {
                        SetVisibleCB d = new SetVisibleCB(SetVisible);
                        ctrl.Invoke(d, new object[] { ctrl, visible });
                    }
                    else
                    {
                        ctrl.Visible = visible;
                    }
                }

                private delegate void SetCurrentVersionTextCB(string Text);

                private void SetCurrentVersionText(string Text)
                {
                    if (this.lblCurrentVersion.InvokeRequired == true)
                    {
                        SetCurrentVersionTextCB d = new SetCurrentVersionTextCB(SetCurrentVersionText);
                        this.lblCurrentVersion.Invoke(d, new object[] { Text });
                    }
                    else
                    {
                        this.lblCurrentVersion.Text = Text;
                    }
                }

                private delegate void SetAvailableVersionTextCB(string Text);

                private void SetAvailableVersionText(string Text)
                {
                    if (this.lblAvailableVersion.InvokeRequired == true)
                    {
                        SetAvailableVersionTextCB d = new SetAvailableVersionTextCB(SetAvailableVersionText);
                        this.lblAvailableVersion.Invoke(d, new object[] { Text });
                    }
                    else
                    {
                        this.lblAvailableVersion.Text = Text;
                    }
                }

                private delegate void SetChangeLogTextCB(string Text);

                private void SetChangeLogText(string Text)
                {
                    if (this.txtChangeLog.InvokeRequired == true)
                    {
                        SetChangeLogTextCB d = new SetChangeLogTextCB(SetChangeLogText);
                        this.txtChangeLog.Invoke(d, new object[] { Text });
                    }
                    else
                    {
                        this.txtChangeLog.Text = Text;
                    }
                }

                private delegate void FocusDownloadButtonCB();

                private void FocusDownloadButton()
                {
                    if (this.btnDownload.InvokeRequired == true)
                    {
                        FocusDownloadButtonCB d = new FocusDownloadButtonCB(FocusDownloadButton);
                        this.btnDownload.Invoke(d);
                    }
                    else
                    {
                        this.btnDownload.Focus();
                    }
                }

                #endregion Threading Callbacks

                #region Form Stuff

                private void Update_Load(object sender, System.EventArgs e)
                {
                    ApplyLanguage();
                    CheckForUpdate();
                }

                private void ApplyLanguage()
                {
                    btnCheckForUpdate.Text = Language.strCheckForUpdate;
                    lblChangeLogLabel.Text = Language.strLabelChangeLog;
                    btnDownload.Text = Language.strDownloadAndInstall;
                    lblCurrentVersionLabel.Text = Language.strAvailableVersion + ":";
                    lblInstalledVersionLabel.Text = Language.strCurrentVersion + ":";
                    lblAvailableVersion.Text = Language.strVersion;
                    lblCurrentVersion.Text = Language.strVersion;
                    TabText = Language.strMenuCheckForUpdates;
                    Text = Language.strMenuCheckForUpdates;
                }

                private void btnCheckForUpdate_Click(System.Object sender, System.EventArgs e)
                {
                    CheckForUpdate();
                }

                private void btnDownload_Click(System.Object sender, System.EventArgs e)
                {
                    DownloadUpdate();
                }

                private void DownloadUpdate()
                {
                    try
                    {
                        if (uD.DownloadUpdate(uD.curUI.DownloadUrl))
                        {
                            this.btnDownload.Enabled = false;

                            if (this.IsUpdateDownloadHandlerDeclared == false)
                            {
                                uD.DownloadProgressChanged += DLProgressChanged;
                                uD.DownloadCompleted += DLCompleted;
                                this.IsUpdateDownloadHandlerDeclared = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            Language.strUpdateDownloadFailed + Constants.vbNewLine +
                                                            ex.Message, true);
                    }
                }

                #endregion Form Stuff

                #region Events

                private void DLProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
                {
                    this.prgbDownload.Value = e.ProgressPercentage;
                }

                private void DLCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e, bool Success)
                {
                    try
                    {
                        this.btnDownload.Enabled = true;

                        if (Success == true)
                        {
                            if (
                                MessageBox.Show(Language.strUpdateDownloadComplete, Language.strMenuCheckForUpdates,
                                                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) ==
                                System.Windows.Forms.DialogResult.OK)
                            {
                                try
                                {
                                    
                                    if (AppInfo.General.IsPortable)
                                    {
                                        var tempdir = Path.Combine(Path.GetTempPath(),Path.GetRandomFileName());
                                        if (!tempdir.EndsWith("\\"))
                                        {
                                            tempdir += "\\";
                                        }
                                        if (!Directory.Exists(tempdir))
                                        {
                                            Directory.CreateDirectory(tempdir);
                                        }
                                        Tools.Misc.UnZipFileVisual(uD.curUI.UpdateLocation, tempdir);
                                        var batName = Path.Combine(Application.StartupPath, "update.bat");

                                        if (File.Exists(batName))
                                        {
                                            File.Delete(batName);
                                        }

                                        File.WriteAllText(batName, string.Format(Resources.UpdateBatFile, tempdir));
                                        
                                        new Process
                                                    {
                                                        StartInfo =
                                                            {
                                                                Arguments = "/C \"" + batName + "\"",
                                                                FileName = "cmd.exe",
                                                                UseShellExecute = false,
                                                                CreateNoWindow = true
                                                            }
                                                    }.Start();
                                        Runtime.Shutdown.BeforeQuit();
                                    }
                                    else
                                    {
                                        Process.Start(uD.curUI.UpdateLocation);
                                        Runtime.Shutdown.BeforeQuit();
                                    }
                                    
                                }
                                catch (Exception ex)
                                {
                                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                                        Language.strUpdateStartFailed +
                                                                        Constants.vbNewLine + ex.Message);
                                }

                                ProjectData.EndApp();
                            }
                            else
                            {
                                try
                                {
                                    File.Delete(uD.curUI.UpdateLocation);
                                }
                                catch (Exception ex)
                                {
                                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                                        Language.strUpdateDeleteFailed +
                                                                        Constants.vbNewLine + ex.Message);
                                }
                            }
                        }
                        else
                        {
                            Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                                Language.strUpdateDownloadFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            Language.strUpdateDownloadCompleteFailed +
                                                            Constants.vbNewLine + ex.Message, true);
                    }
                }

                #endregion Events

                private void pbUpdateImage_Click(System.Object sender, System.EventArgs e)
                {
                    if (pbUpdateImage.Tag != null)
                    {
                        Process.Start((string)pbUpdateImage.Tag);
                    }
                }
            }
        }
    }
}