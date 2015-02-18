using System.Collections.Generic;

namespace ProELib
{
    public class NormalDevice : Device
    {
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

        internal NormalDevice(int id, E3ObjectFabric e3ObjectFabric)
            : base(id, e3ObjectFabric)
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
