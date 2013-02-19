using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using My;
using PSTaskDialog;
using mRemoteNC.App;

namespace mRemoteNC.Tools
{
    static class ProblemFixer
    {
        public static string RDPVer;
        public static string VNCVer;
        public static string XulVer;
        public static string TVVer;
        public static string ICAVer;

        private static bool IsRDPOkInternal()
        {
            try
            {
                using (var RDP = new AxMSTSCLib.AxMsRdpClient7())
                {
                    RDP.CreateControl();
                    var i = 60;
                    while (!RDP.Created && i-- > 0)
                    {
                        Thread.Sleep(10);
                        Application.DoEvents();
                    }
                    RDPVer = RDP.Version;
                }
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsRDPOk()
        {
            try
            {
                return IsRDPOkInternal();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsVNCOk()
        {
            try
            {
                using (var VNC = new VncSharp.RemoteDesktop())
                {
                    VNC.CreateControl();
                    var i = 60;
                    while (!VNC.Created && i-- > 0)
                    {
                        Thread.Sleep(10);
                        Application.DoEvents();
                    }
                    VNCVer = VNC.ProductVersion;
                }
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsPuTTYOk()
        {
            try
            {
                string pPath = Settings.Default.UseCustomPuttyPath == false
                                   ? (new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase()).Info.
                                         DirectoryPath + "\\PuTTYNG.exe"
                                   : Settings.Default.CustomPuttyPath;

                return File.Exists(pPath);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool IsICAOkInternal()
        {
            try
            {
                using (var ICA = new AxWFICALib.AxICAClient { Parent = frmMain.defaultInstance })
                {
                    ICA.CreateControl();
                    ICAVer = ICA.ClientVersion;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsICAOk()
        {
            try
            {
                //If ICA not installed (assembly not found) this will catch Exception
                return IsICAOkInternal();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsGeckoOk()
        {
            try
            {
                bool GeckoBad = Settings.Default.XULRunnerPath == "";

                if (Directory.Exists(Settings.Default.XULRunnerPath))
                {
                    if (File.Exists(Path.Combine(Settings.Default.XULRunnerPath, "xpcom.dll")) == false)
                    {
                        GeckoBad = true;
                    }
                    else
                    {
                        XulVer = FileVersionInfo.GetVersionInfo(Path.Combine(Settings.Default.XULRunnerPath, "xpcom.dll")).FileVersion;
                    }
                }
                else
                {
                    GeckoBad = true;
                }

                return GeckoBad == false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsRDPSessionsOk()
        {
            try
            {
                var eol = new EOLWTSCOM.WTSCOM();
                eol.GetUserInfo();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsTeamViewerOk()
        {
            try
            {
                if (File.Exists(Settings.Default.TeamViewerPath))
                {
                    TVVer = FileVersionInfo.GetVersionInfo(Settings.Default.TeamViewerPath).FileVersion;
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception)
            {
                return false;
            }
            
        }


        private static void InstallXul()
        {
            try
            {
                var temFile = Path.GetTempFileName() + ".zip";
                Misc.DownloadFileVisual("http://ftp.mozilla.org/pub/mozilla.org/xulrunner/releases/16.0.2/runtimes/xulrunner-16.0.2.en-US.win32.zip",temFile);
                Misc.UnZipFile(temFile, ".\\");
                File.Delete(temFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void FixTVProblem()
        {
            try
            {
                if (!Misc.FindTvPaths().Any())
                {
                    cTaskDialog.CommandButtonResult = 99;
                    cTaskDialog.ShowTaskDialogBox(Language.strPfTVProblemFound,
                                                  Language.ProblemFixer_FixTVProblem_TeamViewer_not_found__neither_installed_nor_portable,
                                                  Language.ProblemFixer_FixTVProblem_You_shold_install_TeamViewer, 
                                                  "", "", "", "",
                                                  Language.ProblemFixer_FixTVProblem_Options, eTaskDialogButtons.OK, eSysIcons.Information, eSysIcons.Information);
                    switch (cTaskDialog.CommandButtonResult)
                    {
                        case 0:
                            Runtime.Windows.Show(UI.Window.Type.Options);
                            break;
                        case 1:
                            Process.Start("http://www.teamviewer.com/ru/download/windows.aspx");
                            break;
                    }
                }
                else
                {
                    cTaskDialog.CommandButtonResult = 99;
                    cTaskDialog.ShowTaskDialogBox(Language.strPfTVProblemFound,
                                                  Language.ProblemFixer_FixTVProblem_TeamViewer_found__but_not_set_in_options,
                                                  Language.ProblemFixer_FixTVProblem_You_should_setup_TeamViewer,
                                                  "", "", "", "",
                                                  Language.ProblemFixer_FixTVProblem_ + Misc.FindTvPaths().First(), eTaskDialogButtons.OK, eSysIcons.Information, eSysIcons.Information);
                    switch (cTaskDialog.CommandButtonResult)
                    {
                        case 0:
                            Runtime.Windows.Show(UI.Window.Type.Options);
                            break;
                        case 1:
                            Settings.Default.TeamViewerPath = Misc.FindTvPaths().First();
                            Settings.Default.Save();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        internal static void FixGeckoProblem()
        {
            try
            {
                if (!Misc.FindGeckoPaths().Any())
                {
                    cTaskDialog.CommandButtonResult = 99;
                    cTaskDialog.ShowTaskDialogBox(Language.ProblemFixer_FixGeckoProblem_Gecko__problem_found,
                                                  Language.ProblemFixer_FixGeckoProblem_Xulrunner_folder_not_found,
                                                  Language.strCcGeckoFix,
                                                  "", "", "", "",
                                                  Language.ProblemFixer_FixGeckoProblem_, 
                                                  eTaskDialogButtons.OK, eSysIcons.Information, eSysIcons.Information);
                    switch (cTaskDialog.CommandButtonResult)
                    {
                        case 0:
                            Runtime.Windows.Show(UI.Window.Type.Options);
                            break;
                        case 1:
                            Process.Start("ftp://ftp.mozilla.org/pub/xulrunner/releases/1.8.1.3/contrib/win32/");
                            break;
                        case 2:
                            Process.Start("http://ftp.mozilla.org/pub/mozilla.org/xulrunner/releases/16.0.2/runtimes/");
                            break;
                        case 3:
                            InstallXul();
                            Settings.Default.XULRunnerPath = Path.GetFullPath(".\\xulrunner\\");
                            Settings.Default.Save();
                            break;
                    }
                }
                else
                {
                    cTaskDialog.CommandButtonResult = 99;
                    cTaskDialog.ShowTaskDialogBox(Language.ProblemFixer_FixGeckoProblem_Gecko__problem_found,
                                                  Language.ProblemFixer_FixGeckoProblem_Xulrunner_found__but_not_set_in_options,
                                                  Language.ProblemFixer_FixGeckoProblem_You_should_setup_Xulrunner_path,
                                                  "", "", "", "",
                                                  Language.ProblemFixer_FixGeckoProblem_OpenOptions + Misc.FindGeckoPaths().First() + Language.ProblemFixer_FixGeckoProblem_OpenXULR, eTaskDialogButtons.OK, eSysIcons.Information, eSysIcons.Information);
                    switch (cTaskDialog.CommandButtonResult)
                    {
                        case 0:
                            Runtime.Windows.Show(UI.Window.Type.Options);
                            break;
                        case 1:
                            Settings.Default.XULRunnerPath = Misc.FindGeckoPaths().First();
                            Settings.Default.Save();
                            break;
                        case 2:
                            Process.Start("ftp://ftp.mozilla.org/pub/xulrunner/releases/1.8.1.3/contrib/win32/");
                            break;
                        case 3:
                            Process.Start("http://ftp.mozilla.org/pub/mozilla.org/xulrunner/releases/16.0.2/runtimes/");
                            break;
                        case 4:
                            InstallXul();
                            Settings.Default.XULRunnerPath = Path.GetFullPath(".\\xulrunner\\");
                            Settings.Default.Save();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void FixPuTTYProblem()
        {
            try
            {
                cTaskDialog.CommandButtonResult = 99;
                cTaskDialog.ShowTaskDialogBox(Language.ProblemFixer_FixPuTTYProblem_PuTTY__problem_found,
                                              Language.ProblemFixer_FixPuTTYProblem_PuTTY_not_found__Reinstall_mRemoteNC_or_set_PuTTY_path_in_Options_,
                                              Language.strCcPuttyFailed,
                                              "", "", "", "",
                                              Language.ProblemFixer_FixPuTTYProblem_Open_Options_Open_mRemoteNC_Download_page_Open_PuTTY_download_page_Download_and_install_PuTTY, eTaskDialogButtons.OK, eSysIcons.Information, eSysIcons.Information);
                switch (cTaskDialog.CommandButtonResult)
                {
                    case 0:
                        Runtime.Windows.Show(UI.Window.Type.Options);
                        break;
                    case 1:
                        Process.Start(App.Info.General.URLHome);
                        break;
                    case 2:
                        Process.Start("http://www.chiark.greenend.org.uk/~sgtatham/putty/download.html");
                        break;
                    case 3:
                        Misc.DownloadFileVisual("http://the.earth.li/~sgtatham/putty/latest/x86/putty.exe", "putty.exe");
                        
                        Settings.Default.UseCustomPuttyPath = true;
                        Settings.Default.CustomPuttyPath = Path.GetFullPath("putty.exe");
                        Settings.Default.Save();
                        Settings.Default.Reload();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void FixEolProblem()
        {
            try
            {

                if (File.Exists("eolwtscom.dll"))
                {
                    cTaskDialog.CommandButtonResult = 99;
                    cTaskDialog.ShowTaskDialogBox(Language.ProblemFixer_FixEolProblem_EOLWTSCOM__problem_found,
                                              Language.ProblemFixer_FixEolProblem_EOLWTSCOM__found__but_not_registered,
                                              Language.strCcEOLFailed,
                                              "", "", "", "",
                                              Language.ProblemFixer_FixEolProblem_Register_EOLWTSCOM, eTaskDialogButtons.OK, eSysIcons.Information, eSysIcons.Information);
                    switch (cTaskDialog.CommandButtonResult)
                    {
                        case 0:
                            Misc.RegisterDll(Path.GetFullPath("eolwtscom.dll"));
                            break;
                    }
                }
                else
                {
                    cTaskDialog.CommandButtonResult = 99;
                    cTaskDialog.ShowTaskDialogBox(Language.ProblemFixer_FixEolProblem_EOLWTSCOM__problem_found,
                                              Language.ProblemFixer_FixEolProblem_EOLWTSCOM__not_found,
                                              Language.ProblemFixer_FixEolProblem_Reinstall_application_,
                                              "", "", "", "",
                                              Language.ProblemFixer_FixEolProblem_Open_mRemoteNC_website, eTaskDialogButtons.OK, eSysIcons.Information, eSysIcons.Information);
                    switch (cTaskDialog.CommandButtonResult)
                    {
                        case 0:
                            Process.Start(App.Info.General.URLHome);
                            break;
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static string RAVer;

        internal static bool IsRAdminOk()
        {
            try
            {
                if (File.Exists(Settings.Default.RAdminPath))
                {
                    RAVer = FileVersionInfo.GetVersionInfo(Settings.Default.RAdminPath).FileVersion;
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        internal static void FixRAdminProblem()
        {
            try
            {
                if (!Misc.FindRAdminPaths().Any())
                {
                    cTaskDialog.CommandButtonResult = 99;
                    cTaskDialog.ShowTaskDialogBox(Language.ProblemFixer_FixRAdminProblem_RAdmin__Problem_found,
                                                  Language.ProblemFixer_FixRAdminProblem_RAdmin_not_found__neither_installed_nor_portable,
                                                  Language.ProblemFixer_FixRAdminProblem_You_shold_install_RAdmin_or_setup_RAdmin_path,
                                                  "", "", "", "",
                                                  Language.ProblemFixer_FixRAdminProblem_Open_Options_Open_RAdmin_Download_page, eTaskDialogButtons.OK, eSysIcons.Information, eSysIcons.Information);
                    switch (cTaskDialog.CommandButtonResult)
                    {
                        case 0:
                            Runtime.Windows.Show(UI.Window.Type.Options);
                            break;
                        case 1:
                            Process.Start("http://www.radmin.ru/download/index.php");
                            break;
                    }
                }
                else
                {
                    cTaskDialog.CommandButtonResult = 99;
                    cTaskDialog.ShowTaskDialogBox(Language.ProblemFixer_FixRAdminProblem_RAdmin__Problem_found,
                                                  Language.ProblemFixer_FixRAdminProblem_RAdmin_found__but_not_set_in_options,
                                                  Language.ProblemFixer_FixRAdminProblem_You_should_setup_TeamViewer_path_in_options_or_I_can_do_it_for_you_,
                                                  "", "", "", "",
                                                  Language.ProblemFixer_FixRAdminProblem_OpenOptions_Setuppath+ Misc.FindRAdminPaths().First(), eTaskDialogButtons.OK, eSysIcons.Information, eSysIcons.Information);
                    switch (cTaskDialog.CommandButtonResult)
                    {
                        case 0:
                            Runtime.Windows.Show(UI.Window.Type.Options);
                            break;
                        case 1:
                            Settings.Default.RAdminPath = Misc.FindRAdminPaths().First();
                            Settings.Default.Save();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
