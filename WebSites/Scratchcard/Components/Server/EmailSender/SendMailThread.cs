using System;
using GA.BDC.WEB.ScratchcardWeb.Components.Server.EmailTemplate;
using GA.BDC.Core.efundraisingCore;
using GA.BDC.Core.Configuration;

namespace GA.BDC.WEB.ScratchcardWeb.Components.Server.EmailSender
{
	internal sealed class SendMailThread 
	{
		public void SendLeadConfirmation() {			
			SendMail oSend = new SendMail();
			System.Threading.Thread oSendMail = new System.Threading.Thread(new System.Threading.ThreadStart(oSend.SendLeadConfirmation));
			oSendMail.Start();
		}

	}

	/// <summary>
	/// 
	/// </summary>
	internal sealed class SendMail {
		
		#region private fields

		private eFundEnv _eFundEnv;
		private System.Xml.XmlDocument _oXmlDoc;
		private string _SendFrom;
		private string _SmtpServerName;		
		private string[] _SendConfirmationTo;

		#endregion

		#region public constructor

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pEnv"></param>
		/// <param name="pXmlDoc"></param>
		/// <param name="pSendFrom"></param>
		/// <param name="pSmtpServerName"></param>
		/// <param name="pSendConfirmationTo"></param>
		public SendMail() {
			_eFundEnv = eFundEnv.Create();
			_oXmlDoc = new System.Xml.XmlDocument();
#if DEBUG
            _oXmlDoc.Load(System.Web.HttpContext.Current.Server.MapPath(ApplicationSettings.GetConfig()["LeadConfirmationReport.Debug",
				"TemplateFile"].ToString()));
			this._SendFrom = ApplicationSettings.GetConfig()["LeadConfirmationReport.Debug", "SendFrom"];
			this._SendConfirmationTo = ApplicationSettings.GetConfig()["LeadConfirmationReport.Debug", "SendTo"].Split(',');
#else
			_oXmlDoc.Load(System.Web.HttpContext.Current.Server.MapPath(GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["LeadConfirmationReport.Release",
				"TemplateFile"].ToString()));
			this._SendFrom = ApplicationSettings.GetConfig()["LeadConfirmationReport.Release", "SendFrom"];
			this._SendConfirmationTo = ApplicationSettings.GetConfig()["LeadConfirmationReport.Release", "SendTo"].Split(',');
#endif						
			this._SmtpServerName = ApplicationSettings.GetConfig()["Common.Email.SmtpServer", "host"];
		}

		#endregion

		#region public methods

		/// <summary>
		/// Method sending the confirmation email
		/// </summary>
		public void SendConfirmation() 
		{
			eFundEnv oEnv = eFundEnv.Create();
			GA.BDC.Core.efundraisingCore.Lead oNewLead = oEnv.LeadObject;
			EmailTemplate.EmailPreview oEmailPrev = EmailTemplate.EmailBodyBuilder.GetEmailBodyTemplate(oEnv,"FreeKit", this._oXmlDoc);
			try {
                GA.BDC.Core.Email.SendMail.Send(this._SmtpServerName, "\"" + this._eFundEnv.PartnerInfo.PartnerName + "" + "<" + _SendFrom + ">", 
					this._eFundEnv.LeadObject.Email,"","", "","", oEmailPrev.Subject,"",oEmailPrev.Body);
													
			} catch {
				throw;
			} 
			
		}

		public void SendLeadConfirmation() 
		{
			eFundEnv oEnv = eFundEnv.Create();
			GA.BDC.Core.efundraisingCore.Lead oNewLead = oEnv.LeadObject;
			try {
				string[] oReportReceiver = _SendConfirmationTo;
				EmailTemplate.EmailPreview oEmailAddNew = 
					EmailTemplate.EmailBodyBuilder.GetEmailBodyTemplate(oEnv, "AddNewLeadReport", this._oXmlDoc);
                GA.BDC.Core.Email.SendMail.Send(new GA.BDC.Core.Email.SmtpServer(_SmtpServerName), this._SendFrom, oReportReceiver
					,new string[0], new string[0],"","", oEmailAddNew.Subject, "", oEmailAddNew.Body);
			} catch {
				throw;
			}
		}

		#endregion
	}
}
