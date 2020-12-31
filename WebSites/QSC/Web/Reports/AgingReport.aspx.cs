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
using QSPFulfillment.CommonWeb;

namespace QSPFulfillment.Reports
{
	///<summary>if allowed to see it, show user latest Aging Report</summary>
	public class AgingReport : QSPFulfillment.AcctMgt.AcctMgtPage
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
			this.btViewRpt.Click += new System.EventHandler(this.btViewRpt_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion auto-generated code

		#region Item Declarations
		protected QSPFulfillment.CommonWeb.UC.DateEntry		ucAsOfDate;
		protected QSPFulfillment.CommonWeb.RSGeneration rsGenerationAgingReport;
		protected System.Web.UI.WebControls.Button			btViewRpt;
		#endregion Item Declarations
		
		#region Page Load
		///<summary>start the fun</summary>
		private void Page_Load(object s, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if(aUserProfile.HasRole("Finance"))
				{
					// show the report
					this.ucAsOfDate.Date = System.DateTime.Now;
				}
				else
				{
					//user is not allowed to see this page
					Response.Redirect("../Common/AccessDenied.aspx?p=Reports_AgingReport");
				}
			}
		}
		#endregion Page Load

		#region Postback process
		private void btViewRpt_Click(object s, EventArgs e)
		{
			Page.Validate();
			if(!Page.IsValid)
			{
				this.CurrentMessageManager.Add("The page did not validate properly"); 
				this.SetPageError();
				return;
			}
			
			DateTime AsOfDate = this.ucAsOfDate.Date;
			if(AsOfDate != System.DateTime.MinValue) 
			{
				CallReport(AsOfDate);
			}
			else
			{
				this.CurrentMessageManager.Add("A valid As Of Date must be entered"); 
				this.SetPageError();
			}
		}

		private void CallReport(DateTime AsOfDate)
		{
			ParameterValueCollection parameterValues = new ParameterValueCollection();
			ParameterValue parameterValue = new ParameterValue();
			
			parameterValue.Name = "AsOfDate";
			parameterValue.Value = AsOfDate.ToShortDateString();
			parameterValues.Add(parameterValue);
			
			rsGenerationAgingReport.Generate(parameterValues);
		}
		#endregion Postback process
	}
}
