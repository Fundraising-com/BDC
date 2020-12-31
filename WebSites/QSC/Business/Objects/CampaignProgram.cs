namespace Business.Objects
{
	using System;
	using System.Data;
	using Common;
	using Common.TableDef;
	using DAL;
	using dataSetRef = Common.TableDef.CampaignProgramDataSet;
	using CampaignProgramDataAccessRef = DAL.CampaignProgramData;
	using ProgramDataAccessRef = DAL.ProgramData;

	public enum SummaryReports 
	{
		None,
		/*MagSummary,
		MagSummary_French,
		MagExpressSummary,
		MagExpressSummary_French,
		MagnetSummary,
		MagnetSummary_French,
		StaffSummary,
		StaffSummary_French,
		GiftSummary,
		GiftSummary_French,
        CookieDoughSummary,
		CookieDoughSummary_French,
		CooksSummary,
		CooksSummary_French,
		LoonieMagSummary,
		LoonieMagSummary_French,
		PlannerOnlySummary,
		PlannerOnlySummary_French,
		PlannerComboSummary,
		PlannerComboSummary_French,
		AllOccasionSummary,
		AllOccasionSummary_French,
		AllOccasionComboSummary,
		AllOccasionComboSummary_French,*/
        ComboSummary,
        ComboSummary_French
	}

	public enum ReportedPrograms 
	{
		None = 0,
		Magazine = 1,
		MagazineExpress = 2,
		Gift = 4,
		GiftProgramOnly = 19,
		SweetSensations = 20,
		Loonie = 24,
		PlannerProgramOnly = 26,
		PlannerProgramCombo = 27,
		AllOccasion=32,
        MagazineFullService = 47
	}

	public enum CurrentPrograms 
	{
		None = 0,
		Magazine = 1,
		MagazineExpress = 2,
		Magnet = 3,
		Gift = 4,
		DrawPrize = 9,
		PlanetaryRewardsProgram = 11,
		KanataExtremeRewardsProgram = 12,
		Dollars20GiftCardCoupon = 17,
		DiscoverCanadaProgram = 18,
		GiftOnlyProgram = 19,
		SweetSensations = 20,
		CooksCollection = 21,
		TreasureQuest = 22,
        LargeChart = 23,
		Loonie = 24,
		Online = 25,
		PlannerOnlyProgram = 26,
		PlannerComboProgram = 27,
		PrizeSafari = 29,
		BearProgram=30,
		PrizeDimension=31,
		AllOccasion=32,
        GoForTheGold=33,
        GameOn=34,
        FreeSubs=35,
        LoyaltyBonus=36,
        LargeChartWithNumSubs=39,
        PrizeTime=40,
        PrizeZone=41,
        Cumulative=42,
        Hybrid=43,
        CookieDough=44,
        Chocolate=45,
        PrizeC=46,
        MagazineFullService=47,
        CumulativeMiddleSchool=48,
        Candles=49,
        ToRememberThis=50,
        _59MinuteFundraiser = 51,
        Entertainment = 52,
        GiftsWeLove = 53,
        Festival = 54,
        Bloom = 55,
        KitchenCollection = 56,
        Donations = 57,
        NaturallyGood = 58,
        LifeIsSweet = 59,
        Top20Magazines = 60,
        FFTTPopcorn = 61,
        StainlessSteelTravelCup=62,
        DepositOnlyExtraService = 63,
        QSPSavingsPass = 64,
        GiftCard = 65,
        PapaJackPopcorn = 66,
        Tervis = 67,
        PretzelRods40 = 68,
        TheCureJewelry = 69,
        TastyTreats = 70,
        PretzelRods30 =71,
        LeapLabels = 72,
        CoolCards = 73,
        Rally = 74
   }

   /// <summary>
   ///     This class contains the business rules. 
   /// </summary>
   public class CampaignProgram : BusinessSystem
	{
		private const int MAGAZINE_PROGRAM = 1;

		CampaignProgramDataAccessRef CampaignProgramDataAccess = new CampaignProgramDataAccessRef();
		ProgramDataAccessRef ProgramDataAccess = new ProgramDataAccessRef();
		dataSetRef dtsDataSet;

		int iCampaignID = 0;
		private Campaign campaign;

		public CampaignProgram() : base()
		{
			this.dtsDataSet = new dataSetRef();
			CreateRulesCollection();
		}

		public CampaignProgram(Message messageManager) : base(messageManager) 
		{
			this.dtsDataSet = new dataSetRef();
			CreateRulesCollection();
		}

		public CampaignProgram(Transaction currentTransaction) : this() 
		{
			this.CurrentTransaction = currentTransaction;
		}

		public CampaignProgram(Message messageManager, Transaction currentTransaction) : this(messageManager)
		{
			this.CurrentTransaction = currentTransaction;
		}

		public CampaignProgram(bool getAllPrograms) : this()
		{
			if(getAllPrograms) 
			{
				GetAllPrograms();
			}
		}

		public CampaignProgram(bool getAllPrograms, bool isSelectAll) : this()
		{
			if(getAllPrograms) 
			{
				GetAllPrograms(isSelectAll);
			}
		}

		public CampaignProgram(bool getAllPrograms, Message messageManager) : this(messageManager) 
		{
			if(getAllPrograms) 
			{
				GetAllPrograms();
			}
		}

		public CampaignProgram(bool getAllPrograms, Transaction currentTransaction) : this(currentTransaction)
		{
			if(getAllPrograms) 
			{
				GetAllPrograms();
			}
		}

		public CampaignProgram(bool getAllPrograms, Message messageManager, Transaction currentTransaction) : this(messageManager, currentTransaction) 
		{
			if(getAllPrograms) 
			{
				GetAllPrograms();
			}
		}

		public CampaignProgram(int campaignID) : this(true) 
		{
			iCampaignID = campaignID;

			GetAllByCampaignID(campaignID);
		}

		public CampaignProgram(int campaignID, Message messageManager) : this(true, messageManager) 
		{
			iCampaignID = campaignID;

			GetAllByCampaignID(campaignID);
		}

		public CampaignProgram(int campaignID, Transaction currentTransaction) : this(true, currentTransaction) 
		{
			iCampaignID = campaignID;

			GetAllByCampaignID(campaignID);
		}

		public CampaignProgram(int campaignID, Message messageManager, Transaction currentTransaction) : this(true, messageManager, currentTransaction) 
		{
			iCampaignID = campaignID;

			GetAllByCampaignID(campaignID);
		}

		public dataSetRef dataSet 
		{
			get 
			{
				return this.dtsDataSet;
			}
		}

		internal override DataSet baseDataSet
		{
			get
			{
				return (DataSet) this.dtsDataSet;
			}
		}

		public override string DefaultTableName
		{
			get
			{
				return this.dtsDataSet.CampaignProgram.TableName;
			}
		}

		protected override DBTableOperation DataAccessReference
		{
			get
			{
				return CampaignProgramDataAccess;
			}
		}

		public Campaign Campaign 
		{
			get 
			{
				if(campaign == null && iCampaignID != 0) 
				{
					campaign = new Campaign(iCampaignID, this.CurrentTransaction);
				}

				return campaign;
			}
		}

		public bool Save(int campaignID)
		{			
			this.iCampaignID = campaignID;

			bool isValid = base.UpdateBatch();

			if(CurrentMessageManager.Count > 0) 
			{
				CurrentMessageManager.PrepareErrorMessage();
				throw new MessageException(CurrentMessageManager);
			}

			return isValid;
		}

		public bool SaveWithoutValidation(int campaignID) 
		{
			try 
			{
				this.iCampaignID = campaignID;

				return (Convert.ToBoolean(DataAccessReference.UpdateBatch(baseDataSet, DefaultTableName)));
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
				return false;
			}
		}

		public bool Validate() 
		{
			return ValidateUpdateBatch();
		}

		public bool RunsProgram(CurrentPrograms program)
		{
			DataView view = new DataView(this.dataSet.CampaignProgram);

         view.RowFilter = "ProgramID = " + ((int)program).ToString() + " AND OnlineOnly = 0";

			return view.Count != 0;
		}

      public bool RunsProgramIncludingOnlineAccountDeliveredOnly(CurrentPrograms program)
      {
         DataView view = new DataView(this.dataSet.CampaignProgram);

         view.RowFilter = "ProgramID = " + ((int)program).ToString();

         return view.Count != 0;
      }

      public bool BlackboardPacket()
        {
            DataView view = new DataView(this.dataSet.CampaignProgram);

            view.RowFilter = "BlackboardPacket = 1";

            return view.Count != 0;
        }

        public bool OnlineOnly(CurrentPrograms program)
        {
           DataView view = new DataView(this.dataSet.CampaignProgram);

           view.RowFilter = "ProgramID = " + ((int)program).ToString() + " AND OnlineOnly = 1";

           return view.Count != 0;
        }

        public bool FieldSupplyPacket(CurrentPrograms program)
        {
            DataView view = new DataView(this.dataSet.CampaignProgram);

            view.RowFilter = "ProgramID = " + ((int)program).ToString() + " AND FieldSupplyPacket = 1";

            return view.Count != 0;
        }

        public double GroupProfit(CurrentPrograms program)
        {
           DataView view = new DataView(this.dataSet.CampaignProgram);

           view.RowFilter = "ProgramID = " + ((int)program).ToString();

           return Convert.ToDouble(view[0][3]);
        }
        protected override bool ValidateInsert()
		{
			if(!base.ValidateInsert()) 
			{
				if(this.Campaign.dataSet.Campaign.Count != 0) 
				{
					if(this.Campaign.dataSet.Campaign[0].Status != Convert.ToInt32(CampaignStatus.Cancel) && this.Campaign.dataSet.Campaign[0].Status != Convert.ToInt32(CampaignStatus.Inactive)) 
					{
						this.Campaign.dataSet.Campaign[0].ApprovedStatusDate = Convert.ToDateTime(this.Campaign.dataSet.Campaign.ApprovedStatusDateColumn.NullValue);
						this.Campaign.dataSet.Campaign[0].Status = Convert.ToInt32(CampaignStatus.PendingIncomplete);
					}

					this.Campaign.SaveWithoutValidation();
				}
			}

			return true;
		}

		protected override bool ValidateUpdate()
		{
			bool isValid = base.ValidateUpdate();

			if(!isValid) 
			{
				if(this.Campaign.dataSet.Campaign.Count != 0) 
				{
					if(this.Campaign.dataSet.Campaign[0].Status != Convert.ToInt32(CampaignStatus.Cancel) && this.Campaign.dataSet.Campaign[0].Status != Convert.ToInt32(CampaignStatus.Inactive)) 
					{
						this.Campaign.dataSet.Campaign[0].ApprovedStatusDate = Convert.ToDateTime(this.Campaign.dataSet.Campaign.ApprovedStatusDateColumn.NullValue);
						this.Campaign.dataSet.Campaign[0].Status = Convert.ToInt32(CampaignStatus.PendingIncomplete);
					}

					this.Campaign.SaveWithoutValidation();
				}
			}

			return isValid;
		}

		protected override bool ValidateUpdateBatch()
		{
			bool isValid = true;

			try 
			{
				isValid = base.ValidateUpdateBatch();
			}
			catch(Exception ex) 
			{
				if(ex is MessageException) 
				{
					if(this.Campaign.dataSet.Campaign.Count != 0) 
					{
						if(this.Campaign.dataSet.Campaign[0].Status != Convert.ToInt32(CampaignStatus.Cancel) && this.Campaign.dataSet.Campaign[0].Status != Convert.ToInt32(CampaignStatus.Inactive)) 
						{
							this.Campaign.dataSet.Campaign[0].ApprovedStatusDate = Convert.ToDateTime(this.Campaign.dataSet.Campaign.ApprovedStatusDateColumn.NullValue);
							this.Campaign.dataSet.Campaign[0].Status = Convert.ToInt32(CampaignStatus.PendingIncomplete);
						}

						this.Campaign.SaveWithoutValidation();
					}
				} 
				else 
				{
					ManageError(ex);
					return false;
				}
			}

			return true;
		}

		public void GetAllPrograms()
		{
			try
			{
				ProgramDataAccess.SelectAll(dtsDataSet, this.dtsDataSet.Program.TableName);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
			}
		}

		public void GetAllPrograms(bool isSelectAll)
		{
			try
			{
				ProgramDataAccess.SelectAllActiveAndNonActive(dtsDataSet, this.dtsDataSet.Program.TableName);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
			}
		}

		public void GetAllByCampaignID(int CampaignID) 
		{
			try 
			{
				CampaignProgramDataAccess.SelectAllByCampaignID(dtsDataSet, DefaultTableName, CampaignID);
			} 
			catch (Exception ex) 
			{
				ManageError(ex);
			}
		}

		public bool Clone(int CampaignIDFrom, int CampaignIDTo) 
		{
			bool bIsSuccess = true;
			DataView dv;
			dataSetRef.CampaignProgramRow rowFrom;
			dataSetRef.CampaignProgramRow rowTo;

			try 
			{
				dv = new DataView(this.dataSet.CampaignProgram, "CampaignID = " + CampaignIDFrom.ToString(), "", DataViewRowState.CurrentRows);

				foreach(DataRowView dvRow in dv) 
				{
					rowFrom = (dataSetRef.CampaignProgramRow) dvRow.Row;
					rowTo = this.dataSet.CampaignProgram.NewCampaignProgramRow();

					rowTo.CampaignID = CampaignIDTo;
					CopyCampaignProgramRow(rowFrom, rowTo);

					this.dataSet.CampaignProgram.AddCampaignProgramRow(rowTo);

					bIsSuccess &= base.Insert();
				}
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}

			return bIsSuccess;
		}

		private void CopyCampaignProgramRow(dataSetRef.CampaignProgramRow rowFrom, dataSetRef.CampaignProgramRow rowTo) 
		{
			rowTo.ProgramID = rowFrom.ProgramID;
			rowTo.IsPreCollect = rowFrom.IsPreCollect;
			rowTo.GroupProfit = rowFrom.GroupProfit;
			rowTo.DeletedTF = rowFrom.DeletedTF;
         rowTo.IsPersonalize = rowFrom.IsPersonalize;
         rowTo.BlackboardPacket = rowFrom.BlackboardPacket;
         rowTo.FieldSupplyPacket = rowFrom.FieldSupplyPacket;
         rowTo.OnlineOnly = rowFrom.OnlineOnly;
         rowTo.AllowOnlineAccountDelivery = rowFrom.AllowOnlineAccountDelivery;

      }

		/*public SummaryReports GetSummaryReport()
		{
			SummaryReports oSummaryReport = SummaryReports.None;
			Campaign oCampaign;

			if(this.dataSet.CampaignProgram.Rows.Count != 0) 
			{
				oCampaign = new Campaign();
				if(this.CurrentTransaction != null) 
				{
					oCampaign.CurrentTransaction = this.CurrentTransaction;
				}

				oCampaign.GetOneByID(this.dataSet.CampaignProgram[0].CampaignID);

				if(RunsProgram(CurrentPrograms.Magazine)) 
				{
					if(!RunsProgram(CurrentPrograms.MagazineExpress) && !RunsProgram(CurrentPrograms.Gift) && !RunsProgram(CurrentPrograms.SweetSensations)) 
					{
						if(!oCampaign.dataSet.Campaign[0].IsStaffOrder) 
						{
							if(oCampaign.dataSet.Campaign[0].Lang == "EN") 
							{
								oSummaryReport = SummaryReports.MagSummary;
							} 
							else if(oCampaign.dataSet.Campaign[0].Lang == "FR")
							{
								oSummaryReport = SummaryReports.MagSummary_French;
							}
						} 
						else 
						{
							if(oCampaign.dataSet.Campaign[0].Lang == "EN") 
							{
								oSummaryReport = SummaryReports.StaffSummary;
							} 
							else if(oCampaign.dataSet.Campaign[0].Lang == "FR")
							{
								oSummaryReport = SummaryReports.StaffSummary_French;
							}
						}
					} 
					else 
					{
						if(!oCampaign.dataSet.Campaign[0].IsStaffOrder) 
						{
							if(oCampaign.dataSet.Campaign[0].Lang == "EN") 
							{
								oSummaryReport = SummaryReports.MagComboSummary;
							} 
							else if(oCampaign.dataSet.Campaign[0].Lang == "FR")
							{
								oSummaryReport = SummaryReports.MagComboSummary_French;
							}
						}
					}
				} 
				else if(!oCampaign.dataSet.Campaign[0].IsStaffOrder)
				{
					if(RunsProgram(CurrentPrograms.MagazineExpress)) 
					{
						this.dataSet.CampaignProgram.DefaultView.RowFilter = "ProgramID = " + ((int) ReportedPrograms.Gift).ToString() + " OR ProgramID = " + ((int) ReportedPrograms.SweetSensations).ToString();

						if(!RunsProgram(CurrentPrograms.Gift) && !RunsProgram(CurrentPrograms.SweetSensations)) 
						{
							if(oCampaign.dataSet.Campaign[0].Lang == "EN") 
							{
								oSummaryReport = SummaryReports.MagExpressSummary;
							} 
							else if(oCampaign.dataSet.Campaign[0].Lang == "FR")
							{
								oSummaryReport = SummaryReports.MagExpressSummary_French;
							}
						} 
						else 
						{
							if(oCampaign.dataSet.Campaign[0].Lang == "EN") 
							{
								oSummaryReport = SummaryReports.MagComboSummary;
							} 
							else if(oCampaign.dataSet.Campaign[0].Lang == "FR")
							{
								oSummaryReport = SummaryReports.MagComboSummary_French;
							}
						}
					} 
					else 
					{
						if(this.dataSet.CampaignProgram.Count == 1 && RunsProgram(CurrentPrograms.GiftOnlyProgram)) 
						{
							if(oCampaign.dataSet.Campaign[0].Lang == "EN") 
							{
								oSummaryReport = SummaryReports.GiftOnlySummary;
							} 
							else if(oCampaign.dataSet.Campaign[0].Lang == "FR") 
							{
								oSummaryReport = SummaryReports.GiftOnlySummary_French;
							}
						} 
						else if(this.dataSet.CampaignProgram.Count == 1 && RunsProgram(CurrentPrograms.SweetSensations))
						{
							if(oCampaign.dataSet.Campaign[0].Lang == "EN") 
							{
								oSummaryReport = SummaryReports.CookieDoughSummary;
							} 
							else if(oCampaign.dataSet.Campaign[0].Lang == "FR") 
							{
								oSummaryReport = SummaryReports.CookieDoughSummary_French;
							}
						} 
					}
				}

				if(oSummaryReport != SummaryReports.None && RunsProgram(CurrentPrograms.Magnet)) 
				{
					if(oCampaign.dataSet.Campaign[0].Lang == "EN") 
					{
						oSummaryReport |= SummaryReports.MagnetSummary;
					} 
					else if(oCampaign.dataSet.Campaign[0].Lang == "FR") 
					{
						oSummaryReport |= SummaryReports.MagnetSummary_French;
					}
				}
			}

			return oSummaryReport;
		}*/
	}
}