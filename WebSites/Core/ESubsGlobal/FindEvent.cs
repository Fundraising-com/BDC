//
// 2005-08-21 - Stephen Lim - New class.
//


using System;

namespace GA.BDC.Core.ESubsGlobal
{
	/// <summary>
	/// Summary description for FindEvent.
	/// </summary>
	public class FindEvent : EnvironmentBase, IComparable
	{

		#region Fields
		private Event _event = null;
		private string _address = null;
		private string _city = null;
		private string _subDivisionCode = null;
        private decimal _totalAmount = decimal.MinValue;
        private int _totalSupporters = int.MinValue;
        private decimal _totalDonationAmount = decimal.MinValue;
        private int _totalDonars = int.MinValue;
        private decimal _totalProfit = decimal.MinValue;
		#endregion

        public FindEvent(Event ev, string subDivisionCode, string address, string city) : this(ev, subDivisionCode, address, city, 0M, 0, 0M, 0) { }
        public FindEvent(Event ev, string subDivisionCode, string address, string city, decimal totalAmount, int totalSupporters, decimal totalDonationAmount, int totalDonars)
		{
			_event = ev;
			_address = address;
			_city = city;
			_subDivisionCode = subDivisionCode;
            _totalAmount = totalAmount;
            _totalSupporters = totalSupporters;
            _totalDonationAmount = totalDonationAmount;
            _totalDonars = totalDonars;
		}

		public int CompareTo(object obj)
		{
			if (_event != null && obj != null)
				return _event.Name.CompareTo(((FindEvent) obj).Event.Name);
			else
				return 0;
		}

		#region Properties

        public string Name { get; set; }
        public string event_participation_id { get; set; }
        public string event_id { get; set; }

		public Event Event
		{
			get { return _event; }
			set { _event = value; }
		}

		public string Address {
			get { return _address; }
			set { _address = value; }
		}

		public string City {
			get { return _city; }
			set { _city = value; }
		}

		public string SubDivisionCode
		{
			get { return _subDivisionCode; }
			set { _subDivisionCode = value; }
		}

        public decimal TotalAmount
        {
            get { return _totalAmount; }
            set { _totalAmount = value; }
        }

        public int TotalSupporters
        {
            get { return _totalSupporters; }
            set { _totalSupporters = value; }
        }

        public decimal TotalDonationAmount
        {
            get { return _totalDonationAmount; }
            set { _totalDonationAmount = value; }
        }

        public int TotalDonars
        {
            get { return _totalDonars; }
            set { _totalDonars = value; }
        }

        public decimal TotalProfit
        {
            get { return _totalProfit; }
            set { _totalProfit = value; }
        }
		#endregion

	}
}
