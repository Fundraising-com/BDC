using System;
using GA.BDC.Core.efundraisingCore.DataAccess;

namespace GA.BDC.Core.efundraisingCore
{
	/// <summary>
	/// Summary description for DenaliDonation.
	/// </summary>
	public class DenaliDonation
	{
		public DenaliDonation()
		{
		}
		
		public void InsertDonationForm(string language_id, string first_name, string last_name, string company_name, string address, string city, string province, string postal_code, string phone_number, string email, float contribution, float donate_onreach, string credit_card, string credit_card_number, string credit_card_owner, string credit_card_expiration)
		{
			try
			{
				DenaliDatabase db = new DenaliDatabase();
				db.InsertDonationForm(language_id, first_name, last_name, company_name, address, city, province, postal_code, phone_number, email, contribution, donate_onreach, credit_card, credit_card_number, credit_card_owner, credit_card_expiration);
			}
			catch (Exception ex)
			{
				throw;	
			}
			
		}
	}
}
