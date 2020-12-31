using System;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.ComponentModel;
using DAL;

namespace Business
{
	///<summary>Business Representation of a list of Campaign to Programs</summary>
	public class CampaignProgram  : QBusinessObject
	{
		#region Class Members
		protected int CampaignIDM = -5;
		///<summary>int: Campaign #</summary>
		[DAL.DataColumn("CampaignID")]
		public int CampaignID
		{
			get { return this.CampaignIDM;  }
			set { this.CampaignIDM = value; }
		}

		private System.Collections.ArrayList ProgramsM = new System.Collections.ArrayList(20);

		///<summary>Method to Add a Campaign/Program/Brochure Record to a Campaign</summary>
		///<param name="Input">a Business.CampaignProgramBrochure instance</param>
		public void AddProgram(Business.Program Input)
		{
			ProgramsM.Add(Input);
		}

		#region class members for field supply info

		protected int SuppliesCampaignContactIDM ;
		[DAL.DataColumn("SuppliesCampaignContactID")]
		public int SuppliesCampaignContactID
		{
			get { return this.SuppliesCampaignContactIDM;   }
			set { this.SuppliesCampaignContactIDM = value;  }
		}

		protected int SuppliesShipToCampaignContactIDM ;
		[DAL.DataColumn("SuppliesShipToCampaignContactID")]
		public int SuppliesShipToCampaignContactID
		{
			get { return this.SuppliesShipToCampaignContactIDM;   }
			set { this.SuppliesShipToCampaignContactIDM = value;  }
		}


		protected DateTime SuppliesDeliveryDateM = System.DateTime.MinValue ;
		[DAL.DataColumn("SuppliesDeliveryDate")]
		public DateTime SuppliesDeliveryDate
		{
			get { return this.SuppliesDeliveryDateM;   }
			set { this.SuppliesDeliveryDateM = value;  }
		}

		protected bool FSOrderRecCreatedM ;
		[DAL.DataColumn("FSOrderRecCreated")]
		public bool FSOrderRecCreated
		{
			get { return this.FSOrderRecCreatedM;   }
			set { this.FSOrderRecCreatedM = value;  }
		}

		protected int SuppliesAddressIDM = -1;
		public int SuppliesAddressID
		{
			get { return this.SuppliesAddressIDM;  }
			set { this.SuppliesAddressIDM = value; }
		}

		///<summary>
		/// Function to call address layer,
		/// get an AddressID for a new entry 
		///</summary>
		///<remarks>
		/// Only call when user has requested 
		/// to view the supply address,
		/// otherwise the address table will be full of blank entries
		///</remarks>
		///<returns>
		///boolean value indicating whether the
		/// public int SuppliesAddressID contains a valid ID
		///</returns>
		public bool GenerateSuppliesAddressID()
		{
			if(this.SuppliesAddressIDM != -1)
			{
				//dont request an address ID if one already exists
				return true;
			}

			Business.Address oAddress = new Address();
			oAddress.street1       = " ";
			oAddress.street2       = " ";
			oAddress.city          = " ";
			oAddress.stateProvince = " ";
			oAddress.postal_code   = " ";
			oAddress.zip4          = " ";
			oAddress.country       = "ca";
			oAddress.AddressListID = -1;
			oAddress.address_type  = ((int) Business.AddressType.SupplyAddr);
			if(oAddress.ValidateAndSave() == false)
			{
				//we couldnt get an ID
				return false;
			}
			else
			{
				if(oAddress.address_id == -1)
				{
					//we still couldn't get an ID to 
					//actually be generated
					return false;
				}
				else
				{
					//all good, dude
					//make sure we record this new address back
					this.SuppliesAddressID = oAddress.address_id;
					if(this.SaveNewSuppliesAddressID())
					{
						//we got the new id
						//we saved the new id
						return true;
					}
					else
					{
						//we couldnt save, grr, almost there
						return false;
					}
				}
			}
		}

		#endregion class members for field supply info

		#region class members for validation

		#region validation_info
		protected bool	_ValidBIM;
		///<summary>Gets or sets value indicating if the CA has passsed biz intelligence level validation</summary>
		public bool	ValidBI
		{
			get { return this._ValidBIM;  } 
			set {this._ValidBIM  = value; }
		}

