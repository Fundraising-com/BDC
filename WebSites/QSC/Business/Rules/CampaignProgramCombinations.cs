using System;
using System.Data;
using Common;
using Business.Objects;
using Common.TableDef;

namespace Business.Rules
{
	/// <summary>
	/// Validates required fields
	/// </summary>
	public class CampaignProgramCombinations : RulesBase
	{
		public CampaignProgramCombinations(Message messageManager) : base(messageManager) { }

		/// <summary>
		/// Validates required fields
		/// </summary>
		/// <param name="row">The row to validate</param>
		/// <returns></returns>
		public override bool Validate(BusinessSystem entity, DataRowState state) 
		{
			bool bIsValid = false;
			CampaignProgramDataSet dataSet;
			Business.Objects.CampaignProgram campaignProgram = entity as Business.Objects.CampaignProgram;
			CurrentPrograms runningProgram;

			if(campaignProgram != null) 
			{
				dataSet = entity.baseDataSet.GetChanges(DataRowState.Added | DataRowState.Modified | DataRowState.Unchanged) as CampaignProgramDataSet;
			
				if(dataSet != null) 
				{
					if(dataSet.CampaignProgram.Count > 0) 
					{
						bIsValid = true;

						foreach(CampaignProgramDataSet.CampaignProgramRow row in dataSet.CampaignProgram.Rows) 
						{
							runningProgram = (CurrentPrograms) Enum.ToObject(typeof(CurrentPrograms), row.ProgramRow.ID);

                     if (!row.OnlineOnly)
   							bIsValid &= CampaignProgramValidationFactory.Instance.GetCampaignProgramValidationStrategy(runningProgram, CurrentMessageManager).Validate(campaignProgram, dataSet);
						}
					} 
					else 
					{
						CurrentMessageManager.Add(Message.ERRMSG_PROGRAM_AT_LEAST_ONE_0);
					}
				}
			}

			return bIsValid;
		}
	}
}
