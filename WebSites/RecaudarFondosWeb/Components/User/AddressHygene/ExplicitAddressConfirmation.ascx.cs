namespace efundraising.RecaudarFondosWeb.Components.User.AddressHygiene
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for ExplicitAddressConfirmation.
	/// </summary>
	public class ExplicitAddressConfirmation : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Literal AddressLiteral;
        public event System.EventHandler OnConfirm;
        public event System.EventHandler OnCancel;
        public event System.EventHandler OnSaveWithoutChange;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		public void SetAddress(string address) {
			AddressLiteral.Text = address;
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

        protected void ConfirmButton_Click(object sender, System.EventArgs e)
        {
            if (OnConfirm != null)
            {
                OnConfirm(sender, e);
            }
        }

        protected void SaveWithoutChangeButton_Click(object sender, System.EventArgs e)
        {
            if (OnSaveWithoutChange != null)
            {
                OnSaveWithoutChange(sender, e);
            }
        }

        protected void CancelButton_Click(object sender, System.EventArgs e)
        {
            if (OnCancel != null)
            {
                OnCancel(sender, e);
            }
        }
	}
}
