using System;
using Common.TableDef;
using System.Reflection;

namespace Business.Objects
{
	/// <summary>
	/// Summary description for LetterTemplateGenerationControlFactory.
	/// </summary>
	public class LetterBatchItemFactory
	{
		private static LetterBatchItemFactory singletonInstance;

		private LetterBatchItemFactory() { }

		public static LetterBatchItemFactory Instance 
		{
			get 
			{
				if(singletonInstance == null) 
				{
					singletonInstance = new LetterBatchItemFactory();
				}

				return singletonInstance;
			}
		}

		public LetterBatchItem GetLetterBatchItem(string extendedTable) 
		{
			return (LetterBatchItem) System.Activator.CreateInstance(null, "Business.Objects." + extendedTable + "LetterBatchItem", false, BindingFlags.Default, null, new object[] {}, null, null, null).Unwrap();
		}

		public LetterBatchItem GetLetterBatchItem(string extendedTable, int id, int letterTemplateID, DateTime dateFrom, DateTime dateTo, int runID, bool isPrinted, DateTime datePrinted, bool isLocked, int userIDCreated, DateTime dateCreated, bool deletedTF) 
		{
			return (LetterBatchItem) System.Activator.CreateInstance(null, "Business.Objects." + extendedTable + "LetterBatchItem", false, BindingFlags.Default, null, new object[] {id, letterTemplateID, dateFrom, dateTo, runID, isPrinted, datePrinted, isLocked, userIDCreated, dateCreated, deletedTF}, null, null, null).Unwrap();
		}

		public LetterBatchItem GetLetterBatchItem(string extendedTable, int letterTemplateID, int userIDCreated) 
		{
			return (LetterBatchItem) System.Activator.CreateInstance(null, "Business.Objects." + extendedTable + "LetterBatchItem", false, BindingFlags.Default, null, new object[] {letterTemplateID, userIDCreated}, null, null, null).Unwrap();
		}

		public LetterBatchItem GetLetterBatchItemFromRow(string extendedTable, LetterBatchDataSet.LetterBatchRow row) 
		{
			return (LetterBatchItem) System.Activator.CreateInstance(null, "Business.Objects." + extendedTable + "LetterBatchItem", false, BindingFlags.Default, null, new object[] {row}, null, null, null).Unwrap();
		}
	}
}
