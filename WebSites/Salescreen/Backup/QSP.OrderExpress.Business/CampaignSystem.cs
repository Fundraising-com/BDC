using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Diagnostics;

using AccountFinderService = QSPForm.Business.com.qsp.ws.AccountFinderService;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.CampaignTable;
using dataAccessRef = QSPForm.Data.Campaign;
using QSPForm.Business.Properties;
using QSPForm.Common;
using QSPForm.Data;

using LinqContext = QSP.OrderExpress.Business.Context;
using LinqEntity = QSP.OrderExpress.Business.Entity;
using EntityData = QSP.OrderExpress.Common.Data;

using QSP.OrderExpress.Business;
using QSP.OrderExpress.Business.Validation;
using QSP.OrderExpress.Common.Comunication;
using QSP.OrderExpress.Common.Enum;
using QSP.OrderExpress.Common.Search;

namespace QSPForm.Business
{
    public class CampaignSystem : BusinessSystem
    {
        #region Version 2 code

        public LinqEntity.Campaign GetLatestCampaign(int accountId)
        {
            LinqEntity.Campaign result = new LinqEntity.Campaign();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            result = (from c in db.Campaigns
                      where c.AccountId == accountId
                      orderby c.FiscalYear descending
                      select c
                      ).FirstOrDefault();

            return result;
        }

        #endregion

        #region Version 1 code

        dataAccessRef campDataAccess;
		
		public CampaignSystem()
		{
			campDataAccess = new dataAccessRef();
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
				//Apply Business rules on the Campaign Dates
				isValid &= IsValidDates(row);
				//Only apply when this an update
				if (row.RowState == DataRowState.Modified)
				{
					//Apply Business rules on the Campaign Dates and defined collection Day
					isValid &= IsValidDateForOrder(row);
				}
			}
//			//Validation only for Delete Operation
//			else if (row.RowState == DataRowState.Deleted)
//			{
//				
//				//isValid = IsValid_Integrity(row);
//			}	
            
