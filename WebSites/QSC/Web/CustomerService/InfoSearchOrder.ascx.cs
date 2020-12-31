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
	///		Summary description for OrderInfoSearch.
	/// </summary>
	public partial class InfoSearchOrder : CustomerServiceControl,ISearch
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected QSP.WebControl.DropDownListSearch DropDownList3;
		protected QSPFulfillment.CommonWeb.UC.DateEntry ctrlDateEntryFrom;
		protected QSPFulfillment.CommonWeb.UC.DateEntry ctrlDateEntryTo;
		protected InfoSearchFM ctrlInfoSearchFM;
		protected QSP.WebControl.TextBoxSearch tbxTitle;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.CompareValidator CompareValidator2;
		protected QSP.WebControl.TextBoxSearch tbxTitleCode;
		protected System.Web.UI.WebControls.Label Label14;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			
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


		public ParameterValueList GetParameterValue(string StartParameterName)
		{

		
			ParameterValueList List = this.ctrlInfoSearchFM.GetParameterValue("");
		
			AddParameterValue(this.Controls,List,StartParameterName);

			return List;

		}
		public ParameterValueList GetParameterFilter(string StartParameterName)
		{
			ParameterValueList list = new ParameterValueList();

			if(this.tbxProductCode.Text != string.Empty)
				list.Add(new ParameterValue("ProductCode",this.tbxProductCode.Text));
			
			return list; 
		}						  

		
		public override bool Validate()
		{	
		
			return	ValidFromTo(ctrlDateEntryFrom.Date,ctrlDateEntryTo.Date);
		}

		protected void ddlOrderType_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				DataTable Table = new DataTable();
				this.Page.BusCodeDetail.SelectByCodeHeaderInstance(Table,41000);
				Table.Rows.InsertAt(Table.NewRow(),0);
				this.ddlOrderType.DataSource = Table;
				this.ddlOrderType.DataTextField = CodeDetailTable.FLD_DESCRIPTION;
				this.ddlOrderType.DataValueField = CodeDetailTable.FLD_INSTANCE;
				this.ddlOrderType.DataBind();
			}
		}

		protected void ddlQualifierName_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				DataTable Table = new DataTable();
				this.Page.BusCodeDetail.SelectByCodeHeaderInstance(Table,39000);
				Table.Rows.InsertAt(Table.NewRow(),0);
				this.ddlQualifierName.DataSource = Table;
				this.ddlQualifierName.DataTextField = CodeDetailTable.FLD_DESCRIPTION;
				this.ddlQualifierName.DataValueField = CodeDetailTable.FLD_INSTANCE;
				this.ddlQualifierName.DataBind();
			}
		}

		protected void ddlOrderStatus_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				DataTable Table = new DataTable();
				this.Page.BusIncidentStatus.SelectAll(Table);
				Table.Rows.InsertAt(Table.NewRow(),0);
				this.ddlOrderStatus.DataSource = Table;
				this.ddlOrderStatus.DataTextField = IncidentStatusTable.FLD_DESCRIPTION;
				this.ddlOrderStatus.DataValueField = IncidentStatusTable.FLD_INSTANCE;
				this.ddlOrderStatus.DataBind();
			}
		}
		
		public int ItemType
		{
			get
			{
				return Convert.ToInt32(this.ddlProductType.SelectedItem.Value);
			}
			set{this.ddlProductType.SelectedIndex = value;}
		}
		protected override void AddJavaScript()
		{
			hypFindProductCode.Attributes.Add("onclick","javasrcipt:Open('Magazine.aspx?IsNewWindow=true&ID=true&Fct=SetProductCode');");
			AddSetProductCode();
		}
		private void AddSetProductCode()
		{
			if(!this.Page.IsClientScriptBlockRegistered("SetProductCode"))
			{
				System.Text.StringBuilder SB = new System.Text.StringBuilder();
				SB.Append("<script language=\"javascript\">");
				SB.Append("function SetProductCode(PCID,Desc)");
				SB.Append("{");
				SB.Append("	var tbxProductCode = document.getElementById('"+this.tbxProductCode.UniqueID.Replace("?","_")+"');");
				SB.Append("	tbxProductCode.value = PCID;");
				SB.Append("}");
				SB.Append("</script>");
				this.Page.RegisterClientScriptBlock("SetProductCode",SB.ToString());
			}
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

		
		

		

	
		

		

		

		
	}
}
