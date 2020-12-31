using System;
using System.Data;
using System.Collections;
using System.Runtime.Remoting;
using System.Reflection;
using Business.Rules;
using DAL;
using System.Text.RegularExpressions;
using Common;
using Common.TableDef;

namespace Business.Objects
{
	/// <summary>
	/// This class is central object of the Business Layer.
	/// </summary> 
	public abstract class BusinessSystem : MarshalByRefObject
	{
		private Message messageManager;
		protected int _errorCode = 0;
		protected int NbRowAffected = 0;/// <summary>
		protected object oResultSetReturned;
		//protected ExceptionType errorType;
		protected RulesCollection oRulesCollection;
		protected Transaction oCurrentTransaction = null;

		public BusinessSystem() { }

		public BusinessSystem(Message messageManager) : this() 
		{
			this.messageManager = messageManager;
		}

		internal abstract DataSet baseDataSet
		{
			get;
		}

		public abstract string DefaultTableName 
		{
			get;
		}
		
		public object ResultSetReturned
		{
			get
			{
				return oResultSetReturned;
			}
		}

		protected Message CurrentMessageManager
		{
			get
			{
				if(messageManager == null)
					messageManager = new Message(false);

				return messageManager;
			}
		}

		protected abstract DBTableOperation DataAccessReference
		{
			get;
		}

		internal ConnectionProvider MainConnectionProvider
		{
			set
			{
				DataAccessReference.MainConnectionProvider = value;
			}
		}

		public Transaction CurrentTransaction 
		{
			get 
			{
				return oCurrentTransaction;
			}
			set 
			{
				this.oCurrentTransaction = value;

				if(value != null) 
				{
					this.MainConnectionProvider = value.MainConnectionProvider;
				}
			}
		}

