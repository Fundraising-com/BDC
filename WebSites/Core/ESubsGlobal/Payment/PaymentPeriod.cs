using System;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal.Payment {

	public class PaymentPeriod: DataObject,IComparable{

		private int paymentPeriodId;
		private DateTime startDate;
		private DateTime endDate;
		private DateTime createDate;


		public PaymentPeriod() : this(int.MinValue) { }
		public PaymentPeriod(int paymentPeriodId) : this(paymentPeriodId, DateTime.MinValue) { }
		public PaymentPeriod(int paymentPeriodId, DateTime startDate) : this(paymentPeriodId, startDate, DateTime.MinValue) { }
		public PaymentPeriod(int paymentPeriodId, DateTime startDate, DateTime endDate) : this(paymentPeriodId, startDate, endDate, DateTime.MinValue) { }
		public PaymentPeriod(int paymentPeriodId, DateTime startDate, DateTime endDate, DateTime createDate) {
			this.paymentPeriodId = paymentPeriodId;
			this.startDate = startDate;
			this.endDate = endDate;
			this.createDate = createDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PaymentPeriod>\r\n" +
			"	<PaymentPeriodId>" + paymentPeriodId + "</PaymentPeriodId>\r\n" +
			"	<StartDate>" + startDate + "</StartDate>\r\n" +
			"	<EndDate>" + endDate + "</EndDate>\r\n" +
			"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
			"</PaymentPeriod>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "paymentPeriodId") {
					SetXmlValue(ref paymentPeriodId, node.InnerText);
				}
				if(node.Name.ToLower() == "startDate") {
					SetXmlValue(ref startDate, node.InnerText);
				}
				if(node.Name.ToLower() == "endDate") {
					SetXmlValue(ref endDate, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") {
					SetXmlValue(ref createDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PaymentPeriod[] GetPaymentPeriods() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentPeriods();
		}

		public static PaymentPeriod[] GetPaymentPeriodsSortByDateDescending() 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentPeriods(string.Empty, "Start_date desc");
		}

		public static PaymentPeriod GetPaymentPeriodByID(int id) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentPeriodByID(id);
		}

		
		public static PaymentPeriod GetLatestPaymentPeriod() 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetLatestPaymentPeriod();
		}
        
        public static PaymentPeriod[] GetLatestPaymentPeriods()
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetLatestPaymentPeriods(string.Empty, "Start_date desc");
        }
        
        

		public int Insert() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.InsertPaymentPeriod(this);
		}

        public int InsertNext()
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.InsertNextPaymentPeriod();
        }

		public int Update() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.UpdatePaymentPeriod(this);
		}
		#endregion

		#region Properties
		public int PaymentPeriodId {
			set { paymentPeriodId = value; }
			get { return paymentPeriodId; }
		}

		public DateTime StartDate {
			set { startDate = value; }
			get { return startDate; }
		}

		public DateTime EndDate {
			set { endDate = value; }
			get { return endDate; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion

		#region IComparable Members

		public int CompareTo(object obj)
		{
			return 0;
		}

		#endregion
	}
}
