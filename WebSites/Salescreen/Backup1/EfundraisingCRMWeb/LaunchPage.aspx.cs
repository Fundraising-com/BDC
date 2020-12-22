using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using efundraising.Diagnostics;

namespace EfundraisingCRM
{
    public partial class LaunchPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                      

            string debug = "0";
            try
            {
                int leadID = 0;
                int clID = 0;
                string seq = "";
                int sid = 0;

                if (Request["lid"] != null)
                {
                    leadID = Convert.ToInt32(Request["lid"]);
                }
                if (Request["clid"] != null)
                {
                    clID = Convert.ToInt32(Request["clid"]);
                }
                if (Request["seq"] != null)
                {
                    seq = Request["seq"].ToString();
                }
                if (Request["clseq"] != null)
                {
                    seq = Request["clseq"].ToString();
                }
                if (Request["sid"] != null)
                {
                    sid = Convert.ToInt32(Request["sid"]);
                }
                debug = "1";
              
                string param = "";
                if (leadID > 0)
                {
                    param = "Default&lid=" + leadID;
                }
                else if (clID > 0 && seq != "")
                {
                    param = "Default&clid=" + clID + "&seq=" + seq;
                }
                else if (sid > 0)
                {
                    param = "NewSales&sid=" + sid;
                    //set coubntry
                }
                else
                {
                    param = "Default";
                }

                Logger.LogError("line crm 600");
                string script = "<script language='javascript'>window.open('CrmLogin.aspx?page=" + param + "','SalesScreenWindow')</script>";

             Page.RegisterClientScriptBlock("Open", script);
                 /*       string strscript = "<script language=javascript>javascript:NoConfirm();</script>";
                      if (!Page.IsStartupScriptRegistered("clientScript")) {
                         Page.RegisterStartupScript("clientScript", strscript);   
                      }*/

            }
            catch (Exception x)
            {
                efundraising.Diagnostics.Logger.LogError("Error in Launch Page. Debug=" + debug, x);
            }
            


        }
    }
}
