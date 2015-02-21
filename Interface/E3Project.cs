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

        public List<int> TerminalIds
        {
            get
            {
                dynamic terminalIds = default(dynamic);
                int terminalCount = e3ObjectFabric.GetJob().GetTerminalIds(ref terminalIds);
                List<int> ids = new List<int>(terminalCount);
                for (int i = 1; i <= terminalCount; i++)    // e3 в [0] всегда возвращает null
                    ids.Add(terminalIds[i]);
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

        public CableDevice CreateCableDevice()
        {
            return new CableDevice(0, e3ObjectFabric);
        }

        public Component CreateComponent()
        {
            return new Component(0, e3ObjectFabric);
        }

        public CableCore CreateCableCore()
        {
            return new CableCore(0, e3ObjectFabric);
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

        public NormalDevice CreateNormalDevice()
        {
            return new NormalDevice(0, e3ObjectFabric);
        }

        public DevicePin CreateDevicePin()
        {
            return new DevicePin(0, e3ObjectFabric);
        }

        public WireCore CreateWireCore()
        {
            return new WireCore(0, e3ObjectFabric);
        }

        public E3Text CreateText()
        {
            return new E3Text(0, e3ObjectFabric);
        }

        public Graphic CreateGraphic()
        {
            return new Graphic(0, e3ObjectFabric);
        }

        public Group CreateGroup()
        {
            return new Group(0, e3ObjectFabric);
        }

        public Sheet CreateSheet()
        {
            return new Sheet(0, e3ObjectFabric);
        }

        public Signal CreateSignal()
        {
            return new Signal(0, e3ObjectFabric);
        }

        public Net CreateNet()
        {
            return new Net(0, e3ObjectFabric);
        }

        public Symbol CreateSymbol()
        {
            return new Symbol(0, e3ObjectFabric);
        }

        public Connection CreateConnection()
        {
            return new Connection(0, e3ObjectFabric);
        }

        public Outline CreateOutline()
        {
            return new Outline(0, e3ObjectFabric);
        }

        public List<int> GetSheetIdsByType(int schematicTypeCode)
        {
            Sheet sheet = CreateSheet();
            List<int> ids = new List<int>();
            foreach (int sheetId in SheetIds)
            {
                sheet.Id = sheetId;
                if (sheet.SchematicTypes.Contains(schematicTypeCode))
                    ids.Add(sheetId);
            }
            return ids;
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
