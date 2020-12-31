namespace QSPFulfillment.DataAccess.Business
{
	using System;
	using System.Data;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	using tableRef = QSPFulfillment.DataAccess.Common.TableDef.AddressTable;
	using dataAccessRef =QSPFulfillment.DataAccess.Data.AddressData;
	using QSPFulfillment.DataAccess.Common;

	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	public class AddressBusiness : QSPFulfillment.DataAccess.Business.BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		
		public bool Delete(tableRef Table)
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
		}
		public void SelectAll(DataTable Table)
		{
			try
			{
				dataAccess.SelectAll(Table);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		public void SelectOne(tableRef Table,Int32 address_id)
		{
			try
			{
				dataAccess.SelectOne(Table,address_id);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		/*public void SelectAllAddressType(DataTable Table)
		{
		}*/
		public void SelectAllWaddress_typeLogic(DataTable Table, Int32 address_type)
		{
			try
			{
				dataAccess.SelectAllWaddress_typeLogic(Table,address_type);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void GetFMShipmentAddressByFMID(tableRef Table,string FMID)
		{
			try
			{
				FieldManagerTable fieldManagerTable = new FieldManagerTable();
				FieldManagerData fieldManagerDataAccess = new FieldManagerData();
				fieldManagerDataAccess.SelectOne(fieldManagerTable, FMID);

				AddressData addressDataAccess = new AddressData();
				int addressListID = Convert.ToInt32(fieldManagerTable.Rows[0]["AddressListID"].ToString());
				addressDataAccess.AddressSelectAll(Table, 0, "", "", "", "", "", "", "", 54004, addressListID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void GetAccountShipmentAddressByAccountID(tableRef Table,Int32 AccountID)
		{
			try
			{
				CAccountTable cAccountTable = new CAccountTable();
				CAccountData cAccountDataAccess = new CAccountData();
				cAccountDataAccess.SelectOne(cAccountTable, AccountID);

				AddressData addressDataAccess = new AddressData();
				int addressListID = Convert.ToInt32(cAccountTable.Rows[0]["AddressListID"].ToString());
				addressDataAccess.SelectShippingAddressByAddressListID(Table, addressListID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		//----------------------------------------------------------------
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
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_ADDRESS_TYPE,"Address Type");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_CITY,"City");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_POSTAL_CODE,"Postal Code");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_STATEPROVINCE,"Province");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_STREET1,"Street 1");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_COUNTRY,"Country");
			if (!IsValid)
			{
				messageManager.ValidationExceptionType = ExceptionType.RequiredFields;
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
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_STREET1,"Stree1 ", tableRef.FLD_STREET1_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_STREET2,"Stree2 ", tableRef.FLD_STREET2_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_CITY,"City ", tableRef.FLD_CITY_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_STATEPROVINCE,"Province ", tableRef.FLD_STATEPROVINCE_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_POSTAL_CODE,"Postal Code ", tableRef.FLD_POSTAL_CODE_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_ZIP4,"Zip ", tableRef.FLD_ZIP4_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_COUNTRY,"Country ", tableRef.FLD_COUNTRY_LENGTH);
			return isValid;
		}
		protected override DBInteractionBase DataAccessReference
		{
			get{return this.dataAccess;}
		}
	}
}