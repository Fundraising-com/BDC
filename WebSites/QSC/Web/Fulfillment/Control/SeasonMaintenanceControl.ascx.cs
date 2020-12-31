namespace QSPFulfillment.Fulfillment.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Common;
	using Common.TableDef;
	using Business.Objects;

	/// <summary>
	///		Displays season details
	/// </summary>
	/// <remarks>
	///		Created on 2006-06-28
	///		Created by Madina Saitakhmetova
	/// </remarks>
	public partial class SeasonMaintenanceControl : FulfillmentControl
	{
		private const string TRANSACTION_NAME = "SaveSeasonList";


		private Season season = null;

		public event SelectSeasonEventHandler SaveSeasonClicked;
		public event SelectSeasonEventHandler CancelSeasonClicked;

		#region Event Handlers

		protected void Page_Load(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// event handler responding when SAVE buttons are clicked
		/// </summary>
		protected void btnSubmit_Click(object sender, EventArgs e)
		{
			try 
			{
				if(this.Visible) 
				{
					this.Page.CurrentTransaction = new Transaction(TRANSACTION_NAME, DataBaseName.QSPCanadaCommon);
					
					this.Page.CurrentTransaction.Open();

					this.Save();

					this.Page.CurrentTransaction.Save();
					
					this.Page.CurrentTransaction = null;

					SelectSeasonClickedArgs args;

					args = new SelectSeasonClickedArgs(this.Season);

					if(SaveSeasonClicked != null)
						SaveSeasonClicked(this, args);					
				}
			} 
			catch(MessageException ex) 
			{
				this.Page.SetPageError(ex);
				this.Page.CurrentTransaction.Cancel();				
				this.Page.CurrentTransaction = null;
			} 
		}

		/// <summary>
		/// event handler responding to the CANCEL button click
		/// </summary>
		protected void btnClose_Click(object sender, EventArgs e)
		{
			this.SeasonID = 0;
			this.DataBind();		
	
			SelectSeasonClickedArgs args;

			args = new SelectSeasonClickedArgs(this.Season);

			if(CancelSeasonClicked != null)
				CancelSeasonClicked(this, args);	
		}

		#endregion

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

		#region Public Properties

		/// <summary>
		/// Season object property
		/// </summary>
		public Season Season 
		{
			get 
			{
				if(season == null) 
				{
					season = new Season();
				}

				return season;
			}
			set 
			{
				season = value;
			}
		}

		/// <summary>
		/// SeasonID property
		/// </summary>
		public int SeasonID 
		{
			get 
			{
				if(this.ViewState["SeasonID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["SeasonID"]);
			}
			set 
			{
				this.ViewState["SeasonID"] = value;
			}
		}

		#endregion

		#region Fields

		/// <summary>
		/// SeasonName property refers to textbox "tbxSeasonName"
		/// </summary>
		private string SeasonName 
		{
			get 
			{
				return this.tbxSeasonName.Text;
			}
			set 
			{
				this.tbxSeasonName.Text = value;
			}
		}

		/// <summary>
		/// FiscalYear property refers to textbox "tbxFiscalYear"
		/// </summary>
		private int FiscalYear 
		{
			get 
			{
				return this.tbxFiscalYear.Value;
			}
			set 
			{
				this.tbxFiscalYear.Value = value;
			}
		}

		/// <summary>
		/// SeasonLetter property refers to a drop down list "ddlSeason"
		/// </summary>
		private string SeasonLetter
		{
			get 
			{
				return this.ddlSeason.SelectedValue;
			}
			set 
			{
				this.ddlSeason.SelectedIndex = -1;
				this.ddlSeason.Items.FindByValue(value).Selected = true;
			}
		}

		/// <summary>
		/// DefaultConversionRate property refers to a textbox "tbxDefaultConversionRate"
		/// </summary>
		private double DefaultConversionRate 
		{
			get 
			{
				return this.tbxDefaultConversionRate.Value;
			}
			set 
			{
				this.tbxDefaultConversionRate.Value = value;
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// databinds this control
		/// </summary>
		public override void DataBind()
		{
			SetValueDDLSeason();
			LoadData();
		}

		/// <summary>
		/// saves (update/insert) currently loaded Season
		/// </summary>
		public void Save()
		{
			if(this.Visible)
			{
				SeasonDataSet.SeasonRow row = null;

				//when existing season is being updated
				if(this.SeasonID != 0) 
				{
					this.Season.GetOneByID(this.SeasonID);
					row = (SeasonDataSet.SeasonRow) this.Season.dataSet.Season.Rows[0];
				} 
					//when a new Season being saved (insert)
				else 
				{
					row = this.Season.dataSet.Season.NewSeasonRow();									
				}

				if(row != null) 
				{
					FillSeasonRow(row);
					this.Season.Save(row);
					SetValueEmpty();
				}
			}			
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Load Season details in the control
		/// </summary>
		private void LoadData() 
		{
			if(this.SeasonID != 0) 
			{
				this.Season.GetOneByID(SeasonID);

				if(this.Season.dataSet != null) 
				{
					SetValue();
				} 
				else 
				{
					SetValueEmpty();
				}

				LockYearAndSeason();
			} 
			else 
			{
				SetValueEmpty();				
			}
		}

		/// <summary>
		/// lock Fiscal Year and Season for editing when updating
		/// existing Season
		/// </summary>
		private void LockYearAndSeason()
		{
			this.tbxFiscalYear.ReadOnly = true;
			this.ddlSeason.Enabled = false;
		}

		/// <summary>
		/// unlock Fiscal Year and Season for editing when inserting
		/// a new Season
		/// </summary>
		private void UnLockYearAndSeason()
		{
			this.tbxFiscalYear.ReadOnly = false;
			this.ddlSeason.Enabled = true;
		}

		/// <summary>
		/// bind values to corresponding controls
		/// </summary>
		private void SetValue() 
		{
			SeasonDataSet.SeasonRow row;

			row = (SeasonDataSet.SeasonRow) Season.dataSet.Season.Rows[0];

			this.SeasonName = row.Name;
			this.FiscalYear = row.FiscalYear;
			this.SeasonLetter = row.Season;
			this.DefaultConversionRate = Convert.ToDouble(row.DefaultConversionRate);				
		}

		/// <summary>
		/// reset values to default values
		/// </summary>
		private void SetValueEmpty() 
		{
			this.SeasonName = "";
			this.FiscalYear = this.Season.LastYear + 1;
			this.SeasonLetter = "";
			this.DefaultConversionRate = Convert.ToDouble(this.Season.LastDefaultRate);		
		
			UnLockYearAndSeason();
		}

		/// <summary>
		/// load Season letters in Season DDL
		/// </summary>
		private void SetValueDDLSeason() 
		{
			if(this.ddlSeason.Items.Count == 0)
			{
				this.ddlSeason.DataValueField = "Season";
				this.ddlSeason.DataTextField = "Season";
				this.ddlSeason.DataSource = this.Season.DSSeasonLetters.Tables[0];
				this.ddlSeason.DataBind();	
			}
		}

		/// <summary>
		/// fill the row with values from fields
		/// </summary>
		/// <param name="row">row to be filled</param>
		private void FillSeasonRow(SeasonDataSet.SeasonRow row) 
		{
			row.ID = this.SeasonID;
			row.Name = this.SeasonName;
			row.FiscalYear = this.FiscalYear;
			row.Season = this.SeasonLetter;
			row.DefaultConversionRate = Convert.ToDecimal(this.DefaultConversionRate);
			row.UserIDChanged =  Convert.ToInt32(this.Page.UserID);
		}

		#endregion
	}
}
