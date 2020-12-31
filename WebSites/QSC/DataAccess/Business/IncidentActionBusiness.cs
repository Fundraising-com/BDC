namespace QSPFulfillment.DataAccess.Business
{
	using System;
	using System.Data;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	using tableRef = QSPFulfillment.DataAccess.Common.TableDef.IncidentActionTable;
	using dataAccessRef =QSPFulfillment.DataAccess.Data.IncidentActionData;
	using QSPFulfillment.DataAccess.Common;
	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	public class IncidentActionBusiness : QSPFulfillment.DataAccess.Business.BusinessSystem
	{
		private const int SelectAction = 8;
		private const string SelectComments = "Item opened";

		dataAccessRef dataAccess = new dataAccessRef();
		//private ActionConstraintsTable dtbActionConstraints;
		//private ActionRulesTable dtbActionRule;
		private tableRef dtbIncidentAction;
		private ActionTable dtbAction;
		private int iCurIncidentID = 0;

		public IncidentActionBusiness(Message MessageManager):base(MessageManager)
		{
		
		}
		public IncidentActionBusiness(bool AsMessageManager):base(AsMessageManager)
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
		public void SelectOne(DataTable Table,int Instance)
		{
			try
			{
				dataAccess.SelectOne(Table,Instance);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		public void SelectByIncidentID(DataTable Table,int IncidentID)
		{
			try
			{
				dataAccess.SelectByIncidentID(Table,IncidentID);
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
				/*if(isValid)
					isValid &= IsValid_Action(Row);*/
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
			IsValid = IS_CommentRequired(Row);
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
		/*private bool IsValid_Action(DataRow Row)
		{
			iCurIncidentID=Convert.ToInt32(Row[IncidentActionTable.FLD_INCIDENTINSTANCE]);
			return IsValideAction(iCurIncidentID,Convert.ToInt32(Row[IncidentActionTable.FLD_ACTIONINSTANCE]),Row);
			
		}*/
		/*public bool IsValideAction(int IncidentInstance,int ActionInstance,DataRow CurrentRow)
		{
			bool isValid = true;
			
			DataRow[] row = TableActionRule.Select(ActionRulesTable.FLD_ACTIONINSTANCE +" = " +ActionInstance.ToString());
			
			if(row.Length!=0)
			{
				if(Convert.ToBoolean(row[0][ActionRulesTable.FLD_UNIQUE]))
				{
					isValid &= IsUnique(ActionInstance,CurrentRow,row[0][ActionRulesTable.FLD_ERRORMESSAGE].ToString());
				}
			}

			return isValid;
			
		}*/

		/*private ActionRulesTable TableActionRule
		{
			get 
			{
				if(dtbActionRule == null)
					LoadActionRules();

				return dtbActionRule;
			}
		}*/
		/*private ActionConstraintsTable TableConstraint
		{
			get
			{
				if(dtbActionConstraints == null)
					LoadActionConstraints();

				return dtbActionConstraints;
			}
		}*/
		/*private IncidentActionTable TableIncidentAction
		{
			get
			{
				if(dtbActionConstraints == null)
					LoadIncidentAction();

				return dtbIncidentAction;	
			}
		}*/
		/*private void LoadActionConstraints()
		{
			ActionConstraintsBusiness bus = new ActionConstraintsBusiness();
			dtbActionConstraints = new ActionConstraintsTable();
			bus.SelectAll(dtbActionConstraints);
		}
		private void LoadActionRules()
		{
		
				ActionRulesBusiness bus = new ActionRulesBusiness();
				dtbActionRule = new ActionRulesTable();
				bus.SelectAll(dtbActionRule);
			
		}*/
		private void LoadIncidentAction()
		{
			dtbIncidentAction = new IncidentActionTable();
			this.SelectByIncidentID(dtbIncidentAction,iCurIncidentID);
		}
		private void LoadAction(int ActionInstance)
		{
			ActionBusiness bus = new ActionBusiness(true);
			dtbAction = new ActionTable();
			bus.SelectOne(dtbAction,(Action)ActionInstance);
		}
		private bool IS_CommentRequired(DataRow Row)
		{
			if(dtbAction == null)
				LoadAction(Convert.ToInt32(Row[IncidentActionTable.FLD_ACTIONINSTANCE]));

			if((Row[IncidentActionTable.FLD_COMMENTS].ToString() == String.Empty) && Convert.ToBoolean(dtbAction.Rows[0][ActionTable.FLD_COMMNENTSISREQUIRED]))
			{
				Row.SetColumnError(IncidentActionTable.FLD_COMMENTS, messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1,"Comments"));
				return false;
			}

			return true;
		}
		protected override DBInteractionBase DataAccessReference
		{
			get{return this.dataAccess;}
		}

		public bool InsertItemSelected(int CustomerOrderHeaderInstance, int TransID, int IncidentID, string UserID)
		{
			try
			{	
				IncidentActionTable incidentActionTable = new IncidentActionTable();
				DataRow row = incidentActionTable.NewRow();
				row[IncidentActionTable.FLD_ACTIONINSTANCE] = SelectAction; 
				row[IncidentActionTable.FLD_INCIDENTINSTANCE] = IncidentID;
				row[IncidentActionTable.FLD_USERIDCREATED] = UserID;
				row[IncidentActionTable.FLD_COMMENTS] = SelectComments;
			
				incidentActionTable.Rows.Add(row);
				return this.Insert(incidentActionTable,dataAccess);	
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
