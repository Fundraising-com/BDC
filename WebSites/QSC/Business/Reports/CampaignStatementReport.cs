using System;
using Business.ReportService;
using Common.TableDef;

namespace Business.Reports
{
	public class CampaignStatementReport : Report
	{
		private const string REPORT_NAME = "StatementReportByCampaign";
		//private const string REPORT_NAME = "/QSP CA SystemsStatementReportByCampaign";
	    //private const string REPORT_NAME = "StatementReportByCampaign";

		private bool isFrench = false;

		public CampaignStatementReport() : base() { }

		protected override string ReportName
		{
			get
			{
				string reportName = REPORT_NAME;

				if(IsFrench) 
				{
					reportName += "French";
				}

				return reportName;
			}
		}

		public bool IsFrench 
		{
			get 
			{
				return isFrench;
			}
			set 
			{
				isFrench = value;
			}
		}

		public ParameterFieldReference AccountIDParameter 
		{
			get 
			{
				return ReportParameters[0];
			}
		}

		public ParameterFieldReference CampaignIDParameter 
		{
			get 
			{
				return ReportParameters[1];
			}
		}

		public ParameterFieldReference StartDateParameter 
		{
			get 
			{
				return ReportParameters[2];
			}
		}

		public ParameterFieldReference EndDateParameter
		{
			get 
			{
				return ReportParameters[3];
			}
		}

		protected override void InitializeReportParameters()
		{
			ReportParameters = new ParameterFieldReference[4];

			ReportParameters[0] = new ParameterFieldReference();
			ReportParameters[0].ParameterName = "AccountID";

			ReportParameters[1] = new ParameterFieldReference();
			ReportParameters[1].ParameterName = "CampaignID";

			ReportParameters[2] = new ParameterFieldReference();
			ReportParameters[2].ParameterName = "StartDate";

			ReportParameters[3] = new ParameterFieldReference();
			ReportParameters[3].ParameterName = "EndDate";
		}

		public override byte[] Generate(System.Data.DataRow row)
		{
			CampaignDataSet.CampaignRow campaignRow = row as CampaignDataSet.CampaignRow;

			//this.IsFrench = (invoiceRow != null && invoiceRow.Lang == "FR");

			return base.Generate (row);
		}

	}
}
