using System;
using System.Collections.Generic;
using System.Xml;
using GA.BDC.Core.BusinessBase;

namespace GA.BDC.Core.eFundraisingCommon
{
    /// <summary>
    /// Summary description for Profit.
    /// </summary>
    [Serializable]
    public class Profit : BusinessBase.BusinessBase
    {
        private int _profitId;
        private double _profitPercentage;
        private string _description;
        private string _disclaimer;
        private string _alt_disclaimer;

        public Profit() : this(int.MinValue) { }
        public Profit(int profitId) : this(profitId, double.MinValue) { }
        public Profit(int profitId, double profitPercentage) : this(profitId, profitPercentage, null) { }
        public Profit(int profitId, double profitPercentage, string description) : this(profitId, profitPercentage, description, null) { }
        public Profit(int profitId, double profitPercentage, string description, string disclaimer) : this(profitId, profitPercentage, description, disclaimer, null) { }
        public Profit(int profitId, double profitPercentage, string description, string disclaimer, string alt_disclaimer)
        {
            _profitId = profitId;
            _profitPercentage = profitPercentage;
            _description = description;
            _disclaimer = disclaimer;
            _alt_disclaimer = alt_disclaimer;
        }


        #region XML Methods

        #region Save XML
        private string IdentXML(string xml)
        {
            string newXML = "";
            string[] lines = xml.Split('\r');
            foreach (string strXml in lines)
            {
                if (strXml.Trim() == "")
                    break;
                newXML += "\t" + strXml.Replace("\n", "") + "\r\n";
            }
            return newXML;
        }

        public virtual string GenerateXML()
        {
            return "<Profit>\r\n" +
            "	<ProfitId>" + _profitId + "</ProfitId>\r\n" +
            "	<ProfitPercentage>" + _profitPercentage + "</ProfitPercentage>\r\n" +
            "	<Description>" + _description + "</Description>\r\n" +
            "	<Disclaimer>" + _disclaimer + "</Disclaimer>\r\n" +
            "</Profit>\r\n";
        }
        #endregion

        #region Set Xml Values
        private void SetXmlValue(ref int obj, string val)
        {
            if (val == "") { obj = int.MinValue; return; }
            obj = int.Parse(val);
        }

        private void SetXmlValue(ref string obj, string val)
        {
            if (val == "") { obj = null; return; }
            obj = val;
        }

        private void SetXmlValue(ref bool obj, string val)
        {
            if (val == "") { obj = false; return; }
            obj = (val.ToLower() == "true");
        }

        private void SetXmlValue(ref double obj, string val)
        {
            if (val == "") { obj = double.MinValue; return; }
            obj = double.Parse(val);
        }

        private void SetXmlValue(ref DateTime obj, string val)
        {
            if (val == "") { obj = DateTime.MinValue; return; }
            obj = DateTime.Parse(val);
        }

        #endregion

        #region Load Methods
        // loop through the xml and set the properties and child classes
        public virtual void Load(XmlNode childNodes)
        {
            foreach (XmlNode node in childNodes)
            {
                if (node.Name.ToLower() == "profitid")
                {
                    SetXmlValue(ref _profitId, node.InnerText);
                }
                if (node.Name.ToLower() == "profitpercentage")
                {
                    SetXmlValue(ref _profitPercentage, node.InnerText);
                }
                if (node.Name.ToLower() == "description")
                {
                    SetXmlValue(ref _description, node.InnerText);
                }
                if (node.Name.ToLower() == "disclaimer")
                {
                    SetXmlValue(ref _disclaimer, node.InnerText);
                }
            }
        }
        // load from an xml string 
        public virtual void LoadXml(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            foreach (XmlNode node in doc.ChildNodes)
            {
                Load(node);
            }
        }

        // load from an xml document object
        public virtual void Load(System.Xml.XmlDocument doc)
        {
            foreach (XmlNode node in doc.ChildNodes)
            {
                Load(node);
            }
        }

        // load from a stream
        public virtual void Load(System.IO.Stream inStream)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(inStream);

            foreach (XmlNode node in doc.ChildNodes)
            {
                Load(node);
            }
        }

        // load from a text reader
        public virtual void Load(System.IO.TextReader txtReader)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(txtReader);

            foreach (XmlNode node in doc.ChildNodes)
            {
                Load(node);
            }
        }

        // load from an xml reader
        public virtual void Load(System.Xml.XmlReader reader)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);

            foreach (XmlNode node in doc.ChildNodes)
            {
                Load(node);
            }
        }

        // load from a xml filename
        public virtual void Load(string filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            foreach (XmlNode node in doc.ChildNodes)
            {
                Load(node);
            }
        }

        #endregion

        #region Data Source Methods
        public static List<Profit> GetProfits()
        {
            DataAccess.EFRCommonDatabase dbo = new DataAccess.EFRCommonDatabase();
            return dbo.GetProfits();
        }

        public static Profit GetProfitByID(int id)
        {
            DataAccess.EFRCommonDatabase dbo = new DataAccess.EFRCommonDatabase();
            return dbo.GetProfitByID(id);
        }

        public static List<Profit> GetProfitByProfitGroupID(int id)
        {
            DataAccess.EFRCommonDatabase dbo = new DataAccess.EFRCommonDatabase();
            return dbo.GetProfitByProfitGroupID(id);
        }

        public static Dictionary<int, Profit> GetProfitDictionary()
        {
            Dictionary<int, Profit> dic_list = new Dictionary<int, Profit>();
            foreach (Profit p in GetProfits())
            {
                dic_list.Add(p.ProfitId, p);
            }
            return dic_list;
        }
        #endregion

        #endregion

        #region Properties
        public int ProfitId
        {
            set { _profitId = value; }
            get { return _profitId; }
        }

        public double ProfitPercentage
        {
            set { _profitPercentage = value; }
            get { return _profitPercentage; }
        }

        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }

        public string Disclaimer
        {
            set { _disclaimer = value; }
            get { return _disclaimer; }
        }

        public string AltDisclaimer
        {
            set { _alt_disclaimer = value; }
            get { return _alt_disclaimer; }
        }

        public int ProfitGroupID { get;set; }
        public int QspCatalogTypeID { get; set; }

        #endregion
    }
}
