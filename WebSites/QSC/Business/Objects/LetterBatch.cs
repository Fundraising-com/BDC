namespace Business.Objects
{
	using System;
	using System.Data;
	using Common;
	using Common.TableDef;
	using DAL;
	using dataSetRef = Common.TableDef.LetterBatchDataSet;
	using dataAccessRef = DAL.LetterBatchData;
	
	public enum LetterBatchType 
	{
		DateRange		= 67001,
		RemitBatchID	= 67002,
		CustomerService	= 67003
	}

	/// <summary>
	///     This class contains the business rules. 
	/// </summary>
	public class LetterBatch : BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		dataSetRef dtsDataSet;

		public LetterBatch()
		{
			dtsDataSet = new dataSetRef();
			CreateRulesCollection();
		}

		public LetterBatch(Transaction CurrentTransaction) : this()
		{
			this.CurrentTransaction = CurrentTransaction;
		}

		public dataSetRef dataSet
		{
			get 
			{
				return dtsDataSet;
			}
		}

		internal override DataSet baseDataSet
		{
			get 
			{
				return (DataSet) dtsDataSet;
			}
		}

		public override string DefaultTableName 
		{
			get 
			{
				return dataSet.LetterBatch.TableName;
			}
		}

		protected override DBTableOperation DataAccessReference
		{
			get
			{
				return dataAccess;
			}
		}

		public virtual void GetAll()
		{
			dataAccess.SelectAll(dataSet, DefaultTableName);
		}

		public virtual void GetAll(LetterBatchSearchCriteria letterBatchSearchCriteria)
		{
			int letterTemplateID = letterBatchSearchCriteria.LetterTemplateID;
			DateTime dateCreatedFrom = letterBatchSearchCriteria.DateCreatedFrom;
			DateTime dateCreatedTo = letterBatchSearchCriteria.DateCreatedTo;
			LetterBatchType letterBatchType = letterBatchSearchCriteria.LetterBatchType;
			DateTime dateFrom = letterBatchSearchCriteria.DateFrom;
			DateTime dateTo = letterBatchSearchCriteria.DateTo;
			int runIDFrom = letterBatchSearchCriteria.RunIDFrom;
			int runIDTo = letterBatchSearchCriteria.RunIDTo;
			BooleanNullable isPrinted = letterBatchSearchCriteria.IsPrinted;
			BooleanNullable isLocked = letterBatchSearchCriteria.IsLocked;
			
			dataAccess.SelectAll(dataSet, DefaultTableName, letterTemplateID, dateCreatedFrom, dateCreatedTo, Convert.ToInt32(letterBatchType), dateFrom, dateTo, runIDFrom, runIDTo, isPrinted, isLocked);
		}

		public virtual void GetAllByCustomerOrderDetail(DataSet dataSet, int CustomerOrderHeaderInstance, int TransID)
		{	
			dataAccess.SelectByCustomerOrderDetail(dataSet, DefaultTableName, CustomerOrderHeaderInstance, TransID);
		}

		public virtual int Generate(LetterBatchItem letterBatchItem) 
		{
			return Generate(letterBatchItem, DBInteractionBase.NullValueInteger, DBInteractionBase.NullValueInteger);
		}

		public virtual int Generate(LetterBatchItem letterBatchItem, int customerOrderHeaderInstance, int transID) 
		{
			ValidateGenerate(letterBatchItem, customerOrderHeaderInstance, transID);

			ValidateUnprocessedCount(letterBatchItem, customerOrderHeaderInstance, transID);

			return dataAccess.Generate(
				letterBatchItem.LetterTemplateID,
				Convert.ToInt32(letterBatchItem.LetterBatchType),
				letterBatchItem.RunID,
				letterBatchItem.DateFrom,
				letterBatchItem.DateTo,
				customerOrderHeaderInstance,
				transID,
				letterBatchItem.IsLocked,
				letterBatchItem.UserIDCreated);
		}

		public virtual void Cancel(int ID) 
		{
			if(dataAccess.Delete(ID) == 0) 
			{
				throw new MessageException(Message.ERRMSG_NO_REC_AFF_VAR_0);
			}
		}

		public virtual void CancelLetterBatchCustomerOrderDetail(int CustomerOrderHeaderInstance, int TransID) 
		{
			if(dataAccess.DeleteLetterBatchCustomerOrderDetail(CustomerOrderHeaderInstance, TransID) == 0) 
			{
				throw new MessageException(Message.ERRMSG_NO_REC_AFF_VAR_0);
			}
		}

		public virtual void Update(LetterBatchItem letterBatchItem) 
		{
			if(dataAccess.Update(letterBatchItem.ID, letterBatchItem.IsPrinted, letterBatchItem.DatePrinted, letterBatchItem.IsLocked) == 0) 
			{
				throw new MessageException(Message.ERRMSG_NO_REC_AFF_VAR_0);
			}
		}

		public void ValidateGenerate(LetterBatchItem letterBatchItem) 
		{
			ValidateGenerate(letterBatchItem, DBInteractionBase.NullValueInteger, DBInteractionBase.NullValueInteger);
		}

		public virtual void ValidateGenerate(LetterBatchItem letterBatchItem, int customerOrderHeaderInstance, int transID) 
		{
			bool isValid = true;

			if(letterBatchItem.ID != DBInteractionBase.NullValueInteger) 
			{
				throw new MessageException(CurrentMessageManager.FormatErrorMessage(Message.VALMSG_UNICITY_VAR_2, new string[] {"ID", "Letter Batch"}));
			}

			if(letterBatchItem.LetterBatchType == LetterBatchType.DateRange) 
			{
				if(letterBatchItem.DateFrom > letterBatchItem.DateTo) 
				{
					CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_CANNOT_BE_HIGHER_2, new string[] {"Date From", "Date To"}));
					isValid = false;
				}
			} 
			else if(letterBatchItem.LetterBatchType == LetterBatchType.CustomerService) 
			{
				if(customerOrderHeaderInstance != DBInteractionBase.NullValueInteger &&
					transID != DBInteractionBase.NullValueInteger) 
				{
					letterBatchItem.IsLocked = false;
				} 
				else 
				{
					throw new MessageException(Message.ERRMSG_SEARCH_AT_LEAST_ONE_ENTRY_0);
				}
			}

			if(!isValid) 
			{
				CurrentMessageManager.PrepareErrorMessage();
				throw new MessageException(CurrentMessageManager);
			}
		}

		public virtual void ValidateUnprocessedCount(LetterBatchItem letterBatchItem, int customerOrderHeaderInstance, int transID)
		{
			if(dataAccess.SelectUnprocessedCount(letterBatchItem.LetterTemplateID, letterBatchItem.RunID, letterBatchItem.DateFrom, letterBatchItem.DateTo, customerOrderHeaderInstance, transID) == 0) 
			{
				throw new MessageException(Message.ERRMSG_NO_LETTER_FOR_PARAMS_0);
			}
		}
	}
}