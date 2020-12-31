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
	/// business layer for SummaryFormReport
	/// </summary>
	/// <remarks>
	/// Created: Madina Saitakhmetova, 09 Agust 2006
	/// </remarks>
	public class SummaryFormBatchReport : BatchReportsObject
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

		public SummaryFormBatchReport() : base() { }

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
			Reports = new Business.Reports.Report[1];

			Reports[0] = new Business.Reports.SummaryFormReport();

			MapReportParameters();
		}

		private void MapReportParameters() 
		{
			MapSummaryFormReportParameters();
		}

		private void MapSummaryFormReportParameters() 
		{
			SummaryFormReport oReport = (SummaryFormReport) Reports[0];

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
				dataAccess.SelectSearchForBatchReports(dtsDataSet, dtsDataSet.Campaign.TableName, ID, AccountID, FMID, StartDate, EndDate, ApprovedStatusDateFrom, ApprovedStatusDateTo, false, false);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
			}
		}
	}
}

