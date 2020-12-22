using System;

namespace efundraising.efundraisingCore.Culture {

	[Serializable()]
	public class Culture {

		private string _CultureCode = string.Empty;

		public Culture() {
			
		}

		public Culture(string cultureCode) {
			_CultureCode = cultureCode;
		}

		public string CultureCode {
			get{ return _CultureCode; }
		}
	}
}
