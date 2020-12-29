using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal.QSPECommerce
{
    public class Cart : EnvironmentBase
    {
        private int cart_Id;
        private int x_Catalog_Group_Id;
        private int online_Participant_Id;
        private int site_Id;
        private int billing_X_Postal_Address_Id;
        private int billing_X_Phone_Number_Id;
        private string email;
        private string cC_Type;
        private int cC_Exp_Month;
        private int cC_Exp_Year;
        private string cC_Name_On_Card;
        private int x_Order_Id;
        private string cart_GUID;
        private int x_Credit_Card_Id;
        private int eDS_Order_Id;
        private DateTime create_Date;
        private DateTime modify_Date;
        private string modified_By;
        private int deleted_TF;
        private decimal price_Applied;
        private int template_ID;
        private int isOrderExportable;


        public Cart() : this(int.MinValue) { }
        public Cart(int cart_Id) : this(cart_Id, int.MinValue) { }
        public Cart(int cart_Id, int x_Catalog_Group_Id) : this(cart_Id, x_Catalog_Group_Id, int.MinValue) { }
        public Cart(int cart_Id, int x_Catalog_Group_Id, int online_Participant_Id) : this(cart_Id, x_Catalog_Group_Id, online_Participant_Id, int.MinValue) { }
        public Cart(int cart_Id, int x_Catalog_Group_Id, int online_Participant_Id, int site_Id) : this(cart_Id, x_Catalog_Group_Id, online_Participant_Id, site_Id, int.MinValue) { }
        public Cart(int cart_Id, int x_Catalog_Group_Id, int online_Participant_Id, int site_Id, int billing_X_Postal_Address_Id) : this(cart_Id, x_Catalog_Group_Id, online_Participant_Id, site_Id, billing_X_Postal_Address_Id, int.MinValue) { }
        public Cart(int cart_Id, int x_Catalog_Group_Id, int online_Participant_Id, int site_Id, int billing_X_Postal_Address_Id, int billing_X_Phone_Number_Id) : this(cart_Id, x_Catalog_Group_Id, online_Participant_Id, site_Id, billing_X_Postal_Address_Id, billing_X_Phone_Number_Id, null) { }
        public Cart(int cart_Id, int x_Catalog_Group_Id, int online_Participant_Id, int site_Id, int billing_X_Postal_Address_Id, int billing_X_Phone_Number_Id, string email) : this(cart_Id, x_Catalog_Group_Id, online_Participant_Id, site_Id, billing_X_Postal_Address_Id, billing_X_Phone_Number_Id, email, null) { }
        public Cart(int cart_Id, int x_Catalog_Group_Id, int online_Participant_Id, int site_Id, int billing_X_Postal_Address_Id, int billing_X_Phone_Number_Id, string email, string cC_Type) : this(cart_Id, x_Catalog_Group_Id, online_Participant_Id, site_Id, billing_X_Postal_Address_Id, billing_X_Phone_Number_Id, email, cC_Type, int.MinValue) { }
        public Cart(int cart_Id, int x_Catalog_Group_Id, int online_Participant_Id, int site_Id, int billing_X_Postal_Address_Id, int billing_X_Phone_Number_Id, string email, string cC_Type, int cC_Exp_Month) : this(cart_Id, x_Catalog_Group_Id, online_Participant_Id, site_Id, billing_X_Postal_Address_Id, billing_X_Phone_Number_Id, email, cC_Type, cC_Exp_Month, int.MinValue) { }
        public Cart(int cart_Id, int x_Catalog_Group_Id, int online_Participant_Id, int site_Id, int billing_X_Postal_Address_Id, int billing_X_Phone_Number_Id, string email, string cC_Type, int cC_Exp_Month, int cC_Exp_Year) : this(cart_Id, x_Catalog_Group_Id, online_Participant_Id, site_Id, billing_X_Postal_Address_Id, billing_X_Phone_Number_Id, email, cC_Type, cC_Exp_Month, cC_Exp_Year, null) { }
        public Cart(int cart_Id, int x_Catalog_Group_Id, int online_Participant_Id, int site_Id, int billing_X_Postal_Address_Id, int billing_X_Phone_Number_Id, string email, string cC_Type, int cC_Exp_Month, int cC_Exp_Year, string cC_Name_On_Card) : this(cart_Id, x_Catalog_Group_Id, online_Participant_Id, site_Id, billing_X_Postal_Address_Id, billing_X_Phone_Number_Id, email, cC_Type, cC_Exp_Month, cC_Exp_Year, cC_Name_On_Card, int.MinValue) { }
        public Cart(int cart_Id, int x_Catalog_Group_Id, int online_Participant_Id, int site_Id, int billing_X_Postal_Address_Id, int billing_X_Phone_Number_Id, string email, string cC_Type, int cC_Exp_Month, int cC_Exp_Year, string cC_Name_On_Card, int x_Order_Id) : this(cart_Id, x_Catalog_Group_Id, online_Participant_Id, site_Id, billing_X_Postal_Address_Id, billing_X_Phone_Number_Id, email, cC_Type, cC_Exp_Month, cC_Exp_Year, cC_Name_On_Card, x_Order_Id, null) { }
        public Cart(int cart_Id, int x_Catalog_Group_Id, int online_Participant_Id, int site_Id, int billing_X_Postal_Address_Id, int billing_X_Phone_Number_Id, string email, string cC_Type, int cC_Exp_Month, int cC_Exp_Year, string cC_Name_On_Card, int x_Order_Id, string cart_GUID) : this(cart_Id, x_Catalog_Group_Id, online_Participant_Id, site_Id, billing_X_Postal_Address_Id, billing_X_Phone_Number_Id, email, cC_Type, cC_Exp_Month, cC_Exp_Year, cC_Name_On_Card, x_Order_Id, cart_GUID, int.MinValue) { }
        public Cart(int cart_Id, int x_Catalog_Group_Id, int online_Participant_Id, int site_Id, int billing_X_Postal_Address_Id, int billing_X_Phone_Number_Id, string email, string cC_Type, int cC_Exp_Month, int cC_Exp_Year, string cC_Name_On_Card, int x_Order_Id, string cart_GUID, int x_Credit_Card_Id) : this(cart_Id, x_Catalog_Group_Id, online_Participant_Id, site_Id, billing_X_Postal_Address_Id, billing_X_Phone_Number_Id, email, cC_Type, cC_Exp_Month, cC_Exp_Year, cC_Name_On_Card, x_Order_Id, cart_GUID, x_Credit_Card_Id, int.MinValue) { }
        public Cart(int cart_Id, int x_Catalog_Group_Id, int online_Participant_Id, int site_Id, int billing_X_Postal_Address_Id, int billing_X_Phone_Number_Id, string email, string cC_Type, int cC_Exp_Month, int cC_Exp_Year, string cC_Name_On_Card, int x_Order_Id, string cart_GUID, int x_Credit_Card_Id, int eDS_Order_Id) : this(cart_Id, x_Catalog_Group_Id, online_Participant_Id, site_Id, billing_X_Postal_Address_Id, billing_X_Phone_Number_Id, email, cC_Type, cC_Exp_Month, cC_Exp_Year, cC_Name_On_Card, x_Order_Id, cart_GUID, x_Credit_Card_Id, eDS_Order_Id, DateTime.MinValue) { }
        public Cart(int cart_Id, int x_Catalog_Group_Id, int online_Participant_Id, int site_Id, int billing_X_Postal_Address_Id, int billing_X_Phone_Number_Id, string email, string cC_Type, int cC_Exp_Month, int cC_Exp_Year, string cC_Name_On_Card, int x_Order_Id, string cart_GUID, int x_Credit_Card_Id, int eDS_Order_Id, DateTime create_Date) : this(cart_Id, x_Catalog_Group_Id, online_Participant_Id, site_Id, billing_X_Postal_Address_Id, billing_X_Phone_Number_Id, email, cC_Type, cC_Exp_Month, cC_Exp_Year, cC_Name_On_Card, x_Order_Id, cart_GUID, x_Credit_Card_Id, eDS_Order_Id, create_Date, DateTime.MinValue) { }
        public Cart(int cart_Id, int x_Catalog_Group_Id, int online_Participant_Id, int site_Id, int billing_X_Postal_Address_Id, int billing_X_Phone_Number_Id, string email, string cC_Type, int cC_Exp_Month, int cC_Exp_Year, string cC_Name_On_Card, int x_Order_Id, string cart_GUID, int x_Credit_Card_Id, int eDS_Order_Id, DateTime create_Date, DateTime modify_Date) : this(cart_Id, x_Catalog_Group_Id, online_Participant_Id, site_Id, billing_X_Postal_Address_Id, billing_X_Phone_Number_Id, email, cC_Type, cC_Exp_Month, cC_Exp_Year, cC_Name_On_Card, x_Order_Id, cart_GUID, x_Credit_Card_Id, eDS_Order_Id, create_Date, modify_Date, null) { }
        public Cart(int cart_Id, int x_Catalog_Group_Id, int online_Participant_Id, int site_Id, int billing_X_Postal_Address_Id, int billing_X_Phone_Number_Id, string email, string cC_Type, int cC_Exp_Month, int cC_Exp_Year, string cC_Name_On_Card, int x_Order_Id, string cart_GUID, int x_Credit_Card_Id, int eDS_Order_Id, DateTime create_Date, DateTime modify_Date, string modified_By) : this(cart_Id, x_Catalog_Group_Id, online_Participant_Id, site_Id, billing_X_Postal_Address_Id, billing_X_Phone_Number_Id, email, cC_Type, cC_Exp_Month, cC_Exp_Year, cC_Name_On_Card, x_Order_Id, cart_GUID, x_Credit_Card_Id, eDS_Order_Id, create_Date, modify_Date, modified_By, int.MinValue) { }
        public Cart(int cart_Id, int x_Catalog_Group_Id, int online_Participant_Id, int site_Id, int billing_X_Postal_Address_Id, int billing_X_Phone_Number_Id, string email, string cC_Type, int cC_Exp_Month, int cC_Exp_Year, string cC_Name_On_Card, int x_Order_Id, string cart_GUID, int x_Credit_Card_Id, int eDS_Order_Id, DateTime create_Date, DateTime modify_Date, string modified_By, int deleted_TF) : this(cart_Id, x_Catalog_Group_Id, online_Participant_Id, site_Id, billing_X_Postal_Address_Id, billing_X_Phone_Number_Id, email, cC_Type, cC_Exp_Month, cC_Exp_Year, cC_Name_On_Card, x_Order_Id, cart_GUID, x_Credit_Card_Id, eDS_Order_Id, create_Date, modify_Date, modified_By, deleted_TF, decimal.MinValue) { }
        public Cart(int cart_Id, int x_Catalog_Group_Id, int online_Participant_Id, int site_Id, int billing_X_Postal_Address_Id, int billing_X_Phone_Number_Id, string email, string cC_Type, int cC_Exp_Month, int cC_Exp_Year, string cC_Name_On_Card, int x_Order_Id, string cart_GUID, int x_Credit_Card_Id, int eDS_Order_Id, DateTime create_Date, DateTime modify_Date, string modified_By, int deleted_TF, decimal price_Applied) : this(cart_Id, x_Catalog_Group_Id, online_Participant_Id, site_Id, billing_X_Postal_Address_Id, billing_X_Phone_Number_Id, email, cC_Type, cC_Exp_Month, cC_Exp_Year, cC_Name_On_Card, x_Order_Id, cart_GUID, x_Credit_Card_Id, eDS_Order_Id, create_Date, modify_Date, modified_By, deleted_TF, price_Applied, int.MinValue) { }
        public Cart(int cart_Id, int x_Catalog_Group_Id, int online_Participant_Id, int site_Id, int billing_X_Postal_Address_Id, int billing_X_Phone_Number_Id, string email, string cC_Type, int cC_Exp_Month, int cC_Exp_Year, string cC_Name_On_Card, int x_Order_Id, string cart_GUID, int x_Credit_Card_Id, int eDS_Order_Id, DateTime create_Date, DateTime modify_Date, string modified_By, int deleted_TF, decimal price_Applied, int template_ID) : this(cart_Id, x_Catalog_Group_Id, online_Participant_Id, site_Id, billing_X_Postal_Address_Id, billing_X_Phone_Number_Id, email, cC_Type, cC_Exp_Month, cC_Exp_Year, cC_Name_On_Card, x_Order_Id, cart_GUID, x_Credit_Card_Id, eDS_Order_Id, create_Date, modify_Date, modified_By, deleted_TF, price_Applied, template_ID, int.MinValue) { }
        public Cart(int cart_Id, int x_Catalog_Group_Id, int online_Participant_Id, int site_Id, int billing_X_Postal_Address_Id, int billing_X_Phone_Number_Id, string email, string cC_Type, int cC_Exp_Month, int cC_Exp_Year, string cC_Name_On_Card, int x_Order_Id, string cart_GUID, int x_Credit_Card_Id, int eDS_Order_Id, DateTime create_Date, DateTime modify_Date, string modified_By, int deleted_TF, decimal price_Applied, int template_ID, int isOrderExportable)
        {
            this.cart_Id = cart_Id;
            this.x_Catalog_Group_Id = x_Catalog_Group_Id;
            this.online_Participant_Id = online_Participant_Id;
            this.site_Id = site_Id;
            this.billing_X_Postal_Address_Id = billing_X_Postal_Address_Id;
            this.billing_X_Phone_Number_Id = billing_X_Phone_Number_Id;
            this.email = email;
            this.cC_Type = cC_Type;
            this.cC_Exp_Month = cC_Exp_Month;
            this.cC_Exp_Year = cC_Exp_Year;
            this.cC_Name_On_Card = cC_Name_On_Card;
            this.x_Order_Id = x_Order_Id;
            this.cart_GUID = cart_GUID;
            this.x_Credit_Card_Id = x_Credit_Card_Id;
            this.eDS_Order_Id = eDS_Order_Id;
            this.create_Date = create_Date;
            this.modify_Date = modify_Date;
            this.modified_By = modified_By;
            this.deleted_TF = deleted_TF;
            this.price_Applied = price_Applied;
            this.template_ID = template_ID;
            this.isOrderExportable = isOrderExportable;
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
            return "<Cart>\r\n" +
            "	<Cart_Id>" + cart_Id + "</Cart_Id>\r\n" +
            "	<X_Catalog_Group_Id>" + x_Catalog_Group_Id + "</X_Catalog_Group_Id>\r\n" +
            "	<Online_Participant_Id>" + online_Participant_Id + "</Online_Participant_Id>\r\n" +
            "	<Site_Id>" + site_Id + "</Site_Id>\r\n" +
            "	<Billing_X_Postal_Address_Id>" + billing_X_Postal_Address_Id + "</Billing_X_Postal_Address_Id>\r\n" +
            "	<Billing_X_Phone_Number_Id>" + billing_X_Phone_Number_Id + "</Billing_X_Phone_Number_Id>\r\n" +
            "	<Email>" + System.Web.HttpUtility.HtmlEncode(email) + "</Email>\r\n" +
            "	<CC_Type>" + System.Web.HttpUtility.HtmlEncode(cC_Type) + "</CC_Type>\r\n" +
            "	<CC_Exp_Month>" + cC_Exp_Month + "</CC_Exp_Month>\r\n" +
            "	<CC_Exp_Year>" + cC_Exp_Year + "</CC_Exp_Year>\r\n" +
            "	<CC_Name_On_Card>" + System.Web.HttpUtility.HtmlEncode(cC_Name_On_Card) + "</CC_Name_On_Card>\r\n" +
            "	<X_Order_Id>" + x_Order_Id + "</X_Order_Id>\r\n" +
            "	<Cart_GUID>" + System.Web.HttpUtility.HtmlEncode(cart_GUID) + "</Cart_GUID>\r\n" +
            "	<X_Credit_Card_Id>" + x_Credit_Card_Id + "</X_Credit_Card_Id>\r\n" +
            "	<EDS_Order_Id>" + eDS_Order_Id + "</EDS_Order_Id>\r\n" +
            "	<Create_Date>" + create_Date + "</Create_Date>\r\n" +
            "	<Modify_Date>" + modify_Date + "</Modify_Date>\r\n" +
            "	<Modified_By>" + System.Web.HttpUtility.HtmlEncode(modified_By) + "</Modified_By>\r\n" +
            "	<Deleted_TF>" + deleted_TF + "</Deleted_TF>\r\n" +
            "	<Price_Applied>" + price_Applied + "</Price_Applied>\r\n" +
            "	<Template_ID>" + template_ID + "</Template_ID>\r\n" +
            "	<IsOrderExportable>" + isOrderExportable + "</IsOrderExportable>\r\n" +
            "</Cart>\r\n";
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
            obj = System.Web.HttpUtility.HtmlDecode(val);
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
                if (node.Name.ToLower() == "cart_id")
                {
                    SetXmlValue(ref cart_Id, node.InnerText);
                }
                if (node.Name.ToLower() == "x_catalog_group_id")
                {
                    SetXmlValue(ref x_Catalog_Group_Id, node.InnerText);
                }
                if (node.Name.ToLower() == "online_participant_id")
                {
                    SetXmlValue(ref online_Participant_Id, node.InnerText);
                }
                if (node.Name.ToLower() == "site_id")
                {
                    SetXmlValue(ref site_Id, node.InnerText);
                }
                if (node.Name.ToLower() == "billing_x_postal_address_id")
                {
                    SetXmlValue(ref billing_X_Postal_Address_Id, node.InnerText);
                }
                if (node.Name.ToLower() == "billing_x_phone_number_id")
                {
                    SetXmlValue(ref billing_X_Phone_Number_Id, node.InnerText);
                }
                if (node.Name.ToLower() == "email")
                {
                    SetXmlValue(ref email, node.InnerText);
                }
                if (node.Name.ToLower() == "cc_type")
                {
                    SetXmlValue(ref cC_Type, node.InnerText);
                }
                if (node.Name.ToLower() == "cc_exp_month")
                {
                    SetXmlValue(ref cC_Exp_Month, node.InnerText);
                }
                if (node.Name.ToLower() == "cc_exp_year")
                {
                    SetXmlValue(ref cC_Exp_Year, node.InnerText);
                }
                if (node.Name.ToLower() == "cc_name_on_card")
                {
                    SetXmlValue(ref cC_Name_On_Card, node.InnerText);
                }
                if (node.Name.ToLower() == "x_order_id")
                {
                    SetXmlValue(ref x_Order_Id, node.InnerText);
                }
                if (node.Name.ToLower() == "cart_guid")
                {
                    SetXmlValue(ref cart_GUID, node.InnerText);
                }
                if (node.Name.ToLower() == "x_credit_card_id")
                {
                    SetXmlValue(ref x_Credit_Card_Id, node.InnerText);
                }
                if (node.Name.ToLower() == "eds_order_id")
                {
                    SetXmlValue(ref eDS_Order_Id, node.InnerText);
                }
                if (node.Name.ToLower() == "create_date")
                {
                    SetXmlValue(ref create_Date, node.InnerText);
                }
                if (node.Name.ToLower() == "modify_date")
                {
                    SetXmlValue(ref modify_Date, node.InnerText);
                }
                if (node.Name.ToLower() == "modified_by")
                {
                    SetXmlValue(ref modified_By, node.InnerText);
                }
                if (node.Name.ToLower() == "deleted_tf")
                {
                    SetXmlValue(ref deleted_TF, node.InnerText);
                }
                if (node.Name.ToLower() == "price_applied")
                {
                    SetXmlValue(ref price_Applied, node.InnerText);
                }
                if (node.Name.ToLower() == "template_id")
                {
                    SetXmlValue(ref template_ID, node.InnerText);
                }
                if (node.Name.ToLower() == "isorderexportable")
                {
                    SetXmlValue(ref isOrderExportable, node.InnerText);
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

        #endregion
        /*
        #region Data Source Methods
        public static Cart[] GetCarts()
        {
            DataAccess.Database dbo = new DataAccess.Database();
            return dbo.GetCarts();
        }

        public static Cart GetCartByID(int id)
        {
            DataAccess.Database dbo = new DataAccess.Database();
            return dbo.GetCartByID(id);
        }

        public int Insert()
        {
            DataAccess.Database dbo = new DataAccess.Database();
            return dbo.InsertCart(this);
        }

        public int Update()
        {
            DataAccess.Database dbo = new DataAccess.Database();
            return dbo.UpdateCart(this);
        }
        #endregion*/

        #region Properties
        public int CartID
        {
            set { cart_Id = value; }
            get { return cart_Id; }
        }

        public int XCatalogGroupID
        {
            set { x_Catalog_Group_Id = value; }
            get { return x_Catalog_Group_Id; }
        }

        public int OnlineParticipantID
        {
            set { online_Participant_Id = value; }
            get { return online_Participant_Id; }
        }

        public int SiteID
        {
            set { site_Id = value; }
            get { return site_Id; }
        }

        public int BillingXPostalAddressID
        {
            set { billing_X_Postal_Address_Id = value; }
            get { return billing_X_Postal_Address_Id; }
        }

        public int BillingXPhoneNumberID
        {
            set { billing_X_Phone_Number_Id = value; }
            get { return billing_X_Phone_Number_Id; }
        }

        public string Email
        {
            set { email = value; }
            get { return email; }
        }

        public string CCType
        {
            set { cC_Type = value; }
            get { return cC_Type; }
        }

        public int CCExpMonth
        {
            set { cC_Exp_Month = value; }
            get { return cC_Exp_Month; }
        }

        public int CCExpYear
        {
            set { cC_Exp_Year = value; }
            get { return cC_Exp_Year; }
        }

        public string CCNameOnCard
        {
            set { cC_Name_On_Card = value; }
            get { return cC_Name_On_Card; }
        }

        public int XOrderID
        {
            set { x_Order_Id = value; }
            get { return x_Order_Id; }
        }

        public string CartGUID
        {
            set { cart_GUID = value; }
            get { return cart_GUID; }
        }

        public int XCreditCardID
        {
            set { x_Credit_Card_Id = value; }
            get { return x_Credit_Card_Id; }
        }

        public int EDSOrderID
        {
            set { eDS_Order_Id = value; }
            get { return eDS_Order_Id; }
        }

        public DateTime CreateDate
        {
            set { create_Date = value; }
            get { return create_Date; }
        }

        public DateTime ModifyDate
        {
            set { modify_Date = value; }
            get { return modify_Date; }
        }

        public string ModifiedBy
        {
            set { modified_By = value; }
            get { return modified_By; }
        }

        public int DeletedTF
        {
            set { deleted_TF = value; }
            get { return deleted_TF; }
        }

        public decimal PriceApplied
        {
            set { price_Applied = value; }
            get { return price_Applied; }
        }

        public int TemplateID
        {
            set { template_ID = value; }
            get { return template_ID; }
        }

        public int IsOrderExportable
        {
            set { isOrderExportable = value; }
            get { return isOrderExportable; }
        }

        #endregion
    }
}
