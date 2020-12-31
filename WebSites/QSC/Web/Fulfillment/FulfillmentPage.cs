using System;
using System.Web.UI;
using System.Threading;
using Common;
using QSPFulfillment.CommonWeb;
using Business.Objects;

namespace QSPFulfillment.Fulfillment
{
	/// <summary>
	/// Summary description for AcctMgtPage.
	/// </summary>
	public class FulfillmentPage : QSPPage
	{
		private Message messageManager = new Message(true);
		private Transaction oCurrentTransaction = null;

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			
			InitializeComponent();
			base.OnInit(e);							
		}

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
			writer.WriteLine("window.scrollTo(0, 0);");
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			//this.Page.Error +=new EventHandler(Page_Error);			
		}
		#endregion

		public Transaction CurrentTransaction 
		{
			get 
			{
				return oCurrentTransaction;
			}
			set 
			{
				oCurrentTransaction = value;
			}
		}

		public void SetPageError(MessageException ex)
		{
			this.Page.RegisterClientScriptBlock("ErrorMessage",GetScriptError(ex.HTMLMessage));
		}
		public void SetPageError()
		{
			this.messageManager.PrepareErrorMessage();
			this.Page.RegisterClientScriptBlock("ErrorMessage",GetScriptError(this.messageManager.ErrorHTMLMessage));
		}
		
		
		public string UserID
		{	
			
			get
			{   
				return QSPFulfillment.CommonWeb.QSPPage.aUserProfile.Instance.ToString();
			}
		}

		public Message CurrentMessageManager
		{
			get
			{
				return messageManager;
			}
		}

		protected virtual void AddJavaScript()
		{
		
		}

		public override void ManageError(Exception ex) 
		{
			if(ex is ThreadAbortException) 
			{
				throw ex;
			}

			MessageException messageException = ex as MessageException;

			if(messageException == null)
			{
				QSPFulfillment.DataAccess.Common.ApplicationError.ManageError(ex);

				messageException = new MessageException(Message.ERRMSG_SYSTEM_VAR_0, Message.ERRMSG_SYSTEM_VAR_0);
			}

			this.SetPageError(messageException);
		}

		private string GetScriptError(string ErrorMessage)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append("<script language=\"javascript\">\n");
			
			sb.Append("s=\""+ErrorMessage+"\";\n");
						
			sb.Append("</script>\n");
			return sb.ToString();
		}
	}
}
