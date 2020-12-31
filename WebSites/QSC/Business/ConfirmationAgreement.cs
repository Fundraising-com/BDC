using System;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.ComponentModel;
using System.Diagnostics;

namespace Business
{
	/// <summary>Represents all the info for one Confirmation Agreement</summary>
	public class ConfirmationAgreement : QBusinessObject
	{
		#region Constructors
		///<summary>default constructor</summary>
		public ConfirmationAgreement(){}
		#endregion

		#region variables
		protected Common.PhoneNumber phAccountPhoneM;
		public Common.PhoneNumber AccountPhone
		{
			get { return this.phAccountPhoneM;  }
			set { this.phAccountPhoneM = value; }
		}

		protected Common.PhoneNumber phAccountFaxM;
		public Common.PhoneNumber AccountFax
		{
			get { return this.phAccountFaxM;  }
			set { this.phAccountFaxM = value; }
		}

		protected int CampaignIDM = -1;
		///<summary>Gets or sets a value of the Campaign's DB Id.</summary>
		[DAL.DataColumn("CampaignID")]
		public int CampaignID
		{
			get{ return this.CampaignIDM; }
			set{ this.CampaignIDM = value;}
		}

		protected Business.CampaignStatus CampaignStatusM = Business.CampaignStatus.PendingIncomplete;
		///<summary>Gets or sets a value of a Campaign's status.</summary>
		public Business.CampaignStatus CampaignStatus
		{
			get{ return this.CampaignStatusM; }
			set{ this.CampaignStatusM = value;}
		}
		///<summary>Gets or sets a value of a Campaign's status.</summary>
		[DAL.DataColumn("Status")]
		public int CampaignStatusInt
		{
			get{ return (int) this.CampaignStatusM; }
			set{ this.CampaignStatusM = (Business.CampaignStatus) value;}
		}

		protected Business.CArenewalStatus RenewalStatusM = Business.CArenewalStatus.undefined;
		///<summary>Gets or sets a value indicating the renewal status of the CA</summary>
		public Business.CArenewalStatus RenewalStatus
		{
			get{ return this.RenewalStatusM;}
			set{ this.RenewalStatusM = value; }
		}

		/*private bool Country_AssignedM;*/
		protected string CountryM;
		///<summary>Gets or sets a value indicating what Country a campaign is in</summary>
		///<remarks>Country at the campaign level, is this needed?</remarks>
		[DAL.DataColumn("Country")]
		public string Country
		{
			get{ return this.CountryM; }
			set{ this.CountryM=value; /*this.Country_AssignedM = true;*/ }
		}

		protected string fmidM = "0000";
		///<summary>Gets or sets the value indicating the FM a campaign belongs to</summary>
		[DAL.DataColumn("FMID")]
		public string FMID
		{
			get{ return this.fmidM;   }
			set{ this.fmidM = value;  }
		}

		protected string LanguageM;
		///<summary>Gets or sets the value indicating a campaign's language</summary>
		[DAL.DataColumn("Language")]
		public string Language
		{
			get{ return this.LanguageM; }
			set{ this.LanguageM=value;  }
		}

		protected DateTime TermsStartDateM;
		public DateTime TermsStartDate
		{
			get{ return this.TermsStartDateM;  }
			set{ this.TermsStartDateM = value; }
		}
		protected DateTime TermsEndDateM;
		public DateTime TermsEndDate
		{
			get{ return this.TermsEndDateM;  }
			set{ this.TermsEndDateM = value; }
		}

		protected int EstimatedGrossM;
		public int EstimatedGross
		{
			get{ return this.EstimatedGrossM;  }
			set{ this.EstimatedGrossM = value; }
		}

		protected int StudentsParticipatingM;
		public int StudentsParticipating
		{
			get{ return this.StudentsParticipatingM;  }
			set{ this.StudentsParticipatingM = value; }
		}

		protected int RoomsOrTeamsM;
		public int RoomsOrTeams
		{
			get{ return this.RoomsOrTeamsM;  }
			set{ this.RoomsOrTeamsM = value; }
		}

		protected int StaffAmountM;
		public int StaffAmount
		{
			get{ return this.StaffAmountM;   }
			set{ this.StaffAmountM  = value; }
		}

		protected string SpecialInstructionsM = "";
		///<summary>Gets or sets a value of special instructions for a campaign</summary>
		///<remarks>SpecialInstructionsM: This is not on the CA form, where is it coming from?</remarks>
		private string SpecialInstructions
		{
			get{ return this.SpecialInstructionsM;  }
			set{ this.SpecialInstructionsM = value; }
		}

		///<summary>Gets a value of indicating if a campaign is a staff order</summary>
		///<remarks>IsStaffOrder: This is not on the CA form, where is it coming from? Read only? Would a campaign be a staff order? or is this at the order level ? Is the logic of checking the Magazine Staff program correct? </remarks>
		protected bool IsStaffOrderM;
		private bool IsStaffOrder
		{
			get { return this.IsStaffOrderM;  }
			set { this.IsStaffOrderM = value; }
		}

		protected int StaffOrderDiscountM = 0; // MS May 01, 07 50;
		///<summary>Gets or sets a value with the staff discount percentage</summary>
		///<remarks>StaffOrderDiscountM: This is not on the CA form, where is it coming from? Defaulting to 50</remarks>
		private int StaffOrderDiscount
		{
			get{ return this.StaffOrderDiscountM;  }
			set{ this.StaffOrderDiscountM = value; }
		}

