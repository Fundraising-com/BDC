using System;

namespace GA.BDC.Core.ESubsGlobal.Users {
	/// <summary>
	/// Summary description for GroupMemberRow.
	/// </summary>
	public class GroupMemberRow	{
        private int memberHierarchyID = int.MinValue;
        private int paremtMemberHierarchyID = int.MinValue;
        private int memberID = int.MinValue;
        private int creationChannelID = int.MinValue;
		private string cultureCode;
		private bool optIn;
		private string firstName;
		private string middleName;
		private string lastName;
		private string gender;
		private string emailAddress;
		private string password;
		private bool bounced;
		private string comments;
		private string creationChannelName;
		private string description;
		private bool active;
        private int memberType = int.MinValue;

		public GroupMemberRow() {
			
		}

		#region Properties
		public int MemberHierarchyID {
			set { memberHierarchyID = value; }
			get { return memberHierarchyID; }
		}

		public int ParemtMemberHierarchyID {
			set { paremtMemberHierarchyID = value; }
			get { return paremtMemberHierarchyID; }
		}

		public int MemberID {
			set { memberID = value; }
			get { return memberID; }
		}

		public int CreationChannelID {
			set { creationChannelID = value; }
			get { return creationChannelID; }
		}

		public string CultureCode {
			set { cultureCode = value; }
			get { return cultureCode; }
		}

		public bool OptIn {
			set { optIn = value; }
			get { return optIn; }
		}

		public string FirstName {
			set { firstName = value; }
			get { return firstName; }
		}

		public string MiddleName {
			set { middleName = value; }
			get { return middleName; }
		}

		public string LastName {
			set { lastName = value; }
			get { return lastName; }
		}

		public string Gender {
			set { gender = value; }
			get { return gender; }
		}

		public string EmailAddress {
			set { emailAddress = value; }
			get { return emailAddress; }
		}

		public string Password {
			set { password = value; }
			get { return password; }
		}

		public bool Bounced {
			set { bounced = value; }
			get { return bounced; }
		}

		public string Comments {
			set { comments = value; }
			get { return comments; }
		}

		public string CreationChannelName {
			set { creationChannelName = value; }
			get { return creationChannelName; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public bool Active {
			set { active = value; }
			get { return active; }
		}

		public int MemberType {
			set { memberType = value; }
			get { return memberType; }
		}
		#endregion
	}
}
