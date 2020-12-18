using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.ProgramAgreementStatusChangeTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for AccountSubList.
    /// </summary>
    public partial class ProgramAgreementStatusChangeList : BaseWebSubFormControl {
        private const string DEFAULT_SORT = dataDef.FLD_PKID + " DESC";
        protected dataDef dTblProgramAgreementStatusChanges = new dataDef();
        protected DataView DVProgramAgreementStatusChanges;
        private CommonUtility clsUtil = new CommonUtility();
        private int c_ProgramAgreementID;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            dtgProgramAgreement.Columns[1].Visible = (this.Page.Role == AuthSystem.ROLE_FM);
            dtgProgramAgreement.Columns[2].Visible = (this.Page.Role > AuthSystem.ROLE_FM);
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            InitControl();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {

        }

        private void InitControl() {
            this.DefaultSort = DEFAULT_SORT;
            DVProgramAgreementStatusChanges = new DataView(dTblProgramAgreementStatusChanges);
            this.DataSource = DVProgramAgreementStatusChanges;
            this.MainDataGrid = dtgProgramAgreement;
            dtgProgramAgreement.DataKeyField = ProgramAgreementTable.FLD_PKID;
            base.LabelTotal = lblTotal;

        }

        #endregion

        public int ProgramAgreementID {
            get {
                return c_ProgramAgreementID;
            }
            set {
                c_ProgramAgreementID = value;
            }
        }

        private void GetParamQueryStringFilter() {
            if (Request["ProgramAgreementID"] != null) {
                c_ProgramAgreementID = Convert.ToInt32(Request["ProgramAgreementID"]);
            }
        }

        protected override void LoadDataSourceGrid() {
            ///	Call the appropriate Class from the Biz layer

            QSPForm.Business.ProgramAgreementSystem ordSys = new QSPForm.Business.ProgramAgreementSystem();

            //Set the Account ID Parameter
            GetParamQueryStringFilter();

            dTblProgramAgreementStatusChanges = ordSys.SelectAllProgramAgreementStatusChangeByProgramAgreementID(c_ProgramAgreementID);

            DVProgramAgreementStatusChanges = new DataView(dTblProgramAgreementStatusChanges);
            DVProgramAgreementStatusChanges.Sort = this.dtgProgramAgreement.SortExpression;
            //Resynchronize the DataSource
            base.DataSource = DVProgramAgreementStatusChanges;

            lblTotal.Text = "Number of Change(s) : " + DVProgramAgreementStatusChanges.Count.ToString();
        }

        protected override void OnItemCreated(System.Web.UI.WebControls.DataGridItemEventArgs e) {

            //            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            //            {
            //                String sID = "0";
            //                if (e.Item.DataItem != null)
            //                {
            //                    //sID = ((DataRowView)e.Item.DataItem).Row[dataDef.FLD_PKID].ToString();
            //                    sID = dtgAccount.DataKeys[e.Item.ItemIndex].ToString();
            //                    string sIDName = BaseAccountDetail.ORDER_ID;
            //                    clsUtil.SetJScriptForOpenDetail(e.Item, QSPForm.Business.AppItem.AccountDetailInfo, sIDName, sID, 0,0);

            ////					ImageButton imgBtnDetail = (ImageButton) e.Item.FindControl("imgBtnDetail");
            ////					if (imgBtnDetail != null)
            ////					{
            ////						clsUtil.SetJScriptForOpenDetail(imgBtnDetail, QSPForm.Business.AppItem.AccountDetail, sIDName, sID, 0,0);
            ////					}					
            //                }		
            //            }			
        }
    }
}