		/// <summary>
		/// Insert row inserted in the DataSet's first table
		/// </summary>
		/// <param name="dtsDataSet">DataSet containing the table containing the row to be inserted</param>
		/// <returns>true if successful; otherwise, false.</returns>
		protected virtual bool Insert()
		{
			bool IsSuccess = false;
			try
			{
				if(ValidateInsert()) 
				{

					NbRowAffected = 0;

					if(Convert.ToBoolean(DataAccessReference.Insert(baseDataSet, DefaultTableName)))
					{
						IsSuccess = true; 
					}
					else
					{
						CurrentMessageManager.Add(Message.ERRMSG_SYSTEM_VAR_0);
						CurrentMessageManager.PrepareErrorMessage();
						throw new MessageException(CurrentMessageManager);
					}
					NbRowAffected = DataAccessReference.NbRowAffected;
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
		/// Update row modified in the DataSet's first table
		/// </summary>
		/// <param name="dtsDataSet">DataSet containing the table containing the row to be updated</param>
		/// <returns>true if successful; otherwise, false.</returns>
		protected virtual bool Update()
		{
			bool IsSuccess = false;
			try
			{
				if(ValidateUpdate()) 
				{
						
					NbRowAffected = 0;
						 
					if (Convert.ToBoolean(DataAccessReference.Update(baseDataSet, DefaultTableName)))
					{
						IsSuccess = true;
					}
					else
					{
			
						CurrentMessageManager.Add(Message.ERRMSG_SYSTEM_VAR_0);
						CurrentMessageManager.PrepareErrorMessage();
						throw new MessageException(CurrentMessageManager);
					}
					NbRowAffected = DataAccessReference.NbRowAffected;
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
		/// Delete row deleted in the DataSet's first table
		/// </summary>
		/// <param name="dtsDataSet">DataSet containing the table containing the row to be deleted</param>
		/// <returns>true if successful; otherwise, false.</returns>
		protected virtual bool Delete()
		{
			bool IsSuccess = false;
			try
			{
				if(ValidateDelete()) 
				{
						
					NbRowAffected = 0;
						
					if (Convert.ToBoolean(DataAccessReference.Delete(baseDataSet, DefaultTableName)))
					{
						IsSuccess = true;
					}
					else
					{
						CurrentMessageManager.Add(Message.ERRMSG_SYSTEM_VAR_0);
						CurrentMessageManager.PrepareErrorMessage();
						throw new MessageException(CurrentMessageManager);
					}
					NbRowAffected = DataAccessReference.NbRowAffected;
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
		/// Update, Insert, Delete row in the DataSet's first table
		/// </summary>
		/// <param name="dtsDataSet">DataSet containing table containing the row to be updated, inserted and deleted</param>
		/// <returns>true if successful; otherwise, false.</returns>
		protected virtual bool UpdateBatch()
		{
			bool IsSuccess = false;
			try
			{
				if(ValidateUpdateBatch()) 
				{
					
					NbRowAffected = 0;
		
					if (Convert.ToBoolean(DataAccessReference.UpdateBatch(baseDataSet, DefaultTableName)))
					{
						IsSuccess = true;
					}
					else
					{
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
		/// Validate information in the DataSet
		/// </summary>
		/// <param name="dtsDataSet">DataSet to be validated</param>
		/// <returns>False if DataSet is not valid</returns>
		protected virtual bool ValidateUpdate()
		{
			bool result = true;
			DataSet dtsUpdated;

			// Only Validate the rows with an adequate state
			dtsUpdated = baseDataSet.GetChanges(DataRowState.Modified);
			
			if(dtsUpdated != null) 
			{
				foreach(Rules.RulesBase rule in oRulesCollection) 
				{
					result &= rule.Validate(this, DataRowState.Modified);
				}

				foreach(DataTable Table in dtsUpdated.Tables) 
				{
					foreach(DataRow row in Table.Rows)
					{
						result = ValidateUpdate(row);
						if (!result)
							break;
					}
					if ( !result )
					{
						CurrentMessageManager.PrepareErrorMessage();
						throw new MessageException(CurrentMessageManager);
					}
				}

				baseDataSet.Merge(dtsUpdated, false, MissingSchemaAction.Add);
			}

			return result;
		}
		/// <summary>
		/// Validate information in the DataSet
		/// </summary>
		/// <param name="dtsDataSet">DataSet to be validated</param>
		/// <returns>False if DataSet is not valid</returns>
		protected virtual bool ValidateInsert()
		{
			bool result = true;
			DataSet dtsInserted;

			// Only Validate the rows with an adequate state
			dtsInserted = baseDataSet.GetChanges(DataRowState.Added);
			
			if(dtsInserted != null) 
			{
				foreach(Rules.RulesBase rule in oRulesCollection) 
				{
					result &= rule.Validate(this, DataRowState.Added);
				}

				foreach(DataTable Table in dtsInserted.Tables) 
				{
					foreach(DataRow row in Table.Rows)
					{
						result = ValidateInsert(row);
						if (!result)
							break;
					}
					if ( !result )
					{
						CurrentMessageManager.PrepareErrorMessage();
						throw new MessageException(CurrentMessageManager);
					}
				}

				baseDataSet.Merge(dtsInserted, false, MissingSchemaAction.Add);
			}

			return result;
		}
		/// <summary>
		/// Validate information in the DataSet
		/// </summary>
		/// <param name=\"dtsDataSet\">DataSet to be validated</param>
		/// <returns>False if DataSet is not valid</returns>
		protected virtual bool ValidateDelete()
		{
			bool result = true;
			DataSet dtsDeleted;

			// Only Validate the rows with an adequate state
			dtsDeleted = baseDataSet.GetChanges(DataRowState.Deleted);
			
			if(dtsDeleted != null) 
			{
				foreach(DataTable Table in dtsDeleted.Tables) 
				{
					foreach(DataRow row in Table.Rows)
					{
						result = ValidateDelete(row);
						if (!result)
							break;
					}
					if ( !result )
					{
						CurrentMessageManager.PrepareErrorMessage();
						throw new MessageException(CurrentMessageManager);
					}
				}

				baseDataSet.Merge(dtsDeleted, false, MissingSchemaAction.Add);
			}

			return result;
		}
		/// <summary>
		/// Validate information in the DataSet
		/// </summary>
		/// <param name=\"dtsDataSet\">DataSet to be validated</param>
		/// <returns>False if DataSet is not valid</returns>
		protected virtual bool ValidateUpdateBatch()
		{
			bool result = true;
			DataSet dtsModified;

			foreach(Rules.RulesBase rule in oRulesCollection) 
			{
				result &= rule.Validate(this, DataRowState.Added | DataRowState.Modified);
			}

			// Only Validate the rows with an adequate state
			dtsModified = baseDataSet.GetChanges(DataRowState.Added | DataRowState.Modified | DataRowState.Deleted);

			if(dtsModified != null) 
			{
				foreach(DataTable Table in dtsModified.Tables)
				{
					foreach(DataRow row in Table.Rows)
					{
						switch (row.RowState)
						{
							case DataRowState.Added:
								result &= ValidateInsert(row);
								break;
							case DataRowState.Modified:
								result &= ValidateUpdate(row);
								break;
							case DataRowState.Deleted:
								result &= ValidateDelete(row);
								break;
						}
							
						if (!result)
							break;
					}
				}
			}

			if ( !result )
			{
				CurrentMessageManager.PrepareErrorMessage();
				throw new MessageException(CurrentMessageManager);
			} 
			else if(dtsModified != null) 
			{
				baseDataSet.Merge(dtsModified, false, MissingSchemaAction.Add);
			}

			return result;
		}

		protected virtual bool ValidateUpdate(DataRow row)
		{
			bool isValid = true;

			foreach(Rules.RulesBase rule in oRulesCollection) 
			{
				if(this.CurrentTransaction != null) 
				{
					rule.CurrentTransaction = this.CurrentTransaction;
				}

				isValid &= rule.Validate(row);
			}

			return isValid;
		}

		protected virtual bool ValidateInsert(DataRow row)
		{
			bool isValid = true;

			foreach(Rules.RulesBase rule in oRulesCollection) 
			{
				if(this.CurrentTransaction != null) 
				{
					rule.CurrentTransaction = this.CurrentTransaction;
				}

				isValid &= rule.Validate(row);
			}

			return isValid;
		}

		/// <summary>
		/// calls rules with ValidateDelete() implemented 
		/// to validate the deletion of entries 
		/// </summary>
		/// <param name="row">row to validate</param>
		/// <returns>true if OK to delete, false otherwise</returns>
		protected virtual bool ValidateDelete(DataRow row)
		{
			bool isValid = true;

			foreach(Rules.RulesBase rule in oRulesCollection) 
			{
				if(this.CurrentTransaction != null) 
				{
					rule.CurrentTransaction = this.CurrentTransaction;
				}

				isValid &= rule.ValidateDelete(row);
			}

			return isValid;
		}

		protected virtual bool ValidateUpdateBatch(DataRow row)
		{
			bool isValid = true;

			foreach(Rules.RulesBase rule in oRulesCollection) 
			{
				if(this.CurrentTransaction != null) 
				{
					rule.CurrentTransaction = this.CurrentTransaction;
				}

				isValid &= rule.Validate(row);
			}

			return isValid;
		}

		protected virtual void CreateRulesCollection() 
		{
			RulesBase ruleInstance;

			oRulesCollection = new RulesCollection();

			foreach(DictionaryEntry de in baseDataSet.ExtendedProperties) 
			{
				if(de.Key.ToString().StartsWith("BusinessRule")) 
				{
					try 
					{
						ruleInstance = (RulesBase) System.Activator.CreateInstance(null, "Business.Rules." + de.Value.ToString(), false, BindingFlags.Default, null, new object[] {CurrentMessageManager}, null, null, null).Unwrap();
						
						if(ruleInstance != null) 
						{
							oRulesCollection.Add(ruleInstance);
						}
					} 
					catch { }
				}
			}
		}

		protected void ManageError(Exception ex)
		{

			if (ex is MessageException )
			{
				throw ex;
			}
			else
			{
				if(this.CurrentTransaction != null) 
				{
					this.CurrentTransaction.Cancel();
				}

				QSPFulfillment.DataAccess.Common.ApplicationError.ManageError(ex);
				CurrentMessageManager.SetSystemErrorMessage(Message.ERRMSG_SYSTEM_VAR_0);
				throw new MessageException(CurrentMessageManager, ex);
			}	
			
		}
		protected void ManageError(Common.MessageException ex)
		{			
			QSPFulfillment.DataAccess.Common.ApplicationError.ManageError(ex);	
		}
	}
}