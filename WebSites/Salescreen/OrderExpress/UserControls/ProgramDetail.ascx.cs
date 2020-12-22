//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\Stub_ProgramDetail_ascx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'ProgramDetail.ascx' was also modified to refer to the new class name.
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
using dataDef = QSPForm.Common.DataDef.ProgramTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>Program Detail form</summary>
    public partial class ProgramDetail : BaseProgramDetail {
        private int c_ProgramID;
        protected System.Web.UI.WebControls.ImageButton imgBtnSave;
        protected dataDef dTblProgram;

        private const string PROGRAM_DATA = "ProgramData";

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
            if (Request[PROGRAM_ID] != null) {
                c_ProgramID = Convert.ToInt32(Request[PROGRAM_ID].ToString());
            }
            else {
                c_ProgramID = 0;
            }
            ViewState[PROGRAM_ID] = c_ProgramID;
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            this.ViewState[PROGRAM_DATA] = dTblProgram;
            //Set the close button
            if (!IsPostBack) {
                if (dTblProgram.Rows.Count > 0) {
                    if (dTblProgram.Rows[0].RowState != DataRowState.Added) {
                        //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.ProgramDetailInfo, ProgramDetailInfo.PROGRAM_ID, c_ProgramID.ToString());
                        string url = "~/ProgramDetailInfo.aspx?" + ProgramDetailInfo.PROGRAM_ID + "=" + c_ProgramID.ToString();
                        this.QSPToolBar.CancelButton.NavigateUrl = url;
                    }
                }
            }
        }

        public override void BindForm() {
            ProgramForm_Ctrl.BindForm();
        }

        protected override void LoadData() {
            if (!IsPostBack) {
                SetFormParameter();
                QSPForm.Business.ProgramSystem programSys = new QSPForm.Business.ProgramSystem();
                dTblProgram = programSys.SelectOne(c_ProgramID);
                if (dTblProgram.Rows.Count == 0) {
                    dTblProgram = programSys.InitializeProgram(this.Page.UserID);
                }
                this.ViewState[PROGRAM_ID] = c_ProgramID;
                this.ViewState[PROGRAM_DATA] = dTblProgram;
            }
            else {
                c_ProgramID = Convert.ToInt32(this.ViewState[PROGRAM_ID]);
                dTblProgram = (dataDef)this.ViewState[PROGRAM_DATA];
            }
            ProgramForm_Ctrl.ProgramID = c_ProgramID;
            ProgramForm_Ctrl.DataSource = dTblProgram;
        }

        private void QSPToolBar_DeleteClick(object sender, EventArgs e) {
            //			DeleteUser();
        }

        private void QSPToolBar_SaveClick(object sender, EventArgs e) {
            try {
                bool blnValid = true;
                blnValid = ProgramForm_Ctrl.ValidateForm();
                if (!blnValid) {
                    return;
                }

                blnValid = ProgramForm_Ctrl.UpdateDataSource();
                if (!blnValid) {
                    return;
                }
                QSPForm.Business.ProgramSystem programSys = new QSPForm.Business.ProgramSystem();

                if (dTblProgram.Rows[0].RowState == DataRowState.Added) {
                    blnValid = programSys.Insert(dTblProgram);
                }
                else {
                    blnValid = programSys.Update(dTblProgram);
                }

                //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.ProgramDetailInfo, ProgramDetailInfo.PROGRAM_ID, c_ProgramID.ToString());
                string url = "~/ProgramDetailInfo.aspx?" + ProgramDetailInfo.PROGRAM_ID + "=" + c_ProgramID.ToString();
                Response.Redirect(url);
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }
    }
}