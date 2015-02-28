using System;
using System.Collections.Generic;
using e3;

namespace ProELib
{
    public class Group
    {
        private e3Group e3Group;

        public int Id
        {
            get
            {
                return e3Group.GetId();
            }
            set
            {
                e3Group.SetId(value);
            }
        }

        internal Group(e3Group e3Group)
        {
            this.e3Group = e3Group;
        }

        public int CreateGroup(List<int> ids)
        {
            if (ids!= null && ids.Count > 0)
            {
                dynamic array = Array.CreateInstance(typeof(object), ids.Count + 1); // чтобы группа создалась, необходимо чтобы id объектов представлялись в виде Object[] с первым значением null
                array.SetValue(null, 0);
                for (int i = 0; i < ids.Count; i++)
                    array.SetValue(ids[i], i + 1);
                return e3Group.Create(ref array);
            }
            return 0;
        }
    }
}
