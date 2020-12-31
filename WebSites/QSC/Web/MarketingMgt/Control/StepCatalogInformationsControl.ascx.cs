namespace QSPFulfillment.MarketingMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common;

	/// <summary>
	///		Summary description for StepCatalogInformationsControl.
	/// </summary>
	public partial class StepCatalogInformationsControl : CatalogMaintenanceStepControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			
		}

		protected void StepCatalogInformationsControl_PreRender(object sender, EventArgs e)
		{
			if(IsFirstLoad) 
			{
				try 
				{
					this.DataBind();
					IsFirstLoad = false;
				} 
				catch(Exception ex) 
				{
					this.Page.ManageError(ex);
				}
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			this.StepControl = Step.CatalogInformations;
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.PreRender += new System.EventHandler(this.StepCatalogInformationsControl_PreRender);

		}

		#endregion

		protected void btnSubmit_Click(object sender, System.EventArgs e)
		{
			try 
			{
				SaveCatalogInformations();

				StepCompletedArgs args;
				
				args = new StepCompletedArgs(this.StepControl);
				
				OnStepCompleted(this, args);
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		protected void btnSkip_Click(object sender, System.EventArgs e)
		{
			StepCompletedArgs args;
			
			try 
			{
				args = new StepCompletedArgs(this.StepControl);
				
				OnStepCompleted(this, args);
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private bool IsFirstLoad 
		{
			get 
			{
				bool isFirstLoad = true;

				if(ViewState["IsFirstLoad"] != null) 
				{
					isFirstLoad = Convert.ToBoolean(ViewState["IsFirstLoad"]);
				}

				return isFirstLoad;
			}
			set 
			{
				ViewState["IsFirstLoad"] = value;
			}
		}

		public override void DataBind()
		{
			SetValueDDL();

			if(this.Page.CatalogInfo.CatalogID != 0) 
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
			this.tbxCatalogCode.Text = this.Page.CatalogInfo.CatalogCode;
			this.tbxCatalogName.Text = this.Page.CatalogInfo.Name;
			if(this.Page.CatalogInfo.Type != CatalogType.None) 
			{
				this.ddlType.SelectedIndex = this.ddlType.Items.IndexOf(this.ddlType.Items.FindByValue(Convert.ToInt32(this.Page.CatalogInfo.Type).ToString()));
			}
			if(this.Page.CatalogInfo.Language != "") 
			{
				this.ddlLanguage.SelectedIndex = this.ddlLanguage.Items.IndexOf(this.ddlLanguage.Items.FindByValue(this.Page.CatalogInfo.Language));
			}
			if(this.Page.CatalogInfo.Year != 0) 
			{
				this.ddlYear.SelectedIndex = this.ddlYear.Items.IndexOf(this.ddlYear.Items.FindByValue(this.Page.CatalogInfo.Year.ToString()));
			}
			if(this.Page.CatalogInfo.Season != "") 
			{
				this.ddlSeason.SelectedIndex = this.ddlSeason.Items.IndexOf(this.ddlSeason.Items.FindByValue(this.Page.CatalogInfo.Season));
			}
			if(this.Page.CatalogInfo.Status != CatalogStatus.None) 
			{
				this.ddlStatus.SelectedIndex = this.ddlStatus.Items.IndexOf(this.ddlStatus.Items.FindByValue(Convert.ToInt32(this.Page.CatalogInfo.Status).ToString()));
			}
			if(this.Page.CatalogInfo.IsReplacement != "") 
			{
				this.ddlIsReplacement.SelectedIndex = this.ddlIsReplacement.Items.IndexOf(this.ddlIsReplacement.Items.FindByValue(this.Page.CatalogInfo.IsReplacement));
			}
		}

		private void SetValueEmpty() 
		{
			this.tbxCatalogCode.Text = "";
			this.tbxCatalogName.Text = "";
			this.ddlType.SelectedIndex = 0;
			this.ddlLanguage.SelectedIndex = 0;
			this.ddlYear.SelectedIndex = 0;
			this.ddlSeason.SelectedIndex = 0;
			this.ddlStatus.SelectedIndex = 0;
			this.ddlIsReplacement.SelectedIndex = 0;
		}

		private void SetValueDDL() 
		{
			SetValueDDLYear();
			SetValueDDLSeason();
			SetValueDDLType();
			SetValueDDLLanguage();
			SetValueDDLStatus();
			SetValueDDLIsReplacement();
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

		private void SetValueDDLType() 
		{
			if(this.ddlType.Items.Count == 0)
			{
				DataTable Table = new DataTable();
				this.Page.BusCatalog.SelectAllCatalogTypes(Table);

				this.ddlType.Items.Add(new ListItem(MarketingMgtPage.DROPDOWNLIST_BLANK_ENTRY, "0"));

				foreach(DataRow row in Table.Rows)
				{
					this.ddlType.Items.Add(new ListItem(row["Description"].ToString(), row["Instance"].ToString()));
				}	
			}
		}

		private void SetValueDDLLanguage() 
		{
			if(this.ddlLanguage.Items.Count == 0)
			{
				this.ddlLanguage.Items.Add(new ListItem(MarketingMgtPage.DROPDOWNLIST_BLANK_ENTRY, ""));

				this.ddlLanguage.Items.Add(new ListItem("EN", "EN"));
				this.ddlLanguage.Items.Add(new ListItem("FR", "FR"));
				this.ddlLanguage.Items.Add(new ListItem("EN/FR", "EN/FR"));
			}
		}

		private void SetValueDDLStatus() 
		{
			if(this.ddlStatus.Items.Count == 0)
			{
				DataTable Table = new DataTable();
				this.Page.BusCatalog.SelectAllCatalogStatus(Table);

				this.ddlStatus.Items.Add(new ListItem(MarketingMgtPage.DROPDOWNLIST_BLANK_ENTRY, "0"));
						
				foreach(DataRow row in Table.Rows)
				{
					this.ddlStatus.Items.Add(new ListItem(row["Description"].ToString(), row["Instance"].ToString()));
				}	
			}
		}

		private void SetValueDDLIsReplacement() 
		{
			if(this.ddlIsReplacement.Items.Count == 0)
			{
				this.ddlIsReplacement.Items.Add(new ListItem(MarketingMgtPage.DROPDOWNLIST_BLANK_ENTRY, ""));

				this.ddlIsReplacement.Items.Add(new ListItem("Y", "Y"));
				this.ddlIsReplacement.Items.Add(new ListItem("N", "N"));
			}
		}

		private void SaveCatalogInformations() 
		{
			if(this.Page.CatalogInfo.CatalogID != 0) 
			{
				this.Page.BusCatalog.UpdateCatalogInformations(this.Page.CatalogInfo.CatalogID, GetCatalogCode(), GetCatalogName(), GetCatalogType(), GetLanguage(), GetYear(), GetSeason(), GetStatus(), GetIsReplacement(), this.Page.UserID);
			} 
			else 
			{
				this.Page.CatalogInfo = new QSPFulfillment.DataAccess.Common.ActionObject.Catalog();
				this.Page.CatalogInfo.CatalogID = this.Page.BusCatalog.InsertCatalogInformations(GetCatalogCode(), GetCatalogName(), GetCatalogType(), GetLanguage(), GetYear(), GetSeason(), GetStatus(), GetIsReplacement(), this.Page.UserID);
			}
			
			this.Page.CatalogInfo.CatalogCode = GetCatalogCode();
			this.Page.CatalogInfo.Name = GetCatalogName();
			this.Page.CatalogInfo.Type = (CatalogType) Enum.ToObject(typeof(CatalogType), GetCatalogType());
			this.Page.CatalogInfo.Language = GetLanguage();
			this.Page.CatalogInfo.Year = GetYear();
			this.Page.CatalogInfo.Season = GetSeason();
			this.Page.CatalogInfo.Status = (CatalogStatus) Enum.ToObject(typeof(CatalogStatus), GetStatus());
			this.Page.CatalogInfo.IsReplacement = GetIsReplacement();
		}

		private string GetCatalogCode() 
		{
			return this.tbxCatalogCode.Text;
		}

		private string GetCatalogName() 
		{
			return this.tbxCatalogName.Text;
		}

		private int GetCatalogType() 
		{
			return Convert.ToInt32(this.ddlType.SelectedValue);
		}

		private string GetLanguage() 
		{
			return this.ddlLanguage.SelectedValue;
		}

		private int GetYear() 
		{
			return Convert.ToInt32(this.ddlYear.SelectedValue);
		}

		private string GetSeason() 
		{
			return this.ddlSeason.SelectedValue;
		}

		private int GetStatus() 
		{
			return Convert.ToInt32(this.ddlStatus.SelectedValue);
		}

		private string GetIsReplacement() 
		{
			return this.ddlIsReplacement.SelectedValue;
		}
	}
}
