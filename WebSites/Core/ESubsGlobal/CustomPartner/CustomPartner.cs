
using System;
using System.Collections;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal.CustomPartner
{
	/// <summary>
	/// Summary description for CustomPartner.
	/// </summary>
	public class CustomPartner
	{
		public const string CACHE_KEY = "_ES_CACHE_CUSTOMIZED_PARTNER_";

		private ArrayList partnerlist = new ArrayList();


		public CustomPartner() 
		{

		}

		public static CustomPartner Create(System.Web.Caching.Cache cache) 
		{
			if(cache[CACHE_KEY] != null) 
			{
				return (CustomPartner)cache[CACHE_KEY];
			} 
			else 
			{
				return null;
			}
		}

		public void LoadXML(string filename) 
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(filename);
			foreach(XmlNode node in doc.ChildNodes) 
			{
				this.LoadCustomPartner(node);
			}
		}

		public Partner GetPartnerByID(string childName) 
		{
			Partner defaultPartner = null;
			foreach(Partner partner in partnerlist) 
			{
				if(partner.ID.ToLower() == childName.ToLower()) 
				{
					return partner;
				} 
				else if(partner.ID.ToLower() == "default") 
				{
					defaultPartner = partner;
				}
			}
			return defaultPartner;
		}

		public void LoadCustomPartner(XmlNode node) 
		{
			
			foreach(XmlNode child in node) 
			{
				if(child.Name.ToLower() == "Partner".ToLower()) 
				{
					Partner partner = new Partner();
					partner.LoadPartner(child);
					AddPartner(partner);
				}
			}
		}

		public void AddPartner(Partner partner) 
		{
			PartnerList.Add(partner);
		}

		public ArrayList PartnerList 
		{
			set { partnerlist = value; }
			get { return partnerlist; }
		}

	}
}
