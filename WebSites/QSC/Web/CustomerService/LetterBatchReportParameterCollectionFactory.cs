using System;
using Business.Objects;
using System.Reflection;


namespace QSPFulfillment.CustomerService
{
	/// <summary>
	/// Summary description for LetterBatchReportParameterCollectionFactory.
	/// </summary>
	public class LetterBatchReportParameterCollectionFactory
	{
		private static LetterBatchReportParameterCollectionFactory singletonInstance;

		private LetterBatchReportParameterCollectionFactory() { }

		public static LetterBatchReportParameterCollectionFactory Instance 
		{
			get 
			{
				if(singletonInstance == null) 
				{
					singletonInstance = new LetterBatchReportParameterCollectionFactory();
				}

				return singletonInstance;
			}
		}

		public LetterBatchReportParameterCollection GetLetterBatchReportParameterCollection(string extendedTable, int letterBatchID) 
		{
			return (LetterBatchReportParameterCollection) System.Activator.CreateInstance(null, "QSPFulfillment.CustomerService." + extendedTable + "LetterBatchReportParameterCollection", false, BindingFlags.Default, null, new object[] {letterBatchID}, null, null, null).Unwrap();
		}

		public LetterBatchReportParameterCollection GetLetterBatchReportParameterCollection(string extendedTable, LetterBatchItem letterBatchItem) 
		{
			return (LetterBatchReportParameterCollection) System.Activator.CreateInstance(null, "QSPFulfillment.CustomerService." + extendedTable + "LetterBatchReportParameterCollection", false, BindingFlags.Default, null, new object[] {letterBatchItem}, null, null, null).Unwrap();
		}
	}
}
