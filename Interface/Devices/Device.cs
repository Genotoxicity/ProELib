using System.Collections.Generic;
using e3;

namespace ProELib
{
    public class Device
    {
        protected E3ObjectFabric e3ObjectFabric;
        protected e3Device device;
        protected e3Component component;

        public virtual int Id
        {
            get
            {
                return device.GetId();
            }
            set
            {
                device.SetId(value);
                if (component != null)
                    component.SetId(value);
            }
        }

        public string Name
        {
            get
            {
                return device.GetName();
            }
            set
            {
                device.SetName(value);
            }
        }

        public string ComponentName
        {
            get
            {
                if (component == null)
                    component = e3ObjectFabric.GetComponent(Id);
                return component.GetName();
            }
        }

        public string Assignment
        {
            get
            {
                return device.GetAssignment();
            }
        }

        public string Location
        {
            get
            {
                return device.GetLocation();
            }
        }

        public List<int> PinIds
        {
            get
            {
                dynamic pinIds = default(dynamic);
                int pinCount = device.GetPinIds(ref pinIds);
                List<int> ids = new List<int>(pinCount);
                for (int i = 1; i <= pinCount; i++)
                    ids.Add(pinIds[i]);
                return ids;
            }
        }

        public int PinCount
        {
            get
            {
                return device.GetPinCount();
            }
        }

        protected Device(int id, E3ObjectFabric e3ObjectFabric)
        {
            this.e3ObjectFabric = e3ObjectFabric;
            device = e3ObjectFabric.GetDevice(id);
        }

        public bool IsCable()
        {
            if (device.IsCable() == 1)
                return true;
            return false;
        }

        public bool IsWireGroup()
        {
            if (device.IsWireGroup() == 1)
                return true;
            return false;
        }

        public bool IsTerminal()
        {
            if (device.IsTerminal() == 1)
                return true;
            return false;
        }

        public bool IsMount()
        {
            if (device.IsMount() == 1)
                return true;
            return false;
        }

        public bool IsCableDuct()
        {
            if (device.IsCableDuct() == 1)
                return true;
            return false;
        }

        public bool IsHose()
        {
            if (device.IsHose() == 1)
                return true;
            return false;
        }

        public bool IsBlock()
        {
            if (device.IsBlock() == 1)
                return true;
            return false;
        }

        public string GetAttributeValue(string attributeName)
        {
            return device.GetAttributeValue(attributeName);
        }

        public string GetComponentAttributeValue(string attributeName)
        {
            if (component == null)
                component = e3ObjectFabric.GetComponent(Id);
            return component.GetAttributeValue(attributeName);
        }

    }
}
