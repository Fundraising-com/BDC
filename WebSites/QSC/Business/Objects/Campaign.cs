namespace Business.Objects
{
	using System;
	using System.Data;
	using System.Collections;
	using Business.Rules;
	using Common;
	using Common.TableDef;
	using DAL;
	using dataSetRef = Common.TableDef.CampaignDataSet;
	using dataAccessRef = DAL.CampaignData;

	public enum CampaignStatus 
	{
		PendingIncomplete = 37001,
		Approved = 37002,
		PendingFS = 37003,
		OnHold = 37004,
		Cancel = 37005,
		Inactive = 37006,
		OrderLogged = 37007,
		InternalUsage_Active = 37008,
		InternalUsage_Inactive = 37009
	}

	/// <summary>
	///     This class contains the business rules. 
	/// </summary>
	public class Campaign : BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		dataSetRef dtsDataSet;

		public Campaign() : base()
		{
			this.dtsDataSet = new dataSetRef();
			CreateRulesCollection();
		}

		public Campaign(Message messageManager) : base(messageManager) 
		{
			this.dtsDataSet = new dataSetRef();
			CreateRulesCollection();
		}

		public Campaign(Transaction currentTransaction) : this() 
		{
			this.CurrentTransaction = currentTransaction;
		}

		public Campaign(Message messageManager, Transaction currentTransaction) : this(messageManager) 
		{
			this.CurrentTransaction = currentTransaction;
		}

		public Campaign(int id) : this() 
		{
			GetOneByID(id);
		}

		public Campaign(int id, Message messageManager) : this(messageManager) 
		{
			GetOneByID(id);
		}

		public Campaign(int id, Transaction currentTransaction) : this(currentTransaction) 
		{
			GetOneByID(id);
		}

		public Campaign(int id, Message messageManager, Transaction currentTransaction) : this(messageManager, currentTransaction) 
		{
			GetOneByID(id);
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
				return this.dtsDataSet.Campaign.TableName;
			}
		}

		protected override DBTableOperation DataAccessReference
		{
			get
			{
				return dataAccess;
			}
		}

		public void GetOneByID(Int32 ID)
		{
			try
			{
				dataAccess.SelectOne(dtsDataSet, DefaultTableName, ID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
			}
		}

		public void GetAllByShipToAccountID(int shipToAccountID)
		{
			try
			{
				dataAccess.SelectAllByShipToAccountID(dtsDataSet, DefaultTableName, shipToAccountID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
			}
		}

		public CAccount GetShipToAccount(int CampaignID) 
		{
			CAccount oShipToAccount = null;
			dataSetRef.CampaignRow rowSelected = dataSet.Campaign.FindByID(CampaignID);

			if(rowSelected != null) 
			{
				oShipToAccount = new CAccount(this.CurrentTransaction);
				oShipToAccount.GetOneByIdWithChildren(rowSelected.ShipToAccountID);
			}

			return oShipToAccount;
		}

		public bool Save() 
		{
			bool isValid = base.UpdateBatch();

			if(CurrentMessageManager.Count > 0) 
			{
				CurrentMessageManager.PrepareErrorMessage();
				throw new MessageException(CurrentMessageManager);
			}

			return isValid;
		}

		public bool SaveWithoutValidation() 
		{
			try 
			{
				return (Convert.ToBoolean(DataAccessReference.UpdateBatch(baseDataSet, DefaultTableName)));
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
				return false;
			}
		}

		protected override bool ValidateUpdate(DataRow row)
		{
			return Validate(row);
		}

		protected override bool ValidateInsert(DataRow row)
		{
			return Validate(row);
		}

		protected override bool ValidateUpdateBatch(DataRow row)
		{
			return Validate(row);
		}

		private bool Validate(DataRow row) 
		{
			dataSetRef.CampaignRow rowCampaign;

			if(!base.ValidateUpdate(row)) 
			{
				rowCampaign = row as dataSetRef.CampaignRow;

				if(rowCampaign != null) 
				{
					if(rowCampaign.Status != Convert.ToInt32(CampaignStatus.Cancel) && rowCampaign.Status != Convert.ToInt32(CampaignStatus.Inactive)) 
					{
						rowCampaign.ApprovedStatusDate = Convert.ToDateTime(dataSet.Campaign.ApprovedStatusDateColumn.NullValue);
						rowCampaign.Status = Convert.ToInt32(CampaignStatus.PendingIncomplete);
					}
				}
			} 
			else 
			{
				rowCampaign = row as dataSetRef.CampaignRow;

				if(rowCampaign != null) 
				{
					//It is possible to have validation successful with approved status MS June 18, 2007
					if(rowCampaign.Status == Convert.ToInt32(CampaignStatus.PendingIncomplete) ||
					   rowCampaign.Status == Convert.ToInt32(CampaignStatus.Approved)) 
					{
						rowCampaign.Status = Convert.ToInt32(CampaignStatus.Approved);
						rowCampaign.ApprovedStatusDate = DateTime.Now;
					}
				}
			}

			if(rowCampaign != null) 
			{
				if(rowCampaign.SuppliesCampaignContactID == 0) 
				{
					rowCampaign.SetSuppliesCampaignContactIDNull();
				}
			}

			return true;
		}

		public bool Clone(int ID) 
		{
			bool bIsSuccess = true;
			Contact oContact;
			//CampaignProgram oCampaignProgram;

			try 
			{
				dataSetRef.CampaignRow rowCampaign = dataSet.Campaign.FindByID(ID);
				dataSetRef.CampaignRow rowNewCampaign = dataSet.Campaign.NewCampaignRow();
			
				CopyCampaignRow(rowCampaign, rowNewCampaign);
				AdjustClonedCampaignSeason(rowNewCampaign);

				oContact = new Contact(this.CurrentTransaction);
				oContact.GetLastByAccountID(rowCampaign.ShipToAccountID, true, rowCampaign.Lang);

				rowNewCampaign.ShipToCampaignContactID = oContact.Clone(oContact.dataSet.Contact[0].Id, 0);
				rowNewCampaign.BillToCampaignContactID = rowNewCampaign.ShipToCampaignContactID;

				this.dataSet.Campaign.AddCampaignRow(rowNewCampaign);

				this.SaveWithoutValidation();

				/*if(bIsSuccess) 
				{
					oCampaignProgram = new CampaignProgram(ID);

					bIsSuccess &= oCampaignProgram.Clone(ID, rowNewCampaign.ID);
				}*/

				if(!bIsSuccess) 
				{
					throw new MessageException(Message.ERRMSG_SYSTEM_VAR_0);
				}
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}

			return bIsSuccess;
		}

		private void CopyCampaignRow(dataSetRef.CampaignRow rowFrom, dataSetRef.CampaignRow rowTo) 
		{
			rowTo.Status = Convert.ToInt32(CampaignStatus.PendingIncomplete);
			rowTo.Renewal = true;
			rowTo.Country = rowFrom.Country;
			rowTo.FMID = rowFrom.FMID;
			rowTo.DateChanged = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
			rowTo.Lang = rowFrom.Lang;
			rowTo.EndDate = rowFrom.EndDate;
			rowTo.StartDate = rowFrom.StartDate;
			rowTo.IncentivesBillToID = rowFrom.IncentivesBillToID;
			rowTo.BillToAccountID = rowFrom.BillToAccountID;
			rowTo.ShipToCampaignContactID = rowFrom.ShipToCampaignContactID;
			rowTo.ShipToAccountID = rowFrom.ShipToAccountID;
			rowTo.EstimatedGross = rowFrom.EstimatedGross;
			rowTo.NumberOfParticipants = rowFrom.NumberOfParticipants;
			rowTo.NumberOfClassroooms = rowFrom.NumberOfClassroooms;
			rowTo.NumberOfStaff = rowFrom.NumberOfStaff;
			rowTo.BillToCampaignContactID = rowFrom.BillToCampaignContactID;
			rowTo.SetSuppliesCampaignContactIDNull();
			rowTo.SuppliesShipToCampaignContactID = 0;
			rowTo.SuppliesDeliveryDate = new DateTime(1995, 1, 1);
			rowTo.SpecialInstructions = rowFrom.SpecialInstructions;
			rowTo.IsStaffOrder = rowFrom.IsStaffOrder;
			rowTo.StaffOrderDiscount = rowFrom.StaffOrderDiscount;
			rowTo.IsTestCampaign = rowFrom.IsTestCampaign;
			rowTo.DateModified = DateTime.Now;
			rowTo.UserIDModified = rowFrom.UserIDModified;
			rowTo.IsPayLater = rowFrom.IsPayLater;
			rowTo.IncentivesDistributionID = rowFrom.IncentivesDistributionID;
			rowTo.FSOrderRecCreated = false;
			rowTo.MagnetStatementDate = rowFrom.MagnetStatementDate;
			rowTo.RewardsProgramCumulative = rowFrom.RewardsProgramCumulative;
			rowTo.RewardsProgramChart = rowFrom.RewardsProgramChart;
			rowTo.RewardsProgramDraw = rowFrom.RewardsProgramDraw;
			rowTo.ContactName = rowFrom.ContactName;
			rowTo.PhoneListID = rowFrom.PhoneListID;
			rowTo.SuppliesAddressID = rowFrom.SuppliesAddressID;
			rowTo.DateSubmitted = DateTime.Now;
         rowTo.OnlineOnlyPrograms = rowFrom.OnlineOnlyPrograms;
         rowTo.Extra_1Ups = rowFrom.Extra_1Ups;
         rowTo.Extra_GiftForm = rowFrom.Extra_GiftForm;
         rowTo.CoolCardsBoxes = rowFrom.CoolCardsBoxes;
         rowTo.OnlineNutFree = rowFrom.OnlineNutFree;
         rowTo.OnlineMagazineTRTOnly = rowFrom.OnlineMagazineTRTOnly;
        }

        private void AdjustClonedCampaignSeason(dataSetRef.CampaignRow rowCloned) 
		{
			Season oSeason = new Season();
			SeasonDataSet.SeasonRow rowCampaignSeason;
			SeasonDataSet.SeasonRow rowCurrentSeason;

			if(this.CurrentTransaction != null) 
			{
				oSeason.CurrentTransaction = this.CurrentTransaction;
			}

			oSeason.GetAll();

			rowCampaignSeason = oSeason.FindSeason(rowCloned.StartDate);
			rowCurrentSeason = oSeason.FindSeason(DateTime.Now);

			if(rowCampaignSeason != null && rowCurrentSeason != null) 
			{
				if(rowCampaignSeason.Season == "S" && rowCurrentSeason.Season == "F") 
				{
					rowCloned.StartDate = rowCloned.StartDate.AddYears((DateTime.Now.Year + 1) - rowCloned.StartDate.Year);
					rowCloned.EndDate = rowCloned.EndDate.AddYears((DateTime.Now.Year + 1) - rowCloned.EndDate.Year);
				} 
				else 
				{
					rowCloned.StartDate = rowCloned.StartDate.AddYears((DateTime.Now.Year) - rowCloned.StartDate.Year);
					rowCloned.EndDate = rowCloned.EndDate.AddYears((DateTime.Now.Year) - rowCloned.EndDate.Year);

					//10/26/2006: Jeff: Since you can't save a campaign whose end date is passed,
					//set end date to end of current season
					if (rowCloned.EndDate < DateTime.Now.AddDays(-1))
					{
						rowCloned.EndDate = rowCurrentSeason.EndDate;
					}
				}
			}
		}

		public void UpdateContactForAllAccount(int caccountID, int shipToContactID) 
		{
			try 
			{
				dataAccess.UpdateContactForAllAccount(caccountID, shipToContactID);
			} 
			catch(Exception ex)
			{
				ManageError(ex);
			}
		}
	}
}