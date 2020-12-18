using System;
using QSPForm.Common;

namespace QSPForm.Business
{
	
	using System;
	using System.Data;
	using QSPForm.Common.DataDef;
	using QSPForm.Data;
	using dataDef = QSPForm.Common.DataDef.ProductTable;
	using dataAccessRef = QSPForm.Data.Product;
	
	/// <summary>
	///     This class contains the business rules that are used for an 
	///     Product.
	/// </summary>

	public class ProductSystem : BusinessSystem
	{
		
		dataAccessRef objDataAccess;
		
		public ProductSystem()
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
		//   Validates Product row
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
		//   Validates a specific Product Ownership Table field against his maxlength 
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
			//Product
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_NAME, "Name");
			//Product Number
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_PRODUCT_TYPE_ID, "Product Type");
			//FM Number
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_NB_UNITS, "Nb Units");
			
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
		

		public dataDef SelectAllByProductType(int ProductType)
		{			
			dataDef dTbl;
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = objDataAccess.SelectAllWproduct_type_idLogic(ProductType);				
			
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
	

		public dataDef SelectAll_Search(int SearchType, String Criteria, int ProductType, int VendorID)
		{			
			dataDef dTbl;
			
			//
			// Get the user DataTable from the DataLayer
			//				
			dTbl = objDataAccess.SelectAll_Search(SearchType, Criteria, ProductType, VendorID);				
			
			return dTbl;			
		}				

	}
}
