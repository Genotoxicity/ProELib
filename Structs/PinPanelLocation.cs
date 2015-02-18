namespace ProELib
{
    public struct PinPanelLocation
    {
        private int sheetId;
        private double x;
        private double y;
        private double z;

        public int SheetId
        {
            get
            {
                return sheetId;
            }
        }

        public double X
        {
            get
            {
                return x;
            }
        }

        public double Y
        {
            get
            {
                return y;
            }
        }

        public double Z
        {
            get
            {
                return z;
            }
        }

        public PinPanelLocation(int sheetId, double x, double y, double z)
        {
            this.sheetId = sheetId;
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}
