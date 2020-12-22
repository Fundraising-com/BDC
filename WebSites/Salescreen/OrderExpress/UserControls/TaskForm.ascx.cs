using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.TaskTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for TaskForm.
    /// </summary>
    public partial class TaskForm : BaseWebFormControl {
        protected dataDef dTblTask;

        private int c_TaskID = 0;

        private CommonUtility clsUtil = new CommonUtility();

        override protected void OnLoad(EventArgs e) {
            // Put user code to initialize the page here			
            if (!IsPostBack) {
                //FillDataTableForDropDownList();
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            //			InitControl();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
        }
        #endregion

        public int TaskID {
            get {
                return c_TaskID;
            }
            set {
                c_TaskID = value;
            }
        }

        public dataDef DataSource {
            get {
                return dTblTask;
            }
            set {
                dTblTask = value;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            //Display management
            trParamName.Visible = false;
            trProcName.Visible = false;
            trTemplateEmail.Visible = false;
            trNoteType.Visible = false;

            int TaskTypeID = 0;
            if (ddlTaskType.SelectedIndex > -1) {
                TaskTypeID = Convert.ToInt32(ddlTaskType.SelectedValue);
                if (TaskTypeID == QSPForm.Common.TaskType.EXECUTE_SQL) {
                    trParamName.Visible = true;
                    trProcName.Visible = true;
                }
                else if (TaskTypeID == QSPForm.Common.TaskType.SEND_NOTIFICATION) {
                    trTemplateEmail.Visible = true;
                    trNoteType.Visible = true;
                }
            }
        }

        public new void BindForm() {
            if (!IsPostBack)
                FillDataTableForDropDownList();

            DataRow taskRow = dTblTask.Rows[0];
            //First section -- Account Name and Billing Address
            if (taskRow.RowState == DataRowState.Added)
                lblTaskID.Text = "New Task";
            else
                lblTaskID.Text = taskRow[dataDef.FLD_PKID].ToString();

            txtTaskName.Text = taskRow[dataDef.FLD_TASK_NAME].ToString();

            if (!taskRow.IsNull(dataDef.FLD_TASK_TYPE_ID)) {
                ListItem lstItem = ddlTaskType.Items.FindByValue(taskRow[dataDef.FLD_TASK_TYPE_ID].ToString());
                if (lstItem != null) {
                    ddlTaskType.ClearSelection();
                    lstItem.Selected = true;
                }
            }

            if (!taskRow.IsNull(dataDef.FLD_BUSINESS_NOTIFICATION_TYPE_ID)) {
                ListItem lstItem = ddlNoteType.Items.FindByValue(taskRow[dataDef.FLD_BUSINESS_NOTIFICATION_TYPE_ID].ToString());
                if (lstItem != null) {
                    ddlNoteType.ClearSelection();
                    lstItem.Selected = true;
                }
            }

            if (!taskRow.IsNull(dataDef.FLD_TEMPLATE_EMAIL_ID)) {
                ListItem lstItem = ddlTemplateEmail.Items.FindByValue(taskRow[dataDef.FLD_TEMPLATE_EMAIL_ID].ToString());
                if (lstItem != null) {
                    ddlTemplateEmail.ClearSelection();
                    lstItem.Selected = true;
                }
            }
            txtProcName.Text = taskRow[dataDef.FLD_TASK_SP].ToString();
            txtParamName.Text = taskRow[dataDef.FLD_PARAMETER_NAME].ToString();
        }

        public bool ValidateForm() {
            ReqFldVal_ParamName.Enabled = false;
            ReqFldVal_ProcName.Enabled = false;
            ReqFldVal_TemplateEmail.Enabled = false;
            ReqFldVal_NoteType.Enabled = false;

            int TaskTypeID = 0;
            if (ddlTaskType.SelectedIndex > -1) {
                TaskTypeID = Convert.ToInt32(ddlTaskType.SelectedValue);

                if (TaskTypeID == QSPForm.Common.TaskType.EXECUTE_SQL) {
                    ReqFldVal_ParamName.Enabled = true;
                    ReqFldVal_ProcName.Enabled = true;
                }
                else if (TaskTypeID == QSPForm.Common.TaskType.SEND_NOTIFICATION) {
                    ReqFldVal_TemplateEmail.Enabled = true;
                    ReqFldVal_NoteType.Enabled = true;
                }
            }

            if (!IsValid()) {
                return false;
            }

            //if everything have been ok
            return true;
        }

        public bool UpdateDataSource() {
            if (dTblTask.Rows.Count > 0) {
                DataRow taskRow = dTblTask.Rows[0];
                //-------------------------------------------------
                //		Task Information
                //-------------------------------------------------

                int taskID = Convert.ToInt32(taskRow[TaskTable.FLD_PKID]);

                clsUtil.UpdateRow(taskRow, TaskTable.FLD_TASK_NAME, txtTaskName.Text);
                clsUtil.UpdateRow(taskRow, TaskTable.FLD_TASK_TYPE_ID, ddlTaskType.SelectedValue);

                int TaskTypeID = Convert.ToInt32(ddlTaskType.SelectedValue);
                if (TaskTypeID == QSPForm.Common.TaskType.EXECUTE_SQL) {
                    clsUtil.UpdateRow(taskRow, TaskTable.FLD_TASK_SP, txtProcName.Text);
                    clsUtil.UpdateRow(taskRow, TaskTable.FLD_PARAMETER_NAME, txtParamName.Text);
                    clsUtil.UpdateRow(taskRow, TaskTable.FLD_TEMPLATE_EMAIL_ID, "");
                    clsUtil.UpdateRow(taskRow, TaskTable.FLD_BUSINESS_NOTIFICATION_TYPE_ID, "");
                }
                else if (TaskTypeID == QSPForm.Common.TaskType.SEND_NOTIFICATION) {
                    clsUtil.UpdateRow(taskRow, TaskTable.FLD_TASK_SP, "");
                    clsUtil.UpdateRow(taskRow, TaskTable.FLD_PARAMETER_NAME, "");
                    clsUtil.UpdateRow(taskRow, TaskTable.FLD_TEMPLATE_EMAIL_ID, ddlTemplateEmail.SelectedValue);
                    clsUtil.UpdateRow(taskRow, TaskTable.FLD_BUSINESS_NOTIFICATION_TYPE_ID, ddlNoteType.SelectedValue);
                }
                else {
                    clsUtil.UpdateRow(taskRow, TaskTable.FLD_TASK_SP, "");
                    clsUtil.UpdateRow(taskRow, TaskTable.FLD_PARAMETER_NAME, "");
                    clsUtil.UpdateRow(taskRow, TaskTable.FLD_TEMPLATE_EMAIL_ID, "");
                    clsUtil.UpdateRow(taskRow, TaskTable.FLD_BUSINESS_NOTIFICATION_TYPE_ID, "");
                }
            }

            return true;
        }

        protected override void OnDataBinding(EventArgs e) {
            BindForm();
        }

        private void FillDataTableForDropDownList() {
            try {
                CommonUtility clsUtil = new CommonUtility();
                clsUtil.SetTaskTypeDropDownList(ddlTaskType, true);
                clsUtil.SetTemplateEmailDropDownList(ddlTemplateEmail, true);
                clsUtil.SetBizNotificationTypeDropDownList(ddlNoteType, true);

            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }
    }
}