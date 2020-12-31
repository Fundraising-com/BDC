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
using Business.Objects;
using Common;
using QSPFulfillment.CommonWeb;

namespace QSPFulfillment.Reports
{
	///<summary>Summary description for SummaryFormsReport.</summary>
	///<remarks>Madina Saitakhmetova, August 2006 : implementation</remarks>
	public partial class SummaryFormsReport : QSPFulfillment.AcctMgt.AcctMgtPage
	{
		#region auto-generated code
		///<summary>Required method for Designer support</summary>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		///<summary>Required method for Designer support</summary>
		private void InitializeComponent()
		{
		}
		#endregion auto-generated code

		protected QSPFulfillment.Reports.SummaryFormsReportControl ctrlSummaryFormsReportControl;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!this.IsPostBack)
			{
				try 
				{
					this.ctrlSummaryFormsReportControl.DataBind();					
				} 
				catch(MessageException ex) 
				{
					this.SetPageError(ex);
				}
			}
		}
	}
}
