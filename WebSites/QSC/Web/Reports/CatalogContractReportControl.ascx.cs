namespace QSPFulfillment.Reports
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSP.WebControl;
	using Business.Objects;
	using Common;
	using Common.TableDef;
	using QSPFulfillment.CommonWeb;

	/// <summary>
	///		Summary description for CatalogContractReportControl.
	/// </summary>
	public partial class CatalogContractReportControl : QSPFulfillment.MarketingMgt.Control.MarketingMgtControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
		}

		#region Fields

		public int CatalogID
		{
			get
			{
				return Convert.ToInt32(ddlCatalogID.SelectedValue.ToString());
			}
		}

		public string CatalogName
		{
			get
			{
				return ddlCatalogID.SelectedItem.Text.ToString();
			}
		}

		public int CatalogIDLastSeason
		{
			get
			{
				return Convert.ToInt32(ddlCatalogIDLastSeason.SelectedValue.ToString());
			}
		}

		public string ReportType
		{
			get
			{
				return ddlReportType.SelectedValue.ToString();
			}
		}

		#endregion

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

		protected void ddlCatalogID_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				DataTable Table = new DataTable();
				this.Page.BusCatalog.SelectSearch(Table,String.Empty, String.Empty, 0, String.Empty, 0, String.Empty, 0, 0, String.Empty);
				this.ddlCatalogID.DataSource = Table;
				this.ddlCatalogID.DataTextField = "Program_Type";
				this.ddlCatalogID.DataValueField = "Program_ID";
				this.ddlCatalogID.DataBind();
			}
		}

		protected void ddlCatalogIDLastSeason_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				DataTable Table = new DataTable();
				this.Page.BusCatalog.SelectSearch(Table, String.Empty, String.Empty, 0, String.Empty, 0, String.Empty, 0, 0, String.Empty);
				this.ddlCatalogIDLastSeason.DataSource = Table;
				this.ddlCatalogIDLastSeason.DataTextField = "Program_Type";
				this.ddlCatalogIDLastSeason.DataValueField = "Program_ID";
				this.ddlCatalogIDLastSeason.DataBind();
			}
		}

		protected void btnPreview_Click(object sender, System.EventArgs e)
		{
			Byte[] catalogContractExcel;

			CatalogContract catalogContract = new CatalogContract();
			catalogContractExcel = catalogContract.GetCatalogContract(CatalogID, CatalogIDLastSeason, ReportType);

			SetResponse(catalogContractExcel);
		}

		private void SetResponse(byte[] catalogContractExcel) 
		{
			Response.ClearContent();
			Response.AddHeader("content-disposition", "attachment;filename=CatalogContract" + CatalogName + ReportType + ".xls");
			//Response.AppendHeader("content-length", catalogContractExcel.Length.ToString());
			Response.ContentType = "application/vnd.xls";
			//Response.Charset = "";
			Response.BinaryWrite(catalogContractExcel);
			Response.Flush();
			Response.Close();
		}	
	}
}
