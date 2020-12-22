using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;

using QSPForm.Common;
using QSPForm.Common.DataDef;
using QSPForm.Data;
using dataDef = QSPForm.Common.DataDef.ProgramTable;
using dataAccessRef = QSPForm.Data.Program;

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
	///     This class contains the business rules that are used for a 
	///     campaign.
	/// </summary>
	public class ProgramSystem : BusinessSystem
    {

        #region Version 2 code

        public LinqEntity.Program GetProgram(int programId)
        {
            LinqEntity.Program result = new LinqEntity.Program();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            result = (from p in db.Programs
                      where p.IsDeleted == false
                        && p.ProgramId == programId
                      select p
                      ).SingleOrDefault();

            return result;
        }
        public List<LinqEntity.Program> GetPrograms()
        {
            List<LinqEntity.Program> result = new List<LinqEntity.Program>();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            result = (from p in db.Programs
                      where p.IsDeleted == false
                      select p
                      ).ToList();

            return result;
        }
        public List<LinqEntity.Program> GetPrograms(int programTypeId)
        {
            List<LinqEntity.Program> result = new List<LinqEntity.Program>();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            result = (from p in db.Programs
                      where p.IsDeleted == false
                        && p.ProgramTypeId == programTypeId
                      select p
                      ).ToList();

            return result;
        }

        public LinqEntity.ProgramType GetProgramType(int programTypeId)
        {
            LinqEntity.ProgramType result = new LinqEntity.ProgramType();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            result = (from pt in db.ProgramTypes
                      where pt.Enabled == true
                        && pt.ProgramTypeId == programTypeId
                      select pt
                      ).SingleOrDefault();

            return result;
        }
        public List<LinqEntity.ProgramType> GetProgramTypes()
        {
            List<LinqEntity.ProgramType> result = new List<LinqEntity.ProgramType>();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            result = (from p in db.ProgramTypes
                      where p.Enabled == true
                      select p
                      ).ToList();

            return result;
        }

        public LinqEntity.ProductType GetProductType(int programTypeId)
        {
            LinqEntity.ProductType result = new LinqEntity.ProductType();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            result = (from ptpt in db.ProductTypeProgramTypes
                      where ptpt.ProgramTypeId == programTypeId
                      select ptpt.ProductType
                      ).SingleOrDefault();

            return result;
        }

        #endregion

        #region Version 1 code

        dataAccessRef objDataAccess;
		
		public ProgramSystem()
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
		private bool IsValid_FieldsLength(DataRow row)
		{
			bool isValid = true;
			
			
			isValid &= IsValid_FieldLength(row, dataDef.FLD_PROGRAM_NAME, "Program name", 50);					
			
            
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
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_PROGRAM_NAME, "Program name");		

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

		public dataDef InitializeProgram(int UserID)
		{
			//We prepare the DataSet for all step
			//Add a new Row
			dataDef dTbl = new dataDef();
			
			//Create a new row at start
			DataRow row;
			row = dTbl.NewRow();		
			row[dataDef.FLD_PROGRAM_NAME] = "New Program";
			row[dataDef.FLD_CREATE_USER_ID] = UserID;

			dTbl.Rows.Add(row);		
		
			return dTbl;
			
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

		public dataDef SelectAll_Search(int SearchType, String Criteria, int ProgramTypeID)
		{			
			dataDef dTbl;
			
			//
			// Get the user DataTable from the DataLayer
			//				
			dTbl = objDataAccess.SelectAll_Search(SearchType, Criteria, ProgramTypeID);				
			
			return dTbl;

        }

        #endregion

    }
}
