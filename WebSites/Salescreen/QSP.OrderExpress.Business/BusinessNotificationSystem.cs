namespace QSPForm.Business
{
	using System;
	using System.Data;
	using QSPForm.Common.DataDef;
	using QSPForm.Data;
	using dataDef = QSPForm.Common.DataDef.BusinessNotificationTable; 
	using dataAccessRef = QSPForm.Data.Business_notification;
	using QSPForm.Common;
	
	///<summary>
	///     This class contains the business rules 
	///     that are used for a BusinessNotification item.
	///</summary>
	public class BusinessNotificationSystem : BusinessSystem
	{		
		dataAccessRef objDataAccess;
		private bool sendEmail = false;
		
		///<summary>default constructor</summary>
		public BusinessNotificationSystem()
		{
			objDataAccess = new dataAccessRef();
		}
		/// <summary>
		///     Validates and inserts a new BusinessNotification item.
		///     <remarks>   
		///         Returns BusinessNotification data.  If there are fields that contain errors 
		///         that contain errors they are individually marked.  
		///     </remarks>   
		///     <param name="customer">dataDef to be inserted.</param>
		///     <retvalue>true if successful; otherwise, false.</retvalue>
		/// </summary>
		public bool Insert(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			objDataAccess = new dataAccessRef();
			objDataAccess.SendEmail = SendEmail;
			return this.Insert(Table, objDataAccess);			
		}
	
		public bool Update(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation is provide by the overriden Validate Method 
			//is in the current class
			return this.Update(Table, new dataAccessRef());
		}

		public bool UpdateBatch(dataDef Table)
		{			
			//We call a method from the inherit class, but the
			//validation is provide by the overriden Validate Method 
			//is in the current class
			return this.UpdateBatch(Table, new dataAccessRef());
		}

		public bool Delete(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation is provide by the overriden Validate Method 
			//is in the current class
			return this.Delete(Table, new dataAccessRef());
		}

        public bool DeleteOne(int ID, int UserID)
        {
            //We call a method from the inherit class, but the
            //validation is provide by the overriden Validate Method 
            //is in the current class
            dataAccessRef datAcc = new dataAccessRef();
            return datAcc.DeleteOne(ID, UserID);
        }

		//----------------------------------------------------------------
		// Function Validate:
		//   Validates a BusinessNotificationTable Row
		// Returns:
		//   true if validation is successful 
		//   false if invalid fields exist 
		// Parameters:
		//   [in]  row: BusinessNotificationTable to be validated
		//   [out] row: Returns BusinessNotificationTable data.  If there are fields
		//              that contain errors they are individually marked.  
		//----------------------------------------------------------------
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
				isValid &= IsValid_RequiredFields(row);

				//Apply Maxlength rules
				isValid &=  IsValid_FieldsLength(row);

				//Apply format rules

				//apply any other rules like uniqueness, integrity ...
				//that demand a DataAcess action
			}

//			//Validation only for Delete Operation
//			else if (row.RowState == DataRowState.Deleted)
//			{
//				isValid &= IsValid_Integrity(row);
//			}			

			return isValid;
		}
		
		
        
		//----------------------------------------------------------------
		// Function IsValid_FieldsLength:
		//   Validates a specific BusinessNotificationTable field against his maxlength 
		// Returns:
		//   False if field fails the validation rules.
		// Parameters:
		//   [in]  UserRow: Row of campaign from BusinessNotificationTable to be validated
		//----------------------------------------------------------------
		private bool IsValid_FieldsLength(DataRow row)
		{
			bool isValid = true;

			isValid &= IsValid_FieldLength(row, dataDef.FLD_BUSINESS_NOTIFICATION_NAME, "Name", 50);
			isValid &= IsValid_FieldLength(row, dataDef.FLD_DESCRIPTION, "Description", 200);
			isValid &= IsValid_FieldLength(row, dataDef.FLD_SUBJECT, "Subject", 100);
			isValid &= IsValid_FieldLength(row, dataDef.FLD_MESSAGE, "Message", 4000);

			if (!isValid)
			{
				messageManager.ValidationExceptionType = QSPFormExceptionType.MaxLength;
			}
            
			return isValid;
		}


		//----------------------------------------------------------------
		// Function IsValid_RequiredField:
		//   Validates a specific BusinessNotificationTable field as Mandatory 
		// Returns:
		//   False if field fails the validation rules.
		// Parameters:
		//   [in]  UserRow: Row of campaign from BusinessNotificationTable to be validated
		//----------------------------------------------------------------
		private bool IsValid_RequiredFields(DataRow row)
		{
			bool isValid = true;

			isValid &= IsValid_RequiredField(row, dataDef.FLD_BUSINESS_NOTIFICATION_NAME, "BusinessNotification Name");			
			//isValid &= IsValid_RequiredField(row, dataDef.FLD_DESCRIPTION, "BusinessNotification Description");
				
			if (!isValid)
			{
				messageManager.ValidationExceptionType = QSPFormExceptionType.RequiredFields;
			}
            
			return isValid;
		}
        
//		public dataDef SelectAll_Search(int SearchType, string Criteria)
//		{		
//			//
//			// Get the user DataTable from the DataLayer
//			//
//			return objDataAccess.SelectAll_Search(SearchType, Criteria);
//		}


		public bool SendEmail
		{
			get
			{
				return sendEmail;
			}
			set
			{
				sendEmail = value;
			}
		}

		public dataDef SelectAll()
		{
			// Get the user DataTable from the DataLayer
			return objDataAccess.SelectAll();			
		}

		public dataDef SelectOne(int ID)
		{
			// Get the user DataTable from the DataLayer
			return objDataAccess.SelectOne(ID);			
		}

		public dataDef SelectAllByAssignedUserID(int UserID)
		{
			// Get the user DataTable from the DataLayer
			return objDataAccess.SelectAllWassigned_user_idLogic(UserID);			
		}

		public dataDef SelectAllByBusinessTaskID(int BizTaskID, int EntityTypeID, int EntityID)
		{
			// Get the user DataTable from the DataLayer
			return objDataAccess.SelectAllWbusiness_task_idLogic(BizTaskID, EntityTypeID, EntityID);			
		}

		public bool CompleteAllByBusinessTaskID(int BizTaskID, int EntityTypeID, int EntityID)
		{
			// Get the user DataTable from the DataLayer
			return objDataAccess.CompleteAllWbusiness_task_idLogic(BizTaskID, EntityTypeID, EntityID);			
		}

		public dataDef SelectAll_Search(int SearchType, String Criteria, int AssignedUserID, int BusinessNotificationTypeID, int EntityTypeID, bool IsCompleted)
		{			
			dataDef dTbl;
			
			//
			// Get the user DataTable from the DataLayer
			//				
			dTbl = objDataAccess.SelectAll_Search(SearchType, Criteria, AssignedUserID, BusinessNotificationTypeID, EntityTypeID, IsCompleted);				
			
			return dTbl;			
		}	

		public dataDef SelectAll_Search(int SearchType, String Criteria, int AssignedUserID, int BusinessNotificationTypeID, int EntityTypeID)
		{			
			dataDef dTbl;
			
			//
			// Get the user DataTable from the DataLayer
			//				
			dTbl = objDataAccess.SelectAll_Search(SearchType, Criteria, AssignedUserID, BusinessNotificationTypeID, EntityTypeID);				
			
			return dTbl;			
		}

	}
}
