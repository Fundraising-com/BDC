using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class Newsletter: eFundraisingStoreDataObject {

		private int newsletterId;
		private string cultureCode;
		private int partnerId;
		private string newsMonth;
		private string url;
		private short displayOrder;
		private short enabled;


		public Newsletter() : this(int.MinValue) { }
		public Newsletter(int newsletterId) : this(newsletterId, null) { }
		public Newsletter(int newsletterId, string cultureCode) : this(newsletterId, cultureCode, int.MinValue) { }
		public Newsletter(int newsletterId, string cultureCode, int partnerId) : this(newsletterId, cultureCode, partnerId, null) { }
		public Newsletter(int newsletterId, string cultureCode, int partnerId, string newsMonth) : this(newsletterId, cultureCode, partnerId, newsMonth, null) { }
		public Newsletter(int newsletterId, string cultureCode, int partnerId, string newsMonth, string url) : this(newsletterId, cultureCode, partnerId, newsMonth, url, short.MinValue) { }
		public Newsletter(int newsletterId, string cultureCode, int partnerId, string newsMonth, string url, short displayOrder) : this(newsletterId, cultureCode, partnerId, newsMonth, url, displayOrder, short.MinValue) { }
		public Newsletter(int newsletterId, string cultureCode, int partnerId, string newsMonth, string url, short displayOrder, short enabled) {
			this.newsletterId = newsletterId;
			this.cultureCode = cultureCode;
			this.partnerId = partnerId;
			this.newsMonth = newsMonth;
			this.url = url;
			this.displayOrder = displayOrder;
			this.enabled = enabled;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Newsletter>\r\n" +
			"	<NewsletterId>" + newsletterId + "</NewsletterId>\r\n" +
			"	<CultureCode>" + System.Web.HttpUtility.HtmlEncode(cultureCode) + "</CultureCode>\r\n" +
			"	<PartnerId>" + partnerId + "</PartnerId>\r\n" +
			"	<NewsMonth>" + System.Web.HttpUtility.HtmlEncode(newsMonth) + "</NewsMonth>\r\n" +
			"	<Url>" + System.Web.HttpUtility.HtmlEncode(url) + "</Url>\r\n" +
			"	<DisplayOrder>" + displayOrder + "</DisplayOrder>\r\n" +
			"	<Enabled>" + enabled + "</Enabled>\r\n" +
			"</Newsletter>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "newsletterId") {
					SetXmlValue(ref newsletterId, node.InnerText);
				}
				if(node.Name.ToLower() == "cultureCode") {
					SetXmlValue(ref cultureCode, node.InnerText);
				}
				if(node.Name.ToLower() == "partnerId") {
					SetXmlValue(ref partnerId, node.InnerText);
				}
				if(node.Name.ToLower() == "newsMonth") {
					SetXmlValue(ref newsMonth, node.InnerText);
				}
				if(node.Name.ToLower() == "url") {
					SetXmlValue(ref url, node.InnerText);
				}
				if(node.Name.ToLower() == "displayOrder") {
					SetXmlValue(ref displayOrder, node.InnerText);
				}
				if(node.Name.ToLower() == "enabled") {
					SetXmlValue(ref enabled, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Newsletter[] GetNewsletters() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetNewsletters();
		}

		public static Newsletter GetNewsletterByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetNewsletterByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertNewsletter(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateNewsletter(this);
		}
		#endregion

		#region Properties
		public int NewsletterId {
			set { newsletterId = value; }
			get { return newsletterId; }
		}

		public string CultureCode {
			set { cultureCode = value; }
			get { return cultureCode; }
		}

		public int PartnerId {
			set { partnerId = value; }
			get { return partnerId; }
		}

		public string NewsMonth {
			set { newsMonth = value; }
			get { return newsMonth; }
		}

		public string Url {
			set { url = value; }
			get { return url; }
		}

		public short DisplayOrder {
			set { displayOrder = value; }
			get { return displayOrder; }
		}

		public short Enabled {
			set { enabled = value; }
			get { return enabled; }
		}

		#endregion
	}
}
