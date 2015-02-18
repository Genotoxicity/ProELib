using System.Runtime.InteropServices;
using e3;

namespace ProELib
{
    public class E3ObjectFabric
    {

        CT.Dispatcher dispatcher = new CT.Dispatcher();
        private e3Application app;
        private e3Job job;
        private int processId;

        internal E3ObjectFabric(int applicationProcessId)
        {
            dispatcher = new CT.Dispatcher();
            processId = applicationProcessId;
            app = dispatcher.GetE3ByProcessId(processId) as e3Application;
            job = app.CreateJobObject();
        }

        internal e3Application GetApplication()
        {
            return app;
        }

        internal e3Job GetJob()
        {
            return job;
        }

        internal e3Device GetDevice(int id)
        {
            e3Device device = job.CreateDeviceObject();
            device.SetId(id);
            return device;
        }

        internal e3Component GetComponent(int id)
        {
            e3Component component = job.CreateComponentObject();
            component.SetId(id);
            return component;
        }

        internal e3Pin GetPin(int id)
        {
            e3Pin pin = job.CreatePinObject();
            pin.SetId(id);
            return pin;
        }

        internal e3Sheet GetSheet(int id)
        {
            e3Sheet sheet = job.CreateSheetObject();
            sheet.SetId(id);
            return sheet;
        }

        internal e3Symbol GetSymbol(int id)
        {
            e3Symbol symbol = job.CreateSymbolObject();
            symbol.SetId(id);
            return symbol;
        }

        internal e3Graph GetGraph(int id)
        {
            e3Graph graph = job.CreateGraphObject();
            graph.SetId(id);
            return graph;
        }

        internal e3Text GetText(int id)
        {
            e3Text text = job.CreateTextObject();
            text.SetId(id);
            return text;
        }

        internal e3Group GetGroup(int id)
        {
            e3Group group = job.CreateGroupObject();
            group.SetId(id);
            return group;
        }

        internal e3Signal GetSignal(int id)
        {
            e3Signal signal = job.CreateSignalObject();
            signal.SetId(id);
            return signal;
        }

        internal e3Net GetNet(int id)
        {
            e3Net net = job.CreateNetObject();
            net.SetId(id);
            return net;
        }

        internal e3Outline GetOutline(int id)
        {
            e3Outline outline = job.CreateOutlineObject();
            outline.SetId(id);
            return outline;
        }

        internal e3Connection GetConnection(int id)
        {
            e3Connection connection = job.CreateConnectionObject();
            connection.SetId(id);
            return connection;
        }

        internal void Release()
        {
            app.Quit();
            Marshal.FinalReleaseComObject(app);
            Marshal.FinalReleaseComObject(job);
        }
    }
}
