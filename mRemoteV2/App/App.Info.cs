//using System.Environment;

namespace mRemoteNC
{
    namespace App
    {
        namespace Info
        {
            public class General
            {
                public static readonly string URLHome = "https://github.com/Exaktus/mRemoteNC/";
                public static readonly string URLDonate = "http://donate.mRemoteNC.org/";
                public static readonly string URLForum = "http://forum.mRemoteNC.org/";
                public static readonly string URLBugs = "https://github.com/Exaktus/mRemoteNC/issues/";
                public static readonly string URLAnnouncement = "http://update.mRemoteNC.org/announcement.txt";

                public static readonly string HomePath =
                    (new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase()).Info.DirectoryPath;

                public static string EncryptionKey = "mR3m";
                public static string ReportingFilePath = "";
            }

            public class Settings
            {
#if !PORTABLE
                public static readonly string SettingsPath =
                    (string)
                    (System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\" +
                     (new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase()).Info.ProductName);
#else
                public static readonly string SettingsPath = (new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase()).Info.DirectoryPath;
#endif
                public static readonly string LayoutFileName = "pnlLayout.xml";
                public static readonly string ExtAppsFilesName = "extApps.xml";
                public static readonly string QuickTextsFilesName = "quickTexts.xml";
            }

            public class Update
            {
                public static readonly string URL = "http://update.mRemoteNC.org/";
#if DEBUG
                public static readonly string File = "update-debug.txt";
#else
                public static readonly string File = "update.txt";
#endif
            }

            public class Connections
            {
                public static readonly string DefaultConnectionsPath = (string)Settings.SettingsPath;
                public static readonly string DefaultConnectionsFile = "confCons.xml";

                public static readonly string DefaultConnectionsFileNew = "confConsNew.xml";
                public static readonly double ConnectionFileVersion = 2.5;
            }

            public class Credentials
            {
                public static readonly string CredentialsPath = (string)Settings.SettingsPath;
                public static readonly string CredentialsFile = "confCreds.xml";
                public static readonly string CredentialsFileNew = "confCredsNew.xml";
                public static readonly double CredentialsFileVersion = 1.0;
            }
        }
    }
}