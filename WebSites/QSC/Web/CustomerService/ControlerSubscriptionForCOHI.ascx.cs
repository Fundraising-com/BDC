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

	/// <summary>
	///		Summary description for ControlerAddressHistory.
	/// </summary>
	public class ControlerSubscriptionForCOHI :  CustomerServiceControlDataGrid
	{
		public QSPFulfillment.CustomerService.DataGridObject dtgMain;
		protected System.Web.UI.WebControls.DataList dtlMain;
		protected System.Web.UI.WebControls.Label lblMessage;

		private DataGridItem dgiHeader;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(ShowCheckBoxesForCHADD) 
			{
				AddJavaScript();
				AddJavaScriptEventHandlers();
			} 
			else 
			{
				dtgMain.Columns[0].Visible = false;
			}

			dtgMain.Columns[1].Visible = ShowPrices;
		}

		protected override void AddJavaScript()
		{
			string script;

			base.AddJavaScript();

			script =  "<script language=\"javascript\">\n";
			script += "  function CheckAllDataGridCheckBoxes(aspCheckBoxID, checkVal) {\n";
			script += "    re = new RegExp(':' + aspCheckBoxID + '$')  //generated control\n";
			script += "    for(i = 0; i < document.forms[0].elements.length; i++) {\n";
			script += "      elm = document.forms[0].elements[i]\n";
			script += "      if (elm.type == 'checkbox') {\n";
			script += "        if (re.test(elm.name)) {\n";
			script += "          elm.checked = checkVal\n";
			script += "        }\n";
			script += "      }\n";
			script += "    }\n";
			script += "  }\n";
			script += "</script>\n";

			this.Page.RegisterClientScriptBlock("", script);
		}

		private void AddJavaScriptEventHandlers() 
		{
			HtmlInputCheckBox chkAllItems;

			if(dgiHeader != null) 
			{
				chkAllItems = ((HtmlInputCheckBox) dgiHeader.FindControl("chkAllItems"));
				chkAllItems.Attributes["onclick"] += "CheckAllDataGridCheckBoxes('chkSelect', document.forms[0]." + chkAllItems.ClientID + ".checked);";
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e,dtgMain,lblMessage);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);
			this.dtgMain.ItemCreated += new DataGridItemEventHandler(dtgMain_ItemCreated);
		}
		#endregion
		
		private void dtgMain_ItemCreated(object sender, DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Header) 
			{
				this.dgiHeader = e.Item;
			}
		}

		protected override void LoadData()
		{
			DataSource = new DataTable("Subscription");
			if(!CreditCardBounced)
			{
				this.Page.BusCustomerOrderDetail.SelectSubscriptionForChadd(DataSource,this.Page.OrderInfo.CustomerOrderHeaderInstance, this.Page.OrderInfo.TransID, ShowCancelledSubs, ShowCurrentSubscription);
				if(DataSource.Rows.Count == 0) 
				{
					this.Visible = false;
				}
			}
			else
			{
				DataTable Table = new DataTable("List");
				Table.Columns.Add("CustomerOrderHeaderInstance");
				DataRow row =Table.NewRow();
				row["CustomerOrderHeaderInstance"] = this.Page.OrderInfo.CustomerOrderHeaderInstance;
				Table.Rows.Add(row);
				this.Page.BusSearch.SelectSearchCreditCardDetails(DataSource,Table);
			}
		}

		public bool ShowCheckBoxesForCHADD
		{
			get
			{
				if(ViewState["ShowCheckBoxesForCHADD"] == null)
					return false;

				return Convert.ToBoolean(ViewState["ShowCheckBoxesForCHADD"]);
			}
			set 
			{
				ViewState["ShowCheckBoxesForCHADD"] = value;
			}
		}

		public bool ShowPrices
		{
			get 
			{
				if(ViewState["ShowPrices"] == null)
					return false;

				return Convert.ToBoolean(ViewState["ShowPrices"]);
			}
			set 
			{
				ViewState["ShowPrices"] = value;
			}
		}

		public bool ShowCancelledSubs
		{
			get 
			{
				if(ViewState["ShowCancelledSubs"] == null)
					return false;

				return Convert.ToBoolean(ViewState["ShowCancelledSubs"]);
			}
			set 
			{
				ViewState["ShowCancelledSubs"] = value;
			}
		}

		public bool ShowCurrentSubscription
		{
			get 
			{
				if(ViewState["ShowCurrentSubscription"] == null)
					return true;

				return Convert.ToBoolean(ViewState["ShowCurrentSubscription"]);
			}
			set 
			{
				ViewState["ShowCurrentSubscription"] = value;
			}
		}

		public bool CreditCardBounced
		{
			get
			{
				if(ViewState["CreditCardBounced"] == null)
					return false;

				return Convert.ToBoolean(ViewState["CreditCardBounced"]);
			}
			set
			{
				ViewState["CreditCardBounced"] = value;
			}
		}

		public override void DataBind()
		{
			base.DataBind ();
		}
	}
}
