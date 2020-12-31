using System;
using System.Data;
using Business.Reports;
using Common;
using Common.TableDef;
using DAL;
using dataSetRef = Common.TableDef.CampaignDataSet;
using dataAccessRef = DAL.StatementData;


namespace Business.Objects
{
	public class StatementBatchReport : BatchReportsObject
	{
		dataAccessRef dataAccess = new dataAccessRef();
		dataSetRef dtsDataSet = new dataSetRef();

		private string accountName = String.Empty;
		private int accountID = 0;
		private int campaignID = 0;
		private DateTime fromDate = new DateTime(1995, 1, 1);
		private DateTime toDate = new DateTime(1995, 1, 1);
		private string fmID =String.Empty;
		private int over100=0;
		

		public StatementBatchReport() : base() { }

		protected override DataSet baseDataSet
		{
			get
			{
				return dtsDataSet;
			}
		}

		public dataSetRef dataSet 
		{
			get 
			{
				return dtsDataSet;
			}
		}

		internal override string DefaultTableName
		{
			get
			{
				return dtsDataSet.Campaign.TableName;
			}
		}

		protected override DBTableOperation DataAccessReference
		{
			get
			{
				return dataAccess;
			}
		}

		protected override string FileNameExpression
		{
			get
			{
				return dtsDataSet.Campaign.IDColumn.ColumnName;
			}
		}

		protected override void GetDataList()
		{
			Search(accountID,campaignID,fromDate, toDate);//,over100);
		}

		protected override void InitializeReports()
		{
			Reports = new Business.Reports.Report[3];

			Reports[0] = new Business.Reports.CampaignStatementReport();
			Reports[1] = new Business.Reports.OnlineProfitStatementReport();
			Reports[2] = new Business.Reports.CustomerServiceStatementReport();

			MapReportParameters();
		}

		private void MapReportParameters() 
		{
			MapCampaignStatementReportParameters();
			MapOnlineStatementReportParameters() ;
			MapCustomerServiceStatementReportParameters();
		}

		private void MapCampaignStatementReportParameters() 
		{
			CampaignStatementReport report = (CampaignStatementReport) Reports[0];

			report.AccountIDParameter.FieldAlias = dtsDataSet.Campaign.BillToAccountIDColumn.ColumnName;
			report.CampaignIDParameter.FieldAlias = dtsDataSet.Campaign.IDColumn.ColumnName;
			report.StartDateParameter.FieldAlias= dtsDataSet.Campaign.StartDateColumn.ColumnName;
            report.EndDateParameter.FieldAlias=dtsDataSet.Campaign.EndDateColumn.ColumnName;

		}
		private void MapOnlineStatementReportParameters() 
		{
			OnlineProfitStatementReport report = (OnlineProfitStatementReport) Reports[1];

			report.CampaignIDParameter.FieldAlias = dtsDataSet.Campaign.IDColumn.ColumnName;
			report.Over100Parameter.FieldAlias= dtsDataSet.Campaign.StaffOrderDiscountColumn.ColumnName; //CA DataSet does not have this param
			report.DateFromParameter.FieldAlias=dtsDataSet.Campaign.StartDateColumn.ColumnName;
			report.DateToParameter.FieldAlias=dtsDataSet.Campaign.EndDateColumn.ColumnName;
			report.FMIDParameter.FieldAlias=dtsDataSet.Campaign.FMIDColumn.ColumnName;

		}

		private void MapCustomerServiceStatementReportParameters() 
		{
			
			CustomerServiceStatementReport oReport = (CustomerServiceStatementReport) Reports[2];

			oReport.CampaignIDParameter.FieldAlias = dtsDataSet.Campaign.IDColumn.ColumnName;
			oReport.Over100Parameter.FieldAlias=dtsDataSet.Campaign.StaffOrderDiscountColumn.ColumnName; 
			oReport.DateFromParameter.FieldAlias=dtsDataSet.Campaign.StartDateColumn.ColumnName;
			oReport.DateToParameter.FieldAlias=dtsDataSet.Campaign.EndDateColumn.ColumnName;
			oReport.FMIDParameter.FieldAlias=dtsDataSet.Campaign.FMIDColumn.ColumnName;
		}


		public virtual byte[] Generate(int accountID, int campaignID, DateTime fromDate, DateTime toDate, int over100) 
		{
			this.accountID=accountID;
			this.campaignID = campaignID;
			this.fromDate = fromDate;
			this.toDate = toDate;
			this.over100= over100;

			return base.Generate();
		}

		
		public void Search(int accountID, int campaignID, DateTime fromDate, DateTime toDate)//, int over100)
		{
			try
			{
				dataAccess.SelectCAStatementToPrint(dtsDataSet, DefaultTableName, accountID,campaignID,fromDate, toDate);//,over100 );
			}
			catch (Exception ex)
			{	
				ManageError(ex);
			}
		}
		
	}
}