		protected string _ErrorBIM;
		///<summary>Gets or sets error string associatated with biz intelligence level validation</summary>
		public string ErrorBI
		{ 
			get { return this._ErrorBIM;   } 
			set { this._ErrorBIM  = value; }
		}
		#endregion validation_info

		#region Programs
		///<summary>Gets or sets a boolean value indicating if a Regular Magazine program is being run</summary>
		private bool ProgramMagazineRegularM = false;

		///<summary>Gets or sets a boolean value indicating if a Magazine Express program is being run</summary>
		private bool ProgramMagazineExpressM = false;

		///<summary>Gets or sets a boolean value indicating if a Magazine Combo program is being run</summary>
		private bool ProgramMagazineComboM = false;

		///<summary>Gets or sets a boolean value indicating if a Magnet program is being run</summary>
		private bool ProgramMagnetM = false;

		///<summary>Gets or sets a boolean value indicating if a Magnet is flagged as Pre Collect</summary>
		private bool ProgramMagnet_IsPreCollectM = false;

		///<summary>Gets or sets a boolean value indicating if a Magazine Staff program is being run</summary>
		private bool ProgramMagazineStaffM = false;

		///<summary>Gets or sets a boolean value indicating if a Gift program is being run</summary>
		private bool ProgramGiftM = false;

		///<summary>Gets or sets a boolean value indicating if a Easy as Pie program is being run</summary>
		private bool ProgramEasyAsPieM = false;

		///<summary>Gets or sets a boolean value indicating if a Easy as Pie is flagged as Pre Collect</summary>
		private bool ProgramEasyAsPie_IsPreCollectM = false;

		///<summary>Gets or sets a boolean value indicating if a Prize Zone program is being run</summary>
		private bool ProgramPrizeZoneM = false;

		///<summary>Gets or sets a boolean value indicating if a Prize Zone is flagged as Pre Collect</summary>
		private bool ProgramPrizeZone_IsPreCollectM = false;

		///<summary>Gets or sets a boolean value indicating if a Reach for the stars program is being run</summary>
		private bool ProgramReachForTheStarsM = false;

		///<summary>Gets or sets a boolean value indicating if a Hershey chocolate program is being run</summary>
		private bool ProgramHersheyChocolateM = false;

		///<summary>Gets or sets a boolean value indicating if a Chocolate symphony program program is being run</summary>
		private bool ProgramChocolateSymphonyM = false;

        ///<summary>Gets or sets a boolean value indicating if a Large Chart with Num Subs program is being run</summary>
        private bool ProgramLargeChartWithNumSubsM = false;

        ///<summary>Gets or sets a boolean value indicating if a Large Chart with Num Subs is flagged as Pre Collect</summary>
        private bool ProgramLargeChartWithNumSubs_IsPreCollectM = false;
        #endregion Programs

		#region Rewards
		///<summary>Gets or sets a boolean value indicating if a Draw prize program is being run</summary>
		private bool RewardsDrawPrizeM = false;
		
		///<summary>Gets or sets a boolean value indicating if a Planetary Rewards Program program is being run</summary>
		private bool RewardsPlanetaryM = false;

		///<summary>Gets or sets a boolean value indicating if a Kanata Extreme Rewards Program program is being run</summary>
		private bool RewardsKanataExtremeM = false;

		///<summary>Gets or sets a boolean value indicating if a Cumulative Rewards program is being run</summary>
		private bool RewardsCumulativeM  = false;

		///<summary>Gets or sets a boolean value indicating if a Chart Rewards program is being run</summary>
		private bool RewardsChartM = false;
		#endregion Rewards

		#endregion class members for validation
		#endregion

		#region Constructors
		protected DAL.CampaignProgramDataAccess aTable = new DAL.CampaignProgramDataAccess();
		///<summary>default constructor</summary>
		public CampaignProgram(){}
        
		///<summary>constructor</summary>
		public CampaignProgram(int CampaignID)
		{
			this.CampaignIDM = CampaignID;
		}
		#endregion

