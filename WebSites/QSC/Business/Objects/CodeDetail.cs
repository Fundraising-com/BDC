namespace Business.Objects
{
	using System;
	using System.Data;
	using Common;
	using Common.TableDef;
	using DAL;
	using dataSetRef = Common.TableDef.CodeDetailDataSet;
	using dataAccessRef = DAL.CodeDetailData;

	public enum CodeHeaderInstance 
	{
		PhoneType = 30500,
		CAccountStatus = 35000,
		CampaignStatus = 37000,
		IncentivesBillTo = 51000,
		AddressType = 54000,
		IncentivesDistribution = 62000,
		ShipSuppliesTo = 63000,
		ProgramType = 36000,
		AdjustmentBatchStatus = 64000,
		LetterBatchType = 67000,
        ProductReplacementReason = 69000
	}

	/// <summary>
	///     This class contains the business rules. 
	/// </summary>
	public class CodeDetail : BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		dataSetRef dtsDataSet;

		public CodeDetail() 
		{
			this.dtsDataSet = new dataSetRef();
			CreateRulesCollection();
		}

		public CodeDetail(Transaction CurrentTransaction) : this() 
		{
			this.CurrentTransaction = CurrentTransaction;
		}

		public CodeDetail(int CodeHeaderInstance) : this() 
		{
			this.GetAllByCodeHeaderInstance(CodeHeaderInstance);
		}

		public CodeDetail(CodeHeaderInstance chInstance) : this(Convert.ToInt32(chInstance)) { }

		public CodeDetail(int CodeHeaderInstance, Transaction CurrentTransaction) : this(CurrentTransaction) 
		{
			this.GetAllByCodeHeaderInstance(CodeHeaderInstance);
		}

		public CodeDetail(CodeHeaderInstance chInstance, Transaction CurrentTransaction) : this(Convert.ToInt32(chInstance), CurrentTransaction) { }

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
				return this.dtsDataSet.CodeDetail.TableName;
			}
		}

		protected override DBTableOperation DataAccessReference
		{
			get
			{
				return dataAccess;
			}
		}

		public void GetOneByID(int ID)
		{
			try
			{
				dataAccess.SelectOne(dtsDataSet, DefaultTableName, ID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
			}
		}

		public void GetAllByCodeHeaderInstance(int CodeHeaderInstance)
		{
			try
			{
				dataAccess.SelectAllByCodeHeaderInstance(dtsDataSet, DefaultTableName, CodeHeaderInstance);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
			}
		}

        public void GetIncentivesBillTo(bool IsCampaign2014OrLater)
        {
            try
            {
                dataAccess.SelectIncentivesBillTo(dtsDataSet, DefaultTableName, IsCampaign2014OrLater);
            }
            catch (Exception ex)
            {
                ManageError(ex);
            }
        }
	}
}