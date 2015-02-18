using System;
using System.Collections.Generic;

namespace ProELib.Strings
{
    public class Separator
    {
        private char[] separators;
        private Func<string, E3Font, double> determineStringLength;
        private E3Font font;

        public Separator(char[] separators, E3Font font, E3Text text)
        {
            this.separators = separators;
            this.font = font;
            determineStringLength = new Func<string, E3Font, double>(text.GetTextLength);
        }

        public List<string> GetSeparatedStrings(string value, double maxLength)
        {
            if (GetLength(value) < maxLength)
                return new List<string>(1) { value };
            List<string> strings = new List<string>();
            List<string> substrings = GetSeparatedSubstrings(value);
            string separatedString = String.Empty;
            foreach (string substring in substrings)
                if (GetLength(separatedString + substring) < maxLength)
                    separatedString += substring;
                else
                {
                    strings.Add(separatedString);
                    separatedString = substring;
                }
            strings.Add(separatedString);
            strings.RemoveAll(s => (String.IsNullOrEmpty(s) || String.IsNullOrWhiteSpace(s)));
            return strings;
        }

        public List<string> GetSeparatedSubstrings(string value)
        {
            if (String.IsNullOrEmpty(value))
                return new List<string>(0);
            if (value.Length==1)
                return new List<string>(1) { value };
            int separatorPosition = value.IndexOfAny(separators);
            if (separatorPosition < 0)
                return new List<string>(1) { value };
            List<string> substrings = new List<string>();
            string beforeSeparator = value.Substring(0, separatorPosition);
            if (!String.IsNullOrEmpty(beforeSeparator))
                substrings.Add(beforeSeparator);
            string separator = value.Substring(separatorPosition, 1);
            substrings.Add(separator);
            substrings.AddRange(GetSeparatedSubstrings(value.Substring(++separatorPosition)));
            return substrings;
        }

        private double GetLength(string value)
        {
            return determineStringLength(value, font);
        }
    }
}
