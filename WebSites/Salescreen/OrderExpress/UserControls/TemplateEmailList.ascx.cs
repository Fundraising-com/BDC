using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using QSP.WebControl;
using dataDef = QSPForm.Common.DataDef.TemplateEmailTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>TemplateEmailList</summary>
    public partial class TemplateEmailList : BaseWebSubFormControl {
        QSPForm.Business.TemplateEmailSystem TemplateEmailSys = new QSPForm.Business.TemplateEmailSystem();
        private const string DEFAULT_SORT = TemplateEmailTable.FLD_TEMPLATE_EMAIL_NAME + " ASC";
        protected TemplateEmailTable dTblTemplateEmail = new TemplateEmailTable();
        protected DataView DVTemplateEmail;
        protected System.Web.UI.WebControls.ImageButton imgbtnAddTemplateEmail;
        protected QSP.WebControl.DataGridControl.SortedDataGrid dtgToDoItems;
        private CommonUtility clsUtil = new CommonUtility();

        #region auto-generated, Initialization code
        ///<summary>Required method for Designer support</summary>
        override protected void OnInit(EventArgs e) {
            InitializeComponent();
            InitControl();
            base.OnInit(e);
        }

        ///<summary>Required method for Designer support</summary>
        private void InitializeComponent() {
        }

        private void InitControl() {
            this.DefaultSort = DEFAULT_SORT;
            this.DVTemplateEmail = new DataView(dTblTemplateEmail);
            this.DataSource = this.DVTemplateEmail;
            this.MainDataGrid = this.dtgTemplateEmailItems;
            this.dtgTemplateEmailItems.DataKeyField = dataDef.FLD_PKID;
            base.LabelCurrentIndex = lblCurrentIndex;
            base.LabelTotal = lblTotal;
        }
        #endregion auto-generated, Initialization code

        protected void Page_Load(object sender, System.EventArgs e) {
            if (!Page.IsPostBack) {
                //clsUtil.SetJScriptForOpenDetail(hypLnkAddNew, QSPForm.Business.AppItem.TemplateEmailDetail, BaseTemplateEmailDetail.TEMPLATE_EMAIL_ID, "0", 0,0);
                clsUtil.SetJScriptForOpenDetailNoCMS(hypLnkAddNew, "TemplateEmailDetail.aspx?", BaseTemplateEmailDetail.TEMPLATE_EMAIL_ID, "0", 0, 0);
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
        }

        protected override void LoadDataSourceGrid() {
            //if (this.Page.AppItem == QSPForm.Business.AppItem.TemplateEmailList)
            dTblTemplateEmail = this.TemplateEmailSys.SelectAll();
            //			else
            //				dTblTemplateEmail = this.TemplateEmailSys.SelectAllByAssignedUserID(this.Page.UserID);

            this.DVTemplateEmail = new DataView(dTblTemplateEmail);
            this.DVTemplateEmail.Sort = this.dtgTemplateEmailItems.SortExpression;
            this.DataSource = this.DVTemplateEmail;
            lblTotal.Text = "Number of Template(s) : " + this.DVTemplateEmail.Count.ToString();
        }

        protected override void OnItemCreated(System.Web.UI.WebControls.DataGridItemEventArgs e) {

            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                String sID = "0";
                sID = dtgTemplateEmailItems.DataKeys[e.Item.ItemIndex].ToString();
                string sIDName = TemplateEmailDetailInfo.TEMPLATE_EMAIL_ID;
                //clsUtil.SetJScriptForOpenDetail(e.Item, QSPForm.Business.AppItem.TemplateEmailDetailInfo, sIDName, sID, 0,0);
                clsUtil.SetJScriptForOpenDetailNoCMS(e.Item, "TemplateEmailDetailInfo.aspx?", sIDName, sID, 0, 0);

            }
        }

        public DropDownList SearchCriteria {
            get {
                return QSPFormSearchModule.DropDownListSearchBy;
            }
        }
    }
}