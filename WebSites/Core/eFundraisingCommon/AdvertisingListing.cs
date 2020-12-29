using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace GA.BDC.Core.eFundraisingCommon
{
    public class AdvertisingListing
    {

        private int _listing_id;
        private int _advertising_id;
        private DateTime _start_date;
        private DateTime _end_date;


        public AdvertisingListing() : this(int.MinValue) { }
        public AdvertisingListing(int listing_id) : this(listing_id, int.MinValue) { }
        public AdvertisingListing(int listing_id, int advertising_id) : this(listing_id, advertising_id, DateTime.MinValue) { }
        public AdvertisingListing(int listing_id, int advertising_id, DateTime start_date) : this(listing_id, advertising_id, start_date, DateTime.MinValue) { }
        public AdvertisingListing(int listing_id, int advertising_id, DateTime start_date, DateTime end_date)
        {
            _listing_id = listing_id;
            _advertising_id = advertising_id;
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
            return "<AdvertisingListing>\r\n" +
            "	<Listing_id>" + _listing_id + "</Listing_id>\r\n" +
            "	<Advertising_id>" + _advertising_id + "</Advertising_id>\r\n" +
            "	<Start_date>" + _start_date + "</Start_date>\r\n" +
            "	<End_date>" + _end_date + "</End_date>\r\n" +
            "</AdvertisingListing>\r\n";
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
                if (node.Name.ToLower() == "listing_id")
                {
                    SetXmlValue(ref _listing_id, node.InnerText);
                }
                if (node.Name.ToLower() == "advertising_id")
                {
                    SetXmlValue(ref _advertising_id, node.InnerText);
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
        public int listing_id
        {
            set { _listing_id = value; }
            get { return _listing_id; }
        }

        public int advertising_id
        {
            set { _advertising_id = value; }
            get { return _advertising_id; }
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
    }
}
