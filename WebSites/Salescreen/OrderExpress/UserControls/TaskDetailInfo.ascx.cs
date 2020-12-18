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
using dataDef = QSPForm.Common.DataDef.TaskTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>ToDoDetailInfo: Read only ToDo item information</summary>
    public partial class TaskDetailInfo : BaseWebFormControl {
        protected System.Web.UI.WebControls.HyperLink hypLnkClose;
        protected dataDef dtblTask;

        //private int c_UserID;
        //private int cTaskID = 0;
        public const string TASK_ID = "task_id";
        private const string TASK_DATA = "TaskData";

        #region auto-generated code
        ///<summary>Required method for Designer support</summary>
        override protected void OnInit(EventArgs e) {
            InitializeComponent();
            QSPToolBar.DisplayMode = ToolBar.DISPLAY_READ;
            base.OnInit(e);
        }

        ///<summary>Required method for Designer support</summary>
        private void InitializeComponent() {
            this.QSPToolBar.EditClick += new EventHandler(QSPToolBar_EditClick);

        }
        #endregion auto-generated code

        protected void Page_Load(object s, System.EventArgs e) {
            try {
                if (!IsPostBack) {
                    if (Request[TASK_ID] != null) {
                        iTaskID = Convert.ToInt32(Request[TASK_ID].ToString());
                    }
                }
                LoadData();
                this.ctrlTaskInfo.DataSource = dtblTask;
                this.ctrlTaskInfo.DataBind();
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        private void QSPToolBar_EditClick(object sender, EventArgs e) {
            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.TaskDetail, BaseTaskDetail.TASK_ID, iTaskID.ToString());
            string url = "~/TaskDetail.aspx?" + BaseTaskDetail.TASK_ID + "=" + iTaskID.ToString();
            Response.Redirect(url);
        }

        public int iTaskID {
            get {
                int task_ID = 0;
                if (ViewState[TASK_ID] != null) {
                    task_ID = Convert.ToInt32(ViewState[TASK_ID]);
                }
                return task_ID;
            }
            set {
                ViewState[TASK_ID] = value;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
        }

        protected override void LoadData() {
            QSPForm.Business.TaskSystem taskSys = new QSPForm.Business.TaskSystem();
            dtblTask = taskSys.SelectOne(iTaskID);
        }
    }
}