		#region ValidateAndSave
		///<summary>Check the Campaign/Program/Brochure for compliance with biz rules, then save it</summary>
		///<param name="ReturnCode"></param>
		///<param name="blValidateFS"></param>
		///<returns>bool: did both pieces work ?</returns>
		public bool ValidateAndSave(bool blGeneratingSupplies)
		{
			string stError = "";//start off clean
			bool blValid = true;

			stError += this.Validate(ref blValid, blGeneratingSupplies);

			if(blValid == true)
			{
				//stError += "\r\nValidation Passed, Attempting to save";
				stError += this.Save(ref blValid, blGeneratingSupplies);
			}
//			else
//			{
//				stError += "\r\nValidation Failed, Saving was not attempted!\r\n";
//			}

			this.ErrorBI = stError;
			this.ValidBI = blValid;
			return blValid;
		}

		///<summary>the ValidateAndSave required for the compiler</summary>
		///<remarks>disregards the return code int</remarks>
		///<returns>bool: did both pieces work ?</returns>
		override public bool ValidateAndSave()
		{
			ValidateAndSave(true);
			return this.ValidBI;
		}

		#region Save
		private string Save(ref bool blValid, bool blGeneratingSupplies)
		{
			string stError = "";
			Business.Program ProgramItem;

			int intResult = 0;
			for (int i = 0; i < ProgramsM.Count; i++)
			{
				ProgramItem = ((Business.Program) ProgramsM[i]);
				bool blProgramItem = ProgramItem.ValidateAndSave(ref intResult);
				if(blProgramItem == false)
				{
					stError += "CampaignProgram - Save - Failed\r\n";
					stError += "ProgramId: " + ProgramItem.ProgramID.ToString() + "\r\n";
					stError += "Reason int: " + intResult.ToString() + "\r\n";
					blValid = false;
				}
			}

			SaveFSInfo(ref blValid);

			return stError;
		}

		///<summary>Save the field supply info</summary>
		///<returns>bool</returns>
		private string SaveFSInfo(ref bool blValid)
		{
			string stError = "";

			bool blSaved = aTable.SaveCampaignFSinfo(
				this.CampaignIDM,
				this.SuppliesDeliveryDateM,
				this.SuppliesShipToCampaignContactID,
				this.UserIDModifiedM);

			if(blSaved == false)
			{ 
				stError += "Code -40: An error occured in the DAL layer saving Field Supply info\r\n";
				blValid = false;
			}

			return stError;
		}

		#endregion Save

		#region Validation
		///<summary>Check for compliance with Business Intelligence rules</summary>
		///<returns>bool: Was validation successful ? </returns>

		///<summary>Checks for completness and rule compliance</summary>
		///<param name="blValid">ref bool: Was validation successful ?</param>
		///<param name="blValidateFieldSupplies">bool: Is a FS order being generated(true) or just saved(false)?</param>
		///<returns>string: Error Message</returns>
		public string Validate(ref bool blValid, bool blValidateFieldSupplies)
		{
			/* setup variables to track validation */
			string stError = "";

			/* Analyze the program selections */
			Validate_Parse_Program_Selections();

			/* Check the Campaign/Programs for completeness */
			stError += Validate_MandatorySelections(ref blValid);

			/* Check that the program selections are compatible */
			stError += Validate_ProgramSelections(ref blValid);

			/* Check the field supplies */
			if(blValidateFieldSupplies == true)
			{
				stError += Validate_FieldSupplies(ref blValid, true);
			}

			/* Save the validation results into the CampaignProgram object */
			this.ValidBI = blValid;
			this.ErrorBI += stError;

			return stError;
		}


