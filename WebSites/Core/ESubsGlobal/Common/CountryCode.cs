using System;
using System.Text.RegularExpressions;

namespace GA.BDC.Core.ESubsGlobal.Common {
	/// <summary>
	/// Summary description for CountryCode.
	/// </summary>
    [Serializable]
	public class CountryCode {
		private string code = "US";

		internal CountryCode(){}

		private CountryCode(string countryCode) {
			code = countryCode;
		}

		public static explicit operator string(CountryCode cc) {
			return cc.code;
		}

		public static CountryCode Create(string countryCode) {
			if(countryCode == null) {
				return CountryCode.US;
			}
        
			if(countryCode.ToLower() == "us") {
				return CountryCode.US;
			} else if(countryCode.ToLower() == "ca") {
				return CountryCode.CA;
            } else if (countryCode.ToLower() == "usa"){
                return CountryCode.US;
            } else if(countryCode.ToLower() == "canada") {
				return CountryCode.CA;
			} else {
				return null;
			}
		}

		/// <summary>
		/// Create CountryCode by country name.
		/// </summary>
		/// <param name="countryName"></param>
		/// <returns></returns>
		public static CountryCode CreateByName(string countryName)
		{
			countryName = countryName.ToLower().Trim();

			if (countryName.IndexOf("united states") >= 0 ||
				Regex.Match(countryName, @"u\.*s\.*a*", RegexOptions.IgnoreCase).Success)
			{
				return CountryCode.US;
			}
			else if (countryName == "canada" || countryName == "ca")
			{
				return CountryCode.CA;
			}
			else
			{
				return null;
			}
		}

		public string Code
		{
			get {return this.code;}
			set {code = value;}
		}

		public static CountryCode US {
			get { return new CountryCode("US"); }
		}

		public static CountryCode CA {
			get { return new CountryCode("CA"); }
		}


        //when a country is null, Used to put something so that a payment can be inserted
        public static CountryCode XX
        {
            get { return new CountryCode("XX"); }
        }
	}
}
