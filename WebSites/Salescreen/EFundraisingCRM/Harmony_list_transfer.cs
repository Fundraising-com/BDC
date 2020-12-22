using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class HarmonyListTransfer: EFundraisingCRMDataObject {

		private int id;
		private string listName;
		private string listDesc;


		public HarmonyListTransfer() : this(int.MinValue) { }
		public HarmonyListTransfer(int id) : this(id, null) { }
		public HarmonyListTransfer(int id, string listName) : this(id, listName, null) { }
		public HarmonyListTransfer(int id, string listName, string listDesc) {
			this.id = id;
			this.listName = listName;
			this.listDesc = listDesc;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<HarmonyListTransfer>\r\n" +
			"	<Id>" + id + "</Id>\r\n" +
			"	<ListName>" + System.Web.HttpUtility.HtmlEncode(listName) + "</ListName>\r\n" +
			"	<ListDesc>" + System.Web.HttpUtility.HtmlEncode(listDesc) + "</ListDesc>\r\n" +
			"</HarmonyListTransfer>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("id")) {
					SetXmlValue(ref id, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("listName")) {
					SetXmlValue(ref listName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("listDesc")) {
					SetXmlValue(ref listDesc, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static HarmonyListTransfer[] GetHarmonyListTransfers() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetHarmonyListTransfers();
		}

		public static HarmonyListTransfer GetHarmonyListTransferByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetHarmonyListTransferByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertHarmonyListTransfer(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateHarmonyListTransfer(this);
		}
		#endregion

		#region Properties
		public int Id {
			set { id = value; }
			get { return id; }
		}

		public string ListName {
			set { listName = value; }
			get { return listName; }
		}

		public string ListDesc {
			set { listDesc = value; }
			get { return listDesc; }
		}

		#endregion
	}
}
