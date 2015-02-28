using System.Collections.Generic;
using e3;

namespace ProELib
{
    public class Component
    {
        protected e3Component e3Component;

        public virtual int Id
        {
            get
            {
                return e3Component.GetId();
            }
            set
            {
                e3Component.SetId(value);
            }
        }

        public string Name
        {
            get
            {
                return e3Component.GetName();
            }
        }

        internal Component(e3Component e3Component)
        {
            this.e3Component = e3Component;
        }

        public string GetAttributeValue(string attributeName)
        {
            return e3Component.GetAttributeValue(attributeName);
        }

    }
}
