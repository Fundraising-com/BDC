using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class DepositItem: EFundraisingCRMDataObject {

		private int depositID;
		private int salesID;
		private int paiementNo;


		public DepositItem() : this(int.MinValue) { }
		public DepositItem(int depositID) : this(depositID, int.MinValue) { }
		public DepositItem(int depositID, int salesID) : this(depositID, salesID, int.MinValue) { }
		public DepositItem(int depositID, int salesID, int paiementNo) {
			this.depositID = depositID;
			this.salesID = salesID;
			this.paiementNo = paiementNo;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<DepositItem>\r\n" +
			"	<DepositID>" + depositID + "</DepositID>\r\n" +
			"	<SalesID>" + salesID + "</SalesID>\r\n" +
			"	<PaiementNo>" + paiementNo + "</PaiementNo>\r\n" +
			"</DepositItem>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("depositId")) {
					SetXmlValue(ref depositID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesId")) {
					SetXmlValue(ref salesID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("paiementNo")) {
					SetXmlValue(ref paiementNo, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static DepositItem[] GetDepositItems() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetDepositItems();
		}

		public static DepositItem GetDepositItemByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetDepositItemByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertDepositItem(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateDepositItem(this);
		}
		#endregion

		#region Properties
		public int DepositID {
			set { depositID = value; }
			get { return depositID; }
		}

		public int SalesID {
			set { salesID = value; }
			get { return salesID; }
		}

		public int PaiementNo {
			set { paiementNo = value; }
			get { return paiementNo; }
		}

		#endregion
	}
}
