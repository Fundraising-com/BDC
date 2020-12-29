using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using GA.BDC.Core.ESubsGlobal.DataAccess;

namespace GA.BDC.Core.ESubsGlobal.Touch
{
    [Serializable]
    public class EmailTemplateTag
    {
        private int _emailTemplateID;
        private int _productOfferID;
        private int _tagID;

        public EmailTemplateTag() : this(int.MinValue) { }
        public EmailTemplateTag(int emailTemplateID) : this(emailTemplateID, int.MinValue) { }
        public EmailTemplateTag(int emailTemplateID, int productOfferID) : this(emailTemplateID, productOfferID, int.MinValue) { }
        public EmailTemplateTag(int emailTemplateID, int productOfferID, int tagID)
        {
            _emailTemplateID = emailTemplateID;
            _productOfferID = productOfferID;
            _tagID = tagID;
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
            return "<EmailTemplateTag>\r\n" +
            "	<EmailTemplateID>" + _emailTemplateID + "</EmailTemplateID>\r\n" +
            "	<ProductOfferID>" + _productOfferID + "</ProductOfferID>\r\n" +
            "	<TagID>" + _tagID + "</TagID>\r\n" +
            "</EmailTemplateTag>\r\n";
        }
        #endregion

        #endregion

        #region Methods
        public static List<EmailTemplateTag> GetEmailTemplateTags(Int32 email_template_id, Int32 product_offer_id)
        {
            ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
            return dbo.GetEmailTemplateTags(email_template_id, product_offer_id);
        }
        #endregion

        #region Properties
        public int EmailTemplateID
        {
            set { _emailTemplateID = value; }
            get { return _emailTemplateID; }
        }

        public int ProductOfferID
        {
            set { _productOfferID = value; }
            get { return _productOfferID; }
        }

        public int TagID
        {
            set { _tagID = value; }
            get { return _tagID; }
        }

        #endregion
    }
}
