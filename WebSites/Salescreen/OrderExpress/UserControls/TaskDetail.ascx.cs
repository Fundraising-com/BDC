//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\Stub_TaskDetail_ascx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'TaskDetail.ascx' was also modified to refer to the new class name.
// For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
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
    ///<summary>Task Detail form</summary>
    public partial class TaskDetail : BaseTaskDetail {
        private int c_TaskID;
        protected System.Web.UI.WebControls.ImageButton imgBtnSave;
        protected dataDef dTblTask;

        private const string TASK_DATA = "TaskData";

        protected void Page_Load(object s, System.EventArgs e) {
            try {
                LoadData();
                if (!IsPostBack) {
                    BindForm();
                }
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        #region auto-generated code
        override protected void OnInit(EventArgs e) {
            InitializeComponent();
            this.QSPToolBar.DisplayMode = ToolBar.DISPLAY_EDIT;
            this.QSPToolBar.SaveClick += new EventHandler(this.QSPToolBar_SaveClick);
            this.QSPToolBar.DeleteClick += new EventHandler(this.QSPToolBar_DeleteClick);
            this.QSPToolBar.DeleteButton.Attributes.Add("onclick", "return confirm('Are you sure that you want to delete this user ?');");

            base.OnInit(e);
        }

        ///<summary>Required method for Designer support</summary>
        private void InitializeComponent() {

        }
        #endregion auto-generated code

        private void SetFormParameter() {
            if (Request[TASK_ID] != null) {
                c_TaskID = Convert.ToInt32(Request[TASK_ID].ToString());
            }
            else {
                c_TaskID = 0;
            }
            ViewState[TASK_ID] = c_TaskID;
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            this.ViewState[TASK_DATA] = dTblTask;
            //Set the close button
            if (!IsPostBack) {
                if (dTblTask.Rows.Count > 0) {
                    if (dTblTask.Rows[0].RowState != DataRowState.Added) {
                        //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.TaskDetailInfo, TaskDetailInfo.TASK_ID, c_TaskID.ToString());
                        string url = "~/TaskDetailInfo.aspx?" + TaskDetailInfo.TASK_ID + "=" + c_TaskID.ToString();
                        this.QSPToolBar.CancelButton.NavigateUrl = url;
                    }
                }
            }
        }

        public override void BindForm() {
            TaskForm_Ctrl.BindForm();
        }

        protected override void LoadData() {
            if (!IsPostBack) {
                SetFormParameter();
                QSPForm.Business.TaskSystem taskSys = new QSPForm.Business.TaskSystem();
                dTblTask = taskSys.SelectOne(c_TaskID);
                if (dTblTask.Rows.Count == 0) {
                    dTblTask = taskSys.InitializeTask(this.Page.UserID);
                }
                this.ViewState[TASK_ID] = c_TaskID;
                this.ViewState[TASK_DATA] = dTblTask;
            }
            else {
                c_TaskID = Convert.ToInt32(this.ViewState[TASK_ID]);
                dTblTask = (dataDef)this.ViewState[TASK_DATA];
            }
            TaskForm_Ctrl.TaskID = c_TaskID;
            TaskForm_Ctrl.DataSource = dTblTask;
        }

        private void QSPToolBar_DeleteClick(object sender, EventArgs e) {
            //			DeleteUser();
        }

        private void QSPToolBar_SaveClick(object sender, EventArgs e) {
            try {
                bool blnValid = true;
                blnValid = TaskForm_Ctrl.ValidateForm();
                if (!blnValid) {
                    return;
                }

                blnValid = TaskForm_Ctrl.UpdateDataSource();
                if (!blnValid) {
                    return;
                }
                QSPForm.Business.TaskSystem taskSys = new QSPForm.Business.TaskSystem();

                if (dTblTask.Rows[0].RowState == DataRowState.Added) {
                    blnValid = taskSys.Insert(dTblTask);
                }
                else {
                    blnValid = taskSys.Update(dTblTask);
                }

                //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.TaskDetailInfo, TaskDetailInfo.TASK_ID, c_TaskID.ToString());
                string url = "~/TaskDetailInfo.aspx?" + TaskDetailInfo.TASK_ID + "=" + c_TaskID.ToString();
                Response.Redirect(url);
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }
    }
}