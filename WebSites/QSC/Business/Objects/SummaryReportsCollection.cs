using System.Collections;

namespace Business.Objects 
{
	public class SummaryReportsCollection : CollectionBase 
	{
		public void Add(SummaryReports summaryReports) 
		{
			this.List.Add(summaryReports);
		}

		public void Remove(SummaryReports summaryReports) 
		{
			this.List.Remove(summaryReports);
		}

		public SummaryReports this[int index] 
		{
			get 
			{
				return (SummaryReports) this.List[index];
			}
		}
	}
}