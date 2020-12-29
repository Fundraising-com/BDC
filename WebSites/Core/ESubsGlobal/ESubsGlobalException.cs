/* Title:	Environment Exception
 * Author:	Jean-Francois Buist
 * Summary:	Throw an exception and add XML informations of the environment and the user.
 * 
 * Create Date:	August 1, 2005
 * 
 */

using System;

namespace GA.BDC.Core.ESubsGlobal {
	/// <summary>
	/// Summary description for ESubsGlobalException.
	/// </summary>
	public class ESubsGlobalException : Exception {
		public ESubsGlobalException() {
			
		}

		public ESubsGlobalException(string message) : base(message) {		

		}

		public ESubsGlobalException(string message, System.Exception innerException) : base(message, innerException) {

		}

		public ESubsGlobalException(string message, System.Exception innerException, 
									EnvironmentBase env, Users.eSubsGlobalUser user) : 
			base(message + Environment.NewLine + ESubsGlobalException.CreateMessage(env, user), innerException) 
		{

		}

		public ESubsGlobalException(string message, System.Exception innerException, 
			Users.eSubsGlobalUser user) : this(message + Environment.NewLine +  ESubsGlobalException.CreateMessage(null, user), innerException) 
		{

		}

		public ESubsGlobalException(string message, System.Exception innerException, 
			EnvironmentBase env) : this(message + Environment.NewLine + ESubsGlobalException.CreateMessage(env, null), innerException) 
		{

		}

		public static string CreateMessage(EnvironmentBase env, Users.eSubsGlobalUser user) {
			bool useArtefact = true;

			useArtefact = (GA.BDC.Core.EnterpriseComponents.Helper.GetWebConfigValue("FormatedError").ToLower() == "true"? true: false);

			string message = "";
			string artefactMessage = "$artefact{REPORT}";

			if(useArtefact) {
				string userXML = "";
				string environmentXML = "";
				string webTrackingXML = "";

				WebTracking.VisitorLog visitorLog = new WebTracking.VisitorLog();

				if(System.Web.HttpContext.Current != null) {	// web based
					try {
						visitorLog = WebTracking.VisitorLog.Create(System.Web.HttpContext.Current.Session);
						webTrackingXML = visitorLog.ToXmlString();
					} catch { /* make sure no exception are thrown in the exception object */ }
				}

				if (env != null) {
					environmentXML += env.ToXmlString();
					message += "\r\n\r\n" +	env.ToXmlString() + "\r\n\r\n";
				}

				if(user != null) {
					if(!user.IsUnknown) {
						userXML = user.ToXmlString();
						message += "\r\n\r\n" +	user.ToXmlString() + "\r\n\r\n";
					} else {
						userXML = "<UserType>Unknown User</UserType>";
					}
				}

				if(true) {
					// $artefact tells the logging system that the following 
					// message can be parsed by the artefact SeeThat formater
					// message = "$artefact{START}<eSubs>" + message + "</eSubs>$artefact{END}";
					artefactMessage += "$artefact{START}";
					artefactMessage += "<MagFundraising>\r\n";

					if(userXML != "") {
						artefactMessage += "<eSubsUser>\r\n";
						artefactMessage += userXML;
						artefactMessage += "</eSubsUser>\r\n";
					}

					if(environmentXML != "") {
						artefactMessage += "<eSubsEnvironemnt>\r\n";
						artefactMessage += environmentXML;
						artefactMessage += "</eSubsEnvironemnt>\r\n";
					}

					if(webTrackingXML != "") {
						artefactMessage += "<WebTrackingInformation>\r\n";
						artefactMessage += webTrackingXML;
						artefactMessage += "</WebTrackingInformation>\r\n";
					}

					string links = "<QALinks>\r\n" +
						"	<VisitorSteps>[++MH++]</VisitorSteps>\r\n" +
						"	<CampaignManager>[++CM++]</CampaignManager>\r\n" +
						"	<DecrypterUtil>[++DC++]</DecrypterUtil>\r\n" +
						"	<CustCare>[++CUSTCATE++]</CustCare>\r\n" +
						"</QALinks>\r\n";

					// set the visitor steps link
					if(webTrackingXML != "") {
						links = links.Replace("[++MH++]", System.Web.HttpUtility.HtmlEncode("<a href='http://www.magfundraising.com/AdminVisitorSteps.aspx?bug_vsid=" + visitorLog.VisitorLogID + "'>Click Here To View Vistor Steps</a>"));
					} else {
						links = links.Replace("[++MH++]", "Not Available");
					}

					// set the decrypter url
					links = links.Replace("[++DC++]", System.Web.HttpUtility.HtmlEncode("<a href='http://www.magfundraising.com/AdminDecrypter.aspx'>Decrypt URL Params</a>"));

					// set the campaign manager and qa link
					if(env != null) {
						if(env is ESubsGlobal.eSubsGlobalEnvironment) {
							ESubsGlobal.eSubsGlobalEnvironment myEnv =
								(ESubsGlobal.eSubsGlobalEnvironment) env;

							if(myEnv.Event != null) {
								links = links.Replace("[++CM++]", System.Web.HttpUtility.HtmlEncode("<a href='http://www.magfundraising.com/CMHome.aspx?ep=" + myEnv.Event.SponsorEventParticipationID + "'>Click Here To Log-in the Sponsor Campaign Manager</a>"));
								links = links.Replace("[++CUSTCATE++]", "http://efrnet/IFramePageInTab/Default.aspx?TabID=CustCareID&eventid=" + myEnv.Event.EventID);
							} else {
								links = links.Replace("[++CM++]", "Unable to create link (no information)");
								links = links.Replace("[++CUSTCATE++]", "Unable to generate link");
							}
						} else {
							links = links.Replace("[++CM++]", "Unable to create link (no information)");
							links = links.Replace("[++CUSTCATE++]", "Unable to generate link");
						}
					} else {
						links = links.Replace("[++CM++]", "Unable to create link (no information)");
						links = links.Replace("[++CUSTCATE++]", "Unable to generate link");
					}

					artefactMessage += links;

					artefactMessage += "</MagFundraising>\r\n";
					artefactMessage += "$artefact{END}";

					return artefactMessage;
				}
			} else {
				if(user != null) {
					message += user.ToXmlString();
				}

				if(env != null) {
					message += env.ToXmlString();
				}
			}

            return message;
		}
	}
}
