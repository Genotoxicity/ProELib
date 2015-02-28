using e3;

namespace ProELib
{
    public class WireCore : Core
    {
        public string WireType
        {
            get
            {
                dynamic wireType = default(dynamic);
                dynamic wireName = default(dynamic);
                e3Pin.GetWireType(ref wireType, ref wireName);
                return wireType;
            }
        }

        internal WireCore(e3Pin e3Pin)
            : base(e3Pin)
        {
        }
    }
}
