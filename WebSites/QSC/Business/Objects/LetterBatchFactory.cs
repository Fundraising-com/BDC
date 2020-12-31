using System;
using System.Reflection;

namespace Business.Objects
{
	/// <summary>
	/// Summary description for LetterTemplateGenerationControlFactory.
	/// </summary>
	public class LetterBatchFactory
	{
		private static LetterBatchFactory singletonInstance;

		private LetterBatchFactory() { }

		public static LetterBatchFactory Instance 
		{
			get 
			{
				if(singletonInstance == null) 
				{
					singletonInstance = new LetterBatchFactory();
				}

				return singletonInstance;
			}
		}

		public LetterBatch GetLetterBatch(string extendedTable) 
		{
			return (LetterBatch) System.Activator.CreateInstance(null, "Business.Objects." + extendedTable + "LetterBatch", false, BindingFlags.Default, null, new object[] {}, null, null, null).Unwrap();
		}

		public LetterBatch GetLetterBatch(string extendedTable, Transaction currentTransaction) 
		{
			return (LetterBatch) System.Activator.CreateInstance(null, "Business.Objects." + extendedTable + "LetterBatch", false, BindingFlags.Default, null, new object[] {currentTransaction}, null, null, null).Unwrap();
		}
	}
}
