using System.Collections.Generic;
using e3;

namespace ProELib
{
    public class Net
    {
        private e3Net e3Net;

        public int Id
        {
            get
            {
                return e3Net.GetId();
            }
            set
            {
                e3Net.SetId(value);
            }
        }

        public List<int> NetSegmentIds
        {
            get
            {
                dynamic netSegmentIds = default(dynamic);
                int netSegmentCount = e3Net.GetNetSegmentIds(ref netSegmentIds);
                List<int> ids = new List<int>(netSegmentCount);
                for (int i = 1; i <= netSegmentCount; i++)
                    ids.Add(netSegmentIds[i]);
                return ids;
            }
        }

        public bool IsSignalTransfered
        {
            get
            {
                return (e3Net.IsSignalTransferred()==1);
            }
        }

        internal Net(e3Net e3Net)
        {
            this.e3Net = e3Net;
        }

        public bool TrySetTransferSignal(bool netTransferSignal)
        {
            int transferSignal = 0;
            if (netTransferSignal)
                transferSignal = 1;
            return (e3Net.SetTransferSignal(transferSignal) == 0);
        }
    }
}
