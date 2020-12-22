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
using EFundraisingCRMWeb.Components.Server;
using System.Linq;
using efundraising.EFundraisingCRM.Linq;



namespace EfundraisingCRM
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
   
        protected void Page_Load(object sender, EventArgs e)
        {

            EFundraisingCRMWeb.Components.Server.User.CrmUser crmUser = EFundraisingCRMWeb.Components.Server.User.CrmUser.Create(Session);
            efundraising.EFundraisingCRM.Consultant consultant = efundraising.EFundraisingCRM.Consultant.GetConsultantByNtLogin(crmUser.Name);
            string name = "TestUser";
            if (consultant != null)
            {
                name = consultant.Name;
                int pos = consultant.Name.IndexOf(" ");
                if (pos > 0)
                {
                    name = consultant.Name.Substring(0, pos);
                }
            }
            
            
            WelcomeLabel.Text = "Welcome " + name;

            panel1.Visible = true;
         /*   bool isConsultant = ManageSaleScreen.IsConsultant();
            bool isSaleSupport = ManageSaleScreen.IsInGroup(ManageSaleScreen.Roles.gCAEFR_Intranet_SaleSupport);
            bool isMIS = ManageSaleScreen.IsMIS();
            if (isMIS || isSaleSupport)
            {
                panel1.Visible = true;
            }
            else
            {
                panel1.Visible = false;
            }
            */

            SetOnKeyPressBehavior(SearchTextBox, SearchButton.ClientID);
                     

            bool displayWarning = Convert.ToBoolean(EFundraisingCRMWeb.Components.Server.ManageSaleScreen.GetValueFromWebConfig("EFundraisingProd.Production", "displayWarning"));

            if (displayWarning)
            {

                Label label = new Label();
                label.Font.Bold = true;
                label.Font.Italic = true;
                label.Font.Name = "Arial Unicode MS";
                label.Font.Size = 14;

                PlaceHolder1.Controls.Add(label);


                bool isProd = Convert.ToBoolean(EFundraisingCRMWeb.Components.Server.ManageSaleScreen.GetValueFromWebConfig("EFundraisingProd.Production", "isProduction"));
                if (isProd)
                {
                    label.Text = "This site is connected to PRODUCTION";
                    label.ForeColor = System.Drawing.Color.DarkSalmon;
                }
                else
                {
                    label.Text = "This site is connected to DEV";
                    label.ForeColor = System.Drawing.Color.DarkTurquoise;
                }
            }
        }


        //when the user presses enter, the submit button is invoked  //vines
        public static void SetOnKeyPressBehavior(TextBox txt, String SubmitCtrlRef)
        {
            String strJavaScript = "if (event.keyCode == 13){ window.document.all('" + SubmitCtrlRef + "').click();return false;}";
            txt.Attributes.Add("onKeyPress", strJavaScript);

        }

        protected void SearchButton_Click(object sender, ImageClickEventArgs e)
        {
            if (SearchTextBox.Text.StartsWith("s")){
               Response.Redirect("NewSales.aspx?sid=" + SearchTextBox.Text.Remove(0,1),false);
            }
            else if (SearchTextBox.Text.StartsWith("l"))
            {
                Response.Redirect("default.aspx?lid=" + SearchTextBox.Text.Remove(0,1), false);
            }
            else
            {



                int searchId = 0;
                try
                {
                    searchId = Convert.ToInt32(SearchTextBox.Text);
                }
                catch (Exception x)
                {
                    searchId = 0;
                }

                if (searchId != 0)
                {

                    string connQSP = ConfigurationManager.ConnectionStrings["QSPFulfillmentConnectionStringDEV"].ConnectionString;
                    string connEFR = ManageSaleScreen.GetValueFromWebConfig("EFundraisingProd.SqlConnection.Debug", "connectionString");

                    if (EFundraisingCRMWeb.Components.Server.ManageSaleScreen.IsProd())
                    {
                        connQSP = ConfigurationManager.ConnectionStrings["QSPFulfillmentConnectionString"].ConnectionString;
                        connEFR = ManageSaleScreen.GetValueFromWebConfig("EFundraisingProd.SqlConnection.Release", "connectionString");
                    }

                    QSPFulfillmentDataContext db = new QSPFulfillmentDataContext(connQSP);
                    eFundraisingProdDataContext efrDB = new eFundraisingProdDataContext(connEFR);

                    int orderId = 0;
                    order order = db.orders.SingleOrDefault(o => o.fulf_order_id == searchId.ToString());
                    if (order != null)
                    {
                        orderId = order.order_id;
                    }
                    else
                    {
                        order = db.orders.SingleOrDefault(o => o.order_id == searchId);
                        if (order != null)
                        {
                            orderId = order.order_id;
                        }
                    }

                    if (orderId > 0)
                    {
                        sale sale = efrDB.sales.SingleOrDefault(s => s.ext_order_id == orderId);
                        if (sale != null)
                        {
                            Response.Redirect("NewSales.aspx?sid=" + sale.sales_id);
                        }
                        else
                        {
                            orderId = 0;
                        }
                    }

                    if (orderId == 0)
                    {
                        SearchTextBox.ForeColor = System.Drawing.Color.Red;
                        SearchTextBox.Text = "ID " + SearchTextBox.Text + " could not be found";
                    }


                }
                else
                {
                    SearchTextBox.ForeColor = System.Drawing.Color.Red;
                    SearchTextBox.Text = "ID has incorrect format";
                }
            }
        
        }

        protected void LogOutButton_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Cookies.Clear();
            Response.Redirect("../../crmlogin.aspx");

        }






    }
}
