using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataRef = QSPForm.Common.DataDef.TaskTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>Task item Information - read only</summary>
    public partial class TaskInfo : BaseWebFormControl {
        private CommonUtility util = new CommonUtility();
        protected dataRef dTblTask;

        protected void Page_Load(object sender, System.EventArgs e) {
        }

        #region Web Form Designer generated code
        ///<summary>Required method for Designer support</summary>
        override protected void OnInit(EventArgs e) {
            InitializeComponent();
            base.OnInit(e);
        }

        ///<summary>Required method for Designer support</summary>
        private void InitializeComponent() {
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);

        }
        #endregion Web Form Designer generated code

        public dataRef DataSource {
            get {
                return dTblTask;
            }
            set {
                dTblTask = value;
            }
        }

        public override void BindForm() {
            if (dTblTask.Rows.Count > 0) {
                DataRow row = dTblTask.Rows[0];
                lblTaskID.Text = row[dataRef.FLD_PKID].ToString();
                lblTaskName.Text = row[dataRef.FLD_TASK_NAME].ToString();

                lblTaskTypeName.Text = row[dataRef.FLD_TASK_TYPE_NAME].ToString();
                lblNoteType.Text = row[dataRef.FLD_BUSINESS_NOTIFICATION_TYPE_NAME].ToString();

                lblDescription.Text = row[dataRef.FLD_DESCRIPTION].ToString();

                lblStoredProcName.Text = row[dataRef.FLD_TASK_SP].ToString();
                lblStoredProcParameterName.Text = row[dataRef.FLD_PARAMETER_NAME].ToString();
                int TemplateEmailID = 0;
                lblTemplateName.Text = "";
                if (!row.IsNull(dataRef.FLD_TEMPLATE_EMAIL_ID)) {
                    TemplateEmailID = Convert.ToInt32(row[dataRef.FLD_TEMPLATE_EMAIL_ID]);
                    lblTemplateName.Text = row[dataRef.FLD_TEMPLATE_EMAIL_NAME].ToString();
                    CommonUtility clsUtil = new CommonUtility();
                    //	clsUtil.SetJScriptForOpenDetail(imgBtnDetailTmplEmail, QSPForm.Business.AppItem.TemplateEmailDetailInfo, TemplateEmailDetailInfo.TEMPLATE_EMAIL_ID, TemplateEmailID.ToString(),0,0);
                    clsUtil.SetJScriptForOpenDetailNoCMS(imgBtnDetailTmplEmail, "TemplateEmailDetailInfo.aspx?", TemplateEmailDetailInfo.TEMPLATE_EMAIL_ID, TemplateEmailID.ToString(), 0, 0);
                }

                //Display management
                trParamName.Visible = false;
                trProcName.Visible = false;
                trTemplateEmail.Visible = false;
                trNoteType.Visible = false;

                int TaskTypeID = 0;
                if (!row.IsNull(dataRef.FLD_TASK_TYPE_ID)) {
                    TaskTypeID = Convert.ToInt32(row[dataRef.FLD_TASK_TYPE_ID]);
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
        }

        protected void Page_DataBinding(object sender, EventArgs e) {
            BindForm();
        }
    }
}