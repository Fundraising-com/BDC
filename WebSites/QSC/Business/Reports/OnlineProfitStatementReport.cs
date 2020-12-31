using System;
using Business.ReportService;
using Common.TableDef;

namespace Business.Reports
{
	/// <summary>
	/// Summary description for CampaignConfirmationAgreementReport.
	/// </summary>
	public class OnlineProfitStatementReport : Report
	{
		private const string REPORT_NAME = "OnlineProgramProfitStatement";
		//private const string REPORT_NAME = "/QSP CA SystemsOnlineProgramProfitStatement";
	  //private const string REPORT_NAME = "OnlineProgramProfitStatement";

		private bool isFrench = false;

		public OnlineProfitStatementReport() : base() { }

		protected override string ReportName
		{
			get
			{
				string reportName = REPORT_NAME;

				//if(IsFrench) 
				//{
				//	reportName += "French";
				//}

				return reportName;
			}
		}

		/*public bool IsFrench 
		{
			get 
			{
				return isFrench;
			}
			set 
			{
				isFrench = value;
			}
		}*/

		public ParameterFieldReference CampaignIDParameter 
		{
			get 
			{
				return ReportParameters[0];
			}
		}

		public ParameterFieldReference Over100Parameter 
		{
			get 
			{
				return ReportParameters[1];
			}
		}

		public ParameterFieldReference DateFromParameter
		{
			get 
			{
				return ReportParameters[2];
			}
		}


		public ParameterFieldReference DateToParameter
		{
			get 
			{
				return ReportParameters[3];
			}
		}

		public ParameterFieldReference FMIDParameter
		{
			get 
			{
				return ReportParameters[4];
			}
		}


		protected override void InitializeReportParameters()
		{
			ReportParameters = new ParameterFieldReference[5];

			ReportParameters[0] = new ParameterFieldReference();
			ReportParameters[0].ParameterName = "CampaignID";

			ReportParameters[1] = new ParameterFieldReference();
			ReportParameters[1].ParameterName = "Over100";

			ReportParameters[2] = new ParameterFieldReference();
			ReportParameters[2].ParameterName = "DateFrom";

			ReportParameters[3] = new ParameterFieldReference();
			ReportParameters[3].ParameterName = "DateTo";

			ReportParameters[4] = new ParameterFieldReference();
			ReportParameters[4].ParameterName = "FMID";
		}

		public override byte[] Generate(System.Data.DataRow row)
		{
			CampaignDataSet.CampaignRow campaignRow = row as CampaignDataSet.CampaignRow;

			//this.IsFrench = (invoiceRow != null && invoiceRow.Lang == "FR");

			return base.Generate (row);
		}

	}
}
