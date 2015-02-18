using System.Collections.Generic;
using System.Linq;
using e3;

namespace ProELib
{
    public class Signal
    {
        private e3Signal signal;

        public int Id
        {
            get
            {
                return signal.GetId();
            }
            set
            {
                signal.SetId(value);
            }
        }

        public List<int> DevicePinIds
        {
            get
            {
                dynamic pinIds = default(dynamic);
                int pinCount = signal.GetPinIds(ref pinIds);
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
                int coreCount = signal.GetCoreIds(ref signalCoreIds);
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
                return signal.GetName();
            }
            set
            {
                signal.SetName(value);
            }
        }

        internal Signal(int id, E3ObjectFabric e3ObjectFabric)
        {
            signal = e3ObjectFabric.GetSignal(id);
        }

        public int GetIdByName(string signalName)
        {
            return signal.Search(signalName);
        }

    }
}
