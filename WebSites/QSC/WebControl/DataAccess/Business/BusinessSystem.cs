using System;
using System.Data;
using QSP.WebControl.DataAccess.Data;
using System.Text.RegularExpressions;
using QSP.WebControl.DataAccess.Common;

namespace QSP.WebControl.DataAccess.Business
{
	
	/// <summary>
	/// This class is central object of the Business Layer.
	/// </summary> 
	internal abstract class BusinessSystem: MarshalByRefObject
	{
		internal QSP.WebControl.DataAccess.Common.Message messageManager = new DataAccess.Common.Message(true);
		
		protected int _errorCode = 0;
		protected int NbRowAffected =0;/// <summary>
		
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
		protected bool Insert(DataTable Table,QSP.WebControl.DataAccess.Data.DBTableOperation DataAccess)
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
		/// Update row modified in the Table
		/// </summary>
		/// <param name="Table">Table containing row to be inserted</param>
		/// <param name="DataAccess">Table operation class for this Table</param>
		/// <returns>true if successful; otherwise, false.</returns>
		protected bool Update(DataTable Table, QSP.WebControl.DataAccess.Data.DBTableOperation DataAccess)
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
		/// Delete row deleted in the Table
		/// </summary>
		/// <param name="Table">Table containing row to be deleted</param>
		/// <param name="DataAccess">Table operation class for this Table</param>
		/// <returns>true if successful; otherwise, false.</returns>
		protected bool Delete(DataTable Table, QSP.WebControl.DataAccess.Data.DBTableOperation DataAccess)
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
		protected bool UpdateBatch(DataTable Table, QSP.WebControl.DataAccess.Data.DBTableOperation DataAccess)
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
		/// Get number of row affected by the Querry
		/// </summary>
		public int RowAffected
		{
			get
			{
				return NbRowAffected;
			}
		}
		
		protected void ManageError(Exception ex)
		{

			if (ex is QspException )
			{
				throw ex;
			}
			else
			{
				DataAccess.Common.ApplicationError.ManageError(ex);
				messageManager.SetSystemErrorMessage(Message.ERRMSG_SYSTEM_VAR_0);
				throw new QspException(messageManager, ex);
			}	
			
		}
		protected void ManageError(QSP.WebControl.DataAccess.Common.QspException ex)
		{			
			DataAccess.Common.ApplicationError.ManageError(ex);	
		}
	
	}
}
