using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Win32;

namespace mRemoteNC.Forms.Importer.Helpers
{
    class PuttyConfigSearcher:IConfigSearcher
    {
        public bool SearchInFiles { get { return false; }}

        public IEnumerable<Connection.Info> GetConnections()
        {
            var result = new List<Connection.Info>();
            try
            {
                var allSessionsKey = Registry.CurrentUser.OpenSubKey("Software\\SimonTatham\\PuTTY\\Sessions");
                if (allSessionsKey != null)
                {
                    var allSessions = allSessionsKey.GetSubKeyNames();
                    foreach (var session in allSessions)
                    {
                        var coni = new Connection.Info { PuttySession = session, Description = "PuTTY Session", Icon = "PuTTY" };
                        var sessionKey = allSessionsKey.OpenSubKey(session);
                        if (sessionKey != null)
                        {
                            coni.Hostname = sessionKey.GetValue("HostName").ToString();
                            if (string.IsNullOrWhiteSpace(coni.Hostname))
                            {
                                continue;
                            }
                            coni.Username = sessionKey.GetValue("UserName").ToString();
                            var prot = sessionKey.GetValue("Protocol").ToString();
                            switch (prot)
                            {
                                case "ssh":
                                    coni.Protocol = Convert.ToInt32(sessionKey.GetValue("SshProt")) == 1 ? Protocols.SSH1 : Protocols.SSH2;
                                    coni.Icon = "SSH";
                                    break;
                                case "raw":
                                    coni.Protocol = Protocols.RAW;
                                    break;
                                case "telnet":
                                    coni.Protocol = Protocols.Telnet;
                                    break;
                                case "rlogin":
                                    coni.Protocol = Protocols.Rlogin;
                                    break;
                                case "serial":
                                    coni.Protocol = Protocols.Rlogin;
                                    break;
                                default:
                                    coni.Protocol = Protocols.NONE;
                                    break;
                            }
                            coni.Port = Convert.ToInt32(sessionKey.GetValue("PortNumber"));
                            coni.Name = session + " - Imported";
                            result.Add(coni);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return result;
        }
    }
}
