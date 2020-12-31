namespace QSPFulfillment.DataAccess.Business
{

	using System;
	using System.Data;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	using tableRef = QSPFulfillment.DataAccess.Common.TableDef.FulfillmentHouseContactProductTable;
	using dataAccessRef =QSPFulfillment.DataAccess.Data.FulfillmentHouseContactProductData;
	using  QSPFulfillment.DataAccess.Common;
	
	

	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	public class FulfillmentHouseContactProductBusiness : QSPFulfillment.DataAccess.Business.BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		public FulfillmentHouseContactProductBusiness(Message messageManager) : base(messageManager) { }

		public FulfillmentHouseContactProductBusiness(bool hasMessageManager) : base(hasMessageManager) { }
		
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
		public void SelectAllByFulfillmentHouseContactID(DataTable table, int fulfillmentHouseContactID)
		{
			try
			{
				dataAccess.SelectAllByFulfillmentHouseContactID(table, fulfillmentHouseContactID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public int Insert(int fulfillmentHouseContactID, string productCode, int userID) 
		{
			try 
			{
				return dataAccess.Insert(fulfillmentHouseContactID, productCode, userID);
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
				messageManager.ValidationExceptionType = ExceptionType.Unknown;
				throw new ExceptionFulf(messageManager);
			}
		}

		public bool ValidateInsert(int fulfillmentHouseID, string productCode) 
		{
			bool isSuccess = false;

			ProductBusiness productBusiness = null;
			FulfillmentHouseContactBusiness fulfillmentHouseContactBusiness = null;
			DataTable productTable = null;
			DataTable fulfillmentHouseContactTable = null;
			FulfillmentHouseContactProductTable fulfillmentHouseContactProductTable = null;
			DataView fulfillmentHouseContactProductView = null;

			productBusiness = new ProductBusiness(messageManager);
			productTable = new DataTable("Product");
			productBusiness.SelectAll(productTable, productCode, String.Empty, String.Empty, 0, String.Empty, 0, ProductType.Magazine, 0, fulfillmentHouseID);
				
			if(productTable.Rows.Count > 0) 
			{
				fulfillmentHouseContactBusiness = new FulfillmentHouseContactBusiness(messageManager);
				fulfillmentHouseContactTable = new DataTable("Fulfillment_House_Contacts");

				fulfillmentHouseContactBusiness.SelectAllByFulfillmentHouseID(fulfillmentHouseContactTable, fulfillmentHouseID);

				fulfillmentHouseContactProductTable = new FulfillmentHouseContactProductTable();
				fulfillmentHouseContactProductView = new DataView(fulfillmentHouseContactProductTable, "Product_Code = '" + productCode + "'", String.Empty, DataViewRowState.CurrentRows);

				foreach(DataRow row in fulfillmentHouseContactTable.Rows) 
				{
					SelectAllByFulfillmentHouseContactID(fulfillmentHouseContactProductTable, Convert.ToInt32(row["Instance"]));

					if(fulfillmentHouseContactProductView.Count > 0) 
					{
						messageManager.Add(messageManager.FormatErrorMessage(Message.ERRMSG_PRODUCT_CODE_ALREADY_SELECTED_1, row["FirstName"].ToString() + " " + row["LastName"].ToString()));
						messageManager.PrepareErrorMessage();
						throw new ExceptionFulf(messageManager);
					}

					fulfillmentHouseContactProductTable.Clear();
				}

				isSuccess = true;
			} 
			else 
			{
				messageManager.Add(Message.ERRMSG_INVALID_PRODUCT_CODE_0);
				messageManager.PrepareErrorMessage();
				throw new ExceptionFulf(messageManager);
			}

			return isSuccess;
		}

		public void Delete(int fulfillmentHouseContactID, int id, bool checkAtLeastOne)
		{
			FulfillmentHouseContactProductTable fulfillmentHouseContactProductTable;

			try
			{
				if(checkAtLeastOne) 
				{
					fulfillmentHouseContactProductTable = new FulfillmentHouseContactProductTable();
					SelectAllByFulfillmentHouseContactID(fulfillmentHouseContactProductTable, fulfillmentHouseContactID);

					if(fulfillmentHouseContactProductTable.Rows.Count == 1) 
					{
						messageManager.Add(Message.ERRMSG_PRODUCT_AT_LEAST_ONE_0);
						messageManager.PrepareErrorMessage();
						throw new ExceptionFulf(messageManager);
					}
				}

				dataAccess.Delete(id);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Unknown;
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
			//IsValid &= IsValid_RequiredField(Row,tableRef.FLD_GROSS,"Gross");
			if (!IsValid)
			{
				messageManager.ValidationExceptionType =  ExceptionType.RequiredFields;
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
			//isValid &= IsValid_FieldLength(Row, tableRef.FLD_DESCRIPTION,"Description", tableRef.FLD_DESCRIPTION_LENGTH);
			return isValid;
		}
		protected override DBInteractionBase DataAccessReference
		{
			get{return this.dataAccess;}
		}
	}
}