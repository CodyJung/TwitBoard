using System;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;

namespace Tweetinvi.Utils
{
    public static class StringExtension
    {
        #region Extensions
        public static string cleanString(this string s)
        {
            return s != null ? (s.HTMLDecode().MySQLClean().ReplaceNonPrintableCharacters('\\')) : null;
        }


        public static string HTMLDecode(this string s)
        {
            return WebUtility.HtmlDecode(s);
        }
        
        public static string ReplaceNonPrintableCharacters(this string s, char replaceWith)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] > 51135)
                    result.Append(replaceWith);

                result.Append(s[i]);
            }
            return result.ToString();
        }

        public static string MySQLClean(this string s)
        {
            if (s == null)
                return null;

            StringBuilder result = new StringBuilder(s);

            return Regex.Replace(result.Replace("\\", "\\\\").ToString(), "['′ˈ]", "\\'");;
        } 
        #endregion
    }
}
