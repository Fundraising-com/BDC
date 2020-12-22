using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class TargetedMarketType: EFundraisingCRMDataObject {

		private int targetedMarketTypeId;
		private string description;
		private int decisionMaker;
		private int groupTypeId;
		private string comments;


		public TargetedMarketType() : this(int.MinValue) { }
		public TargetedMarketType(int targetedMarketTypeId) : this(targetedMarketTypeId, null) { }
		public TargetedMarketType(int targetedMarketTypeId, string description) : this(targetedMarketTypeId, description, int.MinValue) { }
		public TargetedMarketType(int targetedMarketTypeId, string description, int decisionMaker) : this(targetedMarketTypeId, description, decisionMaker, int.MinValue) { }
		public TargetedMarketType(int targetedMarketTypeId, string description, int decisionMaker, int groupTypeId) : this(targetedMarketTypeId, description, decisionMaker, groupTypeId, null) { }
		public TargetedMarketType(int targetedMarketTypeId, string description, int decisionMaker, int groupTypeId, string comments) {
			this.targetedMarketTypeId = targetedMarketTypeId;
			this.description = description;
			this.decisionMaker = decisionMaker;
			this.groupTypeId = groupTypeId;
			this.comments = comments;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<TargetedMarketType>\r\n" +
			"	<TargetedMarketTypeId>" + targetedMarketTypeId + "</TargetedMarketTypeId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"	<DecisionMaker>" + decisionMaker + "</DecisionMaker>\r\n" +
			"	<GroupTypeId>" + groupTypeId + "</GroupTypeId>\r\n" +
			"	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
			"</TargetedMarketType>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("targetedMarketTypeId")) {
					SetXmlValue(ref targetedMarketTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("decisionMaker")) {
					SetXmlValue(ref decisionMaker, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("groupTypeId")) {
					SetXmlValue(ref groupTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comments")) {
					SetXmlValue(ref comments, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static TargetedMarketType[] GetTargetedMarketTypes() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTargetedMarketTypes();
		}

		public static TargetedMarketType GetTargetedMarketTypeByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTargetedMarketTypeByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertTargetedMarketType(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateTargetedMarketType(this);
		}
		#endregion

		#region Properties
		public int TargetedMarketTypeId {
			set { targetedMarketTypeId = value; }
			get { return targetedMarketTypeId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public int DecisionMaker {
			set { decisionMaker = value; }
			get { return decisionMaker; }
		}

		public int GroupTypeId {
			set { groupTypeId = value; }
			get { return groupTypeId; }
		}

		public string Comments {
			set { comments = value; }
			get { return comments; }
		}

		#endregion
	}
}
