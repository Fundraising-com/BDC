namespace QSPFulfillment.CommonWeb.UC
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>Email Addr Entry Control class</summary>
	public partial  class Email : System.Web.UI.UserControl
	{
		#region auto-generated code
		///<summary>Required method for Designer support</summary>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		///<summary>Required method for Designer support</summary>
		private void InitializeComponent()
		{
			this.Init += new System.EventHandler(this.OnInit_Handler);
		}
		#endregion auto-generated code

		#region Item Declarations
		#endregion Item Declarations

		#region Control properties
		
			#region Enabled
		private bool _Enabled;
		private bool _Enabled_called;

		///<summary>Gets or Sets a value indicating whether the Web server control is enabled.</summary>
		public bool Enabled
		{
			get
			{
				return _Enabled;
			}
			set
			{
				_Enabled = value;
				_Enabled_called = true;
				tb_EMAIL.Enabled	= _Enabled;
				reg_email.Enabled	= _Enabled;
				if(_Enabled == true)
				{
					tb_EMAIL.BackColor	= System.Drawing.Color.Transparent;
				}
				else
				{
					tb_EMAIL.BackColor	= System.Drawing.Color.LightGray;
				}
				RequiredOrEnabled_ValueChanged();
			}
		}
		#endregion

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
				rq_email.Enabled = true;
			}
			else
			{
				rq_email.Enabled = false;
			}
		}
		#endregion

			#region Text
		public string Text
		{
			get { return this.tb_EMAIL.Text; }
			set { this.tb_EMAIL.Text = value; }
		}
		#endregion

			#region Readonly
		private bool _ReadonlyM;
		
		///<summary>Gets or Sets a value indicating whether the contents of this QSPFulfillment.CommonWeb.UC.Email control can be changed.</summary>
		public bool Readonly
		{
			get 
			{
				return _ReadonlyM;
			}
			set
			{
				_ReadonlyM = value;
				this.tb_EMAIL.ReadOnly = _ReadonlyM;				
			}
		}
		#endregion Readonly

			#region RequiredValErrMsg
		private string _RequiredValErrMsg;
		private bool _RequiredValErrMsg_called;

		///<summary>Gets or Sets a value indicating the error message for the required field validator.</summary>
		public string RequiredValErrMsg
		{
			get
			{
				return _RequiredValErrMsg;
			}
			set
			{
				_RequiredValErrMsg        = value;
				_RequiredValErrMsg_called = true;	
				rq_email.ErrorMessage     = _RequiredValErrMsg;
			}
		}
		#endregion RequiredValErrMsg

			#region Columns 
		///<summary>Gets or sets the display width of the Email control in characters.</summary>
		public int Columns
		{
			get { return this.tb_EMAIL.Columns;  }
			set { this.tb_EMAIL.Columns = value; }
		}
		#endregion Columns

		#endregion Control properties

		#region Page_Load, OnInIt_Handler
		protected void Page_Load(object s, EventArgs e)
		{
		}

		protected void OnInit_Handler(object s, EventArgs e)
		{
			if (this._Enabled_called == false)
			{
				this.Enabled = true;
			}
			if (this._RequiredValErrMsg_called == false)
			{
				this.RequiredValErrMsg = "Please enter a valid email address.";
			}
		}
		#endregion Page_Load, OnInIt_Handler
	}
}
