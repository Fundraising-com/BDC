using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class QSPProgram: EFundraisingCRMDataObject {

		private int qSPProgramID;
		private string description;
		private string baseDirectory;


		public QSPProgram() : this(int.MinValue) { }
		public QSPProgram(int qSPProgramID) : this(qSPProgramID, null) { }
		public QSPProgram(int qSPProgramID, string description) : this(qSPProgramID, description, null) { }
		public QSPProgram(int qSPProgramID, string description, string baseDirectory) {
			this.qSPProgramID = qSPProgramID;
			this.description = description;
			this.baseDirectory = baseDirectory;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<QSPProgram>\r\n" +
			"	<QSPProgramID>" + qSPProgramID + "</QSPProgramID>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"	<BaseDirectory>" + System.Web.HttpUtility.HtmlEncode(baseDirectory) + "</BaseDirectory>\r\n" +
			"</QSPProgram>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("qspProgramId")) {
					SetXmlValue(ref qSPProgramID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("baseDirectory")) {
					SetXmlValue(ref baseDirectory, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static QSPProgram[] GetQSPPrograms() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetQSPPrograms();
		}

		public static QSPProgram GetQSPProgramByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetQSPProgramByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertQSPProgram(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateQSPProgram(this);
		}
		#endregion

		#region Properties
		public int QSPProgramID {
			set { qSPProgramID = value; }
			get { return qSPProgramID; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public string BaseDirectory {
			set { baseDirectory = value; }
			get { return baseDirectory; }
		}

		#endregion
	}
}
