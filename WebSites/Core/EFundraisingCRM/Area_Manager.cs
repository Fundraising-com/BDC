using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class AreaManager: EFundraisingCRMDataObject {

		private int areaManagerID;
		private string areaManagerName;


		public AreaManager() : this(int.MinValue) { }
		public AreaManager(int areaManagerID) : this(areaManagerID, null) { }
		public AreaManager(int areaManagerID, string areaManagerName) {
			this.areaManagerID = areaManagerID;
			this.areaManagerName = areaManagerName;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<AreaManager>\r\n" +
			"	<AreaManagerID>" + areaManagerID + "</AreaManagerID>\r\n" +
			"	<AreaManagerName>" + System.Web.HttpUtility.HtmlEncode(areaManagerName) + "</AreaManagerName>\r\n" +
			"</AreaManager>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("areaManagerId")) {
					SetXmlValue(ref areaManagerID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("areaManagerName")) {
					SetXmlValue(ref areaManagerName, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static AreaManager[] GetAreaManagers() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAreaManagers();
		}

		public static AreaManager GetAreaManagerByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAreaManagerByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertAreaManager(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateAreaManager(this);
		}
		#endregion

		#region Properties
		public int AreaManagerID {
			set { areaManagerID = value; }
			get { return areaManagerID; }
		}

		public string AreaManagerName {
			set { areaManagerName = value; }
			get { return areaManagerName; }
		}

		#endregion
	}
}
