using System.Collections.Generic;
using System.Linq;
using e3;

namespace ProELib
{
    public class Signal
    {
        private e3Signal e3Signal;

        public int Id
        {
            get
            {
                return e3Signal.GetId();
            }
            set
            {
                e3Signal.SetId(value);
            }
        }

        public List<int> DevicePinIds
        {
            get
            {
                dynamic pinIds = default(dynamic);
                int pinCount = e3Signal.GetPinIds(ref pinIds);
                List<int> ids = new List<int>(pinCount);
                for (int i = 1; i <= pinCount; i++)
                    ids.Add(pinIds[i]);
                if (CoreIds.Count > 0)
                {
                    IEnumerable<int> except = ids.Except<int>(CoreIds);
                    ids = except.ToList<int>();
                }
                return ids;
            }
        }

        public List<int> CoreIds
        {
            get
            {
                dynamic signalCoreIds = default(dynamic);
                int coreCount = e3Signal.GetCoreIds(ref signalCoreIds);
                List<int> ids = new List<int>(coreCount);
                for (int i = 1; i <= coreCount; i++)
                    ids.Add(signalCoreIds[i]);
                return ids;
            }
        }

        public string Name
        {
            get
            {
                return e3Signal.GetName();
            }
            set
            {
                e3Signal.SetName(value);
            }
        }

        internal Signal(e3Signal e3Signal)
        {
            this.e3Signal = e3Signal;
        }

        public int GetIdByName(string signalName)
        {
            return e3Signal.Search(signalName);
        }

    }
}
