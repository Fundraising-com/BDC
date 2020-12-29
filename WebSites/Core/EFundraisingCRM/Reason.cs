using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class Reason: EFundraisingCRMDataObject {

		private int reasonID;
		private string description;
		private int isActive;


		public Reason() : this(int.MinValue) { }
		public Reason(int reasonID) : this(reasonID, null) { }
		public Reason(int reasonID, string description) : this(reasonID, description, int.MinValue) { }
		public Reason(int reasonID, string description, int isActive) {
			this.reasonID = reasonID;
			this.description = description;
			this.isActive = isActive;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Reason>\r\n" +
			"	<ReasonID>" + reasonID + "</ReasonID>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"	<IsActive>" + isActive + "</IsActive>\r\n" +
			"</Reason>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("reasonId")) {
					SetXmlValue(ref reasonID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isActive")) {
					SetXmlValue(ref isActive, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Reason[] GetReasons() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetReasons();
		}

		public static Reason GetReasonByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetReasonByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertReason(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateReason(this);
		}
		#endregion

		#region Properties
		public int ReasonID {
			set { reasonID = value; }
			get { return reasonID; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public int IsActive {
			set { isActive = value; }
			get { return isActive; }
		}

		#endregion
	}
}
