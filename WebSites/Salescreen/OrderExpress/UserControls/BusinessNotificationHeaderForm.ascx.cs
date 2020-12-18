using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.BusinessNotificationTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>BusinessNotificationHeaderForm</summary>
    public partial class BusinessNotificationHeaderForm : BaseWebFormControl {

        protected System.Web.UI.HtmlControls.HtmlTable htmlTableBusinessNotificationHeader;
        protected System.Web.UI.WebControls.TextBox tbBusinessNotificationName;
        protected System.Web.UI.WebControls.RequiredFieldValidator rq_tbBusinessNotificationName;

        private int c_BizNoteID = 0;
        private dataDef dtblNote = new dataDef();

        QSPForm.Business.BusinessNotificationSystem todoSys = new QSPForm.Business.BusinessNotificationSystem();
        //QSPForm.Business.UserSystem userSys = new QSPForm.Business.UserSystem();
        private CommonUtility clsUtil = new CommonUtility();
        protected System.Web.UI.WebControls.Label lbProblem;
        private DataTable dtblUserType = new DataTable();

        protected void Page_Load(object sender, System.EventArgs e) {
            // clsUtil.SetJScriptForOpenSelector(imgBtnSelectFM, txtFMID, txtFMName, "FMSelector.aspx", "FMSelector", 0, 0, null);
            clsUtil.SetJScriptForOpenSelector(imgBtnSelectUser, txtAssignedUserID, txtAssignedUserName, "UserSelector.aspx", "UserSelector", 0, 0, null);
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
        #endregion

        protected void Page_DataBinding(object sender, System.EventArgs e) {
            try {
                //retreive data detail item for db					
                BindForm();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public int BizNoteID {
            get {
                return c_BizNoteID;
            }
            set {
                c_BizNoteID = value;
            }
        }

        public dataDef DataSource {
            get {
                return dtblNote;
            }
            set {
                dtblNote = value;
            }
        }

        public override void BindForm() {
            FillList();
            if (dtblNote.Rows.Count > 0) {
                DataRow row;
                row = dtblNote.Rows[0];

                int BusinessNotificationID = Convert.ToInt32(row[dataDef.FLD_PKID]);
                if (BusinessNotificationID == 0)
                    lblBusinessNotificationID.Text = "New";
                else
                    lblBusinessNotificationID.Text = BusinessNotificationID.ToString();
                //Biz Note Name
                txtName.Text = row[dataDef.FLD_BUSINESS_NOTIFICATION_NAME].ToString();
                //Biz Task Name
                lblTask.Text = row[dataDef.FLD_BUSINESS_TASK_NAME].ToString();
                //Assigned User
                if (!row.IsNull(dataDef.FLD_ASSIGNED_USER_ID)) {
                    txtAssignedUserID.Text = row[dataDef.FLD_ASSIGNED_USER_ID].ToString();
                    txtAssignedUserName.Text = row[dataDef.FLD_ASSIGNED_USER_NAME].ToString();
                }
                //Context - Entity type- EntityID				
                if (!row.IsNull(dataDef.FLD_ENTITY_TYPE_ID)) {
                    string sEntityTypeID = row[dataDef.FLD_ENTITY_TYPE_ID].ToString();
                    ListItem lstItem = ddlEntityTypeID.Items.FindByValue(sEntityTypeID);
                    if (lstItem != null) {
                        ddlEntityTypeID.ClearSelection();
                        lstItem.Selected = true;
                    }
                }
                txtEntityID.Text = row[dataDef.FLD_ENTITY_ID].ToString();
                //Subject
                txtSubject.Text = row[dataDef.FLD_SUBJECT].ToString();
                //Message
                txtMessage.Text = row[dataDef.FLD_MESSAGE].ToString();
                //Description
                txtDescription.Text = row[dataDef.FLD_DESCRIPTION].ToString();

                //Completion
                if (!row.IsNull(dataDef.FLD_IS_COMPLETE)) {
                    chkComplete.Checked = Convert.ToBoolean(row[dataDef.FLD_IS_COMPLETE]);
                }
                lblCompleteDate.Text = row[dataDef.FLD_COMPLETE_DATE].ToString();

                //Create and Update Information
                //				QSPForm.Business.UserSystem userSys = new QSPForm.Business.UserSystem();
                //				if(row[dataDef.FLD_CREATE_USER_ID] != System.DBNull.Value)
                //				{
                //					this.lblCreateBy.Text = userSys.NameLookup(
                //						Convert.ToInt32(row[dataDef.FLD_CREATE_USER_ID])
                //						);
                //				}
                //				if(row[dataDef.FLD_CREATE_DATE] != System.DBNull.Value)
                //				{
                //					this.lbCreateDT.Text = Convert.ToDateTime(row[dataDef.FLD_CREATE_DATE]).ToShortDateString();
                //				}
                //				if(row[dataDef.FLD_UPDATE_USER_ID] != System.DBNull.Value)
                //				{
                //					this.lbUpdateBy.Text = userSys.NameLookup(
                //						Convert.ToInt32(row[dataDef.FLD_UPDATE_USER_ID])
                //						);
                //				}
                //				if(row[dataDef.FLD_UPDATE_DATE] != System.DBNull.Value)
                //				{
                //					this.lbUpdateDT.Text = Convert.ToDateTime(row[dataDef.FLD_UPDATE_DATE]).ToShortDateString();
                //				}

                if (row.RowState == DataRowState.Added) {
                    chkComplete.Enabled = false;
                }
            }
            else {
                string msg = "This Note - id # " + this.BizNoteID.ToString() + " does not exist";
                msg += "<br>The Note may have been deleted";
                Page.SetPageMessage(msg);
                this.htmlTableBusinessNotificationHeader.Visible = false;
            }
        }

        public bool UpdateDataSource() {
            bool IsSuccess = false;

            DataRow row;
            row = this.DataSource.Rows[0];
            CommonUtility clsUtil = new CommonUtility();

            clsUtil.UpdateRow(row, dataDef.FLD_BUSINESS_NOTIFICATION_NAME, txtName.Text);
            clsUtil.UpdateRow(row, dataDef.FLD_BUSINESS_NOTIFICATION_TYPE_ID, ddlNoteType.SelectedValue);
            //Biz Task is not updatable
            clsUtil.UpdateRow(row, dataDef.FLD_ASSIGNED_USER_ID, txtAssignedUserID.Text);
            clsUtil.UpdateRow(row, dataDef.FLD_ENTITY_ID, txtEntityID.Text);

            if (ddlEntityTypeID.SelectedIndex > 0)
                clsUtil.UpdateRow(row, dataDef.FLD_ENTITY_TYPE_ID, ddlEntityTypeID.SelectedValue);
            else
                clsUtil.UpdateRow(row, dataDef.FLD_ENTITY_TYPE_ID, "");

            clsUtil.UpdateRow(row, dataDef.FLD_SUBJECT, txtSubject.Text);
            clsUtil.UpdateRow(row, dataDef.FLD_MESSAGE, txtMessage.Text);
            clsUtil.UpdateRow(row, dataDef.FLD_DESCRIPTION, txtDescription.Text);
            clsUtil.UpdateRow(row, dataDef.FLD_IS_COMPLETE, chkComplete.Checked.ToString());

            //clsUtil.UpdateRow(row,dataDef.FLD_COMPLETE_DATE,this.tbCompleteDT.Text);

            if (row.RowState != DataRowState.Unchanged) {
                if (row.RowState == DataRowState.Added) {
                    row[dataDef.FLD_CREATE_USER_ID] = Page.UserID;
                }
                else {
                    row[dataDef.FLD_UPDATE_USER_ID] = Page.UserID;
                }
                IsSuccess = true;
            }

            return IsSuccess;
        }

        private void FillList() {
            try {
                clsUtil.SetBizNotificationTypeDropDownList(ddlNoteType, true);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public bool ValidateForm() {
            return this.IsValid();
        }
    }
}