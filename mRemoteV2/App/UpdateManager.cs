using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Linq;
using mRemoteNC.Tools;

namespace mRemoteNC.App
{
    public class UpdateManager
    {
        public delegate void DownloadProgressChangedEventHandler(
            object sender, DownloadProgressChangedEventArgs e);

        private DownloadProgressChangedEventHandler DownloadProgressChangedEvent;

        public event DownloadProgressChangedEventHandler DownloadProgressChanged
        {
            add
            {
                DownloadProgressChangedEvent =
                    (DownloadProgressChangedEventHandler)Delegate.Combine(DownloadProgressChangedEvent, value);
            }
            remove
            {
                DownloadProgressChangedEvent =
                    (DownloadProgressChangedEventHandler)Delegate.Remove(DownloadProgressChangedEvent, value);
            }
        }

        public delegate void DownloadCompletedEventHandler(
            object sender, System.ComponentModel.AsyncCompletedEventArgs e, bool Success);

        private DownloadCompletedEventHandler DownloadCompletedEvent;

        public event DownloadCompletedEventHandler DownloadCompleted
        {
            add
            {
                DownloadCompletedEvent =
                    (DownloadCompletedEventHandler)Delegate.Combine(DownloadCompletedEvent, value);
            }
            remove
            {
                DownloadCompletedEvent =
                    (DownloadCompletedEventHandler)Delegate.Remove(DownloadCompletedEvent, value);
            }
        }

        #region Public Properties

        public UpdateInfo curUI { get; private set; }

        #endregion Public Properties

        #region Private Properties

        private WebClient webClient = WebClientHelper.GetWebClient();
        private UpdateInfo uI;

        #endregion Private Properties

        #region Public Methods

        public bool IsProxyOK()
        {
            try
            {
                return GetUpdateInfo().InfoOk;
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                    "IsProxyOK (App.Update) failed" + Constants.vbNewLine + ex.Message,
                                                    false);
                return false;
            }
        }

        public bool IsUpdateAvailable()
        {
            try
            {
                if (uI==null||!uI.InfoOk)
                {
                    uI = GetUpdateInfo();
                }

                if (!uI.InfoOk)
                    return false;

                return uI.Version > new Version(Application.ProductVersion);
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                    "IsUpdateAvailable failed" + Constants.vbNewLine + ex.Message,
                                                    true);
                return false;
            }
        }

        public UpdateInfo GetUpdateInfo()
        {
            try
            {
                if (uI != null && uI.InfoOk)
                {
                    return uI;
                }
                string strUpdate = GetUpdateFile();
                
                uI = new UpdateInfo();

                if (strUpdate != "")
                {
                    uI.InfoOk = true;

                    try
                    {
                        var all = strUpdate.Split(new[] { "\n","\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(s =>
                                        {
                                            var r  =s.Split(new[]{": "},StringSplitOptions.RemoveEmptyEntries);
                                            return
                                                new KeyValuePair<string,string>(r[0],r[1]);
                                        })
                                        .ToDictionary(pair => pair.Key, pair => pair.Value);
                        if (all.ContainsKey("Version"))
                        {
                            uI.Version = new Version(all["Version"]);
                        }

                        if (all.ContainsKey("dURL"))
                        {
                            uI.DownloadUrl = all["dURL"];
                        }

                        if (all.ContainsKey("clURL"))
                        {
                            uI.ChangeLog = webClient.DownloadString(all["clURL"]);
                        }

                        try
                        {
                            if (all.ContainsKey("imgURL"))
                            {
                                uI.ImageURL = all["imgURL"];
                            }

                            if (all.ContainsKey("imgURLLink"))
                            {
                                uI.ImageURLLink = all["imgURLLink"];
                            }
                        }
                        catch (Exception ex)
                        {
                            Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                                (string)
                                                                ("Update Image Info could not be read." +
                                                                 Constants.vbNewLine + ex.Message), true);
                        }
                    }
                    catch (Exception)
                    {
                        uI.InfoOk = false;
                    }
                }
                else
                {
                    uI.InfoOk = false;
                }

                curUI = uI;
                return uI;
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                    (string)
                                                    ("Getting update info failed" + Constants.vbNewLine + ex.Message),
                                                    true);
                return null;
            }
        }

        public bool DownloadUpdate(string dURL)
        {
            try
            {
                webClient.DownloadProgressChanged += DLProgressChanged;
                webClient.DownloadFileCompleted += DLCompleted;

                curUI.UpdateLocation = Path.Combine(Path.GetTempPath(), AppInfo.General.IsPortable ? "mRemoteNC_Update.zip" : "mRemoteNC_Update.exe");
                webClient.DownloadFileAsync(new Uri(dURL), curUI.UpdateLocation);

                return true;
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                    "Update download failed" + Constants.vbNewLine + ex.Message, true);
                return false;
            }
        }

        #endregion Public Methods

        #region Private Methods
        
        private string GetUpdateFile()
        {
            try
            {
                string strTemp;

                try
                {
                    strTemp = webClient.DownloadString(AppInfo.Update.URL + AppInfo.Update.File);
                }
                catch (Exception)
                {
                    strTemp = "";
                }

                return strTemp;
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                    "GetUpdateFile failed" + Constants.vbNewLine + ex.Message,
                                                    true);
                return "";
            }
        }

        private void DLProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (DownloadProgressChangedEvent != null)
                DownloadProgressChangedEvent(sender, e);
        }

        private void DLCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            try
            {
                FileInfo fInfo = new FileInfo(curUI.UpdateLocation);

                if (fInfo.Length > 0)
                {
                    if (DownloadCompletedEvent != null)
                        DownloadCompletedEvent(sender, e, true);
                }
                else
                {
                    fInfo.Delete();
                    if (DownloadCompletedEvent != null)
                        DownloadCompletedEvent(sender, e, false);
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                    "DLCompleted failed" + Constants.vbNewLine + ex.Message,
                                                    true);
            }
        }

        #endregion Private Methods

        public class UpdateInfo
        {
            public Version Version { get; set; }

            public string DownloadUrl { get; set; }

            public string UpdateLocation { get; set; }

            public string ChangeLog { get; set; }

            public string ImageURL { get; set; }

            public string ImageURLLink { get; set; }

            public bool InfoOk { get; set; }
        }
    }
}