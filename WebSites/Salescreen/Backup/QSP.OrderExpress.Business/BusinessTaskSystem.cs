namespace QSPForm.Business
{
	
	using System;
	using System.Data;
	using QSPForm.Common.DataDef;
	using QSPForm.Data;
	using dataDef = QSPForm.Common.DataDef.BusinessTaskTable;
	using dataAccessRef = QSPForm.Data.Business_task;
	using QSPForm.Common;
	
	/// <summary>
	///     This class contains the business tasks that are used for a 
	///     campaign.
	/// </summary>
	public class BusinessTaskSystem : BusinessSystem  
	{
		dataAccessRef objDataAccess;
		
		public BusinessTaskSystem()
		{
			objDataAccess = new dataAccessRef();
		}
		/// <summary>
		///     Validates and inserts a new Campaign
		///     <remarks>   
		///         Returns user data.  If there are fields that contain errors 
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
			return this.Insert(Table, new dataAccessRef());			
		}

		public bool Update(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.Update(Table, new dataAccessRef());			
		}

		public bool UpdateBatch(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.UpdateBatch(Table, new dataAccessRef());			
		}

		public bool Delete(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.Delete(Table, new dataAccessRef());			
		}

		//----------------------------------------------------------------
		// Function Validate:
		//   Validates campaign
		// Returns:
		//   true if validation is successful 
		//   false if invalid fields exist 
		// Parameters:
		//   [in]  row: CampaignTable to be validated
		//   [out] row: Returns Campaign data.  If there are fields
		//              that contain errors they are individually marked.  
		//----------------------------------------------------------------
		protected override bool Validate(DataRow row)
		{
			bool isValid = true;
            
			//Clear all errors
			row.ClearErrors();
			if ((row.RowState == DataRowState.Added) || (row.RowState == DataRowState.Modified))
			{
				//Apply Mandatory tasks
				isValid = IsValid_RequiredFields(row);
				//Apply Maxlength tasks
				isValid &= IsValid_FieldsLength(row);			
				//apply any other tasks like unicity, integrity ...
				
				
			}
//			//Validation only for Delete Operation
//			else if (row.RowState == DataRowState.Deleted)
//			{
//				
//				//isValid = IsValid_Integrity(row);
//			}	
            
			return isValid;
		}
		
		//----------------------------------------------------------------
		// Function IsValid_FieldsLength:
		//   Validates a specific dataDef field against his maxlength 
		// Returns:
		//   False if field fails the validation tasks.
		// Parameters:
		//   [in]  UserRow: Row of campaign from Campaign_TABLE to be validated
		//   [in]  fieldName: field in campaignData to be validated
		//   [in]  maxLen: max length for the field
		//----------------------------------------------------------------
		private bool IsValid_FieldsLength(DataRow campaignRow)
		{
			bool isValid = true;
			
			
			isValid &= IsValid_FieldLength(campaignRow, dataDef.FLD_NAME, "Business Task Name", 50);					
			
            
			return isValid;
		}


		//----------------------------------------------------------------
		// Function IsValid_RequiredField:
		//   Validates a specific dataDef field as Mandatory 
		// Returns:
		//   False if field fails the validation tasks.
		// Parameters:
		//   [in]  UserRow: Row of campaign from Campaign_TABLE to be validated
		//----------------------------------------------------------------
		private bool IsValid_RequiredFields(DataRow row)
		{
			bool IsValid = true;

			//Campaign Name
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_NAME, "Business Task Name");

			if (!IsValid)
			{
				messageManager.ValidationExceptionType = QSPFormExceptionType.RequiredFields;
			}
            
			return IsValid;
		}
        
		

		private bool IsValid_Unicity(DataRow row)
		{
			
			bool isValid = false;
			//
			// Ensure that User Name does not already exist in the database.
			//
//			dataDef existing = GetCustomerByEmail(row[UserData.FLD_USER_NAME].ToString());
//            
//			if ( null != existingUser )
//			{
//				//
//				// Email is not unique - make sure the email address belongs this customer
//				//
//				if ( userRow[UserData.FLD_PKID].ToString() != existingUser.Tables[UserData.TBL_USERS].Rows[0][UserData.FLD_PKID].ToString() )
//				{
//					//
//					// User PKID does not match, so this would create a duplicate email address
//					//
//					userRow.SetColumnError(UserData.FLD_USER_NAME, "The User Name aready exist"); //email field is not unique
//					userRow.RowError = "The Email already exist";
//                    
//					return false;
//				}
//			}
//			
			return true;
		
		}

		public dataDef SelectAll()
		{
			//
			// Get the DataTable from the DataLayer
			//
			return objDataAccess.SelectAll();			
		}

		public dataDef SelectOne(int ID)
		{
			//
			// Get the DataTable from the DataLayer
			//
			return objDataAccess.SelectOne(ID);			
		}

		
		public dataDef SelectAllByFormID(int FormID)
		{
			//
			// Get the DataTable from the DataLayer
			//
			return objDataAccess.SelectAllWform_idLogic(FormID);			
		}

		public bool PerformTask(DataSet dts, FormData dtsForm, int UserID, int ParamValue, int EntityTypeID, Data.ConnectionProvider connProvider)
		{		
			bool IsValid = true;

			try
			{	
				ValidationTable dTblVal = (ValidationTable)dts.Tables[ValidationTable.TBL_VALIDATION];
				DataRow valRow = dTblVal.Rows[0];
                string sFMID = "";
                if (EntityTypeID == EntityType.TYPE_ORDER_BILLING)
                    sFMID = valRow[ValidationTable.FLD_ORDER_FM_ID].ToString();
                else
                    sFMID = valRow[ValidationTable.FLD_ACCOUNT_FM_ID].ToString();
				dataDef dtblBizTask = dtsForm.BusinessTask;
				BusinessRuleSystem ruleSys  = new BusinessRuleSystem();
                int sysUserID = 0;


				foreach (DataRow row in dtblBizTask.Rows)
				{
					bool IsValidTask = false;
					string sExpression = row[BusinessTaskTable.FLD_EXPRESSION].ToString();
					
					IsValidTask = ruleSys.EvaluateBooleanExpression(dts, dtsForm, ref sExpression, 1, 0);
					int taskID = Convert.ToInt32(row[BusinessTaskTable.FLD_TASK_ID]);
					IsValid = false;
					TaskSystem taskSys = new TaskSystem();
					TaskTable dTblTask = new TaskTable();
					dTblTask = taskSys.SelectOne(taskID);
                    

					if (dTblTask.Rows.Count > 0)
					{
						DataRow taskRow = dTblTask.Rows[0];
						int taskType = Convert.ToInt32(taskRow[TaskTable.FLD_TASK_TYPE_ID]);
			
						if (taskType == TaskType.SEND_NOTIFICATION) 
						{
							//Perform to the Task System
							if (IsValidTask)
                                IsValid = PerformTask_SendNotification(row, taskRow, ParamValue, EntityTypeID, UserID, sFMID, connProvider);
							
						} 
						else if (taskType == TaskType.MANAGE_TODO) 
						{
							//We run whatever is valid or not
							IsValid = PerformTask_ManageToDo(row, taskRow, ParamValue, IsValidTask, EntityTypeID, connProvider);
						
						}
						else if (taskType == TaskType.EXECUTE_SQL) 
						{
							//Perform to the Task System
							if (IsValidTask)
							{
								//We run whatever is valid or not
								IsValid = PerformTask_ExecuteSQL(taskRow, ParamValue, connProvider);
							}
						}
					}

				}				
				
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return IsValid;
		}

		internal bool PerformTask_SendNotification(DataRow bizTaskRow, DataRow taskRow, int ParamValue, int EntityTypeID, int UserID, string sFMID, Data.ConnectionProvider connProvider)
		{		
			bool IsValid = true;
			//Task -- Send Email Notification
			try
			{		
				Data.Business_notification noteDataAccess = new Data.Business_notification();	
			
				BusinessNotificationTable dTblNote = new BusinessNotificationTable();
				if (connProvider != null)
					noteDataAccess.MainConnectionProvider = connProvider;
				int BizTaskID = Convert.ToInt32(bizTaskRow[BusinessTaskTable.FLD_PKID]);
				
				//*************************************************************
				//			Assignment Type
				//		Get One or All User depending on the Assignment Type
				//*************************************************************
				UserTable dTblUser = new UserTable();
				Data.User userDataAccess = new Data.User();
                if (connProvider != null)
                    userDataAccess.MainConnectionProvider = connProvider;
				int AssignType = Convert.ToInt32(bizTaskRow[BusinessTaskTable.FLD_ASSIGNMENT_TYPE_ID]);
				if (AssignType == BizTask_AssignmentType.SPECIFIC_USER)
				{
					int AssignUserID = Convert.ToInt32(bizTaskRow[BusinessTaskTable.FLD_ASSIGNED_USER_ID]);
					//Fill the info on the specific user
					dTblUser = userDataAccess.SelectOne(AssignUserID);										
				}
				else if (AssignType == BizTask_AssignmentType.CURRENT_USER)
				{
					//Fill the info on the current user
					dTblUser = userDataAccess.SelectOne(UserID);	
				}
				else if (AssignType == BizTask_AssignmentType.SPECIFIC_ROLE)
				{
					//Fill the info on the current user
					int AssignRoleID = -1;
					if (!bizTaskRow.IsNull(BusinessTaskTable.FLD_ASSIGNED_ROLE_ID))
						AssignRoleID = Convert.ToInt32(bizTaskRow[BusinessTaskTable.FLD_ASSIGNED_ROLE_ID]);
					if (AssignRoleID > -1)
						dTblUser = userDataAccess.SelectAllWrole_idLogic(AssignRoleID);
				}
				else if (AssignType == BizTask_AssignmentType.CURRENT_FSM)
				{
					//Fill the info on the current fsm					
					dTblUser = userDataAccess.SelectOneFSM_UserWentity_type_idLogic(EntityTypeID, ParamValue);	
				}
				
				//Create And Insert Notification
				foreach (DataRow userRow in dTblUser.Rows)
				{
					DataRow noteRow = dTblNote.NewRow();
                    //Note Type
                    if (!taskRow.IsNull(TaskTable.FLD_BUSINESS_NOTIFICATION_TYPE_ID))                    
                        noteRow[BusinessNotificationTable.FLD_BUSINESS_NOTIFICATION_TYPE_ID] = taskRow[TaskTable.FLD_BUSINESS_NOTIFICATION_TYPE_ID];                    
                    else
					    noteRow[BusinessNotificationTable.FLD_BUSINESS_NOTIFICATION_TYPE_ID] =  BizNotificationType.GENERAL_NOTIFICATION;

					noteRow[BusinessNotificationTable.FLD_BUSINESS_TASK_ID] = bizTaskRow[BusinessTaskTable.FLD_PKID];
					noteRow[BusinessNotificationTable.FLD_BUSINESS_NOTIFICATION_NAME] = bizTaskRow[BusinessTaskTable.FLD_NAME];
					noteRow[BusinessNotificationTable.FLD_ENTITY_TYPE_ID] = EntityTypeID;
					noteRow[BusinessNotificationTable.FLD_ENTITY_ID] = ParamValue;
					noteRow[BusinessNotificationTable.FLD_DESCRIPTION] = bizTaskRow[BusinessTaskTable.FLD_DESCRIPTION];
					noteRow[BusinessNotificationTable.FLD_ASSIGNED_USER_ID] = userRow[UserTable.FLD_PKID];
                    noteRow[BusinessNotificationTable.FLD_CREATE_USER_ID] = 0; //Sys User-- UserID;					
					noteRow[BusinessNotificationTable.FLD_MESSAGE] = bizTaskRow[BusinessTaskTable.FLD_MESSAGE];
					
					dTblNote.Rows.Add(noteRow);
					
				}
				//**********************************************
				//			Template Email Message
				//		We do at the end for all Note
				//		It'a not customized by user (ToDo next) 
				//**********************************************
				string strCustomMessage = bizTaskRow[BusinessTaskTable.FLD_MESSAGE].ToString();
				if (!taskRow.IsNull(TaskTable.FLD_TEMPLATE_EMAIL_ID))
				{
					int TmplEmailId = Convert.ToInt32(taskRow[TaskTable.FLD_TEMPLATE_EMAIL_ID]);	
					TemplateEmailSystem emailSys = new TemplateEmailSystem();
					emailSys.BuildBizNoteMessage(TmplEmailId, ParamValue, dTblNote, strCustomMessage, connProvider);
				}

				//Insert All in the ToDo Table
				noteDataAccess.SendEmail = true;
				noteDataAccess.Insert(dTblNote);

				IsValid = true;				
				
			}
			catch (Exception ex)
			{
				throw ex;//string msg = ex.Message;
			}					
			
			return IsValid;
		}


		public bool PerformTask_ManageToDo(DataRow bizTaskRow, DataRow taskRow, int ParamValue, bool ToBeAssigned, int EntityTypeID, Data.ConnectionProvider connProvider)
		{		
			bool IsValid = false;
		
			try
			{
				Data.Business_notification todoDataAccess = new Data.Business_notification();
				if (connProvider != null)
					todoDataAccess.MainConnectionProvider = connProvider;
				int BizTaskID = Convert.ToInt32(bizTaskRow[BusinessTaskTable.FLD_PKID]);
					
				if (ToBeAssigned)
				{
					//Task -- Assign ToDo
					//Do a Check to see if the ToDo is already Assigned

					BusinessNotificationTable toDo = new BusinessNotificationTable();					
					toDo = todoDataAccess.SelectAllWbusiness_task_idLogic(BizTaskID, EntityTypeID, ParamValue);
					if (toDo.Rows.Count ==0)
					{
						//Create ToDo
						DataRow toDoRow = toDo.NewRow();
						toDoRow[BusinessNotificationTable.FLD_BUSINESS_NOTIFICATION_TYPE_ID] = BizNotificationType.TODO;
						toDoRow[BusinessNotificationTable.FLD_BUSINESS_TASK_ID] = bizTaskRow[BusinessTaskTable.FLD_PKID];
						toDoRow[BusinessNotificationTable.FLD_BUSINESS_NOTIFICATION_NAME] = bizTaskRow[BusinessTaskTable.FLD_NAME];
						toDoRow[BusinessNotificationTable.FLD_ENTITY_TYPE_ID] = EntityTypeID;
						toDoRow[BusinessNotificationTable.FLD_ENTITY_ID] = ParamValue;
						toDoRow[BusinessNotificationTable.FLD_ASSIGNED_USER_ID] = bizTaskRow[BusinessTaskTable.FLD_ASSIGNED_USER_ID];
						toDoRow[BusinessNotificationTable.FLD_DESCRIPTION] = bizTaskRow[BusinessTaskTable.FLD_DESCRIPTION];
						string strCustomMessage = bizTaskRow[BusinessTaskTable.FLD_MESSAGE].ToString();
						
						toDoRow[BusinessNotificationTable.FLD_CREATE_USER_ID] = 0;	
						//Insert in the ToDo Table
						toDo.Rows.Add(toDoRow);

						if (!taskRow.IsNull(TaskTable.FLD_TEMPLATE_EMAIL_ID))
						{
							int TmplEmailId = Convert.ToInt32(taskRow[TaskTable.FLD_TEMPLATE_EMAIL_ID]);	
							TemplateEmailSystem emailSys = new TemplateEmailSystem();
							emailSys.BuildBizNoteMessage(TmplEmailId, ParamValue, toDo, strCustomMessage, connProvider);
						}

						//Commit to DB						
						todoDataAccess.SendEmail = true;
						todoDataAccess.Insert(toDo);
						IsValid = true;
					}
					else
					{
						//Modification of the ToDo... Nothing for now
						IsValid = true;
					}
				}
				else //Resolved ToDo
				{	
					todoDataAccess.CompleteAllWbusiness_task_idLogic(BizTaskID, EntityTypeID, ParamValue);
					IsValid = true;
				}
				
			}
			catch (Exception ex)
			{
				string msg = ex.Message;
			}
				
			
			return IsValid;
		}

		public bool PerformTask_ExecuteSQL(DataRow taskRow, int ParamValue, Data.ConnectionProvider connProvider)
		{		
			bool IsValid = false;
				
			//Task -- Execute SQL
			try
			{		
				string sp_name = "";
				if (!taskRow.IsNull(TaskTable.FLD_TASK_SP))
					sp_name = taskRow[TaskTable.FLD_TASK_SP].ToString();
				string param_name = "";
				if (!taskRow.IsNull(TaskTable.FLD_PARAMETER_NAME))
					param_name = taskRow[TaskTable.FLD_PARAMETER_NAME].ToString();
				
				if (sp_name.Length > 0)
				{
					System.Data.SqlClient.SqlCommand spCmd = new System.Data.SqlClient.SqlCommand();	
				
					spCmd.CommandText = sp_name;
					spCmd.CommandType = CommandType.StoredProcedure;
					if (param_name.Length > 0)
					{
						spCmd.Parameters.Add(param_name, SqlDbType.Int, 4);
						spCmd.Parameters[param_name].Value = ParamValue;
					}
					Data.Common comDataAccess = new Data.Common();
					if (connProvider != null)
							comDataAccess.MainConnectionProvider = connProvider;
					bool IsSuccess = comDataAccess.ExecuteCmd(spCmd);
					
				}
				
			}
			catch (Exception ex)
			{
				string msg = ex.Message;
			}
			
			return IsValid;
		}

	}
}
