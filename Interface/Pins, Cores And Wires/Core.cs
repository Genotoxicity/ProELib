using System.Collections.Generic;
using e3;

namespace ProELib
{
    public class Core : Pin
    {
        public int StartPinId
        {
            get
            {
                return e3Pin.GetEndPinId(1);
            }
        }

        public int EndPinId
        {
            get
            {
                return e3Pin.GetEndPinId(2);
            }
        }

        public List<int> ConnectedPinIds
        {
            get
            {
                return new List<int>(2) { e3Pin.GetEndPinId(1), e3Pin.GetEndPinId(2) };
            }
        }

        internal Core(e3Pin e3Pin)
            : base(e3Pin)
        {
        }
    }
}
