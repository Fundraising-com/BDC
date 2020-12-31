namespace Business.Objects
{
	using System;
	using System.Data;
	using Common;
	using Common.TableDef;
	using DAL;
	using dataSetRef = Common.TableDef.FieldSuppliesOrderListDataSet;
	using dataAccessRef = DAL.FieldSuppliesData;
	/// <summary>
	///     This class contains the business rules. 
	/// </summary>
	public class FieldSuppliesOrderList : BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		dataSetRef dtsDataSet;

		public FieldSuppliesOrderList()
		{
			dtsDataSet = new dataSetRef();
			CreateRulesCollection();
		}

		public FieldSuppliesOrderList(Transaction CurrentTransaction) : this()
		{
			this.CurrentTransaction = CurrentTransaction;
		}

		public FieldSuppliesOrderList(int CampaignID) : this()
		{
			Search(CampaignID);
		}

		public FieldSuppliesOrderList(int CampaignID, Transaction CurrentTransaction) : this(CurrentTransaction)
		{
			Search(CampaignID);
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
				return dataSet.FieldSuppliesOrderList.TableName;
			}
		}

		protected override DBTableOperation DataAccessReference
		{
			get
			{
				return dataAccess;
			}
		}

		public void Search(int CampaignID)
		{
			try
			{
				dataAccess.SelectSearch(dataSet, DefaultTableName, CampaignID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
			}
		}
	}
}