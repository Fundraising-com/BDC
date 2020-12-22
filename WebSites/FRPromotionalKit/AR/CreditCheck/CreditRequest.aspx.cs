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
using efundraising.EFundraisingCRM;
using System.Diagnostics;
using System.Text;
using efundraising.EnterpriseComponents;
using System.IO;
using System.Threading;
using efundraising.Email;
using System.Configuration;
using efundraising.Diagnostics;
using efundraising.EFundraisingCRMWeb.Components.Server;
using efundraising.CreditCheck;


namespace efundraising.EFundraisingCRMWeb.AR.CreditCheck
{
	/// <summary>
	/// Summary description for CreditRequest.
	/// </summary>
	/// 

	
	public partial class CreditRequest : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button OrderExperianButton;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Button Button3;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Panel UpdateStatusPanel;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.CheckBox ChkAll;
		protected System.Web.UI.WebControls.Image OrderImage;
		protected EFundraisingCRMWeb.Components.User.CreditRequestDetails CreditRequestDetails1;
				
	
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
            Refresh();	
			
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion



		#region Private Methods


		protected void Page_Load(object sender, System.EventArgs e)
		{
		}


		private void FillDataGridRequest(CreditCheckRequest[] ccaList)
		{
			try
			{
     			// get Datagrid information of the new requests and Binds it
				dgRequests.DataSource = ManageCreditRequest.CreateDataTableRequests(ccaList);
				dgRequests.DataBind();
			
				if (dgRequests.Items.Count > 0)
				{
					NoRequestsLabel.Visible = false;
					//diplay first record details
					int leadID = Convert.ToInt32(dgRequests.Items[0].Cells[Convert.ToInt32(ManageCreditRequest.FieldColumns.ColLeadID)].Text.Trim());
					int creditCheckID = Convert.ToInt32(dgRequests.Items[0].Cells[Convert.ToInt32(ManageCreditRequest.FieldColumns.ColCreditCheck)].Text.Trim());

					//load lead
					EFundraisingCRM.Lead l = EFundraisingCRM.Lead.GetLeadByID(leadID);
					if (l != null)
					{
						CreditRequestDetails1.DisplayDetails(l,  creditCheckID);
					}
				}
				else
				{
					NoRequestsLabel.Visible = true;
     			}
			}
			catch(Exception ex)
			{
                Logger.LogError("Error in FillDataGridRequest", ex);
			}
		
		}


		private void Refresh()
		{
			try
			{
				// get the a dataset of the credit requests
				CreditCheckRequest[] ccrList = CreditCheckRequest.GetCreditCheckRequestUnconfirmed();
				if (ccrList !=  null)
				{
					FillDataGridRequest(ccrList);
				}

				//fill credit status and links
				// Iterate through all rows
				for (int i=0; i < dgRequests.Items.Count; i++) 
				{
					CreditCheckStatus[] ccsList = CreditCheckStatus.GetCreditCheckStatus();
					DropDownList list = (DropDownList) dgRequests.Items[i].FindControl("CreditStatusDropDown");
				
					for (int j = 0; j < ccsList.Length; j++)
					{
						ListItem item = new ListItem(ccsList[j].Description,ccsList[j].CreditCheckStatusID.ToString());
						list.Items.Add(item);
					}

					//check score to set default value
					int score = 0;

					if (Helper.IsNumeric(dgRequests.Items[i].Cells[Convert.ToInt32(ManageCreditRequest.FieldColumns.ColScore)].Text))
					{
						score = Convert.ToInt32(dgRequests.Items[i].Cells[Convert.ToInt32(ManageCreditRequest.FieldColumns.ColScore)].Text);
					}
                
					if (score == -1)
					{
						list.SelectedValue =  Convert.ToInt32(CreditStatus.No_Match).ToString();
					}
					else
					{
						list.SelectedValue =  Convert.ToInt32(CreditStatus.Pending).ToString();
					}
				}
			}
			catch(Exception ex)
			{
				Logger.LogError("Error in Refresh", ex);
			}
 		}


		protected void lnkRefresh_Click(object sender, System.EventArgs e)
		{
			Refresh();
		}	
		

