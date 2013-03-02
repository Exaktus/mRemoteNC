using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using AxWFICALib;
using Microsoft.VisualBasic;

namespace mRemoteNC
{
    public enum Protocols
    {
        [LocalizedAttributes.LocalizedDescriptionAttribute("strRDP")]
        RDP = 0,
        [LocalizedAttributes.LocalizedDescriptionAttribute("strVnc")]
        VNC = 1,
        [LocalizedAttributes.LocalizedDescriptionAttribute("strSsh1")]
        SSH1 = 2,
        [LocalizedAttributes.LocalizedDescriptionAttribute("strSsh2")]
        SSH2 = 3,
        [LocalizedAttributes.LocalizedDescriptionAttribute("strTelnet")]
        Telnet = 4,
        [LocalizedAttributes.LocalizedDescriptionAttribute("strRlogin")]
        Rlogin = 5,
        [LocalizedAttributes.LocalizedDescriptionAttribute("strRAW")]
        RAW = 6,
        [LocalizedAttributes.LocalizedDescriptionAttribute("strHttp")]
        HTTP = 7,
        [LocalizedAttributes.LocalizedDescriptionAttribute("strHttps")]
        HTTPS = 8,
        [LocalizedAttributes.LocalizedDescriptionAttribute("strICA")]
        ICA = 9,
        [LocalizedAttributes.LocalizedDescriptionAttribute("strExtApp")]
        IntApp = 20,
        [LocalizedAttributes.LocalizedDescriptionAttribute("strSerial")]
        Serial = 10,
        [LocalizedAttributes.LocalizedDescriptionAttribute("strTeamViewer")]
        TeamViewer = 11,
        [LocalizedAttributes.LocalizedDescriptionAttribute("strRAdmin")]
        RAdmin=12,
        [Browsable(false)]
        NONE = 999,

        
    }
}