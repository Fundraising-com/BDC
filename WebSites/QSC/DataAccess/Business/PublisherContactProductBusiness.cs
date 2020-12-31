namespace QSPFulfillment.DataAccess.Business
{

	using System;
	using System.Data;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	using tableRef = QSPFulfillment.DataAccess.Common.TableDef.PublisherContactProductTable;
	using dataAccessRef =QSPFulfillment.DataAccess.Data.PublisherContactProductData;
	using QSPFulfillment.DataAccess.Common;
	
	

	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	public class PublisherContactProductBusiness : QSPFulfillment.DataAccess.Business.BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		public PublisherContactProductBusiness(Message messageManager) : base(messageManager) { }

		public PublisherContactProductBusiness(bool hasMessageManager) : base(hasMessageManager) { }
		
		public bool Delete(tableRef table)
		{
			//We call a method from the inherit class, but the
			//validation is provide by the overriden Validate Method 
			//is in the current class
			return this.Delete(table, dataAccess);
		}
		public bool Insert(tableRef table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.Insert(table, dataAccess);	
		}
		public bool UpdateBatch(tableRef table)
		{			
			//We call a method from the inherit class, but the
			//validation is provide by the overriden Validate Method 
			//is in the current class
			return this.UpdateBatch(table, dataAccess);
		}
		public bool Update(tableRef table)
		{
			//We call a method from the inherit class, but the
			//validation is provide by the overriden Validate Method 
			//is in the current class
			return this.Update(table, dataAccess);
		}

		public void SelectAllByPublisherContactID(DataTable table, int publisherContactID)
		{
			try
			{
				dataAccess.SelectAllByPublisherContactID(table, publisherContactID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public int Insert(int publisherContactID, string productCode, int userID) 
		{
			try 
			{
				return dataAccess.Insert(publisherContactID, productCode, userID);
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
				messageManager.ValidationExceptionType = ExceptionType.Unknown;
				throw new ExceptionFulf(messageManager);
			}
		}

		public bool ValidateInsert(int publisherID, string productCode) 
		{
			bool isSuccess = false;

			ProductBusiness productBusiness = null;
			PublisherContactBusiness publisherContactBusiness = null;
			DataTable productTable = null;
			DataTable publisherContactTable = null;
			PublisherContactProductTable publisherContactProductTable = null;
			DataView publisherContactProductView = null;

			productBusiness = new ProductBusiness(messageManager);
			productTable = new DataTable("Product");
			productBusiness.SelectAll(productTable, productCode, String.Empty, String.Empty, 0, String.Empty, 0, ProductType.Magazine, publisherID, 0);
				
			if(productTable.Rows.Count > 0) 
			{
				publisherContactBusiness = new PublisherContactBusiness(messageManager);
				publisherContactTable = new DataTable("Publisher_Contacts");

				publisherContactBusiness.SelectAllByPublisherID(publisherContactTable, publisherID);

				publisherContactProductTable = new PublisherContactProductTable();
				publisherContactProductView = new DataView(publisherContactProductTable, "Product_Code = '" + productCode + "'", String.Empty, DataViewRowState.CurrentRows);

				foreach(DataRow row in publisherContactTable.Rows) 
				{
					SelectAllByPublisherContactID(publisherContactProductTable, Convert.ToInt32(row["PContact_Instance"]));

					if(publisherContactProductView.Count > 0) 
					{
						messageManager.Add(messageManager.FormatErrorMessage(Message.ERRMSG_PRODUCT_CODE_ALREADY_SELECTED_1, row["PContact_FName"].ToString() + " " + row["PContact_LName"].ToString()));
						messageManager.PrepareErrorMessage();
						throw new ExceptionFulf(messageManager);
					}

					publisherContactProductTable.Clear();
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

		public void Delete(int publisherContactID, int id, bool checkAtLeastOne)
		{
			PublisherContactProductTable publisherContactProductTable;

			try
			{
				if(checkAtLeastOne) 
				{
					publisherContactProductTable = new PublisherContactProductTable();
					SelectAllByPublisherContactID(publisherContactProductTable, publisherContactID);

					if(publisherContactProductTable.Rows.Count == 1) 
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