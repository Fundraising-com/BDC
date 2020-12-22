using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class Tags: EFundraisingCRMDataObject {

		private int tagsID;
		private string label;
		private string controlName;


		public Tags() : this(int.MinValue) { }
		public Tags(int tagsID) : this(tagsID, null) { }
		public Tags(int tagsID, string label) : this(tagsID, label, null) { }
		public Tags(int tagsID, string label, string controlName) {
			this.tagsID = tagsID;
			this.label = label;
			this.controlName = controlName;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Tags>\r\n" +
			"	<TagsID>" + tagsID + "</TagsID>\r\n" +
			"	<Label>" + System.Web.HttpUtility.HtmlEncode(label) + "</Label>\r\n" +
			"	<ControlName>" + System.Web.HttpUtility.HtmlEncode(controlName) + "</ControlName>\r\n" +
			"</Tags>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("tagsId")) {
					SetXmlValue(ref tagsID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("label")) {
					SetXmlValue(ref label, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("controlName")) {
					SetXmlValue(ref controlName, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Tags[] GetTagss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTagss();
		}

		public static Tags GetTagsByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTagsByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertTags(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateTags(this);
		}
		#endregion

		#region Properties
		public int TagsID {
			set { tagsID = value; }
			get { return tagsID; }
		}

		public string Label {
			set { label = value; }
			get { return label; }
		}

		public string ControlName {
			set { controlName = value; }
			get { return controlName; }
		}

		#endregion
	}
}
