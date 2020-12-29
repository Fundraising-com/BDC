using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class AutomatonTransitionFunction: EFundraisingCRMDataObject {

		private int automatonId;
		private int stateToId;
		private int stateFromId;
		private int automatonFunctionId;
		private int sequence;


		public AutomatonTransitionFunction() : this(int.MinValue) { }
		public AutomatonTransitionFunction(int automatonId) : this(automatonId, int.MinValue) { }
		public AutomatonTransitionFunction(int automatonId, int stateToId) : this(automatonId, stateToId, int.MinValue) { }
		public AutomatonTransitionFunction(int automatonId, int stateToId, int stateFromId) : this(automatonId, stateToId, stateFromId, int.MinValue) { }
		public AutomatonTransitionFunction(int automatonId, int stateToId, int stateFromId, int automatonFunctionId) : this(automatonId, stateToId, stateFromId, automatonFunctionId, int.MinValue) { }
		public AutomatonTransitionFunction(int automatonId, int stateToId, int stateFromId, int automatonFunctionId, int sequence) {
			this.automatonId = automatonId;
			this.stateToId = stateToId;
			this.stateFromId = stateFromId;
			this.automatonFunctionId = automatonFunctionId;
			this.sequence = sequence;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<AutomatonTransitionFunction>\r\n" +
			"	<AutomatonId>" + automatonId + "</AutomatonId>\r\n" +
			"	<StateToId>" + stateToId + "</StateToId>\r\n" +
			"	<StateFromId>" + stateFromId + "</StateFromId>\r\n" +
			"	<AutomatonFunctionId>" + automatonFunctionId + "</AutomatonFunctionId>\r\n" +
			"	<Sequence>" + sequence + "</Sequence>\r\n" +
			"</AutomatonTransitionFunction>\r\n";
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
				if(ToLowerCase(node.Name) == ToLowerCase("automatonFunctionId")) {
					SetXmlValue(ref automatonFunctionId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("sequence")) {
					SetXmlValue(ref sequence, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static AutomatonTransitionFunction[] GetAutomatonTransitionFunctions() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAutomatonTransitionFunctions();
		}

		public static AutomatonTransitionFunction GetAutomatonTransitionFunctionByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAutomatonTransitionFunctionByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertAutomatonTransitionFunction(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateAutomatonTransitionFunction(this);
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

		public int AutomatonFunctionId {
			set { automatonFunctionId = value; }
			get { return automatonFunctionId; }
		}

		public int Sequence {
			set { sequence = value; }
			get { return sequence; }
		}

		#endregion
	}
}
