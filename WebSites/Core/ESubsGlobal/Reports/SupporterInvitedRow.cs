using System;

namespace GA.BDC.Core.ESubsGlobal.Reports {
	/// <summary>
	/// Summary description for SupporterInvitedRow.
	/// </summary>
    [Serializable]
	public class SupporterInvitedRow {
		private string firstName;
		private string lastName;
        private string emailAddress;
        private int numberOfSubs = int.MinValue;
        private Decimal amount = Decimal.MinValue;
        private Decimal donationAmount = Decimal.MinValue;
        private Decimal profit = Decimal.MinValue;
        private Decimal amountGross = Decimal.MinValue;
        private DateTime createDate = DateTime.MinValue;
		private bool issupporter = false;
        private Decimal eFREcommerceDonationAmount = Decimal.MinValue;

		public SupporterInvitedRow() {

		}

		public void Reset() {
			if(amount == Decimal.MinValue) {
				amount = 0;
			}
            if (donationAmount == Decimal.MinValue)
            {
                donationAmount = 0;
            }
			if(firstName == null) {
				firstName = "";
			}
			if(lastName == null) {
				lastName = "";
			}
            if (emailAddress == null)
                emailAddress = "";
			if(numberOfSubs == int.MinValue) {
				numberOfSubs = 0;
			}
            if (profit == Decimal.MinValue)
            {
                profit = 0;
            }
            if (amountGross == Decimal.MinValue)
            {
                amountGross = 0;
            }
            if (eFREcommerceDonationAmount == Decimal.MinValue)
            {
                eFREcommerceDonationAmount = 0;
            }
        }

		#region Properties
		public string FirstName {
			set { firstName = value; }
			get { return firstName; }
		}

		public string LastName {
			set { lastName = value; }
			get { return lastName; }
		}

        public string EmailAddress
        {
            set { emailAddress = value; }
            get { return emailAddress; }
        }

		public int NumberOfSubs {
			set { numberOfSubs = value; }
			get { return numberOfSubs; }
		}

		public Decimal Amount {
			set { amount = value; }
			get { return amount; }
		}

        public Decimal DonationAmount
        {
            set { donationAmount = value; }
            get { return donationAmount; }
        }

        public Decimal Profit
        {
            set { profit = value; }
            get { return profit; }
        }

        public Decimal AmountGross
        {
            set { amountGross = value; }
            get { return amountGross; }
        }

        public DateTime CreateDate
        {
			set { createDate = value; }
			get { return createDate; }
		}
		public bool IsSupporter
		{
			get
			{
				return issupporter;
			}
			set
			{
				issupporter = value;
			}
		}

        public Decimal EFRECommerceDonationAmount
        {
            set { eFREcommerceDonationAmount = value; }
            get { return eFREcommerceDonationAmount; }
        }
		#endregion
	}
}
