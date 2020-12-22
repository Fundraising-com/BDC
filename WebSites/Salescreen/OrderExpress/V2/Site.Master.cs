using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QSP.OrderExpress.Web.V2
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.css.Attributes.Add("href", string.Format("~/css/{0}", this.GetStyleSheet()));
        }

        private bool IsForPrint
        {
            get
            {
                bool result = false;

                bool parseSuccessful = bool.TryParse(Request.QueryString["IsForPrint"], out result);

                if (!parseSuccessful)
                {
                    result = false;
                }

                return result;
            }
        }
        public string GetStyleSheet()
        {
            string result = "";

            if (IsForPrint)
            {
                result = "V2_Print.css";
            }
            else
            {
                result = "V2.css";
            }

            return result;
        }
    }
}
