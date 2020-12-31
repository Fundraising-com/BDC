namespace QSPFulfillment.MarketingMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Common.ActionObject;
	using QSPFulfillment.DataAccess.Common;

	/// <summary>
	///		Summary description for CatalogMaintenanceOneStepControl.
	/// </summary>
	public partial class PremiumMaintenanceControl : MarketingMgtControl
	{

		public event SelectPremiumEventHandler PremiumSaved;
		public event System.EventHandler PremiumCancelled;

		protected void Page_Load(object sender, System.EventArgs e)
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

		}
		#endregion

		protected void btnSubmit_Click(object sender, System.EventArgs e)
		{
			SelectPremiumClickedArgs args;

			try 
			{
				SavePremiumInformations();

				args = new SelectPremiumClickedArgs(this.PremiumInfo);

				if(PremiumSaved != null)
					PremiumSaved(sender, args);
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		protected void btnCancel_Click(object sender, System.EventArgs e)
		{
			try 
			{
				if(PremiumCancelled != null)
					PremiumCancelled(sender, e);
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		public Premium PremiumInfo 
		{
			get 
			{
				return (Premium) this.ViewState["PremiumInfo"];
			}
			set  
			{
				this.ViewState["PremiumInfo"] = value;
			}
		}

		public override void DataBind()
		{
			SetValueDDL();

			if(this.PremiumInfo != null) 
			{
				SetValue();
			} 
			else 
			{
				SetValueEmpty();
			}
		}

		private void SetValue() 
		{
			this.tbxPremiumCode.Text = this.PremiumInfo.PremiumCode;
			this.ddlYear.SelectedIndex = this.ddlYear.Items.IndexOf(this.ddlYear.Items.FindByValue(this.PremiumInfo.Year.ToString()));
			this.ddlSeason.SelectedIndex = this.ddlSeason.Items.IndexOf(this.ddlSeason.Items.FindByValue(this.PremiumInfo.Season));
			this.chkIsActive.Checked = (this.PremiumInfo.IsValid == "Y");
			this.tbxEnglishName.Text = this.PremiumInfo.EnglishDescription;
			this.tbxFrenchName.Text = this.PremiumInfo.FrenchDescription;
		}

		private void SetValueEmpty() 
		{
			this.tbxPremiumCode.Text = "";
			this.ddlYear.SelectedIndex = 0;
			this.ddlSeason.SelectedIndex = 0;
			this.chkIsActive.Checked = false;
			this.tbxEnglishName.Text = "";
			this.tbxFrenchName.Text = "";
		}

		private void SetValueDDL() 
		{
			SetValueDDLYear();
			SetValueDDLSeason();
		}

		private void SetValueDDLYear() 
		{
			if(this.ddlYear.Items.Count == 0)
			{
				DataTable Table = new DataTable();
				this.Page.BusCatalog.SelectAllCatalogFinancialYears(Table);

				this.ddlYear.Items.Add(new ListItem(MarketingMgtPage.DROPDOWNLIST_BLANK_ENTRY, "0"));

				foreach(DataRow row in Table.Rows)
				{
					this.ddlYear.Items.Add(new ListItem(row["FiscalYear"].ToString(), row["FiscalYear"].ToString()));
				}	
			}
		}

		private void SetValueDDLSeason() 
		{
			if(this.ddlSeason.Items.Count == 0)
			{
				DataTable Table = new DataTable();
				this.Page.BusCatalog.SelectAllCatalogSeasons(Table);

				this.ddlSeason.Items.Add(new ListItem(MarketingMgtPage.DROPDOWNLIST_BLANK_ENTRY, ""));

				foreach(DataRow row in Table.Rows)
				{
					this.ddlSeason.Items.Add(new ListItem(row["Season"].ToString(), row["Season"].ToString()));
				}	
			}
		}

		private void SavePremiumInformations() 
		{
			if(this.PremiumInfo != null) 
			{
				this.Page.BusCatalog.UpdatePremiumInformations(this.PremiumInfo.PremiumID, GetPremiumCode(), GetYear(), GetSeason(), GetIsActive(), GetEnglishDescription(), GetFrenchDescription(), this.Page.UserID);
			} 
			else 
			{
				this.PremiumInfo = new Premium();
				this.PremiumInfo.PremiumID = this.Page.BusCatalog.InsertPremiumInformations(GetPremiumCode(), GetYear(), GetSeason(), GetIsActive(), GetEnglishDescription(), GetFrenchDescription(), this.Page.UserID);
			}

			this.PremiumInfo.PremiumCode = GetPremiumCode();
			this.PremiumInfo.Year = GetYear();
			this.PremiumInfo.Season = GetSeason();
			this.PremiumInfo.IsValid = GetIsActive() ? "Y" : "N";
			this.PremiumInfo.EnglishDescription = GetEnglishDescription();
			this.PremiumInfo.FrenchDescription = GetFrenchDescription();
		}

		private string GetPremiumCode() 
		{
			return this.tbxPremiumCode.Text;
		}

		private int GetYear() 
		{
			return Convert.ToInt32(this.ddlYear.SelectedValue);
		}

		private string GetSeason() 
		{
			return this.ddlSeason.SelectedValue;
		}

		private bool GetIsActive() 
		{
			return this.chkIsActive.Checked;
		}

		private string GetEnglishDescription() 
		{
			return this.tbxEnglishName.Text;
		}

		private string GetFrenchDescription() 
		{
			return this.tbxFrenchName.Text;
		}
	}
}
