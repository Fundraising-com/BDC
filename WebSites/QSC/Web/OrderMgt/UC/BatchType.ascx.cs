namespace QSPFulfillment.OrderMgt.UC
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	///<summary>Drop Down list of Batch Types</summary>
	public partial  class BatchType : System.Web.UI.UserControl
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
				ddlBatchType.Enabled	= _Enabled;
				//reg_ddlBatchType.Enabled	= _Enabled;
				if(_Enabled == true)
				{
					ddlBatchType.BackColor	= System.Drawing.Color.Transparent;
				}
				else
				{
					ddlBatchType.BackColor	= System.Drawing.Color.LightGray;
				}
				RequiredOrEnabled_ValueChanged();
			}
		}
		#endregion

		#region Required
		private bool _Required;
		private bool _Required_called = false;

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
				rq_BatchType.Enabled = true;
			}
			else
			{
				rq_BatchType.Enabled = false;
			}
		}
		#endregion

		#region SelectedValue
		///<summary>Gets the selected value of the BatchType control.</summary>
		private int _Selected_Value = -5;
		public int SelectedValue
		{
			get
			{
				if(this.ddlBatchType.SelectedValue == "") {
					return -5;
				} else {
					return Convert.ToInt32(this.ddlBatchType.SelectedValue) ;
				}
			}
			set
			{
				_Selected_Value = value;
				if(this.ddlBatchType.Items.Count > 0)
				{
					this.ddlBatchType.SelectedValue = Convert.ToString(_Selected_Value);
				}
			}
		}
		#endregion SelectedValue

		#region SelectedText
		///<summary>Gets the Text of the selected item from the BatchType control.</summary>
		public string SelectedText
		{
			get
			{
				return this.ddlBatchType.SelectedItem.Text;
			}
		}
		#endregion SelectedText

		#region AllTypesOption
		private bool _AllTypesOption = false;
		///<summary>Gets or sets whether or not a "All Batch Types" listitem should be displayed</summary>
		public bool AllTypesOption
		{
			get { return this._AllTypesOption;  }
			set { this._AllTypesOption = value; }
		}
		#endregion AllTypesOption

		#region CssClass
		///<summary>Get or sets the Cascading Style Sheet (CSS) class rendered by the BatchType control on the client.</summary>
		public string CssClass
		{
			get { return this.ddlBatchType.CssClass;  }
			set { this.ddlBatchType.CssClass = value; }
		}
		#endregion CssClass


		#endregion Control properties

		#region logic calls
		public void Bind()
		{
			this.ddlBatchType.Items.Clear();
			this.ddlBatchType.Items.Add(new ListItem("Select a Batch Type...", String.Empty));
			if(_AllTypesOption)
			{
				this.ddlBatchType.Items.Add(new ListItem("All Batch Types", "0"));
			}
			this.ddlBatchType.Items.Add(new ListItem("CA","41001"));
			this.ddlBatchType.Items.Add(new ListItem("CAFS","41002"));
			this.ddlBatchType.Items.Add(new ListItem("CREDITCM","41003"));
			this.ddlBatchType.Items.Add(new ListItem("DEBITCM","41004"));
			this.ddlBatchType.Items.Add(new ListItem("EMP","41005"));
			this.ddlBatchType.Items.Add(new ListItem("FM","41006"));
			this.ddlBatchType.Items.Add(new ListItem("FMBULK","41007"));
			this.ddlBatchType.Items.Add(new ListItem("GROUP","41008"));
			this.ddlBatchType.Items.Add(new ListItem("MAGNET","41009"));
			this.ddlBatchType.Items.Add(new ListItem("POS","41010"));

			//selected value
			if(_Selected_Value == -5)
			{
				this.ddlBatchType.SelectedValue = "";
			}
			else
			{
				this.ddlBatchType.SelectedValue = Convert.ToString(_Selected_Value);
			}

		}
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
			if (this._Required_called == false)
			{
				this.Required = true;
			}
			//Bind();
		}
		#endregion Page_Load, OnInIt_Handler
	}
}


