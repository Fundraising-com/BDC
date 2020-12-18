using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//using EFundraisingCRMWeb.App_Data;

namespace EFundraisingCRMWeb.Components.User.Sales
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    public delegate void saleSelect(object sender, System.EventArgs e);

    /// <summary>
    ///		Summary description for SaleInfoList.
    /// </summary>
    public partial class SaleInfoList : System.Web.UI.UserControl
    {

        public event saleSelect OnSaleSelect;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here
        }


        //returns number of sales
        public int DoDataBind(efundraising.EFundraisingCRM.SaleCollection saleCol)
        {
            SaleInfoDataGrid.DataSource = saleCol;
            SaleInfoDataGrid.DataBind();

            if (saleCol == null || saleCol.Count < 1)
            {
                return 0;
            }
            else
            {
                return saleCol.Count;
            }


        }


        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            //this.SaleInfoDataGrid.ItemCommand +=new DataGridCommandEventHandler(SaleInfoDataGrid_ItemCommand);
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SaleInfoDataGrid.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.SaleInfoDataGrid_PageIndexChanged);
            this.SaleInfoDataGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.SaleInfoDataGrid_ItemDataBound);

        }
        #endregion

        /*private void SaleInfoDataGrid_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			if (OnSaleSelect != null)
			{				
				OnSaleSelect(SaleInfoDataGrid.DataKeys[e.Item.ItemIndex].ToString(), e);
			}
		}*/


        public void SaleIDLink_Click(object sender, System.EventArgs e)
        {
            //Display Details for lead ID clicked
            LinkButton img = (LinkButton)sender;
            TableCell cell = (TableCell)img.Parent;
            DataGridItem item = (DataGridItem)cell.Parent;

            OnSaleSelect(SaleInfoDataGrid.DataKeys[item.ItemIndex].ToString(), e);


        }

        //TO DO BINDER SUR UNE DATATABLE......representant le contenu
        private void SaleInfoDataGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //get product class
                int saleID = Convert.ToInt32(e.Item.Cells[0].Text);
                string productClass = EFundraisingCRMWeb.Components.Server.ManageClient.GetSalesProductClass(saleID);

                //add tax to totalamount
                //get taxes
                decimal totalTax = 0;
                efundraising.EFundraisingCRM.ApplicableTax[] taxes = efundraising.EFundraisingCRM.ApplicableTax.GetApplicableTaxByID(saleID);
                foreach (efundraising.EFundraisingCRM.ApplicableTax tax in taxes)
                {
                    totalTax += Convert.ToDecimal(tax.TaxAmount);
                }


                //la tax est deja dans le total amt de la sp (du moins ds le cas du client 1099 ca)
                //e.Item.Cells[4].Text = (Convert.ToDecimal(e.Item.Cells[4].Text.Remove(0,1)) + totalTax).ToString("C"); 
                e.Item.Cells[4].Text = (Convert.ToDecimal(e.Item.Cells[4].Text.Remove(0, 1))).ToString("C");

                //get receivable
                decimal receivable = efundraising.EFundraisingCRM.Payment.GetPaymentReceivableBySaleID(saleID);
                //receivable += totalTax; 

                e.Item.Cells[5].Text = receivable.ToString("C");
                e.Item.Cells[3].Text = productClass;

                //get payment term
                int paymentTermID = Convert.ToInt32(e.Item.Cells[6].Text);
                efundraising.EFundraisingCRM.PaymentTerm term = efundraising.EFundraisingCRM.PaymentTerm.GetPaymentTermByID(paymentTermID);

                //get payment due date
                DateTime paymentDueDate = Convert.ToDateTime(e.Item.Cells[7].Text);
                if (paymentDueDate == DateTime.MinValue)
                {
                    e.Item.Cells[7].Text = "";
                }

                e.Item.Cells[6].Text = term.Description;

            }
        }

        private void SaleInfoDataGrid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {

            //get sale collection from session
            efundraising.EFundraisingCRM.SaleCollection sales = (efundraising.EFundraisingCRM.SaleCollection)Session[Global.SessionVariables.SALE_COLLECTION];
            SaleInfoDataGrid.CurrentPageIndex = e.NewPageIndex;
            this.DoDataBind(sales);

        }
    }
}
