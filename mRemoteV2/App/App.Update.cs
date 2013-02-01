using System;
using System.IO;
using System.Net;
using Microsoft.VisualBasic;
using mRemoteNC.App;
using My;

namespace mRemoteNC
{
    public class Update
    {
        public delegate void DownloadProgressChangedEventHandler(
            object sender, System.Net.DownloadProgressChangedEventArgs e);

        private DownloadProgressChangedEventHandler DownloadProgressChangedEvent;

        public event DownloadProgressChangedEventHandler DownloadProgressChanged
        {
            add
            {
                DownloadProgressChangedEvent =
                    (DownloadProgressChangedEventHandler)System.Delegate.Combine(DownloadProgressChangedEvent, value);
            }
            remove
            {
                DownloadProgressChangedEvent =
                    (DownloadProgressChangedEventHandler)System.Delegate.Remove(DownloadProgressChangedEvent, value);
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
                    (DownloadCompletedEventHandler)System.Delegate.Combine(DownloadCompletedEvent, value);
            }
            remove
            {
                DownloadCompletedEvent =
                    (DownloadCompletedEventHandler)System.Delegate.Remove(DownloadCompletedEvent, value);
            }
        }

        #region Public Properties

        private Info _curUI;

        public Info curUI
        {
            get { return _curUI; }
        }

        #endregion Public Properties

        #region Private Properties

        private WebClient wCl;
        private WebProxy wPr;

        #endregion Private Properties

        #region Public Methods

        public bool IsProxyOK()
        {
            try
            {
                Info uI = GetUpdateInfo();

                return uI.InfoOk;
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                    (string)
                                                    ("IsProxyOK (App.Update) failed" + Constants.vbNewLine + ex.Message),
                                                    false);
                return false;
            }
        }

