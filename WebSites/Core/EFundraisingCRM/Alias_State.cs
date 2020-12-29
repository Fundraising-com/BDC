using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class AliasState: EFundraisingCRMDataObject {

		private string inputStateCode;
		private string stateCode;


		public AliasState() : this(null) { }
		public AliasState(string inputStateCode) : this(inputStateCode, null) { }
		public AliasState(string inputStateCode, string stateCode) {
			this.inputStateCode = inputStateCode;
			this.stateCode = stateCode;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<AliasState>\r\n" +
			"	<InputStateCode>" + System.Web.HttpUtility.HtmlEncode(inputStateCode) + "</InputStateCode>\r\n" +
			"	<StateCode>" + System.Web.HttpUtility.HtmlEncode(stateCode) + "</StateCode>\r\n" +
			"</AliasState>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("inputStateCode")) {
					SetXmlValue(ref inputStateCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("stateCode")) {
					SetXmlValue(ref stateCode, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static AliasState[] GetAliasStates() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAliasStates();
		}

		/*
		public static AliasState GetAliasStateByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAliasStateByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertAliasState(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateAliasState(this);
		}*/
		#endregion

		#region Properties
		public string InputStateCode {
			set { inputStateCode = value; }
			get { return inputStateCode; }
		}

		public string StateCode {
			set { stateCode = value; }
			get { return stateCode; }
		}

		#endregion
	}
}
