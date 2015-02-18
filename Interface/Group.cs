using System;
using System.Collections.Generic;
using e3;

namespace ProELib
{
    public class Group
    {
        private e3Group group;

        public int Id
        {
            get
            {
                return group.GetId();
            }
            set
            {
                group.SetId(value);
            }
        }

        internal Group(int id, E3ObjectFabric e3ObjectFabric)
        {
            group = e3ObjectFabric.GetGroup(id);
        }

        public int CreateGroup(List<int> ids)
        {
            if (ids!= null && ids.Count > 0)
            {
                dynamic array = Array.CreateInstance(typeof(object), ids.Count + 1); // чтобы группа создалась, необходимо чтобы id объектов представлялись в виде Object[] с первым значением null
                array.SetValue(null, 0);
                for (int i = 0; i < ids.Count; i++)
                    array.SetValue(ids[i], i + 1);
                return group.Create(ref array);
            }
            return 0;
        }
    }
}
