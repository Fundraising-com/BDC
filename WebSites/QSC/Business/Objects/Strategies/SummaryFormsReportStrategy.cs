using System;
using System.Reflection;
using Common.TableDef;
using Business.Objects;

namespace Business.Objects.Strategies
{
	/// <summary>
	/// Summary description for SummaryFormReport.
	/// </summary>
	public abstract class SummaryFormsReportStrategy
	{
		private const string ENGLISH_SUFFIX = "";
		private const string FRENCH_SUFFIX = "_French";

		private string baseReportName = String.Empty;

		public virtual SummaryReports GetSummaryFormsReport(CampaignProgram campaignProgram)
		{
			SummaryReports summaryFormsReport = SummaryReports.None;

			try 
			{
				summaryFormsReport = (SummaryReports) Enum.Parse(typeof(SummaryReports), GetReportName(campaignProgram));
			} 
			catch { }

			return summaryFormsReport;
		}

		public virtual string GetReportName(CampaignProgram campaignProgram)
		{
			string reportName = String.Empty;

			if(baseReportName == String.Empty) 
			{
				baseReportName = GetReflectedReportName();
			}

			reportName = baseReportName;

			if(campaignProgram != null) 
			{
				if(campaignProgram.Campaign.dataSet.Campaign[0].Lang == "EN") 
				{
					reportName += ENGLISH_SUFFIX;
				}
				else if(campaignProgram.Campaign.dataSet.Campaign[0].Lang == "FR") 
				{
					reportName += FRENCH_SUFFIX;
				}
			}

			return reportName;
		}

		private string GetReflectedReportName() 
		{
			string reflectedReportName = String.Empty;

			try 
			{
				MemberInfo strategyInfo = this.GetType();
				SummaryFormsReportAttribute attribute = (SummaryFormsReportAttribute) strategyInfo.GetCustomAttributes(typeof(SummaryFormsReportAttribute), false)[0];
				
				reflectedReportName = attribute.SummaryReport.ToString();
			} 
			catch { }

			return reflectedReportName;
		}

		public abstract bool Validate(CampaignProgram campaignProgram);
	}
}
