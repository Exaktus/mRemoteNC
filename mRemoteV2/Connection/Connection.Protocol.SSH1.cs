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
using mRemoteNC.Connection;

namespace mRemoteNC
{
    public class SSH1 : PuttyBase
    {
        public SSH1()
        {
            this.PuttyProtocol = Putty_Protocol.ssh;
            this.PuttySSHVersion = Putty_SSHVersion.ssh1;
        }

        public enum Defaults
        {
            Port = 22
        }
    }
}

namespace mRemoteNC.Connection
{
}