using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using e3;

namespace ProELib
{
    public class E3Project
    {
        private e3Application app;
        private e3Job job;
        private int processId;

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
            CT.Dispatcher dispatcher = new CT.Dispatcher();
            processId = applicationProcessId;
            app = dispatcher.GetE3ByProcessId(processId) as e3Application;
            job = app.CreateJobObject();
        }

        public List<int> WireIds
        {
            get
            {
                if (WireGroupId == undefinedId)   // если проводов не найдено
                    return new List<int>(0);   // возвращаем пустой список
                e3Device wireGroup = job.CreateDeviceObject();
                wireGroup.SetId(WireGroupId);
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
            int cableCount = job.GetCableIds(ref cableIds);
            e3Device device = job.CreateDeviceObject();
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
                int cableCount = job.GetCableIds(ref cableIds);
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
                int deviceCount = job.GetDeviceIds(ref deviceIds);
                List<int> ids = new List<int>(deviceCount);
                for (int i = 1; i <= deviceCount; i++)    // e3 в [0] всегда возвращает null
                    ids.Add(deviceIds[i]);
                return ids;
            }
        }

        public List<int> SymbolIds
        {
            get
            {
                dynamic symbolIds = default(dynamic);
                int symbolCount = job.GetSymbolIds(ref symbolIds);
                List<int> ids = new List<int>(symbolCount);
                for (int i = 1; i <= symbolCount; i++)    // e3 в [0] всегда возвращает null
                    ids.Add(symbolIds[i]);
                return ids;
            }
        }

        public List<int> NetIds
        {
            get
            {
                dynamic netlIds = default(dynamic);
                int netCount = job.GetNetIds(ref netlIds);
                List<int> ids = new List<int>(netCount);
                for (int i = 1; i <= netCount; i++)    // e3 в [0] всегда возвращает null
                    ids.Add(netlIds[i]);
                return ids;
            }
        }

        public List<int> TerminalIds
        {
            get
            {
                dynamic terminalIds = default(dynamic);
                int terminalCount =job.GetTerminalIds(ref terminalIds);
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
                int allDeviceCount = job.GetAllDeviceIds(ref allDeviceIds);
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
                int sheetCount = job.GetTreeSelectedSheetIds(ref treeSelectedSheetIds);
                List<int> ids = new List<int>(sheetCount);
                for (int i = 1; i <= sheetCount; i++)    // e3 в [0] всегда возвращает null
                    ids.Add(treeSelectedSheetIds[i]);
                return ids;
            }
        }

        public CableDevice CreateCableDevice()
        {
            return new CableDevice(job);
        }

        public Component CreateComponent()
        {
            return new Component(job);
        }

        public CableCore CreateCableCore()
        {
            return new CableCore(job);
        }

        public List<int> SignalIds
        {
            get
            {
                dynamic signalIds = default(dynamic);
                int signalCount = job.GetSignalIds(ref signalIds);
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
                int selectedSignalCount = job.GetSelectedSignalIds(ref selectedSignalIds);
                List<int> ids = new List<int>(selectedSignalCount);
                for (int i = 1; i <= selectedSignalCount; i++)    // e3 в [0] всегда возвращает null
                    ids.Add(selectedSignalIds[i]);
                return ids;
            }
        }

        public List<int> SelectedAllDeviceIds
        {
            get
            {
                dynamic selectedAllDeviceIds = default(dynamic);
                int selectedAllDeviceCount = job.GetSelectedAllDeviceIds(ref selectedAllDeviceIds);
                List<int> ids = new List<int>(selectedAllDeviceCount);
                for (int i = 1; i <= selectedAllDeviceCount; i++)    // e3 в [0] всегда возвращает null
                    ids.Add(selectedAllDeviceIds[i]);
                return ids;
            }
        }

        public List<int> ConnectionIds
        {
            get
            {
                dynamic connectionIds = default(dynamic);
                int connectionCount = job.GetAllConnectionIds(ref connectionIds);
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
                int selectionConnectionCount = job.GetSelectedConnectionIds(ref selectedConnectionIds);
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
                int selectionCableCount = job.GetSelectedCableIds(ref selectedCableIds);
                List<int> ids = new List<int>();
                for (int i = 1; i <= selectionCableCount; i++)  // e3 в [0] всегда возвращает null
                    ids.Add(selectedCableIds[i]);
                selectionCableCount = job.GetTreeSelectedCableIds(ref selectedCableIds);
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
                int sheetCount = job.GetSheetIds(ref sheetIds);
                List<int> ids = new List<int>(sheetCount);
                for (int i = 1; i <= sheetCount; i++)
                    ids.Add(sheetIds[i]);
                return ids;
            }
        }

        public NormalDevice CreateNormalDevice()
        {
            return new NormalDevice(job);
        }

        public DevicePin CreateDevicePin()
        {
            return new DevicePin(job);
        }

        public WireCore CreateWireCore()
        {
            return new WireCore(job);
        }

        public E3Text CreateText()
        {
            return new E3Text(job);
        }

        public Graphic CreateGraphic()
        {
            return new Graphic(job);
        }

        public Group CreateGroup()
        {
            return new Group(job);
        }

        public Sheet CreateSheet()
        {
            return new Sheet(job);
        }

        public Signal CreateSignal()
        {
            return new Signal(job);
        }

        public Net CreateNet()
        {
            return new Net(job);
        }

        public NetSegment CreateNetSegment()
        {
            return new NetSegment(job);
        }

        public Symbol CreateSymbol()
        {
            return new Symbol(job);
        }

        public Connection CreateConnection()
        {
            return new Connection(job);
        }

        public Outline CreateOutline()
        {
            return new Outline(job);
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
            return job.JumpToID(id);
        }

        public void PutInfo(string value)
        {
            app.PutInfo(0, value);
        }

        public RGB GetRGB(int code)
        {
            dynamic r = default(dynamic);
            dynamic g = default(dynamic);
            dynamic b = default(dynamic);
            job.GetRGBValue(code, ref r, ref g, ref b);
            return new RGB((byte)r, (byte)g, (byte)b);
        }

        public void Release()
        {
            app.Quit();
            Marshal.FinalReleaseComObject(app);
            Marshal.FinalReleaseComObject(job);
        }

    }
}
