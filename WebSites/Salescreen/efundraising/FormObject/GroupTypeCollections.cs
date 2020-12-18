using System;
using System.Data;

using efundraising.Database.efundraising;

namespace efundraising.efundraisingCore.FormObject
{
	/// <summary>
	/// Summary description for GroupTypeCollections.
	/// </summary>
	public class GroupTypeCollections : System.Collections.CollectionBase
	{
		public GroupTypeCollections() {
			
		}

		#region IList Members

		public GroupType this[int index] {
			get{ return (GroupType)List[index]; }
		}

		public GroupType this[string descriptionName] 
		{
			get 
			{
				for(int i=0;i<List.Count;i++) 
				{
					if(((GroupType)List[i]).Description == descriptionName)
						return (GroupType)List[i];
				}
				return null;
			}
		}

		public GroupType GetByGroupTypeId(int pGroupTypeId) {
			foreach(GroupType feGrp in (GroupType[])List) {
				if(feGrp.GroupTypeId == pGroupTypeId)
					return feGrp;
			}
			return new GroupType();		
		}

		public void Insert(int index, GroupType value) {
			List.Insert(index, value);
		}

		public void Remove(GroupType value) {
			List.Remove(value);
		}

		public bool Contains(GroupType value) {
			return List.Contains(value);
		}

		public int IndexOf(GroupType value){
			return List.IndexOf(value);
		}

		public int Add(GroupType value) {
			return List.Add(value);
		}

		#endregion

		#region private static methods

		private static GroupTypeCollections Create() {
			GroupTypeCollections oColl = new GroupTypeCollections();
			DatabaseObject oDb = new DatabaseObject();
			//group_type_id, Description
			foreach(DataRow feRow in oDb.GetGroupType().Rows) 
				oColl.Add(new GroupType(int.Parse(feRow["group_type_id"].ToString()),
					feRow["Description"].ToString()));
			System.Web.HttpContext.Current.Application["GroupType"] = oColl;
			return oColl;
		}

		#endregion

		#region public static methods

		public static GroupTypeCollections Load() {
			GroupTypeCollections oCll = new GroupTypeCollections();
			if(System.Web.HttpContext.Current.Application != null) {
				if(System.Web.HttpContext.Current.Application["GroupType"] != null)
					return (GroupTypeCollections)System.Web.HttpContext.Current.Application["GroupType"];
				else
					return Create();
			}
			return oCll;
		}

		#endregion
	}
}
