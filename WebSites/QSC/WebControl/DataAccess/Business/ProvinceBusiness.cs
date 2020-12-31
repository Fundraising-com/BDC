namespace QSP.WebControl.DataAccess.Business
{
	using System;
	using System.Data;
	using QSP.WebControl.DataAccess.Common.TableDef;
	using QSP.WebControl.DataAccess.Data;
	using tableRef = QSP.WebControl.DataAccess.Common.TableDef.ProvinceTable;
	using dataAccessRef =QSP.WebControl.DataAccess.Data.ProvinceData;
	using QSP.WebControl.DataAccess.Common;
	
	[Serializable]
	public enum CountryCode
	{
		CA,
		US,
		All
	}

	[Serializable]
	public enum TaxRegion 
	{
		All = 0,
		GST = 1,
		HST = 2
	}

	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	internal class ProvinceBusiness : QSP.WebControl.DataAccess.Business.BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		
		/*public bool Delete(tableRef Table)
		{
			//We call a method from the inherit class, but the
			//validation is provide by the overriden Validate Method 
			//is in the current class
			return this.Delete(Table,dataAccess);
		}
		public bool Insert(tableRef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.Insert(Table,dataAccess);	
		}
		public bool UpdateBatch(tableRef Table)
		{			
			//We call a method from the inherit class, but the
			//validation is provide by the overriden Validate Method 
			//is in the current class
			return this.UpdateBatch(Table,dataAccess);
		}
		public bool Update(tableRef Table)
		{
			//We call a method from the inherit class, but the
			//validation is provide by the overriden Validate Method 
			//is in the current class
			return this.Update(Table, dataAccess);
		}*/
		public void SelectAll(DataTable table)
		{
			try
			{	
				dataAccess.DataBase = DataBaseName.QSPCanadaCommon;
				dataAccess.SelectAll(table);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new QspException(messageManager);
			}
		}

		public void SelectByCountryCode(DataTable table, CountryCode code)
		{
			try
			{	
				dataAccess.DataBase = DataBaseName.QSPCanadaCommon;
				dataAccess.SelectByCountryCode(table, code.ToString());
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new QspException(messageManager);
			}	
		}

		public void SelectByTaxRegion(DataTable table, TaxRegion taxRegion)
		{
			try
			{	
				dataAccess.DataBase = DataBaseName.QSPCanadaCommon;
				dataAccess.SelectByTaxRegionID(table, Convert.ToInt32(taxRegion));
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new QspException(messageManager);
			}	
		}

		/*//----------------------------------------------------------------
		// Function Validate:
		//   Validates 
		// Returns:
		//   true if validation is successful 
		//   false if invalid fields exist 
		// Parameters:
		//   [in]  row: to be validated
		//   [out] row: If there are fields
		//              that contain errors they are individually marked.  
		//----------------------------------------------------------------
		protected override bool Validate(DataRow Row)
		{
			bool isValid = true;
			//Clear all errors
			Row.ClearErrors();
			if ((Row.RowState == DataRowState.Added) || (Row.RowState == DataRowState.Modified))
			{
				isValid = IsValid_RequiredFields(Row);
				isValid &= IsValids_FieldLength(Row);
			}
			return isValid;
		}
		//----------------------------------------------------------------
		// Function IsValid_RequiredField:
		//   Validates a specific tableRef field as Mandatory 
		// Returns:
		//   False if field fails the validation rules.
		// Parameters:
		//   [in]  Row: Row to be validated
		//----------------------------------------------------------------
		private bool IsValid_RequiredFields(DataRow Row)
		{
			bool IsValid = true;
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_LAPSE_DAYS_DELIVERY,"LAPSE_DAYS_DELIVERY");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_LAPSE_DAYS_FIELD_SUPPLY_PREP,"LAPSE_DAYS_FIELD_SUPPLY_PREP");
			if (!IsValid)
			{
				errorType = BusinessErrorType.RequiredFields;
			}
			return IsValid;
		}
		//----------------------------------------------------------------
		// Function IsValid_FieldLength:
		//   Validates a specific tableRef field against his maxlength 
		// Returns:
		//   False if field fails the validation rules.
		// Parameters:
		//   [in]  Row: Row to be validated
		//   [in]  fieldName: field in to be validated
		//   [in]  maxLen: max length for the field
		//----------------------------------------------------------------
		private bool IsValids_FieldLength(DataRow Row)
		{
			bool isValid = true;
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_COUNTRY_CODE,"", tableRef.FLD_COUNTRY_CODE_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_PROVINCE_CODE, tableRef.FLD_PROVINCE_CODE_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_PROVINCE_NAME, tableRef.FLD_PROVINCE_NAME_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_TAX_BACKOUT_FUNCTION, tableRef.FLD_TAX_BACKOUT_FUNCTION_LENGTH);
			return isValid;
		}*/
	}
}