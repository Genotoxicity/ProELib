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

        public bool IsCable
        {
            get
            {
                return e3Device.IsCable() == 1;
            }
        }

        public bool IsWireGroup
        {
            get
            {
                return e3Device.IsWireGroup() == 1;
            }
        }

        public bool IsTerminal
        {
            get
            {
                return e3Device.IsTerminal() == 1;
            }
        }

        public bool IsMount
        {
            get
            {
                return e3Device.IsMount() == 1;
            }
        }

        public bool IsCableDuct
        {
            get
            {
                return e3Device.IsCableDuct() == 1;
            }
        }

        public bool IsHose
        {
            get
            {
                return e3Device.IsHose() == 1;
            }
        }

        public bool IsBlock
        {
            get
            {
                return e3Device.IsBlock() == 1;
            }
        }

        public bool IsView
        {
            get
            {
                return e3Device.IsView() == 1;
            }
        }

        public bool IsConnector
        {
            get
            {
                return e3Device.IsConnector()==1;
            }
        }

        protected Device(e3Device e3Device)
        {
            this.e3Device = e3Device;
        }

        public string GetAttributeValue(string attributeName)
        {
            return e3Device.GetAttributeValue(attributeName);
        }

    }
}