		protected bool IsTestCampaignM = false;
		///<summary>Gets or sets a value of indicating if a campaign is a test</summary>
		///<remarks>IsTestCampaignM: This is not on the CA form, where is it coming from? Defaulting to false</remarks>
		private bool IsTestCampaign
		{
			get{ return this.IsTestCampaignM;  }
			set{ this.IsTestCampaignM = value; }
		}

		protected bool IsPayLaterM = false;
		///<summary>Gets or sets a value of indicating if a campaign is paylater</summary>
		///<remarks>IsPayLater: This is not on the CA form, where is it coming from? All fields in db had 'N' as value, converted to bit with 0 value.</remarks>
		private bool IsPayLater
		{
			get{ return this.IsPayLaterM;  }
			set{ this.IsPayLaterM = value; }
		}

		#region Incentives
		#region Incentives distribution
		protected Business.IncentivesDistribution IncentivesDistributionIDM = IncentivesDistribution.Undefined;
		///<summary>Gets or sets a value of indicating how prizes will be given out</summary>
		///<remarks>
		/// Paper form has values "Participant Bag" and "ClassRoom Boxes",
		/// DB has 1,2,3 and NULL. 
		/// What does 3 represent ? 
		/// Would 1 = bag, 2 = box ?
		///</remarks>
		public Business.IncentivesDistribution IncentivesDistributionID
		{
			get{ return this.IncentivesDistributionIDM;  }
			set{ this.IncentivesDistributionIDM = value; }
		}

		///<summary>Gets or sets a value of indicating how prizes will be given out</summary>
		public int IncentivesDistributionIDInt
		{
			get{ return (int) this.IncentivesDistributionIDM;  }
			set{ this.IncentivesDistributionIDM = (Business.IncentivesDistribution) value; }
		}
		#endregion Incentives distribution

		#region bill incentives to 
		protected Business.BillIncentivesTo	BillIncentivesToM;
		///<summary>Gets or sets a value of where to bill incentives to</summary>
		public Business.BillIncentivesTo BillIncentivesTo
		{
			get{ return this.BillIncentivesToM;  }
			set{ this.BillIncentivesToM = value; }
		}
		///<summary>Gets or sets a value of where to bill incentives to</summary>
		public int BillIncentivesToInt
		{
			get{ return (int) this.BillIncentivesToM;  }
			set{ this.BillIncentivesToM = (Business.BillIncentivesTo) value; }
		}
		#endregion bill incentives to 
		#endregion Incentives

		///<remarks>Campaign table, what is the difference between DateChanged varchar(50) and DateModified datetime</remarks>

		protected DateTime MagnetStatementDateM = new DateTime(1995,1,1);//year, month, day
		///<summary>Gets or sets a value of indicating  a magnet date ??</summary>
		///<remarks>MagnetStatementDate: This is not on the CA form, where is it coming from? DEfaulting to 1/1/1995 for now</remarks>
		private DateTime MagnetStatementDate
		{
			get{ return this.MagnetStatementDateM;  }
			set{ this.MagnetStatementDateM = value; }
		}

		protected DateTime ApprovedStatusDateM = new DateTime(1995,1,1);//year, month, day
		///<summary>Gets or sets a value of when a campaign was approved</summary>
		private DateTime ApprovedStatusDate
		{
			get{ return this.ApprovedStatusDateM;  }
			set{ this.ApprovedStatusDateM = value; }
		}

		protected string AccountNameM;
		///<summary>Gets or sets a value of the account name for a campaign.</summary>
		public string AccountName
		{
			get { return this.AccountNameM;  }
			set { this.AccountNameM = value; }
		}

		protected Common.PostalAddress ShipToAddressM;
		public Common.PostalAddress	ShipToAddress
		{
			get { return this.ShipToAddressM;  }
			set { this.ShipToAddressM = value; }
		}

		protected Common.PostalAddress BillToAddressM;
		public Common.PostalAddress	BillToAddress
		{
			get { return this.BillToAddressM;  }
			set { this.BillToAddressM = value; }
		}

		protected string AccountEmailM;
		///<summary>Gets or sets value indicating the email address for the campaign</summary>
		public string AccountEmail
		{
			get { return this.AccountEmailM;  }
			set { this.AccountEmailM = value; }
		}

		protected string ContactNameM;
		///<summary>Gets or sets value indicating the contact name for the campaign</summary>
		public string ContactName
		{
			get { return this.ContactNameM;  }
			set { this.ContactNameM = value; }
		}

		protected Common.PhoneNumber ContactPhoneM;
		///<summary>Gets or sets value indicating the contact phone # for the campaign</summary>
		public Common.PhoneNumber ContactPhone
		{
			get { return this.ContactPhoneM;  }
			set { this.ContactPhoneM = value; }
		}


		protected bool	_ValidGUIM;
		///<summary>Gets or sets value indicating if the CA has passsed user interface level validation</summary>
		public bool	ValidGUI
		{
			get{ return this._ValidGUIM;  }
			set{ this._ValidGUIM = value; }
		}

		protected bool	_ValidBIM;
		///<summary>Gets or sets value indicating if the CA has passsed biz intelligence level validation</summary>
		public bool	ValidBI
		{
			get { return this._ValidBIM;  }
			set {this._ValidBIM  = value; }
		}

