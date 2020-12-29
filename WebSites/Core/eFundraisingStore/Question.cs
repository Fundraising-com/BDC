using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class Question: eFundraisingStoreDataObject {

		private int questionId;
		private string name;
		private string description;
		private int controlTypeId;
		private string fieldName;
		private string defaultValue;
		private int minLenght;
		private int maxLenght;
		private int nbrValue;
		private DateTime datestamp;
		private string storedProcToCall;
		private string answerType;
		private string fieldValue;


		public Question() : this(int.MinValue) { }
		public Question(int questionId) : this(questionId, null) { }
		public Question(int questionId, string name) : this(questionId, name, null) { }
		public Question(int questionId, string name, string description) : this(questionId, name, description, int.MinValue) { }
		public Question(int questionId, string name, string description, int controlTypeId) : this(questionId, name, description, controlTypeId, null) { }
		public Question(int questionId, string name, string description, int controlTypeId, string fieldName) : this(questionId, name, description, controlTypeId, fieldName, null) { }
		public Question(int questionId, string name, string description, int controlTypeId, string fieldName, string defaultValue) : this(questionId, name, description, controlTypeId, fieldName, defaultValue, int.MinValue) { }
		public Question(int questionId, string name, string description, int controlTypeId, string fieldName, string defaultValue, int minLenght) : this(questionId, name, description, controlTypeId, fieldName, defaultValue, minLenght, int.MinValue) { }
		public Question(int questionId, string name, string description, int controlTypeId, string fieldName, string defaultValue, int minLenght, int maxLenght) : this(questionId, name, description, controlTypeId, fieldName, defaultValue, minLenght, maxLenght, int.MinValue) { }
		public Question(int questionId, string name, string description, int controlTypeId, string fieldName, string defaultValue, int minLenght, int maxLenght, int nbrValue) : this(questionId, name, description, controlTypeId, fieldName, defaultValue, minLenght, maxLenght, nbrValue, DateTime.MinValue) { }
		public Question(int questionId, string name, string description, int controlTypeId, string fieldName, string defaultValue, int minLenght, int maxLenght, int nbrValue, DateTime datestamp) : this(questionId, name, description, controlTypeId, fieldName, defaultValue, minLenght, maxLenght, nbrValue, datestamp, null) { }
		public Question(int questionId, string name, string description, int controlTypeId, string fieldName, string defaultValue, int minLenght, int maxLenght, int nbrValue, DateTime datestamp, string storedProcToCall) : this(questionId, name, description, controlTypeId, fieldName, defaultValue, minLenght, maxLenght, nbrValue, datestamp, storedProcToCall, null) { }
		public Question(int questionId, string name, string description, int controlTypeId, string fieldName, string defaultValue, int minLenght, int maxLenght, int nbrValue, DateTime datestamp, string storedProcToCall, string answerType) : this(questionId, name, description, controlTypeId, fieldName, defaultValue, minLenght, maxLenght, nbrValue, datestamp, storedProcToCall, answerType, null) { }
		public Question(int questionId, string name, string description, int controlTypeId, string fieldName, string defaultValue, int minLenght, int maxLenght, int nbrValue, DateTime datestamp, string storedProcToCall, string answerType, string fieldValue) {
			this.questionId = questionId;
			this.name = name;
			this.description = description;
			this.controlTypeId = controlTypeId;
			this.fieldName = fieldName;
			this.defaultValue = defaultValue;
			this.minLenght = minLenght;
			this.maxLenght = maxLenght;
			this.nbrValue = nbrValue;
			this.datestamp = datestamp;
			this.storedProcToCall = storedProcToCall;
			this.answerType = answerType;
			this.fieldValue = fieldValue;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Question>\r\n" +
			"	<QuestionId>" + questionId + "</QuestionId>\r\n" +
			"	<Name>" + System.Web.HttpUtility.HtmlEncode(name) + "</Name>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"	<ControlTypeId>" + controlTypeId + "</ControlTypeId>\r\n" +
			"	<FieldName>" + System.Web.HttpUtility.HtmlEncode(fieldName) + "</FieldName>\r\n" +
			"	<DefaultValue>" + System.Web.HttpUtility.HtmlEncode(defaultValue) + "</DefaultValue>\r\n" +
			"	<MinLenght>" + minLenght + "</MinLenght>\r\n" +
			"	<MaxLenght>" + maxLenght + "</MaxLenght>\r\n" +
			"	<NbrValue>" + nbrValue + "</NbrValue>\r\n" +
			"	<Datestamp>" + datestamp + "</Datestamp>\r\n" +
			"	<StoredProcToCall>" + System.Web.HttpUtility.HtmlEncode(storedProcToCall) + "</StoredProcToCall>\r\n" +
			"	<AnswerType>" + System.Web.HttpUtility.HtmlEncode(answerType) + "</AnswerType>\r\n" +
			"	<FieldValue>" + System.Web.HttpUtility.HtmlEncode(fieldValue) + "</FieldValue>\r\n" +
			"</Question>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "questionId") {
					SetXmlValue(ref questionId, node.InnerText);
				}
				if(node.Name.ToLower() == "name") {
					SetXmlValue(ref name, node.InnerText);
				}
				if(node.Name.ToLower() == "description") {
					SetXmlValue(ref description, node.InnerText);
				}
				if(node.Name.ToLower() == "controlTypeId") {
					SetXmlValue(ref controlTypeId, node.InnerText);
				}
				if(node.Name.ToLower() == "fieldName") {
					SetXmlValue(ref fieldName, node.InnerText);
				}
				if(node.Name.ToLower() == "defaultValue") {
					SetXmlValue(ref defaultValue, node.InnerText);
				}
				if(node.Name.ToLower() == "minLenght") {
					SetXmlValue(ref minLenght, node.InnerText);
				}
				if(node.Name.ToLower() == "maxLenght") {
					SetXmlValue(ref maxLenght, node.InnerText);
				}
				if(node.Name.ToLower() == "nbrValue") {
					SetXmlValue(ref nbrValue, node.InnerText);
				}
				if(node.Name.ToLower() == "datestamp") {
					SetXmlValue(ref datestamp, node.InnerText);
				}
				if(node.Name.ToLower() == "storedProcToCall") {
					SetXmlValue(ref storedProcToCall, node.InnerText);
				}
				if(node.Name.ToLower() == "answerType") {
					SetXmlValue(ref answerType, node.InnerText);
				}
				if(node.Name.ToLower() == "fieldValue") {
					SetXmlValue(ref fieldValue, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Question[] GetQuestions() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetQuestions();
		}

		public static Question GetQuestionByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetQuestionByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertQuestion(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateQuestion(this);
		}
		#endregion

		#region Properties
		public int QuestionId {
			set { questionId = value; }
			get { return questionId; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public int ControlTypeId {
			set { controlTypeId = value; }
			get { return controlTypeId; }
		}

		public string FieldName {
			set { fieldName = value; }
			get { return fieldName; }
		}

		public string DefaultValue {
			set { defaultValue = value; }
			get { return defaultValue; }
		}

		public int MinLenght {
			set { minLenght = value; }
			get { return minLenght; }
		}

		public int MaxLenght {
			set { maxLenght = value; }
			get { return maxLenght; }
		}

		public int NbrValue {
			set { nbrValue = value; }
			get { return nbrValue; }
		}

		public DateTime Datestamp {
			set { datestamp = value; }
			get { return datestamp; }
		}

		public string StoredProcToCall {
			set { storedProcToCall = value; }
			get { return storedProcToCall; }
		}

		public string AnswerType {
			set { answerType = value; }
			get { return answerType; }
		}

		public string FieldValue {
			set { fieldValue = value; }
			get { return fieldValue; }
		}

		#endregion
	}
}
