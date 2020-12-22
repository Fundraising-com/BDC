using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EFundraisingCRMWeb.Components.Server;
using efundraising.EFundraisingCRM;
using System.Net.Mail;
using System.Text;


namespace EfundraisingCRM.Sales.SalesScreen
{
    public partial class AddressPopUp : System.Web.UI.Page
    {
        protected EFundraisingCRMWeb.Components.User.Address Address;
        protected EFundraisingCRMWeb.Components.User.ClientControls.CustomerInformation ClientInformation;

        protected void Page_Load(object sender, EventArgs e)
        {
            Address.SetControlAsShipping(true);

        }


        protected void sendEmailButton_Click(object sender, EventArgs e)
        {

            ClientAddress clFromInterface = this.Address.GetClientAddress();
            
            string to = ManageSaleScreen.GetValueFromWebConfig("ShippingEmailNotification", "value");
            MailMessage msg = new MailMessage("SaleScreen@qsp.com", to);
            msg.Priority = MailPriority.High;
            msg.IsBodyHtml = true;
            msg.Subject = "New Customer Shipping Address";
            string temp = clFromInterface.StateCode;
            StringBuilder body = new StringBuilder();
            body.Append("LeadID: " + Session["NewShippingLeadID"].ToString() + "<BR>");
            body.Append("Addresss: " + clFromInterface.StreetAddress + "<BR>");
            body.Append("City: " + clFromInterface.City + "<BR>");
            body.Append("State: " + clFromInterface.StateCode + "<BR>");
            body.Append("Country: " + clFromInterface.CountryCode + "<BR>");
            body.Append("Zip: " + clFromInterface.ZipCode + "<BR>");
            body.Append("Zone: " + clFromInterface.AddressZoneId + "<BR>");
            body.Append("Attention of: " + clFromInterface.AttentionOf + "<BR>");
            body.Append("Location Name: " + clFromInterface.Location + "<BR>");
            msg.Body = body.ToString();

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = " outgoingsmtp";
            smtpClient.Send(msg);
            string script = "<script language='javascript'>window.close('" + "AddressPopUp.aspx" + "')</script>";
            Page.RegisterClientScriptBlock("Open", script);
                    





        }



    }
        
}
