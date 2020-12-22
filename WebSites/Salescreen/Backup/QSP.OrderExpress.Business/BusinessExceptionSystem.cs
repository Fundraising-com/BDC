using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;

using QSPForm.Common.DataDef;
using QSPForm.Data;
using dataDef = QSPForm.Common.DataDef.BusinessExceptionTable;
using dataAccessRef = QSPForm.Data.Business_exception;
using QSPForm.Common;

using LinqContext = QSP.OrderExpress.Business.Context;
using LinqEntity = QSP.OrderExpress.Business.Entity;
using EntityData = QSP.OrderExpress.Common.Data;

using QSP.OrderExpress.Business.Validation;
using QSP.OrderExpress.Common.Comunication;
using QSP.OrderExpress.Common.Enum;
using QSP.OrderExpress.Common.Search;

namespace QSPForm.Business
{
    /// <summary>
	///     This class contains the business exceptions that are used for a 
	///     campaign.
	/// </summary>
	public class BusinessExceptionSystem : BusinessSystem
    {

        #region Version 1 code

        dataAccessRef objDataAccess;
		
		public BusinessExceptionSystem()
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
				//Apply Mandatory exceptions
				isValid = IsValid_RequiredFields(row);
				//Apply Maxlength exceptions
				isValid &= IsValid_FieldsLength(row);			
				//apply any other exceptions like unicity, integrity ...
				
				
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
		//   False if field fails the validation exceptions.
		// Parameters:
		//   [in]  UserRow: Row of campaign from Campaign_TABLE to be validated
		//   [in]  fieldName: field in campaignData to be validated
		//   [in]  maxLen: max length for the field
		//----------------------------------------------------------------
		private bool IsValid_FieldsLength(DataRow campaignRow)
		{
			bool isValid = true;
			
			
			isValid &= IsValid_FieldLength(campaignRow, dataDef.FLD_NAME, "Business Exception Name", 50);					
			
            
			return isValid;
		}


		//----------------------------------------------------------------
		// Function IsValid_RequiredField:
		//   Validates a specific dataDef field as Mandatory 
		// Returns:
		//   False if field fails the validation exceptions.
		// Parameters:
		//   [in]  UserRow: Row of campaign from Campaign_TABLE to be validated
		//----------------------------------------------------------------
		private bool IsValid_RequiredFields(DataRow row)
		{
			bool IsValid = true;

			//Campaign Name
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_NAME, "Business Exception Name");

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

		public dataDef SelectAllByNoAppItem(Business.AppItem NoAppItem, int FormID)
		{
			//
			// Get the DataTable from the DataLayer
			// 
			return objDataAccess.SelectAllWNoAppItemLogic(Convert.ToInt32(NoAppItem), FormID);	
		}

        public dataDef SelectAllByNoAppItem(Business.AppItem NoAppItem, int FormID, int FormSectionTypeID, int FormSectionNumber)
        {
            //
            // Get the DataTable from the DataLayer
            // 
            return objDataAccess.SelectAllWNoAppItemLogic(Convert.ToInt32(NoAppItem), FormID, FormSectionTypeID, FormSectionNumber);
        }

        public bool PerformValidation(DataSet dts, FormData dtsForm, int UserID, int EntityTypeID, int EntityID)
        {
            return PerformValidation(dts, dtsForm, UserID, EntityTypeID, EntityID, 0);

        }

