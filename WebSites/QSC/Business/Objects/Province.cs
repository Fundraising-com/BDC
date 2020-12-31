namespace Business.Objects
{
	using System;
	using System.Data;
	using Common;
	using Common.TableDef;
	using DAL;
	using dataSetRef = Common.TableDef.ProvinceDataSet;
	using dataAccessRef = DAL.ProvinceData;
	/// <summary>
	///     This class contains the business rules. 
	/// </summary>
	public class Province : BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		dataSetRef dtsDataSet;

		public Province() 
		{
			this.dtsDataSet = new dataSetRef();
			CreateRulesCollection();
		}

		public Province(string ProvinceCode) : this()
		{
			GetOneByProvinceCode(ProvinceCode);
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
				return this.dtsDataSet.Province.TableName;
			}
		}

		protected override DBTableOperation DataAccessReference
		{
			get
			{
				return dataAccess;
			}
		}

		public void SelectAll()
		{
			try
			{
				dataAccess.SelectAll(dtsDataSet, DefaultTableName);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
			}
		}

		public void GetOneByProvinceCode(string ProvinceCode)
		{
			try
			{
				dataAccess.SelectOne(dtsDataSet, DefaultTableName, ProvinceCode);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
			}
		}
	}
}