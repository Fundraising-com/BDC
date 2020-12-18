using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataRef = QSPForm.Common.DataDef.UserTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>User Information - read only</summary>
    public partial class UserInfo : BaseWebFormControl {
        #region Item Declarations
        private CommonUtility util = new CommonUtility();
        protected dataRef dtblUser;
        protected System.Web.UI.WebControls.Label Label1;
        protected System.Web.UI.WebControls.Label Label2;
        protected System.Web.UI.WebControls.Label Label3;
        #endregion Item Declarations

        protected void Page_Load(object sender, System.EventArgs e) {
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

        public dataRef DataSource {
            get {
                return dtblUser;
            }
            set {
                dtblUser = value;
            }
        }

        public override void BindForm() {
            if (dtblUser.Rows.Count > 0) {
                DataRow row;
                row = dtblUser.Rows[0];

                int UserID = Convert.ToInt32(row[dataRef.FLD_PKID]);
                if (UserID == 0) {
                    this.lbUserID.Text = "New";
                }
                else {
                    this.lbUserID.Text = UserID.ToString();
                }

                if (row[dataRef.FLD_FIRST_NAME] != System.DBNull.Value) {
                    this.lblFName.Text = row[dataRef.FLD_FIRST_NAME].ToString();
                }

                if (row[dataRef.FLD_LAST_NAME] != System.DBNull.Value) {
                    this.lblLName.Text = row[dataRef.FLD_LAST_NAME].ToString();
                }

                if (row[dataRef.FLD_USER_NAME] != System.DBNull.Value) {
                    this.lbUserName.Text = row[dataRef.FLD_USER_NAME].ToString();
                }

                if (row[dataRef.FLD_PASSWORD] != System.DBNull.Value) {
                    if (this.Page.Role == QSPForm.Business.AuthSystem.ROLE_SUPER_USER)
                        this.lbPassword.Text = row[dataRef.FLD_PASSWORD].ToString();
                    else
                        lbPassword.Text = "***************";
                }

                #region Email
                if (row[dataRef.FLD_EMAIL] != System.DBNull.Value) {
                    this.hlEmail.Text = row[dataRef.FLD_EMAIL].ToString();
                    this.hlEmail.NavigateUrl = "mailto:" + this.hlEmail.Text;
                    this.hlEmail.Visible = true;
                    this.lbEmail_none.Visible = false;
                }
                else {
                    this.hlEmail.Visible = false;
                    this.lbEmail_none.Visible = true;
                }
                #endregion Email

                #region Phone Num
                if (row[dataRef.FLD_DAY_PHONE_NO] != System.DBNull.Value) {
                    this.lbDayPH.Text = row[dataRef.FLD_DAY_PHONE_NO].ToString();
                }
                if (row[dataRef.FLD_EVENING_PHONE_NO] != System.DBNull.Value) {
                    this.lbEveningPH.Text = row[dataRef.FLD_EVENING_PHONE_NO].ToString();
                }
                if (row[dataRef.FLD_BEST_TIME_TO_CALL] != System.DBNull.Value) {
                    this.lbBestPH.Text = row[dataRef.FLD_BEST_TIME_TO_CALL].ToString();
                }
                if (row[dataRef.FLD_FAX_NO] != System.DBNull.Value) {
                    this.lbFaxPH.Text = row[dataRef.FLD_FAX_NO].ToString();
                }
                #endregion Phone Num

                if (row[dataRef.FLD_TITLE] != System.DBNull.Value) {
                    this.lbTitle.Text = row[dataRef.FLD_TITLE].ToString();
                }

                #region user role
                //				if(row[dataRef.FLD_ROLE_ID] != System.DBNull.Value)
                //				{
                //					int roleID = Convert.ToInt32(row[dataRef.FLD_ROLE_ID]);
                //					switch (roleID)
                //					{
                //						case 0:
                //							this.lbRole.Text = "User";
                //							break;
                //						case 1:
                //							this.lbRole.Text = "FM";
                //							break;
                //						case 2:
                //							this.lbRole.Text = "Data Entry User";
                //							break;
                //						case 3:
                //							this.lbRole.Text = "Help Desk User";
                //							break;
                //						case 4:
                //							this.lbRole.Text = "Administrator";
                //							break;
                //						default:
                //							this.lbRole.Text = roleID.ToString() + " : role unknown";
                //							break;
                //					}
                //				}
                if (row[dataRef.FLD_ROLE_NAME].ToString().Trim() == String.Empty) {
                    this.lbRole.Text = "role unknown";
                }
                else {
                    this.lbRole.Text = row[dataRef.FLD_ROLE_NAME].ToString();
                }

                #endregion user role

                //todo:JLC: add these fields
                #region fields
                //dataDef.FLD_DELETED;
                #endregion fields

                #region created / updated
                //				QSPForm.Business.UserSystem userSys = new QSPForm.Business.UserSystem();
                //				if(row[dataRef.FLD_CREATE_USER_ID] != System.DBNull.Value)
                //				{
                //					this.lbCreateBy.Text = userSys.NameLookup(
                //						Convert.ToInt32(row[dataRef.FLD_CREATE_USER_ID])
                //						);
                //				}
                //				if(row[dataRef.FLD_CREATE_DATE] != System.DBNull.Value)
                //				{
                //					this.lbCreateDT.Text = Convert.ToDateTime(row[dataRef.FLD_CREATE_DATE]).ToShortDateString();
                //				}
                //				if(row[dataRef.FLD_UPDATE_USER_ID] != System.DBNull.Value)
                //				{
                //					this.lbUpdateBy.Text = userSys.NameLookup(
                //						Convert.ToInt32(row[dataRef.FLD_UPDATE_USER_ID])
                //						);
                //				}
                //				if(row[dataRef.FLD_UPDATE_DATE] != System.DBNull.Value)
                //				{
                //					this.lbUpdateDT.Text = Convert.ToDateTime(row[dataRef.FLD_UPDATE_DATE]).ToShortDateString();
                //				}
                #endregion created / updated
            }
        }

        protected void Page_DataBinding(object sender, EventArgs e) {
            BindForm();
        }
    }
}