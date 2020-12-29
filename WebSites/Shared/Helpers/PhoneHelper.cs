using System;
using System.Text.RegularExpressions;

namespace GA.BDC.Shared.Helpers
{
   public static class PhoneHelper
   {
      /// <summary>
      /// Cleans the Phone number and returns it in the format provided
      /// </summary>
      /// <param name="source"></param>
      /// <param name="format"></param>
      /// <returns></returns>
      public static string Clean(string source, string format = "###-###-####")
      {
         if (string.IsNullOrEmpty(source))
         {
            return "0000000000";
         }
         var regexNumberCleaned = Regex.Replace(source, "[ ().-]+", "");
         Int64 value;
         return Int64.TryParse(regexNumberCleaned, out value) ? $"{value:###-###-####}" : "0000000000";
      }
   }
}
