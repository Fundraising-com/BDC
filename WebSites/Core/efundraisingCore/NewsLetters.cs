//
//	September 15, 2005	-	Louis Turmel	-	Class implementation
//

using System;

using GA.BDC.Core.Database.efundraising;

namespace GA.BDC.Core.efundraisingCore
{
	/// <summary>
	/// Summary description for NewsLetters.
	/// </summary>
	public class NewsLetters {
 
		public NewsLetters() {
			
		}

		/// <summary>
		/// Method doing the insertion of new email has newsletter system
		/// </summary>
		/// <param name="Referrer">Where the user come from</param>
		/// <param name="FullName">Full name of the person</param>
		/// <param name="EmailAddress">EmailAddress</param>
		/// <param name="PartnerId">Reference Partner Id Number</param>
		public static void AddNewEntry(string Referrer, string FullName, string EmailAddress, int PartnerId) {
			DatabaseObject oDbi = new DatabaseObject();
			try {
				oDbi.InsertToENewsLetter(Referrer, FullName, EmailAddress, PartnerId);
			} catch(Exception ex) {
				throw;
			}		
		}
	}
}
