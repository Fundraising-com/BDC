namespace Business.Objects
{
	using System;
	using System.Data;
	using Common;
	using Common.TableDef;
	using DAL;
	using dataSetRef = Common.TableDef.AccountCampaignListDataSet;
	using CAccountDataAccessRef = DAL.CAccountData;
	using CampaignDataAccessRef = DAL.CampaignData;
	/// <summary>
	///     This class contains the business rules. 
	/// </summary>
	public class AccountCampaignList : BusinessSystem
	{
		CAccountDataAccessRef CAccountDataAccess = new CAccountDataAccessRef();
		CampaignDataAccessRef CampaignDataAccess = new CampaignDataAccessRef();
		dataSetRef dtsDataSet;

		public AccountCampaignList()
		{
			dtsDataSet = new dataSetRef();
			CreateRulesCollection();
		}

		public AccountCampaignList(Transaction CurrentTransaction) : this()
		{
			this.CurrentTransaction = CurrentTransaction;
		}

		public AccountCampaignList(string FMID, int AccountID, int CampaignID, string Name, string City, string Province, string PostalCode, int FiscalYear, string SupplyDateFrom, string SupplyDateTo) : this()
		{
			Search(FMID, AccountID, CampaignID, Name, City, Province, PostalCode, FiscalYear,SupplyDateFrom,SupplyDateTo,0);
		}

		public AccountCampaignList(string FMID, int AccountID, int CampaignID, string Name, string City, string Province, string PostalCode, int FiscalYear,  string SupplyDateFrom, string SupplyDateTo,Transaction CurrentTransaction) : this(CurrentTransaction)
		{
			Search(FMID, AccountID, CampaignID, Name, City, Province, PostalCode, FiscalYear,SupplyDateFrom,SupplyDateTo,0);
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
				return dataSet.CAccount.TableName;
			}
		}

		protected override DBTableOperation DataAccessReference
		{
			get
			{
				return CAccountDataAccess;
			}
		}

		public void Search(string FMID, int AccountID, int CampaignID, string Name, string City, string Province, string PostalCode, int FiscalYear, string SupplyDateFrom, string SupplyDateTo, int iSuppliesGenerated)
		{
			try
			{
				CAccountDataAccess.SelectSearch(dataSet, dataSet.CAccount.TableName, FMID, AccountID, CampaignID, Name, City, Province, PostalCode, "", "", FiscalYear, 2,SupplyDateFrom,SupplyDateTo,iSuppliesGenerated);
				CampaignDataAccess.SelectSearch(dataSet, dataSet.Campaign.TableName, FMID, AccountID, CampaignID, Name, City, Province, PostalCode, "", "", FiscalYear,SupplyDateFrom,SupplyDateTo,iSuppliesGenerated);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
			}
		}
        public void SearchCampaignByAccount(string FMID, int AccountID, int CampaignID, string Name, string City, string Province, string PostalCode, int FiscalYear, string SupplyDateFrom, string SupplyDateTo, int iSuppliesGenerated)
        {
            try
            {
                CAccountDataAccess.SelectSearch(dataSet, dataSet.CAccount.TableName, FMID, AccountID, CampaignID, Name, City, Province, PostalCode, "", "", FiscalYear, 2, SupplyDateFrom, SupplyDateTo, iSuppliesGenerated);
                CampaignDataAccess.SelectSearch(dataSet, dataSet.Campaign.TableName, FMID, AccountID, CampaignID, Name, City, Province, PostalCode, "", "", FiscalYear, SupplyDateFrom, SupplyDateTo, iSuppliesGenerated);
            }
            catch (Exception ex)
            {
                ManageError(ex);
            }
        }
        public void SearchAccountOnly(string FMID, int AccountID, int CampaignID, string Name, string City, string Province, string PostalCode, int FiscalYear, string SupplyDateFrom, string SupplyDateTo, int iSuppliesGenerated)
		{
			try
			{
				CAccountDataAccess.SelectSearch(dataSet, dataSet.CAccount.TableName, FMID, AccountID, CampaignID, Name, City, Province, PostalCode, "", "", FiscalYear, 2,SupplyDateFrom,SupplyDateTo,iSuppliesGenerated);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
			}
		}
		public void GenerateFieldSupplies( int nFiscal, int campaignID,  string SupplyShipFrom, string SupplyShipTo, string Province, string FMID)
		{
			try
			{
				CampaignDataAccess.GenerateFieldSupplies(nFiscal, campaignID, SupplyShipFrom, SupplyShipTo, Province, FMID);
				//CampaignDataAccess.GenerateFieldSupplies(dataSet, dataSet.CAccount.TableName, FMID, AccountID, CampaignID, Name, City, Province, PostalCode, "", "", FiscalYear, 2,SupplyDateFrom,SupplyDateTo);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
			}
		}
	}
}