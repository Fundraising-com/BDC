using System;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.ComponentModel;


namespace Business
{
	///<summary>Grabs a list of campaigns for a given account</summary>
	public class CampaignList : QBusinessObject
	{
		#region Constructors
		///<summary>Default Constructor</summary>
		public CampaignList(){}

		///<summary>AccountID Constructor</summary>
		public CampaignList(int AccountID){ this.AccountIDM = AccountID; }

		///<summary>AccountID and FMID Constructor</summary>
		public CampaignList(int AccountID, string FMID){ this.AccountIDM = AccountID; this.FMIDM = FMID; }
		#endregion Constructors

		#region ValidateAndSave
		///<summary>
		///No Validation or saving, not applicable
		///here to make the compiler happy
		///</summary>
		override public bool ValidateAndSave()
		{
			return true;
		}
		#endregion ValidateAndSave

		#region Class Members
		protected int AccountIDM = -1;
		///<summary>Which QSP Account to look Campaigns up for.</summary>
		public int AccountID
		{
			get { return this.AccountIDM;  }
			set { this.AccountIDM = value; }
		}
		protected string FMIDM = "";
		///<summary>*Optional* string to choose a field rep to filter results by</summary>
		public string FMID
		{
			get { return this.FMIDM;  }
			set { this.FMIDM = value; }
		}
		private DAL.CampaignListDataAccess aList = new DAL.CampaignListDataAccess();
		#endregion Class Members

		#region GetCampaignList
		public DataTable GetCampaignList()
		{
			return aList.GetCampaignList(this.AccountIDM, this.FMIDM);
		}
		public DataTable GetCampaignListHeader()
		{
			return aList.GetCampaignListHeader(this.AccountIDM);
		}
		#endregion GetCampaignList
	}
}
