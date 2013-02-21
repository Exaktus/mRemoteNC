using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using AxMSTSCLib;
using EOLWTSCOM;
using Microsoft.VisualBasic;
using mRemoteNC.App;
using mRemoteNC.Connection;
using My;

namespace mRemoteNC
{
    public class RDP : Base
    {
        #region Properties

        public bool SmartSize
        {
            get { return this.RDP_Client.AdvancedSettings4.SmartSizing; }
            set { this.RDP_Client.AdvancedSettings4.SmartSizing = value; }
        }

        public bool Fullscreen
        {
            get { return this.RDP_Client.FullScreen; }
            set { this.RDP_Client.FullScreen = value; }
        }

        #endregion Properties

        #region Private Declarations

        private AxMsRdpClient7NotSafeForScripting RDP_Client;
        private Info Info;
        private Version RDPVersion;

        #endregion Private Declarations

        #region Public Methods

        public RDP()
        {
            this.Control = new AxMsRdpClient7NotSafeForScripting();
        }

        public override bool SetProps()
        {
            base.SetProps();

            try
            {
                RDP_Client = (AxMsRdpClient7NotSafeForScripting)this.Control;
                Info = this.InterfaceControl.Info;

                try
                {
                    RDP_Client.CreateControl();

                    while (!this.RDP_Client.Created)
                    {
                        Thread.Sleep(10);
                        System.Windows.Forms.Application.DoEvents();
                    }
                }
                catch (System.Runtime.InteropServices.COMException comEx)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        Language.strRdpControlCreationFailed + Constants.vbNewLine +
                                                        Constants.vbNewLine + comEx.Message);
                    RDP_Client.Dispose();
                    return false;
                }

                this.RDPVersion = new Version((string)RDP_Client.Version);

                RDP_Client.Server = this.Info.Hostname;

                this.SetCredentials();
                this.SetResolution();
                this.RDP_Client.FullScreenTitle = this.Info.Name;

                //not user changeable
                RDP_Client.AdvancedSettings2.GrabFocusOnConnect = true;
                RDP_Client.AdvancedSettings3.EnableAutoReconnect = true;
                RDP_Client.AdvancedSettings3.MaxReconnectAttempts = Settings.Default.RdpReconnectionCount;
                RDP_Client.AdvancedSettings2.keepAliveInterval = 60000; //in milliseconds (10.000 = 10 seconds)
                RDP_Client.AdvancedSettings5.AuthenticationLevel = 0;
                RDP_Client.AdvancedSettings6.EncryptionEnabled = 1;

                RDP_Client.AdvancedSettings2.overallConnectionTimeout = 20;

                RDP_Client.AdvancedSettings2.BitmapPeristence = Convert.ToInt32(this.Info.CacheBitmaps);
                RDP_Client.AdvancedSettings7.EnableCredSspSupport = Info.UseCredSsp;

                this.SetUseConsoleSession();
                this.SetPort();
                this.SetRedirectKeys();
                this.SetRedirection();
                this.SetAuthenticationLevel();
                this.SetRDGateway();

                RDP_Client.ColorDepth = (int)(this.Info.Colors);

                this.SetPerformanceFlags();

                RDP_Client.ConnectingText = Language.strConnecting;

                Control.Anchor = AnchorStyles.None;

