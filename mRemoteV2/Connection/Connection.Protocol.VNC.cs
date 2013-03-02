using System;
using System.Collections;
using System.Collections.Generic;

//using mRemoteNC.Runtime;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using AxWFICALib;
using Microsoft.VisualBasic;
using mRemoteNC.App;
using mRemoteNC.Connection;
using My;

namespace mRemoteNC
{
    public class VNC : Base
    {
        #region Properties

        public bool SmartSize
        {
            get { return VNC_Client.Scaled; }
            set { VNC_Client.Scaled = value; }
        }

        public bool ViewOnly
        {
            get { return VNC_Client.ViewOnly; }
            set { VNC_Client.ViewOnly = value; }
        }

        #endregion Properties

        #region Private Declarations

        private VncSharp.RemoteDesktop VNC_Client;
        private Info Info;

        #endregion Private Declarations

        #region Public Methods

        public VNC()
        {
            this.Control = new VncSharp.RemoteDesktop();
        }

        public override bool SetProps()
        {
            base.SetProps();

            try
            {
                VNC_Client = (VncSharp.RemoteDesktop)this.Control;

                Info = this.InterfaceControl.Info;

                VNC_Client.VncPort = System.Convert.ToInt32(this.Info.Port);

                //If Info.VNCCompression <> Compression.CompNone Then
                //    VNC.JPEGCompression = True
                //    VNC.JPEGCompressionLevel = Info.VNCCompression
                //End If

                //Select Case Info.VNCEncoding
                //    Case Encoding.EncCorre
                //        VNC.Encoding = ViewerX.VNCEncoding.RFB_CORRE
                //    Case Encoding.EncHextile
                //        VNC.Encoding = ViewerX.VNCEncoding.RFB_HEXTILE
                //    Case Encoding.EncRaw
                //        VNC.Encoding = ViewerX.VNCEncoding.RFB_RAW
                //    Case Encoding.EncRRE
                //        VNC.Encoding = ViewerX.VNCEncoding.RFB_RRE
                //    Case Encoding.EncTight
                //        VNC.Encoding = ViewerX.VNCEncoding.RFB_TIGHT
                //    Case Encoding.EncZlib
                //        VNC.Encoding = ViewerX.VNCEncoding.RFB_ZLIB
                //    Case Encoding.EncZLibHex
                //        VNC.Encoding = ViewerX.VNCEncoding.RFB_ZLIBHEX
                //    Case Encoding.EncZRLE
                //        VNC.Encoding = ViewerX.VNCEncoding.RFB_ZRLE
                //End Select

                //If Info.VNCAuthMode = AuthMode.AuthWin Then
                //    VNC.LoginType = ViewerX.ViewerLoginType.VLT_MSWIN
                //    VNC.MsUser = Me.Info.Username
                //    VNC.MsDomain = Me.Info.Domain
                //    VNC.MsPassword = Me.Info.Password
                //Else
                //    VNC.LoginType = ViewerX.ViewerLoginType.VLT_VNC
                //    VNC.Password = Me.Info.Password
                //End If

                //Select Case Info.VNCProxyType
                //    Case ProxyType.ProxyNone
                //        VNC.ProxyType = ViewerX.ConnectionProxyType.VPT_NONE
                //    Case ProxyType.ProxyHTTP
                //        VNC.ProxyType = ViewerX.ConnectionProxyType.VPT_HTTP
                //    Case ProxyType.ProxySocks5
                //        VNC.ProxyType = ViewerX.ConnectionProxyType.VPT_SOCKS5
                //    Case ProxyType.ProxyUltra
                //        VNC.ProxyType = ViewerX.ConnectionProxyType.VPT_ULTRA_REPEATER
                //End Select

                //If Info.VNCProxyType <> ProxyType.ProxyNone Then
                //    VNC.ProxyIP = Info.VNCProxyIP
                //    VNC.ProxyPort = Info.VNCProxyPort
                //    VNC.ProxyUser = Info.VNCProxyUsername
                //    VNC.ProxyPassword = Info.VNCProxyPassword
                //End If

                //If Info.VNCColors = Colors.Col8Bit Then
                //    VNC.RestrictPixel = True
                //Else
                //    VNC.RestrictPixel = False
                //End If

                //VNC.ConnectingText = Language.strInheritConnecting & " (SmartCode VNC viewer)"
                //VNC.DisconnectedText = Language.strInheritDisconnected
                //VNC.MessageBoxes = False
                //VNC.EndInit()

                return true;
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strVncSetPropsFailed + Constants.vbNewLine + ex.Message,
                                                    true);
                return false;
            }
        }

        public override bool Connect()
        {
            this.SetEventHandlers();

            try
            {
                VNC_Client.Connect((string)this.Info.Hostname, System.Convert.ToBoolean(this.Info.VNCViewOnly),
                                    System.Convert.ToBoolean(Info.VNCSmartSizeMode != SmartSizeMode.SmartSNo));
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strVncConnectionOpenFailed + Constants.vbNewLine +
                                                    ex.Message);
                return false;
            }

