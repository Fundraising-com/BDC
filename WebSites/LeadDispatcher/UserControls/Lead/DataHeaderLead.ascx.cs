namespace CRMWeb.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for DataHeader.
	/// </summary>
	public partial class DataHeaderLead : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
           FillData();
       
			//#FFFEC7

		}

		public void Refresh(){
			FillData();
		}

		private void FillData(){
			//Classes.UserInfo ui = (Classes.UserInfo) Session[Global.SessionVariables.USER_INFO];
			// Put user code to initialize the page here
			Classes.LeadInfo_Basic li = (Classes.LeadInfo_Basic) Session[Global.SessionVariables.LEAD_INFO];

			if (li != null) {
				lblGroup.Text = li.GROUP_NAME.ToString();
				lblPerson.Text = li.LEAD_NAME.ToString();
				lblPhone.Text = li.PHONE.ToString();
				lblEmail.Text = li.EMAIL.ToString();
				
			}


			//if (ui != null) {
			//	lblUserName.Text = ui.NAME.ToString();
			
			//}
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

		}
		#endregion
	}
}
