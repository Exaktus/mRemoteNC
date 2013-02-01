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

namespace mRemoteNC
{
    namespace Images
    {
        public class Enums
        {
            public enum TreeImage
            {
                Root = 0,
                Container = 1,
                ConnectionOpen = 2,
                ConnectionClosed = 3
            }

            public enum ErrorImage
            {
                _Information = 0,
                _Warning = 1,
                _Error = 2
            }
        }
    }
}