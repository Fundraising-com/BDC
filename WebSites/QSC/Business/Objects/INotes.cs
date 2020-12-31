using System;
using System.Data;
using Common;
using Common.TableDef;
using System.Configuration;

namespace Business.Objects
{
	public class iNotes
	{
		protected string userID;
		protected string password;
        protected string document;

		public iNotes(string iNotesUser, string iNotesPassword)
		{
            userID = iNotesUser;
            password = iNotesPassword;
            document = iNotesWebmailPath;
		}

        public iNotes(string iNotesUser, string iNotesPassword, string iNotesDocument)
        {
            userID = iNotesUser;
            password = iNotesPassword;
            document = iNotesDocument;
        }

        protected string iNotesPath
        {
            get { return ConfigurationManager.AppSettings["INotesPath"]; }
        }

        protected string iNotesRedirectPath
        {
            get { return ConfigurationManager.AppSettings["INotesRedirectPath"] + document; }
        }

        protected string iNotesWebmailPath
        {
            get { return ConfigurationManager.AppSettings["INotesWebmailPath"]; }
        }

		public string CreateClientHTML()
		{
			string html = "";
            html += "<head><META HTTP-EQUIV='CACHE-CONTROL' CONTENT='NO-CACHE'></head><body onload=\"javascript:_DefaultLogin.submit()\">";
            html += string.Format("<form method=post action={0} name=\"_DefaultLogin\" ID=\"Form1\" >", iNotesPath);
			html += "<input type=\"hidden\" name=\"%%ModDate\" value=\"0000000000000000\" ID=\"hidden\" />";
            html += string.Format("<input type=\"hidden\" name=\"Username\"  value=\"{0}\" />", userID);
            html += string.Format("<input type=\"hidden\" name=\"Password\"  value=\"{0}\" />", password);
            html += string.Format("<input name=\"RedirectTo\" type=\"hidden\" value=\"{0}\" ID=\"Hidden2\" />", iNotesRedirectPath);
            html += "<input name=\"%%Surrogate_WebFormat\" type=\"hidden\" value=\"1\" ID=\"Hidden3\" />";
            html += "<input type=\"hidden\" name=\"WebFormat\" value=\"1\" ID=\"Checkbox1\"/>";
            html += "<input name=\"%%Surrogate_WebPortal\" type=\"hidden\" value=\"1\" ID=\"Hidden4\"/>";
            html += "<input type=\"hidden\" name=\"WebPortal\" ID=\"Select1\" value=\"1\"/>";
			html += "</form>";
			html += "</body>";
            return html;
		}
	}
}