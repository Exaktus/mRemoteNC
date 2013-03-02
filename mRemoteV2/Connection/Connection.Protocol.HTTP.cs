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

namespace mRemoteNC
{
    public class HTTP : HTTPBase
    {
        public HTTP(RenderingEngine RenderingEngine)
            : base(RenderingEngine)
        {
        }

        public override void NewExtended()
        {
            base.NewExtended();

            httpOrS = "http";
            defaultPort = System.Convert.ToInt32(Defaults.Port);
        }

        public enum Defaults
        {
            Port = 80
        }
    }
}

namespace mRemoteNC.Connection
{
}