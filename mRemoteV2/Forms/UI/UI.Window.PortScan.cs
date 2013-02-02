using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using AxMSTSCLib;
using AxWFICALib;
using Microsoft.VisualBasic;
using iptb;
using mRemoteNC;
using mRemoteNC.App;
using My;
using WeifenLuo.WinFormsUI.Docking;

//using mRemoteNC.Runtime;

namespace mRemoteNC
{
    namespace UI
    {
        namespace Window
        {
            public class PortScan : Base
            {
                #region Form Init

                internal System.Windows.Forms.Label lblEndIP;
                internal System.Windows.Forms.Label lblStartIP;
                internal System.Windows.Forms.Button btnScan;
                internal System.Windows.Forms.Panel pnlDivider;
                internal IPTextBox ipEnd;
                internal System.Windows.Forms.SplitContainer splContainer;
                internal System.Windows.Forms.ListView lvHosts;
                internal System.Windows.Forms.ColumnHeader clmHost;
                internal System.Windows.Forms.ColumnHeader clmSSH;
                internal System.Windows.Forms.ColumnHeader clmTelnet;
                internal System.Windows.Forms.ColumnHeader clmHTTP;
                internal System.Windows.Forms.ColumnHeader clmHTTPS;
                internal System.Windows.Forms.ColumnHeader clmRlogin;
                internal System.Windows.Forms.ColumnHeader clmRDP;
                internal System.Windows.Forms.ColumnHeader clmVNC;
                internal System.Windows.Forms.ColumnHeader clmOpenPorts;
                internal System.Windows.Forms.ColumnHeader clmClosedPorts;
                internal System.Windows.Forms.ProgressBar prgBar;
                internal System.Windows.Forms.Label lblOnlyImport;
                internal System.Windows.Forms.ComboBox cbProtocol;
                internal System.Windows.Forms.Panel pnlPorts;
                internal System.Windows.Forms.NumericUpDown portEnd;
                internal System.Windows.Forms.NumericUpDown portStart;
                internal System.Windows.Forms.Label Label2;
                internal System.Windows.Forms.Label Label1;
                internal System.Windows.Forms.Button btnImport;
                internal System.Windows.Forms.Button btnCancel;
                internal IPTextBox ipStart;

