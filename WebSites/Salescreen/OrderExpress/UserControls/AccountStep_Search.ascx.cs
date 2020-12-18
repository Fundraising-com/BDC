using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.AccountData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    /// Summary description for BaseAccountForm.
    /// </summary>
    public partial class AccountStep_Search : BaseWebFormControl {

        protected void Page_Load(object sender, System.EventArgs e) {

        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitControl();
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {


        }
        #endregion

        private void InitControl() {
            OrganizationSelectorStep.SearchAppItem = QSPForm.Business.AppItem.OrganizationSelector;
            OrganizationSelectorStep.ButtonVisible = false;
            this.OrganizationSelectorStep.SelectedIndexChanged += new EventHandler(OrganizationSelectorStep_SelectedIndexChanged);

            MDRSchoolSelectorStep.SearchAppItem = QSPForm.Business.AppItem.MDRSchoolSelector;
            MDRSchoolSelectorStep.ButtonVisible = false;
            this.MDRSchoolSelectorStep.SelectedIndexChanged += new EventHandler(MDRSchoolSelectorStep_SelectedIndexChanged);
        }

        protected void radBtnLstSearchMode_SelectedIndexChanged(object sender, System.EventArgs e) {
            OrganizationSelectorStep.Visible = (radBtnLstSearchMode.SelectedValue == "0");
            MDRSchoolSelectorStep.Visible = (radBtnLstSearchMode.SelectedValue == "1");

            if (radBtnLstSearchMode.SelectedValue == "2") {
                GoToNextStep();
            }
        }

        private void MDRSchoolSelectorStep_SelectedIndexChanged(object sender, EventArgs e) {
            GoToNextStep();
        }

        private void OrganizationSelectorStep_SelectedIndexChanged(object sender, EventArgs e) {
            GoToNextStep();
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            OrganizationSelectorStep.Visible = (radBtnLstSearchMode.SelectedValue == "0");
            MDRSchoolSelectorStep.Visible = (radBtnLstSearchMode.SelectedValue == "1");
        }

        private void GoToNextStep() {
            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.AccountForm_Step2);
            string url = "AccountStep_Selection.aspx?NoMenu=41";

            if (radBtnLstSearchMode.SelectedValue == "0") {
                if (OrganizationSelectorStep.SelectedValue.ToString().Length > 0) {
                    Response.Redirect(url + "&OrgID=" + OrganizationSelectorStep.SelectedValue);
                }
            }
            else if (radBtnLstSearchMode.SelectedValue == "1") {
                //MDR School
                if (MDRSchoolSelectorStep.SelectedOrganization > 0) {
                    Response.Redirect(url + "&OrgID=" + MDRSchoolSelectorStep.SelectedOrganization.ToString());
                }
                else {
                    if (MDRSchoolSelectorStep.SelectedValue.ToString().Length > 0) {
                        Response.Redirect(url + "&MDRPID=" + MDRSchoolSelectorStep.SelectedValue);
                    }
                }
            }
            else if (radBtnLstSearchMode.SelectedValue == "2") {
                //Brand New Organization
                Response.Redirect(url);
            }
        }

        private void imgBtnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            GoToNextStep();
        }
    }
}