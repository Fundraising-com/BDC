using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class Automaton: EFundraisingCRMDataObject {

		private int automatonId;
		private string description;


		public Automaton() : this(int.MinValue) { }
		public Automaton(int automatonId) : this(automatonId, null) { }
		public Automaton(int automatonId, string description) {
			this.automatonId = automatonId;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Automaton>\r\n" +
			"	<AutomatonId>" + automatonId + "</AutomatonId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</Automaton>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("automatonId")) {
					SetXmlValue(ref automatonId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Automaton[] GetAutomatons() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAutomatons();
		}

		public static Automaton GetAutomatonByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAutomatonByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertAutomaton(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateAutomaton(this);
		}
		#endregion

		#region Properties
		public int AutomatonId {
			set { automatonId = value; }
			get { return automatonId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
