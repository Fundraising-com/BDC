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
    /// <summary>
    /// Summary description for AccountDetail.
    /// </summary>
    public partial class ToDoDetail : BaseWebFormControl {
        protected System.Web.UI.WebControls.ValidationSummary ValSum;
        private int c_BizNoteID = 0;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidChange;
        protected System.Web.UI.WebControls.Label lblInstruction;
        protected System.Web.UI.WebControls.Image imgTitle;

        public const string BIZNOTE_ID = "BizNoteID";
        private const string BIZNOTE_DATA = "BizNoteData";
        protected dataDef dTblNote;

        protected void Page_Load(object sender, System.EventArgs e) {
            this.QSPToolBar.ShowDeleteButton = false;
            try {
                // Put user code to initialize the page here	
                LoadData();
                if (!IsPostBack) {
                    //this.Page.ValSummary.Visible = false;
                    BindForm();
                }
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            this.QSPToolBar.SaveClick += new EventHandler(QSPToolBar_SaveClick);
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
        }
        #endregion

        public int BizNoteID {
            get {
                return c_BizNoteID;
            }
            set {
                c_BizNoteID = value;
                ViewState[BIZNOTE_ID] = c_BizNoteID;
            }
        }
        
        protected void Page_PreRender(object sender, System.EventArgs e) {
            this.QSPToolBar.DisplayMode = ToolBar.DISPLAY_EDIT;
            this.ViewState[BIZNOTE_DATA] = dTblNote;
            //Only display when it's a new note
            chkSendMail.Visible = (c_BizNoteID == 0);
        }

        private void QSPToolBar_SaveClick(object sender, EventArgs e) {
            try {
                Boolean blnValid = true;

                blnValid = HeaderDetail.ValidateForm();
                if (!blnValid) {
                    return;
                }

                blnValid = HeaderDetail.UpdateDataSource();
                if (!blnValid) {
                    return;
                }

                QSPForm.Business.BusinessNotificationSystem bizNoteSys = new QSPForm.Business.BusinessNotificationSystem();
                if (c_BizNoteID == 0) {
                    bizNoteSys.SendEmail = chkSendMail.Checked;
                    blnValid = bizNoteSys.Insert(dTblNote);
                }
                else
                    blnValid = bizNoteSys.Update(dTblNote);
                if (blnValid) {
                    //					if((c_BizNoteID == 0) && (this.chkSendMail.Checked))
                    //					{
                    //						QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
                    //						DataRow tdRow = dTblNote.Rows[0];
                    //						Business.UserSystem userSys = new QSPForm.Business.UserSystem();
                    //						if(!tdRow.IsNull(dataDef.FLD_ASSIGNED_USER_ID))
                    //						{
                    //							Common.DataDef.UserTable dtblUser = userSys.SelectOne(Convert.ToInt32(tdRow[dataDef.FLD_ASSIGNED_USER_ID]));
                    //							string userMail = dtblUser.Rows[0][QSPForm.Common.DataDef.UserTable.FLD_EMAIL].ToString();
                    //							if(userMail.Trim() != String.Empty)
                    //							{
                    //								comSys.SendEmailNotification("qspdeveloper@qsp.com",userMail,tdRow[dataDef.FLD_SUBJECT].ToString(),tdRow[dataDef.FLD_MESSAGE].ToString());
                    //							}
                    //						}
                    //					}
                    c_BizNoteID = Convert.ToInt32(dTblNote.Rows[0][dataDef.FLD_PKID]);
                    //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.NoteDetailInfo, BusinessNotificationDetailInfo.BIZNOTE_ID, c_BizNoteID.ToString());
                    string url = "~/BusinessNotificationDetailInfo.aspx?" + BusinessNotificationDetailInfo.BIZNOTE_ID + "=" + c_BizNoteID.ToString();
                    url = url + "&" + BusinessNotificationDetailInfo.NO_CHECK + "=True";
                    Response.Redirect(url, false);
                }
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }
                
        protected void SetFormParameter() {
            if (Request[BIZNOTE_ID] != null) {
                c_BizNoteID = Convert.ToInt32(Request[BIZNOTE_ID].ToString());
            }
            else {
                c_BizNoteID = 0;
            }
            ViewState[BIZNOTE_ID] = c_BizNoteID;
        }

        public override void BindForm() {
            HeaderDetail.BindForm();
        }
        
        protected override void LoadData() {
            if (!IsPostBack) {
                SetFormParameter();
                QSPForm.Business.BusinessNotificationSystem bizNoteSys = new QSPForm.Business.BusinessNotificationSystem();
                //ToDo insert row when c_TODO_ID=0
                //ToDo insert row when c_User_ID=0
                if (c_BizNoteID == 0) {
                    dTblNote = new BusinessNotificationTable();
                    DataRow newRow = dTblNote.NewRow();
                    dTblNote.Rows.Add(newRow);
                }
                else {
                    dTblNote = bizNoteSys.SelectOne(c_BizNoteID);
                }
                
                this.ViewState[BIZNOTE_ID] = c_BizNoteID;
                this.ViewState[BIZNOTE_DATA] = dTblNote;
            }
            else {
                c_BizNoteID = Convert.ToInt32(this.ViewState[BIZNOTE_ID]);
                dTblNote = (dataDef)this.ViewState[BIZNOTE_DATA];
            }

            HeaderDetail.BizNoteID = c_BizNoteID;
            HeaderDetail.DataSource = dTblNote;
        }
    }
}