		///<summary>Go through the array of programs and extract info</summary>
		private void Validate_Parse_Program_Selections()
		{
			//start with everything unselected
			this.ProgramMagazineRegularM = false;
			this.ProgramMagazineExpressM = false;
			this.ProgramMagazineComboM = false;
			this.ProgramMagnetM = false;
			this.ProgramMagazineStaffM = false;
			this.ProgramGiftM = false;
			this.ProgramEasyAsPieM = false;
			this.ProgramPrizeZoneM = false;
			this.ProgramReachForTheStarsM = false;
			this.ProgramHersheyChocolateM = false;
			this.ProgramChocolateSymphonyM = false;
			this.RewardsDrawPrizeM = false;
			this.RewardsPlanetaryM = false;
			this.RewardsKanataExtremeM = false;
			this.RewardsCumulativeM  = false;
			this.RewardsChartM = false;
			this.ProgramMagnet_IsPreCollectM = false;
			this.ProgramEasyAsPie_IsPreCollectM = false;
			this.ProgramPrizeZone_IsPreCollectM = false;
            this.ProgramLargeChartWithNumSubsM = false;
            this.ProgramLargeChartWithNumSubs_IsPreCollectM = false;

			int ProgramID;
			bool IsPreCollect;
			for(int i =0; i < this.ProgramsM.Count; i++)
			{
				if( ((Business.Program)this.ProgramsM[i]).ProgramChoice == true )
				{
					//if the program is selected, log it
					ProgramID    = ((Business.Program)this.ProgramsM[i]).ProgramID;
					IsPreCollect = ((Business.Program)this.ProgramsM[i]).IsPreCollect;

					#region  switch(  ProgramID )

					switch( (Business.ProgramIDType) ProgramID)
					{
						case ProgramIDType.Magazine:
							this.ProgramMagazineRegularM = true;
							break;
						case ProgramIDType.MagazineExpress:
							this.ProgramMagazineExpressM = true;
							break;
						case ProgramIDType.MagazineCombo:
							this.ProgramMagazineComboM = true;
							break;
						case ProgramIDType.Magnet:
							this.ProgramMagnetM = true;
							this.ProgramMagnet_IsPreCollectM = IsPreCollect;
							break;
						case ProgramIDType.MagazineStaff:
							this.ProgramMagazineStaffM = true;
							break;
						case ProgramIDType.Gift:
							this.ProgramGiftM = true;
							break;
						case ProgramIDType.EasyAsPie:
							this.ProgramEasyAsPieM = true;
							this.ProgramEasyAsPie_IsPreCollectM = IsPreCollect;
							break;
						case ProgramIDType.PrizeZone:
							this.ProgramPrizeZoneM = true;
							this.ProgramPrizeZone_IsPreCollectM = IsPreCollect;
							break;
						case ProgramIDType.ReachForTheStars:
							this.ProgramReachForTheStarsM = true;
							break;
						case ProgramIDType.HersheyChocolate:
							this.ProgramHersheyChocolateM = true;
							break;
						case ProgramIDType.ChocolateSymphony:
							this.ProgramChocolateSymphonyM = true;
							break;
						case ProgramIDType.DrawPrize:
							this.RewardsDrawPrizeM = true;
							break;
						case ProgramIDType.PlanetaryRewards:
							this.RewardsPlanetaryM = true;
							break;
						case ProgramIDType.KanataExtremeRewards:
							this.RewardsKanataExtremeM = true;
							break;
						case ProgramIDType.CumulativeRewards:
							this.RewardsCumulativeM = true;
							break;
						case ProgramIDType.ChartRewards:
							this.RewardsChartM = true;
							break;
                        case ProgramIDType.LargeChartWithNumSubs:
                            this.ProgramLargeChartWithNumSubsM = true;
                            this.ProgramLargeChartWithNumSubs_IsPreCollectM = IsPreCollect;
                            break;
                        default:
							break;
					} //end switch( (Business.ProgramIDType) ProgramID)

					#endregion  switch(  ProgramID )

				}//end if program is selected
			}//end for loop on campaign programs
		} //end Validate_Parse_Program_Selections()


