namespace QSPFulfillment.Fulfillment.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSP.WebControl;
	using Business.Objects;
	using Common;
	using Common.TableDef;

	public delegate void SelectSeasonEventHandler(object sender, SelectSeasonClickedArgs e);

	/// <summary>
	///		List of seasons and season details
	/// </summary>
	/// <remarks>
	///		Created on 2006-07-03
	///		Created by Saitakhmetova Madina
	/// </remarks>
	public partial class SeasonListControl : FulfillmentControl
	{
		/************* vars for delete confirmation ********************/
		private const string DELETE_SEASON_CONFIRMATION_MESSAGE = "Are you sure you want to delete this season?";
		protected QSPFulfillment.CustomerService.ControlerConfirmationPage ctrlControlerConfirmationPageDelete;
		/***************************************************************/


		public event SelectSeasonEventHandler DeleteSeasonClicked;
		public event SelectSeasonEventHandler SelectSeasonClicked;

		#region Event Handlers

		protected void Page_Load(object sender, System.EventArgs e)
		{
		}		

		/// <summary>
		/// event handler responding to the page change event
		/// </summary>
		private void dtgMain_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			if(e.NewPageIndex >= 0 && e.NewPageIndex < this.dtgMain.PageCount) 
			{
				this.dtgMain.SelectedIndex = -1;
				this.dtgMain.CurrentPageIndex = e.NewPageIndex;
				DataBind();
			}
		}

		/// <summary>
		/// event handler responding to any item event (delete, select)
		/// of the datagrid
		/// </summary>
		private void dtgMain_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			Season season;

			if(e.CommandName == "Delete") 
			{
				ShowDeleteSeasonConfirmation(GetID(e.Item));				
			}
			else if(e.CommandName == "Select") 
			{
				SelectSeasonClickedArgs args;

				season = new Season(this.Page.CurrentTransaction);

				season.GetOneByID(GetID(e.Item));

				args = new SelectSeasonClickedArgs(season);

				if(SelectSeasonClicked != null)
					SelectSeasonClicked(source, args);
			}
		}

		/// <summary>
		/// event handler responding to the event 
		/// that is raised when deleting is confirmed
		/// </summary>
		private void ctrlControlerConfirmationPageDelete_Confirmed(object sender, EventArgs e)
		{
			Season season = new Season(this.Page.CurrentTransaction);
			season.Delete(this.SeasonIDToDelete);
			this.DataBind();

			//inform parent control that an item has been deleted
			SelectSeasonClickedArgs args;		

			args = new SelectSeasonClickedArgs(season);

			if(DeleteSeasonClicked != null)
				DeleteSeasonClicked(this.dtgMain, args);
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
			this.dtgMain.PageIndexChanged += new DataGridPageChangedEventHandler(dtgMain_PageIndexChanged);
			this.dtgMain.ItemCommand += new DataGridCommandEventHandler(dtgMain_ItemCommand);
			this.ctrlControlerConfirmationPageDelete.Confirmed += new QSPFulfillment.CustomerService.ConfirmEventHandler(ctrlControlerConfirmationPageDelete_Confirmed);
		}
		#endregion

		#region Properties
		/// <summary>
		/// SeasonID of the season that was marked for deletion
		/// </summary>
		public int SeasonIDToDelete 
		{
			get 
			{
				int seasonIDToDelete = 0;

				if(ViewState["SeasonIDToDelete"] != null)
				{
					seasonIDToDelete = Convert.ToInt32(this.ViewState["SeasonIDToDelete"]);
				} 

				return seasonIDToDelete;
			}
			set 
			{
				this.ViewState["SeasonIDToDelete"] = value;
			}
		}	
		#endregion

		#region Public Methods

		/// <summary>
		/// databinds this control
		/// </summary>
		public override void DataBind()
		{
			try 
			{
				LoadData();
			}
			catch(MessageException ex) 
			{
				this.Page.SetPageError(ex);
			}
		}
		#endregion

		#region Private Methods

		/// <summary>
		/// get the list of all seasons and bind it to the datagrid
		/// </summary>
		private void LoadData() 
		{
			Season season = new Season(this.Page.CurrentTransaction);
			season.GetAll();

			this.dtgMain.DataSource = season.dataSet;
			this.dtgMain.DataMember = season.dataSet.Season.TableName;
			this.dtgMain.DataBind();
		}

		/// <summary>
		/// get the id from the passed datagrid item
		/// </summary>
		/// <param name="e">datagrid item</param>
		/// <returns>id</returns>
		private int GetID(DataGridItem e) 
		{
			int iID = 0;

			try 
			{
				iID = Convert.ToInt32(((Label) e.FindControl("lblID")).Text);
			} 
			catch { }

			return iID;
		}

		/// <summary>
		/// validate delete and display delete confirmation
		/// </summary>
		/// <param name="seasonID">ID of the season that is requested for deletion</param>
		private void ShowDeleteSeasonConfirmation(int seasonID) 
		{
			Season season = new Season(this.Page.CurrentTransaction);
			season.GetOneByID(seasonID);
			SeasonDataSet.SeasonRow row = (SeasonDataSet.SeasonRow) season.dataSet.Season.Rows[0];
			row.Delete();

			if(season.VerifyDelete())
			{				
				this.SeasonIDToDelete = seasonID;
				this.ctrlControlerConfirmationPageDelete.Message = DELETE_SEASON_CONFIRMATION_MESSAGE;
				this.ctrlControlerConfirmationPageDelete.ShowConfirmationWindow();
			}
		}

		#endregion

	}
}
