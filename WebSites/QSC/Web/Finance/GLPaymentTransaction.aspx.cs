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
    public partial class GLPaymentTransaction : QSPFulfillment.CommonWeb.QSPPage
    {
        protected InvoiceListDataAccess aInvoiceDataAccess;
        protected DataSet ds = new DataSet();
        protected DataView dv = new DataView();
        protected int m_InvoiceID;
        protected int m_PaymentID;        

        public GLPaymentTransaction()
        {
            aInvoiceDataAccess = new InvoiceListDataAccess();
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            m_InvoiceID = Convert.ToInt32(Request.QueryString["InvoiceID"]);           
            m_PaymentID = Convert.ToInt32(Request.QueryString["PaymentID"]);
            if (!IsPostBack)
            {
                BindGrid(m_InvoiceID, m_PaymentID);
            }
        }

        public void BindGrid(int nInvoiceID, int nPaymentID)
        {
            ds = aInvoiceDataAccess.GetGLEntriesByPayment(nInvoiceID, nPaymentID);
            dv = ds.Tables[0].DefaultView;
            GLDG.DataSource = dv;
            GLDG.DataBind();
            if (ds.Tables[0].DefaultView.Count >= 2)//Don't allow users to enter more than one debit/credit
            {
                GLDG.ShowFooter = false;
            }
        }

        public void DataGrid_ItemCreated(Object sender, DataGridItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Footer:
                    Label lbl = (Label)e.Item.FindControl("add_InvoiceID");
                    Label lbl2 = (Label)e.Item.FindControl("add_PaymentID");
                    TextBox tb = (TextBox)e.Item.FindControl("add_Amount");
                    lbl.Text = Request.QueryString["InvoiceID"] == "0" ? Request.QueryString["OrderID"] : Request.QueryString["InvoiceID"];
                    lbl2.Text = Request.QueryString["PaymentID"];
                    tb.Text = Request.QueryString["Amount"];
                    break;
                default:
                    break;
            }
        }

        public void DataGrid_ItemDataBound(Object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                e.Item.Cells[1].Text = Request.QueryString["InvoiceID"] == "0" ? "<font color='white'>Order<br>ID</font>" : e.Item.Cells[1].Text;                
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {

                //GL Number
                string sGLAcctNumberTextBoxName = e.Item.Cells[1].FindControl("add_GlAcctNumber").ClientID;
                string sGLAcctNumberTextBoxValue = e.Item.Cells[1].FindControl("add_GlAcctNumberValue").ClientID;
                //GL Description
                string sGLAcctDescTextBoxName = e.Item.Cells[2].FindControl("add_GlAcctDecription").ClientID;
                string sGLAcctDescTextBoxValue = e.Item.Cells[2].FindControl("add_GlAcctDescriptionValue").ClientID;
                //GL AccountID
                string sGLAccountIDTextBoxName = e.Item.Cells[2].FindControl("add_GLAccountID").ClientID;
                string sGLAccountIDTextBoxValue = e.Item.Cells[2].FindControl("add_GlAccountIDValue").ClientID;

                HyperLink hl = (HyperLink)e.Item.Cells[1].FindControl("lnk_add_GlAcctNumber");
                hl.NavigateUrl = "javascript:ddl_window=window.open('GLAccountDropDownList.aspx?CtrlGLAccountNumber=" + sGLAcctNumberTextBoxName + "&CtrlGLAccountNumberValue=" + sGLAcctNumberTextBoxValue + "&CtrlGLAccountDescription=" + sGLAcctDescTextBoxName + "&CtrlGLAccountDescriptionValue=" + sGLAcctDescTextBoxValue + "&CtrlGLAccountID=" + sGLAccountIDTextBoxName + "&CtrlGLAccountIDValue=" + sGLAccountIDTextBoxValue + "','ddl_window','width=295,height=50,left=300,top=250');ddl_window.focus();";
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
