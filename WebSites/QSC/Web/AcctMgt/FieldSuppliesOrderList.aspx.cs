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
using Common;
using QSPFulfillment.AcctMgt.Control;
using QSP.WebControl;

namespace QSPFulfillment.AcctMgt
{
	/// <summary>
	/// Summary description for AccountList1.
	/// </summary>
	public partial class FieldSuppliesOrderList : AcctMgtPage
	{
		protected QSPFulfillment.AcctMgt.Control.FieldSuppliesOrderListControl ctrlFieldSuppliesOrderListControl;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			
		}

		private void FieldSuppliesOrderList_PreRender(object sender, EventArgs e)
		{
			if(!IsPostBack && this.CampaignID != 0) 
			{
				this.ctrlFieldSuppliesOrderListControl.CampaignID = this.CampaignID;
				this.ctrlFieldSuppliesOrderListControl.DataBind();
			}
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.PreRender += new EventHandler(FieldSuppliesOrderList_PreRender);
		}
		#endregion

		private int CampaignID 
		{
			get 
			{
				int iCampaignID = 0;

				try 
				{
					iCampaignID = Convert.ToInt32(Request.QueryString["CampaignID"]);
				} 
				catch { }

				return iCampaignID;
			}
		}
	}
}
