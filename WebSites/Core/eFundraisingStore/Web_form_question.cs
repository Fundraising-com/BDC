using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class WebFormQuestion: eFundraisingStoreDataObject {

		private int questionId;
		private int webFormId;
		private short required;
		private int questionOrder;
		private DateTime datestamp;


		public WebFormQuestion() : this(int.MinValue) { }
		public WebFormQuestion(int questionId) : this(questionId, int.MinValue) { }
		public WebFormQuestion(int questionId, int webFormId) : this(questionId, webFormId, short.MinValue) { }
		public WebFormQuestion(int questionId, int webFormId, short required) : this(questionId, webFormId, required, int.MinValue) { }
		public WebFormQuestion(int questionId, int webFormId, short required, int questionOrder) : this(questionId, webFormId, required, questionOrder, DateTime.MinValue) { }
		public WebFormQuestion(int questionId, int webFormId, short required, int questionOrder, DateTime datestamp) {
			this.questionId = questionId;
			this.webFormId = webFormId;
			this.required = required;
			this.questionOrder = questionOrder;
			this.datestamp = datestamp;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<WebFormQuestion>\r\n" +
			"	<QuestionId>" + questionId + "</QuestionId>\r\n" +
			"	<WebFormId>" + webFormId + "</WebFormId>\r\n" +
			"	<Required>" + required + "</Required>\r\n" +
			"	<QuestionOrder>" + questionOrder + "</QuestionOrder>\r\n" +
			"	<Datestamp>" + datestamp + "</Datestamp>\r\n" +
			"</WebFormQuestion>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "questionId") {
					SetXmlValue(ref questionId, node.InnerText);
				}
				if(node.Name.ToLower() == "webFormId") {
					SetXmlValue(ref webFormId, node.InnerText);
				}
				if(node.Name.ToLower() == "required") {
					SetXmlValue(ref required, node.InnerText);
				}
				if(node.Name.ToLower() == "questionOrder") {
					SetXmlValue(ref questionOrder, node.InnerText);
				}
				if(node.Name.ToLower() == "datestamp") {
					SetXmlValue(ref datestamp, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static WebFormQuestion[] GetWebFormQuestions() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetWebFormQuestions();
		}

		public static WebFormQuestion GetWebFormQuestionByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetWebFormQuestionByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertWebFormQuestion(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateWebFormQuestion(this);
		}
		#endregion

		#region Properties
		public int QuestionId {
			set { questionId = value; }
			get { return questionId; }
		}

		public int WebFormId {
			set { webFormId = value; }
			get { return webFormId; }
		}

		public short Required {
			set { required = value; }
			get { return required; }
		}

		public int QuestionOrder {
			set { questionOrder = value; }
			get { return questionOrder; }
		}

		public DateTime Datestamp {
			set { datestamp = value; }
			get { return datestamp; }
		}

		#endregion
	}
}
