using System;
using System.Collections.Generic;
using System.Xml;
using GA.BDC.Core.BusinessBase;

namespace GA.BDC.Core.eFundraisingCommon
{
    /// <summary>
    /// Summary description for PartnerProfit.
    /// </summary>
    [Serializable]
    public class PartnerProfit : BusinessBase.BusinessBase
    {
        private int _partnerProfitId;
        private int _partnerId;
        private int _profitGroupId;
        private DateTime _startDate;
        private DateTime _endDate;

        public PartnerProfit() : this(int.MinValue) { }
        public PartnerProfit(int partnerProfitId) : this(partnerProfitId, int.MinValue) { }
        public PartnerProfit(int partnerProfitId, int partnerId) : this(partnerProfitId, partnerId, int.MinValue) { }
        public PartnerProfit(int partnerProfitId, int partnerId, int profitGroupId) : this(partnerProfitId, partnerId, profitGroupId, DateTime.MinValue) { }
        public PartnerProfit(int partnerProfitId, int partnerId, int profitGroupId, DateTime startDate) : this(partnerProfitId, partnerId, profitGroupId, startDate, DateTime.MinValue) { }
        public PartnerProfit(int partnerProfitId, int partnerId, int profitGroupId, DateTime startDate, DateTime endDate)
        {
            _partnerProfitId = partnerProfitId;
            _partnerId = partnerId;
            _profitGroupId = profitGroupId;
            _startDate = startDate;
            _endDate = endDate;
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
            return "<PartnerProfit>\r\n" +
            "	<PartnerProfitId>" + _partnerProfitId + "</PartnerProfitId>\r\n" +
            "	<PartnerId>" + _partnerId + "</PartnerId>\r\n" +
            "	<ProfitGroupId>" + _profitGroupId + "</ProfitGroupId>\r\n" +
            "	<StartDate>" + _startDate + "</StartDate>\r\n" +
            "	<EndDate>" + _endDate + "</EndDate>\r\n" +
            "</PartnerProfit>\r\n";
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

        private void SetXmlValue(ref Decimal obj, string val)
        {
            if (val == "") { obj = Decimal.MinValue; return; }
            obj = Decimal.Parse(val);
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
                if (node.Name.ToLower() == "partnerprofitid")
                {
                    SetXmlValue(ref _partnerProfitId, node.InnerText);
                }
                if (node.Name.ToLower() == "partnerid")
                {
                    SetXmlValue(ref _partnerId, node.InnerText);
                }
                if (node.Name.ToLower() == "profitgroupid")
                {
                    SetXmlValue(ref _profitGroupId, node.InnerText);
                }
                if (node.Name.ToLower() == "startdate")
                {
                    SetXmlValue(ref _startDate, node.InnerText);
                }
                if (node.Name.ToLower() == "enddate")
                {
                    SetXmlValue(ref _endDate, node.InnerText);
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
        public static List<PartnerProfit> GetPartnerProfits()
        {
            DataAccess.EFRCommonDatabase dbo = new DataAccess.EFRCommonDatabase();
            return dbo.GetPartnerProfits();
        }

        public static List<PartnerProfit> GetPartnerProfitsByID(int id)
        {
            DataAccess.EFRCommonDatabase dbo = new DataAccess.EFRCommonDatabase();
            return dbo.GetPartnerProfitsByID(id);
        }

        public static List<PartnerProfit> GetCurrentPartnerProfits()
        {
            DataAccess.EFRCommonDatabase dbo = new DataAccess.EFRCommonDatabase();
            return dbo.GetCurrentPartnerProfits();
        }

        public static PartnerProfit GetCurrentPartnerProfitByID(int id)
        {
            DataAccess.EFRCommonDatabase dbo = new DataAccess.EFRCommonDatabase();
            return dbo.GetCurrentPartnerProfitByID(id);
        }
        #endregion

        #endregion

        #region Properties
        public int PartnerProfitId
        {
            set { _partnerProfitId = value; }
            get { return _partnerProfitId; }
        }

        public int PartnerId
        {
            set { _partnerId = value; }
            get { return _partnerId; }
        }

        public DateTime StartDate
        {
            set { _startDate = value; }
            get { return _startDate; }
        }

        public DateTime EndDate
        {
            set { _endDate = value; }
            get { return _endDate; }
        }

        public int ProfitGroupID
        {
            set { _profitGroupId = value; }
            get { return _profitGroupId; }
        }

        #endregion
    }
}