		protected string _ErrorGUIM;
		///<summary>Gets or sets error string associatated with user interface level validation</summary>
		public string ErrorGUI
		{
			get { return this._ErrorGUIM;  }
			set { this._ErrorGUIM = value; }
		}

		protected string _ErrorBIM;
		///<summary>Gets or sets error string associatated with biz intelligence level validation</summary>
		public string ErrorBI
		{
			get { return this._ErrorBIM;   }
			set { this._ErrorBIM  = value; }
		}

		protected int ShipToAccountIDM = -1;
		///<summary>Gets or sets the account # to ship to</summary>
		public int ShipToAccountID
		{
			get { return this.ShipToAccountIDM;  }
			set { this.ShipToAccountIDM = value; }
		}

		protected int BillToAccountIDM = -1;
		///<summary>Gets or sets the account # to bill to</summary>
		public int BillToAccountID
		{
			get { return this.BillToAccountIDM;  }
			set { this.BillToAccountIDM = value; }
		}

		protected int ShipToCampaignContactIDM = -1;
		///<summary>Gets or sets</summary>
		public int ShipToCampaignContactID
		{
			get { return this.ShipToCampaignContactIDM;  }
			set { this.ShipToCampaignContactIDM = value; }
		}

		protected int BillToCampaignContactIDM = -1;
		///<summary>Gets or sets</summary>
		public int BillToCampaignContactID
		{
			get { return this.BillToCampaignContactIDM;  }
			set { this.BillToCampaignContactIDM = value; }
		}

		protected int FSCampaignContactIDM = -1;
		///<summary>Gets or sets</summary>
		public int FSCampaignContactID
		{
			get { return this.FSCampaignContactIDM;  }
			set { this.FSCampaignContactIDM = value; }
		}

		protected int FSShipToCampaignContactIDM = -1;
		///<summary>Gets or sets</summary>
		public int FSShipToCampaignContactID
		{
			get { return this.FSShipToCampaignContactIDM;  }
			set { this.FSShipToCampaignContactIDM = value; }
		}

		/*private bool CAccountCodeClass_AssignedM;*/
		protected Business.AccountClass CAccountCodeClassM;
		[DAL.DataColumn("CAccountCodeClass")]
		public Business.AccountClass CAccountCodeClass
		{
			get{ return this.CAccountCodeClassM; }
			set{ this.CAccountCodeClassM = value; /*this.CAccountCodeClass_AssignedM = true;*/  }
		}
		public string CAccountCodeClassStr
		{
			get
			{
				string retval;
				switch(this.CAccountCodeClass)
				{
					case Business.AccountClass.School:
						retval = "Sc";
						break;
					case Business.AccountClass.Sports_Clubs_Affinities:
						retval = "Sp";
						break;
					case Business.AccountClass.Non_School:
					default:
						retval = "NSc";
						break;
				}
				return retval;
			}
			set
			{
				switch(value.ToLower())
				{
					case "sc":
						this.CAccountCodeClass = Business.AccountClass.School;
						break;
					case "sp":
						this.CAccountCodeClass = Business.AccountClass.Sports_Clubs_Affinities;
						break;
					case "nsc":
					default:
						this.CAccountCodeClass = Business.AccountClass.Non_School;
						break;
				}
			}
		}
		public string CAccountCodeClassName
		{
			get
			{
				string retval;
				switch(this.CAccountCodeClass)
				{
					case Business.AccountClass.School:
						retval = "School";
						break;
					case Business.AccountClass.Sports_Clubs_Affinities:
						retval = "Sports/Clubs/Affinities";
						break;
					case Business.AccountClass.Non_School:
					default:
						retval = "Non School";
						break;
				}
				return retval;
			}
		}

