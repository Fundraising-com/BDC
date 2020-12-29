/* Title:	Group
 * Author:	Jean-Francois Buist
 * Summary:	Hold information and operations for groups.
 * 
 * Create Date:	August 1, 2005
 * 
 */

using System;
using GA.BDC.Core.ESubsGlobal.DataAccess;

namespace GA.BDC.Core.ESubsGlobal {
	public enum InsertGroupIntoDatabaseStatus : int {
		SPONSORID_ALREADY_EXISTS = 2, // See user function named es_validate_group
		MEMBER_HIERARCHY_ALREADY_EXIST = 3,
		REDIRECT_URL_ALREADY_EXISTS = 4,
		EXTERNAL_ORGANIZATION_ID_ALREADY_EXISTS = 5,
		OK = 0,
		UNKNOWN_ERROR = -3
	}

	
	public enum GroupComparable 
	{
		Name,
		ID
	}
	/// <summary>
	/// Summary description for Group.
	/// </summary>
    [Serializable]
	public class Group : EnvironmentBase, IComparable {
		private int _groupID = int.MinValue;
		private string _name = null;
		private int _groupParentID = int.MinValue;
		private int _sponsorID = int.MinValue;
		private int _partnerID = int.MinValue;
		private int _leadID = int.MinValue;
		private string _externalGroupID = null;
		private bool _testGroup = false;
		private int _expectedMembership = int.MinValue;
		private string _groupURL = null;
		private string _redirect = null;
		private string _comments = null;

        // Added by Jiro Hidaka (September 21, 2008)
        private string _externalURL = null;

		private GroupComparable sortBy = GroupComparable.Name;

        // Updated by Jiro Hidaka (September 21, 2008)
		public Group() : this(int.MinValue) { }
		public Group(int groupID) : this(groupID, null) { }
		public Group(int groupID, string groupName) : this(groupID, groupName, int.MinValue) { }
		public Group(int groupID, string groupName, int groupParentID) : this (groupID, groupName, groupParentID, int.MinValue) { }
		public Group(int groupID, string groupName, int groupParentID, int sponsorID) : this (groupID, groupName, groupParentID, sponsorID, int.MinValue) { }
		public Group(int groupID, string groupName, int groupParentID, int sponsorID, int partnerID) : this (groupID, groupName, groupParentID, sponsorID, partnerID, int.MinValue) { }
		public Group(int groupID, string groupName, int groupParentID, int sponsorID, int partnerID, int leadID) : this (groupID, groupName, groupParentID, sponsorID, partnerID, leadID, null) { }
		public Group(int groupID, string groupName, int groupParentID, int sponsorID, int partnerID, int leadID, string externalGroupID) : this (groupID, groupName, groupParentID, sponsorID, partnerID, leadID, externalGroupID, false) { }
		public Group(int groupID, string groupName, int groupParentID, int sponsorID, int partnerID, int leadID, string externalGroupID, bool testGroup) : this (groupID, groupName, groupParentID, sponsorID, partnerID, leadID, externalGroupID, testGroup, int.MinValue) { }
		public Group(int groupID, string groupName, int groupParentID, int sponsorID, int partnerID, int leadID, string externalGroupID, bool testGroup, int expectedMemberShip) : this (groupID, groupName, groupParentID, sponsorID, partnerID, leadID, externalGroupID, testGroup, expectedMemberShip, null) { }
		public Group(int groupID, string groupName, int groupParentID, int sponsorID, int partnerID, int leadID, string externalGroupID, bool testGroup, int expectedMemberShip, string groupURL) : this (groupID, groupName, groupParentID, sponsorID, partnerID, leadID, externalGroupID, testGroup, expectedMemberShip, groupURL, null) { }
        public Group(int groupID, string groupName, int groupParentID, int sponsorID, int partnerID, int leadID, string externalGroupID, bool testGroup, int expectedMemberShip, string groupURL, string comments) : this(groupID, groupName, groupParentID, sponsorID, partnerID, leadID, externalGroupID, testGroup, expectedMemberShip, groupURL, comments, null) { }
        public Group(int groupID, string groupName, int groupParentID, int sponsorID, int partnerID, int leadID, string externalGroupID, bool testGroup, int expectedMemberShip, string groupURL, string comments, string externalURL) 
        {
			this._groupID = groupID;
			this._name = groupName;
			this._groupParentID = groupParentID;
			this._sponsorID = sponsorID;
			this._partnerID = partnerID;
			this._leadID = leadID;
			this._externalGroupID = externalGroupID;
			this._testGroup = testGroup;
			this._expectedMembership = expectedMemberShip;
			this._groupURL = groupURL;
			this._redirect = null;
			this._comments = comments;
            this.ExternalURL = externalURL;
		}

        public Group(Users.Sponsor sponsor, Partner partner,
            string name, bool testGroup, int expectedMembership, string comments, string redirect)
            : this(int.MinValue, sponsor, partner, name, testGroup, expectedMembership,
            comments, redirect, null, int.MinValue, null)
        {

        }

