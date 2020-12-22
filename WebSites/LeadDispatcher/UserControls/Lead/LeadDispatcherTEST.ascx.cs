namespace CRMWeb.UserControls.Lead
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Diagnostics;
	using GA.BDC.Core.EnterpriseComponents;
	
	using System.Configuration;
	using System.Threading;
	using System.IO;
	
	
	

	/// <summary>
	///		Summary description for PaymentInfo.
	/// </summary>
	public partial class LeadDispatcherTEST : System.Web.UI.UserControl {
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.ImageButton ImageButton1;
		protected UserControls.PaymentAdjusment_Header PaymentAdjusment_Header;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.DropDownList cboLeadStatus;
		protected System.Web.UI.WebControls.ListBox lstConsultants;
		protected UserControls.MyCalendar MyCalendar;
		protected UserControls.MyCalendar MyCalendarAssign;
		public event EventHandler ChangeTab;
	

		protected void Page_Load(object sender, System.EventArgs e) {
	
				
			try{

				lblError.Text = "";
				Helper.SetOnKeyPressBehavior(txtStartDateAssigned, cmdFilterAssigned.ClientID);
				Helper.SetOnKeyPressBehavior(txtEndDateAssigned, cmdFilterAssigned.ClientID);
				Helper.SetOnKeyPressBehavior(txtStartDateUnassigned, cmdFilterUnassigned.ClientID);
				Helper.SetOnKeyPressBehavior(txtEndDateUnassigned, cmdFilterUnassigned.ClientID);

				// Put user code to initialize the page here

				if (!IsPostBack){

					Session["SortDirection"] = "asc";
				
					DateTime start = DateTime.Now.AddMonths(-1);
					DateTime end = DateTime.Now;
					txtStartDateAssigned.Text = start.ToShortDateString();
					txtEndDateAssigned.Text = end.ToShortDateString();

					
					FillConsultantList(1);
	                FillPromoTypeList();



				}

				
				/*if (Session[Global.SessionVariables.DTUNASSIGNEDLEADS] != null) {
					DataTable dt =  (DataTable) Session[Global.SessionVariables.DTUNASSIGNEDLEADS];
					dgUnassigned.DataSource = dt;
					dgUnassigned.DataBind();
				   
				
				
				}

			*/

			}catch(Exception ex){
				throw new Global.CRMException("",ex,0,"LeadDispatcher.Page_Load");
			}
			
		}

	
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e) {
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);

			MyCalendar.Cal.SelectionChanged += new System.EventHandler(this.Cal_SelectionChanged);
			MyCalendarAssign.Cal.SelectionChanged += new System.EventHandler(this.CalAssign_SelectionChanged);

			this.dgUnassigned.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgUnassigned_ItemDataBound);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		/// 

		

		private void InitializeComponent()
		{
			this.dgUnassigned.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgUnassigned_ItemCommand);
			this.dgUnassigned.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgUnassigned_SortCommand);
			this.dgUnassigned.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgUnassigned_ItemDataBound);
			this.cmdRefresh.Click += new System.Web.UI.ImageClickEventHandler(this.ImageButton4_Click);
			this.cmdCalEnd.Click += new System.Web.UI.ImageClickEventHandler(this.cmdCalEnd_Click);
			this.cmdCalStart.Click += new System.Web.UI.ImageClickEventHandler(this.cmdCalStart_Click);
			this.cmdFilterUnassigned.Click += new System.Web.UI.ImageClickEventHandler(this.ImageButton3_Click);
			this.cmdUnassign.Click += new System.Web.UI.ImageClickEventHandler(this.cmdUnassign_Click);
			this.cmdAssign.Click += new System.Web.UI.ImageClickEventHandler(this.cmdAssign_Click);
			this.dgAssigned.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgAssigned_ItemCommand);
			this.dgAssigned.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgAssigned_SortCommand);
			this.calStartAssigned.Click += new System.Web.UI.ImageClickEventHandler(this.calStartAssigned_Click);
			this.calEndAssigned.Click += new System.Web.UI.ImageClickEventHandler(this.calEndAssigned_Click);
			this.CalStartAssDate.Click += new System.Web.UI.ImageClickEventHandler(this.CalStartAssDate_Click);
			this.calEndAssDate.Click += new System.Web.UI.ImageClickEventHandler(this.calEndAssDate_Click);
			this.cmdFilterAssigned.Click += new System.Web.UI.ImageClickEventHandler(this.cmdFilterAssigned_Click);

		}
		#endregion



		private void FillPromoTypeList(){
			DataTable dt = DatabaseObjects.GetPromoGroups();
			cboPromoGroup.DataSource = dt;
			cboPromoGroup.DataTextField = "description";
			cboPromoGroup.DataValueField = "promo_group_id";
			cboPromoGroup.DataBind();
			cboPromoGroup.Items.Add("Unclassified");
			cboPromoGroup.SelectedIndex = 1;

		}

	
		



		
		private void FillUnassignedLeads(){
			FillUnassignedLeads("",true);
		}
		


		
		
		private int GetSelectedPromoGroup(){
		   try{
			   return Convert.ToInt32(cboPromoGroup.SelectedValue);
			}catch(Exception e){
               return 1;
			}
			
		}


		private void FillUnassignedLeads(string sortOrder, bool reload){
			try{
			/*	DataSet ds = new DataSet(); 
				DataTable dt = new DataTable();
				DataTable dtFlagpole = new DataTable();

				if (reload){
					ds = DatabaseObjects.GetUnassignedLeads(GetSelectedPromoGroup());
					dt = ds.Tables[0];
					if (ds.Tables[1].Rows.Count >0){

						dtFlagpole = ds.Tables[1];
						Classes.BusinessProcess bp = new Classes.BusinessProcess();
						bp.ProcessFlagpoles(dtFlagpole);
					}
				}else{
					dv = (DataView) Session[Global.SessionVariables.DVUNASSIGNEDLEADS];
				}
			
				DataView dv = new DataView(dt);
			
		
            
				dgUnassigned.DataSource = dv;
				dgUnassigned.DataBind();
				Session["datagrid"] = dgUnassigned;

				lblNbUnassigned.Text = "Unassigned Leads: " + dv.Count;// + "  (" + cboPromoGroup.SelectedItem.Text  + ")";

				if (dv.Count > 0){
					lblNoLeadsUnassigned.Visible = false;
				}else{
					lblNoLeadsUnassigned.Visible = true;
				}
  
				Session[Global.SessionVariables.DVUNASSIGNEDLEADS] = dv;

			
				DataTable dtKit = DatabaseObjects.GetKitTypes();

			
				// Iterate through all rows
				*/
             
			}catch(Exception ex){
				throw new Global.CRMException("",ex,0,"LeadDispatcher.FillUnassignedLeads");
			}


		}

		

		
		
		private void FillConsultantList(int groupTypeID){
			try{
				DataTable dt = DatabaseObjects.GetConsultantList(groupTypeID);
				cboConsultants.DataSource = dt;
				cboConsultants.DataTextField = "Name";
				cboConsultants.DataValueField = "consultant_id";
				cboConsultants.DataBind(); 
				cboConsultants.Items.Insert(0, "--Select a Consultant--");
			}catch(Exception ex){
				throw new Global.CRMException("",ex,0,"LeadDispatcher.FillConsultantList");
			}
     
		}
		private void dgUnassigned_SelectedIndexChanged(object sender, System.EventArgs e) {
		
		}

		private void dgUnassigned_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e) {
		
			string [] split = null;
			split = e.SortExpression.ToString().Split(' ');

			string column = split[0].ToString();
			//string order = split[1].ToString();
			
			if (Session["SortDirection"].ToString() == "asc"){
				Session["SortDirection"] = "desc";
			}else{
				Session["SortDirection"] = "asc";
			}   
			FillUnassignedLeads(column + " " + Session["SortDirection"].ToString(),false);

		}


		private void DisplayConsultantLeads(int consultantID){
			DisplayConsultantLeads(consultantID,"");
		}

		private void DisplayConsultantLeads(int consultantID, string sortOrder){
			try{
				txtUncalled.Text = "0";
				lblNoLeads.Visible = false;
			
			
				if (!((Helper.IsDate(txtStartDateAssigned.Text) || txtStartDateAssigned.Text.Trim() == "") &&
					(Helper.IsDate(txtEndDateAssigned.Text) || txtEndDateAssigned.Text.Trim() == "") &&
					(Helper.IsDate(txtStartAssDate.Text) || txtStartAssDate.Text.Trim() == "") &&
					(Helper.IsDate(txtEndAssDate.Text) || txtEndAssDate.Text.Trim() == ""))){
				
					lblError.Visible = true;
					lblError.Text = "Error with dates selected!";
				}else{	
			    
					int nbLeads;
					string entryStartDate = txtStartDateAssigned.Text;
					string entryEndDate = txtEndDateAssigned.Text;
					string assignStartDate = txtStartAssDate.Text;
					string assignEndDate = txtEndAssDate.Text;

        
					DataTable dt = DatabaseObjects.GetConsultantLeads(consultantID, entryStartDate, entryEndDate, assignStartDate, assignEndDate);
					DataView dv = new DataView(dt);
					dv.Sort = sortOrder;
					dgAssigned.DataSource = dv; 
					dgAssigned.DataBind();
		
					nbLeads = dt.Rows.Count;
					lblNbLeads.Text = ": " + nbLeads;

					if (nbLeads == 0){
						lblNoLeads.Visible = true;
					}else{
						lblNoLeads.Visible = false;
					}

					int uncalledLead = 0;
					for (int i=0; i < nbLeads; i++) {
						if (dgAssigned.Items[i].Cells[3].Text == "No"){
							uncalledLead++;
						}
					}
					txtUncalled.Text = uncalledLead.ToString();
				}
			}catch(Exception ex){
				throw new Global.CRMException("",ex,0,"LeadDispatcher.DisplayConsultantLeads");
			}
		
	    }

		private void SetSortDirection(System.Web.UI.WebControls.DataGridSortCommandEventArgs e){
		
		}

		private void cmdCalStart_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
			MyCalendar.Visible = true;
			if (GA.BDC.Core.EnterpriseComponents.Helper.IsDate(txtStartDateUnassigned.Text)){
				MyCalendar.setDate(Convert.ToDateTime(txtStartDateUnassigned.Text));
			}
			Session[Global.SessionVariables.CURRENTDATETEXTBOX] = "txtStartDateUnassigned";
		}
		
		private void Cal_SelectionChanged(object sender, System.EventArgs e) {

			TextBox myTextBox = (TextBox) this.FindControl(Session[Global.SessionVariables.CURRENTDATETEXTBOX].ToString());
			myTextBox.Text = MyCalendar.Cal.SelectedDate.ToShortDateString();
			MyCalendar.Visible = false;
		}

	
		private void cmdCalEnd_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
			MyCalendar.Visible = true;
			if (GA.BDC.Core.EnterpriseComponents.Helper.IsDate(txtEndDateUnassigned.Text)){
				MyCalendar.setDate(Convert.ToDateTime(txtEndDateUnassigned.Text));
			}
			Session[Global.SessionVariables.CURRENTDATETEXTBOX] = "txtEndDateUnassigned";
		}

		private void CalAssign_SelectionChanged(object sender, System.EventArgs e) {

			TextBox myTextBox = (TextBox) this.FindControl(Session[Global.SessionVariables.CURRENTDATETEXTBOX].ToString());
			myTextBox.Text = MyCalendarAssign.Cal.SelectedDate.ToShortDateString();
			MyCalendarAssign.Visible = false;
		}

		private void Button1_Click(object sender, System.EventArgs e) {
		
			DataView dv = new DataView((DataTable) Session[Global.SessionVariables.DVUNASSIGNEDLEADS]);
		

		    dgUnassigned.DataSource = dv;
			dgUnassigned.DataBind();
		}

		private void dgUnassigned_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e) {
          // if (e.Item.Cells[0].Text.Trim().ToLower() == "ca"){		
		//		e.Item.Cells[2].Attributes.Add("style", "background-image: url('images/can8.gif')");
	//		}


		}

		protected void cboConsultants_SelectedIndexChanged(object sender, System.EventArgs e) {
			if (cboConsultants.SelectedIndex.ToString() == "0" ){   
				DisplayConsultantLeads(0);
			}else{
				DisplayConsultantLeads(Convert.ToInt32(cboConsultants.SelectedValue));
			}
		}

		private void Imagebutton2_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
			
			

			
		}

		private void calStartAssigned_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
			MyCalendarAssign.Visible = true;
			if (GA.BDC.Core.EnterpriseComponents.Helper.IsDate(txtStartDateAssigned.Text)){
				MyCalendarAssign.setDate(Convert.ToDateTime(txtStartDateAssigned.Text));
			}
			Session[Global.SessionVariables.CURRENTDATETEXTBOX] = "txtStartDateAssigned";
		}

		private void calEndAssigned_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
			MyCalendarAssign.Visible = true;
			if (GA.BDC.Core.EnterpriseComponents.Helper.IsDate(txtEndDateAssigned.Text)){
				MyCalendarAssign.setDate(Convert.ToDateTime(txtEndDateAssigned.Text));
			}
			Session[Global.SessionVariables.CURRENTDATETEXTBOX] = "txtEndDateAssigned";
		}

		private void Imagebutton6_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
	
		}

		private void cmdFilterAssigned_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
			
			if (Helper.IsNumeric(cboConsultants.SelectedValue)) {
				DisplayConsultantLeads(Convert.ToInt32(cboConsultants.SelectedValue));
			}
		
		}


		private void ImageButton3_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
		   FillUnassignedLeads("", false);
		}

		private void cmdAssign_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
			try{
				lblError.Visible = false;
			

				if (cboConsultants.SelectedIndex.ToString() == "0" ){   
					lblError.Visible = true;
					lblError.Text = "**You must select a consultant";
				}else{

					int consultantID = Convert.ToInt32(cboConsultants.SelectedValue);
					//iterate throught the select leads
					int nbSelected = 0;
					
					for (int i=0; i < dgUnassigned.Items.Count; i++) {

						// Obtain references to row's controls
						DropDownList cboKit = (DropDownList) dgUnassigned.Items[i].FindControl("cboKit");
						CheckBox chkRemove = (CheckBox) dgUnassigned.Items[i].FindControl("Select");

				
						if (chkRemove.Checked){
							nbSelected++;
					
							int leadID = Convert.ToInt32(dgUnassigned.Items[i].Cells[3].Text.Trim());
							int kitID = Convert.ToInt32(cboKit.SelectedValue); 
							if (kitID == 0){
								lblError.Visible = true;
								lblError.Text = "**You must select a kit type for lead  " + leadID.ToString();
							}else{
                           
								int success = DatabaseObjects.InsertFirstCall(leadID);
								success = DatabaseObjects.AssignLead(leadID, consultantID, Convert.ToInt32(Session[Global.SessionVariables.USER_ID]), kitID, false);
						
							}
					
						}
					
					}
					
					if (nbSelected == 0){
						lblError.Visible = true;
						lblError.Text = "**No leads were selected!";
					}else if (lblError.Visible == false){
						Refresh();
			
					}


				}
			}catch(Exception ex){
				throw new Global.CRMException("",ex,0,"LeadDispatcher.cmdAssign_Click");
			}
		
		}

		public void Refresh(){
			FillUnassignedLeads(); 
			if (Helper.IsNumeric(cboConsultants.SelectedValue)){
			   DisplayConsultantLeads(Convert.ToInt32(cboConsultants.SelectedValue));
			}else{
			   DisplayConsultantLeads(-1);
			}
			
		}
		
		private void ImageButton4_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
		   FillUnassignedLeads(); 
		}

		private void cmdUnassign_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
			try{
				if (cboConsultants.SelectedIndex.ToString() == "0" ){   
					lblError.Visible = true;
					lblError.Text = "**You must select a consultant";
				}else{
					int consultantID = Convert.ToInt32(cboConsultants.SelectedValue);
					int user_id =  Convert.ToInt32(Session[Global.SessionVariables.USER_ID]);
			

					//iterate throught the select leads
					int nbSelected = 0;
					
					for (int i=0; i < dgAssigned.Items.Count; i++) {

						CheckBox chk = (CheckBox) dgAssigned.Items[i].FindControl("Select2");

						if (chk.Checked){
							nbSelected++; 
							int leadID = Convert.ToInt32(dgAssigned.Items[i].Cells[1].Text.Trim());
							int success = DatabaseObjects.UnassignLead(leadID,consultantID,user_id);	
						}
					}
					if (nbSelected == 0){
						lblError.Visible = true;
						lblError.Text = "**No leads were selected!";
					}else{
						Refresh();

					}
				}

			}catch(Exception ex){
				throw new Global.CRMException("",ex,0,"LeadDispatcher.cmdUnassign_Click");
			}


		}

		private void dgAssigned_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e) {
			string [] split = null;
			split = e.SortExpression.ToString().Split(' ');

			string column = split[0].ToString();
			//string order = split[1].ToString();
			
			if (Session["SortDirection"].ToString() == "asc"){
				Session["SortDirection"] = "desc";
			}else{
				Session["SortDirection"] = "asc";
			}   
	
			if (Helper.IsNumeric(cboConsultants.SelectedValue)) {
				DisplayConsultantLeads(Convert.ToInt32(cboConsultants.SelectedValue), column + " " + Session["SortDirection"].ToString() );
			}

		}

		private void dgUnassigned_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e) {
	

			if (GA.BDC.Core.EnterpriseComponents.Helper.IsNumeric(e.Item.Cells[3].Text)) {
			
				int leadID = Convert.ToInt32(e.Item.Cells[3].Text.Trim());
				Session[Global.SessionVariables.CURRENT_LEAD_ID] = leadID;
				
				ChangeTab(this,e);
	
			}
		}

		private void dgAssigned_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e) {
			if (GA.BDC.Core.EnterpriseComponents.Helper.IsNumeric(e.Item.Cells[1].Text)) {
			
				int leadID = Convert.ToInt32(e.Item.Cells[1].Text.Trim());
				Session[Global.SessionVariables.CURRENT_LEAD_ID] = leadID;
				
				
				ChangeTab(this,e);
	
			}
		}

		private void CalStartAssDate_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
			MyCalendarAssign.Visible = true;
			if (GA.BDC.Core.EnterpriseComponents.Helper.IsDate(txtStartAssDate.Text)){
				MyCalendarAssign.setDate(Convert.ToDateTime(txtStartAssDate.Text));
			}
			Session[Global.SessionVariables.CURRENTDATETEXTBOX] = "txtStartAssDate";
		}

		private void calEndAssDate_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
			MyCalendarAssign.Visible = true;
			if (GA.BDC.Core.EnterpriseComponents.Helper.IsDate(txtEndAssDate.Text)){
				MyCalendarAssign.setDate(Convert.ToDateTime(txtEndAssDate.Text));
			}
			Session[Global.SessionVariables.CURRENTDATETEXTBOX] = "txtEndAssDate";
		}

		protected void cboPromoGroup_SelectedIndexChanged(object sender, System.EventArgs e) {
	    	FillUnassignedLeads("", true);
		}

		
		
	
	
	}

}
