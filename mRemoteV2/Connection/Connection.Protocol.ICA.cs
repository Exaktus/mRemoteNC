using System;
using System.Threading;
using System.Windows.Forms;
using AxWFICALib;
using Microsoft.VisualBasic;
using mRemoteNC.App;
using mRemoteNC.Connection;
using My;

namespace mRemoteNC
{
    public class ICA : Base
    {
        #region Private Properties

        private AxICAClient ICA_Client;
        private Info Info;

        #endregion Private Properties

        #region Public Methods

        public ICA()
        {
            try
            {
                this.Control = new AxICAClient();
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strIcaControlFailed + Constants.vbNewLine + ex.Message,
                                                    true);
            }
        }

        public override bool SetProps()
        {
            base.SetProps();

            try
            {
                ICA_Client = (AxICAClient)this.Control;
                Info = this.InterfaceControl.Info;

                ICA_Client.CreateControl();

                while (!this.ICA_Client.Created)
                {
                    Thread.Sleep(10);
                    System.Windows.Forms.Application.DoEvents();
                }

                ICA_Client.Address = Info.Hostname;

                this.SetCredentials();

                this.SetResolution();
                this.SetColors();

                this.SetSecurity();

                //Disable hotkeys for international users
                ICA_Client.Hotkey1Shift = null;
                ICA_Client.Hotkey1Char = null;
                ICA_Client.Hotkey2Shift = null;
                ICA_Client.Hotkey2Char = null;
                ICA_Client.Hotkey3Shift = null;
                ICA_Client.Hotkey3Char = null;
                ICA_Client.Hotkey4Shift = null;
                ICA_Client.Hotkey4Char = null;
                ICA_Client.Hotkey5Shift = null;
                ICA_Client.Hotkey5Char = null;
                ICA_Client.Hotkey6Shift = null;
                ICA_Client.Hotkey6Char = null;
                ICA_Client.Hotkey7Shift = null;
                ICA_Client.Hotkey7Char = null;
                ICA_Client.Hotkey8Shift = null;
                ICA_Client.Hotkey8Char = null;
                ICA_Client.Hotkey9Shift = null;
                ICA_Client.Hotkey9Char = null;
                ICA_Client.Hotkey10Shift = null;
                ICA_Client.Hotkey10Char = null;
                ICA_Client.Hotkey11Shift = null;
                ICA_Client.Hotkey11Char = null;

                ICA_Client.PersistentCacheEnabled = Info.CacheBitmaps;

                ICA_Client.Title = Info.Name;

                return true;
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strIcaSetPropsFailed + Constants.vbNewLine + ex.Message,
                                                    true);
                return false;
            }
        }

        public override bool Connect()
        {
            this.SetEventHandlers();

            try
            {
                ICA_Client.Connect();
                base.Connect();
                return true;
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strIcaConnectionFailed + Constants.vbNewLine +
                                                    ex.Message);
                return false;
            }
        }

        #endregion Public Methods

        #region Private Methods

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
                        ICA_Client.Username = Environment.UserName;
                    }
                    else if ((string)Settings.Default.EmptyCredentials == "custom")
                    {
                        ICA_Client.Username = Settings.Default.DefaultUsername;
                    }
                }
                else
                {
                    ICA_Client.Username = _user;
                }

                if (_pass == "")
                {
                    if ((string)Settings.Default.EmptyCredentials == "custom")
                    {
                        if (Settings.Default.DefaultPassword != "")
                        {
                            ICA_Client.SetProp("ClearPassword",
                                                Security.Crypt.Decrypt((string)Settings.Default.DefaultPassword,
                                                                       (string)
                                                                       mRemoteNC.App.Info.General.EncryptionKey));
                        }
                    }
                }
                else
                {
                    ICA_Client.SetProp("ClearPassword", _pass);
                }

                if (_dom == "")
                {
                    if ((string)Settings.Default.EmptyCredentials == "windows")
                    {
                        ICA_Client.Domain = Environment.UserDomainName;
                    }
                    else if ((string)Settings.Default.EmptyCredentials == "custom")
                    {
                        ICA_Client.Domain = Settings.Default.DefaultDomain;
                    }
                }
                else
                {
                    ICA_Client.Domain = _dom;
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strIcaSetCredentialsFailed + Constants.vbNewLine +
                                                    ex.Message, true);
            }
        }

        private void SetResolution()
        {
            try
            {
                if (this.Force == Connection.Info.Force.Fullscreen) //Fixme
                {
                    ICA_Client.SetWindowSize(WFICALib.ICAWindowType.WindowTypeClient,
                                              Screen.FromControl(frmMain.defaultInstance).Bounds.Width,
                                              Screen.FromControl(frmMain.defaultInstance).Bounds.Height, 0);
                    ICA_Client.FullScreenWindow();

                    return;
                }

                if (this.InterfaceControl.Info.Resolution == RDP.RDPResolutions.FitToWindow)
                {
                    ICA_Client.SetWindowSize(WFICALib.ICAWindowType.WindowTypeClient,
                                              this.InterfaceControl.Size.Width, this.InterfaceControl.Size.Height, 0);
                }
                else if (this.InterfaceControl.Info.Resolution == RDP.RDPResolutions.SmartSize)
                {
                    ICA_Client.SetWindowSize(WFICALib.ICAWindowType.WindowTypeClient,
                                              this.InterfaceControl.Size.Width, this.InterfaceControl.Size.Height, 0);
                }
                else if (this.InterfaceControl.Info.Resolution == RDP.RDPResolutions.Fullscreen)
                {
                    ICA_Client.SetWindowSize(WFICALib.ICAWindowType.WindowTypeClient,
                                              Screen.FromControl(frmMain.defaultInstance).Bounds.Width,
                                              Screen.FromControl(frmMain.defaultInstance).Bounds.Height, 0);
                    ICA_Client.FullScreenWindow();
                }
                else
                {
                    ICA_Client.SetWindowSize(WFICALib.ICAWindowType.WindowTypeClient,
                                              RDP.Resolutions.Items(Conversion.Int(Info.Resolution)).Width,
                                              RDP.Resolutions.Items(Conversion.Int(Info.Resolution)).Height, 0);
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strIcaSetResolutionFailed + Constants.vbNewLine +
                                                    ex.Message, true);
            }
        }

        private void SetColors()
        {
            if (Info.Colors == RDP.RDPColors.Colors256)
            {
                ICA_Client.SetProp("DesiredColor", "2");
            }
            else if (Info.Colors == RDP.RDPColors.Colors15Bit)
            {
                ICA_Client.SetProp("DesiredColor", "4");
            }
            else if (Info.Colors == RDP.RDPColors.Colors16Bit)
            {
                ICA_Client.SetProp("DesiredColor", "4");
            }
            else
            {
                ICA_Client.SetProp("DesiredColor", "8");
            }
        }

        private void SetSecurity()
        {
            if (Info.ICAEncryption == EncryptionStrength.Encr128BitLogonOnly)
            {
                ICA_Client.Encrypt = true;
                ICA_Client.EncryptionLevelSession = "EncRC5-0";
            }
            else if (Info.ICAEncryption == EncryptionStrength.Encr40Bit)
            {
                ICA_Client.Encrypt = true;
                ICA_Client.EncryptionLevelSession = "EncRC5-40";
            }
            else if (Info.ICAEncryption == EncryptionStrength.Encr56Bit)
            {
                ICA_Client.Encrypt = true;
                ICA_Client.EncryptionLevelSession = "EncRC5-56";
            }
            else if (Info.ICAEncryption == EncryptionStrength.Encr128Bit)
            {
                ICA_Client.Encrypt = true;
                ICA_Client.EncryptionLevelSession = "EncRC5-128";
            }
        }

        private void SetEventHandlers()
        {
            try
            {
                ICA_Client.OnConnecting += new System.EventHandler(ICAEvent_OnConnecting);
                ICA_Client.OnConnect += new System.EventHandler(ICAEvent_OnConnected);
                ICA_Client.OnConnectFailed += new System.EventHandler(ICAEvent_OnConnectFailed);
                ICA_Client.OnDisconnect += new System.EventHandler(ICAEvent_OnDisconnect);
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strIcaSetEventHandlersFailed + Constants.vbNewLine +
                                                    ex.Message, true);
            }
        }

        #endregion Private Methods

        #region Private Events & Handlers

        private void ICAEvent_OnConnecting(object sender, System.EventArgs e)
        {
            base.Event_Connecting(this);
        }

        private void ICAEvent_OnConnected(object sender, System.EventArgs e)
        {
            base.Event_Connected(this);
        }

        private void ICAEvent_OnConnectFailed(object sender, System.EventArgs e)
        {
            base.Event_ErrorOccured(this, e.ToString());
        }

        private void ICAEvent_OnDisconnect(object sender, System.EventArgs e)
        {
            base.Event_Disconnected(this, e.ToString());
            if (Settings.Default.ReconnectOnDisconnect)
            {
                this.ReconnectGroup = new ReconnectGroup();
                this.ReconnectGroup.Left =
                    (int)
                    Math.Round(
                        (double)((((double)this.Control.Width) / 2.0) - (((double)this.ReconnectGroup.Width) / 2.0)));
                this.ReconnectGroup.Top =
                    (int)
                    Math.Round(
                        (double)
                        ((((double)this.Control.Height) / 2.0) - (((double)this.ReconnectGroup.Height) / 2.0)));
                this.ReconnectGroup.Parent = this.Control;
                this.ReconnectGroup.Show();
                this.tmrReconnect.Enabled = true;
            }
            else
            {
                base.Close();
            }
        }

        #endregion Private Events & Handlers

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
                ICA_Client.Connect();
            }
        }

        #endregion Reconnect Stuff

        #region Enums

        public enum Defaults
        {
            Port = 1494,
            EncryptionStrength = 0
        }

        public enum EncryptionStrength
        {
            [LocalizedAttributes.LocalizedDescriptionAttribute("strEncBasic")]
            EncrBasic = 1,
            [LocalizedAttributes.LocalizedDescriptionAttribute("strEnc128BitLogonOnly")]
            Encr128BitLogonOnly = 127,
            [LocalizedAttributes.LocalizedDescriptionAttribute("strEnc40Bit")]
            Encr40Bit = 40,
            [LocalizedAttributes.LocalizedDescriptionAttribute("strEnc56Bit")]
            Encr56Bit = 56,
            [LocalizedAttributes.LocalizedDescriptionAttribute("strEnc128Bit")]
            Encr128Bit = 128
        }

        #endregion Enums
    }
}