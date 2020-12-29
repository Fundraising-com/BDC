using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class Department: EFundraisingCRMDataObject {

		private int departmentId;
		private string departmentName;


		public Department() : this(int.MinValue) { }
		public Department(int departmentId) : this(departmentId, null) { }
		public Department(int departmentId, string departmentName) {
			this.departmentId = departmentId;
			this.departmentName = departmentName;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Department>\r\n" +
			"	<DepartmentId>" + departmentId + "</DepartmentId>\r\n" +
			"	<DepartmentName>" + System.Web.HttpUtility.HtmlEncode(departmentName) + "</DepartmentName>\r\n" +
			"</Department>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("departmentId")) {
					SetXmlValue(ref departmentId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("departmentName")) {
					SetXmlValue(ref departmentName, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Department[] GetDepartments() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetDepartments();
		}

		public static Department GetDepartmentByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetDepartmentByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertDepartment(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateDepartment(this);
		}
		#endregion

		#region Properties
		public int DepartmentId {
			set { departmentId = value; }
			get { return departmentId; }
		}

		public string DepartmentName {
			set { departmentName = value; }
			get { return departmentName; }
		}

		#endregion
	}
}
