using System;

namespace GA.BDC.Core.EnterpriseStandards {
	/// <summary>
	/// Summary description for Currency.
	/// </summary>
	public class Currency {
		private float cash;

		public Currency() {

		}

		public Currency(int v) {
			cash = (float)v;
		}

		public Currency(float v) {
			cash = v;
		}

		public Currency(Decimal v) {
			cash = (float)v;
		}

		// implicit operator for floats
		public static implicit operator Currency(float v) {
			return new Currency(v);
		}

		// implicit operator for ints
		public static implicit operator Currency(int v) {
			return new Currency(v);
		}

		public static implicit operator Currency(Decimal v) {
			return new Currency(v);
		}

		/// <summary>
		/// ToCurrency will print value with no cash sign
		/// </summary>
		/// <param name="cultureCode"></param>
		/// <returns></returns>
		public string ToCurrency(string cultureCode) {
			string val = "";
			switch(cultureCode) {
				case "fr-CA":
				case "en-CA":
					val = cash.ToString("### ### ### ### ##0.00");
					break;
				default:
					val = cash.ToString("###,###,###,###,##0.00");
					break;
			}
			return val;
		}

		/// <summary>
		/// Print function will print with the culture's cash sign
		/// </summary>
		/// <param name="cultureCode"></param>
		/// <returns></returns>
		public string Print(string cultureCode) {
			string val = "";
			switch(cultureCode) {
				case "en-US":
					val = "$ " + ToCurrency(cultureCode);
					break;
				case "fr-CA":
				case "en-CA":
					val = ToCurrency(cultureCode) + "$";
					break;
				default:
					val = "$ " + ToCurrency(cultureCode);
					break;
			}
			return val;
		}
	}
}
