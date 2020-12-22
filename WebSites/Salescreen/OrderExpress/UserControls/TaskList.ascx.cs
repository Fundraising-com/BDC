using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using QSP.WebControl;
using dataDef = QSPForm.Common.DataDef.TaskTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>TaskList</summary>
    public partial class TaskList : BaseWebSubFormControl {
        private const string DEFAULT_SORT = TaskTable.FLD_TASK_NAME + " ASC";
        protected TaskTable dTblTask = new TaskTable();
        protected DataView DVTask;
        protected System.Web.UI.WebControls.ImageButton imgbtnAddTask;
        private CommonUtility clsUtil = new CommonUtility();
        private const string FILTER_TYPE = "Filter_Type";

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
            this.DVTask = new DataView(dTblTask);
            this.DataSource = this.DVTask;
            this.MainDataGrid = this.dtgTaskItems;
            this.dtgTaskItems.DataKeyField = dataDef.FLD_PKID;
            base.LabelCurrentIndex = lblCurrentIndex;
            base.LabelTotal = lblTotal;
        }
        #endregion auto-generated, Initialization code

        protected void Page_Load(object sender, System.EventArgs e) {
            if (!Page.IsPostBack) {
                if (this.Page.Role >= AuthSystem.ROLE_ADMINISTRATOR) {
                    hypLnkAddNew.Visible = true;
                    //clsUtil.SetJScriptForOpenDetail(hypLnkAddNew, QSPForm.Business.AppItem.TaskDetail, BaseTaskDetail.TASK_ID, "0", 690, 600);

                    clsUtil.SetJScriptForOpenDetailNoCMS(hypLnkAddNew, "TaskDetail.aspx?", BaseTaskDetail.TASK_ID, "0", 690, 600);
                }
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
        }

        protected override void FillFilter() {
            clsUtil.SetTaskTypeDropDownList(ddlTaskType, true);
            ViewState[FILTER_TYPE] = ddlTaskType.SelectedItem.Value;

            base.FillFilter();
        }

        private void QSPFormSearchModule_OnSearch(object sender, SearchModuleClickedArgs e) {
            ViewState[FILTER_TYPE] = ddlTaskType.SelectedItem.Value;
        }

        protected override void LoadDataSourceGrid() {
            QSPForm.Business.TaskSystem taskSys = new QSPForm.Business.TaskSystem();
            string sCriteria = this.dtgTaskItems.FilterExpression;
            switch (this.dtgTaskItems.SearchMode) {
                case 0: {
                        sCriteria = sCriteria + "%";
                        break;
                    }
                default: {
                        sCriteria = "%" + sCriteria + "%";
                        break;
                    }
            }

            int TaskTypeID = Convert.ToInt32(ViewState[FILTER_TYPE]);
            dTblTask = taskSys.SelectAll_Search(dtgTaskItems.SearchMode, sCriteria, TaskTypeID);

            this.DVTask = new DataView(dTblTask);
            this.DVTask.Sort = this.dtgTaskItems.SortExpression;
            this.DataSource = this.DVTask;
            lblTotal.Text = "Number of Task(s) : " + this.DVTask.Count.ToString();
        }

        protected override void OnItemCreated(System.Web.UI.WebControls.DataGridItemEventArgs e) {

            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                String sID = "0";
                if (e.Item.DataItem != null) {
                    sID = dtgTaskItems.DataKeys[e.Item.ItemIndex].ToString();
                    string sIDName = TaskDetailInfo.TASK_ID;
                    //clsUtil.SetJScriptForOpenDetail(e.Item, QSPForm.Business.AppItem.TaskDetailInfo, sIDName, sID, 690, 600);
                    clsUtil.SetJScriptForOpenDetailNoCMS(e.Item, "TaskDetailInfo.aspx?", sIDName, sID, 690, 600);
                }

            }
        }

        public DropDownList SearchCriteria {
            get {
                return QSPFormSearchModule.DropDownListSearchBy;
            }
        }
    }
}