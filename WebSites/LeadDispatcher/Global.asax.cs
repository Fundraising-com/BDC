using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using GA.BDC.Core.EnterpriseComponents;
using System.Data;
using System.Text;
using log4net;


namespace CRMWeb 
{
	/// <summary>
	/// Summary description for Global.
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
      private static readonly ILog _logger = LogManager.GetLogger(typeof(Global));
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;


	
		public struct SessionVariables {
			
			public const string CLIENT_INFO = "clientInfo";
            public const string LEAD_INFO = "leadInfo";
			public const string SALE_INFO = "saleInfo";
            public const string USER_INFO = "userInfo";
            public const string AUTHENTIFICATED = "authentificated";
            public const string CURRENTTAB = "currentTab";
            public const string DVUNASSIGNEDLEADS = "dvUnassignedLeads";
            public const string CURRENTDATETEXTBOX = "currentDateTextBox";
            public const string USER_ID = "userID";
			public const string CURRENT_LEAD_ID = "currentLeadID";
            public const string PROJECT_LOCATION = "projectLocation";
            public const string CONFIRMATION_EMAIL_SUBJECT= "confirmationEmailSubject";
            public const string ACCESS_LEAD_ACCOUNTS_ONLY = "accessLeadAccountsOnly";
			public const string LEAD_ACCOUNTS_ID = "leadAccountsID";

	
			
		}

		public class CRMException : ApplicationException {
			public CRMException(string customMessage, 
				Exception inner, int dbSessionId, string callerMethod) : 
				base(customMessage, inner) {
				_logger.Fatal(customMessage, inner);

				
				
			}
		}

		public Global()
		{
			InitializeComponent();
		}	
		
		protected void Application_Start(Object sender, EventArgs e)
		{

		}
 
		protected void Session_Start(Object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_EndRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_Error(Object sender, EventArgs e)
		{

		}

		protected void Session_End(Object sender, EventArgs e)
		{
		/*	Session[Global.SessionVariables.AUTHENTIFICATED] = "no";
			Session[Global.SessionVariables.ACCESS_LEAD_ACCOUNTS_ONLY] = "false";
			Response.Redirect("default.aspx");
		*/	
		}

		protected void Application_End(Object sender, EventArgs e)
		{

		}
			
		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.components = new System.ComponentModel.Container();
		}
		#endregion
	}
}

