using System;
using System.Collections;

namespace GA.BDC.Core.ESubsGlobal.Reports {
	/// <summary>
	/// Summary description for CampaignSupporterReport.
	/// </summary>
    [Serializable]
	public class CampaignSupporterReport {
		private ArrayList supporters;
		private CampaignSupporterRow unknownSupporter;

		public CampaignSupporterReport() {
			supporters = new ArrayList();
		}

		public static CampaignSupporterReport Create(eSubsGlobalEnvironment env) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetCampaignSupporterReport(env.Event.EventID);
		}

		public void AddCampaignSupporterRow(CampaignSupporterRow csr) {
            if (csr.Amount == Decimal.MinValue)
            {
                csr.Amount = 0;
            }
            if (csr.AmountGross == Decimal.MinValue)
            {
                csr.AmountGross = 0;
            }
            if (csr.ParticipantName == null)
            {
				csr.ParticipantName = "";
			}
			if(csr.SupporterName == null) {
				csr.SupporterName = "";
			}
			if(csr.NumberOfSubs == int.MinValue) {
				csr.NumberOfSubs = 0;
			}
			if(csr.Profit == Decimal.MinValue) {
				csr.Profit = 0;
			}
			supporters.Add(csr);
		}

		public ArrayList Supporters {
			get { return supporters; }
		}

		public CampaignSupporterRow UnknownSupporter {
			get { return unknownSupporter; }
		}

		public void SetUnknownSupporter(CampaignSupporterRow row) {
			if(row.Amount == Decimal.MinValue) {
				row.Amount = 0;
			}
			if(row.SupporterName == null) {
				row.SupporterName = "";
			}
			if(row.ParticipantName == null) {
				row.ParticipantName = "";
			}
			if(row.NumberOfSubs == int.MinValue) {
				row.NumberOfSubs = 0;
			}
			if(row.Profit == Decimal.MinValue) {
				row.Profit = 0;
			}
			unknownSupporter = row;
		}
	}
}
