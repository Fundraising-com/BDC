using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QSPFulfillment.Helper
{
    public class Utility
    {
        #region Methods - Mod-10
        public static Boolean IsValidMod10Checksum(String sP)
        {
            // The given number-string, sP, MUST include the checksum digit;
            // Expects sP to be a zero-filled string, already pre-set to
            //  the desired exact number of digits;
            // Uses the given number's actual last digit ("peeled off" checksum
            // digit) for comparison against the calculated checksum digit.
            Byte tPeeledDigit = Byte.Parse(sP.Substring(sP.Length - 1, 1));
            return IsValidMod10Checksum(sP, tPeeledDigit);
        }

        public static Boolean IsValidMod10Checksum(String sP,
        Byte tGivenCheckDigitP)
        {
            // The given number-string, sP, MUST include the checksum digit;
            // Expects sP to be a zero-filled string, already pre-set to
            //  the desired exact number of digits;
            // Uses the given parameter, tGivenCheckDigitP, for
            //  comparison against the calculated checksum digit (vs. the
            //  actual last digit).
            Byte tCalculatedCheckDigit = CalcMod10Checksum(sP, true);
            return tCalculatedCheckDigit == tGivenCheckDigitP;
        }

        public static Byte CalcMod10Checksum(String sP, Boolean bContainsChkDigitP)
        {
            // Expects a zero-filled string, already pre-set to
            //  the desired exact number of digits;
            // The last digit of the given string, sP, is or is not already
            //  the checksum digit based on given param 'bContainsChkDigitP'.

            // (Int32's are used because there is less overhead from not having
            //   to convert up from a narrower int, nor having to cast)
            Int32 iPos = sP.Length - (bContainsChkDigitP ? 2 : 1);
            String sTot = String.Empty;
            Int32 iMult = 2;
            Boolean bMult = true;
            Int32 iTot = 0;
            for (Int32 i = iPos; i >= 0; i--)
            {
                sTot += Convert.ToString(Byte.Parse(sP.Substring(i, 1)) * (bMult ? iMult : 1));
                bMult = !bMult;
            }
            for (Int32 i = 0; i < sTot.Length; i++)
                iTot += Convert.ToInt32(sTot[i] - 48); // 48 := ASCII "0"
            Int32 iMod = iTot % 10;  // or, alternatively...
            //  sTot = iTot.ToString();
            //  iMod = Convert.ToInt32(sTot[sTot.Length - 1] - 48);
            return (Byte)(iMod == 0 ? 0 : 10 - iMod);
        }

        #endregion

        #region Methods - IsNumeric
        public static Boolean IsNumeric(String sNumAsStringP)
        {
            decimal d;
            return decimal.TryParse(sNumAsStringP, out d);
        }
        #endregion
    }
}