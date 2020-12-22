using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.UserTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>UserHeaderForm</summary>
    public partial class UserHeaderForm : BaseWebFormControl {
        #region Item Declarations

        private int c_UserID = 0;
        private dataDef dtblUser = new dataDef();

        QSPForm.Business.UserSystem userSys = new QSPForm.Business.UserSystem();
        private CommonUtility clsUtil = new CommonUtility();
        protected System.Web.UI.WebControls.Label lbProblem;
        private DataTable dtblUserType = new DataTable();
        #endregion Item Declarations

        protected void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {
                if (this.Page.Role == QSPForm.Business.AuthSystem.ROLE_SUPER_USER)
                    tbPassword.TextMode = TextBoxMode.SingleLine;
                else
                    tbPassword.TextMode = TextBoxMode.Password;
            }
        }

        #region auto-generated code
        ///<summary>Required method for Designer support</summary>
        override protected void OnInit(EventArgs e) {
            InitializeComponent();
            base.OnInit(e);
        }

        ///<summary>Required method for Designer support</summary>
        private void InitializeComponent() {
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);

        }
        #endregion auto-generated code

        protected override void LoadData() {
            this.DataSource = this.userSys.SelectOne(c_UserID);
            base.LoadData();
        }

        protected void Page_DataBinding(object sender, System.EventArgs e) {
            try {
                //retreive data detail item for db					
                BindForm();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public int iUserID {
            get {
                return c_UserID;
            }
            set {
                c_UserID = value;
            }
        }

        public dataDef DataSource {
            get {
                return dtblUser;
            }
            set {
                dtblUser = value;
            }
        }

        public override void BindForm() {
            LoadData();
            FillList();
            if (this.DataSource.Rows.Count > 0) {
                DataRow row;
                row = this.DataSource.Rows[0];

                #region Name / Id num
                int UserID = Convert.ToInt32(row[dataDef.FLD_PKID]);
                if (UserID == 0) {
                    this.lbUserID.Text = "New";
                }
                else {
                    this.lbUserID.Text = UserID.ToString();
                }
                if (row[dataDef.FLD_FIRST_NAME] != System.DBNull.Value) {
                    this.tbFirstN.Text = row[dataDef.FLD_FIRST_NAME].ToString();
                }
                if (row[dataDef.FLD_LAST_NAME] != System.DBNull.Value) {
                    this.tbLastN.Text = row[dataDef.FLD_LAST_NAME].ToString();
                }
                #endregion Name / Id num

                #region user credentials
                if (row[dataDef.FLD_USER_NAME] != System.DBNull.Value) {
                    this.tbUserName.Text = row[dataDef.FLD_USER_NAME].ToString();
                }
                if (row[dataDef.FLD_PASSWORD] != System.DBNull.Value) {
                    this.tbPassword.Text = row[dataDef.FLD_PASSWORD].ToString();
                }
                #endregion user credentials

                #region title
                if (row[dataDef.FLD_TITLE] != System.DBNull.Value) {
                    this.tbTitle.Text = row[dataDef.FLD_TITLE].ToString();
                }
                #endregion title

                #region user role
                if (row[dataDef.FLD_ROLE_ID] != System.DBNull.Value) {
                    int roleID = Convert.ToInt32(row[dataDef.FLD_ROLE_ID]);
                    this.ddlType.SelectedValue = roleID.ToString();
                }
                #endregion user role

                #region Email
                if (row[dataDef.FLD_EMAIL] != System.DBNull.Value) {
                    this.tbEmail.Text = row[dataDef.FLD_EMAIL].ToString();
                }
                #endregion Email

                #region Phone Num
                if (row[dataDef.FLD_DAY_PHONE_NO] != System.DBNull.Value) {
                    this.tbDayPH.Text = row[dataDef.FLD_DAY_PHONE_NO].ToString();
                }
                if (row[dataDef.FLD_EVENING_PHONE_NO] != System.DBNull.Value) {
                    this.tbEveningPH.Text = row[dataDef.FLD_EVENING_PHONE_NO].ToString();
                }
                if (row[dataDef.FLD_BEST_TIME_TO_CALL] != System.DBNull.Value) {
                    this.tbBestPH.Text = row[dataDef.FLD_BEST_TIME_TO_CALL].ToString();
                }
                if (row[dataDef.FLD_FAX_NO] != System.DBNull.Value) {
                    this.tbFaxPH.Text = row[dataDef.FLD_FAX_NO].ToString();
                }
                #endregion Phone Num

                #region created / updated
                if (row[dataDef.FLD_CREATE_USER_ID] != System.DBNull.Value) {
                    this.lbCreateBy.Text = this.userSys.NameLookup(Convert.ToInt32(row[dataDef.FLD_CREATE_USER_ID]));
                }
                if (row[dataDef.FLD_CREATE_DATE] != System.DBNull.Value) {
                    this.lbCreateDT.Text = Convert.ToDateTime(row[dataDef.FLD_CREATE_DATE]).ToShortDateString();
                }
                if (row[dataDef.FLD_UPDATE_USER_ID] != System.DBNull.Value) {
                    this.lbUpdateBy.Text = this.userSys.NameLookup(Convert.ToInt32(row[dataDef.FLD_UPDATE_USER_ID]));
                }
                if (row[dataDef.FLD_UPDATE_DATE] != System.DBNull.Value) {
                    this.lbUpdateDT.Text = Convert.ToDateTime(row[dataDef.FLD_UPDATE_DATE]).ToShortDateString();
                }
                #endregion created / updated
            }
            else if (this.iUserID == 0) {
                #region new user
                this.lbUserID.Text = "New";
                this.tbFirstN.Text = "";
                this.tbLastN.Text = "";
                this.tbUserName.Text = "";
                this.tbPassword.Text = "";
                this.tbTitle.Text = "";
                this.ddlType.SelectedValue = "0";
                this.tbEmail.Text = "";
                this.tbDayPH.Text = "";
                this.tbEveningPH.Text = "";
                this.tbBestPH.Text = "";
                this.tbFaxPH.Text = "";
                this.lbCreateBy.Text = "";
                this.lbCreateDT.Text = "";
                this.lbUpdateBy.Text = "";
                this.lbUpdateDT.Text = "";
                #endregion new user
            }
            else {
                string MSG = "This user - id # " + this.iUserID.ToString() + " does not exist";
                MSG += "<br>The user may have been deleted";
                Page.SetPageMessage(MSG);
                this.htmlTableUserHeader.Visible = false;
            }
        }

        public bool UpdateDataSource() {
            bool IsSuccess = false;
            CommonUtility clsUtil = new CommonUtility();
            DataRow row;
            row = this.DataSource.Rows[0];

            clsUtil.UpdateRow(row, dataDef.FLD_FIRST_NAME, this.tbFirstN.Text);
            clsUtil.UpdateRow(row, dataDef.FLD_FIRST_NAME, this.tbFirstN.Text);
            clsUtil.UpdateRow(row, dataDef.FLD_LAST_NAME, this.tbLastN.Text);
            clsUtil.UpdateRow(row, dataDef.FLD_USER_NAME, this.tbUserName.Text);
            clsUtil.UpdateRow(row, dataDef.FLD_PASSWORD, this.tbPassword.Text);
            clsUtil.UpdateRow(row, dataDef.FLD_TITLE, this.tbTitle.Text);
            clsUtil.UpdateRow(row, dataDef.FLD_ROLE_ID, this.ddlType.SelectedValue);
            clsUtil.UpdateRow(row, dataDef.FLD_EMAIL, this.tbEmail.Text);
            clsUtil.UpdateRow(row, dataDef.FLD_DAY_PHONE_NO, this.tbDayPH.Text);
            clsUtil.UpdateRow(row, dataDef.FLD_EVENING_PHONE_NO, this.tbEveningPH.Text);
            clsUtil.UpdateRow(row, dataDef.FLD_BEST_TIME_TO_CALL, this.tbBestPH.Text);
            clsUtil.UpdateRow(row, dataDef.FLD_FAX_NO, this.tbFaxPH.Text);

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
                //User Type
                QSPForm.Business.AuthSystem authSys = new QSPForm.Business.AuthSystem();
                dtblUserType = authSys.SelectAllRole();
                ddlType.DataSource = dtblUserType;
                ddlType.DataValueField = "role_id";
                ddlType.DataTextField = "role_name";
                ddlType.DataBind();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public bool ValidateForm() {
            this.Page.Validate();
            return this.IsValid();
        }
    }
}