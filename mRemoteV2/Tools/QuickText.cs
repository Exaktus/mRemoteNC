using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mRemoteNC.Tools
{
    public class QuickText
    {
        public string DisplayName;
        public string Text;

        internal string ToCommand(Connection.Info info)
        {
            return Text.Replace("%Password%", info.Password);
        }
    }
}