		///<summary>Check that mandatory Campaign/Program info was entered</summary>
		///<param name="blValid">ref bool: Was validation successful ?</param>
		///<returns>string: Error Message</returns>
		private string Validate_MandatorySelections(ref bool blValid)
		{
			string stError = "";

			///<businessIntelligence>Atleast one fundraising program selection is mandatory</businessIntelligence>
			if ((this.ProgramMagazineRegularM == false)&&
				(this.ProgramMagazineExpressM == false)&&
				(this.ProgramMagazineComboM == false)&&
				(this.ProgramMagnetM == false)&&
				(this.ProgramMagazineStaffM == false)&&
				(this.ProgramGiftM == false)&&
				(this.ProgramEasyAsPieM == false)&&
				(this.ProgramPrizeZoneM == false)&&
				(this.ProgramReachForTheStarsM == false)&&
				(this.ProgramHersheyChocolateM == false)&&
				(this.ProgramChocolateSymphonyM == false)&&
                (this.ProgramLargeChartWithNumSubsM == false))
			{
				blValid = false;
				stError += "Please select at least one Program\r\n";
			}

			///<businessIntelligence>Atleast one reward program selection is mandatory</businessIntelligence>
			if ((this.RewardsCumulativeM == false)&&
				(this.RewardsChartM == false)&&
				(this.RewardsDrawPrizeM == false)&&
				(this.RewardsPlanetaryM == false)&&
				(this.RewardsKanataExtremeM == false) )
			{
				blValid = false;
				stError += "Please select at least one Reward program\r\n";
			}
//
//			/BIZCOMMENT/<businessIntelligence>Atleast one reward distribution program selection is mandatory</businessIntelligence>
//			//if ((this.cbIncentiveParticipantBag == false)&&(this.cbIncentiveClassroomBox == false))
//			if(this.IncentivesDistributionIDM == IncentivesDistribution.Undefined)
//			{
//				blValid = false;
//				stError += "Please select at least one Reward Distribution program\r\n";
//			}

			return stError;
		}


		#region Validation CheckDates

		///<summary>Check date fields for business rule compliance</summary>
		///<param name="blValid">ref bool: Was validation successful ?</param>
		///<returns>string: Error Message</returns>
		///<remarks>I totally fudged 15 days for lagtime, need the real rule</remarks>
		private string Validate_CheckDates(ref bool blValid)
		{
			string stError = "";

			///<businessIntelligence>Field supply date cannot be met because current date + lag time for that province exceeds the delivery date</businessIntelligence>
			stError += Validate_CheckDates_FieldSupplies(ref blValid, 15);

			return stError;
		}

		///<summary>Check that a FS Delivery date is do-able</summary>
		///<param name="blValid">ref bool: Was validation successful ?</param>
		///<param name="lagTime">int: how many days are needed to prepare supplies</param>
		///<returns>string: Error Message</returns>
		private string Validate_CheckDates_FieldSupplies(ref bool blValid, int lagTime)
		{
			string stError = "";
			System.DateTime SupplyDate = new DateTime();
			SupplyDate = DateTime.Now.AddDays(lagTime);
			if ((SupplyDate.Date > this.SuppliesDeliveryDate.Date))
			{
				blValid = false;
				stError += "Due to a preparation time of " + lagTime + " days for field supply preparation,\r\n";
				if( (this.SuppliesDeliveryDateM.Date != new DateTime(1995,1,1).Date) &&
					(this.SuppliesDeliveryDateM.Date != DateTime.MinValue.Date) 
				  )
				{
					stError += "Field Supplies cannot be ready by " + this.SuppliesDeliveryDate.ToString("ddd, MMM dd yyyy") + "\r\n";
				}
				stError += "Field Supplies cannot be ready for this Campaign before: " + SupplyDate.ToString("ddd, MMM dd yyyy") + "\r\n";
			}
			return stError;
		}

		#endregion Validation CheckDates

		#region Validate_ProgramSelections

