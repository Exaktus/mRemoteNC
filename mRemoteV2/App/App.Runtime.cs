using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using System.Xml;
using log4net;
using log4net.Config;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;
using mRemoteNC.App.Info;
using mRemoteNC.Config.Connections;
using mRemoteNC.Connection;
using mRemoteNC.Forms;
using mRemoteNC.Images;
using mRemoteNC.Messages;
using mRemoteNC.Protocol;
using mRemoteNC.Security;
using mRemoteNC.Tools;
using mRemoteNC.Tools.PortScan;
using mRemoteNC.Tree;
using mRemoteNC.UI.Window;
using My;
using My.Resources;
using WeifenLuo.WinFormsUI.Docking;
using Help = mRemoteNC.UI.Window.Help;
using Icon = mRemoteNC.Connection.Icon;
using Save = mRemoteNC.Security.Save;
using Settings = My.Settings;
using TabPage = Crownwood.Magic.Controls.TabPage;
using Timer = System.Timers.Timer;
using Type = mRemoteNC.UI.Window.Type;

//using System.Environment;

//using Timer = System.Timers.Timer;

namespace mRemoteNC
{
    namespace App
    {
        public static class Runtime
        {
            #region Public Properties

            public static Connection.List ConnectionList { get; set; }

            public static Connection.List PreviousConnectionList { get; set; }

            public static Container.List ContainerList { get; set; }

            public static Container.List PreviousContainerList { get; set; }

            public static Credential.List CredentialList { get; set; }

            public static Credential.List PreviousCredentialList { get; set; }

            public static UI.Window.List WindowList { get; set; }

            public static Collector MessageCollector { get; set; }

            public static Controls.NotificationAreaIcon NotificationAreaIcon { get; set; }

            public static SystemMenu SystemMenu { get; set; }

            public static ILog Log { get; set; }

            public static bool IsUpdateAvailable { get; set; }

            public static bool IsAnnouncementAvailable { get; set; }

            public static bool IsConnectionsFileLoaded { get; set; }

            private static Timer _timerSqlWatcher;

            public static Timer TimerSqlWatcher
            {
                get { return _timerSqlWatcher; }
                set
                {
                    _timerSqlWatcher = value;
                    _timerSqlWatcher.Elapsed += new ElapsedEventHandler(tmrSqlWatcher_Elapsed);
                }
            }

            public static DateTime LastSqlUpdate { get; set; }

            public static string LastSelected { get; set; }

            public static Connection.Info DefaultConnection { get; set; }

            public static Connection.Info.Inheritance DefaultInheritance { get; set; }

            private static ArrayList _externalTools = new ArrayList();

            public static ArrayList ExternalTools
            {
                get { return _externalTools; }
                set { _externalTools = value; }
            }

            public static List<QuickText> QuickTexts = new List<QuickText>();

            #endregion Public Properties

            #region Classes

            public class Windows
            {
                public static DockContent treePanel = new DockContent();
                public static UI.Window.Tree treeForm;
                public static DockContent configPanel = new DockContent();
                public static UI.Window.Config configForm;

                public static DockContent errorsPanel = new DockContent();
                public static ErrorsAndInfos errorsForm;

                public static DockContent sessionsPanel = new DockContent();
                public static Sessions sessionsForm;

                public static DockContent screenshotPanel = new DockContent();
                public static ScreenshotManager screenshotForm;
                public static UI.Window.QuickConnect quickyForm;
                public static DockContent quickyPanel = new DockContent();
                public static frmOptions optionsForm;
                public static DockContent optionsPanel = new DockContent();
                public static SaveAs saveasForm;
                public static DockContent saveasPanel = new DockContent();
                public static About aboutForm;
                public static DockContent aboutPanel = new DockContent();
                public static DockContent updatePanel = new DockContent();
                public static UI.Window.Update updateForm;
                public static SSHTransfer sshtransferForm;
                public static DockContent sshtransferPanel = new DockContent();
                public static ADImport adimportForm;
                public static DockContent adimportPanel = new DockContent();
                public static Help helpForm;
                public static DockContent helpPanel = new DockContent();
                public static ExternalApps externalappsForm;
                public static DockContent externalappsPanel = new DockContent();
                public static PortScan portscanForm;
                public static DockContent portscanPanel = new DockContent();
                public static UltraVNCSC ultravncscForm;
                public static DockContent ultravncscPanel = new DockContent();
                public static ComponentsCheck componentscheckForm;
                public static DockContent componentscheckPanel = new DockContent();
                public static UI.Window.Announcement AnnouncementForm;
                public static DockContent AnnouncementPanel = new DockContent();

                public static ConnectionStatusForm connectionStatusForm;
                public static DockContent connectionStatusPanel = new DockContent();
                public static QuickTextEdit quicktextForm;
                public static DockContent quicktextPanel = new DockContent();

                public static void Show(Type WindowType,
                                        PortScanMode PortScanMode = PortScanMode.Normal)
                {
                    try
                    {
                        switch (WindowType)
                        {
                            case Type.About:
                                if (aboutForm == null || aboutPanel == null | aboutPanel.VisibleState==DockState.Unknown)
                                {
                                    aboutForm = new About(aboutPanel);
                                    aboutPanel = aboutForm;
                                    aboutForm.Show(frmMain.Default.pnlDock);
                                }
                                else
                                {
                                    aboutPanel.Focus();
                                    aboutPanel.Show();
                                    aboutPanel.BringToFront();
                                    aboutForm.Focus();
                                }
                                
                                break;
                            case Type.ADImport:
                                adimportForm = new ADImport(adimportPanel);
                                adimportPanel = adimportForm;
                                adimportPanel.Show(frmMain.Default.pnlDock);
                                break;
                            case Type.Options:
                                optionsForm = new frmOptions(optionsPanel);
                                optionsForm.Show(frmMain.Default.pnlDock);
                                break;
                            case Type.SaveAs:
                                saveasForm = new SaveAs(saveasPanel);
                                saveasPanel = saveasForm;
                                saveasForm.Show(frmMain.Default.pnlDock);
                                break;
                            case Type.SSHTransfer:
                                sshtransferForm = new SSHTransfer(sshtransferPanel);
                                sshtransferPanel = sshtransferForm;
                                sshtransferForm.Show(frmMain.Default.pnlDock);
                                break;
                            case Type.Update:
                                updateForm = new UI.Window.Update(updatePanel);
                                updatePanel = updateForm;
                                updateForm.Show(frmMain.Default.pnlDock);
                                break;
                            case Type.Help:
                                helpForm = new Help(helpPanel);
                                helpPanel = helpForm;
                                helpForm.Show(frmMain.Default.pnlDock);
                                break;
                            case Type.ExternalApps:
                                externalappsForm = new ExternalApps(externalappsPanel);
                                externalappsPanel = externalappsForm;
                                externalappsForm.Show(frmMain.Default.pnlDock);
                                break;
                            case Type.PortScan:
                                portscanForm = new PortScan(portscanPanel, PortScanMode);
                                portscanPanel = portscanForm;
                                portscanForm.Show(frmMain.Default.pnlDock);
                                break;
                            case Type.UltraVNCSC:
                                ultravncscForm = new UltraVNCSC(ultravncscPanel);
                                ultravncscPanel = ultravncscForm;
                                ultravncscForm.Show(frmMain.Default.pnlDock);
                                break;
                            case Type.ComponentsCheck:
                                componentscheckForm = new ComponentsCheck(componentscheckPanel);
                                componentscheckPanel = componentscheckForm;
                                componentscheckForm.Show(frmMain.Default.pnlDock);
                                break;
                            case Type.Announcement:
                                AnnouncementForm = new UI.Window.Announcement(AnnouncementPanel);
                                AnnouncementPanel = AnnouncementForm;
                                AnnouncementForm.Show(frmMain.Default.pnlDock);
                                break;
                            case Type.ConnectionStatus:
                                connectionStatusForm = new ConnectionStatusForm();
                                componentscheckPanel = connectionStatusForm;
                                connectionStatusForm.Show(frmMain.Default.pnlDock);
                                break;
                            case Type.QuickText:

                                if (quicktextPanel != null && (quicktextForm == null || quicktextPanel == null | quicktextPanel.VisibleState == DockState.Unknown))
                                {
                                    quicktextForm = new QuickTextEdit(quicktextPanel);
                                    quicktextPanel = quicktextForm;
                                    quicktextForm.Show(frmMain.Default.pnlDock);
                                }
                                else
                                {
                                    quicktextPanel.Focus();
                                    quicktextPanel.Show();
                                    quicktextPanel.BringToFront();
                                    quicktextForm.Focus();
                                }
                                
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageCollector.AddMessage(MessageClass.ErrorMsg,
                                                    (string)
                                                    ("Show (Runtime.Windows) failed" +
                                                     Constants.vbNewLine + ex.Message), true);
                    }
                }

                public static void ShowUpdatesTab()
                {
                    optionsForm = new frmOptions(optionsPanel);
                    optionsForm.Show(frmMain.Default.pnlDock, 5);
                }
            }

            public class Screens
            {
                public static void SendFormToScreen(Screen Screen)
                {
                    bool wasMax = false; //TODO

                    if (frmMain.Default.WindowState == FormWindowState.Maximized)
                    {
                        wasMax = true;
                        frmMain.Default.WindowState = FormWindowState.Normal;
                    }

                    frmMain.Default.Location = Screen.Bounds.Location;

                    if (wasMax)
                    {
                        frmMain.Default.WindowState = FormWindowState.Maximized;
                    }
                }

                public static void SendPanelToScreen(DockContent Panel, Screen Screen)
                {
                    Panel.DockState = DockState.Float;
                    Panel.ParentForm.Left = Screen.Bounds.Location.X;
                    Panel.ParentForm.Top = Screen.Bounds.Location.Y;
                }
            }

