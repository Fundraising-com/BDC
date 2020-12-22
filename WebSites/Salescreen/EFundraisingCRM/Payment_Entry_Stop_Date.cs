using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class PaymentEntryStopDate: EFundraisingCRMDataObject {

		private DateTime paymentEntryStopDate;


		public PaymentEntryStopDate() : this(DateTime.MinValue) { }
		public PaymentEntryStopDate(DateTime paymentEntryStopDate) {
			this.paymentEntryStopDate = paymentEntryStopDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PaymentEntryStopDate>\r\n" +
			"	<PaymentEntryStopDate>" + paymentEntryStopDate + "</PaymentEntryStopDate>\r\n" +
			"</PaymentEntryStopDate>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("paymentEntryStopDate")) {
					SetXmlValue(ref paymentEntryStopDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		/*
		public static PaymentEntryStopDate[] GetPaymentEntryStopDates() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPaymentEntryStopDates();
		}

		public static PaymentEntryStopDate GetPaymentEntryStopDateByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPaymentEntryStopDateByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertPaymentEntryStopDate(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdatePaymentEntryStopDate(this);
		}*/
		#endregion

		#region Properties
		public DateTime PaymentEntryStopDateValue {
			set { paymentEntryStopDate = value; }
			get { return paymentEntryStopDate; }
		}

		#endregion
	}
}
