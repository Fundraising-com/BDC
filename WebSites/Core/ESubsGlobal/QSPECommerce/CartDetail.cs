using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;



namespace GA.BDC.Core.ESubsGlobal.QSPECommerce
{
    public class CartDetail : EnvironmentBase
    {
        private int cart_Detail_Id;
        private int cart_Id;
        private int x_Catalog_Item_Detail_Id;
        private int x_Order_Detail_Id;
        private int renewal_TF;
        private int gift_TF;
        private int quantity;
        private int shipping_X_Postal_Address_Id;
        private DateTime create_Date;
        private DateTime modify_Date;
        private string modified_By;
        private int deleted_TF;
        private decimal price_Applied;
        private string gift_Email_Address;
        private string gift_Personalized_Message;
        private string gift_Recipient;
        private string gift_From;


        public CartDetail() : this(int.MinValue) { }
        public CartDetail(int cart_Detail_Id) : this(cart_Detail_Id, int.MinValue) { }
        public CartDetail(int cart_Detail_Id, int cart_Id) : this(cart_Detail_Id, cart_Id, int.MinValue) { }
        public CartDetail(int cart_Detail_Id, int cart_Id, int x_Catalog_Item_Detail_Id) : this(cart_Detail_Id, cart_Id, x_Catalog_Item_Detail_Id, int.MinValue) { }
        public CartDetail(int cart_Detail_Id, int cart_Id, int x_Catalog_Item_Detail_Id, int x_Order_Detail_Id) : this(cart_Detail_Id, cart_Id, x_Catalog_Item_Detail_Id, x_Order_Detail_Id, int.MinValue) { }
        public CartDetail(int cart_Detail_Id, int cart_Id, int x_Catalog_Item_Detail_Id, int x_Order_Detail_Id, int renewal_TF) : this(cart_Detail_Id, cart_Id, x_Catalog_Item_Detail_Id, x_Order_Detail_Id, renewal_TF, int.MinValue) { }
        public CartDetail(int cart_Detail_Id, int cart_Id, int x_Catalog_Item_Detail_Id, int x_Order_Detail_Id, int renewal_TF, int gift_TF) : this(cart_Detail_Id, cart_Id, x_Catalog_Item_Detail_Id, x_Order_Detail_Id, renewal_TF, gift_TF, int.MinValue) { }
        public CartDetail(int cart_Detail_Id, int cart_Id, int x_Catalog_Item_Detail_Id, int x_Order_Detail_Id, int renewal_TF, int gift_TF, int quantity) : this(cart_Detail_Id, cart_Id, x_Catalog_Item_Detail_Id, x_Order_Detail_Id, renewal_TF, gift_TF, quantity, int.MinValue) { }
        public CartDetail(int cart_Detail_Id, int cart_Id, int x_Catalog_Item_Detail_Id, int x_Order_Detail_Id, int renewal_TF, int gift_TF, int quantity, int shipping_X_Postal_Address_Id) : this(cart_Detail_Id, cart_Id, x_Catalog_Item_Detail_Id, x_Order_Detail_Id, renewal_TF, gift_TF, quantity, shipping_X_Postal_Address_Id, DateTime.MinValue) { }
        public CartDetail(int cart_Detail_Id, int cart_Id, int x_Catalog_Item_Detail_Id, int x_Order_Detail_Id, int renewal_TF, int gift_TF, int quantity, int shipping_X_Postal_Address_Id, DateTime create_Date) : this(cart_Detail_Id, cart_Id, x_Catalog_Item_Detail_Id, x_Order_Detail_Id, renewal_TF, gift_TF, quantity, shipping_X_Postal_Address_Id, create_Date, DateTime.MinValue) { }
        public CartDetail(int cart_Detail_Id, int cart_Id, int x_Catalog_Item_Detail_Id, int x_Order_Detail_Id, int renewal_TF, int gift_TF, int quantity, int shipping_X_Postal_Address_Id, DateTime create_Date, DateTime modify_Date) : this(cart_Detail_Id, cart_Id, x_Catalog_Item_Detail_Id, x_Order_Detail_Id, renewal_TF, gift_TF, quantity, shipping_X_Postal_Address_Id, create_Date, modify_Date, null) { }
        public CartDetail(int cart_Detail_Id, int cart_Id, int x_Catalog_Item_Detail_Id, int x_Order_Detail_Id, int renewal_TF, int gift_TF, int quantity, int shipping_X_Postal_Address_Id, DateTime create_Date, DateTime modify_Date, string modified_By) : this(cart_Detail_Id, cart_Id, x_Catalog_Item_Detail_Id, x_Order_Detail_Id, renewal_TF, gift_TF, quantity, shipping_X_Postal_Address_Id, create_Date, modify_Date, modified_By, int.MinValue) { }
        public CartDetail(int cart_Detail_Id, int cart_Id, int x_Catalog_Item_Detail_Id, int x_Order_Detail_Id, int renewal_TF, int gift_TF, int quantity, int shipping_X_Postal_Address_Id, DateTime create_Date, DateTime modify_Date, string modified_By, int deleted_TF) : this(cart_Detail_Id, cart_Id, x_Catalog_Item_Detail_Id, x_Order_Detail_Id, renewal_TF, gift_TF, quantity, shipping_X_Postal_Address_Id, create_Date, modify_Date, modified_By, deleted_TF, decimal.MinValue) { }
        public CartDetail(int cart_Detail_Id, int cart_Id, int x_Catalog_Item_Detail_Id, int x_Order_Detail_Id, int renewal_TF, int gift_TF, int quantity, int shipping_X_Postal_Address_Id, DateTime create_Date, DateTime modify_Date, string modified_By, int deleted_TF, decimal price_Applied) : this(cart_Detail_Id, cart_Id, x_Catalog_Item_Detail_Id, x_Order_Detail_Id, renewal_TF, gift_TF, quantity, shipping_X_Postal_Address_Id, create_Date, modify_Date, modified_By, deleted_TF, price_Applied, null) { }
        public CartDetail(int cart_Detail_Id, int cart_Id, int x_Catalog_Item_Detail_Id, int x_Order_Detail_Id, int renewal_TF, int gift_TF, int quantity, int shipping_X_Postal_Address_Id, DateTime create_Date, DateTime modify_Date, string modified_By, int deleted_TF, decimal price_Applied, string gift_Email_Address) : this(cart_Detail_Id, cart_Id, x_Catalog_Item_Detail_Id, x_Order_Detail_Id, renewal_TF, gift_TF, quantity, shipping_X_Postal_Address_Id, create_Date, modify_Date, modified_By, deleted_TF, price_Applied, gift_Email_Address, null) { }
        public CartDetail(int cart_Detail_Id, int cart_Id, int x_Catalog_Item_Detail_Id, int x_Order_Detail_Id, int renewal_TF, int gift_TF, int quantity, int shipping_X_Postal_Address_Id, DateTime create_Date, DateTime modify_Date, string modified_By, int deleted_TF, decimal price_Applied, string gift_Email_Address, string gift_Personalized_Message) : this(cart_Detail_Id, cart_Id, x_Catalog_Item_Detail_Id, x_Order_Detail_Id, renewal_TF, gift_TF, quantity, shipping_X_Postal_Address_Id, create_Date, modify_Date, modified_By, deleted_TF, price_Applied, gift_Email_Address, gift_Personalized_Message, null) { }
        public CartDetail(int cart_Detail_Id, int cart_Id, int x_Catalog_Item_Detail_Id, int x_Order_Detail_Id, int renewal_TF, int gift_TF, int quantity, int shipping_X_Postal_Address_Id, DateTime create_Date, DateTime modify_Date, string modified_By, int deleted_TF, decimal price_Applied, string gift_Email_Address, string gift_Personalized_Message, string gift_Recipient) : this(cart_Detail_Id, cart_Id, x_Catalog_Item_Detail_Id, x_Order_Detail_Id, renewal_TF, gift_TF, quantity, shipping_X_Postal_Address_Id, create_Date, modify_Date, modified_By, deleted_TF, price_Applied, gift_Email_Address, gift_Personalized_Message, gift_Recipient, null) { }
        public CartDetail(int cart_Detail_Id, int cart_Id, int x_Catalog_Item_Detail_Id, int x_Order_Detail_Id, int renewal_TF, int gift_TF, int quantity, int shipping_X_Postal_Address_Id, DateTime create_Date, DateTime modify_Date, string modified_By, int deleted_TF, decimal price_Applied, string gift_Email_Address, string gift_Personalized_Message, string gift_Recipient, string gift_From)
        {
            this.cart_Detail_Id = cart_Detail_Id;
            this.cart_Id = cart_Id;
            this.x_Catalog_Item_Detail_Id = x_Catalog_Item_Detail_Id;
            this.x_Order_Detail_Id = x_Order_Detail_Id;
            this.renewal_TF = renewal_TF;
            this.gift_TF = gift_TF;
            this.quantity = quantity;
            this.shipping_X_Postal_Address_Id = shipping_X_Postal_Address_Id;
            this.create_Date = create_Date;
            this.modify_Date = modify_Date;
            this.modified_By = modified_By;
            this.deleted_TF = deleted_TF;
            this.price_Applied = price_Applied;
            this.gift_Email_Address = gift_Email_Address;
            this.gift_Personalized_Message = gift_Personalized_Message;
            this.gift_Recipient = gift_Recipient;
            this.gift_From = gift_From;
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
            return "<Cart_Detail>\r\n" +
            "	<Cart_Detail_Id>" + cart_Detail_Id + "</Cart_Detail_Id>\r\n" +
            "	<Cart_Id>" + cart_Id + "</Cart_Id>\r\n" +
            "	<X_Catalog_Item_Detail_Id>" + x_Catalog_Item_Detail_Id + "</X_Catalog_Item_Detail_Id>\r\n" +
            "	<X_Order_Detail_Id>" + x_Order_Detail_Id + "</X_Order_Detail_Id>\r\n" +
            "	<Renewal_TF>" + renewal_TF + "</Renewal_TF>\r\n" +
            "	<Gift_TF>" + gift_TF + "</Gift_TF>\r\n" +
            "	<Quantity>" + quantity + "</Quantity>\r\n" +
            "	<Shipping_X_Postal_Address_Id>" + shipping_X_Postal_Address_Id + "</Shipping_X_Postal_Address_Id>\r\n" +
            "	<Create_Date>" + create_Date + "</Create_Date>\r\n" +
            "	<Modify_Date>" + modify_Date + "</Modify_Date>\r\n" +
            "	<Modified_By>" + System.Web.HttpUtility.HtmlEncode(modified_By) + "</Modified_By>\r\n" +
            "	<Deleted_TF>" + deleted_TF + "</Deleted_TF>\r\n" +
            "	<Price_Applied>" + price_Applied + "</Price_Applied>\r\n" +
            "	<Gift_Email_Address>" + System.Web.HttpUtility.HtmlEncode(gift_Email_Address) + "</Gift_Email_Address>\r\n" +
            "	<Gift_Personalized_Message>" + System.Web.HttpUtility.HtmlEncode(gift_Personalized_Message) + "</Gift_Personalized_Message>\r\n" +
            "	<Gift_Recipient>" + System.Web.HttpUtility.HtmlEncode(gift_Recipient) + "</Gift_Recipient>\r\n" +
            "	<Gift_From>" + System.Web.HttpUtility.HtmlEncode(gift_From) + "</Gift_From>\r\n" +
            "</Cart_Detail>\r\n";
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
                if (node.Name.ToLower() == "cart_detail_id")
                {
                    SetXmlValue(ref cart_Detail_Id, node.InnerText);
                }
                if (node.Name.ToLower() == "cart_id")
                {
                    SetXmlValue(ref cart_Id, node.InnerText);
                }
                if (node.Name.ToLower() == "x_catalog_item_detail_id")
                {
                    SetXmlValue(ref x_Catalog_Item_Detail_Id, node.InnerText);
                }
                if (node.Name.ToLower() == "x_order_detail_id")
                {
                    SetXmlValue(ref x_Order_Detail_Id, node.InnerText);
                }
                if (node.Name.ToLower() == "renewal_tf")
                {
                    SetXmlValue(ref renewal_TF, node.InnerText);
                }
                if (node.Name.ToLower() == "gift_tf")
                {
                    SetXmlValue(ref gift_TF, node.InnerText);
                }
                if (node.Name.ToLower() == "quantity")
                {
                    SetXmlValue(ref quantity, node.InnerText);
                }
                if (node.Name.ToLower() == "shipping_x_postal_address_id")
                {
                    SetXmlValue(ref shipping_X_Postal_Address_Id, node.InnerText);
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
                if (node.Name.ToLower() == "gift_email_address")
                {
                    SetXmlValue(ref gift_Email_Address, node.InnerText);
                }
                if (node.Name.ToLower() == "gift_personalized_message")
                {
                    SetXmlValue(ref gift_Personalized_Message, node.InnerText);
                }
                if (node.Name.ToLower() == "gift_recipient")
                {
                    SetXmlValue(ref gift_Recipient, node.InnerText);
                }
                if (node.Name.ToLower() == "gift_from")
                {
                    SetXmlValue(ref gift_From, node.InnerText);
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
		public static Cart_Detail[] GetCart_Details() {
			DataAccess.Database dbo = new DataAccess.Database();
			return dbo.GetCart_Details();
		}

		public static Cart_Detail GetCart_DetailByID(int id) {
			DataAccess.Database dbo = new DataAccess.Database();
			return dbo.GetCart_DetailByID(id);
		}

		public int Insert() {
			DataAccess.Database dbo = new DataAccess.Database();
			return dbo.InsertCart_Detail(this);
		}

		public int Update() {
			DataAccess.Database dbo = new DataAccess.Database();
			return dbo.UpdateCart_Detail(this);
		}
		#endregion*/

        #region Properties
        public int CartDetailID
        {
            set { cart_Detail_Id = value; }
            get { return cart_Detail_Id; }
        }

        public int CartID
        {
            set { cart_Id = value; }
            get { return cart_Id; }
        }

        public int XCatalogItemDetailID
        {
            set { x_Catalog_Item_Detail_Id = value; }
            get { return x_Catalog_Item_Detail_Id; }
        }

        public int XOrderDetailID
        {
            set { x_Order_Detail_Id = value; }
            get { return x_Order_Detail_Id; }
        }

        public int RenewalTF
        {
            set { renewal_TF = value; }
            get { return renewal_TF; }
        }

        public int GiftTF
        {
            set { gift_TF = value; }
            get { return gift_TF; }
        }

        public int Quantity
        {
            set { quantity = value; }
            get { return quantity; }
        }

        public int ShippingXPostalAddressID
        {
            set { shipping_X_Postal_Address_Id = value; }
            get { return shipping_X_Postal_Address_Id; }
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

        public string GiftEmailAddress
        {
            set { gift_Email_Address = value; }
            get { return gift_Email_Address; }
        }

        public string GiftPersonalizedMessage
        {
            set { gift_Personalized_Message = value; }
            get { return gift_Personalized_Message; }
        }

        public string GiftRecipient
        {
            set { gift_Recipient = value; }
            get { return gift_Recipient; }
        }

        public string GiftFrom
        {
            set { gift_From = value; }
            get { return gift_From; }
        }

        #endregion
    }
}
