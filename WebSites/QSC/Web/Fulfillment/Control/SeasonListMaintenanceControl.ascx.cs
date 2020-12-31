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
	using QSPFulfillment.CommonWeb;

	/// <summary>
	///		Container and manager control for SeasonMaintenanceControl and SeasonListControl
	/// </summary>
	/// <remarks>
	///		Created on 2006-07-03
	///		Created by Saitakhmetova Madina
	/// </remarks>
	public partial class SeasonListMaintenanceControl : FulfillmentControl
	{
		private const string TRANSACTION_NAME = "SaveSeasonList";
		private const string TRANSACTION_DELETE_NAME = "DeleteSeason";

		/************* vars for delete confirmation ********************/
		private const string SAVE_SEASON_CONFIRMATION_MESSAGE = "Do you want to save the changes?";
		protected QSPFulfillment.Fulfillment.Control.ControlerYesNoConfirmation ctrlControlerYesNoConfirmation;
		/***************************************************************/


		protected QSPFulfillment.Fulfillment.Control.SeasonListControl ctrlSeasonListControl;
		protected QSPFulfillment.Fulfillment.Control.SeasonMaintenanceControl ctrlSeasonMaintenanceControl;

		/// <summary>
		/// Selected Season ID property
		/// </summary>
		public int SelectedSeasonID 
		{
			get 
			{
				if(this.ViewState["SelectedSeasonID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["SelectedSeasonID"]);
			}
			set 
			{
				this.ViewState["SelectedSeasonID"] = value;
			}
		}
		/// <summary>
		/// Visibility of maintenance control
		/// </summary>
		public bool MaintenanceControlVisible 
		{
			get 
			{
				if(this.ViewState["MaintenanceControlVisible"] == null)
					return false;

				return Convert.ToBoolean(this.ViewState["MaintenanceControlVisible"]);
			}
			set 
			{
				this.ViewState["MaintenanceControlVisible"] = value;
			}
		}

		#region Event Handlers

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		/// <summary>
		/// event handler responding to the ADD button click event
		/// </summary>
		protected void btnAddNew_Click(object sender, EventArgs e)
		{
			bool istblSeasonMaintenanceTableVisible = this.tblSeasonMaintenance.Visible;
			
			try 
			{			
				this.SelectedSeasonID = 0;
				this.MaintenanceControlVisible = true;

				if(istblSeasonMaintenanceTableVisible) 
				{					
					ShowSaveSeasonConfirmation();					
				}
				else
				{
					LoadSelectedSeason();					
				}
			} 
			catch(MessageException ex) 
			{
				this.Page.SetPageError(ex);
			}
		}

		/// <summary>
		/// event handler responding to SelectSeasonClicked event from ctrlSeasonListControl
		/// </summary>
		/// <param name="sender">ctrlSeasonListControl</param>
		/// <param name="e">SelectSeasonClickedArgs</param>
		private void ctrlSeasonListControl_SelectSeasonClicked(object sender, SelectSeasonClickedArgs e)
		{
			bool isSeasonTableVisible = this.tblSeasonMaintenance.Visible;

			try 
			{
				this.SelectedSeasonID = e.Season.dataSet.Season[0].ID;
				this.MaintenanceControlVisible = true;

				if(isSeasonTableVisible) 
				{
					//should we save currently loaded Season?
					ShowSaveSeasonConfirmation();
				}
				else
				{
					LoadSelectedSeason();
				}						
			}
			catch(MessageException ex) 
			{
				this.Page.SetPageError(ex);
			} 
		}

		protected void btnInsertNewFiscalYear_Click(object sender, EventArgs e)
		{
			try
			{
				this.Page.CurrentTransaction = new Transaction(TRANSACTION_NAME, DataBaseName.QSPCanadaCommon);
				this.Page.CurrentTransaction.Open();

				Season season = new Season(this.Page.CurrentTransaction);
				season.InsertNewFiscalYear(Convert.ToInt32(this.Page.UserID));

				this.Page.CurrentTransaction.Save();					
				this.Page.CurrentTransaction = null;

				this.ctrlSeasonListControl.DataBind();
			}
			catch(MessageException ex) 
			{
				this.Page.SetPageError(ex);
				this.Page.CurrentTransaction.Cancel();
				
				this.Page.CurrentTransaction = null;
			}
		}

		private void ctrlSeasonListControl_DeleteSeasonClicked(object sender, SelectSeasonClickedArgs e)
		{
			bool isSeasonTableVisible = this.tblSeasonMaintenance.Visible;

			try 
			{
				if(isSeasonTableVisible && this.ctrlSeasonListControl.SeasonIDToDelete.Equals(this.ctrlSeasonMaintenanceControl.SeasonID)) 
				{
					this.SelectedSeasonID = 0;
					this.MaintenanceControlVisible = false;
					LoadSelectedSeason();
				}
			}
			catch(MessageException ex) 
			{
				this.Page.SetPageError(ex);
			} 
		}

		private void ctrlSeasonMaintenanceControl_SaveSeasonClicked(object sender, SelectSeasonClickedArgs e)
		{
			this.MaintenanceControlVisible = this.tblSeasonMaintenance.Visible = false;
			this.ctrlSeasonListControl.DataBind();
		}

		private void ctrlSeasonMaintenanceControl_CancelSeasonClicked(object sender, SelectSeasonClickedArgs e)
		{
			this.MaintenanceControlVisible = this.tblSeasonMaintenance.Visible = false;
		}

		private void ctrlControlerYesNoConfirmation_ConfirmedYes(object sender, EventArgs e)
		{
			try 
			{								
				this.Page.CurrentTransaction = new Transaction(TRANSACTION_NAME, DataBaseName.QSPCanadaCommon);

				this.Page.CurrentTransaction.Open();

				ctrlSeasonMaintenanceControl.Save();

				this.Page.CurrentTransaction.Save();
					
				this.Page.CurrentTransaction = null;

				this.ctrlSeasonListControl.DataBind();

				LoadSelectedSeason();
			}
			catch(MessageException ex) 
			{
				this.Page.SetPageError(ex);
				this.Page.CurrentTransaction.Cancel();
				
				this.Page.CurrentTransaction = null;
			}
		}

		private void ctrlControlerYesNoConfirmation_ConfirmedNo(object sender, EventArgs e)
		{
			try
			{
				LoadSelectedSeason();
			}
			catch(MessageException ex) 
			{
				this.Page.SetPageError(ex);
			}
		}

		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			this.ctrlSeasonListControl.SelectSeasonClicked += new SelectSeasonEventHandler(ctrlSeasonListControl_SelectSeasonClicked);
			this.ctrlSeasonListControl.DeleteSeasonClicked += new SelectSeasonEventHandler(ctrlSeasonListControl_DeleteSeasonClicked);
			this.ctrlSeasonMaintenanceControl.SaveSeasonClicked += new SelectSeasonEventHandler(ctrlSeasonMaintenanceControl_SaveSeasonClicked);
			this.ctrlSeasonMaintenanceControl.CancelSeasonClicked += new SelectSeasonEventHandler(ctrlSeasonMaintenanceControl_CancelSeasonClicked);
			this.ctrlControlerYesNoConfirmation.ConfirmedYes += new ConfirmYesNoEventHandler(ctrlControlerYesNoConfirmation_ConfirmedYes);
			this.ctrlControlerYesNoConfirmation.ConfirmedNo += new ConfirmYesNoEventHandler(ctrlControlerYesNoConfirmation_ConfirmedNo);
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

		/// <summary>
		/// confirm if the season is to be saved or changes discarded
		/// </summary>
		/// <param name="seasonID">ID of the season that is requested for deletion</param>
		private void ShowSaveSeasonConfirmation() 
		{
			try
			{
				this.ctrlControlerYesNoConfirmation.Message = SAVE_SEASON_CONFIRMATION_MESSAGE;
				this.ctrlControlerYesNoConfirmation.ShowConfirmationWindow();
			}
			catch(MessageException ex) 
			{
				this.Page.SetPageError(ex);
			}
		}

		private void LoadSelectedSeason()
		{
			this.ctrlSeasonMaintenanceControl.SeasonID = this.SelectedSeasonID;
			this.ctrlSeasonMaintenanceControl.DataBind();
			this.tblSeasonMaintenance.Visible = this.MaintenanceControlVisible;
		}
	}
}
