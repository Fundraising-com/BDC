using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class CollectionStatus: EFundraisingCRMDataObject {

		private int collectionStatusID;
		private string description;


		public CollectionStatus() : this(int.MinValue) { }
		public CollectionStatus(int collectionStatusID) : this(collectionStatusID, null) { }
		public CollectionStatus(int collectionStatusID, string description) {
			this.collectionStatusID = collectionStatusID;
			this.description = description;
		}

		#region Static Data

		public static CollectionStatus Default {
			get { return CollectionStatus.WaitingForPayment; }
		}
		
		public static CollectionStatus InCourt {
			get { return new CollectionStatus(1, "In Court"); }
		}

		public static CollectionStatus PaymentArrangement {
			get { return new CollectionStatus(2, "Payment arrangement"); }
		}

		public static CollectionStatus NotSufficientFunds_NSF {
			get { return new CollectionStatus(3, "Not sufficient funds (NSF)"); }
		}

		public static CollectionStatus StopPayment {
			get { return new CollectionStatus(4, "Stop payment"); }
		}

		public static CollectionStatus CreditCardChargeBack {
			get { return new CollectionStatus(5, "Credit card charge back"); }
		}

		public static CollectionStatus CollectionAgency {
			get { return new CollectionStatus(6, "Collection agency"); }
		}

		public static CollectionStatus HoldPayment {
			get { return new CollectionStatus(7, "Hold payment"); }
		}

		public static CollectionStatus CheckInHouse {
			get { return new CollectionStatus(8, "Check in house"); }
		}

		public static CollectionStatus WaitingForPayment {
			get { return new CollectionStatus(9, "Waiting for payment"); }
		}

		public static CollectionStatus ToBeCredited {
			get { return new CollectionStatus(10, "To be credited"); }
		}

		public static CollectionStatus AccountClosed {
			get { return new CollectionStatus(11, "Account closed"); }
		}

		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<CollectionStatus>\r\n" +
			"	<CollectionStatusID>" + collectionStatusID + "</CollectionStatusID>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</CollectionStatus>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("collectionStatusId")) {
					SetXmlValue(ref collectionStatusID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static CollectionStatus[] GetCollectionStatuss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCollectionStatuss();
		}

		public static CollectionStatus GetCollectionStatusByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCollectionStatusByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertCollectionStatus(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateCollectionStatus(this);
		}
		#endregion

		#region Properties
		public int CollectionStatusID {
			set { collectionStatusID = value; }
			get { return collectionStatusID; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
