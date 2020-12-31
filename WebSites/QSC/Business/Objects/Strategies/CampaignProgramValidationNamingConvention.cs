using System;

namespace Business.Objects.Strategies
{
	/// <summary>
	/// Summary description for CampaignProgramValidationNamingConvention.
	/// </summary>
	internal class CampaignProgramValidationNamingConvention
	{
		private static CampaignProgramValidationNamingConvention singletonInstance;

		private CampaignProgramValidationNamingConvention() { }

		internal static CampaignProgramValidationNamingConvention Instance 
		{
			get 
			{
				if(singletonInstance == null) 
				{
					singletonInstance = new CampaignProgramValidationNamingConvention();
				}

				return singletonInstance;
			}
		}

		internal string GetValidationStrategyClassName(CurrentPrograms program) 
		{
			return this.GetType().Namespace + "." + program.ToString() + "ValidationStrategy";
		}
	}
}
