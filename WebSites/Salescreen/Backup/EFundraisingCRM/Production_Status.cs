using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class ProductionStatus: EFundraisingCRMDataObject {

		private int productionStatusID;
		private string description;


		public ProductionStatus() : this(int.MinValue) { }
		public ProductionStatus(int productionStatusID) : this(productionStatusID, null) { }
		public ProductionStatus(int productionStatusID, string description) {
			this.productionStatusID = productionStatusID;
			this.description = description;
		}

		#region Static Data
		
		public static ProductionStatus Default {
			get { return ProductionStatus.ToDo; }
		}
		
		public static ProductionStatus ToDo {
			get { return new ProductionStatus(1, "To Do"); }
		}

		public static ProductionStatus BookletPrinted {
			get { return new ProductionStatus(2, "Booklet Printed"); }
		}

		public static ProductionStatus BookletAssembled {
			get { return new ProductionStatus(3, "Booklet Assembled"); }
		}

		public static ProductionStatus StockShipped {
			get { return new ProductionStatus(4, "Stock Shipped"); }
		}

		public static ProductionStatus DesignPrinted {
			get { return new ProductionStatus(5, "Design Printed"); }
		}

		public static ProductionStatus InTransit {
			get { return new ProductionStatus(6, "In Transit"); }
		}

		public static ProductionStatus BoxReturned {
			get { return new ProductionStatus(7, "Box Returned"); }
		}

		public static ProductionStatus StockReshipped {
			get { return new ProductionStatus(8, "Stock Reshipped"); }
		}

		public static ProductionStatus Released {
			get { return new ProductionStatus(9, "Released"); }
		}

		public static ProductionStatus Voided {
			get { return new ProductionStatus(10, "Voided"); }
		}

		public static ProductionStatus Open {
			get { return new ProductionStatus(11, "Open"); }
		}

		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ProductionStatus>\r\n" +
			"	<ProductionStatusID>" + productionStatusID + "</ProductionStatusID>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</ProductionStatus>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("productionStatusId")) {
					SetXmlValue(ref productionStatusID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ProductionStatus[] GetProductionStatuss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetProductionStatuss();
		}

		public static ProductionStatus GetProductionStatusByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetProductionStatusByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertProductionStatus(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateProductionStatus(this);
		}
		#endregion

		#region Properties
		public int ProductionStatusID {
			set { productionStatusID = value; }
			get { return productionStatusID; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
