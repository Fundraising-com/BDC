namespace QSPFulfillment.MarketingMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Common;

	/// <summary>
	///		Summary description for AddressMaintenanceControl.
	/// </summary>
	public partial class PhoneMaintenanceControl : MarketingMgtControl
	{

		private DataTable PhoneTable;
		private DataView dv;

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

		protected void btnRemove_Click(object sender, System.EventArgs e)
		{
			try 
			{
				this.Visible = false;
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		public int PhoneID 
		{
			get 
			{
				if(this.ViewState["PhoneID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["PhoneID"]);
			}
			set 
			{
				this.ViewState["PhoneID"] = value;
			}
		}

		public int PhoneListID 
		{
			get 
			{
				if(this.ViewState["PhoneListID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["PhoneListID"]);
			}
			set 
			{
				this.ViewState["PhoneListID"] = value;
			}		
		}

		public new DataTable DataSource 
		{
			get 
			{
				return this.PhoneTable;
			}
			set 
			{
				this.PhoneTable = value;
			}
		}

		#region Fields

		private int PhoneType 
		{
			get 
			{
				return Convert.ToInt32(this.ddlType.SelectedValue);
			}
			set 
			{
				this.ddlType.SelectedIndex = this.ddlType.Items.IndexOf(this.ddlType.Items.FindByValue(value.ToString()));
			}
		}

		private string PhoneNumber 
		{
			get 
			{
				return this.tbxPhoneNumber.Text;
			}
			set 
			{
				this.tbxPhoneNumber.Text = value;
			}
		}

		private string BestTimeToCall
		{
			get 
			{
				return this.tbxBestTimeToCall.Text;
			}
			set 
			{
				this.tbxBestTimeToCall.Text = value;
			}
		}

		#endregion

		public override void DataBind()
		{
			LoadData();
		}

		private void LoadData() 
		{
			LoadDataDDL();

			if(this.PhoneID != 0 && this.DataSource != null) 
			{
				dv = new DataView(this.DataSource, "ID = " + this.PhoneID.ToString(), "", DataViewRowState.CurrentRows);
				SetValue();
			} 
			else 
			{
				SetValueEmpty();
			}
		}

		private void SetValue() 
		{
			DataRow row = null;

			row = dv[0].Row;
			this.PhoneType = Convert.ToInt32(row["Type"]);
			this.PhoneNumber = row["PhoneNumber"].ToString();
			this.BestTimeToCall = row["BestTimeToCall"].ToString();
		}

		private void SetValueEmpty() 
		{
			this.PhoneType = 0;
			this.PhoneNumber = "";
			this.BestTimeToCall = "";
		}

		private void LoadDataDDL() 
		{
			LoadDataDDLType();
		}

		private void LoadDataDDLType() 
		{
			DataTable dtbCodeDetail = new DataTable("CodeDetail");

			if(this.ddlType.Items.Count == 0) 
			{
				this.Page.BusCodeDetail.SelectByCodeHeaderInstance(dtbCodeDetail, 30500);

				this.ddlType.DataSource = dtbCodeDetail;
				this.ddlType.DataTextField = "Description";
				this.ddlType.DataValueField = "Instance";
				this.ddlType.DataBind();
			}
		}

		public void Save() 
		{
			if(this.Visible) 
			{
				if(this.PhoneID != 0) 
				{
					this.Page.BusCatalog.UpdatePhoneInformations(this.PhoneID, this.PhoneType, this.PhoneNumber, this.BestTimeToCall);
				} 
				else 
				{
					this.Page.BusCatalog.InsertPhoneInformations(this.PhoneType, this.PhoneListID, this.PhoneNumber, this.BestTimeToCall);
				}
			} 
			else 
			{
				if(this.PhoneID != 0) 
				{
					this.Page.BusCatalog.DeletePhone(this.PhoneID);
				}
			}
		}
	}
}
