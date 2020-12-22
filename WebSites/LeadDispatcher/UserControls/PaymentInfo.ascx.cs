namespace CRMWeb.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Diagnostics;
	

	/// <summary>
	///		Summary description for PaymentInfo.
	/// </summary>
	public partial class PaymentInfo : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.ImageButton ImageButton1;
		protected UserControls.PaymentAdjusment_Header PaymentAdjusment_Header;
		protected UserControls.MyCalendar MyCalendar1;

		protected void Page_Load(object sender, System.EventArgs e)
		{
				
			//}
				// Put user code to initialize the page here
			
			    Classes.SaleInfo si = (Classes.SaleInfo) Session[Global.SessionVariables.SALE_INFO];

			if (!IsPostBack && si != null){
					


				cboCollectionStatus.DataSource = DatabaseObjects.GetCollectionStatus();
				cboCollectionStatus.DataTextField = "Description";
				cboCollectionStatus.DataValueField = "collection_status_id";
				cboCollectionStatus.DataBind();
				cboCollectionStatus.Items.Add("");
 
				cboPaymentMethod.DataSource = DatabaseObjects.GetPaymentMethods();
				cboPaymentMethod.DataTextField = "description";
				cboPaymentMethod.DataValueField = "payment_method_id";
				cboPaymentMethod.DataBind();
			}

            
			//	ImageButton cmd = (ImageButton) dgPayments.Items[1].FindControl("Edit");
			//	cmd.Click += new System.Web.UI.ImageClickEventHandler(this.ImageButton_Click);
			
			 
			
		}

	
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);

			MyCalendar1.Cal.SelectionChanged += new System.EventHandler(this.Cal_SelectionChanged);
			txtPaymentNo.Visible = false;

           

		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.dgPayments.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgPayments_ItemCommand);
			this.ImageButton2.Click += new System.Web.UI.ImageClickEventHandler(this.ImageButton2_Click);
			this.calPayment.Click += new System.Web.UI.ImageClickEventHandler(this.calPayment_Click);
			this.calExp.Click += new System.Web.UI.ImageClickEventHandler(this.calExp_Click);
			this.calCashable.Click += new System.Web.UI.ImageClickEventHandler(this.ImageButton5_Click);
			this.cmdUpdate.Click += new System.Web.UI.ImageClickEventHandler(this.ImageButton6_Click);
			this.cmdAdd.Click += new System.Web.UI.ImageClickEventHandler(this.cmdAdd_Click);
			this.cmdCancel.Click += new System.Web.UI.ImageClickEventHandler(this.cmdCancel_Click);

		}
		#endregion

		public void Refresh(){
			Classes.SaleInfo si = (Classes.SaleInfo) Session[Global.SessionVariables.SALE_INFO];

			if (si != null){
				DataTable dt = DatabaseObjects.GetPayments(si.SALE_ID);
				dgPayments.DataSource = dt;
				dgPayments.DataBind();
			}
		}

		private void dgPayments_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e) {
			lblAdd.Visible = true;
			Panel1.Visible = true;
			cmdAdd.Visible = false;
			cmdUpdate.Visible = true;
			
			int paymentID = 0;
			if (e.CommandName == "Edit"){
			   int index = e.Item.ItemIndex;
			
			   paymentID = Convert.ToInt32(dgPayments.Items[index].Cells[0].Text);
			   txtPaymentNo.Text = paymentID.ToString();
			}

			//getPaymentinfo
			Classes.SaleInfo si = (Classes.SaleInfo) Session[Global.SessionVariables.SALE_INFO];
	
			DataTable dt = DatabaseObjects.GetSpecificPayment(si.SALE_ID, paymentID);
			if (dt.Rows.Count > 0){
				txtAmount.Text = dt.Rows[0]["payment_amount"].ToString();
				txtAuthNo.Text = dt.Rows[0]["authorization_number"].ToString();
				txtCashableDate.Text = dt.Rows[0]["cashable_date"].ToString();
				txtCreditCard.Text = dt.Rows[0]["credit_card_no"].ToString();
				txtExpDate.Text = dt.Rows[0]["expiry_date"].ToString();
				txtNameOnCard.Text = dt.Rows[0]["name_on_card"].ToString();

				
				txtPaymentDate.Text = dt.Rows[0]["payment_entry_date"].ToString();
				if (dt.Rows[0]["collection_status"] == DBNull.Value){
				   cboCollectionStatus.SelectedValue = "";
				}else{
				   cboCollectionStatus.SelectedItem.Text = dt.Rows[0]["collection_status"].ToString();
				}
				
			//	Debug.Write(dt.Rows[0]["payment_method"].ToString());
				
		        cboPaymentMethod.SelectedItem.Text = dt.Rows[0]["payment_method"].ToString();
				
                if (Convert.ToBoolean(dt.Rows[0]["commission_paid"])){
					chkCommission.Checked = true;
				}else{
                    chkCommission.Checked = false;
				}

			}


		}

	/*	private void ImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
			Debug.Write(e.ToString());

		}*/

		private void Button1_Click(object sender, System.EventArgs e) {
		    Debug.Write(e.ToString());
		}

		private void ImageButton2_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
			lblAdd.Visible = true;
			Panel1.Visible = true;
			ClearFields();
			cmdAdd.Visible = true;
			cmdUpdate.Visible = false;
		}

		private void ImageButton6_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
	       CreatePayment(false);
			
	
		}

		private void Cal_SelectionChanged(object sender, System.EventArgs e) {

			TextBox myTextBox = (TextBox) this.FindControl(Session["CurrentDateTextBox"].ToString());
 			myTextBox.Text = MyCalendar1.Cal.SelectedDate.ToString();
			MyCalendar1.Visible = false;


		}

		private void ImageButton5_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
			MyCalendar1.Visible = true;
			if (GA.BDC.Core.EnterpriseComponents.Helper.IsDate(txtCashableDate.Text)){
			   MyCalendar1.setDate(Convert.ToDateTime(txtCashableDate.Text));
			}
			
			Session["CurrentDateTextBox"] = "txtCashableDate";
			
		}

		private void calPayment_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
			MyCalendar1.Visible = true;
			if (GA.BDC.Core.EnterpriseComponents.Helper.IsDate(txtPaymentDate.Text)){
				MyCalendar1.setDate(Convert.ToDateTime(txtPaymentDate.Text));
			}
			Session["CurrentDateTextBox"] = "txtPaymentDate";
			
           
		}

		private void calExp_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
			MyCalendar1.Visible = true;
			if (GA.BDC.Core.EnterpriseComponents.Helper.IsDate(txtExpDate.Text)){
				MyCalendar1.setDate(Convert.ToDateTime(txtExpDate.Text));
			}
			Session["CurrentDateTextBox"] = "txtExpDate";
			
		}

		private void ClearFields(){
           txtAmount.Text = "";
			txtAuthNo.Text = "";
			txtCashableDate.Text = "";
			txtCreditCard.Text = "";
			txtExpDate.Text = "";
			txtNameOnCard.Text = "";
			txtPaymentDate.Text = "";
		    cboCollectionStatus.SelectedValue = "";
			chkCommission.Checked = false;

		}

		private void cmdAdd_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
			CreatePayment(true);

		}

		private void CreatePayment(bool newPayment){

			try{
				Classes.SaleInfo si = (Classes.SaleInfo) Session[Global.SessionVariables.SALE_INFO];

				//get last payment
				int col = dgPayments.Items.Count;
                int paymentNo;
				
				if (newPayment){
					paymentNo =  Convert.ToInt32(dgPayments.Items[col -1].Cells[0].Text) + 1;
				}else{
					paymentNo = Convert.ToInt32(txtPaymentNo.Text);
				}

				int collectionStatus = 0;
				if (cboCollectionStatus.SelectedValue != ""){
					collectionStatus = Convert.ToInt32(cboCollectionStatus.SelectedValue);
				}

				DateTime cashableDate;
				DateTime paymentDate;
			
				if (txtCashableDate.Text != ""){
					cashableDate = Convert.ToDateTime(txtCashableDate.Text);

					if  (txtPaymentDate.Text != ""){
						paymentDate = Convert.ToDateTime(txtPaymentDate.Text);
            
					    
						DatabaseObjects.Insert_Update_Payment(Convert.ToInt32(si.SALE_ID),
							paymentNo,
							Convert.ToInt32(cboPaymentMethod.SelectedValue),
							collectionStatus,
							paymentDate,
							cashableDate,
							txtCreditCard.Text,
							txtExpDate.Text,
							txtNameOnCard.Text,txtAuthNo.Text, 
							Convert.ToDecimal(txtAmount.Text),
							Convert.ToInt32(chkCommission.Checked),
							newPayment);
			
			
					}

				}
			}catch(Exception ex ){
				Debug.Write(ex.Message.ToString());
			}
	
			//		DatabaseObjects.InsertPayment(1000,5,1,1,Convert.To ime("07-07-07"),Convert.ToDateTime("06-06-06"),
			//			"txt","txt","xtNam","tx",1,1);

		}

		private void cmdCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
			lblAdd.Visible = false;
			Panel1.Visible = false;
			MyCalendar1.Visible = false; 
		}
		

	



	}
}
