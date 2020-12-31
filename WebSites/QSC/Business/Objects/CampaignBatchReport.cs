using System;
using System.Data;
using Business.Reports;
using Common;
using Common.TableDef;
using DAL;
using dataSetRef = Common.TableDef.CampaignDataSet;
using dataAccessRef = DAL.CampaignData;

namespace Business.Objects
{
	/// <summary>
	/// Summary description for MagNetSalesBatchReport.
	/// </summary>
	public class CampaignBatchReport : BatchReportsObject
	{
		dataAccessRef dataAccess = new dataAccessRef();
		dataSetRef dtsDataSet = new dataSetRef();

		private int iID = 0;
		private int iAccountID = 0;
		private string sFMID = String.Empty;
		private DateTime dtStartDate = new DateTime(1995, 1, 1);
		private DateTime dtEndDate = new DateTime(1995, 1, 1);
		private DateTime dtApprovedStatusDateFrom = new DateTime(1995, 1, 1);
		private DateTime dtApprovedStatusDateTo = new DateTime(1995, 1, 1);

		public CampaignBatchReport() : base() { }

		protected override DataSet baseDataSet
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
			Search(iID, iAccountID, sFMID, dtStartDate, dtEndDate, dtApprovedStatusDateFrom, dtApprovedStatusDateTo);
		}

		protected override void InitializeReports()
		{
			Reports = new Business.Reports.Report[2];

			Reports[0] = new Business.Reports.CampaignConfirmationAgreementReport();
			Reports[1] = new Business.Reports.SummaryFormReport();

			MapReportParameters();
		}

		private void MapReportParameters() 
		{
			MapCampaignConfirmationAgreementReportParameters();
			MapSummaryFormReportParameters();
		}

		private void MapCampaignConfirmationAgreementReportParameters() 
		{
			CampaignConfirmationAgreementReport oReport = (CampaignConfirmationAgreementReport) Reports[0];

			oReport.FMIDParameter.FieldAlias = dtsDataSet.Campaign.FMIDColumn.ColumnName;
			oReport.AccountIDParameter.FieldAlias = dtsDataSet.Campaign.ShipToAccountIDColumn.ColumnName;
			oReport.CampaignIDParameter.FieldAlias = dtsDataSet.Campaign.IDColumn.ColumnName;
			oReport.StartDateParameter.FieldAlias = dtsDataSet.Campaign.StartDateColumn.ColumnName;
			oReport.EndDateParameter.FieldAlias = dtsDataSet.Campaign.EndDateColumn.ColumnName;
		}

		private void MapSummaryFormReportParameters() 
		{
			SummaryFormReport oReport = (SummaryFormReport) Reports[1];

			oReport.ICampaignIDParameter.FieldAlias = dtsDataSet.Campaign.IDColumn.ColumnName;
		}

		public virtual byte[] Generate(int ID, int AccountID, string FMID, DateTime StartDate, DateTime EndDate, DateTime ApprovedStatusDateFrom, DateTime ApprovedStatusDateTo) 
		{
			iID = ID;
			iAccountID = AccountID;
			sFMID = FMID;
			dtStartDate = StartDate;
			dtEndDate = EndDate;
			dtApprovedStatusDateFrom = ApprovedStatusDateFrom;
			dtApprovedStatusDateTo = ApprovedStatusDateTo;

			return base.Generate();
		}

		private void Search(int ID, int AccountID, string FMID, DateTime StartDate, DateTime EndDate, DateTime ApprovedStatusDateFrom, DateTime ApprovedStatusDateTo)
		{
			try
			{
				dataAccess.SelectSearchForBatchReports(dtsDataSet, dtsDataSet.Campaign.TableName, ID, AccountID, FMID, StartDate, EndDate, ApprovedStatusDateFrom, ApprovedStatusDateTo, true, true);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
			}
		}
	}
}
