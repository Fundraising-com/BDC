using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class AutomatonShippingStatus: EFundraisingCRMDataObject {

		private int fromStatusID;
		private int toStatusID;


		public AutomatonShippingStatus() : this(int.MinValue) { }
		public AutomatonShippingStatus(int fromStatusID) : this(fromStatusID, int.MinValue) { }
		public AutomatonShippingStatus(int fromStatusID, int toStatusID) {
			this.fromStatusID = fromStatusID;
			this.toStatusID = toStatusID;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<AutomatonShippingStatus>\r\n" +
			"	<FromStatusID>" + fromStatusID + "</FromStatusID>\r\n" +
			"	<ToStatusID>" + toStatusID + "</ToStatusID>\r\n" +
			"</AutomatonShippingStatus>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("fromStatusId")) {
					SetXmlValue(ref fromStatusID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("toStatusId")) {
					SetXmlValue(ref toStatusID, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static AutomatonShippingStatus[] GetAutomatonShippingStatuss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAutomatonShippingStatuss();
		}

		public static AutomatonShippingStatus GetAutomatonShippingStatusByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAutomatonShippingStatusByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertAutomatonShippingStatus(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateAutomatonShippingStatus(this);
		}
		#endregion

		#region Properties
		public int FromStatusID {
			set { fromStatusID = value; }
			get { return fromStatusID; }
		}

		public int ToStatusID {
			set { toStatusID = value; }
			get { return toStatusID; }
		}

		#endregion
	}
}
