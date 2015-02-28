using System.Collections.Generic;
using e3;

namespace ProELib
{
    public class NetSegment
    {
        private e3NetSegment e3NetSegment;

        public int Id
        {
            get
            {
                return e3NetSegment.GetId();
            }
            set
            {
                e3NetSegment.SetId(value);
            }
        }

        public List<int> NodeIds
        {
            get
            {
                dynamic nodeIds = default(dynamic);
                int nodeCount = e3NetSegment.GetNodeIds(ref nodeIds);
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
                int symbolCount = e3NetSegment.GetConnectedSymbolIds(ref connectedSymbolIds);
                List<int> ids = new List<int>(symbolCount);
                for (int i = 1; i <= symbolCount; i++)
                    ids.Add(connectedSymbolIds[i]);
                return ids;
            }
        }

        internal NetSegment(e3NetSegment e3NetSegment)
        {
            this.e3NetSegment = e3NetSegment;
        }

        public void Highlight()
        {
            e3NetSegment.Highlight();
        }

    }
}