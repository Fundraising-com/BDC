using System;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal {

	public class PrizeType: DataObject {

		private int prizeTypeId;
		private string prizeTypeName;
		private DateTime createDate;


		public PrizeType() : this(int.MinValue) { }
		public PrizeType(int prizeTypeId) : this(prizeTypeId, null) { }
		public PrizeType(int prizeTypeId, string prizeTypeName) : this(prizeTypeId, prizeTypeName, DateTime.MinValue) { }
		public PrizeType(int prizeTypeId, string prizeTypeName, DateTime createDate) {
			this.prizeTypeId = prizeTypeId;
			this.prizeTypeName = prizeTypeName;
			this.createDate = createDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PrizeType>\r\n" +
				"	<PrizeTypeId>" + prizeTypeId + "</PrizeTypeId>\r\n" +
				"	<PrizeTypeName>" + System.Web.HttpUtility.HtmlEncode(prizeTypeName) + "</PrizeTypeName>\r\n" +
				"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
				"</PrizeType>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "prizeTypeId") {
					SetXmlValue(ref prizeTypeId, node.InnerText);
				}
				if(node.Name.ToLower() == "prizeTypeName") {
					SetXmlValue(ref prizeTypeName, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") {
					SetXmlValue(ref createDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PrizeType[] GetPrizeTypes() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPrizeTypes();
		}

		public static PrizeType GetPrizeTypeByID(int id) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPrizeTypeByID(id);
		}

		public int Insert() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.InsertPrizeType(this);
		}

		public int Update() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.UpdatePrizeType(this);
		}
		#endregion

		#region Properties
		public int PrizeTypeId {
			set { prizeTypeId = value; }
			get { return prizeTypeId; }
		}

		public string PrizeTypeName {
			set { prizeTypeName = value; }
			get { return prizeTypeName; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion
	}
}
