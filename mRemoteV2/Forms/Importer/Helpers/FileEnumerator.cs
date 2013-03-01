using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace mRemoteNC.Forms.Importer.Helpers
{
    static class  FileEnumerator
    {
        static List<string> _listedFiles = new List<string>();
        private static bool _firstGet = true;
        private static readonly object locker = new object();

        public static IEnumerable<string> AllFiles
        { 
            get
            {
                if (_firstGet)
                {
                    Enumerate(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                }
                return _listedFiles;
            } 
        }
        

        private static void ApplyAllFiles(string folder, Action<string> fileAction)
        {
            foreach (var file in Directory.GetFiles(folder))
            {
                fileAction(file);
            }
            foreach (var subDir in Directory.GetDirectories(folder))
            {
                try
                {
                    ApplyAllFiles(subDir, fileAction);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }
        }

        public static void ClearFiles()
        {
            try
            {
                lock (locker)
                {
                    _firstGet = true;
                    _listedFiles.Clear();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public static void Enumerate(string folder)
        {
            try
            {
                lock (locker)
                {
                    _firstGet = false;
                    ApplyAllFiles(folder, _listedFiles.Add);
                    _listedFiles = _listedFiles.Distinct().ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public static void StartEnumeration(string folder)
        {
            ThreadPool.QueueUserWorkItem(state => Enumerate(folder));
        }
    }
}
