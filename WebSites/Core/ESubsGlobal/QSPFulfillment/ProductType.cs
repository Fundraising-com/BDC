using System;
using System.Collections.Generic;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal.QSPFulfillment
{
    public class ProductType
    {
        private int product_type_id;
		private int product_line_id;
		private string product_type_name;
		private int administration_supply;
		private decimal fulfillment_charge;
		private int erp_product_type_id;


		public ProductType() : this(int.MinValue) { }
		public ProductType(int product_type_id) : this(product_type_id, int.MinValue) { }
		public ProductType(int product_type_id, int product_line_id) : this(product_type_id, product_line_id, null) { }
		public ProductType(int product_type_id, int product_line_id, string product_type_name) : this(product_type_id, product_line_id, product_type_name, int.MinValue) { }
		public ProductType(int product_type_id, int product_line_id, string product_type_name, int administration_supply) : this(product_type_id, product_line_id, product_type_name, administration_supply, decimal.MinValue) { }
		public ProductType(int product_type_id, int product_line_id, string product_type_name, int administration_supply, decimal fulfillment_charge) : this(product_type_id, product_line_id, product_type_name, administration_supply, fulfillment_charge, int.MinValue) { }
		public ProductType(int product_type_id, int product_line_id, string product_type_name, int administration_supply, decimal fulfillment_charge, int erp_product_type_id) {
			this.product_type_id = product_type_id;
			this.product_line_id = product_line_id;
			this.product_type_name = product_type_name;
			this.administration_supply = administration_supply;
			this.fulfillment_charge = fulfillment_charge;
			this.erp_product_type_id = erp_product_type_id;
		}


		#region XML Methods

		#region Save XML
		private string IdentXML(string xml) {
			string newXML = "";
			string[] lines = xml.Split('\r');
			foreach(string strXml in lines) {
				if(strXml.Trim() == "")
					break;
				newXML += "\t" + strXml.Replace("\n", "") + "\r\n";
			}
			return newXML;
		}

		public virtual string GenerateXML() {
			return "<Product_type>\r\n" +
			"	<Product_type_id>" + product_type_id + "</Product_type_id>\r\n" +
			"	<Product_line_id>" + product_line_id + "</Product_line_id>\r\n" +
			"	<Product_type_name>" + System.Web.HttpUtility.HtmlEncode(product_type_name) + "</Product_type_name>\r\n" +
			"	<Administration_supply>" + administration_supply + "</Administration_supply>\r\n" +
			"	<Fulfillment_charge>" + fulfillment_charge + "</Fulfillment_charge>\r\n" +
			"	<Erp_product_type_id>" + erp_product_type_id + "</Erp_product_type_id>\r\n" +
			"</Product_type>\r\n";
		}
		#endregion

		#region Set Xml Values
		private void SetXmlValue(ref int obj, string val) {
			if(val == "") { obj = int.MinValue; return; }
			obj = int.Parse(val);
		}

		private void SetXmlValue(ref string obj, string val) {
			if(val == "") { obj = null; return; }
			obj = System.Web.HttpUtility.HtmlDecode(val);
		}
		
		private void SetXmlValue(ref bool obj, string val) {
			if(val == "") { obj = false; return; }
			obj = (val.ToLower() == "true");
		}

		private void SetXmlValue(ref Decimal obj, string val) {
			if(val == "") { obj = Decimal.MinValue; return; }
			obj = Decimal.Parse(val);
		}

		private void SetXmlValue(ref DateTime obj, string val) {
			if(val == "") { obj = DateTime.MinValue; return; }
			obj = DateTime.Parse(val);
		}

		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public virtual void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "product_type_id") {
					SetXmlValue(ref product_type_id, node.InnerText);
				}
				if(node.Name.ToLower() == "product_line_id") {
					SetXmlValue(ref product_line_id, node.InnerText);
				}
				if(node.Name.ToLower() == "product_type_name") {
					SetXmlValue(ref product_type_name, node.InnerText);
				}
				if(node.Name.ToLower() == "administration_supply") {
					SetXmlValue(ref administration_supply, node.InnerText);
				}
				if(node.Name.ToLower() == "fulfillment_charge") {
					SetXmlValue(ref fulfillment_charge, node.InnerText);
				}
				if(node.Name.ToLower() == "erp_product_type_id") {
					SetXmlValue(ref erp_product_type_id, node.InnerText);
				}
			}
		}
		// load from an xml string 
		public virtual void LoadXml(string xml) {
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		// load from an xml document object
		public virtual void Load(System.Xml.XmlDocument doc) {
			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		// load from a stream
		public virtual void Load(System.IO.Stream inStream) {
			XmlDocument doc = new XmlDocument();
			doc.Load(inStream);

			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		// load from a text reader
		public virtual void Load(System.IO.TextReader txtReader) {
			XmlDocument doc = new XmlDocument();
			doc.Load(txtReader);

			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		// load from an xml reader
		public virtual void Load(System.Xml.XmlReader reader) {
			XmlDocument doc = new XmlDocument();
			doc.Load(reader);

			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		// load from a xml filename
		public virtual void Load(string filename) {
			XmlDocument doc = new XmlDocument();
			doc.Load(filename);

			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		#endregion

		#endregion

		#region Data Source Methods
		public static List<ProductType> GetProductTypes() {
            DataAccess.QSPReplication dbo = new GA.BDC.Core.ESubsGlobal.DataAccess.QSPReplication();
			return dbo.GetProduct_types();
		}

        public static ProductType GetProductTypeByID(int id)
        {
            DataAccess.QSPReplication dbo = new GA.BDC.Core.ESubsGlobal.DataAccess.QSPReplication();
			return dbo.GetProductTypeByID(id);
		}

        public static ProductType GetProductTypeByCatalogItemID(int catalog_item_id)
        {
            DataAccess.QSPReplication dbo = new GA.BDC.Core.ESubsGlobal.DataAccess.QSPReplication();
            return dbo.GetProductTypeByCatalogItemID(catalog_item_id);
        }
	    #endregion

		#region Properties
		public int ProductTypeID {
			set { product_type_id = value; }
			get { return product_type_id; }
		}

		public int ProductLineID {
			set { product_line_id = value; }
			get { return product_line_id; }
		}

		public string ProductTypeName {
			set { product_type_name = value; }
			get { return product_type_name; }
		}

		public int AdministrationSupply {
			set { administration_supply = value; }
			get { return administration_supply; }
		}

		public decimal FulfillmentCharge {
			set { fulfillment_charge = value; }
			get { return fulfillment_charge; }
		}

		public int ERPProductTypeID {
			set { erp_product_type_id = value; }
			get { return erp_product_type_id; }
		}

		#endregion
    }
}
