using System;

namespace GA.BDC.Core.ESubsGlobal.Common {

	public class CultureCaptionCollections : System.Collections.CollectionBase {
		
		#region public properties

		public CultureCaption this[Culture culture] {
			get{ 
				CultureCaption currentGet = new CultureCaption();
				for(int i=0;i<List.Count;i++) {
					if(((CultureCaption)List[i]).culture.CultureCode == culture.CultureCode)
						currentGet = (CultureCaption)List[i];
				}
				return currentGet;
			} 
		}

		#endregion

		#region IList Members

		public CultureCaption this[int index] {
			get{
				return (CultureCaption)List[index];
			}
		}

		public void Insert(int index, CultureCaption value)
		{
			List.Insert(index, value);
		}

		public void Remove(CultureCaption value)
		{
			List.Remove(value);
		}

		public bool Contains(CultureCaption value)
		{
			return List.Contains(value);
		}	

		public int IndexOf(CultureCaption value) {
			return List.IndexOf(value);
		}

		public int Add(CultureCaption value) {
			return List.Add(value);
		}


		#endregion
	}

	public class CultureCaption {

		#region private fields

		private Culture _culture;
		private string _captionValue;

		#endregion	

		public CultureCaption() {
				
		}

		public CultureCaption(Culture culture, string captionValue) {
			_culture = culture;
			_captionValue = captionValue;
		}


		#region public properties

		public Culture culture {
			get{ return _culture; }
			set{ _culture = value; }
		}

		public string CaptionValue {
			get{ return _captionValue; }
			set{ _captionValue = value; }
		}

		#endregion
	}

	
	public class SubDivisionCollections : System.Collections.CollectionBase {

		#region public properties

		public SubDivision this[SubDivision subDivision] {
			get { 

				SubDivision subDiv = new SubDivision();
				for(int i=0;i<List.Count;i++) {
					if(((SubDivision)List[i]) == subDivision)
						return (SubDivision)List[i];
				}
				return subDiv;
			}
		}

		#endregion

        // Code added by Andre Showairy on August 18, 2008
        // Resolution:
        //      Adding canadian provinces to US states

        /// <summary>
        /// Merge Collection A to Collection B and returns the result in a new instance
        /// </summary>
        /// <param name="LeftHandCollection"></param>
        /// <param name="RightHandCollection"></param>
        /// <returns></returns>
        public static SubDivisionCollections operator +(SubDivisionCollections A, SubDivisionCollections B)
        {
            SubDivisionCollections Result = null;

            if (A != null)
            {
                // initialize the result
                if (Result == null)
                    Result = new SubDivisionCollections();

                // moves the content to result
                for (int i = 0; i < A.Count; i++)
                    Result.Add(A[i]);
            }

            if (B != null)
            {
                // initialize the result if not yet initialized
                if (Result == null)
                    Result = new SubDivisionCollections();

                // moves the content to result
                for (int i = 0; i < B.Count; i++)
                    Result.Add(B[i]);
            }

            return Result;
        }
        // end of code added

		#region IList Members

		public SubDivision this[int index] {
			get{ return (SubDivision)List[index]; }
		}

		public void Insert(int index, SubDivision value) {
			List.Insert(index, value);
		}

		public void Remove(SubDivision value) {
			List.Remove(value);
		}

		public bool Contains(SubDivision value) {
			return List.Contains(value);
		}

		public int IndexOf(SubDivision value) {
			return List.IndexOf(value);
		}

		public int Add(SubDivision value){
			return List.Add(value);
		}

		#endregion

		#region public static methods

		public static SubDivisionCollections Create(string countryCode) {
			DataAccess.ESubsGlobalDatabase dba = new GA.BDC.Core.ESubsGlobal.DataAccess.ESubsGlobalDatabase();
			return dba.GetSubvision(countryCode);
		}

		#endregion
	}

	public class SubDivision : SubDivisionCode {
		
		#region private fields

		private SubDivisionCode _subDivisionCode = null;
		private CultureCaptionCollections _cultureCaptionCollections = new CultureCaptionCollections();

