using e3;

namespace ProELib
{
    public class Net
    {
        private e3Net net;

        public int Id
        {
            get
            {
                return net.GetId();
            }
            set
            {
                net.SetId(value);
            }
        }

        public bool IsSignalTransfered
        {
            get
            {
                return (net.IsSignalTransferred()==1);
            }
        }

        internal Net(int id, E3ObjectFabric e3ObjectFabric)
        {
            net = e3ObjectFabric.GetNet(id);
        }

        public bool TrySetTransferSignal(bool netTransferSignal)
        {
            int transferSignal = 0;
            if (netTransferSignal)
                transferSignal = 1;
            return (net.SetTransferSignal(transferSignal) == 0);
        }
    }
}
