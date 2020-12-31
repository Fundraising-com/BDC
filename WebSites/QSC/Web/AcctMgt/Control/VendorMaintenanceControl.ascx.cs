namespace QSPFulfillment.AcctMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Business.Objects;
	using Common;
	using Common.TableDef;

	/// <summary>
	///		Summary description for VendorMaintenanceControl.
	/// </summary>
	public partial class VendorMaintenanceControl : System.Web.UI.UserControl
	{

		private CAccount caccount;
		private CAccountDataSet.CAccountRow rowCAccount;

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

		public int AccountID 
		{
			get 
			{
				if(this.ViewState["AccountID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["AccountID"]);
			}
			set 
			{
				this.ViewState["AccountID"] = value;
			}
		}

		public CAccount oCAccount 
		{
			get 
			{
				return this.caccount;
			}
			set 
			{
				this.caccount = value;
			}
		}

		#region Fields

		private string VendorNumber 
		{
			get 
			{
				return this.tbxVendorNumber.Text;
			}
			set 
			{
				this.tbxVendorNumber.Text = value;
			}
		}

		private string VendorSiteName 
		{
			get 
			{
				return this.tbxVendorSiteName.Text;
			}
			set 
			{
				this.tbxVendorSiteName.Text = value;
			}
		}

		private string VendorPayGroup 
		{
			get 
			{
				return this.tbxVendorPayGroup.Text;
			}
			set 
			{
				this.tbxVendorPayGroup.Text = value;
			}
		}

		#endregion

		public override void DataBind()
		{
			if(this.AccountID != 0 && this.oCAccount != null) 
			{
				LoadData();
				SetValue();
			} 
			else 
			{
				SetValueEmpty();
			}
		}

		private void LoadData() 
		{
			rowCAccount = this.oCAccount.dataSet.CAccount.FindById(this.AccountID);
		}

		private void SetValue() 
		{
			this.VendorNumber = rowCAccount.VendorNumber;
			this.VendorSiteName = rowCAccount.VendorSiteName;
			this.VendorPayGroup = rowCAccount.VendorPayGroup;
		}

		private void SetValueEmpty() 
		{
			this.VendorNumber = String.Empty;
			this.VendorSiteName = String.Empty;
			this.VendorPayGroup = String.Empty;
		}

		public void Save() 
		{
			if(this.oCAccount != null) 
			{
				LoadData();

				if(rowCAccount != null) 
				{
					FillVendorInformation(rowCAccount);
				}
			}
		}

		private void FillVendorInformation(CAccountDataSet.CAccountRow row) 
		{
			row.VendorNumber = this.VendorNumber;
			row.VendorSiteName = this.VendorSiteName;
			row.VendorPayGroup = this.VendorPayGroup;
		}
	}
}
