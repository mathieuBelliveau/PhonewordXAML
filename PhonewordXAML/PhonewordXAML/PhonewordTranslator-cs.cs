using System;
using System.Collections.Generic;
using System.Text;

namespace Phoneword
{
    public static class PhonewordTranslator
    {
        //Method to translate number
        public static string ToNumber(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return null;

            raw = raw.ToUpperInvariant();

            var newNumber = new StringBuilder();

            foreach (var c in raw)
            {
                //Already a num? Send it through
                if (" -0123456789".Contains(c))
                    newNumber.Append(c);

                //Char to number
                else
                {
                    var result = TranslateToNumber(c);
                    if (result != null)
                        newNumber.Append(result);

                    else
                        return null;
                }
            }
            return newNumber.ToString();
        }

        //How does this inherit from parent "Contains"?
        static bool Contains(this string keyString, char c)
        {
            return keyString.IndexOf(c) >= 0;
        }

        static readonly string[] digits =
        {
            "ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ"
        };

        //goes through digits[] cell by cell to confirm which index it belongs to.
        static int? TranslateToNumber(char c)
        {
            for (int i = 0; i<digits.Length; i++)
            {
                if (digits[i].Contains(c))
                    return 2 + i;
            }
            return null;
        }
    }
}
