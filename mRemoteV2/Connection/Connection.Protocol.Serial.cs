using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using AxWFICALib;
using Microsoft.VisualBasic;
using mRemoteNC.Connection;

namespace mRemoteNC
{
    public class Serial : PuttyBase
    {
        public Serial()
        {
            this.PuttyProtocol = Putty_Protocol.serial;
        }

        public enum Defaults
        {
            Port = 9600
        }
    }
}

namespace mRemoteNC.Connection
{
}