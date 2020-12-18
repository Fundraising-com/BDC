using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class LeadDuplicatesLogin: EFundraisingCRMDataObject {

		private int lEADDUPLICATESLOGINID;
		private string dUPLICATESFOUND;
		private string rELATEDTABLE;
		private string dETECTEDFIELDS;
		private string fIELDSVALUES;
		private string nTLOGIN;
		private string mACHINE;
		private DateTime tIMESTAMP;


		public LeadDuplicatesLogin() : this(int.MinValue) { }
		public LeadDuplicatesLogin(int lEADDUPLICATESLOGINID) : this(lEADDUPLICATESLOGINID, null) { }
		public LeadDuplicatesLogin(int lEADDUPLICATESLOGINID, string dUPLICATESFOUND) : this(lEADDUPLICATESLOGINID, dUPLICATESFOUND, null) { }
		public LeadDuplicatesLogin(int lEADDUPLICATESLOGINID, string dUPLICATESFOUND, string rELATEDTABLE) : this(lEADDUPLICATESLOGINID, dUPLICATESFOUND, rELATEDTABLE, null) { }
		public LeadDuplicatesLogin(int lEADDUPLICATESLOGINID, string dUPLICATESFOUND, string rELATEDTABLE, string dETECTEDFIELDS) : this(lEADDUPLICATESLOGINID, dUPLICATESFOUND, rELATEDTABLE, dETECTEDFIELDS, null) { }
		public LeadDuplicatesLogin(int lEADDUPLICATESLOGINID, string dUPLICATESFOUND, string rELATEDTABLE, string dETECTEDFIELDS, string fIELDSVALUES) : this(lEADDUPLICATESLOGINID, dUPLICATESFOUND, rELATEDTABLE, dETECTEDFIELDS, fIELDSVALUES, null) { }
		public LeadDuplicatesLogin(int lEADDUPLICATESLOGINID, string dUPLICATESFOUND, string rELATEDTABLE, string dETECTEDFIELDS, string fIELDSVALUES, string nTLOGIN) : this(lEADDUPLICATESLOGINID, dUPLICATESFOUND, rELATEDTABLE, dETECTEDFIELDS, fIELDSVALUES, nTLOGIN, null) { }
		public LeadDuplicatesLogin(int lEADDUPLICATESLOGINID, string dUPLICATESFOUND, string rELATEDTABLE, string dETECTEDFIELDS, string fIELDSVALUES, string nTLOGIN, string mACHINE) : this(lEADDUPLICATESLOGINID, dUPLICATESFOUND, rELATEDTABLE, dETECTEDFIELDS, fIELDSVALUES, nTLOGIN, mACHINE, DateTime.MinValue) { }
		public LeadDuplicatesLogin(int lEADDUPLICATESLOGINID, string dUPLICATESFOUND, string rELATEDTABLE, string dETECTEDFIELDS, string fIELDSVALUES, string nTLOGIN, string mACHINE, DateTime tIMESTAMP) {
			this.lEADDUPLICATESLOGINID = lEADDUPLICATESLOGINID;
			this.dUPLICATESFOUND = dUPLICATESFOUND;
			this.rELATEDTABLE = rELATEDTABLE;
			this.dETECTEDFIELDS = dETECTEDFIELDS;
			this.fIELDSVALUES = fIELDSVALUES;
			this.nTLOGIN = nTLOGIN;
			this.mACHINE = mACHINE;
			this.tIMESTAMP = tIMESTAMP;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<LeadDuplicatesLogin>\r\n" +
			"	<LEADDUPLICATESLOGINID>" + lEADDUPLICATESLOGINID + "</LEADDUPLICATESLOGINID>\r\n" +
			"	<DUPLICATESFOUND>" + System.Web.HttpUtility.HtmlEncode(dUPLICATESFOUND) + "</DUPLICATESFOUND>\r\n" +
			"	<RELATEDTABLE>" + System.Web.HttpUtility.HtmlEncode(rELATEDTABLE) + "</RELATEDTABLE>\r\n" +
			"	<DETECTEDFIELDS>" + System.Web.HttpUtility.HtmlEncode(dETECTEDFIELDS) + "</DETECTEDFIELDS>\r\n" +
			"	<FIELDSVALUES>" + System.Web.HttpUtility.HtmlEncode(fIELDSVALUES) + "</FIELDSVALUES>\r\n" +
			"	<NTLOGIN>" + System.Web.HttpUtility.HtmlEncode(nTLOGIN) + "</NTLOGIN>\r\n" +
			"	<MACHINE>" + System.Web.HttpUtility.HtmlEncode(mACHINE) + "</MACHINE>\r\n" +
			"	<TIMESTAMP>" + tIMESTAMP + "</TIMESTAMP>\r\n" +
			"</LeadDuplicatesLogin>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("leadDuplicatesLoginId")) {
					SetXmlValue(ref lEADDUPLICATESLOGINID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("duplicatesFound")) {
					SetXmlValue(ref dUPLICATESFOUND, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("relatedTable")) {
					SetXmlValue(ref rELATEDTABLE, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("detectedFields")) {
					SetXmlValue(ref dETECTEDFIELDS, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("fieldsValues")) {
					SetXmlValue(ref fIELDSVALUES, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("ntLogin")) {
					SetXmlValue(ref nTLOGIN, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("machine")) {
					SetXmlValue(ref mACHINE, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("timeStamp")) {
					SetXmlValue(ref tIMESTAMP, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static LeadDuplicatesLogin[] GetLeadDuplicatesLogins() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadDuplicatesLogins();
		}

		public static LeadDuplicatesLogin GetLeadDuplicatesLoginByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadDuplicatesLoginByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertLeadDuplicatesLogin(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateLeadDuplicatesLogin(this);
		}
		#endregion

		#region Properties
		public int LEADDUPLICATESLOGINID {
			set { lEADDUPLICATESLOGINID = value; }
			get { return lEADDUPLICATESLOGINID; }
		}

		public string DUPLICATESFOUND {
			set { dUPLICATESFOUND = value; }
			get { return dUPLICATESFOUND; }
		}

		public string RELATEDTABLE {
			set { rELATEDTABLE = value; }
			get { return rELATEDTABLE; }
		}

		public string DETECTEDFIELDS {
			set { dETECTEDFIELDS = value; }
			get { return dETECTEDFIELDS; }
		}

		public string FIELDSVALUES {
			set { fIELDSVALUES = value; }
			get { return fIELDSVALUES; }
		}

		public string NTLOGIN {
			set { nTLOGIN = value; }
			get { return nTLOGIN; }
		}

		public string MACHINE {
			set { mACHINE = value; }
			get { return mACHINE; }
		}

		public DateTime TIMESTAMP {
			set { tIMESTAMP = value; }
			get { return tIMESTAMP; }
		}

		#endregion
	}
}
