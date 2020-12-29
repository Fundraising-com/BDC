using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace GA.BDC.Core.eFundraisingCommon
{
    public class ProfitRange 
    {

		private int _profitRangeID;
		private int _profitID;
		private double _profitRangePercentage;
		private int _minSub;
		private int _minAmount;
		private string _operator;
		private string _disclaimer;


		public ProfitRange() : this(int.MinValue) { }
		public ProfitRange(int profitRangeID) : this(profitRangeID, int.MinValue) { }
		public ProfitRange(int profitRangeID, int profitID) : this(profitRangeID, profitID, double.MinValue) { }
		public ProfitRange(int profitRangeID, int profitID, double profitRangePercentage) : this(profitRangeID, profitID, profitRangePercentage, int.MinValue) { }
		public ProfitRange(int profitRangeID, int profitID, double profitRangePercentage, int minSub) : this(profitRangeID, profitID, profitRangePercentage, minSub, int.MinValue) { }
		public ProfitRange(int profitRangeID, int profitID, double profitRangePercentage, int minSub, int minAmount) : this(profitRangeID, profitID, profitRangePercentage, minSub, minAmount, null) { }
		public ProfitRange(int profitRangeID, int profitID, double profitRangePercentage, int minSub, int minAmount, string oper) : this(profitRangeID, profitID, profitRangePercentage, minSub, minAmount, oper, null) { }
		public ProfitRange(int profitRangeID, int profitID, double profitRangePercentage, int minSub, int minAmount, string oper, string disclaimer) {
			_profitRangeID = profitRangeID;
			_profitID = profitID;
			_profitRangePercentage = profitRangePercentage;
			_minSub = minSub;
			_minAmount = minAmount;
			_operator = oper;
			_disclaimer = disclaimer;
        }

        #region DAL Methods
        public static List<ProfitRange> GetProfitRange()
        {
            DataAccess.EFRCommonDatabase dbo = new GA.BDC.Core.eFundraisingCommon.DataAccess.EFRCommonDatabase();
            return dbo.GetProfitRanges();
        }

        public static List<ProfitRange> GetProfitRangeByProfitID(int profitID)
        {
            DataAccess.EFRCommonDatabase dbo = new GA.BDC.Core.eFundraisingCommon.DataAccess.EFRCommonDatabase();
            return dbo.GetProfitRangeByProfitID(profitID);
        }
        #endregion

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
			return "<ProfitRange>\r\n" +
			"	<ProfitRangeID>" + _profitRangeID + "</ProfitRangeID>\r\n" +
			"	<ProfitID>" + _profitID + "</ProfitID>\r\n" +
			"	<ProfitRangePercentage>" + _profitRangePercentage + "</ProfitRangePercentage>\r\n" +
			"	<MinSub>" + _minSub + "</MinSub>\r\n" +
			"	<MinAmount>" + _minAmount + "</MinAmount>\r\n" +
			"	<Operator>" + _operator + "</Operator>\r\n" +
			"	<Disclaimer>" + _disclaimer + "</Disclaimer>\r\n" +
			"</ProfitRange>\r\n";
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

        private void SetXmlValue(ref Double obj, string val)
        {
            if (val == "") { obj = Double.MinValue; return; }
            obj = Double.Parse(val);
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
				if(node.Name.ToLower() == "profitrangeid") {
					SetXmlValue(ref _profitRangeID, node.InnerText);
				}
				if(node.Name.ToLower() == "profitid") {
					SetXmlValue(ref _profitID, node.InnerText);
				}
				if(node.Name.ToLower() == "profitrangepercentage") {
					SetXmlValue(ref _profitRangePercentage, node.InnerText);
				}
				if(node.Name.ToLower() == "minsub") {
					SetXmlValue(ref _minSub, node.InnerText);
				}
				if(node.Name.ToLower() == "minamount") {
					SetXmlValue(ref _minAmount, node.InnerText);
				}
				if(node.Name.ToLower() == "operator") {
					SetXmlValue(ref _operator, node.InnerText);
				}
				if(node.Name.ToLower() == "disclaimer") {
					SetXmlValue(ref _disclaimer, node.InnerText);
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
		public int ProfitRangeID {
			set { _profitRangeID = value; }
			get { return _profitRangeID; }
		}

		public int ProfitID {
			set { _profitID = value; }
			get { return _profitID; }
		}

		public double ProfitRangePercentage {
			set { _profitRangePercentage = value; }
			get { return _profitRangePercentage; }
		}

		public int MinSub {
			set { _minSub = value; }
			get { return _minSub; }
		}

		public int MinAmount {
			set { _minAmount = value; }
			get { return _minAmount; }
		}

		public string Operator {
			set { _operator = value; }
			get { return _operator; }
		}

		public string Disclaimer {
			set { _disclaimer = value; }
			get { return _disclaimer; }
		}

		#endregion
	}
}
