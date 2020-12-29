using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class Country: EFundraisingCRMDataObject {

		private string countryCode;
		private string countryName;
		private string currencyCode;
		private StateCollection countryStates = new StateCollection();


		public Country() : this(null) { }
		public Country(string countryCode) : this(countryCode, null) { }
		public Country(string countryCode, string countryName) : this(countryCode, countryName, null) { }
		public Country(string countryCode, string countryName, string currencyCode) {
			this.countryCode = countryCode;
			this.countryName = countryName;
			this.currencyCode = currencyCode;
		}

		#region Static Data

		public static Country Australia {
			get { return new Country("AU", "Australia", "$ US"); }
		}

		public static Country Canada {
			get { return new Country("CA", "Canada", "$ CAN"); }
		}

		public static Country Brasil {
			get { return new Country("BR", "Brasil", "$ US"); }
		}

		public static Country Guam {
			get { return new Country("GU", "Guam", "$ US"); }
		}

		public static Country PuertoRico {
			get { return new Country("PR", "Puerto Rico", "$ US"); }
		}

		public static Country NewZealand {
			get { return new Country("NZ", "New Zealand", "$ US"); }
		}

		public static Country Other {
			get { return new Country("Unknown", "Other", "None"); }
		}

		public static Country UnitedStates {
			get { return new Country("US", "United States", "$ US"); }
		}

		public static Country Mexico {
			get { return new Country("MX", "Mexico", "None"); }
		}

		public static Country India {
			get { return new Country("ID", "India", "$ US"); }
		}

		public static Country UnitedKingdom {
			get { return new Country("UK", "United Kingdom", "None"); }
		}

		public static Country France {
			get { return new Country("FR", "France", "$ US"); }
		}

		public static Country VirginIslands {
			get { return new Country("VI", "Virgin Islands", "$ US"); }
		}

		public static Country Bermuda {
			get { return new Country("BM", "Bermuda", "$ US"); }
		}

		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Country>\r\n" +
			"	<CountryCode>" + System.Web.HttpUtility.HtmlEncode(countryCode) + "</CountryCode>\r\n" +
			"	<CountryName>" + System.Web.HttpUtility.HtmlEncode(countryName) + "</CountryName>\r\n" +
			"	<CurrencyCode>" + System.Web.HttpUtility.HtmlEncode(currencyCode) + "</CurrencyCode>\r\n" +
			"</Country>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("countryCode")) {
					SetXmlValue(ref countryCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("countryName")) {
					SetXmlValue(ref countryName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("currencyCode")) {
					SetXmlValue(ref currencyCode, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Country[] GetCountrys() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCountrys();
		}

		/*
		public static Country GetCountryByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCountryByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertCountry(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateCountry(this);
		}*/
		#endregion

		#region Properties
		public string CountryCode {
			set { countryCode = value; }
			get { return countryCode; }
		}

		public string CountryName {
			set { countryName = value; }
			get { return countryName; }
		}

		public string CurrencyCode {
			set { currencyCode = value; }
			get { return currencyCode; }
		}

		
		public StateCollection CountryStates 
		{
			set { countryStates = value; }
			get { return countryStates; }
		}

		#endregion
	}

	public class CountryCollections : System.Collections.CollectionBase 
	{
		
		#region IList Members

		public Country this[int index] 
		{
			get{ return (Country)List[index]; }
		}

		public Country this[string countryCode] 
		{
			get
			{
				foreach(Country feCountry in this) 
				{
					if(feCountry.CountryCode == countryCode)
						return feCountry;
				}
				return null;
			}
		}

		public void Insert(int index, Country value) 
		{
			List.Insert(index, value);
		}

		public void Remove(Country value) 
		{
			List.Remove(value);
		}
 
		public bool Contains(Country value) 
		{ 
			return List.Contains(value);
		}

		public int IndexOf(Country value) 
		{
			return List.IndexOf(value);
		}

		public int Add(Country value) 
		{
			return List.Add(value);	
		}

		#endregion	

		#region public static methods

		private static CountryCollections Create() 
		{
			CountryCollections countryList = new CountryCollections();
			Country unitedState = new Country("US", "United States");
			State[] sts = State.GetStatesByCountryCode("US");
			for (int i=0; i< sts.Length; i++)
			{
				unitedState.CountryStates.Add(sts[i]);
			}
			unitedState.CountryStates.Sort();
			countryList.Add(unitedState);
			return countryList;
		}


		#endregion
	}

}
