using System.Collections.Generic;
using e3;

namespace ProELib
{
    public class Graphic
    {
        private e3Graph e3Graph;

        public int Id
        {
            get
            {
                return e3Graph.GetId();
            }
            set
            {
                e3Graph.SetId(value);
            }
        }

        public List<int> GraphicIds
        {
            get
            {
                dynamic graphicIds = default(dynamic);
                int graphicCount = e3Graph.GetGraphIds(ref graphicIds);
                List<int> ids = new List<int>(graphicCount);
                for (int i = 1; i <= graphicCount; i++)
                    ids.Add(graphicIds[i]);
                return ids;
            }
        }

        internal Graphic(e3Graph e3Graph)
        {
            this.e3Graph = e3Graph;
        }

        public int CreateLine(int sheetId, double x1, double y1, double x2, double y2)
        {
            Id = e3Graph.CreateLine(sheetId, x1, y1, x2, y2);
            return Id;
        }

        public int CreateLine(int sheetId, double x1, double y1, double x2, double y2, int colorIndex)
        {
            Id = e3Graph.CreateLine(sheetId, x1, y1, x2, y2);
            e3Graph.SetColour(colorIndex);
            return Id;
        }

        public int CreateLine(int sheetId, double x1, double y1, double x2, double y2, double width)
        {
            Id = e3Graph.CreateLine(sheetId, x1, y1, x2, y2);
            e3Graph.SetLineWidth(width);
            return Id;
        }

        public int CreateLine(int sheetId, double x1, double y1, double x2, double y2, double width, int colorIndex)
        {
            Id = e3Graph.CreateLine(sheetId, x1, y1, x2, y2);
            e3Graph.SetLineWidth(width);
            e3Graph.SetColour(colorIndex);
            return Id;
        }

        public int CreateRectangle(int sheetId, double x1, double y1, double x2, double y2)
        {
            Id = e3Graph.CreateRectangle(sheetId, x1, y1, x2, y2);
            return Id;
        }

        public int CreateRectangle(int sheetId, double x1, double y1, double x2, double y2, double width)
        {
            Id = e3Graph.CreateRectangle(sheetId, x1, y1, x2, y2);
            e3Graph.SetLineWidth(width);
            return Id;
        }

        public int CreateCircle(int sheetId, double x, double y, double radius )
        {
            Id = e3Graph.CreateCircle(sheetId, x, y, radius);
            return Id;
        }

        public int CreateArc(int sheetId, double x, double y, double radius, double startAngle, double endAngle)
        {
            Id = e3Graph.CreateArc(sheetId, x, y, radius, startAngle, endAngle);
            return Id;
        }

        public int CreateArc(int sheetId, double x, double y, double radius, double startAngle, double endAngle, double width, int colorIndex)
        {
            Id = e3Graph.CreateArc(sheetId, x, y, radius, startAngle, endAngle);
            e3Graph.SetLineWidth(width);
            e3Graph.SetColour(colorIndex);
            return Id;
        }

        public int CreateText(int sheetId, string value, double x, double y)
        {
            Id = e3Graph.CreateText(sheetId, value, x, y);
            return Id;
        }

        public int CreateVerticalText(int sheetId, string value, double x, double y)
        {
            Id = e3Graph.CreateRotatedText(sheetId, value, x, y, 90);
            return Id;
        }

        public double SetLineWidth(double width)
        {
            return e3Graph.SetLineWidth(width);
        }

        public double SetLineStyle(int lineStyle)
        {
            return e3Graph.SetLineStyle(lineStyle);
        }

        public int SetColor(int colorIndex)
        {
            return e3Graph.SetColour(colorIndex);
        }

        public int Delete()
        {
            return e3Graph.Delete();
        }

        public int CreateFromSymbol(int sheetId, double x, double y, string symbolName, string symbolVersion)
        {
            return e3Graph.CreateFromSymbol(sheetId, x, y, null, 0, 0, symbolName, symbolVersion);
        }
    }
}
