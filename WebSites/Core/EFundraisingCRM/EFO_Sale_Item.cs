using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class EFOSaleItem: EFundraisingCRMDataObject {

		private int itemID;
		private int saleID;
		private float quantity;


		public EFOSaleItem() : this(int.MinValue) { }
		public EFOSaleItem(int itemID) : this(itemID, int.MinValue) { }
		public EFOSaleItem(int itemID, int saleID) : this(itemID, saleID, float.MinValue) { }
		public EFOSaleItem(int itemID, int saleID, float quantity) {
			this.itemID = itemID;
			this.saleID = saleID;
			this.quantity = quantity;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<EFOSaleItem>\r\n" +
			"	<ItemID>" + itemID + "</ItemID>\r\n" +
			"	<SaleID>" + saleID + "</SaleID>\r\n" +
			"	<Quantity>" + quantity + "</Quantity>\r\n" +
			"</EFOSaleItem>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("itemId")) {
					SetXmlValue(ref itemID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("saleId")) {
					SetXmlValue(ref saleID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("quantity")) {
					SetXmlValue(ref quantity, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static EFOSaleItem[] GetEFOSaleItems() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOSaleItems();
		}

		public static EFOSaleItem GetEFOSaleItemByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOSaleItemByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertEFOSaleItem(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateEFOSaleItem(this);
		}
		#endregion

		#region Properties
		public int ItemID {
			set { itemID = value; }
			get { return itemID; }
		}

		public int SaleID {
			set { saleID = value; }
			get { return saleID; }
		}

		public float Quantity {
			set { quantity = value; }
			get { return quantity; }
		}

		#endregion
	}
}
