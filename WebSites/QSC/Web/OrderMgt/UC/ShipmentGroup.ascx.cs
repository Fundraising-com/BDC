namespace QSPFulfillment.OrderMgt.UC
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

    ///<summary>Drop Down list of Shipment Groups</summary>
    public partial  class ShipmentGroup : System.Web.UI.UserControl
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
			this.Load += new System.EventHandler(this.Page_Load);
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
                ddlShipmentGroup.Enabled	= _Enabled;
				//reg_ddlShipmentGroup.Enabled	= _Enabled;
				if(_Enabled == true)
				{
                    ddlShipmentGroup.BackColor	= System.Drawing.Color.Transparent;
				}
				else
				{
                    ddlShipmentGroup.BackColor	= System.Drawing.Color.LightGray;
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
                rq_ShipmentGroup.Enabled = true;
			}
			else
			{
                rq_ShipmentGroup.Enabled = false;
			}
		}
        #endregion

        #region SelectedValue
        ///<summary>Gets the selected value of the ShipmentGroup control.</summary>
        private int _Selected_Value = -5;
		public int SelectedValue
		{
			get
			{
				if(this.ddlShipmentGroup.SelectedValue == "") {
					return -5;
				} else {
					return Convert.ToInt32(this.ddlShipmentGroup.SelectedValue) ;
				}
			}
			set
			{
				_Selected_Value = value;
				if(this.ddlShipmentGroup.Items.Count > 0)
				{
					this.ddlShipmentGroup.SelectedValue = Convert.ToString(_Selected_Value);
				}
			}
		}
		#endregion SelectedValue

		#region SelectedText
		///<summary>Gets the Text of the selected item from the ShipmentGroup control.</summary>
		public string SelectedText
		{
			get
			{
				return this.ddlShipmentGroup.SelectedItem.Text;
			}
		}
        #endregion SelectedText

        #region AllShipmentGroupsOption
        private bool _AllShipmentGroupsOption = false;
		///<summary>Gets or sets whether or not a "All Statuses" listitem should be displayed</summary>
		public bool AllShipmentGroupsOption
        {
			get { return this._AllShipmentGroupsOption;  }
			set { this._AllShipmentGroupsOption = value; }
		}
		#endregion AllQualifiersOption

		#region CssClass
		///<summary>Get or sets the Cascading Style Sheet (CSS) class rendered by the ShipmentGroup control on the client.</summary>
		public string CssClass
		{
			get { return this.ddlShipmentGroup.CssClass;  }
			set { this.ddlShipmentGroup.CssClass = value; }
		}
		#endregion CssClass


		#endregion Control properties

		#region logic calls
		public void Bind()
		{
			this.ddlShipmentGroup.Items.Clear();
			this.ddlShipmentGroup.Items.Add(new ListItem("Select a Shipment Group...", String.Empty));
			if(_AllShipmentGroupsOption)
			{
				this.ddlShipmentGroup.Items.Add(new ListItem("All Shipment Groups", "0"));
			}
            this.ddlShipmentGroup.Items.Add(new ListItem("Gift/Prizes", "1"));
            this.ddlShipmentGroup.Items.Add(new ListItem("Cookie Dough", "2"));
			this.ddlShipmentGroup.Items.Add(new ListItem("Field Supplies", "3"));
			this.ddlShipmentGroup.Items.Add(new ListItem("Popcorn", "4"));

			//selected value
			if(_Selected_Value == -5)
			{
				this.ddlShipmentGroup.SelectedValue = "";
			}
			else
			{
				this.ddlShipmentGroup.SelectedValue = Convert.ToString(_Selected_Value);
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


