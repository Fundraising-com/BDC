using System;
using System.Text;

namespace QSP.Text
{
    /// <summary>
    /// Implementation of the Soundex algorithm used to locate similar sounding words.
    /// </summary>
    public class Soundex
    {

        #region Constructors
        private Soundex()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Return the soundex code for a given string.
        /// </summary>
        public static string ToSoundexCode(string str)
        {

            string word = str.ToUpper();
            StringBuilder soundexCode = new StringBuilder();
            int wordLength = word.Length;

            // Rule 1. Keep the first character of the word
            soundexCode.Append(word.Substring(0, 1));

            // Rule 2. Perform a transformation on each remaining characters
            for (int i = 1; i < wordLength; i++)
            {
                String transformedChar = Transform(word.Substring(i, 1));

                // Rule 3. If a character is the same as the previous, do not include in code
                if (!transformedChar.Equals(soundexCode.ToString().Substring(soundexCode.Length - 1)))
                {

                    // Rule 4. If character is "A" or "S" do not include in code
                    if (!transformedChar.Equals("A") && !transformedChar.Equals("S"))
                    {

                        // Rule 5. If a character is blank, then do not include in code 
                        if (!transformedChar.Equals(" "))
                        {
                            soundexCode.Append(transformedChar);
                        }
                    }
                }
            }

            // Rule 6. A soundex code must be exactly 4 characters long.  If the
            //         code is too short then pad with zeros, otherwise truncate.
            soundexCode.Append("0000");

            return soundexCode.ToString().Substring(0, 4);
        }

        /// <summary>
        /// Transform the A-Z alphabetic characters to the appropriate soundex code.
        /// </summary>
        private static string Transform(string str)
        {
            switch (str)
            {
                case "A":
                case "E":
                case "I":
                case "O":
                case "U":
                case "Y":
                    return "A";
                case "H":
                case "W":
                    return "S";
                case "B":
                case "F":
                case "P":
                case "V":
                    return "1";
                case "C":
                case "G":
                case "J":
                case "K":
                case "Q":
                case "S":
                case "X":
                case "Z":
                    return "2";
                case "D":
                case "T":
                    return "3";
                case "L":
                    return "4";
                case "M":
                case "N":
                    return "5";
                case "R":
                    return "6";
            }

            return " ";
        }
        #endregion
    }
}

