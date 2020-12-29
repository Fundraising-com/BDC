using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class AdvertisingSupport: EFundraisingCRMDataObject {

		private int advertisingSupportID;
		private int advertisingSupportTypeID;
		private string title;
		private DateTime publishnigDate;
		private string webSite;
		private string orderingPhoneNumber;
		private int periodicity;
		private int nbDraw;
		private float magazinePrice;
		private string comments;


		public AdvertisingSupport() : this(int.MinValue) { }
		public AdvertisingSupport(int advertisingSupportID) : this(advertisingSupportID, int.MinValue) { }
		public AdvertisingSupport(int advertisingSupportID, int advertisingSupportTypeID) : this(advertisingSupportID, advertisingSupportTypeID, null) { }
		public AdvertisingSupport(int advertisingSupportID, int advertisingSupportTypeID, string title) : this(advertisingSupportID, advertisingSupportTypeID, title, DateTime.MinValue) { }
		public AdvertisingSupport(int advertisingSupportID, int advertisingSupportTypeID, string title, DateTime publishnigDate) : this(advertisingSupportID, advertisingSupportTypeID, title, publishnigDate, null) { }
		public AdvertisingSupport(int advertisingSupportID, int advertisingSupportTypeID, string title, DateTime publishnigDate, string webSite) : this(advertisingSupportID, advertisingSupportTypeID, title, publishnigDate, webSite, null) { }
		public AdvertisingSupport(int advertisingSupportID, int advertisingSupportTypeID, string title, DateTime publishnigDate, string webSite, string orderingPhoneNumber) : this(advertisingSupportID, advertisingSupportTypeID, title, publishnigDate, webSite, orderingPhoneNumber, int.MinValue) { }
		public AdvertisingSupport(int advertisingSupportID, int advertisingSupportTypeID, string title, DateTime publishnigDate, string webSite, string orderingPhoneNumber, int periodicity) : this(advertisingSupportID, advertisingSupportTypeID, title, publishnigDate, webSite, orderingPhoneNumber, periodicity, int.MinValue) { }
		public AdvertisingSupport(int advertisingSupportID, int advertisingSupportTypeID, string title, DateTime publishnigDate, string webSite, string orderingPhoneNumber, int periodicity, int nbDraw) : this(advertisingSupportID, advertisingSupportTypeID, title, publishnigDate, webSite, orderingPhoneNumber, periodicity, nbDraw, float.MinValue) { }
		public AdvertisingSupport(int advertisingSupportID, int advertisingSupportTypeID, string title, DateTime publishnigDate, string webSite, string orderingPhoneNumber, int periodicity, int nbDraw, float magazinePrice) : this(advertisingSupportID, advertisingSupportTypeID, title, publishnigDate, webSite, orderingPhoneNumber, periodicity, nbDraw, magazinePrice, null) { }
		public AdvertisingSupport(int advertisingSupportID, int advertisingSupportTypeID, string title, DateTime publishnigDate, string webSite, string orderingPhoneNumber, int periodicity, int nbDraw, float magazinePrice, string comments) {
			this.advertisingSupportID = advertisingSupportID;
			this.advertisingSupportTypeID = advertisingSupportTypeID;
			this.title = title;
			this.publishnigDate = publishnigDate;
			this.webSite = webSite;
			this.orderingPhoneNumber = orderingPhoneNumber;
			this.periodicity = periodicity;
			this.nbDraw = nbDraw;
			this.magazinePrice = magazinePrice;
			this.comments = comments;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<AdvertisingSupport>\r\n" +
			"	<AdvertisingSupportID>" + advertisingSupportID + "</AdvertisingSupportID>\r\n" +
			"	<AdvertisingSupportTypeID>" + advertisingSupportTypeID + "</AdvertisingSupportTypeID>\r\n" +
			"	<Title>" + System.Web.HttpUtility.HtmlEncode(title) + "</Title>\r\n" +
			"	<PublishnigDate>" + publishnigDate + "</PublishnigDate>\r\n" +
			"	<WebSite>" + System.Web.HttpUtility.HtmlEncode(webSite) + "</WebSite>\r\n" +
			"	<OrderingPhoneNumber>" + System.Web.HttpUtility.HtmlEncode(orderingPhoneNumber) + "</OrderingPhoneNumber>\r\n" +
			"	<Periodicity>" + periodicity + "</Periodicity>\r\n" +
			"	<NbDraw>" + nbDraw + "</NbDraw>\r\n" +
			"	<MagazinePrice>" + magazinePrice + "</MagazinePrice>\r\n" +
			"	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
			"</AdvertisingSupport>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("advertisingSupportId")) {
					SetXmlValue(ref advertisingSupportID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("advertisingSupportTypeId")) {
					SetXmlValue(ref advertisingSupportTypeID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("title")) {
					SetXmlValue(ref title, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("publishnigDate")) {
					SetXmlValue(ref publishnigDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("webSite")) {
					SetXmlValue(ref webSite, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("orderingPhoneNumber")) {
					SetXmlValue(ref orderingPhoneNumber, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("periodicity")) {
					SetXmlValue(ref periodicity, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("nbDraw")) {
					SetXmlValue(ref nbDraw, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("magazinePrice")) {
					SetXmlValue(ref magazinePrice, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comments")) {
					SetXmlValue(ref comments, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static AdvertisingSupport[] GetAdvertisingSupports() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAdvertisingSupports();
		}

		public static AdvertisingSupport GetAdvertisingSupportByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAdvertisingSupportByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertAdvertisingSupport(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateAdvertisingSupport(this);
		}
		#endregion

		#region Properties
		public int AdvertisingSupportID {
			set { advertisingSupportID = value; }
			get { return advertisingSupportID; }
		}

		public int AdvertisingSupportTypeID {
			set { advertisingSupportTypeID = value; }
			get { return advertisingSupportTypeID; }
		}

		public string Title {
			set { title = value; }
			get { return title; }
		}

		public DateTime PublishnigDate {
			set { publishnigDate = value; }
			get { return publishnigDate; }
		}

		public string WebSite {
			set { webSite = value; }
			get { return webSite; }
		}

		public string OrderingPhoneNumber {
			set { orderingPhoneNumber = value; }
			get { return orderingPhoneNumber; }
		}

		public int Periodicity {
			set { periodicity = value; }
			get { return periodicity; }
		}

		public int NbDraw {
			set { nbDraw = value; }
			get { return nbDraw; }
		}

		public float MagazinePrice {
			set { magazinePrice = value; }
			get { return magazinePrice; }
		}

		public string Comments {
			set { comments = value; }
			get { return comments; }
		}

		#endregion
	}
}
