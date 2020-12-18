using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class AdvertismentType: EFundraisingCRMDataObject {

		private int advertismentTypeID;
		private string description;


		public AdvertismentType() : this(int.MinValue) { }
		public AdvertismentType(int advertismentTypeID) : this(advertismentTypeID, null) { }
		public AdvertismentType(int advertismentTypeID, string description) {
			this.advertismentTypeID = advertismentTypeID;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<AdvertismentType>\r\n" +
			"	<AdvertismentTypeID>" + advertismentTypeID + "</AdvertismentTypeID>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</AdvertismentType>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("advertismentTypeId")) {
					SetXmlValue(ref advertismentTypeID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static AdvertismentType[] GetAdvertismentTypes() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAdvertismentTypes();
		}

		public static AdvertismentType GetAdvertismentTypeByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAdvertismentTypeByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertAdvertismentType(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateAdvertismentType(this);
		}
		#endregion

		#region Properties
		public int AdvertismentTypeID {
			set { advertismentTypeID = value; }
			get { return advertismentTypeID; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
