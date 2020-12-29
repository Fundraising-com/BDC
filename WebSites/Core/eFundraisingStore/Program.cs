using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class Program: eFundraisingStoreDataObject {

		private int programId;
		private string name;
		private DateTime createDate;


		public Program() : this(int.MinValue) { }
		public Program(int programId) : this(programId, null) { }
		public Program(int programId, string name) : this(programId, name, DateTime.MinValue) { }
		public Program(int programId, string name, DateTime createDate) {
			this.programId = programId;
			this.name = name;
			this.createDate = createDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Program>\r\n" +
			"	<ProgramId>" + programId + "</ProgramId>\r\n" +
			"	<Name>" + System.Web.HttpUtility.HtmlEncode(name) + "</Name>\r\n" +
			"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
			"</Program>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "programId") {
					SetXmlValue(ref programId, node.InnerText);
				}
				if(node.Name.ToLower() == "name") {
					SetXmlValue(ref name, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") {
					SetXmlValue(ref createDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Program[] GetPrograms() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPrograms();
		}

		public static Program GetProgramByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProgramByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertProgram(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateProgram(this);
		}
		#endregion

		#region Properties
		public int ProgramId {
			set { programId = value; }
			get { return programId; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion
	}
}
