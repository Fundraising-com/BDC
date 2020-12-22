using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.Promo_TextTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for CampaignSelector.
    /// </summary>
    public partial class Promo_TextSelector : BaseWebSubFormControl {
        private const string DEFAULT_SORT = dataDef.FLD_PROMO_TEXT_NAME;
        protected dataDef dTblList = new dataDef();
        protected DataView DVPromo_Text;
        //		private String IDRefCtrl = "";
        //		private String NameRefCtrl = "";
        //		private String AddRefCtrl = "";
        private CommonUtility clsUtil = new CommonUtility();

        protected void Page_Load(object sender, System.EventArgs e) {
            if ((this.ImageID == String.Empty) && (this.LinkImage == "true")) {
                string error = "Please select an image before choosing a promotion related to it.";
                this.Page.SetPageError(new Exception(error));
            }
        }

        //		override protected void OnLoad(System.EventArgs e)
        //		{
        //			base.OnLoad(e);
        //		}

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
            DVPromo_Text = new DataView(dTblList);
            this.DataSource = DVPromo_Text;
            this.MainDataGrid = dtgList;
            dtgList.DataKeyField = dataDef.FLD_PKID;
            base.LabelCurrentIndex = lblCurrentIndex;
            base.LabelTotal = lblTotal;
        }

        #endregion

        public string ParentID {
            get {
                if (Request["IDRefCtrl"] != null) {
                    return Request["IDRefCtrl"].ToString();
                }
                else {
                    return String.Empty;
                }
            }
        }

        public string ParentDesc {
            get {
                if (Request["NameRefCtrl"] != null) {
                    return Request["NameRefCtrl"].ToString();
                }
                else {
                    return String.Empty;
                }
            }
        }

        public string ParentText {
            get {
                if (Request["TxtRefCtrl"] != null) {
                    return Request["TxtRefCtrl"].ToString();
                }
                else {
                    return String.Empty;
                }
            }
        }

        public string ParentTempText {
            get {
                if (Request["hidTxtRefCtrl"] != null) {
                    return Request["hidTxtRefCtrl"].ToString();
                }
                else {
                    return String.Empty;
                }
            }
        }

        public string LinkImage {
            get {
                if (Request["lnkImg"] != null) {
                    return Request["lnkImg"].ToString();
                }
                else {
                    return String.Empty;
                }
            }
        }

        public string ImageID {
            get {
                if (Request["imgID"] != null) {
                    return Request["imgID"].ToString();
                }
                else {
                    return String.Empty;
                }
            }
        }

        protected override void LoadDataSourceGrid() {
            QSPForm.Business.Promo_TextSystem objSys = new QSPForm.Business.Promo_TextSystem();

            string sCriteria = dtgList.FilterExpression;
            switch (this.dtgList.SearchMode) {
                case 0: {
                        sCriteria = sCriteria + "%";
                        break;
                    }
                default: {
                        sCriteria = "%" + sCriteria + "%";
                        break;
                    }
            }

            if ((this.ImageID != String.Empty) && (this.LinkImage == "true")) {
                dTblList = objSys.SelectAll_Search(dtgList.SearchMode, sCriteria, string.Empty, -1, -1, string.Empty, false, Convert.ToInt32(this.ImageID));
            }
            else {
                dTblList = objSys.SelectAll_Search(dtgList.SearchMode, sCriteria, string.Empty, -1, -1, string.Empty, false, 0);
            }

            DVPromo_Text = new DataView(dTblList);
            DVPromo_Text.Sort = this.dtgList.SortExpression;
            base.DataSource = DVPromo_Text;

            lblTotal.Text = "Number of Text(s) : " + DVPromo_Text.Count.ToString();
        }

        protected override void OnItemDataBound(System.Web.UI.WebControls.DataGridItemEventArgs e) {
            string sID = "";
            string sName = "";
            string sText = "";

            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {

                if (e.Item.DataItem != null) {
                    sID = ((Label)e.Item.FindControl("lblID")).Text;
                    sName = ((Label)e.Item.FindControl("Description")).Text;
                    sText = ((Label)e.Item.FindControl("promo_text")).Text;
                    ((ImageButton)e.Item.FindControl("imgBtnSelect")).Attributes.Add("onclick", "CloseSelector(\"" + sID + "\",\"" + sName + "\",\"" + sText + "\");window.close();");
                }
            }
        }

        public DropDownList SearchCriteria {
            get {
                return QSPFormSearchModule.DropDownListSearchBy;
            }
        }
    }
}