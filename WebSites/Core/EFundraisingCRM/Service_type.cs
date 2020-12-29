using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class ServiceType: EFundraisingCRMDataObject {

		private int serviceTypeId;
		private string description;


		public ServiceType() : this(int.MinValue) { }
		public ServiceType(int serviceTypeId) : this(serviceTypeId, null) { }
		public ServiceType(int serviceTypeId, string description) {
			this.serviceTypeId = serviceTypeId;
			this.description = description;
		}

		#region Static Data
		public static ServiceType Bulk {
			get { return new ServiceType(1, "Bulk"); }
		}

		public static ServiceType MaximumService {
			get { return new ServiceType(2, "Maximum Service"); }
		}
		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ServiceType>\r\n" +
			"	<ServiceTypeId>" + serviceTypeId + "</ServiceTypeId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</ServiceType>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("serviceTypeId")) {
					SetXmlValue(ref serviceTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ServiceType[] GetServiceTypes() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetServiceTypes();
		}

		/*
		public static ServiceType GetServiceTypeByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetServiceTypeByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertServiceType(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateServiceType(this);
		}*/
		#endregion

		#region Properties
		public int ServiceTypeId {
			set { serviceTypeId = value; }
			get { return serviceTypeId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
