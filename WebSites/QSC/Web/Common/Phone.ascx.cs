namespace QSPFulfillment.CommonWeb.UC
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>Phone # Entry Control class</summary>
	public partial  class Phone : System.Web.UI.UserControl
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
				tb_PHONE.Enabled	= _Enabled;
				reg_phone.Enabled	= _Enabled;
				if(_Enabled == true)
				{
					tb_PHONE.BackColor	= System.Drawing.Color.Transparent;
				}
				else
				{
					tb_PHONE.BackColor	= System.Drawing.Color.LightGray;
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
				rq_phone.Enabled = true;
			}
			else
			{
				rq_phone.Enabled = false;
			}
		}
		#endregion Required

			#region PhoneNumber
		public Common.PhoneNumber PhoneNumber
		{
			get
			{
				return new Common.PhoneNumber(this.tb_PHONE.Text);
			}
			set
			{
				try
				{
					if((value != null)&&(value.GetNumber != ""))
					{
						this.tb_PHONE.Text = value.GetNumber;
					}
				}
				catch
				{
					this.tb_PHONE.Text = "";
				}
			}
		}
		#endregion PhoneNumber

			#region Text
		
		///<summary>Gets or Sets the text content of the QSPFulfillment.CommonWeb.UC.Phone control</summary>
		public string Text
		{
			get { return this.tb_PHONE.Text; }
			set { this.tb_PHONE.Text = value; } 
		}
		#endregion Text

			#region Readonly
		private bool _ReadonlyM;
		
		///<summary>Gets or Sets a value indicating whether the contents of this QSPFulfillment.CommonWeb.UC.Phone control can be changed.</summary>
		public bool Readonly
		{
			get 
			{
				return _ReadonlyM;
			}
			set
			{
				_ReadonlyM = value;
				this.tb_PHONE.ReadOnly = _ReadonlyM;
			}
		}
		#endregion Readonly

			#region DisplayLabel
		private bool _DisplayLabelM;
		
		///<summary>Gets or Sets a value indicating whether the "(Please enter numbers only)" lbl should be displayed</summary>
		public bool DisplayLabel
		{
			get 
			{
				return _DisplayLabelM;
			}
			set
			{
				_DisplayLabelM = value;
				this.txt_phone.Visible = _DisplayLabelM;
			}
		}
		#endregion DisplayLabel

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
				rq_phone.ErrorMessage     = _RequiredValErrMsg;
			}
		}
		#endregion RequiredValErrMsg

			#region RegularExpErrMsg
		private string _RegularExpErrMsg;
		private bool _RegularExpErrMsg_called;

		///<summary>Gets or Sets a value indicating the error message for the regular expression field validator.</summary>
		public string RegularExpErrMsg
		{
			get
			{
				return _RegularExpErrMsg;
			}
			set
			{
				_RegularExpErrMsg          = value;
				_RegularExpErrMsg_called   = true;
				reg_phone.ErrorMessage     = _RegularExpErrMsg;
			}
		}
		#endregion RegularExpErrMsg



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
			if (this._RequiredValErrMsg_called == false)
			{
				this.RequiredValErrMsg = "Please enter a phone number, area code included, without formatting.";
			}
			if (this._RegularExpErrMsg_called == false)
			{
				this.RegularExpErrMsg  = "Please enter a phone number, area code included, without formatting.";
			}
		}
		#endregion Page_Load, OnInit_Handler
	}
}