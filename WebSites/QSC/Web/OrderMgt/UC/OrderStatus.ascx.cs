namespace QSPFulfillment.OrderMgt.UC
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	///<summary>Drop Down list of Batch/Order statuses</summary>
	public partial  class OrderStatus : System.Web.UI.UserControl
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
				ddlOrderStatus.Enabled	= _Enabled;
				//reg_ddlOrderStatus.Enabled	= _Enabled;
				if(_Enabled == true)
				{
					ddlOrderStatus.BackColor	= System.Drawing.Color.Transparent;
				}
				else
				{
					ddlOrderStatus.BackColor	= System.Drawing.Color.LightGray;
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
				rq_OrderStatus.Enabled = true;
			}
			else
			{
				rq_OrderStatus.Enabled = false;
			}
		}
		#endregion

		#region SelectedValue
		///<summary>Gets the selected value of the OrderStatus control.</summary>
		private int _Selected_Value = -5;
		public int SelectedValue
		{
			get
			{
				if(this.ddlOrderStatus.SelectedValue == "") {
					return -5;
				} else {
					return Convert.ToInt32(this.ddlOrderStatus.SelectedValue) ;
				}
			}
			set
			{
				_Selected_Value = value;
				if(this.ddlOrderStatus.Items.Count > 0)
				{
					this.ddlOrderStatus.SelectedValue = Convert.ToString(_Selected_Value);
				}
			}
		}
		#endregion SelectedValue

		#region SelectedText
		///<summary>Gets the Text of the selected item from the OrderStatus control.</summary>
		public string SelectedText
		{
			get
			{
				return this.ddlOrderStatus.SelectedItem.Text;
			}
		}
		#endregion SelectedText

		#region AllStatusesOption
		private bool _AllStatusesOption = false;
		///<summary>Gets or sets whether or not a "All Statuses" listitem should be displayed</summary>
		public bool AllStatusesOption
		{
			get { return this._AllStatusesOption;  }
			set { this._AllStatusesOption = value; }
		}
		#endregion AllStatusesOption

		#region CssClass
		///<summary>Get or sets the Cascading Style Sheet (CSS) class rendered by the OrderStatus control on the client.</summary>
		public string CssClass
		{
			get { return this.ddlOrderStatus.CssClass;  }
			set { this.ddlOrderStatus.CssClass = value; }
		}
		#endregion CssClass


		#endregion Control properties

		#region logic calls
		public void Bind()
		{
			this.ddlOrderStatus.Items.Clear();
			this.ddlOrderStatus.Items.Add(new ListItem("Select a Status...", String.Empty));
			if(_AllStatusesOption)
			{
				this.ddlOrderStatus.Items.Add(new ListItem("All Statuses", "0"));
			}
			this.ddlOrderStatus.Items.Add(new ListItem("Approved","40004"));
			this.ddlOrderStatus.Items.Add(new ListItem("At Warehouse","40010"));
			this.ddlOrderStatus.Items.Add(new ListItem("Cancelled","40005"));
			this.ddlOrderStatus.Items.Add(new ListItem("CC Pending","40006"));
			this.ddlOrderStatus.Items.Add(new ListItem("Fulfilled","40013"));
			this.ddlOrderStatus.Items.Add(new ListItem("Housekeeping","40007"));
			this.ddlOrderStatus.Items.Add(new ListItem("HousekeepingC","40008"));
			this.ddlOrderStatus.Items.Add(new ListItem("In Process","40002"));
			this.ddlOrderStatus.Items.Add(new ListItem("New","40001"));
			this.ddlOrderStatus.Items.Add(new ListItem("Partially Fulfilled","40014"));
			this.ddlOrderStatus.Items.Add(new ListItem("Pending Review","40016"));
			this.ddlOrderStatus.Items.Add(new ListItem("Pickable","40009"));
			this.ddlOrderStatus.Items.Add(new ListItem("Sent to TPL","40012"));
			this.ddlOrderStatus.Items.Add(new ListItem("Shipped","40011"));
			this.ddlOrderStatus.Items.Add(new ListItem("Under Review","40003"));

			//selected value
			if(_Selected_Value == -5)
			{
				this.ddlOrderStatus.SelectedValue = "";
			}
			else
			{
				this.ddlOrderStatus.SelectedValue = Convert.ToString(_Selected_Value);
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


