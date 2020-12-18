using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class SalesConstraints: EFundraisingCRMDataObject {

		private short salesConstraintId;
		private short productClassId;
		private string description;
		private int highLevel;


		public SalesConstraints() : this(short.MinValue) { }
		public SalesConstraints(short salesConstraintId) : this(salesConstraintId, short.MinValue) { }
		public SalesConstraints(short salesConstraintId, short productClassId) : this(salesConstraintId, productClassId, null) { }
		public SalesConstraints(short salesConstraintId, short productClassId, string description) : this(salesConstraintId, productClassId, description, int.MinValue) { }
		public SalesConstraints(short salesConstraintId, short productClassId, string description, int highLevel) {
			this.salesConstraintId = salesConstraintId;
			this.productClassId = productClassId;
			this.description = description;
			this.highLevel = highLevel;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<SalesConstraints>\r\n" +
			"	<SalesConstraintId>" + salesConstraintId + "</SalesConstraintId>\r\n" +
			"	<ProductClassId>" + productClassId + "</ProductClassId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"	<HighLevel>" + highLevel + "</HighLevel>\r\n" +
			"</SalesConstraints>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("salesConstraintId")) {
					SetXmlValue(ref salesConstraintId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("productClassId")) {
					SetXmlValue(ref productClassId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("highLevel")) {
					SetXmlValue(ref highLevel, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static SalesConstraints[] GetSalesConstraintss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSalesConstraintss();
		}

		/*
		public static SalesConstraints GetSalesConstraintsByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSalesConstraintsByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertSalesConstraints(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateSalesConstraints(this);
		}*/
		#endregion

		#region Properties
		public short SalesConstraintId {
			set { salesConstraintId = value; }
			get { return salesConstraintId; }
		}

		public short ProductClassId {
			set { productClassId = value; }
			get { return productClassId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public int HighLevel {
			set { highLevel = value; }
			get { return highLevel; }
		}

		#endregion
	}
}
