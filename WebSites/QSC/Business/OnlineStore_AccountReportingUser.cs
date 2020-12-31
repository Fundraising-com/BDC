using System;
using System.Data;
using System.Data.SqlClient;
//using System.Runtime.InteropServices;
//using System.Reflection;
//using System.ComponentModel;

namespace Business
{
	///<summary>Data representation of an user of the Account Reporting section of the Online Store.</summary>
	public class OnlineStore_AccountReportingUser : QBusinessObject
	{
		#region Constructors
		protected DAL.OnlineStoreDataAccess oOnlineStoreDATA;
		///<summary>default constructor</summary>
		public OnlineStore_AccountReportingUser()
		{
			oOnlineStoreDATA = new DAL.OnlineStoreDataAccess();
		}
		#endregion Constructors
		#region Class Members

		protected int AccountIDM = 0;
		[DAL.DataColumn("AccountID")]
		public int AccountID
		{
			get{ return this.AccountIDM; }
			set{ this.AccountIDM=value;  }
		}

		protected string AccountNameM = "";
		[DAL.DataColumn("AccountName")]
		public string AccountName
		{
			get{ return this.AccountNameM; }
			set{ this.AccountNameM=value;  }
		}

		protected string CityM = "";
		[DAL.DataColumn("City")]
		public string City
		{
			get{ return this.CityM; }
			set{ this.CityM=value;  }
		}

		protected string StateM = "";
		[DAL.DataColumn("State")]
		public string State
		{
			get{ return this.StateM; }
			set{ this.StateM=value;  }
		}

		protected string UserNameM = "";
		[DAL.DataColumn("UserName")]
		public string UserName
		{
			get{ return this.UserNameM; }
			set{ this.UserNameM=value;  }
		}

		protected string PasswordM = "";
		[DAL.DataColumn("Password")]
		public string Password
		{
			get{ return this.PasswordM; }
			set{ this.PasswordM=value;  }
		}

		protected bool DeletedTFM = false;
		[DAL.DataColumn("DeletedTF")]
		public bool DeletedTF
		{
			get{ return this.DeletedTFM; }
			set{ this.DeletedTFM=value;  }
		}

		private string CountryM = "CA";
//		[DAL.DataColumn("Country")]
//		public string Country
//		{
//			get{ return this.CountryM; }
//			set{ this.CountryM=value;  }
//		}
		
		#region Validation class members
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
		#endregion Validation class members

		#endregion Class Members

		#region ValidateAndSave
		///<summary>check it then submit it</summary>
		override public bool ValidateAndSave()
		{
			if(this.Validate() == true)
			{
				//this.ErrorBI = "this.Validate() returned true, lets try a save !<br>" + this.ErrorBI;
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
			
			///<validation>A record needs an Account ID</validation>
			if(this.AccountIDM == 0)
			{
				blValid = false;
				stError += "Please enter a Group Number.\r\n";
			}

			///<validation>A record needs a username</validation>
			if(this.UserNameM == "")
			{
				blValid = false;
				stError += "Please enter a UserName.\r\n";
			}

			///<validation>A record needs a Password</validation>
			if(this.PasswordM == "")
			{
				blValid = false;
				stError += "Please enter a Password.\r\n";
			}
			
			/* Save the validation results into the object */
			this.ValidBIM = blValid;
			this.ErrorBIM = stError;
			
			return blValid;
		}


		///<summary>Save record to the db</summary>
		///<returns>bool: Was saving successful ? </returns>
		public bool Save()
		{
			bool blSave = false;

			int ReturnCode;
			blSave = oOnlineStoreDATA.Update_AcctReportingUsers(
				  this.AccountIDM
				, this.UserNameM
				, this.PasswordM
				, this.DeletedTFM
				, this.CountryM
				, out ReturnCode );
			if ((blSave == false)||(ReturnCode != 1))
			{
				if(ReturnCode == -1)
				{
					//  \"\" becomes "" in the javascript, becomes " in the text to user
					//this.ErrorBI += "The username: \"" + this.UserNameM;
					//this.ErrorBI += "\" is already being used, please pick a new one" + "\r\n";
					this.ErrorBI += "The username: " + this.UserNameM;
					this.ErrorBI += " is already being used, please pick a new one" + "\r\n";
				}
				else if(
					(ReturnCode == -2) ||
					(ReturnCode == -3) ||
					(ReturnCode == -5)
					)
				{
					this.ErrorBI += "General Error - Please contact IT support with the following info: ";
					this.ErrorBI += "Action - update online store report user - errorcode - ";
					this.ErrorBI += ReturnCode.ToString() + " (along with the information you are trying to submit)";
					this.ErrorBI += "\r\n";
				}
				else
				{
					this.ErrorBI += "Unknown Error - Please contact IT support with the following info: ";
					this.ErrorBI += "Action - update online store report user - errorcode - ";
					this.ErrorBI += ReturnCode.ToString() + " (along with the information you are trying to submit)";
					this.ErrorBI += "\r\n";
				}
				this.ValidBI = false;//if a save fails, something definetly wasnt valid
				return false;
			}
			else
			{
				//this.ValidBI = true;//if a save passes, it doesnt necessarily mean the record is valid
				return true;
			}
		}
		#endregion ValidateAndSave

		public void PopulateFromDB()
		{
			DataTable DT = this.oOnlineStoreDATA.Get_AcctReportingUsers_byAccountID(this.AccountIDM, this.CountryM);

			if(DT.Rows.Count < 1)
			{
				this.UserNameM = "";
				this.PasswordM = "";
			}
			else if(DT.Rows.Count > 1)
			{
				string ErrorStr = DT.Rows.Count.ToString() + " rows of data were returned when at most 1 was expected";
				throw new ArgumentException(ErrorStr, this.AccountIDM.ToString());
			}
			else
			{
				/* one row returned, good stuff */
				DataRow DR = DT.Rows[0];
				//Fill(DR, this.GetType());
				this.UserName = Convert.ToString( DR["UserName"] );
				this.AccountName = Convert.ToString( DR["AccountName"] );
				this.City = Convert.ToString( DR["City"] );
				this.State = Convert.ToString( DR["State"] );
				this.UserName = Convert.ToString( DR["UserName"] );
				this.Password = Convert.ToString( DR["Password"] );
				this.DeletedTF = Convert.ToBoolean( DR["DeletedTF"] );				
			}
		}

		public DataTable GetUserData()
		{
			DataTable DT = this.oOnlineStoreDATA.Get_AcctReportingUsers_byAccountID(this.AccountIDM, this.CountryM);

			if(DT.Rows.Count > 1)
			{
				string ErrorStr = DT.Rows.Count.ToString() + " rows of data were returned when at most 1 was expected";
				throw new ArgumentException(ErrorStr, this.AccountIDM.ToString());
			}
			return DT;
		}

	}
}