		public string CAccountCodeGroupName
		{
			get
			{
				string retval;
				switch(this.CAccountCodeGroup)
				{
					case Business.AccountCode.Sc_Elementary:
						retval = "Elementary";
						break;
					case Business.AccountCode.Sc_High_School:
						retval = "High School";
						break;
					case Business.AccountCode.Sc_Junior_High_School:
						retval = "Junior High School";
						break;
					case Business.AccountCode.Sc_Middle_School:
						retval = "Middle School";
						break;
					case Business.AccountCode.Sc_Cegep:
						retval = "Cegep";
						break;
					case Business.AccountCode.Sc_College:
						retval = "College";
						break;
					case Business.AccountCode.Sc_University:
						retval = "University";
						break;
					case Business.AccountCode.Sc_School_Board:
						retval = "School Board";
						break;
					case Business.AccountCode.Sc_Adult:
						retval = "Adult";
						break;
					case Business.AccountCode.Sc_Vocational:
						retval = "Vocational";
						break;
					case Business.AccountCode.Sc_Other:
						retval = "Other";
						break;
					case Business.AccountCode.Sc_Combined:
						retval = "Combined";
						break;
					case Business.AccountCode.Sc_Pre_School:
						retval = "Pre-School";
						break;
					case Business.AccountCode.Sp_Ice_Skating:
						retval = "Ice Skating";
						break;
					case Business.AccountCode.Sp_Hockey:
						retval = "Hockey";
						break;
					case Business.AccountCode.Sp_Bowling:
						retval = "Bowling";
						break;
					case Business.AccountCode.Sp_Soccer:
						retval = "Soccer";
						break;
					case Business.AccountCode.Sp_Baseball:
						retval = "Baseball";
						break;
					case Business.AccountCode.Sp_Volleyball:
						retval = "Volleyball";
						break;
					case Business.AccountCode.Sp_Gymnastics:
						retval = "Gymnastics";
						break;
					case Business.AccountCode.Sp_Basketball:
						retval = "Basketball";
						break;
					case Business.AccountCode.Sp_Travel:
						retval = "Travel";
						break;
					case Business.AccountCode.Sp_Music_Band:
						retval = "Music Band";
						break;
					case Business.AccountCode.Sp_Theater:
						retval = "Theater";
						break;
					case Business.AccountCode.Sp_Athletics:
						retval = "Athletics";
						break;
					case Business.AccountCode.Sp_Dance:
						retval = "Dance";
						break;
					case Business.AccountCode.Sp_Karaté:
						retval = "Karaté";
						break;
					case Business.AccountCode.Sp_Curling:
						retval = "Curling";
						break;
					case Business.AccountCode.Sp_Equestrian:
						retval = "Equestrian";
						break;
					case Business.AccountCode.Sp_Aqua_Swim:
						retval = "Aqua/Swim";
						break;
					case Business.AccountCode.NSc_Daycare:
						retval = "Daycare";
						break;
					case Business.AccountCode.NSc_Gym:
						retval = "Gym";
						break;
					case Business.AccountCode.NSc_Scouts_Guides:
						retval = "Scouts/Guides";
						break;
					case Business.AccountCode.NSc_Company:
						retval = "Company";
						break;
					case Business.AccountCode.NSc_Church:
						retval = "Church";
						break;
					case Business.AccountCode.NSc_Lodge:
						retval = "Lodge";
						break;
					case Business.AccountCode.NSc_Other:
					default:
						retval = "Other";
						break;

				}
				return retval;
			}
		}

