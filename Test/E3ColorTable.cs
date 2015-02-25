using System.Collections.Generic;
using System.Windows.Media;
using e3;

namespace ProELib
{
    static class E3ColorTable
    {
        public static Dictionary<int, Color> GetColorByCode(E3Project project)
        {
            int maxColorIndex = 255;
            Dictionary<int, Color> colorByCode = new Dictionary<int, Color>();
            colorByCode.Add(-1, Colors.Black);
            for (int i = 0; i <= maxColorIndex; i++)
            {
                RGB rgb = project.GetRGB(i);
                colorByCode.Add(i, Color.FromArgb(0xFF, rgb.R, rgb.G, rgb.B));
            }
            return colorByCode;
        }
    }
}
