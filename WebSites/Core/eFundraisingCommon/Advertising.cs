using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using GA.BDC.Core.eFundraisingCommon.DataAccess;

namespace GA.BDC.Core.eFundraisingCommon
{
    public class Advertising
    {

        private int _advertising_id;
        private int _lead_id;
        private int _org_promotion_id;
        private int _advertsing_type_id;
        private string _first_name;
        private string _last_name;
        private string _phone;
        private string _email;
        private string _compagnie_name;
        private string _compagnie_url;
        private string _display_url;
        private string _listing_text;
        private byte[] _picture_url;
        private string _image_type;
        private string _is_visible;
        private DateTime _start_date;
        private DateTime _end_date;


        public Advertising() : this(int.MinValue) { }
        public Advertising(int advertising_id) : this(advertising_id, int.MinValue) { }
        public Advertising(int advertising_id, int lead_id) : this(advertising_id, lead_id, int.MinValue) { }
        public Advertising(int advertising_id, int lead_id, int org_promotion_id) : this(advertising_id, lead_id, org_promotion_id, int.MinValue) { }
        public Advertising(int advertising_id, int lead_id, int org_promotion_id, int advertsing_type_id) : this(advertising_id, lead_id, org_promotion_id, advertsing_type_id, null) { }
        public Advertising(int advertising_id, int lead_id, int org_promotion_id, int advertsing_type_id, string first_name) : this(advertising_id, lead_id, org_promotion_id, advertsing_type_id, first_name, null) { }
        public Advertising(int advertising_id, int lead_id, int org_promotion_id, int advertsing_type_id, string first_name, string last_name) : this(advertising_id, lead_id, org_promotion_id, advertsing_type_id, first_name, last_name, null) { }
        public Advertising(int advertising_id, int lead_id, int org_promotion_id, int advertsing_type_id, string first_name, string last_name, string phone) : this(advertising_id, lead_id, org_promotion_id, advertsing_type_id, first_name, last_name, phone, null) { }
        public Advertising(int advertising_id, int lead_id, int org_promotion_id, int advertsing_type_id, string first_name, string last_name, string phone, string email) : this(advertising_id, lead_id, org_promotion_id, advertsing_type_id, first_name, last_name, phone, email, null) { }
        public Advertising(int advertising_id, int lead_id, int org_promotion_id, int advertsing_type_id, string first_name, string last_name, string phone, string email, string compagnie_name) : this(advertising_id, lead_id, org_promotion_id, advertsing_type_id, first_name, last_name, phone, email, compagnie_name, null) { }
        public Advertising(int advertising_id, int lead_id, int org_promotion_id, int advertsing_type_id, string first_name, string last_name, string phone, string email, string compagnie_name, string compagnie_url) : this(advertising_id, lead_id, org_promotion_id, advertsing_type_id, first_name, last_name, phone, email, compagnie_name, compagnie_url, null) { }
        public Advertising(int advertising_id, int lead_id, int org_promotion_id, int advertsing_type_id, string first_name, string last_name, string phone, string email, string compagnie_name, string compagnie_url, string display_url) : this(advertising_id, lead_id, org_promotion_id, advertsing_type_id, first_name, last_name, phone, email, compagnie_name, compagnie_url, display_url, null) { }
        public Advertising(int advertising_id, int lead_id, int org_promotion_id, int advertsing_type_id, string first_name, string last_name, string phone, string email, string compagnie_name, string compagnie_url, string display_url, string listing_text) : this(advertising_id, lead_id, org_promotion_id, advertsing_type_id, first_name, last_name, phone, email, compagnie_name, compagnie_url, display_url, listing_text, null) { }
        public Advertising(int advertising_id, int lead_id, int org_promotion_id, int advertsing_type_id, string first_name, string last_name, string phone, string email, string compagnie_name, string compagnie_url, string display_url, string listing_text, byte[] picture_url) : this(advertising_id, lead_id, org_promotion_id, advertsing_type_id, first_name, last_name, phone, email, compagnie_name, compagnie_url, display_url, listing_text, picture_url, null) { }
        public Advertising(int advertising_id, int lead_id, int org_promotion_id, int advertsing_type_id, string first_name, string last_name, string phone, string email, string compagnie_name, string compagnie_url, string display_url, string listing_text, byte[] picture_url, string image_type) : this(advertising_id, lead_id, org_promotion_id, advertsing_type_id, first_name, last_name, phone, email, compagnie_name, compagnie_url, display_url, listing_text, picture_url, image_type, null) { }
        public Advertising(int advertising_id, int lead_id, int org_promotion_id, int advertsing_type_id, string first_name, string last_name, string phone, string email, string compagnie_name, string compagnie_url, string display_url, string listing_text, byte[] picture_url, string image_type, string is_visible) : this(advertising_id, lead_id, org_promotion_id, advertsing_type_id, first_name, last_name, phone, email, compagnie_name, compagnie_url, display_url, listing_text, picture_url, image_type, is_visible, DateTime.MinValue) { }
        public Advertising(int advertising_id, int lead_id, int org_promotion_id, int advertsing_type_id, string first_name, string last_name, string phone, string email, string compagnie_name, string compagnie_url, string display_url, string listing_text, byte[] picture_url, string image_type, string is_visible, DateTime start_date) : this(advertising_id, lead_id, org_promotion_id, advertsing_type_id, first_name, last_name, phone, email, compagnie_name, compagnie_url, display_url, listing_text, picture_url, image_type, is_visible, start_date, DateTime.MinValue) { }
        public Advertising(int advertising_id, int lead_id, int org_promotion_id, int advertsing_type_id, string first_name, string last_name, string phone, string email, string compagnie_name, string compagnie_url, string display_url, string listing_text, byte[] picture_url, string image_type, string is_visible, DateTime start_date, DateTime end_date)
        {
            _advertising_id = advertising_id;
            _lead_id = lead_id;
            _org_promotion_id = org_promotion_id;
            _advertsing_type_id = advertsing_type_id;
            _first_name = first_name;
            _last_name = last_name;
            _phone = phone;
            _email = email;
            _compagnie_name = compagnie_name;
            _compagnie_url = compagnie_url;
            _display_url = display_url;
            _listing_text = listing_text;
            _picture_url = picture_url;
            _image_type = image_type;
            _is_visible = is_visible;
            _start_date = start_date;
            _end_date = end_date;
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
            return "<advertising>\r\n" +
            "	<Advertising_id>" + _advertising_id + "</Advertising_id>\r\n" +
            "	<Lead_id>" + _lead_id + "</Lead_id>\r\n" +
            "	<Org_promotion_id>" + _org_promotion_id + "</Org_promotion_id>\r\n" +
            "	<Advertsing_type_id>" + _advertsing_type_id + "</Advertsing_type_id>\r\n" +
            "	<First_name>" + _first_name + "</First_name>\r\n" +
            "	<Last_name>" + _last_name + "</Last_name>\r\n" +
            "	<Phone>" + _phone + "</Phone>\r\n" +
            "	<Email>" + _email + "</Email>\r\n" +
            "	<Compagnie_name>" + _compagnie_name + "</Compagnie_name>\r\n" +
            "	<Compagnie_url>" + _compagnie_url + "</Compagnie_url>\r\n" +
            "	<Display_url>" + _display_url + "</Display_url>\r\n" +
            "	<Listing_text>" + _listing_text + "</Listing_text>\r\n" +
            "	<Picture_url>" + _picture_url + "</Picture_url>\r\n" +
            "	<Image_type>" + _image_type + "</Image_type>\r\n" +
            "	<Is_visible>" + _is_visible + "</Is_visible>\r\n" +
            "	<Start_date>" + _start_date + "</Start_date>\r\n" +
            "	<End_date>" + _end_date + "</End_date>\r\n" +
            "</advertising>\r\n";
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
                if (node.Name.ToLower() == "advertising_id")
                {
                    SetXmlValue(ref _advertising_id, node.InnerText);
                }
                if (node.Name.ToLower() == "lead_id")
                {
                    SetXmlValue(ref _lead_id, node.InnerText);
                }
                if (node.Name.ToLower() == "org_promotion_id")
                {
                    SetXmlValue(ref _org_promotion_id, node.InnerText);
                }
                if (node.Name.ToLower() == "advertsing_type_id")
                {
                    SetXmlValue(ref _advertsing_type_id, node.InnerText);
                }
                if (node.Name.ToLower() == "first_name")
                {
                    SetXmlValue(ref _first_name, node.InnerText);
                }
                if (node.Name.ToLower() == "last_name")
                {
                    SetXmlValue(ref _last_name, node.InnerText);
                }
                if (node.Name.ToLower() == "phone")
                {
                    SetXmlValue(ref _phone, node.InnerText);
                }
                if (node.Name.ToLower() == "email")
                {
                    SetXmlValue(ref _email, node.InnerText);
                }
                if (node.Name.ToLower() == "compagnie_name")
                {
                    SetXmlValue(ref _compagnie_name, node.InnerText);
                }
                if (node.Name.ToLower() == "compagnie_url")
                {
                    SetXmlValue(ref _compagnie_url, node.InnerText);
                }
                if (node.Name.ToLower() == "display_url")
                {
                    SetXmlValue(ref _display_url, node.InnerText);
                }
                if (node.Name.ToLower() == "listing_text")
                {
                    SetXmlValue(ref _listing_text, node.InnerText);
                }
                //if (node.Name.ToLower() == "picture_url")
                //{
                //    SetXmlValue(ref _picture_url, node.InnerText);
                //}
                if (node.Name.ToLower() == "image_type")
                {
                    SetXmlValue(ref _image_type, node.InnerText);
                }
                if (node.Name.ToLower() == "is_visible")
                {
                    SetXmlValue(ref _is_visible, node.InnerText);
                }
                if (node.Name.ToLower() == "start_date")
                {
                    SetXmlValue(ref _start_date, node.InnerText);
                }
                if (node.Name.ToLower() == "end_date")
                {
                    SetXmlValue(ref _end_date, node.InnerText);
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

        #region Properties
        public int advertising_id
        {
            set { _advertising_id = value; }
            get { return _advertising_id; }
        }

        public int lead_id
        {
            set { _lead_id = value; }
            get { return _lead_id; }
        }

        public int org_promotion_id
        {
            set { _org_promotion_id = value; }
            get { return _org_promotion_id; }
        }

        public int advertsing_type_id
        {
            set { _advertsing_type_id = value; }
            get { return _advertsing_type_id; }
        }

        public string first_name
        {
            set { _first_name = value; }
            get { return _first_name; }
        }

        public string last_name
        {
            set { _last_name = value; }
            get { return _last_name; }
        }

        public string phone
        {
            set { _phone = value; }
            get { return _phone; }
        }

        public string email
        {
            set { _email = value; }
            get { return _email; }
        }

        public string compagnie_name
        {
            set { _compagnie_name = value; }
            get { return _compagnie_name; }
        }

        public string compagnie_url
        {
            set { _compagnie_url = value; }
            get { return _compagnie_url; }
        }
        public string display_url
        {
            set { _display_url = value; }
            get { return _display_url; }
        }

        public string listing_text
        {
            set { _listing_text = value; }
            get { return _listing_text; }
        }

        public byte[] picture_url
        {
            set { _picture_url = value; }
            get { return _picture_url; }
        }

        public string image_type
        {
            set { _image_type = value; }
            get { return _image_type; }
        }
        public string is_visible
        {
            set { _is_visible = value; }
            get { return _is_visible; }
        }

        public DateTime start_date
        {
            set { _start_date = value; }
            get { return _start_date; }
        }

        public DateTime end_date
        {
            set { _end_date = value; }
            get { return _end_date; }
        }

        #endregion


        public void InsertNewAdvertising()
        {
            GA.BDC.Core.eFundraisingCommon.DataAccess.EFRCommonDatabase dbo = new GA.BDC.Core.eFundraisingCommon.DataAccess.EFRCommonDatabase();
            this.advertising_id = dbo.InsertNewAdvertising(this.lead_id, this.org_promotion_id, this.advertsing_type_id, this.first_name, this.last_name, this.phone, this.email, this.compagnie_name, this.compagnie_url, this.display_url, this.listing_text, this.is_visible, this.image_type, this.start_date, this.end_date);
        }

        public void UpdateClientAdvertisingInfo(int lead_id, int advertsing_type_id, string irst_name, string last_name, string email, string phone, string compagnie_name, string compagnie_url, string display_url, string listing_text, string is_visible, DateTime start_date, DateTime end_date)
        {
            GA.BDC.Core.eFundraisingCommon.DataAccess.EFRCommonDatabase dbo = new GA.BDC.Core.eFundraisingCommon.DataAccess.EFRCommonDatabase();
            dbo.UpdateClientAdvertisingInfo(this.lead_id, this.advertsing_type_id, this.first_name, this.last_name, this.phone, this.email, this.compagnie_name, this.compagnie_url, this.display_url, this.listing_text, this.is_visible, this.start_date, this.end_date);
        }

        public void UpdateClientImage(int lead_id, byte[] image, string imageType)
        {
            GA.BDC.Core.eFundraisingCommon.DataAccess.EFRCommonDatabase dbo = new GA.BDC.Core.eFundraisingCommon.DataAccess.EFRCommonDatabase();
            dbo.UpdateClientImage(lead_id, image, imageType);
        }
                
        public static Advertising GetClientInformation(int leadID)
        {
            GA.BDC.Core.eFundraisingCommon.DataAccess.EFRCommonDatabase dbo = new GA.BDC.Core.eFundraisingCommon.DataAccess.EFRCommonDatabase();
            return dbo.GetClientInformation(leadID);
        }
        
        public void InsertImage(int advertID, byte[] myimage, string imageContentType)
        {
            GA.BDC.Core.eFundraisingCommon.DataAccess.EFRCommonDatabase dbo = new GA.BDC.Core.eFundraisingCommon.DataAccess.EFRCommonDatabase();
            dbo.InsertImage(advertID, myimage, imageContentType);
        }

        public void InsertAdvertisingListing(int listing_id, int advertID, DateTime start_date, DateTime end_date)
        {
            GA.BDC.Core.eFundraisingCommon.DataAccess.EFRCommonDatabase dbo = new GA.BDC.Core.eFundraisingCommon.DataAccess.EFRCommonDatabase();
            dbo.InsertAdvertisingListing(listing_id, advertID, start_date, end_date);
        }

        public static List<Advertising> GetAdvertisingInfo(int advertID)
        {
            DataAccess.EFRCommonDatabase dbo = new DataAccess.EFRCommonDatabase();
            return dbo.GetAdvertisingInfo(advertID);
        }
    }
}
