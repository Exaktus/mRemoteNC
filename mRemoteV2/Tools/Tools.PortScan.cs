using System;
using System.Collections;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using mRemoteNC.App;
using My;

namespace mRemoteNC
{
    namespace Tools
    {
        namespace PortScan
        {
            public enum PortScanMode
            {
                Normal = 1,
                Import = 2
            }

            public class ScanHost
            {
                #region Properties

                private static int _SSHPort = System.Convert.ToInt32(mRemoteNC.SSH1.Defaults.Port);

                public static int SSHPort
                {
                    get { return _SSHPort; }
                    set { _SSHPort = value; }
                }

                private static int _TelnetPort = System.Convert.ToInt32(mRemoteNC.Telnet.Defaults.Port);

                public static int TelnetPort
                {
                    get { return _TelnetPort; }
                    set { _TelnetPort = value; }
                }

                public static int SerialPort { get; set; }

                public static int HTTPPort { get; set; }

                private static int _HTTPSPort = System.Convert.ToInt32(mRemoteNC.HTTPS.Defaults.Port);

                public static int HTTPSPort
                {
                    get { return _HTTPSPort; }
                    set { _HTTPSPort = value; }
                }

                public static int RloginPort { get; set; }

                private static int _RDPPort = System.Convert.ToInt32(mRemoteNC.RDP.Defaults.Port);

                public static int RDPPort
                {
                    get { return _RDPPort; }
                    set { _RDPPort = value; }
                }

                private static int _VNCPort = System.Convert.ToInt32(mRemoteNC.VNC.Defaults.Port);

                public static int VNCPort
                {
                    get { return _VNCPort; }
                    set { _VNCPort = value; }
                }

                private string _HostName;

                public string HostName
                {
                    get { return _HostName; }
                    set { _HostName = value; }
                }

                public string HostNameWithoutDomain
                {
                    get
                    {
                        if (_HostName != _HostIP)
                        {
                            if (_HostName != null)
                            {
                                if (_HostName.Contains("."))
                                {
                                    return _HostName.Substring(0, _HostName.IndexOf("."));
                                }
                                else
                                {
                                    return _HostName;
                                }
                            }
                            else
                            {
                                return _HostIP;
                            }
                        }
                        else
                        {
                            return _HostIP;
                        }
                    }
                }

                private string _HostIP;

                public string HostIP
                {
                    get { return _HostIP; }
                    set { _HostIP = value; }
                }

                private ArrayList _OpenPorts = new ArrayList();

                public ArrayList OpenPorts
                {
                    get { return _OpenPorts; }
                    set { _OpenPorts = value; }
                }

                private ArrayList _ClosedPorts;

                public ArrayList ClosedPorts
                {
                    get { return _ClosedPorts; }
                    set { _ClosedPorts = value; }
                }

                private bool _RDP;

                public bool RDP
                {
                    get { return _RDP; }
                    set { _RDP = value; }
                }

                private bool _VNC;

                public bool VNC
                {
                    get { return _VNC; }
                    set { _VNC = value; }
                }

                private bool _SSH;

                public bool SSH
                {
                    get { return _SSH; }
                    set { _SSH = value; }
                }

                private bool _Telnet;

                public bool Telnet
                {
                    get { return _Telnet; }
                    set { _Telnet = value; }
                }

                public bool Rlogin { get; set; }

                public bool Serial { get; set; }

                private bool _HTTP;

                public bool HTTP
                {
                    get { return _HTTP; }
                    set { _HTTP = value; }
                }

                private bool _HTTPS;

                public bool HTTPS
                {
                    get { return _HTTPS; }
                    set { _HTTPS = value; }
                }

                #endregion Properties

                #region Methods

                public ScanHost(string host)
                {
                    _HostIP = host;
                    _OpenPorts = new ArrayList();
                    _ClosedPorts = new ArrayList();
                }

                static ScanHost()
                {
                    RloginPort = System.Convert.ToInt32(mRemoteNC.Rlogin.Defaults.Port);
                    HTTPPort = System.Convert.ToInt32(mRemoteNC.HTTP.Defaults.Port);
                    SerialPort = System.Convert.ToInt32(mRemoteNC.Serial.Defaults.Port);
                }

