using System;
using System.Web.UI;
using System.Data;
using System.ComponentModel;
using QSPFulfillment.DataAccess.Common;
using QSPFulfillment.DataAccess.Business;

//using System.Web.Services.Protocols;
using Business.ReportExecution;
//using System.Runtime.InteropServices;
using Microsoft.ReportingServices.Interfaces;
//using Business.ReportService;
using Business.Objects;
//////using Microsoft.Samples.ReportingServices.CustomSecurity;

namespace QSPFulfillment.Finance.Rpt
{
	///<summary>
	/// Base page for Finance section
	///	pages that submit reports into the Reporting Services report queue
	/// based on a code branch from AcctMgtPage.cs, rev 1.2
	///</summary>
	public class FinanceReportPage : QSPFulfillment.CommonWeb.QSPPage
	{
		#region Page initialization
		///<summary>default constructor</summary>
		public FinanceReportPage(){}

		private void Page_Load(object s, EventArgs e)
		{
			if(!Page.IsPostBack) { }
		}

		//private void Page_PreRender(object s, EventArgs e){ AddJavaScript(); }
		//protected virtual void AddJavaScript(){}

		#region auto-generated code
		///<summary>Required method for Designer support</summary>
		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		///<summary>Required method for Designer support</summary>
		private void InitializeComponent()
		{
			//this.Page.Error +=new EventHandler(Page_Error);
			this.Page.Load +=new EventHandler(Page_Load);
			//this.Page.PreRender +=new EventHandler(Page_PreRender);
		}
		#endregion auto-generated code
		#endregion Page initialization

		#region MessageManager stuff
		private Message messageManager = new Message(true);
		public Message MessageManager
		{
			get
			{
				return messageManager;
			}
		}
		public void MsgMgrAddStrings(string sep, string toAdd)
		{
			char[] Csep = sep.ToCharArray();
			foreach(string ss in toAdd.Split(Csep))
			{
				if(ss.Length > 0)
					this.MessageManager.Add(ss);
			}
		}
		#endregion MessageManager stuff
		
		#region JavaScript manipulation
		protected override void RenderChildren(HtmlTextWriter writer)
		{
			writer.WriteLine("<script id=clientEventHandlersJS language=javascript>");
			writer.WriteLine("<!--");
			writer.WriteLine("var span,s;\n");
			writer.WriteLine("s ='';");
			writer.WriteLine("function window_onunload() ");
			writer.WriteLine("{");
			writer.WriteLine("if(s !='')");
            writer.WriteLine("{");
            writer.WriteLine("s = encodeURI(s);");  
			writer.WriteLine("openErrorWindow('/QSPFulfillment/customerservice/showerrorspage.aspx?Message='+s);");
			writer.WriteLine("}");
            writer.WriteLine("}");
			writer.WriteLine("//-->");
			writer.WriteLine("</script>");
			base.RenderChildren (writer);
		}
		public override void RegisterClientScriptBlock(string key, string script)
		{
			if(key == "ValidatorIncludeScript")
			{
				script = "\r\n<script language=\"javascript\" src=\"/QSPFulfillment/CustomerService/CSWebUIValidation.js\"></script>";
			}
			base.RegisterClientScriptBlock (key, script);
		}

		private string GetScriptError(string ErrorMessage)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append("<script language=\"javascript\">\n");

			sb.Append("s=\""+ErrorMessage+"\";\n");
			//sb.Append("span  = document.createElement(\"SPAN\");\n");
			//sb.Append("span.innerHTML = s;\n");
			//sb.Append("s = span.innerText;\n");

			sb.Append("</script>\n");
			return sb.ToString();
		}
		
		
		public void SetPageError(ExceptionFulf ex)
		{
			this.Page.RegisterClientScriptBlock("ErrorMessage",GetScriptError(ex.HTMLMessage));
		}
		public void SetPageError()
		{
			this.messageManager.PrepareErrorMessage();
			this.Page.RegisterClientScriptBlock("ErrorMessage",GetScriptError(this.messageManager.ErrorHTMLMessage));
		}
		public void SetPageMessage()
		{
			this.messageManager.PrepareErrorMessage();
			this.Page.RegisterClientScriptBlock("Message",GetScriptError(this.messageManager.ErrorHTMLMessage));
		}
		#endregion JavaScript manipulation

		#region Item Declarations
		protected RSClient oRS;
		protected Business.ReportExecution.ParameterValue[] extensionParams;
		#endregion Item Declarations
	}
}
