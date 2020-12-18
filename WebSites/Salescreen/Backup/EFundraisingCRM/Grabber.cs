using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class Grabber: EFundraisingCRMDataObject {

		private int grabberId;
		private string grabberDesc;


		public Grabber() : this(int.MinValue) { }
		public Grabber(int grabberId) : this(grabberId, null) { }
		public Grabber(int grabberId, string grabberDesc) {
			this.grabberId = grabberId;
			this.grabberDesc = grabberDesc;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Grabber>\r\n" +
			"	<GrabberId>" + grabberId + "</GrabberId>\r\n" +
			"	<GrabberDesc>" + System.Web.HttpUtility.HtmlEncode(grabberDesc) + "</GrabberDesc>\r\n" +
			"</Grabber>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("grabberId")) {
					SetXmlValue(ref grabberId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("grabberDesc")) {
					SetXmlValue(ref grabberDesc, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Grabber[] GetGrabbers() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetGrabbers();
		}

		public static Grabber GetGrabberByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetGrabberByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertGrabber(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateGrabber(this);
		}
		#endregion

		#region Properties
		public int GrabberId {
			set { grabberId = value; }
			get { return grabberId; }
		}

		public string GrabberDesc {
			set { grabberDesc = value; }
			get { return grabberDesc; }
		}

		#endregion
	}
}
