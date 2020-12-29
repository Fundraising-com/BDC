using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class XJumpstart: EFundraisingCRMDataObject {

		private int leadId;
		private int year;


		public XJumpstart() : this(int.MinValue) { }
		public XJumpstart(int leadId) : this(leadId, int.MinValue) { }
		public XJumpstart(int leadId, int year) {
			this.leadId = leadId;
			this.year = year;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<XJumpstart>\r\n" +
			"	<LeadId>" + leadId + "</LeadId>\r\n" +
			"	<Year>" + year + "</Year>\r\n" +
			"</XJumpstart>\r\n";
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
		public static XJumpstart[] GetXJumpstarts() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetXJumpstarts();
		}

		public static XJumpstart GetXJumpstartByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetXJumpstartByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertXJumpstart(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateXJumpstart(this);
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
