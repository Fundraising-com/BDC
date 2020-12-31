namespace Business.Objects
{
	using System;
	using System.Data;
	using Common;
	using Common.TableDef;
	using DAL;
	using dataSetRef = Common.TableDef.AdjustmentBatchDataSet;
	using dataAccessRef = DAL.AdjustmentBatchData;

	[Serializable]
	public enum AdjustmentBatchStatus 
	{
		Approved = 64001,
		Error = 64002,
		Canceled = 64003
	}

	/// <summary>
	///     This class contains the business rules. 
	/// </summary>
	public class AdjustmentBatch : BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		dataSetRef dtsDataSet;

		public AdjustmentBatch()
		{
			dtsDataSet = new dataSetRef();
			CreateRulesCollection();
		}

		public AdjustmentBatch(Transaction CurrentTransaction) : this()
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
				return dataSet.AdjustmentBatch.TableName;
			}
		}

		protected override DBTableOperation DataAccessReference
		{
			get
			{
				return dataAccess;
			}
		}

		public int Generate(int adjustmentTypeID, DateTime dateFrom, DateTime dateTo, int userID) 
		{
			int adjustmentBatchID = 0;

			try 
			{
				if(ValidateGenerateAdjustment(adjustmentTypeID, dateFrom, dateTo))
				{
					adjustmentBatchID = dataAccess.Generate(adjustmentTypeID, dateFrom, dateTo, userID);
				}
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}

			return adjustmentBatchID;
		}

		private bool ValidateGenerateAdjustment(int adjustmentTypeID, DateTime dateFrom, DateTime dateTo) 
		{
			string adjustmentBatchIDList = String.Empty;
			bool isValid = true;
			AdjustmentBatchList adjustmentBatchList = new AdjustmentBatchList();
			DataView adjustmentBatchListView = null;
			AdjustmentBatchListDataSet.AdjustmentBatchRow adjustmentBatchRow = null;

			adjustmentBatchList.Search(0, adjustmentTypeID, 0, dateFrom, dateTo, false);
			adjustmentBatchListView = new DataView(adjustmentBatchList.dataSet.AdjustmentBatch, "Status = 64001 OR Status = 64004", String.Empty, DataViewRowState.CurrentRows);

			if(adjustmentBatchListView.Count > 0) 
			{
				foreach(DataRowView rowView in adjustmentBatchListView) 
				{
					adjustmentBatchRow = rowView.Row as AdjustmentBatchListDataSet.AdjustmentBatchRow;

					if(adjustmentBatchRow != null) 
					{
						if(adjustmentBatchIDList != String.Empty) 
						{
							adjustmentBatchIDList += ", ";
						}

						adjustmentBatchIDList += adjustmentBatchRow.ID.ToString();
					}
				}

				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_ADJUSTMENT_BATCH_CONFLICT_1, adjustmentBatchIDList));
				CurrentMessageManager.PrepareErrorMessage();
				throw new MessageException(CurrentMessageManager);
			}

			return isValid;
		}

		public void UpdateStatus(int id, AdjustmentBatchStatus status, int userID) 
		{
			try 
			{
				dataAccess.UpdateStatus(id, Convert.ToInt32(status), userID);
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}
	}
}