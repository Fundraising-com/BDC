using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class QuestionParamTarget: eFundraisingStoreDataObject {

		private int questionId;
		private int webFormId;
		private string parameterTarget;


		public QuestionParamTarget() : this(int.MinValue) { }
		public QuestionParamTarget(int questionId) : this(questionId, int.MinValue) { }
		public QuestionParamTarget(int questionId, int webFormId) : this(questionId, webFormId, null) { }
		public QuestionParamTarget(int questionId, int webFormId, string parameterTarget) {
			this.questionId = questionId;
			this.webFormId = webFormId;
			this.parameterTarget = parameterTarget;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<QuestionParamTarget>\r\n" +
			"	<QuestionId>" + questionId + "</QuestionId>\r\n" +
			"	<WebFormId>" + webFormId + "</WebFormId>\r\n" +
			"	<ParameterTarget>" + System.Web.HttpUtility.HtmlEncode(parameterTarget) + "</ParameterTarget>\r\n" +
			"</QuestionParamTarget>\r\n";
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
				if(node.Name.ToLower() == "parameterTarget") {
					SetXmlValue(ref parameterTarget, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static QuestionParamTarget[] GetQuestionParamTargets() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetQuestionParamTargets();
		}

		public static QuestionParamTarget GetQuestionParamTargetByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetQuestionParamTargetByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertQuestionParamTarget(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateQuestionParamTarget(this);
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

		public string ParameterTarget {
			set { parameterTarget = value; }
			get { return parameterTarget; }
		}

		#endregion
	}
}
