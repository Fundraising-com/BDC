using System;
using System.ComponentModel;
using Business.Objects;

namespace Business.Objects.Strategies
{
	/// <summary>
	/// Summary description for SummaryFormReportAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class SummaryFormsReportAttribute : Attribute
	{
		private SummaryReports summaryReport;

		public SummaryFormsReportAttribute(SummaryReports summaryReport)
		{
			this.summaryReport = summaryReport;
		}

		public SummaryReports SummaryReport 
		{
			get 
			{
				return summaryReport;
			}
		}
	}
}
