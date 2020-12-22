using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MyExtensionMethods
{
    public static class StringExtensions
    {

       public static string[] Split(this string input, string separator)
       {
         return System.Text.RegularExpressions.Regex.Split(input, separator);
       }

        public static string ToMoney(this string amount, bool showDollarSign)
        {
            decimal d = Convert.ToDecimal(amount);
            string amt = "0";

            if (showDollarSign)
            {
                amt = d.ToString("C");
            }
            else
            {
                amt = d.ToString("C").Remove(0, 1);
            }
             
            return amt;

        }

        public static bool IsNumeric(this string number)
        {
      
            UInt32 result;

            if (UInt32.TryParse(number, out result))
            {
                return true;
            }
            else
            {
                return false;
            }

        }



    }
}
