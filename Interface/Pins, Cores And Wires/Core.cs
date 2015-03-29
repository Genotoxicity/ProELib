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

        public List<int> NetSegmentIds
        {
            get
            {
                dynamic netSegmentIds = default(dynamic);
                int netSegmentCount = e3Pin.GetNetSegmentIds(ref netSegmentIds);
                List<int> ids = new List<int>(netSegmentCount);
                for (int i = 1; i <= netSegmentCount; i++)
                    ids.Add(netSegmentIds[i]);
                return ids;
            }
        }

        internal Core(e3Pin e3Pin)
            : base(e3Pin)
        {
        }
    }
}