			return isValid;
		}
		private bool IsValidDateForOrder(DataRow campaignRow)
		{
		
//			QSPForm.Business.CollectionDaySystem colDaySyst = new CollectionDaySystem();
//			CollectionDayTable dtbColDay = colDaySyst.SelectAllByCampaignID(Convert.ToInt32(campaignRow[CampaignTable.FLD_PKID]));
//
//			DateTime dStart = Convert.ToDateTime(campaignRow[dataDef.FLD_START_DATE]);
//			DateTime dEnd = Convert.ToDateTime(campaignRow[dataDef.FLD_END_DATE]);
//						
//			foreach(DataRow row in dtbColDay.Rows)
//			{
//				
//                DateTime tempTime = Convert.ToDateTime(row[CollectionDayTable.FLD_COLLECTION_DATE]);
//				tempTime = new DateTime(tempTime.Year,tempTime.Month,tempTime.Day);
//			
//				if(dStart.CompareTo(tempTime) > 0 || dEnd.CompareTo(tempTime)< 0)
//				{
//					campaignRow.SetColumnError(dataDef.FLD_START_DATE,"A collection day, already defined in the system, is not include between the start date and the end date of the campaign.");
//					messageManager.ValidationExceptionType = QSPForm.Common.QSPFormExceptionType.OtherBusinessRules;
//					return false;
//				}
//			}
			return true;
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
			
			
			isValid &= IsValid_FieldLength(campaignRow, dataDef.FLD_NAME, "Campaign name", 100);					
			
            
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
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_NAME, "Campaign Name");
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_START_DATE, "Start Date");
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_END_DATE, "End Date");
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_PROG_TYPE_ID, "Program Type");			
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_FISCAL_YEAR, "Fiscal Year");			

			if (!IsValid)
			{
				messageManager.ValidationExceptionType = QSPFormExceptionType.RequiredFields;
			}
            
			return IsValid;
		}
        
		//----------------------------------------------------------------
		// Function IsValidDate:
		//   Validates Date fields for the Campaign
		// Returns:
		//   False if Date fields don't passe the validation rules.
		// Parameters:
		//   [in]  campaignRow : Row of dataDef from CAMPAIGN_TABLE to be validated
		//   [out] campaignRow : Returns campaign data.  If there are fields
		//                    that contain errors they are individually marked.  
		//----------------------------------------------------------------
		private bool IsValidDates(DataRow campaignRow)
		{
			
			bool isValid = false;
			DateTime dStart;
			DateTime dEnd;
			//Start Date
			dStart = Convert.ToDateTime(campaignRow[dataDef.FLD_START_DATE.ToString()].ToString());
			dEnd = Convert.ToDateTime(campaignRow[dataDef.FLD_END_DATE.ToString()].ToString());
			
			//Start Date must be less than End Date
			if (dStart > dEnd)
			{
				//
				// Mark the field as invalid
				//
				campaignRow.SetColumnError(dataDef.FLD_START_DATE,"The Start Date must be less than the End Date");
                messageManager.ValidationExceptionType = QSPFormExceptionType.OtherBusinessRules;
				return false;
			}
			
			
			isValid = true;

            
			return isValid;
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

		public dataDef SelectOne(int CampaignID)
		{
			//
			// Get the user DataTable from the DataLayer
			//QSPForm.Data.Campaign campDataAccess = new QSPForm.Data.Campaign();	
			return campDataAccess.SelectOne(CampaignID);			
		}

		public dataDef SelectOne_Info(int CampaignID)
		{
			//
			// Get the user DataTable from the DataLayer
			//QSPForm.Data.Campaign campDataAccess = new QSPForm.Data.Campaign();	
			return campDataAccess.SelectOne_Info(CampaignID);
			
		}

		public dataDef SelectAllByFMID(string FMID)
		{
			//
			// Get the user DataTable from the DataLayer
			//			
			return campDataAccess.SelectAllWfm_idLogic(FMID);					
		}

		public dataDef SelectAllByAccountID(int AccountID)
		{
			//
			// Get the user DataTable from the DataLayer
			//			
			return campDataAccess.SelectAllWaccount_idLogic(AccountID);					
		}

		public dataDef SelectAllByAccountID(int AccountID, int FiscalYear)
		{
			//
			// Get the user DataTable from the DataLayer
			//			
			return campDataAccess.SelectAllWaccount_idLogic(AccountID, FiscalYear);					
		}

		public dataDef SelectLastOneByAccountID(int AccountID)
		{
			//
			// Get the user DataTable from the DataLayer
			//			
			return campDataAccess.SelectLastOneWaccount_idLogic(AccountID);					
		}

		public CampaignData SelectAllDetail(int ID)
		{			
			//This method fill the All Data needed for an organization
			//into a predefined DataSet
			CampaignData dts = new CampaignData();
			dts.Merge(campDataAccess.SelectOne(ID));
			//Postal Address
			PostalAddressSystem addSys = new PostalAddressSystem();
			dts.Merge(addSys.SelectAllByCampaignID(ID));
			//dts.PostalAddress  = addSys.SelectAllByOrganizationID(ID);
			//Phone Number
			PhoneNumberSystem phoneSys = new PhoneNumberSystem();
			dts.Merge(phoneSys.SelectAllByCampaignID(ID));
			//dts.PhoneNumber  = phoneSys.SelectAllByOrganizationID(ID);
			//Email Addess
			EmailAddressSystem emailSys = new EmailAddressSystem();
			dts.Merge(emailSys.SelectAllByCampaignID(ID));			
			//dts.EmailAddress  = emailSys.SelectAllByOrganizationID(ID);
			return dts;
			
		}

		public bool UpdateAllDetail(CampaignData dts)
		{			
			bool IsSuccess = true;
			//This method fill the All Data needed for an organization
			//into a predefined DataSet			
			IsSuccess = UpdateBatch(dts.Campaign);
			if (!IsSuccess)
				return IsSuccess;
			//Postal Address
			PostalAddressSystem addSys = new PostalAddressSystem();
			IsSuccess = addSys.UpdateBatch(dts.PostalAddress);
			if (!IsSuccess)
				return IsSuccess;
			//Phone Number
			PhoneNumberSystem phoneSys = new PhoneNumberSystem();
			IsSuccess = phoneSys.UpdateBatch(dts.PhoneNumber);
			if (!IsSuccess)
				return IsSuccess;
			//Email Addess
			EmailAddressSystem emailSys = new EmailAddressSystem();
			IsSuccess = emailSys.UpdateBatch(dts.EmailAddress);
			
			return IsSuccess;
			
		}

		
		public dataDef SelectAll_Search(int SearchType, String Criteria, string FM_ID, int FiscalYear, int ProgramType, string SubdivisionCode, DateTime StartDate, DateTime EndDate, bool WithAllFMReportTo)
		{	
			dataDef dTbl;
			
			//
			// Get the user DataTable from the DataLayer
			//				
			dTbl = campDataAccess.SelectAll_Search(SearchType, Criteria, FM_ID, FiscalYear, ProgramType, SubdivisionCode, StartDate, EndDate, WithAllFMReportTo);				
			
			return dTbl;			
		}	

#endregion
	}
}
