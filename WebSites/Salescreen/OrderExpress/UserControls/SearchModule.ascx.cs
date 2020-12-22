//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\Stub_SearchModule_ascx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'SearchModule.ascx' was also modified to refer to the new class name.
// For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
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
    ///		Summary description for SearchModule.
    /// </summary>
    public partial class SearchModule : BaseSearchModule {
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
            this.PreRender += new EventHandler(SearchModule_PreRender);
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

        private void SearchModule_PreRender(object sender, EventArgs e) {
        }
    }
}