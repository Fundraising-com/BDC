using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.Promo_LogoTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for CampaignSelector.
    /// </summary>
    public partial class Promo_LogoSelector : BaseWebSubFormControl {
        private const string DEFAULT_SORT = dataDef.FLD_PROMO_LOGO_NAME;
        protected dataDef dTblList = new dataDef();
        protected DataView DVPromo_logo;
        //		private String IDRefCtrl = "";
        //		private String NameRefCtrl = "";
        //		private String AddRefCtrl = "";
        private CommonUtility clsUtil = new CommonUtility();

        override protected void OnLoad(System.EventArgs e) {
            base.OnLoad(e);
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
            DVPromo_logo = new DataView(dTblList);
            this.DataSource = DVPromo_logo;
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

        public string ParentImage {
            get {
                if (Request["ImgRefCtrl"] != null) {
                    return Request["ImgRefCtrl"].ToString();
                }
                else {
                    return String.Empty;
                }
            }
        }

        public string ParentTempImage {
            get {
                if (Request["hidImgRefCtrl"] != null) {
                    return Request["hidImgRefCtrl"].ToString();
                }
                else {
                    return String.Empty;
                }
            }
        }

        protected override void LoadDataSourceGrid() {
            QSPForm.Business.Promo_LogoSystem objSys = new QSPForm.Business.Promo_LogoSystem();

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
            dTblList = objSys.SelectAll_Search(dtgList.SearchMode, sCriteria, string.Empty, -1, -1, string.Empty, false, -1);

            DVPromo_logo = new DataView(dTblList);
            DVPromo_logo.Sort = this.dtgList.SortExpression;
            base.DataSource = DVPromo_logo;

            lblTotal.Text = "Number of Logo(s) : " + DVPromo_logo.Count.ToString();
        }

        protected override void OnItemDataBound(System.Web.UI.WebControls.DataGridItemEventArgs e) {
            string sID = "";
            string sName = "";
            string sUrl = "";

            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {

                if (e.Item.DataItem != null) {
                    ((ImageButton)e.Item.FindControl("imgBtnDetail")).ImageUrl = QSPForm.Common.QSPFormConfiguration.Promo_LogoImagePreviewPath +
                        ((DataRowView)e.Item.DataItem).Row[dataDef.FLD_PKID].ToString() + "." +
                        QSPForm.Common.QSPFormConfiguration.ImagePreviewFileExtension;


                    sID = ((Label)e.Item.FindControl("lblID")).Text;
                    sName = ((Label)e.Item.FindControl("Description")).Text;
                    sUrl = ((ImageButton)e.Item.FindControl("imgBtnDetail")).ImageUrl;
                    ((ImageButton)e.Item.FindControl("imgBtnSelect")).Attributes.Add("onclick", "CloseSelector(\"" + sID + "\",\"" + sName + "\",\"" + sUrl + "\");");
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