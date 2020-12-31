using System;
using System.Data;
using Common;
using Business.Objects;
using Common.TableDef;

namespace Business.Rules
{
	/// <summary>
	/// Validates the deletion of the season
	/// </summary>
	/// <remarks>
	/// Created on 2006-07-04
	/// Created by Madina Saitakhmetova
	/// </remarks>
	public class SeasonValidateDelete : RulesBase
	{
		public SeasonValidateDelete(Message messageManager) : base(messageManager) { }

		/// <summary>
		/// Validates the deletion of a season
		/// </summary>
		/// <param name="row">The row to validate</param>
		/// <returns>allow deletion?</returns>
		public override bool ValidateDelete(DataRow row) 
		{
			//verify only if the row is about to be deleted
			if(row.RowState == DataRowState.Deleted)
			{
				row.RejectChanges();

				bool isValid = true;
				SeasonDataSet.SeasonRow seasonRow = row as SeasonDataSet.SeasonRow;
				Business.Objects.Season season = null;

				if(seasonRow != null && seasonRow.ID != 0) 
				{
					season = new Business.Objects.Season(this.CurrentTransaction);

					if(season.IsSeasonReferenced(seasonRow.ID)) 
					{
						CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_SEASON_REFERENCED, seasonRow.Name));
						isValid = false;
					}
				}
				else
				{
					CurrentMessageManager.Add(Message.VALMSG_GENERAL_VAR_0);
					isValid = false;
				}

				row.Delete();

				return isValid;
			}

			else return true;			
		}
	}
}
