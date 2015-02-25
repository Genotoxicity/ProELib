using System.Collections.Generic;
using e3;

namespace ProELib
{
    public class Component
    {
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

        public string Name
        {
            get
            {
                return component.GetName();
            }
        }

        internal Component(e3Job job)
        {
            component = job.CreateComponentObject();
        }

        public string GetAttributeValue(string attributeName)
        {
            return component.GetAttributeValue(attributeName);
        }

    }
}
