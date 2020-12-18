using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class SpecialOffer: EFundraisingCRMDataObject {

		private int specialOfferId;
		private int brandId;
		private int productClassId;
		private string specialOfferText;


		public SpecialOffer() : this(int.MinValue) { }
		public SpecialOffer(int specialOfferId) : this(specialOfferId, int.MinValue) { }
		public SpecialOffer(int specialOfferId, int brandId) : this(specialOfferId, brandId, int.MinValue) { }
		public SpecialOffer(int specialOfferId, int brandId, int productClassId) : this(specialOfferId, brandId, productClassId, null) { }
		public SpecialOffer(int specialOfferId, int brandId, int productClassId, string specialOfferText) {
			this.specialOfferId = specialOfferId;
			this.brandId = brandId;
			this.productClassId = productClassId;
			this.specialOfferText = specialOfferText;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<SpecialOffer>\r\n" +
			"	<SpecialOfferId>" + specialOfferId + "</SpecialOfferId>\r\n" +
			"	<BrandId>" + brandId + "</BrandId>\r\n" +
			"	<ProductClassId>" + productClassId + "</ProductClassId>\r\n" +
			"	<SpecialOfferText>" + System.Web.HttpUtility.HtmlEncode(specialOfferText) + "</SpecialOfferText>\r\n" +
			"</SpecialOffer>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("specialOfferId")) {
					SetXmlValue(ref specialOfferId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("brandId")) {
					SetXmlValue(ref brandId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("productClassId")) {
					SetXmlValue(ref productClassId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("specialOfferText")) {
					SetXmlValue(ref specialOfferText, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static SpecialOffer[] GetSpecialOffers() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSpecialOffers();
		}

		/*
		public static SpecialOffer GetSpecialOfferByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSpecialOfferByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertSpecialOffer(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateSpecialOffer(this);
		}*/
		#endregion

		#region Properties
		public int SpecialOfferId {
			set { specialOfferId = value; }
			get { return specialOfferId; }
		}

		public int BrandId {
			set { brandId = value; }
			get { return brandId; }
		}

		public int ProductClassId {
			set { productClassId = value; }
			get { return productClassId; }
		}

		public string SpecialOfferText {
			set { specialOfferText = value; }
			get { return specialOfferText; }
		}

		#endregion
	}
}
