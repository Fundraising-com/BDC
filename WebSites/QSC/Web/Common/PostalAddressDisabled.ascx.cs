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
	public partial  class PostalAddressDisabled : System.Web.UI.UserControl
	{

	
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{

		}
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
		
		}

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
			}
		}
	
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
			}
		}
	

		
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
			}
		}



		///<summary>Gets or Sets a value for city.</summary>
		public string pStateProvince
		{
			get 
			{ 
				return this.lblStateProvince.Text;
				
			} 
			set 
			{ 
				this.lblcoma.Visible= true;
				this.lblStateProvince.Text = value; 
			}
		}

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
			}
		}
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


		///<summary>Gets or Sets a value for the server control's selected item</summary>
		public string pCountry
		{
			get 
			{
				return this.lblCountry.Text;
			}
			set
			{
				switch (value)
				{
					case "us":
					case "usa":
					case "united states":
					case "united states of america":
						this.lblCountry.Text = "USA";
						break;
					case "ca":
						this.lblCountry.Text = "Canada";
						break;
					case "canada":
						this.lblCountry.Text = "Canada";
						break;
					default:
						this.lblCountry.Text = "Canada";
						break;
				}
				
			}
		}

        ///<summary>Gets or Sets a value for city.</summary>
        public string pEmail
        {
            get
            {
                return this.lblEmail.Text;
            }
            set
            {
                this.lblEmail.Text = value;
            }
        }
		
	}
}