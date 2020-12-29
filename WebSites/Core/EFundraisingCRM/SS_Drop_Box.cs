using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class SSDropBox: EFundraisingCRMDataObject {

		private int sSDropBoxId;
		private string sSDropBoxName;
		private int displayOrder;


		public SSDropBox() : this(int.MinValue) { }
		public SSDropBox(int sSDropBoxId) : this(sSDropBoxId, null) { }
		public SSDropBox(int sSDropBoxId, string sSDropBoxName) : this(sSDropBoxId, sSDropBoxName, int.MinValue) { }
		public SSDropBox(int sSDropBoxId, string sSDropBoxName, int displayOrder) {
			this.sSDropBoxId = sSDropBoxId;
			this.sSDropBoxName = sSDropBoxName;
			this.displayOrder = displayOrder;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<SSDropBox>\r\n" +
			"	<SSDropBoxId>" + sSDropBoxId + "</SSDropBoxId>\r\n" +
			"	<SSDropBoxName>" + System.Web.HttpUtility.HtmlEncode(sSDropBoxName) + "</SSDropBoxName>\r\n" +
			"	<DisplayOrder>" + displayOrder + "</DisplayOrder>\r\n" +
			"</SSDropBox>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("ssDropBoxId")) {
					SetXmlValue(ref sSDropBoxId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("ssDropBoxName")) {
					SetXmlValue(ref sSDropBoxName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("displayOrder")) {
					SetXmlValue(ref displayOrder, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static SSDropBox[] GetSSDropBoxs() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSSDropBoxs();
		}

		public static SSDropBox GetSSDropBoxByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSSDropBoxByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertSSDropBox(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateSSDropBox(this);
		}
		#endregion

		#region Properties
		public int SSDropBoxId {
			set { sSDropBoxId = value; }
			get { return sSDropBoxId; }
		}

		public string SSDropBoxName {
			set { sSDropBoxName = value; }
			get { return sSDropBoxName; }
		}

		public int DisplayOrder {
			set { displayOrder = value; }
			get { return displayOrder; }
		}

		#endregion
	}
}
