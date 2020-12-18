using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class ConsultantTransferStatus: EFundraisingCRMDataObject {

		private short consultantTransferStatusId;
		private string consultantTransferStatusDesc;


		public ConsultantTransferStatus() : this(short.MinValue) { }
		public ConsultantTransferStatus(short consultantTransferStatusId) : this(consultantTransferStatusId, null) { }
		public ConsultantTransferStatus(short consultantTransferStatusId, string consultantTransferStatusDesc) {
			this.consultantTransferStatusId = consultantTransferStatusId;
			this.consultantTransferStatusDesc = consultantTransferStatusDesc;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ConsultantTransferStatus>\r\n" +
			"	<ConsultantTransferStatusId>" + consultantTransferStatusId + "</ConsultantTransferStatusId>\r\n" +
			"	<ConsultantTransferStatusDesc>" + System.Web.HttpUtility.HtmlEncode(consultantTransferStatusDesc) + "</ConsultantTransferStatusDesc>\r\n" +
			"</ConsultantTransferStatus>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("consultantTransferStatusId")) {
					SetXmlValue(ref consultantTransferStatusId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("consultantTransferStatusDesc")) {
					SetXmlValue(ref consultantTransferStatusDesc, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ConsultantTransferStatus[] GetConsultantTransferStatuss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetConsultantTransferStatuss();
		}

		/*
		public static ConsultantTransferStatus GetConsultantTransferStatusByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetConsultantTransferStatusByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertConsultantTransferStatus(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateConsultantTransferStatus(this);
		}*/
		#endregion

		#region Properties
		public short ConsultantTransferStatusId {
			set { consultantTransferStatusId = value; }
			get { return consultantTransferStatusId; }
		}

		public string ConsultantTransferStatusDesc {
			set { consultantTransferStatusDesc = value; }
			get { return consultantTransferStatusDesc; }
		}

		#endregion
	}
}
