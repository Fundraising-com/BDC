using System;
using System.Data;
using QSPForm.Data;
using System.Text.RegularExpressions;
using System.Globalization;
using QSPForm.Common;

namespace QSPForm.Business
{
	/// <summary>
	///		--------Biz Layer Central Class -- Create by F6 28/01/2004
	///		This class is central object of the Business Layer.
	///		Pratically all class contains in this layer inherit from it
	///		The purpose of this object is to give functionnality
	///		to the rest of class of this layer for consideration
	///		like error Message code, the type of error, the Definition of
	///		the Error Message on itself.
	///		
	/// </summary> 

		
	public abstract class BusinessSystem: MarshalByRefObject
	{		
		internal QSPForm.Common.QSPFormMessage messageManager = new QSPForm.Common.QSPFormMessage();
		//
		// Const regular expression format used to validate an Email address
		//   this expression should comply with the email address definition
		//   in RFC822.
		//
		private const String REGEXP_ISVALIDEMAIL = @"^\w+((-\w+)|(\.\w+))*\@\w+((\.|-)\w+)*\.\w+$";
		
		//protected CommonRules CommonR = new CommonRules();

		public BusinessSystem()
		{

		}

		protected bool IsValidEmail(string email)
		{
			bool isValid = false;
			//
			// Check field format
			//
			isValid = (new Regex(REGEXP_ISVALIDEMAIL)).IsMatch(email);            
            
			return isValid;
		}

		//----------------------------------------------------------------
		// Function IsValid_FieldLength:
		//   Validates a specific field against his maxlength 
		// Returns:
		//   False if field fails the validation rules.
		// Parameters:
		//   [in]  Row: The Row where the field must be vaidate
		//   [in]  fieldName: field in the Row to be validated
		//   [in]  maxLen: max length for the field
		//----------------------------------------------------------------
		internal bool IsValid_FieldLength(DataRow row, String fieldName, String fieldLabel, short maxLen)
		{
			short i = (short)(row[fieldName].ToString().Trim().Length);
			if ( (i < 0) || (i > maxLen) )
			{
				//
				// Mark the field as invalid
				//
				row.SetColumnError(fieldName, messageManager.FormatErrorMessage(QSPFormMessage.VALMSG_MAX_LENGTH, new String[] {fieldLabel, maxLen.ToString()}));
				messageManager.ValidationExceptionType = QSPFormExceptionType.MaxLength;
				return false;
			}
            
			return true;
		}

		internal bool IsValid_RequiredField(DataRow row, String fieldName, String fieldLabel)
		{
			
			if (row[fieldName].ToString() == String.Empty)
			{
				//
				// Mark the field as invalid
				//
				row.SetColumnError(fieldName, messageManager.FormatErrorMessage(QSPFormMessage.VALMSG_REQUIRED_FIELD, fieldLabel));				
				messageManager.ValidationExceptionType = QSPFormExceptionType.RequiredFields;
				return false;
			}
			return true;

		}

		protected virtual bool Validate(DataRow row)
		{
			return true;
		}

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
				throw new QSPForm.Common.QSPFormValidationException(messageManager);
			}

