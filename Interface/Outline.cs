using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using e3;

namespace ProELib
{
    public class Outline
    {
        private e3Outline outline;

        public int Id
        {
            get
            {
                return outline.GetId();
            }
            set
            {
                outline.SetId(value);
            }
        }

        public OutlineType Type
        {
            get
            {
                return (OutlineType)outline.GetType();
            }
        }

        internal Outline(int id, E3ObjectFabric e3ObjectFabric)
        {
            outline = e3ObjectFabric.GetOutline(id);
        }

        public int GetPosition(out double x, out double y, out double z)
        { 
            dynamic dx = default(dynamic);
            dynamic dy = default(dynamic);
            dynamic dz = default(dynamic);
            int code = outline.GetPosition(ref dx, ref dy, ref dz);
            x = (double) dx;
            y = (double) dy;
            z = (double) dz;
            return code;
        }

        public List<Point> GetPath()
        {
            List<Point> points;
            dynamic xarr = default(dynamic);
            dynamic yarr = default(dynamic);
            int pointCount = outline.GetPath(ref xarr, ref yarr);
            if (pointCount > 0)
            {
                points = new List<Point>(pointCount);
                for (int i = 1; i <= pointCount; i++)
                    points.Add(new Point(xarr[i], yarr[i]));
            }
            else
                points = new List<Point>(0);
            return points;
        }

    }
}
