using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class GLTable: EFundraisingCRMDataObject {

		private string gLCode;
		private string description;
		private string gLAccountNo;
		private string debitCredit;


		public GLTable() : this(null) { }
		public GLTable(string gLCode) : this(gLCode, null) { }
		public GLTable(string gLCode, string description) : this(gLCode, description, null) { }
		public GLTable(string gLCode, string description, string gLAccountNo) : this(gLCode, description, gLAccountNo, null) { }
		public GLTable(string gLCode, string description, string gLAccountNo, string debitCredit) {
			this.gLCode = gLCode;
			this.description = description;
			this.gLAccountNo = gLAccountNo;
			this.debitCredit = debitCredit;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<GLTable>\r\n" +
			"	<GLCode>" + System.Web.HttpUtility.HtmlEncode(gLCode) + "</GLCode>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"	<GLAccountNo>" + System.Web.HttpUtility.HtmlEncode(gLAccountNo) + "</GLAccountNo>\r\n" +
			"	<DebitCredit>" + System.Web.HttpUtility.HtmlEncode(debitCredit) + "</DebitCredit>\r\n" +
			"</GLTable>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("glCode")) {
					SetXmlValue(ref gLCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("glAccountNo")) {
					SetXmlValue(ref gLAccountNo, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("debitCredit")) {
					SetXmlValue(ref debitCredit, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static GLTable[] GetGLTables() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetGLTables();
		}

		/*
		public static GLTable GetGLTableByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetGLTableByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertGLTable(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateGLTable(this);
		}*/
		#endregion

		#region Properties
		public string GLCode {
			set { gLCode = value; }
			get { return gLCode; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public string GLAccountNo {
			set { gLAccountNo = value; }
			get { return gLAccountNo; }
		}

		public string DebitCredit {
			set { debitCredit = value; }
			get { return debitCredit; }
		}

		#endregion
	}
}
