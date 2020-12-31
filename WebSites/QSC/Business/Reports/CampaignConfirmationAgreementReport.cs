using System;
using Business.ReportService;

namespace Business.Reports
{
	/// <summary>
	/// Summary description for CampaignConfirmationAgreementReport.
	/// </summary>
	public class CampaignConfirmationAgreementReport : Report
	{
		private const string REPORT_NAME = "CampaignConfirmationAgreement";

		public CampaignConfirmationAgreementReport() : base() { }

		protected override string ReportName
		{
			get
			{
				return REPORT_NAME;
			}
		}

		public ParameterFieldReference FMIDParameter 
		{
			get 
			{
				return ReportParameters[0];
			}
		}

		public ParameterFieldReference AccountIDParameter 
		{
			get 
			{
				return ReportParameters[1];
			}
		}

		public ParameterFieldReference CampaignIDParameter 
		{
			get 
			{
				return ReportParameters[2];
			}
		}

		public ParameterFieldReference StartDateParameter 
		{
			get 
			{
				return ReportParameters[3];
			}
		}

		public ParameterFieldReference EndDateParameter
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
			ReportParameters[0].ParameterName = "FMID";
			
			ReportParameters[1] = new ParameterFieldReference();
			ReportParameters[1].ParameterName = "AccountID";

			ReportParameters[2] = new ParameterFieldReference();
			ReportParameters[2].ParameterName = "CampaignID";

			ReportParameters[3] = new ParameterFieldReference();
			ReportParameters[3].ParameterName = "StartDate";

			ReportParameters[4] = new ParameterFieldReference();
			ReportParameters[4].ParameterName = "EndDate";
		}
	}
}
