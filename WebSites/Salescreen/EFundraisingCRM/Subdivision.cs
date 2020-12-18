using System;
using System.Xml;

namespace efundraising.EFundraisingCRM 
{

	public class Subdivision: EFundraisingCRMDataObject 
	{

		private string subdivisionCode;
		private string countryCode;
		private string subdivisionName1;
		private string subdivisionName2;
		private string subdivisionName3;
		private string regionalDivision;
		private string subdivisionCategory;


		public Subdivision() : this(null) { }
		public Subdivision(string subdivisionCode) : this(subdivisionCode, null) { }
		public Subdivision(string subdivisionCode, string countryCode) : this(subdivisionCode, countryCode, null) { }
		public Subdivision(string subdivisionCode, string countryCode, string subdivisionName1) : this(subdivisionCode, countryCode, subdivisionName1, null) { }
		public Subdivision(string subdivisionCode, string countryCode, string subdivisionName1, string subdivisionName2) : this(subdivisionCode, countryCode, subdivisionName1, subdivisionName2, null) { }
		public Subdivision(string subdivisionCode, string countryCode, string subdivisionName1, string subdivisionName2, string subdivisionName3) : this(subdivisionCode, countryCode, subdivisionName1, subdivisionName2, subdivisionName3, null) { }
		public Subdivision(string subdivisionCode, string countryCode, string subdivisionName1, string subdivisionName2, string subdivisionName3, string regionalDivision) : this(subdivisionCode, countryCode, subdivisionName1, subdivisionName2, subdivisionName3, regionalDivision, null) { }
		public Subdivision(string subdivisionCode, string countryCode, string subdivisionName1, string subdivisionName2, string subdivisionName3, string regionalDivision, string subdivisionCategory) 
		{
			this.subdivisionCode = subdivisionCode;
			this.countryCode = countryCode;
			this.subdivisionName1 = subdivisionName1;
			this.subdivisionName2 = subdivisionName2;
			this.subdivisionName3 = subdivisionName3;
			this.regionalDivision = regionalDivision;
			this.subdivisionCategory = subdivisionCategory;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() 
		{
			return "<Subdivision>\r\n" +
				"	<SubdivisionCode>" + subdivisionCode + "</SubdivisionCode>\r\n" +
				"	<CountryCode>" + countryCode + "</CountryCode>\r\n" +
				"	<SubdivisionName1>" + subdivisionName1 + "</SubdivisionName1>\r\n" +
				"	<SubdivisionName2>" + subdivisionName2 + "</SubdivisionName2>\r\n" +
				"	<SubdivisionName3>" + subdivisionName3 + "</SubdivisionName3>\r\n" +
				"	<RegionalDivision>" + regionalDivision + "</RegionalDivision>\r\n" +
				"	<SubdivisionCategory>" + subdivisionCategory + "</SubdivisionCategory>\r\n" +
				"</Subdivision>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) 
		{
			foreach(XmlNode node in childNodes) 
			{
				if(node.Name.ToLower() == "subdivisionCode") 
				{
					SetXmlValue(ref subdivisionCode, node.InnerText);
				}
				if(node.Name.ToLower() == "countryCode") 
				{
					SetXmlValue(ref countryCode, node.InnerText);
				}
				if(node.Name.ToLower() == "subdivisionName1") 
				{
					SetXmlValue(ref subdivisionName1, node.InnerText);
				}
				if(node.Name.ToLower() == "subdivisionName2") 
				{
					SetXmlValue(ref subdivisionName2, node.InnerText);
				}
				if(node.Name.ToLower() == "subdivisionName3") 
				{
					SetXmlValue(ref subdivisionName3, node.InnerText);
				}
				if(node.Name.ToLower() == "regionalDivision") 
				{
					SetXmlValue(ref regionalDivision, node.InnerText);
				}
				if(node.Name.ToLower() == "subdivisionCategory") 
				{
					SetXmlValue(ref subdivisionCategory, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Subdivision[] GetSubdivisions() 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSubdivisions();
		}

		public static Subdivision[] GetSubdivisionsByCountryCode(string country) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSubdivisionsByCountryCode(country);
		}

		public static Subdivision GetSubdivisionByCode(string code) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSubdivisionByCode(code);
		}

		/*public int Insert() 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertSubdivision(this);
		}

		public int Update() 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateSubdivision(this);
		}*/
		#endregion

		#region Properties
		public string SubdivisionCode 
		{
			set { subdivisionCode = value; }
			get { return subdivisionCode; }
		}

		public string CountryCode 
		{
			set { countryCode = value; }
			get { return countryCode; }
		}

		public string SubdivisionName1 
		{
			set { subdivisionName1 = value; }
			get { return subdivisionName1; }
		}

		public string SubdivisionName2 
		{
			set { subdivisionName2 = value; }
			get { return subdivisionName2; }
		}

		public string SubdivisionName3 
		{
			set { subdivisionName3 = value; }
			get { return subdivisionName3; }
		}

		public string RegionalDivision 
		{
			set { regionalDivision = value; }
			get { return regionalDivision; }
		}

		public string SubdivisionCategory 
		{
			set { subdivisionCategory = value; }
			get { return subdivisionCategory; }
		}

		#endregion
	}
}
