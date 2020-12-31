namespace Business.Objects
{
	using System;
	using System.Data;
	using Common;
	using Common.TableDef;
	using DAL;
	using dataSetRef = Common.TableDef.AdjustmentTypeDataSet;
	using dataAccessRef = DAL.AdjustmentTypeData;
	
	/// <summary>
	///     This class contains the business rules. 
	/// </summary>
	public class AdjustmentType : BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		dataSetRef dtsDataSet;

		public AdjustmentType()
		{
			dtsDataSet = new dataSetRef();
			CreateRulesCollection();
		}

		public AdjustmentType(Transaction CurrentTransaction) : this()
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
				return dataSet.ADJUSTMENT_TYPE.TableName;
			}
		}

		protected override DBTableOperation DataAccessReference
		{
			get
			{
				return dataAccess;
			}
		}

		public void GetAll()
		{
			try
			{
				dataAccess.SelectAll(dataSet, DefaultTableName);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
			}
		}

		public void GetAllForBatches()
		{
			try
			{
				dataAccess.SelectAllForBatches(dataSet, DefaultTableName);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
			}
		}
	}
}