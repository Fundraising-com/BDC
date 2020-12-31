namespace QSPFulfillment.AcctMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.ComponentModel;

	/// <summary>
	///		Summary description for AccountIDPickerControl.
	/// </summary>
	public partial class AccountIDPickerControl : AcctMgtControl
	{
		private const string HYL_ENABLED_IMAGE_URL = "../images/find.gif";
		private const string HYL_DISABLED_IMAGE_URL = "../images/find_disabled.gif";
		private const string HYL_NAVIGATE_URL_PREFIX = "javascript: OpenBig(\"AccountList.aspx?IsNewWindow=true&";


		protected void Page_Load(object sender, System.EventArgs e)
		{

		}

		protected void AccountIDPickerControl_PreRender(object sender, EventArgs e)
		{
			AddJavaScript();
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
			this.PreRender += new System.EventHandler(this.AccountIDPickerControl_PreRender);

		}
		#endregion

		protected override void AddJavaScript()
		{
			base.AddJavaScript ();

			this.hylPicker.NavigateUrl = HYL_NAVIGATE_URL_PREFIX + "&ParentControlName=" + this.tbxAccountID.ClientID + "&AccountIDSearch=\" + document.getElementById(\"" + this.tbxAccountID.ClientID + "\").value);";
		}

		[Bindable(true),Category("Appearance"),	DefaultValue(false)] 
		public bool Enabled 
		{
			get 
			{
				return !this.tbxAccountID.ReadOnly;
			}
			set 
			{
				this.tbxAccountID.ReadOnly = !value;
				this.hylPicker.Enabled = value;

				if(value) 
				{
					this.hylPicker.ImageUrl = HYL_ENABLED_IMAGE_URL;
				} 
				else 
				{
					this.hylPicker.ImageUrl = HYL_DISABLED_IMAGE_URL;
				}
			}
		}

		[Bindable(true),Category("Appearance"),	DefaultValue(false)] 
		public bool Required 
		{
			get 
			{
				return this.tbxAccountID.Required;
			}
			set 
			{
				this.tbxAccountID.Required = value;
			}
		}

		[Bindable(true),Category("Appearance"),	DefaultValue(true)] 
		public bool EnableClientScript 
		{
			get 
			{
				return this.tbxAccountID.ClientScript;
			}
			set 
			{
				this.tbxAccountID.ClientScript = value;
			}
		}

		public int AccountID
		{
			get 
			{
				int iAccountID = -1;

				try 
				{
					iAccountID = Convert.ToInt32(this.tbxAccountID.Text);
				} 
				catch { }

				return iAccountID;
			}
			set 
			{
				this.tbxAccountID.Text = value.ToString();
			}
		}
	}
}
