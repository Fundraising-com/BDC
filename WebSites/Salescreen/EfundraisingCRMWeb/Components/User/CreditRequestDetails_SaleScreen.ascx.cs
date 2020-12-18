using log4net;

namespace EFundraisingCRMWeb.Components.User
{
    
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using efundraising.EFundraisingCRM;
	using efundraising.EnterpriseComponents;
	using EFundraisingCRMWeb.Components.Server;


	/// <summary>
	///		Summary description for CreditRequestDetails.
	/// </summary>
	public partial class CreditRequestDetails_SaleScreen : System.Web.UI.UserControl
	{
		protected System.Web.UI.HtmlControls.HtmlGenericControl Div1;
        public ILog Logger { get; set; }
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

	    public CreditRequestDetails_SaleScreen()
	    {
            Logger = LogManager.GetLogger(GetType());
	    }
		#region Public Methods


		//This user control let you display, for a specific Credit Request, the lead information sent by the User (FC)
		//and the request history for that lead
		//

		public void DisplayDetails(int creditCheckID)
		{
			try
			{
				//display lead info			
				LeadIDLabel.Text = Session[Global.SessionVariables.LEAD_ID].ToString();
			
				//display credit check info
				CreditCheckRequest cca = CreditCheckRequest.GetCreditCheckRequestByID(creditCheckID);
				if (cca != null)
				{
					AddressLabel.Text = cca.Address + ", " + cca.City + " " + cca.State + " " + cca.Zip;
					AmtREquestedLabel.Text = Helper.FormatCurrency(cca.AmountRequested);
					FirstNameLabel.Text = cca.FirstName;
					LastNameLabel.Text = cca.LastName;
				}

				
			}
			catch(Exception ex)
			{
				Logger.Error("Error in DisplayDetails. Credit Check ID =" + creditCheckID, ex);
			}
		}


		protected void InfoImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				//Order The report through the web service
				ImageButton img = (ImageButton) sender;
				TableCell cell = (TableCell) img.Parent;
				DataGridItem item = (DataGridItem) cell.Parent;

				int i = item.ItemIndex;
							
				int creditCheckID = Convert.ToInt32(dgHistory.Items[i].Cells[0].Text);
				
				DisplayDetails(creditCheckID);
				
			}	
			catch(Exception ex)
			{
				Logger.Error("Error in ReportImageButton_Click", ex);
			}
		}

		#endregion

		#region Private Methods
		
	
		public void FillDataGridHistory(CreditCheckRequest[] ccaList)
		{
			//Get the history of the requests made by a lead
			try
			{
				//dgHistory.DataSource = ManageCreditRequest.CreateDataTableHistory_SaleScreen(ccaList);
				//dgHistory.DataBind();
			}
			catch(Exception ex)
			{
				Logger.Error("Error in FillDataGridHistory", ex);
			}
 		
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
			this.dgHistory.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgHistory_PageIndexChanged);

		}
		#endregion

		private void dgHistory_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{

			dgHistory.CurrentPageIndex = e.NewPageIndex;
			int leadID = Convert.ToInt32(Session[Global.SessionVariables.LEAD_ID]);
			CreditCheckRequest[] ccr = CreditCheckRequest.GetCreditCheckRequestByLeadID(leadID);
			FillDataGridHistory(ccr);
		   
		}
	}
}