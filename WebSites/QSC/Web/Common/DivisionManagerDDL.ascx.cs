namespace QSPFulfillment.CommonWeb.UC
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>Drop down list of Division Managers</summary>
	public partial  class DivisionManagerDDL : System.Web.UI.UserControl
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
				ddlDivisionManager.Enabled	= _Enabled;
				if(_Enabled == true)
				{
					ddlDivisionManager.BackColor	= System.Drawing.Color.Transparent;
				}
				else
				{
					ddlDivisionManager.BackColor	= System.Drawing.Color.LightGray;
				}
				//RequiredOrEnabled_ValueChanged();
			}
		}
		#endregion

			#region Required
//		private bool _Required;
//		
//		///<summary>Gets or Sets a value indicating whether the Web server control will be subject to required field validation.</summary>
//		public bool Required
//		{
//			get 
//			{
//				return _Required;
//			}
//			set
//			{
//				_Required = value;
//				RequiredOrEnabled_ValueChanged();
//			}
//		}
//
//		private void RequiredOrEnabled_ValueChanged()
//		{
//			if((_Required == true)&&(_Enabled == true))
//			{
//				rq_email.Enabled = true;
//			}
//			else
//			{
//				rq_email.Enabled = false;
//			}
//		}
		#endregion

			#region Width 
		///<summary>Gets the width of the DivisionManagerDDL control.</summary>
		public double Width
		{
			get { return this.ddlDivisionManager.Width.Value;  }
		}
		#endregion Width

			#region SelectedValue
		///<summary>Gets the selected value of the DivisionManagerDDL control.</summary>
		public string SelectedValue
		{
			get { return this.ddlDivisionManager.SelectedValue;  }
		}

		#endregion SelectedValue

		#endregion Control properties

		#region logic calls
		#endregion logic calls

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
		}
		#endregion Page_Load, OnInIt_Handler
	}
}

