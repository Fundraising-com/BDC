namespace QSPFulfillment.CustomerService.action
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Text;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Common.ActionObject;
	using QSPFulfillment.DataAccess.Common;

	/// <summary>
	///		Summary description for cancelsub.
	/// </summary>
	public partial class ReprintSwitchLetter : CustomerServiceActionControl
	{
		protected const string MSG_HEADER = "Reprint Switch Letter";
		protected DropDownList ddlReason;
		protected void Page_Load(object sender, System.EventArgs e)
		{
						
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			
		}
		#endregion

		
		
		protected override void SetValueElement()
		{
			this.Page.Header = MSG_HEADER;
		}
		protected override void DoAction()
		{
			DataSet dataSet = new DataSet("LetterBatch");
			this.Page.BusSwitchLetterBatch.GetAllByCustomerOrderDetail(dataSet, this.Page.OrderInfo.CustomerOrderHeaderInstance, this.Page.OrderInfo.TransID);
			
			if(dataSet.Tables[0].Rows.Count > 0)
			{
				/* 03/22/2006 - Ben : Reprint Switch Letter now needs to create a SwitchLetterBatch
				StringBuilder sb = new StringBuilder();

				sb.Append(System.Configuration.ConfigurationSettings.AppSettings["RSDefaultUrl"] + "/Customer Service Reports/SwitchLetterSub&rs:Command=Render&rs:Format=PDF");
				sb.Append("&iRemitBatchID=0");
				sb.Append("&dFrom=01/01/1955");
				sb.Append("&dTo=01/01/1955");
				sb.Append("&sTitleCode=0");
				sb.Append("&sReport=pr_SwitchLetterSelectReportSub");
				sb.Append("&iSwitchLetterBatchID=0");
				sb.Append("&iCustomerOrderHeaderInstance=" + this.Page.OrderInfo.CustomerOrderHeaderInstance.ToString());
				sb.Append("&iTransID=" + this.Page.OrderInfo.TransID.ToString());*/

				Business.Objects.InactiveMagazineLetterBatchItem inactiveMagazineLetterBatchItem = new Business.Objects.InactiveMagazineLetterBatchItem(Convert.ToInt32(this.Page.UserID));
				inactiveMagazineLetterBatchItem.Reason = Convert.ToInt32(dataSet.Tables[0].Rows[0][17]);
				inactiveMagazineLetterBatchItem.ProductCode = Convert.ToString(dataSet.Tables[0].Rows[0][16]);
				this.Page.BusSwitchLetterBatch.CancelLetterBatchCustomerOrderDetail(this.Page.OrderInfo.CustomerOrderHeaderInstance, this.Page.OrderInfo.TransID);
				this.Page.BusSwitchLetterBatch.Generate(inactiveMagazineLetterBatchItem, this.Page.OrderInfo.CustomerOrderHeaderInstance, this.Page.OrderInfo.TransID);	

				//this.Page.RegisterStartupScript("ConfirmReload","<script language=\"javascript\"> window.opener.RefreshAction("+(int)Action.ReprintSwitchLetter+");window.open(\"../SwitchLetterReportPage.aspx?IsNewWindow=true&Preview=false&SLBID="+(int)this.Page.BusSwitchLetterBatch.ResultSetReturned+"\");self.close();</script>");			
			}
			else
			{
				this.Page.MessageManager.Add("There is no Switch Letter generated for this subscription. You cannot reprint it.");
				this.Page.MessageManager.PrepareErrorMessage();
				throw new ExceptionFulf(this.Page.MessageManager);
				
			}
		}
		
		
		
			
			
				
			
			
		
	}
}
