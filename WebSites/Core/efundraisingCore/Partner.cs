//
//	Louis Turmel	-	June 27, 2005	-	class creation and documentation
//

using System;
using System.Data;
using GA.BDC.Core.Database.efundraising;
using GA.BDC.Core.efundraisingCore.DataAccess;
using System.Web;


namespace GA.BDC.Core.efundraisingCore {

	/// <summary>
	/// Object reprensenting an Partner entity
	/// </summary>
    [Serializable]
    public class Partner {
		
		#region Fields

		private int _PartnerID = -1;
		private int _PartnerGroupTypeID = -1;
		private int _PartnerSubGroupTypeID = -1;
		private int _CountryID = -1;
		private string _PartnerName = "";
		private string _PartnerPath = "";
		private string _PartnerPassword = "";
		private string _ESubsUrl = "";
		private string _EStoreUrl = "";
		private string _FreeKitUrl = "";
		private string _Logo = "";
		private string _PhoneNumber = "";
		private string _EmailExt = "";
		private string _Url = "";
		private string _Guid = "";
		private bool _PrizeEligible = false;
		private bool _HasCollectionSite = false;
		private string _PartnerFolder = "";
		

		#endregion

		#region Constructors

		public Partner() {
		}

		#endregion

		#region Methods
		
		#region (Added for eReport Online 2006-08-24)

		public static Partner GetPartnerByPath(string partnerPath) {
			EFundDatabase dbo = new EFundDatabase();
			return dbo.GetPartnerByPath(partnerPath);
		}

		public static Partner[] GetPartners() {
			EFundDatabase dbo = new EFundDatabase();
			return dbo.GetPartners();
		}

		#endregion

		public void Save(System.Web.SessionState.HttpSessionState session) {
			session["_PARTNER_INFO_"] = this;
		}

        public void Save(HttpSessionStateBase session)
        {
            session["_PARTNER_INFO_"] = this;
        }
		
		public static Partner Create(System.Web.SessionState.HttpSessionState session) {
			if(session["_PARTNER_INFO_"] != null) {
				return (Partner)session["_PARTNER_INFO_"];
			}
			return Partner.GetPartnerInfoByID(0);
		}

        public static Partner Create(HttpSessionStateBase session)
        {
			if(session["_PARTNER_INFO_"] != null) {
				return (Partner)session["_PARTNER_INFO_"];
			}
			return Partner.GetPartnerInfoByID(0);
		}

        


		public static Partner CreateOnly(System.Web.SessionState.HttpSessionState session) {
			if(session["_PARTNER_INFO_"] == null)
				return null;
			return (Partner)session["_PARTNER_INFO_"];
		}

		public static void LogOut(System.Web.SessionState.HttpSessionState Session) {
			Session["_PARTNER_INFO_"] = null;
		}
		
		public static Partner GetPartnerByID(int partnerID) {
			Partner p = new Partner();
			DatabaseObject db = new DatabaseObject();

			DataTable dt = db.GetPartnerInfo(partnerID);
			foreach(DataRow feRow in dt.Rows) {
				p.PartnerID = int.Parse(feRow["Partner_ID"].ToString());
				p.PartnerName = feRow["Partner_Name"].ToString();
				p.ESubsUrl = feRow["eSubs_url"].ToString();
				p.Url = feRow["url"].ToString();
			}

			return p;
		}
		
		public static Partner GetPartnerInfoByID(int partnerID) {
			EFundDatabase db = new EFundDatabase();
			Partner partner = db.GetPartnerInfoByID(partnerID);
			
			return partner;
		}
		
		public static Partner GetPartnerInfoByURL(string url) {
			EFundDatabase db = new EFundDatabase();
			Partner partner = db.GetPartnerInfoByURL(url);
			
			return partner;
		}
		
		public static Partner GetPartnerInfoByFolder(string folder) {
			EFundDatabase db = new EFundDatabase();
			Partner partner = db.GetPartnerInfoByFolder(folder);
			
			return partner;
		}
		
		#endregion

		#region Properties

		public int PartnerID {
			get { return this._PartnerID; }
			set { this._PartnerID = value; }
		}
	
		public int PartnerGroupTypeID {
			get { return this._PartnerGroupTypeID; }
			set { this._PartnerGroupTypeID = value; }
		}

		public int PartnerSubGroupTypeID {
			get { return this._PartnerSubGroupTypeID; }
			set { this._PartnerSubGroupTypeID = value; }
		}

		public int CountryID {
			get { return this._CountryID; }
			set { this._CountryID = value; }
		}

		public string PartnerName {
			get { return this._PartnerName; }
			set { this._PartnerName = value; }
		}

		public string PartnerPath {
			get { return this._PartnerPath; }
			set { this._PartnerPath = value; }
		}

		public string PartnerPassword {
			get { return this._PartnerPassword; }
			set { this._PartnerPassword = value; }
		}

		public string ESubsUrl {
			get { return this._EStoreUrl; }
			set { this._ESubsUrl = value; }
		}

		public string EStoreUrl {
			get { return this._EStoreUrl; }
			set { this._EStoreUrl = value; }
		}

		public string FreeKitUrl {
			get { return this._FreeKitUrl; }
			set { this._FreeKitUrl = value; }
		}

		public string Logo {
			get { return this._Logo; }
			set { this._Logo = value; }
		}

		public string PhoneNumber {
			get { return this._PhoneNumber; }
			set { 
				if (value != null && value != "") {
					this._PhoneNumber = value;
				}
				else {
					this._PhoneNumber = "1-866-313-8867";
				} 
			}
		}

