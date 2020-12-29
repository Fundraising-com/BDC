using System;
using System.Collections.Generic;

namespace GA.BDC.Core.ESubsGlobal.Reports {
	/// <summary>
	/// Summary description for ParticipantDetail.
	/// </summary>
    [Serializable]
	public class ParticipantDetail
    {
        #region Public/Private Fields
        private int event_participantionID = int.MinValue;
        private string encr_event_participantionID;
		private string first_name;
        private string last_name;
		private DateTime createdDate;
        private int numberOfEmailsSent = int.MinValue;
        private int numberOfItemSold = int.MinValue;
        private Decimal amount = Decimal.MinValue;
        private Decimal profit = Decimal.MinValue;
        private string imageURL;
        private int imageID = int.MinValue;

        private int personalizationID;
        private string encr_personalizationID;
        private Decimal fundraisingGoal;
        private int imageApprovalStatusId;
        private Decimal donationAmount;
        private int totalSupporters = 0;
        private int totalDonars = 0;
        #endregion

        #region Constructor
        public ParticipantDetail() {

        }
        #endregion

        #region Public/Private Properties
        /// <summary>
        /// The participants Event Participation ID
        /// </summary>
		public int EventParticipantionID {
            set { event_participantionID = value; }
            get { return event_participantionID; }
		}

        public string EncryptedEventParticipantionID
        {
            set { encr_event_participantionID = value; }
            get { return encr_event_participantionID; }
        }

        public string FirstName
        {
            set { first_name = value; }
            get { return first_name != null ? first_name.Trim() : string.Empty; }
        }

        public string LastName
        {
            set { last_name = value; }
            get { return last_name != null ? last_name.Trim() : string.Empty; }
        }

		public string FullName {
            get { return (first_name != null ? first_name.Trim() : string.Empty) + " " + (last_name != null ? last_name.Trim() : string.Empty); }
		}

		public DateTime CreatedDate {
			set { createdDate = value; }
			get { return createdDate; }
		}

		public int NumberOfEmailsSent {
			set { numberOfEmailsSent = value; }
			get { return numberOfEmailsSent; }
		}

		public int NumberOfItemSold {
			set { numberOfItemSold = value; }
			get { return numberOfItemSold; }
		}

        public int ImageApprovalStatusId
        {
            set { imageApprovalStatusId = value; }
            get { return imageApprovalStatusId; }
        }

		public Decimal Amount {
			set { amount = value; }
			get { return amount; }
		}

		public Decimal Profit {
			set { profit = value; }
			get { return profit; }
		}

        public string ImageURL
        {
            set { imageURL = (!string.IsNullOrEmpty(value)) ? value : "/Resources/Images/sponsor/participant_default.gif"; }
            get { return imageURL; }
        }

        public int ImageID
        {
            set { imageID = value; }
            get { return this.imageID; }
        }

        public int PersonalizationID
        {
            set { personalizationID = value; }
            get { return personalizationID; }
        }

        public string EncryptedPersonalizationID
        {
            set { encr_personalizationID = value; }
            get { return encr_personalizationID; }
        }

        public Decimal FundraisingGoal
        {
            set { fundraisingGoal = value; }
            get { return fundraisingGoal; }
        }

        public Decimal DonationAmount
        {
            set { donationAmount = value; }
            get { return donationAmount; }
        }

        public int TotalSupporters
        {
            set { totalSupporters = value; }
            get { return totalSupporters; }
        }

        public int TotalDonars
        {
            set { totalDonars = value; }
            get { return totalDonars; }
        }
		#endregion

        #region Public/Private Functions
        public static List<ParticipantDetail> GetParticipantsByHierarchyAndEventID(int member_hierarchy_id, int eventID, string keyword)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetParticipantsByHierarchyAndEventID(member_hierarchy_id, eventID, keyword);
        }

        /// <summary>
        /// (DO NOT USE) Use instead ParticipantTotalAmount.GetTop3ParticipantTotalAmountByParnerID(int partner_id)
        /// </summary>
        /// <param name="partnerID"></param>
        /// <returns></returns>
        public static List<ParticipantDetail> GetParticipantsByPartnerID(int partnerID)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetParticipantsByPartnerID(partnerID);
        }
        #endregion

    }
}
