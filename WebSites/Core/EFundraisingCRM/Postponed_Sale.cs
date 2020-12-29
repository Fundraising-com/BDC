using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class PostponedSale: EFundraisingCRMDataObject {

		private int salesID;
		private int postponedStatusID;
		private string comments;


		public PostponedSale() : this(int.MinValue) { }
		public PostponedSale(int salesID) : this(salesID, int.MinValue) { }
		public PostponedSale(int salesID, int postponedStatusID) : this(salesID, postponedStatusID, null) { }
		public PostponedSale(int salesID, int postponedStatusID, string comments) {
			this.salesID = salesID;
			this.postponedStatusID = postponedStatusID;
			this.comments = comments;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PostponedSale>\r\n" +
			"	<SalesID>" + salesID + "</SalesID>\r\n" +
			"	<PostponedStatusID>" + postponedStatusID + "</PostponedStatusID>\r\n" +
			"	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
			"</PostponedSale>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("salesId")) {
					SetXmlValue(ref salesID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("postponedStatusId")) {
					SetXmlValue(ref postponedStatusID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comments")) {
					SetXmlValue(ref comments, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PostponedSale[] GetPostponedSales() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPostponedSales();
		}

		public static PostponedSale GetPostponedSaleByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPostponedSaleByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertPostponedSale(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdatePostponedSale(this);
		}
		#endregion

		#region Properties
		public int SalesID {
			set { salesID = value; }
			get { return salesID; }
		}

		public int PostponedStatusID {
			set { postponedStatusID = value; }
			get { return postponedStatusID; }
		}

		public string Comments {
			set { comments = value; }
			get { return comments; }
		}

		#endregion
	}
}