			return result;
		}

		protected bool ValidateRows(DataRow[] dataRows)
		{
			bool result = true;

			//Only Validate the row with a the properly state
			foreach(DataRow row in dataRows)
			{
				if ((row.RowState == DataRowState.Added) ||
					(row.RowState == DataRowState.Modified) ||
					(row.RowState == DataRowState.Deleted))
				{
					result = Validate(row);
					if (!result)
						break;			
				}
			}

			if ( !result )
			{	
				messageManager.SetErrorMessage(dataRows[0].Table);
				throw new QSPForm.Common.QSPFormValidationException(messageManager);
			}

			return result;
		}

		/// <summary>
		///     Validates and inserts new Rows
		///     <remarks>   
		///         Returns participant data.  If there are fields that contain errors 
		///         that contain errors they are individually marked.  
		///     </remarks>   
		///     <param name="customer">CollectionDayTable to be inserted.</param>
		///     <retvalue>true if successful; otherwise, false.</retvalue>
		///     <exception> class='System.ApplicationException'>
		///         An invalid user was passed in.
		///     </exception>
		/// </summary>
		protected bool Insert(DataTable Table, QSPForm.Data.DBTableOperation DataAccess)
		{
	
			bool IsSuccess = false;
			try
			{
				
				if (ValidateTable(Table))
				{	
					int NbRecAff = 0;
					NbRecAff = DataAccess.Insert(Table);
					if (NbRecAff != 0)
					{
						IsSuccess = true; 
					}
					else
					{
						messageManager.ValidationExceptionType = QSPFormExceptionType.OtherBusinessRules;
						messageManager.SetErrorMessage(QSPFormMessage.ERRMSG_NO_REC_AFF);
						IsSuccess = false;
					}
				}
			}
			catch(Exception ex)
			{
				if (ex is QSPForm.Common.QSPFormException)
				{
					//send the exception and the info to present
					throw ex;
				}
				else
				{
					ManageError(ex);
					throw new QSPForm.Common.QSPFormException(messageManager, ex);
				}				
			}
			//
			// Return the result of the operation
			//            
			return IsSuccess;
		}

		/// <summary>
		///     Validates and inserts new Rows
		///     <remarks>   
		///         Returns participant data.  If there are fields that contain errors 
		///         that contain errors they are individually marked.  
		///     </remarks>   
		///     <param name="customer">CollectionDayTable to be inserted.</param>
		///     <retvalue>true if successful; otherwise, false.</retvalue>
		///     <exception> class='System.ApplicationException'>
		///         An invalid user was passed in.
		///     </exception>
		/// </summary>
		protected bool Insert(DataSet dataSet, String tableName, QSPForm.Data.DBTableOperation DataAccess)
		{
			bool IsSuccess = false;
			try
			{
				if (ValidateTable(dataSet.Tables[tableName]))
				{
					int NbRecAff = 0;
					NbRecAff = DataAccess.Insert(dataSet);
					if (NbRecAff != 0)
					{
						IsSuccess = true; 
					}
					else
					{
						messageManager.ValidationExceptionType = QSPFormExceptionType.OtherBusinessRules;
						messageManager.SetErrorMessage(QSPFormMessage.ERRMSG_NO_REC_AFF);
						IsSuccess = false;
					}											
				}
			}
			catch(Exception ex)
			{
				if (ex is QSPForm.Common.QSPFormException)
				{
					//send the exception and the info to present
					throw ex;
				}
				else
				{
					ManageError(ex);
					throw new QSPForm.Common.QSPFormException(messageManager, ex);
				}				
			}
			//
			// Return the result of the operation
			//            
			return IsSuccess;
		}

		/// <summary>
		///     Validates and updates a new customer
		///     <remarks>   
		///         Returns user data.  If there are fields that contain errors 
		///         that contain errors they are individually marked.  
		///     </remarks>   
		///     <param name="customer">userData to be updated.</param>
		///     <retvalue>true if successful; otherwise, false.</retvalue>
		///     <exception> class='System.ApplicationException'>
		///         An invalid customer was passed in.
		///     </exception>
		/// </summary>
		protected bool Update(DataTable Table, QSPForm.Data.DBTableOperation DataAccess)
		{
			
			bool IsSuccess = false;
			try
			{
				if (ValidateTable(Table))
				{	
					int NbRecAff = 0;
					NbRecAff = DataAccess.Update(Table);
					if (NbRecAff != 0)
					{
						IsSuccess = true; 
					}
					else
					{
						messageManager.ValidationExceptionType = QSPFormExceptionType.OtherBusinessRules;
						messageManager.SetErrorMessage(QSPFormMessage.ERRMSG_NO_REC_AFF);
						IsSuccess = false;
					}
				}
			}
			catch(Exception ex)
			{
				if (ex is QSPForm.Common.QSPFormException)
				{
					//send the exception and the info to present
					throw ex;
				}
				else
				{
					ManageError(ex);
					throw new QSPForm.Common.QSPFormException(messageManager, ex);
				}
			}
			//
			// Return the result of the operation
			//            
			return IsSuccess;
			
		}

		protected bool UpdateBatch(DataTable Table, QSPForm.Data.DBTableOperation DataAccess)
		{			
			
			bool IsSuccess = false;
			try
			{
				if (ValidateTable(Table))
				{	
					int NbRecAff = 0;
					NbRecAff = DataAccess.UpdateBatch(Table);
					if (NbRecAff != 0)
					{
						IsSuccess = true; 
					}
					else
					{
						messageManager.ValidationExceptionType = QSPFormExceptionType.OtherBusinessRules;
						messageManager.SetErrorMessage(QSPFormMessage.ERRMSG_NO_REC_AFF);
						IsSuccess = false;
					}
				}
			}
			catch(Exception ex)
			{
				if (ex is QSPForm.Common.QSPFormException)
				{
					//send the exception and the info to present
					throw ex;
				}
				else
				{
					ManageError(ex);
					throw new QSPForm.Common.QSPFormException(messageManager, ex);
				}
			}
			//
			// Return the result of the operation
			//            
			return IsSuccess;
			
		}

		protected bool UpdateBatch(DataRow[] dataRows, QSPForm.Data.DBTableOperation DataAccess)
		{			
			
			bool IsSuccess = false;
			try
			{
				if (ValidateRows(dataRows))
				{	
					int NbRecAff = 0;
					NbRecAff = DataAccess.UpdateBatch(dataRows);
					if (NbRecAff != 0)
					{
						IsSuccess = true; 
					}
					else
					{
						messageManager.ValidationExceptionType = QSPFormExceptionType.OtherBusinessRules;
						messageManager.SetErrorMessage(QSPFormMessage.ERRMSG_NO_REC_AFF);
						IsSuccess = false;
					}
				}
			}
			catch(Exception ex)
			{
				if (ex is QSPForm.Common.QSPFormException)
				{
					//send the exception and the info to present
					throw ex;
				}
				else
				{
					ManageError(ex);
					throw new QSPForm.Common.QSPFormException(messageManager, ex);
				}
			}
			//
			// Return the result of the operation
			//            
			return IsSuccess;
			
		}

		protected bool Delete(DataTable Table, QSPForm.Data.DBTableOperation DataAccess)
		{
			
			bool IsSuccess = false;
			
			try
			{
				if (ValidateTable(Table))
				{	
					int NbRecAff = 0;
					NbRecAff = DataAccess.Delete(Table);
					if (NbRecAff != 0)
					{
						IsSuccess = true; 
					}
					else
					{
						messageManager.ValidationExceptionType = QSPFormExceptionType.OtherBusinessRules;
						messageManager.SetErrorMessage(QSPFormMessage.ERRMSG_NO_REC_AFF);
						IsSuccess = false;
					}
				}
			}
			catch(Exception ex)
			{
				if (ex is QSPForm.Common.QSPFormException)
				{
					//send the exception and the info to present
					throw ex;
				}
				else
				{
					ManageError(ex);
					throw new QSPForm.Common.QSPFormException(messageManager, ex);
				}

			}
			//
			// Return the result of the operation
			//            
			return IsSuccess;
			
		}

		protected void ManageError(Exception ex)
		{
			
			if ((!(ex is QSPForm.Common.QSPFormValidationException)) || (!(ex is QSPForm.Common.QSPFormException)))
			{				
				//We replace the exception by a friendly message
				//QSPForm.Common.QSPFormMessage messageManager = new QSPForm.Common.QSPFormMessage();
				messageManager.SetSystemErrorMessage(QSPForm.Common.QSPFormMessage.ERRMSG_SYSTEM);										
				//messageManager.SetErrorMessage(new QSPForm.Common.QSPFormException(messageManager, ex));
				//We sent the information to the framework that can be sent by email
				//or inserted in a Table stroring the error from the current app
				//That depend if those facilities have been set in web.config
				//In the logic of error management only unhadled error
				//will be provide
				QSPForm.SystemFramework.ApplicationError.ManageError(ex);					
				
			}			
		}
		

	}
		
}