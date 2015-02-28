using System.Collections.Generic;
using e3;

namespace ProELib
{
    public class NormalDevice : Device
    {
        private e3Pin e3Pin;

        public List<int> OutlineIds
        {
            get
            {
                dynamic outlineIds = default(dynamic);
                int outlineCount = e3Device.GetOutlineIds(ref outlineIds);
                List<int> ids = new List<int>(outlineCount);
                for (int i = 1; i <= outlineCount; i++)
                    ids.Add(outlineIds[i]);
                return ids;
            }
        }

        public List<int> DeviceIds
        {
            get
            {
                dynamic deviceIds = default(dynamic);
                int deviceCount = e3Device.GetDeviceIds(ref deviceIds);
                List<int> ids = new List<int>(deviceCount);
                for (int i = 1; i <= deviceCount; i++)
                    ids.Add(deviceIds[i]);
                return ids;
            }
        }

        public int CarrierId
        {
            get
            {
                return e3Device.GetCarrierId();
            }
        }

        public int TerminalBlockId
        {
            get
            {
                return e3Device.GetTerminalBlockId();
            }
        }

        public List<int> ConnectedDeviceIds
        {
            get
            {
                List<int> connectedDeviceIds = new List<int>();
                int originalId = Id;
                int originalPinId = e3Pin.GetId();
                foreach (int pinId in PinIds)
                {
                    e3Pin.SetId(pinId);
                    int connectedPinId = e3Pin.GetConnectedPinId();
                    if (connectedPinId > 0)
                    {
                        Id = connectedPinId;
                        int connectedId = Id;
                        if (!connectedDeviceIds.Contains(connectedId))
                            connectedDeviceIds.Add(connectedId);
                    }
                }
                Id = originalId;
                e3Pin.SetId(originalPinId);
                return connectedDeviceIds;
            }
        }

        internal NormalDevice(e3Device e3Device, e3Pin e3Pin)
            : base(e3Device)
        {
            this.e3Pin = e3Pin;
        }

        public List<int> GetSymbolIds(SymbolReturnParameter parameter)
        {
            dynamic symbolIds = default(dynamic);
            int symbolCount = e3Device.GetSymbolIds(ref symbolIds, (int) parameter);
            List<int> ids = new List<int>(symbolCount);
            for (int i = 1; i <= symbolCount; i++)
                ids.Add(symbolIds[i]);
            return ids;
        }

        public int Create2DView(string sheetName, string sheetFormat)
        {
            return e3Device.Create2DView(0, sheetName, sheetFormat, 0, 0); 
        }

    }
}
