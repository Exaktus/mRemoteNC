using System.Collections.Generic;
using mRemoteNC.Connection;

namespace mRemoteNC.Forms.Importer.Helpers
{
    internal interface IConfigSearcher
    {
        bool SearchInFiles { get;}
        IEnumerable<Info> GetConnections();
    }
}
 