		/*private bool CAccountCodeGroup_AssignedM;*/
		protected Business.AccountCode CAccountCodeGroupM;
		[DAL.DataColumn("CAccountCodeGroup")]
		public Business.AccountCode CAccountCodeGroup
		{
			get{ return this.CAccountCodeGroupM; }
			set{ this.CAccountCodeGroupM = value; /*this.CAccountCodeGroup_AssignedM = true;*/  }
		}
		public string CAccountCodeGroupStr
		{
			get
			{
				string retval;
				switch(this.CAccountCodeGroup)
				{
					case Business.AccountCode.Sc_Elementary:
						retval = "Sc1";
						break;
					case Business.AccountCode.Sc_High_School:
						retval = "Sc2";
						break;
					case Business.AccountCode.Sc_Junior_High_School:
						retval = "Sc3";
						break;
					case Business.AccountCode.Sc_Middle_School:
						retval = "Sc4";
						break;
					case Business.AccountCode.Sc_Cegep:
						retval = "Sc5";
						break;
					case Business.AccountCode.Sc_College:
						retval = "Sc6";
						break;
					case Business.AccountCode.Sc_University:
						retval = "Sc7";
						break;
					case Business.AccountCode.Sc_School_Board:
						retval = "Sc8";
						break;
					case Business.AccountCode.Sc_Adult:
						retval = "Sc9";
						break;
					case Business.AccountCode.Sc_Vocational:
						retval = "Sc10";
						break;
					case Business.AccountCode.Sc_Other:
						retval = "Sc11";
						break;
					case Business.AccountCode.Sc_Combined:
						retval = "Sc12";
						break;
					case Business.AccountCode.Sc_Pre_School:
						retval = "Sc13";
						break;
					case Business.AccountCode.Sp_Ice_Skating:
						retval = "Sp1";
						break;
					case Business.AccountCode.Sp_Hockey:
						retval = "Sp2";
						break;
					case Business.AccountCode.Sp_Bowling:
						retval = "Sp3";
						break;
					case Business.AccountCode.Sp_Soccer:
						retval = "Sp4";
						break;
					case Business.AccountCode.Sp_Baseball:
						retval = "Sp5";
						break;
					case Business.AccountCode.Sp_Volleyball:
						retval = "Sp6";
						break;
					case Business.AccountCode.Sp_Gymnastics:
						retval = "Sp7";
						break;
					case Business.AccountCode.Sp_Basketball:
						retval = "Sp8";
						break;
					case Business.AccountCode.Sp_Travel:
						retval = "Sp9";
						break;
					case Business.AccountCode.Sp_Music_Band:
						retval = "Sp10";
						break;
					case Business.AccountCode.Sp_Theater:
						retval = "Sp11";
						break;
					case Business.AccountCode.Sp_Athletics:
						retval = "Sp12";
						break;
					case Business.AccountCode.Sp_Dance:
						retval = "Sp13";
						break;
					case Business.AccountCode.Sp_Karaté:
						retval = "Sp14";
						break;
					case Business.AccountCode.Sp_Curling:
						retval = "Sp15";
						break;
					case Business.AccountCode.Sp_Equestrian:
						retval = "Sp16";
						break;
					case Business.AccountCode.Sp_Aqua_Swim:
						retval = "Sp17";
						break;
					case Business.AccountCode.NSc_Daycare:
						retval = "NSc1";
						break;
					case Business.AccountCode.NSc_Gym:
						retval = "NSc2";
						break;
					case Business.AccountCode.NSc_Scouts_Guides:
						retval = "NSc3";
						break;
					case Business.AccountCode.NSc_Company:
						retval = "NSc4";
						break;
					case Business.AccountCode.NSc_Church:
						retval = "NSc5";
						break;
					case Business.AccountCode.NSc_Lodge:
						retval = "NSc6";
						break;
					case Business.AccountCode.NSc_Other:
					default:
						retval = "NSc7";
						break;
				}
				return retval;
			}
			set
			{
				switch (value.ToLower())
				{
					case "sc1":
						this.CAccountCodeGroup = Business.AccountCode.Sc_Elementary;
						break;
					case "sc2":
						this.CAccountCodeGroup = Business.AccountCode.Sc_High_School;
						break;
					case "sc3":
						this.CAccountCodeGroup = Business.AccountCode.Sc_Junior_High_School;
						break;
					case "sc4":
						this.CAccountCodeGroup = Business.AccountCode.Sc_Middle_School;
						break;
					case "sc5":
						this.CAccountCodeGroup = Business.AccountCode.Sc_Cegep;
						break;
					case "sc6":
						this.CAccountCodeGroup = Business.AccountCode.Sc_College;
						break;
					case "sc7":
						this.CAccountCodeGroup = Business.AccountCode.Sc_University;
						break;
					case "sc8":
						this.CAccountCodeGroup = Business.AccountCode.Sc_School_Board;
						break;
					case "sc9":
						this.CAccountCodeGroup = Business.AccountCode.Sc_Adult;
						break;
					case "sc10":
						this.CAccountCodeGroup = Business.AccountCode.Sc_Vocational;
						break;
					case "sc11":
						this.CAccountCodeGroup = Business.AccountCode.Sc_Other;
						break;
					case "sc12":
						this.CAccountCodeGroup = Business.AccountCode.Sc_Combined;
						break;
					case "sc13":
						this.CAccountCodeGroup = Business.AccountCode.Sc_Pre_School;
						break;
					case "sp1":
						this.CAccountCodeGroup = Business.AccountCode.Sp_Ice_Skating;
						break;
					case "sp2":
						this.CAccountCodeGroup = Business.AccountCode.Sp_Hockey;
						break;
					case "sp3":
						this.CAccountCodeGroup = Business.AccountCode.Sp_Bowling;
						break;
					case "sp4":
						this.CAccountCodeGroup = Business.AccountCode.Sp_Soccer;
						break;
					case "sp5":
						this.CAccountCodeGroup = Business.AccountCode.Sp_Baseball;
						break;
					case "sp6":
						this.CAccountCodeGroup = Business.AccountCode.Sp_Volleyball;
						break;
					case "sp7":
						this.CAccountCodeGroup = Business.AccountCode.Sp_Gymnastics;
						break;
					case "sp8":
						this.CAccountCodeGroup = Business.AccountCode.Sp_Basketball;
						break;
					case "sp9":
						this.CAccountCodeGroup = Business.AccountCode.Sp_Travel;
						break;
					case "sp10":
						this.CAccountCodeGroup = Business.AccountCode.Sp_Music_Band;
						break;
					case "sp11":
						this.CAccountCodeGroup = Business.AccountCode.Sp_Theater;
						break;
					case "sp12":
						this.CAccountCodeGroup = Business.AccountCode.Sp_Athletics;
						break;
					case "sp13":
						this.CAccountCodeGroup = Business.AccountCode.Sp_Dance;
						break;
					case "sp14":
						this.CAccountCodeGroup = Business.AccountCode.Sp_Karaté;
						break;
					case "sp15":
						this.CAccountCodeGroup = Business.AccountCode.Sp_Curling;
						break;
					case "sp16":
						this.CAccountCodeGroup = Business.AccountCode.Sp_Equestrian;
						break;
					case "sp17":
						this.CAccountCodeGroup = Business.AccountCode.Sp_Aqua_Swim;
						break;
					case "nsc1":
						this.CAccountCodeGroup = Business.AccountCode.NSc_Daycare;
						break;
					case "nsc2":
						this.CAccountCodeGroup = Business.AccountCode.NSc_Gym;
						break;
					case "nsc3":
						this.CAccountCodeGroup = Business.AccountCode.NSc_Scouts_Guides;
						break;
					case "nsc4":
						this.CAccountCodeGroup = Business.AccountCode.NSc_Company;
						break;
					case "nsc5":
						this.CAccountCodeGroup = Business.AccountCode.NSc_Church;
						break;
					case "nsc6":
						this.CAccountCodeGroup = Business.AccountCode.NSc_Lodge;
						break;
					case "nsc7":
					default:
						this.CAccountCodeGroup = Business.AccountCode.NSc_Other;
						break;
				}
			}


		}

		protected DAL.CampaignDataAccess aCampaignTableM = new DAL.CampaignDataAccess();
		#endregion variables

		#region ValidateAndSave
		/// <summary>Check the CA for compliance with biz rules, then save it</summary>
		/// <returns>bool: did both pieces work ? </returns>
		override public bool ValidateAndSave()
		{
			if(this.Validate() == true)
			{
				return this.Save();
			}
			else
			{
				return false;
			}
		}


