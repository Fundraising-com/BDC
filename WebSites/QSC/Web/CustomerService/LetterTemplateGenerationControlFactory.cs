using System;
using Business.Objects;

namespace QSPFulfillment.CustomerService
{
	/// <summary>
	/// Summary description for LetterTemplateGenerationControlFactory.
	/// </summary>
	public class LetterTemplateGenerationControlFactory
	{
		private static LetterTemplateGenerationControlFactory singletonInstance;

		private LetterTemplateGenerationControlFactory() { }

		public static LetterTemplateGenerationControlFactory Instance 
		{
			get 
			{
				if(singletonInstance == null) 
				{
					singletonInstance = new LetterTemplateGenerationControlFactory();
				}

				return singletonInstance;
			}
		}

		public string GetLetterTemplateGenerationControlPath(LetterTemplateItem item) 
		{
			string path = String.Empty;

			if(item.ExtendedTable == String.Empty)
			{
				path += "DefaultLetterTemplateGenerationControl.ascx";
			}
			else
			{
				path += item.ExtendedTable + "LetterTemplateGenerationControl.ascx";
			}

			return path;
		}
	}
}
