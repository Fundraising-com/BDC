using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class XHarmony: EFundraisingCRMDataObject {

		private int leadId;
		private int year;


		public XHarmony() : this(int.MinValue) { }
		public XHarmony(int leadId) : this(leadId, int.MinValue) { }
		public XHarmony(int leadId, int year) {
			this.leadId = leadId;
			this.year = year;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<XHarmony>\r\n" +
			"	<LeadId>" + leadId + "</LeadId>\r\n" +
			"	<Year>" + year + "</Year>\r\n" +
			"</XHarmony>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("leadId")) {
					SetXmlValue(ref leadId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("year")) {
					SetXmlValue(ref year, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static XHarmony[] GetXHarmonys() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetXHarmonys();
		}

		public static XHarmony GetXHarmonyByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetXHarmonyByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertXHarmony(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateXHarmony(this);
		}
		#endregion

		#region Properties
		public int LeadId {
			set { leadId = value; }
			get { return leadId; }
		}

		public int Year {
			set { year = value; }
			get { return year; }
		}

		#endregion
	}
}
