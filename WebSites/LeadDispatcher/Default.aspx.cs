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
using System.Diagnostics;
using GA.BDC.Core.EnterpriseComponents;
using System.IO;


namespace CRMWeb
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public partial class Default : System.Web.UI.Page  
	{
		protected UserControls.Login Login1;
		protected System.Web.UI.WebControls.PlaceHolder PlaceHolderDataHeader;
        protected UserControls.TopMenuAR TopMenuAR1;
        protected UserControls.TopMenuLead TopMenuLead1;
		protected UserControls.AdjustmentInfo AdjustmentInfo1;
        protected UserControls.PaymentInfo PaymentInfo1;
		protected UserControls.DataHeaderAR DataHeaderAR1;
        protected UserControls.DataHeaderLead DataHeaderLead1;
        protected UserControls.Lead.LeadDispatcher LeadDispatcher1;
        protected UserControls.Lead.LeadInfo1 LeadInfo1;
		protected UserControls.LeftMenuConsultant LeftMenuConsultant1;
        protected UserControls.Admin_Section Admin_Section1;

		

		protected void Page_Load(object sender, System.EventArgs e)
		{
  
	
 			try{
	         
               //get parameters////
				
                if (!IsPostBack){

					Session[Global.SessionVariables.PROJECT_LOCATION] = Server.MapPath(".");

					int leadIDForAccounts = Convert.ToInt32(Request["la"]);
					bool process = Convert.ToBoolean(Request["process"]);
				
					//IF la <> 0, means we want to go to AllAccountsForLeads.acsx, unauthentificated
					if (leadIDForAccounts != 0){
						Session[Global.SessionVariables.ACCESS_LEAD_ACCOUNTS_ONLY] = "true";
                        Session[Global.SessionVariables.LEAD_ACCOUNTS_ID] = leadIDForAccounts;
						TopMenuLead1.TabStripLead.SelectedIndex = 2;
						TabStripLead_SelectedIndexChange(sender, new System.EventArgs());
                        TopMenuLead1.TabStripLead.Enabled = false;

						Admin_Section1.Admin_Menu1.TabStripAdmin.SelectedIndex = 1;
                        Admin_Section1.TabStripAdmin_SelectedIndexChange(sender, new System.EventArgs());
                        Admin_Section1.Refresh(leadIDForAccounts,process);
						Admin_Section1.Admin_Menu1.TabStripAdmin.Enabled = false;
	
					}else{
						Session[Global.SessionVariables.ACCESS_LEAD_ACCOUNTS_ONLY] = "false";
					}
				}
               ///////////////////////////////////////////
              	 		

			
				if (Session[Global.SessionVariables.AUTHENTIFICATED] == null){
					Session[Global.SessionVariables.AUTHENTIFICATED] = "no";
				}

				if (Session[Global.SessionVariables.AUTHENTIFICATED].ToString() == "no" &&
					Session[Global.SessionVariables.ACCESS_LEAD_ACCOUNTS_ONLY] == "false"){
					cmdLogOff_Click(sender,new ImageClickEventArgs(0,0));
				}
			//	}

				if (!IsPostBack){
 			

	
					EnableSearch(false);
					cboQuickSearch.Items.Add("Lead ID");
					if (Request.Cookies["userName"] != null){
						Login1.txtUserName.Text = Server.HtmlEncode(Request.Cookies["userName"].Value);
					}

				}

				Helper.SetOnKeyPressBehavior(this.txtQuickSearch, this.cmdQuickSearch.ClientID);
				Helper.SetOnKeyPressBehavior(Login1.txtPassword, Login1.cmdLogin.ClientID);
				Helper.SetOnKeyPressBehavior(Login1.txtUserName, Login1.cmdLogin.ClientID);


				// Put user code to initialize the page here

				//		if (!IsPostBack){

				Classes.ClientInfo ci = new Classes.ClientInfo();
				Classes.SaleInfo si = new Classes.SaleInfo();
	

						/*	DataTable dt = DatabaseObjects.GetSalesPaymentAdjustment(si.SALE_ID);
								if (dt.Rows.Count > 0 ){
	

									// Debug.Write(dt.Rows[0]["total_adjustment"].ToString());
    
									if (dt.Rows[0]["total_paid"] == DBNull.Value){
										si.TOTAL_PAID = 0;
									}else{
										si.TOTAL_PAID = Convert.ToDecimal(dt.Rows[0]["total_paid"]);
									}
	
									if (dt.Rows[0]["total_amount"] == DBNull.Value){
										si.TOTAL_AMOUNT = 0;
									}else{
										si.TOTAL_AMOUNT = Convert.ToDecimal(dt.Rows[0]["total_amount"]);
									}
	
									if (dt.Rows[0]["total_adjustment"] == DBNull.Value){
										si.TOTAL_ADJUSTMENT = 0;
									}else{
										si.TOTAL_ADJUSTMENT = Convert.ToDecimal(dt.Rows[0]["total_adjustment"]);
									}

					
									si.BALANCE = si.TOTAL_AMOUNT - si.TOTAL_PAID - si.TOTAL_ADJUSTMENT;
	

									ci.FIRST_NAME = dt.Rows[0]["FIRST_NAME"].ToString();
									ci.LAST_NAME = dt.Rows[0]["LAST_NAME"].ToString();
									ci.CLIENT_NO = dt.Rows[0]["client_id"].ToString();
									ci.CLIENT_SEQUENCE_CODE = dt.Rows[0]["client_sequence_code"].ToString();
									ci.ORGANIZATION = dt.Rows[0]["organization"].ToString();
	
									Session[Global.SessionVariables.CLIENT_INFO] = ci;
									Session[Global.SessionVariables.SALE_INFO] = si;
								}
				*/
	
						///load right page
						///

	


						//&&
						//Session[Global.SessionVariables.CURRENTTAB] != null){
		
						//	UserControls.DataHeaderAR dh = (UserControls.DataHeaderAR) LoadControl("UserControls/DataHeaderAR.ascx");
						//	PlaceHolderDataHeader.Controls.Add(dh);
		
		
						/*	switch(Session[Global.SessionVariables.CURRENTTAB].ToString()){
									case "Payment":
										UserControls.PaymentInfo c = (UserControls.PaymentInfo) LoadControl("UserControls/PaymentInfo.ascx");
										PlaceHolder.Controls.Add(c);
										break;
									case "Adjustment":
										UserControls.AdjustmentInfo  a = (UserControls.AdjustmentInfo) LoadControl("UserControls/AdjustmentInfo.ascx");
										PlaceHolder.Controls.Add(a);
										break;
								}
		
							*/		
					
				
	
			
				//	}
			}catch(Exception ex){
				throw new Global.CRMException("",ex,0,"Default.Page_Load");
			
			}
			
		
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
		
			Login1.cmdLogin.Click += new System.Web.UI.ImageClickEventHandler(this.cmdLogin_Click);
			TopMenuAR1.TabStripAR.SelectedIndexChange += new System.EventHandler(this.TabStripAR_SelectedIndexChange);
			TopMenuLead1.TabStripLead.SelectedIndexChange += new System.EventHandler(this.TabStripLead_SelectedIndexChange);
			LeadDispatcher1.ChangeTab += new System.EventHandler(Dispatcher_ChangeTab);
			LeftMenuConsultant1.cmdLogOff.Click += new System.Web.UI.ImageClickEventHandler(this.cmdLogOff_Click);
		
			
			
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.cmdQuickSearch.Click += new System.Web.UI.ImageClickEventHandler(this.cmdQuickSearch_Click);

		}
		#endregion


		private void cmdLogOff_Click(object sender, System.Web.UI.ImageClickEventArgs e){
			Session[Global.SessionVariables.AUTHENTIFICATED] = "no";
			Session[Global.SessionVariables.ACCESS_LEAD_ACCOUNTS_ONLY] = "false";
			SetInvisible();
			Login1.Visible = true;
			EnableSearch(false);
			TopMenuLead1.TabStripLead.Enabled = true;
			Admin_Section1.Admin_Menu1.TabStripAdmin.Enabled = true;
            //Session.Abandon();
		}


		private void cmdLogin_Click(object sender, System.Web.UI.ImageClickEventArgs e){

			StreamWriter sw;
			try{

            
				Classes.UserInfo ui = new Classes.UserInfo();

				ui.USER_NAME = Login1.txtUserName.Text;
				ui.PASSWORD = Login1.txtPassword.Text;
            

				DataTable dt =  DatabaseObjects.AuthentificatedUser(ui.USER_NAME, ui.PASSWORD);
				if (dt.Rows.Count > 0 ){
					ui.ACCESS_LEVEL = Convert.ToInt32(dt.Rows[0]["access_level"]);
					ui.NAME = dt.Rows[0]["name"].ToString();
				
					Session[Global.SessionVariables.USER_ID] = dt.Rows[0]["consultant_id"].ToString();
					Session[Global.SessionVariables.USER_INFO] = ui;
					DataHeaderLead1.Refresh();
 			
					if (ui.ACCESS_LEVEL == 1 || ui.ACCESS_LEVEL == 3){
						HttpCookie aCookie = new HttpCookie("userName");
						aCookie.Value = ui.USER_NAME;
						aCookie.Expires = DateTime.Now.AddDays(10);
						Response.Cookies.Add(aCookie);


					
						Login1.lblError.Visible = false;
						SetInvisible();
						EnableSearch(true);
						LoadLead();
					
						TopMenuLead1.Visible = true;
						TopMenuLead1.TabStripLead.SelectedIndex = 0;
						TabStripLead_SelectedIndexChange(sender, new System.EventArgs());

						Session[Global.SessionVariables.AUTHENTIFICATED] = "yes";
				

						//TopMenuAR1.Visible = true;
					
						//	Session[Global.SessionVariables.CURRENTTAB] = "Payment";
			
						//PaymentInfo1.Visible = true;
				
						//DataHeaderAR1.Visible = true;
						//DataHeaderLead1.Visible = true;
						//	UserControls.DataHeaderAR dh = (UserControls.DataHeaderAR) LoadControl("UserControls/DataHeaderAR.ascx");
						//	PlaceHolderDataHeader.Controls.Add(dh);
					}else{
						Login1.lblError.Visible = true;
					}

				}else{
					Login1.lblError.Visible = true;
				}
	
			}catch(Exception ex){
				
				throw new Global.CRMException("Error login in",ex,0,"Default.Login_Click");
			
			}
		}

		
		public void Dispatcher_ChangeTab(object sender, System.EventArgs e){
			TopMenuLead1.TabStripLead.SelectedIndex = 1;
			TabStripLead_SelectedIndexChange(sender, new System.EventArgs());
		

		}


		private void SetInvisible(){
			TopMenuAR1.Visible = false;
			TopMenuLead1.Visible = false;
			Login1.Visible = false;
			LeadDispatcher1.Visible = false;
			Admin_Section1.Visible = false;
			LeadInfo1.Visible = false;
			DataHeaderLead1.Visible = false;
			DataHeaderAR1.Visible = false;

		}


		private void EnableSearch(bool enable){
			txtQuickSearch.Enabled = enable;
			cboQuickSearch.Enabled = enable;
		}

		private void TabStripAR_SelectedIndexChange(object sender, System.EventArgs e) {
		//	PlaceHolder.Controls.Clear();
		    PaymentInfo1.Visible = false;
			AdjustmentInfo1.Visible = false;
		
			switch(TopMenuAR1.TabStripAR.SelectedIndex){         
				case 0:   
					break;                  
				case 1:
					PaymentInfo1.Visible = true;
				//	UserControls.PaymentInfo p = (UserControls.PaymentInfo) LoadControl("UserControls/PaymentInfo.ascx");
				//	PlaceHolder.Controls.Add(p);

						break;
				case 2:
					AdjustmentInfo1.Visible = true;
					break;
		
			}


		}

		private void TabStripLead_SelectedIndexChange(object sender, System.EventArgs e) {
				
			//if its a user for lead accounts only, we send him to logon screen if he tries another screen
	/*		if (Session[Global.SessionVariables.ACCESS_LEAD_ACCOUNTS_ONLY].ToString() == "true" &&
				Convert.ToInt32(TopMenuLead1.TabStripLead.SelectedIndex) != 2){
				cmdLogOff_Click(sender,new ImageClickEventArgs(0,0));
			}else{

*/
				//	PlaceHolder.Controls.Clear();
				SetInvisible();
				TopMenuLead1.Visible = true;

				switch(TopMenuLead1.TabStripLead.SelectedIndex){         
					case 0:  
						LeadDispatcher1.Visible = true;
						break;                  
					case 1:
						LeadInfo1.Refresh();
						DataHeaderLead1.Refresh();
						DataHeaderLead1.Visible = true;
						LeadInfo1.Visible = true;
						break;
					case 2:
						Admin_Section1.Visible = true;
						break;
		
				}
//			}
		}

		private void LoadLead(){
			LeadDispatcher1.Refresh();
			LeadInfo1.Refresh();
			DataHeaderLead1.Refresh();
			Admin_Section1.Refresh();
		}

		protected void txtQuickSearch_TextChanged(object sender, System.EventArgs e) {
		
		}

		private void cmdQuickSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e) {

			switch(cboQuickSearch.SelectedItem.Text){         
				case "Lead ID":  
					
					if (GA.BDC.Core.EnterpriseComponents.Helper.IsNumeric(txtQuickSearch.Text)) {
			
						int leadID = Convert.ToInt32(txtQuickSearch.Text);
						Session[Global.SessionVariables.CURRENT_LEAD_ID] = leadID;
								
						TopMenuLead1.TabStripLead.SelectedIndex = 1;
						TabStripLead_SelectedIndexChange(sender, new System.EventArgs());
				
					}
					
					break;                  
			}
			
		
		}
	}
}