		///<summary>Save a CA to the db</summary>
		///<returns>bool: Was saving successful ? </returns>
		public bool Save()
		{
			DAL.CampaignDataAccess aCampaignTable = new DAL.CampaignDataAccess();
			bool blSave = false;
			this.DateChanged = DateTime.Now;
//			string msg = "start" + this.TermsStartDateM.ToString() + "<br>";
//			this.ErrorBI += msg;//System.Diagnostics.Assert(false, msg);
//			msg = "end" + this.TermsEndDateM.ToString() + "<br>";
//			this.ErrorBI += msg;//System.Diagnostics.Assert(false, msg);
//			msg = "FS" + this.FieldSupplyDeliveryDateM.ToString() + "<br>";
//			this.ErrorBI += msg;//System.Diagnostics.Assert(false, msg);
//			msg = "dateChangeM " + this.dateChangedM.ToString() + "<br>";
//			msg += "MagnetStatementDate " + this.MagnetStatementDate.ToString() + "<br>";
//			this.ErrorBI += msg;

			#region Insert or Update
				if(this.CampaignID == -1)
				{
					/* Set Country ? */
					blSave = aCampaignTable.Insert(
						out this.CampaignIDM,
						this.CampaignStatusInt,
						this.FMID,
						this.dateChangedM.ToString(),
						this.Language,
						this.TermsStartDateM,
						this.TermsEndDateM,
						this.BillIncentivesToInt,
						this.BillToAccountID,
						this.ShipToAccountID,
						this.EstimatedGrossM,
						this.StudentsParticipatingM,
						this.RoomsOrTeamsM,
						this.StaffAmountM,
						this.SpecialInstructions,
						this.IsStaffOrder,
						this.StaffOrderDiscount,
						this.IsTestCampaign,
						this.DateChanged,
						this.UserIDModifiedM, /*Convert.ToInt32(this.UserIDChanged), <remarks>this var is a string, db wants an int</remarks>*/
						this.IsPayLater,
						this.IncentivesDistributionIDInt,
						this.MagnetStatementDate,
						this.ContactName,
						this.ContactPhone.GetNumberRaw
						);

				}
				else
				{
					blSave = aCampaignTable.Update(
						this.CampaignIDM,
						this.CampaignStatusInt,
						this.FMID,
						this.dateChangedM.ToString(),
						this.Language,
						this.TermsStartDateM,
						this.TermsEndDateM,
						this.BillIncentivesToInt,
						this.BillToAccountID,
						this.ShipToAccountID,
						this.EstimatedGrossM,
						this.StudentsParticipatingM,
						this.RoomsOrTeamsM,
						this.StaffAmountM,
						this.SpecialInstructionsM,
						this.IsStaffOrder,
						this.StaffOrderDiscount,
						this.IsTestCampaign,
						this.dateChangedM,
						this.UserIDModifiedM, //Convert.ToInt32(this.UserIDChanged),
						this.IsPayLater,
						this.IncentivesDistributionIDInt,
//						this.FSOrderRecCreated,
						this.MagnetStatementDate,
						this.ContactName,
						this.ContactPhone.GetNumberRaw
						);
				}
				#endregion Insert or Update

			return blSave;
		}


		/// <summary>Check for compliance with Business Intelligence rules</summary>
		/// <returns>bool: Was validation successful ? </returns>
		public bool Validate()
		{
			/* setup variables to track validation */
			bool blValid = true;
			string stError = "";

			/* Check the CA for completeness */
			stError += Validate_MandatorySelections(ref blValid);

			/* Now that the CA has been checked for completeness, check for compliance with business rules */
			//Check that the dates entered meet various rules
			stError += Validate_CheckDates(ref blValid);

			/* Save the validation results into the CA object */
			this.ValidBI = blValid;
			this.ErrorBI = stError;

			return blValid;
		}

		#endregion

		#region Validate_MandatorySelections functions
		private string Validate_MandatorySelections(ref bool blValid)
		{
			string stError = "";

			///<businessIntelligence>Contact info (contact name) for a campaign is mandatory</businessIntelligence>
			if (this.ContactNameM == "")
			{
				blValid = false;
				stError += "Please enter a contact name\r\n";
			}

			///<businessIntelligence>Contact info (contact phone #) for a campaign is mandatory</businessIntelligence>
			if (this.ContactPhoneM.GetNumber == "")
			{
				blValid = false;
				stError += "Please enter a contact phone number\r\n";
			}

			///<businessIntelligence>Start Date for a campaign is mandatory</businessIntelligence>
			if (this.TermsStartDateM == System.DateTime.MinValue)
			{
				blValid = false;
				stError += "Please enter a start date\r\n";
			}

			///<businessIntelligence>End Date for a campaign is mandatory</businessIntelligence>
			if (this.TermsEndDateM == System.DateTime.MinValue)
			{
				blValid = false;
				stError += "Please enter an end date\r\n";
			}

			///<businessIntelligence>Estimated Gross for a campaign is mandatory</businessIntelligence>
			if (this.EstimatedGrossM < 1)
			{
				blValid = false;
				stError += "Please enter an Estimated Gross\r\n";
			}

			///<businessIntelligence>Number of Students Participating is mandatory</businessIntelligence>
			if (this.StudentsParticipatingM < 1)
			{
				blValid = false;
				stError += "Please enter the Number of Students Participating\r\n";
			}

			///<businessIntelligence>Number of Classrooms or Teams is mandatory</businessIntelligence>
			if (this.RoomsOrTeamsM < 1)
			{
				blValid = false;
				stError += "Please enter the Number of Rooms or Teams\r\n";
			}

			///<businessIntelligence>Number of Staff is mandatory</businessIntelligence>
			if (this.StaffAmountM < 1)
			{
				blValid = false;
				stError += "Please enter the Number of Staff\r\n";
			}

			///<businessIntelligence>Atleast one reward distribution program selection is mandatory</businessIntelligence>
			//if ((this.cbIncentiveParticipantBag == false)&&(this.cbIncentiveClassroomBox == false))
			if(this.IncentivesDistributionIDM == IncentivesDistribution.Undefined)
			{
				blValid = false;
				stError += "Please select at least one Reward Distribution program\r\n";
			}

			return stError;
		}