        public Group(Users.Sponsor sponsor, Partner partner,
            string name, bool testGroup, int expectedMembership, string comments, string redirect, string externalGroupID)
            : this(int.MinValue, sponsor, partner, name, testGroup, expectedMembership,
            comments, redirect, null, int.MinValue, externalGroupID)
        {

        }

        public Group(int groupParentID, Users.Sponsor sponsor, Partner partner,
            string name, bool testGroup, int expectedMembership, string comments, string redirect)
            : this(groupParentID, sponsor, partner, name, testGroup, expectedMembership,
            comments, redirect, null, int.MinValue, null)
        {

        }

        public Group(int groupParentID, Users.Sponsor sponsor, Partner partner,
            string name, bool testGroup, int expectedMembership, string comments,
            string redirect, string groupURL, int leadID, string externalGroupID)
        {

            _groupParentID = groupParentID;
            _sponsorID = sponsor.HierarchyID;
            _partnerID = partner.PartnerID;
            _name = name;
            _testGroup = testGroup;
            _expectedMembership = expectedMembership;
            _redirect = redirect;
            _groupURL = groupURL;
            _comments = comments;
            _leadID = leadID;
            _externalGroupID = externalGroupID;
        }

		public InsertGroupIntoDatabaseStatus InsertIntoDatabase() {
			try 
			{
				DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
                // Updated by Jiro Hidaka (September 21, 2008)
                // Added '_externalURL' parameter
				return dbo.InsertGroup(_groupParentID, _sponsorID, _partnerID, _leadID, _externalGroupID,
					_name, _testGroup, _expectedMembership, _groupURL, _redirect, _comments, _externalURL,
					ref _groupID);
			}
			catch (Exception ex)
			{
				throw new ESubsGlobalException(ex.Message, ex, this);
			}

		}

		public InsertGroupIntoDatabaseStatus UpdateInDatabase() {
			ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();

			return dbo.UpdateGroup(_groupParentID, _sponsorID, _partnerID, _leadID,
				_externalGroupID, _name, _expectedMembership, _groupURL, _comments,
				_redirect, _externalURL, ref _groupID);
		}

		public static Group[] GetGroupsByOrders(DateTime start, DateTime end) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetGroupByOrderDates(start, end);
		}

		public static Group LoadRootGroupByGroupID(int groupID)
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetRootGroup(groupID);
		}

		public static Group LoadByGroupID(int groupID) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetGroup(groupID);
		}
		
		public static Group LoadByExternalGroupID(int partnerID, string extGroupID)
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetGroupByExternalGroupID(partnerID, extGroupID);
		}

		public static Group LoadByEmail(int partnerID, string email)
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetGroupByEmail(partnerID, email);
		}

		public static Group LoadByGroupRedirect(string redirect) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return new Group();
		}

		public static Group LoadGroupByEventID(int eventID)
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetGroupByEventID(eventID);
		}

        public static Group LoadGroupByMemberHierarchyID(int memberHierarchyID)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetGroupByMemberHierarchyID(memberHierarchyID);
        }

		public static Group GetGroupByPaymentID(int id)
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetGroupByPaymentID(id);
		}
		public bool TerminateEvent()
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.TerminateEvent(this);
		}

		public bool MoveParticipants(Group newGrp)
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.MoveParticipants(this, newGrp);
		}

		#region Properties
		public int GroupID {
			set { _groupID = value; }
			get { return _groupID; }
		}

		public string Name {
			set { _name = value; }
			get { return _name; }
		}

		public int GroupParentID {
			set { _groupParentID = value; }
			get { return _groupParentID; }
		}

		public int SponsorID {
			set { _sponsorID = value; }
			get { return _sponsorID; }
		}

		public int PartnerID {
			set { _partnerID = value; }
			get { return _partnerID; }
		}

		public int LeadID {
			set { _leadID = value; }
			get { return _leadID; }
		}

		public string ExternalGroupID {
			set { _externalGroupID = value; }
			get { return _externalGroupID; }
		}

		public bool TestGroup {
			set { _testGroup = value; }
			get { return _testGroup; }
		}

		public int ExpectedMembership {
			set { _expectedMembership = value; }
			get { return _expectedMembership; }
		}

		public string GroupURL {
			set { _groupURL = value; }
			get { return _groupURL; }
		}

		/*
		public string Redirect {
			set { _redirect = value; }
			get { return _redirect; }
		}*/

		public string Comments {
			set { _comments = value; }
			get { return _comments; }
		}

        // Added by Jiro Hidaka (September 21, 2008)
        public string ExternalURL
        {
            set { _externalURL = value; }
            get { return _externalURL; }
        }
	
		public GroupComparable SortBy
		{
			get { return sortBy;}
			set { sortBy = value;}
		}

		#endregion

		#region IComparable Members

		public int CompareTo(object obj)
		{
			
			Group grp = obj as Group;
			if (grp != null)
			{
				switch (sortBy)
				{
					case GroupComparable.ID:
					{
						return (this.GroupID > grp.GroupID ? 1 : 0);
					}
					default:
						return string.Compare(Name, grp.Name, true);
				}
			}
			return 0;
		}

		#endregion
	}
}
