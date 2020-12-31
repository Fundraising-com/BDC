using System;
using Business.Objects;

namespace QSPFulfillment.CustomerService
{
	/// <summary>
	/// Summary description for LetterTemplateGenerationControlFactory.
	/// </summary>
	public class LetterBatchMaintenanceControlFactory
	{
		private static LetterBatchMaintenanceControlFactory singletonInstance;

		private LetterBatchMaintenanceControlFactory() { }

		public static LetterBatchMaintenanceControlFactory Instance 
		{
			get 
			{
				if(singletonInstance == null) 
				{
					singletonInstance = new LetterBatchMaintenanceControlFactory();
				}

				return singletonInstance;
			}
		}

		public string GetLetterBatchMaintenanceControlPath(LetterTemplateItem item) 
		{
			string path = String.Empty;

			if(item == null || item.ExtendedTable == String.Empty)
			{
				path += "DefaultLetterBatchMaintenanceControl.ascx";
			}
			else
			{
				path += item.ExtendedTable + "LetterBatchMaintenanceControl.ascx";
			}


			return path;
		}
	}
}
