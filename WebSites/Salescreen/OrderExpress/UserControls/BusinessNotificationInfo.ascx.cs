using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataRef = QSPForm.Common.DataDef.BusinessNotificationTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>BusinessNotification item Information - read only</summary>
    public partial class BusinessNotificationInfo : BaseWebFormControl {

        private CommonUtility util = new CommonUtility();
        protected dataRef dTblBusinessNotification;

        #region auto-generated code
        ///<summary>Required method for Designer support</summary>
        override protected void OnInit(EventArgs e) {
            InitializeComponent();
            base.OnInit(e);
        }

        ///<summary>Required method for Designer support</summary>
        private void InitializeComponent() {


        }
        #endregion auto-generated code

        protected void Page_Load(object sender, System.EventArgs e) {
        }

        public dataRef DataSource {
            get {
                return dTblBusinessNotification;
            }
            set {
                dTblBusinessNotification = value;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            trBizNoteID.Visible = (Page.Role == QSPForm.Business.AuthSystem.ROLE_SUPER_USER);
            trBizName.Visible = (Page.Role == QSPForm.Business.AuthSystem.ROLE_SUPER_USER);
            trBizNoteType.Visible = (Page.Role == QSPForm.Business.AuthSystem.ROLE_SUPER_USER);
            trContextInfo.Visible = (Page.Role == QSPForm.Business.AuthSystem.ROLE_SUPER_USER);
            trDescription.Visible = (Page.Role == QSPForm.Business.AuthSystem.ROLE_SUPER_USER);
        }

        public override void BindForm() {
            if (dTblBusinessNotification.Rows.Count > 0) {
                DataRow row = dTblBusinessNotification.Rows[0];
                lblBusinessNotificationID.Text = row[dataRef.FLD_PKID].ToString();
                lblBusinessNotificationName.Text = row[dataRef.FLD_BUSINESS_NOTIFICATION_NAME].ToString();

                if (!row.IsNull(dataRef.FLD_BUSINESS_NOTIFICATION_TYPE_ID)) {
                    int NoteTypeID = 0;
                    NoteTypeID = Convert.ToInt32(row[dataRef.FLD_BUSINESS_NOTIFICATION_TYPE_ID]);
                    if (NoteTypeID != QSPForm.Common.BizNotificationType.TODO) {
                        trComplete.Visible = false;
                    }
                    lblNoteType.Text = row[dataRef.FLD_BUSINESS_NOTIFICATION_TYPE_NAME].ToString();

                }
                lblReceivedDate.Text = row[dataRef.FLD_CREATE_DATE].ToString();
                txtCreatorUserName.Text = row[dataRef.FLD_CREATE_USER_NAME].ToString();
                txtAssignedUserName.Text = row[dataRef.FLD_ASSIGNED_USER_NAME].ToString();

                txtSubject.Text = row[dataRef.FLD_SUBJECT].ToString();
                lblMessage.Text = row[dataRef.FLD_MESSAGE].ToString();

                lblDescription.Text = row[dataRef.FLD_DESCRIPTION].ToString();

                if (!row.IsNull(dataRef.FLD_ENTITY_TYPE_ID)) {
                    //Means that we have a context associate to this notification
                    string sContextInfo = "";
                    sContextInfo = row[dataRef.FLD_ENTITY_TYPE_NAME].ToString();
                    if (!row.IsNull(dataRef.FLD_ENTITY_ID)) {
                        sContextInfo = sContextInfo + "&nbsp;#" + row[dataRef.FLD_ENTITY_ID].ToString();
                    }
                    lblContextInfo.Text = sContextInfo;
                }
                if (!row.IsNull(dataRef.FLD_COMPLETE_DATE))
                    lblCompletionDate.Text = Convert.ToDateTime(row[dataRef.FLD_COMPLETE_DATE]).ToShortDateString()
                        + " " + Convert.ToDateTime(row[dataRef.FLD_COMPLETE_DATE]).ToShortTimeString();
                if (!row.IsNull(dataRef.FLD_IS_COMPLETE))
                    chkBoxCompleted.Checked = Convert.ToBoolean(row[dataRef.FLD_IS_COMPLETE]);
            }
        }
    }
}