using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class EFOItem: EFundraisingCRMDataObject {

		private int itemID;
		private string title;
		private float price;
		private float amount2Supplier;
		private float amount2Group;
		private string description;


		public EFOItem() : this(int.MinValue) { }
		public EFOItem(int itemID) : this(itemID, null) { }
		public EFOItem(int itemID, string title) : this(itemID, title, float.MinValue) { }
		public EFOItem(int itemID, string title, float price) : this(itemID, title, price, float.MinValue) { }
		public EFOItem(int itemID, string title, float price, float amount2Supplier) : this(itemID, title, price, amount2Supplier, float.MinValue) { }
		public EFOItem(int itemID, string title, float price, float amount2Supplier, float amount2Group) : this(itemID, title, price, amount2Supplier, amount2Group, null) { }
		public EFOItem(int itemID, string title, float price, float amount2Supplier, float amount2Group, string description) {
			this.itemID = itemID;
			this.title = title;
			this.price = price;
			this.amount2Supplier = amount2Supplier;
			this.amount2Group = amount2Group;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<EFOItem>\r\n" +
			"	<ItemID>" + itemID + "</ItemID>\r\n" +
			"	<Title>" + System.Web.HttpUtility.HtmlEncode(title) + "</Title>\r\n" +
			"	<Price>" + price + "</Price>\r\n" +
			"	<Amount2Supplier>" + amount2Supplier + "</Amount2Supplier>\r\n" +
			"	<Amount2Group>" + amount2Group + "</Amount2Group>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</EFOItem>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("itemId")) {
					SetXmlValue(ref itemID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("title")) {
					SetXmlValue(ref title, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("price")) {
					SetXmlValue(ref price, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("amount2supplier")) {
					SetXmlValue(ref amount2Supplier, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("amount2group")) {
					SetXmlValue(ref amount2Group, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static EFOItem[] GetEFOItems() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOItems();
		}

		public static EFOItem GetEFOItemByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOItemByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertEFOItem(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateEFOItem(this);
		}
		#endregion

		#region Properties
		public int ItemID {
			set { itemID = value; }
			get { return itemID; }
		}

		public string Title {
			set { title = value; }
			get { return title; }
		}

		public float Price {
			set { price = value; }
			get { return price; }
		}

		public float Amount2Supplier {
			set { amount2Supplier = value; }
			get { return amount2Supplier; }
		}

		public float Amount2Group {
			set { amount2Group = value; }
			get { return amount2Group; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
