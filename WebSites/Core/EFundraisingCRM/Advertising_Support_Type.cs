using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class AdvertisingSupportType: EFundraisingCRMDataObject {

		private int advertisingSupportTypeID;
		private string description;
		private string comments;


		public AdvertisingSupportType() : this(int.MinValue) { }
		public AdvertisingSupportType(int advertisingSupportTypeID) : this(advertisingSupportTypeID, null) { }
		public AdvertisingSupportType(int advertisingSupportTypeID, string description) : this(advertisingSupportTypeID, description, null) { }
		public AdvertisingSupportType(int advertisingSupportTypeID, string description, string comments) {
			this.advertisingSupportTypeID = advertisingSupportTypeID;
			this.description = description;
			this.comments = comments;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<AdvertisingSupportType>\r\n" +
			"	<AdvertisingSupportTypeID>" + advertisingSupportTypeID + "</AdvertisingSupportTypeID>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
			"</AdvertisingSupportType>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("advertisingSupportTypeId")) {
					SetXmlValue(ref advertisingSupportTypeID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comments")) {
					SetXmlValue(ref comments, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static AdvertisingSupportType[] GetAdvertisingSupportTypes() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAdvertisingSupportTypes();
		}

		public static AdvertisingSupportType GetAdvertisingSupportTypeByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAdvertisingSupportTypeByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertAdvertisingSupportType(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateAdvertisingSupportType(this);
		}
		#endregion

		#region Properties
		public int AdvertisingSupportTypeID {
			set { advertisingSupportTypeID = value; }
			get { return advertisingSupportTypeID; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public string Comments {
			set { comments = value; }
			get { return comments; }
		}

		#endregion
	}
}
