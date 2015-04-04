using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using e3;

namespace ProELib
{
    public class E3Project
    {
        private int processId;
        private e3Application app;
        private e3Job job;
        private e3Component e3Component;
        private e3Connection e3Connection;
        private e3Text e3Text;
        private e3Graph e3Graph;
        private e3Group e3Group;
        private e3Net e3Net;
        private e3NetSegment e3NetSegment;
        private e3Outline e3Outline;
        private e3Sheet e3Sheet;
        private e3Signal e3Signal;
        private e3Symbol e3Symbol;
        private e3Device e3Device;
        private e3Pin e3Pin;

        private int wireGroupId = 0;

        public E3Project(int applicationProcessId)
        {
            CT.Dispatcher dispatcher = new CT.Dispatcher();
            processId = applicationProcessId;
            app = dispatcher.GetE3ByProcessId(processId) as e3Application;
            job = app.CreateJobObject();
            e3Component = job.CreateComponentObject();
            e3Connection = job.CreateConnectionObject();
            e3Text = job.CreateTextObject();
            e3Graph = job.CreateGraphObject();
            e3Group = job.CreateGroupObject();
            e3Net = job.CreateNetObject();
            e3NetSegment = job.CreateNetSegmentObject();
            e3Outline = job.CreateOutlineObject();
            e3Sheet = job.CreateSheetObject();
            e3Signal = job.CreateSignalObject();
            e3Symbol = job.CreateSymbolObject();
            e3Device = job.CreateDeviceObject();
            e3Pin = job.CreatePinObject();
            CableDevice = new CableDevice(e3Device);
            NormalDevice = new NormalDevice(e3Device, e3Pin);
            Component = new Component(e3Component);
            CableCore = new CableCore(e3Pin);
            DevicePin = new DevicePin(e3Pin);
            WireCore = new WireCore(e3Pin);
            E3Text = new E3Text(e3Text);
            Graphic = new Graphic(e3Graph);
            Group = new Group(e3Group);
            Sheet = new Sheet(e3Sheet);
            Signal = new Signal(e3Signal);
            Net = new Net(e3Net);
            NetSegment = new NetSegment(e3NetSegment);
            Symbol = new Symbol(e3Symbol);
            Connection = new Connection(e3Connection);
            Outline = new Outline(e3Outline);
        }

        #region Ids Properties
        private int WireGroupId
        {
            get
            {
                if (wireGroupId == 0)
                {
                    dynamic cableIds = default(dynamic);
                    int cableCount = job.GetCableIds(ref cableIds);
                    for (int i = 1; i <= cableCount; i++)
                    {
                        e3Device.SetId(cableIds[i]);
                        if (e3Device.IsWireGroup() == 1)  // определение устройства "Провода" содержащего в себе все провода в проекте
                        {
                            wireGroupId = cableIds[i];
                            break;
                        }
                    }
                }
                return wireGroupId;
            }
        }

        public List<int> WireIds
        {
            get
            {
                if (WireGroupId == 0)   // если проводов не найдено
                    return new List<int>(0);   // возвращаем пустой список
                e3Device.SetId(WireGroupId);
                dynamic wireIds = default(dynamic);
                int wireCount = e3Device.GetCoreIds(ref wireIds);
                List<int> ids = new List<int>(wireCount);
                for (int i = 1; i <= wireCount; i++)    // e3 в [0] всегда возвращает null
                    ids.Add(wireIds[i]);
                return ids;
            }
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

        public int ActiveSheetId
        {
            get
            {
                return job.GetActiveSheetId();
            }
        }

        public List<int> TreeSelectedAllDeiveIds
        {
            get
            {
                dynamic treeSelectedAllDeviceIds = default(dynamic);
                int deviceCount = job.GetTreeSelectedAllDeviceIds(ref treeSelectedAllDeviceIds);
                List<int> ids = new List<int>(deviceCount);
                for (int i = 1; i <= deviceCount; i++)    // e3 в [0] всегда возвращает null
                    ids.Add(treeSelectedAllDeviceIds[i]);
                return ids;
            }
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
        #endregion

        public CableDevice CableDevice {get; private set;}

        public NormalDevice NormalDevice { get; private set; }

        public Component Component { get; private set; }

        public CableCore CableCore { get; private set; }

        public DevicePin DevicePin { get; private set; }

        public WireCore WireCore { get; private set; }

        public E3Text E3Text { get; private set; }

        public Graphic Graphic { get; private set; }

        public Group Group { get; private set; }

        public Sheet Sheet { get; private set; }

        public Signal Signal { get; private set; }

        public Net Net { get; private set; }

        public NetSegment NetSegment { get; private set; }

        public Symbol Symbol { get; private set; }

        public Connection Connection { get; private set; }

        public Outline Outline { get; private set; }

        public List<int> GetSheetIdsByType(int schematicTypeCode)
        {
            int originalId = Sheet.Id;
            List<int> ids = new List<int>();
            foreach (int sheetId in SheetIds)
            {
                Sheet.Id = sheetId;
                if (Sheet.SchematicTypes.Contains(schematicTypeCode))
                    ids.Add(sheetId);
            }
            Sheet.Id = originalId;
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
