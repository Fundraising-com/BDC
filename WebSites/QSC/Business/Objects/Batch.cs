namespace Business.Objects
{
	using System;
	using System.Data;
	using Common;
	using Common.TableDef;
	using DAL;
	using dataSetRef = Common.TableDef.BatchDataSet;
	using dataAccessRef = DAL.BatchData;
	/// <summary>
	///     This class contains the business rules. 
	/// </summary>
	public class Batch : BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		dataSetRef dtsDataSet;

		public Batch()
		{
			this.dtsDataSet = new dataSetRef();
			CreateRulesCollection();
		}

		public Batch(Transaction CurrentTransaction) : this() 
		{
			this.CurrentTransaction = CurrentTransaction;
		}

		public Batch(int CampaignID, int OrderQualifierID, Transaction CurrentTransaction) : this(CurrentTransaction)
		{
			GetAllByCampaignAndOrderQualifier(CampaignID, OrderQualifierID);
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
				return dataSet.Batch.TableName;
			}
		}

		protected override DBTableOperation DataAccessReference
		{
			get
			{
				return dataAccess;
			}
		}

		public void GetAllByCampaignAndOrderQualifier(int CampaignID, int OrderQualifierID)
		{
			try
			{
				dataAccess.SelectAllByCampaignIDAndOrderQualifierID(dataSet, DefaultTableName, CampaignID, OrderQualifierID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
			}
		}

		public bool ContainsBatch 
		{
			get 
			{
				return this.dtsDataSet.Batch.Rows.Count != 0;
			}
		}
	}
}