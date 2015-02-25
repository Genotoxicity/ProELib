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
                pin.GetWireType(ref wireType, ref wireName);
                return wireType;
            }
        }

        internal WireCore(e3Job job)
            : base(job)
        {
        }
    }
}
