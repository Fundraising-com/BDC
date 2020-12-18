using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Xml.Linq;
using System.IO;

namespace EfundraisingCRMWeb.Sales.SalesScreen
{
    public partial class XML : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(@"\\Caefr3k07\SYSTEM\Log");
                FilesDropDownList.DataSource = dirInfo.GetFiles("efrSync*.xml");
                FilesDropDownList.DataBind();
            }


           
        }

        private void UpdatePaymentGrid(bool payment, bool insertion)
        {
            XmlDocument doc = (XmlDocument) Session["doc"];
            XmlNodeList _fnames = null;
            if (payment)
            {
                if (insertion)
                {
                    _fnames = doc.SelectNodes("//Payment_Values/Sale_Found[contains(.,'Success')]");
                }
                else
                {
                    _fnames = doc.SelectNodes("//Payment_Values/Sale_Found[contains(.,'Record')]");
                }
            }
            else
            {
                if (insertion)
                {
                    _fnames = doc.SelectNodes("//Adjustment_Values/Sale_Found[contains(.,'Success')]");
                }
                else
                {
                    _fnames = doc.SelectNodes("//Adjustment_Values/Sale_Found[contains(.,'Record')]");
                }
            }
            ////LAST_Name[contains(., 'JOHN')]

            List<Payment> ps = new List<Payment>();
            foreach (XmlNode node in _fnames)
            {
                Payment p = new Payment();

                string id = node.Attributes["SaleID"].Value;
                string status = node.InnerText;

                p.SaleId = Convert.ToInt32(id);
                p.Status = status;
                ps.Add(p);
            }

            PaymentsGridView.DataSource = ps;
            PaymentsGridView.DataBind();

        }
     

        protected void PaymentButton_Click(object sender, EventArgs e)
        {
            UpdatePaymentGrid(true,false);

            Session["CurrentView"] = "PaymentUpdate";
        }

        protected void PaymentInsertedButton_Click(object sender, EventArgs e)
        {
            Session["CurrentView"] = "PaymentInsert"; 
            UpdatePaymentGrid(true, true);
        }

        protected void AdjustmentUpdatedButton_Click(object sender, EventArgs e)
        {
            UpdatePaymentGrid(false,false);
            Session["CurrentView"] = "AdjUpdate";
        }

        protected void AdjustmentInsertedButton0_Click(object sender, EventArgs e)
        {
            UpdatePaymentGrid(false, true);
            Session["CurrentView"] = "AdjInsert";
        }

        protected void FilesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrids();            
        }

        private void BindGrids()
        {
            //fill a collection with Sales udpates only
            XmlDocument doc = new XmlDocument();
            //doc.Load("EfundraisingCRMWeb/Ressources/" + FilesDropDownList.SelectedItem.Text);
            doc.Load(@"\\Caefr3k07\SYSTEM\Log\" + FilesDropDownList.SelectedItem.Text);
            Session["doc"] = doc;
            //XmlNodeList _fnames = doc.GetElementsByTagName("Sale_Found");
            //xpath
            XmlNodeList _fnames = doc.SelectNodes("//Sale_Found");
            ////LAST_Name[contains(., 'JOHN')]

            List<SaleUpdate> sus = new List<SaleUpdate>();
            foreach (XmlNode node in _fnames)
            {
                SaleUpdate su = new SaleUpdate();

                string id = node.Attributes["SaleID"].Value;
                string changes = node.InnerText;

                su.SaleId = Convert.ToInt32(id);
                su.ShipTracking = changes;
                sus.Add(su);
            }

            
            SaleUpdatesGridView.DataSource = sus;
            SaleUpdatesGridView.DataBind();

            UpdatePaymentGrid(false, true);

        }

        protected void SaleUpdatesGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SaleUpdatesGridView.PageIndex = e.NewPageIndex;
            BindGrids();
        }

        protected void PaymentsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {            
                


            PaymentsGridView.PageIndex = e.NewPageIndex;
            
            string currentView = Session["CurrentView"].ToString();
            switch (currentView)
	        {
                case "PaymentInsert": UpdatePaymentGrid(true, true);
                    break;

                case "PaymentUpdate": UpdatePaymentGrid(true,false);
                    break;

                case "AdjInsert": UpdatePaymentGrid(false, true);
                    break;

                case "AdjUpdate": UpdatePaymentGrid(false, false);
                    break;

                
	        }                
            
    

        }
    }

    internal class SaleUpdate
    {
        public int  SaleId { get; set; }
        public string ShipTracking { get; set; }
        
    }
    internal class Payment
    {
        public int SaleId { get; set; }
        public string Status { get; set; }

    }
}
