namespace QSPFulfillment.AcctMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Business.Objects;
	using Common;
	using Common.TableDef;

	/// <summary>
	///		Summary description for AddressListMaintenanceControl.
	/// </summary>
	public partial class AddressListMaintenanceControl : AcctMgtControl
	{
		protected QSPFulfillment.AcctMgt.Control.AddressMaintenanceControl ctrlAddressMaintenanceControlShipTo;
		protected QSPFulfillment.AcctMgt.Control.AddressMaintenanceControl ctrlAddressMaintenanceControlBillTo;
		private Address addr;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			AddJavaScript();
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

		public int AddressListID 
		{
			get 
			{
				if(this.ViewState["AddressListID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["AddressListID"]);
			}
			set 
			{
				this.ViewState["AddressListID"] = value;
			}
		}

		#region JavaScript

		protected override void AddJavaScript()
		{
			base.AddJavaScript ();

			//AddJavaScriptCopyFromShipTo();
			AddJavaScriptCopyOnBlur();
		}

		private void AddJavaScriptCopyFromShipTo() 
		{
			string script;

			script  = "<script language=\"javascript\">\n";
			script += "  function CopyFromShipTo() {\n";
			//script += "  document.getElementById(\"" + this.ctrlAddressMaintenanceControlBillTo.ReBindAddress()+"\"); \n";

			//script += "    document.getElementById(\"" + this.ctrlAddressMaintenanceControlBillTo.CountryControl.ClientID + "\").selectedIndex = document.getElementById(\"" + this.ctrlAddressMaintenanceControlShipTo.CountryControl.ClientID + "\").selectedIndex;\n";
			//script += "    document.getElementById(\"" + this.ctrlAddressMaintenanceControlBillTo.CountryControl.ClientID + "\").DataBind(); \n";
			//script += "    document.getElementById(\"" + this.ctrlAddressMaintenanceControlBillTo.ProvinceControl.ClientID + "\").DataBind(); \n";
			//script += "    document.getElementById(\"" + this.ctrlAddressMaintenanceControlBillTo.ProvinceControl.ClientID+ "\").selectedIndex = document.getElementById(\"" + this.ctrlAddressMaintenanceControlShipTo.ProvinceControl.Code + "\").selectedIndex;\n";
			//script += "    document.getElementById(\"" + this.ctrlAddressMaintenanceControlBillTo.Address1Control.ClientID + "\").value = document.getElementById(\"" + this.ctrlAddressMaintenanceControlShipTo.Address1Control.ClientID + "\").value;\n";
			//script += "    document.getElementById(\"" + this.ctrlAddressMaintenanceControlBillTo.Address2Control.ClientID + "\").value = document.getElementById(\"" + this.ctrlAddressMaintenanceControlShipTo.Address2Control.ClientID + "\").value;\n";
			//script += "    document.getElementById(\"" + this.ctrlAddressMaintenanceControlBillTo.CityControl.ClientID + "\").value = document.getElementById(\"" + this.ctrlAddressMaintenanceControlShipTo.CityControl.ClientID + "\").value;\n";
			//script += "    document.getElementById(\"" + this.ctrlAddressMaintenanceControlBillTo.ProvinceControl.Code + "\").selectedIndex = document.getElementById(\"" + this.ctrlAddressMaintenanceControlShipTo.ProvinceControl.Code + "\").selectedIndex;\n";
			//script += "    document.getElementById(\"" + this.ctrlAddressMaintenanceControlBillTo.PostalCodeControl.ClientID + "\").value = document.getElementById(\"" + this.ctrlAddressMaintenanceControlShipTo.PostalCodeControl.ClientID + "\").value;\n";
			
			script += "  }\n";
			script += "</script>\n";

			//this.Page.RegisterClientScriptBlock("CopyFromShipTo", script);
			//this.btnCopyFromShipTo.Attributes["onclick"] = "CopyFromShipTo();";
		}

		private void AddJavaScriptCopyOnBlur() 
		{
			string script;

			script  = "<script language=\"javascript\">\n";
			script += "  function CopyOnBlur(clientID, copyValue) {\n";
			script += "    if(!document.getElementById(clientID).selectedIndex) {\n";
			script += "      if(document.getElementById(clientID).value == \"\") {\n";
			script += "        document.getElementById(clientID).value = copyValue;\n";
			script += "      }\n";
			script += "    } else {\n";
			script += "      if(document.getElementById(clientID).selectedIndex == 0) {\n";
			script += "        document.getElementById(clientID).selectedIndex = copyValue;\n";
			script += "      }\n";
			script += "    }\n";
			script += "  }\n";
			script += "</script>\n";

			this.Page.RegisterClientScriptBlock("CopyOnBlur", script);
			this.ctrlAddressMaintenanceControlShipTo.Address1Control.Attributes["onBlur"] = "CopyOnBlur(\"" + this.ctrlAddressMaintenanceControlBillTo.Address1Control.ClientID + "\", event.srcElement.value);";
			this.ctrlAddressMaintenanceControlShipTo.Address2Control.Attributes["onBlur"] = "CopyOnBlur(\"" + this.ctrlAddressMaintenanceControlBillTo.Address2Control.ClientID + "\", event.srcElement.value);";
			this.ctrlAddressMaintenanceControlShipTo.CityControl.Attributes["onBlur"] = "CopyOnBlur(\"" + this.ctrlAddressMaintenanceControlBillTo.CityControl.ClientID + "\", event.srcElement.value);";
			this.ctrlAddressMaintenanceControlShipTo.CountryControl.Attributes["onBlur"] = "CopyOnBlur(\"" + this.ctrlAddressMaintenanceControlBillTo.CountryControl.ClientID + "\", event.srcElement.value);";
			this.ctrlAddressMaintenanceControlShipTo.ProvinceControl.Attributes["onBlur"] = "CopyOnBlur(\"" + this.ctrlAddressMaintenanceControlBillTo.ProvinceControl.ClientID + "\", event.srcElement.value);";//event.srcElement.selectedIndex);";
			this.ctrlAddressMaintenanceControlShipTo.PostalCodeControl.Attributes["onBlur"] = "CopyOnBlur(\"" + this.ctrlAddressMaintenanceControlBillTo.PostalCodeControl.ClientID + "\", event.srcElement.value);";
			
		}

		


		#endregion

		public override void DataBind()
		{
            if(this.AddressListID != 0) 
            {
                addr = new Address(this.AddressListID, this.Page.CurrentTransaction);

                this.ctrlAddressMaintenanceControlShipTo.AddressID = addr.GetOneByType(AddressType.ShipTo).address_id;
                this.ctrlAddressMaintenanceControlShipTo.DataSource = addr;
                this.ctrlAddressMaintenanceControlShipTo.DataBind();

                this.ctrlAddressMaintenanceControlBillTo.AddressID = addr.GetOneByType(AddressType.BillTo).address_id;
                this.ctrlAddressMaintenanceControlBillTo.DataSource = addr;
                this.ctrlAddressMaintenanceControlBillTo.DataBind();
            }
            else
            {
                this.ctrlAddressMaintenanceControlShipTo.DataBind();
                this.ctrlAddressMaintenanceControlBillTo.DataBind();
            }
		}

		public void Save() 
		{
			if(this.AddressListID != 0) 
			{
				addr = new Address(this.AddressListID, this.Page.CurrentTransaction);

				this.ctrlAddressMaintenanceControlShipTo.DataSource = addr;
				this.ctrlAddressMaintenanceControlShipTo.AddressListID = this.AddressListID;
				this.ctrlAddressMaintenanceControlShipTo.Save();

				this.ctrlAddressMaintenanceControlBillTo.DataSource = addr;
				this.ctrlAddressMaintenanceControlBillTo.AddressListID = this.AddressListID;
				this.ctrlAddressMaintenanceControlBillTo.Save();

			

				addr.Save();
			}
		}

		public void SaveNew() 
		{
			this.ctrlAddressMaintenanceControlShipTo.AddressID = 0;
			this.ctrlAddressMaintenanceControlBillTo.AddressID = 0;
			
			Save();
		}

		
		public void  CopyAddress()
		{
			this.ctrlAddressMaintenanceControlBillTo.Address1Control.Text =this.ctrlAddressMaintenanceControlShipTo.Address1Control.Text;
			this.ctrlAddressMaintenanceControlBillTo.Address2Control.Text =this.ctrlAddressMaintenanceControlShipTo.Address2Control.Text;
			this.ctrlAddressMaintenanceControlBillTo.CityControl.Text=this.ctrlAddressMaintenanceControlShipTo.CityControl.Text;
			//this.ctrlAddressMaintenanceControlBillTo.PostalCodeControl.Text=this.ctrlAddressMaintenanceControlShipTo.PostalCodeControl.Text;
			
			if (this.ctrlAddressMaintenanceControlShipTo.CountryControl.SelectedValue == "US")
			{
				this.ctrlAddressMaintenanceControlBillTo.CountryControl.SelectedValue  = this.ctrlAddressMaintenanceControlShipTo.CountryControl.SelectedValue;
				this.ctrlAddressMaintenanceControlBillTo.CountryControl.DataBind();
				this.ctrlAddressMaintenanceControlBillTo.ProvinceControl.Code = QSP.WebControl.DataAccess.Business.CountryCode.US;
				this.ctrlAddressMaintenanceControlBillTo.ProvinceControl.DataBind();
				this.ctrlAddressMaintenanceControlBillTo.ProvinceControl.SelectedIndex=this.ctrlAddressMaintenanceControlShipTo.ProvinceControl.SelectedIndex;

				this.ctrlAddressMaintenanceControlBillTo.PostalCodeControl.MaxLength=10;
				this.ctrlAddressMaintenanceControlBillTo.PostalCodeControl.Text=this.ctrlAddressMaintenanceControlShipTo.PostalCodeControl.Text;
				this.ctrlAddressMaintenanceControlBillTo.PostalCodeLabelText = "Zip Code";
				this.ctrlAddressMaintenanceControlBillTo.ProvinceLabelText="State";

			}
			else if (this.ctrlAddressMaintenanceControlShipTo.CountryControl.SelectedValue == "CA")
			{
				this.ctrlAddressMaintenanceControlBillTo.CountryControl.SelectedValue  = this.ctrlAddressMaintenanceControlShipTo.CountryControl.SelectedValue;
				this.ctrlAddressMaintenanceControlBillTo.CountryControl.DataBind();
				this.ctrlAddressMaintenanceControlBillTo.ProvinceControl.Code = QSP.WebControl.DataAccess.Business.CountryCode.CA;
				this.ctrlAddressMaintenanceControlBillTo.ProvinceControl.DataBind();
				this.ctrlAddressMaintenanceControlBillTo.ProvinceControl.SelectedIndex=this.ctrlAddressMaintenanceControlShipTo.ProvinceControl.SelectedIndex;

				this.ctrlAddressMaintenanceControlBillTo.PostalCodeControl.MaxLength=6;
				this.ctrlAddressMaintenanceControlBillTo.PostalCodeControl.Text=this.ctrlAddressMaintenanceControlShipTo.PostalCodeControl.Text;
				this.ctrlAddressMaintenanceControlBillTo.PostalCodeLabelText = "Postal Code";
				this.ctrlAddressMaintenanceControlBillTo.ProvinceLabelText="Province";
						
			}
						
		}

		protected void btnCopyFromShipTo_ServerClick(object sender, System.EventArgs e)
		{
			CopyAddress();
		}
				

		
	}
}
