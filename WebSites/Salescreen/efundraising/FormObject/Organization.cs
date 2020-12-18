using System;

namespace efundraising.efundraisingCore.FormObject
{
	/// <summary>
	/// Summary description for Organization.
	/// </summary>
	public class Organization {
		#region private fields

		private int _OrganizationId = -1;
		private string _OrganizationDesc = string.Empty;

		#endregion

		#region public constructors

		public Organization() {
		
		}

		public Organization(int pOrganizationId, string pOrganizationDesc) {
			_OrganizationId = pOrganizationId;
			_OrganizationDesc = pOrganizationDesc;
		}

		#endregion

		#region public properties

		public int OrganizationId {
			get{ return _OrganizationId; }
		}

		public string OrganizationDescription {
			get{ return _OrganizationDesc; }
		}

		#endregion
	}
}
