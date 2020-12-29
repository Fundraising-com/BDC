using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using GA.BDC.Core.ESubsGlobal.DataAccess;

namespace GA.BDC.Core.ESubsGlobal
{
    [Serializable]
    public class ProductOffer
    {
        private int _productOfferID = int.MinValue;
        private string _description;

        public enum ESubs
        {
            ALL = 1, MAG_ONLY, MAG_RESTO, RESTO_MAG, DONATION_ONLY, MAG_AND_MORE, BOXTOPS
        }

        public const string AllProduct = "ALL";
        public const string MagOnlyProduct = "MAG-ONLY";
        public const string MagRestoProduct = "MAG-RESTO";
        public const string RestoMagProduct = "RESTO-MAG";
        public const string DonationOnlyProduct = "DONATION-ONLY";
        public const string MagAndMore = "MAG & MORE";
        public const string BoxTops = "BOXTOPS";

        public ProductOffer() : this(int.MinValue) { }
        public ProductOffer(int productOfferID) : this(productOfferID, null) { }
        public ProductOffer(int productOfferID, string description)
        {
            _productOfferID = productOfferID;
            _description = description;
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
            return "<ProductOffer>\r\n" +
            "	<ProductOfferID>" + _productOfferID + "</ProductOfferID>\r\n" +
            "	<Description>" + _description + "</Description>\r\n" +
            "</ProductOffer>\r\n";
        }
        #endregion

        #endregion

        #region Methods
        public static ProductOffer GetProductOfferByID(Int32 product_offer_id)
        {
            ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
            return dbo.GetProductOfferByID(product_offer_id);
        }
        #endregion

        #region Properties
        public int ProductOfferID
        {
            set { _productOfferID = value; }
            get { return _productOfferID; }
        }

        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        #endregion
    }
}
