namespace QSPFulfillment.AcctMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Common.TableDef;
	using QSPFulfillment.CommonWeb;

	/// <summary>
	///		Summary description for CampaignListControl.
	/// </summary>
	public class CampaignListControl : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.DataGrid dtgMain;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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
			this.Load += new System.EventHandler(this.Page_Load);
			this.DataBinding += new System.EventHandler(this.CampaignListControl_DataBinding);
			this.dtgMain.PageIndexChanged += new DataGridPageChangedEventHandler(dtgMain_PageIndexChanged);
			this.dtgMain.ItemCreated += new DataGridItemEventHandler(dtgMain_ItemCreated);
			this.dtgMain.ItemDataBound += new DataGridItemEventHandler(dtgMain_ItemDataBound);
		}
		#endregion

		private void CampaignListControl_DataBinding(object sender, EventArgs e)
		{
			DataGridItem dgi = (DataGridItem) this.BindingContainer;

			if(!(dgi.DataItem is DataSet))
				throw new ArgumentException("Please change the TemplateDataMode attribute to 'Table' in the HierarGrid declaration");

			AccountCampaignListDataSet ds = (AccountCampaignListDataSet) dgi.DataItem;
			DataSource = ds;
			BindChildGrid();
		}

		private void dtgMain_ItemCreated(object sender, DataGridItemEventArgs e)
		{
			/*HtmlAnchor lnkNewCampaign;

			if(e.Item.ItemType == ListItemType.Footer && !QSPPage.aUserProfile.IsFM) 
			{
				try 
				{
					int iAccountID = Convert.ToInt32(((Label) ((DataGridItem) this.BindingContainer).FindControl("lblGroupID")).Text);

					lnkNewCampaign = new HtmlAnchor();

					lnkNewCampaign.InnerText = "New Campaign";
					lnkNewCampaign.HRef = "../CampaignMaintenance.aspx?CampaignID=0&AccountID=" + iAccountID.ToString();

					e.Item.Cells[12].Controls.Add(lnkNewCampaign);
				} 
				catch { }
			}*/
		}

		private void dtgMain_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			ConfirmationAgreementLinkButton hypCA = null;
			CASummaryHyperLink hypCASummary = null;
			//LinkButton lnkCloneCA = null;

			if(e.Item.ItemType == ListItemType.Item ||
				e.Item.ItemType == ListItemType.AlternatingItem ||
				e.Item.ItemType == ListItemType.EditItem ||
				e.Item.ItemType == ListItemType.SelectedItem) 
			{
				try 
				{
					hypCA = (ConfirmationAgreementLinkButton) e.Item.FindControl("hypCA");
				} 
				catch { }

				if(hypCA != null) 
				{
					hypCA.DataBind();
				}

				try 
				{
					hypCASummary = (CASummaryHyperLink) e.Item.FindControl("hypCASummary");
				} 
				catch { }

				if(hypCASummary != null) 
				{
					hypCASummary.DataBind();
				}

				/* Ben - 03/17/2006 - FMs can now Clone CAs
				if(QSPPage.aUserProfile.IsFM) 
				{
					try 
					{
						lnkCloneCA = (LinkButton) e.Item.FindControl("lnkCloneCA");
					} 
					catch { }

					if(lnkCloneCA != null) 
					{
						lnkCloneCA.Visible = false;
					}
				}*/
			}
		}

		private void dtgMain_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			if(e.NewPageIndex >= 0 && e.NewPageIndex < this.dtgMain.PageCount) 
			{
				this.dtgMain.CurrentPageIndex = e.NewPageIndex;

				BindChildGrid();
			}
		}

		private string SESSIONKEY_DATASOURCE
		{
			get { return this.UniqueID + "_DataSource"; }
		}

		private AccountCampaignListDataSet DataSource 
		{
			get 
			{
				return (AccountCampaignListDataSet) Session[SESSIONKEY_DATASOURCE];
			}
			set 
			{
				Session[SESSIONKEY_DATASOURCE] = value;
			}
		}

		private void BindChildGrid() 
		{
			dtgMain.DataSource = DataSource;
			dtgMain.DataMember = DataSource.Campaign.TableName;
			dtgMain.DataBind();
		}
	}
}
