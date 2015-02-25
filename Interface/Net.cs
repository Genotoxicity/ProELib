using System.Collections.Generic;
using e3;

namespace ProELib
{
    public class Net
    {
        private e3Net net;

        public int Id
        {
            get
            {
                return net.GetId();
            }
            set
            {
                net.SetId(value);
            }
        }

        public List<int> NetSegmentIds
        {
            get
            {
                dynamic netSegmentIds = default(dynamic);
                int netSegmentCount = net.GetNetSegmentIds(ref netSegmentIds);
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
                return (net.IsSignalTransferred()==1);
            }
        }

        internal Net(e3Job job)
        {
            net = job.CreateNetObject();
        }

        public bool TrySetTransferSignal(bool netTransferSignal)
        {
            int transferSignal = 0;
            if (netTransferSignal)
                transferSignal = 1;
            return (net.SetTransferSignal(transferSignal) == 0);
        }
    }
}