            return true;
        }

        public override void Disconnect()
        {
            try
            {
                VNC_Client.Disconnect();
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strVncConnectionDisconnectFailed + Constants.vbNewLine +
                                                    ex.Message, true);
            }
        }

        public void SendSpecialKeys(SpecialKeys Keys)
        {
            try
            {
                switch (Keys)
                {
                    case SpecialKeys.CtrlAltDel:
                        VNC_Client.SendSpecialKeys(VncSharp.SpecialKeys.CtrlAltDel);
                        break;
                    case SpecialKeys.CtrlEsc:
                        VNC_Client.SendSpecialKeys(VncSharp.SpecialKeys.CtrlEsc);
                        break;
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strVncSendSpecialKeysFailed + Constants.vbNewLine +
                                                    ex.Message, true);
            }
        }

        public void ToggleSmartSize()
        {
            try
            {
                SmartSize = System.Convert.ToBoolean(!SmartSize);
                RefreshScreen();
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strVncToggleSmartSizeFailed + Constants.vbNewLine +
                                                    ex.Message, true);
            }
        }

        public void ToggleViewOnly()
        {
            try
            {
                ViewOnly = System.Convert.ToBoolean(!ViewOnly);
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strVncToggleViewOnlyFailed + Constants.vbNewLine +
                                                    ex.Message, true);
            }
        }

        public void StartChat()
        {
            try
            {
                //If VNC.Capabilities.Chat = True Then
                //    VNC.OpenChat()
                //Else
                //    mC.AddMessage(Messages.MessageClass.InformationMsg, "VNC Server doesn't support chat")
                //End If
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strVncStartChatFailed + Constants.vbNewLine +
                                                    ex.Message, true);
            }
        }

        public void StartFileTransfer()
        {
            try
            {
                //If VNC.Capabilities.FileTransfer = True Then
                //    VNC.OpenFileTransfer()
                //Else
                //    mC.AddMessage(Messages.MessageClass.InformationMsg, "VNC Server doesn't support file transfers")
                //End If
            }
            catch (Exception)
            {
            }
        }

        public void RefreshScreen()
        {
            try
            {
                VNC_Client.FullScreenUpdate();
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strVncRefreshFailed + Constants.vbNewLine + ex.Message,
                                                    true);
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void SetEventHandlers()
        {
            try
            {
                VNC_Client.ConnectComplete += new VncSharp.ConnectCompleteHandler(VNCEvent_Connected);
                VNC_Client.ConnectionLost += new System.EventHandler(VNCEvent_Disconnected);
                frmMain.clipboardchange += new frmMain.clipboardchangeEventHandler(VNCEvent_ClipboardChanged);
                if (!string.IsNullOrEmpty((string)Info.Password))
                {
                    VNC_Client.GetPassword = VNCEvent_Authenticate;
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strVncSetEventHandlersFailed + Constants.vbNewLine +
                                                    ex.Message, true);
            }
        }

        #endregion Private Methods

        #region Private Events & Handlers

        private void VNCEvent_Connected(object sender, EventArgs e)
        {
            base.Event_Connected(this);
            VNC_Client.AutoScroll = System.Convert.ToBoolean(Info.VNCSmartSizeMode == SmartSizeMode.SmartSNo);
        }

        private void VNCEvent_Disconnected(object sender, EventArgs e)
        {
            base.Event_Disconnected(sender, e.ToString());
            base.Close();
        }

        private void VNCEvent_ClipboardChanged()
        {
            if (VNC_Client != null) VNC_Client.FillServerClipboard();
        }

        private string VNCEvent_Authenticate()
        {
            return Info.Password;
        }

        #endregion Private Events & Handlers

        #region Enums

        public enum Defaults
        {
            Port = 5900
        }

        public enum SpecialKeys
        {
            CtrlAltDel,
            CtrlEsc
        }

        public enum Compression
        {
            [LocalizedAttributes.LocalizedDescriptionAttribute("strNoCompression")]
            CompNone = 99,
            [Description("0")]
            Comp0 = 0,
            [Description("1")]
            Comp1 = 1,
            [Description("2")]
            Comp2 = 2,
            [Description("3")]
            Comp3 = 3,
            [Description("4")]
            Comp4 = 4,
            [Description("5")]
            Comp5 = 5,
            [Description("6")]
            Comp6 = 6,
            [Description("7")]
            Comp7 = 7,
            [Description("8")]
            Comp8 = 8,
            [Description("9")]
            Comp9 = 9
        }

        public enum Encoding
        {
            [Description("Raw")]
            EncRaw,
            [Description("RRE")]
            EncRRE,
            [Description("CoRRE")]
            EncCorre,
            [Description("Hextile")]
            EncHextile,
            [Description("Zlib")]
            EncZlib,
            [Description("Tight")]
            EncTight,
            [Description("ZlibHex")]
            EncZLibHex,
            [Description("ZRLE")]
            EncZRLE
        }

        public enum AuthMode
        {
            [LocalizedAttributes.LocalizedDescriptionAttribute("VNC")]
            AuthVNC,
            [LocalizedAttributes.LocalizedDescriptionAttribute("Windows")]
            AuthWin
        }

        public enum ProxyType
        {
            [LocalizedAttributes.LocalizedDescriptionAttribute("strNone")]
            ProxyNone,
            [LocalizedAttributes.LocalizedDescriptionAttribute("strHttp")]
            ProxyHTTP,
            [LocalizedAttributes.LocalizedDescriptionAttribute("strSocks5")]
            ProxySocks5,
            [LocalizedAttributes.LocalizedDescriptionAttribute("strUltraVncRepeater")]
            ProxyUltra
        }

        public enum Colors
        {
            [LocalizedAttributes.LocalizedDescriptionAttribute("strNormal")]
            ColNormal,
            [Description("8-bit")]
            Col8Bit
        }

        public enum SmartSizeMode
        {
            [LocalizedAttributes.LocalizedDescriptionAttribute("strNoSmartSize")]
            SmartSNo,
            [LocalizedAttributes.LocalizedDescriptionAttribute("strFree")]
            SmartSFree,
            [LocalizedAttributes.LocalizedDescriptionAttribute("strAspect")]
            SmartSAspect
        }

        #endregion Enums
    }
}
