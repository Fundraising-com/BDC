using System.Collections;

namespace Business.Objects.Strategies
{
	public class SummaryFormsReportStrategyCollection : CollectionBase 
	{
		public void Add(SummaryFormsReportStrategy summaryFormsReportStrategy) 
		{
			this.List.Add(summaryFormsReportStrategy);
		}

		public void Remove(SummaryFormsReportStrategy summaryFormsReportStrategy) 
		{
			this.List.Remove(summaryFormsReportStrategy);
		}

		public SummaryFormsReportStrategy this[int index] 
		{
			get 
			{
				return (SummaryFormsReportStrategy) this.List[index];
			}
		}
	}
}