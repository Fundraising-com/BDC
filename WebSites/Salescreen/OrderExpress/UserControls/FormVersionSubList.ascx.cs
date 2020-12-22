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
    ///		Summary description for FormSubList.
    /// </summary>
    public partial class FormVersionSubList : BaseWebSubFormControl {
        private const string DEFAULT_SORT = FormTable.FLD_PKID + " ASC";
        protected DataView DVForm;
        private int c_FormID;
        protected dataDef dTblForm = new dataDef();
        private CommonUtility clsUtil = new CommonUtility();

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
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

        }

        private void InitControl() {
            this.DefaultSort = DEFAULT_SORT;
            DVForm = new DataView(dTblForm);
            this.DataSource = DVForm;
            this.MainDataGrid = dtgForm;
            dtgForm.DataKeyField = FormTable.FLD_PKID;
            base.LabelTotal = lblTotal;

        }
        #endregion

        protected override void LoadDataSourceGrid() {
            ///	Call the appropriate Class from the Biz layer

            QSPForm.Business.FormSystem frmSys = new QSPForm.Business.FormSystem();

            dTblForm = frmSys.SelectAllVersionByFormID(c_FormID);

            DVForm = new DataView();
            DVForm.Table = dTblForm;

            DVForm.Sort = this.dtgForm.SortExpression;
            lblTotal.Text = "Number of Form(s) : " + DVForm.Count.ToString();
        }

        public int FormID {
            get {
                return c_FormID;
            }
            set {
                c_FormID = value;
            }
        }

        protected override void OnItemDataBound(System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                String sID = "0";
                if (e.Item.DataItem != null) {
                    sID = ((DataRowView)e.Item.DataItem).Row[FormTable.FLD_PKID].ToString();
                    string sIDName = BaseFormDetail.FORM_ID;
                    //clsUtil.SetJScriptForOpenDetail(e.Item, AppItem.Form_Detail, sIDName, sID, 0, 0);
                    clsUtil.SetJScriptForOpenDetailNoCMS(e.Item, "FormDetail.aspx?", sIDName, sID, 0, 0);

                    //					ImageButton imgBtnDetail = (ImageButton) e.Item.FindControl("imgBtnDetail");
                    //					if (imgBtnDetail != null)
                    //					{
                    //						clsUtil.SetJScriptForOpenDetail(imgBtnDetail, AppItem.FormDetail, sIDName, sID, 0,0);
                    //					}
                    //					HyperLink hypLnkName = (HyperLink) e.Item.FindControl("hypLnkName");
                    //					if (hypLnkName != null)
                    //					{
                    //						clsUtil.SetJScriptForOpenDetail(hypLnkName, AppItem.FormDetail, sIDName, sID, 0,0);
                    //					}
                }
            }
        }
    }
}