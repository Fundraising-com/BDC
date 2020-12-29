using System;
using System.Data;
using GA.BDC.Core.ESubsGlobal.DataAccess;

namespace GA.BDC.Core.ESubsGlobal.Reports
{
	/// <summary>
	/// Summary description for SearchEventCriteria.
	/// </summary>
	
	public class SimilarCampaign
	{
		public static DataSet GetSimilarCampaigns(int EventID)
		{
			ESubsGlobalDatabase esubsDB = new ESubsGlobalDatabase();
			return esubsDB.GetSimilarCampaigns(EventID);
		}

		
		public static int CheckIfCampaignHasCheck(int CampaignID)
		{
			ESubsGlobalDatabase esubsDB = new ESubsGlobalDatabase();
			return esubsDB.CheckIfCampaignHasCheck(CampaignID);
		}

		
		public static bool IsEventActive(int CampaignID)
		{
			ESubsGlobalDatabase esubsDB = new ESubsGlobalDatabase();
			return !(esubsDB.IsEventActive(CampaignID) == 0);
		}

		public static DataTable GetMergeHistory(int EventID)
		{						
			ESubsGlobalDatabase esubsDB = new ESubsGlobalDatabase();
			return esubsDB.GetMergeHistory(EventID);
		}
		
		public static int DoMergeCampaigns(int org_event_id, int org_group_id,
			int merged_event_id,int merged_event_participation_id ,int merged_group_id, string userName, string mergedComments)
		{						
			ESubsGlobalDatabase esubsDB = new ESubsGlobalDatabase();
			return esubsDB.MergeGroupController(
				org_event_id, org_group_id, merged_event_id, 
				merged_event_participation_id , merged_group_id, 
				userName, mergedComments);
		}
	}


	public class CheckHistoryInfo
	{
		public static DataTable GetResult(int EventID)
		{
			ESubsGlobalDatabase esubsDB = new ESubsGlobalDatabase();
			return esubsDB.GetCheckHistoty(EventID);
		}
	}


	public class OrderInfo
	{
		public static DataTable GetResult(int eventID)
		{
			ESubsGlobalDatabase esubsDB = new ESubsGlobalDatabase();
			return esubsDB.GetOrders(eventID);
		}

		public static void UpdateParent(Int32 memberHierarchyID, Int32 parentMemberHierarchyID)
		{

			// Create database access
			GA.BDC.Core.ESubsGlobal.DataAccess.ESubsGlobalDatabase db =
				new GA.BDC.Core.ESubsGlobal.DataAccess.ESubsGlobalDatabase();
			// Assign new parent with cc_assign_parent
			db.UpdateParent(memberHierarchyID, parentMemberHierarchyID);
		}
	}


	public class HandleUserInCustCare
	{
		public static Users.UnknownUser[] GetParticipantsByEvent(int eventId)
		{
			
			// Create database access
			GA.BDC.Core.ESubsGlobal.DataAccess.ESubsGlobalDatabase db =
				new GA.BDC.Core.ESubsGlobal.DataAccess.ESubsGlobalDatabase();

			// Get all users from current eventID stored in database
			return db.GetParticipantsByEvent(eventId);
		}
	}

	public class TabStruct
	{
		public string Name;
		public string Path;
		public string[] Roles;
		public string TabID;
		public string IFrameSRC;
		public TabStruct() {}
	}


	public class GenericRadioButtonCriteria
	{	
		private bool isChecked = false;
		virtual public bool IsChecked
		{
			get { return isChecked; }
			set { isChecked = value; }
		}
	}


	public class GlobalSearchEvent
	{
		private string searchEventName = string.Empty; 
		private int searchEventID= int.MinValue ;
		private string memberType= string.Empty;
		private string memberEmailAddress= string.Empty ;
		private string memberName= string.Empty ;
		private int eventLeadID= int.MinValue ;
		private string eventGroupName= string.Empty;
		private int eventCheckNumber= int.MinValue ;
		private DateTime dateFirstDay = DateTime.MinValue;
		private DateTime dateSecondDay= DateTime.MinValue;
		private string selectTopResult = "1000";

		public GlobalSearchEvent(string topResult)
		{
			try
			{
				int i = int.Parse(topResult);
				if (i > 0)
					selectTopResult = topResult.ToString();
				else
					selectTopResult = string.Empty;
			}
			catch (Exception)
			{
				selectTopResult = string.Empty;
			}
		}

		public GlobalSearchEvent()
		{
		}


		#region Properties

		public string SearchEventName
		{
			get
			{
				return searchEventName; 
			}
			set
			{
				searchEventName = value;
			}
		}

		public int SearchEventID
		{
			get
			{
				return searchEventID;
			}
			set
			{
				searchEventID= value;
			}
		}
		public string MemberType
		{
			get {return memberType;}
			set {memberType = value;}
		}

		public string MemberEmailAddress
		{
			get {return memberEmailAddress;}
			set {memberEmailAddress = value;}
		}
		public string MemberName
		{
			get {return memberName;}
			set {memberName =value;}
		}
		public int EventLeadID
		{
			get {return eventLeadID;}
			set {eventLeadID = value;}
		}
		public string EventGroupName
		{
			get {return eventGroupName;}
			set {eventGroupName= value;}
		}
		public int EventCheckNumber
		{
			get {return eventCheckNumber;}
			set {eventCheckNumber = value;}
		}
		public DateTime EventBetweenFirstDay
		{
			get {return dateFirstDay;}
			set {dateFirstDay= value;}
		}
		public DateTime EventBetweenSecondDay
		{
			get {return dateSecondDay;}
			set {dateSecondDay =value;}
		}

		#endregion

		public System.Collections.Hashtable GetHashTableParameters()
		{
			System.Collections.Hashtable hashValue = new System.Collections.Hashtable ();
			hashValue.Add ("@SearchEventName", SearchEventName);
			hashValue.Add ("@SearchEventID", SearchEventID );
			// Members
			hashValue.Add("@MemberEACriteria", MemberEmailAddress);
			hashValue.Add("@MemberMNCriteria", MemberName);
			// Events
			hashValue.Add("@EventLID", EventLeadID);
			hashValue.Add("@EventGN", EventGroupName);
			hashValue.Add("@EventCN", EventCheckNumber);
			// Date	
			hashValue.Add("@DateFirstDay", EventBetweenFirstDay);
			hashValue.Add("@DateSecondDay", EventBetweenSecondDay);
			hashValue.Add("@SelectTop", this.selectTopResult);
			return hashValue;

		}

		public DataTable GetResult()
		{			
			ESubsGlobalDatabase esubsDB = new ESubsGlobalDatabase() ;
			
			DataTable dtTmp = esubsDB.GetSearchResultInDataTable (this);
			if (dtTmp == null)
				return null;
			DataTable dt = dtTmp.Clone ();
			dt.Constraints.Add ("EventID" , dt.Columns["EventID"], true);
			int i = 0;
			while (i < dtTmp.Rows.Count)
			{
				try
				{
					dt.ImportRow (dtTmp.Rows[i]);
				}
				catch (Exception) //Ignore the duplicated Event ID .
				{
				}
				i++;
			}
			return dt;

		}

	}

}
