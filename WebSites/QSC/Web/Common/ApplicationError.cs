using System;
using System.Web;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;

namespace QSPFulfillment.CommonWeb
{
	///<summary>handle all execeptions bubbled up to this layer</summary>
	public class ApplicationError
	{
		public static void ManageError(Exception EX)
		{
			int ErrorID =0;
			//Log info into database web site
			if(System.Configuration.ConfigurationSettings.AppSettings["DBInsertEnabled"]=="Y")
				ErrorID = Log_Exception(EX);

			//send mail error with the link
			if(ErrorID != 0 && System.Configuration.ConfigurationSettings.AppSettings["SendMailErrorEnabled"]=="Y")
			{
				new SendMail(QSPFulfillment.CommonWeb.MailTypes.ErrorWebApplication,"http://www.qsp.com/admin/errors_main.asp?site_id=6&id=" + ErrorID + "\r\n",
					System.Configuration.ConfigurationSettings.AppSettings["MailFrom"].ToString(),
					System.Configuration.ConfigurationSettings.AppSettings["MailTo"].ToString(),
					System.Configuration.ConfigurationSettings.AppSettings["MailSmtp"].ToString(),EX);
			}
			//send mail without link
			else if(System.Configuration.ConfigurationSettings.AppSettings["SendMailErrorEnabled"]=="Y")
			{
				new SendMail(QSPFulfillment.CommonWeb.MailTypes.ErrorWebApplication,"",
					System.Configuration.ConfigurationSettings.AppSettings["MailFrom"].ToString(),
					System.Configuration.ConfigurationSettings.AppSettings["MailTo"].ToString(),
					System.Configuration.ConfigurationSettings.AppSettings["MailSmtp"].ToString(),EX);
			}


		}
		///<summary>Logs exceptions into QSPCanadaError DB.</summary>
		///<param name="ex">The exception to be logged</param>
		///<param name="nv">A collection of additional developer comments</param>
		///<returns>the error ID #</returns>
		private static int Log_Exception(System.Exception ex)
		{
			StackTrace st = new StackTrace(ex, true);
			StackFrame sf = st.GetFrame(0);
			string stack = ex.StackTrace.ToString().Replace("at ", "");


			//get the namespace
			int holder = stack.IndexOf(".", 0);
			string nspace;
			try   { nspace = stack.Substring(0, holder); }
			catch { nspace = null; }
			stack = stack.Replace(nspace + ".", "");

			holder = stack.IndexOf(".", 0);
			string cclass;
			try   { cclass = stack.Substring(0, holder); }
			catch { cclass = null; }
			stack = stack.Replace(cclass + ".", "");

			string sASPErrorDescription;
			try  { sASPErrorDescription = ex.Message.ToString().Replace("'", "''"); }
			catch{ sASPErrorDescription = ""; }

			string sASPErrorCategory;
			try  { sASPErrorCategory = ex.GetType().ToString().Replace("'", "''"); }
			catch{ sASPErrorCategory = ""; }

			string sRemoteIP 				= HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
			string sServerName				= HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
			string sServerIP				= HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"];
			string sServerPort				= HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
			string sRequestMethod			= HttpContext.Current.Request.ServerVariables["REQUEST_METHOD"];
			string sFormValues				= HttpContext.Current.Request.Form.ToString();
			string sBrowserType				= HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];
			string sReferer					= HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];

			//url
			string sPageURL;
			try  
			{
				char[] sep = {'/'};
				string []tmp    = HttpContext.Current.Request.ServerVariables["SERVER_PROTOCOL"].Split(sep);
				string protocol = tmp[0];
				string fullURL  = protocol + "://" + HttpContext.Current.Request.ServerVariables["Server_Name"] + HttpContext.Current.Request.ServerVariables["Script_Name"];

				if( HttpContext.Current.Request.ServerVariables["Query_String"] != "") 	
				{
					fullURL += "?" + HttpContext.Current.Request.ServerVariables["Query_String"];
				}
				sPageURL = fullURL;
			} 
			catch { sPageURL = null; }

			string sSessionVariables = "";
			if(HttpContext.Current.Session != null)
			{
				for(int i=0; i < HttpContext.Current.Session.Count; i++) 
				{
					sSessionVariables += "|" + HttpContext.Current.Session.Keys[i].ToString() + ": " + HttpContext.Current.Session.Contents[i].ToString();
				}
			}
			string sAdditionalComments = "";

			string line = null;
			try     { line = sf.GetFileLineNumber().ToString(); }
			catch   { line = null; }
			string cols = null;
			try     { cols = sf.GetFileColumnNumber().ToString(); }
			catch   { cols = null; }
			int iErrorID=0;

			DAL.ApplicationErrorDataAccess oErrorDAL = new DAL.ApplicationErrorDataAccess();

			oErrorDAL.InsertASPNET(
				  ex.StackTrace.ToString()	//Stack
				, ex.Source.ToString()		//Project
				, nspace					//Namespace
				, cclass					//Class
				, ex.TargetSite.ToString()	//Func
				, ""						//Ffile ////sf.GetFileName().ToString()
				, ex.HelpLink				//HelpLink
				, sServerName				//ServerName
				, sServerIP					//ServerIP
				, sServerPort				//Port
				, sPageURL					//PageURL
				, sBrowserType				//BrowserInfo
				, sASPErrorDescription		//ASPErrorDescription
				, sASPErrorCategory			//ASPErrorCategory
				, null						//ASPErrorNumber
				, sRemoteIP					//IPAddress
				, sFormValues				//FormValues
				, sRequestMethod			//FormMethod
				, line						//LineNumber
				, cols						//ColumnNumber
				, sReferer					//ReferingURL
				, 6							//SiteId
				, sSessionVariables			//SessionVariables
				, sAdditionalComments		//AdditionalComments
				, out iErrorID				//ErrorId (id value of error inserted)
				);

			return iErrorID;
		}

	}
}
