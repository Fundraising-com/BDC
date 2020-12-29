using System;
using System.Web.Security;
using System.Security.Principal;

namespace GA.BDC.Core.ESubsGlobal.Security
{
	/// <summary>
	/// Summary description for CustomPrincipal.
	/// </summary>
	public class CustomPrincipal: IPrincipal
	{
		private IIdentity _identity;
		private string[] _roles;

		public CustomPrincipal(IIdentity identity, string[] roles)
		{
			_identity = identity;
			_roles = new string[ roles.Length];
			roles.CopyTo(_roles, 0);
			Array.Sort (_roles);
		}
		#region IPrincipal Members		
		public IIdentity Identity
		{
			get
			{
				return _identity;
			}
		}

		public bool IsInRole(string role)
		{
			if (string.Compare(role.Trim() , "all", true) ==0) return true;
			return Array.BinarySearch(_roles, role) >=0 ? true : false;
		}

		public bool IsInAllRoles(params string [] roles)
		{
			foreach (string searchrole in roles)
			{
				if (Array.BinarySearch(_roles, searchrole) < 0)
					return false;
			}
			return true;
		}

		public bool IsInAnyRoles(params string[] roles)
		{
			foreach (string searchrole in roles)
			{	
				if (string.Compare(searchrole.Trim() , "all", true) ==0) return true;
				if (Array.BinarySearch (_roles, searchrole) >= 0)
					return true;
			}
			return false;
		}
		#endregion

	}
}
