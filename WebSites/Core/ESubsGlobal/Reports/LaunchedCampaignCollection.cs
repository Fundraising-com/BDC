using System;
using System.Collections;

namespace GA.BDC.Core.ESubsGlobal.Reports {
	/// <summary>
	/// Summary description for ParticipantDetail.
	/// </summary>
	public class LaunchedCampaignCollection {
        private ArrayList _launchedCampaign;

		public LaunchedCampaignCollection() {
			_launchedCampaign = new ArrayList();
		}

		public static LaunchedCampaignCollection GetLaunchedCampaigns(int sponsorID) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetLaunchedCampaignCollection(sponsorID);
		}

		public void AddLaunchedCampaign(LaunchedCampaign lc) {
			_launchedCampaign.Add(lc);
		}

		#region Properties
		public ArrayList LaunchedCampaigns {
			get { return _launchedCampaign; }
		}
		#endregion
	}
}
