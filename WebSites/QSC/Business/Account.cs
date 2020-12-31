using System;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.ComponentModel;
using DAL;

namespace Business
{
	///<summary>Data representation of an account for a fundraising group/organization.</summary>
	public class Account : QBusinessObject
	{
		#region Class Members

		protected int IDM=-1;
		[DAL.DataColumn("Account_ID")]
		public int Account_ID
		{
			get{ return this.IDM; }
			set{ this.IDM=value;  }
		}
		protected string NameM;
		[DAL.DataColumn("Name")]
		public string Name
		{
			get{ return this.NameM; }
			set{ this.NameM=value;  }
		}
		private bool Country_AssignedM;
		protected string CountryM;
		[DAL.DataColumn("Country")]
		public string Country
		{
			get{ return this.CountryM; }
			set{ this.CountryM=value; this.Country_AssignedM = true; }
		}
		protected string LangM;
		[DAL.DataColumn("Lang")]
		public string Lang
		{
			get{ return this.LangM; }
			set{ this.LangM=value;  }
		}
		private bool CAccountCodeClass_AssignedM;
		protected Business.AccountClass CAccountCodeClassM;
		[DAL.DataColumn("CAccountCodeClass")]
		public Business.AccountClass CAccountCodeClass
		{
			get{ return this.CAccountCodeClassM; }
			set{ this.CAccountCodeClassM = value; this.CAccountCodeClass_AssignedM = true;  }
		}
		public string CAccountCodeClassStr
		{
			get
			{
				if(this.CAccountCodeClassM == Business.AccountClass.School)
				{
					return "Sc";
				}
				else if(this.CAccountCodeClassM == Business.AccountClass.Sports_Clubs_Affinities)
				{
					return "Sp";
				}
				else if(this.CAccountCodeClassM == Business.AccountClass.Non_School)
				{
					return "NSc";
				}
				else
				{
					return "Undefined";
				}
			}
			set
			{
				if(value.ToLower() == "sc")
				{
					this.CAccountCodeClassM = Business.AccountClass.School;
					this.CAccountCodeClass_AssignedM = true;
				}
				else if(value.ToLower() == "other")
				{
					this.CAccountCodeClassM = Business.AccountClass.Sports_Clubs_Affinities;
					this.CAccountCodeClass_AssignedM = true;
				}
				else
				{
					this.CAccountCodeClassM = Business.AccountClass.Non_School;
					this.CAccountCodeClass_AssignedM = true;
				}
			}
		}
		protected string CAccountCodeClass_OtherSpecifyM;
		public string CAccountCodeClass_OtherSpecify
		{
			get{ return this.CAccountCodeClass_OtherSpecifyM; }
			set{ this.CAccountCodeClass_OtherSpecifyM=value;  }
		}

