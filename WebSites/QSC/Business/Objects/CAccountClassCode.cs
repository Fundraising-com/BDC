namespace Business.Objects
{
	using System;
	using System.Data;
	using Common;
	using Common.TableDef;
	using DAL;
	using dataSetRef = Common.TableDef.CAccountClassCodeDataSet;
	using CAccountClassDataAccessRef = DAL.CAccountClassData;
	using CAccountCodeDataAccessRef = DAL.CAccountCodeData;
	/// <summary>
	///     This class contains the business rules. 
	/// </summary>
	public class CAccountClassCode : BusinessSystem
	{
		CAccountClassDataAccessRef CAccountClassDataAccess = new CAccountClassDataAccessRef();
		CAccountCodeDataAccessRef CAccountCodeDataAccess = new CAccountCodeDataAccessRef();
		dataSetRef dtsDataSet;

		public CAccountClassCode()
		{
			this.dtsDataSet = new dataSetRef();
			CreateRulesCollection();
		}

		public CAccountClassCode(Transaction CurrentTransaction) : this() 
		{
			this.CurrentTransaction = CurrentTransaction;
		}

		public dataSetRef dataSet 
		{
			get 
			{
				return this.dtsDataSet;
			}
		}

		internal override DataSet baseDataSet
		{
			get
			{
				return (DataSet) this.dtsDataSet;
			}
		}

		public override string DefaultTableName
		{
			get
			{
				return this.dtsDataSet.CAccountClass.TableName;
			}
		}

		protected override DBTableOperation DataAccessReference
		{
			get
			{
				return CAccountClassDataAccess;
			}
		}

		public void GetAll()
		{
			try
			{
				CAccountClassDataAccess.SelectAll(dtsDataSet, dtsDataSet.CAccountClass.TableName);
				CAccountCodeDataAccess.SelectAll(dtsDataSet, dtsDataSet.CAccountCode.TableName);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
			}
		}
	}
}