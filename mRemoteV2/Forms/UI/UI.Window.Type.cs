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
    namespace UI
    {
        namespace Window
        {
            public enum Type
            {
                Tree = 0,
                Connection = 1,
                Config = 2,
                Sessions = 3,
                ErrorsAndInfos = 4,
                ScreenshotManager = 5,
                Options = 6,
                SaveAs = 7,
                About = 8,
                Update = 9,
                SSHTransfer = 10,
                ADImport = 11,
                Help = 12,
                ExternalApps = 13,
                PortScan = 14,
                UltraVNCSC = 16,
                ComponentsCheck = 17,
                Announcement = 18,
                ConnectionStatus = 19,
                QuickText
            }
        }
    }
}