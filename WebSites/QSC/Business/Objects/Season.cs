namespace Business.Objects
{
	using System;
	using System.Data;
	using Common;
	using Common.TableDef;
	using DAL;
	using dataSetRef = Common.TableDef.SeasonDataSet;
	using dataAccessRef = DAL.SeasonData;
	using Business.Rules;

	/// <summary>
	///     This class contains the business rules and wraps the Season DataSet. 
	/// </summary>
	public class Season : BusinessSystem
	{
		private const string DEFAULT_COUNTRY = "CA";

		dataAccessRef dataAccess = new dataAccessRef();
		dataSetRef dtsDataSet;
		DataSet dsSeasonLetters; //contains the season Letters
		DataSet dsLastYearAndRate; //contains default Year and Rate

		#region Constructors

		public Season()
		{
			this.dtsDataSet = new dataSetRef();
			this.dsSeasonLetters = new DataSet();
			this.dsLastYearAndRate = new DataSet();
			CreateRulesCollection();
		}

		public Season(Transaction CurrentTransaction) : this()
		{
			this.CurrentTransaction = CurrentTransaction;
		}

		public Season(bool bGetAll) : this()
		{
			if(bGetAll) 
			{
				GetAll();
			}
		}

		public Season(bool bGetAll, Transaction CurrentTransaction) : this(CurrentTransaction)
		{
			if(bGetAll) 
			{
				GetAll();
			}
		}

		#endregion

		#region Public Properties

		public dataSetRef dataSet
		{
			get 
			{
				return dtsDataSet;
			}
		}

		/// <summary>
		/// gets the dataset containing season letters
		/// </summary>
		public DataSet DSSeasonLetters
		{
			get
			{
				if(dsSeasonLetters.Tables.Count.Equals(0))
				{
					this.GetSeasonLetters();
				}

				return dsSeasonLetters;
			}
		}

		/// <summary>
		/// gets the latest year from the Season table
		/// </summary>
		public int LastYear
		{
			get
			{
				if(dsLastYearAndRate.Tables.Count == 0)
				{
					dataAccess.SelectLastYearAndRate(dsLastYearAndRate, DefaultTableName);
				}
				return Convert.ToInt32(this.dsLastYearAndRate.Tables[0].Rows[0][0]);				
			}
		}

		/// <summary>
		/// gets the latest default conversion rate from the Season table
		/// </summary>
		public decimal LastDefaultRate
		{
			get
			{
				if(dsLastYearAndRate.Tables.Count == 0)
				{
					dataAccess.SelectLastYearAndRate(dsLastYearAndRate, DefaultTableName);
				}
				return Convert.ToDecimal(this.dsLastYearAndRate.Tables[0].Rows[0][1]);
			}
		}

		internal override DataSet baseDataSet
		{
			get 
			{
				return (DataSet) dtsDataSet;
			}
		}

		public override string DefaultTableName 
		{
			get 
			{
				return dataSet.Season.TableName;
			}
		}

		protected override DBTableOperation DataAccessReference
		{
			get
			{
				return dataAccess;
			}
		}

		#endregion

		#region Public Methods

		public void GetAll() 
		{
			try 
			{
				dataAccess.SelectAll(dataSet, DefaultTableName);
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}
		
		public void GetAllFiscalYears()
		{
			try
			{
				dataSet.Season.Constraints.Clear();
				dataSet.Season.IDColumn.Unique = false;

				dataAccess.SelectAllFiscalYears(dataSet, DefaultTableName);
			}
			catch(Exception ex)
			{	
				ManageError(ex);
			}
		}

		public void GetOneByDate(DateTime date) 
		{
			try 
			{
				dataAccess.SelectOneByDate(dataSet, DefaultTableName, date);
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}

		public dataSetRef.SeasonRow FindSeason(DateTime dSearch) 
		{
			dataSetRef.SeasonRow rowSeason = null;
			
			foreach(dataSetRef.SeasonRow row in dataSet.Season) 
			{
				if(row.Season != "Y" && dSearch >= row.StartDate && dSearch <= row.EndDate) 
				{
					rowSeason = row;
				}
			}

			return rowSeason;
		}

		public dataSetRef.SeasonRow FindYear(DateTime dSearch) 
		{
			dataSetRef.SeasonRow rowSeason = null;
			
			foreach(dataSetRef.SeasonRow row in dataSet.Season) 
			{
				if(row.Season == "Y" && dSearch >= row.StartDate && dSearch <= row.EndDate) 
				{
					rowSeason = row;
				}
			}

			return rowSeason;
		}

		/// <summary>
		/// verify if season can be deleted before showing the delete confirmation
		/// </summary>
		/// <returns>delete allowed?</returns>
		public bool VerifyDelete()
		{
			return this.ValidateDelete();
		}

		/// <summary>
		/// delete the Season, given season ID
		/// </summary>
		/// <param name="seasonID">ID of the season to be deleted</param>
		public void Delete(int seasonID)
		{
			dataAccess.SelectOne(dtsDataSet, DefaultTableName, seasonID);
			dtsDataSet.Season.Rows[0].Delete();
			base.Delete();
		}

		/// <summary>
		/// get one Season entry
		/// </summary>
		/// <param name="ID">Season ID</param>
		public void GetOneByID(int ID) 
		{
			try 
			{
				dtsDataSet.Clear();
				dataAccess.SelectOne(dtsDataSet, DefaultTableName, ID);
			} 
			catch(Exception ex)
			{
				ManageError(ex);
			}
		}

		/// <summary>
		/// get the list of season letters
		/// </summary>
		public void GetSeasonLetters()
		{
			dataAccess.SelectSeasonLetters(dsSeasonLetters, DefaultTableName);
		}

		/// <summary>
		/// get the count of seasons that have given year and season in
		/// the Season table
		/// </summary>
		public int GetSeasonCountByYearSeason(int id, int fiscalYear, string season)
		{
			return dataAccess.SelectCountByYearSeason(id, fiscalYear, season);
		}

		/// <summary>
		/// update or insert an entry in the Season table
		/// </summary>
		/// <param name="row">row to be processed</param>
		public void Save(dataSetRef.SeasonRow row)
		{
			//update
			if(row.ID > 0)
			{
				base.Update();
			}
				//insert
			else
			{
				dataAccess.SelectOne(dtsDataSet, DefaultTableName, row.ID);
				row.Country = DEFAULT_COUNTRY;
				DateTime [] dtStartEndDates = GetStartEndDates(row.Season, row.FiscalYear);
				row.StartDate = dtStartEndDates[0];
				row.EndDate = dtStartEndDates[1];
				dtsDataSet.Season.AddSeasonRow(row);
				base.Insert();
				dtsDataSet.Clear();
			}
		}

		/// <summary>
		/// insert three seasons for next fiscal year (fall, spring, fiscal year)
		/// </summary>
		/// <param name="UserID">user initiating the creation of a new fiscal period</param>
		public void InsertNewFiscalYear(int UserID)
		{
			foreach(DataRow dr in this.DSSeasonLetters.Tables[0].Rows)
			{
				SeasonDataSet.SeasonRow row = this.dataSet.Season.NewSeasonRow();
				row.ID = 0;
				row.Name = GetSeasonName(dr["Season"].ToString(), this.LastYear + 1);
				row.FiscalYear = this.LastYear + 1;
				row.Season = dr["Season"].ToString();
				row.DefaultConversionRate = this.LastDefaultRate;
				row.UserIDChanged = UserID;
				this.Save(row);
			}
		}

		/// <summary>
		/// check if given season is referenced in the database
		/// </summary>
		/// <param name="seasonID">Season ID to check</param>
		/// <returns>referenced?</returns>
		public bool IsSeasonReferenced(int seasonID)
		{
			return !dataAccess.IsSeasonRefrenced(seasonID).Equals(0);
		}

		#endregion

		#region Private Methods
		
		/// <summary>
		/// derive start and end dates given year and season letter
		/// </summary>
		/// <param name="seasonLetter">season letter</param>
		/// <param name="year">season year</param>
		/// <returns>start and end dates</returns>
		private DateTime [] GetStartEndDates(string seasonLetter, int year)
		{
			DateTime [] dtStartEndDates = new DateTime [2];
			switch(seasonLetter)
			{
				case "f": 
				case "F":
					dtStartEndDates[0] = new DateTime(year-1, 7, 1);
					dtStartEndDates[1] = new DateTime(year-1, 12, 31);
					break;

				case "s":
				case "S":
					dtStartEndDates[0] = new DateTime(year, 1, 1);
					dtStartEndDates[1] = new DateTime(year, 6, 30);
					break;

				case "y":
				case "Y":
					dtStartEndDates[0] = new DateTime(year-1, 7, 1);
					dtStartEndDates[1] = new DateTime(year, 6, 30);
					break;

				default:
					CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_SEASON_INVALID_LETTER, seasonLetter));
					CurrentMessageManager.PrepareErrorMessage();
					throw new MessageException(CurrentMessageManager);
			}

			return dtStartEndDates;
		}
	

		/// <summary>
		/// Used when inserting new fiscal year (all three seasons at once)
		/// to get the Season names for each season
		/// </summary>
		/// <param name="season">season letter</param>
		/// <param name="year">year</param>
		/// <returns>Season Name</returns>
		private string GetSeasonName(string season, int year)
		{
			switch(season)
			{
				case "f": 
				case "F":
					return "Fall " + (year - 1).ToString();

				case "s":
				case "S":
					return "Spring " + year.ToString();

				case "y":
				case "Y":
					return "Fiscal Year " + year.ToString();

				default:
					CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_SEASON_INVALID_LETTER, season));
					CurrentMessageManager.PrepareErrorMessage();
					throw new MessageException(CurrentMessageManager);
			}
		}

		#endregion
	}
}