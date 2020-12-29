using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	/*
	 * Lead channel represents how the lead and/or client has
	 * been created.
	 * 
	 */

	public class LeadChannel: EFundraisingCRMDataObject {

		private string channelCode;
		private string description;


		public LeadChannel() : this(null) { }
		public LeadChannel(string channelCode) : this(channelCode, null) { }
		public LeadChannel(string channelCode, string description) {
			this.channelCode = channelCode;
			this.description = description;
		}

		#region Static Data

		public static LeadChannel CallIn {
			get { return new LeadChannel("CI", "Call In"); }
		}

		public static LeadChannel Fax {
			get { return new LeadChannel("FAX", "Fax"); }
		}

		public static LeadChannel Internet {
			get { return new LeadChannel("INT", "Internet"); }
		}
		
		public static LeadChannel Mail {
			get { return new LeadChannel("MAIL", "Mail"); }
		}
		
		public static LeadChannel Email {
			get { return new LeadChannel("EMAI", "email"); }
		}

		public static LeadChannel List {
			get { return new LeadChannel("LIST", "List"); }
		}

		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<LeadChannel>\r\n" +
			"	<ChannelCode>" + System.Web.HttpUtility.HtmlEncode(channelCode) + "</ChannelCode>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</LeadChannel>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("channelCode")) {
					SetXmlValue(ref channelCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static LeadChannel[] GetLeadChannels() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadChannels();
		}

		
		public static LeadChannel GetLeadChannelByID(string id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadChannelByID(id);
		}
/*
		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertLeadChannel(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateLeadChannel(this);
		}*/
		#endregion

		#region Properties
		public string ChannelCode {
			set { channelCode = value; }
			get { return channelCode; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
