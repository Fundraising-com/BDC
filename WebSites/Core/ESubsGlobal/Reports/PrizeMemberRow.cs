using System;

namespace GA.BDC.Core.ESubsGlobal.Reports {
	/// <summary>
	/// Summary description for PrizeMemberRow.
	/// </summary>
	public class PrizeMemberRow {
		private string firstName;
		private string lastName;
        private int eventParticipantionID = int.MinValue;
        private int numberOfSupporters = int.MinValue;
        private int numberOfSubs = int.MinValue;

		public PrizeMemberRow() {

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

		public int EventParticipantionID {
			set { eventParticipantionID = value; }
			get { return eventParticipantionID; }
		}

		public int NumberOfSupporters {
			set { numberOfSupporters = value; }
			get { return numberOfSupporters; }
		}

		public int NumberOfSubs {
			set { numberOfSubs = value; }
			get { return numberOfSubs; }
		}
		#endregion
	}
}
