using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class AutomatonFunction: EFundraisingCRMDataObject {

		private int automatonFunctionId;
		private string functionName;
		private string description;


		public AutomatonFunction() : this(int.MinValue) { }
		public AutomatonFunction(int automatonFunctionId) : this(automatonFunctionId, null) { }
		public AutomatonFunction(int automatonFunctionId, string functionName) : this(automatonFunctionId, functionName, null) { }
		public AutomatonFunction(int automatonFunctionId, string functionName, string description) {
			this.automatonFunctionId = automatonFunctionId;
			this.functionName = functionName;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<AutomatonFunction>\r\n" +
			"	<AutomatonFunctionId>" + automatonFunctionId + "</AutomatonFunctionId>\r\n" +
			"	<FunctionName>" + System.Web.HttpUtility.HtmlEncode(functionName) + "</FunctionName>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</AutomatonFunction>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("automatonFunctionId")) {
					SetXmlValue(ref automatonFunctionId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("functionName")) {
					SetXmlValue(ref functionName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static AutomatonFunction[] GetAutomatonFunctions() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAutomatonFunctions();
		}

		public static AutomatonFunction GetAutomatonFunctionByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAutomatonFunctionByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertAutomatonFunction(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateAutomatonFunction(this);
		}
		#endregion

		#region Properties
		public int AutomatonFunctionId {
			set { automatonFunctionId = value; }
			get { return automatonFunctionId; }
		}

		public string FunctionName {
			set { functionName = value; }
			get { return functionName; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
