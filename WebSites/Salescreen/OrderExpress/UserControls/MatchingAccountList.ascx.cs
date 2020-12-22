using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using QSPForm.Business;
using QSPForm.Business.com.qsp.ws.AccountFinderService;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    public partial class MatchingAccountList : BaseWebFormControl {
        private Account searchAccount = null;

        public event System.EventHandler<MatchingAccountsConfirmEventArgs> MatchingAccountsConfirmed;

        #region Events

        protected override void OnInit(EventArgs e) {
            base.OnInit(e);

            PreRender += new EventHandler(MatchingAccountList_PreRender);
            MatchingAccountsPanelConfirmButton.Click += new ImageClickEventHandler(MatchingAccountsPanelConfirmButton_Click);
            MatchingAccountsGridView.RowDataBound += new GridViewRowEventHandler(MatchingAccountsGridView_RowDataBound);
            MatchingAccountsGridView.PageIndexChanging += new GridViewPageEventHandler(MatchingAccountsGridView_PageIndexChanging);
        }

        protected void MatchingAccountList_PreRender(object sender, EventArgs e) {
            try {
                InitializePopupPanel();
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        protected void MatchingAccountsPanelConfirmButton_Click(object sender, ImageClickEventArgs e) {
            try {
                if (MatchingAccountsConfirmed != null) {
                    MatchingAccountsConfirmed(this, new MatchingAccountsConfirmEventArgs(MatchingAccounts));
                }
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        protected void MatchingAccountsGridView_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.Pager) {
                DataBindPager(e.Row);
            }
        }

        protected void MatchingAccountsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            try {
                MatchingAccountsGridView.PageIndex = e.NewPageIndex;

                MatchingAccountsGridView.DataSource = MatchingAccounts;
                MatchingAccountsGridView.DataBind();

                MatchingAccountsModalPopupExtender.Show();
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        #endregion

        #region Properties

        public Account DataSource {
            get {
                return searchAccount;
            }
            set {
                searchAccount = value;
            }
        }

        public List<OutputAccount> MatchingAccounts {
            get {
                return (List<OutputAccount>)ViewState["MatchingAccounts"];
            }
            set {
                ViewState["MatchingAccounts"] = value;
            }
        }

        private int ClientWidth {
            get {
                return Convert.ToInt32(WidthHidden.Value);
            }
        }

        private int ClientHeight {
            get {
                return Convert.ToInt32(HeightHidden.Value);
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Initializes the modal pop up panel.
        /// </summary>
        private void InitializePopupPanel() {
            if ((ClientWidth != 0 && ClientWidth <= 800) ||
                (ClientHeight != 0 && ClientHeight <= 500) ||
                (MatchingAccounts != null && MatchingAccounts.Count >= 6 &&
                ((ClientWidth != 0 && ClientWidth <= 1024) ||
                (ClientHeight != 0 && ClientHeight <= 668)))) {
                MatchingAccountsModalPopupExtender.X = 10;
                MatchingAccountsModalPopupExtender.Y = 15;
                MatchingAccountsModalPopupExtender.RepositionMode = AjaxControlToolkit.ModalPopupRepositionMode.None;
            }
            else {
                MatchingAccountsModalPopupExtender.X = -1;
                MatchingAccountsModalPopupExtender.Y = -1;
                MatchingAccountsModalPopupExtender.RepositionMode = AjaxControlToolkit.ModalPopupRepositionMode.RepositionOnWindowResizeAndScroll;
            }
        }

        public override void DataBind() {
            AccountSystem accountSystem = new AccountSystem();

            MatchingAccounts = accountSystem.GetMatchingAccounts(searchAccount);

            if (MatchingAccounts.Count > 0) {
                MatchingAccounts.Sort(CompareMatchingAccountsDescending);

                MatchingAccountsGridView.DataSource = MatchingAccounts;
                MatchingAccountsGridView.DataBind();
            }
        }

        private static int CompareMatchingAccountsDescending(OutputAccount outputAccount1, OutputAccount outputAccount2) {
            int comparisonValue = outputAccount2.MatchScore.CompareTo(outputAccount1.MatchScore);

            if (comparisonValue == 0) {
                comparisonValue = outputAccount1.Name.CompareTo(outputAccount2.Name);
            }

            return comparisonValue;
        }

        public bool Validate() {
            bool isValid = true;

            if (!Convert.ToBoolean(MatchingAccountsConfirmedIndicator.Value)) {
                isValid = (DisplayDuplicateAccounts() == 0);
            }
            else {
                MatchingAccountsConfirmedIndicator.Value = false.ToString();
            }

            return isValid;
        }

        private int DisplayDuplicateAccounts() {
            DataBind();

            if (MatchingAccounts.Count > 0) {
                MatchingAccountsModalPopupExtender.Show();
            }

            return MatchingAccounts.Count;
        }

        /// <summary>
        /// Databinds the matching account list pager.
        /// Note that this is a tweak to enable paging since the
        /// modal pop up disables Javascript to keep the page read only.
        /// </summary>
        /// <param name="row">Pager row</param>
        private void DataBindPager(GridViewRow row) {
            TableRow tableRow;
            TableCell tableCell;
            LinkButton linkButton;
            string eventReference = String.Empty;
            int nextOrPreviousPageNumber = 0;

            if (row.Controls.Count > 0 &&
                row.Controls[0].Controls.Count > 0 &&
                row.Controls[0].Controls[0].Controls.Count > 0) {
                tableRow = row.Controls[0].Controls[0].Controls[0] as TableRow;

                if (tableRow != null) {
                    for (int i = 0; i < tableRow.Controls.Count; i++) {
                        tableCell = tableRow.Controls[i] as TableCell;

                        if (tableCell != null) {
                            foreach (System.Web.UI.Control control in tableRow.Controls[i].Controls) {
                                linkButton = control as LinkButton;

                                if (linkButton != null) {
                                    if (linkButton.Text != "...") {
                                        eventReference = Page.ClientScript.GetPostBackEventReference(MatchingAccountsGridView, "Page$" + linkButton.Text);
                                    }
                                    // Handle page series
                                    else {
                                        if (i == 0) {
                                            nextOrPreviousPageNumber =
                                                Convert.ToInt32(Math.Floor(MatchingAccountsGridView.PageIndex /
                                                Convert.ToDouble(MatchingAccountsGridView.PagerSettings.PageButtonCount))) *
                                                MatchingAccountsGridView.PagerSettings.PageButtonCount;
                                        }
                                        else if (i == (tableRow.Controls.Count - 1)) {
                                            nextOrPreviousPageNumber =
                                                Convert.ToInt32(Math.Ceiling((MatchingAccountsGridView.PageIndex + 1) /
                                                Convert.ToDouble(MatchingAccountsGridView.PagerSettings.PageButtonCount))) *
                                                MatchingAccountsGridView.PagerSettings.PageButtonCount + 1;
                                        }

                                        eventReference = Page.ClientScript.GetPostBackEventReference(MatchingAccountsGridView, "Page$" + nextOrPreviousPageNumber.ToString());
                                    }

                                    linkButton.OnClientClick = eventReference;
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}