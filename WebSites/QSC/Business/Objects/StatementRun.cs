namespace Business.Objects
{
	using System;
	using System.Data;
	using Common;
	using Common.TableDef;
	using DAL;
	using dataSetRef = Common.TableDef.StatementRunDataSet;
    using dataAccessRef = DAL.StatementRunData;
	
	/// <summary>
	///     This class contains the business rules. 
	/// </summary>
	public class StatementRun : BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		dataSetRef dtsDataSet;

        public StatementRun()
		{
			dtsDataSet = new dataSetRef();
			CreateRulesCollection();
		}

		public StatementRun(Transaction CurrentTransaction) : this()
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
				return dataSet.StatementRun.TableName;
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

        public void GetAllCompleted()
        {
            try
            {
                dataAccess.SelectAll(dataSet, DefaultTableName);

                for (int i = dataSet.Tables[this.DefaultTableName].Rows.Count - 1; i >= 0; i--)
                {
                    if (Convert.ToDateTime(dataSet.Tables[this.DefaultTableName].Rows[i]["StatementRunDate"].ToString()) >= DateTime.Now)
                        dataSet.Tables[this.DefaultTableName].Rows.RemoveAt(i);
                }
            }
            catch (Exception ex)
            {
                ManageError(ex);
            }
        }
	}
}