using System;

namespace GA.BDC.Core.ESubsGlobal
{
	/// <summary>
	/// Login Information contains the basic information about a login row.
	/// </summary>
    [Serializable]
	public class LoginInformation : GA.BDC.Core.BusinessBase.BusinessBase {
        private int userID = int.MinValue;
        private int memberID = int.MinValue;
        private int memberHierarchyID = int.MinValue;
        private int eventParticipationID = int.MinValue;
        private int eventID = int.MinValue;
		private bool active;
        private int partnerID = int.MinValue;
		private Users.UserType userType;

		public LoginInformation() {

		}

		public static LoginInformation[] GetLoginInformations(string username, string password, int partnerID) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetLoginInformations(username, password, partnerID);
		}

		public static System.Data.DataTable GetCampaignList(string userEmailAddress, string password, int partnerID) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetCampaignList(userEmailAddress, password, partnerID);
		}

		
		public static System.Data.DataTable GetCampaignList(int memberID) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetCampaignList(memberID);
		}

		public static LoginInformation[] GetLoginInfoByQSPOnlineAccount(string i)
		{
			return null;
		}

		#region Attributes

        public int UserID
        {
            set { userID = value; }
            get { return userID; }
        }

		public int MemberID {
			set { memberID = value; }
			get { return memberID; }
		}

		public int MemberHierarchyID {
			set { memberHierarchyID = value; }
			get { return memberHierarchyID; }
		}

		public int EventParticipationID {
			set { eventParticipationID = value; }
			get { return eventParticipationID; }
		}

		public int EventID {
			set { eventID = value; }
			get { return eventID; }
		}

		public bool Active {
			set { active = value; }
			get { return active; }
		}

		public int PartnerID {
			set { partnerID = value; }
			get { return partnerID; }
		}

		public Users.UserType UserType {
			set { userType = value; }
			get { return userType; }
		}
		#endregion
	}
}
