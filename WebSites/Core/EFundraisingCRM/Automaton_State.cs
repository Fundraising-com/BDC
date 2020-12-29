using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class AutomatonState: EFundraisingCRMDataObject {

		private int automatonId;
		private int stateId;
		private string description;


		public AutomatonState() : this(int.MinValue) { }
		public AutomatonState(int automatonId) : this(automatonId, int.MinValue) { }
		public AutomatonState(int automatonId, int stateId) : this(automatonId, stateId, null) { }
		public AutomatonState(int automatonId, int stateId, string description) {
			this.automatonId = automatonId;
			this.stateId = stateId;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<AutomatonState>\r\n" +
			"	<AutomatonId>" + automatonId + "</AutomatonId>\r\n" +
			"	<StateId>" + stateId + "</StateId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</AutomatonState>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("automatonId")) {
					SetXmlValue(ref automatonId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("stateId")) {
					SetXmlValue(ref stateId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static AutomatonState[] GetAutomatonStates() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAutomatonStates();
		}

		public static AutomatonState GetAutomatonStateByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAutomatonStateByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertAutomatonState(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateAutomatonState(this);
		}
		#endregion

		#region Properties
		public int AutomatonId {
			set { automatonId = value; }
			get { return automatonId; }
		}

		public int StateId {
			set { stateId = value; }
			get { return stateId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
