using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using efundraising.EFundraisingCRM;

namespace EFundraisingCRMWeb.Sales.SalesScreen
{
	/// <summary>
	/// Summary description for CreditCheck.
	/// </summary>
	public partial class CreditCheckSA : EFundraisingCRMSalesBasePage, IPage, INoQuickCreate, INoPageInformation, INoQuickSearch
	{
		protected EFundraisingCRMWeb.Components.User.CreditRequestDetails_SaleScreen CreditRequestDetails_SaleScreen1;
		protected EFundraisingCRMWeb.Components.User.CreditCheck.CreditInfo CreditInfo1;

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			
			int leadID = Convert.ToInt32(Request["lid"]);
			CreditCheckRequest[] ccr = CreditCheckRequest.GetCreditCheckRequestByLeadID(leadID);
			if (ccr.Length > 0)
			{
				CreditRequestDetails_SaleScreen1.FillDataGridHistory(ccr);
			}
		
		}

		#region Private Properties and Methods



		private int saleId
		{
			get
			{
				try
				{
					if (Request["sid"] == null)
						return int.MinValue;
					return System.Convert.ToInt32(Request["sid"]);
				}
				catch (Exception)
				{
					return int.MinValue;
				}
			}
		}

		#endregion

		private void Refresh(object sender, System.EventArgs e)
		{
			int leadID = Convert.ToInt32(Request["lid"]);
			CreditCheckRequest[] ccr = CreditCheckRequest.GetCreditCheckRequestByLeadID(leadID);
			if (ccr.Length > 0)
			{
				CreditRequestDetails_SaleScreen1.FillDataGridHistory(ccr);
			}
		}
		#region IPage Members

		public string PageInformation 
		{
			get 
			{
				return "Credit Check";
			}
		}

		public string PageDescription 
		{
			get 
			{
				return "Credit Check";
			}
		}

		public override void Search(string searchQuery) 
		{
			//get leadid coprrspondiong to sale id
			Sale s = Sale.GetSaleByID(Convert.ToInt32(searchQuery));
			if (s != null)
			{
				Redirect("../../Sales/SalesScreen/Default.aspx?clid=" + s.ClientId + "&seq=" + s.ClientSequenceCode);
			}
		}

		public override void Create(string redirection) 
		{
			base.Create(redirection);
		}

		#endregion
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			CreditInfo1.AfterCreditRequestSaved += new System.EventHandler(this.Refresh);
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		protected void GoBackButton_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language='javascript'> { window.close();}</script>");
		}

		
	}
}