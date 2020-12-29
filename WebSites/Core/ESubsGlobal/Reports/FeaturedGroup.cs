using System;

namespace GA.BDC.Core.ESubsGlobal.Reports
{
	/// <summary>
	/// Summary description for FeaturedGroup.
	/// </summary>
	public class FeaturedGroup : IComparable
	{
		private string eventId;		
		private string eventName;
		private string state;
		private string nbMember;
		private string nbSub;
        private decimal amount = decimal.MinValue;

		public FeaturedGroup()
		{
			
		}

		public FeaturedGroup(string s1, string s2, string s3, string s4, string s5, decimal d6) {
			EventId = s1;
			EventName = s2;
			State = s3;
			NbMember = s4;
			NbSub = s5;
			Amount = d6;
		}

		public string EventId {
			get { return eventId; }
			set { eventId = value; }
		}

		public string EventName {
			get { return eventName; }
			set { eventName = value;}
		}

		public string State {
			get { return state; }
			set { state = value;}
		}

		public string NbMember {
			get { return nbMember; }
			set { nbMember = value;}
		}
		
		public string NbSub {
			get { return nbSub; }
			set { nbSub = value;}
		}
		
		public decimal Amount {
			get { return amount; }
			set { amount = value;}
		}

		#region IComparable Members

		public int CompareTo(object obj) {
			FeaturedGroup temp = (FeaturedGroup)obj;
			return temp.Amount.CompareTo(this.Amount); // reverse order
		}

		#endregion
	}
}
