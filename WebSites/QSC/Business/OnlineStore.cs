using System;
using System.Data;
using System.Data.SqlClient;
//using System.Runtime.InteropServices;
//using System.Reflection;
//using System.ComponentModel;

namespace Business
{
	///<summary>Collection of info related to the QSP.ca webstore</summary>
	public class OnlineStore : QBusinessObject
	{
		#region Class Members
////		private DAL.ContactDataAccess aTable;
		private DAL.OnlineStoreDataAccess oOnlineStoreDATA;

////			private bool	ValidGUIM;
////			///<summary>Gets or sets value indicating if the CA has passsed user interface level validation</summary>
////			public bool		ValidGUI	{ get{ return this.ValidGUIM; } set{this.ValidGUIM = value; } }
////	
////			private bool	ValidBIM;
////			///<summary>Gets or sets value indicating if the CA has passsed biz intelligence level validation</summary>
////			public bool		ValidBI		{ get{ return this.ValidBIM;  } set{this.ValidBIM  = value; } }
////	
////			private string	ErrorGUIM;
////			///<summary>Gets or sets error string associated with user interface level validation</summary>
////			public string	ErrorGUI	{ get{ return this.ErrorGUIM; } set{this.ErrorGUIM = value; } }
////	
////			private string	ErrorBIM;
////			///<summary>Gets or sets error string associated with biz intelligence level validation</summary>
////			public string	ErrorBI		{ get{ return this.ErrorBIM;  } set{this.ErrorBIM  = value; } }
////	
		private System.Collections.ArrayList RptUsersM = new System.Collections.ArrayList(20);

////			///<summary>Method to Add a Contact Record to the maintenance group</summary>
////			///<param name="Input">a Business.CampaignProgramBrochure instance</param>
////			public void AddContact(Business.Contact Input)
////			{
////				ContactsM.Add(Input);
////			}
		///<summary>Method to add an Account Reporting User record to the maintenance group</summary>
		///<param name="Input">a Business.OnlineStore_AccountReportingUser instance</param>
		public void AddRptUser(Business.OnlineStore_AccountReportingUser Input)
		{
			RptUsersM.Add(Input);
		}
		#endregion  Class Members

		#region Constructors
////			public ContactMaintenance()
////			{
////				aTable = new DAL.ContactDataAccess();
////			}
		public OnlineStore()
		{
			oOnlineStoreDATA = new DAL.OnlineStoreDataAccess();
		}
		#endregion Constructors

		#region ValidateAndSave
		override public bool ValidateAndSave()
		{
			return false;
		}
		#endregion ValidateAndSave

		#region Populate Functions
		public System.Collections.ArrayList GetRptUsersByAccountID(int AccountID)
		{
			DataTable DT = oOnlineStoreDATA.Get_AcctReportingUsers_byAccountID(AccountID, "CA");
			ConvertDataToItems(DT);
			return RptUsersM;
		}

		public System.Collections.ArrayList GetRptUsersByFMID(string FMID)
		{
			DataTable DT = oOnlineStoreDATA.Get_AcctReportingUsers_byFMID(FMID, "CA");
			ConvertDataToItems(DT);
			return RptUsersM;
		}

		private void ConvertDataToItems(DataTable DT)
		{
			Business.OnlineStore_AccountReportingUser aRptUser; // = new Business.OnlineStore_AccountReportingUser();
			for (int i = 0; i < DT.Rows.Count; i++)
			{
				aRptUser = new Business.OnlineStore_AccountReportingUser();
				aRptUser.AccountID = Convert.ToInt32( DT.Rows[i]["AccountID"] );
				aRptUser.AccountName = Convert.ToString( DT.Rows[i]["AccountName"] );
				aRptUser.City = Convert.ToString( DT.Rows[i]["City"] );
				aRptUser.State = Convert.ToString( DT.Rows[i]["State"] );
				aRptUser.UserName = Convert.ToString( DT.Rows[i]["UserName"] );
				aRptUser.Password = Convert.ToString( DT.Rows[i]["Password"] );
				aRptUser.DeletedTF = Convert.ToBoolean( DT.Rows[i]["DeletedTF"] );
				this.AddRptUser(aRptUser);
			}
		}
		#endregion Populate Functions
	}
}