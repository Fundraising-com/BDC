using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Common;
using Common.TableDef;
using Business.Objects;

namespace QSPFulfillment.AcctMgt.Control
{
	/// <summary>
	/// Summary description for CASummaryHyperLink.
	/// </summary>
	[DefaultProperty("Text"), 
	ToolboxData("<{0}:CASummaryHyperLink runat=server></{0}:CASummaryHyperLink>")]
	public class CASummaryHyperLink : System.Web.UI.HtmlControls.HtmlAnchor, INamingContainer
	{
		private const string REPORT_URL = "/QSPFulfillment/AcctMgt/SummaryFormsReport.aspx?IsNewWindow=true&CampaignID=";

		private CampaignProgram cap;
		private SummaryReportsCollection summaryReportsCollection;

		[Bindable(true), 
		Category("Data"), 
		DefaultValue(0)] 
		public int CampaignID 
		{
			get 
			{
				if(this.ViewState["CampaignID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["CampaignID"]);
			}
			set 
			{
				this.ViewState["CampaignID"] = value;
			}
		}

		public CampaignProgram CampaignProgram
		{
			get
			{
				return cap;
			}
		}

		public override void DataBind()
		{
			if(this.CampaignID != 0) 
			{
				LoadData();
				SetValue();
			} 

			SetVisible();

			base.DataBind ();
		}

		private void LoadData() 
		{
			cap = new CampaignProgram(this.CampaignID);
		}

		private void SetValue() 
		{
			summaryReportsCollection = SummaryFormsReportFactory.Instance.GetSummaryReports(this.CampaignProgram);

			if(summaryReportsCollection.Count > 0) 
			{
				this.HRef = REPORT_URL + this.CampaignID.ToString();
			} 
			else 
			{
				this.HRef = "";
			}

			this.Target = "_blank";
		}

		private void SetVisible() 
		{
			this.Visible = (this.CampaignID != 0 && summaryReportsCollection.Count > 0);
		}
	}
}