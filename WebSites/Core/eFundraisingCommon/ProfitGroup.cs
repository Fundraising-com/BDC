using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using GA.BDC.Core.BusinessBase;

namespace GA.BDC.Core.eFundraisingCommon
{
    [Serializable]
    public class ProfitGroup
    {
        private int _ProfitGroup_id;
		private string _description;
		private string _disclaimer;
		private string _alt_disclaimer;


		public ProfitGroup() : this(int.MinValue) { }
		public ProfitGroup(int ProfitGroup_id) : this(ProfitGroup_id, null) { }
		public ProfitGroup(int ProfitGroup_id, string description) : this(ProfitGroup_id, description, null) { }
		public ProfitGroup(int ProfitGroup_id, string description, string disclaimer) : this(ProfitGroup_id, description, disclaimer, null) { }
		public ProfitGroup(int ProfitGroup_id, string description, string disclaimer, string alt_disclaimer) {
			_ProfitGroup_id = ProfitGroup_id;
			_description = description;
			_disclaimer = disclaimer;
			_alt_disclaimer = alt_disclaimer;
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
			return "<ProfitGroup>\r\n" +
			"	<ProfitGroup_id>" + _ProfitGroup_id + "</ProfitGroup_id>\r\n" +
			"	<Description>" + _description + "</Description>\r\n" +
			"	<Disclaimer>" + _disclaimer + "</Disclaimer>\r\n" +
			"	<Alt_disclaimer>" + _alt_disclaimer + "</Alt_disclaimer>\r\n" +
			"</ProfitGroup>\r\n";
		}
		#endregion

		#region Set Xml Values
		private void SetXmlValue(ref int obj, string val) {
			if(val == "") { obj = int.MinValue; return; }
			obj = int.Parse(val);
		}

		private void SetXmlValue(ref string obj, string val) {
			if(val == "") { obj = null; return; }
			obj = val;
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
				if(node.Name.ToLower() == "ProfitGroup_id") {
					SetXmlValue(ref _ProfitGroup_id, node.InnerText);
				}
				if(node.Name.ToLower() == "description") {
					SetXmlValue(ref _description, node.InnerText);
				}
				if(node.Name.ToLower() == "disclaimer") {
					SetXmlValue(ref _disclaimer, node.InnerText);
				}
				if(node.Name.ToLower() == "alt_disclaimer") {
					SetXmlValue(ref _alt_disclaimer, node.InnerText);
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

		#region Properties
		public int ProfitGroup_id {
			set { _ProfitGroup_id = value; }
			get { return _ProfitGroup_id; }
		}

		public string Description {
			set { _description = value; }
			get { return _description; }
		}

		public string Disclaimer {
			set { _disclaimer = value; }
			get { return _disclaimer; }
		}

		public string AltDisclaimer {
			set { _alt_disclaimer = value; }
			get { return _alt_disclaimer; }
		}

		#endregion


        #region Data Source Methods


        //public static ProfitGroup GetProfitGroupByParnerID(int id)
        //{
        //    ProfitGroup profitgroup = null;
        //    List<PartnerProfit> listPP = PartnerProfit.GetPartnerProfitsByID(id);
        //     if (listPP.Count > 0)
        //     {
        //        profitgroup = ProfitGroup.GetProfitGroupByID(listPP[0].ProfitGroupID);

        //     }
        //     return profitgroup;
        //}

        public static ProfitGroup GetProfitGroupByID(int id)
        {
            DataAccess.EFRCommonDatabase dbo = new DataAccess.EFRCommonDatabase();
            return dbo.GetProfitGroupByID(id,null);
        }

        
        #endregion
	}
}

   
