using System.Collections.Generic;
using e3;

namespace ProELib
{
    public class CableDevice : Device
    {
        public List<int> CoreIds
        {
            get
            {
                dynamic cableCoreIds = default(dynamic);
                int coreCount = device.GetAllCoreIds(ref cableCoreIds);
                List<int> ids = new List<int>(coreCount);
                for (int i = 1; i <= coreCount; i++)
                    ids.Add(cableCoreIds[i]);
                return ids;
            }
        }

        internal CableDevice(e3Job job)
            : base(job)
        {
        }
    }
}
