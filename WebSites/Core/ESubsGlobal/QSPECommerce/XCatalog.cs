using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal.QSPECommerce
{
    public class XCatalog : EnvironmentBase
    {
        private int x_Catalog_Id;
		private string catalog_Name;
		private int language_id;
		private int homepage_Order;
		private DateTime create_Date;
		private DateTime modify_Date;
		private string modified_By;
		private int deleted_TF;
		private int x_Catalog_Type_Id;


		public XCatalog() : this(int.MinValue) { }
		public XCatalog(int x_Catalog_Id) : this(x_Catalog_Id, null) { }
		public XCatalog(int x_Catalog_Id, string catalog_Name) : this(x_Catalog_Id, catalog_Name, int.MinValue) { }
		public XCatalog(int x_Catalog_Id, string catalog_Name, int language_id) : this(x_Catalog_Id, catalog_Name, language_id, int.MinValue) { }
		public XCatalog(int x_Catalog_Id, string catalog_Name, int language_id, int homepage_Order) : this(x_Catalog_Id, catalog_Name, language_id, homepage_Order, DateTime.MinValue) { }
		public XCatalog(int x_Catalog_Id, string catalog_Name, int language_id, int homepage_Order, DateTime create_Date) : this(x_Catalog_Id, catalog_Name, language_id, homepage_Order, create_Date, DateTime.MinValue) { }
		public XCatalog(int x_Catalog_Id, string catalog_Name, int language_id, int homepage_Order, DateTime create_Date, DateTime modify_Date) : this(x_Catalog_Id, catalog_Name, language_id, homepage_Order, create_Date, modify_Date, null) { }
		public XCatalog(int x_Catalog_Id, string catalog_Name, int language_id, int homepage_Order, DateTime create_Date, DateTime modify_Date, string modified_By) : this(x_Catalog_Id, catalog_Name, language_id, homepage_Order, create_Date, modify_Date, modified_By, int.MinValue) { }
		public XCatalog(int x_Catalog_Id, string catalog_Name, int language_id, int homepage_Order, DateTime create_Date, DateTime modify_Date, string modified_By, int deleted_TF) : this(x_Catalog_Id, catalog_Name, language_id, homepage_Order, create_Date, modify_Date, modified_By, deleted_TF, int.MinValue) { }
		public XCatalog(int x_Catalog_Id, string catalog_Name, int language_id, int homepage_Order, DateTime create_Date, DateTime modify_Date, string modified_By, int deleted_TF, int x_Catalog_Type_Id) {
			this.x_Catalog_Id = x_Catalog_Id;
			this.catalog_Name = catalog_Name;
			this.language_id = language_id;
			this.homepage_Order = homepage_Order;
			this.create_Date = create_Date;
			this.modify_Date = modify_Date;
			this.modified_By = modified_By;
			this.deleted_TF = deleted_TF;
			this.x_Catalog_Type_Id = x_Catalog_Type_Id;
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
			return "<X_Catalog>\r\n" +
			"	<X_Catalog_Id>" + x_Catalog_Id + "</X_Catalog_Id>\r\n" +
			"	<Catalog_Name>" + System.Web.HttpUtility.HtmlEncode(catalog_Name) + "</Catalog_Name>\r\n" +
			"	<Language_id>" + language_id + "</Language_id>\r\n" +
			"	<Homepage_Order>" + homepage_Order + "</Homepage_Order>\r\n" +
			"	<Create_Date>" + create_Date + "</Create_Date>\r\n" +
			"	<Modify_Date>" + modify_Date + "</Modify_Date>\r\n" +
			"	<Modified_By>" + System.Web.HttpUtility.HtmlEncode(modified_By) + "</Modified_By>\r\n" +
			"	<Deleted_TF>" + deleted_TF + "</Deleted_TF>\r\n" +
			"	<X_Catalog_Type_Id>" + x_Catalog_Type_Id + "</X_Catalog_Type_Id>\r\n" +
			"</X_Catalog>\r\n";
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
				if(node.Name.ToLower() == "x_catalog_id") {
					SetXmlValue(ref x_Catalog_Id, node.InnerText);
				}
				if(node.Name.ToLower() == "catalog_name") {
					SetXmlValue(ref catalog_Name, node.InnerText);
				}
				if(node.Name.ToLower() == "language_id") {
					SetXmlValue(ref language_id, node.InnerText);
				}
				if(node.Name.ToLower() == "homepage_order") {
					SetXmlValue(ref homepage_Order, node.InnerText);
				}
				if(node.Name.ToLower() == "create_date") {
					SetXmlValue(ref create_Date, node.InnerText);
				}
				if(node.Name.ToLower() == "modify_date") {
					SetXmlValue(ref modify_Date, node.InnerText);
				}
				if(node.Name.ToLower() == "modified_by") {
					SetXmlValue(ref modified_By, node.InnerText);
				}
				if(node.Name.ToLower() == "deleted_tf") {
					SetXmlValue(ref deleted_TF, node.InnerText);
				}
				if(node.Name.ToLower() == "x_catalog_type_id") {
					SetXmlValue(ref x_Catalog_Type_Id, node.InnerText);
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
	
		public static XCatalog GetXCatalogByID(int xCatalogID) {
            ESubsGlobal.DataAccess.QSPReplication dbo = new GA.BDC.Core.ESubsGlobal.DataAccess.QSPReplication();
            return dbo.GetXCatalogByID(xCatalogID);
		}
     
		#endregion

		#region Properties
		public int XCatalogID {
			set { x_Catalog_Id = value; }
			get { return x_Catalog_Id; }
		}

		public string CatalogName {
			set { catalog_Name = value; }
			get { return catalog_Name; }
		}

		public int LanguageID {
			set { language_id = value; }
			get { return language_id; }
		}

		public int HomepageOrder {
			set { homepage_Order = value; }
			get { return homepage_Order; }
		}

		public DateTime CreateDate {
			set { create_Date = value; }
			get { return create_Date; }
		}

		public DateTime ModifyDate {
			set { modify_Date = value; }
			get { return modify_Date; }
		}

		public string ModifiedBy {
			set { modified_By = value; }
			get { return modified_By; }
		}

		public int DeletedTF {
			set { deleted_TF = value; }
			get { return deleted_TF; }
		}

		public int XCatalogTypeID {
			set { x_Catalog_Type_Id = value; }
			get { return x_Catalog_Type_Id; }
		}

		#endregion
    }
}
