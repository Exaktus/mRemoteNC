using System;
using System.Net;
using Microsoft.VisualBasic;
using mRemoteNC.App;
using My;
using mRemoteNC.Tools;

//using mRemoteNC.Runtime;

namespace mRemoteNC
{
    public class Announcement : IDisposable
    {
        #region Private Variables

        private WebClient webClient = WebClientHelper.GetWebClient();
        private WebProxy webProxy;

        #endregion Private Variables

        #region Public Properties

        private Info _currentAnnouncementInfo;

        public Info CurrentAnnouncementInfo
        {
            get { return _currentAnnouncementInfo; }
        }

        #endregion Public Properties

        #region Public Methods

        public bool IsAnnouncementAvailable()
        {
            try
            {
                Info aI = GetAnnouncementInfo();

                if (aI.InfoOk == false)
                {
                    return false;
                }

                if (aI.Name != Settings.Default.LastAnnouncement)
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
                                                    ("IsAnnouncementAvailable failed" + Constants.vbNewLine + ex.Message),
                                                    true);
                return false;
            }
        }

        public Info GetAnnouncementInfo()
        {
            try
            {
                string strAnnouncement = GetAnnouncementFile();


                Info aI = new Info();

                if (strAnnouncement != "")
                {
                    aI.InfoOk = true;

                    try
                    {
                        //get Name
                        string strName =
                            strAnnouncement.Substring(System.Convert.ToInt32(strAnnouncement.IndexOf("Name: ") + 6),
                                                      System.Convert.ToInt32(
                                                          strAnnouncement.IndexOf(Constants.vbNewLine) - 6));
                        aI.Name = strName;

                        strAnnouncement = strAnnouncement.Remove(0,
                                                                 System.Convert.ToInt32(
                                                                     strAnnouncement.IndexOf(Constants.vbNewLine) + 2));

                        //get Download URL
                        string strU = "";

                        strU = strAnnouncement.Substring(System.Convert.ToInt32(strAnnouncement.IndexOf("URL: ") + 5),
                                                         System.Convert.ToInt32(
                                                             strAnnouncement.IndexOf(Constants.vbNewLine) - 5));

                        aI.Url = strU;
                    }
                    catch (Exception)
                    {
                        aI.InfoOk = false;
                    }
                }
                else
                {
                    aI.InfoOk = false;
                }

                _currentAnnouncementInfo = aI;
                return aI;
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                    (string)
                                                    ("Getting Announcement info failed" + Constants.vbNewLine +
                                                     ex.Message), true);
                return null;
            }
        }

        private string GetAnnouncementFile()
        {
            try
            {

                string strTemp;

                try
                {
                    strTemp = webClient.DownloadString(AppInfo.General.URLAnnouncement);
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
                                                    (string)
                                                    ("GetAnnouncementFile failed" + Constants.vbNewLine + ex.Message),
                                                    true);
                return "";
            }
        }

       

        #endregion Public Methods

        public class Info
        {
            private string _Name;

            public string Name
            {
                get { return _Name; }
                set { _Name = value; }
            }

            private string _url;

            public string Url
            {
                get { return _url; }
                set { _url = value; }
            }

            private bool _InfoOk;

            public bool InfoOk
            {
                get { return _InfoOk; }
                set { _InfoOk = value; }
            }
        }

        #region IDisposable Support

        private bool disposedValue; // To detect redundant calls

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    if (webClient != null)
                    {
                        webClient.Dispose();
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                // TODO: set large fields to null.
            }
            this.disposedValue = true;
        }

        // TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        //Protected Overrides Sub Finalize()
        //    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        //    Dispose(False)
        //    MyBase.Finalize()
        //End Sub

        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }
}