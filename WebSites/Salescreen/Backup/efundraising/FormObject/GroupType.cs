using System;

namespace efundraising.efundraisingCore.FormObject {

	public class GroupType {
	
		#region private fields

		//group_type_id, Description
		private int _Group_Type_Id = -1;
		private string _Description = string.Empty;

		#endregion

		#region public constructors

		public GroupType() {
		
		}

		public GroupType(int pGroupTypeId, string pDescription) {
			_Group_Type_Id = pGroupTypeId;
			_Description = pDescription;
		}

		#endregion

		#region public properties

		public int GroupTypeId {
			get{ return this._Group_Type_Id; }
		}

		public string Description {
			get{ return _Description; }
		}

		#endregion
	}
}
