namespace QSPFulfillment.CommonWeb.UC
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.ComponentModel;

	/// <summary>State/Province Drop Down Control class</summary>
	public partial  class StateProvince : System.Web.UI.UserControl
	{
		//string mParameterName ="";

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Init += new System.EventHandler(this.OnInit_Handler);
		}
		#endregion

		#region Item Declarations
		public System.Web.UI.WebControls.DropDownList			DDL;
		#endregion

		#region Control properties
		
		#region AutoPostBack
		public bool AutoPostBack
		{
			get
			{
				return DDL.AutoPostBack;
			}
			set
			{
				DDL.AutoPostBack = value;
			}
		}

		#endregion

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
				DDL.Enabled	= _Enabled;
				if(_Enabled)
				{
					DDL.BackColor	= System.Drawing.Color.Transparent;
					DDL.AutoPostBack = true;
				}
				else
				{
					DDL.BackColor	= System.Drawing.Color.LightGray;
					DDL.AutoPostBack = false;
				}
				if(_Required && _Enabled)
				{
					VAL_RQ.Enabled = true;
				}
				else
				{
					VAL_RQ.Enabled = false;
				}
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
				if((_Required == true)&&(_Enabled == true))
				{
					VAL_RQ.Enabled = true;
				}
				else
				{
					VAL_RQ.Enabled = false;
				}
			}
		}
		#endregion

		#region Value
		///<summary>Gets or Sets a value for the server control's selected item</summary>
		[Bindable(true),Category("SqlQuery"),DefaultValue("")]
		public string Value
		{
			get 
			{
				return this.DDL.SelectedItem.Value.ToString();
			}
			set
			{
				for(int i=0; i < DDL.Items.Count; i++)
				{
					if(DDL.Items[i].Value == value)
					{
						DDL.SelectedIndex = i;
						break;
					}
				}
			}
		}
		public string Text
		{
			get 
			{
				return this.DDL.SelectedItem.Text;
			}
			
		}
		#endregion

		#region ReadOnly
		private bool _ReadOnly;

		///<summary>Gets or Sets a value indicating whether the Web server control is readable.</summary>
		public bool ReadOnly
		{
			//fake a readonly for a DDL since they dont have this property
			get
			{
				return _ReadOnly;
			}
			set
			{
				_ReadOnly = value;
				if(_ReadOnly)
				{
					DDL.Enabled = false;
				}
				else
				{
					DDL.Enabled = true;
				}
				DDL.ForeColor = System.Drawing.Color.Black;
			}
		}
		#endregion

		#endregion

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

		#endregion
		
	}
}
