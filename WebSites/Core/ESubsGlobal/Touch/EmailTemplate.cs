using System;
using GA.BDC.Core.ESubsGlobal.DataAccess;

namespace GA.BDC.Core.ESubsGlobal.Touch
{
	/// <summary>
	/// EmailTemplate.
	/// </summary>
    [Serializable]
	public class EmailTemplate : EnvironmentBase
	{

		#region Fields
		private int _templateId = int.MinValue;
		private int _templateTypeId = int.MinValue;
		private string _cultureCode = null;
		private string _subject = null;
		private string _templateName = null;
		private string _description = null;
		private string _textBody = null;
		private string _htmlBody = null;
		private string _procedureCall = null;
		private string _fromName = null;
		private string _fromEmail = null;
		private string _replyToName = null;
		private string _replyToEmail = null;
		private string _bounceName = null;
		private string _bounceEmail = null;
		private string _textFooter = null;
		private string _htmlFooter = null;
		#endregion

		#region Constructors
		public EmailTemplate()
		{

		}
		#endregion

		#region Methods
		public static EmailTemplate Create(int emailTemplateId, string cultureCode)
		{
			ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
			return dbo.GetEmailTemplate(emailTemplateId, cultureCode);
		}

		public static bool Exists(int emailTemplateId, string cultureCode)
		{
			try 
			{
				ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
				if (dbo.GetEmailTemplate(emailTemplateId, cultureCode) != null)
					return true;
				
			}
			catch {}

			return false;
		}

		public void UpdateDatabase()
		{
			try 
			{
				ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
				dbo.UpdateEmailTemplate(this);
			}
			catch (Exception ex)
			{
				throw new ESubsGlobalException(ex.Message, ex);
			}
		}

		public void InsertDatabase()
		{
			try 
			{
				ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
				dbo.InsertEmailTemplate(this);
			}
			catch (Exception ex)
			{
				throw new ESubsGlobalException(ex.Message, ex);
			}
		}
		#endregion

		#region Properties
		public int TemplateId 
		{
			get { return _templateId;}
			set { _templateId = value;}
		}

		public int TemplateTypeId 
		{
			get { return _templateTypeId;}
			set { _templateTypeId = value;}
		}

		public string CultureCode
		{
			get { return _cultureCode;}
			set { _cultureCode = value;}
		}

		public string Subject
		{
			get { return _subject; }
			set { _subject = value; }
		}

		public string TemplateName
		{
			get { return _templateName; }
			set { _templateName = value; }
		}

		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}

		public string TextBody
		{
			get { return _textBody; }
			set { _textBody = value; }
		}

		public string HtmlBody
		{
			get { return _htmlBody; }
			set { _htmlBody = value; }
		}

		public string ProcedureCall
		{
			get { return _procedureCall; }
			set { _procedureCall = value; }
		}

		public string FromName
		{
			get { return _fromName; }
			set { _fromName = value; }
		}

		public string FromEmail
		{
			get { return _fromEmail; }
			set { _fromEmail = value; }
		}

		public string ReplyToName
		{
			get { return _replyToName; }
			set { _replyToName = value; }
		}

		public string ReplyToEmail
		{
			get { return _replyToEmail; }
			set { _replyToEmail = value; }
		}

		public string BounceName
		{
			get { return _bounceName; }
			set { _bounceName = value; }
		}

		public string BounceEmail
		{
			get { return _bounceEmail; }
			set { _bounceEmail = value; }
		}

		public string TextFooter
		{
			get { return _textFooter; }
			set { _textFooter = value; }
		}

		public string HtmlFooter
		{
			get { return _htmlFooter; }
			set { _htmlFooter = value; }
		}
		#endregion

	}
}
