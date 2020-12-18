using System;
using QSPForm.Common;
using System.Diagnostics;

namespace QSPForm.Business
{
    using System;
    using System.Data;
    using QSPForm.Common.DataDef;
    using dataDef = QSPForm.Common.DataDef.ProgramAgreementCatalogTable;
    using dataAccessRef = QSPForm.Data.ProgramAgreementCatalog;

    public class ProgramAgreementCatalogSystem : BusinessSystem
    {
        dataAccessRef prgDataAccess;
		
		public ProgramAgreementCatalogSystem()
		{
			prgDataAccess = new dataAccessRef();
        }

        #region Unused methods

        public bool Insert_not_used(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.Insert(Table, prgDataAccess);			
		}

        public bool Update_not_used(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.Update(Table, prgDataAccess);			
		}

        public bool UpdateBatch_not_used(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.UpdateBatch(Table, prgDataAccess);			
		}

        public bool Delete_not_used(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.Delete(Table, prgDataAccess);
        }

        #endregion

        //----------------------------------------------------------------
		// Function Validate:
		//   Validates ProgramAgreement row
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
        //   Validates a specific ProgramAgreement Ownership Table field against his maxlength 
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

        public dataDef SelectAllByProgramAgreementID_not_used(int programAgreementId)
        {
            dataDef dTbl;
            //
            // Get the user DataTable from the DataLayer
            //
            dTbl = prgDataAccess.SelectAllProgramAgreementCatalogs(programAgreementId,EntityType.TYPE_PROGRAM_AGREEMENT);

            return dTbl;		
        }
    }
}
