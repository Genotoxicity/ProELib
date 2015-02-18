using System.Collections.Generic;
using System.Linq;
using e3;

namespace ProELib
{
    public class E3Project
    {
        private E3ObjectFabric e3ObjectFabric;
        private int undefinedId = -1;
        private int wireGroupId = 0;

        private int WireGroupId
        {
            get
            {
                if (wireGroupId == 0)
                    wireGroupId = GetWireGroupId();
                return wireGroupId;
            }
        }

        public E3Project(int applicationProcessId)
        {
            e3ObjectFabric = new E3ObjectFabric(applicationProcessId);
        }

        public List<int> WireIds
        {
            get
            {
                if (WireGroupId == undefinedId)   // если проводов не найдено
                    return new List<int>(0);   // возвращаем пустой список
                e3Device wireGroup = e3ObjectFabric.GetDevice(WireGroupId);
                dynamic wireIds = default(dynamic);
                int wireCount = wireGroup.GetCoreIds(ref wireIds);
                List<int> ids = new List<int>(wireCount);
                for (int i = 1; i <= wireCount; i++)    // e3 в [0] всегда возвращает null
                    ids.Add(wireIds[i]);
                return ids;
            }
        }

        private int GetWireGroupId()
        {
            dynamic cableIds = default(dynamic);
            int cableCount = e3ObjectFabric.GetJob().GetCableIds(ref cableIds);
            e3Device device = e3ObjectFabric.GetJob().CreateDeviceObject();
            for (int i = 1; i <= cableCount; i++)
            {
                device.SetId(cableIds[i]);
                if (device.IsWireGroup() == 1)  // определение устройства "Провода" содержащего в себе все провода в проекте
                    return cableIds[i];
            }
            return undefinedId;   // если проводов не найдено
        }

        public List<int> CableIds
        {
            get
            {
                dynamic cableIds = default(dynamic);
                int cableCount = e3ObjectFabric.GetJob().GetCableIds(ref cableIds);
                List<int> ids = new List<int>(cableCount);
                for (int i = 1; i <= cableCount; i++)    // e3 в [0] всегда возвращает null
                    ids.Add(cableIds[i]);
                ids.Remove(WireGroupId);    // удаляем провода
                return ids;
            }
        }

        public List<int> DeviceIds
        {
            get
            {
                dynamic deviceIds = default(dynamic);
                int deviceCount = e3ObjectFabric.GetJob().GetDeviceIds(ref deviceIds);
                List<int> ids = new List<int>(deviceCount);
                for (int i = 1; i <= deviceCount; i++)    // e3 в [0] всегда возвращает null
                    ids.Add(deviceIds[i]);
                return ids;
            }
        }

        public List<int> AllDeviceIds
        {
            get
            {
                dynamic allDeviceIds = default(dynamic);
                int allDeviceCount = e3ObjectFabric.GetJob().GetAllDeviceIds(ref allDeviceIds);
                List<int> ids = new List<int>(allDeviceCount);
                for (int i = 1; i <= allDeviceCount; i++)    // e3 в [0] всегда возвращает null
                    ids.Add(allDeviceIds[i]);
                return ids;
            }
        }

        public List<int> TreeSelectedSheetIds
        {
            get
            {
                dynamic treeSelectedSheetIds = default(dynamic);
                int sheetCount = e3ObjectFabric.GetJob().GetTreeSelectedSheetIds(ref treeSelectedSheetIds);
                List<int> ids = new List<int>(sheetCount);
                for (int i = 1; i <= sheetCount; i++)    // e3 в [0] всегда возвращает null
                    ids.Add(treeSelectedSheetIds[i]);
                return ids;
            }
        }

        public CableDevice GetCableDeviceById(int id)
        {
            return new CableDevice(id, e3ObjectFabric);
        }

        public Component GetComponentById(int id)
        {
            return new Component(id, e3ObjectFabric);
        }

        public CableCore GetCableCoreById(int id)
        {
            return new CableCore(id, e3ObjectFabric);
        }

        public List<int> SignalIds
        {
            get
            {
                dynamic signalIds = default(dynamic);
                int signalCount = e3ObjectFabric.GetJob().GetSignalIds(ref signalIds);
                List<int> ids = new List<int>(signalCount);
                for (int i = 1; i <= signalCount; i++)    // e3 в [0] всегда возвращает null
                    ids.Add(signalIds[i]);
                return ids;
            }
        }

