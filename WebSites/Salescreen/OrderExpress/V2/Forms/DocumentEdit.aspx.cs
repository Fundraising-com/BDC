using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using QSPForm.Business;

using QSP.OrderExpress.Business;
using QSP.OrderExpress.Business.Entity;
using QSP.OrderExpress.Common.Comunication;
using QSP.OrderExpress.Common.Data;
using QSP.OrderExpress.Common.Enum;
using QSP.OrderExpress.Web.Code;
using QSP.OrderExpress.Web.V2.UserControls;

namespace QSP.OrderExpress.Web.V2.Forms
{
    public partial class DocumentEdit : System.Web.UI.Page
    {
        private DocumentData Document;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ValidateUserSession();

            if (!Page.IsPostBack)
            {
                this.ValidateFormParameters();
                this.ValidateUserPermissions();

                this.LoadDocument();

                this.BindDocument();

                this.SetPageTitle();
                this.SetCommandLinks();

                this.trMethodNotification.Visible = false;
            }
        }
        protected void lbSave_Click(object sender, EventArgs e)
        {
            #region Get data from controls

            this.LoadDocument();

            #region Document

            DocumentData documentData = this.ucDocumentInformationEdit.GetValueFromDocument();
            this.Document.IsApproved = documentData.IsApproved;
            this.Document.ExemptionNumber = documentData.ExemptionNumber;
            this.Document.ExemptionStartDate = documentData.ExemptionStartDate;
            this.Document.ExemptionEndDate = documentData.ExemptionEndDate;

            #endregion

            #endregion

            #region Set update user

            LoggedUser loggedUser = (LoggedUser)Session["LoggedUser"];

            this.Document.UpdateDate = DateTime.Now;
            this.Document.UpdateUserId = loggedUser.UserId;

            #endregion

            #region Save changes

            DocumentSystem documentSystem = new DocumentSystem();
            MethodResult result = documentSystem.SaveDocument(this.Document);

            #endregion

            if (result.IsSuccessful)
            {
                Response.Redirect(string.Format("~/V2/Forms/DocumentView.aspx?DocumentId={0}", this.Document.Id));
            }
            else
            {
                this.trMethodNotification.Visible = true;

                this.ucMethodNotificationList.SetValueForList(result.ResultNotifications);
                this.ucMethodNotificationList.SetValuesToForm();
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

            if (this.Document != null)
            {
                title = String.Format("{0} - Edit document - Order Express", this.Document.Name);
            }
            else
            {
                title = "Edit document - Order Express";
            }

            this.Title = title;
        }
        private void SetCommandLinks()
        {
            this.hlCancel.NavigateUrl = string.Format("~/V2/Forms/DocumentView.aspx?DocumentId={0}", this.Document.Id);
            this.hlClose.NavigateUrl = "javascript:window.close();";
        }

        private void LoadDocument()
        {
            DocumentSystem documentSystem = new DocumentSystem();
            this.Document = documentSystem.GetDocument(this.DocumentId, true);
        }

        private void BindDocument()
        {
            this.ucDocumentInformationEdit.SetValueForDocument(this.Document);
        }
    }
}
