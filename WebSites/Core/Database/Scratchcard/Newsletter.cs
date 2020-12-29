using System;
using efundraising.Scratchcard.DataAccess;


namespace efundraising.Scratchcard
{
	/// <summary>
	/// Summary description for Newsletter.
	/// </summary>
	public class Newsletter
	{
		private string _name = "";
		private string _email = "";

		public Newsletter(string name, string email)
		{
			_name = name;
			_email = email;
		}

		public void InsertDatabase()
		{
			ScratchcardDatabase dbi = new ScratchcardDatabase();
			dbi.InsertNewsletter(this);
		}



		public string Name
		{
			get {return _name;}
		}

		public string Email
		{
			get {return _email;}
		}
	}
}
