using System;
using System.Web;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Globalization;
using GA.BDC.Web.MGP.Properties;

namespace GA.BDC.Web.MGP.Helpers.Extensions
{
    public static class ApplicationExtension
    {
        #region Public Extension Methods

        public static bool IsDefinedController(this string value)
        {
            var controllerCollection = Settings.Default.MvcControllerNamesCollection.Split(new[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries);
            return controllerCollection.Contains(value);
        }

        public static bool IsAngularJSDirective(this string value)
        {
            return Regex.IsMatch(value, @"(\{\{.*?\}\})");
        }

        public static bool IsEmpty(this string text)
        {
            return (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text));
        }

        public static bool IsNotEmpty(this string text)
        {
            return !text.IsEmpty();
        }

        public static string TrimStart(this string target, string trimChars)
        {
            return target.TrimStart(trimChars.ToCharArray());
        }

        public static string ReplaceLegacyImagePath(this string input)
        {
            string resourcePattern = ".*" + Constants.LEGACY_IMAGE_PATH_STARTSWITH;
            string sponsorPattern = Constants.LEGACY_IMAGE_PATH_SPONSOR;
            string efundClassicPattern = Constants.LEGACY_IMAGE_PATH_EFUNDCLASSIC;
            string personalizedBigPhotosPattern = Constants.LEGACY_IMAGE_PATH_PERSONALIZED_BIG_PHOTOS;
            string partnerPersonalizationPattern = Constants.LEGACY_IMAGE_PARTNER_PERSONALIZATION;
            //string personalizedFoldersPattern = @"\d{2}(" + Constants.LEGACY_IMAGE_PATH_PERSONALIZED_FOLDERS + ")";            

            Regex rgx = new Regex(resourcePattern, RegexOptions.IgnoreCase);
            string result = rgx.Replace(input, "");
            rgx = new Regex(sponsorPattern, RegexOptions.IgnoreCase);
            result = rgx.Replace(result, "");
            rgx = new Regex(efundClassicPattern, RegexOptions.IgnoreCase);
            result = rgx.Replace(result, "");
            rgx = new Regex(personalizedBigPhotosPattern, RegexOptions.IgnoreCase);
            result = rgx.Replace(result, Settings.Default.PersonalizationImageGalleryDirectory);
            rgx = new Regex(partnerPersonalizationPattern, RegexOptions.IgnoreCase);
            result = rgx.Replace(result, "partner");
            //rgx = new Regex(personalizedFoldersPattern, RegexOptions.IgnoreCase);
            //var match = rgx.Match(result);
            //if (match.Success)
            //    result = rgx.Replace(result, match.Value.TrimStart("012345"));

            return result;
        }

        public static string PrefixImagePath(this string path)
        {
            return path.IsEmpty()
                       ? string.Empty
                       : !path.Contains(Settings.Default.PersonalizationImageDirectory) &&
                         !path.Contains(Settings.Default.ImagesVirtualDirectory)
                            ? string.Concat(Settings.Default.PersonalizationImageDirectory, "/", path.TrimStart("/"))
                            : string.Concat("/", path.TrimStart("/"));
        }

        public static string TransformToMGPImagePath(this string path)
        {
            return path.IsEmpty()
                       ? string.Empty
                       : path.ReplaceLegacyImagePath().PrefixImagePath();
        }

        public static string ReturnAsDollarAmount(this decimal decimalValue)
        {
            return decimalValue.ToString("$#,##0.00");
        }

        public static string ReturnAsDollarAmountNoDollarSign(this decimal decimalValue)
        {
            return decimalValue.ToString("#,##0.00");
        }

        public static string ReturnAsDollarAmount(this double doubleValue)
        {
            return doubleValue.ToString("$#,##0.00");
        }

        public static string ReturnAsDollarAmountNoDollarSign(this double doubleValue)
        {
            return doubleValue.ToString("#,##0.00");
        }

        public static string ReturnAsDollarAmount(this int intValue)
        {
            return intValue.ToString("$#,##0.00");
        }

        public static string ReturnAsDollarAmountNoDollarSign(this int intValue)
        {
            return intValue.ToString("#,##0.00");
        }

        public static string ReturnAsAmountWithComma(this int intValue)
        {
            return intValue.ToString("#,##0");
        }

        public static string DeSpace(this string toClean)
        {
            return toClean.Replace(" ", "");
        }

        public static string FirstLetterUpper(this string toFormat)
        {
            if (toFormat.IsEmpty())
            {
                return toFormat;
            }

            var words = toFormat.ToLower().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var processedList = words.Select(word => word.Length == 1 ? word.ToUpper() : string.Format("{0}{1}", word[0].ToString(CultureInfo.InvariantCulture).ToUpper(), word.Substring(1))).ToList();

            return string.Join(" ", processedList);
        }

        public static string FirstLetterLower(this string toFormat)
        {
            if (toFormat.IsEmpty())
            {
                return toFormat;
            }

            var words = toFormat.ToLower().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var processedList = words.Select(word => word.Length == 1 ? word.ToLower() : string.Format("{0}{1}", word[0].ToString(CultureInfo.InvariantCulture).ToLower(), word.Substring(1))).ToList();

            return string.Join(" ", processedList);
        }

        public static string HtmlEncodeDecode(this string toProcess)
        {
            return toProcess.IsEmpty()
                       ? toProcess
                       : HttpUtility.HtmlEncode(HttpUtility.HtmlDecode(toProcess));
        }
        
        public static string UrlEncode(this string toProcess)
        {
            return toProcess.IsEmpty()
                       ? toProcess
                       : HttpUtility.UrlEncode(toProcess);
        }

        public static string UrlDecode(this string toProcess)
        {
            return toProcess.IsEmpty()
                       ? toProcess
                       : HttpUtility.UrlDecode(toProcess);
        }

        public static string CleanupRedirect(this string redirect)
        {
            if (redirect.IsEmpty())
                return redirect;
            return Regex.Replace(redirect.Replace(" ",""), @"[^0-9a-zA-Z]+", "");
        }

        public static string CleanupContactEntry(this string entry)
        {
            if (entry.IsEmpty())
                return entry;
            return entry.Replace(",", "").ReplaceSingleQuoteToAlternativeVersion();
        }

        public static string StripHtml(this string text)
        {
            return Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
        }

        public static string StripBR(this string text)
        {
            return text.Replace("<br/>", string.Empty).Replace("<br />", string.Empty).Replace("<br>", string.Empty);
        }

        public static string StripNewLine(this string text)
        {
            return text.Replace("\r\n", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty);
        }

        public static string EncodeDoubleQuotes(this string text)
        {
            return text.Replace("\"", "&quote;");
        }

        public static string ReplaceSingleQuoteToAlternativeVersion(this string text)
        {
            if (text.IsEmpty())
                return text;
            return text.Replace("'", "’");
        }

        public static string TransformQuotesForUI(this string text)
        {
            if (text.IsEmpty())
                return text;
            return text.ReplaceSingleQuoteToAlternativeVersion();
        }

        public static string ReplaceNewLineToBR(this string text)
        {
            if (text.IsEmpty())
                return text;
            return text.Replace("\r\n", "<br />").Replace("\n", string.Empty).Replace("\r", string.Empty);
        }

        public static string FormatGUID(this string guid)
        {
            return guid.Replace("{", "").Replace("}", "").Replace("-", "").ToLower();
        }

        #endregion
    }
}