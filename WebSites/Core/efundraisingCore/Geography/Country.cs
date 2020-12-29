using System;

namespace GA.BDC.Core.efundraisingCore.Geography
{
	public class Country {

		#region private fields

		private string _CountryCode = string.Empty;
		private string _CountryName = string.Empty;
        private string _CountryID = string.Empty;               // Added December 1, 2008
        private string _CountryCurrencyCode = string.Empty;     // Added December 1, 2008

		private StateCollections _States = new StateCollections();

		#endregion

		#region public constructors

		public Country() {
			
		}

		public Country(string pCountryCode, string pCountryName) {
			_CountryCode = pCountryCode;
			_CountryName = pCountryName;
			_States = StateCollections.Create(pCountryCode);
		}

        // Added December 1, 2008
        public Country(string pCountryCode, string pCountryName, string pCountryID, string pCountryCurrencyCode)
        {
            _CountryCode = pCountryCode;
            _CountryName = pCountryName;
            _States = StateCollections.Create(pCountryCode);
            _CountryID = pCountryID;
            _CountryCurrencyCode = pCountryCurrencyCode;
        }

		#endregion

		#region public properties

		public string CountryCode 
        {
            set { _CountryCode = value; }
			get{ return this._CountryCode; }
		}

		public string CountryName {
            set { _CountryName = value; }
			get{ return this._CountryName; }
		}

		public StateCollections States {
			get{ return this._States; }
		}

        public string CountryCurrencyCode
        {
            set { _CountryCurrencyCode = value; }
            get { return this._CountryCurrencyCode; }
        }

        public string CountryID
        {
            set { _CountryID = value; }
            get { return this._CountryID; }
        }

		#endregion
	}
}
