using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using efundraising.EnterpriseComponents;
using efundraising.EFundraisingCRM;

namespace efundraising.EFundraisingCRMWeb.AR.CreditCheck
{
	/// <summary>
	/// Summary description for CreditResponse.
	/// </summary>
	public partial class CreditResponse : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{


			// Put user code to initialize the page here
			if (!IsPostBack)
			{
                  Refresh();

			}
		}


		private void Refresh()
		{/*
			// get the a dataset of the credit requests
			CreditCheckRequest[] ccaList = CreditCheckRequest.GetCreditCheckRequestProcessed();
			if (ccaList != null)
			{
				FillResponseDataGrid(ccaList);
				FillDropDowns();
			}
*/
			
			
		}


		public void FillDropDowns()
		{
			
			/*			 
			CreditCheckStatus[] ccsList = CreditCheckStatus.GetCreditCheckStatus();
			for (int i = 0; i < ccsList.Length; i++)
			{
				ListItem item = new ListItem(ccsList[i].Description,ccsList[i].CreditCheckStatusID.ToString());
				CreditStatusDropDown.Items.Add(item);
			}

			LeadCreditRating[] lcrList = LeadCreditRating.GetLeadCreditRating();
			for (int i = 0; i < lcrList.Length; i++)
			{
				ListItem item = new ListItem(lcrList[i].Description,lcrList[i].LeadCreditRatingID.ToString());
				CreditRatingDropDown.Items.Add(item);
			}

*/
		}

		private void FillResponseDataGrid(CreditCheckRequest[] ccaList)
		{
           /*
			// Build our data table for binding
			DataTable dt = new DataTable();
			dt.Columns.Add("credit_check_id");
			dt.Columns.Add("lead_id");
			dt.Columns.Add("lead_name");
			dt.Columns.Add("FC");
			dt.Columns.Add("amount");
			dt.Columns.Add("time");
			dt.Columns.Add("score");

				
			//for every credit check activities
			for (int i = ccaList.Length - 1; i >= 0 ; i--)
			{
				DataRow dr = dt.NewRow();
                dr["credit_check_id"] = ccaList[i].CreditCheckID;
				System.Diagnostics.Debug.Write(ccaList[i].LeadID.ToString());
				dr["lead_id"] = ccaList[i].LeadID;
				dr["lead_name"] = ccaList[i].FirstName + " " + ccaList[i].LastName;
				dr["amount"] = EnterpriseComponents.Helper.FormatCurrency(ccaList[i].AmountRequested);
				//go get consultant_name w/ consultant_id
				Consultant c = Consultant.GetConsultantByID(ccaList[i].ConsultantID); 
				if (c != null)
				{
					dr["FC"] = c.Name;
				}
						
				dr["time"] =  ccaList[i].ResultDate.ToShortDateString() + " " + ccaList[i].ResultDate.ToShortTimeString();
                dr["score"] = ccaList[i].CreditScore;

				dt.Rows.Add(dr);
			}

			// Bind
			dgResults.DataSource = dt;
			dgResults.DataBind();

			/*if (dgResults.Items.Count > 0)
			{
				//NoResultsLabel.Visible = false;
				//diplay first record details
				int leadID = Convert.ToInt32(dgResults.Items[0].Cells[1].Text.Trim());
				int creditCheckID = Convert.ToInt32(dgResults.Items[0].Cells[2].Text.Trim());

				//load lead
				EFundraisingCRM.Lead l = EFundraisingCRM.Lead.GetLeadByID(leadID);
				if (l != null)
				{
					DisplayDetails(l, l.CreditRatingID, creditCheckID);
					
				}
			}
			else
			{
				NoRequestsLabel.Visible = true;
     
			}
			*/



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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.dgResults.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgResults_ItemCommand);

		}
		#endregion


		private void dgResults_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			
/*
			int creditCheckID = Convert.ToInt32(e.Item.Cells[0].Text);
			int leadID = Convert.ToInt32(e.Item.Cells[1].Text);
    		LeadIDTextBox.Text = leadID.ToString();
			
			//load lead
			EFundraisingCRM.Lead l = EFundraisingCRM.Lead.GetLeadByID(leadID);
		/*	if (l != null)
			{
				//check if bnull
				if (l.CreditRatingID == int.MinValue)
				{
					CreditRatingDropDown.SelectedValue = Convert.ToInt32(CreditRating.Standard).ToString();
				}
				else
				{
					CreditRatingDropDown.SelectedValue = l.CreditRatingID.ToString();
				}
				
				ValidUntilTextBox.Text = l.CreditRatingEndDate.ToShortDateString();
				
				//check if null
				if (l.MaximumCreditAmount.Equals(float.MinValue))
				{
					MaxAmountTextBox.Text = "$0";
				}
				else
				{
					
					MaxAmountTextBox.Text = EnterpriseComponents.Helper.FormatCurrency(l.MaximumCreditAmount);
				}
				
			}

			//load credit check info
            CreditCheckRequest ccr = CreditCheckRequest.GetCreditCheckRequestByID(creditCheckID);
			if (ccr != null)
			{
				AmtRequestedTextBox.Text = EnterpriseComponents.Helper.FormatCurrency(ccr.AmountRequested);
				CreditStatusDropDown.SelectedValue = ccr.CreditStatusID.ToString();
				AmountConfirmedTextBox.Text = "$0";
			}
*/
		}
	
	}
}
