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
    ///<summary>ToDoDetailInfo: Read only ToDo item information</summary>
    public partial class ProgramDetailInfo : BaseWebFormControl {

        protected System.Web.UI.WebControls.HyperLink hypLnkClose;
        protected dataDef dtblProgram;

        //private int c_UserID;
        //private int cProgramID = 0;
        public const string PROGRAM_ID = "program_id";
        private const string PROGRAM_DATA = "ProgramData";

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
                    if (Request[PROGRAM_ID] != null) {
                        iProgramID = Convert.ToInt32(Request[PROGRAM_ID].ToString());
                    }
                }
                LoadData();
                this.ctrlProgramInfo.DataSource = dtblProgram;
                this.ctrlProgramInfo.DataBind();
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        private void QSPToolBar_EditClick(object sender, EventArgs e) {
            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.ProgramDetail, BaseProgramDetail.PROGRAM_ID, iProgramID.ToString());
            string url = "~/ProgramDetail.aspx?" + BaseProgramDetail.PROGRAM_ID + "=" + iProgramID.ToString();
            Response.Redirect(url);
        }

        public int iProgramID {
            get {
                int program_ID = 0;
                if (ViewState[PROGRAM_ID] != null) {
                    program_ID = Convert.ToInt32(ViewState[PROGRAM_ID]);
                }
                return program_ID;
            }
            set {
                ViewState[PROGRAM_ID] = value;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
        }

        protected override void LoadData() {
            QSPForm.Business.ProgramSystem programSys = new QSPForm.Business.ProgramSystem();
            dtblProgram = programSys.SelectOne(iProgramID);
        }
    }
}