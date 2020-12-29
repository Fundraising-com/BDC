using System;
using System.Collections;
using System.Xml; 

namespace GA.BDC.Core.ESubsGlobal.DefaultPersonalization
{
	/// <summary>
	/// Summary description for PersonalizationSection.
	/// </summary>
	public class PersonalizationSection 
	{

		private string id;
		private string name;
		private string personalizationid;
		private string headertitle1;
		private string headertitle2;
		private string body;
		private string fundraisinggoal;
		private string sitebgcolor;
		private string headerbgcolor;
		private string headercolor;
		private string groupurl;
		private string imageurl;

		public PersonalizationSection() 
		{

		}

		public virtual ESubsGlobal.Personalization GetPersonalizationObject(int eventParticipationID) 
		{
			ESubsGlobal.Personalization perso =
				new ESubsGlobal.Personalization();
			perso.EventParticipationID = eventParticipationID;
			perso.Body = body;
			perso.FundraisingGoal = decimal.Parse(fundraisinggoal);
			perso.GroupUrl = groupurl;
			perso.HeaderBackgroundColor = headerbgcolor;
			perso.HeaderTextColor = headercolor;
			perso.HeaderTitle1 = headertitle1;
			perso.HeaderTitle2 = headertitle2;
			perso.ImageUrl = imageurl;
			perso.PersonalizationId =0;
			perso.SiteBackgroundColor = sitebgcolor;
			return perso;
		}

		public virtual void LoadPersonalizationSection(XmlNode node) 
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
				else if(child.Name.ToLower() == "PersonalizationID".ToLower()) 
				{
					PersonalizationID = child.InnerText;
				}
				else if(child.Name.ToLower() == "HeaderTitle1".ToLower()) 
				{
					HeaderTitle1 = child.InnerText;
				}
				else if(child.Name.ToLower() == "HeaderTitle2".ToLower()) 
				{
					HeaderTitle2 = child.InnerText;
				}
				else if(child.Name.ToLower() == "Body".ToLower()) 
				{
					Body = child.InnerText;
				}
				else if(child.Name.ToLower() == "FundraisingGoal".ToLower()) 
				{
					FundraisingGoal = child.InnerText;
				}
				else if(child.Name.ToLower() == "SiteBGColor".ToLower()) 
				{
					SiteBGColor = child.InnerText;
				}
				else if(child.Name.ToLower() == "HeaderBGColor".ToLower()) 
				{
					HeaderBGColor = child.InnerText;
				}
				else if(child.Name.ToLower() == "HeaderColor".ToLower()) 
				{
					HeaderColor = child.InnerText;
				}
				else if(child.Name.ToLower() == "GroupURL".ToLower()) 
				{
					GroupURL = child.InnerText;
				}
				else if(child.Name.ToLower() == "ImageURL".ToLower()) 
				{
					ImageURL = child.InnerText;
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

		public string PersonalizationID 
		{
			set { personalizationid = value; }
			get { return personalizationid; }
		}

		public string HeaderTitle1 
		{
			set { headertitle1 = value; }
			get { return headertitle1; }
		}

		public string HeaderTitle2 
		{
			set { headertitle2 = value; }
			get { return headertitle2; }
		}

		public string Body 
		{
			set { body = value; }
			get { return body; }
		}

		public string FundraisingGoal 
		{
			set { fundraisinggoal = value; }
			get { return fundraisinggoal; }
		}

		public string SiteBGColor 
		{
			set { sitebgcolor = value; }
			get { return sitebgcolor; }
		}

		public string HeaderBGColor 
		{
			set { headerbgcolor = value; }
			get { return headerbgcolor; }
		}

		public string HeaderColor 
		{
			set { headercolor = value; }
			get { return headercolor; }
		}

		public string GroupURL 
		{
			set { groupurl = value; }
			get { return groupurl; }
		}

		public string ImageURL 
		{
			set { imageurl = value; }
			get { return imageurl; }
		}

	}

}
