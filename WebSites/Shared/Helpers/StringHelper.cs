using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GA.BDC.Shared.Helpers
{
    public static class StringHelper
    {
        public static string RemoveWhiteSpaces(string str) {
				return str != null ? Regex.Replace(str, @"\s+", "") : "";
        }
        public static string RemoveSpecialCharacters(string str)
        {
				return str != null ? Regex.Replace(str, @"[^0-9a-zA-Z]+", "") : "";
        }
        public static string CanonicalString(string str) {
			return str != null ? RemoveSpecialCharacters(RemoveWhiteSpaces(str)) : "";
        }
    }
}
