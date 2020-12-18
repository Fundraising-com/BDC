//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\Stub_SessionInfo_ascx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'SessionInfo.ascx' was also modified to refer to the new class name.
// For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>SessionInfo: User information displayed in the side of every page</summary>
    public partial class SessionInfo : BaseSessionInfo {
        #region auto-generated code
        ///<summary>Required method for Designer support</summary>
        override protected void OnInit(EventArgs e) {
            InitializeComponent();
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);
            base.OnInit(e);
        }

        ///<summary>Required method for Designer support</summary>
        private void InitializeComponent() {
        }
        #endregion auto-generated code

        #region Item Declarations
        private CampaignTable camp = new CampaignTable();
        #endregion Item Declarations

        protected void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {
                //				string strPageURl = Page.GetPageToGo(QSPForm.Business.AppItem.Admin_Campaigns_Selection);
                //				lnkChange.NavigateUrl = strPageURl;
            }
        }

        private void Page_DataBinding(object sender, System.EventArgs e) {
            //Campaign info			
            //			camp = Page.CampaignTableInfo;
            //			if (camp.Rows.Count >0)
            //			{
            //				DataRow row = camp.Rows[0];				
            //				lblCampName.Text= row[CampaignTable.FLD_NAME].ToString();
            //				//Start Date
            //				if (row[CampaignTable.FLD_START_DATE].ToString() != String.Empty)
            //				{
            //					lblStartDate.Text = Convert.ToDateTime(row[CampaignTable.FLD_START_DATE]).ToShortDateString();
            //					
            //				}
            //				else
            //				{
            //					lblLabelStartDate.Text = "N/D";
            //				}
            //				//End Date
            //				if (row[CampaignTable.FLD_END_DATE].ToString() != String.Empty)
            //				{
            //					lblEndDate.Text = Convert.ToDateTime(row[CampaignTable.FLD_END_DATE]).ToShortDateString();
            //					
            //				}
            //				else
            //				{
            //					lblStartDate.Text = "N/D";
            //				}
            //				
            //				//Retreive information based on this account;				
            //			
            //				lblAccountNumber.Text = row[AccountOwnershipTable.FLD_ACCOUNT_ID].ToString().PadLeft(9,'0');
            //				lblFY.Text = row[AccountOwnershipTable.FLD_FISCALYR].ToString();				
            //				
            //				lblProgramType.Text  = row["ProgramDescription"].ToString();
            //				lblProductType.Text = row["ProductDescription"].ToString().PadLeft(4,'0');;
            //
            //				//Retreive info on the FM related
            //				lblFMName.Text  = row["FM_Name"].ToString();
            //				lblFMNo.Text = row[AccountOwnershipTable.FLD_FM_ID].ToString().PadLeft(4,'0');;
            //				
            //			}

            //User info
            //bool IsCUser = false;

            if (QSPForm.Business.AuthSystem.ROLE_USER == Page.Role) {
                //Go in Standard QSPForm User
                UserTable user = new UserTable();
                QSPForm.Business.UserSystem userSys = new QSPForm.Business.UserSystem();
                user = userSys.SelectOne(Page.UserID);
                if (user.Rows.Count > 0) {
                    lblUserName.Text = user.Rows[0][UserTable.FLD_USER_NAME].ToString();
                }

            }
            else {
                //Go in CUSerProfile
                CUserTable cuser = new CUserTable();
                QSPForm.Business.CUserSystem cuserSys = new QSPForm.Business.CUserSystem();
                cuser = cuserSys.SelectOne(Page.UserID);
                if (cuser.Rows.Count > 0) {
                    lblUserName.Text = cuser.Rows[0][CUserTable.FLD_LAST_NAME].ToString() + ", " + cuser.Rows[0][CUserTable.FLD_FIRST_NAME].ToString();
                }
                //IsCUser = true;

            }
            //lnkChange.Visible = IsCUser;
        }
    }
}