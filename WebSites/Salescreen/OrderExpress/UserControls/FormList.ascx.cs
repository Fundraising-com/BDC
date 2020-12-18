using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.FormTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for FormList.
    /// </summary>
    public partial class FormList : BaseWebSubFormControl {
        private const string DEFAULT_SORT = FormTable.FLD_FORM_NAME + " ASC";
        protected dataDef dTblForms = new dataDef();
        protected DataView DVForms;
        private CommonUtility clsUtil = new CommonUtility();

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            if (!IsPostBack) {
                //clsUtil.SetJScriptForOpenDetail(hypLnkAddNew, QSPForm.Business.AppItem.Form_Detail, FormDetail.FORM_ID, "0", 0,0);
                tblFilter.Visible = (this.Page.Role == QSPForm.Business.AuthSystem.ROLE_SUPER_USER);
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            InitControl();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.imgBtnAddNew.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnAddNew_Click);
        }

        private void InitControl() {
            this.DefaultSort = DEFAULT_SORT;
            DVForms = new DataView(dTblForms);
            this.DataSource = DVForms;
            this.MainDataGrid = dtgForm;
            dtgForm.DataKeyField = FormTable.FLD_PKID;
            base.LabelCurrentIndex = lblCurrentIndex;
            base.LabelTotal = lblTotal;
        }

        #endregion

        protected override void LoadDataSourceGrid() {
            ///	Call the appropriate Class from the Biz layer

            QSPForm.Business.FormSystem formSys = new QSPForm.Business.FormSystem();

            if (chkIncludeBaseForm.Checked)
                dTblForms = formSys.SelectAll(true);
            else
                dTblForms = formSys.SelectAll();
            DVForms = new DataView(dTblForms);
            DVForms.Sort = this.dtgForm.SortExpression;
            //Resynchronize the DataSource
            base.DataSource = DVForms;

            lblTotal.Text = "Number of Form(s) : " + DVForms.Count.ToString();
        }

        protected override void OnItemDataBound(System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                String sID = "0";
                if (e.Item.DataItem != null) {
                    sID = ((DataRowView)e.Item.DataItem).Row[dataDef.FLD_PKID].ToString();
                    //clsUtil.SetJScriptForOpenDetail(e.Item, AppItem.Form_Detail, BaseFormDetail.FORM_ID, sID, 0,0);
                    clsUtil.SetJScriptForOpenDetailNoCMS(e.Item, "FormDetail.aspx?", BaseFormDetail.FORM_ID, sID, 0, 0);

                    //					ImageButton imgBtnDetail = (ImageButton) e.Item.FindControl("imgBtnDetail");
                    //					if (imgBtnDetail != null)
                    //					{
                    //						clsUtil.SetJScriptForOpenDetail(imgBtnDetail, AppItem.Form_Detail, FormDetail.FORM_ID, sID, 0,0);
                    //					}
                    //					LinkButton lnkBtnForm = (LinkButton) e.Item.FindControl("lnkBtnForm");
                    //					if (lnkBtnForm != null)
                    //					{
                    //						clsUtil.SetJScriptForOpenDetail(lnkBtnForm, AppItem.Form_Detail, FormDetail.FORM_ID, sID, 0,0);
                    //					}
                }
            }
        }

        protected void chkIncludeBaseForm_CheckedChanged(object sender, System.EventArgs e) {
            BindGrid();
        }

        private void imgBtnAddNew_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //string strUrl = this.Page.GetPageToGo(QSPForm.Business.AppItem.BusinessForm_Step1);
            string strUrl = "~/BusinessFormStep_Selection.aspx";
            Response.Redirect(strUrl);
        }
    }
}