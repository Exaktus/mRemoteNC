using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using RDPFileReader;
using mRemoteNC.Connection;

namespace mRemoteNC.Forms.Importer.Helpers
{
    class RDPFileImporter:IConfigSearcher
    {
        public bool SearchInFiles { get { return true; } }

        public IEnumerable<Info> GetConnections()
        {
            var result = new List<Info>();
            try
            {
                result.AddRange(FileEnumerator.AllFiles.AsParallel().Where(s => s.EndsWith(".rdp")).Select(RDPFileToConnectionInfo));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return result;
        }

        private Info RDPFileToConnectionInfo(string rdpFile)
        {
            var rdp = new RDPFile();
            rdp.Read(rdpFile);
            return new Info
                       {
                           Name = Path.GetFileNameWithoutExtension(rdpFile) + " - Imported",
                           Icon = "Remote Desktop",
                           Description = Path.GetFileNameWithoutExtension(rdpFile) + " - RDP File",
                           Hostname = rdp.FullAddress,
                           Username = rdp.Username,
                           RedirectDiskDrives = Convert.ToBoolean(rdp.RedirectDrives),
                           DisplayThemes = !Convert.ToBoolean(rdp.DisableThemes)
                           //ToDO:
                       };
        }
    }
}
