
using System;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;

namespace QSP.Text
{

    /// <summary>
    /// Specify options used for the StringConvert class.
    /// </summary>
    public enum StringConvertOptions
    {
        /// <summary>
        /// No options.
        /// </summary>
        None = 0,

        /// <summary>
        /// Remove any "=" signs.
        /// </summary>
        RemoveEqualSign = 1,

        /// <summary>
        /// Remove any "=3D" signs. Specify IgnoreCase option to remove "=3d" sign also.
        /// </summary>
        RemoveEqual3DSign = 2,

        /// <summary>
        /// Trim spaces.
        /// </summary>
        TrimSpace = 4,

        /// <summary>
        /// Ignore case.
        /// </summary>
        IgnoreCase = 8
    }

    /// <summary>
    /// Convert for common string types. Refer to each method implementation for various conversion rules.
    /// </summary>
    /// <example>
    /// The following converts a number with alphabets. The resulting value is "3202".
    /// <code>
    /// int id = StringConvert.ToInt32("32a02");
    /// </code>
    /// 
    /// The following will throw an exception because the value cannot be converted.
    /// <code>
    /// int id = StringConvert.ToInt32("xyz");
    /// </code>
    /// 
    /// To avoid an exception, you can specify a default value. The resulting value is -1.
    /// <code>
    /// int id  = StringConvert.ToInt32("xyz", -1);
    /// </code>
    /// 
    /// The following removes any "=3D" signs from the string. The resulting value is "Montreal".
    /// <code>
    /// string id = StringConvert.ToString("Montr=3Deal", StringConvertOptions.RemoveEqual3DSign);
    /// </code>
    /// 
    /// The following will throw an exception because the value cannot be converted.
    /// <code>
    /// DateTime cal = StringConvert.ToDateTime("Jan 233, 30000");
    /// </code>
    /// 
    /// To avoid an exception, you can specify a default value. The resulting value is the time from DateTime.Now.
    /// <code>
    /// DateTime cal = StringConvert.ToDateTime("Jan 233, 30000", DateTime.Now);
    /// </code>
    /// 
    /// </example>
    public class StringConvert
    {

        #region Constructors
        public StringConvert()
        {

        }
        #endregion

        #region Methods
        /// <summary>
        /// Get the best match for string.
        /// </summary>
        /// <param name="s">Input to parse.</param>
        /// <param name="opt">Conversion options.</param>
        /// <returns>A cleaned up string.</returns>
        public static string ToString(string s, StringConvertOptions opt)
        {
            // Remove spaces
            if ((opt & StringConvertOptions.TrimSpace) == StringConvertOptions.TrimSpace)
                s = s.Trim();

            // Remove "=" sign
            if ((opt & StringConvertOptions.RemoveEqualSign) == StringConvertOptions.RemoveEqualSign)
                s = s.Replace("=", "");

            // Remove "=3D" sign
            if ((opt & StringConvertOptions.RemoveEqual3DSign) == StringConvertOptions.RemoveEqual3DSign)
                if ((opt & StringConvertOptions.IgnoreCase) == StringConvertOptions.IgnoreCase)
                    s = Regex.Replace(s, "=3D", "", RegexOptions.IgnoreCase);
                else
                    s = s.Replace("=3D", "");

            return s;
        }


        /// <summary>
        /// Get the best match for a Base64 string. The base 64 digits in ascending order 
        /// from zero are the uppercase characters 'A' to 'Z', lowercase characters 'a' to 'z', numerals '0' to '9', and the symbols '+' and '/'. The valueless character, '=', is used for trailing padding.
        /// </summary>
        /// <param name="s">Input to parse.</param>
        /// <returns>Base64 string.</returns>
        public static string ToBase64String(string s)
        {
            return Regex.Replace(s, @"[^A-Za-z0-9+/=]", "");
        }

        /// <summary>
        /// Get the best match for Byte. All non-digit elements are removed.
        /// </summary>
        /// <param name="s">Input to parse.</param>
        /// <returns>Byte value.</returns>
        /// <exception cref="ArgumentException">value is a null reference (Nothing in Visual Basic).</exception>
        /// <exception cref="FormatException">value does not consist of a sequence of digits (zero through nine).</exception>
        /// <exception cref="OverflowException">value represents a number less than MinValue or greater than MaxValue.</exception>
        public static byte ToByte(string s)
        {
            s = Regex.Replace(s, @"\D", "");
            return Convert.ToByte(s);
        }

