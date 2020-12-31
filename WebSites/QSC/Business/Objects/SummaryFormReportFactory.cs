using System;
using System.Reflection;
using Business.Objects.Strategies;

namespace Business.Objects
{
	/// <summary>
	/// Summary description for CampaignProgramValidationFactory.
	/// </summary>
	/// <remarks>
	///		Instance of the Simple Factory Pattern
	///		Implements the Singleton Pattern
	/// </remarks>
	public class SummaryFormsReportFactory
	{
		private static SummaryFormsReportFactory singletonInstance;
		private SummaryFormsReportStrategyCollection summaryFormsReportStrategyCache;

		private SummaryFormsReportFactory() { }

		public static SummaryFormsReportFactory Instance 
		{
			get 
			{
				if(singletonInstance == null) 
				{
					singletonInstance = new SummaryFormsReportFactory();
				}

				return singletonInstance;
			}
		}

		private SummaryFormsReportStrategyCollection SummaryFormsReportStrategyCache 
		{
			get 
			{
				if(summaryFormsReportStrategyCache == null) 
				{
					FillSummaryFormsReportStrategyCache();
				}

				return summaryFormsReportStrategyCache;
			}
		}

		public SummaryReportsCollection GetSummaryReports(CampaignProgram campaignProgram) 
		{
			SummaryReportsCollection summaryReportsCollection = new SummaryReportsCollection();

			foreach(SummaryFormsReportStrategy strategy in SummaryFormsReportStrategyCache)
			{
				if(strategy.Validate(campaignProgram)) 
				{
					summaryReportsCollection.Add(strategy.GetSummaryFormsReport(campaignProgram));
				}
			}

			return summaryReportsCollection;
		}

		private void FillSummaryFormsReportStrategyCache() 
		{
			SummaryFormsReportStrategy strategy;

			try 
			{
				summaryFormsReportStrategyCache = new SummaryFormsReportStrategyCollection();

				foreach(string reportName in Enum.GetNames(typeof(SummaryReports)))
				{
					if(reportName != "None" && !reportName.EndsWith("_French")) 
					{
						strategy = (SummaryFormsReportStrategy) System.Activator.CreateInstance(null, SummaryFormsReportNamingConvention.Instance.GetSummaryFormsReportStrategyClassName((SummaryReports) Enum.Parse(typeof(SummaryReports), reportName, false)), false, BindingFlags.Default, null, new object[] {}, null, null, null).Unwrap();

						if(strategy != null) 
						{
							summaryFormsReportStrategyCache.Add(strategy);
						} 
						else 
						{
							throw new Exception();
						}
					}
				}
			} 
			catch
			{
				throw new ArgumentException("The naming convention has not been respected and the strategy cannot be loaded.");
			}
		}
	}
}
