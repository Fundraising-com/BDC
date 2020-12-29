//
//	March 17, 2005	-	Louis Turmel	Code Comments
//

using System;
using System.Xml;

using GA.BDC.Core.Utilities.ESubsXmlConfig.Variables;

namespace GA.BDC.Core.Utilities.ESubsXmlConfig
{

	/// <summary>
	/// class containing some static functions and method for getting the Mail Provider informations
	/// </summary>
	public class MailProviders {
		/// <summary>
		/// static function returning the name of Mail Provider Name
		/// </summary>
		/// <param name="pFilename"></param>
		/// <param name="pMailProviderName"></param>
		/// <returns></returns>
		public static string GetMailProviderName(string pFilename, string pMailProviderName) {
			string oResult = "";
			XmlDocument oDoc = new XmlDocument();
			try {
				oDoc.Load(pFilename);
				oResult = oDoc.SelectSingleNode(Variables.MailProvidersVars.__XPath_MailProvider.Replace(Variables.MailProvidersVars.__Provider,pMailProviderName)).Attributes["DisplayName"].Value;
			} catch(XmlException ex) {
				if(pMailProviderName.Length <= 0)
					throw new Exception("The parameter pMailProviderName should contain valid MailProvider Name",ex);
				else
					throw ex;
			} catch(System.IO.IOException ex) {
				if(pFilename.Length <= 0)
					throw new Exception("The parameter pFilename should contain valid filename",ex);
				else 
					throw ex;
			} catch(Exception ex) {
				throw ex;
			} finally {

			}
			return oResult;
		}
	}
}
