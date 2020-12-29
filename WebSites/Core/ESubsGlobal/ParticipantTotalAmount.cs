using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using GA.BDC.Core.ESubsGlobal.DataAccess;

namespace GA.BDC.Core.ESubsGlobal
{
    public class ParticipantTotalAmount
    {
        private int _id = int.MinValue;
        private int _event_participation_id = int.MinValue;
        private string _participant_name;
        private int _items = int.MinValue;
        private decimal _total_amount = decimal.MinValue;
        private int _total_supporters = int.MinValue;
        private decimal _total_donation_amount = decimal.MinValue;
        private int _total_donors = int.MinValue;
        private decimal _total_profit = 0M;
        private DateTime _create_date;

        public ParticipantTotalAmount() : this(int.MinValue) { }
        public ParticipantTotalAmount(int id) : this(id, int.MinValue) { }
        public ParticipantTotalAmount(int id, int event_participation_id) : this(id, event_participation_id, "") { }
        public ParticipantTotalAmount(int id, int event_participation_id, string participant_name) : this(id, event_participation_id, participant_name, int.MinValue) { }
        public ParticipantTotalAmount(int id, int event_participation_id, string participant_name, int items) : this(id, event_participation_id, participant_name, items, decimal.MinValue) { }
        public ParticipantTotalAmount(int id, int event_participation_id, string participant_name, int items, decimal total_amount) : this(id, event_participation_id, participant_name, items, total_amount, int.MinValue) { }
        public ParticipantTotalAmount(int id, int event_participation_id, string participant_name, int items, decimal total_amount, int total_supporters) : this(id, event_participation_id, participant_name, items, total_amount, total_supporters, decimal.MinValue) { }
        public ParticipantTotalAmount(int id, int event_participation_id, string participant_name, int items, decimal total_amount, int total_supporters, decimal total_donation_amount) : this(id, event_participation_id, participant_name, items, total_amount, total_supporters, total_donation_amount, int.MinValue) { }
        public ParticipantTotalAmount(int id, int event_participation_id, string participant_name, int items, decimal total_amount, int total_supporters, decimal total_donation_amount, int total_donors) : this(id, event_participation_id, participant_name, items, total_amount, total_supporters, total_donation_amount, total_donors, DateTime.MinValue) { }
        public ParticipantTotalAmount(int id, int event_participation_id, string participant_name, int items, decimal total_amount, int total_supporters, decimal total_donation_amount, int total_donors, DateTime create_date)
        {
            _id = id;
            _event_participation_id = event_participation_id;
            _participant_name = participant_name;
            _items = items;
            _total_amount = total_amount;
            _total_supporters = total_supporters;
            _total_donation_amount = total_donation_amount;
            _total_donors = total_donors;
            _create_date = create_date;
        }

        #region Public Methods
        public static List<ParticipantTotalAmount> GetTop3ParticipantTotalAmountByParnerID(Int32 partner_id)
        {
            ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
            return dbo.GetTop3ParticipantTotalAmountByParnerID(partner_id);
        }
        #endregion

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
            return "<ParticipantTotalAmount>\r\n" +
            "	<Id>" + _id + "</Id>\r\n" +
            "	<Event_participation_id>" + _event_participation_id + "</Event_participation_id>\r\n" +
            "	<Participant_name>" + _participant_name + "</Participant_name>\r\n" +
            "	<Items>" + _items + "</Items>\r\n" +
            "	<Total_amount>" + _total_amount + "</Total_amount>\r\n" +
            "	<Total_supporters>" + _total_supporters + "</Total_supporters>\r\n" +
            "	<Total_donation_amount>" + _total_donation_amount + "</Total_donation_amount>\r\n" +
            "	<Total_donors>" + _total_donors + "</Total_donors>\r\n" +
            "	<Total_profit>" + _total_profit + "</Total_profit>\r\n" +
            "	<Create_date>" + _create_date + "</Create_date>\r\n" +
            "</ParticipantTotalAmount>\r\n";
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
                if (node.Name.ToLower() == "id")
                {
                    SetXmlValue(ref _id, node.InnerText);
                }
                if (node.Name.ToLower() == "event_participation_id")
                {
                    SetXmlValue(ref _event_participation_id, node.InnerText);
                }
                if (node.Name.ToLower() == "participant_name")
                {
                    SetXmlValue(ref _participant_name, node.InnerText);
                }
                if (node.Name.ToLower() == "items")
                {
                    SetXmlValue(ref _items, node.InnerText);
                }
                if (node.Name.ToLower() == "total_amount")
                {
                    SetXmlValue(ref _total_amount, node.InnerText);
                }
                if (node.Name.ToLower() == "total_supporters")
                {
                    SetXmlValue(ref _total_supporters, node.InnerText);
                }
                if (node.Name.ToLower() == "total_donation_amount")
                {
                    SetXmlValue(ref _total_donation_amount, node.InnerText);
                }
                if (node.Name.ToLower() == "total_donors")
                {
                    SetXmlValue(ref _total_donors, node.InnerText);
                }
                if (node.Name.ToLower() == "create_date")
                {
                    SetXmlValue(ref _create_date, node.InnerText);
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
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }

        public int EventParticipationId
        {
            set { _event_participation_id = value; }
            get { return _event_participation_id; }
        }

        public string ParticipantName
        {
            set { _participant_name = value; }
            get { return _participant_name; }
        }

        public int Items
        {
            set { _items = value; }
            get { return _items; }
        }

        public decimal TotalAmount
        {
            set { _total_amount = value; }
            get { return _total_amount; }
        }

        public int TotalSupporters
        {
            set { _total_supporters = value; }
            get { return _total_supporters; }
        }

        public decimal TotalDonationAmount
        {
            set { _total_donation_amount = value; }
            get { return _total_donation_amount; }
        }

        public int TotalDonors
        {
            set { _total_donors = value; }
            get { return _total_donors; }
        }

        public decimal TotalProfit
        {
            set { _total_profit = value; }
            get { return _total_profit; }
        }

        public DateTime CreateDate
        {
            set { _create_date = value; }
            get { return _create_date; }
        }

        #endregion
    }
}