        public bool IsUpdateAvailable()
        {
            try
            {
                Info uI = GetUpdateInfo();

                if (uI.InfoOk == false)
                {
                    return false;
                }

                if (uI.Version >
                    (new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase()).Info.Version)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                    (string)
                                                    ("IsUpdateAvailable failed" + Constants.vbNewLine + ex.Message),
                                                    true);
                return false;
            }
        }

        public Info GetUpdateInfo()
        {
            try
            {
                string strUpdate = GetUpdateFile();

                CreateWebClient();

                Info uI = new Info();

                if (strUpdate != "")
                {
                    uI.InfoOk = true;

                    try
                    {
                        //get Version
                        string strV = strUpdate.Substring(System.Convert.ToInt32(strUpdate.IndexOf("Version: ") + 9),
                                                          System.Convert.ToInt32(
                                                              strUpdate.IndexOf(Constants.vbNewLine) - 9));
                        uI.Version = new Version(strV);

                        strUpdate = strUpdate.Remove(0,
                                                     System.Convert.ToInt32(strUpdate.IndexOf(Constants.vbNewLine) + 2));

                        //get Download URL
                        string strU = "";

                        strU = strUpdate.Substring(System.Convert.ToInt32(strUpdate.IndexOf("dURL: ") + 6),
                                                   System.Convert.ToInt32(strUpdate.IndexOf(Constants.vbNewLine) - 6));

                        uI.DownloadUrl = strU;

                        strUpdate = strUpdate.Remove(0,
                                                     System.Convert.ToInt32(strUpdate.IndexOf(Constants.vbNewLine) + 2));

                        //get Change Log
                        string strClURL = strUpdate.Substring(System.Convert.ToInt32(strUpdate.IndexOf("clURL: ") + 7),
                                                              System.Convert.ToInt32(
                                                                  strUpdate.IndexOf(Constants.vbNewLine) - 7));
                        string strCl = wCl.DownloadString(strClURL);
                        uI.ChangeLog = strCl;

                        strUpdate = strUpdate.Remove(0,
                                                     System.Convert.ToInt32(strUpdate.IndexOf(Constants.vbNewLine) + 2));

                        try
                        {
                            //get Image
                            string strImgURL =
                                strUpdate.Substring(System.Convert.ToInt32(strUpdate.IndexOf("imgURL: ") + 8),
                                                    System.Convert.ToInt32(strUpdate.IndexOf(Constants.vbNewLine) - 8));
                            uI.ImageURL = strImgURL;

                            strUpdate = strUpdate.Remove(0,
                                                         System.Convert.ToInt32(strUpdate.IndexOf(Constants.vbNewLine) +
                                                                                2));

                            //get Image Link
                            string strImgURLLink =
                                strUpdate.Substring(System.Convert.ToInt32(strUpdate.IndexOf("imgURLLink: ") + 12),
                                                    System.Convert.ToInt32(strUpdate.IndexOf(Constants.vbNewLine) - 12));
                            uI.ImageURLLink = strImgURLLink;
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

                _curUI = uI;
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
                CreateWebClient();

                wCl.DownloadProgressChanged += new System.Net.DownloadProgressChangedEventHandler(DLProgressChanged);
                wCl.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(DLCompleted);

                _curUI.UpdateLocation =
                    (new Microsoft.VisualBasic.Devices.ServerComputer()).FileSystem.SpecialDirectories.Temp +
                    "\\mRemote_Update.exe";
                wCl.DownloadFileAsync(new Uri(dURL), _curUI.UpdateLocation);

                return true;
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                    (string)
                                                    ("Update download failed" + Constants.vbNewLine + ex.Message), true);
                return false;
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void CreateWebClient()
        {
            wCl = new WebClient();

            if (Settings.Default.UpdateUseProxy)
            {
                wPr = new WebProxy(Settings.Default.UpdateProxyAddress,
                                   System.Convert.ToBoolean(Settings.Default.UpdateProxyPort));

                if (Settings.Default.UpdateProxyUseAuthentication)
                {
                    ICredentials cred;
                    cred = new NetworkCredential((string)Settings.Default.UpdateProxyAuthUser,
                                                 Security.Crypt.Decrypt((string)Settings.Default.UpdateProxyAuthPass,
                                                                        (string)App.Info.General.EncryptionKey));

                    wPr.Credentials = cred;
                }

                wCl.Proxy = wPr;
            }
        }

        private string GetUpdateFile()
        {
            try
            {
                CreateWebClient();

                string strTemp;

                try
                {
                    strTemp = wCl.DownloadString(App.Info.Update.URL + App.Info.Update.File);
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
                                                    (string)("GetUpdateFile failed" + Constants.vbNewLine + ex.Message),
                                                    true);
                return "";
            }
        }

        private void DLProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            if (DownloadProgressChangedEvent != null)
                DownloadProgressChangedEvent(sender, e);
        }

        private void DLCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            try
            {
                FileInfo fInfo = new FileInfo(_curUI.UpdateLocation);

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
                                                    (string)("DLCompleted failed" + Constants.vbNewLine + ex.Message),
                                                    true);
            }
        }

        #endregion Private Methods

        public class Info
        {
            private Version _Version;

            public Version Version
            {
                get { return _Version; }
                set { _Version = value; }
            }

            private string _DownloadUrl;

            public string DownloadUrl
            {
                get { return _DownloadUrl; }
                set { _DownloadUrl = value; }
            }

            private string _UpdateLocation;

            public string UpdateLocation
            {
                get { return _UpdateLocation; }
                set { _UpdateLocation = value; }
            }

            private string _ChangeLog;

            public string ChangeLog
            {
                get { return _ChangeLog; }
                set { _ChangeLog = value; }
            }

            private string _ImageURL;

            public string ImageURL
            {
                get { return _ImageURL; }
                set { _ImageURL = value; }
            }

            private string _ImageURLLink;

            public string ImageURLLink
            {
                get { return _ImageURLLink; }
                set { _ImageURLLink = value; }
            }

            private bool _InfoOk;

            public bool InfoOk
            {
                get { return _InfoOk; }
                set { _InfoOk = value; }
            }
        }
    }
}

//using mRemoteNC.Runtime;

namespace mRemoteNC
{
}