using System;
using System.Collections.Generic;
using System.Data;
using QSPForm.Common.DataDef;
using QSPForm.Data;

namespace QSPForm.Business
{
	/// <summary>
	/// Summary description for AuthSystem.
	/// </summary>
	public class AuthSystem : BusinessSystem
	{
		
		public const string MODE_LOGIN = "QSPForm_ModeLogin";
		
		public const int MODE_LOGIN_FM = 1;
		public const int MODE_LOGIN_USER = 0;//Default
		
		public const string ROLE = "QSPForm_Role";
		
		public const int ROLE_SUPER_USER = 5;
		public const int ROLE_ADMINISTRATOR = 4;
		public const int ROLE_ACCOUNTING_MANAGER = 3;
		public const int ROLE_FIELD_SUPPORT = 2;
		public const int ROLE_FM = 1;
		public const int ROLE_USER = 0; //Default
		public const int ROLE_SYSTEM_PROCESS = -1;
		public const int ROLE_SYNCHRONIZATION_PROCESS = -2;
		public const int ROLE_WS_ACCESS = -3;

		int _role;
		int _registry_id = 0;
		int _campaign_id =0;
		string _fm_id = "";
		
		public AuthSystem()
		{
			
		}
		
		public int QSPForm_Authentication(string userName, string password, int Mode)
		{
			int userID = 0;
			
			
			if (Mode == MODE_LOGIN_FM)
			{
				userID = CUser_Authentication(userName, password);					
			}
			else
			{
				userID = User_Authentication(userName, password);
			}
			//Enter a line in the registry if successfull
			if (userID > 0)
			{
				QSPForm.Common.DataDef.RegistryTable regTable = new QSPForm.Common.DataDef.RegistryTable();
				DataRow drw = regTable.NewRow();
				drw[RegistryTable.FLD_USER_ID] = userID;
				drw[RegistryTable.FLD_USER_NAME] = userName;
				drw[RegistryTable.FLD_ROLE] = Role;
				if (Role == AuthSystem.ROLE_FM)
					drw[RegistryTable.FLD_FMID] = _fm_id;
				regTable.Rows.Add(drw);
				_registry_id = Register_Login(regTable);
			}
	
			return userID;
		}
		
		
		
		public int User_Authentication(string userName, string password)
		{
			int userID = 0;			
			//
			// Get the user DataTable from the DataLayer
			//
			UserSystem userSys = new UserSystem();
			UserTable dTbl;
			dTbl = userSys.SelectAllByUserName(userName);
			//    
			// Verify the user's password
			//
			DataRowCollection rows = dTbl.Rows;
        
			if ( ( rows.Count > 0 ) && (String.Compare(rows[0][UserTable.FLD_PASSWORD].ToString(), password, true) ==0))
			{
				DataRow userRow = dTbl.Rows[0];
				userID = Convert.ToInt32(userRow[UserTable.FLD_PKID].ToString());
				//TODO Determinate the role in the row
				_role = Convert.ToInt32(userRow[UserTable.FLD_ROLE_ID].ToString());
				if (_role == ROLE_FM)
				{
					_fm_id = userRow[UserTable.FLD_FM_ID].ToString();
				}
			}			
			
			return userID;
		}

		public int CUser_Authentication(string userName, string password)
		{
			int userID = 0;
			
			//
			// Get the user DataTable from the DataLayer
			//
			QSPForm.Data.CUserProfile cuserDataAccess = new QSPForm.Data.CUserProfile();
			CUserTable dTbl;
			dTbl = cuserDataAccess.SelectAllWUserNameLogic(userName);
			//    
			// Verify the customer's password
			//
			DataRowCollection rows = dTbl.Rows;
        
			if ( ( rows.Count > 0 ) && (String.Compare(rows[0][CUserTable.FLD_PASSWORD].ToString(),password,true) == 0))
			{
				userID = Convert.ToInt32(dTbl.Rows[0][CUserTable.FLD_PKID].ToString());
				_fm_id = dTbl.Rows[0][CUserTable.FLD_FM_ID].ToString();
				if (_fm_id == "8888")
				{
					_role = ROLE_ADMINISTRATOR;
				}
				else if (_fm_id == "9999")
				{
					_role = ROLE_FIELD_SUPPORT;
				}
				else
				{
					_role = ROLE_FM;
				}


			}			
			return userID;
		}

		public int Register_Login(RegistryTable Table)
		{
			int RegistryID = 0;
			
			Registry regDataAccess = new Registry();
			bool IsSuccess =false;				
			int nbRecAff = regDataAccess.Insert(Table);
			IsSuccess = (nbRecAff != 0);
			if (IsSuccess)
			{
				RegistryID = Convert.ToInt32(Table.Rows[0][RegistryTable.FLD_PKID]);
			}			
			return RegistryID;
		}	

		public bool Register_Logout(int RegistryID)
		{
			bool IsSuccess = false;
			
			Registry regDataAccess = new Registry();
			RegistryTable Table = regDataAccess.SelectOne(RegistryID);
			if (Table.Rows.Count > 0)
			{
				//We trigged the DataRowstate to modify
				//The Date will be provide by the GETDATE() in the SP
				DataRow drw = Table.Rows[0];
				drw[RegistryTable.FLD_LOGOUT_DATETIME] = DateTime.Now;
			}
			int nbRecAff = regDataAccess.Update(Table);
			IsSuccess = (nbRecAff != 0);
			
			return IsSuccess;
		}

		public RegistryTable Registry_SelectAll_Search(int SearchType, string Criteria, DateTime StartDate, DateTime EndDate, int ExcludeRoleID)
		{
			RegistryTable dTbl;
			Registry regDataAccess = new Registry();
			//
			// Get the user DataTable from the DataLayer
			//				
			dTbl = regDataAccess.SelectAll_Search(SearchType, Criteria, StartDate, EndDate, ExcludeRoleID);				
			
			return dTbl;
		}

		public DateTime LastVisit(int UserID)
		{
			DateTime lastVisit = DateTime.MinValue;
			
			Registry regDataAccess = new Registry();
			RegistryTable Table = regDataAccess.SelectLastOneWuser_idLogic(UserID);
			if (Table.Rows.Count > 0)
			{					
				DataRow drw = Table.Rows[0];					
				lastVisit = Convert.ToDateTime(drw[RegistryTable.FLD_LOGIN_DATETIME]);
			}				
			
			return lastVisit;
		}

		public DateTime LastVisit(int UserID, int RegistryID)
		{
			DateTime lastVisit = DateTime.MinValue;
			
			Registry regDataAccess = new Registry();
			RegistryTable Table = regDataAccess.SelectLastOneWuser_idLogic(UserID, RegistryID);
			if (Table.Rows.Count > 0)
			{					
				DataRow drw = Table.Rows[0];					
				lastVisit = Convert.ToDateTime(drw[RegistryTable.FLD_LOGIN_DATETIME]);
			}				
			
			return lastVisit;
		}

		public int Role
		{
			get
			{
				return _role;
			}
		}

		public string FM_ID
		{
			get
			{
				return _fm_id;
			}
		}

		public int RegistryID
		{
			get
			{
				return _registry_id;
			}
		}


		public RoleTable SelectAllRole()
		{
			//
			// Get the DataTable from the DataLayer
			//
			Data.Common comDataAccess = new Data.Common();
			return comDataAccess.SelectAllRole();

		}

        public RoleTable SelectAllRoleByLoginName(string name)
        {
            Data.Common comDataAccess = new Data.Common();
            return comDataAccess.SelectAllRoleByLoginName(name);
        }
	}
}
