using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class SportAssociation: EFundraisingCRMDataObject {

		private int sportAssociationId;
		private string sportAssDesc;


		public SportAssociation() : this(int.MinValue) { }
		public SportAssociation(int sportAssociationId) : this(sportAssociationId, null) { }
		public SportAssociation(int sportAssociationId, string sportAssDesc) {
			this.sportAssociationId = sportAssociationId;
			this.sportAssDesc = sportAssDesc;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<SportAssociation>\r\n" +
			"	<SportAssociationId>" + sportAssociationId + "</SportAssociationId>\r\n" +
			"	<SportAssDesc>" + System.Web.HttpUtility.HtmlEncode(sportAssDesc) + "</SportAssDesc>\r\n" +
			"</SportAssociation>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("sportAssociationId")) {
					SetXmlValue(ref sportAssociationId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("sportAssDesc")) {
					SetXmlValue(ref sportAssDesc, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static SportAssociation[] GetSportAssociations() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSportAssociations();
		}

		public static SportAssociation GetSportAssociationByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSportAssociationByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertSportAssociation(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateSportAssociation(this);
		}
		#endregion

		#region Properties
		public int SportAssociationId {
			set { sportAssociationId = value; }
			get { return sportAssociationId; }
		}

		public string SportAssDesc {
			set { sportAssDesc = value; }
			get { return sportAssDesc; }
		}

		#endregion
	}
}