                return true;
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strRdpSetPropsFailed + Constants.vbNewLine + ex.Message,
                                                    true);
                return false;
            }
        }

        public override bool Connect()
        {
            this.SetEventHandlers();

            try
            {
                RDP_Client.Connect();
                base.Connect();
                return true;
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strRdpConnectionOpenFailed + Constants.vbNewLine +
                                                    ex.Message);
            }

            return false;
        }

        public override void Disconnect()
        {
            try
            {
                RDP_Client.Disconnect();
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strRdpDisconnectFailed + Constants.vbNewLine +
                                                    ex.Message, true);
                base.Close();
            }
        }

        public void ToggleFullscreen()
        {
            try
            {
                this.Fullscreen = System.Convert.ToBoolean(!this.Fullscreen);
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strRdpToggleFullscreenFailed + Constants.vbNewLine +
                                                    ex.Message, true);
            }
        }

        public void ToggleSmartSize()
        {
            try
            {
                this.SmartSize = System.Convert.ToBoolean(!this.SmartSize);
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strRdpToggleSmartSizeFailed + Constants.vbNewLine +
                                                    ex.Message, true);
            }
        }

        public override void Focus()
        {
            try
            {
                if (RDP_Client.ContainsFocus == false)
                {
                    RDP_Client.Focus();
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strRdpFocusFailed + Constants.vbNewLine + ex.Message,
                                                    true);
            }
        }

        public override void Resize()
        {
            Control.Location = InterfaceControl.Location;
            Control.Size = InterfaceControl.Size;
            base.Resize();
        }

        #endregion Public Methods

        #region Private Methods

        private void SetRDGateway()
        {
            try
            {
                if (RDP_Client.TransportSettings.GatewayIsSupported == 1)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.InformationMsg,
                                                        Language.strRdpGatewayIsSupported, true);
                    if (Info.RDGatewayUsageMethod != RDGatewayUsageMethod.Never)
                    {
                        RDP_Client.TransportSettings2.GatewayProfileUsageMethod = 1;
                        RDP_Client.TransportSettings.GatewayUsageMethod =
                            Convert.ToUInt16(Info.RDGatewayUsageMethod);
                        RDP_Client.TransportSettings.GatewayHostname = Info.RDGatewayHostname;
                        if (Info.RDGatewayUseConnectionCredentials == RDGatewayUseConnectionCredentials.Yes)
                        {
                            RDP_Client.TransportSettings2.GatewayUsername = Info.Username;
                            RDP_Client.TransportSettings2.GatewayPassword = Info.Password;
                            RDP_Client.TransportSettings2.GatewayDomain = Info.Domain;
                        }
                        else if (Info.RDGatewayUseConnectionCredentials ==
                                 RDGatewayUseConnectionCredentials.SmartCard)
                        {
                            RDP_Client.TransportSettings2.GatewayCredsSource = 1;
                            // TSC_PROXY_CREDS_MODE_SMARTCARD
                            RDP_Client.TransportSettings2.GatewayCredSharing = 0;
                        }
                        else
                        {
                            RDP_Client.TransportSettings2.GatewayUsername = Info.RDGatewayUsername;
                            RDP_Client.TransportSettings2.GatewayPassword = Info.RDGatewayPassword;
                            RDP_Client.TransportSettings2.GatewayDomain = Info.RDGatewayDomain;
                            RDP_Client.TransportSettings2.GatewayCredSharing = 0;
                        }
                    }
                }
                else
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.InformationMsg,
                                                        Language.strRdpGatewayNotSupported, true);
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strRdpSetGatewayFailed + Constants.vbNewLine +
                                                    ex.Message, true);
            }
        }

        private void SetUseConsoleSession()
        {
            try
            {
                if (this.Force == Connection.Info.Force.UseConsoleSession)
                {
                    if (RDPVersion < Versions.RDC61)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.InformationMsg,
                                                            string.Format(Language.strRdpSetConsoleSwitch, "6.0"),
                                                            true);
                        RDP_Client.AdvancedSettings2.ConnectToServerConsole = true;
                    }
                    else
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.InformationMsg,
                                                            string.Format(Language.strRdpSetConsoleSwitch, "6.1"),
                                                            true);
                        RDP_Client.AdvancedSettings7.ConnectToAdministerServer = true;
                    }
                }
                else if (this.Force == Connection.Info.Force.DontUseConsoleSession)
                {
                    if (RDPVersion < Versions.RDC61)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.InformationMsg,
                                                            string.Format(Language.strRdpSetConsoleSwitch, "6.0"),
                                                            true);
                        RDP_Client.AdvancedSettings2.ConnectToServerConsole = false;
                    }
                    else
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.InformationMsg,
                                                            string.Format(Language.strRdpSetConsoleSwitch, "6.1"),
                                                            true);
                        RDP_Client.AdvancedSettings7.ConnectToAdministerServer = false;
                    }
                }
                else
                {
                    if (RDPVersion < Versions.RDC61)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.InformationMsg,
                                                            string.Format(Language.strRdpSetConsoleSwitch, "6.0"),
                                                            true);
                        RDP_Client.AdvancedSettings2.ConnectToServerConsole = this.Info.UseConsoleSession;
                    }
                    else
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.InformationMsg,
                                                            string.Format(Language.strRdpSetConsoleSwitch, "6.1"),
                                                            true);
                        RDP_Client.AdvancedSettings7.ConnectToAdministerServer = this.Info.UseConsoleSession;
                    }
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strRdpSetConsoleSessionFailed + Constants.vbNewLine +
                                                    ex.Message, true);
            }
        }

        private void SetCredentials()
        {
            try
            {
                string _user = (string)this.Info.Username;
                string _pass = (string)this.Info.Password;
                string _dom = (string)this.Info.Domain;

                if (_user == "")
                {
                    if ((string)Settings.Default.EmptyCredentials == "windows")
                    {
                        RDP_Client.UserName = Environment.UserName;
                    }
                    else if ((string)Settings.Default.EmptyCredentials == "custom")
                    {
                        RDP_Client.UserName = Settings.Default.DefaultUsername;
                    }
                }
                else
                {
                    RDP_Client.UserName = _user;
                }

                if (_pass == "")
                {
                    if ((string)Settings.Default.EmptyCredentials == "custom")
                    {
                        if (Settings.Default.DefaultPassword != "")
                        {
                            RDP_Client.AdvancedSettings2.ClearTextPassword =
                                Security.Crypt.Decrypt((string)Settings.Default.DefaultPassword,
                                                       (string)mRemoteNC.AppInfo.General.EncryptionKey);
                        }
                    }
                }
                else
                {
                    RDP_Client.AdvancedSettings2.ClearTextPassword = _pass;
                }

                if (_dom == "")
                {
                    if ((string)Settings.Default.EmptyCredentials == "windows")
                    {
                        RDP_Client.Domain = Environment.UserDomainName;
                    }
                    else if ((string)Settings.Default.EmptyCredentials == "custom")
                    {
                        RDP_Client.Domain = Settings.Default.DefaultDomain;
                    }
                }
                else
                {
                    RDP_Client.Domain = _dom;
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strRdpSetCredentialsFailed + Constants.vbNewLine +
                                                    ex.Message, true);
            }
        }

        private void SetResolution()
        {
            try
            {
                if (this.Force == Connection.Info.Force.Fullscreen)
                {
                    RDP_Client.FullScreen = true;
                    RDP_Client.DesktopWidth = Screen.FromControl(frmMain.defaultInstance).Bounds.Width;
                    RDP_Client.DesktopHeight = Screen.FromControl(frmMain.defaultInstance).Bounds.Height;

                    return;
                }

                if (this.InterfaceControl.Info.Resolution == RDPResolutions.FitToWindow)
                {
                    RDP_Client.DesktopWidth = this.InterfaceControl.Size.Width;
                    RDP_Client.DesktopHeight = this.InterfaceControl.Size.Height;
                }
                else if (this.InterfaceControl.Info.Resolution == RDPResolutions.SmartSize)
                {
                    //ToDo
                    //RDP_Client.AdvancedSettings.SmartSizing = true;
                    RDP_Client.DesktopWidth = this.InterfaceControl.Size.Width;
                    RDP_Client.DesktopHeight = this.InterfaceControl.Size.Height;
                }
                else if (this.InterfaceControl.Info.Resolution == RDPResolutions.Fullscreen)
                {
                    RDP_Client.FullScreen = true;
                    RDP_Client.DesktopWidth = Screen.FromControl(frmMain.defaultInstance).Bounds.Width;
                    RDP_Client.DesktopHeight = Screen.FromControl(frmMain.defaultInstance).Bounds.Height;
                }
                else
                {
                    RDP_Client.DesktopWidth = Resolutions.Items(Conversion.Int(this.Info.Resolution)).Width;
                    RDP_Client.DesktopHeight = Resolutions.Items(Conversion.Int(this.Info.Resolution)).Height;
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strRdpSetResolutionFailed + Constants.vbNewLine +
                                                    ex.Message, true);
            }
        }

        private void SetPort()
        {
            try
            {
                if (this.Info.Port != (int)RDP.Defaults.Port)
                {
                    RDP_Client.AdvancedSettings2.RDPPort = this.Info.Port;
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strRdpSetPortFailed + Constants.vbNewLine + ex.Message,
                                                    true);
            }
        }

        private void SetRedirectKeys()
        {
            try
            {
                if (this.Info.RedirectKeys)
                {
                    RDP_Client.AdvancedSettings2.ContainerHandledFullScreen = 1;
                    RDP_Client.AdvancedSettings2.DisplayConnectionBar = false;
                    RDP_Client.AdvancedSettings2.PinConnectionBar = false;
                    RDP_Client.FullScreen = true;
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strRdpSetRedirectKeysFailed + Constants.vbNewLine +
                                                    ex.Message, true);
            }
        }

        private void SetRedirection()
        {
            try
            {
                RDP_Client.AdvancedSettings2.RedirectDrives = this.Info.RedirectDiskDrives;
                RDP_Client.AdvancedSettings2.RedirectPorts = this.Info.RedirectPorts;
                RDP_Client.AdvancedSettings2.RedirectPrinters = this.Info.RedirectPrinters;
                RDP_Client.AdvancedSettings2.RedirectSmartCards = this.Info.RedirectSmartCards;
                RDP_Client.SecuredSettings2.AudioRedirectionMode = (int)this.Info.RedirectSound;
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strRdpSetRedirectionFailed + Constants.vbNewLine +
                                                    ex.Message, true);
            }
        }

        private void SetPerformanceFlags()
        {
            try
            {
                int pFlags = 0;
                if (this.Info.DisplayThemes == false)
                {
                    pFlags +=
                        System.Convert.ToInt32(
                            Conversion.Int(RDP.RDPPerformanceFlags.DisableThemes));
                }

                if (this.Info.DisplayWallpaper == false)
                {
                    pFlags +=
                        System.Convert.ToInt32(
                            Conversion.Int(RDP.RDPPerformanceFlags.DisableWallpaper));
                }

                if (this.Info.EnableFontSmoothing)
                {
                    pFlags +=
                        System.Convert.ToInt32(
                            Conversion.Int(RDP.RDPPerformanceFlags.EnableFontSmoothing));
                }

                if (this.Info.EnableDesktopComposition)
                {
                    pFlags +=
                        System.Convert.ToInt32(
                            Conversion.Int(RDP.RDPPerformanceFlags.EnableDesktopComposition));
                }

                RDP_Client.AdvancedSettings2.PerformanceFlags = pFlags;
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strRdpSetPerformanceFlagsFailed + Constants.vbNewLine +
                                                    ex.Message, true);
            }
        }

        private void SetAuthenticationLevel()
        {
            try
            {
                RDP_Client.AdvancedSettings5.AuthenticationLevel = (uint)this.Info.RDPAuthenticationLevel;
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strRdpSetAuthenticationLevelFailed +
                                                    Constants.vbNewLine + ex.Message, true);
            }
        }

        private void SetEventHandlers()
        {
            try
            {
                RDP_Client.OnConnecting += new System.EventHandler(RDPEvent_OnConnecting);
                RDP_Client.OnConnected += new System.EventHandler(RDPEvent_OnConnected);
                RDP_Client.OnFatalError += RDPEvent_OnFatalError;
                RDP_Client.OnDisconnected += RDPEvent_OnDisconnected;
                RDP_Client.OnLeaveFullScreenMode += new System.EventHandler(RDPEvent_OnLeaveFullscreenMode);
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strRdpSetEventHandlersFailed + Constants.vbNewLine +
                                                    ex.Message, true);
            }
        }

        #endregion Private Methods

        #region Private Events & Handlers

        private void RDPEvent_OnFatalError(object sender, AxMSTSCLib.IMsTscAxEvents_OnFatalErrorEvent e)
        {
            base.Event_ErrorOccured(this, e.errorCode.ToString());
        }
        
        private void RDPEvent_OnDisconnected(object sender, IMsTscAxEvents_OnDisconnectedEvent e)
        {
            const int UI_ERR_NORMAL_DISCONNECT = 0xb08;
            if (e.discReason != UI_ERR_NORMAL_DISCONNECT)
            {
                string reason = RDP_Client.GetErrorDescription((uint)e.discReason, (uint)RDP_Client.ExtendedDisconnectReason);
                Event_Disconnected(this, e.discReason + Constants.vbCrLf + reason);
            }

            if (Settings.Default.ReconnectOnDisconnect)
            {
                ReconnectGroup = new ReconnectGroup();
                ReconnectGroup.Left = (Control.Width / 2) - (ReconnectGroup.Width / 2);
                ReconnectGroup.Top = (Control.Height / 2) - (ReconnectGroup.Height / 2);
                ReconnectGroup.Parent = Control;
                ReconnectGroup.Show();
                tmrReconnect.Enabled = true;
            }
            else
            {
                Close();
            }
        }

        private void RDPEvent_OnConnecting(object sender, System.EventArgs e)
        {
            base.Event_Connecting(this);
        }

        private void RDPEvent_OnConnected(object sender, System.EventArgs e)
        {
            base.Event_Connected(this);
        }

        private void RDPEvent_OnLeaveFullscreenMode(object sender, System.EventArgs e)
        {
            if (LeaveFullscreenEvent != null)
                LeaveFullscreenEvent(this, e);
        }

        #endregion Private Events & Handlers

        #region Public Events & Handlers

        public delegate void LeaveFullscreenEventHandler(RDP sender, System.EventArgs e);

        private LeaveFullscreenEventHandler LeaveFullscreenEvent;

        public event LeaveFullscreenEventHandler LeaveFullscreen
        {
            add
            {
                LeaveFullscreenEvent =
                    (LeaveFullscreenEventHandler)System.Delegate.Combine(LeaveFullscreenEvent, value);
            }
            remove
            {
                LeaveFullscreenEvent =
                    (LeaveFullscreenEventHandler)System.Delegate.Remove(LeaveFullscreenEvent, value);
            }
        }

        #endregion Public Events & Handlers

        #region Enums

        public enum Defaults
        {
            Colors = RDPColors.Colors16Bit,
            Sounds = RDPSounds.DoNotPlay,
            Resolution = RDPResolutions.FitToWindow,
            Port = 3389
        }

        public enum RDPColors
        {
            [LocalizedAttributes.LocalizedDescriptionAttribute("strRDP256Colors")]
            Colors256 = 8,
            [LocalizedAttributes.LocalizedDescriptionAttribute("strRDP32768Colors")]
            Colors15Bit = 15,
            [LocalizedAttributes.LocalizedDescriptionAttribute("strRDP65536Colors")]
            Colors16Bit = 16,
            [LocalizedAttributes.LocalizedDescriptionAttribute("strRDP16777216Colors")]
            Colors24Bit = 24,
            [LocalizedAttributes.LocalizedDescriptionAttribute("strRDP4294967296Colors")]
            Colors32Bit = 32
        }

        public enum RDPSounds
        {
            [LocalizedAttributes.LocalizedDescriptionAttribute("strRDPSoundBringToThisComputer")]
            BringToThisComputer = 0,
            [LocalizedAttributes.LocalizedDescriptionAttribute("strRDPSoundLeaveAtRemoteComputer")]
            LeaveAtRemoteComputer = 1,
            [LocalizedAttributes.LocalizedDescriptionAttribute("strRDPSoundDoNotPlay")]
            DoNotPlay = 2
        }

        private enum RDPPerformanceFlags
        {
            [Description("strRDPDisableWallpaper")]
            DisableWallpaper = 0x1,
            [Description("strRDPDisableFullWindowdrag")]
            DisableFullWindowDrag = 0x2,
            [Description("strRDPDisableMenuAnimations")]
            DisableMenuAnimations = 0x4,
            [Description("strRDPDisableThemes")]
            DisableThemes = 0x8,
            [Description("strRDPDisableCursorShadow")]
            DisableCursorShadow = 0x20,
            [Description("strRDPDisableCursorblinking")]
            DisableCursorBlinking = 0x40,
            [Description("strRDPEnableFontSmoothing")]
            EnableFontSmoothing = 0x80,
            [Description("strRDPEnableDesktopComposition")]
            EnableDesktopComposition = 0x100
        }

        public enum RDPResolutions
        {
            [LocalizedAttributes.LocalizedDescriptionAttribute("strRDPFitToPanel")]
            FitToWindow,
            [LocalizedAttributes.LocalizedDescriptionAttribute("strFullscreen")]
            Fullscreen,
            [LocalizedAttributes.LocalizedDescriptionAttribute("strRDPSmartSize")]
            SmartSize,
            [Description("640x480")]
            Res640x480,
            [Description("800x600")]
            Res800x600,
            [Description("1024x768")]
            Res1024x768,
            [Description("1152x864")]
            Res1152x864,
            [Description("1280x800")]
            Res1280x800,
            [Description("1280x1024")]
            Res1280x1024,
            [Description("1400x1050")]
            Res1400x1050,
            [Description("1440x900")]
            Res1440x900,
            [Description("1600x1024")]
            Res1600x1024,
            [Description("1600x1200")]
            Res1600x1200,
            [Description("1600x1280")]
            Res1600x1280,
            [Description("1680x1050")]
            Res1680x1050,
            [Description("1900x1200")]
            Res1900x1200,
            [Description("1920x1200")]
            Res1920x1200,
            [Description("2048x1536")]
            Res2048x1536,
            [Description("2560x2048")]
            Res2560x2048,
            [Description("3200x2400")]
            Res3200x2400,
            [Description("3840x2400")]
            Res3840x2400
        }

        public enum AuthenticationLevel
        {
            [LocalizedAttributes.LocalizedDescriptionAttribute("strAlwaysConnectEvenIfAuthFails")]
            NoAuth = 0,
            [LocalizedAttributes.LocalizedDescriptionAttribute("strDontConnectWhenAuthFails")]
            AuthRequired = 1,
            [LocalizedAttributes.LocalizedDescriptionAttribute("strWarnIfAuthFails")]
            WarnOnFailedAuth = 2
        }

        public enum RDGatewayUsageMethod
        {
            [LocalizedAttributes.LocalizedDescriptionAttribute("strNever")]
            Never = 0, // TSC_PROXY_MODE_NONE_DIRECT
            [LocalizedAttributes.LocalizedDescriptionAttribute("strAlways")]
            Always = 1, // TSC_PROXY_MODE_DIRECT
            [LocalizedAttributes.LocalizedDescriptionAttribute("strDetect")]
            Detect = 2 // TSC_PROXY_MODE_DETECT
        }

        public enum RDGatewayUseConnectionCredentials
        {
            [LocalizedAttributes.LocalizedDescriptionAttribute("strUseDifferentUsernameAndPassword")]
            No = 0,
            [LocalizedAttributes.LocalizedDescriptionAttribute("strUseSameUsernameAndPassword")]
            Yes = 1,
            [LocalizedAttributes.LocalizedDescriptionAttribute("strUseSmartCard")]
            SmartCard = 2
        }

        #endregion Enums

        #region Resolution

        public class Resolution
        {
            private int _Width;

            public int Width
            {
                get { return this._Width; }
                set { this._Width = value; }
            }

            private int _Height;

            public int Height
            {
                get { return this._Height; }
                set { this._Height = value; }
            }

            public Resolution(int Width, int Height)
            {
                this._Width = Width;
                this._Height = Height;
            }
        }

        public class Resolutions
        {
            public static Collection List = new Collection();

            public static void AddResolutions()
            {
                try
                {
                    foreach (RDPResolutions RDPResolution in Enum.GetValues(typeof(RDPResolutions)))
                    {
                        if (RDPResolution == RDPResolutions.FitToWindow || RDPResolution == RDPResolutions.SmartSize ||
                            RDPResolution == RDPResolutions.Fullscreen)
                        {
                            Resolutions.Add(new Resolution(0, 0));
                        }
                        else
                        {
                            string[] ResSize = (Enum.GetName(typeof(RDPResolutions), RDPResolution)).Split('x');
                            string ResWidth = (string)(ResSize[0].Substring(3));
                            string ResHeight = ResSize[1];
                            Resolutions.Add(new Resolution(Convert.ToInt32(ResWidth), Convert.ToInt32(ResHeight)));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        Language.strRdpAddResolutionsFailed + Constants.vbNewLine +
                                                        ex.Message, true);
                }
            }

            public static Resolution Items(object Index)
            {
                if (Index is Resolution)
                {
                    return (Resolution)Index;
                }
                else
                {
                    return ((Resolution)(List[(int)Index + 1]));
                }
            }

            public static int ItemsCount
            {
                get { return List.Count; }
            }

            public static Resolution Add(Resolution nRes)
            {
                try
                {
                    List.Add(nRes, null, null, null);

                    return nRes;
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        Language.strRdpAddResolutionFailed + Constants.vbNewLine +
                                                        ex.Message, true);
                }

                return null;
            }
        }

        #endregion Resolution

        public class Versions
        {
            public static Version RDC60 = new Version(6, 0, 6000);
            public static Version RDC61 = new Version(6, 0, 6001);
        }

        #region Terminal Sessions

        public class TerminalSessions
        {
            private WTSCOM oWTSCOM = new WTSCOM();
            private WTSSessions oWTSSessions = new WTSSessions();

            public long ServerHandle;

            public bool OpenConnection(string SrvName)
            {
                try
                {
                    ServerHandle = oWTSCOM.WTSOpenServer(SrvName);

                    if (ServerHandle != 0)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        Language.strRdpOpenConnectionFailed + Constants.vbNewLine +
                                                        ex.Message, true);
                }

                return false;
            }

            public void CloseConnection(long SrvHandle)
            {
                try
                {
                    oWTSCOM.WTSCloseServer((int)ServerHandle);
                    ServerHandle = 0;
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        Language.strRdpCloseConnectionFailed + Constants.vbNewLine +
                                                        ex.Message, true);
                }
            }

            public Sessions GetSessions()
            {
                Sessions colSessions = new Sessions();

                try
                {
                    oWTSSessions = oWTSCOM.WTSEnumerateSessions((int)ServerHandle);

                    long SessionID;
                    string SessionUser;
                    long SessionState;
                    string SessionName;

                    foreach (WTSSession oWTSSession in oWTSSessions)
                    {
                        SessionID = oWTSSession.SessionId;
                        SessionUser = oWTSCOM.WTSQuerySessionInformation((int)ServerHandle, oWTSSession.SessionId,
                                                                         5); //WFUsername = 5
                        SessionState = oWTSSession.State;
                        SessionName = oWTSSession.WinStationName + "\r\n";

                        if (SessionUser != "")
                        {
                            if (SessionState == 0)
                            {
                                colSessions.Add(SessionID, Language.strActive, SessionUser, SessionName);
                            }
                            else
                            {
                                colSessions.Add(SessionID, Language.strInactive, SessionUser, SessionName);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        Language.strRdpGetSessionsFailed + Constants.vbNewLine +
                                                        ex.Message, true);
                }

                return colSessions;
            }

            public bool KillSession(long SessionID)
            {
                return oWTSCOM.WTSLogoffSession((int)ServerHandle, (int)SessionID, true);
            }
        }

        public class Sessions : CollectionBase
        {
            public Session this[int Index]
            {
                get { return ((Session)(List[Index])); }
            }

            public int ItemsCount
            {
                get { return List.Count; }
            }

            public Session Add(long SessionID, string SessionState, string SessionUser, string SessionName)
            {
                Session newSes = new Session();

                try
                {
                    newSes.SessionID = SessionID;
                    newSes.SessionState = SessionState;
                    newSes.SessionUser = SessionUser;
                    newSes.SessionName = SessionName;

                    List.Add(newSes);
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        Language.strRdpAddSessionFailed + Constants.vbNewLine +
                                                        ex.Message, true);
                }

                return newSes;
            }

            public void ClearSessions()
            {
                List.Clear();
            }
        }

        public class Session : CollectionBase
        {
            private long lngSessionID;

            public long SessionID
            {
                get { return lngSessionID; }
                set { lngSessionID = value; }
            }

            private string lngSessionState;

            public string SessionState
            {
                get { return lngSessionState; }
                set { lngSessionState = value; }
            }

            private string strSessionUser;

            public string SessionUser
            {
                get { return strSessionUser; }
                set { strSessionUser = value; }
            }

            private string strSessionName;

            public string SessionName
            {
                get { return strSessionName; }
                set { strSessionName = value; }
            }
        }

        #endregion Terminal Sessions

        #region Fatal Errors

        public class FatalErrors
        {
            public FatalErrors()
            {
                dic.Add("0", Language.strRdpErrorUnknown);
                dic.Add("1", Language.strRdpErrorCode1);
                dic.Add("2", Language.strRdpErrorOutOfMemory);
                dic.Add("3", Language.strRdpErrorWindowCreation);
                dic.Add("4", Language.strRdpErrorCode2);
                dic.Add("5", Language.strRdpErrorCode3);
                dic.Add("6", Language.strRdpErrorCode4);
                dic.Add("7", Language.strRdpErrorConnection);
                dic.Add("100", Language.strRdpErrorWinsock);
            }

            //protected static string[] _description= new string[] {0 == Language.strRdpErrorUnknown, 1 == Language.strRdpErrorCode1, 2 == Language.strRdpErrorOutOfMemory, 3 == Language.strRdpErrorWindowCreation, 4 == Language.strRdpErrorCode2, 5 == Language.strRdpErrorCode3, 6 == Language.strRdpErrorCode4, 7 == Language.strRdpErrorConnection, 100 == Language.strRdpErrorWinsock} ;
            private static Dictionary<string, string> dic = new Dictionary<string, string>();

            public static string GetError(string id)
            {
                try
                {
                    return (dic[id]);
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        Language.strRdpErrorGetFailure + Constants.vbNewLine +
                                                        ex.Message, true);
                    return string.Format(Language.strRdpErrorUnknown, id);
                }
            }
        }

        #endregion Fatal Errors

        #region Reconnect Stuff

        private void tmrReconnect_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            bool srvReady =
                System.Convert.ToBoolean(Tools.PortScan.Scanner.IsPortOpen(Info.Hostname, Info.Port.ToString()));

            ReconnectGroup.ServerReady = srvReady;

            if (ReconnectGroup.ReconnectWhenReady && srvReady)
            {
                tmrReconnect.Enabled = false;
                ReconnectGroup.DisposeReconnectGroup();
                //SetProps()
                RDP_Client.Connect();
            }
        }

        #endregion Reconnect Stuff
    }
}