using System;
using System.Data;
using QSPFulfillment.DataAccess.Data;
using System.Text.RegularExpressions;
using QSPFulfillment.DataAccess.Common;
using QSPFulfillment.DataAccess.Common.TableDef;

namespace QSPFulfillment.DataAccess.Business
{
	
	/// <summary>
	/// This class is central object of the Business Layer.
	/// </summary> 
	public abstract class BusinessSystem: MarshalByRefObject
	{
		//Only one Message Manager for all because we throw error
		//only one will be use
		internal DataAccess.Common.Message messageManager;
		
		protected int _errorCode = 0;
		protected int NbRowAffected =0;/// <summary>
		protected object oResultSetReturned;
		private ActionData dtaAction;
		
		public BusinessSystem()
		{
			if(messageManager == null)
				messageManager = new DataAccess.Common.Message(true);
		}
		public BusinessSystem(bool AsMessageManager)
		{
			if(messageManager == null && AsMessageManager)
				messageManager = new DataAccess.Common.Message(true);
		}
		public BusinessSystem(Message MessageManager)
		{
			messageManager = MessageManager;
		}
		
		/// <summary>
		/// Validates a specific field against his maxlength
		/// </summary>
		/// <param name="row">The Row where the field must be vaidate</param>
		/// <param name="fieldName">field in the Row to be validated</param>
		/// <param name="maxLen">max length for the field</param>
		/// <returns>False if field fails the validation rules.</returns>
		protected bool IsValid_FieldLength(DataRow row, string fieldName,string fieldLabel, short maxLen)
		{
			short i = (short)(row[fieldName].ToString().Trim().Length);
			if ( (i < 0) || (i > maxLen) )
			{
				//
				// Mark the field as invalid
				//
				row.SetColumnError(fieldName, messageManager.FormatErrorMessage(Message.VALMSG_MAX_LENGTH_VAR_2, new String[] {fieldLabel, maxLen.ToString()}));
				messageManager.ValidationExceptionType = ExceptionType.MaxLength;
				return false;
			}
			return true;
		}
		/// <summary>
		/// Validates required field
		/// </summary>
		/// <param name="row">The Row where the field must be vaidate</param>
		/// <param name="FieldNameToValidate">Field to be validate</param>
		/// <param name="FieldForErrorMessage">Field for the error message</param>
		/// <returns></returns>
		protected bool IsValid_RequiredField(DataRow row, string FieldNameToValidate, string FieldForErrorMessage)
		{
			if (row[FieldNameToValidate].ToString() == string.Empty)
			{
				//
				// Mark the field as invalid
				//
				row.SetColumnError(FieldNameToValidate,messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1, FieldForErrorMessage));
				messageManager.ValidationExceptionType = ExceptionType.RequiredFields;
				return false;
			}
			return true;
		}
		protected virtual bool Validate(DataRow row)
		{
			return true;
		}
		/// <summary>
		/// Validate information in the table
		/// </summary>
		/// <param name="Table">Table to be validate</param>
		/// <returns>False if table is not valid</returns>
		protected bool ValidateTable(DataTable Table)
		{
			bool result = true;
			//Only Validate the row with a the properly state
			DataView DV = new DataView(Table);
			DV.RowStateFilter = DataViewRowState.Added | 
				DataViewRowState.ModifiedCurrent |
				DataViewRowState.Deleted;
			foreach(DataRowView dvrow in DV)
			{
				DataRow row = dvrow.Row;
				result = Validate(row);
				if (!result)
					break;
			}
			if ( !result )
			{
				messageManager.SetErrorMessage(Table);
				throw new ValidationException(messageManager);
			}
			return result;
		}
		/// <summary>
		/// Insert row inserted in the Table
		/// </summary>
		/// <param name="Table">Table containing row to be inserted</param>
		/// <param name="DataAccess">Table operation class for this Table</param>
		/// <returns>true if successful; otherwise, false.</returns>
		protected bool Insert(DataTable Table,QSPFulfillment.DataAccess.Data.DBTableOperation DataAccess)
		{
			bool IsSuccess = false;
			try
			{
				if (ValidateTable(Table))
				{	
					NbRowAffected = 0;
					NbRowAffected = DataAccess.Insert(Table);
					if (NbRowAffected != 0)
					{
						IsSuccess = true; 
					}
					else
					{
						messageManager.ValidationExceptionType  = ExceptionType.OtherBusinessRules;
						messageManager.SetErrorMessage(Message.ERRMSG_REQUEST_NO_ROW_AFFECT_0);
						IsSuccess = false;
					}
				}
			}
			catch(Exception EX)
			{
				ManageError(EX);
				return false;
			}
			return IsSuccess;
		}
		/// <summary>
		/// Update row modified in the Table
		/// </summary>
		/// <param name="Table">Table containing row to be inserted</param>
		/// <param name="DataAccess">Table operation class for this Table</param>
		/// <returns>true if successful; otherwise, false.</returns>
		protected bool Update(DataTable Table, QSPFulfillment.DataAccess.Data.DBTableOperation DataAccess)
		{
			bool IsSuccess = false;
			try
			{
				if (ValidateTable(Table))
				{	
					NbRowAffected = 0;
					NbRowAffected = DataAccess.Update(Table);
					if (NbRowAffected != 0)
					{
						IsSuccess = true; 
					}
					else
					{
						messageManager.ValidationExceptionType = ExceptionType.OtherBusinessRules;
						messageManager.SetErrorMessage(Message.ERRMSG_REQUEST_NO_ROW_AFFECT_0);
						IsSuccess = false;
					}
				}
			}
			catch(Exception EX)
			{
				ManageError(EX);
				return false;
			}
			return IsSuccess;
		}
		/// <summary>
		/// Delete row deleted in the Table
		/// </summary>
		/// <param name="Table">Table containing row to be deleted</param>
		/// <param name="DataAccess">Table operation class for this Table</param>
		/// <returns>true if successful; otherwise, false.</returns>
		protected bool Delete(DataTable Table, QSPFulfillment.DataAccess.Data.DBTableOperation DataAccess)
		{
			bool IsSuccess = false;
			try
			{
				if (ValidateTable(Table))
				{	
					NbRowAffected = 0;
					NbRowAffected = DataAccess.Delete(Table);
					if (NbRowAffected != 0)
					{
						IsSuccess = true; 
					}
					else
					{
						messageManager.ValidationExceptionType = ExceptionType.OtherBusinessRules;
						messageManager.SetErrorMessage(Message.ERRMSG_NO_REC_AFF_VAR_0);
						IsSuccess = false;
					}
				}
			}
			catch(Exception EX)
			{
				ManageError(EX);
				return false;
			}
			return IsSuccess;
		}
		/// <summary>
		/// Update,Insert,Delete row in the Table
		/// </summary>
		/// <param name="Table">Table containing row to be Update,Insert and delete</param>
		/// <param name="DataAccess">Table operation class for this Table</param>
		/// <returns>true if successful; otherwise, false.</returns>
		protected bool UpdateBatch(DataTable Table, QSPFulfillment.DataAccess.Data.DBTableOperation DataAccess)
		{
			bool IsSuccess = false;
			try
			{
				if (ValidateTable(Table))
				{
					NbRowAffected = 0;
					NbRowAffected = DataAccess.UpdateBatch(Table);
					if (NbRowAffected != 0)
					{
						IsSuccess = true; 
					}
					else
					{
						messageManager.ValidationExceptionType = ExceptionType.OtherBusinessRules;
						messageManager.SetErrorMessage(Message.ERRMSG_REQUEST_NO_ROW_AFFECT_0);
						IsSuccess = false;
					}
				}
			}
			catch(Exception EX)
			{
				ManageError(EX);
				return false;
			}
			return IsSuccess;
		}

