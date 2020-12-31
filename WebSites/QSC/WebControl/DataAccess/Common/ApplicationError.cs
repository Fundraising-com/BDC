using System;
using System.Web;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;

namespace QSP.WebControl.DataAccess.Common
{
	/// <summary>
	/// Summary description for ApplicationError.
	/// </summary>
	public class ApplicationError
	{
		
		internal static void ManageError(Exception EX)
		{
			
			/*int ErrorID =0;
			//Log info into database web site
			if(ApplicationConfiguration.DBInsertEnabled)
				ErrorID = Log_Exception(EX);

			//send mail error with the link
			if(ErrorID != 0 && ApplicationConfiguration.SendMailErrorEnabled)
			{
				new QSP.WebControl.SendMail(QSP.WebControl.MailTypes.ErrorWebApplication,"http://www.qsp.com/admin/errors_main.asp?site_id=4&id=" + ErrorID + "\r\n",ApplicationConfiguration.MailFrom,ApplicationConfiguration.MailTo,ApplicationConfiguration.MailSmtp,EX);
			}
			//send mail without link
			else if(ApplicationConfiguration.SendMailErrorEnabled)
			{
				new QSP.WebControl.SendMail(QSP.WebControl.MailTypes.ErrorWebApplication,"",ApplicationConfiguration.MailFrom,ApplicationConfiguration.MailTo,ApplicationConfiguration.MailSmtp,EX);
			}*/
			
			
		}
		///<summary>Logs exceptions into QSPError DB.</summary>
		///<param name="ex">The exception to be logged</param>
		///<param name="nv">A collection of additional developer comments</param>
		///<returns>the error ID #</returns>
		private static int Log_Exception(System.Exception ex)
		{
			/*StackTrace st = new StackTrace(ex, true);
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
		
			
			SqlConnection conn = null;
			try	
			{
				conn = new SqlConnection(QCAP.SystemFramework.ApplicationConfiguration.ErrorConnectionString);
			} 
			catch	
			{
				
			}
    
			//setup stored procedure call
			SqlCommand cmdAdd;
			cmdAdd = new SqlCommand( "QSPError..sp_Insert_Error_ASPNET", conn );
			cmdAdd.CommandType = CommandType.StoredProcedure;
			AddParam_Or_Null(ref cmdAdd, "@Stack",					ex.StackTrace.ToString()	);
			AddParam_Or_Null(ref cmdAdd, "@Project",				ex.Source.ToString()		);
			AddParam_Or_Null(ref cmdAdd, "@Namespace",				nspace						);
			AddParam_Or_Null(ref cmdAdd, "@Class",					cclass						);
			AddParam_Or_Null(ref cmdAdd, "@Func",					ex.TargetSite.ToString()	);
			AddParam_Or_Null(ref cmdAdd, "@Ffile",					"");//sf.GetFileName().ToString() );
			AddParam_Or_Null(ref cmdAdd, "@HelpLink",				ex.HelpLink					);
			AddParam_Or_Null(ref cmdAdd, "@ServerName",				sServerName					);
			AddParam_Or_Null(ref cmdAdd, "@ServerIP",				sServerIP					);
			AddParam_Or_Null(ref cmdAdd, "@Port",					sServerPort					);
			AddParam_Or_Null(ref cmdAdd, "@PageURL",				sPageURL					);
			AddParam_Or_Null(ref cmdAdd, "@BrowserInfo",			sBrowserType				);
			AddParam_Or_Null(ref cmdAdd, "@ASPErrorDescription",	sASPErrorDescription		);
			AddParam_Or_Null(ref cmdAdd, "@ASPErrorCategory",		sASPErrorCategory			);
			AddParam_Or_Null(ref cmdAdd, "@ASPErrorNumber",			null						);
			AddParam_Or_Null(ref cmdAdd, "@IPAddress",				sRemoteIP					);
			AddParam_Or_Null(ref cmdAdd, "@FormValues",				sFormValues					);
			AddParam_Or_Null(ref cmdAdd, "@FormMethod",				sRequestMethod				);

			string line = null;
			try     { line = sf.GetFileLineNumber().ToString(); }
			catch   { line = null; }
			AddParam_Or_Null(ref cmdAdd, "@LineNumber",				line						);
			
			string cols = null;
			try     { cols = sf.GetFileColumnNumber().ToString(); }
			catch   { cols = null; }
			AddParam_Or_Null(ref cmdAdd, "@ColumnNumber",			cols						);

			AddParam_Or_Null(ref cmdAdd, "@ReferingURL",			sReferer					);
			cmdAdd.Parameters.Add( "@SiteId",						4							);
			AddParam_Or_Null(ref cmdAdd, "@SessionVariables",		sSessionVariables			);
			AddParam_Or_Null(ref cmdAdd, "@AdditionalComments",		sAdditionalComments			);

			// ID Output Parameter
			SqlParameter outError;
			outError = cmdAdd.Parameters.Add( "@ErrorId", SqlDbType.Int );
			outError.Direction = ParameterDirection.Output;
    
			try
			{
				conn.Open();
				cmdAdd.ExecuteScalar();
				conn.Close();
				
			}
			catch
			{
				
			}
		
			int iErrorID=0;
			if (! Convert.IsDBNull(cmdAdd.Parameters["@ErrorId"].Value))
			{
				iErrorID = (int) cmdAdd.Parameters["@ErrorId"].Value;
			} 
			
			

			return iErrorID;*/
			return 0;
		}

		private static void AddParam_Or_Null(ref SqlCommand cmd, string param, string val)
		{
			if ( val == null || val.Trim() == "") 
			{
				cmd.Parameters.Add( param, DBNull.Value	); 
			} 
			else	
			{
				cmd.Parameters.Add( param, val.Trim()	); 
			}
		}
	}
}
