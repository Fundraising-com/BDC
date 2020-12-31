namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Microsoft.Web.UI.WebControls;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common.TableDef;

	/// <summary>
	///		Summary description for ControlerToolBar.
	/// </summary>
	public partial class ControlerAction : CustomerServiceControl
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected ActionTable Table = new ActionTable();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			string script;

			Ajax.Utility.RegisterTypeForAjax(typeof(CustomerServicePage));
			
			script  = "<script language=\"javascript\">\n";
			script += "  function Action(id)\n";
			script += "  {\n";
			script += "    var pageSwitchStateID = CustomerServicePage.SavePageSwitchStateFromClient(document.forms[0].elements[\"__VIEWSTATE\"].value).value;\n";
			script += "    if(id == 0) {\n";
			script += "      var ddlAction = document.getElementById('ctrlControlerAction_ddlAction');\n";
			script += "      Open('action/default.aspx?IsNewWindow=true&PageSwitchStateID=' + pageSwitchStateID + '&Action='+ddlAction.options[ddlAction.selectedIndex].value);\n";
			script += "    } else {\n";
			script += "      Open('action/default.aspx?IsNewWindow=true&PageSwitchStateID=' + pageSwitchStateID + '&Action='+id);\n";
			script += "    }\n";
			script += "    var hidDataBind = document.getElementById('hidDataBind');\n";
			script += "    hidDataBind.value = id;\n";
			script += "  }\n";
			script += "</script>\n";

			this.Page.RegisterClientScriptBlock("OpenAction", script);
		}
		protected void Page_PreRender(object sender, EventArgs e)
		{
			

			if(!this.Page.ActionReasonEntered)
			{
				Enabled = false;
			}
			else
			{
				LoadData();
				SetValueDDLAction();
				InternalEnabled();
			}
		}
		

		public bool Enabled
		{
			get
			{
				return this.hylGo.Enabled;
			}
			set
			{
				
				this.ddlAction.Enabled = value;
				this.hylGo.Enabled = value;
				this.hylGo.DataBind();
				this.hylRefund.Enabled = value;
				this.hylRefund.DataBind();
				this.hypCancel.Enabled = value;
				this.hypCancel.DataBind();
				this.hypNew.Enabled = value;
				this.hypNew.DataBind();
				this.hypNoAction.Enabled = value;
				this.hypNoAction.DataBind();
				this.hypNotByPhone.Enabled = value;
				this.hypNotByPhone.DataBind();
				this.hypCHADD.Enabled = value;
				this.hypCHADD.DataBind();
				
			}
		}
		
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			DataSource = Table;
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

		private void LoadData()
		{
			this.Page.BusAction.SelectAll(DataSource,this.Page.OrderInfo.CustomerOrderHeaderInstance,this.Page.OrderInfo.TransID, (this.Page.ResultType == SearchMultiPage.CreditCard));
			
		}
		private void SetValueDDLAction()
		{
			this.ddlAction.Items.Clear();
			this.ddlAction.Enabled = (DataSource.Rows.Count > 0);

			foreach(DataRow row in DataSource.Rows)
			{
                if (Convert.ToInt32(row[ActionTable.FLD_INSTANCE]) == (int)QSPFulfillment.DataAccess.Business.Action.CancelSubBeforeRemit)
				{
					this.ddlAction.Items.Add(new ListItem("Cancel Sub",row[ActionTable.FLD_INSTANCE].ToString()));	
				}
				else
				{
					this.ddlAction.Items.Add(new ListItem(row[ActionTable.FLD_DESCRIPTION].ToString(),row[ActionTable.FLD_INSTANCE].ToString()));	
				}
			}
		}

		private void InternalEnabled()
		{
			//int ss = DataSource.Select(ActionTable.FLD_INSTANCE + "="+((int)Action.IssueCustomerRefund).ToString()).Length ;
			
			this.hylGo.Enabled = true;
			this.hylGo.DataBind();
            this.hylRefund.Enabled = (DataSource.Select(ActionTable.FLD_INSTANCE + " = " + ((int)QSPFulfillment.DataAccess.Business.Action.IssueCustomerRefund).ToString()).Length == 0 ? false : true);
			this.hylRefund.DataBind();
			SetHyperlinkCancel();
            this.hypNew.Enabled = (DataSource.Select(ActionTable.FLD_INSTANCE + " = " + ((int)QSPFulfillment.DataAccess.Business.Action.NewSub).ToString()).Length == 0 ? false : true);
			this.hypNew.DataBind();
            this.hypNoAction.Enabled = (DataSource.Select(ActionTable.FLD_INSTANCE + " = " + ((int)QSPFulfillment.DataAccess.Business.Action.NoActionRequired).ToString()).Length == 0 ? false : true);
			this.hypNoAction.DataBind();
            this.hypNotByPhone.Enabled = (DataSource.Select(ActionTable.FLD_INSTANCE + " = " + ((int)QSPFulfillment.DataAccess.Business.Action.NotifyPublByPhone).ToString()).Length == 0 ? false : true);
			this.hypNotByPhone.DataBind();
            this.hypCHADD.Enabled = (DataSource.Select(ActionTable.FLD_INSTANCE + " = " + ((int)QSPFulfillment.DataAccess.Business.Action.ChangeNameAddress).ToString()).Length == 0 ? false : true);
			this.hypCHADD.DataBind();
			
		}
		private void SetHyperlinkCancel()
		{
            int Cancel = DataSource.Select(ActionTable.FLD_INSTANCE + " = " + ((int)QSPFulfillment.DataAccess.Business.Action.CancelSub).ToString()).Length;
            int CancelSubRemit = DataSource.Select(ActionTable.FLD_INSTANCE + " = " + ((int)QSPFulfillment.DataAccess.Business.Action.CancelSubBeforeRemit).ToString()).Length;
			if(Cancel ==1 || CancelSubRemit == 1)
			{
				this.hypCancel.Enabled  = true;

				if(Cancel == 1)
				{
					this.hypCancel.NavigateUrl  = "javascript:Action(1);";
				}
				else
				{
					this.hypCancel.NavigateUrl  = "javascript:Action(14);";
				}
			}
			else
			{
				this.hypCancel.Enabled  = false;
			}
		
			this.hypCancel.DataBind();
			
		}
	}
}