		public bool PerformValidation(DataSet dts, FormData dtsForm, int UserID, int EntityTypeID, int EntityID, int ExceptionTypeID)
		{		
			bool IsValid = true;

			try
			{
				EntityExceptionTable dTblEntityException = (EntityExceptionTable)dts.Tables[EntityExceptionTable.TBL_ENTITY_EXCEPTION];
				ValidationTable dTblVal = (ValidationTable)dts.Tables[ValidationTable.TBL_VALIDATION];
				
				//DataRow valRow = dTblVal.Rows[0];
				dataDef dTblBizExc = dtsForm.BusinessException;				
				BusinessRuleSystem ruleSys  = new BusinessRuleSystem();
				DataView dvEntityExc = new DataView(dTblEntityException);
                DataView dvBizExc = new DataView(dTblBizExc);
                if (ExceptionTypeID > 0)
                    dvBizExc.RowFilter = dataDef.FLD_EXCEPTION_TYPE_ID + " = " + ExceptionTypeID.ToString();
				dvEntityExc.Sort = EntityExceptionTable.FLD_BUSINESS_EXCEPTION_ID;
                
                foreach (DataRowView vrow in dvBizExc)
				{					
					//Evaluate or re-Evaluate Biz Exception
                    DataRow row = vrow.Row;
					bool IsException = false;
					string sExpression = row[BusinessExceptionTable.FLD_EXPRESSION].ToString();
                    string sBizFeesExpression = row[BusinessExceptionTable.FLD_FEES_VALUE_EXPRESSION].ToString();
                    int excType = Convert.ToInt32(row[BusinessExceptionTable.FLD_EXCEPTION_TYPE_ID]);
                    string msg = row[BusinessExceptionTable.FLD_MESSAGE].ToString();
                    int FormSectionTypeID = 0;
                    int FormSectionNumber = 0;
                    if (!row.IsNull(BusinessExceptionTable.FLD_FORM_SECTION_TYPE_ID))
                        FormSectionTypeID = Convert.ToInt32(row[BusinessExceptionTable.FLD_FORM_SECTION_TYPE_ID]);
                    if (!row.IsNull(BusinessExceptionTable.FLD_FORM_SECTION_NUMBER))
                        FormSectionNumber = Convert.ToInt32(row[BusinessExceptionTable.FLD_FORM_SECTION_NUMBER]);

					IsException = ruleSys.EvaluateBooleanExpression(dts, dtsForm, ref sExpression, FormSectionTypeID, FormSectionNumber);
					
					int bizExcID = Convert.ToInt32(row[BusinessExceptionTable.FLD_PKID]);
					int iIndex = -1;
					iIndex = dvEntityExc.Find(bizExcID);

					//Only Add those that we cannot find
					
					if (IsException)
					{
						//Adding a new exception when is not already exist
						if (iIndex == -1)
						{
							DataRow excRow = dTblEntityException.NewRow();
							excRow[EntityExceptionTable.FLD_ENTITY_ID] = EntityID;
							excRow[EntityExceptionTable.FLD_ENTITY_TYPE_ID] = EntityTypeID;
							excRow[EntityExceptionTable.FLD_BUSINESS_EXCEPTION_ID] = row[BusinessExceptionTable.FLD_PKID];
							excRow[EntityExceptionTable.FLD_BUSINESS_EXCEPTION_NAME] = row[BusinessExceptionTable.FLD_NAME];
							excRow[EntityExceptionTable.FLD_EXCEPTION_TYPE_ID] = row[BusinessExceptionTable.FLD_EXCEPTION_TYPE_ID];
							excRow[EntityExceptionTable.FLD_EXCEPTION_TYPE_NAME] = row[BusinessExceptionTable.FLD_EXCEPTION_TYPE_NAME];
							excRow[EntityExceptionTable.FLD_EXPRESSION] = row[BusinessExceptionTable.FLD_EXPRESSION] + " = " + sExpression;
							//We verify if a Shipping Fees have to be calculated
							if (sBizFeesExpression.Length >0)
							{
                                decimal FeesAmount = ruleSys.EvaluateDecimalExpression(dts, dtsForm, ref sBizFeesExpression, FormSectionTypeID, FormSectionNumber);
								//Copy the result of the Expression of the Fees Amount
								excRow[EntityExceptionTable.FLD_FEES_VALUE_AMOUNT] = FeesAmount;
								excRow[EntityExceptionTable.FLD_FEES_VALUE_EXPRESSION] = sBizFeesExpression;
							}
							excRow[EntityExceptionTable.FLD_MESSAGE] = row[BusinessExceptionTable.FLD_MESSAGE];
							excRow[EntityExceptionTable.FLD_CREATE_USER_ID] = UserID;
							
							excRow[EntityExceptionTable.FLD_IS_VALID] = false;
							
							excRow.SetColumnError(EntityExceptionTable.FLD_IS_VALID, msg);
							IsValid = false;
							dTblEntityException.Rows.Add(excRow);

						}
						else //we just re-filling the error stuff
						{
							DataRow excRow = dvEntityExc[iIndex].Row;
							if (!Convert.ToBoolean(excRow[EntityExceptionTable.FLD_APPROVED]))
							{
								excRow.SetColumnError(EntityExceptionTable.FLD_IS_VALID, msg);
								IsValid = false;
							}
						}					

					}
					else //When it's valid, we have to check if a remainig biz exception exist
					{	
						if (iIndex > -1)
						{
							//if the exception already exist we mark it as deleted
							DataRow excRow = dvEntityExc[iIndex].Row;
//							if (!Convert.ToBoolean(excRow[EntityExceptionTable.FLD_APPROVED]))
//							{
								excRow[EntityExceptionTable.FLD_IS_VALID] = true;
								excRow[EntityExceptionTable.FLD_UPDATE_USER_ID] = UserID;
								excRow.Delete();
//							}
						}
					}
					
				}	
					
				if (!IsValid)
				{
					messageManager.HeaderText = "Some requirements have not been met, you can correct the situation or save the order as it is :";
					messageManager.SetErrorMessage(dTblEntityException);
					messageManager.ValidationExceptionType = QSPFormExceptionType.OrderBizRule;
					//valRow[ValidationTable.FLD_MESSAGE] =  messageManager.ErrorHTMLMessage;				
				}

				
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return IsValid;
		}

        #endregion

	}
}
