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
                int coreCount = e3Device.GetAllCoreIds(ref cableCoreIds);
                List<int> ids = new List<int>(coreCount);
                for (int i = 1; i <= coreCount; i++)
                    ids.Add(cableCoreIds[i]);
                return ids;
            }
        }

        internal CableDevice(e3Device e3Device)
            : base(e3Device)
        {
        }
    }
}
