using System;

namespace CRMWeb.Classes
{
	/// <summary>
	/// Summary description for ClientInfo.
	/// </summary>
	public class ClientInfo
	{

		protected string clientNo;
		protected string clientSequenceCode;
		protected string firstName;
		protected string lastName;
		protected string organization;


		public ClientInfo()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public string CLIENT_NO {
			get {
			   return clientNo;
			}
			set {
				clientNo = value;
			}
		}

		public string CLIENT_SEQUENCE_CODE {
			get {
				return clientSequenceCode;
			}
			set {
				clientSequenceCode = value;
			}
		}

		public string FIRST_NAME {
			get {
				return firstName;
			}
			set {
				firstName = value;
			}
		}

		public string LAST_NAME {
			get {
				return lastName;
			}
			set {
				lastName = value;
			}
		}

		public string ORGANIZATION {
			get {
				return organization;
			}
			set {
				organization = value;
			}
		}


	}
}
