using System;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.ComponentModel;

namespace Business
{
	///<summary>UserProfile</summary>
	public class UserProfile : QBusinessObject
	{

		protected int InstanceM=-1;
		protected string zUserNameM;
		protected string zFirstNameM;
		protected string zLastNameM;
		protected string zPasswordM;
		protected string zFmidM;
		private System.Collections.ArrayList zRolesAL;
		private DAL.UserProfileDataAccess aDA;
		private DAL.UserPermissionsDataAccess zPermissionsData;

		#region Class Members

		[DAL.DataColumn("Instance")]
		public int Instance
		{
			get{ return this.InstanceM; }
			set{ this.InstanceM=value;  }
		}

		[DAL.DataColumn("UserName")]
		public string UserName
		{
			get{ return this.zUserNameM; }
			set{ this.zUserNameM = value; }
		}

		[DAL.DataColumn("FirstName")]
		public string FirstName
		{
			get{ return this.zFirstNameM; }
			set{ this.zFirstNameM = value; }
		}

		[DAL.DataColumn("LastName")]
		public string LastName
		{
			get{ return this.zLastNameM; }
			set{ this.zLastNameM = value; }
		}

		[DAL.DataColumn("Password")]
		public string Password
		{
			get{ return this.zPasswordM; }
			set{ this.zPasswordM = value; }
		}

		[DAL.DataColumn("FMID")]
		public string FMID
		{
			get{ return this.zFmidM; }
			set{ this.zFmidM = value; }
		}

		public string FullName
		{
			get{ return this.zFirstNameM + " " + this.zLastNameM; }
		}
		#endregion

		public UserProfile()
		{
			aDA = new DAL.UserProfileDataAccess();
			//zPermissionsData = new DAL.UserPermissionsDataAccess();
			zRolesAL = new System.Collections.ArrayList(7);
		}

		/// <summary>
		/// Function:  ValidateAndSave
		/// No insert only done by sys admin
		/// </summary>
		override public bool ValidateAndSave()
		{
			bool bOk=true;

			return bOk;
		}
		/// <summary>
		/// Function:  Login
		/// No insert only done by sys admin
		/// </summary>
		public bool Login(string zUserName, string zPassword)
		{
//			// check this user's login
			DataTable dt = aDA.LogIn(zUserName, zPassword);
			string rowsStr = dt.Rows.Count.ToString();
			if(dt.Rows.Count < 1)
			{
				//bad login
				return false;
			}
			else if(dt.Rows.Count > 1)
			{
				string msg = "Multiple logins were returned when 1 was expected";
				throw new ArgumentException(msg, zUserName);
			}
			else
			{
				/* one row returned, good stuff */
				DataRow dr = dt.Rows[0];
				Fill(dr,this.GetType());
				return true;
			}
		}
		public bool IsFM
		{
			get
			{
				//return (this.FMID != "" && this.FMID != "9999");
				return HasRole("FieldManager");
			}
		}
//		public string[] RolesOld
//		{
//			get
//			{
//				if(this.zRolesAL.Count == 0)
//				{
//					//retrieve roles from DB
//					DataTable DT = zPermissionsData.GetUserRoles(this.InstanceM);
//
//					for(int i = 0; i < DT.Rows.Count; i++)
//					{
//						this.zRolesAL.Add(DT.Rows[i]["PName"].ToString());
//					}
//				}
//				if(this.zRolesAL.Count == 0)
//				{
//					this.zRolesAL.Add("NoPermissions");
//				}
//
//				//return this.zRolesAL.ToArray(System.TypeCode.String);
//				return new string[1]{"Depreciated"};
//			}
//		}

		public System.Collections.ArrayList Roles
		{
			get
			{
				if(this.zRolesAL.Count == 0)
				{
					//retrieve roles from DB
					zPermissionsData = new DAL.UserPermissionsDataAccess();
					DataTable DT = zPermissionsData.GetUserRoles(this.InstanceM);

					for(int i = 0; i < DT.Rows.Count; i++)
					{
						this.zRolesAL.Add(DT.Rows[i]["PName"].ToString());
					}
				}
				if(this.zRolesAL.Count == 0)
				{
					this.zRolesAL.Add("NoPermissions");
				}

				return this.zRolesAL;
			}
		}

		public bool HasRole(string role)
		{
			foreach(string ss in Roles)
			{
				if(ss == role)
				{
					return true;
				}
			}
			return false;
		}

	}
}
