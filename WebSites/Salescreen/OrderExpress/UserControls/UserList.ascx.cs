using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using QSP.WebControl;
using dataDef = QSPForm.Common.DataDef.UserTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>UserList</summary>
    public partial class UserList : BaseWebSubFormControl {
        #region auto-generated, Initialization code
        ///<summary>Required method for Designer support</summary>
        override protected void OnInit(EventArgs e) {
            InitializeComponent();
            InitControl();
            base.OnInit(e);
        }

        ///<summary>Required method for Designer support</summary>
        private void InitializeComponent() {
            this.Page.RefreshPage += new EventHandler(Page_RefreshPage);
        }

        private void InitControl() {
            this.DefaultSort = DEFAULT_SORT;
            DVUsers = new DataView(dTblUsers);
            this.DataSource = DVUsers;
            this.MainDataGrid = dtgUsers;
            dtgUsers.DataKeyField = UserTable.FLD_PKID;
            base.LabelCurrentIndex = lblCurrentIndex;
            base.LabelTotal = lblTotal;
        }
        #endregion auto-generated, Initialization code

        #region Item Declarations
        QSPForm.Business.UserSystem userSys = new QSPForm.Business.UserSystem();
        private const string DEFAULT_SORT = UserTable.FLD_USER_NAME + " ASC";
        protected UserTable dTblUsers = new UserTable();
        protected DataView DVUsers;
        private CommonUtility clsUtil = new CommonUtility();
        #endregion Item Declarations

        protected void Page_Load(object sender, System.EventArgs e) {
            //clsUtil.SetJScriptForOpenDetail(hypLnkAddNew, QSPForm.Business.AppItem.UserDetail, BaseUserDetail.USER_ID, "0", 0,0);
            clsUtil.SetJScriptForOpenDetailNoCMS(hypLnkAddNew, "UserDetail.aspx?", BaseUserDetail.USER_ID, "0", 0, 0);
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
        }

        protected override void LoadDataSourceGrid() {
            ///	Call the appropriate Class from the Biz layer

            //TypeSearch SearchType = TypeSearch.ByAlpha;
            string sCriteria = this.dtgUsers.FilterExpression;
            switch (this.dtgUsers.SearchMode) {
                case 0: {
                        sCriteria = sCriteria + "%";
                        break;
                    }
                default: {
                        sCriteria = "%" + sCriteria + "%";
                        break;
                    }
            }

            dTblUsers = userSys.SelectAll_Search(dtgUsers.SearchMode, sCriteria);

            DVUsers = new DataView(dTblUsers);
            DVUsers.Sort = this.dtgUsers.SortExpression;
            this.DataSource = DVUsers;
            lblTotal.Text = "Number of User(s) : " + DVUsers.Count.ToString();
        }

        protected override void OnItemDataBound(System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                String sID = "0";
                if (e.Item.DataItem != null) {
                    sID = ((DataRowView)e.Item.DataItem).Row[dataDef.FLD_PKID].ToString();
                    string sIDName = UserDetailInfo.USER_ID;
                    //clsUtil.SetJScriptForOpenDetail(e.Item, QSPForm.Business.AppItem.UserDetailInfo, sIDName, sID, 0,0, "OnDblClick");
                    clsUtil.SetJScriptForOpenDetailNoCMS(e.Item, "UserDetailInfo.aspx?", sIDName, sID, 0, 0, "OnDblClick");

                    ImageButton imgBtnDetail = (ImageButton)e.Item.FindControl("imgBtnDetail");
                    if (imgBtnDetail != null) {
                        //clsUtil.SetJScriptForOpenDetail(imgBtnDetail, QSPForm.Business.AppItem.UserDetailInfo, sIDName, sID, 0,0);
                        clsUtil.SetJScriptForOpenDetailNoCMS(e.Item, "UserDetailInfo.aspx?", sIDName, sID, 0, 0);
                    }
                    LinkButton lnkBtn = (LinkButton)e.Item.FindControl("lnkBtn");
                    if (lnkBtn != null) {
                        //clsUtil.SetJScriptForOpenDetail(lnkBtn, QSPForm.Business.AppItem.UserDetailInfo, sIDName, sID, 0,0);
                        clsUtil.SetJScriptForOpenDetailNoCMS(e.Item, "UserDetailInfo.aspx?", sIDName, sID, 0, 0);
                    }
                }
            }
        }

        private void Page_RefreshPage(object sender, EventArgs e) {
            BindGrid();
        }
        public DropDownList SearchCriteria {
            get {
                return QSPFormSearchModule.DropDownListSearchBy;
            }
        }

        public Label SearchName {
            get {
                return QSPFormSearchModule.LabelSearchByAlpha;
            }
            set {
                QSPFormSearchModule.LabelSearchByAlpha = value;
            }
        }
    }
}