                public override string ToString()
                {
                    try
                    {
                        return "SSH: " + _SSH + " Telnet: " + _Telnet + " HTTP: " + _HTTP + " HTTPS: " + _HTTPS +
                               " Rlogin: " + Rlogin + " RDP: " + _RDP + " VNC: " + _VNC + " Serial: " + _VNC;
                    }
                    catch (Exception)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                            "ToString failed (Tools.PortScan)", true);
                        return "";
                    }
                }

                public ListViewItem ToListViewItem(PortScanMode Mode)
                {
                    try
                    {
                        ListViewItem lvI = new ListViewItem();
                        lvI.Tag = this;
                        if (_HostName != "")
                        {
                            lvI.Text = _HostName;
                        }
                        else
                        {
                            lvI.Text = _HostIP;
                        }

                        if (Mode == PortScanMode.Import)
                        {
                            lvI.SubItems.Add(BoolToYesNo(_SSH));
                            lvI.SubItems.Add(BoolToYesNo(_Telnet));
                            lvI.SubItems.Add(BoolToYesNo(_HTTP));
                            lvI.SubItems.Add(BoolToYesNo(_HTTPS));
                            lvI.SubItems.Add(BoolToYesNo(Rlogin));
                            lvI.SubItems.Add(BoolToYesNo(Serial));
                            lvI.SubItems.Add(BoolToYesNo(_RDP));
                            lvI.SubItems.Add(BoolToYesNo(_VNC));
                        }
                        else
                        {
                            string strOpen = "";
                            string strClosed = "";

                            foreach (int p in _OpenPorts)
                            {
                                strOpen += System.Convert.ToString(p.ToString() + ", ");
                            }

                            foreach (int p in _ClosedPorts)
                            {
                                strClosed += System.Convert.ToString(p.ToString() + ", ");
                            }

                            lvI.SubItems.Add(strOpen.Substring(0,
                                                               System.Convert.ToInt32(strOpen.Length > 0
                                                                                          ? strOpen.Length - 2
                                                                                          : strOpen.Length)));
                            lvI.SubItems.Add(strClosed.Substring(0,
                                                                 System.Convert.ToInt32(strClosed.Length > 0
                                                                                            ? strClosed.Length - 2
                                                                                            : strClosed.Length)));
                        }

                        return lvI;
                    }
                    catch (Exception)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                            "ToString failed (Tools.PortScan)", true);
                        return null;
                    }
                }

                private string BoolToYesNo(bool Bool)
                {
                    if (@Bool == true)
                    {
                        return Language.strYes;
                    }
                    else
                    {
                        return Language.strNo;
                    }
                }

                public void SetAllProtocols(bool Open)
                {
                    _VNC = false;
                    _Telnet = false;
                    _SSH = false;
                    Rlogin = false;
                    _RDP = false;
                    _HTTPS = false;
                    _HTTP = false;
                    Serial = false;
                }

                #endregion Methods
            }

            public class Scanner
            {
                #region Properties

                private string _StartIP;

                public string StartIP
                {
                    get { return _StartIP; }
                    set { _StartIP = value; }
                }

                private string _EndIP;

                public string EndIP
                {
                    get { return _EndIP; }
                    set { _EndIP = value; }
                }

                private int _StartPort;

                public int StartPort
                {
                    get { return _StartPort; }
                    set { _StartPort = value; }
                }

                private int _EndPort;

                public int EndPort
                {
                    get { return _EndPort; }
                    set { _EndPort = value; }
                }

                private ArrayList _ScannedHosts;

                public ArrayList ScannedHosts
                {
                    get { return _ScannedHosts; }
                    set { _ScannedHosts = value; }
                }

                #endregion Properties

                #region Methods

                public Scanner(string startIP, string endIP)
                {
                    Mode = PortScanMode.Import;

                    _StartIP = startIP;
                    _EndIP = endIP;

                    Ports = new ArrayList();
                    Ports.AddRange(new int[]
                                       {
                                           ScanHost.SSHPort, ScanHost.TelnetPort, ScanHost.HTTPPort, ScanHost.HTTPSPort,
                                           ScanHost.RloginPort, ScanHost.RDPPort, ScanHost.VNCPort, ScanHost.SerialPort
                                       });

                    Hosts = GetIPRange(_StartIP, _EndIP);

                    _ScannedHosts = new ArrayList();
                }

                public Scanner(string startIP, string endIP, string startPort, string endPort)
                {
                    Mode = PortScanMode.Normal;

                    _StartIP = startIP;
                    _EndIP = endIP;

                    _StartPort = int.Parse(startPort);
                    _EndPort = int.Parse(endPort);

                    Ports = new ArrayList();
                    for (int p = Convert.ToInt32(startPort); p <= Convert.ToInt32(endPort); p++)
                    {
                        Ports.Add(p);
                    }

                    Hosts = GetIPRange(_StartIP, _EndIP);

                    _ScannedHosts = new ArrayList();
                }

                public delegate void BeginHostScanEventHandler(string Host);

                private BeginHostScanEventHandler BeginHostScanEvent;

                public event BeginHostScanEventHandler BeginHostScan
                {
                    add
                    {
                        BeginHostScanEvent =
                            (BeginHostScanEventHandler)System.Delegate.Combine(BeginHostScanEvent, value);
                    }
                    remove
                    {
                        BeginHostScanEvent =
                            (BeginHostScanEventHandler)System.Delegate.Remove(BeginHostScanEvent, value);
                    }
                }

                public delegate void HostScannedEventHandler(
                    ScanHost SHost, int HostsAlreadyScanned, int HostsToBeScanned);

                private HostScannedEventHandler HostScannedEvent;

                public event HostScannedEventHandler HostScanned
                {
                    add { HostScannedEvent = (HostScannedEventHandler)System.Delegate.Combine(HostScannedEvent, value); }
                    remove { HostScannedEvent = (HostScannedEventHandler)System.Delegate.Remove(HostScannedEvent, value); }
                }

                public delegate void ScanCompleteEventHandler(ArrayList Hosts);

                private ScanCompleteEventHandler ScanCompleteEvent;

                public event ScanCompleteEventHandler ScanComplete
                {
                    add { ScanCompleteEvent = (ScanCompleteEventHandler)System.Delegate.Combine(ScanCompleteEvent, value); }
                    remove { ScanCompleteEvent = (ScanCompleteEventHandler)System.Delegate.Remove(ScanCompleteEvent, value); }
                }

                private ArrayList Hosts;
                private ArrayList Ports;

                private PortScanMode Mode;

                private Thread sThread;

                public void StartScan()
                {
                    sThread = new Thread(new System.Threading.ThreadStart(StartScanBG));
                    sThread.SetApartmentState(System.Threading.ApartmentState.STA);
                    sThread.IsBackground = true;
                    sThread.Start();
                }

                public void StopScan()
                {
                    sThread.Abort();
                }

                public static bool IsPortOpen(string Hostname, string Port)
                {
                    try
                    {
                        System.Net.Sockets.TcpClient tcpClient = new System.Net.Sockets.TcpClient(Hostname,
                                                                                                  int.Parse(Port));
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }

                private void StartScanBG()
                {
                    try
                    {
                        int hCount = 0;

                        foreach (string Host in Hosts)
                        {
                            if (BeginHostScanEvent != null)
                                BeginHostScanEvent(Host);

                            ScanHost sHost = new ScanHost(Host);
                            hCount++;

                            bool HostAlive = false;

                            HostAlive = IsHostAlive(Host);

                            if (HostAlive == false)
                            {
                                sHost.ClosedPorts.AddRange(Ports);
                                sHost.SetAllProtocols(false);
                            }
                            else
                            {
                                foreach (int Port in Ports)
                                {
                                    bool err = false;

                                    try
                                    {
                                        System.Net.Sockets.TcpClient tcpClient = new System.Net.Sockets.TcpClient(Host,
                                                                                                                  Port);

                                        err = false;
                                        sHost.OpenPorts.Add(Port);
                                    }
                                    catch (Exception)
                                    {
                                        err = true;
                                        sHost.ClosedPorts.Add(Port);
                                    }

                                    if (Port == ScanHost.SSHPort)
                                    {
                                        sHost.SSH = System.Convert.ToBoolean(!err);
                                    }
                                    else if (Port == ScanHost.TelnetPort)
                                    {
                                        sHost.Telnet = System.Convert.ToBoolean(!err);
                                    }
                                    else if (Port == ScanHost.HTTPPort)
                                    {
                                        sHost.HTTP = System.Convert.ToBoolean(!err);
                                    }
                                    else if (Port == ScanHost.HTTPSPort)
                                    {
                                        sHost.HTTPS = System.Convert.ToBoolean(!err);
                                    }
                                    else if (Port == ScanHost.RloginPort)
                                    {
                                        sHost.Rlogin = System.Convert.ToBoolean(!err);
                                    }
                                    else if (Port == ScanHost.SerialPort)
                                    {
                                        sHost.Serial = System.Convert.ToBoolean(!err);
                                    }
                                    else if (Port == ScanHost.RDPPort)
                                    {
                                        sHost.RDP = System.Convert.ToBoolean(!err);
                                    }
                                    else if (Port == ScanHost.VNCPort)
                                    {
                                        sHost.VNC = System.Convert.ToBoolean(!err);
                                    }
                                }
                            }

                            if (HostAlive == true)
                            {
                                try
                                {
                                    sHost.HostName = System.Net.Dns.GetHostEntry(sHost.HostIP).HostName;
                                }
                                catch (Exception)
                                {
                                }
                            }

                            _ScannedHosts.Add(sHost);
                            if (HostScannedEvent != null)
                                HostScannedEvent(sHost, hCount, Hosts.Count);
                        }

                        if (ScanCompleteEvent != null)
                            ScanCompleteEvent(_ScannedHosts);
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                            (string)
                                                            ("StartScanBG failed (Tools.PortScan)" + Constants.vbNewLine +
                                                             ex.Message), true);
                    }
                }

                private bool IsHostAlive(string Host)
                {
                    Ping pingSender = new Ping();
                    PingReply pReply;

                    try
                    {
                        pReply = pingSender.Send(Host);

                        if (pReply.Status == IPStatus.Success)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }

                private ArrayList GetIPRange(string fromIP, string toIP)
                {
                    try
                    {
                        if (fromIP == toIP)
                        {
                            return new ArrayList(1){toIP};
                        }
                        ArrayList arrIPs = new ArrayList();

                        int[] ipFrom = fromIP.Split(".".ToCharArray()).ToList().ConvertAll(Convert.ToInt32).ToArray();
                        int[] ipTo = toIP.Split(".".ToCharArray()).ToList().ConvertAll(Convert.ToInt32).ToArray();

                        while (!matchIP(ipFrom.ToList().ConvertAll(Convert.ToString).ToArray(), ipTo.ToList().ConvertAll(Convert.ToString).ToArray()))
                        {
                            arrIPs.Add(string.Format("{0}.{1}.{2}.{3}", ipFrom[0], ipFrom[1], ipFrom[2], ipFrom[3]));
                            ipFrom[3]++;
                            if (ipFrom[3] > 255)
                            {
                                ipFrom[3] = 0;
                                ipFrom[2]++;
                                if (ipFrom[2] > 255)
                                {
                                    ipFrom[2] = 0;
                                    ipFrom[1]++;
                                    if (ipFrom[1] > 255)
                                    {
                                        ipFrom[1] = 0;
                                        ipFrom[0]++;
                                        if (ipFrom[0] > 255)
                                        {
                                            ipFrom[0] = 0;
                                        }
                                    }
                                }
                            }
                        }

                        return arrIPs;
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                            (string)
                                                            ("GetIPRange failed (Tools.PortScan)" + Constants.vbNewLine +
                                                             ex.Message), true);
                        return null;
                    }
                }

                private bool matchIP(string[] fromIP, string[] toIP)
                {
                    try
                    {
                        for (int c = 0; c <= fromIP.Length - 1; c++)
                        {
                            if (c == fromIP.Length - 1)
                            {
                                if (!(fromIP[c] == toIP[c] + 1))
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                if (!(fromIP[c] == toIP[c]))
                                {
                                    return false;
                                }
                            }
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                            (string)
                                                            ("matchIP failed (Tools.PortScan)" + Constants.vbNewLine +
                                                             ex.Message), true);
                        return false;
                    }
                }

                #endregion Methods
            }
        }
    }
}