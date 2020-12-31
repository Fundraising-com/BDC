namespace QSPFulfillment.OrderMgt.UC
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	///<summary>Drop Down list of Order Qualifiers</summary>
	public partial  class OrderQualifier : System.Web.UI.UserControl
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
				ddlOrderQualifier.Enabled	= _Enabled;
				//reg_ddlOrderQualifier.Enabled	= _Enabled;
				if(_Enabled == true)
				{
					ddlOrderQualifier.BackColor	= System.Drawing.Color.Transparent;
				}
				else
				{
					ddlOrderQualifier.BackColor	= System.Drawing.Color.LightGray;
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
				rq_OrderQualifier.Enabled = true;
			}
			else
			{
				rq_OrderQualifier.Enabled = false;
			}
		}
		#endregion

		#region SelectedValue
		///<summary>Gets the selected value of the OrderQualifier control.</summary>
		private int _Selected_Value = -5;
		public int SelectedValue
		{
			get
			{
				if(this.ddlOrderQualifier.SelectedValue == "") {
					return -5;
				} else {
					return Convert.ToInt32(this.ddlOrderQualifier.SelectedValue) ;
				}
			}
			set
			{
				_Selected_Value = value;
				if(this.ddlOrderQualifier.Items.Count > 0)
				{
					this.ddlOrderQualifier.SelectedValue = Convert.ToString(_Selected_Value);
				}
			}
		}
		#endregion SelectedValue

		#region SelectedText
		///<summary>Gets the Text of the selected item from the OrderQualifier control.</summary>
		public string SelectedText
		{
			get
			{
				return this.ddlOrderQualifier.SelectedItem.Text;
			}
		}
		#endregion SelectedText

		#region AllQualifiersOption
		private bool _AllQualifiersOption = false;
		///<summary>Gets or sets whether or not a "All Statuses" listitem should be displayed</summary>
		public bool AllQualifiersOption
		{
			get { return this._AllQualifiersOption;  }
			set { this._AllQualifiersOption = value; }
		}
		#endregion AllQualifiersOption

		#region CssClass
		///<summary>Get or sets the Cascading Style Sheet (CSS) class rendered by the OrderQualifier control on the client.</summary>
		public string CssClass
		{
			get { return this.ddlOrderQualifier.CssClass;  }
			set { this.ddlOrderQualifier.CssClass = value; }
		}
		#endregion CssClass


		#endregion Control properties

		#region logic calls
		public void Bind()
		{
			this.ddlOrderQualifier.Items.Clear();
			this.ddlOrderQualifier.Items.Add(new ListItem("Select a Qualifier...", String.Empty));
			if(_AllQualifiersOption)
			{
				this.ddlOrderQualifier.Items.Add(new ListItem("All Qualifiers", "0"));
			}
            this.ddlOrderQualifier.Items.Add(new ListItem("Book Problem Solver", "39023"));
            this.ddlOrderQualifier.Items.Add(new ListItem("CC Reprocess Courtesy", "39014"));
			this.ddlOrderQualifier.Items.Add(new ListItem("CC Reprocessed to invoice", "39015"));
			this.ddlOrderQualifier.Items.Add(new ListItem("Credit Card Reprocess", "39013"));
			this.ddlOrderQualifier.Items.Add(new ListItem("Customer Service","39008"));
			this.ddlOrderQualifier.Items.Add(new ListItem("Customer Service to Invoice", "39020"));
			this.ddlOrderQualifier.Items.Add(new ListItem("Field Supplies","39007"));
			this.ddlOrderQualifier.Items.Add(new ListItem("FM Bulk Supply", "39022"));
			this.ddlOrderQualifier.Items.Add(new ListItem("FM Gift Sample", "39016"));
			this.ddlOrderQualifier.Items.Add(new ListItem("GiftFix","39010"));
			this.ddlOrderQualifier.Items.Add(new ListItem("Gift PSolver", "39019"));
			this.ddlOrderQualifier.Items.Add(new ListItem("Internet","39009"));
			this.ddlOrderQualifier.Items.Add(new ListItem("Internet Fix","39011"));
			this.ddlOrderQualifier.Items.Add(new ListItem("Kanata","39006"));
			this.ddlOrderQualifier.Items.Add(new ListItem("Kanata PSolver", "39018"));
			this.ddlOrderQualifier.Items.Add(new ListItem("Main","39001"));
			this.ddlOrderQualifier.Items.Add(new ListItem("Order Correction","39012"));
			this.ddlOrderQualifier.Items.Add(new ListItem("Problem Solver","39005"));
			this.ddlOrderQualifier.Items.Add(new ListItem("Staff","39003"));
			this.ddlOrderQualifier.Items.Add(new ListItem("Supplement","39002"));
			this.ddlOrderQualifier.Items.Add(new ListItem("Test","39004"));
			this.ddlOrderQualifier.Items.Add(new ListItem("Time Main", "39021"));
			this.ddlOrderQualifier.Items.Add(new ListItem("WFC Signing Bonus", "39017"));

			//selected value
			if(_Selected_Value == -5)
			{
				this.ddlOrderQualifier.SelectedValue = "";
			}
			else
			{
				this.ddlOrderQualifier.SelectedValue = Convert.ToString(_Selected_Value);
			}

		}

		public void KanataBind(int CatalogType)
		{
         if (CatalogType == 30325)
         {
            this.ddlOrderQualifier.Items.Clear();
            this.ddlOrderQualifier.Items.Add(new ListItem("FM Bulk Supply", "39022"));
            this.ddlOrderQualifier.Items.Add(new ListItem("Gift Problem Solver", "39019"));
         }
         else if (CatalogType == 30308)
         {
            this.ddlOrderQualifier.Items.Clear();
            this.ddlOrderQualifier.Items.Add(new ListItem("FM Bulk Supply", "39022"));
            this.ddlOrderQualifier.Items.Add(new ListItem("Cookie Dough Problem Solver", "39019"));
         }
         else if (CatalogType == 30323)
         {
            this.ddlOrderQualifier.Items.Clear();
            this.ddlOrderQualifier.Items.Add(new ListItem("FM Bulk Supply", "39022"));
            this.ddlOrderQualifier.Items.Add(new ListItem("Gift Problem Solver", "39019"));
         }
         else if (CatalogType == 30327)
         {
            this.ddlOrderQualifier.Items.Clear();
            this.ddlOrderQualifier.Items.Add(new ListItem("Donation Problem Solver", "39019"));
         }
         else if (CatalogType == 30329)
         {
            this.ddlOrderQualifier.Items.Clear();
            this.ddlOrderQualifier.Items.Add(new ListItem("FM Bulk Supply", "39022"));
            this.ddlOrderQualifier.Items.Add(new ListItem("Gift Problem Solver", "39019"));
         }
         else if (CatalogType == 30315)
         {
            this.ddlOrderQualifier.Items.Clear();
            this.ddlOrderQualifier.Items.Add(new ListItem("FM Bulk Supply", "39022"));
         }
         else if (CatalogType == 30331)
         {
            this.ddlOrderQualifier.Items.Clear();
            this.ddlOrderQualifier.Items.Add(new ListItem("Main", "39001"));
            this.ddlOrderQualifier.Items.Add(new ListItem("Supplement", "39002"));
            this.ddlOrderQualifier.Items.Add(new ListItem("FM Bulk Supply", "39022"));
            this.ddlOrderQualifier.Items.Add(new ListItem("Popcorn Problem Solver", "39019"));
         }
         else if (CatalogType == 30310)
         {
            this.ddlOrderQualifier.Items.Clear();
            this.ddlOrderQualifier.Items.Add(new ListItem("Kanata", "39006"));
            this.ddlOrderQualifier.Items.Add(new ListItem("Kanata PSolver", "39018"));
         }
         else if (CatalogType == 30311)
         {
            this.ddlOrderQualifier.Items.Clear();
            this.ddlOrderQualifier.Items.Add(new ListItem("Kanata", "39006"));
            this.ddlOrderQualifier.Items.Add(new ListItem("Kanata PSolver", "39018"));
         }
         else if (CatalogType == 30334)
         {
            this.ddlOrderQualifier.Items.Clear();
            this.ddlOrderQualifier.Items.Add(new ListItem("Main", "39001"));
            this.ddlOrderQualifier.Items.Add(new ListItem("Supplement", "39002"));
            this.ddlOrderQualifier.Items.Add(new ListItem("FM Bulk Supply", "39022"));
            this.ddlOrderQualifier.Items.Add(new ListItem("QSP Savings Pass Problem Solver", "39019"));
         }
         else if (CatalogType == 30335)
         {
            this.ddlOrderQualifier.Items.Clear();
            this.ddlOrderQualifier.Items.Add(new ListItem("FM Bulk Supply", "39022"));
            this.ddlOrderQualifier.Items.Add(new ListItem("Gift Card Problem Solver", "39019"));
         }
         else if (CatalogType == 30336)
         {
            this.ddlOrderQualifier.Items.Clear();
            this.ddlOrderQualifier.Items.Add(new ListItem("Main", "39001"));
            this.ddlOrderQualifier.Items.Add(new ListItem("Supplement", "39002"));
            this.ddlOrderQualifier.Items.Add(new ListItem("FM Bulk Supply", "39022"));
            this.ddlOrderQualifier.Items.Add(new ListItem("Popcorn Problem Solver", "39019"));
         }
         else if (CatalogType == 30337)
         {
            this.ddlOrderQualifier.Items.Clear();
            this.ddlOrderQualifier.Items.Add(new ListItem("FM Bulk Supply", "39022"));
            this.ddlOrderQualifier.Items.Add(new ListItem("Gift Problem Solver", "39019"));
         }
         else if (CatalogType == 30338)
         {
            this.ddlOrderQualifier.Items.Clear();
            this.ddlOrderQualifier.Items.Add(new ListItem("Main", "39001"));
            this.ddlOrderQualifier.Items.Add(new ListItem("Supplement", "39002"));
            this.ddlOrderQualifier.Items.Add(new ListItem("FM Bulk Supply", "39022"));
            this.ddlOrderQualifier.Items.Add(new ListItem("Pretzel Rods Problem Solver", "39019"));
         }
         else if (CatalogType == 30339)
         {
            this.ddlOrderQualifier.Items.Clear();
            this.ddlOrderQualifier.Items.Add(new ListItem("FM Bulk Supply", "39022"));
            this.ddlOrderQualifier.Items.Add(new ListItem("Jewelry Problem Solver", "39019"));
         }
         else if (CatalogType == 30340)
         {
            this.ddlOrderQualifier.Items.Clear();
            this.ddlOrderQualifier.Items.Add(new ListItem("FM Bulk Supply", "39022"));
            this.ddlOrderQualifier.Items.Add(new ListItem("Tasty Treats Problem Solver", "39019"));
         }
         else if (CatalogType == 30341)
         {
            this.ddlOrderQualifier.Items.Clear();
            this.ddlOrderQualifier.Items.Add(new ListItem("Main", "39001"));
            this.ddlOrderQualifier.Items.Add(new ListItem("Supplement", "39002"));
            this.ddlOrderQualifier.Items.Add(new ListItem("FM Bulk Supply", "39022"));
            this.ddlOrderQualifier.Items.Add(new ListItem("Pretzel Rods Problem Solver", "39019"));
         }
         else if (CatalogType == 30342)
         {
            this.ddlOrderQualifier.Items.Clear();
            this.ddlOrderQualifier.Items.Add(new ListItem("Main", "39001"));
            this.ddlOrderQualifier.Items.Add(new ListItem("Supplement", "39002"));
            this.ddlOrderQualifier.Items.Add(new ListItem("FM Bulk Supply", "39022"));
            this.ddlOrderQualifier.Items.Add(new ListItem("QSP Leap Labels Problem Solver", "39019"));
         }
         else if (CatalogType == 30345)
         {
            this.ddlOrderQualifier.Items.Clear();
            this.ddlOrderQualifier.Items.Add(new ListItem("Main", "39001"));
            this.ddlOrderQualifier.Items.Add(new ListItem("Supplement", "39002"));
            this.ddlOrderQualifier.Items.Add(new ListItem("FM Bulk Supply", "39022"));
            this.ddlOrderQualifier.Items.Add(new ListItem("Cool Cards Problem Solver", "39019"));
         }
         else if (CatalogType == 30346)
         {
            this.ddlOrderQualifier.Items.Clear();
            this.ddlOrderQualifier.Items.Add(new ListItem("Rally Problem Solver", "39019"));
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