		protected void ProcessButton_Click(object sender, System.EventArgs e)
		{
			try
			{
				bool error = false;
				ErrorLabel.Visible = false;

				//Goes through each request in Datatable, for each request checkes, get full credit request from databse and updates it
				for (int i=0; i < dgRequests.Items.Count; i++) 
				{
					HtmlInputCheckBox chk = (HtmlInputCheckBox) dgRequests.Items[i].FindControl("RequestCheckBox");
				
					if (chk.Checked)
					{
						DropDownList ddl = (DropDownList) dgRequests.Items[i].FindControl("CreditStatusDropDown");
					
						int leadID = Convert.ToInt32(dgRequests.Items[i].Cells[Convert.ToInt32(ManageCreditRequest.FieldColumns.ColLeadID)].Text);
						int creditCheckID = Convert.ToInt32(dgRequests.Items[i].Cells[Convert.ToInt32(ManageCreditRequest.FieldColumns.ColCreditCheck)].Text);
	    				int creditStatusID = Convert.ToInt32(ddl.SelectedValue);
					
						CreditCheckRequest ccr =  CreditCheckRequest.GetCreditCheckRequestByID(creditCheckID);
						ccr.CreditStatusID = creditStatusID;					
					
						//if denied partially
						if (creditStatusID == Convert.ToInt32(CreditStatus.Denied_Partially)) 
						{
							TextBox txt = (TextBox) dgRequests.Items[i].FindControl("AmtTextBox");
							if (Helper.IsNumeric(txt.Text))
							{
								ccr.AmountApproved = Convert.ToDouble(txt.Text);
								ccr.ResultConfirmationDate = DateTime.Now;
							}
							else
							{
								error = true;
							}
						}//if Accepted
						else if (creditStatusID == Convert.ToInt32(CreditStatus.Accepted))
						{
							ccr.AmountApproved = ccr.AmountRequested;
							ccr.ResultConfirmationDate = DateTime.Now;
						}//if Denied
 						else if (creditStatusID == Convert.ToInt32(CreditStatus.Denied))
						{
							ccr.AmountApproved = 0;
							ccr.ResultConfirmationDate = DateTime.Now;
						}//if NoMatch
						else if (creditStatusID == Convert.ToInt32(CreditStatus.No_Match))
						{
							ccr.AmountApproved = 0;
							ccr.ResultConfirmationDate = DateTime.Now;
						}
					  	
						if (error)
						{
							ErrorLabel.Visible = true;
						}
						else
						{
							CreditCheckRequest.UpdateCreditCheckRequest(ccr);
							dgRequests.Items[i].Visible = false;
						}
					
						//send email if necessary
						if (ccr.CreditStatusID != Convert.ToInt32(CreditStatus.Pending))
						{
							ccr.Reason = 15;
							ManageCreditRequest.SendConfirmationEmail(ccr);
						}
					}
				}
			}		
			catch(Exception ex)
			{
				Logger.LogError("Error in ProcessButton_Click", ex);
			}

		}

			
		private void OrderExperian(int creditCheckID, int leadID)
		{
			try
			{
				//Call Process Order ANd Display Report Result in TextBox
				string statusCode = ManageCreditRequest.ProcessConsultantRequest(creditCheckID);
					
				//if error in web service communication
				if (Convert.ToInt32(statusCode) >= 1003)
				{
					ReportTextBox.Text = "Communication error with the credit bureau. Please try again later.";
				}
				else
				{					
					CreditCheckRequest ccr = CreditCheckRequest.GetCreditCheckRequestByID(creditCheckID);
					string creditReport = ccr.CreditReport;
									
					if (creditReport.Length > 0)
					{
                    	ReportTextBox.Text = ccr.CreditReport;		
					}
					else
					{
						Logger.LogError("Error in OrderExperian. Report Was Not Found. Lead ID=  " + leadID);
					}
				}		
				Refresh();
			}
			catch(Exception ex)
			{
				Logger.LogError("Error in OrderExperian. CreditCheckID=" + creditCheckID, ex);
			}
		}


		protected void BulkOrderButton_Click(object sender, System.EventArgs e)
		{
			try
			{
             	//Order Report for every lead selected
				for (int i=0; i < dgRequests.Items.Count; i++) 
				{
					HtmlInputCheckBox chk = (HtmlInputCheckBox) dgRequests.Items[i].FindControl("RequestCheckBox");
				
					if (chk.Checked)
					{
						int leadID = Convert.ToInt32(dgRequests.Items[i].Cells[Convert.ToInt32(ManageCreditRequest.FieldColumns.ColLeadID)].Text);
						int creditCheckID = Convert.ToInt32(dgRequests.Items[i].Cells[Convert.ToInt32(ManageCreditRequest.FieldColumns.ColCreditCheck)].Text);
						OrderExperian(creditCheckID, leadID);
					}
				}
				
			}		
			catch(Exception ex)
			{
				Logger.LogError("Error in BulkOrderButton_Click", ex);
			}
			
		}
		#endregion Private Methods

