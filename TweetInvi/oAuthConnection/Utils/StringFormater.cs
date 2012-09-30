using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace oAuthConnection.Utils
{
    internal class StringFormater
    {
        private static string unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

        public static string UrlEncode(string str)
        {
            StringBuilder result = new StringBuilder();

            foreach (char c in str)
                if (unreservedChars.Contains(c))
                    result.Append(c);
                else
                    result.Append('%' + String.Format("{0:X2}", (int)c));

            return result.ToString();
        }
    }
}
