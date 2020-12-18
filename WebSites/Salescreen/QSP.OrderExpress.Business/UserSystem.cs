namespace QSPForm.Business
{
	
	using System;
	using System.Data;
    using System.Collections.Generic;
    using System.Linq;

	using QSPForm.Common.DataDef;
	using QSPForm.Data;
	using dataDef = QSPForm.Common.DataDef.UserTable;
	using dataAccessRef = QSPForm.Data.User;
	using QSPForm.Common;
	using QSP.Business.Fulfillment;

    using LinqContext = QSP.OrderExpress.Business.Context;
    using LinqEntity = QSP.OrderExpress.Business.Entity;
    using QSP.OrderExpress.Common.Enum;

	/// <summary>
	///     This class contains the business rules that are used for a 
	///     user.
	/// </summary>
	public class UserSystem : BusinessSystem
	{		

        /// <summary>
        /// 
        /// </summary>
        public UserSystem()
		{
			
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Table"></param>
        /// <returns></returns>
		public bool Insert(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			if (this.Insert(Table, new dataAccessRef()))
			{
				// Make sure role doesn't already exist
				UserRole role = UserRole.GetUserRole((int)Table.Rows[0][dataDef.FLD_PKID], (int)Table.Rows[0][dataDef.FLD_ROLE_ID]);
				if (role == null)
				{
					role = new UserRole();
					role.RoleId = (int)Table.Rows[0][dataDef.FLD_ROLE_ID];
					role.UserId = (int)Table.Rows[0][dataDef.FLD_PKID];
					role.CreateUserId = (int)Table.Rows[0][dataDef.FLD_CREATE_USER_ID];
					role.UpdateUserId = (int)Table.Rows[0][dataDef.FLD_CREATE_USER_ID];
					role.Save();
				}

				return true;
			}

			return false;			
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Table"></param>
        /// <returns></returns>
		public bool Update(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation is provide by the overriden Validate Method 
			//is in the current class
			QSP.Business.Fulfillment.User user = QSP.Business.Fulfillment.User.GetUser((int)Table.Rows[0][dataDef.FLD_PKID]);
			if (user != null && user.RoleId.HasValue)
			{
				UserRole role = UserRole.GetUserRole((int)Table.Rows[0][dataDef.FLD_PKID], user.RoleId.Value);
				
				// Check if the new role already exists
				if (UserRole.GetUserRole((int)Table.Rows[0][dataDef.FLD_PKID], (int)Table.Rows[0][dataDef.FLD_ROLE_ID]) == null)
				{
					role.RoleId = (int)Table.Rows[0][dataDef.FLD_ROLE_ID];
					role.UpdateUserId = (int)Table.Rows[0][dataDef.FLD_UPDATE_USER_ID];
					role.Save();
				}
				else if (role.RoleId != (int)Table.Rows[0][dataDef.FLD_ROLE_ID])
				{
					role.Delete();
				}
			}

			return this.Update(Table, new dataAccessRef());
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Table"></param>
        /// <returns></returns>
		public bool Delete(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation is provide by the overriden Validate Method 
			//is in the current class
			return this.Delete(Table, new dataAccessRef());
		}

        /// <summary>
        /// Validates a UserTable Row
        /// </summary>
        /// <remarks>
        /// The "row" parameter returns UserTable data.  If there are fields that contain errors they are individually marked. 
        /// </remarks>
        /// <param name="row">UserTable to be validated</param>
        /// <returns>false if invalid fields exist </returns>
		protected override bool Validate(DataRow row)
		{
			
			bool isValid = true;
            
			//Clear all errors
			row.ClearErrors();
			//Validation only for Insert/Update Operation
			if ((row.RowState == DataRowState.Added) || (row.RowState == DataRowState.Modified))
			{
				//We cumulate the Errors for this part
				//When DataAccess is not imply
				//Apply Mandatory rules
				isValid = IsValid_RequiredFields(row);
				//Apply Maxlength rules
				isValid &=  IsValid_FieldsLength(row);
				//Apply format rules
				if (row[dataDef.FLD_EMAIL].ToString() != String.Empty)
					isValid &= IsValidEmail(row[dataDef.FLD_EMAIL].ToString());
				//apply any other rules like unicity, integrity ...
				//that demand a DataAcess action
				if (isValid)
					isValid  = IsValid_Unicity(row);
			}
//				//Validation only for Delete Operation
//			else if (row.RowState == DataRowState.Deleted)
//			{
//				isValid = IsValid_Integrity(row);
//			}			

			return isValid;
		}

        /// <summary>
        /// Validates a specific UserTable field against his maxlength
        /// </summary>
        /// <param name="row">Row from User_TABLE to be validated</param>
        /// <returns>False if field fails the validation rules.</returns>
		private bool IsValid_FieldsLength(DataRow row)
		{
			bool isValid = true;
			
			isValid &= IsValid_FieldLength(row, dataDef.FLD_USER_NAME, "User Name", 50);					
			isValid &= IsValid_FieldLength(row, dataDef.FLD_PASSWORD, "Password", 50);			
			isValid &= IsValid_FieldLength(row, dataDef.FLD_TITLE, "Title", 50);			
			isValid &= IsValid_FieldLength(row, dataDef.FLD_EMAIL, "Email", 50);			
			isValid &= IsValid_FieldLength(row, dataDef.FLD_BEST_TIME_TO_CALL, "Best Time to Call", 5);			
			isValid &= IsValid_FieldLength(row, dataDef.FLD_DAY_PHONE_NO, "Day Phone no", 20);			
			isValid &= IsValid_FieldLength(row, dataDef.FLD_EVENING_PHONE_NO, "Evening Phone no", 20);			
			isValid &= IsValid_FieldLength(row, dataDef.FLD_FAX_NO, "Fax no", 20);			
            
			return isValid;
		}

        /// <summary>
        /// Validates a specific UserTable field as Mandatory
        /// </summary>
        /// <param name="row">Row from User_TABLE to be validated</param>
        /// <returns>False if field fails the validation rules.</returns>
		private bool IsValid_RequiredFields(DataRow row)
		{
			
			bool IsValid = true;

			//User Name
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_USER_NAME, "User Name");
			//Password
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_PASSWORD, "Password");			
			
			if (!IsValid)
			{
				messageManager.ValidationExceptionType = QSPFormExceptionType.RequiredFields;
			}
            
			return IsValid;
			
		}
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
		private bool IsValid_Unicity(DataRow row)
		{
			
			// 
			// Ensure that User Name does not already exist in the database.
			// Call a Method from the Data Layer
			// Select_UsernameCheck - checks both the CUserProfile and User tables
			//
			string userName = "";
			userName = row[dataDef.FLD_USER_NAME].ToString();
			int userID = Convert.ToInt32(row[dataDef.FLD_PKID]);

			DataTable DT = new dataAccessRef().Select_UsernameCheck(userName, userID);
			if(DT.Rows.Count == 1)
			{
				if(Convert.ToInt32(DT.Rows[0]["UserNameCount"]) > 0)
				{
					row.SetColumnError(dataDef.FLD_USER_NAME, "This username already exists, please choose a new one.");
					messageManager.ValidationExceptionType = QSPFormExceptionType.Unicity;
					return false;
				}
			}
			else
			{
				row.SetColumnError(dataDef.FLD_USER_NAME, " - There was an error validating if the username is available");
				messageManager.ValidationExceptionType = QSPFormExceptionType.Unicity;
				return false;
			}
			return true;
				
         	#region old-style
//			UserTable existingUser = new UserTable();
//			existingUser = new dataAccessRef().SelectAllWuser_nameLogic(userName);
//			//Applying filter
//			DataView DV = new DataView(existingUser);
//			String sFilter = "";
//			sFilter = dataDef.FLD_USER_NAME + " = '" + row[dataDef.FLD_USER_NAME].ToString().Replace("'","''") + "' AND " +
//				dataDef.FLD_PKID + " <> " + row[dataDef.FLD_PKID].ToString();
//				
//
//			DV.RowFilter = sFilter;
//
//			if ( DV.Count > 0 )
//			{
//				//
//				// The User Name is not unique - http://localhost/QSPForm_Web/CommonWeb/
//				//
//				//
//				// PKID does not match, so this would create a duplicate partcicpant
//				//
//				row.SetColumnError(dataDef.FLD_USER_NAME, "A User with the same User Name, already exist in the QSPForm system");
//				messageManager.ValidationExceptionType = QSPFormExceptionType.Unicity;
//                
//				return false;
//			
//			}
			#endregion old-style		
		}

		public dataDef SelectAllByUserName(String userName)
		{
			//
			// Get the user DataTable from the DataLayer
			//
					
			return new dataAccessRef().SelectAllWuser_nameLogic(userName);						
			
		}

		public dataDef SelectOne(int UserID)
		{

			//
			// Get the user DataTable from the DataLayer
			//
					
			return new dataAccessRef().SelectOne(UserID);	
			
		}

		public dataDef SelectAll_Search(int SearchType, String Criteria)
		{		
			
			//
			// Get the user DataTable from the DataLayer
			//	
			return new dataAccessRef().SelectAll_Search(SearchType, Criteria);				
			
		}
		
		public UserData SelectAllDetail(int ID)
		{			
			//This method fill the All Data needed for a user
			//into a predefined DataSet
			UserData dts = new UserData();
			dts.Merge(new dataAccessRef().SelectOne(ID)); 			
			return dts;			
		}

		public string NameLookup(int iUserID)
		{
			DataTable DT = new dataAccessRef().Select_NameLookup(iUserID);
			if(DT.Rows.Count > 0)
			{
				return 
					DT.Rows[0]["first_name"].ToString() 
					+ " " 
					+ DT.Rows[0]["last_name"].ToString();
			}
			else
			{
				return iUserID.ToString();
			}
		}

		public dataDef SelectOneFSM_ByEntityType(int EntityTypeID, int EntityID)
		{		
			
			//
			// Get the user DataTable from the DataLayer
			//	
			return new dataAccessRef().SelectOneFSM_UserWentity_type_idLogic(EntityTypeID, EntityID);

        }

        #region Refactored code

        public LinqEntity.User GetUser(int userId)
        {
            LinqEntity.User result = new LinqEntity.User();

            LinqContext.OrderExpressDataContext context = new LinqContext.OrderExpressDataContext();

            var query = from u in context.Users
                        where u.UserId == userId
                        select u;

            result = query.SingleOrDefault();

            return result;
        }
        public LinqEntity.Role GetRole(int roleId)
        {
            LinqEntity.Role result = new LinqEntity.Role();

            LinqContext.OrderExpressDataContext context = new LinqContext.OrderExpressDataContext();

            var query = from r in context.Roles
                        where r.RoleId == roleId
                        select r;

            result = query.SingleOrDefault();

            return result;
        }
        public List<LinqEntity.UserRole> GetUserRoleListFromUser(int userId)
        {
            List<LinqEntity.UserRole> result = new List<LinqEntity.UserRole>();

            LinqContext.OrderExpressDataContext context = new LinqContext.OrderExpressDataContext();

            var query = from ur in context.UserRoles
                        where ur.UserId == userId
                        select ur;

            result = query.ToList<LinqEntity.UserRole>();

            return result;
        }

        #endregion


    }
}
