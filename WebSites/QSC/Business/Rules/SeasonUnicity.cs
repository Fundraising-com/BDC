using System;
using System.Data;
using Common;
using Business.Objects;
using Common.TableDef;

namespace Business.Rules
{
	/// <summary>
	/// Validates Season's unicity : Year+Season must be unique
	/// </summary>
	/// <remarks>
	/// Created on 2006-06-28
	/// Created by Saitakhmetova Madina
	/// </remarks>
	public class SeasonUnicity : RulesBase
	{
		public SeasonUnicity(Message messageManager) : base(messageManager) { }

		/// <summary>
		/// Validates Season Unicity
		/// </summary>
		/// <param name="row">The row to validate</param>
		/// <returns></returns>
		public override bool Validate(DataRow row) 
		{
			bool IsValid = true;
			SeasonDataSet.SeasonRow seasonRow = row as SeasonDataSet.SeasonRow;
			Business.Objects.Season season = null;

			if(seasonRow != null)
			{
				season = new Business.Objects.Season(this.CurrentTransaction);

				if(season.GetSeasonCountByYearSeason(seasonRow.ID, seasonRow.FiscalYear, seasonRow.Season) > 0) 
				{
					CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.VALMSG_UNICITY_VAR_2, new string [] {"The Season", "Fiscal Year and Season Letter"}));
					IsValid = false;
				}
			}

			return IsValid;
		}
	}
}
