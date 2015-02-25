using System;
using System.Windows;
using e3;

namespace ProELib
{
    public class E3Text
    {
        private e3Text text;
        private e3Graph graph;

        public int Id
        {
            get
            {
                return text.GetId();
            }
            set
            {
                text.SetId(value);
            }
        }

        internal E3Text(e3Job job)
        {
            text = job.CreateTextObject();
            graph = job.CreateGraphObject();
        }

        public int CreateText(int sheetId, string value, double x, double y)
        {
            Id = graph.CreateText(sheetId, value, x, y);
            return Id;
        }

        public int CreateText(int sheetId, string value, double x, double y, E3Font font)
        {
            Id = graph.CreateText(sheetId, value, x, y);
            SetFont(font);
            return Id;
        }

        public int CreateVerticalText(int sheetId, string value, double x, double y)
        {
            Id = graph.CreateRotatedText(sheetId, value, x, y, 90);
            return Id;
        }

        public int CreateVerticalText(int sheetId, string value, double x, double y, E3Font font)
        {
            Id = graph.CreateRotatedText(sheetId, value, x, y, 90);
            SetFont(font);
            return Id;
        }

        public double GetTextLength(string value, E3Font font)
        {
            if (String.IsNullOrEmpty(value))
                return 0;
            dynamic xArray = default(dynamic);
            dynamic yArray = default(dynamic);
            text.CalculateBoxAt(0, value, 0, 0, 0, font.height, (int)font.mode, (int)font.style, font.name, 0, 0, ref xArray, ref yArray); // в качестве начальных координат для простоты устанавливаем 0, 0
            return (double)xArray[2];    // координата X второго угла textBox
        }

        public Size GetTextBoxSize(string value, E3Font font, double rotation)
        {
            if (String.IsNullOrEmpty(value))
                return new Size(0, 0);
            dynamic xArray = default(dynamic);
            dynamic yArray = default(dynamic);
            text.CalculateBoxAt(0, value, 0, 0, rotation, font.height, (int)font.mode, (int)font.style, font.name, 0, 0, ref xArray, ref yArray); // в качестве начальных координат для простоты устанавливаем 0, 0
            double xMax = double.MinValue;
            double yMax = double.MinValue;
            double xMin = double.MaxValue;
            double yMin = double.MaxValue;
            for (int i = 1; i < 5; i++)
            {
                double x = xArray[i];
                double y = yArray[i];
                xMax = Math.Max(xMax, x);
                yMax = Math.Max(yMax, y);
                xMin = Math.Min(xMin, x);
                yMin = Math.Min(yMin, y);
            }
            return new Size(xMax-xMin, yMax-yMin);
        }

        public double GetTextAbsciss(double textCenterX, double textWidth, E3Font font, Sheet sheet)
        {
            double textOffset = font.height - textWidth / 2;
            return sheet.MoveRight(textCenterX, textOffset);
        }

        public double GetTextOrdinate(double textCenterY, double textHeight, E3Font font, Sheet sheet)
        {
            double textOffset = font.height - textHeight / 2;
            return sheet.MoveDown(textCenterY, textOffset);
        }

        public E3Font GetFont()
        {
            string fontName = text.GetFontName();
            double height = text.GetHeight();
            Alignment alignment = (Alignment)text.GetAlignment();
            Mode mode = (Mode)text.GetMode();
            Styles style = (Styles)text.GetStyle();
            int color = text.GetColour();
            return new E3Font(fontName, height, alignment, mode, style, color);
        }

        public void SetFont(E3Font font)
        {
            if (font != null)
            {
                text.SetFontName(font.name);
                text.SetHeight(font.height);
                text.SetAlignment((int)font.alignment);
                text.SetMode((int)font.mode);
                text.SetStyle((int)font.style);
                text.SetColour(font.ColorIndex);
            }
        }

        public Size GetBox()
        {
            dynamic width = default(dynamic);
            dynamic height = default(dynamic);
            text.GetBox(ref width, ref height);
            if (width != null && height != null)
                return new Size((double)width, (double)height);
            else
                return new Size();
        }

        public int SetBox(double width, double height)
        {
            return text.SetBox(width, height);
        }

        public int SetBox(Size size)
        {
            return text.SetBox(size.Width, size.Height);
        }

        public Point GetLocation()
        {
            dynamic xCoordinate = default(dynamic), yCoordinate = default(dynamic), grid = default(dynamic);
            text.GetSchemaLocation(ref xCoordinate, ref yCoordinate, ref grid);
            if (xCoordinate != null && yCoordinate != null)
                return new Point(xCoordinate, yCoordinate);
            else
                return new Point();
        }

        public int SetLocation(double x, double y)
        {
            return text.SetSchemaLocation(x,y);
        }

        public int SetLocation(Point position)
        {
            return text.SetSchemaLocation(position.X, position.Y);
        }

        public string GetText()
        {
            return text.GetText();
        }

        public int SetText(string value)
        {
            return text.SetText(value);
        }
    }
}
