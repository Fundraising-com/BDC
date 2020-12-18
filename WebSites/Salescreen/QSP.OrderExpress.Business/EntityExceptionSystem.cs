using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;

using QSPForm.Common;
using QSPForm.Common.DataDef;
using QSPForm.Data;
using dataDef = QSPForm.Common.DataDef.EntityExceptionTable;
using dataAccessRef = QSPForm.Data.Entity_exception;

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
	///     This class contains the business rules that are used for an 
	///     Entity.
	/// </summary>
	public class EntityExceptionSystem : BusinessSystem
    {

        #region Version 2 code

        public LinqEntity.EntityException GetEntityException(int entityExceptionId)
        {
            LinqEntity.EntityException result = new LinqEntity.EntityException();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            result = (from ee in db.EntityExceptions
                      where ee.BusinessExceptionId == entityExceptionId
                        && ee.IsDeleted == false
                      select ee
                      ).SingleOrDefault();

            return result;
        }
        public List<LinqEntity.EntityException> GetBusinessExceptions(EntityTypeEnum entityType, int entityId)
        {
            List<LinqEntity.EntityException> result = new List<LinqEntity.EntityException>();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            result = (from ee in db.EntityExceptions
                      where ee.EntityTypeId == (int)entityType
                        && ee.EntityId == entityId
                        && ee.IsDeleted == false
                      select ee
                      ).ToList();

            return result;
        }

        #endregion

        #region Version 1 code

        dataAccessRef objDataAccess;
		
		public EntityExceptionSystem()
		{
			objDataAccess = new dataAccessRef();
		}
		public bool Insert(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.Insert(Table, objDataAccess);			
		}

		public bool Update(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.Update(Table, objDataAccess);			
		}

		public bool UpdateBatch(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.UpdateBatch(Table, objDataAccess);			
		}

		public bool Delete(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.Delete(Table, objDataAccess);			
		}

		//----------------------------------------------------------------
		// Function Validate:
		//   Validates Entity row
		// Returns:
		//   true if validation is successful 
		//   false if invalid fields exist 
		// Parameters:
		//   [in]  row: DataRow to be validated
		//   [out] row: Returns row data.  If there are fields
		//              that contain errors they are individually marked.  
		//----------------------------------------------------------------
		protected override bool Validate(DataRow row)
		{
			bool isValid = true;
            
			//Clear all errors
			row.ClearErrors();
			
			if ((row.RowState == DataRowState.Modified) || (row.RowState == DataRowState.Added))
			{
				//Apply Mandatory rules
				isValid = IsValid_RequiredFields(row);
				//Apply Maxlength rules
				isValid &= IsValid_FieldsLength(row);	
				//apply any other rules like unicity, integrity ...
				//Not for now
			}
			//Validation only for Delete Operation
			else if (row.RowState == DataRowState.Deleted)
			{
				isValid = IsValid_Integrity(row);
			}						
            
			return isValid;
		}
        
		//----------------------------------------------------------------
		// Function IsValid_FieldLength:
		//   Validates a specific Entity Ownership Table field against his maxlength 
		// Returns:
		//   False if field fails the validation rules.
		// Parameters:
		//   [in]  row: DataRow of DataTable to be validated
		//   [in]  fieldName: field in DataTable to be validated
		//   [in]  maxLen: max length for the field
		//----------------------------------------------------------------
		private bool IsValid_FieldsLength(DataRow row)
		{
			bool isValid = false;
			
			//No string variable to test
			isValid = true;
			
            
			return isValid;
		}


		//----------------------------------------------------------------
		// Function IsValid_RequiredField:
		//   Validates a specific DataTable field as Mandatory 
		// Returns:
		//   False if field fails the validation rules.
		// Parameters:
		//   [in]  row: DataRow from DataTable to be validated
		//----------------------------------------------------------------
		private bool IsValid_RequiredFields(DataRow row)
		{
			bool IsValid = true;
			//Entity
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_ENTITY_ID, "Entity");
			
			if (!IsValid)
			{
				messageManager.ValidationExceptionType = QSPFormExceptionType.RequiredFields;
			}
			            
			return IsValid;
		}
        

		private bool IsValid_Unicity(DataRow row)
		{
			
			return true;
		
		}

		private bool IsValid_Integrity(DataRow row)
		{
		
			return true;
		
		}

		public dataDef SelectOne(int ID)
		{			
			dataDef dTbl;			
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = objDataAccess.SelectOne(ID);				
			
			return dTbl;
			
		}
	

		public dataDef SelectAllByEntityID(int EntityID, int EntityTypeID)
		{			
			dataDef dTbl;
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = objDataAccess.SelectAllWentity_idLogic(EntityID, EntityTypeID);				
			
			return dTbl;			
		}

		public dataDef SelectAllByOrderID(int OrderID)
		{			
			dataDef dTbl;
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = objDataAccess.SelectAllWentity_idLogic(OrderID, EntityType.TYPE_ORDER_BILLING);				
			
			return dTbl;			
		}

		public dataDef SelectAllByAccountID(int AccountID)
		{			
			dataDef dTbl;
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = objDataAccess.SelectAllWentity_idLogic(AccountID, EntityType.TYPE_ACCOUNT);				
			
			return dTbl;			
		}

		public dataDef SelectAllByCreditApplicationID(int CreditAppID)
		{			
			dataDef dTbl;
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = objDataAccess.SelectAllWentity_idLogic(CreditAppID, EntityType.TYPE_CREDIT_APPLICATION);				
			
			return dTbl;			
		}
		
        #endregion

	}
}
