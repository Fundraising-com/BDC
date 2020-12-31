namespace QSPFulfillment.AcctMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Common;
	using Common.TableDef;
	using Business.Objects;

	/// <summary>
	///		Summary description for LastContactViewerControl.
	/// </summary>
	public partial class AddressViewerControl : AcctMgtControl
	{

		private Address address;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

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

		}
		#endregion

		public int AddressID 
		{
			get 
			{
				if(this.ViewState["AddressID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["AddressID"]);
			}
			set 
			{
				this.ViewState["AddressID"] = value;
			}
		}

		public Address oAddress 
		{
			get 
			{
				return address;
			}
		}

		public override void DataBind() 
		{
			if(this.AddressID != 0) 
			{
				LoadData();
				SetValue();
			} 
			else 
			{
				SetValueEmpty();
			}

			SetVisible();
		}

		private void LoadData() 
		{
			address = new Address();
			address.CurrentTransaction = this.Page.CurrentTransaction;

			if(this.AddressID != 0) 
			{
				oAddress.GetOneByIDWithChildren(this.AddressID);
			} 
		}

		private void SetValue() 
		{
			AddressDataSet.AddressRow rowSelected = this.oAddress.dataSet.Address.FindByaddress_id(this.AddressID);
			CodeDetailDataSet.CodeDetailRow rowCodeDetailType;
			ProvinceDataSet.ProvinceRow rowProvince;

			if(rowSelected != null) 
			{
				rowCodeDetailType = oAddress.CodeDetailType.dataSet.CodeDetail.FindByInstance(rowSelected.address_type);
				rowProvince = oAddress.AddressProvince.dataSet.Province.FindByCOUNTRY_CODEPROVINCE_CODE("CA", rowSelected.stateProvince);

				if(rowCodeDetailType != null) 
				{
					this.lblAddressType.Text = rowCodeDetailType.Description;
				}

				this.lblAddressLine1.Text = rowSelected.street1;
				this.lblAddressLine2.Text = rowSelected.street2;
				this.lblCity.Text = rowSelected.city;

				if(rowProvince != null) 
				{
					this.lblProvince.Text = rowProvince.PROVINCE_NAME;
				}

				this.lblPostalCode.Text = rowSelected.postal_code;
				this.lblCountryCode.Text = rowSelected.country;
			}
		}

		private void SetValueEmpty() 
		{
			this.lblAddressType.Text = String.Empty;
			this.lblAddressLine1.Text = String.Empty;
			this.lblAddressLine2.Text = String.Empty;
			this.lblCity.Text = String.Empty;
			this.lblProvince.Text = String.Empty;
			this.lblPostalCode.Text = String.Empty;
			this.lblCountryCode.Text = String.Empty;
		}

		private void SetVisible() 
		{
			this.Visible = (this.AddressID != 0 && oAddress.dataSet.Address.FindByaddress_id(this.AddressID) != null);
		}
	}
}
