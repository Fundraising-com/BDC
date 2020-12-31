using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
//using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Web.UI.HtmlControls;

namespace QSPFulfillment.AcctMgt
{
	///<summary>
	/// Grab campaign info 
	/// base on Group/Account id 
	/// to use for QSP.ca Links
	///</summary>
	public partial class CampaignsForLinks : AcctMgtPage
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
			this.btSubmit.Click +=new EventHandler(btSubmit_Click);
			this.DataGridCampaigns.ItemDataBound +=new DataGridItemEventHandler(DataGridCampaigns_ItemDataBound);
		}
		#endregion auto-generated code

		#region Item Declarations
		private   DAL.CampaignDataAccess				oCampaignDAL;
		#endregion Item Declarations
		
		protected void Page_Load(object s, EventArgs e)
		{
		}

		#region Event Handler(s)
		private void btSubmit_Click(object s, EventArgs e)
		{
			try
			{
				int AccountID = Convert.ToInt32(tbAccountID.Text);
				this.DataGridCampaigns_Bind(AccountID);
			}
			catch
			{
				return;
			}
		}
		#endregion Event Handler(s)

		#region DataGrid stuff
		private void DataGridCampaigns_Bind(int AccountID)
		{
			oCampaignDAL = new DAL.CampaignDataAccess();
			DataGridCampaigns.DataSource = oCampaignDAL.GetCampaignInfoForLinksCA(AccountID);
			DataGridCampaigns.DataBind();
		}

		private void DataGridCampaigns_ItemDataBound(object s, DataGridItemEventArgs e)
		{
			Label LB = new Label();
			LB = (Label) e.Item.FindControl("lbStatus");
			if(LB != null)
			{
				switch(LB.Text)
				{
						#region switch contents
					case "37001":
						LB.Text = "Pending";
						break;
					case "37002":
						LB.Text = "Approved";
						break;
					case "37003":
						LB.Text = "Pending FS";
						break;
					case "37004":
						LB.Text = "On Hold";
						break;
					case "37005":
						LB.Text = "Cancel";
						break;
					case "37006":
						LB.Text = "Inactive";
						break;
					case "37007":
						LB.Text = "Logged";
						break;
					default:
						break;
						#endregion switch contents
				}
			}

			LB = new Label();
			LB = (Label) e.Item.FindControl("lbFMID");
			if(LB != null)
			{
				#region fix appearance of FMIDs
				if(LB.Text.Length == 1)
				{
					LB.Text = "000" + LB.Text;
				}
				else if(LB.Text.Length == 2)
				{
					LB.Text = "00" + LB.Text;
				}
				else if(LB.Text.Length == 3)
				{
					LB.Text = "0" + LB.Text;
				}
				#endregion fix appearance of FMIDs
			}

			LB = new Label();
			LB = (Label) e.Item.FindControl("lbValidYear");
			if(LB != null)
			{
				if(LB.Text == "TRUE")
				{
					LB.Font.Bold = true;
					LB.ForeColor = System.Drawing.Color.Green;
				}
				else
				{
					LB.Font.Bold = false;
					LB.ForeColor = System.Drawing.Color.Red;
				}                
			}

			LB = new Label();
			LB = (Label) e.Item.FindControl("lbStartDt");
			if(LB != null)
			{
				try
				{
					LB.Text = Convert.ToDateTime(LB.Text).ToShortDateString();
				}
				catch
				{
				}
			}
			LB = new Label();
			LB = (Label) e.Item.FindControl("lbEndDt");
			if(LB != null)
			{
				try
				{
					LB.Text = Convert.ToDateTime(LB.Text).ToShortDateString();
				}
				catch
				{
				}
			}
			
			CommonWeb.UC.DynamicList DL = new CommonWeb.UC.DynamicList();
			DL = (CommonWeb.UC.DynamicList) e.Item.FindControl("ucDynListPrograms");
			if(DL != null)
			{
				DL.Seperator = ";";
				DL.Bind();
			}

//			if(e.Item.ItemType == ListItemType.AlternatingItem)
//			{
//				foreach(HtmlTableCell i in ((HtmlTableRow)e.Item.FindControl("trProgram")).Cells)
//				{
//					i.BgColor = "#ededed"; //Nice light gray for alternating items
//				}
//			}
		}
		#endregion DataGrid stuff
	}
}
