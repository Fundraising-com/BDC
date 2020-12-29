using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class CultureSubdivision: eFundraisingStoreDataObject {

		private string cultureCode;
		private string subdivisionCode;
		private string name;


		public CultureSubdivision() : this(null) { }
		public CultureSubdivision(string cultureCode) : this(cultureCode, null) { }
		public CultureSubdivision(string cultureCode, string subdivisionCode) : this(cultureCode, subdivisionCode, null) { }
		public CultureSubdivision(string cultureCode, string subdivisionCode, string name) {
			this.cultureCode = cultureCode;
			this.subdivisionCode = subdivisionCode;
			this.name = name;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<CultureSubdivision>\r\n" +
			"	<CultureCode>" + System.Web.HttpUtility.HtmlEncode(cultureCode) + "</CultureCode>\r\n" +
			"	<SubdivisionCode>" + System.Web.HttpUtility.HtmlEncode(subdivisionCode) + "</SubdivisionCode>\r\n" +
			"	<Name>" + System.Web.HttpUtility.HtmlEncode(name) + "</Name>\r\n" +
			"</CultureSubdivision>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "cultureCode") {
					SetXmlValue(ref cultureCode, node.InnerText);
				}
				if(node.Name.ToLower() == "subdivisionCode") {
					SetXmlValue(ref subdivisionCode, node.InnerText);
				}
				if(node.Name.ToLower() == "name") {
					SetXmlValue(ref name, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static CultureSubdivision[] GetCultureSubdivisions() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetCultureSubdivisions();
		}

		/*
		public static CultureSubdivision GetCultureSubdivisionByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetCultureSubdivisionByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertCultureSubdivision(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateCultureSubdivision(this);
		}*/
		#endregion

		#region Properties
		public string CultureCode {
			set { cultureCode = value; }
			get { return cultureCode; }
		}

		public string SubdivisionCode {
			set { subdivisionCode = value; }
			get { return subdivisionCode; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		#endregion
	}
}
