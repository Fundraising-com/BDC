using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class QuestionsEntryForm: eFundraisingStoreDataObject {

		private int questionId;
		private int webFormId;
		private short required;
		private int questionOrder;
		private string insertTable;
		private string insertColumn;
		private string defaultValue;


		public QuestionsEntryForm() : this(int.MinValue) { }
		public QuestionsEntryForm(int questionId) : this(questionId, int.MinValue) { }
		public QuestionsEntryForm(int questionId, int webFormId) : this(questionId, webFormId, short.MinValue) { }
		public QuestionsEntryForm(int questionId, int webFormId, short required) : this(questionId, webFormId, required, int.MinValue) { }
		public QuestionsEntryForm(int questionId, int webFormId, short required, int questionOrder) : this(questionId, webFormId, required, questionOrder, null) { }
		public QuestionsEntryForm(int questionId, int webFormId, short required, int questionOrder, string insertTable) : this(questionId, webFormId, required, questionOrder, insertTable, null) { }
		public QuestionsEntryForm(int questionId, int webFormId, short required, int questionOrder, string insertTable, string insertColumn) : this(questionId, webFormId, required, questionOrder, insertTable, insertColumn, null) { }
		public QuestionsEntryForm(int questionId, int webFormId, short required, int questionOrder, string insertTable, string insertColumn, string defaultValue) {
			this.questionId = questionId;
			this.webFormId = webFormId;
			this.required = required;
			this.questionOrder = questionOrder;
			this.insertTable = insertTable;
			this.insertColumn = insertColumn;
			this.defaultValue = defaultValue;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<QuestionsEntryForm>\r\n" +
			"	<QuestionId>" + questionId + "</QuestionId>\r\n" +
			"	<WebFormId>" + webFormId + "</WebFormId>\r\n" +
			"	<Required>" + required + "</Required>\r\n" +
			"	<QuestionOrder>" + questionOrder + "</QuestionOrder>\r\n" +
			"	<InsertTable>" + System.Web.HttpUtility.HtmlEncode(insertTable) + "</InsertTable>\r\n" +
			"	<InsertColumn>" + System.Web.HttpUtility.HtmlEncode(insertColumn) + "</InsertColumn>\r\n" +
			"	<DefaultValue>" + System.Web.HttpUtility.HtmlEncode(defaultValue) + "</DefaultValue>\r\n" +
			"</QuestionsEntryForm>\r\n";
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
				if(node.Name.ToLower() == "insertTable") {
					SetXmlValue(ref insertTable, node.InnerText);
				}
				if(node.Name.ToLower() == "insertColumn") {
					SetXmlValue(ref insertColumn, node.InnerText);
				}
				if(node.Name.ToLower() == "defaultValue") {
					SetXmlValue(ref defaultValue, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static QuestionsEntryForm[] GetQuestionsEntryForms() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetQuestionsEntryForms();
		}

		public static QuestionsEntryForm GetQuestionsEntryFormByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetQuestionsEntryFormByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertQuestionsEntryForm(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateQuestionsEntryForm(this);
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

		public string InsertTable {
			set { insertTable = value; }
			get { return insertTable; }
		}

		public string InsertColumn {
			set { insertColumn = value; }
			get { return insertColumn; }
		}

		public string DefaultValue {
			set { defaultValue = value; }
			get { return defaultValue; }
		}

		#endregion
	}
}
