using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using QSP.OrderExpress.Common.Enum;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls 
{
    public partial class Menu : System.Web.UI.UserControl 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UserAgent.IndexOf("AppleWebKit") > 0)
            {
                Request.Browser.Adapters.Clear();
            }

            this.SetMenuVisibility();
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
        private void SetMenuVisibility()
        {
            this.HeaderDiv.Visible = false;
            this.HeaderDivPrint.Visible = false;

            this.MenuDiv.Visible = false;
            this.MenuSuperUser.Visible = false;
            this.MenuAdmin.Visible = false;
            this.MenuAccountManager.Visible = false;
            this.MenuFieldSupport.Visible = false;
            this.MenuFieldSalesManager.Visible = false;
            this.MenuNoRole.Visible = false;

            if (this.IsForPrint)
            {
                #region Print header

                //this.HeaderDivPrint.Visible = true;

                #endregion
            }
            else
            {
                #region Web page header

                this.HeaderDiv.Visible = true;
                this.MenuDiv.Visible = true;

                if (Session["LoggedUser"] == null)
                {
                    this.MenuNoRole.Visible = true;
                }
                else
                {
                    LoggedUser loggedUser = (LoggedUser)Session["LoggedUser"];

                    switch (loggedUser.UserTypeId)
                    {
                        case (int)UserTypeEnum.SuperUser:
                            this.MenuSuperUser.Visible = true;
                            break;
                        case (int)UserTypeEnum.Admin:
                            this.MenuAdmin.Visible = true;
                            break;
                        case (int)UserTypeEnum.AccountingManager:
                            this.MenuAccountManager.Visible = true;
                            break;
                        case (int)UserTypeEnum.FieldSupport:
                            this.MenuFieldSupport.Visible = true;
                            break;
                        case (int)UserTypeEnum.FieldSaleManager:
                            this.MenuFieldSalesManager.Visible = true;
                            break;
                    }
                }

                #endregion
            }
        }
    } 
}