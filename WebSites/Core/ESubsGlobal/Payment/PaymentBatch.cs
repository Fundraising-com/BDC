using System;
using System.Xml;
using System.Collections.Generic;

namespace GA.BDC.Core.ESubsGlobal.Payment {

	public class PaymentBatch {

		private int _paymentBatchID;
		private string _fileName;
		private DateTime _createDate;
		private DateTime _confirmationDate;
		private DateTime _cancelledDate;


		public PaymentBatch() : this(int.MinValue) { }
		public PaymentBatch(int paymentBatchID) : this(paymentBatchID, null) { }
		public PaymentBatch(int paymentBatchID, string fileName) : this(paymentBatchID, fileName, DateTime.MinValue) { }
		public PaymentBatch(int paymentBatchID, string fileName, DateTime createDate) : this(paymentBatchID, fileName, createDate, DateTime.MinValue) { }
		public PaymentBatch(int paymentBatchID, string fileName, DateTime createDate, DateTime confirmationDate) : this(paymentBatchID, fileName, createDate, confirmationDate, DateTime.MinValue) { }
		public PaymentBatch(int paymentBatchID, string fileName, DateTime createDate, DateTime confirmationDate, DateTime cancelledDate) {
			_paymentBatchID = paymentBatchID;
			_fileName = fileName;
			_createDate = createDate;
			_confirmationDate = confirmationDate;
			_cancelledDate = cancelledDate;
        }

        #region DAL methods

        public static PaymentBatch GetPaymentBatchByID(int id)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetPaymentBatchByID(id);
        }

        public static List<PaymentBatch> GetPaymentBatches()
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetPaymentBatch();
        }

        public static int UpdatePaymentBatch(PaymentBatch batch)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.UpdatePaymentBatch(batch);
        }


        public static int InsertPaymentBatch(PaymentBatch batch)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.InsertPaymentBatch(batch);
        }

        #endregion

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
			return "<PaymentBatch>\r\n" +
			"	<PaymentBatchID>" + _paymentBatchID + "</PaymentBatchID>\r\n" +
			"	<FileName>" + _fileName + "</FileName>\r\n" +
			"	<CreateDate>" + _createDate + "</CreateDate>\r\n" +
			"	<ConfirmationDate>" + _confirmationDate + "</ConfirmationDate>\r\n" +
			"	<CancelledDate>" + _cancelledDate + "</CancelledDate>\r\n" +
			"</PaymentBatch>\r\n";
		}
		#endregion

		#region Set Xml Values
		private void SetXmlValue(ref int obj, string val) {
			if(val == "") { obj = int.MinValue; return; }
			obj = int.Parse(val);
		}

		private void SetXmlValue(ref string obj, string val) {
			if(val == "") { obj = null; return; }
			obj = val;
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
				if(node.Name.ToLower() == "paymentbatchid") {
					SetXmlValue(ref _paymentBatchID, node.InnerText);
				}
				if(node.Name.ToLower() == "filename") {
					SetXmlValue(ref _fileName, node.InnerText);
				}
				if(node.Name.ToLower() == "createdate") {
					SetXmlValue(ref _createDate, node.InnerText);
				}
				if(node.Name.ToLower() == "confirmationdate") {
					SetXmlValue(ref _confirmationDate, node.InnerText);
				}
				if(node.Name.ToLower() == "cancelleddate") {
					SetXmlValue(ref _cancelledDate, node.InnerText);
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

		#region Properties
		public int PaymentBatchID {
			set { _paymentBatchID = value; }
			get { return _paymentBatchID; }
		}

		public string FileName {
			set { _fileName = value; }
			get { return _fileName; }
		}

		public DateTime CreateDate {
			set { _createDate = value; }
			get { return _createDate; }
		}

		public DateTime ConfirmationDate {
			set { _confirmationDate = value; }
			get { return _confirmationDate; }
		}

		public DateTime CancelledDate {
			set { _cancelledDate = value; }
			get { return _cancelledDate; }
		}

		#endregion
	}
}