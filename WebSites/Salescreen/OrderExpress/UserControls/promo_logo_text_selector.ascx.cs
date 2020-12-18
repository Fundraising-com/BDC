using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for promo_logo_text_selector.
    /// </summary>
    public partial class promo_logo_text_selector : BaseWebFormControl//System.Web.UI.UserControl
    {
        protected System.Web.UI.WebControls.Label Label3;
        protected System.Web.UI.WebControls.CheckBox chk;

        private CommonUtility clsUtil = new CommonUtility();

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here

            if (hidImg.Value != String.Empty) {
                this.imgPromo.ImageUrl = hidImg.Value;
            }
            if (hidPromoText.Value != String.Empty) {
                this.lblPromotion.Text = hidPromoText.Value;
            }
            AddJavascript();
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.imgBtnEdit.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnEdit_Click);

        }
        #endregion

        #region Event
        private void imgBtnEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            lblPromotion.Visible = false;
            txtPromotion.Text = lblPromotion.Text;
            txtPromotion.Visible = true;
        }

        #endregion Event

        #region Property
        public int PromoTextID {
            get {
                try { return Convert.ToInt32(txtPromoID.Text); }
                catch { return 0; }
            }
            set { txtPromoID.Text = value.ToString(); }
        }
        public int PromoImageID {
            get {
                try { return Convert.ToInt32(txtImgID.Text); }
                catch { return 0; }
            }
            set { txtImgID.Text = value.ToString(); }
        }
        public string PromoTextName {
            get { return txtPromoDesc.Text; }
            set { txtPromoDesc.Text = value; }
        }
        public string PromoImageName {
            get { return txtImgDesc.Text; }
            set { txtImgDesc.Text = value; }
        }
        #endregion Property

        #region Method
        public override void DataBind() {
            this.imgPromo.ImageUrl = QSPForm.Common.QSPFormConfiguration.Promo_LogoImagePreviewPath + PromoImageID + "." +
                QSPForm.Common.QSPFormConfiguration.ImagePreviewFileExtension;


            QSPForm.Business.Promo_TextSystem promoSys = new QSPForm.Business.Promo_TextSystem();
            DataTable promoTable = promoSys.SelectOne(this.PromoTextID);
            if (promoTable.Rows.Count >= 1) {
                this.txtPromotion.Text = promoTable.Rows[0][QSPForm.Common.DataDef.Promo_TextTable.FLD_DESCRIPTION].ToString();
                this.lblPromotion.Text = promoTable.Rows[0][QSPForm.Common.DataDef.Promo_TextTable.FLD_DESCRIPTION].ToString();
            }
        }

        private void AddJavascript() {
            txtImgDesc.Attributes.Add("onfocus", "javascript:window.focus();");
            txtImgID.Attributes.Add("onfocus", "javascript:window.focus();");
            txtPromoDesc.Attributes.Add("onfocus", "javascript:window.focus();");
            txtPromoID.Attributes.Add("onfocus", "javascript:window.focus();");

            //clsUtil.SetJScriptForOpenSelector(imgBtnSelectImage,txtImgID,txtImgDesc,QSPForm.Business.AppItem.Promo_LogoSelector,0,0,"&hidImgRefCtrl="+this.hidImg.ClientID+"&ImgRefCtrl="+this.imgPromo.ClientID);
            //clsUtil.SetJScriptForOpenSelector(imgBtnSelectText,txtPromoID,txtPromoDesc,QSPForm.Business.AppItem.Promo_TextSelector,0,0,"&hidTxtRefCtrl="+this.hidPromoText+"&TxtRefCtrl="+this.lblPromotion.ClientID);
            //imgBtnSelectText.Attributes.Add("onclick","OpenPromo_TextSelector();");

            clsUtil.SetJScriptForOpenSelector(imgBtnSelectImage, txtImgID, txtImgDesc, "Promo_LogoSelector.aspx", "PromoDetail", 0, 0, "&hidImgRefCtrl=" + this.hidImg.ClientID + "&ImgRefCtrl=" + this.imgPromo.ClientID);
            // clsUtil.SetJScriptForOpenSelector(imgBtnSelectText,txtPromoID,txtPromoDesc,"Promo_TextSelector.aspx",0,0,"&hidTxtRefCtrl="+this.hidPromoText+"&TxtRefCtrl="+this.lblPromotion.ClientID);

        }

        #endregion Method
    }
}