
using System;
using System.Collections.Generic;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal.DefaultPersonalization
{
	/// <summary>
	/// Summary description for Cultures.
	/// </summary>
	public class Cultures 
	{

        private List<Culture> culturelist;


		public Cultures() 
		{
            culturelist = new List<Culture>();
		}

		public virtual Culture GetCultureByID(string childName) 
		{
			foreach(Culture culture in culturelist) 
			{
				if(culture.ID.ToLower() == childName.ToLower()) 
				{
					return culture;
				}
			}
			return null;
		}


		public virtual Culture GetCultureByName(string childName) 
		{
			foreach(Culture culture in culturelist) 
			{
				if(culture.Name.ToLower() == childName.ToLower()) 
				{
					return culture;
				}
			}
			return null;
		}


		public virtual void LoadCultures(XmlNode node) 
		{			
			foreach(XmlNode child in node) 
			{
				if(child.Name.ToLower() == "Culture".ToLower()) 
				{
					Culture culture = new Culture();
					culture.LoadCulture(child);
					AddCulture(culture);
				}
			}
		}

		public void AddCulture(Culture culture) 
		{
			CultureList.Add(culture);
		}

        public List<Culture> CultureList 
		{
			set { culturelist = value; }
			get { return culturelist; }
		}

	}
}
