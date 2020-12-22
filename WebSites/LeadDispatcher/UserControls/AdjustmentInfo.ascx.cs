namespace CRMWeb.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Diagnostics;
	using GA.BDC.Core.EnterpriseComponents;

	/// <summary>
	///		Summary description for AdjustmentInfo.
	/// </summary>
	public partial class AdjustmentInfo : System.Web.UI.UserControl
	{
        protected UserControls.MyCalendar MyCalendar1;


		protected void Page_Load(object sender, System.EventArgs e)
		{
		
			if (!IsPostBack){
				//RefreshData(); 

				cboReason.DataSource = DatabaseObjects.GetAdjustmentReasons();
				cboReason.DataTextField = "Description";
				cboReason.DataValueField = "reason_id";
				cboReason.DataBind();
				cboReason.Items.Add("");
			

			}
			
			
			txtAdjNo.Visible = false;
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
		
			
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.dgAdjustment.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgAdjustment_ItemCommand);
			this.ImageButton2.Click += new System.Web.UI.ImageClickEventHandler(this.ImageButton2_Click);
			this.calAdjustment.Click += new System.Web.UI.ImageClickEventHandler(this.calPayment_Click);
			this.cmdAdd.Click += new System.Web.UI.ImageClickEventHandler(this.cmdAdd_Click);
			this.cmdCancel.Click += new System.Web.UI.ImageClickEventHandler(this.cmdCancel_Click);

		}
		#endregion

		public void Refresh(){
			Classes.SaleInfo si = (Classes.SaleInfo) Session[Global.SessionVariables.SALE_INFO];
			if (si != null){
				DataTable dt = DatabaseObjects.GetAdjustments(si.SALE_ID);
				dgAdjustment.DataSource = dt;
				dgAdjustment.DataBind();
			}
		}


		private void dgAdjustment_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e) {
			lblAdd.Visible = true;
			Panel1.Visible = true;
			cmdAdd.Visible = false;
			cmdUpdate.Visible = true;
			
			int adjustmentID = 0;
			if (e.CommandName == "Edit"){
				int index = e.Item.ItemIndex;
				adjustmentID = Convert.ToInt32(dgAdjustment.Items[index].Cells[0].Text);
				txtAdjNo.Text = adjustmentID.ToString();
			}

			//getAdjinfo
			Classes.SaleInfo si = (Classes.SaleInfo) Session[Global.SessionVariables.SALE_INFO];
	
	//		DataTable dt = DatabaseObjects.GetSpecificPayment(si.SALE_ID, paymentID);
		//	if (dt.Rows.Count > 0){
			//}
			
		}


		private void ImageButton2_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
			lblAdd.Visible = true;
			Panel1.Visible = true;
		//	ClearFields();
			cmdAdd.Visible = true;
			cmdUpdate.Visible = false;
		}

		private void cmdCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
			lblAdd.Visible = false;
			Panel1.Visible = false;
			MyCalendar1.Visible = false; 
		}

		private void cmdAdd_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
			CreateAdjustment(true);
			Refresh();
		}

		private void CreateAdjustment(bool newAdj){

			try{
				Classes.SaleInfo si = (Classes.SaleInfo) Session[Global.SessionVariables.SALE_INFO];

				//get last payment
				int col = dgAdjustment.Items.Count;
				int adjustmentNo = 0;
				
				if (newAdj){
					if (dgAdjustment.Items.Count == 0){
                       adjustmentNo = 1;
					}else{
					   adjustmentNo =  Convert.ToInt32(dgAdjustment.Items[col -1].Cells[0].Text) + 1;
					}
					
				}else{
					adjustmentNo = Convert.ToInt32(txtAdjNo.Text);
				}

				int reasonID  = 0;
				if (cboReason.SelectedValue != ""){
					reasonID = Convert.ToInt32(cboReason.SelectedValue);
				}

			
				decimal shippingAmt = 0;
				if (Helper.IsNumeric(txtShippingAmt.Text)){
                   shippingAmt = Convert.ToDecimal(txtShippingAmt.Text);
				}

				DateTime adjDate;
				if (txtAdjDate.Text != "" && GA.BDC.Core.EnterpriseComponents.Helper.IsNumeric(txtAmount.Text)){
					adjDate = Convert.ToDateTime(txtAdjDate.Text);
					    
						DatabaseObjects.Insert_Update_Adjustment ( Convert.ToInt32(si.SALE_ID),
							adjustmentNo,
							Convert.ToInt32(cboReason.SelectedValue),
							adjDate,
							Convert.ToDecimal(txtAmount.Text),
							shippingAmt,
							Convert.ToDecimal(txtAmount.Text) + shippingAmt,
							txtComments.Text,
							newAdj);
			
			
					}

				
			}catch(Exception ex ){
				Debug.Write(ex.Message.ToString());
			}
	
			//		DatabaseObjects.InsertPayment(1000,5,1,1,Convert.To ime("07-07-07"),Convert.ToDateTime("06-06-06"),
			//			"txt","txt","xtNam","tx",1,1);

		}

		private void calPayment_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
				MyCalendar1.Visible = true;
				if (GA.BDC.Core.EnterpriseComponents.Helper.IsDate(txtAdjDate.Text)){
					MyCalendar1.setDate(Convert.ToDateTime(txtAdjDate.Text));
				}
				Session["CurrentDateTextBox"] = "txtAdjDate";
			
		}


		private void Cal_SelectionChanged(object sender, System.EventArgs e) {

			TextBox myTextBox = (TextBox) this.FindControl(Session["CurrentDateTextBox"].ToString());
			myTextBox.Text = MyCalendar1.Cal.SelectedDate.ToString();
			MyCalendar1.Visible = false;


		}
	
	}
}
