using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using GA.BDC.Core.ESubsGlobal.DataAccess;

namespace GA.BDC.Core.ESubsGlobal
{
    [Serializable]
    public class PartnerProductOffer
    {
        private int _partnerID;
        private int _productOfferID;

        public PartnerProductOffer() : this(int.MinValue) { }
        public PartnerProductOffer(int partnerID) : this(partnerID, int.MinValue) { }
        public PartnerProductOffer(int partnerID, int productOfferID)
        {
            _partnerID = partnerID;
            _productOfferID = productOfferID;
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
            return "<PartnerProductOffer>\r\n" +
            "	<PartnerID>" + _partnerID + "</PartnerID>\r\n" +
            "	<PartnerOfferID>" + _productOfferID + "</PartnerOfferID>\r\n" +
            "</PartnerProductOffer>\r\n";
        }
        #endregion

        #endregion

        #region Methods
        public static PartnerProductOffer GetPartnerProductOfferByID(Int32 partner_id)
        {
            ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
            return dbo.GetPartnerProductOfferByID(partner_id);
        }
        #endregion

        #region Properties
        public int PartnerID
        {
            set { _partnerID = value; }
            get { return _partnerID; }
        }

        public int ProductOfferID
        {
            set { _productOfferID = value; }
            get { return _productOfferID; }
        }
        #endregion
    }
}
