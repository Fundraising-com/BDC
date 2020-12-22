using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using QSPForm.Business;

using QSP.OrderExpress.Business;
using QSP.OrderExpress.Business.Entity;
using QSP.OrderExpress.Common.Data;
using QSP.OrderExpress.Common.Enum;
using QSP.OrderExpress.Common.Search;
using QSP.OrderExpress.Web.Code;
using QSP.OrderExpress.Web.V2.UserControls;

namespace QSP.OrderExpress.Web.V2.Forms
{
    public partial class DocumentView : System.Web.UI.Page
    {
        private DocumentData Document;
        private AccountData Account;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ValidateUserSession();

            if (!Page.IsPostBack)
            {
                this.ValidateFormParameters();
                this.ValidateUserPermissions();

                this.LoadDocument();
                this.LoadAccount();

                this.BindAccount();
                this.BindDocument();

                this.SetPageTitle();
                this.SetCommandLinks();
            }
        }

        private int DocumentId
        {
            get
            {
                int result = 0;

                bool parseSuccessful = Int32.TryParse(Request.QueryString["DocumentId"], out result);

                if (!parseSuccessful)
                {
                    result = 0;
                }

                return result;
            }
        }
        private int AccountId
        {
            get
            {
                int result = 0;

                if (this.Document != null)
                {
                    if (this.Document.AccountId.HasValue)
                    {
                        result = this.Document.AccountId.Value;
                    }
                }

                return result;
            }
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

        private void ValidateUserSession()
        {
            if (Session["LoggedUser"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        private void ValidateFormParameters()
        {
            if (this.DocumentId <= 0)
            {
                Response.Redirect("~/V2/Forms/DocumentSearch.aspx");
            }
        }
        private void ValidateUserPermissions()
        {
            LoggedUser loggedUser = (LoggedUser)Session["LoggedUser"];

            if (loggedUser.UserTypeId >= (int)UserTypeEnum.FieldSupport)
            {
                // Access granted by default
            }
            else
            {
                // No access, redirect to home screen
                Response.Redirect("~/Default.aspx");
            }
        }
        private void SetPageTitle()
        {
            string title;

            if (this.Account != null)
            {
                title = String.Format("{0} - View document - Order Express", this.Document.Name);
            }
            else
            {
                title = "View document - Order Express";
            }

            this.Title = title;
        }
        private void SetCommandLinks()
        {
            if (this.IsForPrint)
            {
                this.trButtons.Visible = false;
                this.trButtonsSpacer.Visible = false;
            }
            else
            {
                this.trButtons.Visible = true;
                this.trButtonsSpacer.Visible = true;

                this.hlPrint.NavigateUrl = string.Format("~/V2/Forms/DocumentView.aspx?DocumentId={0}&IsForPrint=true", this.Document.Id);
                this.hlEdit.NavigateUrl = string.Format("~/V2/Forms/DocumentEdit.aspx?DocumentId={0}", this.Document.Id);

                this.hlClose.NavigateUrl = "javascript:window.close();";
            }

        }

        private void LoadDocument()
        {
            DocumentSystem documentSystem = new DocumentSystem();
            this.Document = documentSystem.GetDocument(this.DocumentId, true);
        }
        private void LoadAccount()
        {
            AccountSystem accountSystem = new AccountSystem();
            this.Account = accountSystem.GetAccount(this.AccountId, true);
        }

        private void BindDocument()
        {
            if (this.IsForPrint)
            {
                this.lblDirections.Visible = false;
            }
            else
            {
                this.lblDirections.Visible = true;
            }

            #region Document information

            this.ucDocumentInformationView.SetValueForDocument(this.Document);
            this.ucDocumentInformationView.SetValuesToForm();

            #endregion
        }
        private void BindAccount()
        {
            this.ucAccountInformationView.SetValueForAccount(this.Account);
            this.ucAccountInformationView.SetValuesToForm();
        }
    }
}
