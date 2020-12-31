using System;

namespace Business.Objects.Strategies
{
	/// <summary>
	/// Summary description for CampaignProgramValidationNamingConvention.
	/// </summary>
	internal class SummaryFormsReportNamingConvention
	{
		private static SummaryFormsReportNamingConvention singletonInstance;

		private SummaryFormsReportNamingConvention() { }

		internal static SummaryFormsReportNamingConvention Instance 
		{
			get 
			{
				if(singletonInstance == null) 
				{
					singletonInstance = new SummaryFormsReportNamingConvention();
				}

				return singletonInstance;
			}
		}

		internal string GetSummaryFormsReportStrategyClassName(SummaryReports summaryReport) 
		{
			return this.GetType().Namespace + "." + summaryReport.ToString().Replace("_French", String.Empty) + "ReportStrategy";
		}
	}
}
