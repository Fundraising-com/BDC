using System;
using System.Collections.Generic;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal.DefaultPersonalization
{
	/// <summary>
	/// Summary description for PersonalizationSections.
	/// </summary>
	public class PersonalizationSections 
	{
        private string eventTypeId;
        private List<PersonalizationSection> personalizationsectionlist;


		public PersonalizationSections() 
		{
            personalizationsectionlist = new List<PersonalizationSection>();
		}

		public PersonalizationSection GetPersonalizationSectionByID(string childName) 
		{
			foreach(PersonalizationSection personalizationsection in personalizationsectionlist) 
			{
				if(personalizationsection.ID.ToLower() == childName.ToLower()) 
				{
					return personalizationsection;
				}
			}
			return null;
		}


		public PersonalizationSection GetPersonalizationSectionByName(string childName) 
		{
			foreach(PersonalizationSection personalizationsection in personalizationsectionlist) 
			{
				if(personalizationsection.Name.ToLower() == childName.ToLower()) 
				{
					return personalizationsection;
				}
			}
			return null;
		}


		public virtual void LoadPersonalizationSections(XmlNode node) 
		{
			
			foreach(XmlNode child in node) 
			{
                if (child.Name.ToLower() == "EventTypeID".ToLower())
                {
                    EventTypeId = child.InnerText;
                }
				else if(child.Name.ToLower() == "PersonalizationSection".ToLower()) 
				{
					PersonalizationSection personalizationsection = new PersonalizationSection();
					personalizationsection.LoadPersonalizationSection(child);
					AddPersonalizationSection(personalizationsection);
				}
			}
		}

		public void AddPersonalizationSection(PersonalizationSection personalizationsection) 
		{
			PersonalizationSectionList.Add(personalizationsection);
		}

        public string EventTypeId
        {
            set { eventTypeId = value; }
            get { return eventTypeId; }
        }

        public List<PersonalizationSection> PersonalizationSectionList 
		{
			set { personalizationsectionlist = value; }
			get { return personalizationsectionlist; }
		}

	}
}
