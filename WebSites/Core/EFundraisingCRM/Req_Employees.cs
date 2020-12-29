using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class ReqEmployees: EFundraisingCRMDataObject {

		private int employeeId;
		private string employeeName;
		private int isMIS;
		private string password;
		private string email;
		private int isManager;
		private int isActive;


		public ReqEmployees() : this(int.MinValue) { }
		public ReqEmployees(int employeeId) : this(employeeId, null) { }
		public ReqEmployees(int employeeId, string employeeName) : this(employeeId, employeeName, int.MinValue) { }
		public ReqEmployees(int employeeId, string employeeName, int isMIS) : this(employeeId, employeeName, isMIS, null) { }
		public ReqEmployees(int employeeId, string employeeName, int isMIS, string password) : this(employeeId, employeeName, isMIS, password, null) { }
		public ReqEmployees(int employeeId, string employeeName, int isMIS, string password, string email) : this(employeeId, employeeName, isMIS, password, email, int.MinValue) { }
		public ReqEmployees(int employeeId, string employeeName, int isMIS, string password, string email, int isManager) : this(employeeId, employeeName, isMIS, password, email, isManager, int.MinValue) { }
		public ReqEmployees(int employeeId, string employeeName, int isMIS, string password, string email, int isManager, int isActive) {
			this.employeeId = employeeId;
			this.employeeName = employeeName;
			this.isMIS = isMIS;
			this.password = password;
			this.email = email;
			this.isManager = isManager;
			this.isActive = isActive;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ReqEmployees>\r\n" +
			"	<EmployeeId>" + employeeId + "</EmployeeId>\r\n" +
			"	<EmployeeName>" + System.Web.HttpUtility.HtmlEncode(employeeName) + "</EmployeeName>\r\n" +
			"	<IsMIS>" + isMIS + "</IsMIS>\r\n" +
			"	<Password>" + System.Web.HttpUtility.HtmlEncode(password) + "</Password>\r\n" +
			"	<Email>" + System.Web.HttpUtility.HtmlEncode(email) + "</Email>\r\n" +
			"	<IsManager>" + isManager + "</IsManager>\r\n" +
			"	<IsActive>" + isActive + "</IsActive>\r\n" +
			"</ReqEmployees>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("employeeId")) {
					SetXmlValue(ref employeeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("employeeName")) {
					SetXmlValue(ref employeeName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isMis")) {
					SetXmlValue(ref isMIS, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("password")) {
					SetXmlValue(ref password, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("email")) {
					SetXmlValue(ref email, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isManager")) {
					SetXmlValue(ref isManager, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isActive")) {
					SetXmlValue(ref isActive, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ReqEmployees[] GetReqEmployeess() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetReqEmployeess();
		}

		public static ReqEmployees GetReqEmployeesByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetReqEmployeesByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertReqEmployees(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateReqEmployees(this);
		}
		#endregion

		#region Properties
		public int EmployeeId {
			set { employeeId = value; }
			get { return employeeId; }
		}

		public string EmployeeName {
			set { employeeName = value; }
			get { return employeeName; }
		}

		public int IsMIS {
			set { isMIS = value; }
			get { return isMIS; }
		}

		public string Password {
			set { password = value; }
			get { return password; }
		}

		public string Email {
			set { email = value; }
			get { return email; }
		}

		public int IsManager {
			set { isManager = value; }
			get { return isManager; }
		}

		public int IsActive {
			set { isActive = value; }
			get { return isActive; }
		}

		#endregion
	}
}
