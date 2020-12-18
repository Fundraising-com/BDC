using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class JavaErrors: EFundraisingCRMDataObject {

		private int errorID;
		private string className;
		private string errorMessage;
		private DateTime errorDate;


		public JavaErrors() : this(int.MinValue) { }
		public JavaErrors(int errorID) : this(errorID, null) { }
		public JavaErrors(int errorID, string className) : this(errorID, className, null) { }
		public JavaErrors(int errorID, string className, string errorMessage) : this(errorID, className, errorMessage, DateTime.MinValue) { }
		public JavaErrors(int errorID, string className, string errorMessage, DateTime errorDate) {
			this.errorID = errorID;
			this.className = className;
			this.errorMessage = errorMessage;
			this.errorDate = errorDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<JavaErrors>\r\n" +
			"	<ErrorID>" + errorID + "</ErrorID>\r\n" +
			"	<ClassName>" + System.Web.HttpUtility.HtmlEncode(className) + "</ClassName>\r\n" +
			"	<ErrorMessage>" + System.Web.HttpUtility.HtmlEncode(errorMessage) + "</ErrorMessage>\r\n" +
			"	<ErrorDate>" + errorDate + "</ErrorDate>\r\n" +
			"</JavaErrors>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("errorId")) {
					SetXmlValue(ref errorID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("className")) {
					SetXmlValue(ref className, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("errorMessage")) {
					SetXmlValue(ref errorMessage, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("errorDate")) {
					SetXmlValue(ref errorDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static JavaErrors[] GetJavaErrorss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetJavaErrorss();
		}

		public static JavaErrors GetJavaErrorsByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetJavaErrorsByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertJavaErrors(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateJavaErrors(this);
		}
		#endregion

		#region Properties
		public int ErrorID {
			set { errorID = value; }
			get { return errorID; }
		}

		public string ClassName {
			set { className = value; }
			get { return className; }
		}

		public string ErrorMessage {
			set { errorMessage = value; }
			get { return errorMessage; }
		}

		public DateTime ErrorDate {
			set { errorDate = value; }
			get { return errorDate; }
		}

		#endregion
	}
}
