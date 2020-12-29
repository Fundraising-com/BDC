using System;
using GA.BDC.Core.ESubsGlobal.DataAccess;

namespace GA.BDC.Core.ESubsGlobal.Touch
{
	/// <summary>
	/// Summary description for EmailTemplateList.
	/// </summary>
    [Serializable]
	public class EmailTemplateList : EnvironmentBase
	{

		#region Fields
		private int _templateId = int.MinValue;
		private string _templateName = null;
		private string _description = null;
		private string _cultureCode = null;
		#endregion	

		#region Constructors
		public EmailTemplateList()
		{

		}
		#endregion

		#region Methods
		public static EmailTemplateList[] GetList()
		{
			ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
			return dbo.GetEmailTemplateList();
		}
		#endregion

		#region Properties
		public int TemplateId
		{
			get { return _templateId; }
			set { _templateId = value; }
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

		public string CultureCode
		{
			get { return _cultureCode; }
			set { _cultureCode = value; }
		}
		#endregion

		
	}
}
