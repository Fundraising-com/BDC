using System;

namespace GA.BDC.Core.ESubsGlobal.Reports {
	/// <summary>
	/// Summary description for CampaignSupporterRow.
	/// </summary>
    [Serializable]
	public class CampaignSupporterRow {
		private string participantName;
		private string supporterName;
        private int numberOfSubs = int.MinValue;
        private Decimal amount = Decimal.MinValue;
        private Decimal amountGross = Decimal.MinValue;
        private Decimal profit = Decimal.MinValue;

		public CampaignSupporterRow() {
			
		}

		public string ParticipantName {
			set { participantName = value; }
			get { return participantName; }
		}

		public string SupporterName {
			set { supporterName = value; }
			get { return supporterName; }
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
	}
}
