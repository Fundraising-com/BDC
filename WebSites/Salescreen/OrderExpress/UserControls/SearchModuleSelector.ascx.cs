using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using QSP.WebControl;
using System.Collections;
using System.Web.UI;
using QSPForm.Business;
using QSPForm.Common.DataDef;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for SearchModuleSelector.
    /// </summary>
    public partial class SearchModuleSelector : BaseSearchModule//BaseWebUserControl
    {
        protected void Page_Load(object sender, EventArgs e) {
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            IniControl();
            base.OnInit(e);
        }

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {

        }
        #endregion

        private void IniControl() {
            base.LabelHeader = lblHeader;
            base.DropDownListSearchBy = ddlSearchBy;
            base.TextBoxCriteria = txtCriteria;
            base.LabelSearchByAlpha = lblSearchByAlpha;
            base.ControlAlphaSearch = ctrlAlphaSearch;
            base.ButtonSearch = imgBtnRefresh;
        }
    }
}