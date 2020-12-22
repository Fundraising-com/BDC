using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class TmpTotalAdjustment: EFundraisingCRMDataObject {

		private int salesID;
		private float adjustmentAmount;


		public TmpTotalAdjustment() : this(int.MinValue) { }
		public TmpTotalAdjustment(int salesID) : this(salesID, float.MinValue) { }
		public TmpTotalAdjustment(int salesID, float adjustmentAmount) {
			this.salesID = salesID;
			this.adjustmentAmount = adjustmentAmount;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<TmpTotalAdjustment>\r\n" +
			"	<SalesID>" + salesID + "</SalesID>\r\n" +
			"	<AdjustmentAmount>" + adjustmentAmount + "</AdjustmentAmount>\r\n" +
			"</TmpTotalAdjustment>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("salesId")) {
					SetXmlValue(ref salesID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("adjustmentAmount")) {
					SetXmlValue(ref adjustmentAmount, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static TmpTotalAdjustment[] GetTmpTotalAdjustments() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTmpTotalAdjustments();
		}

		public static TmpTotalAdjustment GetTmpTotalAdjustmentByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTmpTotalAdjustmentByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertTmpTotalAdjustment(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateTmpTotalAdjustment(this);
		}
		#endregion

		#region Properties
		public int SalesID {
			set { salesID = value; }
			get { return salesID; }
		}

		public float AdjustmentAmount {
			set { adjustmentAmount = value; }
			get { return adjustmentAmount; }
		}

		#endregion
	}
}