        /// <summary>
        /// Get the best match for Byte. All non-digit elements are removed.
        /// </summary>
        /// <param name="s">Input to parse.</param>
        /// <param name="defaultValue">Default replacement value if conversion fails. This method will not throw exceptions.</param>
        /// <returns>Byte value.</returns>
        public static byte ToByte(string s, byte defaultValue)
        {
            try
            {
                return ToByte(s);
            }
            catch
            {
                return defaultValue;
            }
        }


        /// <summary>
        /// Get the best match for SByte. All non-digit elements are removed except for optional minus sign "-".
        /// </summary>
        /// <param name="s">Input to parse.</param>
        /// <returns>Byte value.</returns>
        /// <exception cref="ArgumentException">value is a null reference (Nothing in Visual Basic).</exception>
        /// <exception cref="FormatException">value does not consist of an optional sign followed by a sequence of digits (zero through nine).</exception>
        /// <exception cref="OverflowException">value represents a number less than MinValue or greater than MaxValue.</exception>
        public static sbyte ToSByte(string s)
        {
            // Remove any character except "-" and digits.
            s = Regex.Replace(s, @"[^\-\d]", "");

            // If string begins with "-xxx", remove any hidden "-" inside string.
            // If string does not begin with "-", remove all hyphens "-".
            if (Regex.Match(s, @"^-.*?-").Success)
                s = "-" + s.Replace("-", "");
            else if (Regex.Match(s, @".+?-").Success)
            {
                s = s.Replace("-", "");
            }

            return Convert.ToSByte(s);
        }

        /// <summary>
        /// Get the best match for Byte. All non-digit elements are removed.
        /// </summary>
        /// <param name="s">Input to parse.</param>
        /// <param name="defaultValue">Default replacement value if conversion fails. This method will not throw exceptions.</param>
        /// <returns>Byte value.</returns>
        public static sbyte ToSByte(string s, sbyte defaultValue)
        {
            try
            {
                return ToSByte(s);
            }
            catch
            {
                return defaultValue;
            }
        }


        /// <summary>
        /// Get the best match for Char (unicode character equivalent). Removes any traling characters leaving only the first character.
        /// </summary>
        /// <param name="s">Input to parse.</param>
        /// <returns>Char value.</returns>
        /// <exception cref="ArgumentException">value is a null reference (Nothing in Visual Basic).</exception>
        /// <exception cref="FormatException">The length of value is not 1.</exception>
        public static char ToChar(string s)
        {
            if (s.Length > 1)
                s = s.Substring(0, 1);
            return Convert.ToChar(s);
        }