        public List<int> SelectedSignalIds
        {
            get
            {
                dynamic selectedSignalIds = default(dynamic);
                int selectedSignalCount = e3ObjectFabric.GetJob().GetSelectedSignalIds(ref selectedSignalIds);
                List<int> ids = new List<int>(selectedSignalCount);
                for (int i = 1; i <= selectedSignalCount; i++)    // e3 в [0] всегда возвращает null
                    ids.Add(selectedSignalIds[i]);
                return ids;
            }
        }

        public List<int> ConnectionIds
        {
            get
            {
                dynamic connectionIds = default(dynamic);
                int connectionCount = e3ObjectFabric.GetJob().GetAllConnectionIds(ref connectionIds);
                List<int> ids = new List<int>(connectionCount);
                for (int i = 1; i <= connectionCount; i++)  // e3 в [0] всегда возвращает null
                    ids.Add(connectionIds[i]);
                return ids;
            }
        }

        public List<int> SelectedConnectionIds
        {
            get
            {
                dynamic selectedConnectionIds = default(dynamic);
                int selectionConnectionCount = e3ObjectFabric.GetJob().GetSelectedConnectionIds(ref selectedConnectionIds);
                List<int> ids = new List<int>(selectionConnectionCount);
                for (int i = 1; i <= selectionConnectionCount; i++)  // e3 в [0] всегда возвращает null
                    ids.Add(selectedConnectionIds[i]);
                return ids;
            }
        }

        public List<int> SelectedCableIds
        {
            get
            {
                dynamic selectedCableIds = default(dynamic);
                int selectionCableCount = e3ObjectFabric.GetJob().GetSelectedCableIds(ref selectedCableIds);
                List<int> ids = new List<int>();
                for (int i = 1; i <= selectionCableCount; i++)  // e3 в [0] всегда возвращает null
                    ids.Add(selectedCableIds[i]);
                selectionCableCount = e3ObjectFabric.GetJob().GetTreeSelectedCableIds(ref selectedCableIds);
                for (int i = 1; i <= selectionCableCount; i++)
                    ids.Add(selectedCableIds[i]);
                ids = ids.Distinct().ToList();
                return ids;
            }
        }

        public List<int> SheetIds
        {
            get
            {
                dynamic sheetIds = default(dynamic);
                int sheetCount = e3ObjectFabric.GetJob().GetSheetIds(ref sheetIds);
                List<int> ids = new List<int>(sheetCount);
                for (int i = 1; i <= sheetCount; i++)
                    ids.Add(sheetIds[i]);
                return ids;
            }
        }

        public NormalDevice GetNormalDeviceById(int id)
        {
            return new NormalDevice(id, e3ObjectFabric);
        }

        public DevicePin GetDevicePinById(int id)
        {
            return new DevicePin(id, e3ObjectFabric);
        }

        public WireCore GetWireCoreById(int id)
        {
            return new WireCore(id, e3ObjectFabric);
        }

        public E3Text GetTextById(int id)
        {
            return new E3Text(id, e3ObjectFabric);
        }

        public Graphic GetGraphicById(int id)
        {
            return new Graphic(id, e3ObjectFabric);
        }

        public Group GetGroupById(int id)
        {
            return new Group(id, e3ObjectFabric);
        }

        public Sheet GetSheetById(int id)
        {
            return new Sheet(id, e3ObjectFabric);
        }

        public Signal GetSignalById(int id)
        {
            return new Signal(id, e3ObjectFabric);
        }

        public Net GetNetById(int id)
        {
            return new Net(id, e3ObjectFabric);
        }

        public Symbol GetSymbolById(int id)
        {
            return new Symbol(id, e3ObjectFabric);
        }

        public Connection GetConnectionById(int id)
        {
            return new Connection(id, e3ObjectFabric);
        }

        public Outline GetOutlineById(int id)
        {
            return new Outline(id, e3ObjectFabric);
        }

        public int JumpToId(int id)
        {
            return e3ObjectFabric.GetJob().JumpToID(id);
        }

        public void PutInfo(string value)
        {
            e3ObjectFabric.GetApplication().PutInfo(0, value);
        }

        public void Release()
        {
            e3ObjectFabric.Release();
        }

    }
}
