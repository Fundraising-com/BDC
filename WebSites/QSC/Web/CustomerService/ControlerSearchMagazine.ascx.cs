namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common.TableDef;

	/// <summary>
	///		Summary description for ControlerSearchProblemCode.
	/// </summary>
	/// 
	
	public partial class ControlerSearchMagazine : ControlSearch
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(IsOnlyMagazine)
			{
				this.ddlProductType.Enabled=false;
				this.lblSearchDescription.Text = "Title";
			} 
			else 
			{
				this.lblSearchDescription.Text = "Product Name";
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
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

		

	
		public override SearchMultiPage ResultType
		{
			get
			{
				return SearchMultiPage.None;
				
			}
		}
		protected override ParameterValueList GetValueToSearch()
		{
			ParameterValueList List = new ParameterValueList();
			
			AddParameterValue(this.Controls,List,"");

			return List;
		}

		protected void ddlProductType_Load(object sender, System.EventArgs e)
		{
			if(ddlProductType.Items.Count ==0)
			{
				DataTable Table = new DataTable("Product");
				this.Page.BusCodeDetail.SelectByCodeHeaderInstance(Table,46000);
				this.ddlProductType.DataSource = Table;
				this.ddlProductType.DataTextField = CodeDetailTable.FLD_DESCRIPTION;
				this.ddlProductType.DataValueField = CodeDetailTable.FLD_INSTANCE;
				this.ddlProductType.DataBind();
			}
		}

		
	
		private bool IsOnlyMagazine
		{
			get
			{
				
				if(Request.QueryString["IsOnlyMagazine"] != null)
					return Convert.ToBoolean(Request.QueryString["IsOnlyMagazine"]);
				else
					return false;
				
			}
			
			
		}
	}
}
