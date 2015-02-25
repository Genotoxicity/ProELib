using System.Collections.Generic;
using e3;

namespace ProELib
{
    public class NormalDevice : Device
    {
        private DevicePin pin;

        private DevicePin Pin
        { 
            get
            {
                if (pin == null)
                    pin = new DevicePin(job);
                return pin;
            }
        }

        public List<int> OutlineIds
        {
            get
            {
                dynamic outlineIds = default(dynamic);
                int outlineCount = device.GetOutlineIds(ref outlineIds);
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
                int deviceCount = device.GetDeviceIds(ref deviceIds);
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
                return device.GetCarrierId();
            }
        }

        public int TerminalBlockId
        {
            get
            {
                return device.GetTerminalBlockId();
            }
        }

        public List<int> ConnectedDeviceIds
        {
            get
            {
                List<int> connectedDeviceIds = new List<int>();
                int originalId = Id;
                foreach (int pinId in PinIds)
                {
                    Pin.Id = pinId;
                    int connectedPinId = Pin.ConnectedPinId;
                    if (connectedPinId > 0)
                    {
                        Id = connectedPinId;
                        int connectedId = Id;
                        if (!connectedDeviceIds.Contains(connectedId))
                            connectedDeviceIds.Add(connectedId);
                    }
                }
                Id = originalId;
                return connectedDeviceIds;
            }
        }

        internal NormalDevice(e3Job job)
            : base(job)
        { 
        
        }

        public List<int> GetSymbolIds(SymbolReturnParameter parameter)
        {
            dynamic symbolIds = default(dynamic);
            int symbolCount = device.GetSymbolIds(ref symbolIds, (int) parameter);
            List<int> ids = new List<int>(symbolCount);
            for (int i = 1; i <= symbolCount; i++)
                ids.Add(symbolIds[i]);
            return ids;
        }

        public int Create2DView(string sheetName, string sheetFormat)
        {
            return device.Create2DView(0, sheetName, sheetFormat, 0, 0); 
        }

    }
}
