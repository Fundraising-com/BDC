using System;
using GA.BDC.Core.Database.Scratchcard;


namespace GA.BDC.Core.Database.Scratchcard.DataAccess
{
	/// <summary>
	/// Summary description for Newsletter.
	/// </summary>
	public class NewsletterSub
	{
		private string _name = "";
		private string _email = "";

		public NewsletterSub(string name, string email)
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
