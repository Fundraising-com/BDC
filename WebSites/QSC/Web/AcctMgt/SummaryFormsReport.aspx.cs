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
using Business.Reports;

namespace QSPFulfillment.AcctMgt
{
	/// <summary>
	/// Summary description for CampaignMaintenance.
	/// </summary>
	public partial class SummaryFormsReport : AcctMgtPage
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(CampaignID != 0) 
			{
				Generate();
			} 
			else 
			{
				AddScriptClose();
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

		private void AddScriptClose() 
		{
			this.RegisterStartupScript("AddScriptClose", "<script language=\"javascript\"> self.close(); </script>");
		}

		private void Generate() 
		{
			SummaryFormReport report = new SummaryFormReport();
			Campaign campaign = new Campaign(CampaignID);
			byte[] result;

			if(campaign.dataSet.Campaign.Count > 0) 
			{
				report.ICampaignIDParameter.FieldAlias = campaign.dataSet.Campaign.IDColumn.ColumnName;

				result = report.Generate(campaign.dataSet.Campaign[0]);

				Response.ClearContent();
				Response.AppendHeader("content-length", result.Length.ToString());
				Response.ContentType = "application/pdf";
				Response.BinaryWrite(result);
				Response.Flush();
				//Response.Close();
                Response.End();
			} 
			else 
			{
				AddScriptClose();
			}
		}
	}
}
