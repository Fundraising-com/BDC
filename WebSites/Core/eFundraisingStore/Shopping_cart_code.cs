using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class ShoppingCartCode: eFundraisingStoreDataObject {

		private string shoppingCartCode;
		private string description;


		public ShoppingCartCode() : this(null) { }
		public ShoppingCartCode(string shoppingCartCode) : this(shoppingCartCode, null) { }
		public ShoppingCartCode(string shoppingCartCode, string description) {
			this.shoppingCartCode = shoppingCartCode;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ShoppingCartCode>\r\n" +
			"	<ShoppingCartCode>" + System.Web.HttpUtility.HtmlEncode(shoppingCartCode) + "</ShoppingCartCode>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</ShoppingCartCode>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "shoppingCartCode") {
					SetXmlValue(ref shoppingCartCode, node.InnerText);
				}
				if(node.Name.ToLower() == "description") {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ShoppingCartCode[] GetShoppingCartCodes() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetShoppingCartCodes();
		}
		/*
		public static ShoppingCartCode GetShoppingCartCodeByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetShoppingCartCodeByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertShoppingCartCode(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateShoppingCartCode(this);
		}*/
		#endregion

		#region Properties
		public string ShoppingCartCodeID {
			set { shoppingCartCode = value; }
			get { return shoppingCartCode; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
