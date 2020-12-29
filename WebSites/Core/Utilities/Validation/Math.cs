using System;

namespace GA.BDC.Core.Utilities.Validation {
	/// <summary>
	/// Wrapper for math validation
	/// </summary>
	public class Math {

		public Math() {
			//
			// TODO: Add constructor logic here
			//
		}

		public static bool IsInt(string s) {
			int i = 0;
			try {
				i = int.Parse(s);
			} catch {
				return false;
			}
			return false;
		}

		public static bool IsInt16(string s) {
			Int16 i = 0;
			try {
				i = Int16.Parse(s);
			} catch {
				return false;
			}
			return false;
		}

		public static bool IsInt32(string s) {
			Int32 i = 0;
			try {
				i = Int32.Parse(s);
			} catch {
				return false;
			}
			return false;
		}

		public static bool IsInt64(string s) {
			Int32 i = 0;
			try {
				i = Int32.Parse(s);
			} catch {
				return false;
			}
			return false;
		}

		public static bool IsDecimal(string s) {
			Decimal i = 0;
			try {
				i = Decimal.Parse(s);
			} catch {
				return false;
			}
			return false;
		}

		public static bool IsDateTime(string s) {
			DateTime i = DateTime.Now;
			try {
				i = DateTime.Parse(s);
			} catch {
				return false;
			}
			return false;
		}

		public static bool IsFloat(string s) {
			float i = 0;
			try {
				i = float.Parse(s);
			} catch {
				return false;
			}
			return false;
		}

	}
}
