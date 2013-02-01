namespace mRemoteNC
{
    public class HTTPS : HTTPBase
    {
        public HTTPS(RenderingEngine RenderingEngine)
            : base(RenderingEngine)
        {
        }

        public override void NewExtended()
        {
            base.NewExtended();

            httpOrS = "https";
            defaultPort = System.Convert.ToInt32(Defaults.Port);
        }

        public enum Defaults
        {
            Port = 443
        }
    }
}

namespace mRemoteNC.Connection
{
}