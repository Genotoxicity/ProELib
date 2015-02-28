using System;
using System.Windows;
using e3;

namespace ProELib
{
    public class E3Text
    {
        private e3Text e3Text;

        public int Id
        {
            get
            {
                return e3Text.GetId();
            }
            set
            {
                e3Text.SetId(value);
            }
        }

        internal E3Text(e3Text e3Text)
        {
            this.e3Text = e3Text;
        }

        public double GetTextLength(string value, E3Font font)
        {
            if (String.IsNullOrEmpty(value))
                return 0;
            dynamic xArray = default(dynamic);
            dynamic yArray = default(dynamic);
            e3Text.CalculateBoxAt(0, value, 0, 0, 0, font.height, (int)font.mode, (int)font.style, font.name, 0, 0, ref xArray, ref yArray); // в качестве начальных координат для простоты устанавливаем 0, 0
            return (double)xArray[2];    // координата X второго угла textBox
        }

        public Size GetTextBoxSize(string value, E3Font font, double rotation)
        {
            if (String.IsNullOrEmpty(value))
                return new Size(0, 0);
            dynamic xArray = default(dynamic);
            dynamic yArray = default(dynamic);
            e3Text.CalculateBoxAt(0, value, 0, 0, rotation, font.height, (int)font.mode, (int)font.style, font.name, 0, 0, ref xArray, ref yArray); // в качестве начальных координат для простоты устанавливаем 0, 0
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
            string fontName = e3Text.GetFontName();
            double height = e3Text.GetHeight();
            Alignment alignment = (Alignment)e3Text.GetAlignment();
            Mode mode = (Mode)e3Text.GetMode();
            Styles style = (Styles)e3Text.GetStyle();
            int color = e3Text.GetColour();
            return new E3Font(fontName, height, alignment, mode, style, color);
        }

        public void SetFont(E3Font font)
        {
            if (font != null)
            {
                e3Text.SetFontName(font.name);
                e3Text.SetHeight(font.height);
                e3Text.SetAlignment((int)font.alignment);
                e3Text.SetMode((int)font.mode);
                e3Text.SetStyle((int)font.style);
                e3Text.SetColour(font.ColorIndex);
            }
        }

        public Size GetBox()
        {
            dynamic width = default(dynamic);
            dynamic height = default(dynamic);
            e3Text.GetBox(ref width, ref height);
            if (width != null && height != null)
                return new Size((double)width, (double)height);
            else
                return new Size();
        }

        public int SetBox(double width, double height)
        {
            return e3Text.SetBox(width, height);
        }

        public int SetBox(Size size)
        {
            return e3Text.SetBox(size.Width, size.Height);
        }

        public Point GetLocation()
        {
            dynamic xCoordinate = default(dynamic), yCoordinate = default(dynamic), grid = default(dynamic);
            e3Text.GetSchemaLocation(ref xCoordinate, ref yCoordinate, ref grid);
            if (xCoordinate != null && yCoordinate != null)
                return new Point(xCoordinate, yCoordinate);
            else
                return new Point();
        }

        public int SetLocation(double x, double y)
        {
            return e3Text.SetSchemaLocation(x,y);
        }

        public int SetLocation(Point position)
        {
            return e3Text.SetSchemaLocation(position.X, position.Y);
        }

        public string GetText()
        {
            return e3Text.GetText();
        }

        public int SetText(string value)
        {
            return e3Text.SetText(value);
        }
    }
}
