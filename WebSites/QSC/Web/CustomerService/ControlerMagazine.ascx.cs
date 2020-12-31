namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess;

	/// <summary>
	///		Summary description for ControlerMagazine.
	/// </summary>
	public class ControlerMagazine : CustomerServiceControlDataGrid
	{
		protected DataGridObject dtgMain;
		protected Label lblMessage;
		

		#region event page
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(IsOnlyMagazine) 
			{
				this.dtgMain.Columns[0].HeaderText = "Title Code";
				this.dtgMain.Columns[1].HeaderText = "Title";
			} 
			else 
			{
				this.dtgMain.Columns[0].HeaderText = "Product Code";
				this.dtgMain.Columns[1].HeaderText = "Product Name";
			}
		}	
		private void Page_PreRender(object sender, EventArgs e)
		{
			
			/*if(IsSelectOnly && !IsPostBack)
			{
				//this.dtgMain.Columns[2].Visible = false;
				//this.dtgMain.ShowFooter = false;
				//this.dtgMain.Columns[3].Visible = true;
				DataBind();
			}*/
			if(!IsPostBack)
				DataBind();

		}

		#endregion
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			
			InitializeComponent();
			base.OnInit(e,this.dtgMain,lblMessage);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.dtgMain.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dtgMain_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);
			this.PreRender += new System.EventHandler(this.Page_PreRender);

		}
		#endregion
	
		#region override base
		
		protected override void LoadData()
		{
			DataSource = new DataTable("Magazine");
			
			if(List == null)
			{
					List = new ParameterValueList();
			}
			if((IsSwitchLetter && !IsPostBack) || (IsSwitchLetter && this.Page.NewSearch))
			{
				List.Add(new ParameterValue("iStatus",((int)MagazineStatus.Inactive).ToString()));
			}
			this.Page.BusSearch.SelectSearchMagazine(DataSource,this.List);	
		
		}
		#endregion
		#region Get or set
		private int GetTitleCode(DataGridCommandEventArgs e)
		{
			return Convert.ToInt32(e.CommandArgument);
		}
		private string GetTitle(DataGridItem Item)
		{
			try
			{
				return ((TextBox)Item.FindControl("tbxDescriptionUpdate")).Text;
			}
			catch
			{
				return "";
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
		# endregion

		private void dtgMain_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.SelectedItem ||e.Item.ItemType == ListItemType.AlternatingItem) 
			{
				System.Web.UI.HtmlControls.HtmlAnchor htmlAnchor;

				htmlAnchor = (System.Web.UI.HtmlControls.HtmlAnchor )e.Item.FindControl("hypSelect");

				if(htmlAnchor != null)
				{
					DataRowView view = (DataRowView)e.Item.DataItem;
					htmlAnchor.Attributes.Add("onclick","javascript:"+GetJavaScriptFunction()+"('"+view["Product_Code"]+"','"+view["Product_sort_name"].ToString().Replace("'","\\'")+"')");
						
				}
					
			}
		}
		private bool IsSwitchLetter 
		{
			get
			{
				
					if(Request.QueryString["SwitchLetter"] != null)
						return Convert.ToBoolean(Request.QueryString["SwitchLetter"]);
					else
						return false;
				
			}
			
		}
		private string GetJavaScriptFunction()
		{
			if(Request.QueryString["Fct"] != null)
				return Request.QueryString["Fct"];
			else
				return "SetTitleCode";
		}
		protected override void AddJavaScript()
		{


			if(!this.Page.IsClientScriptBlockRegistered("GetJavaScriptFunction()"))
			{
				System.Text.StringBuilder SB = new System.Text.StringBuilder();
				SB.Append("<script language=\"javascript\">");
				SB.Append("function "+GetJavaScriptFunction()+"(Code,Description)");
				SB.Append("{");
				SB.Append("	window.opener."+GetJavaScriptFunction()+"(Code,Description);");
				SB.Append("	self.close();");
				SB.Append("}");
				SB.Append("</script>");
				this.Page.RegisterClientScriptBlock("GetJavaScriptFunction()",SB.ToString());
			}
		


		}
	}
}