		public string EmailExt {
			get { return this._EmailExt; }
			set { this._EmailExt = value; }
		}

		public string Url {
			get { return this._Url; }
			set { this._Url = value; }
		}

		public string GUID {
			get { return this._Guid; }
			set { this._Guid = value; }
		}
		
		public bool PrizeEligible {
			get { return this._PrizeEligible; }
			set { this._PrizeEligible = value; }
		}

		public bool HasCollectionSite {
			get { return this._HasCollectionSite; }
			set { this._HasCollectionSite = value; }
		}

		public string PartnerFolder {
			get { return this._PartnerFolder; }
			set { this._PartnerFolder = value; }
		}

		#endregion
	}
}

/*
//
//	Louis Turmel	-	June 27, 2005	-	class creation and documentation
//

using System;
using System.Data;
using efundraising.Database.efundraising;
using efundraising.efundraisingCore.DataAccess;

namespace efundraising.efundraisingCore {

	/// <summary>
	/// Object reprensenting an Partner entity
	/// </summary>
	public class Partner {
		
		#region private fields

		private int _PartnerID = -1;
		private int _PartnerGroupTypeID = -1;
		private string _PartnerName = "";
		private string _ESubsUrl = "";
		private string _EStoreUrl = "";
		private string _FreeKitUrl = "";
		private string _PhoneNumber = "";
		private string _Url = "";
		private string _Guid = "";
		private string _PartnerPath = "";
		private string _PartnerFolder = "";
		private bool _PrizeEligible = false;
		private bool _HasCollectionSite = false;
		private string _PartnerPassword = "";

		#endregion

		#region public constructors
		
		public Partner() {
			
		}

		#endregion


		#region Methods
		
		public void Save(System.Web.SessionState.HttpSessionState session) {
			session["_PARTNER_INFO_"] = this;
		}
		
		public static Partner Create(System.Web.SessionState.HttpSessionState session) {
			if(session["_PARTNER_INFO_"] != null) {
				return (Partner)session["_PARTNER_INFO_"];
			}
			return Partner.GetPartnerInfoByID(0);
		}

		public static Partner CreateOnly(System.Web.SessionState.HttpSessionState session) {
			if(session["_PARTNER_INFO_"] == null)
				return null;
			return (Partner)session["_PARTNER_INFO_"];
		}

		public static void LogOut(System.Web.SessionState.HttpSessionState Session) {
			Session["_PARTNER_INFO_"] = null;
		}

		public static Partner GetPartnerByID(int partnerID)
		{
			Partner p = new Partner();
			DatabaseObject db = new DatabaseObject();

			DataTable dt = db.GetPartnerInfo(partnerID);
			foreach(DataRow feRow in dt.Rows) 
			{
				p.PartnerID = int.Parse(feRow["Partner_ID"].ToString());
				p.PartnerName = feRow["Partner_Name"].ToString();
				p.ESubsUrl = feRow["eSubs_url"].ToString();
				p.Url = feRow["url"].ToString();
			}

			return p;
		}
		
		public static Partner GetPartnerInfoByID(int partnerID)
		{
			EFundDatabase db = new EFundDatabase();
			Partner partner = db.GetPartnerInfoByID(partnerID);
			
			return partner;
		}
		
		public static Partner GetPartnerInfoByURL(string url)
		{
			EFundDatabase db = new EFundDatabase();
			Partner partner = db.GetPartnerInfoByURL(url);
			
			return partner;
		}
		
		public static Partner GetPartnerInfoByFolder(string folder)
		{
			EFundDatabase db = new EFundDatabase();
			Partner partner = db.GetPartnerInfoByFolder(folder);
			
			return partner;
		}
		
		#endregion

		#region Properties

		public int PartnerID 
		{
			get{ return this._PartnerID; }
			set{ this._PartnerID = value; }
		}
	
		public int PartnerGroupTypeID {
			get{ return this._PartnerGroupTypeID; }
			set{ this._PartnerGroupTypeID = value; }
		}

		public string PartnerName {
			get{ return this._PartnerName; }
			set{ this._PartnerName = value; }
		}

		public string PartnerPath {
			get { return this._PartnerPath; }
			set { this._PartnerPath = value; }
		}

		public string PartnerPassword {
			get { return this._PartnerPassword; }
			set { this._PartnerPassword = value; }
		}

		public string ESubsUrl {
			get{ return this._EStoreUrl; }
			set{ this._ESubsUrl = value; }
		}

		public string EStoreUrl {
			get{ return this._EStoreUrl; }
			set{ this._EStoreUrl = value; }
		}

		public string FreeKitUrl {
			get{ return this._FreeKitUrl; }
			set{ this._FreeKitUrl = value; }
		}

		public string PhoneNumber {
			get{ return this._PhoneNumber; }
			set
			{ 
				if (value != null && value != "")
				{
					this._PhoneNumber = value;
				}
				else
				{
					this._PhoneNumber = "1-866-313-8867";
				}
				 
			}
		}

		public string Url {
			get{ return this._Url; }
			set{ this._Url = value; }
		}

		public string GUID {
			get{ return this._Guid; }
			set{ this._Guid = value; }
		}

		public string PartnerFolder 
		{
			get{ return this._PartnerFolder; }
			set{ this._PartnerFolder = value; }
		}

		public bool PrizeEligible {
			get{ return this._PrizeEligible; }
			set{ this._PrizeEligible = value; }
		}

		public bool HasCollectionSite {
			get{ return this._HasCollectionSite; }
			set{ this._HasCollectionSite = value; }
		}

		#endregion
	}
}
*/