using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class Priority: EFundraisingCRMDataObject {

		private int priorityID;
		private string description;
		private int colorCode;


		public Priority() : this(int.MinValue) { }
		public Priority(int priorityID) : this(priorityID, null) { }
		public Priority(int priorityID, string description) : this(priorityID, description, int.MinValue) { }
		public Priority(int priorityID, string description, int colorCode) {
			this.priorityID = priorityID;
			this.description = description;
			this.colorCode = colorCode;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Priority>\r\n" +
			"	<PriorityID>" + priorityID + "</PriorityID>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"	<ColorCode>" + colorCode + "</ColorCode>\r\n" +
			"</Priority>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("priorityId")) {
					SetXmlValue(ref priorityID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("colorCode")) {
					SetXmlValue(ref colorCode, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Priority[] GetPrioritys() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPrioritys();
		}

		public static Priority GetPriorityByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPriorityByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertPriority(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdatePriority(this);
		}
		#endregion

		#region Properties
		public int PriorityID {
			set { priorityID = value; }
			get { return priorityID; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public int ColorCode {
			set { colorCode = value; }
			get { return colorCode; }
		}

		#endregion
	}
}
