using System.Collections.Generic;
using e3;

namespace ProELib
{
    public class Component
    {
        protected E3ObjectFabric e3ObjectFabric;
        protected e3Component component;

        public virtual int Id
        {
            get
            {
                return component.GetId();
            }
            set
            {
                component.SetId(value);
            }
        }

        internal Component(int id, E3ObjectFabric e3ObjectFabric)
        {
            this.e3ObjectFabric = e3ObjectFabric;
            component = e3ObjectFabric.GetComponent(id);
        }


        public string GetAttributeValue(string attributeName)
        {
            return component.GetAttributeValue(attributeName);
        }

    }
}
