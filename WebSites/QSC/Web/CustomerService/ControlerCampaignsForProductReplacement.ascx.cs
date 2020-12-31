namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Common.ActionObject;
	using QSPFulfillment.CommonWeb;

	public delegate void SelectCampaignEventHandler(object sender, Campaign campaign);
	/// <summary>
	///		Summary description for ControlerCampaignsForGiftReplacement.
	/// </summary>
	public class ControlerCampaignsForProductReplacement : CustomerServiceControlDataGrid
	{
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Label lblGroupID;
		protected QSP.WebControl.TextBoxInteger tbxGroupID;
		protected System.Web.UI.WebControls.Label lblGroupName;
		protected System.Web.UI.WebControls.TextBox tbxGroupName;
		protected System.Web.UI.WebControls.Label lblCampaignID;
		protected QSP.WebControl.TextBoxInteger tbxCampaignID;
		protected System.Web.UI.WebControls.Label lblFMID;
		protected System.Web.UI.WebControls.TextBox tbxFMID;
		protected System.Web.UI.WebControls.Label lblFMFirstName;
		protected System.Web.UI.WebControls.TextBox tbxFMFirstName;
		protected System.Web.UI.WebControls.Label lblFMLastName;
		protected System.Web.UI.WebControls.TextBox tbxFMLastName;
		protected System.Web.UI.WebControls.Label lblCity;
		protected System.Web.UI.WebControls.TextBox tbxCity;
		protected System.Web.UI.WebControls.Label lblProvince;
		protected System.Web.UI.WebControls.TextBox tbxProvince;
		protected System.Web.UI.WebControls.Label lblPostalCode;
		protected System.Web.UI.WebControls.TextBox tbxPostalCode;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.Label lblProductType;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label lblPrice;
		protected System.Web.UI.WebControls.Label lblTerm;
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label lblProductCode;
		protected System.Web.UI.WebControls.Label lblMagInstance;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblTitle2;
		protected System.Web.UI.WebControls.Button btnReset;
		protected DataGridObject dtgMain;

		public event SelectCampaignEventHandler SelectCampaignClick;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				if(QSPPage.aUserProfile.IsFM && QSPPage.aUserProfile.FMID != "9999")
					SetFMView();
			}

			try 
			{
				dtgMain.FilterExpression = "";
				dtgMain.SortExpression = "";
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e,dtgMain,lblMessage);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.dtgMain.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgMain_ItemCommand);
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			this.List = null;
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			try 
			{
				this.Page.NewSearch = true;

				this.dtgMain.SelectedIndex = -1;

				DataBind();
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private void dtgMain_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			Campaign campaign;

			try 
			{
				if(e.CommandName == "Select")
				{
					if(SelectCampaignClick != null) 
					{
						campaign = new Campaign();

						campaign.AccountID = GetGroupID(e.Item);
						campaign.CampaignID = GetCampaignID(e.Item);
						campaign.FMID = sGetFMID(e.Item);

						SelectCampaignClick(this, campaign);
					}
				}
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		public ProductReplacementType ProductType
		{
			get 
			{
				if(ViewState["ProductType"] == null) 
				{
					ViewState["ProductType"] = new GiftReplacement();
				}
				
				return (ProductReplacementType) ViewState["ProductType"];
			}
			set 
			{
				ViewState["ProductType"] = value;
			}
		}

		protected override void LoadData()
		{
			DataSource = new DataTable("Campaign");

			this.Page.BusCampaignProgram.SelectCampaignsForProductReplacement(DataSource,GetGroupIDSearch(), GetGroupNameSearch(), GetCampaignIDSearch(), GetFMIDSearch(), GetFMLastNameSearch(), GetFMFirstNameSearch(), GetCitySearch(), GetProvinceSearch(), GetPostalCodeSearch(), this.ProductType.ProductTypeName);
		}

		private void SetFMView()
		{
			this.tbxFMID.Text = QSPPage.aUserProfile.FMID;
			this.tbxFMID.Enabled = false;

			this.tbxFMFirstName.Text = QSPPage.aUserProfile.FirstName;
			this.tbxFMFirstName.Enabled = false;

			this.tbxFMLastName.Text = QSPPage.aUserProfile.LastName;
			this.tbxFMLastName.Enabled = false;
		}

		private int GetGroupIDSearch() 
		{
			if(this.tbxGroupID.Text == string.Empty)
				return 0;

			return Convert.ToInt32(this.tbxGroupID.Text);
		}
		private string GetGroupNameSearch() 
		{
			return ReplaceValue(this.tbxGroupName.Text);
		}
		private int GetCampaignIDSearch() 
		{
			if(this.tbxCampaignID.Text == string.Empty) 
			{
				return 0;
			}

			return Convert.ToInt32(this.tbxCampaignID.Text);
		}
		private string GetFMIDSearch() 
		{
			return ReplaceValue(this.tbxFMID.Text);
		}
		private string GetFMFirstNameSearch() 
		{
			return ReplaceValue(this.tbxFMFirstName.Text);
		}
		private string GetFMLastNameSearch() 
		{
			return ReplaceValue(this.tbxFMLastName.Text);
		}
		private string GetCitySearch() 
		{
			return ReplaceValue(this.tbxCity.Text);
		}
		private string GetProvinceSearch() 
		{
			return this.tbxProvince.Text;
		}
		private string GetPostalCodeSearch() 
		{
			return this.tbxPostalCode.Text;
		}

		private int GetGroupID(DataGridItem e) 
		{
			return Convert.ToInt32(((Label) e.FindControl("lblGroupID")).Text);
		}
		private string GetGroupName(DataGridItem e) 
		{
			return ((Label) e.FindControl("lblGroupName")).Text;
		}
		private int GetCampaignID(DataGridItem e) 
		{
			return Convert.ToInt32(((Label) e.FindControl("lblCampaignID")).Text);
		}
		private DateTime GetStartDate(DataGridItem e) 
		{
			return Convert.ToDateTime(((Label) e.FindControl("lblStartDate")).Text);
		}
		private DateTime GetEndDate(DataGridItem e) 
		{
			return Convert.ToDateTime(((Label) e.FindControl("lblEndDate")).Text);
		}
		private int GetFMID(DataGridItem e) 
		{
			return Convert.ToInt32(((Label) e.FindControl("lblFMID")).Text);
		}
		private string sGetFMID(DataGridItem e) 
		{
			return ((Label) e.FindControl("lblFMID")).Text;
		}
		private string GetFMFirstName(DataGridItem e) 
		{
			return ((Label) e.FindControl("lblFMFirstName")).Text;
		}
		private string GetFMLastName(DataGridItem e) 
		{
			return ((Label) e.FindControl("lblFMLastName")).Text;
		}
		private string GetCity(DataGridItem e) 
		{
			return ((Label) e.FindControl("lblCity")).Text;
		}
		private string GetProvince(DataGridItem e) 
		{
			return ((Label) e.FindControl("lblProvince")).Text;
		}
		private string GetPostalCode(DataGridItem e) 
		{
			return ((Label) e.FindControl("lblPostalCode")).Text;
		}

		private void btnReset_Click(object sender, System.EventArgs e)
		{
			tbxGroupID.Text = "";
			tbxGroupName.Text = "";
			tbxCampaignID.Text = "";
			tbxFMID.Text = "";
			tbxFMFirstName.Text = "";
			tbxFMLastName.Text = "";
			tbxCity.Text = "";
			tbxProvince.Text = "";
			tbxPostalCode.Text = "";

			if (!this.tbxFMID.Enabled)
				SetFMView();
		}
	}
}
