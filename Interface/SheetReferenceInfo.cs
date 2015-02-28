namespace ProELib
{
    public class SheetReferenceInfo
    {
        public ReferenceDirection InOut { get; private set; }

        public ReferenceType Type { get; private set; }

        public string Signal { get; private set; }

        public string Reference { get; private set; }

        public SheetReferenceInfo(int inOut, int type, string signal, string reference)
        {
            InOut = (ReferenceDirection)(inOut - 1);
            Type = (ReferenceType)(type - 1);
            Signal = signal;
            Reference = reference;
        }
    }
}
