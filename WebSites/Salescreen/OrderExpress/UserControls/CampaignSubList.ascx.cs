using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.CampaignTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for CampaignList.
    /// </summary>
    public partial class CampaignSubList : BaseWebSubFormControl {
        private const string DEFAULT_SORT = CampaignTable.FLD_NAME + " ASC";
        protected dataDef dTblCampaigns = new dataDef();
        protected DataView DVCampaigns;
        private int c_AccountID;
        private CommonUtility clsUtil = new CommonUtility();

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
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
            DVCampaigns = new DataView(dTblCampaigns);
            this.DataSource = DVCampaigns;
            this.MainDataGrid = dtgCamp;
            dtgCamp.DataKeyField = CampaignTable.FLD_PKID;
            base.LabelTotal = lblTotal;

        }

        #endregion

        public int AccountID {
            get {
                return c_AccountID;
            }
            set {
                c_AccountID = value;
            }
        }

        protected override void LoadDataSourceGrid() {
            ///	Call the appropriate Class from the Biz layer

            QSPForm.Business.CampaignSystem campSys = new QSPForm.Business.CampaignSystem();

            //FM Hierarchy Filter
            string FMID = "";
            if (Page.Role == AuthSystem.ROLE_FM)
                FMID = Page.FMID;

            dTblCampaigns = campSys.SelectAllByAccountID(c_AccountID);

            DVCampaigns = new DataView(dTblCampaigns);
            DVCampaigns.Sort = this.dtgCamp.SortExpression;
            //Resynchronize the DataSource
            base.DataSource = DVCampaigns;

            lblTotal.Text = "Number of Campaign(s) : " + DVCampaigns.Count.ToString();
        }


        protected override void OnItemCreated(System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                String sID = "0";
                if (e.Item.DataItem != null) {
                    //sID = dtgCamp.DataKeys[e.Item.ItemIndex].ToString();
                    //clsUtil.SetJScriptForOpenDetail(e.Item, QSPForm.Business.AppItem.CampaignDetail, CampaignDetail.CAMP_ID, sID, 0,0, "OnDblClick");

                    //ImageButton imgBtnDetail = (ImageButton) e.Item.FindControl("imgBtnDetail");
                    //if (imgBtnDetail != null)
                    //{
                    //    clsUtil.SetJScriptForOpenDetail(imgBtnDetail, QSPForm.Business.AppItem.CampaignDetail, CampaignDetail.CAMP_ID, sID, 0,0);
                    //}
                    //LinkButton lnkBtnCampaign = (LinkButton) e.Item.FindControl("lnkBtnCampaign");
                    //if (lnkBtnCampaign != null)
                    //{
                    //    clsUtil.SetJScriptForOpenDetail(lnkBtnCampaign, QSPForm.Business.AppItem.CampaignDetail, CampaignDetail.CAMP_ID, sID, 0,0);
                    //}
                }
            }
        }
    }
}