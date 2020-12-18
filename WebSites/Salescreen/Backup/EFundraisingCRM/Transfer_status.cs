using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class TransferStatus: EFundraisingCRMDataObject {

		private int transferStatusId;
		private string transferStatusDesc;


		public TransferStatus() : this(int.MinValue) { }
		public TransferStatus(int transferStatusId) : this(transferStatusId, null) { }
		public TransferStatus(int transferStatusId, string transferStatusDesc) {
			this.transferStatusId = transferStatusId;
			this.transferStatusDesc = transferStatusDesc;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<TransferStatus>\r\n" +
			"	<TransferStatusId>" + transferStatusId + "</TransferStatusId>\r\n" +
			"	<TransferStatusDesc>" + System.Web.HttpUtility.HtmlEncode(transferStatusDesc) + "</TransferStatusDesc>\r\n" +
			"</TransferStatus>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("transferStatusId")) {
					SetXmlValue(ref transferStatusId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("transferStatusDesc")) {
					SetXmlValue(ref transferStatusDesc, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static TransferStatus[] GetTransferStatuss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTransferStatuss();
		}

		/*
		public static TransferStatus GetTransferStatusByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTransferStatusByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertTransferStatus(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateTransferStatus(this);
		}*/
		#endregion

		#region Properties
		public int TransferStatusId {
			set { transferStatusId = value; }
			get { return transferStatusId; }
		}

		public string TransferStatusDesc {
			set { transferStatusDesc = value; }
			get { return transferStatusDesc; }
		}

		#endregion
	}
}
