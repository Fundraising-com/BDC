/* Title:	Partner
 * Author:	Jean-Francois Buist
 * Summary:	Parthership data.
 * 
 * Create Date:	August 1, 2005
 * 
 */

using System;
using System.Data;

using GA.BDC.Core.ESubsGlobal.DataAccess;

namespace GA.BDC.Core.ESubsGlobal {
	/// <summary>
	/// This class reference to the current partner information.
	/// 
	/// *NOTE*: Partner has partner attributes from the database.
	///         These attributes are called by using C# Attributes.
	///			The PartnerAttributeCollection is private and should
	///			remain private.  So pleas always use Parter's attribute 
	///			feature to retreive them.
	/// </summary>
	///
    [Serializable]
	public class Partner : EnvironmentBase, IComparable {
		private int partnerID = int.MinValue;
		private string name = null;
		private int partnerTypeID = int.MinValue; // league, association, group
		private string host = null;
		private bool hasCollectionSite = false;
		private ESubsProgramCollection programCollection = null;
		private string guid = null;
		private PartnerAttributeCollection partnerAttributeCollection = null;
		protected PartnerComparable sortBy = PartnerComparable.Name;
        // Added by Jiro Hidaka (November 14, 2008)
        private PartnerLinkCollection partnerLinkCollection = null;
        private PartnerProductOffer partnerProductOffer = null;
        private ProductOffer.ESubs esubsProductOffer;

		public Partner() {
			partnerID = int.MinValue;
			programCollection = new ESubsProgramCollection();
			partnerAttributeCollection = new PartnerAttributeCollection();
            partnerLinkCollection = new PartnerLinkCollection();
            partnerProductOffer = new PartnerProductOffer();
            esubsProductOffer = ProductOffer.ESubs.ALL;
		}

		public Partner(string _name, int _partnerTypeID, bool _hasCollectionSite) {
			name = _name;
			partnerTypeID = _partnerTypeID;
			hasCollectionSite = _hasCollectionSite;
		}

		public void UpdateInDatabase() {
			try {
				ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
				dbo.UpdatePartner(partnerID, name, hasCollectionSite);
			} catch (Exception ex) {
				throw new ESubsGlobalException(ex.Message, ex, this);
			}
		}

		public bool InsertPartner() {
			try 
			{
				DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
				return dbo.InsertPartner(partnerID, partnerTypeID, name, hasCollectionSite);
			}
			catch (Exception ex)
			{
				throw new ESubsGlobalException(ex.Message, ex, this);
			}
		}

		public void SetPartnerAttributeCollection(PartnerAttributeCollection _pac) {
			try 
			{
				partnerAttributeCollection = _pac;
			}
			catch (Exception ex)
			{
				throw new ESubsGlobalException(ex.Message, ex, this);
			}
		}

		public void Save() {
			try 
			{
				// web environment
				if(System.Web.HttpContext.Current.Session != null) 
				{
					System.Web.HttpContext.Current.Session["_ESUBS_GLOBAL_PARTNER_"] = this;
				}
			}
			catch (Exception ex)
			{
				throw new ESubsGlobalException(ex.Message, ex, this);
			}
		}

		public static Partner Create() {
			// web environment
			if(System.Web.HttpContext.Current.Session != null) {
				if(System.Web.HttpContext.Current.Session["_ESUBS_GLOBAL_PARTNER_"] != null) {
					return (Partner)System.Web.HttpContext.Current.Session["_ESUBS_GLOBAL_PARTNER_"];
				}
			}
			return null;
		}

		public static Partner LoadByID(int id, Culture culture) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			Partner partner = dbo.GetPartnerByID(id, culture);
			return partner;
		}

		public static Partner LoadByGUID(string guid, Culture culture) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			Partner partner = dbo.GetPartnerByGUID(guid, culture);
			return partner;
		}

		public static Partner LoadByHost(string host, Culture culture) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			Partner partner = dbo.GetPartnerByHost(host, culture);
			return partner;
		}
		public static Partner GetPartnerByPaymentID( int paymentID)
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPartnerByPaymentID(paymentID);
		}
		public static Partner[] GetPartners() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			Partner[] partners = dbo.GetPartners(Culture.DEFAULT);
			return partners;
		}
		
		public static Partner[] GetPartners(Culture culture) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			Partner[] partners = dbo.GetPartners(culture);
			return partners;
		}

		public static PartnerCollections GetPartnerCollections() 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			PartnerCollections partners = dbo.GetPartnerCollections(Culture.DEFAULT);
			return partners;
		}
        		
		public static Payment.PaymentInfo GetPaymentInfoByPartnerID(int partnerID) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentInfoByPartnerID(partnerID);
		}

		#region Properties
		public int PartnerID {
			set { partnerID = value; }
			get { return partnerID; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public string PartnerPath
		{
			get 
			{
                // Changes done by Andre Showairy on July 23, 2008
                // old code:
                //      if (partnerAttributeCollection != null)
                // problem:
                //      User with culture en-CA in its record in the member table.
                //      partnerAttributeCollection.GetPartnerAttributeByName("partner_path") is null.
                //      old code should work when all settings for en-CA is set properly.
				if (partnerAttributeCollection != null &&
                    partnerAttributeCollection.GetPartnerAttributeByName("partner_path") != null )
				{
					return partnerAttributeCollection.GetPartnerAttributeByName("partner_path").Value;
				}

				return null;
			}
		}

        public string ESubsURL
        {
            get
            {
                if (partnerAttributeCollection != null &&
                    partnerAttributeCollection.GetPartnerAttributeByName("esubs_url") != null)
                {
                    return partnerAttributeCollection.GetPartnerAttributeByName("esubs_url").Value;
                }

                return null;
            }
        }

        public string EIN
        {
            get
            {
                if (partnerAttributeCollection != null &&
                    partnerAttributeCollection.GetPartnerAttributeByName("partner_ein") != null)
                {
                    return partnerAttributeCollection.GetPartnerAttributeByName("partner_ein").Value;
                }

                return null;
            }
        }

		public int PartnerTypeID {
			set { partnerTypeID = value; }
			get { return partnerTypeID; }
		}

		public string Host {
			set { host = value; }
			get { return host; }
		}

		public ESubsProgramCollection ProgramCollection {
			set { programCollection = value; }
			get { return programCollection; }
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

        public PartnerLinkCollection PartnerLinkCollection
        {
            set { partnerLinkCollection = value; }
            get { return partnerLinkCollection; }
        }

        public PartnerProductOffer PartnerProductOffer
        {
            set { partnerProductOffer = value; }
            get { return partnerProductOffer; }
        }

        public ProductOffer.ESubs ESubsProductOffer
        {
            set { esubsProductOffer = value; }
            get { return esubsProductOffer; }
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
