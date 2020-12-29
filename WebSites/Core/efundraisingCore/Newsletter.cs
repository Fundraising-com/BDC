using System;
using GA.BDC.Core.efundraisingCore.DataAccess;

namespace GA.BDC.Core.efundraisingCore
{
	/// <summary>
	/// Summary description for Newsletter.
	/// </summary>
	public class Newsletter
	{
		#region Fields
		
		int newsletterId;
		string referrer;
		string email;
		string fullname;
		bool unsubscribed;
		DateTime subscribeDate;
		int partnerId;
				
		#endregion
		
		public Newsletter(string referrer, string email, string fullname, int partnerId)
		{
			this.referrer = referrer;
			this.email = email;
			this.fullname = fullname;
			this.partnerId = partnerId;	
		}
		
		public Newsletter()
		{
		}
		
		public void Unsubscribe(string email, int partnerID) {
			try {
				EFundDatabase db = new EFundDatabase();
				db.UnSubscribeNewsletter(email, partnerID);	
			} catch(Exception ex) {
				throw;
			}
		}
		
		public int Subscribe(string name, string email, int partnerID) {
			int newId = 0;			
			try {
				EFundDatabase db = new EFundDatabase();
				newId = db.SubscribeNewsletter(name, email, partnerID);
			} catch (Exception ex) {
			
			}
			return newId;
		}
		
		public void Subscribe() 
		{
			try 
			{
				EFundDatabase db = new EFundDatabase();
				db.SubscribeNewsletter(this);
			} 
			catch (Exception ex) 
			{
			
			}
		}

		#region Properties
		
		public int PartnerId
		{
			get { return this.partnerId; }
			set { this.partnerId = value; }
		}

		public string Email
		{
			get { return this.email; }
			set { this.email = value; }
		}

		public System.DateTime SubscribeDate
		{
			get { return this.subscribeDate; }
			set { this.subscribeDate = value; }
		}

		public string Referrer
		{
			get { return this.referrer; }
			set { this.referrer = value; }
		}

		public string Fullname
		{
			get { return this.fullname; }
			set { this.fullname = value; }
		}

		public bool Unsubscribed
		{
			get { return this.unsubscribed; }
			set { this.unsubscribed = value; }
		}

		public int NewsletterId
		{
			get { return this.newsletterId; }
			set { this.newsletterId = value; }
		}
		
		#endregion
	}
}
