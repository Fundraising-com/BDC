using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


namespace EfundraisingCRM
{
    public partial class test : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
         /*   string conn = ConfigurationManager.ConnectionStrings["QSPFulfillmentConnectionString"].ConnectionString;
            QSPFulfillmentDataContext db = new QSPFulfillmentDataContext(conn);




            db.Log = new DebuggerWriter();

            //string conn = ConfigurationManager.ConnectionStrings["QSPFulfillmentConnectionStringDEV"].ConnectionString;
            account acct = db.accounts.SingleOrDefault(a => a.account_id == 506179);

            if (acct != null)
            {
                acct.fm_id = "1566";
                db.SubmitChanges();
              
            }
                            
*/
                            

           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

         //   ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopup", "window.setTimeout('PopupModal()',50);", True);

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
    
        }


      
    }
}
