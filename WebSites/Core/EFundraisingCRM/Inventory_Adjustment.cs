using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class InventoryAdjustment: EFundraisingCRMDataObject {

		private int inventoryAdjustmentID;
		private int inventoryAdjustmentTypeID;
		private int scratchBookId;
		private DateTime adjustmentDate;
		private int quantity;


		public InventoryAdjustment() : this(int.MinValue) { }
		public InventoryAdjustment(int inventoryAdjustmentID) : this(inventoryAdjustmentID, int.MinValue) { }
		public InventoryAdjustment(int inventoryAdjustmentID, int inventoryAdjustmentTypeID) : this(inventoryAdjustmentID, inventoryAdjustmentTypeID, int.MinValue) { }
		public InventoryAdjustment(int inventoryAdjustmentID, int inventoryAdjustmentTypeID, int scratchBookId) : this(inventoryAdjustmentID, inventoryAdjustmentTypeID, scratchBookId, DateTime.MinValue) { }
		public InventoryAdjustment(int inventoryAdjustmentID, int inventoryAdjustmentTypeID, int scratchBookId, DateTime adjustmentDate) : this(inventoryAdjustmentID, inventoryAdjustmentTypeID, scratchBookId, adjustmentDate, int.MinValue) { }
		public InventoryAdjustment(int inventoryAdjustmentID, int inventoryAdjustmentTypeID, int scratchBookId, DateTime adjustmentDate, int quantity) {
			this.inventoryAdjustmentID = inventoryAdjustmentID;
			this.inventoryAdjustmentTypeID = inventoryAdjustmentTypeID;
			this.scratchBookId = scratchBookId;
			this.adjustmentDate = adjustmentDate;
			this.quantity = quantity;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<InventoryAdjustment>\r\n" +
			"	<InventoryAdjustmentID>" + inventoryAdjustmentID + "</InventoryAdjustmentID>\r\n" +
			"	<InventoryAdjustmentTypeID>" + inventoryAdjustmentTypeID + "</InventoryAdjustmentTypeID>\r\n" +
			"	<ScratchBookId>" + scratchBookId + "</ScratchBookId>\r\n" +
			"	<AdjustmentDate>" + adjustmentDate + "</AdjustmentDate>\r\n" +
			"	<Quantity>" + quantity + "</Quantity>\r\n" +
			"</InventoryAdjustment>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("inventoryAdjustmentId")) {
					SetXmlValue(ref inventoryAdjustmentID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("inventoryAdjustmentTypeId")) {
					SetXmlValue(ref inventoryAdjustmentTypeID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("scratchBookId")) {
					SetXmlValue(ref scratchBookId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("adjustmentDate")) {
					SetXmlValue(ref adjustmentDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("quantity")) {
					SetXmlValue(ref quantity, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static InventoryAdjustment[] GetInventoryAdjustments() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetInventoryAdjustments();
		}

		public static InventoryAdjustment GetInventoryAdjustmentByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetInventoryAdjustmentByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertInventoryAdjustment(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateInventoryAdjustment(this);
		}
		#endregion

		#region Properties
		public int InventoryAdjustmentID {
			set { inventoryAdjustmentID = value; }
			get { return inventoryAdjustmentID; }
		}

		public int InventoryAdjustmentTypeID {
			set { inventoryAdjustmentTypeID = value; }
			get { return inventoryAdjustmentTypeID; }
		}

		public int ScratchBookId {
			set { scratchBookId = value; }
			get { return scratchBookId; }
		}

		public DateTime AdjustmentDate {
			set { adjustmentDate = value; }
			get { return adjustmentDate; }
		}

		public int Quantity {
			set { quantity = value; }
			get { return quantity; }
		}

		#endregion
	}
}
