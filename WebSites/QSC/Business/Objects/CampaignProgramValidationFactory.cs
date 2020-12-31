using System;
using System.Reflection;
using Common;
using Business.Objects.Strategies;

namespace Business.Objects
{
	/// <summary>
	/// Summary description for CampaignProgramValidationFactory.
	/// </summary>
	/// <remarks>
	///		Instance of the Simple Factory Pattern
	///		Implements the Singleton Pattern
	/// </remarks>
	internal class CampaignProgramValidationFactory
	{
		private static CampaignProgramValidationFactory singletonInstance;

		private CampaignProgramValidationFactory() { }

		internal static CampaignProgramValidationFactory Instance 
		{
			get 
			{
				if(singletonInstance == null) 
				{
					singletonInstance = new CampaignProgramValidationFactory();
				}

				return singletonInstance;
			}
		}

		internal CampaignProgramValidationStrategy GetCampaignProgramValidationStrategy(CurrentPrograms program, Message messageManager) 
		{
			CampaignProgramValidationStrategy strategy;

			try 
			{
				strategy = (CampaignProgramValidationStrategy) System.Activator.CreateInstance(null, CampaignProgramValidationNamingConvention.Instance.GetValidationStrategyClassName(program), false, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, new object[] {messageManager}, null, null, null).Unwrap();

				if(strategy == null) 
				{
					throw new Exception();
				}
			} 
			catch
			{
				throw new ArgumentException("The naming convention has not been respected and the strategy cannot be loaded.");
			}

			return strategy;
		}
	}
}
