using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ProELib.Strings
{
    public class NaturalSortingComparer : IComparer<string>
    {
        public int Compare(string a, string b)
        {
            bool aNullOrEmpty = String.IsNullOrEmpty(a);
            bool bNullOrEmpty = String.IsNullOrEmpty(b);
            if (aNullOrEmpty && bNullOrEmpty)
                return 0;
            if (aNullOrEmpty && !bNullOrEmpty)
                return -1;
            if (!aNullOrEmpty && bNullOrEmpty)
                return 1;
            int aLength = a.Length;
            int bLength = b.Length;
            int minLength = Math.Min(aLength, bLength);
            string numberPattern = @"^\d+";
            for (int i = 0; i < minLength; i++)
            {
                char aChar = a[i];
                char bChar = b[i];
                if (Char.IsDigit(aChar) && Char.IsDigit(bChar))
                {
                    string aNum = Regex.Match(a.Substring(i), numberPattern).ToString();
                    string bNum = Regex.Match(b.Substring(i), numberPattern).ToString();
                    int aNumLength = aNum.Length;
                    int bNumLength = bNum.Length;
                    int aValue, bValue;
                    int.TryParse(aNum, out aValue);
                    int.TryParse(bNum, out bValue);
                    if (aValue == bValue)
                    {
                        if (aNumLength == bNumLength)
                        {
                            i += aNumLength;
                            continue;
                        }
                        else
                            return -(aNumLength - bNumLength);
                    }
                    else
                        return aValue - bValue;
                }
                else
                {
                    int compareResult = aChar.CompareTo(bChar);
                    if (compareResult != 0)
                        return compareResult;
                }
            }
            return aLength - bLength;
        }
    }
}
