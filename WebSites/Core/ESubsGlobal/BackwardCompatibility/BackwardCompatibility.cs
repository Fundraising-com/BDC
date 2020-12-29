using System;

namespace GA.BDC.Core.ESubsGlobal.BackwardCompatibility
{
	/// <summary>
	/// Summary description for BackwardCompatibility.
	/// </summary>
	public class BackwardCompatibility {
		public BackwardCompatibility() {

		}

		public static Users.Sponsor GetMemberHierarchyIDByOrganizerID(int organizerID) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetUserFromPreviousESubsVersionUserID(int.MinValue, int.MinValue,
				int.MinValue, organizerID);
		}

		public static Users.Participant GetMemberHierarchyIDByParticipantID(int participantID) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetUserFromPreviousESubsVersionUserID(int.MinValue, int.MinValue,
				participantID, int.MinValue);
		}

		public static Users.Supporter GetMemberHierarchyIDBySupporterID(int supporterID) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetUserFromPreviousESubsVersionUserID(int.MinValue, supporterID,
				int.MinValue, int.MinValue);
		}

		public static Users.UnknownUser GetSponsorMemberHierarchyIDByCampaignID(int campaignID) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetUserFromPreviousESubsVersionUserID(campaignID, int.MinValue,
				int.MinValue, int.MinValue);
		}

	}
}
