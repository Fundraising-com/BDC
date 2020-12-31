using System;
using Common.TableDef;
using System.Reflection;

namespace Business.Objects
{
	/// <summary>
	/// Summary description for LetterBatchSearchCriteriaFactory.
	/// </summary>
	public class LetterBatchSearchCriteriaFactory
	{
		private static LetterBatchSearchCriteriaFactory singletonInstance;

		private LetterBatchSearchCriteriaFactory() { }

		public static LetterBatchSearchCriteriaFactory Instance 
		{
			get 
			{
				if(singletonInstance == null) 
				{
					singletonInstance = new LetterBatchSearchCriteriaFactory();
				}

				return singletonInstance;
			}
		}

		public LetterBatchSearchCriteria GetLetterBatchSearchCriteria(string extendedTable) 
		{
			return (LetterBatchSearchCriteria) System.Activator.CreateInstance(null, "Business.Objects." + extendedTable + "LetterBatchSearchCriteria", false, BindingFlags.Default, null, new object[] {}, null, null, null).Unwrap();
		}
	}
}