		///<summary>
		///	Check to make sure the correct combination of programs, 
		///	rewards, and prize distributions are selected
		///</summary>
		///<param name="blValid">ref bool: Was validation successful ?</param>
		///<returns>string: Error Message</returns>
		private string Validate_ProgramSelections(ref bool blValid)
		{
			string stError = "";

			///<businessIntelligence>Magazine and Magazine Express cannot both be selected</businessIntelligence>
			if((this.ProgramMagazineRegularM == true)&&(this.ProgramMagazineExpressM == true))
			{
				blValid = false;
				stError += "The 'Magazine Regular' and 'Magazine Express' programs cannot both be selected for the same campaign\r\n";
			}

			///<businessIntelligence>Magazine and Magnet cannot both be selected</businessIntelligence>
			if((this.ProgramMagazineRegularM == true)&&(this.ProgramMagnetM == true))
			{
				blValid = false;
				stError += "The 'Magazine Regular' and 'Magnet' programs cannot both be selected for the same campaign\r\n";
			}

			///<businessIntelligence>Magazine Express and Magnet cannot both be selected</businessIntelligence>
			if((this.ProgramMagazineExpressM == true)&&(this.ProgramMagnetM == true))
			{
				blValid = false;
				stError += "The 'Magazine Express' and 'Magnet' programs cannot both be selected for the same campaign\r\n";
			}

			///<businessIntelligence>Easy As Pie and Prize Zone cannot both be selected</businessIntelligence>
			if((this.ProgramEasyAsPieM == true)&&(this.ProgramPrizeZoneM == true))
			{
				blValid = false;
				stError += "The 'Easy As Pie' and 'Prize Zone' programs cannot both be selected for the same campaign\r\n";
			}

            ///<businessIntelligence>Easy As Pie and Large Chart With Num Subs cannot both be selected</businessIntelligence>
            if ((this.ProgramEasyAsPieM == true) && (this.ProgramLargeChartWithNumSubsM == true))
            {
                blValid = false;
                stError += "The 'Easy As Pie' and 'Large Chart With Num Subs' programs cannot both be selected for the same campaign\r\n";
            }

			///<businessIntelligence>Magnet cannot be Pre-Collect</businessIntelligence>
			if((this.ProgramMagnetM == true)&&(this.ProgramMagnet_IsPreCollectM == true))
			{
				blValid = false;
				stError += "The 'Magnet' programs cannot have Pre-Collect selected\r\n";
			}

			///<businessIntelligence>Easy As Pie cannot be Pre-Collect</businessIntelligence>
			if((this.ProgramEasyAsPieM == true)&&(this.ProgramEasyAsPie_IsPreCollectM == true))
			{
				blValid = false;
				stError += "The 'Easy As Pie' program cannot have Pre-Collect selected\r\n";
			}

			///<businessIntelligence>Prize Zone cannot be Pre-Collect</businessIntelligence>
			if((this.ProgramPrizeZoneM == true)&&(this.ProgramPrizeZone_IsPreCollectM == true))
			{
				blValid = false;
				stError += "The 'Prize Zone' program cannot have Pre-Collect selected\r\n";
			}

            ///<businessIntelligence>Large Chart with Num Subs cannot be Pre-Collect</businessIntelligence>
            if ((this.ProgramLargeChartWithNumSubsM == true) && (this.ProgramLargeChartWithNumSubs_IsPreCollectM == true))
            {
                blValid = false;
                stError += "The 'Large Chart with Num Subs' program cannot have Pre-Collect selected\r\n";
            }

			return stError;
		}
		#endregion

		#region Validate Field Supplies

		///<summary>Check the Field Supply Info</summary>
		///<param name="blValid">ref bool: Was validation successful ?</param>
		///<param name="GeneratingSupplies"></param>
		///<returns>string: Error Message</returns>
		private string Validate_FieldSupplies(ref bool blValid, bool GeneratingSupplies)
		{
			string stError = "";

			stError += Validate_FieldSupplies_MandatoryItems(ref blValid, GeneratingSupplies);

			if (GeneratingSupplies == true)
			{
				/* Check that the dates entered meet various rules */
				stError += Validate_CheckDates(ref blValid);
			}

			return stError;
		}


		///<summary>Check that mandatory ield Supply info was entered</summary>
		//<param name="blValid">ref bool: Was validation successful ?</param>
		///<param name="GeneratingSupplies" datatype="bool">
		///  <item value="true" Description="Saving info to the DB, then Generating a Field Supply order" />
		///  <item value="true" Description="Saving info to the DB only, no FS order generated" />
		///</param>
		///<returns>string: Error Message</returns>
		private string Validate_FieldSupplies_MandatoryItems(ref bool blValid, bool GeneratingSupplies)
		{
			string stError = "";

			if(this.CampaignIDM == -5) 
			{ 
				stError += /*"Code -35: "*/"A Campaign ID is needed\r\n";
				blValid = false;
			}

			if(this.SuppliesDeliveryDateM.Date == DateTime.MinValue.Date) 
			{ 
				stError += /*"Code -36: "*/"A supply delivery date was never assigned\r\n";
				blValid = false;
			}

			if(GeneratingSupplies == true)
			{
				if(this.SuppliesDeliveryDateM.Date == new DateTime(1995,1,1).Date) 
				{
					stError += /*"Code -37: "*/"A supply delivery date is needed\r\n";
					blValid = false;
				}

				if(this.SuppliesShipToCampaignContactIDM == (int) Business.ShipSuppliesTo.Undefined)
				{
					stError += /*"Code -38: "*/"Where are the supplies being shipped to? Please select a value.\r\n";
					blValid = false;
				}
			}

			if(this.UserIDModifiedM < -5)
			{
				stError += /*"Code -39: "*/"Without a UserID, who can I attribute this to ? Please contact IT.\r\n";
				blValid = false;
			}

			return stError;
		}

