using System.Collections.Generic;
using e3;

namespace ProELib
{
    public class Graphic
    {
        private e3Graph graph;

        public int Id
        {
            get
            {
                return graph.GetId();
            }
            set
            {
                graph.SetId(value);
            }
        }

        public List<int> GraphicIds
        {
            get
            {
                dynamic graphicIds = default(dynamic);
                int graphicCount = graph.GetGraphIds(ref graphicIds);
                List<int> ids = new List<int>(graphicCount);
                for (int i = 1; i <= graphicCount; i++)
                    ids.Add(graphicIds[i]);
                return ids;
            }
        }

        internal Graphic(e3Job job)
        {
            graph = job.CreateGraphObject();
        }

        public int CreateLine(int sheetId, double x1, double y1, double x2, double y2)
        {
            Id = graph.CreateLine(sheetId, x1, y1, x2, y2);
            return Id;
        }

        public int CreateLine(int sheetId, double x1, double y1, double x2, double y2, int colorIndex)
        {
            Id = graph.CreateLine(sheetId, x1, y1, x2, y2);
            graph.SetColour(colorIndex);
            return Id;
        }

        public int CreateLine(int sheetId, double x1, double y1, double x2, double y2, double width)
        {
            Id = graph.CreateLine(sheetId, x1, y1, x2, y2);
            graph.SetLineWidth(width);
            return Id;
        }

        public int CreateLine(int sheetId, double x1, double y1, double x2, double y2, double width, int colorIndex)
        {
            Id = graph.CreateLine(sheetId, x1, y1, x2, y2);
            graph.SetLineWidth(width);
            graph.SetColour(colorIndex);
            return Id;
        }

        public int CreateRectangle(int sheetId, double x1, double y1, double x2, double y2)
        {
            Id = graph.CreateRectangle(sheetId, x1, y1, x2, y2);
            return Id;
        }

        public int CreateRectangle(int sheetId, double x1, double y1, double x2, double y2, double width)
        {
            Id = graph.CreateRectangle(sheetId, x1, y1, x2, y2);
            graph.SetLineWidth(width);
            return Id;
        }

        public int CreateCircle(int sheetId, double x, double y, double radius )
        {
            Id = graph.CreateCircle(sheetId, x, y, radius);
            return Id;
        }

        public int CreateArc(int sheetId, double x, double y, double radius, double startAngle, double endAngle)
        {
            Id = graph.CreateArc(sheetId, x, y, radius, startAngle, endAngle);
            return Id;
        }

        public int CreateArc(int sheetId, double x, double y, double radius, double startAngle, double endAngle, double width, int colorIndex)
        {
            Id = graph.CreateArc(sheetId, x, y, radius, startAngle, endAngle);
            graph.SetLineWidth(width);
            graph.SetColour(colorIndex);
            return Id;
        }

        public double SetLineWidth(double width)
        {
            return graph.SetLineWidth(width);
        }

        public double SetLineStyle(int lineStyle)
        {
            return graph.SetLineStyle(lineStyle);
        }

        public int SetColor(int colorIndex)
        {
            return graph.SetColour(colorIndex);
        }

        public int Delete()
        {
            return graph.Delete();
        }

        public int CreateFromSymbol(int sheetId, double x, double y, string symbolName, string symbolVersion)
        {
            return graph.CreateFromSymbol(sheetId, x, y, null, 0, 0, symbolName, symbolVersion);
        }
    }
}
