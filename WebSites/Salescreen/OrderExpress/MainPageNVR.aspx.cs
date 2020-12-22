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
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web {
    /// <summary>
    /// Summary description for MainPage.
    /// </summary>
    public partial class MainPageNVR : BaseWebForm {

        override protected void OnLoad(EventArgs e) {
            try {
                //InitPage();
                if (!IsPostBack)
                    LoadUserControl(this.ControlURL);
                base.OnLoad(e);

            }
            catch (Exception ex) {
                SetPageError(ex);
            }
        }

        private void LoadUserControl(string sControlURL) {
            Control ctl = LoadControl(sControlURL);
            plHoldBodyPage.Controls.Add(ctl);
        }

        protected override void LoadViewState(object savedState) {
            if (savedState != null) {
                // Load State from the array of objects that was saved at ;
                // SavedViewState.
                object[] myState = (object[])savedState;
                string sControlURL = "";
                if (myState[1] != null)
                    sControlURL = (string)myState[1];

                LoadUserControl(sControlURL);

                if (myState[0] != null)
                    base.LoadViewState(myState[0]);
            }
        }
        
        protected override object SaveViewState() {  // Change Text Property of Label when this function is invoked.
            // Save State as a cumulative array of objects.
            object baseState = base.SaveViewState();
            string sControlURL = this.ControlURL;
            object[] allStates = new object[2];
            allStates[0] = baseState;
            allStates[1] = sControlURL;
            return allStates;
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            InitPage();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {

        }
        #endregion

        private void InitPage() {
            if (Request.QueryString["NoMenu"] != null) {
                int NoMenu = Convert.ToInt32(Request.QueryString["NoMenu"]);
                this.AppItem = (QSPForm.Business.AppItem)NoMenu;
            }
        }

        protected override void InitControl() {
            base.HiddenChange = hidChange;
            base.LabelInstruction = lblInstruction;
            base.LabelMessage = lblMessage;
            base.LabelPageTitle = lblPageTitle;
            base.LabelSectionTitle = lblSectionTitle;
            base.LabelDirectionTitle = lblDirectionTitle;
            base.ImageIcon = imgIcon;
            base.ValSummary = ValSum;
            base.InitControl();
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            if (!IsPostBack) {
            }
        }
    }
}