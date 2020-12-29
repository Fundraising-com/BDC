using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class ReqRequest: EFundraisingCRMDataObject {

		private int requestId;
		private int languageId;
		private int requestTypeID;
		private int projectTypeID;
		private int priorityId;
		private string projectName;
		private string summaryDescription;
		private DateTime requestDate;
		private DateTime decisionRequiredDate;
		private string impactDescription;
		private string misImpactDescription;
		private string decisionDescription;
		private int decisionId;
		private DateTime decisionDate;
		private DateTime projectSheduledStartDate;
		private DateTime projectSheduledEndDate;
		private DateTime projectStartDate;
		private DateTime projectEndDate;
		private int managerID;
		private int employeeId;
		private int mISID;


		public ReqRequest() : this(int.MinValue) { }
		public ReqRequest(int requestId) : this(requestId, int.MinValue) { }
		public ReqRequest(int requestId, int languageId) : this(requestId, languageId, int.MinValue) { }
		public ReqRequest(int requestId, int languageId, int requestTypeID) : this(requestId, languageId, requestTypeID, int.MinValue) { }
		public ReqRequest(int requestId, int languageId, int requestTypeID, int projectTypeID) : this(requestId, languageId, requestTypeID, projectTypeID, int.MinValue) { }
		public ReqRequest(int requestId, int languageId, int requestTypeID, int projectTypeID, int priorityId) : this(requestId, languageId, requestTypeID, projectTypeID, priorityId, null) { }
		public ReqRequest(int requestId, int languageId, int requestTypeID, int projectTypeID, int priorityId, string projectName) : this(requestId, languageId, requestTypeID, projectTypeID, priorityId, projectName, null) { }
		public ReqRequest(int requestId, int languageId, int requestTypeID, int projectTypeID, int priorityId, string projectName, string summaryDescription) : this(requestId, languageId, requestTypeID, projectTypeID, priorityId, projectName, summaryDescription, DateTime.MinValue) { }
		public ReqRequest(int requestId, int languageId, int requestTypeID, int projectTypeID, int priorityId, string projectName, string summaryDescription, DateTime requestDate) : this(requestId, languageId, requestTypeID, projectTypeID, priorityId, projectName, summaryDescription, requestDate, DateTime.MinValue) { }
		public ReqRequest(int requestId, int languageId, int requestTypeID, int projectTypeID, int priorityId, string projectName, string summaryDescription, DateTime requestDate, DateTime decisionRequiredDate) : this(requestId, languageId, requestTypeID, projectTypeID, priorityId, projectName, summaryDescription, requestDate, decisionRequiredDate, null) { }
		public ReqRequest(int requestId, int languageId, int requestTypeID, int projectTypeID, int priorityId, string projectName, string summaryDescription, DateTime requestDate, DateTime decisionRequiredDate, string impactDescription) : this(requestId, languageId, requestTypeID, projectTypeID, priorityId, projectName, summaryDescription, requestDate, decisionRequiredDate, impactDescription, null) { }
		public ReqRequest(int requestId, int languageId, int requestTypeID, int projectTypeID, int priorityId, string projectName, string summaryDescription, DateTime requestDate, DateTime decisionRequiredDate, string impactDescription, string misImpactDescription) : this(requestId, languageId, requestTypeID, projectTypeID, priorityId, projectName, summaryDescription, requestDate, decisionRequiredDate, impactDescription, misImpactDescription, null) { }
		public ReqRequest(int requestId, int languageId, int requestTypeID, int projectTypeID, int priorityId, string projectName, string summaryDescription, DateTime requestDate, DateTime decisionRequiredDate, string impactDescription, string misImpactDescription, string decisionDescription) : this(requestId, languageId, requestTypeID, projectTypeID, priorityId, projectName, summaryDescription, requestDate, decisionRequiredDate, impactDescription, misImpactDescription, decisionDescription, int.MinValue) { }
		public ReqRequest(int requestId, int languageId, int requestTypeID, int projectTypeID, int priorityId, string projectName, string summaryDescription, DateTime requestDate, DateTime decisionRequiredDate, string impactDescription, string misImpactDescription, string decisionDescription, int decisionId) : this(requestId, languageId, requestTypeID, projectTypeID, priorityId, projectName, summaryDescription, requestDate, decisionRequiredDate, impactDescription, misImpactDescription, decisionDescription, decisionId, DateTime.MinValue) { }
		public ReqRequest(int requestId, int languageId, int requestTypeID, int projectTypeID, int priorityId, string projectName, string summaryDescription, DateTime requestDate, DateTime decisionRequiredDate, string impactDescription, string misImpactDescription, string decisionDescription, int decisionId, DateTime decisionDate) : this(requestId, languageId, requestTypeID, projectTypeID, priorityId, projectName, summaryDescription, requestDate, decisionRequiredDate, impactDescription, misImpactDescription, decisionDescription, decisionId, decisionDate, DateTime.MinValue) { }
		public ReqRequest(int requestId, int languageId, int requestTypeID, int projectTypeID, int priorityId, string projectName, string summaryDescription, DateTime requestDate, DateTime decisionRequiredDate, string impactDescription, string misImpactDescription, string decisionDescription, int decisionId, DateTime decisionDate, DateTime projectSheduledStartDate) : this(requestId, languageId, requestTypeID, projectTypeID, priorityId, projectName, summaryDescription, requestDate, decisionRequiredDate, impactDescription, misImpactDescription, decisionDescription, decisionId, decisionDate, projectSheduledStartDate, DateTime.MinValue) { }
		public ReqRequest(int requestId, int languageId, int requestTypeID, int projectTypeID, int priorityId, string projectName, string summaryDescription, DateTime requestDate, DateTime decisionRequiredDate, string impactDescription, string misImpactDescription, string decisionDescription, int decisionId, DateTime decisionDate, DateTime projectSheduledStartDate, DateTime projectSheduledEndDate) : this(requestId, languageId, requestTypeID, projectTypeID, priorityId, projectName, summaryDescription, requestDate, decisionRequiredDate, impactDescription, misImpactDescription, decisionDescription, decisionId, decisionDate, projectSheduledStartDate, projectSheduledEndDate, DateTime.MinValue) { }
		public ReqRequest(int requestId, int languageId, int requestTypeID, int projectTypeID, int priorityId, string projectName, string summaryDescription, DateTime requestDate, DateTime decisionRequiredDate, string impactDescription, string misImpactDescription, string decisionDescription, int decisionId, DateTime decisionDate, DateTime projectSheduledStartDate, DateTime projectSheduledEndDate, DateTime projectStartDate) : this(requestId, languageId, requestTypeID, projectTypeID, priorityId, projectName, summaryDescription, requestDate, decisionRequiredDate, impactDescription, misImpactDescription, decisionDescription, decisionId, decisionDate, projectSheduledStartDate, projectSheduledEndDate, projectStartDate, DateTime.MinValue) { }
		public ReqRequest(int requestId, int languageId, int requestTypeID, int projectTypeID, int priorityId, string projectName, string summaryDescription, DateTime requestDate, DateTime decisionRequiredDate, string impactDescription, string misImpactDescription, string decisionDescription, int decisionId, DateTime decisionDate, DateTime projectSheduledStartDate, DateTime projectSheduledEndDate, DateTime projectStartDate, DateTime projectEndDate) : this(requestId, languageId, requestTypeID, projectTypeID, priorityId, projectName, summaryDescription, requestDate, decisionRequiredDate, impactDescription, misImpactDescription, decisionDescription, decisionId, decisionDate, projectSheduledStartDate, projectSheduledEndDate, projectStartDate, projectEndDate, int.MinValue) { }
		public ReqRequest(int requestId, int languageId, int requestTypeID, int projectTypeID, int priorityId, string projectName, string summaryDescription, DateTime requestDate, DateTime decisionRequiredDate, string impactDescription, string misImpactDescription, string decisionDescription, int decisionId, DateTime decisionDate, DateTime projectSheduledStartDate, DateTime projectSheduledEndDate, DateTime projectStartDate, DateTime projectEndDate, int managerID) : this(requestId, languageId, requestTypeID, projectTypeID, priorityId, projectName, summaryDescription, requestDate, decisionRequiredDate, impactDescription, misImpactDescription, decisionDescription, decisionId, decisionDate, projectSheduledStartDate, projectSheduledEndDate, projectStartDate, projectEndDate, managerID, int.MinValue) { }
		public ReqRequest(int requestId, int languageId, int requestTypeID, int projectTypeID, int priorityId, string projectName, string summaryDescription, DateTime requestDate, DateTime decisionRequiredDate, string impactDescription, string misImpactDescription, string decisionDescription, int decisionId, DateTime decisionDate, DateTime projectSheduledStartDate, DateTime projectSheduledEndDate, DateTime projectStartDate, DateTime projectEndDate, int managerID, int employeeId) : this(requestId, languageId, requestTypeID, projectTypeID, priorityId, projectName, summaryDescription, requestDate, decisionRequiredDate, impactDescription, misImpactDescription, decisionDescription, decisionId, decisionDate, projectSheduledStartDate, projectSheduledEndDate, projectStartDate, projectEndDate, managerID, employeeId, int.MinValue) { }
		public ReqRequest(int requestId, int languageId, int requestTypeID, int projectTypeID, int priorityId, string projectName, string summaryDescription, DateTime requestDate, DateTime decisionRequiredDate, string impactDescription, string misImpactDescription, string decisionDescription, int decisionId, DateTime decisionDate, DateTime projectSheduledStartDate, DateTime projectSheduledEndDate, DateTime projectStartDate, DateTime projectEndDate, int managerID, int employeeId, int mISID) {
			this.requestId = requestId;
			this.languageId = languageId;
			this.requestTypeID = requestTypeID;
			this.projectTypeID = projectTypeID;
			this.priorityId = priorityId;
			this.projectName = projectName;
			this.summaryDescription = summaryDescription;
			this.requestDate = requestDate;
			this.decisionRequiredDate = decisionRequiredDate;
			this.impactDescription = impactDescription;
			this.misImpactDescription = misImpactDescription;
			this.decisionDescription = decisionDescription;
			this.decisionId = decisionId;
			this.decisionDate = decisionDate;
			this.projectSheduledStartDate = projectSheduledStartDate;
			this.projectSheduledEndDate = projectSheduledEndDate;
			this.projectStartDate = projectStartDate;
			this.projectEndDate = projectEndDate;
			this.managerID = managerID;
			this.employeeId = employeeId;
			this.mISID = mISID;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ReqRequest>\r\n" +
			"	<RequestId>" + requestId + "</RequestId>\r\n" +
			"	<LanguageId>" + languageId + "</LanguageId>\r\n" +
			"	<RequestTypeID>" + requestTypeID + "</RequestTypeID>\r\n" +
			"	<ProjectTypeID>" + projectTypeID + "</ProjectTypeID>\r\n" +
			"	<PriorityId>" + priorityId + "</PriorityId>\r\n" +
			"	<ProjectName>" + System.Web.HttpUtility.HtmlEncode(projectName) + "</ProjectName>\r\n" +
			"	<SummaryDescription>" + System.Web.HttpUtility.HtmlEncode(summaryDescription) + "</SummaryDescription>\r\n" +
			"	<RequestDate>" + requestDate + "</RequestDate>\r\n" +
			"	<DecisionRequiredDate>" + decisionRequiredDate + "</DecisionRequiredDate>\r\n" +
			"	<ImpactDescription>" + System.Web.HttpUtility.HtmlEncode(impactDescription) + "</ImpactDescription>\r\n" +
			"	<MisImpactDescription>" + System.Web.HttpUtility.HtmlEncode(misImpactDescription) + "</MisImpactDescription>\r\n" +
			"	<DecisionDescription>" + System.Web.HttpUtility.HtmlEncode(decisionDescription) + "</DecisionDescription>\r\n" +
			"	<DecisionId>" + decisionId + "</DecisionId>\r\n" +
			"	<DecisionDate>" + decisionDate + "</DecisionDate>\r\n" +
			"	<ProjectSheduledStartDate>" + projectSheduledStartDate + "</ProjectSheduledStartDate>\r\n" +
			"	<ProjectSheduledEndDate>" + projectSheduledEndDate + "</ProjectSheduledEndDate>\r\n" +
			"	<ProjectStartDate>" + projectStartDate + "</ProjectStartDate>\r\n" +
			"	<ProjectEndDate>" + projectEndDate + "</ProjectEndDate>\r\n" +
			"	<ManagerID>" + managerID + "</ManagerID>\r\n" +
			"	<EmployeeId>" + employeeId + "</EmployeeId>\r\n" +
			"	<MISID>" + mISID + "</MISID>\r\n" +
			"</ReqRequest>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("requestId")) {
					SetXmlValue(ref requestId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("languageId")) {
					SetXmlValue(ref languageId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("requestTypeId")) {
					SetXmlValue(ref requestTypeID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("projectTypeId")) {
					SetXmlValue(ref projectTypeID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("priorityId")) {
					SetXmlValue(ref priorityId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("projectName")) {
					SetXmlValue(ref projectName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("summaryDescription")) {
					SetXmlValue(ref summaryDescription, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("requestDate")) {
					SetXmlValue(ref requestDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("decisionRequiredDate")) {
					SetXmlValue(ref decisionRequiredDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("impactDescription")) {
					SetXmlValue(ref impactDescription, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("misImpactDescription")) {
					SetXmlValue(ref misImpactDescription, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("decisionDescription")) {
					SetXmlValue(ref decisionDescription, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("decisionId")) {
					SetXmlValue(ref decisionId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("decisionDate")) {
					SetXmlValue(ref decisionDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("projectSheduledStartDate")) {
					SetXmlValue(ref projectSheduledStartDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("projectSheduledEndDate")) {
					SetXmlValue(ref projectSheduledEndDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("projectStartDate")) {
					SetXmlValue(ref projectStartDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("projectEndDate")) {
					SetXmlValue(ref projectEndDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("managerId")) {
					SetXmlValue(ref managerID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("employeeId")) {
					SetXmlValue(ref employeeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("misId")) {
					SetXmlValue(ref mISID, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ReqRequest[] GetReqRequests() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetReqRequests();
		}

		public static ReqRequest GetReqRequestByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetReqRequestByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertReqRequest(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateReqRequest(this);
		}
		#endregion

		#region Properties
		public int RequestId {
			set { requestId = value; }
			get { return requestId; }
		}

		public int LanguageId {
			set { languageId = value; }
			get { return languageId; }
		}

		public int RequestTypeID {
			set { requestTypeID = value; }
			get { return requestTypeID; }
		}

		public int ProjectTypeID {
			set { projectTypeID = value; }
			get { return projectTypeID; }
		}

		public int PriorityId {
			set { priorityId = value; }
			get { return priorityId; }
		}

		public string ProjectName {
			set { projectName = value; }
			get { return projectName; }
		}

		public string SummaryDescription {
			set { summaryDescription = value; }
			get { return summaryDescription; }
		}

		public DateTime RequestDate {
			set { requestDate = value; }
			get { return requestDate; }
		}

		public DateTime DecisionRequiredDate {
			set { decisionRequiredDate = value; }
			get { return decisionRequiredDate; }
		}

		public string ImpactDescription {
			set { impactDescription = value; }
			get { return impactDescription; }
		}

		public string MisImpactDescription {
			set { misImpactDescription = value; }
			get { return misImpactDescription; }
		}

		public string DecisionDescription {
			set { decisionDescription = value; }
			get { return decisionDescription; }
		}

		public int DecisionId {
			set { decisionId = value; }
			get { return decisionId; }
		}

		public DateTime DecisionDate {
			set { decisionDate = value; }
			get { return decisionDate; }
		}

		public DateTime ProjectSheduledStartDate {
			set { projectSheduledStartDate = value; }
			get { return projectSheduledStartDate; }
		}

		public DateTime ProjectSheduledEndDate {
			set { projectSheduledEndDate = value; }
			get { return projectSheduledEndDate; }
		}

		public DateTime ProjectStartDate {
			set { projectStartDate = value; }
			get { return projectStartDate; }
		}

		public DateTime ProjectEndDate {
			set { projectEndDate = value; }
			get { return projectEndDate; }
		}

		public int ManagerID {
			set { managerID = value; }
			get { return managerID; }
		}

		public int EmployeeId {
			set { employeeId = value; }
			get { return employeeId; }
		}

		public int MISID {
			set { mISID = value; }
			get { return mISID; }
		}

		#endregion
	}
}
