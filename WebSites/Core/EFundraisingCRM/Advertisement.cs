using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class Advertisement: EFundraisingCRMDataObject {

		private int advertisementId;
		private short divisionId;
		private string description;
		private float size;
		private int nbColors;
		private string comments;


		public Advertisement() : this(int.MinValue) { }
		public Advertisement(int advertisementId) : this(advertisementId, short.MinValue) { }
		public Advertisement(int advertisementId, short divisionId) : this(advertisementId, divisionId, null) { }
		public Advertisement(int advertisementId, short divisionId, string description) : this(advertisementId, divisionId, description, short.MinValue) { }
		public Advertisement(int advertisementId, short divisionId, string description, float size) : this(advertisementId, divisionId, description, size, int.MinValue) { }
		public Advertisement(int advertisementId, short divisionId, string description, float size, int nbColors) : this(advertisementId, divisionId, description, size, nbColors, null) { }
		public Advertisement(int advertisementId, short divisionId, string description, float size, int nbColors, string comments) {
			this.advertisementId = advertisementId;
			this.divisionId = divisionId;
			this.description = description;
			this.size = size;
			this.nbColors = nbColors;
			this.comments = comments;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Advertisement>\r\n" +
			"	<AdvertisementId>" + advertisementId + "</AdvertisementId>\r\n" +
			"	<DivisionId>" + divisionId + "</DivisionId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"	<Size>" + size + "</Size>\r\n" +
			"	<NbColors>" + nbColors + "</NbColors>\r\n" +
			"	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
			"</Advertisement>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("advertisementId")) {
					SetXmlValue(ref advertisementId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("divisionId")) {
					SetXmlValue(ref divisionId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("size")) {
					SetXmlValue(ref size, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("nbColors")) {
					SetXmlValue(ref nbColors, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comments")) {
					SetXmlValue(ref comments, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Advertisement[] GetAdvertisements() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAdvertisements();
		}

		public static Advertisement GetAdvertisementByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAdvertisementByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertAdvertisement(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateAdvertisement(this);
		}
		#endregion

		#region Properties
		public int AdvertisementId {
			set { advertisementId = value; }
			get { return advertisementId; }
		}

		public short DivisionId {
			set { divisionId = value; }
			get { return divisionId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public float Size {
			set { size = value; }
			get { return size; }
		}

		public int NbColors {
			set { nbColors = value; }
			get { return nbColors; }
		}

		public string Comments {
			set { comments = value; }
			get { return comments; }
		}

		#endregion
	}
}
