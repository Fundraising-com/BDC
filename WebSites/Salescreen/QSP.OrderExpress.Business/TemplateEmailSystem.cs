namespace QSPForm.Business
{
	
	using System;
	using System.Data;
	using QSPForm.Common.DataDef;
	using QSPForm.Data;
	using dataDef = QSPForm.Common.DataDef.TemplateEmailTable;
	using dataAccessRef = QSPForm.Data.TemplateEmail;
	using QSPForm.Common;
	
	/// <summary>
	///     This class contains the business rules that are used for a 
	///     campaign.
	/// </summary>
	public class TemplateEmailSystem : BusinessSystem  
	{
		dataAccessRef objDataAccess;
		
		public TemplateEmailSystem()
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
				//Apply Mandatory rules
				isValid = IsValid_RequiredFields(row);
				//Apply Maxlength rules
				isValid &= IsValid_FieldsLength(row);			
				//apply any other rules like unicity, integrity ...
				
				
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
		//   False if field fails the validation rules.
		// Parameters:
		//   [in]  UserRow: Row of campaign from Campaign_TABLE to be validated
		//   [in]  fieldName: field in campaignData to be validated
		//   [in]  maxLen: max length for the field
		//----------------------------------------------------------------
		private bool IsValid_FieldsLength(DataRow campaignRow)
		{
			bool isValid = true;
			
			
			isValid &= IsValid_FieldLength(campaignRow, dataDef.FLD_TEMPLATE_EMAIL_NAME, "Template Email name", 100);					
			
            
			return isValid;
		}


		//----------------------------------------------------------------
		// Function IsValid_RequiredField:
		//   Validates a specific dataDef field as Mandatory 
		// Returns:
		//   False if field fails the validation rules.
		// Parameters:
		//   [in]  UserRow: Row of campaign from Campaign_TABLE to be validated
		//----------------------------------------------------------------
		private bool IsValid_RequiredFields(DataRow row)
		{
			bool IsValid = true;

			//Campaign Name
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_TEMPLATE_EMAIL_NAME, "Template Email name");		

			if (!IsValid)
			{
				messageManager.ValidationExceptionType = QSPFormExceptionType.RequiredFields;
			}
            
			return IsValid;
		}
        
		

		public bool IsValid_Unicity(DataRow row)
		{
			
			//bool isValid = false;
			//
			// Ensure that User Name does not already exist in the database.
			//
//			dataDef existing = GetCustomerByEmail(row[UserData.FLD_USER_NAME].ToString());
//			if(existingUser != null)
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
			
			return true;
		
		}

		private bool IsValid_Integrity(DataRow row)
		{
		
			//
			// Ensure that no task are associated to this Template Email.
			//			
//			int partID = Convert.ToInt32(row[tableRef.FLD_PKID, DataRowVersion.Original]);
//			ParticipantScheduleTable dTblSchedule = new ParticipantScheduleTable();
//			QCAP.Data.Participant_schedule scheduleDataAccess = new QCAP.Data.Participant_schedule();
//			dTblSchedule = scheduleDataAccess.SelectAllWparticipant_idLogic(partID);
//			
//			if ( dTblSchedule.Rows.Count > 0)
//			{
//				//
//				// The Participant is associated to at least one Schedule
//				//
//				row.RejectChanges();
//				string msg = messageManager.FormatErrorMessage(QCAPMessage.VALMSG_INTEGRITY, new String[] {"participant","schedule"});
//				row.SetColumnError(tableRef.FLD_PKID, msg); 
//				messageManager.ValidationExceptionType = QSPFormExceptionType.Integrity;
//			    
				return false;
//			
//			}
		
		}

		public dataDef SelectAll()
		{
			//
			// Get the user DataTable from the DataLayer
			//	
			return objDataAccess.SelectAll();			
		}

		public dataDef SelectOne(int ID)
		{
			//
			// Get the user DataTable from the DataLayer
			//	
			return objDataAccess.SelectOne(ID);			
		}

		public dataDef SelectAll_Search(int SearchType, String Criteria)
		{		
			
			//
			// Get the user DataTable from the DataLayer
			//	
			return objDataAccess.SelectAll_Search(SearchType, Criteria);				
			
		}

		internal bool BuildBizNoteMessage(int TemplateEmailID, int ParamValue, BusinessNotificationTable dTblNote, string sCustomMessage, Data.ConnectionProvider connProvider)
		{
			bool IsSuccess = false;
			dataDef dTblEmail = new dataDef();
			dTblEmail = SelectOne(TemplateEmailID);
			if (dTblEmail.Rows.Count > 0)
			{
				DataRow tempEmail = dTblEmail.Rows[0];
				string sp_name = "";
				if (!tempEmail.IsNull(dataDef.FLD_TEMPLATE_EMAIL_SP))
					sp_name = tempEmail[dataDef.FLD_TEMPLATE_EMAIL_SP].ToString();
				string param_name = "";
				if (!tempEmail.IsNull(dataDef.FLD_PARAMETER_NAME))
					param_name = tempEmail[dataDef.FLD_PARAMETER_NAME].ToString();
				//Parse the email with the columns return by the Stored Proc
				Data.Common comDataAccess = new Data.Common();
				if (connProvider != null)
				{
					comDataAccess.MainConnectionProvider = connProvider;
				}
				string strSubject = tempEmail[dataDef.FLD_SUBJECT].ToString();
				string strBodyText = tempEmail[dataDef.FLD_BODY_TEXT].ToString();
				string strBodyHTML = tempEmail[dataDef.FLD_BODY_HTML].ToString();
				
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
					DataSet dts = comDataAccess.Select(spCmd);
					if (dts.Tables.Count > 0)
					{
						DataTable dTblDataToParse = dts.Tables[0];
						if (dTblDataToParse.Rows.Count > 0)
						{
							DataRow rowToParse = dTblDataToParse.Rows[0];
							foreach(DataColumn col in dTblDataToParse.Columns)
							{
								string tagName = "[" + col.ColumnName + "]";
								string tagValue = rowToParse[col.ColumnName].ToString();
								strSubject = strSubject.Replace(tagName,tagValue);
								strBodyText = strBodyText.Replace(tagName,tagValue);
								strBodyHTML = strBodyHTML.Replace(tagName,tagValue);											
							}
							if (sCustomMessage.Length >0)
							{
								strBodyText = strBodyText.Replace("[Message]",sCustomMessage);
								strBodyHTML = strBodyHTML.Replace("[Message]",sCustomMessage);
							}
						}					
					}
				}
				//Fill Note Table
				
				foreach (DataRow noteRow in dTblNote.Rows)
				{
					noteRow[BusinessNotificationTable.FLD_SUBJECT] = strSubject;
					noteRow[BusinessNotificationTable.FLD_MESSAGE] = strBodyHTML;
				}
				IsSuccess = true;

			}
			return IsSuccess;
		}

		//This Function can only be used with the Mass Mailer that not generate himnself his header.
		public string JoinEmail(string BodyText, string BodyHTML)
		{
			string sHeader= "MIME-Version: 1.0\nContent-Type: multipart/alternative; boundary=boundary-1\n\n--boundary-1\n" +
				"Content-Type: text/plain; charset=us-ascii\n\n";
			string sMiddleEmail = "\n--boundary-1\nContent-Type: text/html; charset=us-ascii\n\n";
			string sEndEmail = "\n--boundary-1--\n\n";
			System.Text.StringBuilder sbCompleteBody = new System.Text.StringBuilder();
			sbCompleteBody.Append(sHeader);
			sbCompleteBody.Append(BodyText);
			sbCompleteBody.Append(sMiddleEmail);
			sbCompleteBody.Append(BodyHTML);
			sbCompleteBody.Append(sEndEmail);
			return sbCompleteBody.ToString();
		}

	}
}
