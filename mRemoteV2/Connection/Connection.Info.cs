using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using mRemoteNC.App;
using mRemoteNC.Tools;
using My;

namespace mRemoteNC.Connection
{
    [DefaultProperty("Name")]
    public class Info
    {
        #region Properties

        #region 1 Display

        private string _Name = Language.strNewConnection;

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryDisplay", 1), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameName"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionName")]
        public string Name
        {
            get { return this._Name; }
            set { this._Name = value; }
        }

        private string _Description = (string)Settings.Default.ConDefaultDescription;

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryDisplay", 1), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameDescription"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionDescription")]
        public string Description
        {
            get
            {
                if (Inherit != null && this.Inherit.Description && this._Parent != null && _Parent as Container.Info!=null)
                {
                    var info = _Parent as Container.Info;
   
                        Info parCon = info.ConnectionInfo;

                        if (_IsContainer)
                        {
                            var curCont = (Container.Info)this._Parent;
                            var parCont = (Container.Info)curCont.Parent;
                            parCon = parCont.ConnectionInfo;
                        }
                        return parCon.Description;
                }
                else
                {
                    return this._Description;
                }
            }
            set { this._Description = value; }
        }

        private string _Icon = (string)Settings.Default.ConDefaultIcon;

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryDisplay", 1), TypeConverter(typeof(Icon)),
         Browsable(true), LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameIcon"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionIcon")]
        public string Icon
        {
            get
            {
                if (Inherit != null && this.Inherit.Icon && this._Parent != null)
                {
                    var parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.Icon;
                }
                else
                {
                    return this._Icon;
                }
            }
            set { this._Icon = value; }
        }

        private string _Panel = Language.strGeneral;

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryDisplay", 1), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNamePanel"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionPanel")]
        public string Panel
        {
            get
            {
                if (Inherit != null && this.Inherit.Panel && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.Panel;
                }
                else
                {
                    return this._Panel;
                }
            }
            set { this._Panel = value; }
        }

        #endregion 1 Display

        #region 2 Connection

        private string _Hostname = "";

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryConnection", 2), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameAddress"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionAddress")]
        public string Hostname
        {
            get { return this._Hostname.Trim(); }
            set { this._Hostname = value.Trim(); }
        }

        private string _Username = (string)Settings.Default.ConDefaultUsername;

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryConnection", 2), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameUsername"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionUsername")]
        public string Username
        {
            get
            {
                if (Inherit != null && this.Inherit.Username && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.Username.Trim();
                }
                else
                {
                    return this._Username.Trim();
                }
            }
            set { this._Username = value.Trim(); }
        }

        private string _Password = (string)Settings.Default.ConDefaultPassword;

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryConnection", 2), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNamePassword"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionPassword"), PasswordPropertyText(true)
        ]
        public string Password
        {
            get
            {
                if (Inherit != null && this.Inherit.Password && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.Password;
                }
                else
                {
                    return this._Password;
                }
            }
            set { this._Password = value; }
        }

        private string _Domain = (string)Settings.Default.ConDefaultDomain;

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryConnection", 2), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameDomain"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionDomain")]
        public string Domain
        {
            get
            {
                if (Inherit != null && this.Inherit.Domain && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.Domain.Trim();
                }
                else
                {
                    return this._Domain.Trim();
                }
            }
            set { this._Domain = value.Trim(); }
        }

        #endregion 2 Connection

        #region 3 Protocol

        private Protocols _Protocol =
            (Protocols)
            Tools.Misc.StringToEnum(typeof(mRemoteNC.Protocols), Settings.Default.ConDefaultProtocol);

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryProtocol", 3), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameProtocol"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionProtocol"),
         TypeConverter(typeof(Tools.Misc.EnumTypeConverter))]
        public Protocols Protocol
        {
            get
            {
                if (Inherit != null && this.Inherit.Protocol && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.Protocol;
                }
                else
                {
                    return this._Protocol;
                }
            }
            set { this._Protocol = value; }
        }

        private string _ExtApp = (string)Settings.Default.ConDefaultExtApp;

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryProtocol", 3), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameExternalTool"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionExternalTool"),
         TypeConverter(typeof(Tools.ExternalAppsTypeConverter))]
        public string ExtApp
        {
            get
            {
                if (Inherit != null && this.Inherit.ExtApp && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.ExtApp;
                }
                else
                {
                    return this._ExtApp;
                }
            }
            set { this._ExtApp = value; }
        }

        private int _Port = 0;

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryProtocol", 3), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNamePort"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionPort")]
        public int Port
        {
            get
            {
                if (this.Inherit != null && this.Inherit.Port && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.Port;
                }
                else
                {
                    return this._Port;
                }
            }
            set { this._Port = value; }
        }

        private string _PuttySession = (string)Settings.Default.ConDefaultPuttySession;

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryProtocol", 3), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNamePuttySession"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionPuttySession"),
         TypeConverter(typeof(PuttySession))]
        public string PuttySession
        {
            get
            {
                if (Inherit != null && this.Inherit.PuttySession && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.PuttySession;
                }
                else
                {
                    return this._PuttySession;
                }
            }
            set { this._PuttySession = value; }
        }

        private ICA.EncryptionStrength _ICAEncryption =
            (ICA.EncryptionStrength)
            Tools.Misc.StringToEnum(typeof(mRemoteNC.ICA.EncryptionStrength),
                                    Settings.Default.ConDefaultICAEncryptionStrength);

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryProtocol", 3), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameEncryptionStrength"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionEncryptionStrength"),
         TypeConverter(typeof(Tools.Misc.EnumTypeConverter))]
        public ICA.EncryptionStrength ICAEncryption
        {
            get
            {
                if (Inherit != null && this.Inherit.ICAEncryption && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.ICAEncryption;
                }
                else
                {
                    return this._ICAEncryption;
                }
            }
            set { _ICAEncryption = value; }
        }

        private bool _UseConsoleSession = System.Convert.ToBoolean(Settings.Default.ConDefaultUseConsoleSession);

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryProtocol", 3), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameUseConsoleSession"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionUseConsoleSession"),
         TypeConverter(typeof(Misc.YesNoTypeConverter))]
        public bool UseConsoleSession
        {
            get
            {
                if (Inherit != null && this.Inherit.UseConsoleSession && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.UseConsoleSession;
                }
                else
                {
                    return this._UseConsoleSession;
                }
            }
            set { this._UseConsoleSession = value; }
        }

        private RDP.AuthenticationLevel _RDPAuthenticationLevel =
            (RDP.AuthenticationLevel)
            Tools.Misc.StringToEnum(typeof(mRemoteNC.RDP.AuthenticationLevel),
                                    Settings.Default.ConDefaultRDPAuthenticationLevel);

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryProtocol", 3), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameAuthenticationLevel"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionAuthenticationLevel"),
         TypeConverter(typeof(Tools.Misc.EnumTypeConverter))]
        public RDP.AuthenticationLevel RDPAuthenticationLevel
        {
            get
            {
                if (Inherit != null && this.Inherit.RDPAuthenticationLevel && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.RDPAuthenticationLevel;
                }
                else
                {
                    return this._RDPAuthenticationLevel;
                }
            }
            set { _RDPAuthenticationLevel = value; }
        }

        private HTTPBase.RenderingEngine _RenderingEngine =
            (HTTPBase.RenderingEngine)
            Tools.Misc.StringToEnum(typeof(mRemoteNC.HTTPBase.RenderingEngine),
                                    Settings.Default.ConDefaultRenderingEngine);

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryProtocol", 3), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameRenderingEngine"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionRenderingEngine"),
         TypeConverter(typeof(Tools.Misc.EnumTypeConverter))]
        public HTTPBase.RenderingEngine RenderingEngine
        {
            get
            {
                if (Inherit != null && this.Inherit.RenderingEngine && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.RenderingEngine;
                }
                else
                {
                    return this._RenderingEngine;
                }
            }
            set { _RenderingEngine = value; }
        }

        private bool _useCredSsp = System.Convert.ToBoolean(Settings.Default.ConDefaultUseCredSsp);

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryProtocol", 3), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameUseCredSsp"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionUseCredSsp"),
         TypeConverter(typeof(Misc.YesNoTypeConverter))]
        public bool UseCredSsp
        {
            get
            {
                if (Inherit != null && this.Inherit.UseCredSsp && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.UseCredSsp;
                }
                else
                {
                    return this._useCredSsp;
                }
            }
            set { this._useCredSsp = value; }
        }

        #endregion 3 Protocol

        #region 4 RD Gateway

        private RDP.RDGatewayUsageMethod _RDGatewayUsageMethod =
            (RDP.RDGatewayUsageMethod)
            Tools.Misc.StringToEnum(typeof(mRemoteNC.RDP.RDGatewayUsageMethod),
                                    Settings.Default.ConDefaultRDGatewayUsageMethod);

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryGateway", 4), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameRDGatewayUsageMethod"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionRDGatewayUsageMethod"),
         TypeConverter(typeof(Tools.Misc.EnumTypeConverter))]
        public RDP.RDGatewayUsageMethod RDGatewayUsageMethod
        {
            get
            {
                if (Inherit != null && this.Inherit.RDGatewayUsageMethod && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.RDGatewayUsageMethod;
                }
                else
                {
                    return _RDGatewayUsageMethod;
                }
            }
            set { _RDGatewayUsageMethod = value; }
        }

        private string _RDGatewayHostname = (string)Settings.Default.ConDefaultRDGatewayHostname;

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryGateway", 4), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameRDGatewayHostname"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionRDGatewayHostname")]
        public string RDGatewayHostname
        {
            get
            {
                if (Inherit != null && this.Inherit.RDGatewayHostname && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.RDGatewayHostname.Trim();
                }
                else
                {
                    return this._RDGatewayHostname.Trim();
                }
            }
            set { this._RDGatewayHostname = value.Trim(); }
        }

        private RDP.RDGatewayUseConnectionCredentials _RDGatewayUseConnectionCredentials =
            (RDP.RDGatewayUseConnectionCredentials)
            Tools.Misc.StringToEnum(typeof(mRemoteNC.RDP.RDGatewayUseConnectionCredentials),
                                    Settings.Default.ConDefaultRDGatewayUseConnectionCredentials);

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryGateway", 4), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameRDGatewayUseConnectionCredentials"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionRDGatewayUseConnectionCredentials"),
         TypeConverter(typeof(Tools.Misc.EnumTypeConverter))]
        public RDP.RDGatewayUseConnectionCredentials
            RDGatewayUseConnectionCredentials
        {
            get
            {
                if (Inherit != null && this.Inherit.RDGatewayUseConnectionCredentials && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.RDGatewayUseConnectionCredentials;
                }
                else
                {
                    return _RDGatewayUseConnectionCredentials;
                }
            }
            set { _RDGatewayUseConnectionCredentials = value; }
        }

        private string _RDGatewayUsername = (string)Settings.Default.ConDefaultRDGatewayUsername;

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryGateway", 4), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameRDGatewayUsername"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionRDGatewayUsername")]
        public string RDGatewayUsername
        {
            get
            {
                if (Inherit != null && this.Inherit.RDGatewayUsername && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.RDGatewayUsername;
                }
                else
                {
                    return this._RDGatewayUsername;
                }
            }
            set { this._RDGatewayUsername = value; }
        }

        private string _RDGatewayPassword = (string)Settings.Default.ConDefaultRDGatewayPassword;

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryGateway", 4), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameRDGatewayPassword"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyNameRDGatewayPassword"),
         PasswordPropertyText(true)]
        public string RDGatewayPassword
        {
            get
            {
                if (Inherit != null && this.Inherit.RDGatewayPassword && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.RDGatewayPassword;
                }
                else
                {
                    return this._RDGatewayPassword;
                }
            }
            set { this._RDGatewayPassword = value; }
        }

        private string _RDGatewayDomain = (string)Settings.Default.ConDefaultRDGatewayDomain;

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryGateway", 4), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameRDGatewayDomain"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionRDGatewayDomain")]
        public string RDGatewayDomain
        {
            get
            {
                if (Inherit != null && this.Inherit.RDGatewayDomain && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.RDGatewayDomain.Trim();
                }
                else
                {
                    return this._RDGatewayDomain.Trim();
                }
            }
            set { this._RDGatewayDomain = value.Trim(); }
        }

        #endregion 4 RD Gateway

        #region 5 Appearance

        private RDP.RDPResolutions _Resolution =
            (RDP.RDPResolutions)
            Tools.Misc.StringToEnum(typeof(mRemoteNC.RDP.RDPResolutions),
                                    Settings.Default.ConDefaultResolution);

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryAppearance", 5), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameResolution"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionResolution"),
         TypeConverter(typeof(Tools.Misc.EnumTypeConverter))]
        public RDP.RDPResolutions Resolution
        {
            get
            {
                if (Inherit != null && this.Inherit.Resolution && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.Resolution;
                }
                else
                {
                    return this._Resolution;
                }
            }
            set { this._Resolution = value; }
        }

        private RDP.RDPColors _Colors =
            (RDP.RDPColors)
            Tools.Misc.StringToEnum(typeof(mRemoteNC.RDP.RDPColors), Settings.Default.ConDefaultColors);

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryAppearance", 5), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameColors"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionColors"),
         TypeConverter(typeof(Tools.Misc.EnumTypeConverter))]
        public RDP.RDPColors Colors
        {
            get
            {
                if (Inherit != null && this.Inherit.Colors && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.Colors;
                }
                else
                {
                    return this._Colors;
                }
            }
            set { this._Colors = value; }
        }

        private bool _CacheBitmaps = System.Convert.ToBoolean(Settings.Default.ConDefaultCacheBitmaps);

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryAppearance", 5), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameCacheBitmaps"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionCacheBitmaps"),
         TypeConverter(typeof(Misc.YesNoTypeConverter))]
        public bool CacheBitmaps
        {
            get
            {
                if (Inherit != null && this.Inherit.CacheBitmaps && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        Container.Info curCont = (Container.Info)this._Parent;
                        Container.Info parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.CacheBitmaps;
                }
                else
                {
                    return this._CacheBitmaps;
                }
            }
            set { this._CacheBitmaps = value; }
        }

        private bool _DisplayWallpaper = System.Convert.ToBoolean(Settings.Default.ConDefaultDisplayWallpaper);

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryAppearance", 5), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameDisplayWallpaper"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionDisplayWallpaper"),
         TypeConverter(typeof(Misc.YesNoTypeConverter))]
        public bool DisplayWallpaper
        {
            get
            {
                if (Inherit != null && this.Inherit.DisplayWallpaper && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.DisplayWallpaper;
                }
                else
                {
                    return this._DisplayWallpaper;
                }
            }
            set { this._DisplayWallpaper = value; }
        }

        private bool _DisplayThemes = System.Convert.ToBoolean(Settings.Default.ConDefaultDisplayThemes);

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryAppearance", 5), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameDisplayThemes"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionDisplayThemes"),
         TypeConverter(typeof(Misc.YesNoTypeConverter))]
        public bool DisplayThemes
        {
            get
            {
                if (Inherit != null && this.Inherit.DisplayThemes && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.DisplayThemes;
                }
                else
                {
                    return this._DisplayThemes;
                }
            }
            set { this._DisplayThemes = value; }
        }

        private bool _EnableFontSmoothing = System.Convert.ToBoolean(Settings.Default.ConDefaultEnableFontSmoothing);

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryAppearance", 5), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameEnableFontSmoothing"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionEnableFontSmoothing"),
         TypeConverter(typeof(Misc.YesNoTypeConverter))]
        public bool EnableFontSmoothing
        {
            get
            {
                if (Inherit != null && this.Inherit.EnableFontSmoothing && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.EnableFontSmoothing;
                }
                else
                {
                    return this._EnableFontSmoothing;
                }
            }
            set { this._EnableFontSmoothing = value; }
        }

        private bool _EnableDesktopComposition =
            System.Convert.ToBoolean(Settings.Default.ConDefaultEnableDesktopComposition);

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryAppearance", 5), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameEnableDesktopComposition"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionEnableDesktopComposition"),
         TypeConverter(typeof(Misc.YesNoTypeConverter))]
        public bool EnableDesktopComposition
        {
            get
            {
                if (Inherit != null && this.Inherit.EnableDesktopComposition && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.EnableDesktopComposition;
                }
                else
                {
                    return this._EnableDesktopComposition;
                }
            }
            set { this._EnableDesktopComposition = value; }
        }

        #endregion 5 Appearance

        #region 6 Redirect

        private bool _RedirectKeys = System.Convert.ToBoolean(Settings.Default.ConDefaultRedirectKeys);

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryRedirect", 6), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameRedirectKeys"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionRedirectKeys"),
         TypeConverter(typeof(Misc.YesNoTypeConverter))]
        public bool RedirectKeys
        {
            get
            {
                if (Inherit != null && this.Inherit.RedirectKeys && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.RedirectKeys;
                }
                else
                {
                    return this._RedirectKeys;
                }
            }
            set { this._RedirectKeys = value; }
        }

        private bool _RedirectDiskDrives = System.Convert.ToBoolean(Settings.Default.ConDefaultRedirectDiskDrives);

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryRedirect", 6), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameRedirectDrives"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionRedirectDrives"),
         TypeConverter(typeof(Misc.YesNoTypeConverter))]
        public bool RedirectDiskDrives
        {
            get
            {
                if (Inherit != null && this.Inherit.RedirectDiskDrives && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.RedirectDiskDrives;
                }
                else
                {
                    return this._RedirectDiskDrives;
                }
            }
            set { this._RedirectDiskDrives = value; }
        }

        private bool _RedirectPrinters = System.Convert.ToBoolean(Settings.Default.ConDefaultRedirectPrinters);

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryRedirect", 6), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameRedirectPrinters"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionRedirectPrinters"),
         TypeConverter(typeof(Misc.YesNoTypeConverter))]
        public bool RedirectPrinters
        {
            get
            {
                if (Inherit != null && this.Inherit.RedirectPrinters && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.RedirectPrinters;
                }
                else
                {
                    return this._RedirectPrinters;
                }
            }
            set { this._RedirectPrinters = value; }
        }

        private bool _RedirectPorts = System.Convert.ToBoolean(Settings.Default.ConDefaultRedirectPorts);

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryRedirect", 6), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameRedirectPorts"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionRedirectPorts"),
         TypeConverter(typeof(Misc.YesNoTypeConverter))]
        public bool RedirectPorts
        {
            get
            {
                if (Inherit != null && this.Inherit.RedirectPorts && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.RedirectPorts;
                }
                else
                {
                    return this._RedirectPorts;
                }
            }
            set { this._RedirectPorts = value; }
        }

        private bool _RedirectSmartCards = System.Convert.ToBoolean(Settings.Default.ConDefaultRedirectSmartCards);

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryRedirect", 6), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameRedirectSmartCards"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionRedirectSmartCards"),
         TypeConverter(typeof(Misc.YesNoTypeConverter))]
        public bool RedirectSmartCards
        {
            get
            {
                if (Inherit != null && this.Inherit.RedirectSmartCards && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.RedirectSmartCards;
                }
                else
                {
                    return this._RedirectSmartCards;
                }
            }
            set { this._RedirectSmartCards = value; }
        }

        private RDP.RDPSounds _RedirectSound =
            (RDP.RDPSounds)
            Tools.Misc.StringToEnum(typeof(mRemoteNC.RDP.RDPSounds), Settings.Default.ConDefaultRedirectSound);

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryRedirect", 6), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameRedirectSounds"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionRedirectSounds"),
         TypeConverter(typeof(Tools.Misc.EnumTypeConverter))]
        public RDP.RDPSounds RedirectSound
        {
            get
            {
                if (Inherit != null && this.Inherit.RedirectSound && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.RedirectSound;
                }
                else
                {
                    return this._RedirectSound;
                }
            }
            set { this._RedirectSound = value; }
        }

        #endregion 6 Redirect

        #region 7 Misc

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryMiscellaneous", 7),
        Browsable(true),
        LocalizedAttributes.LocalizedDisplayName("strPropertyNameConnectOnStartup"),
        LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionConnectOnStartup"),
        TypeConverter(typeof(Misc.YesNoTypeConverter))]
        public bool ConnectOnStartup { get; set; }

        private string _PreExtApp = (string)Settings.Default.ConDefaultPreExtApp;

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryMiscellaneous", 7), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameExternalToolBefore"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionExternalToolBefore"),
         TypeConverter(typeof(Tools.ExternalAppsTypeConverter))]
        public string PreExtApp
        {
            get
            {
                if (Inherit != null && this.Inherit.PreExtApp && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.PreExtApp;
                }
                else
                {
                    return this._PreExtApp;
                }
            }
            set { this._PreExtApp = value; }
        }

        private string _PostExtApp = (string)Settings.Default.ConDefaultPostExtApp;

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryMiscellaneous", 7), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameExternalToolAfter"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionExternalToolAfter"),
         TypeConverter(typeof(Tools.ExternalAppsTypeConverter))]
        public string PostExtApp
        {
            get
            {
                if (Inherit != null && this.Inherit.PostExtApp && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.PostExtApp;
                }
                else
                {
                    return this._PostExtApp;
                }
            }
            set { this._PostExtApp = value; }
        }

        private string _MacAddress = (string)Settings.Default.ConDefaultMacAddress;

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryMiscellaneous", 7), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameMACAddress"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionMACAddress")]
        public string MacAddress
        {
            get
            {
                if (Inherit != null && this.Inherit.MacAddress && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.MacAddress;
                }
                else
                {
                    return this._MacAddress;
                }
            }
            set { this._MacAddress = value; }
        }

        private string _UserField = (string)Settings.Default.ConDefaultUserField;

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryMiscellaneous", 7), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameUser1"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionUser1")]
        public string UserField
        {
            get
            {
                if (Inherit != null && this.Inherit.UserField && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer == true)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.UserField;
                }
                else
                {
                    return this._UserField;
                }
            }
            set { this._UserField = value; }
        }

        #endregion 7 Misc

        #region 8 VNC

        private VNC.Compression _VNCCompression =
            (VNC.Compression)
            Tools.Misc.StringToEnum(typeof(mRemoteNC.VNC.Compression),
                                    Settings.Default.ConDefaultVNCCompression);

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryAppearance", 5), Browsable(false),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameCompression"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionCompression"),
         TypeConverter(typeof(Tools.Misc.EnumTypeConverter))]
        public VNC.Compression VNCCompression
        {
            get
            {
                if (Inherit != null && this.Inherit.VNCCompression && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.VNCCompression;
                }
                else
                {
                    return _VNCCompression;
                }
            }
            set { _VNCCompression = value; }
        }

        private VNC.Encoding _VNCEncoding =
            (VNC.Encoding)
            Tools.Misc.StringToEnum(typeof(mRemoteNC.VNC.Encoding), Settings.Default.ConDefaultVNCEncoding);

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryAppearance", 5), Browsable(false),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameEncoding"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionEncoding"),
         TypeConverter(typeof(Tools.Misc.EnumTypeConverter))]
        public VNC.Encoding VNCEncoding
        {
            get
            {
                if (Inherit != null && this.Inherit.VNCEncoding && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.VNCEncoding;
                }
                else
                {
                    return _VNCEncoding;
                }
            }
            set { _VNCEncoding = value; }
        }

        private VNC.AuthMode _VNCAuthMode =
            (VNC.AuthMode)
            Tools.Misc.StringToEnum(typeof(mRemoteNC.VNC.AuthMode), Settings.Default.ConDefaultVNCAuthMode);

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryConnection", 2), Browsable(false),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameAuthenticationMode"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionAuthenticationMode"),
         TypeConverter(typeof(Tools.Misc.EnumTypeConverter))]
        public VNC.AuthMode VNCAuthMode
        {
            get
            {
                if (Inherit != null && this.Inherit.VNCAuthMode && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.VNCAuthMode;
                }
                else
                {
                    return _VNCAuthMode;
                }
            }
            set { _VNCAuthMode = value; }
        }

        private VNC.ProxyType _VNCProxyType =
            (VNC.ProxyType)
            Tools.Misc.StringToEnum(typeof(mRemoteNC.VNC.ProxyType), Settings.Default.ConDefaultVNCProxyType);

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryMiscellaneous", 7), Browsable(false),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameVNCProxyType"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionVNCProxyType"),
         TypeConverter(typeof(Tools.Misc.EnumTypeConverter))]
        public VNC.ProxyType VNCProxyType
        {
            get
            {
                if (Inherit != null && this.Inherit.VNCProxyType && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.VNCProxyType;
                }
                else
                {
                    return _VNCProxyType;
                }
            }
            set { _VNCProxyType = value; }
        }

        private string _VNCProxyIP = (string)Settings.Default.ConDefaultVNCProxyIP;

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryMiscellaneous", 7), Browsable(false),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameVNCProxyAddress"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionVNCProxyAddress")]
        public string VNCProxyIP
        {
            get
            {
                if (Inherit != null && this.Inherit.VNCProxyIP && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.VNCProxyIP;
                }
                else
                {
                    return _VNCProxyIP;
                }
            }
            set { _VNCProxyIP = value; }
        }

        private int _VNCProxyPort = System.Convert.ToInt32(Settings.Default.ConDefaultVNCProxyPort);

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryMiscellaneous", 7), Browsable(false),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameVNCProxyPort"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionVNCProxyPort")]
        public int VNCProxyPort
        {
            get
            {
                if (Inherit != null && this.Inherit.VNCProxyPort && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.VNCProxyPort;
                }
                else
                {
                    return _VNCProxyPort;
                }
            }
            set { _VNCProxyPort = value; }
        }

        private string _VNCProxyUsername = (string)Settings.Default.ConDefaultVNCProxyUsername;

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryMiscellaneous", 7), Browsable(false),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameVNCProxyUsername"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionVNCProxyUsername")]
        public string VNCProxyUsername
        {
            get
            {
                if (Inherit != null && this.Inherit.VNCProxyUsername && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.VNCProxyUsername;
                }
                else
                {
                    return _VNCProxyUsername;
                }
            }
            set { _VNCProxyUsername = value; }
        }

        private string _VNCProxyPassword = (string)Settings.Default.ConDefaultVNCProxyPassword;

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryMiscellaneous", 7), Browsable(false),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameVNCProxyPassword"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionVNCProxyPassword"),
         PasswordPropertyText(true)]
        public string VNCProxyPassword
        {
            get
            {
                if (Inherit != null && this.Inherit.VNCProxyPassword && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.VNCProxyPassword;
                }
                else
                {
                    return _VNCProxyPassword;
                }
            }
            set { _VNCProxyPassword = value; }
        }

        private VNC.Colors _VNCColors =
            (VNC.Colors)
            Tools.Misc.StringToEnum(typeof(mRemoteNC.VNC.Colors), Settings.Default.ConDefaultVNCColors);

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryAppearance", 5), Browsable(false),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameColors"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionColors"),
         TypeConverter(typeof(Tools.Misc.EnumTypeConverter))]
        public VNC.Colors VNCColors
        {
            get
            {
                if (Inherit != null && this.Inherit.VNCColors && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.VNCColors;
                }
                else
                {
                    return _VNCColors;
                }
            }
            set { _VNCColors = value; }
        }

        private VNC.SmartSizeMode _VNCSmartSizeMode =
            (VNC.SmartSizeMode)
            Tools.Misc.StringToEnum(typeof(mRemoteNC.VNC.SmartSizeMode),
                                    Settings.Default.ConDefaultVNCSmartSizeMode);

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryAppearance", 5), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameSmartSizeMode"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionSmartSizeMode"),
         TypeConverter(typeof(Tools.Misc.EnumTypeConverter))]
        public VNC.SmartSizeMode VNCSmartSizeMode
        {
            get
            {
                if (Inherit != null && this.Inherit.VNCSmartSizeMode && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.VNCSmartSizeMode;
                }
                else
                {
                    return _VNCSmartSizeMode;
                }
            }
            set { _VNCSmartSizeMode = value; }
        }

        private bool _VNCViewOnly = System.Convert.ToBoolean(Settings.Default.ConDefaultVNCViewOnly);

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryAppearance", 5), Browsable(true),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameViewOnly"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionViewOnly"),
         TypeConverter(typeof(Tools.Misc.YesNoTypeConverter))]
        public bool VNCViewOnly
        {
            get
            {
                if (Inherit != null && this.Inherit.VNCViewOnly && this._Parent != null)
                {
                    global::mRemoteNC.Connection.Info parCon = (this._Parent as Container.Info).ConnectionInfo;

                    if (this._IsContainer)
                    {
                        var curCont = (Container.Info)this._Parent;
                        var parCont = (Container.Info)curCont.Parent;
                        parCon = parCont.ConnectionInfo;
                    }

                    return parCon.VNCViewOnly;
                }
                else
                {
                    return _VNCViewOnly;
                }
            }
            set { _VNCViewOnly = value; }
        }

        #endregion 8 VNC

        [Category(""), Browsable(false)]
        public Inheritance Inherit
        {
            get
            {
                //ToDo
                return _inherit ?? (_inherit = new Inheritance(this));
            }
            set { _inherit = value; }
        }

        [Category(""), Browsable(false)]
        public mRemoteNC.List OpenConnections { get; set; }

        private bool _IsContainer = false;

        [Category(""), Browsable(false)]
        public bool IsContainer
        {
            get { return this._IsContainer; }
            set { this._IsContainer = value; }
        }

        private bool _IsDefault = false;

        [Category(""), Browsable(false)]
        public bool IsDefault
        {
            get { return this._IsDefault; }
            set { this._IsDefault = value; }
        }

        private object _Parent;

        [Category(""), Browsable(false)]
        public object Parent
        {
            get { return this._Parent; }
            set { this._Parent = value; }
        }

        private int _PositionID = 0;

        [Category(""), Browsable(false)]
        public int PositionID
        {
            get { return this._PositionID; }
            set { _PositionID = value; }
        }

        private string _ConstantID = (string)Tools.Misc.CreateConstantID();

        [Category(""), Browsable(false)]
        public string ConstantID
        {
            get { return _ConstantID; }
            set { _ConstantID = value; }
        }

        private TreeNode _TreeNode;

        [Category(""), Browsable(false)]
        public TreeNode TreeNode
        {
            get { return this._TreeNode; }
            set { this._TreeNode = value; }
        }

        private bool _IsQuicky = false;

        [Category(""), Browsable(false)]
        public bool IsQuicky
        {
            get { return this._IsQuicky; }
            set { this._IsQuicky = value; }
        }

        private bool _PleaseConnect = false;
        private Inheritance _inherit;

        [Category(""), Browsable(false)]
        public bool PleaseConnect
        {
            get { return _PleaseConnect; }
            set { _PleaseConnect = value; }
        }

        #endregion Properties

        #region Methods

        public Info Copy()
        {
            Connection.Info newConnectionInfo = (Info) this.MemberwiseClone();
            newConnectionInfo.OpenConnections = new mRemoteNC.List();
            return newConnectionInfo;
        }

        public Info()
        {
            this.OpenConnections = new mRemoteNC.List();
            this.SetDefaults();
        }

        public Info(Container.Info Parent)
        {
            this.OpenConnections = new mRemoteNC.List();
            this.SetDefaults();
            this._IsContainer = true;
            this._Parent = Parent;
        }

        public void SetDefaults()
        {
            if (this.Port == 0)
            {
                this.SetDefaultPort();
            }
        }

        public void SetDefaultPort()
        {
            try
            {
                switch (this._Protocol)
                {
                    //Placeholder: Protocol
                    case Protocols.RDP:
                        this._Port = System.Convert.ToInt32(RDP.Defaults.Port);
                        break;
                    case Protocols.VNC:
                        this._Port = System.Convert.ToInt32(VNC.Defaults.Port);
                        break;
                    case Protocols.SSH1:
                        this._Port = System.Convert.ToInt32(SSH1.Defaults.Port);
                        break;
                    case Protocols.SSH2:
                        this._Port = System.Convert.ToInt32(SSH2.Defaults.Port);
                        break;
                    case Protocols.Telnet:
                        this._Port = System.Convert.ToInt32(Telnet.Defaults.Port);
                        break;
                    case Protocols.Rlogin:
                        this._Port = System.Convert.ToInt32(Rlogin.Defaults.Port);
                        break;
                    case Protocols.Serial:
                        this._Port = System.Convert.ToInt32(Serial.Defaults.Port);
                        break;
                    case Protocols.RAW:
                        this._Port = System.Convert.ToInt32(RAW.Defaults.Port);
                        break;
                    case Protocols.HTTP:
                        this._Port = System.Convert.ToInt32(HTTP.Defaults.Port);
                        break;
                    case Protocols.HTTPS:
                        this._Port = System.Convert.ToInt32(HTTPS.Defaults.Port);
                        break;
                    case Protocols.ICA:
                        this._Port = System.Convert.ToInt32(ICA.Defaults.Port);
                        break;
                    case Protocols.IntApp:
                        this._Port = System.Convert.ToInt32(IntApp.Defaults.Port);
                        break;
                    case Protocols.TeamViewer:
                        this._Port = 0;
                        break;
                    case Protocols.RAdmin:
                        _Port = 4899;
                        break;
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strConnectionSetDefaultPortFailed + Constants.vbNewLine +
                                                    ex.Message);
            }
        }

        #endregion Methods

        #region Inheritance

        public class Inheritance
        {
            public Inheritance(object Parent, bool InheritEverything = false)
            {
                PreExtApp = System.Convert.ToBoolean(Settings.Default.InhDefaultPreExtApp);
                this._Parent = Parent;

                if (InheritEverything == true)
                {
                    this.TurnOnInheritanceCompletely();
                }
            }

            public Info.Inheritance Copy()
            {
                return (Info.Inheritance)this.MemberwiseClone();
            }

            public void TurnOnInheritanceCompletely()
            {
                SetAllValues(true);
            }

            public void TurnOffInheritanceCompletely()
            {
                SetAllValues(false);
            }


            private void SetAllValues(bool val)
            {
                this._CacheBitmaps = val;
                this._Colors = val;
                this._Description = val;
                this._DisplayThemes = val;
                this._DisplayWallpaper = val;
                this._EnableFontSmoothing = val;
                this._EnableDesktopComposition = val;
                this._Domain = val;
                this._Icon = val;
                this._Password = val;
                this._Port = val;
                this._Protocol = val;
                this._PuttySession = val;
                this._RedirectDiskDrives = val;
                this._RedirectKeys = val;
                this._RedirectPorts = val;
                this._RedirectPrinters = val;
                this._RedirectSmartCards = val;
                this._RedirectSound = val;
                this._Resolution = val;
                this._UseConsoleSession = val;
                _useCredSsp = val;
                this._RenderingEngine = val;
                this._Username = val;
                this._Panel = val;
                this._ICAEncryption = val;
                this._RDPAuthenticationLevel = val;
                this.PreExtApp = val;
                this._PostExtApp = val;
                this._MacAddress = val;
                this._UserField = val;

                this._VNCAuthMode = val;
                this._VNCColors = val;
                this._VNCCompression = val;
                this._VNCEncoding = val;
                this._VNCProxyIP = val;
                this._VNCProxyPassword = val;
                this._VNCProxyPort = val;
                this._VNCProxyType = val;
                this._VNCProxyUsername = val;
                this._VNCSmartSizeMode = val;
                this._VNCViewOnly = val;
                this._ExtApp = val;

                this._RDGatewayDomain = val;
                this._RDGatewayHostname = val;
                this._RDGatewayPassword = val;
                this._RDGatewayUsageMethod = val;
                this._RDGatewayUseConnectionCredentials = val;
                this._RDGatewayUsername = val;
                //Me._RDPAuthenticationLevel = val
            }

            private object _Parent;

            [Category(""), Browsable(false)]
            public object Parent
            {
                get { return this._Parent; }
                set { this._Parent = value; }
            }

            private bool _IsDefault = false;

            [Category(""), Browsable(false)]
            public bool IsDefault
            {
                get { return this._IsDefault; }
                set { this._IsDefault = value; }
            }

            #region 1 General

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryGeneral", 1), Browsable(true),
             LocalizedAttributes.LocalizedDisplayNameInheritAttribute("strPropertyNameAll"),
             LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyDescriptionAll"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool EverythingInherited
            {
                //LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameInheritAll"), _
                get
                {
                    if (this._CacheBitmaps && this._Colors && this._Description && this._DisplayThemes &&
                        this._DisplayWallpaper && this._EnableFontSmoothing && this._EnableDesktopComposition &&
                        this._Domain && this._Icon && this._Password && this._Port && this._Protocol &&
                        this._PuttySession && this._RedirectDiskDrives && this._RedirectKeys && this._RedirectPorts &&
                        this._RedirectPrinters && this._RedirectSmartCards && this._RedirectSound && this._Resolution &&
                        this._UseConsoleSession && _useCredSsp && this._RenderingEngine && this._UserField &&
                        this._ExtApp && this._Username && this._Panel && this._ICAEncryption &&
                        this._RDPAuthenticationLevel && this.PreExtApp && this._PostExtApp && this._MacAddress &&
                        this._VNCAuthMode && this._VNCColors && this._VNCCompression && this._VNCEncoding &&
                        this._VNCProxyIP && this._VNCProxyPassword && this._VNCProxyPort && this._VNCProxyType &&
                        this._VNCProxyUsername)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                set
                {
                    if (value == true)
                    {
                        this.TurnOnInheritanceCompletely();
                    }
                    else
                    {
                        this.TurnOffInheritanceCompletely();
                    }
                }
            }

            #endregion 1 General

            #region 2 Display

            private bool _Description = System.Convert.ToBoolean(Settings.Default.InhDefaultDescription);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryDisplay", 2), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyDescriptionDescription"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool Description
            {
                get { return this._Description; }
                set { this._Description = value; }
            }

            private bool _Icon = System.Convert.ToBoolean(Settings.Default.InhDefaultIcon);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryDisplay", 2), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameIcon"),
             LocalizedAttributes.LocalizedDisplayNameInheritAttribute("strPropertyDescriptionIcon"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool Icon
            {
                get { return this._Icon; }
                set { this._Icon = value; }
            }

            private bool _Panel = System.Convert.ToBoolean(Settings.Default.InhDefaultPanel);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryDisplay", 2), Browsable(true),
             LocalizedAttributes.LocalizedDisplayNameInheritAttribute("strPropertyDescriptionPanel"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool Panel
            {
                get { return this._Panel; }
                set { this._Panel = value; }
            }

            #endregion 2 Display

            #region 3 Connection

            private bool _Username = System.Convert.ToBoolean(Settings.Default.InhDefaultUsername);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryConnection", 3), Browsable(true),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionUsername"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool Username
            {
                get { return this._Username; }
                set { this._Username = value; }
            }

            private bool _Password = System.Convert.ToBoolean(Settings.Default.InhDefaultPassword);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryConnection", 3), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNamePassword"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool Password
            {
                get { return this._Password; }
                set { this._Password = value; }
            }

            private bool _Domain = System.Convert.ToBoolean(Settings.Default.InhDefaultDomain);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryConnection", 3), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameDomain"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionDomain"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool Domain
            {
                get { return this._Domain; }
                set { this._Domain = value; }
            }

            #endregion 3 Connection

            #region 4 Protocol

            private bool _Protocol = System.Convert.ToBoolean(Settings.Default.InhDefaultProtocol);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryProtocol", 4), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameProtocol"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionProtocol"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool Protocol
            {
                get { return this._Protocol; }
                set { this._Protocol = value; }
            }

            private bool _ExtApp = System.Convert.ToBoolean(Settings.Default.InhDefaultExtApp);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryProtocol", 4), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameExternalTool"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionExternalTool"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool ExtApp
            {
                get { return this._ExtApp; }
                set { this._ExtApp = value; }
            }

            private bool _Port = System.Convert.ToBoolean(Settings.Default.InhDefaultPort);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryProtocol", 4), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNamePort"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionPort"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool Port
            {
                get { return this._Port; }
                set { this._Port = value; }
            }

            private bool _PuttySession = System.Convert.ToBoolean(Settings.Default.InhDefaultPuttySession);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryProtocol", 4), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNamePuttySession"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionPuttySession"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool PuttySession
            {
                get { return this._PuttySession; }
                set { this._PuttySession = value; }
            }

            private bool _ICAEncryption = System.Convert.ToBoolean(Settings.Default.InhDefaultICAEncryptionStrength);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryProtocol", 4), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameEncryptionStrength"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionEncryptionStrength"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool ICAEncryption
            {
                get { return this._ICAEncryption; }
                set { this._ICAEncryption = value; }
            }

            private bool _RDPAuthenticationLevel =
                System.Convert.ToBoolean(Settings.Default.InhDefaultRDPAuthenticationLevel);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryProtocol", 4), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameAuthenticationLevel"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionAuthenticationLevel"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool RDPAuthenticationLevel
            {
                get { return this._RDPAuthenticationLevel; }
                set { this._RDPAuthenticationLevel = value; }
            }

            private bool _RenderingEngine = System.Convert.ToBoolean(Settings.Default.InhDefaultRenderingEngine);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryProtocol", 4), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameRenderingEngine"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionRenderingEngine"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool RenderingEngine
            {
                get { return this._RenderingEngine; }
                set { this._RenderingEngine = value; }
            }

            private bool _UseConsoleSession = System.Convert.ToBoolean(Settings.Default.InhDefaultUseConsoleSession);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryProtocol", 4), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameUseConsoleSession"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionUseConsoleSession"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool UseConsoleSession
            {
                get { return this._UseConsoleSession; }
                set { this._UseConsoleSession = value; }
            }

            private bool _useCredSsp = System.Convert.ToBoolean(Settings.Default.InhDefaultUseCredSsp);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryProtocol", 4), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameUseCredSsp"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionUseCredSsp"),
             TypeConverter(typeof(Tools.Misc.YesNoTypeConverter))]
            public bool UseCredSsp
            {
                get { return _useCredSsp; }
                set { _useCredSsp = value; }
            }

            #endregion 4 Protocol

            #region 5 RD Gateway

            private bool _RDGatewayUsageMethod =
                System.Convert.ToBoolean(Settings.Default.InhDefaultRDGatewayUsageMethod);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryGateway", 5), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameRDGatewayUsageMethod"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionRDGatewayUsageMethod"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool RDGatewayUsageMethod
            {
                get { return this._RDGatewayUsageMethod; }
                set { this._RDGatewayUsageMethod = value; }
            }

            private bool _RDGatewayHostname = System.Convert.ToBoolean(Settings.Default.InhDefaultRDGatewayHostname);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryGateway", 5), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameRDGatewayHostname"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionRDGatewayHostname"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool RDGatewayHostname
            {
                get { return this._RDGatewayHostname; }
                set { this._RDGatewayHostname = value; }
            }

            private bool _RDGatewayUseConnectionCredentials =
                System.Convert.ToBoolean(Settings.Default.InhDefaultRDGatewayUseConnectionCredentials);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryGateway", 5), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameRDGatewayUseConnectionCredentials"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute(
                 "strPropertyDescriptionRDGatewayUseConnectionCredentials"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool RDGatewayUseConnectionCredentials
            {
                get { return this._RDGatewayUseConnectionCredentials; }
                set { this._RDGatewayUseConnectionCredentials = value; }
            }

            private bool _RDGatewayUsername = System.Convert.ToBoolean(Settings.Default.InhDefaultRDGatewayUsername);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryGateway", 5), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameRDGatewayUsername"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionRDGatewayUsername"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool RDGatewayUsername
            {
                get { return this._RDGatewayUsername; }
                set { this._RDGatewayUsername = value; }
            }

            private bool _RDGatewayPassword = System.Convert.ToBoolean(Settings.Default.InhDefaultRDGatewayPassword);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryGateway", 5), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameRDGatewayPassword"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionRDGatewayPassword"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool RDGatewayPassword
            {
                get { return this._RDGatewayPassword; }
                set { this._RDGatewayPassword = value; }
            }

            private bool _RDGatewayDomain = System.Convert.ToBoolean(Settings.Default.InhDefaultRDGatewayDomain);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryGateway", 5), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameRDGatewayDomain"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionRDGatewayDomain"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool RDGatewayDomain
            {
                get { return this._RDGatewayDomain; }
                set { this._RDGatewayDomain = value; }
            }

            #endregion 5 RD Gateway

            #region 6 Appearance

            private bool _Resolution = System.Convert.ToBoolean(Settings.Default.InhDefaultResolution);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryAppearance", 6), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameResolution"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionResolution"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool Resolution
            {
                get { return this._Resolution; }
                set { this._Resolution = value; }
            }

            private bool _Colors = System.Convert.ToBoolean(Settings.Default.InhDefaultColors);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryAppearance", 6), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameColors"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionColors"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool Colors
            {
                get { return this._Colors; }
                set { this._Colors = value; }
            }

            private bool _CacheBitmaps = System.Convert.ToBoolean(Settings.Default.InhDefaultCacheBitmaps);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryAppearance", 6), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameCacheBitmaps"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionCacheBitmaps"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool CacheBitmaps
            {
                get { return this._CacheBitmaps; }
                set { this._CacheBitmaps = value; }
            }

            private bool _DisplayWallpaper = System.Convert.ToBoolean(Settings.Default.InhDefaultDisplayWallpaper);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryAppearance", 6), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameDisplayWallpaper"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionDisplayWallpaper"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool DisplayWallpaper
            {
                get { return this._DisplayWallpaper; }
                set { this._DisplayWallpaper = value; }
            }

            private bool _DisplayThemes = System.Convert.ToBoolean(Settings.Default.InhDefaultDisplayThemes);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryAppearance", 6), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameDisplayThemes"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionDisplayThemes"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool DisplayThemes
            {
                get { return this._DisplayThemes; }
                set { this._DisplayThemes = value; }
            }

            private bool _EnableFontSmoothing = System.Convert.ToBoolean(Settings.Default.InhDefaultEnableFontSmoothing);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryAppearance", 6), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameEnableFontSmoothing"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionEnableFontSmoothing"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool EnableFontSmoothing
            {
                get { return this._EnableFontSmoothing; }
                set { this._EnableFontSmoothing = value; }
            }

            private bool _EnableDesktopComposition =
                System.Convert.ToBoolean(Settings.Default.InhDefaultEnableDesktopComposition);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryAppearance", 6), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameEnableDesktopComposition"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute(
                 "strPropertyDescriptionEnableEnableDesktopComposition"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool EnableDesktopComposition
            {
                get { return this._EnableDesktopComposition; }
                set { this._EnableDesktopComposition = value; }
            }

            #endregion 6 Appearance

            #region 7 Redirect

            private bool _RedirectKeys = System.Convert.ToBoolean(Settings.Default.InhDefaultRedirectKeys);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryRedirect", 7), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameRedirectKeys"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionRedirectKeys"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool RedirectKeys
            {
                get { return this._RedirectKeys; }
                set { this._RedirectKeys = value; }
            }

            private bool _RedirectDiskDrives = System.Convert.ToBoolean(Settings.Default.InhDefaultRedirectDiskDrives);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryRedirect", 7), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameRedirectDrives"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionRedirectDrives"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool RedirectDiskDrives
            {
                get { return this._RedirectDiskDrives; }
                set { this._RedirectDiskDrives = value; }
            }

            private bool _RedirectPrinters = System.Convert.ToBoolean(Settings.Default.InhDefaultRedirectPrinters);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryRedirect", 7), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameRedirectPrinters"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionRedirectPrinters"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool RedirectPrinters
            {
                get { return this._RedirectPrinters; }
                set { this._RedirectPrinters = value; }
            }

            private bool _RedirectPorts = System.Convert.ToBoolean(Settings.Default.InhDefaultRedirectPorts);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryRedirect", 7), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameRedirectPorts"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionRedirectPorts"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool RedirectPorts
            {
                get { return this._RedirectPorts; }
                set { this._RedirectPorts = value; }
            }

            private bool _RedirectSmartCards = System.Convert.ToBoolean(Settings.Default.InhDefaultRedirectSmartCards);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryRedirect", 7), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameRedirectSmartCards"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionRedirectSmartCards"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool RedirectSmartCards
            {
                get { return this._RedirectSmartCards; }
                set { this._RedirectSmartCards = value; }
            }

            private bool _RedirectSound = System.Convert.ToBoolean(Settings.Default.InhDefaultRedirectSound);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryRedirect", 7), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameRedirectSounds"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionRedirectSounds"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool RedirectSound
            {
                get { return this._RedirectSound; }
                set { this._RedirectSound = value; }
            }

            #endregion 7 Redirect

            #region 8 Misc

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryMiscellaneous", 8),
            Browsable(true),
            LocalizedAttributes.LocalizedDisplayName("strPropertyNameConnectOnStartup"),
            LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionConnectOnStartup"),
            TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool ConnectOnStartup { get; set; }

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryMiscellaneous", 8), Browsable(true), LocalizedAttributes.LocalizedDisplayName("strPropertyNameExternalToolBefore"), LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionExternalToolBefore"), TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool PreExtApp { get; set; }

            private bool _PostExtApp = System.Convert.ToBoolean(Settings.Default.InhDefaultPostExtApp);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryMiscellaneous", 8), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameExternalToolAfter"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionExternalToolAfter"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool PostExtApp
            {
                get { return this._PostExtApp; }
                set { this._PostExtApp = value; }
            }

            private bool _MacAddress = System.Convert.ToBoolean(Settings.Default.InhDefaultMacAddress);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryMiscellaneous", 8), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameMACAddress"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionMACAddress"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool MacAddress
            {
                get { return this._MacAddress; }
                set { this._MacAddress = value; }
            }

            private bool _UserField = System.Convert.ToBoolean(Settings.Default.InhDefaultUserField);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryMiscellaneous", 8), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameUser1"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionUser1"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool UserField
            {
                get { return this._UserField; }
                set { this._UserField = value; }
            }

            #endregion 8 Misc

            #region 9 VNC

            private bool _VNCCompression = System.Convert.ToBoolean(Settings.Default.InhDefaultVNCCompression);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryAppearance", 9), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameCompression"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionCompression"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool VNCCompression
            {
                get { return _VNCCompression; }
                set { _VNCCompression = value; }
            }

            private bool _VNCEncoding = System.Convert.ToBoolean(Settings.Default.InhDefaultVNCEncoding);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryAppearance", 9), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameEncoding"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionEncoding"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool VNCEncoding
            {
                get { return _VNCEncoding; }
                set { _VNCEncoding = value; }
            }

            private bool _VNCAuthMode = System.Convert.ToBoolean(Settings.Default.InhDefaultVNCAuthMode);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryConnection", 9), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameAuthenticationMode"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionAuthenticationMode"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool VNCAuthMode
            {
                get { return _VNCAuthMode; }
                set { _VNCAuthMode = value; }
            }

            private bool _VNCProxyType = System.Convert.ToBoolean(Settings.Default.InhDefaultVNCProxyType);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryMiscellaneous", 9), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameVNCProxyType"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionVNCProxyType"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool VNCProxyType
            {
                get { return _VNCProxyType; }
                set { _VNCProxyType = value; }
            }

            private bool _VNCProxyIP = System.Convert.ToBoolean(Settings.Default.InhDefaultVNCProxyIP);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryMiscellaneous", 9), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameVNCProxyAddress"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionVNCProxyAddress"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool VNCProxyIP
            {
                get { return _VNCProxyIP; }
                set { _VNCProxyIP = value; }
            }

            private bool _VNCProxyPort = System.Convert.ToBoolean(Settings.Default.InhDefaultVNCProxyPort);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryMiscellaneous", 9), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameVNCProxyPort"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionVNCProxyPort"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool VNCProxyPort
            {
                get { return _VNCProxyPort; }
                set { _VNCProxyPort = value; }
            }

            private bool _VNCProxyUsername = System.Convert.ToBoolean(Settings.Default.InhDefaultVNCProxyUsername);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryMiscellaneous", 9), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameVNCProxyUsername"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionVNCProxyUsername"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool VNCProxyUsername
            {
                get { return _VNCProxyUsername; }
                set { _VNCProxyUsername = value; }
            }

            private bool _VNCProxyPassword = System.Convert.ToBoolean(Settings.Default.InhDefaultVNCProxyPassword);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryMiscellaneous", 9), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameVNCProxyPassword"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionVNCProxyPassword"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool VNCProxyPassword
            {
                get { return _VNCProxyPassword; }
                set { _VNCProxyPassword = value; }
            }

            private bool _VNCColors = System.Convert.ToBoolean(Settings.Default.InhDefaultVNCColors);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryAppearance", 9), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameColors"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionColors"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool VNCColors
            {
                get { return _VNCColors; }
                set { _VNCColors = value; }
            }

            private bool _VNCSmartSizeMode = System.Convert.ToBoolean(Settings.Default.InhDefaultVNCSmartSizeMode);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryAppearance", 9), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameSmartSizeMode"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionSmartSizeMode"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool VNCSmartSizeMode
            {
                get { return _VNCSmartSizeMode; }
                set { _VNCSmartSizeMode = value; }
            }

            private bool _VNCViewOnly = System.Convert.ToBoolean(Settings.Default.InhDefaultVNCViewOnly);

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryAppearance", 9), Browsable(true),
             LocalizedAttributes.LocalizedDisplayName("strPropertyNameViewOnly"),
             LocalizedAttributes.LocalizedDescriptionInheritAttribute("strPropertyDescriptionViewOnly"),
             TypeConverter(typeof(Misc.YesNoTypeConverter))]
            public bool VNCViewOnly
            {
                get { return _VNCViewOnly; }
                set { _VNCViewOnly = value; }
            }

            #endregion 9 VNC
        }

        #endregion Inheritance

        [Flags()]
        public enum Force
        {
            None = 0,
            UseConsoleSession = 1,
            Fullscreen = 2,
            DoNotJump = 4,
            OverridePanel = 8,
            DontUseConsoleSession = 16
        }
    }
}