using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class PromotionDestination: eFundraisingStoreDataObject {

		private int promotionDestinationId;
		private string url;
		private DateTime createDate;


		public PromotionDestination() : this(int.MinValue) { }
		public PromotionDestination(int promotionDestinationId) : this(promotionDestinationId, null) { }
		public PromotionDestination(int promotionDestinationId, string url) : this(promotionDestinationId, url, DateTime.MinValue) { }
		public PromotionDestination(int promotionDestinationId, string url, DateTime createDate) {
			this.promotionDestinationId = promotionDestinationId;
			this.url = url;
			this.createDate = createDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PromotionDestination>\r\n" +
			"	<PromotionDestinationId>" + promotionDestinationId + "</PromotionDestinationId>\r\n" +
			"	<Url>" + System.Web.HttpUtility.HtmlEncode(url) + "</Url>\r\n" +
			"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
			"</PromotionDestination>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "promotionDestinationId") {
					SetXmlValue(ref promotionDestinationId, node.InnerText);
				}
				if(node.Name.ToLower() == "url") {
					SetXmlValue(ref url, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") {
					SetXmlValue(ref createDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PromotionDestination[] GetPromotionDestinations() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPromotionDestinations();
		}

		public static PromotionDestination GetPromotionDestinationByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPromotionDestinationByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertPromotionDestination(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdatePromotionDestination(this);
		}
		#endregion

		#region Properties
		public int PromotionDestinationId {
			set { promotionDestinationId = value; }
			get { return promotionDestinationId; }
		}

		public string Url {
			set { url = value; }
			get { return url; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion
	}
}
