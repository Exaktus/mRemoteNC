using mRemoteNC.Connection;

namespace mRemoteNC
{
    public class SSH2 : PuttyBase
    {
        public SSH2()
        {
            PuttyProtocol = Putty_Protocol.ssh;
            PuttySSHVersion = Putty_SSHVersion.ssh2;
        }

        public enum Defaults
        {
            Port = 22
        }
    }
}