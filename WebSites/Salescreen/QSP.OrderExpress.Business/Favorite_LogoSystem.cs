using System;
using QSPForm.Common;
using System.Web;

namespace QSPForm.Business
{
	
	using System;
	using System.Data;
	using QSPForm.Common.DataDef;
	using QSPForm.Data;
	using dataDef = QSPForm.Common.DataDef.Favorite_LogoTable;
	using dataAccessRef = QSPForm.Data.Favorite_Logo;

	
	/// <summary>
	///     This class contains the business rules that are used for an 
	///     Product.
	/// </summary>

	public class Favorite_LogoSystem : BusinessSystem
	{
		
		dataAccessRef objDataAccess;
		
		public Favorite_LogoSystem()
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
            /*
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
            */
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
			return true;	
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
			return true;
		}
        

		private bool IsValid_Unicity(DataRow row)
		{
			return true;
		}

		private bool IsValid_Integrity(DataRow row)
		{
		
			return true;
		
		}
		
		
		//***************************************************************
		// 
		//***************************************************************
        //public Favorite_LogoTable SelectAllByUserID(int UserID)
        //{
        //    return objDataAccess.SelectAll_SearchByUserID(UserID);            
        //}

        //public System.Collections.ArrayList SelectAllLogoIDByUserID(int UserID)
        //{
        //    Favorite_LogoTable tbl = objDataAccess.SelectAll_SearchByUserID(UserID);
        //    System.Collections.ArrayList al = new System.Collections.ArrayList();
        //    foreach (DataRow row in tbl.Rows)
        //    {
        //        al.Add(row[QSPForm.Common.DataDef.Favorite_LogoTable.FLD_LOGO_ID]);
        //    }
        //    return al;
        //}
        public QSPForm.Common.DataDef.Favorite_LogoTable SelectLogoIDByFM_ID(string FM_ID)
        {
            return objDataAccess.SelectLogoByFMID(FM_ID);
        }
        public System.Collections.ArrayList SelectAllLogoIDByFM_ID(string FM_ID)
        {
            Favorite_LogoTable tbl = objDataAccess.SelectAll_SearchByFM_ID(FM_ID);
            System.Collections.ArrayList al = new System.Collections.ArrayList();
            foreach (DataRow row in tbl.Rows)
            {
                al.Add(row[QSPForm.Common.DataDef.Favorite_LogoTable.FLD_LOGO_ID]);
            }
            return al;
        }
        public System.Collections.ArrayList SelectAllDefault()
        {
            Favorite_LogoTable tbl = objDataAccess.SelectAllDefault();
            System.Collections.ArrayList al = new System.Collections.ArrayList();
            foreach (DataRow row in tbl.Rows)
            {
                al.Add(row[QSPForm.Common.DataDef.Favorite_LogoTable.FLD_LOGO_ID]);
            }
            return al;
        }

        //public void Insert(int UserID, int LogoID)
        //{
        //    objDataAccess.Insert(UserID, LogoID);
        //}
        public void InsertWithFMID(string FMID, int LogoID)
        {
            objDataAccess.Insert(FMID, LogoID);
        }


        //public void Delete(int UserID, int LogoID)
        //{
        //    dataDef tbl = objDataAccess.SelectOne(UserID, LogoID);
        //    if (tbl.Rows.Count == 0)
        //    {
        //        throw new Exception("This Favorite_Logo UserID:" + UserID + ", LogoID:" + LogoID + " do not exists");
        //    }
        //    else
        //    {
        //        Delete(Convert.ToInt32(tbl.Rows[0][dataDef.FLD_PKID].ToString()));
        //    }
        //}
        public void Delete(string FMID, int LogoID)
        {
            dataDef tbl = objDataAccess.SelectOne(FMID, LogoID);
            if (tbl.Rows.Count == 0)
            {
                throw new Exception("This Favorite_Logo Field Sales Manager :" + FMID + ", LogoID:" + LogoID + " do not exists");
            }
            else
            {
                Delete(Convert.ToInt32(tbl.Rows[0][dataDef.FLD_PKID].ToString()));
            }
        }
                
        public void Delete(int ID)
        {
            objDataAccess.Delete(ID);
        }
		
		public dataDef SelectOne(int ID)
		{			
			return objDataAccess.SelectOne(ID);		
		}
        public dataDef SelectOne(int UserID, int LogoID)
        {
            return objDataAccess.SelectOne(UserID,LogoID);
        }
	}
}
