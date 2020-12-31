namespace Business.Objects
{
	using System;
	using System.Data;
	using Common;
	using Common.TableDef;
	using DAL;
	using dataSetRef = Common.TableDef.FieldManagerDataSet;
	using dataAccessRef = DAL.FieldManagerData;
	/// <summary>
	///     This class contains the business rules. 
	/// </summary>
	public class FieldManager : BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		dataSetRef dtsDataSet;

		public FieldManager()
		{
			this.dtsDataSet = new FieldManagerDataSet();
			CreateRulesCollection();
		}

		public FieldManager(Transaction CurrentTransaction) : this()
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
				return dataSet.FieldManager.TableName;
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

		public void GetAllByCountryCode(string countrycode)
		{
			try
			{
				dataAccess.SelectAllByCountry(dataSet, DefaultTableName, countrycode);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
			}
		}

        public void GetOneByFMID(string FMID)
        {
            try
            {
                dataAccess.SelectOne(dataSet, DefaultTableName, FMID);
            }
            catch (Exception ex)
            {
                ManageError(ex);
            }
        }

		protected override DBTableOperation DataAccessReference
		{
			get
			{
				return dataAccess;
			}
		}
	}
}