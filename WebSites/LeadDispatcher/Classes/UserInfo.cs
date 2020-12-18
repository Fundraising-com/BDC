using System;

namespace CRMWeb.Classes
{
	/// <summary>
	/// Summary description for User.
	/// </summary>
	public class UserInfo
	{

		protected string userName;
		protected string password;
		protected string name;
		protected int accessLevel;

		public UserInfo()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public string USER_NAME {
			get {
				return userName;
			}
			set {
				userName = value;
			}
		}

		public string PASSWORD {
			get {
				return password;
			}
			set {
				password = value;
			}
		}
		
		public string NAME {
			get {
				return name;
			}
			set {
				name = value;
			}
		}
		public int ACCESS_LEVEL {
			get {
				return accessLevel;
			}
			set {
				accessLevel = value;
			}
		}
	}
}