		private bool CAccountCodeGroup_AssignedM;
		protected Business.AccountCode CAccountCodeGroupM;
		[DAL.DataColumn("CAccountCodeGroup")]
		public Business.AccountCode CAccountCodeGroup
		{
			get{ return this.CAccountCodeGroupM; }
			set{ this.CAccountCodeGroupM = value; this.CAccountCodeGroup_AssignedM = true;  }
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
		protected string CAccountCodeGroup_OtherSpecifyM;
		public string CAccountCodeGroup_OtherSpecify
		{
			get{ return this.CAccountCodeGroup_OtherSpecifyM; }
			set{ this.CAccountCodeGroup_OtherSpecifyM=value;  }
		}


		protected int PhoneListIDM = -1;
		[DAL.DataColumn("PhoneListID")]
		public int PhoneListID
		{
			get{ return this.PhoneListIDM; }
			set{ this.PhoneListIDM=value;  }
		}
		protected int AddressListIDM = -1;
		[DAL.DataColumn("AddressListID")]
		public int AddressListID
		{
			get{ return this.AddressListIDM; }
			set{ this.AddressListIDM=value;  }
		}
		protected Business.AccountStatus StatusIDM = Business.AccountStatus.Pending;
		[DAL.DataColumn("StatusID")]
		public int StatusID
		{
			get{ return (int) this.StatusIDM; }
			set{ this.StatusIDM = (Business.AccountStatus) value;  }
		}
		protected int EnrollmentM;
		[DAL.DataColumn("Enrollment")]
		public int Enrollment
		{
			get{ return this.EnrollmentM; }
			set{ this.EnrollmentM=value;  }
		}
		protected string CommentM;
		[DAL.DataColumn("Comment")]
		public string Comment
		{
			get{ return this.CommentM; }
			set{ this.CommentM=value;  }
		}
		protected string EMailM;
		[DAL.DataColumn("EMail")]
		public string EMail
		{
			get{ return this.EMailM; }
			set{ this.EMailM=value;  }
		}
		protected bool IsPrivateOrgM;
		[DAL.DataColumn("IsPrivateOrg")]
		public bool IsPrivateOrg
		{
			get{ return this.IsPrivateOrgM; }
			set{ this.IsPrivateOrgM=value;  }
		}
		[DAL.DataColumn("IsPrivateOrg")]
		public string IsPrivateOrgStr
		{
			get{
				if(this.IsPrivateOrgM == true){ return "1";}
				else {return "0"; }
			}
			set
			{
				if((value.ToLower() == "y")||(value.ToLower() == "1")||(value.ToLower() == "true")){ this.IsPrivateOrgM=true;  }
				else if((value.ToLower() == "n")||(value.ToLower() == "0")||(value.ToLower() == "false")){ this.IsPrivateOrgM=false;  }
				else { throw new System.FormatException("IsPrivateOrgStr String could not be converted to a valid Boolean"); }
			}
		}

		protected bool IsAdultGroupM;
		[DAL.DataColumn("IsAdultGroup")]
		public bool IsAdultGroup
		{
			get{ return this.IsAdultGroupM; }
			set{ this.IsAdultGroupM=value;  }
		}
		[DAL.DataColumn("IsAdultGroup")]
		public string IsAdultGroupStr
		{
			get
			{
				if(this.IsAdultGroupM == true){ return "1";}
				else {return "0"; }
			}
			set
			{
				if((value.ToLower() == "y")||(value.ToLower() == "1")||(value.ToLower() == "true")){ this.IsAdultGroupM=true;  }
				else if((value.ToLower() == "n")||(value.ToLower() == "0")||(value.ToLower() == "false")){ this.IsAdultGroupM=false;  }
				else { throw new System.FormatException("IsAdultGroupStr String could not be converted to a valid Boolean"); }
			}
		}
		protected int ParentIDM;
		[DAL.DataColumn("ParentID")]
		public int ParentID
		{
			get{ return this.ParentIDM; }
			set{ this.ParentIDM=value;  }
		}
		protected int SalesRegionIDM;
		[DAL.DataColumn("SalesRegionID")]
		public int SalesRegionID
		{
			get{ return this.SalesRegionIDM; }
			set{ this.SalesRegionIDM=value;  }
		}
		protected int StatementPrintCycleIDM;
		[DAL.DataColumn("StatementPrintCycleID")]
		public int StatementPrintCycleID
		{
			get{ return this.StatementPrintCycleIDM; }
			set{ this.StatementPrintCycleIDM=value;  }
		}
		protected int StatementPrintSlotM;
		[DAL.DataColumn("StatementPrintSlot")]
		public int StatementPrintSlot
		{
			get{ return this.StatementPrintSlotM; }
			set{ this.StatementPrintSlotM=value;  }
		}
//		protected DateTime DateCreatedM;
//		[DAL.DataColumn("DateCreated")]
//		public DateTime DateCreated
//		{
//			get{ return this.DateCreatedM; }
//			set{ this.DateCreatedM=value;  }
//		}

//		protected string userIDCreatedM;
//		[DAL.DataColumn("UserIDCreated")]
//		public string UserIDCreated
//		{
//			get{ return this.UserIDCreatedM; }
//			set{ this.UserIDCreatedM=value;  }
//		}
		protected DateTime DateUpdatedM;
		[DAL.DataColumn("DateUpdated")]
		public DateTime DateUpdated
		{
			get{ return this.DateUpdatedM; }
			set{ this.DateUpdatedM=value;  }
		}
//		[DAL.DataColumn("DateUpdated")]
//		public object DateUpdatedObj
//		{
//			get{ return this.DateUpdatedM; }
//			set{ if(value != null) this.DateUpdatedM= Convert.ToDateTime(value);  }
//		}
		protected string UserIDUpdatedM;
		[DAL.DataColumn("UserIDUpdated")]
		public string UserIDUpdated
		{
			get{ return this.UserIDUpdatedM; }
			set{ this.UserIDUpdatedM=value;  }
		}


		private bool _VendorNumberM_assigned = false;
		protected string VendorNumberM;
		[DAL.DataColumn("VendorNumber")]
		public string VendorNumber
		{
			get{ return this.VendorNumberM; }
			set
			{ 
				this.VendorNumberM=value; 
				this._VendorNumberM_assigned = true; 
			}
		}
		
		private bool _VendorSiteNameM_assigned = false;
		protected string VendorSiteNameM;
		[DAL.DataColumn("VendorSiteName")]
		public string VendorSiteName
		{
			get{ return this.VendorSiteNameM; }
			set
			{ 
				this.VendorSiteNameM=value; 
				this._VendorNumberM_assigned = true; 
			}
		}
		
		private bool _VendorPayGroupM_assigned = false;
		protected string VendorPayGroupM;
		[DAL.DataColumn("VendorPayGroup")]
		public string VendorPayGroup
		{
			get{ return this.VendorPayGroupM; }
			set
			{
				this.VendorPayGroupM=value;
				this._VendorPayGroupM_assigned = true; 
			}
		}
		
		protected string SponsorM;
		[DAL.DataColumn("Sponsor")]
		public string Sponsor
		{
			get{ return this.SponsorM; }
			set{ this.SponsorM=value;  }
		}

		protected System.Collections.ArrayList PostalAddressesM;
		public System.Collections.ArrayList PostalAddresses
		{
			get{ return this.PostalAddressesM; }
			set{ this.PostalAddressesM=value;  }
		}
		protected System.Collections.ArrayList PhoneNumbersM;
		public System.Collections.ArrayList PhoneNumbers
		{
			get{ return this.PhoneNumbersM; }
			set{ this.PhoneNumbersM=value;  }
		}
		private bool	ValidGUIM;
		///<summary>Gets or sets value indicating if the CA has passsed user interface level validation</summary>
		public bool		ValidGUI	{ get{ return this.ValidGUIM; } set{this.ValidGUIM = value; } }

		private bool	ValidBIM;
		///<summary>Gets or sets value indicating if the CA has passsed biz intelligence level validation</summary>
		public bool		ValidBI		{ get{ return this.ValidBIM;  } set{this.ValidBIM  = value; } }

		private string	ErrorGUIM;
		///<summary>Gets or sets error string associatated with user interface level validation</summary>
		public string	ErrorGUI	{ get{ return this.ErrorGUIM; } set{this.ErrorGUIM = value; } }

		private string	ErrorBIM;
		///<summary>Gets or sets error string associatated with biz intelligence level validation</summary>
		public string	ErrorBI		{ get{ return this.ErrorBIM;  } set{this.ErrorBIM  = value; } }

		#endregion

		#region Constructors
		protected DAL.AccountDataAccess aTable;
		protected DAL.PhoneDataAccess aPhone;
		protected DAL.AddressDataAccess aPostalAddress;
		///<summary>default constructor</summary>
		public Account()
		{
			this.IDM = -1;
			this.CAccountCodeClass_AssignedM = false;
			this.CAccountCodeGroup_AssignedM = false;
			this.Country_AssignedM = false;
			aTable = new DAL.AccountDataAccess();
			aPhone = new DAL.PhoneDataAccess();
			aPostalAddress = new DAL.AddressDataAccess();
		}
		///<summary>constructor</summary>
		public Account(int Account_ID)
		{
			this.IDM = Account_ID;
			this.CAccountCodeClass_AssignedM = false;
			this.CAccountCodeGroup_AssignedM = false;
			this.Country_AssignedM = false;
			aTable = new DAL.AccountDataAccess();
			aPhone = new DAL.PhoneDataAccess();
			aPostalAddress = new DAL.AddressDataAccess();
		}
		#endregion

		#region ValidateAndSave
		///<summary>check it then submit it</summary>
		override public bool ValidateAndSave()
		{
			if(this.Validate() == true)
			{
				this.ErrorBI = "this.Validate() returned true, lets try a save !<br>" + this.ErrorBI;
				return this.Save();
			}
			else
			{
				return false;
			}
		}

		///<summary>Check for compliance with biz rules</summary>
		public bool Validate()
		{

			/* setup variables to track validation */
			bool blValid = true;
			string stError = "";

			/* Check the Account for completeness */
			stError += Validate_MandatorySelections(ref blValid);

			/* Check for completeness within the Postal Addresses */
			///<businessIntelligence>Each Address entered must be complete</businessIntelligence>
			for (int i = 0; i < this.PostalAddressesM.Count; i++ )
			{
				try
				{
					if( ((Common.PostalAddress)this.PostalAddressesM[i]).Validate() == false)
					{
						blValid = false;
						stError += "Some of the Postal Addresses entered were incomplete\r\n";
						break;
					}
				}
				catch(System.InvalidCastException){}
			}

			/* Save the validation results into the Account object */
			this.ValidBIM = blValid;
			this.ErrorBIM = stError;

			if((this.ValidBIM == true)&&(this.ValidGUIM == true))
			{
				this.StatusIDM = Business.AccountStatus.Active;
			}
			else
			{
				this.StatusIDM = Business.AccountStatus.Pending;
			}

			return blValid;
		}


		///<summary>Save an Account to the db</summary>
		///<returns>bool: Was saving successful ? </returns>
		public bool Save()
		{
			return ( Save_Account() && Save_PhoneNumbers() && Save_PostalAddresses() );
		}

		public void SetCountry()
		{
			for(int i=0; i < this.PostalAddressesM.Count; i++)
			{
				if(((Common.PostalAddress)this.PostalAddressesM[i]).Type == ((int)(Business.AddressType.ShipTo)) )
				{
					if(this.Country_AssignedM == false)
					{
						this.Country = ((Common.PostalAddress)this.PostalAddressesM[i]).Country;
						break;
					}
				}
			}
		}

		///<summary>Save the core piece of an Account (the CAccount record) to the db</summary>
		///<returns>bool: Was saving successful ? </returns>
		private bool Save_Account()
		{
			bool blSave = false;
			this.ErrorBI += "this.Save_Account() IDM (before):" + IDM.ToString() + "\r\n";
			if(IDM == -1)
			{
				this.ErrorBI += "this.Save_Account() in insert\r\n";
				SetCountry();
				
				
				//switch "" to null, on insert only
				//for vendor fields
				if((_VendorNumberM_assigned == false)||(VendorNumberM == ""))
				{					
					VendorNumberM = null;
				}
				if((_VendorSiteNameM_assigned == false)||(VendorSiteNameM == ""))
				{					
					VendorSiteNameM = null;
				}
				if((_VendorPayGroupM_assigned == false)||(VendorPayGroupM == ""))
				{					
					VendorPayGroupM = null;
				}

				blSave = aTable.Insert(out IDM, NameM, CountryM, LangM, CAccountCodeClassStr, CAccountCodeGroupStr, out PhoneListIDM, out AddressListIDM, StatusID, EnrollmentM, CommentM, EMailM, IsPrivateOrgM, IsAdultGroupM, SponsorM, ParentIDM, UserIDModified,VendorNumberM,VendorSiteNameM,VendorPayGroupM);
			}
			else
			{
				this.ErrorBI += "this.Save_Account() in update\r\n";
				blSave = aTable.Update(IDM, NameM, LangM, CAccountCodeClassStr, CAccountCodeGroupStr, StatusID, EnrollmentM, CommentM, EMailM, IsPrivateOrgM, IsAdultGroupM, SponsorM, ParentIDM, UserIDModified);
			}
			this.ErrorBI += "this.Save_Account() IDM (after):" + IDM.ToString() + "\r\n";
			this.ErrorBI += "this.Save_Account() PhoneListIDM:" + PhoneListIDM.ToString() + "\r\n";
			this.ErrorBI += "this.Save_Account() AddressListIDM:" + AddressListIDM.ToString() + "\r\n";
			this.ErrorBI += "this.Save_Account() blSave:" + blSave.ToString() + "\r\n";
			return blSave;
		}

		///<summary>Save the Account's phone numbers to the db</summary>
		///<returns>bool: Was saving successful ? </returns>
		private bool Save_PhoneNumbers()
		{
			bool result = false;
			Common.PhoneNumber oEntry;
			for(int i=0; i < this.PhoneNumbersM.Count; i++)
			{
				oEntry = (Common.PhoneNumber) this.PhoneNumbersM[i];
				//this.ErrorBI += "this.Save_PhoneNumbers() oEntry.Phone_ID (before):" + oEntry.Phone_ID.ToString() + "\r\n";
				if(oEntry.Phone_ID == -1)
				{
					//this.ErrorBI += "this.Save_PhoneNumbers() in insert\r\n";
					int newID = -1;
					result = this.aPhone.Insert(oEntry.Type, this.PhoneListID, oEntry.GetNumber, oEntry.BestTimeToCall, out newID);
					//this.ErrorBI += "this.Save_PhoneNumbers() results: " + resultS + "\r\n";

					if((result == false)||(newID == -1))
					{
						return false;
					}

					((Common.PhoneNumber)this.PhoneNumbersM[i]).Phone_ID = newID;
				}
				else
				{
					//this.ErrorBI += "this.Save_PhoneNumbers() in update\r\n";
					//this.ErrorBI += "this.aPhone.Update(oEntry.Phone_ID, oEntry.Type, this.PhoneListID, oEntry.GetNumber,oEntry.BestTimeToCall)<br>";
					//this.ErrorBI += "this.aPhone.Update(" + oEntry.Phone_ID.ToString() + ", " + oEntry.Type.ToString() + ", " + this.PhoneListID.ToString() + ", " + oEntry.GetNumber.ToString() + ", " + oEntry.BestTimeToCall.ToString() + ")<br>";
					result = this.aPhone.Update(oEntry.Phone_ID, oEntry.Type, this.PhoneListID, oEntry.GetNumber,oEntry.BestTimeToCall);
					//this.ErrorBI += "this.Save_PhoneNumbers() results: " + resultS + "\r\n";
					if(result == false)
					{
						return false;
					}
				}
			}
			return true;
		}

		private bool Save_PostalAddresses()
		{
			bool result;
			Common.PostalAddress oEntry;
			for(int i=0; i < this.PostalAddressesM.Count; i++)
			{
				oEntry = (Common.PostalAddress) this.PostalAddressesM[i];
				int newID = -1;
				if(oEntry.Address_ID == -1)
				{
					result = this.aPostalAddress.Insert(oEntry.Street1, oEntry.Street2, oEntry.City, oEntry.StateProvince, oEntry.PostalCode, oEntry.PostalPlus4Code, oEntry.Country, oEntry.Type, this.AddressListIDM, out newID);
					if((result == false)||(newID == -1)){ return false; }

					((Common.PostalAddress)this.PostalAddressesM[i]).Address_ID = newID;
				}
				else
				{
					result = this.aPostalAddress.Update(oEntry.Address_ID, oEntry.Street1, oEntry.Street2, oEntry.City, oEntry.StateProvince, oEntry.PostalCode, oEntry.PostalPlus4Code, oEntry.Country, oEntry.Type, this.AddressListIDM);
					if(result == false){ return false; }
				}
			}
			return true;
		}


		#region Validate_MandatorySelections
		private string Validate_MandatorySelections(ref bool blValid)
		{
			string stError = "";

//			//no sponsor field right now, placeholder for future
//			'''<businessIntelligence>A Sponsor mandatory</businessIntelligence>
//			if(this.SponsorM == "")
//			{
//				blValid = false;
//				stError += "Please enter a Sponsor Name\r\n";
//			}

			///<businessIntelligence>A Language selection mandatory</businessIntelligence>
			if (this.LangM == "")
			{
				blValid = false;
				stError += "Please select a language\r\n";
			}

			///<businessIntelligence>Account Group Class is mandatory</businessIntelligence>
			if (this.CAccountCodeClass_AssignedM == false)
			{
				blValid = false;
				stError += "Please select a Group Class\r\n";
			}

			///<businessIntelligence>Account Group Code is mandatory</businessIntelligence>
			if (this.CAccountCodeGroup_AssignedM == false)
			{
				blValid = false;
				stError += "Please select a Group Code\r\n";
			}

			///<businessIntelligence>A ShipTo Address is mandatory</businessIntelligence>
			bool ShipTo = false;
			bool ValidType = true;
			//loop through the Postal Addresses to check
			for (int i = 0; i < this.PostalAddressesM.Count; i++ )
			{
				try
				{
					if( ((Common.PostalAddress)this.PostalAddressesM[i]).Type == ((int)(Business.AddressType.ShipTo)) )
					{
						ShipTo = true;
					}
				}
				catch(System.InvalidCastException)
				{
					ValidType = false;
				}
			}
			if(ShipTo == false)
			{
				blValid = false;
				stError += "Please enter a Ship To Address\r\n";
			}
			if(ValidType == false)
			{
				blValid = false;
				stError += "An unrecognized item is in the Postal Address collection. Please contact IT.\r\n";
			}

//			///<businessIntelligence>A Contact name is mandatory</businessIntelligence>
//			if (this.ContactNameM == "")
//			{
//				blValid = false;
//				stError += "Please enter an Contact Name\r\n";
//			}

			///<businessIntelligence>Account Email is mandatory</businessIntelligence>
/*			if (this.EMailM == "")
			{
				blValid = false;
				stError += "Please enter an Email Address\r\n";
			}
*/
			///<businessIntelligence>A Telephone number is mandatory</businessIntelligence>
			///<businessIntelligence>A Fax number is mandatory</businessIntelligence>
			bool MainNum = false;
			bool FaxNum = true;
			ValidType = true;
			int phType;
			//loop through the Phone Numbers to check
			for (int i = 0; i < this.PhoneNumbersM.Count; i++ )
			{
				try
				{
					phType = ((Common.PhoneNumber)this.PhoneNumbersM[i]).Type;
					if(phType == ((int)(Business.PhoneType.Main)) )
					{
						MainNum = true;
					}
					else if(phType == ((int)(Business.PhoneType.Fax)) )
					{
						FaxNum = true;
					}
				}
				catch(System.InvalidCastException)
				{
					ValidType = false;
				}
			}
			if(MainNum == false)
			{
				blValid = false;
				stError += "Please enter a telephone # for the account\r\n";
			}
			if(FaxNum == false)
			{
				blValid = false;
				stError += "Please enter a fax # for the account\r\n";
			}
			if(ValidType == false)
			{
				blValid = false;
				stError += "An unrecognized item is in the Phone Number collection. Please contact IT.\r\n";
			}

			return stError;
		}

		#endregion

		public bool ValidateAndSaveVendorInfo()
		{
			if(this.ValidateVendorInfo() == true)
			{
				return this.SaveVendorInfo();
			}
			else
			{
				return false;
			}
		}

		private bool ValidateVendorInfo()
		{
			return true;
		}

		private bool SaveVendorInfo()
		{
			bool blSave = false;
			if(IDM == -1)
			{
				this.ErrorBI += "this.SaveVendorInfo() : ";
				this.ErrorBI += "The account needs to be saved before Vendor info can be inserted\r\n";
				blSave = false;
			}
			else
			{
				blSave = aTable.UpdateVendorInfo(IDM,VendorNumberM,VendorSiteNameM,VendorPayGroupM, UserIDModified);
			}
			return blSave;

		}



		#endregion

		#region Populate Functions
		public void PopulateFromDB()
		{
			DataTable dt = aTable.GetCAccountByID(this.Account_ID);
			string rowsStr = dt.Rows.Count.ToString();
			if(dt.Rows.Count < 1)
			{
				throw new ArgumentException("There is no data for the Account_Id", this.Account_ID.ToString());
			}
			else if(dt.Rows.Count > 1)
			{
				rowsStr += " rows of data were returned when 1 was expected";
				throw new ArgumentException(rowsStr, this.Account_ID.ToString());
			}
			else
			{
				/* one row returned, good stuff */
				DataRow DR = dt.Rows[0];
//				Fill(DR, this.GetType());

				this.Name				= Convert.ToString(DR["Name"]);
				this.StatusID			= Convert.ToInt32(DR["StatusID"]);
				//this.Sponsor			= Convert.ToString(DR["Sponsor"]);
				this.Country			= Convert.ToString(DR["Country"]);
				this.Lang				= Convert.ToString(DR["Lang"]);
				this.CAccountCodeClassStr	= Convert.ToString(DR["CAccountCodeClass"]);
				this.CAccountCodeGroupStr	= Convert.ToString(DR["CAccountCodeGroup"]);
				this.PhoneListID		= Convert.ToInt32(DR["PhoneListID"]);
				this.AddressListID		= Convert.ToInt32(DR["AddressListID"]);
				this.Comment			= Convert.ToString(DR["Comment"]);
				this.Enrollment			= Convert.ToInt32(DR["Enrollment"]);
				this.EMail				= Convert.ToString(DR["Email"]);
				this.IsPrivateOrg		= Convert.ToBoolean(DR["IsPrivateOrg"]);
				this.IsAdultGroup		= Convert.ToBoolean(DR["IsAdultGroup"]);
				this.DateUpdated		= Convert.ToDateTime(DR["DateUpdated"]);
				this.UserIDModified		= Convert.ToInt32(DR["UserIDModified"]);
				this.ParentID			= Convert.ToInt32(DR["ParentID"]);
				//this.UserIDCreated	= (string)	dt.Rows[0].ItemArray[15];
				//this.DateCreated		= Convert.ToDateTime(dt.Rows[0].ItemArray[16]);
				//this.DateUpdatedObj	= dt.Rows[0].ItemArray[17];
				//this.UserIDChanged	= this.UserIDUpdated.t; //same as UserIDUpdated ?
				this.VendorNumber		= Convert.ToString(DR["VendorNumber"]);
				this.VendorPayGroup		= Convert.ToString(DR["VendorPayGroup"]);
				this.VendorSiteName		= Convert.ToString(DR["VendorSiteName"]);

			}

			DAL.PhoneListDataAccess aPhoneList = new DAL.PhoneListDataAccess();
			DataTable dtPhone = aPhoneList.GetPhoneListByID(this.PhoneListID);
			this.PhoneNumbers = null;
			this.PhoneNumbers = new System.Collections.ArrayList();
			string number;
			int    Phone_ID;
			int    type;
			int    PhoneListID;
			string BestTimeToCall;
			bool blnPhMain = false; //is there a main # in the db
			//bool blnPhFax = false; //is there a fax # in the db
			for(int i = 0; i < dtPhone.Rows.Count; i++)
			{

				Phone_ID		= Convert.ToInt32(dtPhone.Rows[i].ItemArray[0].ToString());
				type			= Convert.ToInt32(dtPhone.Rows[i].ItemArray[1]);
				PhoneListID		= Convert.ToInt32(dtPhone.Rows[i].ItemArray[2]);
				number			= dtPhone.Rows[i].ItemArray[3].ToString();
				BestTimeToCall	= dtPhone.Rows[i].ItemArray[4].ToString();
				this.PhoneNumbers.Add(new Common.PhoneNumber(number, Phone_ID, type, PhoneListID, BestTimeToCall));
				if( type == ((int)(Business.PhoneType.Main)) ) { blnPhMain = true; }
				//if( type == ((int)(Business.PhoneType.Fax))  ) { blnPhFax  = true; }
			}
			if(blnPhMain == false) { this.PhoneNumbers.Add(new Common.PhoneNumber((int)(Business.PhoneType.Main))); }
			//if(blnPhFax == false)  { this.PhoneNumbers.Add(new Common.PhoneNumber((int)(Business.PhoneType.Fax))); }


			DAL.AddressListDataAccess anAddressList = new DAL.AddressListDataAccess();
			DataTable dtAddress = anAddressList.GetAddressListByID(this.AddressListID);
			this.PostalAddresses = null;
			this.PostalAddresses = new System.Collections.ArrayList();
			int    Address_ID;
			int    Type;
			string Street1;
			string Street2;
			string City;
			string StateProvince;
			string PostalCode;
			string PostalPlus4Code;
			string Country;
			bool blnAddrShipTo = false;//is there a ship to addr in the db
			int ShipToTypeInt = ((int)(Business.AddressType.ShipTo));

			for(int i = 0; i < dtAddress.Rows.Count; i++)
			{
				Address_ID		= (int)		dtAddress.Rows[i].ItemArray[0];
				Type			= (int)		dtAddress.Rows[i].ItemArray[1];
				Street1			= dtAddress.Rows[i].ItemArray[2].ToString();
				Street2			= dtAddress.Rows[i].ItemArray[3].ToString();
				City			= dtAddress.Rows[i].ItemArray[4].ToString();
				StateProvince	= dtAddress.Rows[i].ItemArray[5].ToString();
				PostalCode		= dtAddress.Rows[i].ItemArray[6].ToString();
				PostalPlus4Code	= dtAddress.Rows[i].ItemArray[7].ToString();
				Country			= dtAddress.Rows[i].ItemArray[8].ToString();
				this.PostalAddresses.Add(new Common.PostalAddress(Address_ID,Street1, Street2, City, StateProvince, PostalCode, PostalPlus4Code, Country, this.AddressListID, Type));
				if(Type == ShipToTypeInt) { blnAddrShipTo = true; }
			}
			if(blnAddrShipTo == false) { this.PostalAddresses.Add(new Common.PostalAddress(ShipToTypeInt)); }
		}

		public void PopulateWithNewAccount()
		{

			this.ParentID = 0;
			this.PostalAddresses = null;
			this.PostalAddresses = new System.Collections.ArrayList();
			int ShipToTypeInt = ((int)(Business.AddressType.ShipTo));
			this.PostalAddresses.Add(new Common.PostalAddress(ShipToTypeInt));

			this.PhoneNumbers = null;
			this.PhoneNumbers = new System.Collections.ArrayList();
			int PhoneTypeInt = ((int)(Business.PhoneType.Main));
			this.PhoneNumbers.Add(new Common.PhoneNumber(PhoneTypeInt));
			PhoneTypeInt = ((int)(Business.PhoneType.Fax));
			this.PhoneNumbers.Add(new Common.PhoneNumber(PhoneTypeInt));
		}

		#endregion Populate Functions

		#region Comment out at release compilation
//		public string DebugAccount()
//		{
//			string retval = "DebugAccount Account_ID:|" + this.Account_ID.ToString()+ "|\r\n";
//			retval += "DebugAccount IDM:|" + this.IDM.ToString()+ "|\r\n";
//			retval += "DebugAccount name:|" + NameM.ToString() + "|\r\n";
//			retval += "DebugAccount country:|" + CountryM.ToString() + "|\r\n";
//			retval += "DebugAccount lang:|" + LangM.ToString() + "|\r\n";
//			retval += "DebugAccount class:|" + CAccountCodeClassM.ToString() + "|\r\n";
//			retval += "DebugAccount code:|" + CAccountCodeGroupM.ToString() + "|\r\n";
//			retval += "DebugAccount pid:|" + PhoneListIDM.ToString() + "|\r\n";
//			for (int i=0; i < this.PhoneNumbersM.Count; i++)
//			{
//				retval += "&nbsp;&nbsp;&nbsp;|" + ((Common.PhoneNumber)this.PhoneNumbersM[i]).XMLline.ToString() + "|\r\n";
//			}
//			retval += "DebugAccount aid:|" + AddressListIDM.ToString() + "|\r\n";
//			for (int i=0; i < this.PostalAddressesM.Count; i++)
//			{
//				retval += "&nbsp;&nbsp;&nbsp;Type:|" + ((Common.PostalAddress)this.PostalAddressesM[i]).Type.ToString() + "|\r\n";
//				retval += "&nbsp;&nbsp;&nbsp;:|" + ((Common.PostalAddress)this.PostalAddressesM[i]).Street1.ToString() + "|, |";
//				retval += ((Common.PostalAddress)this.PostalAddressesM[i]).Street2.ToString() + "|\r\n";
//				retval += "&nbsp;&nbsp;&nbsp;:|" + ((Common.PostalAddress)this.PostalAddressesM[i]).City.ToString() + "|, |";
//				retval += ((Common.PostalAddress)this.PostalAddressesM[i]).StateProvince.ToString() + "|, |";
//				retval += ((Common.PostalAddress)this.PostalAddressesM[i]).PostalCode.ToString() + "|, |";
//				//retval += ((Common.PostalAddress)this.PostalAddressesM[i]).PostalPlus4Code.ToString() + "|\r\n";
//				retval += "&nbsp;&nbsp;&nbsp;:|" + ((Common.PostalAddress)this.PostalAddressesM[i]).Country.ToString() + "|\r\n";
//			}
//			retval += "DebugAccount status:|" + StatusIDM.ToString() + "|\r\n";
//			retval += "DebugAccount enrollment:|" + EnrollmentM.ToString() + "|\r\n";
//			retval += "DebugAccount comment:|" + CommentM.ToString() + "|\r\n";
//			retval += "DebugAccount email:|" + EMailM.ToString() + "|\r\n";
//			retval += "DebugAccount private:|" + IsPrivateOrgM.ToString() + "|\r\n";
//			retval += "DebugAccount adult:|" + IsAdultGroupM.ToString() + "|\r\n";
//			retval += "DebugAccount created:|" + userIDCreatedM.ToString() + "|\r\n";
//			retval += "DebugAccount sponsor:|" + SponsorM.ToString() + "|\r\n";
//			retval += "DebugAccount acctid:|" + IDM.ToString() + "|\r\n";
//			return retval;
//		}
		#endregion Comment out at release compilation

	}
}
