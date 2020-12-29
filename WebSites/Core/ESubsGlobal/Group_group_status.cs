using System;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal {

	public enum GroupGroupStatusStatus
	{
		Error,
		Ok
	}

	public class GroupGroupStatus  {

		private int groupId;
		private int groupStatusId;
		private DateTime createDate;


		public GroupGroupStatus() : this(int.MinValue) { }
		public GroupGroupStatus(int groupId) : this(groupId, int.MinValue) { }
		public GroupGroupStatus(int groupId, int groupStatusId) : this(groupId, groupStatusId, DateTime.MinValue) { }
		public GroupGroupStatus(int groupId, int groupStatusId, DateTime createDate) {
			this.groupId = groupId;
			this.groupStatusId = groupStatusId;
			this.createDate = createDate;
		}


		#region XML Methods

		#region Save XML
		private string IdentXML(string xml) {
			string newXML = "";
			string[] lines = xml.Split('\r');
			foreach(string strXml in lines) {
				if(strXml.Trim() == "")
					break;
				newXML += "\t" + strXml.Replace("\n", "") + "\r\n";
			}
			return newXML;
		}

		public virtual string GenerateXML() {
			return "<GroupGroupStatus>\r\n" +
			"	<GroupId>" + groupId + "</GroupId>\r\n" +
			"	<GroupStatusId>" + groupStatusId + "</GroupStatusId>\r\n" +
			"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
			"</GroupGroupStatus>\r\n";
		}
		#endregion

		#region Set Xml Values
		private void SetXmlValue(ref int obj, string val) {
			if(val == "") { obj = int.MinValue; return; }
			obj = int.Parse(val);
		}

		private void SetXmlValue(ref string obj, string val) {
			if(val == "") { obj = null; return; }
			obj = System.Web.HttpUtility.HtmlDecode(val);
		}
		
		private void SetXmlValue(ref bool obj, string val) {
			if(val == "") { obj = false; return; }
			obj = (val.ToLower() == "true");
		}

		private void SetXmlValue(ref Decimal obj, string val) {
			if(val == "") { obj = Decimal.MinValue; return; }
			obj = Decimal.Parse(val);
		}

		private void SetXmlValue(ref DateTime obj, string val) {
			if(val == "") { obj = DateTime.MinValue; return; }
			obj = DateTime.Parse(val);
		}

		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public virtual void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "groupId") {
					SetXmlValue(ref groupId, node.InnerText);
				}
				if(node.Name.ToLower() == "groupStatusId") {
					SetXmlValue(ref groupStatusId, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") {
					SetXmlValue(ref createDate, node.InnerText);
				}
			}
		}
		// load from an xml string 
		public virtual void LoadXml(string xml) {
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		// load from an xml document object
		public virtual void Load(System.Xml.XmlDocument doc) {
			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		// load from a stream
		public virtual void Load(System.IO.Stream inStream) {
			XmlDocument doc = new XmlDocument();
			doc.Load(inStream);

			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		// load from a text reader
		public virtual void Load(System.IO.TextReader txtReader) {
			XmlDocument doc = new XmlDocument();
			doc.Load(txtReader);

			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		// load from an xml reader
		public virtual void Load(System.Xml.XmlReader reader) {
			XmlDocument doc = new XmlDocument();
			doc.Load(reader);

			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		// load from a xml filename
		public virtual void Load(string filename) {
			XmlDocument doc = new XmlDocument();
			doc.Load(filename);

			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		#endregion

		#endregion

		#region Data Source Methods
		public static GroupGroupStatus[] GetGroupGroupStatuss() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetGroupGroupStatuss();
		}

		public static GroupGroupStatus GetGroupGroupStatusByID(int id) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetGroupGroupStatusByID(id);
		}

		public int Insert() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.InsertGroupGroupStatus(this);
		}

		public int Update() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.UpdateGroupGroupStatus(this);
		}
		#endregion

		#region Properties
		public int GroupId {
			set { groupId = value; }
			get { return groupId; }
		}

		public int GroupStatusId {
			set { groupStatusId = value; }
			get { return groupStatusId; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion
	}
}
