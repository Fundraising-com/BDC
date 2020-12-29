using System;

namespace GA.BDC.Core.ESubsGlobal.Reports {
	/// <summary>
	/// Summary description for GroupMemberRow.
	/// </summary>
    [Serializable]
	public class GroupMemberRow {
		private string memberName;
        private int emailSent = int.MinValue;
        private int numberOfSubs = int.MinValue;
        private Decimal amount = Decimal.MinValue;
        private Decimal amountGross = Decimal.MinValue;
        private Decimal profit = Decimal.MinValue;

		public GroupMemberRow() {

		}

		#region Properties
		public string MemberName {
			set { memberName = value; }
			get { return memberName; }
		}

		public int EmailSent {
			set { emailSent = value; }
			get { return emailSent; }
		}

		public int NumberOfSubs {
			set { numberOfSubs = value; }
			get { return numberOfSubs; }
		}

        public Decimal Amount
        {
            set { amount = value; }
            get { return amount; }
        }

        public Decimal AmountGross
        {
            set { amountGross = value; }
            get { return amountGross; }
        }

        public Decimal Profit
        {
			set { profit = value; }
			get { return profit; }
		}
		#endregion
	}
}
