
using System;
using System.Collections.Generic;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal.DefaultPersonalization
{
	/// <summary>
	/// Summary description for Culture.
	/// </summary>
	public class Culture
	{

        private List<PersonalizationSections> personalizationsectionslist;
		private string id;
		private string name;

		public Culture() 
		{
            personalizationsectionslist = new List<PersonalizationSections>();
		}



		public virtual void LoadCulture(XmlNode node) 
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
				else if(child.Name.ToLower() == "PersonalizationSections".ToLower()) 
				{
					PersonalizationSections personalizationsections = new PersonalizationSections();
					personalizationsections.LoadPersonalizationSections(child);
					AddPersonalizationSections(personalizationsections);
				}
			}
		}

		public void AddPersonalizationSections(PersonalizationSections personalizationsections) 
		{
			PersonalizationSectionsList.Add(personalizationsections);
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

        public List<PersonalizationSections> PersonalizationSectionsList 
		{
			set { personalizationsectionslist = value; }
			get { return personalizationsectionslist; }
		}

	}
}
