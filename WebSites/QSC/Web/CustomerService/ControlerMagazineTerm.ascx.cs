namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common;
	
	public delegate void SelectMagazineEventHandler(object sender, SelectMagazineClickedArgs e);
	/// <summary>
	///		Summary description for ControlMagazineTerm.
	/// </summary>
	public class ControlerMagazineTerm : CustomerServiceControlDataGrid
	{
		protected System.Web.UI.WebControls.Label lblTitle2;
		protected System.Web.UI.WebControls.Label Label1s;
		protected System.Web.UI.WebControls.TextBox tbxTitleCode;
		protected System.Web.UI.WebControls.Label Label3s;
		protected System.Web.UI.WebControls.TextBox tbxTitle;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox tbxTerm;
		protected System.Web.UI.WebControls.RangeValidator RangeValidator5;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblProductCode;
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label lblTerm;
		protected System.Web.UI.WebControls.Label lblPrice;
		protected System.Web.UI.WebControls.Label lblMagInstance;
		protected System.Web.UI.HtmlControls.HtmlContainerControl divSearch;
		protected DataGridObject dtgMain;
		
		public event SelectMagazineEventHandler SelectMagazineClick;
		private void Page_Load(object sender, System.EventArgs e)
		{
			
		}
		
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			btnSearch.Click +=new EventHandler(btnSearch_Click);
			base.OnInit(e,dtgMain,lblMessage);

		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.dtgMain.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgMain_ItemCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private int GetMagPriceInstance()
		{
			if(Context.Request.QueryString["MagPriceInstance"]!=null)
				return 0;
			
			return Convert.ToInt32(Context.Request.QueryString["MagPriceInstance"]);
		}
		private void btnSearch_Click(object sender, System.EventArgs e)
		{

			this.Page.NewSearch = true;
			this.dtgMain.FilterExpression ="";

			if(GetTitleSearch() != String.Empty)
				this.dtgMain.FilterExpression ="Product_Sort_Name like '%" + GetTitleSearch().Replace("'", "''") +"%'";
			
			if(GetTermSearch() != String.Empty)
			{
				if(this.dtgMain.FilterExpression != string.Empty)
				{
					this.dtgMain.FilterExpression +=  "	and term = "+GetTermSearch();
				}
				else
				{
					this.dtgMain.FilterExpression +=  "term = "+GetTermSearch();
				}
			}

			this.dtgMain.SelectedIndex = -1;

			// TO REMOVE:
			DataBind();
		}
		protected override void LoadData()
		{
			DataSource = new DataTable("Magazine");

			try 
			{
				if(this.ProductCode != "") 
				{
					this.Page.BusProduct.SelectByCampaignTitleCode(DataSource, this.ProductCode,this.Page.OrderInfo.CampaignID,this.ProductType,this.Page.CustomerInfo.CustomerInstance,this.CouponSetID);
				} 
				else 
				{
					this.Page.BusProduct.SelectByCampaignTitleCode(DataSource,GetProductCodeSearch(),this.Page.OrderInfo.CampaignID,this.ProductType,this.Page.CustomerInfo.CustomerInstance,this.CouponSetID);
				}
			}
			catch(System.NullReferenceException ex) 
			{
				bool hasKey = false;

				foreach(string key in Session.Keys) 
				{
					if(key == "CurrentInfoSession") 
					{
						hasKey = true;
					}
				}

				if(hasKey) 
				{
					ex.Source += " Has the session key.";
				} 
				else 
				{
					ex.Source += " Does not have the session key.";
				}

				throw ex;
			}
			
		
		}
		private string GetTitleSearch()
		{
			return this.tbxTitle.Text;
		}
		private string GetProductCodeSearch()
		{
			if(tbxTitleCode.Text == String.Empty)
				return "%";
			return tbxTitleCode.Text;

		
		}
		private string GetTermSearch()
		{
			return this.tbxTerm.Text;
		}

		private void dtgMain_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			if(e.CommandName == "Select")
			{
				if(this.GetNewRenew(e.Item)== "S" && ShowNewRenew)
				{
					this.Page.MessageManager.Add(this.Page.MessageManager.FormatErrorMessage(QSPFulfillment.DataAccess.Common.Message.VALMSG_REQUIRED_FIELD_VAR_1,"New/Renew"));
					this.Page.MessageManager.PrepareErrorMessage();
					this.Page.SetPageError();
				
					
				}
				else
				{
					SelectMagazineClickedArgs args;
				
					if(this.dtgMain.Columns[this.dtgMain.Columns.Count-1].Visible)
						args =  new SelectMagazineClickedArgs(new QSPFulfillment.DataAccess.Common.ActionObject.Magazine(this.GetTitle(e.Item),this.GetMagInstance(e.Item),this.GetProductCode(e.Item),this.GetPrice(e.Item),this.GetTerm(e.Item),this.GetNewRenew(e.Item), this.GetProductType(e.Item)));
					else
						args =  new SelectMagazineClickedArgs(new QSPFulfillment.DataAccess.Common.ActionObject.Magazine(this.GetTitle(e.Item),this.GetMagInstance(e.Item),this.GetProductCode(e.Item),this.GetPrice(e.Item),this.GetTerm(e.Item), this.GetProductType(e.Item)));
				
					if(SelectMagazineClick != null)
						SelectMagazineClick(source,args);
				}
			}
		}
		private string GetTitle(DataGridItem e)
		{
			return ((Label)e.FindControl("lblTitle")).Text;
		}
		private int GetMagInstance(DataGridItem e)
		{
			return Convert.ToInt32(((Label)e.FindControl("lblMagInstance")).Text);
		}								  
		private string GetProductCode(DataGridItem e)
		{
			return ((Label)e.FindControl("lblProductCode")).Text;
		}
		private float GetPrice(DataGridItem e)
		{
			return Convert.ToSingle(((Label)e.FindControl("lblPrice")).Text);
		}
		private int GetTerm(DataGridItem e)
		{
			return Convert.ToInt32(((Label)e.FindControl("lblTerm")).Text);
		}
		private string GetNewRenew(DataGridItem e)
		{
			return ((DropDownList)e.FindControl("ddlNewRenew")).SelectedItem.Value;
		}
		private int GetProductType(DataGridItem e) 
		{
			return Convert.ToInt32(((Label)e.FindControl("lblProductType")).Text);
		}
		public int CouponSetID
		{
			get
			{
				if(ViewState["CouponSetID"] == null)
					return 0;

				return Convert.ToInt32(ViewState["CouponSetID"]);
			}
			set
			{
				this.ViewState["CouponSetID"]= value;
			}
		}
		public int ProductType
		{
			get 
			{
				if(ViewState["ProductType"] == null)
					return 46001;

				return Convert.ToInt32(ViewState["ProductType"]);
			}
			set 
			{
				this.ViewState["ProductType"] = value;
			}
		}
		public bool ShowNewRenew
		{
			get
			{
				if(ViewState["ShowNewRenew"] == null)
					return false;

				return Convert.ToBoolean(ViewState["ShowNewRenew"]);
			}
			set
			{
				this.ViewState["ShowNewRenew"]= value;
				this.dtgMain.Columns[this.dtgMain.Columns.Count-2].Visible = value;
			}
		}

		public bool ShowSearch 
		{
			get 
			{
				return divSearch.Visible;
			}
			set 
			{
				divSearch.Visible = value;
			}
		}

		public string ProductCode 
		{
			get 
			{
				if(ViewState["ProductCode"] == null)
					return "";

				return ViewState["ProductCode"].ToString();
			}
			set 
			{
				ViewState["ProductCode"] = value;
			}
		}
	}
}


	

