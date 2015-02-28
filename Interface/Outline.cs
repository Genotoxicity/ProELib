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
        private e3Outline e3Outline;

        public int Id
        {
            get
            {
                return e3Outline.GetId();
            }
            set
            {
                e3Outline.SetId(value);
            }
        }

        public OutlineType Type
        {
            get
            {
                return (OutlineType)e3Outline.GetType();
            }
        }

        internal Outline(e3Outline e3Outline)
        {
            this.e3Outline = e3Outline;
        }

        public int GetPosition(out double x, out double y, out double z)
        { 
            dynamic dx = default(dynamic);
            dynamic dy = default(dynamic);
            dynamic dz = default(dynamic);
            int code = e3Outline.GetPosition(ref dx, ref dy, ref dz);
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
            int pointCount = e3Outline.GetPath(ref xarr, ref yarr);
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
