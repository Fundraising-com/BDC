using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class QuestionEntryForm: EFundraisingCRMDataObject {

		private int entryFormID;
		private int questionID;
		private int isRequired;


		public QuestionEntryForm() : this(int.MinValue) { }
		public QuestionEntryForm(int entryFormID) : this(entryFormID, int.MinValue) { }
		public QuestionEntryForm(int entryFormID, int questionID) : this(entryFormID, questionID, int.MinValue) { }
		public QuestionEntryForm(int entryFormID, int questionID, int isRequired) {
			this.entryFormID = entryFormID;
			this.questionID = questionID;
			this.isRequired = isRequired;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<QuestionEntryForm>\r\n" +
			"	<EntryFormID>" + entryFormID + "</EntryFormID>\r\n" +
			"	<QuestionID>" + questionID + "</QuestionID>\r\n" +
			"	<IsRequired>" + isRequired + "</IsRequired>\r\n" +
			"</QuestionEntryForm>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("entryFormId")) {
					SetXmlValue(ref entryFormID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("questionId")) {
					SetXmlValue(ref questionID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isRequired")) {
					SetXmlValue(ref isRequired, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static QuestionEntryForm[] GetQuestionEntryForms() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetQuestionEntryForms();
		}

		public static QuestionEntryForm GetQuestionEntryFormByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetQuestionEntryFormByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertQuestionEntryForm(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateQuestionEntryForm(this);
		}
		#endregion

		#region Properties
		public int EntryFormID {
			set { entryFormID = value; }
			get { return entryFormID; }
		}

		public int QuestionID {
			set { questionID = value; }
			get { return questionID; }
		}

		public int IsRequired {
			set { isRequired = value; }
			get { return isRequired; }
		}

		#endregion
	}
}
