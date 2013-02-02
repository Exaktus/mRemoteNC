using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using My;
using PSTaskDialog;
using Shell32;
using WFICALib;
using mRemoteNC.App;

namespace mRemoteNC.Tools
{
    static class ProblemFixer
    {
        public static string RDPVer;
        public static string VNCVer;
        public static string XulVer;
        public static string TVVer;

        public static bool IsRDPOk()
        {
            try
            {
                using (var RDP = new AxMSTSCLib.AxMsRdpClient7NotSafeForScripting())
                {
                    RDP.CreateControl();
                    var i = 60;
                    while (!RDP.Created &&i --> 0)
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

        public static bool IsVNCOk()
        {
            try
            {
                using (var VNC = new VncSharp.RemoteDesktop())
                {
                    VNC.CreateControl();

                    while (!VNC.Created)
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
                string pPath = "";
                pPath = Settings.Default.UseCustomPuttyPath == false
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

        public static bool IsICAOk()
        {
            
            try
            {
                /*using (ICAClient ic = new ICAClient())
                {
                    //ICA = new AxICAClient { Parent = this };
                    //ICA.CreateControl();

                    /*while (!ICA.Created)
                    {
                        Thread.Sleep(10);
                        System.Windows.Forms.Application.DoEvents();
                    }
                }*/
                
                /*MessageBox.Show(ic.ClientVersion);
                pbCheck4.Image = global::My.Resources.Resources.Good_Symbol;
                lblCheck4.ForeColor = Color.DarkOliveGreen;
                lblCheck4.Text = "ICA (Citrix ICA) " + Language.strCcCheckSucceeded;
                txtCheck4.Text = string.Format(Language.strCcICAOK, ic.Version);*/
                //ICA.Dispose();
                throw new Exception();
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool IsGeckoOk()
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
                    XulVer = FileVersionInfo.GetVersionInfo(@Path.Combine(Settings.Default.XULRunnerPath, "xpcom.dll")).FileVersion;
                }
            }
            else
            {
                GeckoBad = true;
            }

            return GeckoBad == false;
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

        public static IEnumerable<string> FindTvPaths()
        {
            var res = new List<string>();
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "TeamViewer");
            if (Directory.Exists(path))
            {
                res.AddRange(Directory.GetDirectories(path).Select(dir => Path.Combine(path, dir, "TeamViewer.exe")));
            }
            res.Add(Path.Combine(Environment.CurrentDirectory, "TeamViewerPortable", "TeamViewer.exe"));
            return res.Where(File.Exists);
        }

        public static IEnumerable<string> FindGeckoPaths()
        {
            var res = new List<string>();
            res.Add(Path.Combine(Environment.CurrentDirectory, "xulrunner", "xpcom.dll"));
            return res.Where(File.Exists).Select(Path.GetDirectoryName);
        }

        public static void InstallXul()
        {
            try
            {
                var temFile = Path.GetTempFileName() + ".zip";
                using (var webClient = new WebClient())
                {
                    webClient.DownloadFile(
                        "http://ftp.mozilla.org/pub/mozilla.org/xulrunner/releases/16.0.2/runtimes/xulrunner-16.0.2.en-US.win32.zip",
                        temFile);
                }
                Extract(temFile, ".\\");
                File.Delete(temFile);
            }
            catch (Exception)
            {
                
            }
        }

        public static void FixTVProblem()
        {
            try
            {
                if (!FindTvPaths().Any())
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
                                                  Language.ProblemFixer_FixTVProblem_ + FindTvPaths().First(), eTaskDialogButtons.OK, eSysIcons.Information, eSysIcons.Information);
                    switch (cTaskDialog.CommandButtonResult)
                    {
                        case 0:
                            Runtime.Windows.Show(UI.Window.Type.Options);
                            break;
                        case 1:
                            Settings.Default.TeamViewerPath = FindTvPaths().First();
                            Settings.Default.Save();
                            break;
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }

        public static void Extract(string zipFileName, string targetPath)
        {
            Folder srcFolder = new Shell().NameSpace(Path.GetFullPath(zipFileName));
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }
            Folder dstFolder = new Shell().NameSpace(Path.GetFullPath(targetPath));
            dstFolder.CopyHere(srcFolder.Items(), 20);
        }

        internal static void FixGeckoProblem()
        {
            try
            {
                if (!FindGeckoPaths().Any())
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
                                                  Language.ProblemFixer_FixGeckoProblem_OpenOptions + FindGeckoPaths().First() + Language.ProblemFixer_FixGeckoProblem_OpenXULR, eTaskDialogButtons.OK, eSysIcons.Information, eSysIcons.Information);
                    switch (cTaskDialog.CommandButtonResult)
                    {
                        case 0:
                            Runtime.Windows.Show(UI.Window.Type.Options);
                            break;
                        case 1:
                            Settings.Default.XULRunnerPath = FindGeckoPaths().First();
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
            catch (Exception)
            {
                
            }
        }

        public static void Registar_Dlls(string filePath)
        {
            try
            {
                //'/s' : Specifies regsvr32 to run silently and to not display any message boxes.
                string arg_fileinfo = "/s" + " " + "\"" + filePath + "\"";
                Process reg = new Process();
                //This file registers .dll files as command components in the registry.
                reg.StartInfo.FileName = "regsvr32.exe";
                reg.StartInfo.Arguments = arg_fileinfo;
                reg.StartInfo.UseShellExecute = false;
                reg.StartInfo.CreateNoWindow = true;
                reg.StartInfo.Verb = "runas";
                reg.StartInfo.RedirectStandardOutput = true;
                reg.Start();
                reg.WaitForExit();
                reg.Close();
            }
            catch (Exception ex)
            {
                
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
                        using (var webClient = new WebClient())
                        {
                            webClient.DownloadFile(
                                "http://the.earth.li/~sgtatham/putty/latest/x86/putty.exe",
                                "putty.exe");
                        }
                        Settings.Default.UseCustomPuttyPath = true;
                        Settings.Default.CustomPuttyPath = Path.GetFullPath("putty.exe");
                        Settings.Default.Save();
                        break;
                }
            }
            catch (Exception)
            {
                
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
                            Registar_Dlls(Path.GetFullPath("eolwtscom.dll"));
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
            catch (Exception)
            {
                
            }
        }
    }
}
