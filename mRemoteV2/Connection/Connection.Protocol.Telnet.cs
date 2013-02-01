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
    public class Telnet : PuttyBase
    {
        public Telnet()
        {
            this.PuttyProtocol = Putty_Protocol.telnet;
        }

        public enum Defaults
        {
            Port = 23
        }
    }
}

namespace mRemoteNC.Connection
{
}