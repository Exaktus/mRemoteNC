using System;
using System.Net;
using My;

namespace mRemoteNC.Tools
{
    static class WebClientHelper
    {
        internal static WebClient GetWebClient()
        {
            var _wCl = new WebClient();

            if (Settings.Default.UpdateUseProxy)
            {
                var wPr = new WebProxy(Settings.Default.UpdateProxyAddress,
                                   Convert.ToBoolean(Settings.Default.UpdateProxyPort));

                if (Settings.Default.UpdateProxyUseAuthentication)
                {
                    ICredentials cred = new NetworkCredential(Settings.Default.UpdateProxyAuthUser,
                                                              Security.Crypt.Decrypt(Settings.Default.UpdateProxyAuthPass,
                                                                                     AppInfo.General.EncryptionKey));

                    wPr.Credentials = cred;
                }

                _wCl.Proxy = wPr;
            }
            else
            {
                //SpeedHack
                _wCl.Proxy = null;
            }
            return _wCl;
        }
    }
}
