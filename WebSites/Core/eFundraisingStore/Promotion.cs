using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class Promotion: eFundraisingStoreDataObject {

		private int promotionId;
		private string promotionTypeCode;
		private int promotionDestinationId;
		private string name;
		private string scriptName;
		private short active;
		private DateTime createDate;


		public Promotion() : this(int.MinValue) { }
		public Promotion(int promotionId) : this(promotionId, null) { }
		public Promotion(int promotionId, string promotionTypeCode) : this(promotionId, promotionTypeCode, int.MinValue) { }
		public Promotion(int promotionId, string promotionTypeCode, int promotionDestinationId) : this(promotionId, promotionTypeCode, promotionDestinationId, null) { }
		public Promotion(int promotionId, string promotionTypeCode, int promotionDestinationId, string name) : this(promotionId, promotionTypeCode, promotionDestinationId, name, null) { }
		public Promotion(int promotionId, string promotionTypeCode, int promotionDestinationId, string name, string scriptName) : this(promotionId, promotionTypeCode, promotionDestinationId, name, scriptName, short.MinValue) { }
		public Promotion(int promotionId, string promotionTypeCode, int promotionDestinationId, string name, string scriptName, short active) : this(promotionId, promotionTypeCode, promotionDestinationId, name, scriptName, active, DateTime.MinValue) { }
		public Promotion(int promotionId, string promotionTypeCode, int promotionDestinationId, string name, string scriptName, short active, DateTime createDate) {
			this.promotionId = promotionId;
			this.promotionTypeCode = promotionTypeCode;
			this.promotionDestinationId = promotionDestinationId;
			this.name = name;
			this.scriptName = scriptName;
			this.active = active;
			this.createDate = createDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Promotion>\r\n" +
			"	<PromotionId>" + promotionId + "</PromotionId>\r\n" +
			"	<PromotionTypeCode>" + System.Web.HttpUtility.HtmlEncode(promotionTypeCode) + "</PromotionTypeCode>\r\n" +
			"	<PromotionDestinationId>" + promotionDestinationId + "</PromotionDestinationId>\r\n" +
			"	<Name>" + System.Web.HttpUtility.HtmlEncode(name) + "</Name>\r\n" +
			"	<ScriptName>" + System.Web.HttpUtility.HtmlEncode(scriptName) + "</ScriptName>\r\n" +
			"	<Active>" + active + "</Active>\r\n" +
			"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
			"</Promotion>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "promotionId") {
					SetXmlValue(ref promotionId, node.InnerText);
				}
				if(node.Name.ToLower() == "promotionTypeCode") {
					SetXmlValue(ref promotionTypeCode, node.InnerText);
				}
				if(node.Name.ToLower() == "promotionDestinationId") {
					SetXmlValue(ref promotionDestinationId, node.InnerText);
				}
				if(node.Name.ToLower() == "name") {
					SetXmlValue(ref name, node.InnerText);
				}
				if(node.Name.ToLower() == "scriptName") {
					SetXmlValue(ref scriptName, node.InnerText);
				}
				if(node.Name.ToLower() == "active") {
					SetXmlValue(ref active, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") {
					SetXmlValue(ref createDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Promotion[] GetPromotions() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPromotions();
		}

		public static Promotion GetPromotionByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPromotionByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertPromotion(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdatePromotion(this);
		}
		#endregion

		#region Properties
		public int PromotionId {
			set { promotionId = value; }
			get { return promotionId; }
		}

		public string PromotionTypeCode {
			set { promotionTypeCode = value; }
			get { return promotionTypeCode; }
		}

		public int PromotionDestinationId {
			set { promotionDestinationId = value; }
			get { return promotionDestinationId; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public string ScriptName {
			set { scriptName = value; }
			get { return scriptName; }
		}

		public short Active {
			set { active = value; }
			get { return active; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion
	}
}
