using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class AutomatonTransition: EFundraisingCRMDataObject {

		private int automatonId;
		private int stateToId;
		private int stateFromId;
		private string description;


		public AutomatonTransition() : this(int.MinValue) { }
		public AutomatonTransition(int automatonId) : this(automatonId, int.MinValue) { }
		public AutomatonTransition(int automatonId, int stateToId) : this(automatonId, stateToId, int.MinValue) { }
		public AutomatonTransition(int automatonId, int stateToId, int stateFromId) : this(automatonId, stateToId, stateFromId, null) { }
		public AutomatonTransition(int automatonId, int stateToId, int stateFromId, string description) {
			this.automatonId = automatonId;
			this.stateToId = stateToId;
			this.stateFromId = stateFromId;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<AutomatonTransition>\r\n" +
			"	<AutomatonId>" + automatonId + "</AutomatonId>\r\n" +
			"	<StateToId>" + stateToId + "</StateToId>\r\n" +
			"	<StateFromId>" + stateFromId + "</StateFromId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</AutomatonTransition>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("automatonId")) {
					SetXmlValue(ref automatonId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("stateToId")) {
					SetXmlValue(ref stateToId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("stateFromId")) {
					SetXmlValue(ref stateFromId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static AutomatonTransition[] GetAutomatonTransitions() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAutomatonTransitions();
		}

		public static AutomatonTransition GetAutomatonTransitionByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAutomatonTransitionByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertAutomatonTransition(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateAutomatonTransition(this);
		}
		#endregion

		#region Properties
		public int AutomatonId {
			set { automatonId = value; }
			get { return automatonId; }
		}

		public int StateToId {
			set { stateToId = value; }
			get { return stateToId; }
		}

		public int StateFromId {
			set { stateFromId = value; }
			get { return stateFromId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
