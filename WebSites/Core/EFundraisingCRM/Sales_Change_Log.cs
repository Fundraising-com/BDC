using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class SalesChangeLog: EFundraisingCRMDataObject {

		private int salesID;
		private string tableName;
		private string columnName;
		private DateTime changeDateTime;
		private string userName;
		private string fromValue;
		private string toValue;
		private string comment;
		private string computerName;
		private int cancelationReasonId;
		private string otherReason;


		public SalesChangeLog() : this(int.MinValue) { }
		public SalesChangeLog(int salesID) : this(salesID, null) { }
		public SalesChangeLog(int salesID, string tableName) : this(salesID, tableName, null) { }
		public SalesChangeLog(int salesID, string tableName, string columnName) : this(salesID, tableName, columnName, DateTime.MinValue) { }
		public SalesChangeLog(int salesID, string tableName, string columnName, DateTime changeDateTime) : this(salesID, tableName, columnName, changeDateTime, null) { }
		public SalesChangeLog(int salesID, string tableName, string columnName, DateTime changeDateTime, string userName) : this(salesID, tableName, columnName, changeDateTime, userName, null) { }
		public SalesChangeLog(int salesID, string tableName, string columnName, DateTime changeDateTime, string userName, string fromValue) : this(salesID, tableName, columnName, changeDateTime, userName, fromValue, null) { }
		public SalesChangeLog(int salesID, string tableName, string columnName, DateTime changeDateTime, string userName, string fromValue, string toValue) : this(salesID, tableName, columnName, changeDateTime, userName, fromValue, toValue, null) { }
		public SalesChangeLog(int salesID, string tableName, string columnName, DateTime changeDateTime, string userName, string fromValue, string toValue, string comment) : this(salesID, tableName, columnName, changeDateTime, userName, fromValue, toValue, comment, null) { }
		public SalesChangeLog(int salesID, string tableName, string columnName, DateTime changeDateTime, string userName, string fromValue, string toValue, string comment, string computerName) : this(salesID, tableName, columnName, changeDateTime, userName, fromValue, toValue, comment, computerName, int.MinValue) { }
		public SalesChangeLog(int salesID, string tableName, string columnName, DateTime changeDateTime, string userName, string fromValue, string toValue, string comment, string computerName, int cancelationReasonId) : this(salesID, tableName, columnName, changeDateTime, userName, fromValue, toValue, comment, computerName, cancelationReasonId, null) { }
		public SalesChangeLog(int salesID, string tableName, string columnName, DateTime changeDateTime, string userName, string fromValue, string toValue, string comment, string computerName, int cancelationReasonId, string otherReason) {
			this.salesID = salesID;
			this.tableName = tableName;
			this.columnName = columnName;
			this.changeDateTime = changeDateTime;
			this.userName = userName;
			this.fromValue = fromValue;
			this.toValue = toValue;
			this.comment = comment;
			this.computerName = computerName;
			this.cancelationReasonId = cancelationReasonId;
			this.otherReason = otherReason;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<SalesChangeLog>\r\n" +
			"	<SalesID>" + salesID + "</SalesID>\r\n" +
			"	<TableName>" + System.Web.HttpUtility.HtmlEncode(tableName) + "</TableName>\r\n" +
			"	<ColumnName>" + System.Web.HttpUtility.HtmlEncode(columnName) + "</ColumnName>\r\n" +
			"	<ChangeDateTime>" + changeDateTime + "</ChangeDateTime>\r\n" +
			"	<UserName>" + System.Web.HttpUtility.HtmlEncode(userName) + "</UserName>\r\n" +
			"	<FromValue>" + System.Web.HttpUtility.HtmlEncode(fromValue) + "</FromValue>\r\n" +
			"	<ToValue>" + System.Web.HttpUtility.HtmlEncode(toValue) + "</ToValue>\r\n" +
			"	<Comment>" + System.Web.HttpUtility.HtmlEncode(comment) + "</Comment>\r\n" +
			"	<ComputerName>" + System.Web.HttpUtility.HtmlEncode(computerName) + "</ComputerName>\r\n" +
			"	<CancelationReasonId>" + cancelationReasonId + "</CancelationReasonId>\r\n" +
			"	<OtherReason>" + System.Web.HttpUtility.HtmlEncode(otherReason) + "</OtherReason>\r\n" +
			"</SalesChangeLog>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("salesId")) {
					SetXmlValue(ref salesID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("tableName")) {
					SetXmlValue(ref tableName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("columnName")) {
					SetXmlValue(ref columnName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("changeDateTime")) {
					SetXmlValue(ref changeDateTime, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("userName")) {
					SetXmlValue(ref userName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("fromValue")) {
					SetXmlValue(ref fromValue, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("toValue")) {
					SetXmlValue(ref toValue, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comment")) {
					SetXmlValue(ref comment, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("computerName")) {
					SetXmlValue(ref computerName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("cancelationReasonId")) {
					SetXmlValue(ref cancelationReasonId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("otherReason")) {
					SetXmlValue(ref otherReason, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static SalesChangeLog[] GetSalesChangeLogs() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSalesChangeLogs();
		}

		public static SalesChangeLog GetSalesChangeLogByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSalesChangeLogByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertSalesChangeLog(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateSalesChangeLog(this);
		}
		#endregion

		#region Properties
		public int SalesID {
			set { salesID = value; }
			get { return salesID; }
		}

		public string TableName {
			set { tableName = value; }
			get { return tableName; }
		}

		public string ColumnName {
			set { columnName = value; }
			get { return columnName; }
		}

		public DateTime ChangeDateTime {
			set { changeDateTime = value; }
			get { return changeDateTime; }
		}

		public string UserName {
			set { userName = value; }
			get { return userName; }
		}

		public string FromValue {
			set { fromValue = value; }
			get { return fromValue; }
		}

		public string ToValue {
			set { toValue = value; }
			get { return toValue; }
		}

		public string Comment {
			set { comment = value; }
			get { return comment; }
		}

		public string ComputerName {
			set { computerName = value; }
			get { return computerName; }
		}

		public int CancelationReasonId {
			set { cancelationReasonId = value; }
			get { return cancelationReasonId; }
		}

		public string OtherReason {
			set { otherReason = value; }
			get { return otherReason; }
		}

		#endregion
	}
}
