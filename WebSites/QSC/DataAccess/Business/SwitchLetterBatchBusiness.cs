namespace QSPFulfillment.DataAccess.Business
{

	using System;
	using System.Data;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	using tableRef = QSPFulfillment.DataAccess.Common.TableDef.SwitchLetterBatchTable;
	using dataAccessRef =QSPFulfillment.DataAccess.Data.SwitchLetterBatchData;
	using QSPFulfillment.DataAccess.Common;
	
	public enum MagazineStatus
	{
		Active = 30600,     
		Inactive = 30601,  
		Pending = 30602    
	}
	
	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	public class SwitchLetterBatchBusiness : QSPFulfillment.DataAccess.Business.BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		private ProductBusiness busProd;
		private ProductTable dtbProduct;
		public SwitchLetterBatchBusiness(Message MessageManager):base(MessageManager)
		{
		
		}
		public SwitchLetterBatchBusiness(bool AsMessageManager):base(AsMessageManager)
		{
		
		}
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
		public void SelectBySub(DataTable table, int customerOrderHeaderInstance, int transID)
		{
			try
			{
				dataAccess.SelectBySub(table, customerOrderHeaderInstance, transID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		public int GenerateSwitchLetterBatch(string TitleCode,int RemitBatchID,int ReasonID,DateTime From,DateTime To,string UserID)
		{
			int switchLetterBatchInstance = 0;
			try
			{
				if(RemitBatchID !=0)
                    switchLetterBatchInstance =	dataAccess.GenerateSwitchLetterBatch(TitleCode,RemitBatchID,ReasonID,UserID);
				else
					switchLetterBatchInstance =	dataAccess.GenerateSwitchLetterBatch(TitleCode,ReasonID,From,To,UserID);
								
				this.oResultSetReturned = switchLetterBatchInstance;
			}
			catch(Exception ex)
			{
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Unknown;
				throw new ExceptionFulf(messageManager);
				
			}
				
			return switchLetterBatchInstance;
			
		}
		public void GenerateSwitchLetterSub(int CustomerOrderHeaderInstance,int TransID,int ReasonID,string UserID)
		{			
			try
			{									
				oResultSetReturned =	dataAccess.GenerateSwitchLetterSub(CustomerOrderHeaderInstance,TransID,ReasonID,UserID);
							
			}
			catch(Exception ex)
			{
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Unknown;
				throw new ExceptionFulf(messageManager);
				
			}
			
			
		}

		public void UpdateSwitchLetterStatus(int BatchID, int IsLocked, int IsPrinted )
		{			
			try
			{									
				oResultSetReturned =	dataAccess.UpdateSwitchLetterStatus(BatchID,IsLocked,IsPrinted);
							
			}
			catch(Exception ex)
			{
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Unknown;
				throw new ExceptionFulf(messageManager);
				
			}
			
			
		}
		
		public void CancelSwitchLetterBatch(int SwitchLetterBatchID,string UserID)
		{

			try
			{
				if (ValidateResetSwitchLetterBatch(SwitchLetterBatchID))
				{						
					oResultSetReturned =	dataAccess.CancelSwitchLetterBatch(SwitchLetterBatchID,UserID);
										
					if (ResultSetReturned  == null)
					{
						messageManager.ValidationExceptionType = ExceptionType.OtherBusinessRules;
						messageManager.SetErrorMessage(Message.ERRMSG_REQUEST_NO_ROW_AFFECT_0);
						throw new ValidationException(messageManager);
					}
					
				}
			}
			catch(Exception ex)
			{
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Unknown;
				throw new ExceptionFulf(messageManager);
				
			}
			
		
		}
		public void CancelSwitchLetterSub(int CustomerOrderHeaderInstance,int TransID,string UserID)
		{
					
			try
			{
				if(AlreadyExist(CustomerOrderHeaderInstance,TransID))
				{
					dataAccess.CancelSwitchLetterSub(CustomerOrderHeaderInstance,TransID,UserID);
				}
				else//this switch letter dont exist
				{
					
					messageManager.Add(messageManager.FormatErrorMessage(Message.ERRMSG_SWITCH_LETTER_SUB_NOT_EXIST_0,""));
					messageManager.ValidationExceptionType = ExceptionType.OtherBusinessRules;
					messageManager.PrepareErrorMessage();
					throw new ValidationException(messageManager);
				}
			
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Unknown;
				throw new ExceptionFulf(messageManager);
			}
		}
		public void SelectSwitchLetterReport(DataTable Table,int SwitchLetterBatchID)
		{
			try
			{

				dataAccess.SelectSwitchLetterReport(Table,SwitchLetterBatchID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		public void SelectSwitchLetterReportPreview(DataTable Table,string TitleCode,int RemitBatchID,DateTime From,DateTime To)
		{
			try
			{
					if(RemitBatchID	!=0)			
					dataAccess.SelectSwitchLetterReportPreview(Table,TitleCode,RemitBatchID);
					else
					dataAccess.SelectSwitchLetterReportPreview(Table,TitleCode,From,To);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		public void SelectSwitchLetterReportPreviewSub(DataTable Table,int CustomerOrderHeaderInstance,int TransID)
		{
			try
			{
									
					dataAccess.SelectSwitchLetterReportPreviewSub(Table,CustomerOrderHeaderInstance,TransID);
				
								
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
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_PRODUCTCODE,"ProductCode");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_DATECREATED,"DateCreated");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_USERID,"UserID");
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
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_PRODUCTCODE,"", tableRef.FLD_PRODUCTCODE_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_USERID,"", tableRef.FLD_USERID_LENGTH);
			return isValid;
		}
		public void ValidateGenerateSwitchLetterSub(int CustomerOrderHeaderInstance,int TransID,int ReasonID)
		{
			
			bool isValid = true;
			isValid = IsValidRequired("1",ReasonID);
			string TitleCode = "";
			DateTime OrderKeyedDate;
			int Year;
			string Season;
			DataRow row;

			if(isValid)
			{

				if(!AlreadyExist(CustomerOrderHeaderInstance,TransID))
				{
					CustomerOrderDetailRemitHistoryBusiness bus = new CustomerOrderDetailRemitHistoryBusiness(false);
					DataTable Table = new DataTable();
					bus.SelectOneLastTransaction(Table,CustomerOrderHeaderInstance,TransID);


					if(Table.Rows.Count ==0)
					{
						//TODO:
						messageManager.Add(messageManager.FormatErrorMessage("",""));
						isValid = false;
					}
					else
					{
						row = Table.Rows[0];
						TitleCode = row[CustomerOrderDetailRemitHistoryTable.FLD_TITLECODE].ToString();
						OrderKeyedDate = Convert.ToDateTime(row["OrderKeyedDate"]);
						Year = GetProductYear(OrderKeyedDate);
						Season = GetProductSeason(OrderKeyedDate);
						isValid &= IsValidTitleCode(TitleCode, Year, Season);
					}
				}
				else//this switch letter already exist
				{
					isValid = false;
					messageManager.Add(messageManager.FormatErrorMessage(Message.ERRMSG_SWITCH_LETTER_SUB_EXIST_0,""));
			
				}
			}

			if(!isValid)
			{
				messageManager.ValidationExceptionType = ExceptionType.OtherBusinessRules;
				messageManager.PrepareErrorMessage();
				throw new ValidationException(messageManager);
			}
			
					
			
		
		}
		public void ValidateGenerateSwitchLetterBatch(int RemitBatchID, string TitleCode, int ReasonID, DateTime From, DateTime To)
		{
			int Year = GetProductYear(DateTime.Now);
			string Season = GetProductSeason(DateTime.Now);
			
			bool isValid = true;
		
			isValid = IsValidRequired(TitleCode,ReasonID);
			isValid &= IsValidRequired(RemitBatchID,From,To); 
			

			if(isValid)
			{
				isValid &= IsValidTitleCode(TitleCode, Year, Season);
				isValid &= IsValidRemitBatch(RemitBatchID);
			}
			if(isValid)
			{
				if(RemitBatchID != 0)
				{
					if(!AlreadyExist(RemitBatchID, TitleCode))
					{
						isValid = false;
						messageManager.Add(messageManager.FormatErrorMessage(Message.ERRMSG_TITLE_CODE_REMIT_BATCH_ID_COMBINATION_0,""));
						messageManager.ValidationExceptionType = ExceptionType.OtherBusinessRules;
					}
					
				}
				else
				{
					if(!AlreadyExit(TitleCode, From, To))
					{
						isValid = false;
						messageManager.Add(messageManager.FormatErrorMessage(Message.ERRMSG_TITLE_CODE_FROM_TO_COMBINATION_0,""));
						messageManager.ValidationExceptionType = ExceptionType.OtherBusinessRules;
					
					}
				}
				
			}

			

			if(!isValid)
			{
				messageManager.ValidationExceptionType = ExceptionType.OtherBusinessRules;
				messageManager.PrepareErrorMessage();
				throw new ValidationException(messageManager);
			}
			
		}

		private int GetProductYear(DateTime DateAsked) 
		{
			int Year;

			if(DateAsked.Month <= 7) 
			{
				Year = DateAsked.Year;
			} 
			else 
			{
				Year = DateAsked.Year + 1;
			}

			return Year;
		}

		private string GetProductSeason(DateTime DateAsked) 
		{
			string Season;

			if(DateAsked.Month >= 2 && DateAsked.Month <= 7) 
			{
				Season = "S";
			} 
			else 
			{
				Season = "F";
			}

			return Season;
		}
		
		private bool ValidateResetSwitchLetterBatch(int SwitchLetterBatchID)
		{
			bool isValid = true;
			
			return isValid;
		}
		private bool IsValidTitleCode(string TitleCode, int Year, string Season)
		{
			bool isValid = true;

			busProd = new ProductBusiness(false);
			dtbProduct = new ProductTable();
			busProd.SelectByProductCode(dtbProduct, TitleCode, Year, Season);

			if(dtbProduct.Rows.Count ==0)
			{
				isValid = false;
				messageManager.Add(messageManager.FormatErrorMessage(Message.ERRMSG_INSTANCE_DO_NOT_EXIST_1,"Title Code"));
			}
			else
			{
				DataRow row = dtbProduct.Rows[0];
				MagazineStatus ms = (MagazineStatus)Convert.ToInt32(row[ProductTable.FLD_STATUS]);
			
				if(ms != MagazineStatus.Inactive)
				{
					messageManager.Add(messageManager.FormatErrorMessage(Message.ERRMSG_TITLE_CODE_STATUS_INVALID_0,"Title Code"));
					isValid = false;
				}
			}

			return isValid;
			
		}
		private bool IsValidRequired(string TitleCode,int ReasonID)
		{
			bool isValid = true;
		
			if(TitleCode == "")
			{
					messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1,"Title Code"));
					isValid = false;
			}
			if(ReasonID == 0)
			{
					messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1,"Reason"));
					isValid = false;
			}
			
			return isValid;
		}
		private bool IsValidRequired(int RemitBatchID,DateTime From,DateTime To)
		{
			bool isValid = true;
		
			
			if((RemitBatchID == 0) && From.Date == DateTime.MinValue && To.Date == DateTime.MinValue)
			{
				messageManager.Add(messageManager.FormatErrorMessage(Message.ERRMSG_SWITCH_LETTER_BATCH_AT_LEAST_0,""));
				isValid = false;
			}
			if(RemitBatchID != 0 && From.Date != DateTime.MinValue && To.Date != DateTime.MinValue)
			{
				messageManager.Add(messageManager.FormatErrorMessage(Message.ERRMSG_SWITCH_LETTER_BATCH_NOT_BOTH_0,""));
				isValid = false;
			}
			return isValid;
		}
		/*private bool AsSubscription(string TitleCode,int RemitBatchID)
		{
			bool isValid = true;
			if(CountSubscriptionSwitchLetter(TitleCode,RemitBatchID)==0)
			{
				if(RemitBatchID ==0)
				{
					messageManager.Add(messageManager.FormatErrorMessage(Message.ERRMSG_TITLE_CODE_SWITCH_LETTER_0,""));
					isValid = false;
				}
				else
				{
					messageManager.Add(messageManager.FormatErrorMessage(Message.ERRMSG_TITLE_CODE_REMIT_BATCH_ID_COMBINATION_0,""));
					isValid = false;
				}
				
			}
			return isValid;
		}*/
		private int CountSubscriptionSwitchLetter(string TitleCode,int RemitBatchID)
		{
			try
			{
				return dataAccess.CountSubscriptionSwitchLetter(TitleCode,RemitBatchID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		private bool IsValidRemitBatch(int RemitBatchID)
		{
			bool isValid = true;

			if(RemitBatchID!=0)
			{
				DataTable Table = new DataTable();
				RemitBatchBusiness bus = new RemitBatchBusiness(false);

				bus.SelectOne(Table,RemitBatchID);
				if(Table.Rows.Count ==0)
				{
					messageManager.Add(messageManager.FormatErrorMessage(Message.ERRMSG_INSTANCE_DO_NOT_EXIST_1,"Remit Batch ID"));
					isValid = false;
				}
			}
			return isValid;
		}
		public bool AlreadyExist(int CustomerOrderHeaderInstance,int TransID)
		{
			return dataAccess.AlreadyExist(CustomerOrderHeaderInstance,TransID);
		}
		public bool AlreadyExist(int RemitBatchID,string TitleCode)
		{
			return dataAccess.AlreadyExist(RemitBatchID,TitleCode);
		}
		public bool AlreadyExit(string TitleCode,DateTime From,DateTime To)
		{
			return dataAccess.AlreadyExist(TitleCode,From,To);
		}
		protected override DBInteractionBase DataAccessReference
		{
			get{return this.dataAccess;}
		}
	}
}