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
using AjaxControlToolkit;
using log4net;


namespace EfundraisingCRM
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        public ILog Logger { get; set; }

        public Site1()
	    {
            Logger = LogManager.GetLogger(GetType());
	    }   
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

                   

                    string connEFR = "";
                    efundraising.EFundraisingCRM.Linq.eFundraisingProdDataContext db = new efundraising.EFundraisingCRM.Linq.eFundraisingProdDataContext();
                    if (EFundraisingCRMWeb.Components.Server.ManageSaleScreen.IsProd())
                    {
                        connEFR = ConfigurationManager.ConnectionStrings["EFRProdConnectionString"].ConnectionString;
                        db = new efundraising.EFundraisingCRM.Linq.eFundraisingProdDataContext(connEFR);
                    }
                    else
                    {
                        connEFR = ConfigurationManager.ConnectionStrings["EFRProdConnectionStringDEV"].ConnectionString;
                        db = new efundraising.EFundraisingCRM.Linq.eFundraisingProdDataContext(connEFR);
                    }
                    
                    
                   
                    //string orderId = "";
                    sale sale = db.sales.SingleOrDefault(o => o.ext_order_id.ToString() == searchId.ToString());
                    //order order = db.orders.SingleOrDefault(o => o.fulf_order_id == searchId.ToString());
                    if (sale != null)
                    {
                        //orderId = sale.sales_id.ToString();
                        Response.Redirect("NewSales.aspx?sid=" + sale.sales_id);
                    }
                    else 
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

            Logger.Debug("line crm 700");
            Response.Redirect("../../crmlogin.aspx");

        }






    }
}
