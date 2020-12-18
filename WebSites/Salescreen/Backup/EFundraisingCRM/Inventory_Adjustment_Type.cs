using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class InventoryAdjustmentType: EFundraisingCRMDataObject {

		private int inventoryAdjustmentTypeID;
		private string inventoryAdjustmentTypeDesc;


		public InventoryAdjustmentType() : this(int.MinValue) { }
		public InventoryAdjustmentType(int inventoryAdjustmentTypeID) : this(inventoryAdjustmentTypeID, null) { }
		public InventoryAdjustmentType(int inventoryAdjustmentTypeID, string inventoryAdjustmentTypeDesc) {
			this.inventoryAdjustmentTypeID = inventoryAdjustmentTypeID;
			this.inventoryAdjustmentTypeDesc = inventoryAdjustmentTypeDesc;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<InventoryAdjustmentType>\r\n" +
			"	<InventoryAdjustmentTypeID>" + inventoryAdjustmentTypeID + "</InventoryAdjustmentTypeID>\r\n" +
			"	<InventoryAdjustmentTypeDesc>" + System.Web.HttpUtility.HtmlEncode(inventoryAdjustmentTypeDesc) + "</InventoryAdjustmentTypeDesc>\r\n" +
			"</InventoryAdjustmentType>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("inventoryAdjustmentTypeId")) {
					SetXmlValue(ref inventoryAdjustmentTypeID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("inventoryAdjustmentTypeDesc")) {
					SetXmlValue(ref inventoryAdjustmentTypeDesc, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static InventoryAdjustmentType[] GetInventoryAdjustmentTypes() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetInventoryAdjustmentTypes();
		}

		public static InventoryAdjustmentType GetInventoryAdjustmentTypeByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetInventoryAdjustmentTypeByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertInventoryAdjustmentType(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateInventoryAdjustmentType(this);
		}
		#endregion

		#region Properties
		public int InventoryAdjustmentTypeID {
			set { inventoryAdjustmentTypeID = value; }
			get { return inventoryAdjustmentTypeID; }
		}

		public string InventoryAdjustmentTypeDesc {
			set { inventoryAdjustmentTypeDesc = value; }
			get { return inventoryAdjustmentTypeDesc; }
		}

		#endregion
	}
}
