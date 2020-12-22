using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class ReqPriority: EFundraisingCRMDataObject {

		private int priorityId;
		private int languageId;
		private string description;
		private int isDefault;


		public ReqPriority() : this(int.MinValue) { }
		public ReqPriority(int priorityId) : this(priorityId, int.MinValue) { }
		public ReqPriority(int priorityId, int languageId) : this(priorityId, languageId, null) { }
		public ReqPriority(int priorityId, int languageId, string description) : this(priorityId, languageId, description, int.MinValue) { }
		public ReqPriority(int priorityId, int languageId, string description, int isDefault) {
			this.priorityId = priorityId;
			this.languageId = languageId;
			this.description = description;
			this.isDefault = isDefault;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ReqPriority>\r\n" +
			"	<PriorityId>" + priorityId + "</PriorityId>\r\n" +
			"	<LanguageId>" + languageId + "</LanguageId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"	<IsDefault>" + isDefault + "</IsDefault>\r\n" +
			"</ReqPriority>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("priorityId")) {
					SetXmlValue(ref priorityId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("languageId")) {
					SetXmlValue(ref languageId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isDefault")) {
					SetXmlValue(ref isDefault, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ReqPriority[] GetReqPrioritys() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetReqPrioritys();
		}

		public static ReqPriority GetReqPriorityByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetReqPriorityByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertReqPriority(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateReqPriority(this);
		}
		#endregion

		#region Properties
		public int PriorityId {
			set { priorityId = value; }
			get { return priorityId; }
		}

		public int LanguageId {
			set { languageId = value; }
			get { return languageId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public int IsDefault {
			set { isDefault = value; }
			get { return isDefault; }
		}

		#endregion
	}
}
