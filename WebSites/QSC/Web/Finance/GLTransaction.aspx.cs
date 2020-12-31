using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Configuration;
using DAL;

namespace QSPFulfillment.Finance
{
	public partial class GLTransaction : QSPFulfillment.CommonWeb.QSPPage
	{
		protected InvoiceListDataAccess aInvoiceDataAccess;
		protected DataSet ds  = new DataSet();
		protected DataView dv = new DataView();
		protected Label add_InvoiceID;
		protected int m_InvoiceID;
		protected int m_AdjustmentID;
		protected bool bDidOne = false;

		public GLTransaction()
		{
			aInvoiceDataAccess = new InvoiceListDataAccess();
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			m_InvoiceID		= Convert.ToInt32(Request.QueryString["InvoiceID"]);
			m_AdjustmentID	= Convert.ToInt32(Request.QueryString["AdjustmentID"]);

			if (!IsPostBack)
			{
				BindGrid(m_InvoiceID,m_AdjustmentID);
			}
		}

		public void BindGrid(int nInvoiceID, int nAdjustmentID)
		{
			ds = aInvoiceDataAccess.GetGLEntriesByAdjustment(nInvoiceID,nAdjustmentID);
			dv = ds.Tables[0].DefaultView;
			GLDG.DataSource = dv;
			GLDG.DataBind();
			if(ds.Tables[0].DefaultView.Count >= 2 && bDidOne == false)
			{
				GLDG.ShowFooter=false;
			}
			CheckBalance(m_AdjustmentID);
		}

		public void DataGrid_ItemCreated(Object sender, DataGridItemEventArgs e)
		{
			switch(e.Item.ItemType)
			{
				case ListItemType.Footer:
					Label lbl  = (Label)e.Item.FindControl("add_GLEntryID");
					Label lbl2 = (Label)e.Item.FindControl("add_AdjustmentID");
					TextBox tb = (TextBox)e.Item.FindControl("add_Amount");
					lbl.Text   = Request.QueryString["GLEntryID"];
					lbl2.Text  = Request.QueryString["AdjustmentID"];
					tb.Text    = Request.QueryString["Amount"];

					WebControl button = (WebControl)e.Item.FindControl("btnAdd");
					button.Attributes.Add("onclick", "return confirm('Are you sure you want to add this adjustment?');");
					break;
				default:
					break;
			}
		}

		public void DataGrid_ItemDataBound(Object sender, DataGridItemEventArgs e)
		{
			if ( e.Item.ItemType == ListItemType.Footer )
			{
				//GL Number
				string sGLAcctNumberTextBoxName	 = e.Item.Cells[1].FindControl("add_GlAcctNumber").ClientID;
				string sGLAcctNumberTextBoxValue = e.Item.Cells[1].FindControl("add_GlAcctNumberValue").ClientID;
				//GL Description
				string sGLAcctDescTextBoxName  = e.Item.Cells[2].FindControl("add_GlAcctDecription").ClientID;
				string sGLAcctDescTextBoxValue = e.Item.Cells[2].FindControl("add_GlAcctDescriptionValue").ClientID;
                //GL AccountID
                string sGLAccountIDTextBoxName = e.Item.Cells[2].FindControl("add_GLAccountID").ClientID;
                string sGLAccountIDTextBoxValue = e.Item.Cells[2].FindControl("add_GlAccountIDValue").ClientID;

				HyperLink hl		= (HyperLink)e.Item.Cells[1].FindControl("lnk_add_GlAcctNumber");
                hl.NavigateUrl = "javascript:ddl_window=window.open('GLAccountDropDownList.aspx?CtrlGLAccountNumber=" + sGLAcctNumberTextBoxName + "&CtrlGLAccountNumberValue=" + sGLAcctNumberTextBoxValue + "&CtrlGLAccountDesc=" + sGLAcctDescTextBoxName + "&CtrlGLAccountDescValue=" + sGLAcctDescTextBoxValue + "&CtrlGLAccountID=" + sGLAccountIDTextBoxName + "&CtrlGLAccountIDValue=" + sGLAccountIDTextBoxValue + "','ddl_window','width=295,height=50,left=300,top=250');ddl_window.focus();";
			}
		}

		public void doAddGLEntry(Object sender, DataGridCommandEventArgs e)
		{
			if ( e.CommandName == "Add" )
			{
				Label GLEntryID							= (Label)e.Item.FindControl("add_GLEntryID");
                HtmlInputHidden GLAccountIDValue        = (HtmlInputHidden)e.Item.FindControl("add_GLAccountIDValue");
				DropDownList ddlDebitCredit				= (DropDownList)e.Item.FindControl("add_DebitCredit");
				TextBox Amount							= (TextBox)e.Item.FindControl("add_Amount");
				
				if (Amount.Text.ToString().Trim().Length != 0)
				{
					try
					{
							aInvoiceDataAccess.AddGLTransactionForAdjustment(
								Convert.ToInt32(GLEntryID.Text.Trim()),
                                Convert.ToInt32(GLAccountIDValue.Value.Trim()),
								ddlDebitCredit.SelectedItem.Value.Trim(),
								Convert.ToDecimal(Amount.Text.Trim()));
					}
					catch (Exception exc)
					{
						Response.Write("Message: " + exc.Message+ "<br>");
					}
				}
				else
				{
					lblMsg.Text= "No Amount entered";
				}
			}
			bDidOne = true;
			BindGrid(m_InvoiceID, m_AdjustmentID );
		}

		public void CheckBalance(int nAdjustmentID)
		{
			int nValue = 0;
			nValue = aInvoiceDataAccess.GetGLEntryAdjustmentBalance(nAdjustmentID);

			if(nValue !=0 )
			{
				lblBalance.Text = "GL Entries for this adjustment are not balanced.";
			}
			else
			{
				lblBalance.Text ="";
			}

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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion
	}
}
