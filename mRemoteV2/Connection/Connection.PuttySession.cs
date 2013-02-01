using System.ComponentModel;

namespace mRemoteNC.Connection
{
    public class PuttySession : StringConverter
    {
        public static string[] PuttySessions = new string[] { };

        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(
            System.ComponentModel.ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(PuttySessions);
        }

        public override bool GetStandardValuesExclusive(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}