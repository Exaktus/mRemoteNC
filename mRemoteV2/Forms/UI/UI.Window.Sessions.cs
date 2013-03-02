using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using AxWFICALib;
using Microsoft.VisualBasic;
using mRemoteNC;
using mRemoteNC.App;
using mRemoteNC.AppInfo;
using mRemoteNC.Connection;
using mRemoteNC.Tree;
using My;
using WeifenLuo.WinFormsUI.Docking;
using Settings = My.Settings;

//using mRemoteNC.Runtime;

namespace mRemoteNC
{
    namespace UI
    {
        namespace Window
        {
            public class Sessions : Base
            {
                #region Form Init

                internal System.Windows.Forms.ColumnHeader clmSesType;
                internal System.Windows.Forms.ColumnHeader clmSesUsername;
                internal System.Windows.Forms.ColumnHeader clmSesActivity;
                internal System.Windows.Forms.ContextMenuStrip cMenSession;
                private System.ComponentModel.Container components = null;
                internal System.Windows.Forms.ToolStripMenuItem cMenSessionRefresh;
                internal System.Windows.Forms.ToolStripMenuItem cMenSessionLogOff;
                internal System.Windows.Forms.ListView lvSessions;

                private void InitializeComponent()
                {
                    this.components = new System.ComponentModel.Container();
                    this.Load += new System.EventHandler(this.Sessions_Load);
                    System.ComponentModel.ComponentResourceManager resources =
                        new System.ComponentModel.ComponentResourceManager(typeof(Sessions));
                    this.lvSessions = new System.Windows.Forms.ListView();
                    this.clmSesUsername = new System.Windows.Forms.ColumnHeader();
                    this.clmSesActivity = new System.Windows.Forms.ColumnHeader();
                    this.clmSesType = new System.Windows.Forms.ColumnHeader();
                    this.cMenSession = new System.Windows.Forms.ContextMenuStrip(this.components);
                    this.cMenSessionRefresh = new System.Windows.Forms.ToolStripMenuItem();
                    this.cMenSessionRefresh.Click += new System.EventHandler(this.cMenSessionRefresh_Click);
                    this.cMenSessionLogOff = new System.Windows.Forms.ToolStripMenuItem();
                    this.cMenSessionLogOff.Click += new System.EventHandler(this.cMenSessionLogOff_Click);
                    this.cMenSession.SuspendLayout();
                    this.SuspendLayout();
                    //
                    //lvSessions
                    //
                    this.lvSessions.Anchor =
                        (System.Windows.Forms.AnchorStyles)
                        (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) |
                          System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
                    this.lvSessions.BorderStyle = System.Windows.Forms.BorderStyle.None;
                    this.lvSessions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.clmSesUsername, this.clmSesActivity, this.clmSesType });
                    this.lvSessions.ContextMenuStrip = this.cMenSession;
                    this.lvSessions.FullRowSelect = true;
                    this.lvSessions.GridLines = true;
                    this.lvSessions.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
                    this.lvSessions.Location = new System.Drawing.Point(0, -1);
                    this.lvSessions.MultiSelect = false;
                    this.lvSessions.Name = "lvSessions";
                    this.lvSessions.ShowGroups = false;
                    this.lvSessions.Size = new System.Drawing.Size(242, 174);
                    this.lvSessions.TabIndex = 0;
                    this.lvSessions.UseCompatibleStateImageBehavior = false;
                    this.lvSessions.View = System.Windows.Forms.View.Details;
                    //
                    //clmSesUsername
                    //
                    this.clmSesUsername.Text = Language.strColumnUsername;
                    this.clmSesUsername.Width = 80;
                    //
                    //clmSesActivity
                    //
                    this.clmSesActivity.Text = Language.strActivity;
                    //
                    //clmSesType
                    //
                    this.clmSesType.Text = Language.strType;
                    this.clmSesType.Width = 80;
                    //
                    //cMenSession
                    //
                    this.cMenSession.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.cMenSessionRefresh, this.cMenSessionLogOff });
                    this.cMenSession.Name = "cMenSession";
                    this.cMenSession.Size = new System.Drawing.Size(124, 48);
                    //
                    //cMenSessionRefresh
                    //
                    this.cMenSessionRefresh.Image = global::My.Resources.Resources.Refresh;
                    this.cMenSessionRefresh.Name = "cMenSessionRefresh";
                    this.cMenSessionRefresh.Size = new System.Drawing.Size(123, 22);
                    this.cMenSessionRefresh.Text = Language.strRefresh;
                    //
                    //cMenSessionLogOff
                    //
                    this.cMenSessionLogOff.Image = global::My.Resources.Resources.Session_LogOff;
                    this.cMenSessionLogOff.Name = "cMenSessionLogOff";
                    this.cMenSessionLogOff.Size = new System.Drawing.Size(123, 22);
                    this.cMenSessionLogOff.Text = Language.strLogOff;
                    //
                    //Sessions
                    //
                    this.ClientSize = new System.Drawing.Size(242, 173);
                    this.Controls.Add(this.lvSessions);
                    this.HideOnClose = true;
                    this.Icon = (System.Drawing.Icon)(resources.GetObject("$this.Icon"));
                    this.Name = "Sessions";
                    this.TabText = Language.strMenuSessions;
                    this.Text = Language.strMenuSessions;
                    this.cMenSession.ResumeLayout(false);
                    this.ResumeLayout(false);
                }

                #endregion Form Init

                #region Private Properties

                private string tServerName;
                private string tUserName;
                private string tPassword;
                private string tDomain;
                private long tSessionID;
                private bool tKillingSession;
                private System.Threading.Thread threadSessions;
                private long tServerHandle;

                #endregion Private Properties

                #region Public Properties

                private string _CurrentHost;

                public string CurrentHost
                {
                    get { return this._CurrentHost; }
                    set { this._CurrentHost = value; }
                }

                #endregion Public Properties

                #region Form Stuff

                private void Sessions_Load(object sender, System.EventArgs e)
                {
                    ApplyLanguage();
                }

                private void ApplyLanguage()
                {
                    clmSesUsername.Text = Language.strColumnUsername;
                    clmSesActivity.Text = Language.strActivity;
                    clmSesType.Text = Language.strType;
                    cMenSessionRefresh.Text = Language.strRefresh;
                    cMenSessionLogOff.Text = Language.strLogOff;
                    TabText = Language.strMenuSessions;
                    Text = Language.strMenuSessions;
                }

                #endregion Form Stuff

                #region Private Methods

                private void GetSessionsBG()
                {
                    try
                    {
                        RDP.TerminalSessions tS = new RDP.TerminalSessions();
                        Security.Impersonator sU = new Security.Impersonator();
                        RDP.Sessions tsSessions = new RDP.Sessions();

                        sU.StartImpersonation(tDomain, tUserName, tPassword);

                        try
                        {
                            //Trace.WriteLine("Opening connection to server: " & tServerName)
                            if (tS.OpenConnection(tServerName) == true)
                            {
                                tServerHandle = tS.ServerHandle;
                                //Trace.WriteLine("Trying to get sessions")
                                tsSessions = tS.GetSessions();
                            }
                        }
                        catch (Exception)
                        {
                        }

                        int i = 0;

                        //Trace.WriteLine("Sessions Count: " & tsSessions.Count)

                        if (tServerName == this._CurrentHost)
                        {
                            for (i = 0; i <= tsSessions.ItemsCount - 1; i++)
                            {
                                ListViewItem lItem = new ListViewItem();
                                lItem.Tag = tsSessions[i].SessionID;
                                lItem.Text = (string)(tsSessions[i].SessionUser);
                                lItem.SubItems.Add(tsSessions[i].SessionState);
                                lItem.SubItems.Add(Strings.Replace((string)(tsSessions[i].SessionName),
                                                                   Constants.vbNewLine, "", 1, -1, 0));

                                //Trace.WriteLine("Session " & i & ": " & tsSessions(i).SessionUser)

                                AddToList(lItem);
                            }
                        }

                        sU.StopImpersonation();
                        sU = null;
                        tS.CloseConnection(tServerHandle);
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            Language.strSessionGetFailed + Constants.vbNewLine +
                                                            ex.Message, true);
                    }
                }

                private void KillSessionBG()
                {
                    try
                    {
                        if (tUserName == "" || tPassword == "")
                        {
                            //Trace.WriteLine("No Logon Info")
                            return;
                        }

                        RDP.TerminalSessions ts = new RDP.TerminalSessions();
                        Security.Impersonator sU = new Security.Impersonator();

                        sU.StartImpersonation(tDomain, tUserName, tPassword);

                        try
                        {
                            if (ts.OpenConnection(tServerName) == true)
                            {
                                if (ts.KillSession(tSessionID) == true)
                                {
                                    sU.StopImpersonation();
                                    //Trace.WriteLine("Successfully killed session: " & tSessionID)
                                }
                                else
                                {
                                    sU.StopImpersonation();
                                    //Trace.WriteLine("Killing session " & tSessionID & " failed")
                                }
                            }
                        }
                        catch (Exception)
                        {
                            sU.StopImpersonation();
                        }

                        sU.StopImpersonation();

                        ClearList();

                        GetSessionsBG();
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            Language.strSessionKillFailed + Constants.vbNewLine +
                                                            ex.Message, true);
                    }
                }

                private delegate void AddToListCB(ListViewItem ListItem);

                private void AddToList(ListViewItem ListItem)
                {
                    if (this.lvSessions.InvokeRequired)
                    {
                        AddToListCB d = new AddToListCB(AddToList);
                        this.lvSessions.Invoke(d, new object[] { ListItem });
                    }
                    else
                    {
                        this.lvSessions.Items.Add(ListItem);
                    }
                }

                private delegate void ClearListCB();

                private void ClearList()
                {
                    if (this.lvSessions.InvokeRequired)
                    {
                        ClearListCB d = new ClearListCB(ClearList);
                        this.lvSessions.Invoke(d);
                    }
                    else
                    {
                        this.lvSessions.Items.Clear();
                    }
                }

                private void cMenSessionRefresh_Click(System.Object sender, System.EventArgs e)
                {
                    this.GetSessions();
                }

                private void cMenSessionLogOff_Click(System.Object sender, System.EventArgs e)
                {
                    this.KillSession();
                }

                #endregion Private Methods

                #region Public Methods

                public Sessions(DockContent Panel)
                {
                    this.WindowType = Type.Sessions;
                    this.DockPnl = Panel;
                    this.InitializeComponent();
                }

                public void GetSessionsAuto()
                {
                    try
                    {
                        string nowHost = "";

                        if (Node.SelectedNode.Tag is Info)
                        {
                            nowHost = (Node.SelectedNode.Tag as Info).Hostname;
                        }
                        else
                        {
                            this.ClearList();
                            return;
                        }

                        if (Settings.Default.AutomaticallyGetSessionInfo && this._CurrentHost == nowHost)
                        {
                            Info conI = (Info)Node.SelectedNode.Tag;

                            if (conI.Protocol == Protocols.RDP || conI.Protocol == Protocols.ICA)
                            {
                                //continue
                            }
                            else
                            {
                                this.ClearList();
                                return;
                            }

                            string sUser = (string)conI.Username;
                            string sPass = (string)conI.Password;
                            string sDom = (string)conI.Domain;

                            if (Settings.Default.EmptyCredentials == "custom")
                            {
                                if (sUser == "")
                                {
                                    sUser = (string)Settings.Default.DefaultUsername;
                                }

                                if (sPass == "")
                                {
                                    sPass = Security.Crypt.Decrypt((string)Settings.Default.DefaultPassword,
                                                                   (string)General.EncryptionKey);
                                }

                                if (sDom == "")
                                {
                                    sDom = (string)Settings.Default.DefaultDomain;
                                }
                            }

                            this.GetSessions((string)conI.Hostname, sUser, sPass, sDom);
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("GetSessionsAuto (UI.Window.Sessions) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                public void GetSessions()
                {
                    if (Node.SelectedNode == null)
                    {
                        return;
                    }

                    if (Node.SelectedNode.Tag is Info)
                    {
                        //continue
                    }
                    else
                    {
                        this.ClearList();
                        return;
                    }

                    Info conI = (Info)Node.SelectedNode.Tag;

                    if (conI.Protocol == Protocols.RDP || conI.Protocol == Protocols.ICA)
                    {
                        //continue
                    }
                    else
                    {
                        this.ClearList();
                        return;
                    }

                    this._CurrentHost = (string)conI.Hostname;

                    string sUser = (string)conI.Username;
                    string sPass = (string)conI.Password;
                    string sDom = (string)conI.Domain;

                    if (Settings.Default.EmptyCredentials == "custom")
                    {
                        if (sUser == "")
                        {
                            sUser = (string)Settings.Default.DefaultUsername;
                        }

                        if (sPass == "")
                        {
                            sPass = Security.Crypt.Decrypt((string)Settings.Default.DefaultPassword,
                                                           (string)General.EncryptionKey);
                        }

                        if (sDom == "")
                        {
                            sDom = (string)Settings.Default.DefaultDomain;
                        }
                    }

                    this.GetSessions((string)conI.Hostname, sUser, sPass, sDom);
                }

                public void GetSessions(string ServerName, string Username, string Password, string Domain)
                {
                    try
                    {
                        this.lvSessions.Items.Clear();

                        tServerName = ServerName;
                        tUserName = Username;
                        tPassword = Password;
                        tDomain = Domain;

                        threadSessions = new System.Threading.Thread(new System.Threading.ThreadStart(GetSessionsBG));
                        threadSessions.SetApartmentState(System.Threading.ApartmentState.STA);
                        threadSessions.IsBackground = true;
                        threadSessions.Start();
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("GetSessions (UI.Window.Sessions) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                public void KillSession()
                {
                    if (Node.SelectedNode.Tag is Info)
                    {
                        //continue
                    }
                    else
                    {
                        return;
                    }

                    Info conI = (Info)Node.SelectedNode.Tag;

                    if (conI.Protocol == Protocols.RDP)
                    {
                        //continue
                    }
                    else
                    {
                        return;
                    }

                    if (this.lvSessions.SelectedItems.Count > 0)
                    {
                        //continue
                    }
                    else
                    {
                        return;
                    }

                    foreach (ListViewItem lvItem in this.lvSessions.SelectedItems)
                    {
                        this.KillSession((string)conI.Hostname, (string)conI.Username, (string)conI.Password,
                                         (string)conI.Domain, (string)lvItem.Tag);
                    }
                }

                public void KillSession(string ServerName, string Username, string Password, string Domain,
                                        string SessionID)
                {
                    try
                    {
                        tServerName = ServerName;
                        tUserName = Username;
                        tPassword = Password;
                        tDomain = Domain;
                        tSessionID = Convert.ToInt64(SessionID);

                        threadSessions = new System.Threading.Thread(new System.Threading.ThreadStart(KillSessionBG));
                        threadSessions.SetApartmentState(System.Threading.ApartmentState.STA);
                        threadSessions.IsBackground = true;
                        threadSessions.Start();
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("KillSession (UI.Window.Sessions) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                #endregion Public Methods
            }
        }
    }
}