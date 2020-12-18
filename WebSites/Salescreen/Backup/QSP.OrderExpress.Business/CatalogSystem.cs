using System;
using System.Data;
using System.Linq;

using QSPForm.Common;
using QSPForm.Common.DataDef;
using QSPForm.Data;

using dataDef = QSPForm.Common.DataDef.CatalogTable;
using dataAccessRef = QSPForm.Data.Catalog;

using LinqContext = QSP.OrderExpress.Business.Context;
using LinqEntity = QSP.OrderExpress.Business.Entity;

namespace QSPForm.Business
{
	
	
	/// <summary>
	///     This class contains the business rules that are used for an 
	///     Catalog.
	/// </summary>

	public class CatalogSystem : BusinessSystem
    {
        #region Refactored code

        public bool IsCatalogAvailable(int formId, bool isPriced)
        {
            bool result = false;

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            var query = from fs in db.FormSections
                            join cic in db.CatalogItemCategories on fs.CatalogItemCategoryId equals cic.CatalogItemCategoryId
                            join c in db.Catalogs on cic.CatalogId equals c.CatalogId
                        where fs.FormId == formId
                            && c.IsPriced == isPriced
                        select c.CatalogId;

            int count = query.Count();

            if (count > 0)
            {
                result = true;
            }

            return result;
        }

        #endregion

        dataAccessRef objDataAccess;
		
		public CatalogSystem()
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
		//   Validates Catalog row
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
		//   Validates a specific Catalog Ownership Table field against his maxlength 
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
			//Catalog
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_NAME, "Name");
			//Catalog Number
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_CATALOG_GROUP_ID, "Catalog Group");
			
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

		public dataDef SelectAll()
		{			
			dataDef dTbl;			
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = objDataAccess.SelectAll();				
			
			return dTbl;
			
		}
		

		public dataDef SelectAllByCatalogGroupID(int CatalogGroupID)
		{			
			dataDef dTbl;
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = objDataAccess.SelectAllWcatalog_group_idLogic(CatalogGroupID);				
			
			return dTbl;			
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
	

		public dataDef SelectAll_Search(int SearchType, string Criteria, string Culture, DateTime StartDate, DateTime EndDate)
		{			
			dataDef dTbl;
			
			//
			// Get the user DataTable from the DataLayer
			//				
			dTbl = objDataAccess.SelectAll_Search(SearchType, Criteria, Culture, StartDate, EndDate);				
			
			return dTbl;			
		}			
	
		public CatalogData SelectAllDetail(int ID)
		{			
			//This method fill the All Data needed for an organization
			//into a predefined DataSet
			CatalogData dts = new CatalogData();
			dts.Merge(objDataAccess.SelectOne(ID));
			//Catalog Group Catalog (Association between Catalog and Catalog Group)
			CatalogGroupCatalogSystem grpCatSys = new CatalogGroupCatalogSystem();
			dts.Merge(grpCatSys.SelectByCatalogID(ID));
			//Catalog Item
			CatalogItemSystem catItemSys = new CatalogItemSystem();
			dts.Merge(catItemSys.SelectAllByCatalogID(ID));
			//Catalog Item Category
			CatalogItemCategorySystem categSys = new CatalogItemCategorySystem();
			dts.Merge(categSys.SelectAllByCatalogID(ID));
			
			return dts;
			
		}

		public bool UpdateAllDetail(CatalogData dts)
		{			
			bool IsSuccess = true;
			//This method fill the All Data needed for an organization
			//into a predefined DataSet			
//			if (dts.Form.GetChanges() != null)
//			{
//				IsSuccess = UpdateBatch(dts.Form);
//				if (!IsSuccess)
//					return IsSuccess;			
//			}
//			PrepareTransactionWithNewID(dts);
//			//Business Rule
//			if (dts.BusinessRule.GetChanges() != null)
//			{
//				BusinessRuleSystem bizSys = new BusinessRuleSystem();
//				IsSuccess = bizSys.UpdateBatch(dts.BusinessRule);
//				if (!IsSuccess)
//					return IsSuccess;
//			}
//			//Business Exception
//			if (dts.BusinessException.GetChanges() != null)
//			{
//				BusinessExceptionSystem bizExcSys = new BusinessExceptionSystem();
//				IsSuccess = bizExcSys.UpdateBatch(dts.BusinessException);
//				if (!IsSuccess)
//					return IsSuccess;
//			}
//			//Business Task
//			if (dts.BusinessTask.GetChanges() != null)
//			{
//				BusinessTaskSystem bizTaskSys = new BusinessTaskSystem();
//				IsSuccess = bizTaskSys.UpdateBatch(dts.BusinessTask);
//				if (!IsSuccess)
//					return IsSuccess;
//			}
			return IsSuccess;
			
		}

		private void PrepareTransactionWithNewID(CatalogData dts)
		{
//			int NewID = Convert.ToInt32(dts.Catalog.Rows[0][CatalogTable.FLD_PKID]);
//			foreach(DataRow row in dts.BusinessRule.Rows)
//			{
//				if (row.RowState == DataRowState.Added)
//				{
//					row[BusinessRuleTable.FLD_FORM_ID] = NewID;
//				}
//			}
//			foreach(DataRow row in dts.BusinessException.Rows)
//			{
//				if (row.RowState == DataRowState.Added)
//				{
//					row[BusinessExceptionTable.FLD_FORM_ID] = NewID;
//				}
//			}
//			foreach(DataRow row in dts.BusinessTask.Rows)
//			{
//				if (row.RowState == DataRowState.Added)
//				{
//					row[BusinessTaskTable.FLD_FORM_ID] = NewID;
//				}
//			}		
		}





	}
}
