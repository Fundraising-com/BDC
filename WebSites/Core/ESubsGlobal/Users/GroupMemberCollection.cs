using System;
using System.Collections;

namespace GA.BDC.Core.ESubsGlobal.Users {
	/// <summary>
	/// Summary description for GroupMemberCollection.
	/// </summary>
	public class GroupMemberCollection {
		private ArrayList groupMembers;

		public GroupMemberCollection() {
			groupMembers = new ArrayList();
		}

		public static GroupMemberCollection Create(eSubsGlobalEnvironment env) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetGroupMemberCollection(env.Group.GroupID);
		}

		public void AddGroupMember(GroupMemberRow row) {
			groupMembers.Add(row);
		}

		public ArrayList GroupMembers {
			get { return groupMembers; }
		}
	}
}