		#endregion Validate Field Supplies

		#endregion Validation

		#endregion ValidateAndSave
     
		#region DAL calls - GetCampaignProgramList
		///<summary>Get a list of programs</summary>
		public DataTable GetCampaignProgramList()
		{
			if (this.CampaignIDM != -5)
			{
				return aTable.GetCampaignPrograms(this.CampaignIDM, true);
			}
			else
			{
				throw new ArgumentNullException("CampaignID", "A CampaignID is needed to get a list of attached programs");
			}
		}


		///<summary>Get a list of programs</summary>
		public DataTable GetCampaignProgramList(int CampaignID)
		{
			this.CampaignIDM = CampaignID;
			return GetCampaignProgramList();
		}

		///<summary>Get a list of selected programs</summary>
		public DataTable GetSelectedPrograms(int CampaignID)
		{
			this.CampaignIDM = CampaignID;
			return GetSelectedPrograms();
		}

		///<summary>Get a list of selected programs</summary>
		public DataTable GetSelectedPrograms()
		{
			if (this.CampaignIDM != -5)
			{
				return aTable.GetCampaignPrograms(this.CampaignIDM, false);
			}
			else
			{
				throw new ArgumentNullException("CampaignID", "A CampaignID is needed to get a list of selected programs");
			}
		}



		///<summary>Get the Field Supply info for a given Campaign</summary>
		///<returns>bool: did this function succeed?</returns>
		public bool GetFSInfo()
		{
			DataTable DT = aTable.Get_Campaign_FSinfo(this.CampaignIDM);
			if(DT.Rows.Count < 1) 
			{ 
				string msg = "No FS Data was found for the campaign " + this.CampaignIDM.ToString();
				throw new RowNotInTableException(msg);
			}
			else if (DT.Rows.Count > 1)
			{
				string msg = "Too much FS Data was found for the campaign " + this.CampaignIDM.ToString();
				throw new ArgumentException(msg);
			}

			DataRow DR = DT.Rows[0]; 
//			Fill(DR, this.GetType());

			this.CampaignIDM						= Convert.ToInt32(DR["CampaignID"]);
			this.SuppliesCampaignContactIDM			= Convert.ToInt32(DR["SuppliesCampaignContactID"]);
			this.SuppliesShipToCampaignContactIDM	= Convert.ToInt32(DR["SuppliesShipToCampaignContactID"]);
			this.SuppliesDeliveryDateM				= Convert.ToDateTime(DR["SuppliesDeliveryDate"]);
			this.FSOrderRecCreatedM					= Convert.ToBoolean(DR["FSOrderRecCreated"]);
			this.SuppliesAddressIDM					= Convert.ToInt32(DR["SuppliesAddressID"]);

			return true;
		}

		///<summary>Generates the Field Supplys for the given campaign</summary>
		///<returns>int: 0 is success, -17 or -18 missing inputs</returns>
		public int GenerateFSOrder()
		{
			//validate that the inputs have been set
			if (this.CampaignIDM == -5)
			{
				return -17;
			}
			if(this.UserIDModifiedM < -5)
			{
				return -18;
			}

			//generate the order
			aTable.GenerateFSOrder(this.CampaignIDM, this.UserIDModifiedM);
			return 0;
		}

		private bool SaveNewSuppliesAddressID()
		{
			return aTable.SaveNewSuppliesAddressID(this.CampaignIDM, this.SuppliesAddressID);
		}

		#endregion DAL calls - GetCampaignProgramList
	}
}


