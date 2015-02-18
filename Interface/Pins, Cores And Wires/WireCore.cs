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

        internal WireCore(int id, E3ObjectFabric e3ObjectFabric)
            : base(id, e3ObjectFabric)
        {
        }
    }
}
