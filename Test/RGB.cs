using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProELib
{
    public struct RGB
    {
        private byte r, g, b;

        public byte R
        {
            get
            {
                return r;
            }
        }

        public byte G
        {
            get
            {
                return g;
            }
        }

        public byte B
        {
            get
            {
                return b;
            }
        }

        public RGB(byte r, byte g, byte b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }
    }
}
