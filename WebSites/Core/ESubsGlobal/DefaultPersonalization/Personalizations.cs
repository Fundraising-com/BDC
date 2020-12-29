using System;
using System.Collections.Generic;
using System.Xml; 

namespace GA.BDC.Core.ESubsGlobal.DefaultPersonalization
{

	/// <summary>
	/// Summary description for Personalizations.
	/// </summary>
	public class Personalizations
	{


        private List<Partner> partnerlist;

		public Personalizations()
		{
			
            partnerlist = new List<Partner>();
		}

		public virtual void LoadXML(string filename) 
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(filename);
			foreach(XmlNode node in doc.ChildNodes) 
			{
				this.LoadPersonalizations(node);
			}
		}

		public virtual void LoadPersonalizations(XmlNode node) 
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

        public List<Partner> PartnerList 
		{
			set { partnerlist = value; }
			get { return partnerlist; }
		}

	}



}
