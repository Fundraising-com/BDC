using System;
using System.Linq;
using System.Collections.Generic;
using GA.BDC.Core.ESubsGlobal.Users;

namespace GA.BDC.Core.ESubsGlobal.Reports {
	/// <summary>
	/// Summary description for SupporterInvitedReport.
	/// </summary>
    [Serializable]
	public class SupporterInvitedReport {
        private List<SupporterInvitedRow> supportersInvited;

		public SupporterInvitedReport() {
            supportersInvited = new List<SupporterInvitedRow>();
		}

		public int GetTotalSubs() {
			int total = 0;
			foreach(SupporterInvitedRow sir in supportersInvited) {
				total += sir.NumberOfSubs;
			}
			return total;
		}

        public int GetTotalSubs(List<UnknownUser> users)
        {
            int total = 0;
            foreach (SupporterInvitedRow sir in supportersInvited)
            {
                if (users.Where(x => x.EmailAddress == sir.EmailAddress).Count() != 0)
                    total += sir.NumberOfSubs;
            }
            return total;
        }

        public Decimal GetTotalAmount()
        {
            Decimal total = 0;
            foreach (SupporterInvitedRow sir in supportersInvited)
            {
                total += sir.Amount;
            }
            return total;
        }

        public Decimal GetTotalAmount(List<UnknownUser> users)
        {
            Decimal total = 0;
            foreach (SupporterInvitedRow sir in supportersInvited)
            {
                if (users.Where(x => x.EmailAddress == sir.EmailAddress).Count() != 0)
                    total += sir.Amount;
            }
            return total;
        }

        public Decimal GetTotalDonationAmount()
        {
            Decimal total = 0;
            foreach (SupporterInvitedRow sir in supportersInvited)
            {
                total += sir.DonationAmount;
            }
            return total;
        }

        public Decimal GetTotalDonationAmount(List<UnknownUser> users)
        {
            Decimal total = 0;
            foreach (SupporterInvitedRow sir in supportersInvited)
            {
                if (users.Where(x => x.EmailAddress == sir.EmailAddress).Count() != 0)
                    total += sir.Amount;
            }
            return total;
        }

        public Decimal GetAmountGross()
        {
            Decimal total = 0;
            foreach (SupporterInvitedRow sir in supportersInvited)
            {
                total += sir.AmountGross;
            }
            return total;
        }

        public Decimal GetAmountGross(List<UnknownUser> users)
        {
            Decimal total = 0;
            foreach (SupporterInvitedRow sir in supportersInvited)
            {
                if (users.Where(x => x.EmailAddress == sir.EmailAddress).Count() != 0)
                    total += sir.AmountGross;
            }
            return total;
        }

        public Decimal GetTotalProfit()
        {
			Decimal total = 0;
			foreach(SupporterInvitedRow sir in supportersInvited) {
				total += sir.Profit;
			}
			return total;
		}

        public Decimal GetTotalProfit(List<UnknownUser> users)
        {
            Decimal total = 0;
            foreach (SupporterInvitedRow sir in supportersInvited)
            {
                if (users.Where(x => x.EmailAddress == sir.EmailAddress).Count() != 0)
                    total += sir.Profit;
            }
            return total;
        }

        public Decimal GetTotalEFREcommerceDonationAmount()
        {
            Decimal total = 0;
            foreach (SupporterInvitedRow sir in supportersInvited)
            {
                total += sir.EFRECommerceDonationAmount;
            }
            return total;
        }

		public static SupporterInvitedReport Create(eSubsGlobalEnvironment env) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetSupportersInvited(env.EventParticipation.EventParticipationID);
		}

        public static SupporterInvitedReport Create(int eventParticipationID)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetSupportersInvited(eventParticipationID);
        }

		public void AddSupporterInvitedRow(SupporterInvitedRow row) {
			row.Reset();
			supportersInvited.Add(row);
		}

        public List<SupporterInvitedRow> RemoveNonSupporters()
        {
            List<SupporterInvitedRow> newList = new List<SupporterInvitedRow>();
            foreach (SupporterInvitedRow row in supportersInvited)
            {
                if (row.IsSupporter)
                    newList.Add(row);
            }
            return newList;
        }

        public List<SupporterInvitedRow> SupportersInvited
        {
			get { return supportersInvited; }
		}
	}
}
