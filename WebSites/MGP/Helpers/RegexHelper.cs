using System.Text.RegularExpressions;

namespace GA.BDC.Web.MGP.Helpers
{
    public static class RegexHelper
    {
        /// <summary>
        /// Verifies if the string received is a valid email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>True if the email is valid</returns>
        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
        }
    }
}