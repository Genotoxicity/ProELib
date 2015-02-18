namespace ProELib
{
    public struct Area
    {
        private double left;
        private double right;
        private double top;
        private double bottom;
        
        public double Left
        {
            get
            {
                return left;
            }
       }

        public double Right
        {
            get
            {
                return right;
            }
        }

        public double Top
        {
            get
            {
                return top;
            }
        }

        public double Bottom
        {
            get
            {
                return bottom;
            }
        }

        public double Width
        {
            get
            {
                return System.Math.Abs(left - right);
            }
        }

        public double Height
        {
            get
            {
                return System.Math.Abs(top - bottom);
            }
        }

        public Area(double left, double right, double top, double bottom)
        {
            this.left = left;
            this.right = right;
            this.top = top;
            this.bottom = bottom;
        } 
    }
}
