using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class CultureSubdivisionName: eFundraisingStoreDataObject {

		private string cultureCode;
		private string subdivisionCode;
		private string subdivisionName;


		public CultureSubdivisionName() : this(null) { }
		public CultureSubdivisionName(string cultureCode) : this(cultureCode, null) { }
		public CultureSubdivisionName(string cultureCode, string subdivisionCode) : this(cultureCode, subdivisionCode, null) { }
		public CultureSubdivisionName(string cultureCode, string subdivisionCode, string subdivisionName) {
			this.cultureCode = cultureCode;
			this.subdivisionCode = subdivisionCode;
			this.subdivisionName = subdivisionName;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<CultureSubdivisionName>\r\n" +
			"	<CultureCode>" + cultureCode + "</CultureCode>\r\n" +
			"	<SubdivisionCode>" + subdivisionCode + "</SubdivisionCode>\r\n" +
			"	<SubdivisionName>" + subdivisionName + "</SubdivisionName>\r\n" +
			"</CultureSubdivisionName>\r\n";
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
				if(node.Name.ToLower() == "subdivisionName") {
					SetXmlValue(ref subdivisionName, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		/*
		public static CultureSubdivisionName[] GetCultureSubdivisionNames() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetCultureSubdivisionNames();
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

		public string SubdivisionName {
			set { subdivisionName = value; }
			get { return subdivisionName; }
		}

		#endregion
	}
}
