namespace CRMWeb.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for Admin_Section.
	/// </summary>
	public partial class Admin_Section : System.Web.UI.UserControl
	{    


		public UserControls.Admin_Menu Admin_Menu1;
		protected UserControls.Lead.PromotionGroups PromotionGroups1;
        protected UserControls.zzz Zzz1;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);	
			Admin_Menu1.TabStripAdmin.SelectedIndexChange += new System.EventHandler(this.TabStripAdmin_SelectedIndexChange);
		
			
		
	}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion


		
		private void SetInvisible(){
		   PromotionGroups1.Visible = false;
		   //Admin_All_Accounts1.Visible = false;
			Zzz1.Visible = false;
		}

		public void Refresh(){
			PromotionGroups1.Refresh();
		}

		public void Refresh(int leadID, bool process){
			if (process){
			   Classes.BusinessProcess bp = new Classes.BusinessProcess();
			   bp.ProcessFlagpoles(leadID);
			}
			
			Zzz1.Refresh(leadID);
		}


		public void TabStripAdmin_SelectedIndexChange(object sender, System.EventArgs e) {

			//if its a user for lead accounts only, we send him back
		/*	if (Session[Global.SessionVariables.ACCESS_LEAD_ACCOUNTS_ONLY].ToString() == "true" &&
				Convert.ToInt32(Admin_Menu1.TabStripAdmin.SelectedIndex) != 1){
				Admin_Menu1.TabStripAdmin.SelectedIndex = 1;
				TabStripAdmin_SelectedIndexChange(sender, e);
			}else{
		*/	
				SetInvisible();
		
				switch(Admin_Menu1.TabStripAdmin.SelectedIndex){         
					case 0:  
						PromotionGroups1.Visible = true;
						break;                  
					case 1:
						//Admin_All_Accounts21.Visible = true;
						Zzz1.Visible = true;
						break;
				
		
				}
			//}
		}
	}
}
