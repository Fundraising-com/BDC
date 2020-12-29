
using System;
using System.Collections.Generic;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal.DefaultPersonalization
{
	/// <summary>
	/// Summary description for Partner.
	/// </summary>
	public class Partner
	{


		private string id;
		private string name;
        private List<Cultures> cultureslist;

		public Partner() 
		{
            cultureslist = new List<Cultures>();
		}


		public void AddCultures(Cultures cultures) 
		{
			CulturesList.Add(cultures);
		}


        public List<Cultures> CulturesList 
		{
			set { cultureslist = value; }
			get { return cultureslist; }
		}


		public virtual void LoadPartner(XmlNode node) 
		{
			
			foreach(XmlNode child in node) 
			{
				if(child.Name.ToLower() == "ID".ToLower()) 
				{
					ID = child.InnerText;
				}
				else if(child.Name.ToLower() == "Name".ToLower()) 
				{
					Name = child.InnerText;
				} 
				else if(child.Name.ToLower() == "Cultures".ToLower()) 
				{
					Cultures cultures = new Cultures();
					cultures.LoadCultures(child);
					AddCultures(cultures);
				}
			}
		}


		public string ID 
		{
			set { id = value; }
			get { return id; }
		}

		public string Name 
		{
			set { name = value; }
			get { return name; }
		}


	}
}