            public class Startup
            {
                public static void CheckCompatibility()
                {
                    RegistryKey regKey;

                    bool isFipsPolicyEnabled = false;

                    // Windows XP/Windows Server 2003
                    regKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Control\\Lsa");
                    if (!(Convert.ToInt32(regKey.GetValue("FIPSAlgorithmPolicy")) == 0))
                    {
                        isFipsPolicyEnabled = true;
                    }

                    // Windows Vista/Windows Server 2008 and newer
                    regKey =
                        Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Control\\Lsa\\FIPSAlgorithmPolicy");
                    if (!(Convert.ToInt32(regKey.GetValue("Enabled")) == 0))
                    {
                        isFipsPolicyEnabled = true;
                    }

                    if (isFipsPolicyEnabled)
                    {
                        //TODO
                        MessageBox.Show(
                            string.Format(Language.strErrorFipsPolicyIncompatible,
                                          (new WindowsFormsApplicationBase()).
                                              Info.ProductName),
                            (new WindowsFormsApplicationBase()).Info.
                                ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(1);
                    }
                }

                public static void CreatePanels()
                {
                    Windows.connectionStatusForm = new ConnectionStatusForm(Windows.connectionStatusPanel);
                    Windows.connectionStatusPanel = Windows.connectionStatusForm;

                    Windows.configForm = new UI.Window.Config(Windows.configPanel);
                    Windows.configPanel = Windows.configForm;

                    Windows.treeForm = new UI.Window.Tree(Windows.treePanel);
                    Windows.treePanel = Windows.treeForm;
                    Node.TreeView = Windows.treeForm.tvConnections;

                    Windows.errorsForm = new ErrorsAndInfos(Windows.errorsPanel);
                    Windows.errorsPanel = Windows.errorsForm;

                    Windows.sessionsForm = new Sessions(Windows.sessionsPanel);
                    Windows.sessionsPanel = Windows.sessionsForm;

                    Windows.screenshotForm = new ScreenshotManager(Windows.screenshotPanel);
                    Windows.screenshotPanel = Windows.screenshotForm;

                    Windows.quickyForm = new UI.Window.QuickConnect(Windows.quickyPanel);
                    Windows.quickyPanel = Windows.quickyForm;

                    Windows.updateForm = new UI.Window.Update(Windows.updatePanel);
                    Windows.updatePanel = Windows.updateForm;

                    Windows.AnnouncementForm = new UI.Window.Announcement(Windows.AnnouncementPanel);
                    Windows.AnnouncementPanel = Windows.AnnouncementForm;
                }

                public static void SetDefaultLayout()
                {
                    frmMain.Default.pnlDock.Visible = false;

                    frmMain.Default.pnlDock.DockLeftPortion = 0.25;
                    frmMain.Default.pnlDock.DockRightPortion = 0.25;
                    frmMain.Default.pnlDock.DockTopPortion = 0.25;
                    frmMain.Default.pnlDock.DockBottomPortion = 0.25;

                    Windows.treePanel.Show(frmMain.Default.pnlDock, DockState.DockLeft);
                    Windows.configPanel.Show(frmMain.Default.pnlDock);
                    Windows.configPanel.DockTo(Windows.treePanel.Pane, DockStyle.Bottom, -1);

                    Windows.quickyPanel.Show(frmMain.Default.pnlDock, DockState.DockBottomAutoHide);
                    Windows.screenshotPanel.Show(Windows.quickyPanel.Pane, Windows.quickyPanel);
                    Windows.sessionsPanel.Show(Windows.quickyPanel.Pane, Windows.screenshotPanel);
                    Windows.errorsPanel.Show(Windows.quickyPanel.Pane, Windows.sessionsPanel);

                    Windows.screenshotForm.Hide();
                    Windows.quickyForm.Hide();

                    frmMain.Default.pnlDock.Visible = true;
                }

                public static void GetConnectionIcons()
                {
                    string iPath =
                        (new WindowsFormsApplicationBase()).Info.DirectoryPath +
                        "\\Icons\\";

                    if (Directory.Exists(iPath) == false)
                    {
                        return;
                    }

                    foreach (string f in Directory.GetFiles(iPath, "*.ico", SearchOption.AllDirectories))
                    {
                        FileInfo fInfo = new FileInfo(f);

                        Array.Resize(ref Icon.Icons, Convert.ToInt32(Icon.Icons.Length + 1));
                        Icon.Icons.SetValue(fInfo.Name.Replace(".ico", ""), Icon.Icons.Length - 1);
                    }
                }

                public static void GetPuttySessions()
                {
                    PuttySession.PuttySessions = (string[])PuttyBase.GetSessions();
                }

                public static void CreateLogger()
                {
                    XmlConfigurator.Configure(new FileInfo("mRemoteNC.exe.config"));
                    Log = LogManager.GetLogger("mRemoteNC.Log");
                    Log.InfoFormat("{0} started.",
                                   (new WindowsFormsApplicationBase()).Info.
                                       ProductName);
                    Log.InfoFormat("Command Line: {0}", Environment.GetCommandLineArgs());
                    try
                    {
                        int servicePack;
                        foreach (
                            ManagementObject managementObject in
                                new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem").Get())
                        {
                            servicePack =
                                Convert.ToInt32(managementObject.GetPropertyValue("ServicePackMajorVersion"));
                            if (servicePack == 0)
                            {
                                Log.InfoFormat("{0} {1}", managementObject.GetPropertyValue("Caption").ToString().Trim(),
                                               managementObject.GetPropertyValue("OSArchitecture"));
                            }
                            else
                            {
                                Log.InfoFormat("{0} Service Pack {1} {2}",
                                               managementObject.GetPropertyValue("Caption").ToString().Trim(),
                                               servicePack.ToString(),
                                               managementObject.GetPropertyValue("OSArchitecture"));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.WarnFormat("Error retrieving operating system information from WMI. {0}", ex.Message);
                    }
                    Log.InfoFormat("Microsoft .NET Framework {0}", Environment.Version.ToString());
#if !PORTABLE
                    Log.InfoFormat("{0} {1}", (new WindowsFormsApplicationBase()).Info.ProductName.ToString(),
                                   (new WindowsFormsApplicationBase()).Info.Version.ToString());
#else
                    Log.InfoFormat("{0} {1} {2}",
                                   (new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase()).Info.
                                       ProductName.ToString(),
                                   (new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase()).Info.
                                       Version.ToString(), Language.strLabelPortableEdition);
#endif
                    Log.InfoFormat("System Culture: {0}/{1}",
                                   Thread.CurrentThread.CurrentUICulture.Name,
                                   Thread.CurrentThread.CurrentUICulture.NativeName);
                }

                public static void UpdateCheck()
                {
                    if (Settings.Default.CheckForUpdatesAsked && Settings.Default.CheckForUpdatesOnStartup)
                    {
                        if (Settings.Default.UpdatePending ||
                            Settings.Default.CheckForUpdatesLastCheck <
                            DateTime.Now.Subtract(
                                TimeSpan.FromDays(Convert.ToDouble(Settings.Default.CheckForUpdatesFrequencyDays))))
                        {
                            frmMain.Default.tmrShowUpdate.Enabled = true;
                            Windows.updateForm.CheckForUpdate();
                            Windows.updateForm.UpdateCheckCompleted += UpdateCheckComplete;
                            //new System.EventHandler(UpdateCheckComplete);
                        }
                    }
                }

                private static void UpdateCheckComplete(bool UpdateAvailable)
                {
                    Settings.Default.CheckForUpdatesLastCheck = DateTime.Now;
                    Settings.Default.UpdatePending = UpdateAvailable;
                    IsUpdateAvailable = UpdateAvailable;
                }

                public static void AnnouncementCheck()
                {
                    if (Settings.Default.CheckForUpdatesAsked && Settings.Default.CheckForUpdatesOnStartup)
                    {
                        if (Settings.Default.CheckForUpdatesLastCheck <
                            DateTime.Now.Subtract(
                                TimeSpan.FromDays(Convert.ToDouble(Settings.Default.CheckForUpdatesFrequencyDays))))
                        {
                            frmMain.Default.tmrShowUpdate.Enabled = true;
                            Windows.AnnouncementForm.CheckForAnnouncement();
                            Windows.AnnouncementForm.AnnouncementCheckCompleted += AnnouncementCheckComplete;
                        }
                    }
                }

                private static void AnnouncementCheckComplete(bool AnnouncementAvailable)
                {
                    Settings.Default.CheckForUpdatesLastCheck = DateTime.Now;
                    IsAnnouncementAvailable = AnnouncementAvailable;
                }

                public static void ParseCommandLineArgs()
                {
                    try
                    {
                        Misc.CMDArguments cmd = new Misc.CMDArguments(Environment.GetCommandLineArgs());

                        string ConsParam = "";
                        if (cmd["cons"] != null)
                        {
                            ConsParam = "cons";
                        }
                        if (cmd["c"] != null)
                        {
                            ConsParam = "c";
                        }

                        string ResetPosParam = "";
                        if (cmd["resetpos"] != null)
                        {
                            ResetPosParam = "resetpos";
                        }
                        if (cmd["rp"] != null)
                        {
                            ResetPosParam = "rp";
                        }

                        string ResetPanelsParam = "";
                        if (cmd["resetpanels"] != null)
                        {
                            ResetPanelsParam = "resetpanels";
                        }
                        if (cmd["rpnl"] != null)
                        {
                            ResetPanelsParam = "rpnl";
                        }

                        string ResetToolbarsParam = "";
                        if (cmd["resettoolbar"] != null)
                        {
                            ResetToolbarsParam = "resettoolbar";
                        }
                        if (cmd["rtbr"] != null)
                        {
                            ResetToolbarsParam = "rtbr";
                        }

                        if (cmd["reset"] != null)
                        {
                            ResetPosParam = "rp";
                            ResetPanelsParam = "rpnl";
                            ResetToolbarsParam = "rtbr";
                        }

                        string NoReconnectParam = "";
                        if (cmd["noreconnect"] != null)
                        {
                            NoReconnectParam = "noreconnect";
                        }
                        if (cmd["norc"] != null)
                        {
                            NoReconnectParam = "norc";
                        }

                        if (ConsParam != "")
                        {
                            if (File.Exists(Convert.ToString(cmd[ConsParam])) == false)
                            {
                                if (
                                    File.Exists(
                                        (new WindowsFormsApplicationBase()).
                                            Info.DirectoryPath + "\\" + cmd[ConsParam]))
                                {
                                    Settings.Default.LoadConsFromCustomLocation = true;
                                    Settings.Default.CustomConsPath =
                                        (new WindowsFormsApplicationBase()).
                                            Info.DirectoryPath + "\\" + cmd[ConsParam];
                                    return;
                                }
                                else if (
                                    File.Exists(
                                        (string)
                                        (Connections.DefaultConnectionsPath + "\\" + cmd[ConsParam])))
                                {
                                    Settings.Default.LoadConsFromCustomLocation = true;
                                    Settings.Default.CustomConsPath = Connections.DefaultConnectionsPath +
                                                                      "\\" + cmd[ConsParam];
                                    return;
                                }
                            }
                            else
                            {
                                Settings.Default.LoadConsFromCustomLocation = true;
                                Settings.Default.CustomConsPath = cmd[ConsParam];
                                return;
                            }
                        }

                        if (ResetPosParam != "")
                        {
                            Settings.Default.MainFormKiosk = false;
                            Settings.Default.MainFormLocation = new Point(999, 999);
                            Settings.Default.MainFormSize = new Size(900, 600);
                            Settings.Default.MainFormState = FormWindowState.Normal;
                        }

                        if (ResetPanelsParam != "")
                        {
                            Settings.Default.ResetPanels = true;
                        }

                        if (NoReconnectParam != "")
                        {
                            Settings.Default.NoReconnect = true;
                        }

                        if (ResetToolbarsParam != "")
                        {
                            Settings.Default.ResetToolbars = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageCollector.AddMessage(MessageClass.ErrorMsg,
                                                    Language.strCommandLineArgsCouldNotBeParsed +
                                                    Constants.vbNewLine + ex.Message);
                    }
                }

                public static void CreateSQLUpdateHandlerAndStartTimer()
                {
                    if (Settings.Default.UseSQLServer == true)
                    {
                        Misc.SQLUpdateCheckFinished += SQLUpdateCheckFinished;
                        TimerSqlWatcher = new Timer(3000);
                        TimerSqlWatcher.Start();
                    }
                }

                public static void DestroySQLUpdateHandlerAndStopTimer()
                {
                    try
                    {
                        LastSqlUpdate = DateAndTime.Now;
                        Misc.SQLUpdateCheckFinished -= SQLUpdateCheckFinished;
                        if (TimerSqlWatcher != null)
                        {
                            TimerSqlWatcher.Stop();
                            TimerSqlWatcher.Close();
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            public class Shutdown
            {
                public static void Quit()
                {
                    frmMain.Default.Close();
                }

                public static void BeforeQuit()
                {
                    try
                    {
                        if (NotificationAreaIcon != null)
                        {
                            if (NotificationAreaIcon.Disposed == false)
                            {
                                NotificationAreaIcon.Dispose();
                            }
                        }

                        if (Settings.Default.SaveConsOnExit)
                        {
                            SaveConnections();
                        }

                        Config.SettingsManager.Save SettingsSave = new Config.SettingsManager.Save();
                        SettingsSave.SaveSettings();
                    }
                    catch (Exception ex)
                    {
                        MessageCollector.AddMessage(MessageClass.ErrorMsg,
                                                    Language.strSettingsCouldNotBeSavedOrTrayDispose +
                                                    Constants.vbNewLine + ex.Message, true);
                    }
                }
            }

            #endregion Classes

            #region Default Connection

            public static Connection.Info DefaultConnectionFromSettings()
            {
                DefaultConnection = new Connection.Info();
                DefaultConnection.IsDefault = true;

                return DefaultConnection;
            }

            public static void DefaultConnectionToSettings()
            {
                Settings.Default.ConDefaultDescription = DefaultConnection.Description;
                Settings.Default.ConDefaultIcon = DefaultConnection.Icon;
                Settings.Default.ConDefaultUsername = DefaultConnection.Username;
                Settings.Default.ConDefaultPassword = DefaultConnection.Password;
                Settings.Default.ConDefaultDomain = DefaultConnection.Domain;
                Settings.Default.ConDefaultProtocol = DefaultConnection.Protocol.ToString();
                Settings.Default.ConDefaultPuttySession = DefaultConnection.PuttySession;
                Settings.Default.ConDefaultICAEncryptionStrength = DefaultConnection.ICAEncryption.ToString();
                Settings.Default.ConDefaultRDPAuthenticationLevel = DefaultConnection.RDPAuthenticationLevel.ToString();
                Settings.Default.ConDefaultUseConsoleSession = DefaultConnection.UseConsoleSession;
                Settings.Default.ConDefaultUseCredSsp = DefaultConnection.UseCredSsp;
                Settings.Default.ConDefaultRenderingEngine = DefaultConnection.RenderingEngine.ToString();
                Settings.Default.ConDefaultResolution = DefaultConnection.Resolution.ToString();
                Settings.Default.ConDefaultColors = DefaultConnection.Colors.ToString();
                Settings.Default.ConDefaultCacheBitmaps = DefaultConnection.CacheBitmaps;
                Settings.Default.ConDefaultDisplayWallpaper = DefaultConnection.DisplayWallpaper;
                Settings.Default.ConDefaultDisplayThemes = DefaultConnection.DisplayThemes;
                Settings.Default.ConDefaultEnableFontSmoothing = DefaultConnection.EnableFontSmoothing;
                Settings.Default.ConDefaultEnableDesktopComposition = DefaultConnection.EnableDesktopComposition;
                Settings.Default.ConDefaultRedirectKeys = DefaultConnection.RedirectKeys;
                Settings.Default.ConDefaultRedirectDiskDrives = DefaultConnection.RedirectDiskDrives;
                Settings.Default.ConDefaultRedirectPrinters = DefaultConnection.RedirectPrinters;
                Settings.Default.ConDefaultRedirectPorts = DefaultConnection.RedirectPorts;
                Settings.Default.ConDefaultRedirectSmartCards = DefaultConnection.RedirectSmartCards;
                Settings.Default.ConDefaultRedirectSound = DefaultConnection.RedirectSound.ToString();
                Settings.Default.ConDefaultPreExtApp = DefaultConnection.PreExtApp;
                Settings.Default.ConDefaultPostExtApp = DefaultConnection.PostExtApp;
                Settings.Default.ConDefaultMacAddress = DefaultConnection.MacAddress;
                Settings.Default.ConDefaultUserField = DefaultConnection.UserField;
                Settings.Default.ConDefaultVNCAuthMode = DefaultConnection.VNCAuthMode.ToString();
                Settings.Default.ConDefaultVNCColors = DefaultConnection.VNCColors.ToString();
                Settings.Default.ConDefaultVNCCompression = DefaultConnection.VNCCompression.ToString();
                Settings.Default.ConDefaultVNCEncoding = DefaultConnection.VNCEncoding.ToString();
                Settings.Default.ConDefaultVNCProxyIP = DefaultConnection.VNCProxyIP;
                Settings.Default.ConDefaultVNCProxyPassword = DefaultConnection.VNCProxyPassword;
                Settings.Default.ConDefaultVNCProxyPort = DefaultConnection.VNCProxyPort;
                Settings.Default.ConDefaultVNCProxyType = DefaultConnection.VNCProxyType.ToString();
                Settings.Default.ConDefaultVNCProxyUsername = DefaultConnection.VNCProxyUsername;
                Settings.Default.ConDefaultVNCSmartSizeMode = DefaultConnection.VNCSmartSizeMode.ToString();
                Settings.Default.ConDefaultVNCViewOnly = DefaultConnection.VNCViewOnly;
                Settings.Default.ConDefaultExtApp = DefaultConnection.ExtApp;
                Settings.Default.ConDefaultRDGatewayUsageMethod = DefaultConnection.RDGatewayUsageMethod.ToString();
                Settings.Default.ConDefaultRDGatewayHostname = DefaultConnection.RDGatewayHostname;
                Settings.Default.ConDefaultRDGatewayUsername = DefaultConnection.RDGatewayUsername;
                Settings.Default.ConDefaultRDGatewayPassword = DefaultConnection.RDGatewayPassword;
                Settings.Default.ConDefaultRDGatewayDomain = DefaultConnection.RDGatewayDomain;
                Settings.Default.ConDefaultRDGatewayUseConnectionCredentials =
                    DefaultConnection.RDGatewayUseConnectionCredentials.ToString();
            }

            #endregion Default Connection

            #region Default Inheritance

            public static Connection.Info.Inheritance DefaultInheritanceFromSettings()
            {
                DefaultInheritance = new Connection.Info.Inheritance(null);
                DefaultInheritance.IsDefault = true;

                return DefaultInheritance;
            }

            public static void DefaultInheritanceToSettings()
            {
                Settings.Default.InhDefaultDescription = DefaultInheritance.Description;
                Settings.Default.InhDefaultIcon = DefaultInheritance.Icon;
                Settings.Default.InhDefaultPanel = DefaultInheritance.Panel;
                Settings.Default.InhDefaultUsername = DefaultInheritance.Username;
                Settings.Default.InhDefaultPassword = DefaultInheritance.Password;
                Settings.Default.InhDefaultDomain = DefaultInheritance.Domain;
                Settings.Default.InhDefaultProtocol = DefaultInheritance.Protocol;
                Settings.Default.InhDefaultPort = DefaultInheritance.Port;
                Settings.Default.InhDefaultPuttySession = DefaultInheritance.PuttySession;
                Settings.Default.InhDefaultUseConsoleSession = DefaultInheritance.UseConsoleSession;
                Settings.Default.InhDefaultUseCredSsp = DefaultInheritance.UseCredSsp;
                Settings.Default.InhDefaultRenderingEngine = DefaultInheritance.RenderingEngine;
                Settings.Default.InhDefaultICAEncryptionStrength = DefaultInheritance.ICAEncryption;
                Settings.Default.InhDefaultRDPAuthenticationLevel = DefaultInheritance.RDPAuthenticationLevel;
                Settings.Default.InhDefaultResolution = DefaultInheritance.Resolution;
                Settings.Default.InhDefaultColors = DefaultInheritance.Colors;
                Settings.Default.InhDefaultCacheBitmaps = DefaultInheritance.CacheBitmaps;
                Settings.Default.InhDefaultDisplayWallpaper = DefaultInheritance.DisplayWallpaper;
                Settings.Default.InhDefaultDisplayThemes = DefaultInheritance.DisplayThemes;
                Settings.Default.InhDefaultEnableFontSmoothing = DefaultInheritance.EnableFontSmoothing;
                Settings.Default.InhDefaultEnableDesktopComposition = DefaultInheritance.EnableDesktopComposition;
                Settings.Default.InhDefaultRedirectKeys = DefaultInheritance.RedirectKeys;
                Settings.Default.InhDefaultRedirectDiskDrives = DefaultInheritance.RedirectDiskDrives;
                Settings.Default.InhDefaultRedirectPrinters = DefaultInheritance.RedirectPrinters;
                Settings.Default.InhDefaultRedirectPorts = DefaultInheritance.RedirectPorts;
                Settings.Default.InhDefaultRedirectSmartCards = DefaultInheritance.RedirectSmartCards;
                Settings.Default.InhDefaultRedirectSound = DefaultInheritance.RedirectSound;
                Settings.Default.InhDefaultPreExtApp = DefaultInheritance.PreExtApp;
                Settings.Default.InhDefaultPostExtApp = DefaultInheritance.PostExtApp;
                Settings.Default.InhDefaultMacAddress = DefaultInheritance.MacAddress;
                Settings.Default.InhDefaultUserField = DefaultInheritance.UserField;
                // VNC inheritance
                Settings.Default.InhDefaultVNCAuthMode = DefaultInheritance.VNCAuthMode;
                Settings.Default.InhDefaultVNCColors = DefaultInheritance.VNCColors;
                Settings.Default.InhDefaultVNCCompression = DefaultInheritance.VNCCompression;
                Settings.Default.InhDefaultVNCEncoding = DefaultInheritance.VNCEncoding;
                Settings.Default.InhDefaultVNCProxyIP = DefaultInheritance.VNCProxyIP;
                Settings.Default.InhDefaultVNCProxyPassword = DefaultInheritance.VNCProxyPassword;
                Settings.Default.InhDefaultVNCProxyPort = DefaultInheritance.VNCProxyPort;
                Settings.Default.InhDefaultVNCProxyType = DefaultInheritance.VNCProxyType;
                Settings.Default.InhDefaultVNCProxyUsername = DefaultInheritance.VNCProxyUsername;
                Settings.Default.InhDefaultVNCSmartSizeMode = DefaultInheritance.VNCSmartSizeMode;
                Settings.Default.InhDefaultVNCViewOnly = DefaultInheritance.VNCViewOnly;
                // Ext. App inheritance
                Settings.Default.InhDefaultExtApp = DefaultInheritance.ExtApp;
                // RDP gateway inheritance
                Settings.Default.InhDefaultRDGatewayUsageMethod = DefaultInheritance.RDGatewayUsageMethod;
                Settings.Default.InhDefaultRDGatewayHostname = DefaultInheritance.RDGatewayHostname;
                Settings.Default.InhDefaultRDGatewayUsername = DefaultInheritance.RDGatewayUsername;
                Settings.Default.InhDefaultRDGatewayPassword = DefaultInheritance.RDGatewayPassword;
                Settings.Default.InhDefaultRDGatewayDomain = DefaultInheritance.RDGatewayDomain;
                Settings.Default.InhDefaultRDGatewayUseConnectionCredentials =
                    DefaultInheritance.RDGatewayUseConnectionCredentials;
            }

            #endregion Default Inheritance

            #region Panels

            public static Form AddPanel(string title = "", bool noTabber = false)
            {
                try
                {
                    if (title == "")
                    {
                        title = Language.strNewPanel;
                    }

                    DockContent pnlcForm = new DockContent();
                    UI.Window.Connection cForm = new UI.Window.Connection(pnlcForm);
                    pnlcForm = cForm;

                    //create context menu
                    ContextMenuStrip cMen = new ContextMenuStrip();

                    //create rename item
                    ToolStripMenuItem cMenRen = new ToolStripMenuItem();
                    cMenRen.Text = Language.strRename;
                    cMenRen.Image = Resources.Rename;
                    cMenRen.Tag = pnlcForm;
                    cMenRen.Click += new EventHandler(cMenConnectionPanelRename_Click);

                    ToolStripMenuItem cMenScreens = new ToolStripMenuItem();
                    cMenScreens.Text = Language.strSendTo;
                    cMenScreens.Image = Resources.Monitor;
                    cMenScreens.Tag = pnlcForm;
                    cMenScreens.DropDownItems.Add("Dummy");
                    cMenScreens.DropDownOpening += new EventHandler(cMenConnectionPanelScreens_DropDownOpening);

                    cMen.Items.AddRange(new ToolStripMenuItem[] { cMenRen, cMenScreens });

                    pnlcForm.TabPageContextMenuStrip = cMen;

                    cForm.SetFormText(title.Replace("&", "&&"));
                    
                    //ToDo: Fix this
                    try
                    {
                        frmMain.Default.pnlDock.DocumentStyle = frmMain.Default.pnlDock.DocumentsCount > 1 ? DocumentStyle.DockingMdi : DocumentStyle.DockingSdi;
                        pnlcForm.Show(frmMain.Default.pnlDock, DockState.Document);
                    }
                    catch (Exception)
                    {
                        frmMain.Default.pnlDock.DocumentStyle = DocumentStyle.DockingSdi;
                        pnlcForm.Show(frmMain.Default.pnlDock, DockState.Document);
                    }

                    

                    if (noTabber)
                    {
                        cForm.TabController.Dispose();
                    }
                    else
                    {
                        WindowList.Add(cForm);
                    }

                    return cForm;
                }
                catch (Exception ex)
                {
                    MessageCollector.AddMessage(MessageClass.ErrorMsg,
                                                (string)
                                                ("Couldn\'t add panel" + Constants.vbNewLine + ex.Message));
                    return null;
                }
            }

            private static void cMenConnectionPanelRename_Click(Object sender, EventArgs e)
            {
                try
                {
                    UI.Window.Connection conW =
                        (UI.Window.Connection)
                        NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null);
                    string nTitle = Interaction.InputBox(Language.strNewTitle + ":", "",
                                                         Conversions.ToString(
                                                             NewLateBinding.LateGet(
                                                                 NewLateBinding.LateGet(
                                                                     NewLateBinding.LateGet(sender, null, "Tag",
                                                                                            new object[0], null, null,
                                                                                            null), null, "Text",
                                                                     new object[0], null, null, null), null, "Replace",
                                                                 new object[] { "&&", "&" }, null, null, null)), -1, -1);
                    if (nTitle != "")
                    {
                        conW.SetFormText(nTitle.Replace("&", "&&"));
                    }
                }
                catch (Exception exception1)
                {
                    ProjectData.SetProjectError(exception1);
                    Exception ex = exception1;
                    MessageCollector.AddMessage(MessageClass.ErrorMsg, "Couldn't rename panel\r\n" + ex.Message, false);
                    ProjectData.ClearProjectError();
                }
            }

            private static void cMenConnectionPanelScreens_DropDownOpening(Object sender, EventArgs e)
            {
                try
                {
                    ToolStripMenuItem cMenScreens = (ToolStripMenuItem)sender;
                    cMenScreens.DropDownItems.Clear();

                    for (int i = 0; i <= Screen.AllScreens.Length - 1; i++)
                    {
                        ToolStripMenuItem cMenScreen =
                            new ToolStripMenuItem(Language.strScreen + " " + (i.ToString() + 1));
                        cMenScreen.Tag = new ArrayList();
                        cMenScreen.Image = Resources.Monitor_GoTo;
                        (cMenScreen.Tag as ArrayList).Add(Screen.AllScreens[i]);
                        (cMenScreen.Tag as ArrayList).Add(cMenScreens.Tag);
                        cMenScreen.Click += new EventHandler(cMenConnectionPanelScreen_Click);

                        cMenScreens.DropDownItems.Add(cMenScreen);
                    }
                }
                catch (Exception ex)
                {
                    MessageCollector.AddMessage(MessageClass.ErrorMsg,
                                                (string)
                                                ("Couldn\'t enumerate screens" + Constants.vbNewLine +
                                                 ex.Message));
                }
            }

            private static void cMenConnectionPanelScreen_Click(object sender, EventArgs e)
            {
                try
                {
                    Screen screen =
                        (Screen)NewLateBinding.LateIndexGet((sender as ToolStripMenuItem).Tag, new object[] { 0 }, null);
                    DockContent panel =
                        (DockContent)
                        NewLateBinding.LateIndexGet((sender as ToolStripMenuItem).Tag, new object[] { 1 }, null);
                    Screens.SendPanelToScreen(panel, screen);
                }
                catch (Exception exception1)
                {
                    ProjectData.SetProjectError(exception1);
                    Exception ex = exception1;
                    ProjectData.ClearProjectError();
                }
            }

            #endregion Panels

            #region Credential Loading/Saving

            public static void LoadCredentials()
            {
            }

            #endregion Credential Loading/Saving

            #region Connections Loading/Saving

            public static void NewConnections(string filename)
            {
                try
                {
                    ConnectionList = new Connection.List();
                    ContainerList = new Container.List();

                    Load conL = new Load();

                    Settings.Default.LoadConsFromCustomLocation = false;

                    Directory.CreateDirectory(Path.GetDirectoryName(filename));
                    XmlTextWriter xW = new XmlTextWriter(filename, Encoding.UTF8);
                    xW.Formatting = Formatting.Indented;
                    xW.Indentation = 4;

                    xW.WriteStartDocument();
                    xW.WriteStartElement("Connections"); // Do not localize
                    xW.WriteAttributeString("Name", Language.strConnections);
                    xW.WriteAttributeString("Export", "", "False");
                    xW.WriteAttributeString("Protected", "",
                                            "GiUis20DIbnYzWPcdaQKfjE2H5jh//L5v4RGrJMGNXuIq2CttB/d/BxaBP2LwRhY");
                    xW.WriteAttributeString("ConfVersion", "", "2.4");

                    xW.WriteEndElement();
                    xW.WriteEndDocument();

                    xW.Close();

                    conL.ConnectionList = ConnectionList;
                    conL.ContainerList = ContainerList;
                    conL.Import = false;

                    Node.ResetTree();

                    conL.RootTreeNode = Windows.treeForm.tvConnections.Nodes[0];

                    // Load config
                    conL.ConnectionFileName = filename;
                    conL.LoadConnections();

                    Windows.treeForm.tvConnections.SelectedNode = conL.RootTreeNode;
                }
                catch (Exception ex)
                {
                    MessageCollector.AddMessage(MessageClass.ErrorMsg,
                                                Language.strCouldNotCreateNewConnectionsFile +
                                                Constants.vbNewLine + ex.Message);
                }
            }

            private static void LoadConnectionsBG(bool WithDialog = false, bool Update = false)
            {
                _withDialog = false;
                _loadUpdate = true;

                Thread t = new Thread(LoadConnectionsBGd);
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
            }

            private static bool _withDialog = false;
            private static bool _loadUpdate = false;

            private static void LoadConnectionsBGd()
            {
                LoadConnections(_withDialog, _loadUpdate);
            }

            public static void LoadConnections(bool WithDialog = false, bool Update = false)
            {
                Load conL = new Load();

                try
                {
                    bool tmrWasEnabled = false;
                    if (TimerSqlWatcher != null)
                    {
                        tmrWasEnabled = TimerSqlWatcher.Enabled;

                        if (TimerSqlWatcher.Enabled == true)
                        {
                            TimerSqlWatcher.Stop();
                        }
                    }

                    if (ConnectionList != null && ContainerList != null)
                    {
                        PreviousConnectionList = ConnectionList.Copy();
                        //PreviousContainerList = ContainerList.Copy;
                    }

                    ConnectionList = new Connection.List();
                    ContainerList = new Container.List();

                    if (Settings.Default.UseSQLServer == false)
                    {
                        if (WithDialog)
                        {
                            OpenFileDialog lD = Controls.ConnectionsLoadDialog();

                            if (lD.ShowDialog() == DialogResult.OK)
                            {
                                conL.ConnectionFileName = lD.FileName;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            conL.ConnectionFileName = GetStartupConnectionFileName();
                        }

                        if (File.Exists((string)conL.ConnectionFileName) == false)
                        {
                            if (WithDialog)
                            {
                                MessageCollector.AddMessage(MessageClass.WarningMsg,
                                                            string.Format(
                                                                Language.strConnectionsFileCouldNotBeLoaded,
                                                                conL.ConnectionFileName));
                            }
                            else
                            {
                                MessageCollector.AddMessage(MessageClass.InformationMsg,
                                                            string.Format(
                                                                Language.
                                                                    strConnectionsFileCouldNotBeLoadedNew,
                                                                conL.ConnectionFileName));
                                NewConnections(conL.ConnectionFileName);
                            }

                            return;
                        }

                        CreateBackupFile((string)conL.ConnectionFileName);
                    }

                    conL.ConnectionList = ConnectionList;
                    conL.ContainerList = ContainerList;

                    if (PreviousConnectionList != null && PreviousContainerList != null)
                    {
                        conL.PreviousConnectionList = PreviousConnectionList;
                        conL.PreviousContainerList = PreviousContainerList;
                    }

                    if (Update == true)
                    {
                        conL.PreviousSelected = LastSelected;
                    }

                    conL.Import = false;

                    Node.ResetTree();

                    conL.RootTreeNode = Windows.treeForm.tvConnections.Nodes[0];

                    conL.UseSQL = Settings.Default.UseSQLServer;
                    conL.SQLHost = Settings.Default.SQLHost;
                    conL.SQLDatabaseName = Settings.Default.SQLDatabaseName;
                    conL.SQLUsername = Settings.Default.SQLUser;
                    conL.SQLPassword = Crypt.Decrypt((string)Settings.Default.SQLPass,
                                                     (string)General.EncryptionKey);
                    conL.SQLUpdate = Update;

                    conL.LoadConnections();

                    if (Settings.Default.UseSQLServer == true)
                    {
                        LastSqlUpdate = DateTime.Now;
                    }
                    else
                    {
                        if (conL.ConnectionFileName ==
                            Connections.DefaultConnectionsPath + "\\" +
                            Connections.DefaultConnectionsFile)
                        {
                            Settings.Default.LoadConsFromCustomLocation = false;
                        }
                        else
                        {
                            Settings.Default.LoadConsFromCustomLocation = true;
                            Settings.Default.CustomConsPath = conL.ConnectionFileName;
                        }
                    }

                    if (tmrWasEnabled && TimerSqlWatcher != null)
                    {
                        TimerSqlWatcher.Start();
                    }
                }
                catch (Exception ex)
                {
                    MessageCollector.AddMessage(MessageClass.ErrorMsg,
                                                string.Format(
                                                    Language.strConnectionsFileCouldNotBeLoaded +
                                                    Constants.vbNewLine + ex.Message + ex.StackTrace,
                                                    conL.ConnectionFileName));
                    if (Settings.Default.UseSQLServer == false)
                    {
                        if (conL.ConnectionFileName != GetStartupConnectionFileName())
                        {
                            LoadConnections();
                            return;
                        }
                        else
                        {
                            Interaction.MsgBox(
                                string.Format(Language.strErrorStartupConnectionFileLoad, Constants.vbNewLine,
                                              Application.ProductName, GetStartupConnectionFileName(), ex.Message),
                                MsgBoxStyle.OkOnly, null);
                            Application.Exit();
                        }
                    }
                }
            }

            internal static void CreateBackupFile(string fileName)
            {
                // This intentionally doesn't prune any existing backup files. We just assume the user doesn't want any new ones created.
                if (Settings.Default.BackupFileKeepCount == 0)
                {
                    return;
                }

                try
                {
                    string backupFileName = string.Format(Settings.Default.BackupFileNameFormat, fileName,
                                                          DateTime.UtcNow).Trim();
                    File.Copy(fileName, backupFileName);
                    PruneBackupFiles(fileName);
                }
                catch (Exception ex)
                {
                    MessageCollector.AddMessage(MessageClass.WarningMsg,
                                                Language.strConnectionsFileBackupFailed +
                                                Constants.vbNewLine + Constants.vbNewLine + ex.Message);
                    throw;
                }
            }

            internal static void PruneBackupFiles(string baseName)
            {
                string fileName = Path.GetFileName(baseName);
                string directoryName = Path.GetDirectoryName(baseName);

                if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(directoryName))
                {
                    return;
                }

                string searchPattern = string.Format(Settings.Default.BackupFileNameFormat.Trim(), fileName, "*");
                string[] files = Directory.GetFiles(directoryName, searchPattern);

                if (files.Length <= Settings.Default.BackupFileKeepCount)
                {
                    return;
                }

                Array.Sort(files);
                Array.Resize(ref files, files.Length - Settings.Default.BackupFileKeepCount);

                foreach (string file in files)
                {
                    File.Delete(file);
                }
            }

            internal static string GetStartupConnectionFileName()
            {
                string fileName = "";

                if (Settings.Default.LoadConsFromCustomLocation == false)
                {
                    string oldPath =
                        (string)
                        (Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" +
                         (new WindowsFormsApplicationBase()).Info.ProductName +
                         "\\" + Connections.DefaultConnectionsFile);
                    string newPath =
                        (string)
                        (Connections.DefaultConnectionsPath + "\\" +
                         Connections.DefaultConnectionsFile);
#if !PORTABLE
                    fileName = File.Exists(oldPath) ? oldPath : newPath;
#else
                    fileName = newPath;
#endif
                }
                else
                {
                    fileName = (string)Settings.Default.CustomConsPath;
                }

                return fileName;
            }

            public static void ImportConnections()
            {
                try
                {
                    OpenFileDialog lD = Controls.ConnectionsLoadDialog();
                    lD.Multiselect = true;

                    if (lD.ShowDialog() == DialogResult.OK)
                    {
                        TreeNode nNode = null;

                        for (int i = 0; i <= lD.FileNames.Length - 1; i++)
                        {
                            nNode = Node.AddNode(Node.Type.Container, "Import #" + i.ToString());

                            Container.Info nContI = new Container.Info();
                            nContI.TreeNode = nNode;
                            nContI.ConnectionInfo = new Connection.Info(nContI);

                            if (Node.SelectedNode != null)
                            {
                                if (Node.GetNodeType(Node.SelectedNode) == Node.Type.Container)
                                {
                                    nContI.Parent = Node.SelectedNode.Tag;
                                }
                            }

                            nNode.Tag = nContI;
                            ContainerList.Add(nContI);

                            Load conL = new Load
                                            {
                                                ConnectionFileName = lD.FileNames[i],
                                                RootTreeNode = nNode,
                                                Import = true,
                                                ConnectionList = ConnectionList,
                                                ContainerList = ContainerList
                                            };

                            conL.LoadConnections();

                            Windows.treeForm.tvConnections.SelectedNode.Nodes.Add(nNode);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageCollector.AddMessage(MessageClass.ErrorMsg,
                                                Language.strConnectionsFileCouldNotBeImported +
                                                Constants.vbNewLine + ex.Message);
                }
            }

            public static void ImportConnectionsFromRDPFiles()
            {
                try
                {
                    OpenFileDialog lD = Controls.ConnectionsRDPImportDialog();
                    lD.Multiselect = true;

                    if (lD.ShowDialog() == DialogResult.OK)
                    {
                        for (int i = 0; i <= lD.FileNames.Length - 1; i++)
                        {
                            string[] lines = File.ReadAllLines(lD.FileNames[i]);

                            TreeNode nNode = Node.AddNode(Node.Type.Connection,
                                                          Path.GetFileNameWithoutExtension(lD.FileNames[i]));

                            Connection.Info nConI = new Connection.Info();
                            nConI.Inherit = new Connection.Info.Inheritance(nConI);

                            nConI.Name = nNode.Text;

                            foreach (string l in lines)
                            {
                                string pName = l.Substring(0, l.IndexOf(":"));
                                string pValue = l.Substring(Convert.ToInt32(l.LastIndexOf(":") + 1));

                                switch (pName.ToLower())
                                {
                                    case "full address":
                                        nConI.Hostname = pValue;
                                        break;
                                    case "server port":
                                        nConI.Port = Convert.ToInt32(pValue);
                                        break;
                                    case "username":
                                        nConI.Username = pValue;
                                        break;
                                    case "domain":
                                        nConI.Domain = pValue;
                                        break;
                                    case "session bpp":
                                        switch (Convert.ToInt32(pValue))
                                        {
                                            case 8:
                                                nConI.Colors = RDP.RDPColors.Colors256;
                                                break;
                                            case 15:
                                                nConI.Colors = RDP.RDPColors.Colors15Bit;
                                                break;
                                            case 16:
                                                nConI.Colors = RDP.RDPColors.Colors16Bit;
                                                break;
                                            case 24:
                                                nConI.Colors = RDP.RDPColors.Colors24Bit;
                                                break;
                                            case 32:
                                                nConI.Colors = RDP.RDPColors.Colors32Bit;
                                                break;
                                        }
                                        break;
                                    case "bitmapcachepersistenable":
                                        nConI.CacheBitmaps = pValue == "1";
                                        break;
                                    case "screen mode id":
                                        nConI.Resolution = pValue == "2"
                                                               ? RDP.RDPResolutions.Fullscreen
                                                               : RDP.RDPResolutions.FitToWindow;
                                        break;
                                    case "connect to console":
                                        if (pValue == "1")
                                        {
                                            nConI.UseConsoleSession = true;
                                        }
                                        break;
                                    case "disable wallpaper":
                                        nConI.DisplayWallpaper = pValue == "1";
                                        break;
                                    case "disable themes":
                                        nConI.DisplayThemes = pValue == "1";
                                        break;
                                    case "allow font smoothing":
                                        nConI.EnableFontSmoothing = pValue == "1";
                                        break;
                                    case "allow desktop composition":
                                        nConI.EnableDesktopComposition = pValue == "1";
                                        break;
                                    case "redirectsmartcards":
                                        nConI.RedirectSmartCards = pValue == "1";
                                        break;
                                    case "redirectdrives":
                                        nConI.RedirectDiskDrives = pValue == "1";
                                        break;
                                    case "redirectcomports":
                                        nConI.RedirectPorts = pValue == "1";
                                        break;
                                    case "redirectprinters":
                                        nConI.RedirectPrinters = pValue == "1";
                                        break;
                                    case "audiomode":
                                        switch (Convert.ToInt32(pValue))
                                        {
                                            case 0:
                                                nConI.RedirectSound =
                                                    RDP.RDPSounds.BringToThisComputer;
                                                break;
                                            case 1:
                                                nConI.RedirectSound =
                                                    RDP.RDPSounds.LeaveAtRemoteComputer;
                                                break;
                                            case 2:
                                                nConI.RedirectSound = RDP.RDPSounds.DoNotPlay;
                                                break;
                                        }
                                        break;
                                }
                            }

                            nNode.Tag = nConI;
                            Windows.treeForm.tvConnections.SelectedNode.Nodes.Add(nNode);

                            if (Node.GetNodeType(nNode.Parent) == Node.Type.Container)
                            {
                                nConI.Parent = nNode.Parent.Tag;
                            }

                            ConnectionList.Add(nConI);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageCollector.AddMessage(MessageClass.ErrorMsg,
                                                Language.strRdpFileCouldNotBeImported + Constants.vbNewLine +
                                                Constants.vbNewLine + ex.Message);
                }
            }

            public static void ImportConnectionsFromPortScan(ArrayList Hosts, Protocols Protocol)
            {
                foreach (ScanHost Host in Hosts)
                {
                    Protocols finalProt = Protocols.NONE;
                    bool protOK = false;

                    TreeNode nNode = Node.AddNode(Node.Type.Connection, (string)Host.HostNameWithoutDomain);

                    Connection.Info nConI = new Connection.Info();
                    nConI.Inherit = new Connection.Info.Inheritance(nConI);

                    nConI.Name = Host.HostNameWithoutDomain;
                    nConI.Hostname = Host.HostName;

                    switch (Protocol)
                    {
                        case Protocols.SSH2:
                            if (Host.SSH)
                            {
                                finalProt = Protocols.SSH2;
                                protOK = true;
                            }
                            break;
                        case Protocols.Telnet:
                            if (Host.Telnet)
                            {
                                finalProt = Protocols.Telnet;
                                protOK = true;
                            }
                            break;
                        case Protocols.HTTP:
                            if (Host.HTTP)
                            {
                                finalProt = Protocols.HTTP;
                                protOK = true;
                            }
                            break;
                        case Protocols.HTTPS:
                            if (Host.HTTPS)
                            {
                                finalProt = Protocols.HTTPS;
                                protOK = true;
                            }
                            break;
                        case Protocols.Rlogin:
                            if (Host.Rlogin)
                            {
                                finalProt = Protocols.Rlogin;
                                protOK = true;
                            }
                            break;
                        case Protocols.Serial:
                            if (Host.Serial)
                            {
                                finalProt = Protocols.Serial;
                                protOK = true;
                            }
                            break;
                        case Protocols.RDP:
                            if (Host.RDP)
                            {
                                finalProt = Protocols.RDP;
                                protOK = true;
                            }
                            break;
                        case Protocols.VNC:
                            if (Host.VNC)
                            {
                                finalProt = Protocols.VNC;
                                protOK = true;
                            }
                            break;
                    }

                    if (protOK == false)
                    {
                        nConI = null;
                    }
                    else
                    {
                        nConI.Protocol = finalProt;
                        nConI.SetDefaultPort();

                        nNode.Tag = nConI;
                        Windows.treeForm.tvConnections.SelectedNode.Nodes.Add(nNode);

                        if (Node.GetNodeType(nNode.Parent) == Node.Type.Container)
                        {
                            nConI.Parent = nNode.Parent.Tag;
                        }

                        ConnectionList.Add(nConI);
                    }
                }
            }

            public static void ImportConnectionsFromCSV()
            {
            }

            public static void SaveConnectionsBG()
            {
                _saveUpdate = true;

                Thread t = new Thread(new ThreadStart(SaveConnectionsBGd));
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
            }

            private static bool _saveUpdate = false;
            private static object _saveLock = new object();

            private static void SaveConnectionsBGd()
            {
                Monitor.Enter(_saveLock);
                SaveConnections(_saveUpdate);
                Monitor.Exit(_saveLock);
            }

            public static void SaveConnections(bool Update = false)
            {
                try
                {
                    if (Update == true && Settings.Default.UseSQLServer == false)
                    {
                        return;
                    }

                    bool tmrWasEnabled = false;

                    if (TimerSqlWatcher != null)
                    {
                        tmrWasEnabled = TimerSqlWatcher.Enabled;
                        if (TimerSqlWatcher.Enabled == true)
                        {
                            TimerSqlWatcher.Stop();
                        }
                    }

                    Config.Connections.Save conS = new Config.Connections.Save();

                    if (Settings.Default.UseSQLServer == false)
                    {
                        if (Settings.Default.LoadConsFromCustomLocation == false)
                        {
                            conS.ConnectionFileName =
                                (string)
                                (Connections.DefaultConnectionsPath + "\\" +
                                 Connections.DefaultConnectionsFile);
                        }
                        else
                        {
                            conS.ConnectionFileName = (string)Settings.Default.CustomConsPath;
                        }
                    }

                    conS.ConnectionList = ConnectionList;
                    conS.ContainerList = ContainerList;
                    conS.Export = false;
                    conS.SaveSecurity = new Save(false);
                    conS.RootTreeNode = Windows.treeForm.tvConnections.Nodes[0];

                    if (Settings.Default.UseSQLServer == true)
                    {
                        conS.SaveFormat = Config.Connections.Save.Format.SQL;
                        conS.SQLHost = (string)Settings.Default.SQLHost;
                        conS.SQLDatabaseName = (string)Settings.Default.SQLDatabaseName;
                        conS.SQLUsername = (string)Settings.Default.SQLUser;
                        conS.SQLPassword = Crypt.Decrypt((string)Settings.Default.SQLPass,
                                                         (string)General.EncryptionKey);
                    }

                    conS.SaveConnections();

                    if (Settings.Default.UseSQLServer == true)
                    {
                        LastSqlUpdate = DateTime.Now;
                    }

                    if (tmrWasEnabled)
                    {
                        TimerSqlWatcher.Start();
                    }
                }
                catch (Exception ex)
                {
                    MessageCollector.AddMessage(MessageClass.ErrorMsg,
                                                Language.strConnectionsFileCouldNotBeSaved +
                                                Constants.vbNewLine + ex.Message);
                }
            }

            public static void SaveConnectionsAs(Save SaveSecurity, TreeNode RootNode)
            {
                Config.Connections.Save conS = new Config.Connections.Save();
                try
                {
                    bool tmrWasEnabled;

                    if (TimerSqlWatcher != null)
                    {
                        tmrWasEnabled = TimerSqlWatcher.Enabled;
                        if (TimerSqlWatcher.Enabled == true)
                        {
                            TimerSqlWatcher.Stop();
                        }
                    }

                    SaveFileDialog sD = Controls.ConnectionsSaveAsDialog();

                    if (sD.ShowDialog() == DialogResult.OK)
                    {
                        conS.ConnectionFileName = sD.FileName;
                    }
                    else
                    {
                        return;
                    }

                    switch (sD.FilterIndex)
                    {
                        case 1:
                            conS.SaveFormat = Config.Connections.Save.Format.mRXML;
                            break;
                        case 2:
                            conS.SaveFormat = Config.Connections.Save.Format.mRCSV;
                            break;
                        case 3:
                            conS.SaveFormat = Config.Connections.Save.Format.vRDCSV;
                            break;
                    }

                    if (RootNode == Windows.treeForm.tvConnections.Nodes[0])
                    {
                        if (conS.SaveFormat != Config.Connections.Save.Format.mRXML &&
                            conS.SaveFormat != Config.Connections.Save.Format.None)
                        {
                        }
                        else
                        {
                            if (conS.ConnectionFileName ==
                                Connections.DefaultConnectionsPath + "\\" +
                                Connections.DefaultConnectionsFile)
                            {
                                Settings.Default.LoadConsFromCustomLocation = false;
                            }
                            else
                            {
                                Settings.Default.LoadConsFromCustomLocation = true;
                                Settings.Default.CustomConsPath = conS.ConnectionFileName;
                            }
                        }
                    }

                    conS.ConnectionList = ConnectionList;
                    conS.ContainerList = ContainerList;
                    if (RootNode != Windows.treeForm.tvConnections.Nodes[0])
                    {
                        conS.Export = true;
                    }
                    conS.SaveSecurity = SaveSecurity;
                    conS.RootTreeNode = RootNode;

                    conS.SaveConnections();
                }
                catch (Exception ex)
                {
                    MessageCollector.AddMessage(MessageClass.ErrorMsg,
                                                (string)
                                                (string.Format(Language.strConnectionsFileCouldNotSaveAs,
                                                               conS.ConnectionFileName) +
                                                 Constants.vbNewLine + ex.Message));
                }
            }

            #endregion Connections Loading/Saving

            #region Opening Connection

            public static Connection.Info CreateQuicky(string ConString,
                                                       Protocols Protocol = Protocols.NONE)
            {
                try
                {
                    Uri Uri = new Uri((string)("dummyscheme" + Uri.SchemeDelimiter + ConString));

                    if (!string.IsNullOrEmpty(Uri.Host))
                    {
                        Connection.Info newConnectionInfo = new Connection.Info();

                        newConnectionInfo.Name = string.Format(Language.strQuick, Uri.Host);
                        newConnectionInfo.Protocol = Protocol;
                        newConnectionInfo.Hostname = Uri.Host;
                        if (Uri.Port == -1)
                        {
                            newConnectionInfo.Port = 0;
                        }
                        else
                        {
                            newConnectionInfo.Port = Uri.Port;
                        }
                        newConnectionInfo.IsQuicky = true;

                        Windows.quickyForm.ConnectionInfo = newConnectionInfo;

                        if (Protocol == Protocols.NONE)
                        {
                            Windows.quickyPanel.Show(frmMain.Default.pnlDock, DockState.DockBottomAutoHide);
                        }

                        return newConnectionInfo;
                    }
                }
                catch (Exception ex)
                {
                    MessageCollector.AddMessage(MessageClass.ErrorMsg,
                                                Language.strQuickConnectFailed + Constants.vbNewLine +
                                                ex.Message);
                }

                return null;
            }

            public static void OpenConnection()
            {
                try
                {
                    OpenConnection(Settings.Default.DoubleClickStartsNewConnection
                                       ? Connection.Info.Force.DoNotJump
                                       : Connection.Info.Force.None);
                }
                catch (Exception ex)
                {
                    MessageCollector.AddMessage(MessageClass.ErrorMsg,
                                                Language.strConnectionOpenFailed + Constants.vbNewLine +
                                                ex.Message);
                }
            }

            public static void OpenConnection(Connection.Info.Force Force)
            {
                try
                {
                    if (Windows.treeForm.tvConnections.SelectedNode.Tag == null)
                    {
                        return;
                    }

                    if (Node.GetNodeType(Node.SelectedNode) == Node.Type.Connection)
                    {
                        OpenConnection((Connection.Info)Windows.treeForm.tvConnections.SelectedNode.Tag, Force);
                    }
                    else if (Node.GetNodeType(Node.SelectedNode) == Node.Type.Container)
                    {
                        foreach (TreeNode tNode in Node.SelectedNode.Nodes)
                        {
                            if (Node.GetNodeType(tNode) == Node.Type.Connection)
                            {
                                if (tNode.Tag != null)
                                {
                                    OpenConnection((Connection.Info)tNode.Tag, Force);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageCollector.AddMessage(MessageClass.ErrorMsg,
                                                Language.strConnectionOpenFailed + Constants.vbNewLine +
                                                ex.Message);
                }
            }

            public static void OpenConnection(Connection.Info ConnectionInfo)
            {
                try
                {
                    OpenConnection(ConnectionInfo, Connection.Info.Force.None);
                }
                catch (Exception ex)
                {
                    MessageCollector.AddMessage(MessageClass.ErrorMsg,
                                                Language.strConnectionOpenFailed + Constants.vbNewLine +
                                                ex.Message);
                }
            }

            public static void OpenConnection(Connection.Info ConnectionInfo, Form ConnectionForm)
            {
                try
                {
                    OpenConnectionFinal(ConnectionInfo, Connection.Info.Force.None, ConnectionForm);
                }
                catch (Exception ex)
                {
                    MessageCollector.AddMessage(MessageClass.ErrorMsg,
                                                Language.strConnectionOpenFailed + Constants.vbNewLine +
                                                ex.Message);
                }
            }

            public static void OpenConnection(Connection.Info ConnectionInfo, Form ConnectionForm,
                                              Connection.Info.Force Force)
            {
                try
                {
                    OpenConnectionFinal(ConnectionInfo, Force, ConnectionForm);
                }
                catch (Exception ex)
                {
                    MessageCollector.AddMessage(MessageClass.ErrorMsg,
                                                Language.strConnectionOpenFailed + Constants.vbNewLine +
                                                ex.Message);
                }
            }

            public static void OpenConnection(Connection.Info ConnectionInfo, Connection.Info.Force Force)
            {
                try
                {
                    OpenConnectionFinal(ConnectionInfo, Force, null);
                }
                catch (Exception ex)
                {
                    MessageCollector.AddMessage(MessageClass.ErrorMsg,
                                                Language.strConnectionOpenFailed + Constants.vbNewLine +
                                                ex.Message);
                }
            }

            private static void OpenConnectionFinal(Connection.Info newConnectionInfo, Connection.Info.Force Force,
                                                    Form ConForm)
            {
                try
                {
                    if (newConnectionInfo.Hostname == "" &&
                        newConnectionInfo.Protocol != Protocols.IntApp)
                    {
                        MessageCollector.AddMessage(MessageClass.WarningMsg,
                                                    Language.strConnectionOpenFailedNoHostname);
                        return;
                    }

                    if (newConnectionInfo.PreExtApp != "")
                    {
                        ExternalTool extA = GetExtAppByName(newConnectionInfo.PreExtApp);
                        if (extA != null)
                        {
                            extA.Start(newConnectionInfo);
                        }
                    }

                    //TODO
                    if ((Force) != Connection.Info.Force.DoNotJump)
                    {
                        if (SwitchToOpenConnection(newConnectionInfo))
                        {
                            return;
                        }
                    }

                    Base newProtocol = new Base();
                    // Create connection based on protocol type
                    switch (newConnectionInfo.Protocol)
                    {
                        case Protocols.RDP:
                            newProtocol = new RDP();
                            break;
                        case Protocols.VNC:
                            newProtocol = new VNC();
                            break;
                        case Protocols.SSH1:
                            newProtocol = new SSH1();
                            break;
                        case Protocols.SSH2:
                            newProtocol = new SSH2();
                            break;
                        case Protocols.Telnet:
                            newProtocol = new Telnet();
                            break;
                        case Protocols.Rlogin:
                            newProtocol = new Rlogin();
                            break;
                        case Protocols.Serial:
                            newProtocol = new Serial();
                            break;
                        case Protocols.RAW:
                            newProtocol = new RAW();
                            break;
                        case Protocols.HTTP:
                            newProtocol = new HTTP(newConnectionInfo.RenderingEngine);
                            break;
                        case Protocols.HTTPS:
                            newProtocol = new HTTPS(newConnectionInfo.RenderingEngine);
                            break;
                        case Protocols.TeamViewer:
                            newProtocol = new TeamViewer();
                            break;
                        case Protocols.RAdmin:
                            newProtocol=new RAdmin();
                            break;
                        case Protocols.ICA:
                            newProtocol = new ICA();
                            break;
                        case Protocols.IntApp:
                            newProtocol = new IntApp();
                            if (newConnectionInfo.ExtApp == "")
                            {
                                throw (new Exception(Language.strNoExtAppDefined));
                            }
                            break;
                        default:
                            return;
                    }

                    Control cContainer;
                    Form cForm;

                    string cPnl;
                    if (newConnectionInfo.Panel == "" || (Force) == Connection.Info.Force.OverridePanel ||
                        Settings.Default.AlwaysShowPanelSelectionDlg)
                    {
                        frmChoosePanel frmPnl = new frmChoosePanel();
                        if (frmPnl.ShowDialog() == DialogResult.OK)
                        {
                            cPnl = frmPnl.Panel;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        cPnl = (string)newConnectionInfo.Panel;
                    }

                    if (ConForm == null)
                    {
                        cForm = WindowList.FromString(cPnl);
                    }
                    else
                    {
                        cForm = ConForm;
                    }

                    if (cForm == null)
                    {
                        cForm = AddPanel(cPnl);
                        cForm.Focus();
                    }
                    else
                    {
                        (cForm as UI.Window.Connection).Show(frmMain.Default.pnlDock);
                        (cForm as UI.Window.Connection).Focus();
                    }

                    cContainer = (cForm as UI.Window.Connection).AddConnectionTab(newConnectionInfo);

                    if (newConnectionInfo.Protocol == Protocols.IntApp)
                    {
                        if (GetExtAppByName(newConnectionInfo.ExtApp).Icon != null)
                        {
                            (cContainer as TabPage).Icon =
                                GetExtAppByName(newConnectionInfo.ExtApp).Icon;
                        }
                    }

                    newProtocol.Closed +=  (cForm as UI.Window.Connection).Prot_Event_Closed;
                    newProtocol.Connected += (cForm as UI.Window.Connection).Prot_Event_Connected;
                    newProtocol.Disconnected += Prot_Event_Disconnected;
                    newProtocol.Connected += Prot_Event_Connected;
                    newProtocol.Closed += Prot_Event_Closed;
                    newProtocol.ErrorOccured += Prot_Event_ErrorOccured;

                    newProtocol.InterfaceControl = new InterfaceControl(cContainer, newProtocol, newConnectionInfo);

                    newProtocol.Force = Force;

                    if (newProtocol.SetProps() == false)
                    {
                        newProtocol.Close();
                        return;
                    }

                    if (newProtocol.Connect() == false)
                    {
                        newProtocol.Close();
                        return;
                    }

                    newConnectionInfo.OpenConnections.Add(newProtocol);

                    if (newConnectionInfo.IsQuicky == false)
                    {
                        if (newConnectionInfo.Protocol != Protocols.IntApp)
                        {
                            Node.SetNodeImage(newConnectionInfo.TreeNode, Enums.TreeImage.ConnectionOpen);
                        }
                        else
                        {
                            ExternalTool extApp = GetExtAppByName((string)newConnectionInfo.ExtApp);
                            if (extApp != null)
                            {
                                if (extApp.TryIntegrate)
                                {
                                    if (newConnectionInfo.TreeNode != null)
                                    {
                                        Node.SetNodeImage(newConnectionInfo.TreeNode,
                                                          Enums.TreeImage.ConnectionOpen);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageCollector.AddMessage(MessageClass.ErrorMsg,
                                                Language.strConnectionOpenFailed + Constants.vbNewLine +
                                                ex.Message);
                }
            }

            public static bool SwitchToOpenConnection(Connection.Info nCi)
            {
                var IC = FindConnectionContainer(nCi);

                if (IC != null)
                {
                    ((UI.Window.Connection)IC.FindForm()).Focus();
                    (IC.FindForm() as UI.Window.Connection).Show(frmMain.Default.pnlDock);
                    TabPage t = (TabPage)IC.Parent;
                    t.Selected = true;
                    return true;
                }

                return false;
            }

            #endregion Opening Connection

            #region Event Handlers

            public static void Prot_Event_Disconnected(object sender, string DisconnectedMessage)
            {
                try
                {
                    MessageCollector.AddMessage(MessageClass.InformationMsg,
                                                string.Format(Language.strProtocolEventDisconnected,
                                                              DisconnectedMessage), true);

                    Base Prot = (Base)sender;
                    if (Prot.InterfaceControl.Info.Protocol == Protocols.RDP)
                    {
                        string[] Reason = DisconnectedMessage.Split("\r\n".ToCharArray());
                        string ReasonCode = Reason[0];
                        string ReasonDescription = Reason[1];
                        if (Convert.ToInt32(ReasonCode) > 3)
                        {
                            if (ReasonDescription != "")
                            {
                                MessageCollector.AddMessage(MessageClass.WarningMsg,
                                                            Language.strRdpDisconnected +
                                                            Constants.vbNewLine + ReasonDescription +
                                                            Constants.vbNewLine +
                                                            string.Format(Language.strErrorCode, ReasonCode));
                            }
                            else
                            {
                                MessageCollector.AddMessage(MessageClass.WarningMsg,
                                                            Language.strRdpDisconnected +
                                                            Constants.vbNewLine +
                                                            string.Format(Language.strErrorCode, ReasonCode));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageCollector.AddMessage(MessageClass.ErrorMsg,
                                                string.Format(Language.strProtocolEventDisconnectFailed,
                                                              ex.Message), true);
                }
            }

            public static void Prot_Event_Closed(object sender)
            {
                try
                {
                    Base Prot = (Base)sender;

                    MessageCollector.AddMessage(MessageClass.InformationMsg,
                                                Language.strConnenctionCloseEvent, true);

                    MessageCollector.AddMessage(MessageClass.ReportMsg,
                                                string.Format(Language.strConnenctionClosedByUser,
                                                              Prot.InterfaceControl.Info.Hostname,
                                                              Prot.InterfaceControl.Info.Protocol.ToString(),
                                                              (new User()).Name));

                    Prot.InterfaceControl.Info.OpenConnections.Remove(Prot);

                    if (Prot.InterfaceControl.Info.OpenConnections.Count < 1 &&
                        Prot.InterfaceControl.Info.IsQuicky == false)
                    {
                        Node.SetNodeImage(Prot.InterfaceControl.Info.TreeNode,
                                          Enums.TreeImage.ConnectionClosed);
                    }

                    if (Prot.InterfaceControl.Info.PostExtApp != "")
                    {
                        ExternalTool extA = GetExtAppByName(Prot.InterfaceControl.Info.PostExtApp);
                        if (extA != null)
                        {
                            extA.Start(Prot.InterfaceControl.Info);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageCollector.AddMessage(MessageClass.ErrorMsg,
                                                Language.strConnenctionCloseEventFailed +
                                                Constants.vbNewLine + ex.Message, true);
                }
            }

            public static void Prot_Event_Connected(object sender)
            {
                Base prot = (Base)sender;

                MessageCollector.AddMessage(MessageClass.InformationMsg,
                                            Language.strConnectionEventConnected, true);
                MessageCollector.AddMessage(MessageClass.ReportMsg,
                                            string.Format(Language.strConnectionEventConnectedDetail,
                                                          prot.InterfaceControl.Info.Hostname,
                                                          prot.InterfaceControl.Info.Protocol.ToString(),
                                                          (new User()).Name,
                                                          prot.InterfaceControl.Info.Description,
                                                          prot.InterfaceControl.Info.UserField));
            }

            public static void Prot_Event_ErrorOccured(object sender, string ErrorMessage)
            {
                try
                {
                    MessageCollector.AddMessage(MessageClass.InformationMsg,
                                                Language.strConnectionEventErrorOccured, true);

                    Base Prot = (Base)sender;

                    if (Prot.InterfaceControl.Info.Protocol == Protocols.RDP)
                    {
                        if (Convert.ToInt32(ErrorMessage) > -1)
                        {
                            MessageCollector.AddMessage(MessageClass.WarningMsg,
                                                        string.Format(Language.strConnectionRdpErrorDetail,
                                                                      ErrorMessage,
                                                                      RDP.FatalErrors.
                                                                          GetError(ErrorMessage)));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageCollector.AddMessage(MessageClass.ErrorMsg,
                                                Language.strConnectionEventConnectionFailed +
                                                Constants.vbNewLine + ex.Message, true);
                }
            }

            #endregion Event Handlers

            #region External Apps

            public static void GetExtApps()
            {
                Array.Clear(ExternalAppsTypeConverter.ExternalApps, 0,
                            Convert.ToInt32(ExternalAppsTypeConverter.ExternalApps.Length));
                Array.Resize(ref ExternalAppsTypeConverter.ExternalApps, ExternalTools.Count + 1);

                int i = 0;

                foreach (ExternalTool extA in ExternalTools)
                {
                    ExternalAppsTypeConverter.ExternalApps[i] = extA.DisplayName;

                    i++;
                }

                ExternalAppsTypeConverter.ExternalApps[i] = "";
            }

            public static ExternalTool GetExtAppByName(string Name)
            {
                return ExternalTools.Cast<ExternalTool>().FirstOrDefault(extA => extA.DisplayName == Name);
            }

            #endregion External Apps

            #region Misc

            public static void GoToURL(string URL)
            {
                Connection.Info cI = new Connection.Info();

                cI.Name = "";
                cI.Hostname = URL;
                cI.Protocol = URL.StartsWith("https:") ? Protocols.HTTPS : Protocols.HTTP;
                cI.SetDefaultPort();
                cI.IsQuicky = true;

                Runtime.OpenConnection(cI, Connection.Info.Force.DoNotJump);
            }

            public static void GoToWebsite()
            {
                GoToURL((string)General.URLHome);
            }

            public static void GoToDonate()
            {
                GoToURL((string)General.URLDonate);
            }

            public static void GoToForum()
            {
                GoToURL((string)General.URLForum);
            }

            public static void GoToBugs()
            {
                GoToURL((string)General.URLBugs);
            }

            public static void Report(string Text)
            {
                try
                {
                    StreamWriter sWr =
                        new StreamWriter(
                            (new WindowsFormsApplicationBase()).Info.
                                DirectoryPath + "\\Report.log", true);
                    sWr.WriteLine(Text);
                    sWr.Close();
                }
                catch (Exception)
                {
                    MessageCollector.AddMessage(MessageClass.ErrorMsg,
                                                Language.strLogWriteToFileFailed);
                }
            }

            public static bool SaveReport()
            {
                StreamReader streamReader = null;
                StreamWriter streamWriter = null;
                try
                {
                    streamReader =
                        new StreamReader(
                            (new WindowsFormsApplicationBase()).Info.
                                DirectoryPath + "\\Report.log");
                    string text = streamReader.ReadToEnd();
                    streamReader.Close();

                    streamWriter = new StreamWriter(General.ReportingFilePath, true);
                    streamWriter.Write(text);

                    return true;
                }
                catch (Exception ex)
                {
                    MessageCollector.AddMessage(MessageClass.ErrorMsg,
                                                Language.strLogWriteToFileFinalLocationFailed +
                                                Constants.vbNewLine + ex.Message, true);
                    return false;
                }
                finally
                {
                    if (streamReader != null)
                    {
                        streamReader.Close();
                        streamReader.Dispose();
                    }
                    if (streamWriter != null)
                    {
                        streamWriter.Close();
                        streamWriter.Dispose();
                    }
                }
            }

            public static void SetMainFormText(string ConnectionFileName = "")
            {
                try
                {
                    string txt =
                        (new WindowsFormsApplicationBase()).Info.ProductName;

                    if (ConnectionFileName != "" && IsConnectionsFileLoaded == true)
                    {
                        if (Settings.Default.ShowCompleteConsPathInTitle)
                        {
                            txt += " - " + ConnectionFileName;
                        }
                        else
                        {
                            txt +=
                                (string)
                                (" - " +
                                 ConnectionFileName.Substring(
                                     Convert.ToInt32(ConnectionFileName.LastIndexOf("\\") + 1)));
                        }
                    }

                    ChangeMainFormText(txt);
                }
                catch (Exception ex)
                {
                    MessageCollector.AddMessage(MessageClass.ErrorMsg,
                                                Language.strSettingMainFormTextFailed + Constants.vbNewLine +
                                                ex.Message, true);
                }
            }

            private delegate void ChangeMainFormTextCB(string Text);

            private static void ChangeMainFormText(string Text)
            {
                if (frmMain.Default.InvokeRequired == true)
                {
                    ChangeMainFormTextCB d = ChangeMainFormText;
                    frmMain.Default.Invoke(d, new object[] { Text });
                }
                else
                {
                    frmMain.Default.Text = Text;
                }
            }

            public static InterfaceControl FindConnectionContainer(Connection.Info ConI)
            {
                if (ConI.OpenConnections.Count > 0)
                {
                    for (int i = 0; i <= WindowList.Count - 1; i++)
                    {
                        if (WindowList[i] is UI.Window.Connection)
                        {
                            UI.Window.Connection conW = (UI.Window.Connection)WindowList[i];

                            if (conW.TabController != null)
                            {
                                foreach (TabPage t in conW.TabController.TabPages)
                                {
                                    if (t.Controls[0] != null)
                                    {
                                        if (t.Controls[0] is InterfaceControl)
                                        {
                                            InterfaceControl IC = (InterfaceControl)t.Controls[0];
                                            if (IC.Info == ConI)
                                            {
                                                return IC;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return null;
            }

            // Override the font of all controls in a container with the default font based on the OS version
            public static void FontOverride(Control ctlParent)
            {
                foreach (Control ctlChild in ctlParent.Controls)
                {
                    ctlChild.Font = new Font(SystemFonts.MessageBoxFont.Name, ctlChild.Font.Size,
                                             ctlChild.Font.Style, ctlChild.Font.Unit,
                                             ctlChild.Font.GdiCharSet);
                    if (ctlChild.Controls.Count > 0)
                    {
                        FontOverride(ctlChild);
                    }
                }
            }

            #endregion Misc

            #region SQL Watcher

            private static void tmrSqlWatcher_Elapsed(object sender, ElapsedEventArgs e)
            {
                Misc.IsSQLUpdateAvailableBG();
            }

            private static void SQLUpdateCheckFinished(bool UpdateAvailable)
            {
                if (UpdateAvailable == true)
                {
                    MessageCollector.AddMessage(MessageClass.InformationMsg,
                                                Language.strSqlUpdateCheckUpdateAvailable, true);
                    LoadConnectionsBG();
                }
            }

            #endregion SQL Watcher
        }
    }
}