namespace QSPFulfillment.DataAccess.Business
{
	using System;
	using System.Data;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	using tableRef = QSPFulfillment.DataAccess.Common.TableDef.IncidentTable;
	using dataAccessRef =QSPFulfillment.DataAccess.Data.IncidentData;
	using QSPFulfillment.DataAccess.Common;
	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	public class IncidentBusiness : QSPFulfillment.DataAccess.Business.BusinessSystem
	{
		private const string SelectComments = "Item opened";
		private const int SelectProblemCode = 220;
		private const int SelectStatus = 1;
		private const int SelectCommunicationChannel = 1;
		private const int SelectCommunicationSource = 3;

		dataAccessRef dataAccess = new dataAccessRef();
		private ProblemCodeTable dtbProblemCode;
		private DataTable dtbIncident;
		public IncidentBusiness(Message MessageManager):base(MessageManager)
		{
		
		}
		public IncidentBusiness(bool AsMessageManager):base(AsMessageManager)
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
		public void SelectOne(DataTable Table,int IncidentInstance)
		{
			try
			{
				dataAccess.SelectOne(Table,IncidentInstance);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		public void SelectByCOHInstance(DataTable Table,int COHInstance,int TransID)
		{
			try
			{
				dataAccess.SelectByCOHInstance(Table,COHInstance,TransID);
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
				if(isValid)
					isValid &= IsValid_Business(Row);
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
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_PROBLEMCODEINSTANCE,"Problem Code");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_CUSTOMERORDERHEADERINSTANCE,"CustomerOrderHeaderInstance");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_TRANSID,"TransID");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_COMMUNICATIONCHANNELINSTANCE,"Communication Channel");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_COMMUNICATIONSOURCEINSTANCE,"Communication Source");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_STATUSINSTANCE,"Status");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_USERIDCREATED,"User ID");

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
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_COMMENTS,"Comments", tableRef.FLD_COMMENTS_LENGTH);
			return isValid;
		}
		private bool IsValid_Business(DataRow Row)
		{
			bool isValid = true;
			isValid = IsValid_ProblemCode(Row,"Problem Code");
			isValid &= IsValid_IncidentID(Row,"Incident ID");
			return isValid;
		}
		private bool IsValid_ProblemCode(DataRow Row,string UserFriendlyColumnName)
		{
			if(dtbProblemCode == null)
				LoadTableProblemCode();
			
			if((dtbProblemCode.Select(ProblemCodeTable.FLD_INSTANCE +"="+Row[tableRef.FLD_PROBLEMCODEINSTANCE].ToString()).Length == 0))
			{
				Row.SetColumnError(IncidentTable.FLD_PROBLEMCODEINSTANCE, messageManager.FormatErrorMessage(Message.ERRMSG_INSTANCE_DO_NOT_EXIST_1,UserFriendlyColumnName));
				messageManager.ValidationExceptionType = ExceptionType.Integrity;
				return false;
			}
			return true;
			
		}
		private void LoadTableProblemCode()
		{
			 dtbProblemCode = new ProblemCodeTable();
			 ProblemCodeBusiness busProCode = new ProblemCodeBusiness(false);
			 busProCode.SelectAll(dtbProblemCode);

		}
		private bool IsValid_IncidentID(DataRow Row,string UserFriendlyColumnName)
		{
			if(Row[tableRef.FLD_REFERTOINCIDENTINSTANCE] != DBNull.Value)
			{
			if(dtbIncident == null)
				LoadTableIncidentHistory(Row);
				
				if((dtbIncident.Select(tableRef.FLD_INCIDENTINSTANCE +"="+Row[tableRef.FLD_REFERTOINCIDENTINSTANCE].ToString()).Length == 0))
				{
					Row.SetColumnError(tableRef.FLD_REFERTOINCIDENTINSTANCE, messageManager.FormatErrorMessage(Message.ERRMSG_INSTANCE_DO_NOT_EXIST_1,UserFriendlyColumnName));
					messageManager.ValidationExceptionType = ExceptionType.Integrity;
					return false;
				}
			}
			return true;
			
		}
		private void LoadTableIncidentHistory(DataRow Row)
		{
			dtbIncident = new DataTable();
			IncidentBusiness bus = new IncidentBusiness(false);
			bus.SelectByCOHInstance(dtbIncident,Convert.ToInt32(Row[IncidentTable.FLD_CUSTOMERORDERHEADERINSTANCE]),Convert.ToInt32(Row[IncidentTable.FLD_TRANSID]));

		}
		protected override DBInteractionBase DataAccessReference
		{
			get{return this.dataAccess;}
		}

		public int InsertItemSelected(int CustomerOrderHeaderInstance, int TransID, string UserID)
		{
			try
			{	
				IncidentTable incidentTable = new IncidentTable();
				DataRow row = incidentTable.NewRow();
				row[IncidentTable.FLD_CUSTOMERORDERHEADERINSTANCE] = CustomerOrderHeaderInstance; 
				row[IncidentTable.FLD_TRANSID] = TransID;
				row[IncidentTable.FLD_PROBLEMCODEINSTANCE] = SelectProblemCode;
				row[IncidentTable.FLD_STATUSINSTANCE] = SelectStatus;
				row[IncidentTable.FLD_COMMUNICATIONSOURCEINSTANCE] = SelectCommunicationSource;
				row[IncidentTable.FLD_COMMUNICATIONCHANNELINSTANCE] = SelectCommunicationChannel;
				row[IncidentTable.FLD_USERIDCREATED]  = UserID;
				row[IncidentTable.FLD_COMMENTS] = SelectComments;

				incidentTable.Rows.Add(row);
				this.Insert(incidentTable,dataAccess);
				return (int)incidentTable.Rows[0][IncidentTable.FLD_INCIDENTINSTANCE];
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

	}
}