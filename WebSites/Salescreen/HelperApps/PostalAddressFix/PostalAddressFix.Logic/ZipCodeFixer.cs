using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostalAddressFix.Logic
{
    public class ZipCodeFixer
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zip"></param>
        /// <returns></returns>
        public static string FixUSAZip(string zip)
        {
            string result = string.Empty;

            // USA zip code: #####-####

            // Remove spaces
            zip = zip.Trim();
            zip = zip.ToUpper(); 
            zip = zip.Replace(" ", "");

            // Fix common typos
            zip = zip.Replace('O', '0');
            zip = zip.Replace('L', '1');
            zip = zip.Replace('I', '1');

            if (zip.Length < 5)
            {
                // Zip code should be at least 5 characters
                result = "";
            }
            else
            {
                #region Try to get zip5

                string zip5 = string.Empty;

                // Check if first 5 characters are digits
                if (char.IsDigit(zip[0]) && char.IsDigit(zip[1]) && char.IsDigit(zip[2]) && char.IsDigit(zip[3]) && char.IsDigit(zip[4]))
                {
                    // We have a good 5 digit zip
                    zip5 = zip.Substring(0, 5);
                }

                #endregion

                if (zip.Length == 5)
                {
                    if (zip5 != string.Empty)
                    {
                        result = zip5;
                    }
                }
                else if (zip.Length > 5)
                {
                    if (zip5 != string.Empty)
                    {
                        #region Try to get zip4

                        string zip4 = string.Empty;

                        string temp = zip.Substring(5, zip.Length - 5);

                        // Remove dash if it exists
                        temp = temp.Replace("-", "");

                        // Take 4 characters
                        if (temp.Length >= 4)
                        {
                            temp = temp.Substring(0, 4);

                            // Check if they are digits
                            if (char.IsDigit(temp[0]) && char.IsDigit(temp[1]) && char.IsDigit(temp[2]) && char.IsDigit(temp[3]))
                            {
                                // We have a good 4 digit zip4
                                zip4 = temp;
                            }
                        }

                        #endregion

                        if (zip4 != string.Empty)
                        {
                            result = string.Format("{0}-{1}", zip5, zip4);
                        }
                        else
                        {
                            result = zip5;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zip"></param>
        /// <returns></returns>
        public static string FixCanadaZip(string zip)
        {
            string result = string.Empty;

            // Canada zip code: A#A #A#

            // Remove spaces
            zip = zip.Trim();
            zip = zip.ToUpper();
            zip = zip.Replace(" ", "");
            zip = zip.Replace("-", "");

            if (zip.Length < 6)
            {
                // Zip code should be at least 6 characters
                result = "";
            }
            else
            {
                // Take 6 characters 
                zip = zip.Substring(0, 6);

                #region Fix common typos

                string temp = string.Empty;

                for (int i = 0; i < zip.Length; i++)
                {
                    if (i == 0 || i == 2 || i == 4)
                    {
                        if (zip[i] == '0')
                        {
                            temp += 'O';
                        }
                        else if (zip[i] == '1')
                        {
                            temp += 'I';
                        }
                        else
                        {
                            temp += zip[i];
                        }
                    }
                    else 
                    {
                        if (zip[i] == 'O')
                        {
                            temp += '0';
                        }
                        else if (zip[i] == 'L')
                        {
                            temp += '1';
                        }
                        else if (zip[i] == 'I')
                        {
                            temp += '1';
                        }
                        else
                        {
                            temp += zip[i];
                        }
                    }
                }

                zip = temp;

                #endregion

                // See if it is a valid Canadian zip code
                if (char.IsLetter(zip[0]) &&
                    char.IsDigit(zip[1]) &&
                    char.IsLetter(zip[2]) &&
                    char.IsDigit(zip[3]) &&
                    char.IsLetter(zip[4]) &&
                    char.IsDigit(zip[5]))
                {
                    // We have a good Canadian zip code
                    // Add a space in the middle

                    result = string.Format("{0}{1}{2} {3}{4}{5}", zip[0], zip[1], zip[2], zip[3], zip[4], zip[5]);
                }
            }

            return result;
        }

    }
}
