using System;
using GA.BDC.Core.Database.Scratchcard;

namespace GA.BDC.Core.Database.Scratchcard.DataAccess
{
	/// <summary>
	/// Summary description for SuccessStory.
	/// </summary>
	public class Story
	{
		
		private string _story = "";
		
		public Story(int storyType, int groupTypeID)
		{
			ScratchcardDatabase dbi = new ScratchcardDatabase();
			_story = dbi.getStory(storyType, groupTypeID);
		}
		
		public string StoryString
		{
			get {return _story;}
		}

	}
}
