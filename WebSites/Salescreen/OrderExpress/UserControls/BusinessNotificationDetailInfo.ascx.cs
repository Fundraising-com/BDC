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
using dataDef = QSPForm.Common.DataDef.BusinessNotificationTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>BusinessNotificationDetailInfo: Read only BusinessNotification item information</summary>
    public partial class BusinessNotificationDetailInfo : BaseWebFormControl {

        protected dataDef dtblNote;
        private bool NoCheck = false;

        //private int c_UserID;
        public const string BIZNOTE_ID = "BizNoteID";
        public const string NO_CHECK = "NoCheck";
        private const string BIZNOTE_DATA = "BizNoteData";
        
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

        protected void Page_Load(object s, System.EventArgs e) {
            try {
                if (!IsPostBack) {
                    if (Request[BIZNOTE_ID] != null) {
                        BizNoteID = Convert.ToInt32(Request[BIZNOTE_ID].ToString());
                    }
                }
                LoadData();
                this.BusinessNotificationInfo_Usr.DataSource = dtblNote;
                this.BusinessNotificationInfo_Usr.BindForm();
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        public int BizNoteID {
            get {
                int bizNote_id = 0;
                if (ViewState[BIZNOTE_ID] != null) {
                    bizNote_id = Convert.ToInt32(ViewState[BIZNOTE_ID]);
                }
                return bizNote_id;
            }
            set {
                ViewState[BIZNOTE_ID] = value;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            if (!IsPostBack) {
                if (Request[NO_CHECK] != null) {
                    NoCheck = Convert.ToBoolean(Request[NO_CHECK]);
                }
                if (!NoCheck) {
                    if (dtblNote != null) {
                        if (dtblNote.Rows.Count > 0) {
                            DataRow noteRow = dtblNote.Rows[0];
                            int NoteType = 0;
                            if (!noteRow.IsNull(dataDef.FLD_BUSINESS_NOTIFICATION_TYPE_ID)) {
                                NoteType = Convert.ToInt32(noteRow[dataDef.FLD_BUSINESS_NOTIFICATION_TYPE_ID]);
                            }
                            if (NoteType != QSPForm.Common.BizNotificationType.TODO) {
                                bool IsRead = false;
                                if (!noteRow.IsNull(dataDef.FLD_IS_COMPLETE)) {
                                    IsRead = Convert.ToBoolean(noteRow[dataDef.FLD_IS_COMPLETE]);
                                }
                                if (!IsRead) {
                                    noteRow[dataDef.FLD_IS_COMPLETE] = true;
                                    noteRow[dataDef.FLD_UPDATE_USER_ID] = this.Page.UserID;
                                    QSPForm.Business.BusinessNotificationSystem bizNoteSys = new QSPForm.Business.BusinessNotificationSystem();
                                    bizNoteSys.Update(dtblNote);
                                }
                            }
                        }
                    }
                }
            }
        }

        protected override void LoadData() {
            QSPForm.Business.BusinessNotificationSystem bizNoteSys = new QSPForm.Business.BusinessNotificationSystem();
            dtblNote = bizNoteSys.SelectOne(BizNoteID);
        }

        protected void imgBtnDelete_Click(object sender, ImageClickEventArgs e) {
            try {
                bool IsSuccess = true;
                QSPForm.Business.BusinessNotificationSystem bizNoteSys = new QSPForm.Business.BusinessNotificationSystem();
                IsSuccess = bizNoteSys.DeleteOne(BizNoteID, this.Page.UserID);
                this.Page.RegisterClientScriptBlock("", "<script>window.opener.RefreshPage();window.close();</script>");
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }
    }
}