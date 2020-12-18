namespace CRMWeb.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using GA.BDC.Core.EnterpriseComponents;

	/// <summary>
	///		Summary description for Login.
	/// </summary>
	public partial class zzz : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		public System.Web.UI.WebControls.ImageButton cmdLogin;
		public System.Web.UI.WebControls.Label lblError;
		public System.Web.UI.WebControls.TextBox txtUserName;
		public System.Web.UI.WebControls.TextBox txtPassword;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected UserControls.AllAccountsForLead AllAccountsForLead1; 

		protected void Page_Load(object sender, System.EventArgs e)
		{

			// Put user code to initialize the page here

			Helper.SetOnKeyPressBehavior(this.txtLeadID, this.cmdGo.ClientID);
			
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.cmdGo.Click += new System.Web.UI.ImageClickEventHandler(this.cmdGo_Click);

		}
		#endregion

		private void txtUserName_TextChanged(object sender, System.EventArgs e) {
		
		}

		public void Refresh(int leadID){
		
			txtLeadID.Text = leadID.ToString();
			GenerateData();
		}
		
		
		private void cmdGo_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
			GenerateData();		
		
		}

		private void GenerateData(){
			lblNoAccounts.Visible = false;
			if (Helper.IsNumeric(txtLeadID.Text)){
				int nbRows = AllAccountsForLead1.GenerateControl(Convert.ToInt32(txtLeadID.Text));
				if (nbRows == 0){
					lblNoAccounts.Visible = true;
				}
			}else{
				lblNoAccounts.Visible = true;
			}
		}

		

		

	

		
	}
}
