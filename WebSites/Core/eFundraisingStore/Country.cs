using System;
using System.Xml;

using GA.BDC.Core.Database.efundraising;
using System.Data;
using System.Data.SqlClient;
using GA.BDC.Core.Data.Sql;

namespace GA.BDC.Core.eFundraisingStore {

	public class Country: eFundraisingStoreDataObject {

		private string countryCode;
		private string name;
		private string descriptiveInformation;
		private string currency_code;
		private short display;


		public Country() : this(null) { }
		public Country(string countryCode) : this(countryCode, null) { }
		public Country(string countryCode, string name) : this(countryCode, name, null) { }
		public Country(string countryCode, string name, string descriptiveInformation) : this(countryCode, name, descriptiveInformation, null) { }
		public Country(string countryCode, string name, string descriptiveInformation, string currency_code) : this(countryCode, name, descriptiveInformation, currency_code, short.MinValue) { }
		public Country(string countryCode, string name, string descriptiveInformation, string currency_code, short display) {
			this.countryCode = countryCode;
			this.name = name;
			this.descriptiveInformation = descriptiveInformation;
			this.currency_code = currency_code;
			this.display = display;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Country>\r\n" +
			"	<CountryCode>" + System.Web.HttpUtility.HtmlEncode(countryCode) + "</CountryCode>\r\n" +
			"	<Name>" + System.Web.HttpUtility.HtmlEncode(name) + "</Name>\r\n" +
			"	<CurrencyCode>" + System.Web.HttpUtility.HtmlEncode(currency_code) + "</CurrencyCode>\r\n" +
			"	<DescriptiveInformation>" + System.Web.HttpUtility.HtmlEncode(descriptiveInformation) + "</DescriptiveInformation>\r\n" +
			"	<Display>" + display + "</Display>\r\n" +
			"</Country>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "countryCode") {
					SetXmlValue(ref countryCode, node.InnerText);
				}
				if(node.Name.ToLower() == "name") {
					SetXmlValue(ref name, node.InnerText);
				}
				if(node.Name.ToLower() == "descriptiveInformation") {
					SetXmlValue(ref descriptiveInformation, node.InnerText);
				}
				
				if(node.Name.ToLower() == "currencyCode") 
				{
					SetXmlValue(ref currency_code, node.InnerText);
				}
				
				if(node.Name.ToLower() == "display") {
					SetXmlValue(ref display, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
        //private static Country LoadCountry(DataRow row) 
        //{
        //    Country country = new Country();

        //    // Store database values into our business object
        //    country.CountryCode = DBValue.ToString(row["country_code"]);
        //    country.Name = DBValue.ToString(row["Country_Name"]);
        //    //country.DescriptiveInformation = DBValue.ToString(row["descriptive_information"]);
        //    //country.Display = DBValue.ToInt16(row["display"]);
        //    country.CurrencyCode = DBValue.ToString(row["Currency_Code"]);

        //    // return the filled object
        //    return country;
        //}

        //private static Country[] GetCountrys() {
        ////DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
        //efundraising.Database.efundraising.DatabaseObject dbo = new efundraising.Database.efundraising.DatabaseObject();
        //  DataTable dt = dbo.GetCountrys();
        
        //    //GetCountrys
        //    Country[] countrys = null;
        //    if (dt != null) 
        //    {
        //        countrys = new Country[dt.Rows.Count];

        //        for (int i = 0; i < dt.Rows.Count; i++)	
        //        {
        //            // fill our objects
        //            try 
        //            {
        //                countrys[i] = LoadCountry(dt.Rows[i]);
        //            } 
        //            catch(Exception ex) 
        //            {
        //                throw new SqlDataException("Unable to fill object using ", ex);
        //            }
        //        }
        //    }
        //    return countrys;
			
        //}


        //private static Country[] Create()
        //{
        //    Country[] oCtn = GetCountrys();
        //    Country[] oCtn = null;
        //    System.Web.HttpContext.Current.Application["Country"] = oCtn;
        //    return oCtn;
        //}

        //public static Country[] Load() 
        //{
        //    if(System.Web.HttpContext.Current.Application != null) 
        //    {
        //        if(System.Web.HttpContext.Current.Application["Country"] != null)
        //            return (Country[])System.Web.HttpContext.Current.Application["Country"];
        //        else
        //            return Create();
        //    }
        //    else
        //        return Create();
        //}
		
		
		
		
		
		
		
		
		
		
		
		/*
		public static Country GetCountryByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetCountryByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertCountry(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateCountry(this);
		}*/
		#endregion

		#region Properties
		public string CountryCode {
			set { countryCode = value; }
			get { return countryCode; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public string DescriptiveInformation {
			set { descriptiveInformation = value; }
			get { return descriptiveInformation; }
		}
		public string CurrencyCode 
		{
			set { currency_code = value; }
			get { return currency_code; }
		}

		public short Display {
			set { display = value; }
			get { return display; }
		}

		#endregion
	}
}
