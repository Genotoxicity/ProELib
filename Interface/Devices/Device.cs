using System.Collections.Generic;
using e3;

namespace ProELib
{
    public class Device
    {
        protected e3Device e3Device;

        public virtual int Id
        {
            get
            {
                return e3Device.GetId();
            }
            set
            {
                e3Device.SetId(value);
            }
        }

        public string Name
        {
            get
            {
                return e3Device.GetName();
            }
            set
            {
                e3Device.SetName(value);
            }
        }

        public string Assignment
        {
            get
            {
                return e3Device.GetAssignment();
            }
        }

        public string Location
        {
            get
            {
                return e3Device.GetLocation();
            }
        }

        public List<int> PinIds
        {
            get
            {
                dynamic pinIds = default(dynamic);
                int pinCount = e3Device.GetPinIds(ref pinIds);
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
                return e3Device.GetPinCount();
            }
        }

        protected Device(e3Device e3Device)
        {
            this.e3Device = e3Device;
        }

        public bool IsCable()
        {
            if (e3Device.IsCable() == 1)
                return true;
            return false;
        }

        public bool IsWireGroup()
        {
            if (e3Device.IsWireGroup() == 1)
                return true;
            return false;
        }

        public bool IsTerminal()
        {
            if (e3Device.IsTerminal() == 1)
                return true;
            return false;
        }

        public bool IsMount()
        {
            if (e3Device.IsMount() == 1)
                return true;
            return false;
        }

        public bool IsCableDuct()
        {
            if (e3Device.IsCableDuct() == 1)
                return true;
            return false;
        }

        public bool IsHose()
        {
            if (e3Device.IsHose() == 1)
                return true;
            return false;
        }

        public bool IsBlock()
        {
            if (e3Device.IsBlock() == 1)
                return true;
            return false;
        }

        public string GetAttributeValue(string attributeName)
        {
            return e3Device.GetAttributeValue(attributeName);
        }

    }
}
