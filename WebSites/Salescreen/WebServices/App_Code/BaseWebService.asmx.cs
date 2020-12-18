using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Web.Security;
using QSPForm.Business;

namespace QSPFormWebServices
{
	/// <summary>
	/// Summary description for BaseWebService1.
	/// </summary>
	public class BaseWebService : System.Web.Services.WebService
	{

		private const string IDNAME = "ID";
		private const string ROLENAME = QSPForm.Business.AuthSystem.ROLE;
		private const string FMIDNAME = "fm_id";
		private const string REGISTRYIDNAME = "Registry_ID";
		private const string USER_TABLE_INFO = "UserTableInfo";

		public BaseWebService()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();
		}

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion

		public int RegistryID
		{
			get
			{	
				try
				{
					return Convert.ToInt32(HttpContext.Current.Session[REGISTRYIDNAME]);
				}
				catch
				{
					
					return -1;
				}
			}
			set
			{
				HttpContext.Current.Session[REGISTRYIDNAME] = value;			
			}
		}
		/// <summary>
		/// Get the Role of the current user
		/// </summary>
		public string FMID
		{
			get
			{	
				try
				{
					return HttpContext.Current.Session[FMIDNAME].ToString();
				}
				catch
				{
					//return default role
					return "";
				}
			}
			set
			{
				HttpContext.Current.Session[FMIDNAME] = value;			
			}
		}
		/// <summary>
		/// Get the Role of the current user
		/// </summary>
		public int Role
		{
			get
			{	
				try
				{
					return Convert.ToInt32(HttpContext.Current.Session[ROLENAME]);
				}
				catch
				{
					//return default role
					return 0;
				}
			}
			set
			{
				HttpContext.Current.Session[ROLENAME] = value;			
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool IsIDNull()
		{
			try
			{
				return HttpContext.Current.Session[IDNAME] == null;
			}
			catch
			{
				return false;
			}
		}
		
		/// <summary>
		/// Get the ID of the current user
		/// </summary>
		public  int UserID
		{
			get
			{
				try
				{				
					return Convert.ToInt32(HttpContext.Current.Session[IDNAME]);
				}
				catch
				{
					return 0;
				}
			}
			set
			{
				HttpContext.Current.Session[IDNAME] = value;			
			}
		}
		
		public  void setIDToNull()
		{
			try
			{
				HttpContext.Current.Session[IDNAME] = null;
			}
			catch
			{}
		}
		public void setRoleToNull()
		{
			try
			{
				HttpContext.Current.Session[ROLENAME] = null;
			}
			catch
			{}
		}

		protected void VerifyAuthentification()
		{
			if((!HttpContext.Current.Request.IsAuthenticated) && this.EnableAuthentification)
			{
				throw new Exception("Access denied");
			}
		}
	

		/// <summary>
		/// This method is used to authenticate the user.
		/// A user must be authenticate to be able to use any 
		/// web method.
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		
		[WebMethod(EnableSession=true)]
		public bool LogonUser(string userName, string password) 
		{
			bool isSuccess = false;
			QSPForm.Business.AuthSystem authSys = new QSPForm.Business.AuthSystem();				
			int ID = authSys.QSPForm_Authentication(userName, password, AuthSystem.MODE_LOGIN_USER);
			//if User is found in DB
			if (ID != 0)
			{
				//Store the info in the session via the base page
				UserID = ID;
				this.Role = authSys.Role;
				this.FMID = authSys.FM_ID;					
				this.RegistryID = authSys.RegistryID;
				isSuccess = true;
				FormsAuthentication.SetAuthCookie(userName,false);	
			}			

			return isSuccess;

		}

		private bool EnableAuthentification
		{
			get
			{
				try
				{
					return Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["Authentification"].ToString());
				}
				catch{return false;}
			}
		}
	}
}
