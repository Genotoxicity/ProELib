using System.Collections.Generic;

namespace ProELib
{
    public class Core : Pin
    {
        public int StartPinId
        {
            get
            {
                return pin.GetEndPinId(1);
            }
        }

        public int EndPinId
        {
            get
            {
                return pin.GetEndPinId(2);
            }
        }

        public List<int> ConnectedPinIds
        {
            get
            {
                return new List<int>(2) { pin.GetEndPinId(1), pin.GetEndPinId(2) };
            }
        }

        internal Core(int id, E3ObjectFabric e3ObjectFabric)
            : base(id, e3ObjectFabric)
        {
        }
    }
}
