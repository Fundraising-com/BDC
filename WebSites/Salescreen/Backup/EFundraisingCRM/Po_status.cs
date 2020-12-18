using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class PoStatus: EFundraisingCRMDataObject{

		private float poStatusId;
		private string description;


		public PoStatus() : this(float.MinValue) { }
		public PoStatus(float poStatusId) : this(poStatusId, null) { }
		public PoStatus(float poStatusId, string description) {
			this.poStatusId = poStatusId;
			this.description = description;
		}

		#region Static Data
        public static PoStatus NA
        {
            get { return new PoStatus(4, "N/A"); }
        }
        
        public static PoStatus Confirmed {
			get { return new PoStatus(2, "Confirmed"); }
		}

		public static PoStatus Pending {
			get { return new PoStatus(1, "Pending"); }
		}

		public static PoStatus Received {
			get { return new PoStatus(3, "Received"); }
		}
		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PoStatus>\r\n" +
			"	<PoStatusId>" + poStatusId + "</PoStatusId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</PoStatus>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("poStatusId")) {
					SetXmlValue(ref poStatusId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PoStatus[] GetPoStatuss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPoStatuss();
		}

		
		public static PoStatusCollections GetPoStatussCollections() 
		{
			PoStatusCollections poCollection = new PoStatusCollections();
			PoStatus[] poSts = GetPoStatuss();
			for (int i=0; i< poSts.Length; i++)
				poCollection.Add(poSts[i]);
			return poCollection;
		}

		
		public static PoStatus GetPoStatusByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPoStatusByID(id);
		}
/*
		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertPoStatus(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdatePoStatus(this);
		}*/
		#endregion

		#region Properties
		public float PoStatusId {
			set { poStatusId = value; }
			get { return poStatusId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion

		
	}
}
