using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Collections.Generic;

namespace GA.BDC.Core.Utilities.Encryption.QspEncryption
{
    /// <summary>
    /// Friend class for shared utility methods used by multiple Encryption classes
    /// </summary>
    internal class Utils
    {

        private Utils()
        {

        }

        /// <summary>
        /// converts an array of bytes to a string Hex representation
        /// </summary>
        static internal string ToHex(byte[] ba)
        {
            if (ba == null || ba.Length == 0)
            {
                return "";
            }
            const string HexFormat = "{0:X2}";
            StringBuilder sb = new StringBuilder();
            foreach (byte b in ba)
            {
                sb.Append(string.Format(System.Globalization.CultureInfo.CurrentCulture, HexFormat, b));
            }
            return sb.ToString();
        }

        /// <summary>
        /// converts from a string Hex representation to an array of bytes
        /// </summary>
        static internal byte[] FromHex(string hexEncoded)
        {
            if (hexEncoded == null || hexEncoded.Length == 0)
            {
                return null;
            }
            try
            {
                int l = Convert.ToInt32(hexEncoded.Length / 2);
                byte[] b = new byte[l - 1];
                for (int i = 0; i <= l - 1; i++)
                {
                    b[i] = Convert.ToByte(hexEncoded.Substring(i * 2, 2), 16);
                }
                return b;
            }
            catch (Exception ex)
            {
                throw new System.FormatException("The provided string does not appear to be Hex encoded:" + Environment.NewLine + hexEncoded + Environment.NewLine, ex);
            }
        }

        /// <summary>
        /// converts from a string Base64 representation to an array of bytes
        /// </summary>
        static internal byte[] FromBase64(string base64Encoded)
        {
            if (base64Encoded == null || base64Encoded.Length == 0)
            {
                return null;
            }
            try
            {
                return Convert.FromBase64String(base64Encoded);
            }
            catch (System.FormatException ex)
            {
                throw new System.FormatException("The provided string does not appear to be Base64 encoded:" + Environment.NewLine + base64Encoded + Environment.NewLine, ex);
            }
        }

        /// <summary>
        /// converts from an array of bytes to a string Base64 representation
        /// </summary>
        static internal string ToBase64(byte[] b)
        {
            if (b == null || b.Length == 0)
            {
                return "";
            }
            return Convert.ToBase64String(b);
        }

        /// <summary>
        /// retrieve an element from an XML string
        /// </summary>
        static internal string GetXmlElement(string xml, string element)
        {
            Match m;
            m = Regex.Match(xml, "<" + element + ">(?<Element>[^>]*)</" + element + ">", RegexOptions.IgnoreCase);
            if (m == null)
            {
                throw new Exception("Could not find <" + element + "></" + element + "> in provided Public Key XML.");
            }
            return m.Groups["Element"].ToString();
        }

        /// <summary>
        /// Returns the specified string value from the application .config file
        /// </summary>
        static internal string GetConfigString(string key)
        {
            return GetConfigString(key, true);
        }
        static internal string GetConfigString(string key, bool isRequired)
        {

            string s = (string)ConfigurationSettings.AppSettings["EnableQSPEncryption"];
            if (s == null)
            {
                if (isRequired)
                {
                    throw new Exception("Enabled to read from web(app) config: key <" + key + "> is missing from .config file");
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return s;
            }
        }

		private static Dictionary<string, string> urlSpecialCharacters = new Dictionary<string,string> 
				{ {"+","{{pl}}"}
				, {"/","{{s}}"}
				, {"?","{{q}}"}
				, {"%","{{pe}}"}
				, {"#","{{po}}"}
				, {"&","{{a}}"}
				, {"=","{{e}}"}
				};

		/// <summary>
		/// used to encode url to eliminate possibilities to contain special characters
		/// </summary>
		/// <param name="unEncoded"></param>
		/// <returns></returns>
		static internal string LocalEncode(string unEncoded)
		{
			string RetVal = unEncoded;

			foreach(KeyValuePair<string, string> kvp in urlSpecialCharacters)
			{
				RetVal = RetVal.Replace(kvp.Key, kvp.Value);
			}

			return RetVal;
		}

		/// <summary>
		/// used to decode url to eliminate possibilities to contain special characters
		/// </summary>
		/// <param name="unEncoded"></param>
		/// <returns></returns>
		static internal string LocalDecode(string Encoded)
		{
			string RetVal = Encoded;

			foreach (KeyValuePair<string, string> kvp in urlSpecialCharacters)
			{
				RetVal = RetVal.Replace(kvp.Value, kvp.Key);
			}

			return RetVal;
		}
        

    }
}