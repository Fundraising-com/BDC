namespace CRMWeb.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for AllFMForLead.
	/// </summary>
	public partial class AllAccountsForLead_Lotus : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label lblFSMID;
		protected System.Web.UI.WebControls.Label lblW;
		protected System.Web.UI.WebControls.Label lblMobi;
		protected System.Web.UI.WebControls.Label lblHome;
		protected System.Web.UI.WebControls.Label lblWork;
		protected System.Web.UI.WebControls.Label lblMobile;
		protected System.Web.UI.WebControls.Label lblName;
		protected System.Web.UI.WebControls.Label lblEmail;
		protected System.Web.UI.WebControls.Label lblAcctNo;
		protected System.Web.UI.WebControls.Label lblAcctName;
		protected System.Web.UI.WebControls.Label Label24;
		protected System.Web.UI.WebControls.Label Label25;
		protected System.Web.UI.WebControls.Label Label26;
		protected System.Web.UI.WebControls.Label Label27;
		protected System.Web.UI.WebControls.Label Label28;
		protected System.Web.UI.WebControls.Label Label29;
		protected System.Web.UI.WebControls.Label Label30;
		protected System.Web.UI.WebControls.Label Label31;
		protected System.Web.UI.WebControls.Label Label32;
		protected System.Web.UI.WebControls.Label Label33;
		protected System.Web.UI.WebControls.Label Label34;
		protected System.Web.UI.WebControls.Label Label35;
		protected System.Web.UI.WebControls.Label Label36;
		protected System.Web.UI.WebControls.Label Label37;
		protected System.Web.UI.WebControls.Label Label38;
		protected System.Web.UI.WebControls.Label Label39;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.Label Label16;

		protected void Page_Load(object sender, System.EventArgs e)
		{
		  // GenerateControl(516605);
		}

		       
		
		public void GenerateControl(int leadID){
			
			DataTable dt = DatabaseObjects.GetAllAccountsForLead(leadID);
			DataView myDV = new DataView(dt);
		
			myRepeaterAltSep.DataSource = dt;
			myRepeaterAltSep.DataBind();
		
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
			this.myRepeaterAltSep.ItemCommand += new System.Web.UI.WebControls.RepeaterCommandEventHandler(this.myRepeaterAltSep_ItemCommand);

		}
		#endregion

		private void myRepeaterAltSep_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e) {
		
		}
	}
}
