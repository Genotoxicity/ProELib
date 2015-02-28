using System.Collections.Generic;
using e3;

namespace ProELib
{
    public abstract class Pin
    {
        protected e3Pin e3Pin;

        public virtual int Id
        {
            get
            {
                return e3Pin.GetId();
            }
            set
            {
                e3Pin.SetId(value);
            }
        }

        public string Name
        {
            get
            {
                return e3Pin.GetName();
            }
            set
            {
                e3Pin.SetName(value);
            }
        }

        public string SignalName
        {
            get
            {
                return e3Pin.GetSignalName();
            }
            set
            {
                e3Pin.SetSignalName(value);
            }

        }

        public List<int> NodeIds
        {
            get
            {
                dynamic nodeIds = default(dynamic);
                int nodeCount = e3Pin.GetNodeIds(ref nodeIds);
                List<int> ids = new List<int>(nodeCount);
                for (int i = 1; i <= nodeCount; i++)
                    ids.Add(nodeIds[i]);
                return ids;
            }
        }

        protected Pin(e3Pin e3Pin)
        {
            this.e3Pin = e3Pin ;
        }

        public string GetAttributeValue(string attributeName)
        {
            return e3Pin.GetAttributeValue(attributeName);
        }
    }
}
