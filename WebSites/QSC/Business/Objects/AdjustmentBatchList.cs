namespace Business.Objects
{
	using System;
	using System.Data;
	using Common;
	using Common.TableDef;
	using DAL;
	using dataSetRef = Common.TableDef.AdjustmentBatchListDataSet;
	using AdjustmentBatchDataAccessRef = DAL.AdjustmentBatchData;
	using AdjustmentDataAccessRef = DAL.AdjustmentData;
	/// <summary>
	///     This class contains the business rules. 
	/// </summary>
	public class AdjustmentBatchList : BusinessSystem
	{
		AdjustmentBatchDataAccessRef AdjustmentBatchDataAccess = new AdjustmentBatchDataAccessRef();
		AdjustmentDataAccessRef AdjustmentDataAccess = new AdjustmentDataAccessRef();
		dataSetRef dtsDataSet;

		public AdjustmentBatchList()
		{
			dtsDataSet = new dataSetRef();
			CreateRulesCollection();
		}

		public AdjustmentBatchList(Transaction CurrentTransaction) : this()
		{
			this.CurrentTransaction = CurrentTransaction;
		}

		public AdjustmentBatchList(int id, int adjustmentTypeID, int status, DateTime dateFrom, DateTime dateTo) : this()
		{
			Search(id, adjustmentTypeID, status, dateFrom, dateTo);
		}

		public AdjustmentBatchList(int id, int adjustmentTypeID, int status, DateTime dateFrom, DateTime dateTo, Transaction CurrentTransaction) : this(CurrentTransaction)
		{
			Search(id, adjustmentTypeID, status, dateFrom, dateTo);
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
				return dataSet.AdjustmentBatch.TableName;
			}
		}

		protected override DBTableOperation DataAccessReference
		{
			get
			{
				return AdjustmentBatchDataAccess;
			}
		}

		public void Search(int id, int adjustmentTypeID, int status, DateTime dateFrom, DateTime dateTo) 
		{
			Search(id, adjustmentTypeID, status, dateFrom, dateTo, true);
		}

		public void Search(int id, int adjustmentTypeID, int status, DateTime dateFrom, DateTime dateTo, bool searchAdjustments)
		{
			try
			{
				AdjustmentBatchDataAccess.SelectSearch(dataSet, dataSet.AdjustmentBatch.TableName, id, adjustmentTypeID, status, dateFrom, dateTo);
				
				if(searchAdjustments) 
				{
					AdjustmentDataAccess.SelectSearch(dataSet, dataSet.ADJUSTMENT.TableName, id, adjustmentTypeID, status, dateFrom, dateTo);
				}
			}
			catch (Exception ex)
			{	
				ManageError(ex);
			}
		}

		public void Preview(int adjustmentTypeID, DateTime dateFrom, DateTime dateTo) 
		{
			try 
			{
				// Create blank AdjustmentBatch row for preview
				dataSet.AdjustmentBatch.AddAdjustmentBatchRow(0, String.Empty, 0, String.Empty, DateTime.Now, DateTime.Now, 0, String.Empty, DateTime.Now, 0, String.Empty, DateTime.Now);
				AdjustmentDataAccess.PreviewBatch(dataSet, dataSet.ADJUSTMENT.TableName, adjustmentTypeID, dateFrom, dateTo);
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}
	}
}