		protected string CleanString(string str) 
		{
			return str.Replace("'", "''");
		}

		/// <summary>
		/// Get number of row affected by the Querry
		/// </summary>
		public int RowAffected
		{
			get
			{
				return NbRowAffected;
			}
		}
		
		public object ResultSetReturned
		{
			get
			{
				return oResultSetReturned;
			}
		}


		protected void ManageError(Exception ex)
		{

			if (ex is ExceptionFulf || ex is System.Threading.ThreadAbortException)
			{
				throw ex;
			}
			else
			{
				
				QSPFulfillment.DataAccess.Common.ApplicationError.ManageError(ex);
				messageManager.SetSystemErrorMessage(Message.ERRMSG_SYSTEM_VAR_0);
				throw new ExceptionFulf(messageManager, ex);
			}	
			
		}
		protected void ManageError(QSPFulfillment.DataAccess.Common.ExceptionFulf ex)
		{			
			QSPFulfillment.DataAccess.Common.ApplicationError.ManageError(ex);	
		}
		public bool IsValidAction(int ActionInstance,int CustomerOrderHeaderInstance,int TransID)
		{
			if(dtaAction == null)
				dtaAction = new ActionData();

			return (bool)dtaAction.IsValidAction(ActionInstance,CustomerOrderHeaderInstance,TransID);
		}
		public ConnectionProvider MainConnectionProvider
		{
			get 
			{
				return DataAccessReference.MainConnectionProvider;
			}
			set
			{
				DataAccessReference.MainConnectionProvider = value;
			}
		}

		protected abstract DBInteractionBase DataAccessReference
		{
			get;
		}

	}
}