                private void InitializeComponent()
                {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PortScan));
            this.ipStart = new iptb.IPTextBox();
            this.ipEnd = new iptb.IPTextBox();
            this.lblStartIP = new System.Windows.Forms.Label();
            this.lblEndIP = new System.Windows.Forms.Label();
            this.btnScan = new System.Windows.Forms.Button();
            this.pnlDivider = new System.Windows.Forms.Panel();
            this.splContainer = new System.Windows.Forms.SplitContainer();
            this.lvHosts = new System.Windows.Forms.ListView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.cbProtocol = new System.Windows.Forms.ComboBox();
            this.lblOnlyImport = new System.Windows.Forms.Label();
            this.clmHost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmSSH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmTelnet = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmHTTP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmHTTPS = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmRlogin = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmRDP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmVNC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmOpenPorts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmClosedPorts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.pnlPorts = new System.Windows.Forms.Panel();
            this.portEnd = new System.Windows.Forms.NumericUpDown();
            this.portStart = new System.Windows.Forms.NumericUpDown();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splContainer)).BeginInit();
            this.splContainer.Panel1.SuspendLayout();
            this.splContainer.Panel2.SuspendLayout();
            this.splContainer.SuspendLayout();
            this.pnlPorts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.portStart)).BeginInit();
            this.SuspendLayout();
            // 
            // ipStart
            // 
            this.ipStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipStart.Location = new System.Drawing.Point(7, 37);
            this.ipStart.Name = "ipStart";
            this.ipStart.Size = new System.Drawing.Size(132, 20);
            this.ipStart.TabIndex = 10;
            this.ipStart.ToolTipText = "";
            // 
            // ipEnd
            // 
            this.ipEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipEnd.Location = new System.Drawing.Point(159, 37);
            this.ipEnd.Name = "ipEnd";
            this.ipEnd.Size = new System.Drawing.Size(135, 20);
            this.ipEnd.TabIndex = 15;
            this.ipEnd.ToolTipText = "";
            // 
            // lblStartIP
            // 
            this.lblStartIP.AutoSize = true;
            this.lblStartIP.Location = new System.Drawing.Point(4, 10);
            this.lblStartIP.Name = "lblStartIP";
            this.lblStartIP.Size = new System.Drawing.Size(45, 13);
            this.lblStartIP.TabIndex = 0;
            this.lblStartIP.Text = "Start IP:";
            // 
            // lblEndIP
            // 
            this.lblEndIP.AutoSize = true;
            this.lblEndIP.Location = new System.Drawing.Point(156, 10);
            this.lblEndIP.Name = "lblEndIP";
            this.lblEndIP.Size = new System.Drawing.Size(42, 13);
            this.lblEndIP.TabIndex = 5;
            this.lblEndIP.Text = "End IP:";
            // 
            // btnScan
            // 
            this.btnScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScan.Image = global::My.Resources.Resources.Search;
            this.btnScan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnScan.Location = new System.Drawing.Point(763, 9);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(86, 59);
            this.btnScan.TabIndex = 20;
            this.btnScan.Text = "&Scan";
            this.btnScan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // pnlDivider
            // 
            this.pnlDivider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDivider.BackColor = System.Drawing.Color.DimGray;
            this.pnlDivider.Location = new System.Drawing.Point(0, 11);
            this.pnlDivider.Name = "pnlDivider";
            this.pnlDivider.Size = new System.Drawing.Size(861, 10);
            this.pnlDivider.TabIndex = 20;
            // 
            // splContainer
            // 
            this.splContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splContainer.IsSplitterFixed = true;
            this.splContainer.Location = new System.Drawing.Point(0, 74);
            this.splContainer.Name = "splContainer";
            this.splContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splContainer.Panel1
            // 
            this.splContainer.Panel1.Controls.Add(this.lvHosts);
            this.splContainer.Panel1.Controls.Add(this.pnlDivider);
            // 
            // splContainer.Panel2
            // 
            this.splContainer.Panel2.Controls.Add(this.btnCancel);
            this.splContainer.Panel2.Controls.Add(this.btnImport);
            this.splContainer.Panel2.Controls.Add(this.cbProtocol);
            this.splContainer.Panel2.Controls.Add(this.lblOnlyImport);
            this.splContainer.Size = new System.Drawing.Size(861, 410);
            this.splContainer.SplitterDistance = 367;
            this.splContainer.TabIndex = 27;
            // 
            // lvHosts
            // 
            this.lvHosts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvHosts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvHosts.FullRowSelect = true;
            this.lvHosts.GridLines = true;
            this.lvHosts.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvHosts.HideSelection = false;
            this.lvHosts.Location = new System.Drawing.Point(0, 27);
            this.lvHosts.Name = "lvHosts";
            this.lvHosts.Size = new System.Drawing.Size(861, 339);
            this.lvHosts.TabIndex = 26;
            this.lvHosts.UseCompatibleStateImageBehavior = false;
            this.lvHosts.View = System.Windows.Forms.View.Details;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(778, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 111;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.Location = new System.Drawing.Point(697, 8);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 101;
            this.btnImport.Text = "&Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // cbProtocol
            // 
            this.cbProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProtocol.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbProtocol.FormattingEnabled = true;
            this.cbProtocol.Items.AddRange(new object[] {
            "SSH2",
            "Telnet",
            "HTTP",
            "HTTPS",
            "Rlogin",
            "RDP",
            "VNC"});
            this.cbProtocol.Location = new System.Drawing.Point(111, 9);
            this.cbProtocol.Name = "cbProtocol";
            this.cbProtocol.Size = new System.Drawing.Size(122, 21);
            this.cbProtocol.TabIndex = 28;
            // 
            // lblOnlyImport
            // 
            this.lblOnlyImport.AutoSize = true;
            this.lblOnlyImport.Location = new System.Drawing.Point(3, 12);
            this.lblOnlyImport.Name = "lblOnlyImport";
            this.lblOnlyImport.Size = new System.Drawing.Size(92, 13);
            this.lblOnlyImport.TabIndex = 1;
            this.lblOnlyImport.Text = "Protocol to import:";
            // 
            // clmHost
            // 
            this.clmHost.Text = "Hostname/IP";
            this.clmHost.Width = 130;
            // 
            // clmSSH
            // 
            this.clmSSH.Text = "SSH";
            this.clmSSH.Width = 50;
            // 
            // clmTelnet
            // 
            this.clmTelnet.Text = "Telnet";
            this.clmTelnet.Width = 50;
            // 
            // clmHTTP
            // 
            this.clmHTTP.Text = "HTTP";
            this.clmHTTP.Width = 50;
            // 
            // clmHTTPS
            // 
            this.clmHTTPS.Text = "HTTPS";
            this.clmHTTPS.Width = 50;
            // 
            // clmRlogin
            // 
            this.clmRlogin.Text = "Rlogin";
            this.clmRlogin.Width = 50;
            // 
            // clmRDP
            // 
            this.clmRDP.Text = "RDP";
            this.clmRDP.Width = 50;
            // 
            // clmVNC
            // 
            this.clmVNC.Text = "VNC";
            this.clmVNC.Width = 50;
            // 
            // clmOpenPorts
            // 
            this.clmOpenPorts.Text = "Open Ports";
            this.clmOpenPorts.Width = 150;
            // 
            // clmClosedPorts
            // 
            this.clmClosedPorts.Text = "Closed Ports";
            this.clmClosedPorts.Width = 150;
            // 
            // prgBar
            // 
            this.prgBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prgBar.Location = new System.Drawing.Point(7, 63);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(751, 16);
            this.prgBar.Step = 1;
            this.prgBar.TabIndex = 28;
            // 
            // pnlPorts
            // 
            this.pnlPorts.Controls.Add(this.portEnd);
            this.pnlPorts.Controls.Add(this.portStart);
            this.pnlPorts.Controls.Add(this.Label2);
            this.pnlPorts.Controls.Add(this.Label1);
            this.pnlPorts.Location = new System.Drawing.Point(319, 8);
            this.pnlPorts.Name = "pnlPorts";
            this.pnlPorts.Size = new System.Drawing.Size(152, 51);
            this.pnlPorts.TabIndex = 18;
            // 
            // portEnd
            // 
            this.portEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.portEnd.Location = new System.Drawing.Point(79, 28);
            this.portEnd.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.portEnd.Name = "portEnd";
            this.portEnd.Size = new System.Drawing.Size(67, 20);
            this.portEnd.TabIndex = 15;
            // 
            // portStart
            // 
            this.portStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.portStart.Location = new System.Drawing.Point(6, 28);
            this.portStart.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.portStart.Name = "portStart";
            this.portStart.Size = new System.Drawing.Size(67, 20);
            this.portStart.TabIndex = 5;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(78, 1);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(51, 13);
            this.Label2.TabIndex = 10;
            this.Label2.Text = "End Port:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(3, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(54, 13);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Start Port:";
            // 
            // PortScan
            // 
            this.AcceptButton = this.btnImport;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(861, 484);
            this.Controls.Add(this.pnlPorts);
            this.Controls.Add(this.prgBar);
            this.Controls.Add(this.splContainer);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.lblEndIP);
            this.Controls.Add(this.lblStartIP);
            this.Controls.Add(this.ipEnd);
            this.Controls.Add(this.ipStart);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PortScan";
            this.TabText = "Port Scan";
            this.Text = "Port Scan";
            this.splContainer.Panel1.ResumeLayout(false);
            this.splContainer.Panel2.ResumeLayout(false);
            this.splContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splContainer)).EndInit();
            this.splContainer.ResumeLayout(false);
            this.pnlPorts.ResumeLayout(false);
            this.pnlPorts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.portStart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

                }

                #endregion Form Init

                #region Public Methods

                public PortScan(DockContent Panel, Tools.PortScan.PortScanMode Mode)
                {
                    this.WindowType = Type.PortScan;
                    this.DockPnl = Panel;
                    this.InitializeComponent();
                    psMode = Mode;
                }

                #endregion Public Methods

                private Tools.PortScan.PortScanMode psMode;
                private Tools.PortScan.Scanner pScanner;
                private bool scanning = false;

                #region Form Stuff

                private void PortScan_Load(System.Object sender, System.EventArgs e)
                {
                    ApplyLanguage();

                    try
                    {
                        if (psMode == Tools.PortScan.PortScanMode.Normal)
                        {
                            this.lvHosts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.clmHost, this.clmOpenPorts, this.clmClosedPorts });
                            pnlPorts.Visible = true;
                            splContainer.Panel2Collapsed = true;
                        }
                        else
                        {
                            this.lvHosts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[]
                                                              {
                                                                  this.clmHost, this.clmSSH, this.clmTelnet, this.clmHTTP,
                                                                  this.clmHTTPS, this.clmRlogin, this.clmRDP, this.clmVNC
                                                              });
                            pnlPorts.Visible = false;
                            splContainer.Panel2Collapsed = false;
                            cbProtocol.SelectedIndex = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            Language.strPortScanCouldNotLoadPanel + Constants.vbNewLine +
                                                            Constants.vbNewLine + ex.Message);
                    }
                }

                private void ApplyLanguage()
                {
                    lblStartIP.Text = Language.strStartIP + ":";
                    lblEndIP.Text = Language.strEndIP + ":";
                    btnScan.Text = Language.strButtonScan;
                    btnCancel.Text = Language.strButtonCancel;
                    btnImport.Text = Language.strButtonImport;
                    lblOnlyImport.Text = Language.strProtocolToImport + ":";
                    clmHost.Text = Language.strColumnHostnameIP;
                    clmOpenPorts.Text = Language.strOpenPorts;
                    clmClosedPorts.Text = Language.strClosedPorts;
                    Label2.Text = Language.strEndPort + ":";
                    Label1.Text = Language.strStartPort + ":";
                    TabText = Language.strMenuPortScan;
                    Text = Language.strMenuPortScan;
                }

                private void btnScan_Click(System.Object sender, System.EventArgs e)
                {
                    if (scanning == true)
                    {
                        StopScan();
                    }
                    else
                    {
                        if (ipOK())
                        {
                            StartScan();
                        }
                        else
                        {
                            Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                                Language.strCannotStartPortScan);
                        }
                    }
                }

                private void btnImport_Click(System.Object sender, System.EventArgs e)
                {
                    Protocols prot =
                        (Protocols)Tools.Misc.StringToEnum(typeof(Protocols), cbProtocol.SelectedItem.ToString());

                    ArrayList arrHosts = new ArrayList();
                    foreach (ListViewItem lvItem in lvHosts.SelectedItems)
                    {
                        arrHosts.Add(lvItem.Tag);
                    }

                    Runtime.ImportConnectionsFromPortScan(arrHosts, prot);

                    //Me.DialogResult = System.Windows.Forms.DialogResult.OK
                    //Me.Close()
                }

                private void btnCancel_Click(System.Object sender, System.EventArgs e)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.Close();
                }

                #endregion Form Stuff

                #region Threading Delegates

                private delegate void AddListViewItemCB(ListViewItem Item);

                private void AddListViewItem(ListViewItem Item)
                {
                    if (lvHosts.InvokeRequired)
                    {
                        AddListViewItemCB d = new AddListViewItemCB(AddListViewItem);
                        this.Invoke(d, new object[] { Item });
                    }
                    else
                    {
                        lvHosts.Items.Add(Item);
                        Item.EnsureVisible();
                    }
                }

                private delegate void SwitchButtonTextCB();

                private void SwitchButtonText()
                {
                    if (btnScan.InvokeRequired)
                    {
                        SwitchButtonTextCB d = new SwitchButtonTextCB(SwitchButtonText);
                        this.Invoke(d);
                    }
                    else
                    {
                        if (scanning == true)
                        {
                            btnScan.Text = Language.strButtonStop;
                        }
                        else
                        {
                            btnScan.Text = Language.strButtonScan;
                        }
                    }
                }

                private delegate void SetPrgBarCB(int Value, int Max);

                private void SetPrgBar(int Value, int Max)
                {
                    if (prgBar.InvokeRequired)
                    {
                        SetPrgBarCB d = new SetPrgBarCB(SetPrgBar);
                        this.Invoke(d, new object[] { Value, Max });
                    }
                    else
                    {
                        prgBar.Maximum = Max;
                        prgBar.Value = Value;
                    }
                }

                #endregion Threading Delegates

                #region Methods

                private void StartScan()
                {
                    try
                    {
                        scanning = true;
                        SwitchButtonText();
                        SetPrgBar(0, 100);
                        lvHosts.Items.Clear();

                        if (psMode == Tools.PortScan.PortScanMode.Import)
                        {
                            pScanner = new Tools.PortScan.Scanner(ipStart.Text, ipEnd.Text);
                        }
                        else
                        {
                            pScanner = new Tools.PortScan.Scanner(ipStart.Text, ipEnd.Text, portStart.Value.ToString(),
                                                                  portEnd.Value.ToString());
                        }

                        pScanner.BeginHostScan += Event_BeginHostScan;
                        pScanner.HostScanned += Event_HostScanned;
                        pScanner.ScanComplete += Event_ScanComplete;

                        pScanner.StartScan();
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("StartScan failed (UI.Window.PortScan)" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void StopScan()
                {
                    pScanner.StopScan();
                    scanning = false;
                    SwitchButtonText();
                }

                private bool ipOK()
                {
                    return ipStart.IsValid() && ipEnd.IsValid();
                }

                #endregion Methods

                #region Event Handlers

                private void Event_BeginHostScan(string Host)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.InformationMsg, "Scanning " + Host, true);
                }

                private void Event_HostScanned(Tools.PortScan.ScanHost Host, int AlreadyScanned, int ToBeScanned)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.InformationMsg,
                                                        "Host scanned " + Host.HostIP, true);

                    ListViewItem lvI = Host.ToListViewItem(psMode);
                    AddListViewItem(lvI);

                    SetPrgBar(AlreadyScanned, ToBeScanned);
                }

                private void Event_ScanComplete(ArrayList Hosts)
                {
                    scanning = false;
                    SwitchButtonText();
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.InformationMsg, "Scan complete!");
                }

                #endregion Event Handlers
            }
        }
    }
}