		#endregion

		public SubDivision(){}

		public SubDivision(string subDivisionCodeName) {
			_subDivisionCode = SubDivisionCode.Create(subDivisionCodeName);
		}

       	public SubDivision(SubDivisionCode subDivisionCode) {
			_subDivisionCode = subDivisionCode;
		}

		#region public properties

		public CultureCaptionCollections CultureCaptions {
			get{ return _cultureCaptionCollections; }
			set{ _cultureCaptionCollections = value; }
		}

		public SubDivisionCode GetSubDivisionCode {
			get{ return _subDivisionCode; }
		}

		public SubDivisionCode SetSubDivisionCode
		{
			get{ return _subDivisionCode; }
			set{ _subDivisionCode = value; }
		}

		#endregion
	}

	public class CountryCollections : System.Collections.CollectionBase {
		
		#region IList Members

		public Country this[int index] {
			get{ return (Country)List[index]; }
		}

		public Country this[string countryCode] {
			get{
				foreach(Country feCountry in this) {
					if(feCountry == countryCode)
						return feCountry;
				}
				return null;
			}
		}

		public void Insert(int index, Country value) {
			List.Insert(index, value);
		}

		public void Remove(Country value) {
			List.Remove(value);
		}
 
		public bool Contains(Country value) { 
			return List.Contains(value);
		}

		public int IndexOf(Country value) {
			return List.IndexOf(value);
		}

		public int Add(Country value) {
			return List.Add(value);	
		}
		public CountryCode GetCountryCode(string countryName)
		{
			if (string.Compare(countryName, "United States", true) == 0)
				return Country.US;

			
			if (string.Compare(countryName, "Canada", true) == 0)
				return Country.CA;

			for (int i= 0; i < this.Count; i++)
			{
				if (string.Compare(this[i].CountryName, countryName, true) == 0)
					return (CountryCode)this[i];
			}
			return null;
		}

		#endregion	

		#region public static methods

		public static CountryCollections Create() {
			CountryCollections countryList = new CountryCollections();
			Country canada = new Country(Country.CA, "Canada");
			Country unitedState = new Country(Country.US, "United States");
			countryList.Add(canada);
			countryList.Add(unitedState);
			return countryList;
		}

		
		public static CountryCollections AllCountries() 
		{
			CountryCollections countryList = new CountryCollections();
			Country canada = new Country(Country.CA, "Canada");
			canada.Code = Country.CA.Code;
			//canada.GetSubDivision = SubDivisionCode.GetSubDivisionCollectionsByCountryCode(canada.Code);

			Country unitedState = new Country(Country.US, "United States");
			//unitedState.GetSubDivision = SubDivisionCode.GetSubDivisionCollectionsByCountryCode(unitedState.Code);
			countryList.Add(canada);
			countryList.Add(unitedState);
			return countryList;
		}

		#endregion
	}

	public class Country : CountryCode {
		
		#region private fields

		private CountryCode _countryCode = null;
		private string _countryName = string.Empty;
		private CultureCaptionCollections _countryCaptionList = new CultureCaptionCollections();
		private SubDivisionCollections _SubDivisionCollections = new SubDivisionCollections();

		#endregion

		public static implicit operator string(Country country) {
			return (string)country._countryCode;
		}

		public Country() {}

        public Country(CountryCode countryCode) {
			_countryCode = countryCode;
			_SubDivisionCollections = SubDivisionCollections.Create((string)countryCode);	
		}

		public Country(CountryCode countryCode, string countryName) : this(countryCode) {
			_countryName = countryName;
		}

		#region public properties

		public CultureCaptionCollections CountryCaptions {
			get{ return this._countryCaptionList; }
		}

		public CountryCode GetCountryCode {
			get{ return _countryCode; }
		}

		public SubDivisionCollections GetSubDivision {
			get{ return _SubDivisionCollections; }
			set { _SubDivisionCollections = value;}
		}

		public string CountryName {
			get{ return _countryName; }
		}

		#endregion
	}
}