using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class Conciliation: EFundraisingCRMDataObject {

		private int conciliationId;
		private int salesId;
		private int salesItemNo;
		private short supplierId;
		private DateTime conciliateDate;
		private int isConciliated;
		private int isOrdered;
		private string invoiceNumber;


		public Conciliation() : this(int.MinValue) { }
		public Conciliation(int conciliationId) : this(conciliationId, int.MinValue) { }
		public Conciliation(int conciliationId, int salesId) : this(conciliationId, salesId, int.MinValue) { }
		public Conciliation(int conciliationId, int salesId, int salesItemNo) : this(conciliationId, salesId, salesItemNo, short.MinValue) { }
		public Conciliation(int conciliationId, int salesId, int salesItemNo, short supplierId) : this(conciliationId, salesId, salesItemNo, supplierId, DateTime.MinValue) { }
		public Conciliation(int conciliationId, int salesId, int salesItemNo, short supplierId, DateTime conciliateDate) : this(conciliationId, salesId, salesItemNo, supplierId, conciliateDate, int.MinValue) { }
		public Conciliation(int conciliationId, int salesId, int salesItemNo, short supplierId, DateTime conciliateDate, int isConciliated) : this(conciliationId, salesId, salesItemNo, supplierId, conciliateDate, isConciliated, int.MinValue) { }
		public Conciliation(int conciliationId, int salesId, int salesItemNo, short supplierId, DateTime conciliateDate, int isConciliated, int isOrdered) : this(conciliationId, salesId, salesItemNo, supplierId, conciliateDate, isConciliated, isOrdered, null) { }
		public Conciliation(int conciliationId, int salesId, int salesItemNo, short supplierId, DateTime conciliateDate, int isConciliated, int isOrdered, string invoiceNumber) {
			this.conciliationId = conciliationId;
			this.salesId = salesId;
			this.salesItemNo = salesItemNo;
			this.supplierId = supplierId;
			this.conciliateDate = conciliateDate;
			this.isConciliated = isConciliated;
			this.isOrdered = isOrdered;
			this.invoiceNumber = invoiceNumber;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Conciliation>\r\n" +
			"	<ConciliationId>" + conciliationId + "</ConciliationId>\r\n" +
			"	<SalesId>" + salesId + "</SalesId>\r\n" +
			"	<SalesItemNo>" + salesItemNo + "</SalesItemNo>\r\n" +
			"	<SupplierId>" + supplierId + "</SupplierId>\r\n" +
			"	<ConciliateDate>" + conciliateDate + "</ConciliateDate>\r\n" +
			"	<IsConciliated>" + isConciliated + "</IsConciliated>\r\n" +
			"	<IsOrdered>" + isOrdered + "</IsOrdered>\r\n" +
			"	<InvoiceNumber>" + System.Web.HttpUtility.HtmlEncode(invoiceNumber) + "</InvoiceNumber>\r\n" +
			"</Conciliation>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("conciliationId")) {
					SetXmlValue(ref conciliationId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesId")) {
					SetXmlValue(ref salesId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesItemNo")) {
					SetXmlValue(ref salesItemNo, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("supplierId")) {
					SetXmlValue(ref supplierId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("conciliateDate")) {
					SetXmlValue(ref conciliateDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isConciliated")) {
					SetXmlValue(ref isConciliated, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isOrdered")) {
					SetXmlValue(ref isOrdered, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("invoiceNumber")) {
					SetXmlValue(ref invoiceNumber, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Conciliation[] GetConciliations() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetConciliations();
		}

		public static Conciliation GetConciliationByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetConciliationByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertConciliation(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateConciliation(this);
		}
		#endregion

		#region Properties
		public int ConciliationId {
			set { conciliationId = value; }
			get { return conciliationId; }
		}

		public int SalesId {
			set { salesId = value; }
			get { return salesId; }
		}

		public int SalesItemNo {
			set { salesItemNo = value; }
			get { return salesItemNo; }
		}

		public short SupplierId {
			set { supplierId = value; }
			get { return supplierId; }
		}

		public DateTime ConciliateDate {
			set { conciliateDate = value; }
			get { return conciliateDate; }
		}

		public int IsConciliated {
			set { isConciliated = value; }
			get { return isConciliated; }
		}

		public int IsOrdered {
			set { isOrdered = value; }
			get { return isOrdered; }
		}

		public string InvoiceNumber {
			set { invoiceNumber = value; }
			get { return invoiceNumber; }
		}

		#endregion
	}
}