        /// <summary>
        /// Get the best match for Char (unicode character equivalent). Removes any traling characters leaving only the first character.
        /// </summary>
        /// <param name="s">Input to parse.</param>
        /// <param name="defaultValue">Default replacement value if conversion fails. This method will not throw exceptions.</param>
        /// <returns>Char value.</returns>
        public static char ToChar(string s, char defaultValue)
        {
            try
            {
                return ToChar(s);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Get the best match for Int16. All non-digit elements are removed except an optional minus "-" sign.
        /// </summary>
        /// <param name="s">Input to parse.</param>
        /// <returns>Int16 number.</returns>
        /// <exception cref="ArgumentException">value is a null reference (Nothing in Visual Basic).</exception>
        /// <exception cref="FormatException">value does not consist of an optional sign followed by a sequence of digits (zero through nine).</exception>
        /// <exception cref="OverflowException">value represents a number less than MinValue or greater than MaxValue.</exception>
        public static int ToInt16(string s)
        {
            // Remove any character after the first period "."
            s = Regex.Replace(s, @"\..*", "");

            // Remove any character except "-" and digits.
            s = Regex.Replace(s, @"[^\-\d]", "");

            // If string begins with "-xxx", remove any hidden "-" inside string.
            // If string does not begin with "-", remove all hyphens "-".
            if (Regex.Match(s, @"^-.*?-").Success)
                s = "-" + s.Replace("-", "");
            else if (Regex.Match(s, @".+?-").Success)
            {
                s = s.Replace("-", "");
            }

            return Convert.ToInt16(s);
        }

        /// <summary>
        /// Get the best match for Int16. All non-digit elements are removed except an optional minus "-" sign.
        /// </summary>
        /// <param name="s">Input to parse.</param>
        /// <param name="defaultValue">Default replacement value if conversion fails. This method will not throw exceptions.</param>
        /// <returns>Int16 number.</returns>
        public static int ToInt16(string s, int defaultValue)
        {
            try
            {
                return ToInt16(s);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Get the best match for Int32. All non-digit elements are removed except an optional minus "-" sign.
        /// </summary>
        /// <param name="s">Input to parse.</param>
        /// <returns>Int32 number.</returns>
        /// <exception cref="ArgumentException">value is a null reference (Nothing in Visual Basic).</exception>
        /// <exception cref="FormatException">value does not consist of an optional sign followed by a sequence of digits (zero through nine).</exception>
        /// <exception cref="OverflowException">value represents a number less than MinValue or greater than MaxValue.</exception>
        public static int ToInt32(string s)
        {
            // Remove any character after the first period "."
            s = Regex.Replace(s, @"\..*", "");

            // Remove any character except "-" and digits.
            s = Regex.Replace(s, @"[^\-\d]", "");

            // If string begins with "-xxx", remove any hidden "-" inside string.
            // If string does not begin with "-", remove all hyphens "-".
            if (Regex.Match(s, @"^-.*?-").Success)
                s = "-" + s.Replace("-", "");
            else if (Regex.Match(s, @".+?-").Success)
            {
                s = s.Replace("-", "");
            }

            return Convert.ToInt32(s);
        }

        /// <summary>
        /// Get the best match for Int32. All non-digit elements are removed except an optional minus "-" sign.
        /// </summary>
        /// <param name="s">Input to parse.</param>
        /// <param name="defaultValue">Default replacement value if conversion fails. This method will not throw exceptions.</param>
        /// <returns>Int32 number.</returns>
        public static int ToInt32(string s, int defaultValue)
        {
            try
            {
                return ToInt32(s);
            }
            catch
            {
                return defaultValue;
            }
        }


        /// <summary>
        /// Get the best match for UInt32. All non-digit elements are removed.
        /// </summary>
        /// <param name="s">Input to parse.</param>
        /// <returns>UInt32 number.</returns>
        /// <exception cref="ArgumentException">value is a null reference (Nothing in Visual Basic).</exception>
        /// <exception cref="FormatException">value does not consist of a sequence of digits (zero through nine).</exception>
        /// <exception cref="OverflowException">value represents a number less than MinValue or greater than MaxValue.</exception>
        public static uint ToUInt32(string s)
        {
            // Remove any character after the first period "."
            s = Regex.Replace(s, @"\..*", "");

            // Remove any character except digits.
            s = Regex.Replace(s, @"\D", "");

            return Convert.ToUInt32(s);
        }

        /// <summary>
        /// Get the best match for UInt32. All non-digit elements are removed.
        /// </summary>
        /// <param name="s">Input to parse.</param>
        /// <param name="defaultValue">Default replacement value if conversion fails. This method will not throw exceptions.</param>
        /// <returns>UInt32 number.</returns>
        public static uint ToUInt32(string s, uint defaultValue)
        {
            try
            {
                return ToUInt32(s);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Get the best match for Int64. All non-digit elements are removed except an optional minus "-" sign.
        /// </summary>
        /// <param name="s">Input to parse.</param>
        /// <returns>Int64 number.</returns>
        /// <exception cref="ArgumentException">value is a null reference (Nothing in Visual Basic).</exception>
        /// <exception cref="FormatException">value does not consist of an optional sign followed by a sequence of digits (zero through nine).</exception>
        /// <exception cref="OverflowException">value represents a number less than MinValue or greater than MaxValue.</exception>
        public static long ToInt64(string s)
        {
            // Remove any character after the first period "."
            s = Regex.Replace(s, @"\..*", "");

            // Remove any character except "-" and digits.
            s = Regex.Replace(s, @"[^\-\d]", "");

            // If string begins with "-xxx", remove any hidden "-" inside string.
            // If string does not begin with "-", remove all hyphens "-".
            if (Regex.Match(s, @"^-.*?-").Success)
                s = "-" + s.Replace("-", "");
            else if (Regex.Match(s, @".+?-").Success)
            {
                s = s.Replace("-", "");
            }

            return Convert.ToInt64(s);
        }

        /// <summary>
        /// Get the best match for Int64. All non-digit elements are removed except an optional minus "-" sign.
        /// </summary>
        /// <param name="s">Input to parse.</param>
        /// <param name="defaultValue">Default replacement value if conversion fails. This method will not throw exceptions.</param>
        /// <returns>Int64 number.</returns>
        public static long ToInt64(string s, long defaultValue)
        {
            try
            {
                return ToInt64(s);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Get the best match for UInt64. All non-digit elements are removed.
        /// </summary>
        /// <param name="s">Input to parse.</param>
        /// <returns>UInt64 number.</returns>
        /// <exception cref="ArgumentException">value is a null reference (Nothing in Visual Basic).</exception>
        /// <exception cref="FormatException">value does not consist of a sequence of digits (zero through nine).</exception>
        /// <exception cref="OverflowException">value represents a number less than MinValue or greater than MaxValue.</exception>
        public static ulong ToUInt64(string s)
        {
            // Remove any character after the first period "."
            s = Regex.Replace(s, @"\..*", "");

            // Remove any character except digits.
            s = Regex.Replace(s, @"\D", "");

            return Convert.ToUInt64(s);
        }

        /// <summary>
        /// Get the best match for UInt64. All non-digit elements are removed.
        /// </summary>
        /// <param name="s">Input to parse.</param>
        /// <param name="defaultValue">Default replacement value if conversion fails. This method will not throw exceptions.</param>
        /// <returns>UInt64 number.</returns>
        public static ulong ToUInt64(string s, ulong defaultValue)
        {
            try
            {
                return ToUInt64(s);
            }
            catch
            {
                return defaultValue;
            }
        }


        /// <summary>
        /// Get the best match for Double. All non-digit elements are removed except an optional minus "-" sign and period ".".
        /// </summary>
        /// <param name="s">Input to parse.</param>
        /// <returns>Double number.</returns>
        /// <exception cref="ArgumentException">value is a null reference (Nothing in Visual Basic).</exception>
        /// <exception cref="FormatException">value does not consist of an optional sign followed by a sequence of digits (zero through nine) and period.</exception>
        /// <exception cref="OverflowException">value represents a number less than MinValue or greater than MaxValue.</exception>
        public static double ToDouble(string s)
        {
            // Remove any character after the second period "."
            s = Regex.Replace(s, @"(\..*?)\..*", "$1");

            // Remove any character except "-" and digits.
            s = Regex.Replace(s, @"[^\-\.\d]", "");

            // If string begins with "-xxx", remove any hidden "-" inside string.
            // If string does not begin with "-", remove all hyphens "-".
            if (Regex.Match(s, @"^-.*?-").Success)
                s = "-" + s.Replace("-", "");
            else if (Regex.Match(s, @".+?-").Success)
            {
                s = s.Replace("-", "");
            }

            return Convert.ToDouble(s);
        }

        /// <summary>
        /// Get the best match for Double. All non-digit elements are removed except an optional minus "-" sign and period ".".
        /// </summary>
        /// <param name="s">Input to parse.</param>
        /// <param name="defaultValue">Default replacement value if conversion fails. This method will not throw exceptions.</param>
        /// <returns>Double number.</returns>
        public static double ToDouble(string s, double defaultValue)
        {
            try
            {
                return ToDouble(s);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Get the best match for Single (float). All non-digit elements are removed except an optional minus "-" sign and period ".".
        /// </summary>
        /// <param name="s">Input to parse.</param>
        /// <returns>Single number.</returns>
        /// <exception cref="ArgumentException">value is a null reference (Nothing in Visual Basic).</exception>
        /// <exception cref="FormatException">value does not consist of an optional sign followed by a sequence of digits (zero through nine) and period.</exception>
        /// <exception cref="OverflowException">value represents a number less than MinValue or greater than MaxValue.</exception>
        public static float ToSingle(string s)
        {
            // Remove any character after the second period "."
            s = Regex.Replace(s, @"(\..*?)\..*", "$1");

            // Remove any character except "-" and digits.
            s = Regex.Replace(s, @"[^\-\.\d]", "");

            // If string begins with "-xxx", remove any hidden "-" inside string.
            // If string does not begin with "-", remove all hyphens "-".
            if (Regex.Match(s, @"^-.*?-").Success)
                s = "-" + s.Replace("-", "");
            else if (Regex.Match(s, @".+?-").Success)
            {
                s = s.Replace("-", "");
            }

            return Convert.ToSingle(s);
        }

        /// <summary>
        /// Get the best match for Single (float). All non-digit elements are removed except an optional minus "-" sign and period ".".
        /// </summary>
        /// <param name="s">Input to parse.</param>
        /// <param name="defaultValue">Default replacement value if conversion fails. This method will not throw exceptions.</param>
        /// <returns>Single number.</returns>
        public static float ToSingle(string s, float defaultValue)
        {
            try
            {
                return ToSingle(s);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Get the best match for Boolean.
        /// <list type="table">
        /// <listheader>
        /// <term>Value</term>
        /// <term>Description</term>
        /// </listheader>
        /// <item>
        /// <term>True</term>
        /// <term>"true", "t", "yes", "y" in lowercase and uppercase or any non-zero integer.</term>
        /// </item><item>
        /// <term>False</term>
        /// <term>"false", "f", "no", "n" in lowercase and uppercase or zero integer.</term>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="s">Input to parse.</param>
        /// <returns>Boolean value.</returns>
        /// <exception cref="ArgumentException">value is a null reference (Nothing in Visual Basic).</exception>
        /// <exception cref="FormatException">value does not consist of a valid boolean string.</exception>
        public static bool ToBoolean(string s)
        {
            s = s.Trim().ToLower();

            // Look for 0 for False or != 0 for True.
            if (Regex.Match(s, @"^[\-\d]+$").Success)
            {
                try
                {
                    int i = Convert.ToInt32(s);
                    return (i != 0);
                }
                catch { }
            }

            // Look for "t", "y" or "yes" for True and "f", "n" or "no" for false.
            if (s == "t" || s == "y" || s == "yes")
                return true;
            else if (s == "f" || s == "n" || s == "no")
                return false;

            // This looks for "true" and "false" case insensitive.
            return Convert.ToBoolean(s);
        }


        /// <summary>
        /// Get the best match for Boolean.
        /// <list type="table">
        /// <listheader>
        /// <term>Value</term>
        /// <term>Description</term>
        /// </listheader>
        /// <item>
        /// <term>True</term>
        /// <term>"true", "t", "yes", "y" in lowercase and uppercase or any non-zero integer.</term>
        /// </item><item>
        /// <term>False</term>
        /// <term>"false", "f", "no", "n" in lowercase and uppercase or zero integer.</term>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="s">Input to parse.</param>
        /// <param name="defaultValue">Default replacement value if conversion fails. This method will not throw exceptions.</param>
        /// <returns>Boolean value.</returns>
        public static bool ToBoolean(string s, bool defaultValue)
        {
            try
            {
                return ToBoolean(s);
            }
            catch
            {
                return defaultValue;
            }
        }


        /// <summary>
        /// Get the best match for DateTime.
        /// </summary>
        /// <param name="s">Input to parse.</param>
        /// <returns>DateTime value.</returns>
        /// <exception cref="ArgumentException">value is a null reference (Nothing in Visual Basic).</exception>
        /// <exception cref="FormatException">value does not consist of valid DateTime string representation.</exception>
        public static DateTime ToDateTime(string s)
        {
            return DateTime.Parse(s.Trim());
        }

        /// <summary>
        /// Get the best match for DateTime.
        /// </summary>
        /// <param name="s">Input to parse.</param>
        /// <param name="defaultValue">Default replacement value if conversion fails. This method will not throw exceptions.</param>
        /// <returns>DateTime value.</returns>
        public static DateTime ToDateTime(string s, DateTime defaultValue)
        {
            try
            {
                return ToDateTime(s);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Extract credit card hint from credit card number.
        /// </summary>
        /// <param name="ccNumber">A valid credit card number.</param>
        /// <returns>Returns the credit card hint (xxxx***xxxx) where x denotes 
        /// a number or return empty string if credit card number is not a valid.</returns>
        public static string ToCreditCardHint(string creditCardNumber)
        {
            // Extract credit card hint (xxxx***xxxx) from credit card number
            creditCardNumber = creditCardNumber.Trim();
            if (creditCardNumber != "" && creditCardNumber.Length > 8)
                return creditCardNumber.Substring(0, 4) + "***" + creditCardNumber.Substring(creditCardNumber.Length - 4, 4);
            else
                return "";
        }


        /// <summary>
        /// Repair the padding in a single base64 string.
        /// </summary>
        public static string RepairBase64String(string base64String)
        {
            if (base64String != null)
            {
                // Strip off any "=" padding
                base64String = base64String.Replace("=", "");

                // Calculate needed padding
                int pad = 4 - (base64String.Length % 4);

                if (pad == 1)
                    base64String += "=";
                else if (pad == 2)
                    base64String += "==";
            }

            return base64String;
        }

        /// <summary>
        /// Force a long text to wrap.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="maxWordLength"></param>
        /// <returns></returns>
        public static string BreakText(string text, int maxWordLength)
        {
            return StringConvert.BreakText(text, maxWordLength, "\r\n");
        }


        /// <summary>
        /// Force a long text to wrap.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string BreakText(string text, int maxWordLength, string breaker)
        {
            if (text == null)
                return text;

            StringBuilder temp = new StringBuilder();
            int count = 0;
            for (int i = 0; i < text.Length; i++)
            {
                temp.Append(text[i]);

                // Increment non-breaking count
                count++;

                if (text[i] == ' ' ||
                    text[i] == '\r' ||
                    text[i] == '\n' ||
                    text[i] == '\t')
                {
                    // Reset count
                    count = 0;
                }

                if (count > maxWordLength)
                {
                    temp.Append(breaker);

                    // Reset count
                    count = 0;
                }
            }

            return temp.ToString();
        }


        /// <summary>
        /// Convert a DateTime into RFC 822 format like "Wed, 04 Jan 2006 16:03:00 GMT".
        /// </summary>
        public static string ToRfc822Date(DateTime time)
        {
            // See this for help on format string
            // http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpguide/html/cpconcustomdatetimeformatstrings.asp 
            return time.ToUniversalTime().ToString(@"ddd, dd MMM yyyy HH:mm:ss G\MT");
        }


        /// <summary>
        /// Convert a relative URL to absolute URL string.
        /// </summary>
        /// <param name="originUri"></param>
        /// <param name="relativeUrl"></param>
        /// <returns></returns>
        public static string ToAbsoluteUrl(Uri originUri, string relativeUrl)
        {
            return StringConvert.ToAbsoluteUrl(originUri, relativeUrl, false);
        }

        /// <summary>
        /// Convert a relative URL to absolute URL string.
        /// </summary>
        /// <param name="originUri"></param>
        /// <param name="relativeUrl"></param>
        /// <param name="secure"></param>
        /// <returns></returns>
        public static string ToAbsoluteUrl(Uri originUri, string relativeUrl, bool secure)
        {
            if (originUri == null)
                return null;

            if (relativeUrl == null)
                return originUri.ToString();

            if (relativeUrl.IndexOf("http") != 0)
            {
                Uri absoluteUri = new Uri(originUri, relativeUrl);
                if (secure && originUri.Scheme.ToLower() == "http")
                    return Regex.Replace(absoluteUri.ToString(), "^http:", "https:", RegexOptions.IgnoreCase);
                else if (!secure && originUri.Scheme.ToLower() == "https")
                    return Regex.Replace(absoluteUri.ToString(), "^https:", "http:", RegexOptions.IgnoreCase);

                return absoluteUri.ToString();
            }
            else
                return relativeUrl;
        }

        /// <summary>
        /// Converts a color to a hexidecimal value.
        /// </summary>
        public static string ColorToHex(Color color)
        {
            return StringConvert.ColorToHex(color, false);
        }

        /// <summary>
        /// Converts a color to a hexidecimal value including optional #.
        /// </summary>
        public static string ColorToHex(Color color, bool includeHash)
        {
            string hex = String.Format("0x{0:X8}", color.ToArgb());
            hex = hex.Substring(hex.Length - 6, 6);
            if (includeHash)
                return "#" + hex;
            else
                return hex;
        }

        /// <summary>
        /// Converts a hexidecimal value to a System.Drawing.Color object
        /// </summary>
        /// <param name="hex">The hexidecimal color value</param>
        /// <returns>A Color object</returns>
        public static Color HexToColor(string hex)
        {
            ColorConverter conv = new ColorConverter();
            Color color = (Color)conv.ConvertFromString(hex.Replace("#", ""));
            return color;
        }
        #endregion

    }
}
