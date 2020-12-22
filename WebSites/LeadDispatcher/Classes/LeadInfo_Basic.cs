using System;

namespace CRMWeb.Classes
{
	/// <summary>
	/// Summary description for LeadInfo_Basic.
	/// </summary>
	public class LeadInfo_Basic
	{

		protected string leadName;
        protected string groupName;
		protected string phone;
		protected string email;

		public LeadInfo_Basic()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public string LEAD_NAME {
			get {
				return leadName;
			}
			set {
				leadName = value;
			}
		}
		
		public string GROUP_NAME {
			get {
				return groupName;
			}
			set {
				groupName = value;
			}
		}

		public string PHONE {
			get {
				return phone;
			}
			set {
				phone = value;
			}
		}

		public string EMAIL {
			get {
				return email;
			}
			set {
				email = value;
			}
		}
	}
}