		#endregion

		#region Validate_CheckDates functions
		private string Validate_CheckDates(ref bool blValid)
		{
			string stError = "";

			///<businessIntelligence>Start date of campaign is 30 days prior to current date</businessIntelligence>
			stError += Validate_CheckDates_StartDate(ref blValid, 30);
			///<businessIntelligence>End date of campaign is prior to start date of campaign</businessIntelligence>
			stError += Validate_CheckDates_EndDate(ref blValid);
			///<businessIntelligence>Field supply date cannot be met because current date + lag time for that province exceeds the delivery date</businessIntelligence>
			//stError += Validate_CheckDates_FieldSupplies(ref blValid, 15);

			return stError;
		}

		private string Validate_CheckDates_StartDate(ref bool blValid, int daysPrior)
		{
			string stError = "";
			System.DateTime NOW = new DateTime();
			NOW = DateTime.Now;
			TimeSpan span = new TimeSpan(NOW.Ticks - this.TermsStartDateM.Ticks);
			if (span.Days > daysPrior)
			{
				blValid = false;
				stError += "The start date was more than " + daysPrior.ToString() + " before the current date\r\n";
			}

			return stError;
		}

		private string Validate_CheckDates_EndDate(ref bool blValid)
		{
			string stError = "";
			if (this.TermsEndDateM.Date < this.TermsStartDateM.Date)
			{
				blValid = false;
				stError += "The end date was prior to the start date\r\n";
			}
			return stError;
		}
	
		#endregion

		#region Populate Functions
		public void PopulateFromDBCampaign()
		{
			DataTable dt = aCampaignTableM.Exists(this.CampaignID);
			string rowsStr = dt.Rows.Count.ToString();
			if(dt.Rows.Count < 1)
			{
				throw new ArgumentException("There is no data for the CampaignID", this.CampaignID.ToString());
			}
			else if(dt.Rows.Count > 1)
			{
				rowsStr += " rows of data were returned when 1 was expected";
				throw new ArgumentException(rowsStr, this.CampaignID.ToString());
			}
			else
			{
				/* one row returned, good stuff */
				//two are conver.toDecimal - swtich to int !

				//cant use this method right now
				//DataRow dr = dt.Rows[0];
				//Fill(dr,this.GetType());

				#region Set class values
				DataRow DR = dt.Rows[0];
				this.CampaignID			= Convert.ToInt32(DR["CampaignID"]);
				this.CampaignStatusInt	= Convert.ToInt32(DR["Status"]);
				this.Country			= Convert.ToString(DR["Country"]);
				this.FMID				= Convert.ToString(DR["FMID"]);
				this.Language			= Convert.ToString(DR["Lang"]);
				this.TermsStartDate		= Convert.ToDateTime(DR["StartDate"]);
				this.TermsEndDate		= Convert.ToDateTime(DR["EndDate"]);
				this.BillIncentivesToInt = Convert.ToInt32(DR["IncentivesBillToID"]);
				this.IncentivesDistributionIDInt = Convert.ToInt32(DR["IncentivesDistributionID"]);
				this.ShipToCampaignContactID = Convert.ToInt32(DR["ShipToCampaignContactID"]);
				this.ShipToAccountID = Convert.ToInt32(DR["ShipToAccountID"]);
				this.BillToAccountID = Convert.ToInt32(DR["BillToAccountID"]);
				this.EstimatedGross = Convert.ToInt32(Convert.ToDecimal(DR["EstimatedGross"]));
				this.StudentsParticipating  = Convert.ToInt32(DR["NumberOfParticipants"]);
				this.RoomsOrTeams = Convert.ToInt32(DR["NumberOfClassroooms"]);
				this.StaffAmount = Convert.ToInt32(DR["NumberOfStaff"]);
				this.BillToCampaignContactID = Convert.ToInt32(DR["BillToCampaignContactID"]);
				this.FSCampaignContactID = Convert.ToInt32(DR["SuppliesCampaignContactID"]);
				this.FSShipToCampaignContactID = Convert.ToInt32(DR["SuppliesShipToCampaignContactID"]);
				this.SpecialInstructions = Convert.ToString(DR["SpecialInstructions"]);
				this.IsStaffOrder = Convert.ToBoolean(DR["IsStaffOrder"]);
				this.StaffOrderDiscount = Convert.ToInt32(Convert.ToDecimal(DR["StaffOrderDiscount"]));
				this.IsTestCampaign = Convert.ToBoolean(DR["IsTestCampaign"]);
				this.IsPayLater = Convert.ToBoolean(DR["IsPayLater"]);
				this.ApprovedStatusDate = Convert.ToDateTime(DR["ApprovedStatusDate"]);
				this.MagnetStatementDate = Convert.ToDateTime(DR["MagnetStatementDate"]);
				//this.Account_ID = Convert.ToInt32(DR["AccountId"]);
				this.AccountName = Convert.ToString(DR["AccountName"]);
				this.ShipToAddress = new Common.PostalAddress(
					Convert.ToInt32(DR["ShipToAddrId"]),
					Convert.ToString(DR["ShipToAddress1"]),
					Convert.ToString(DR["ShipToAddress2"]),
					Convert.ToString(DR["ShipToCity"]),
					Convert.ToString(DR["ShipToState"]),
					Convert.ToString(DR["ShipToZip"]),
					Convert.ToString(DR["ShipToZip4"]),
					Convert.ToString(DR["ShipToCountry"]));
				this.BillToAddress = new Common.PostalAddress(
					Convert.ToInt32(DR["BillToAddrId"]),
					Convert.ToString(DR["BillToAddress1"]),
					Convert.ToString(DR["BillToAddress2"]),
					Convert.ToString(DR["BillToCity"]),
					Convert.ToString(DR["BillToState"]),
					Convert.ToString(DR["BillToZip"]),
					Convert.ToString(DR["BillToZip4"]),
					Convert.ToString(DR["BillToCountry"]));
				this.CAccountCodeClassStr = Convert.ToString(DR["AccountClass"]);
				this.CAccountCodeGroupStr = Convert.ToString(DR["AccountCode"]);
				this.AccountPhone = new Common.PhoneNumber(Convert.ToString(DR["AccountPhone"]));
				this.AccountFax   = new Common.PhoneNumber(Convert.ToString(DR["AccountFax"]));
				this.AccountEmail = Convert.ToString(DR["AccountEmail"]);
				this.ContactName  = Convert.ToString(DR["ContactName"]);
				this.ContactPhone = new Common.PhoneNumber(Convert.ToString(DR["ContactPhone"]));
				this.DateChanged = Convert.ToDateTime(DR["DateModified"]);
				this.UserIDChanged = Convert.ToInt32(DR["UserIDModified"]).ToString();
				#endregion Set class values
			}
		}


