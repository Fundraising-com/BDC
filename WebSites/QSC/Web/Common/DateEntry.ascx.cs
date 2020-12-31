namespace QSPFulfillment.CommonWeb.UC
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.ComponentModel;

    public delegate void tb_Date_Text_Changed_EventHandler(object sender);

	/// <summary>Date Entry Control class</summary>
	public partial  class DateEntry : System.Web.UI.UserControl,QSP.WebControl.ISearch
	{
		private string mParameterName = "";
		private string mContentType = "";
		private bool bValidation = true;

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
			this.Init += new System.EventHandler(this.OnInit_Handler);
            this.tb_DATE.TextChanged += new System.EventHandler(this.tb_DATE_TextChanged);
        }
		#endregion

		#region Item Declarations

		//protected System.Web.UI.WebControls.Panel	calBtn;
		//protected System.Web.UI.WebControls.Label	lb_format;


		#endregion

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
				tb_DATE.Enabled		= _Enabled;
				reg_date.Enabled	= _Enabled;
				if(_Enabled == true)
				{
					tb_DATE.BackColor	= System.Drawing.Color.Transparent;
				}
				else
				{
					tb_DATE.BackColor	= System.Drawing.Color.LightGray;
				}
				if((_Required == true)&&(_Enabled == true))
				{
					rq_date.Enabled = true;
				}
				else
				{
					rq_date.Enabled = false;
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
					rq_date.Enabled = true;
				}
				else
				{
					rq_date.Enabled = false;
				}
			}
		}

		public bool ReadOnly 
		{
			get 
			{
				return tb_DATE.ReadOnly;
			}
			set 
			{
				tb_DATE.ReadOnly = value;
			}
		}

		#endregion

			#region GetDate
		
		///<summary>Gets a value of the date in this field.</summary>
		public DateTime Date
		{
			get 
			{
				string dateString = "";
				try   { dateString = tb_DATE.Text.Trim(); }
				catch { dateString = ""; } 

				if(dateString == "") { return EmptyValue; }

				try
				{
					return Convert.ToDateTime(dateString);
				}
				catch
				{
					return EmptyValue;
				}
			}
			set
			{
				if(value != EmptyValue) 
				{
					tb_DATE.Text = value.ToString("MM/dd/yyyy");
				} 
				else 
				{
					tb_DATE.Text = String.Empty;
				}
			}
		}
		#endregion

        public event EventHandler tb_DATE_TextChanged_Event;

        protected void tb_DATE_TextChanged(object sender, System.EventArgs e)
        {
            if (tb_DATE_TextChanged_Event != null)
            {
                tb_DATE_TextChanged_Event(this, e);
            }
        }

		[Bindable(true),Category("Appearance"),DefaultValue("0")]
		public int Columns 
		{
			get 
			{
				return this.tb_DATE.Columns;
			}
			set 
			{
				this.tb_DATE.Columns = value;
			}
		}

		[Bindable(true),Category("Behavior"),DefaultValue("0")]
		public short TabIndex 
		{
			get 
			{
				return this.tb_DATE.TabIndex;
			}
			set 
			{
				this.tb_DATE.TabIndex = value;
			}
		}

		public DateTime EmptyValue 
		{
			get 
			{
				DateTime emptyValue = DateTime.MinValue;

				if(ViewState["EmptyValue"] != null) 
				{
					emptyValue = Convert.ToDateTime(ViewState["EmptyValue"]);
				}

				return emptyValue;
			}
			set 
			{
				ViewState["EmptyValue"] = value;
			}
		}

		#endregion

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
		}
		#endregion

		[Bindable(true),Category("SqlQuery"),DefaultValue("")]
		public string ParameterName
		{
			get
			{
				return this.mParameterName;
			}
			set
			{	
				
				this.mParameterName = value;	
			}
		}
		[Bindable(true),Category("SqlQuery"),DefaultValue("")]
		public string Value
		{
			get{return this.tb_DATE.Text;}
		}

		[Bindable(true), Category("SqlQuery"), DefaultValue("")]
		public string ContentType
		{
			get 
			{
				return this.mContentType;
			}
			set  
			{
				this.mContentType = value;
			}
		}

		[Bindable(true), Category("SqlQuery"), DefaultValue(true)]
		public bool Validation
		{
			get
			{
				return this.bValidation;
			}
			set 
			{
				this.bValidation = value;
			}
		}

		public string CssClass
		{
			get { return this.tb_DATE.CssClass;  }
			set { this.tb_DATE.CssClass = value; } 
		}

		public void ClearDate() 
		{
			this.tb_DATE.Text = "";
		}
	}
}
