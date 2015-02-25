using System.Collections.Generic;
using e3;

namespace ProELib
{
    public class NetSegment
    {
        private e3NetSegment netSegment;

        public int Id
        {
            get
            {
                return netSegment.GetId();
            }
            set
            {
                netSegment.SetId(value);
            }
        }

        public List<int> NodeIds
        {
            get
            {
                dynamic nodeIds = default(dynamic);
                int nodeCount = netSegment.GetNodeIds(ref nodeIds);
                List<int> ids = new List<int>(nodeCount);
                for (int i = 1; i <= nodeCount; i++)
                    ids.Add(nodeIds[i]);
                return ids;
            }
        }

        public List<int> ConnectedSymbolIds
        {
            get
            {
                dynamic connectedSymbolIds = default(dynamic);
                int symbolCount = netSegment.GetConnectedSymbolIds(ref connectedSymbolIds);
                List<int> ids = new List<int>(symbolCount);
                for (int i = 1; i <= symbolCount; i++)
                    ids.Add(connectedSymbolIds[i]);
                return ids;
            }
        }

        internal NetSegment(e3Job job)
        {
            netSegment = job.CreateNetSegmentObject();
        }

        public void Highlight()
        {
            netSegment.Highlight();
        }

    }
}