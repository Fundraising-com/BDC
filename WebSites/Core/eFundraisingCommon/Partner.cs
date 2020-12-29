
using System;
using System.Data;
using System.Collections.Generic;
using System.Web;

using GA.BDC.Core.eFundraisingCommon.DataAccess;

namespace GA.BDC.Core.eFundraisingCommon
{
    [Serializable()]
	public class Partner :  IComparable {
		private int partnerID = int.MinValue;
		private string name = null;
		private int partnerTypeID = int.MinValue; // league, association, group
		private string host = null;
		private bool hasCollectionSite = false;
		private string guid = null;
        private string _PartnerFolder = "";
        private string _PartnerName = "";
        private string _PhoneNumber = "";
        private string _Url = "";
        private string _ESubsUrl = "";

        //Post Affiliate Partner parameter
        private string partnerAttributeName = "";
        private string value = "";
        private string partnertypename = "";





		protected PartnerComparable sortBy = PartnerComparable.Name;
  

		public Partner()
        {
		}

       
        public void Save(System.Web.SessionState.HttpSessionState session)
        {
            session["_PARTNER_INFO_"] = this;
        }

        public void Save(HttpSessionStateBase session)
        {
            session["_PARTNER_INFO_"] = this;
        }

        public static Partner Create(System.Web.SessionState.HttpSessionState session)
        {
            if (session["_PARTNER_INFO_"] != null)
            {
                return (Partner)session["_PARTNER_INFO_"];
            }
            return Partner.GetPartnerInfoByID(0);
        }

        public static Partner Create(HttpSessionStateBase session)
        {
            if (session["_PARTNER_INFO_"] != null)
            {
                return (Partner)session["_PARTNER_INFO_"];
            }
            return Partner.GetPartnerInfoByID(0);
        }

        
        public Partner(string _name, int _partnerTypeID, bool _hasCollectionSite) 
        {
			name = _name;
			partnerTypeID = _partnerTypeID;
			hasCollectionSite = _hasCollectionSite;
		}


		public static Partner LoadByID(int id)
        {
            DataAccess.EFRCommonDatabase dbo = new DataAccess.EFRCommonDatabase();
			Partner partner = dbo.GetPartnerByID(id);
			return partner;
		}



        public static List<Partner> GetPartners() 
        {
			DataAccess.EFRCommonDatabase dbo = new DataAccess.EFRCommonDatabase();
			 List<Partner> partners = dbo.GetPartners();
			return partners;
		}

        
        public static Partner GetPartnerInfoByFolder(string folder)
        {
            DataAccess.EFRCommonDatabase dbo = new DataAccess.EFRCommonDatabase();
            Partner partner = dbo.GetPartnerInfoByFolder(folder);

            return partner;
        }

        public static Partner GetPartnerInfoByID(int partnerID)
        {
            DataAccess.EFRCommonDatabase dbo = new DataAccess.EFRCommonDatabase();
            Partner partner = dbo.GetPartnerInfoByID(partnerID);

            return partner;
        }

        public static Partner GetPartnerInfoByURL(string url)
        {
            DataAccess.EFRCommonDatabase dbo = new DataAccess.EFRCommonDatabase();
            Partner partner = dbo.GetPartnerInfoByURL(url);

            return partner;
        }

        public static Partner GetPAPPartnerInfo(string a_aid)
        {

            DataAccess.EFRCommonDatabase dbo = new DataAccess.EFRCommonDatabase();
            Partner partner = dbo.GetPAPPartnerInfo(a_aid);

            return partner;


        }


        public int InsertPartner(string papid, string name, string path, string folder, string esubsurl, string tempdesc)
        {
            DataAccess.EFRCommonDatabase dbo = new DataAccess.EFRCommonDatabase();
            partnerID = dbo.InsertPartner(papid, name, path, folder, esubsurl, tempdesc);
            return partnerID;

     
        }


		
	   		
			#region Properties

        public string PartnerAttributeName
        {
            get { return this.partnerAttributeName; }
            set { this.partnerAttributeName = value; }
        }

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }


        public string PartnerTypeName
        {
            get { return this.partnertypename; }
            set { this.partnertypename = value; }
        }

        
        
        
        
        public string ESubsUrl
        {
            get { return this._ESubsUrl; }
            set { this._ESubsUrl = value; }
        }
        
        public string PhoneNumber
        {
            get { return this._PhoneNumber; }
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
        
       
        
        
        
        
        
        
        
        public string Url
        {
            get { return this._Url; }
            set { this._Url = value; }
        }
        
        public string PartnerName
        {
            get { return this._PartnerName; }
            set { this._PartnerName = value; }
        }

        public string PartnerFolder
        {
            get { return this._PartnerFolder; }
            set { this._PartnerFolder = value; }
        }
        
        
        public int PartnerID {
			set { partnerID = value; }
			get { return partnerID; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

	public int PartnerTypeID {
			set { partnerTypeID = value; }
			get { return partnerTypeID; }
		}

		public string Host {
			set { host = value; }
			get { return host; }
		}



		public bool HasCollectionSite {
			set { hasCollectionSite = value; }
			get { return hasCollectionSite; }
		}

		public string GUID {
			set { guid = value; }
			get { return guid; }
		}

		public PartnerComparable SortBy
		{
			get { return sortBy;}
			set { sortBy = value;}
		}

		#endregion

		#region IComparable Members

		public int CompareTo(object obj)
		{
			Partner prt = obj as Partner;
			if (prt != null)
			{
				switch (sortBy)
				{
					case PartnerComparable.ID:
					{
						return (this.PartnerID > prt.PartnerID ? 1 : 0);
					}
					default:
						return string.Compare(Name, prt.Name, true);
				}
			}
			return 0;
		}


		#endregion

      
    }

	public enum PartnerComparable 
	{
		Name,
		ID
	}

}
