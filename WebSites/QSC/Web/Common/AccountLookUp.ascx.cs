namespace QSPFulfillment.CommonWeb.UC
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

/// <summary>Account Lookup/Popup Control class</summary>
	public partial class AccountLookUp : System.Web.UI.UserControl
	{
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		private void InitializeComponent()
		{
			this.Init += new System.EventHandler(this.OnInit_Handler);
		}
		#endregion Web Form Designer generated code

		#region Item Declarations
		#endregion Item Declarations

		#region Control properties
		
		#region Enabled
		private bool _Enabled;
		private bool _Enabled_called;

		///<summary>Gets or Sets a value indicating whether the Web server control is enabled.</summary>
		public bool Enabled
		{
			get { return _Enabled; }
			set
			{
				_Enabled = value;
				_Enabled_called = true;
				tbAccount.Enabled		= _Enabled;
				regAccount.Enabled		= _Enabled;
				if(_Enabled == true)
				{
					tbAccount.BackColor	= System.Drawing.Color.Transparent;
				}
				else
				{
					tbAccount.BackColor	= System.Drawing.Color.LightGray;
				}
				RequiredOrEnabled_ValueChanged();
			}
		}
		#endregion Enabled

		#region Required
		private bool _Required;
		
		///<summary>Gets or Sets a value indicating whether the Web server control will be subject to required field validation.</summary>
		public bool Required
		{
			get 
			{
				return _Required;
			}
			set
			{
				_Required = value;
				RequiredOrEnabled_ValueChanged();
			}
		}
		private void RequiredOrEnabled_ValueChanged()
		{
			if((_Required == true)&&(_Enabled == true))
			{
				rqAccount.Enabled = true;
			}
			else
			{
				rqAccount.Enabled = false;
			}
		}
		#endregion Required

		#region Text
		///<summary>Gets or sets the Text value of this field.</summary>
		public string Text
		{
			get	{ return this.tbAccount.Text.Trim();  }
			set	{ this.tbAccount.Text = value.Trim(); }
		}
		#endregion Text

		#region AccountID
		///<summary>Gets or sets the numeric value of this account field.</summary>
		public int AccountID
		{
			get
			{ 
				try { return Convert.ToInt32(this.tbAccount.Text.Trim()); }
				catch { return -5; } 
			}
			set	{ this.tbAccount.Text = value.ToString(); }
		}
		#endregion AccountID

		#region LinkText
		///<summary>Gets or sets the text for the account search link.</summary>
		private bool _LinkText_called = false;
		public string LinkText
		{
			get
			{ 
				return this.ahrefPopUp.InnerText;
			}
			set	
			{ 
				this.ahrefPopUp.InnerText = value;
				_LinkText_called = true;
			}
		}
		#endregion LinkText

		#endregion Control properties

		#region Page_Load, OnInit_Handler
		protected void Page_Load(object s, EventArgs e)
		{
		}

		protected void OnInit_Handler(object s, EventArgs e)
		{
			if (this._Enabled_called == false)
			{
				this.Enabled = true;
			}
			if (this._LinkText_called == false)
			{
				this.LinkText = "Search";
			}
		}
		#endregion Page_Load, OnInit_Handler
	}
}
