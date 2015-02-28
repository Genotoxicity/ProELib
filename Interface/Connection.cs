using System.Collections.Generic;
using System.Windows;
using System;
using e3;

namespace ProELib
{
    public class Connection
    {
        private e3Connection e3Connection;

        public List<int> PinIds
        {
            get
            {
                dynamic connectionPinIds = default(dynamic);
                int pinCount = e3Connection.GetPinIds(ref connectionPinIds);
                List<int> ids = new List<int>(pinCount);
                for (int i = 1; i <= pinCount; i++)
                    ids.Add(connectionPinIds[i]);
                return ids;
            }
        }

        public List<int> ReferenceSymbolIds
        { 
            get
            {
                dynamic symbolIds = default(dynamic);
                int symbolCount = e3Connection.GetReferenceSymbolIds(ref symbolIds);
                List<int> ids = new List<int>(symbolCount);
                for (int i = 1; i <= symbolCount; i++)
                    ids.Add(symbolIds[i]);
                return ids;
            }
        }

        public int Id
        {
            get
            {
                return e3Connection.GetId();
            }
            set
            {
                e3Connection.SetId(value);
            }
        }

        public string Name
        {
            get
            {
                return e3Connection.GetName();
            }
        }

        internal Connection(e3Connection e3Connection)
        {
            this.e3Connection = e3Connection;
        }

        public bool IsUnique()
        {
            return PinIds.Count <= 2;
        }

        public void Highlight()
        {
            e3Connection.Highlight();
        }

        public int Create(int sheetId, List<Point> points)
        {
            int pointCount = points.Count;
            dynamic arrayOfX = Array.CreateInstance(typeof(object), pointCount + 1); // e3 работает только с массивами, начинающимися с null
            dynamic arrayOfY = Array.CreateInstance(typeof(object), pointCount + 1); 
            arrayOfX.SetValue(null, 0);
            arrayOfY.SetValue(null, 0);
            for (int i = 0; i < pointCount; i++)
            {
                arrayOfX.SetValue(points[i].X, i + 1);
                arrayOfY.SetValue(points[i].Y, i + 1);
            }
            return e3Connection.Create(sheetId, pointCount, ref arrayOfX, ref arrayOfY);
        }

    }
}
