using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;

//using mRemoteNC.Runtime;
using System.Threading;
using System.Windows.Forms;
using AxMSTSCLib;
using AxWFICALib;
using Microsoft.VisualBasic;
using mRemoteNC.App;
using My;
using WeifenLuo.WinFormsUI.Docking;

namespace mRemoteNC
{
    namespace UI
    {
        namespace Window
        {
            public class Announcement : Base
            {
                #region Form Init

                internal System.Windows.Forms.WebBrowser wBrowser;

                private void InitializeComponent()
                {
                    this.wBrowser = new System.Windows.Forms.WebBrowser();
                    this.Load += new System.EventHandler(this.Announcement_Load);
                    this.SuspendLayout();
                    //
                    //wBrowser
                    //
                    this.wBrowser.AllowWebBrowserDrop = false;
                    this.wBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.wBrowser.Location = new System.Drawing.Point(0, 0);
                    this.wBrowser.MinimumSize = new System.Drawing.Size(20, 20);
                    this.wBrowser.Name = "wBrowser";
                    this.wBrowser.Size = new System.Drawing.Size(549, 474);
                    this.wBrowser.TabIndex = 0;
                    //
                    //Announcement
                    //
                    this.ClientSize = new System.Drawing.Size(549, 474);
                    this.Controls.Add(this.wBrowser);
                    this.Name = "Announcement";
                    this.TabText = "Announcement";
                    this.Text = "Announcement";
                    this.Icon = global::My.Resources.Resources.News_Icon;
                    this.ResumeLayout(false);
                }

                #endregion Form Init

                #region Public Methods

                public Announcement(DockContent Panel)
                {
                    this.WindowType = Type.Announcement;
                    this.DockPnl = Panel;
                    this.InitializeComponent();
                }

                #endregion Public Methods

                #region Private Methods

                private void Announcement_Load(object sender, System.EventArgs e)
                {
                    ApplyLanguage();

                    this.CheckForAnnouncement();
                }

                private void ApplyLanguage()
                {
                }

                #endregion Private Methods

                private mRemoteNC.Announcement aN=new mRemoteNC.Announcement();
                private Thread uT;

                public delegate void AnnouncementCheckCompletedEventHandler(bool AnnouncementAvailable);

                private AnnouncementCheckCompletedEventHandler AnnouncementCheckCompletedEvent;

                public event AnnouncementCheckCompletedEventHandler AnnouncementCheckCompleted
                {
                    add
                    {
                        AnnouncementCheckCompletedEvent =
                            (AnnouncementCheckCompletedEventHandler)
                            System.Delegate.Combine(AnnouncementCheckCompletedEvent, value);
                    }
                    remove
                    {
                        AnnouncementCheckCompletedEvent =
                            (AnnouncementCheckCompletedEventHandler)
                            System.Delegate.Remove(AnnouncementCheckCompletedEvent, value);
                    }
                }

                private bool IsAnnouncementCheckHandlerDeclared = false;

                public void CheckForAnnouncement()
                {
                    try
                    {
                        uT = new Thread(new System.Threading.ThreadStart(CheckForAnnouncementBG));
                        uT.SetApartmentState(ApartmentState.STA);
                        uT.IsBackground = true;

                        if (this.IsAnnouncementCheckHandlerDeclared == false)
                        {
                            AnnouncementCheckCompleted +=
                                new Announcement.AnnouncementCheckCompletedEventHandler(AnnouncementCheckComplete);
                            this.IsAnnouncementCheckHandlerDeclared = true;
                        }

                        uT.Start();
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("CheckForAnnouncement (UI.Window.Announcement) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void CheckForAnnouncementBG()
                {
                    try
                    {
                        //aN = new App.App.Runtime.Announcement();

                        if (Runtime.IsAnnouncementAvailable == true)
                        {
                            if (AnnouncementCheckCompletedEvent != null)
                                AnnouncementCheckCompletedEvent(true);
                        }
                        else
                        {
                            if (AnnouncementCheckCompletedEvent != null)
                                AnnouncementCheckCompletedEvent(false);
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("CheckForAnnouncementBG (UI.Window.Announcement) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void AnnouncementCheckComplete(bool AnnouncementAvailable)
                {
                    try
                    {
                        wBrowser.Navigate(aN.CurrentAnnouncementInfo.Url);
                        Settings.Default.LastAnnouncement = aN.CurrentAnnouncementInfo.Name;
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("AnnouncementCheckComplete (UI.Window.Announcement) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }
            }
        }
    }
}