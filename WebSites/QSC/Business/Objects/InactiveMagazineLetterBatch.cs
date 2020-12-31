using System;
using Common.TableDef;
using DAL;
using dataSetRef = Common.TableDef.LetterBatchDataSet;
using dataAccessRef = DAL.LetterBatchData;
using Common;

namespace Business.Objects
{
	/// <summary>
	/// Summary description for LetterBatchItem.
	/// </summary>
	public class InactiveMagazineLetterBatch : LetterBatch
	{
		dataAccessRef dataAccess = new dataAccessRef();

		public InactiveMagazineLetterBatch():base() {}

		public InactiveMagazineLetterBatch(Transaction CurrentTransaction) : base(CurrentTransaction) {}

		public override string DefaultTableName 
		{
			get 
			{
				return dataSet.InactiveMagazineLetterBatch.TableName;
			}
		}

		public override void GetAll()
		{
			dataAccess.InactiveMagazineSelectAll(dataSet, DefaultTableName);
		}

		public override void GetAll(LetterBatchSearchCriteria letterBatchSearchCriteria)
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

			InactiveMagazineLetterBatchSearchCriteria inactiveMagazineLetterBatchSearchCriteria = letterBatchSearchCriteria as InactiveMagazineLetterBatchSearchCriteria;

			int letterBatchID = inactiveMagazineLetterBatchSearchCriteria.LetterBatchID;
			string productCode = inactiveMagazineLetterBatchSearchCriteria.ProductCode;
			int reason = inactiveMagazineLetterBatchSearchCriteria.Reason;
			
			dataAccess.InactiveMagazineSelectAll(dataSet, DefaultTableName, letterTemplateID, dateCreatedFrom, dateCreatedTo, Convert.ToInt32(letterBatchType), dateFrom, dateTo, runIDFrom, runIDTo, isPrinted, isLocked, productCode, reason);
		}

		public override int Generate(LetterBatchItem letterBatchItem) 
		{
			return Generate(letterBatchItem, DBInteractionBase.NullValueInteger, DBInteractionBase.NullValueInteger);
		}


		public override int Generate(LetterBatchItem letterBatchItem, int customerOrderHeaderInstance, int transID)
		{
			InactiveMagazineLetterBatchItem inactiveMagazineLetterBatchItem = letterBatchItem as InactiveMagazineLetterBatchItem;

			base.ValidateGenerate(letterBatchItem, customerOrderHeaderInstance, transID);

			ValidateGenerate(letterBatchItem, customerOrderHeaderInstance, transID);

			ValidateUnprocessedCount(letterBatchItem, customerOrderHeaderInstance, transID);

			return dataAccess.InactiveMagazineGenerate(letterBatchItem.LetterTemplateID,
				Convert.ToInt32(letterBatchItem.LetterBatchType),
				letterBatchItem.RunID,
				letterBatchItem.DateFrom,
				letterBatchItem.DateTo,
				customerOrderHeaderInstance,
				transID,
				letterBatchItem.IsLocked,
				letterBatchItem.UserIDCreated,
				inactiveMagazineLetterBatchItem.ProductCode,
				inactiveMagazineLetterBatchItem.Reason);						
		}

		public override void ValidateGenerate(LetterBatchItem letterBatchItem, int customerOrderHeaderInstance, int transID) 
		{
			InactiveMagazineLetterBatchItem inactiveMagazineLetterBatchItem = letterBatchItem as InactiveMagazineLetterBatchItem;

			if(inactiveMagazineLetterBatchItem.LetterBatchID != DBInteractionBase.NullValueInteger) 
			{
				throw new MessageException(CurrentMessageManager.FormatErrorMessage(Message.VALMSG_UNICITY_VAR_2, new string[] {"LetterBatchID", "Inactive Magazine Letter Batch"}));
			}

			if(inactiveMagazineLetterBatchItem.ProductCode == DBInteractionBase.NullValueString) 
			{
				string var = "Product Code";
				throw new MessageException(Message.ERRMSG_INSTANCE_DO_NOT_EXIST_1, var);
			}

			if(inactiveMagazineLetterBatchItem.Reason == DBInteractionBase.NullValueInteger) 
			{
				string var = "Reason";
				throw new MessageException(Message.ERRMSG_INSTANCE_DO_NOT_EXIST_1, var);
			}
		}

		public override void ValidateUnprocessedCount(LetterBatchItem letterBatchItem, int customerOrderHeaderInstance, int transID)
		{
			InactiveMagazineLetterBatchItem inactiveMagazineLetterBatchItem = letterBatchItem as InactiveMagazineLetterBatchItem;


			if(dataAccess.InactiveMagazineSelectUnprocessedCount(letterBatchItem.LetterTemplateID, letterBatchItem.RunID, letterBatchItem.DateFrom, letterBatchItem.DateTo, customerOrderHeaderInstance, transID, inactiveMagazineLetterBatchItem.ProductCode, inactiveMagazineLetterBatchItem.Reason) == 0)
			{
				throw new MessageException(Message.ERRMSG_NO_LETTER_FOR_PARAMS_0);
			}
		}

		public override void GetAllByCustomerOrderDetail(System.Data.DataSet dataSet, int CustomerOrderHeaderInstance, int TransID)
		{	
			dataAccess.InactiveMagazineSelectByCustomerOrderDetail(dataSet, DefaultTableName, CustomerOrderHeaderInstance, TransID);
		}

	}
}