		#region Protected Methods
		protected void CreditStatusDropDown_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				//Get whick dropdown was selected and display amount field below if  denied_partially is selected
				DropDownList lst = (DropDownList) sender;
				TableCell cell = (TableCell) lst.Parent;
				DataGridItem item = (DataGridItem) cell.Parent;
				int i = item.ItemIndex;

				TextBox txt = (TextBox) dgRequests.Items[i].FindControl("AmtTextBox");
				if (Convert.ToInt32(lst.SelectedItem.Value) == Convert.ToInt32(CreditStatus.Denied_Partially))
				{				
					txt.Visible = true;
				}
				else
				{
					txt.Visible = false;
				}

			}	
			catch(Exception ex)
			{
				Logger.LogError("Error in CreditStatusDropDown_SelectedIndexChanged", ex);
			}
 		}

		protected void LeadNameLink_Click(object sender, System.EventArgs e)
		{
			try
			{
				//Display Details for lead ID clicked
				LinkButton lnk = (LinkButton) sender;
				TableCell cell = (TableCell) lnk.Parent;
				DataGridItem item = (DataGridItem) cell.Parent;
				int i = item.ItemIndex;
		    
				int leadID = Convert.ToInt32(dgRequests.Items[i].Cells[Convert.ToInt32(ManageCreditRequest.FieldColumns.ColLeadID)].Text);
				int creditCheckID = Convert.ToInt32(dgRequests.Items[i].Cells[Convert.ToInt32(ManageCreditRequest.FieldColumns.ColCreditCheck)].Text);
				EFundraisingCRM.Lead l = EFundraisingCRM.Lead.GetLeadByID(leadID);
				if (l != null)
				{
					CreditRequestDetails1.DisplayDetails(l, creditCheckID);
    			}
			}	
			catch(Exception ex)
			{
				Logger.LogError("Error in LeadNameLink_Click", ex);
			}
			
		}


		protected void ChkAll_CheckedChanged(object sender, System.EventArgs e)
		{
			try
			{
				CheckBox all = (CheckBox) sender;
				bool check = all.Checked;

				for (int i=0; i < dgRequests.Items.Count; i++) 
				{
					HtmlInputCheckBox chk = (HtmlInputCheckBox) dgRequests.Items[i].FindControl("RequestCheckBox");
					chk.Checked = check;
				}
			}	
			catch(Exception ex)
			{
				Logger.LogError("Error in ChlAll_CheckedChange", ex);
			}

		}

		protected void ReportImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				//Order The report through the web service
				ImageButton img = (ImageButton) sender;
				TableCell cell = (TableCell) img.Parent;
				DataGridItem item = (DataGridItem) cell.Parent;

				int i = item.ItemIndex;
			
				int leadID = Convert.ToInt32(dgRequests.Items[i].Cells[Convert.ToInt32(ManageCreditRequest.FieldColumns.ColLeadID)].Text);
				int creditCheckID = Convert.ToInt32(dgRequests.Items[i].Cells[Convert.ToInt32(ManageCreditRequest.FieldColumns.ColCreditCheck)].Text);
				EFundraisingCRM.Lead l = EFundraisingCRM.Lead.GetLeadByID(leadID);
	
				CreditRequestDetails1.DisplayDetails(l, creditCheckID);
				OrderExperian(creditCheckID,leadID);
			}	
			catch(Exception ex)
			{
				Logger.LogError("Error in ReportImageButton_Click", ex);
			}
		}

		protected void ScoreLinkButton_Click(object sender, System.EventArgs e)
		{
			try
			{
				//Open the report to dipslay in the textbox (From Cache)
				LinkButton lnk = (LinkButton) sender;
				TableCell cell = (TableCell) lnk.Parent;
				DataGridItem item = (DataGridItem) cell.Parent;

				int i = item.ItemIndex;
				int creditCheckID = Convert.ToInt32(dgRequests.Items[i].Cells[Convert.ToInt32(ManageCreditRequest.FieldColumns.ColCreditCheck)].Text);
				
				CreditCheckRequest ccr = CreditCheckRequest.GetCreditCheckRequestByID(creditCheckID);
				string creditReport = ccr.CreditReport;
				int leadID = ccr.LeadID;
				
				if (creditReport.Length > 0)
				{
					ReportTextBox.Text = creditReport;		
				}
				else
				{
                    OrderExperian(creditCheckID,leadID);
				}
			}	
			catch(Exception ex)
			{
				Logger.LogError("Error in ScoreLinkButton_Click", ex);
			}
		}
		#endregion Protected Methods

	
	}
}
