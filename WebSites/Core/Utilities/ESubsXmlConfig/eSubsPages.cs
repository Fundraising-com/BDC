//
//	March 17, 2005	-	Louis Turmel	Code Comments
//

using System;
using System.Xml;

namespace GA.BDC.Core.Utilities.ESubsXmlConfig
{
	
	/// <summary>
	/// 
	/// </summary>
	public class eSubsPages {
			
		/// <summary>
		/// Check into the xml file if the page ID required campaign login from organizer
		/// </summary>
		/// <param name="pFileName">Filename of xml file</param>
		/// <param name="pPageID">Page ID of the page</param>
		/// <returns>boolean</returns>
		public static bool PageRequiredCampaign(string pFileName, string pageName) {
			bool oIsRequired = true;
			XmlDocument oDoc = new XmlDocument();
			try {
				oDoc.Load(pFileName);
				XmlNode oWebPage = oDoc.SelectSingleNode(Variables.eSubsPages.__XPATH_CampaignNeededPageByName.Replace(Variables.eSubsPages.__WebPageID,pageName));
				if(oWebPage != null && oWebPage.Attributes["webPageName"].Value.ToLower() == pageName.ToLower())
					oIsRequired = true;				
			} catch(System.IO.IOException ex) {
				if(pFileName.Length <= 0)
					throw new Exception("The parameter pFileName should contain valid filename",ex);
			} catch(System.Xml.XmlException ex) {
				throw ex;
			} catch(Exception ex) {
				throw ex;
			} finally {

			}
			return oIsRequired;	
		}

		/// <summary>
		/// Check into the xml file if the partner id is defined as a template.
		/// </summary>
		/// <param name="filename">Filename</param>
		/// <param name="partnerID">Partner ID</param>
		/// <returns>Boolean</returns>
		public static bool PartnerTemplate(string filename, short partnerID) {
			bool template = false;
			XmlDocument doc = new XmlDocument();
			try {
				doc.Load(filename);
				XmlNode node = doc.SelectSingleNode(Variables.eSubsPages.__TemplateID.Replace(Variables.eSubsPages.__PartnerID, partnerID.ToString()));
				if(node != null) {
					template = true;
				}
			} catch(System.IO.IOException ioEx) {
				throw ioEx;
			} catch(System.Xml.XmlException xmlEx) {
				throw xmlEx;
			} catch(System.Exception ex) {
				throw ex;
			}

			return template;
		}
	}
}
