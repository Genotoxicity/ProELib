using System;

namespace ProELib
{
    [Serializable]
    public class E3Font
    {
        private int colorIndex;

        public string name;
        public double height;
        public Alignment alignment;
        public Mode mode;
        public Styles style;

        public int ColorIndex
        {
            get
            {
                return colorIndex;
            }
            set
            {
                if (value < -1 || value > 255)  // таблица цветов в E3 содержит значения от -1 до 255, где -1 значит "Авто"
                    value = -1;
                colorIndex = -1;
            }
        }

        public E3Font(string name = "GOST type A", double height=3, Alignment alignment = Alignment.Centered, Mode mode = Mode.Normal, Styles style = Styles.Italic, int colorIndex= -1 )
        {
            this.name = name;
            this.height = height;
            this.alignment = alignment;
            this.mode = mode;
            this.style = style;
            ColorIndex = colorIndex;
        }

    }
}
