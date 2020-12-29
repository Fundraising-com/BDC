using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class MSysConf: EFundraisingCRMDataObject {

		private int config;
		private string cHValue;
		private int nValue;
		private string comments;


		public MSysConf() : this(int.MinValue) { }
		public MSysConf(int config) : this(config, null) { }
		public MSysConf(int config, string cHValue) : this(config, cHValue, int.MinValue) { }
		public MSysConf(int config, string cHValue, int nValue) : this(config, cHValue, nValue, null) { }
		public MSysConf(int config, string cHValue, int nValue, string comments) {
			this.config = config;
			this.cHValue = cHValue;
			this.nValue = nValue;
			this.comments = comments;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<MSysConf>\r\n" +
			"	<Config>" + config + "</Config>\r\n" +
			"	<CHValue>" + System.Web.HttpUtility.HtmlEncode(cHValue) + "</CHValue>\r\n" +
			"	<NValue>" + nValue + "</NValue>\r\n" +
			"	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
			"</MSysConf>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("config")) {
					SetXmlValue(ref config, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("chvalue")) {
					SetXmlValue(ref cHValue, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("nvalue")) {
					SetXmlValue(ref nValue, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comments")) {
					SetXmlValue(ref comments, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static MSysConf[] GetMSysConfs() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetMSysConfs();
		}

		public static MSysConf GetMSysConfByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetMSysConfByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertMSysConf(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateMSysConf(this);
		}
		#endregion

		#region Properties
		public int Config {
			set { config = value; }
			get { return config; }
		}

		public string CHValue {
			set { cHValue = value; }
			get { return cHValue; }
		}

		public int NValue {
			set { nValue = value; }
			get { return nValue; }
		}

		public string Comments {
			set { comments = value; }
			get { return comments; }
		}

		#endregion
	}
}
