using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using GA.BDC.Core.ESubsGlobal.DataAccess;

namespace GA.BDC.Core.ESubsGlobal
{
    public class EventTotalAmount : EnvironmentBase
    {
        private int _id = int.MinValue;
        private int _event_id = int.MinValue;
        private int _items = 0;
        private decimal _total_amount = 0M;
        private DateTime _create_date;
        private int _total_supporters = 0;
        private decimal _total_donation_amount = 0M;
        private int _total_donars = 0;
        private decimal _total_profit = 0M;

        public EventTotalAmount() : this(int.MinValue) { }
        public EventTotalAmount(int id) : this(id, int.MinValue) { }
        public EventTotalAmount(int id, int event_id) : this(id, event_id, int.MinValue) { }
        public EventTotalAmount(int id, int event_id, int items) : this(id, event_id, items, decimal.MinValue) { }
        public EventTotalAmount(int id, int event_id, int items, decimal total_amount) : this(id, event_id, items, total_amount, DateTime.MinValue) { }
        public EventTotalAmount(int id, int event_id, int items, decimal total_amount, DateTime create_date) : this(id, event_id, items, total_amount, 0, create_date) { }
        public EventTotalAmount(int id, int event_id, int items, decimal total_amount, int total_supporters, DateTime create_date) : this(id, event_id, items, total_amount, total_supporters, 0, 0, create_date) { }
        public EventTotalAmount(int id, int event_id, int items, decimal total_amount, int total_supporters, decimal total_donation_amount, int total_donars, DateTime create_date)
        {
            _id = id;
            _event_id = event_id;
            _items = items;
            _total_amount = total_amount;            
            _total_supporters = total_supporters;
            _total_donation_amount = total_donation_amount;
            _total_donars = total_donars;
            _create_date = create_date;
        }

        #region Public Methods
        public static EventTotalAmount GetEventTotalAmountByPartnerID(Int32 partner_id)
        {
            return GetEventTotalAmountByPartnerID(partner_id, false);
        }

        public static EventTotalAmount GetEventTotalAmountByPartnerID(Int32 partner_id, bool applyPartnerProfit)
        {
            ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
            EventTotalAmount eta = dbo.GetEventTotalAmountByPartnerID(partner_id);

            if (applyPartnerProfit)
            {
                // Get current partner profit from EFRCommon
                GA.BDC.Core.eFundraisingCommon.PartnerProfit pp = GA.BDC.Core.eFundraisingCommon.PartnerProfit.GetCurrentPartnerProfitByID(partner_id);
                if (pp != null)
                {
                    List<GA.BDC.Core.eFundraisingCommon.Profit> profits = GA.BDC.Core.eFundraisingCommon.Profit.GetProfitByProfitGroupID(pp.ProfitGroupID);
                    if (profits != null)
                    {
                        GA.BDC.Core.eFundraisingCommon.Profit currentProfit = null;
                        foreach (GA.BDC.Core.eFundraisingCommon.Profit p in profits)
                        {
                            if (p.QspCatalogTypeID == int.MinValue)
                                currentProfit = p;
                        }
                        if (currentProfit != null)
                        {
                            if (eta.TotalAmount > 0)
                                eta.TotalAmount = eta.TotalAmount * Convert.ToDecimal(currentProfit.ProfitPercentage / 100);
                        }
                    }
                }
            }

            return eta;
        }

        public static EventTotalAmount GetEventTotalAmountByEventID(Int32 event_id)
        {
            ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
            return dbo.GetEventTotalAmountByEventID(event_id);
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
            return "<EventTotalAmount>\r\n" +
            "	<Id>" + _id + "</Id>\r\n" +
            "	<Event_id>" + _event_id + "</Event_id>\r\n" +
            "	<Items>" + _items + "</Items>\r\n" +
            "	<Total_amount>" + _total_amount + "</Total_amount>\r\n" +
            "	<Total_supporters>" + _total_supporters + "</Total_supporters>\r\n" +
            "	<Total_donation_amount>" + _total_donation_amount + "</Total_donation_amount>\r\n" +
            "	<Total_donars>" + _total_donars + "</Total_donars>\r\n" +
            "	<Total_profit>" + _total_profit + "</Total_profit>\r\n" +
            "	<Create_date>" + _create_date + "</Create_date>\r\n" +
            "</EventTotalAmount>\r\n";
        }
        #endregion

        #endregion

        #region Properties
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }

        public int EventID
        {
            set { _event_id = value; }
            get { return _event_id; }
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

        public string TotalAmountString
        {
            get { return _total_amount.ToString("$###,###,##0", new System.Globalization.CultureInfo("en-US")); }
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

        public int TotalDonars
        {
            set { _total_donars = value; }
            get { return _total_donars; }
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
