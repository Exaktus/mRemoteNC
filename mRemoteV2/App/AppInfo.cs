using System.IO;
using System.Windows.Forms;

namespace mRemoteNC
{
    namespace AppInfo
    {
        public static class General
        {
            public const string URLHome = "http://exaktus.github.com/mRemoteNC/";
            public const string URLDonate = "http://exaktus.github.com/mRemoteNC/";
            public const string URLForum = "https://github.com/Exaktus/mRemoteNC/issues/";
            public const string URLBugs = "https://github.com/Exaktus/mRemoteNC/issues/";
            public const string URLAnnouncement = "http://exaktus.github.com/mRemoteNC/update/announcement.txt";

            public static readonly bool IsPortable =
#if PORTABLE
 true;
#else 
                false; 
#endif

            public static readonly string HomePath = Application.StartupPath;

            public const string EncryptionKey = "mR3m";
            public const string ReportingFilePath = "";
        }

        public static class Settings
        {
            public static readonly string SettingsPath =
                General.IsPortable ?
                General.HomePath :
                Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), Application.ProductName);

            public const string LayoutFileName = "pnlLayout.xml";
            public const string ExtAppsFilesName = "extApps.xml";
            public const string QuickTextsFilesName = "quickTexts.xml";
        }

        public static class Update
        {
            public const string URL = "http://exaktus.github.com/mRemoteNC/update/";
            public static string File
            {
                get
                {
                    return
#if DEBUG
                            General.IsPortable ? "update-portable-debug.txt" : "update-debug.txt";
#else 
                            General.IsPortable ? "update-portable.txt" : "update.txt";
#endif
                }
            }


        }

        public static class Connections
        {
            public static readonly string DefaultConnectionsPath = Settings.SettingsPath;
            public const string DefaultConnectionsFile = "confCons.xml";

            public static readonly string DefaultConnectionsFileNew = "confConsNew.xml";
            public const double ConnectionFileVersion = 2.5;
        }

        public class Credentials
        {
            public static readonly string CredentialsPath = Settings.SettingsPath;
            public static readonly string CredentialsFile = "confCreds.xml";
            public static readonly string CredentialsFileNew = "confCredsNew.xml";
            public static readonly double CredentialsFileVersion = 1.0;
        }
    }
}