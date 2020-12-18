using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class ARStatus: EFundraisingCRMDataObject {

		private int aRStatusID;
		private string description;
		private int commissionOnHold;
		private int commissionIsPayable;
		private int commissionIsCredited;


		public ARStatus() : this(int.MinValue) { }
		public ARStatus(int aRStatusID) : this(aRStatusID, null) { }
		public ARStatus(int aRStatusID, string description) : this(aRStatusID, description, int.MinValue) { }
		public ARStatus(int aRStatusID, string description, int commissionOnHold) : this(aRStatusID, description, commissionOnHold, int.MinValue) { }
		public ARStatus(int aRStatusID, string description, int commissionOnHold, int commissionIsPayable) : this(aRStatusID, description, commissionOnHold, commissionIsPayable, int.MinValue) { }
		public ARStatus(int aRStatusID, string description, int commissionOnHold, int commissionIsPayable, int commissionIsCredited) {
			this.aRStatusID = aRStatusID;
			this.description = description;
			this.commissionOnHold = commissionOnHold;
			this.commissionIsPayable = commissionIsPayable;
			this.commissionIsCredited = commissionIsCredited;
		}

		#region Static Data 
		
		public static ARStatus Default {
			get { return ARStatus.NotPaid; }
		}
		
		public static ARStatus Paid {
			get { return new ARStatus(20, "Paid", int.MinValue, int.MinValue, int.MinValue); }
		}

		public static ARStatus NotPaid {
			get { return new ARStatus(21, "Not paid", int.MinValue, int.MinValue, int.MinValue); }
		}

		public static ARStatus Credited {
			get { return new ARStatus(22, "Credited", int.MinValue, int.MinValue, int.MinValue); }
		}

		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ARStatus>\r\n" +
			"	<ARStatusID>" + aRStatusID + "</ARStatusID>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"	<CommissionOnHold>" + commissionOnHold + "</CommissionOnHold>\r\n" +
			"	<CommissionIsPayable>" + commissionIsPayable + "</CommissionIsPayable>\r\n" +
			"	<CommissionIsCredited>" + commissionIsCredited + "</CommissionIsCredited>\r\n" +
			"</ARStatus>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("arStatusId")) {
					SetXmlValue(ref aRStatusID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("commissionOnHold")) {
					SetXmlValue(ref commissionOnHold, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("commissionIsPayable")) {
					SetXmlValue(ref commissionIsPayable, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("commissionIsCredited")) {
					SetXmlValue(ref commissionIsCredited, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ARStatus[] GetARStatuss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetARStatuss();
		}

		public static ARStatus GetARStatusByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetARStatusByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertARStatus(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateARStatus(this);
		}
		#endregion

		#region Properties
		public int ARStatusID {
			set { aRStatusID = value; }
			get { return aRStatusID; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public int CommissionOnHold {
			set { commissionOnHold = value; }
			get { return commissionOnHold; }
		}

		public int CommissionIsPayable {
			set { commissionIsPayable = value; }
			get { return commissionIsPayable; }
		}

		public int CommissionIsCredited {
			set { commissionIsCredited = value; }
			get { return commissionIsCredited; }
		}

		#endregion
	}
}