		public void PopulateFromDBAccount()
		{
			//DataTable dt = aCampaignTableM.AccountForNewCampaign(this.Account_ID);
			//this.BillToAccountID = this.Account_ID;
			//this.ShipToAccountID = this.Account_ID;

			if(this.BillToAccountID != this.ShipToAccountID)
			{
				throw new ArgumentException("Programmer Error, Billto and Shipto AccountIDs need to be the same here","AccountID");
			}
			DataTable dt = aCampaignTableM.AccountForNewCampaign(this.ShipToAccountID);
			string rowsStr = dt.Rows.Count.ToString();
			if(dt.Rows.Count < 1)
			{
				throw new ArgumentException("There is no data for the ShipToAccountID", this.ShipToAccountID.ToString());
			}
			else if(dt.Rows.Count > 1)
			{
				rowsStr += " rows of data were returned when 1 was expected";
				throw new ArgumentException(rowsStr, this.ShipToAccountID.ToString());
			}
			else
			{
				/* one row returned, good stuff */
				#region Set class values
				DataRow DR = dt.Rows[0];
				this.AccountName = Convert.ToString(DR["AccountName"]);
				this.ShipToAddress = new Common.PostalAddress(
					Convert.ToInt32(DR["ShipToAddrId"]),
					Convert.ToString(DR["ShipToAddress1"]),
					Convert.ToString(DR["ShipToAddress2"]),
					Convert.ToString(DR["ShipToCity"]),
					Convert.ToString(DR["ShipToState"]),
					Convert.ToString(DR["ShipToZip"]),
					Convert.ToString(DR["ShipToZip4"]),
					Convert.ToString(DR["ShipToCountry"]));
				this.BillToAddress = new Common.PostalAddress(
					Convert.ToInt32(DR["BillToAddrId"]),
					Convert.ToString(DR["BillToAddress1"]),
					Convert.ToString(DR["BillToAddress2"]),
					Convert.ToString(DR["BillToCity"]),
					Convert.ToString(DR["BillToState"]),
					Convert.ToString(DR["BillToZip"]),
					Convert.ToString(DR["BillToZip4"]),
					Convert.ToString(DR["BillToCountry"]));
				this.CAccountCodeClassStr = Convert.ToString(DR["AccountClass"]);
				this.CAccountCodeGroupStr = Convert.ToString(DR["AccountCode"]);
				this.AccountPhone	= new Common.PhoneNumber(Convert.ToString(DR["AccountPhone"]));
				this.AccountFax		= new Common.PhoneNumber(Convert.ToString(DR["AccountFax"]));
				this.AccountEmail	= Convert.ToString(DR["AccountEmail"]);
				this.ContactName	= Convert.ToString(DR["ContactName"]);
				this.Language		= Convert.ToString(DR["Lang"]);
				this.TermsStartDate = DateTime.MinValue;
				this.TermsEndDate   = DateTime.MinValue;
				this.FMID           = "";
				#endregion Set class values
			}
		}

		#endregion Populate Functions
	}
}
