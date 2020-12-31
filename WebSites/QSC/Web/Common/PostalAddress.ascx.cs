namespace QSPFulfillment.CommonWeb.UC
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	/// PostalAddress user control.
	/// </summary>
	public partial  class PostalAddress : System.Web.UI.UserControl
	{
		#region item declarations
		protected QSPFulfillment.CommonWeb.UC.StateProvince StateProvince;
		private bool _Enabled;
		private bool _Enabled_called;
		#endregion item declarations
	
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
	
		#region Control properties
		
		#region Enabled
	

		///<summary>Gets or Sets a value indicating whether the Web server control is enabled.</summary>
		public bool Enabled
		{
			get
			{
				return _Enabled;
			}
			set
			{
				_Enabled				= value;
				_Enabled_called			= true;
				street1.Enabled			= _Enabled;
				street2.Enabled			= _Enabled;
				city.Enabled			= _Enabled;
				StateProvince.Enabled	= _Enabled;
				PostalCode.Enabled		= _Enabled;
				PostalCode4.Enabled		= _Enabled;
				if(_Enabled)
				{
					street1.Visible = true; ;//.BackColor		= System.Drawing.Color.Transparent;
					this.lblStreet1.Visible = false;
					//street1;//.BackColor		= System.Drawing.Color.Transparent;
					StateProvince.Visible = true;
					this.lblStateProvince.Visible = false;

					street2.Visible = true;//.BackColor		= System.Drawing.Color.Transparent;
					this.lblStreet2.Visible = false;
					city.Visible = true;//.BackColor			= System.Drawing.Color.Transparent;
					lblCity.Visible = false;
					//StateProvince.BackColor	= System.Drawing.Color.Transparent;
					
					PostalCode.Visible = true;//.BackColor	= System.Drawing.Color.Transparent;
					lblPostalCode.Visible = false;
					PostalCode4.Visible = true;//.BackColor	= System.Drawing.Color.Transparent;
					
					ddlCountry.Visible = true;//.BackColor	= System.Drawing.Color.Transparent;
					lblCountry.Visible = false;
					this.reg_PostalCode.Visible = true;
					this.rq_city.Visible = true;
					this.rq_PostalCode.Visible = true;
					this.rq_street1.Visible = true;
				}
				else
				{
					street1.Visible = false; ;//.BackColor		= System.Drawing.Color.Transparent;
					this.lblStreet1.Visible = true;
					//street1;//.BackColor		= System.Drawing.Color.Transparent;
					
					street2.Visible = false;//.BackColor		= System.Drawing.Color.Transparent;
					this.lblStreet2.Visible = true;
					city.Visible = false;//.BackColor			= System.Drawing.Color.Transparent;
					lblCity.Visible = true;
					//StateProvince.BackColor	= System.Drawing.Color.Transparent;
					
					PostalCode.Visible = false;//.BackColor	= System.Drawing.Color.Transparent;
					lblPostalCode.Visible = true;
					PostalCode4.Visible = false;//.BackColor	= System.Drawing.Color.Transparent;
					
					ddlCountry.Visible = false;//.BackColor	= System.Drawing.Color.Transparent;
					lblCountry.Visible = true;

					StateProvince.Visible = false;
					this.lblStateProvince.Visible = true;

					this.reg_PostalCode.Visible = false;
					this.rq_city.Visible = false;
					this.rq_PostalCode.Visible = false;
					this.rq_street1.Visible = false;
					
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
				rq_street1.Enabled		= true;
				rq_city.Enabled			= true;
				//StateProvince.Enabled	= true;
				StateProvince.Required	= true;
				rq_PostalCode.Enabled	= true;
			}
			else
			{
				rq_street1.Enabled		= false;
				rq_city.Enabled			= false;
				//StateProvince.Enabled	= false;
				StateProvince.Required	= false;
				rq_PostalCode.Enabled	= false;
			}
		}
		#endregion

		#region Street1
		///<summary>Gets or Sets a value for street address 1.</summary>
		public string pStreet1
		{
			get 
			{
				return street1.Text;
			}
			set
			{
				street1.Text = value;
				lblStreet1.Text = value;
			}
		}
		#endregion

		#region Street2
		///<summary>Gets or Sets a value for street address 2.</summary>
		public string pStreet2
		{
			get 
			{
				return street2.Text;
			}
			set
			{
				street2.Text = value;
				lblStreet2.Text = value;
			}
		}
		#endregion

		#region City
		///<summary>Gets or Sets a value for city.</summary>
		public string pCity
		{
			get 
			{
				return this.city.Text;
			}
			set
			{
				this.city.Text = value;
				this.lblCity.Text = value;
			}
		}
		#endregion

		#region StateProvince
		///<summary>Gets or Sets a value for city.</summary>
		public string pStateProvince
		{
			get { return this.StateProvince.Value; } 
			set 
			{
				this.StateProvince.Value = value;
				this.lblStateProvince.Text = this.StateProvince.Text;
			}
		}
		#endregion

		#region PostalCode
		///<summary>Gets or Sets a value for the postal code.</summary>
		public string pPostalCode
		{
			get 
			{
				return this.PostalCode.Text;
			}
			set
			{
				this.PostalCode.Text = value;
				this.lblPostalCode.Text = value;
			}
		}
		#endregion
		
		#region PostalCode4
		///<summary>Gets or Sets a value for the postal +4 code.</summary>
		public string pPostalCode4
		{
			get 
			{
				return this.PostalCode4.Text;
			}
			set
			{
				this.PostalCode4.Text = value;
			}
		}
		#endregion

		#region FullAddress
		///<summary>Gets or sets full PostalAddress class value.</summary>
		public Common.PostalAddress FullAddress
		{
			get 
			{
				return new Common.PostalAddress(this.street1.Text, this.street2.Text, this.city.Text, this.StateProvince.Value, this.PostalCode.Text, this.PostalCode4.Text, this.ddlCountry.SelectedValue.ToString());
			}
			set 
			{
				this.pStreet1		= value.Street1;
				this.pStreet2		= value.Street2;
				this.pCity			= value.City;
				this.pStateProvince	= value.StateProvince;
				this.pPostalCode	= value.PostalCode;
				this.pPostalCode4	= value.PostalPlus4Code;
				this.Country		= value.Country;
			}
		}
		#endregion

		#region P4Enabled
		///<summary>Gets or Sets a value indicating whether postal+4 code is enabled.</summary>
		public bool P4Enabled
		{
			get
			{
				return this.tablecell_PostalCode4.Visible;
			}
			set
			{
				//this.PostalCode4.Enabled = value;
				this.tablecell_PostalCode4.Visible  = value;
			}
		}
		#endregion

		#region ReadOnly
		private bool _ReadOnly;
		protected DropDownList Dropdownlist1;
		///<summary>Gets or Sets a value indicating whether the Web server control readable.</summary>
		public bool ReadOnly
		{
			get
			{
				return _ReadOnly;
			}
			set
			{
				_ReadOnly				= value;
				street1.ReadOnly		= _ReadOnly;
				street2.ReadOnly		= _ReadOnly;
				city.ReadOnly			= _ReadOnly;
				StateProvince.ReadOnly	= _ReadOnly;
				PostalCode.ReadOnly		= _ReadOnly;
				PostalCode4.ReadOnly	= _ReadOnly;
				if(_ReadOnly == true) { this.ddlCountry.Enabled = false;}
				else { this.ddlCountry.Enabled = true;}
			}
		}
		#endregion

		#region Country
		private bool _Country_called;
		///<summary>Gets or Sets a value for the server control's selected item</summary>
		public string Country
		{
			get 
			{
				return this.ddlCountry.SelectedItem.Value.ToString();
			}
			set
			{
				this.lblCountry.Text = value;
				_Country_called = true;
				string country;
				string input = "";
				try   {input = value.ToLower().Trim(); }
				catch (NullReferenceException){}
				switch (input)
				{
					case "us":
					case "usa":
					case "united states":
					case "united states of america":
						country = "USA";
						break;
					case "ca":
					case "canada":
					default:
						country = "CA";
						break;
				}
				for(int i=0; i < ddlCountry.Items.Count; i++)
				{
					if(ddlCountry.Items[i].Value == country)
					{
						ddlCountry.SelectedIndex = i;
						break;
					}
				}
				ddlCountry_SelectedIndexChanged(ddlCountry.SelectedValue);
			}
		}
		public void ddlCountry_SelectedIndexChanged(object s, EventArgs e)
		{
			ddlCountry_SelectedIndexChanged(ddlCountry.SelectedValue);
		}
		private void ddlCountry_SelectedIndexChanged(string SelectedValue)
		{
			switch (SelectedValue.ToLower().Trim())
			{
				case "usa":
					lbStateProvince.Text = "State";
					P4Enabled = true;
					PostalCode.MaxLength = 5;
					reg_PostalCode.Enabled = true;
					break;
				case "ca":
				default:
					lbStateProvince.Text = "Province";
					P4Enabled = false;
					PostalCode.MaxLength = 7;
					reg_PostalCode.Enabled = false;
					break;
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
			if (this._Country_called == false)
			{
				this.Country = "";
			}
		}
		#endregion		

		public bool FirstColumn 
		{
			get
			{
				if(ViewState["FirstColumn"] == null)
						return false;
				return true;
			}
			set
			{
				ViewState["FirstColumn"] = value;
				this.Label1.Visible= value;
				this.Label2.Visible = value;
				this.Label3.Visible = value;
				this.Label4.Visible = value;
				this.Label5.Visible =value;
				this.lbStateProvince.Visible = value;
			}
		}
	}
}