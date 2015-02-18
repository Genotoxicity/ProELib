using e3;

namespace ProELib
{
    public abstract class Pin
    {
        protected E3ObjectFabric e3ObjectFabric;
        protected e3Pin pin;

        public virtual int Id
        {
            get
            {
                return pin.GetId();
            }
            set
            {
                pin.SetId(value);
            }
        }

        public string Name
        {
            get
            {
                return pin.GetName();
            }
            set
            {
                pin.SetName(value);
            }
        }

        public string SignalName
        {
            get
            {
                return pin.GetSignalName();
            }
            set
            {
                pin.SetSignalName(value);
            }

        }

        protected Pin(int id, E3ObjectFabric e3ObjectFabric)
        {
            this.e3ObjectFabric = e3ObjectFabric;
            pin = e3ObjectFabric.GetPin(id);
        }

        public string GetAttributeValue(string attributeName)
        {
            return pin.GetAttributeValue(attributeName);
        }
    }
}
