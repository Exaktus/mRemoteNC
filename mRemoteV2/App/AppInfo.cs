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

            public const bool IsPortable = 
#if !PORTABLE 
                false; 
#else 
                true; 
#endif

            public static readonly string HomePath =
                (new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase()).Info.DirectoryPath;

            public const string EncryptionKey = "mR3m";
            public const string ReportingFilePath = "";
        }

        public static class Settings
        {
            public static readonly string SettingsPath =
#if !PORTABLE
                (string)
                (System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\" +
                    (new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase()).Info.ProductName);
#else
            (new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase()).Info.DirectoryPath;
#endif
            public const string LayoutFileName = "pnlLayout.xml";
            public const string ExtAppsFilesName = "extApps.xml";
            public const string QuickTextsFilesName = "quickTexts.xml";
        }

        public static class Update
        {
            public const string URL = "http://exaktus.github.com/mRemoteNC/update/";
#if PORTABLE

#if DEBUG
            public const string File = "update-portable-debug.txt";
#else //DEBUG
            public const string File = "update-portable.txt";
#endif //DEBUG

#else //PORTABLE

#if DEBUG
            public const string File = "update-debug.txt";
#else //DEBUG
            public const string File = "update.txt";
#endif //DEBUG

#endif //PORTABLE
        }

        public static class Connections
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