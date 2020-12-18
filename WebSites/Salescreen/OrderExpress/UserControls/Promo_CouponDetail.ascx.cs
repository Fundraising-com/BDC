//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\Stub_Promo_CouponDetail_ascx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'Promo_CouponDetail.ascx' was also modified to refer to the new class name.
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
using dataDef = QSPForm.Common.DataDef.PromoCouponData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    /// Summary description for AccountDetail.
    /// </summary>
    public partial class Promo_CouponDetail : BasePromo_CouponDetail {
        protected System.Web.UI.WebControls.ValidationSummary ValSum;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidChange;
        protected System.Web.UI.WebControls.Label lblInstruction;
        protected System.Web.UI.WebControls.Image imgTitle;

        private const string Promo_Coupon_DATA = "Promo_Coupon_data";
        //private int _LogoID = 0;
        //protected dataDef dtsPromo_Coupon;
        //private QSPForm.Business.PromoCouponSystem promoSys = new QSPForm.Business.PromoCouponSystem();
        //private QSPForm.Common.DataDef.PromoCouponTable dtblCoupon;

        protected void Page_Load(object sender, System.EventArgs e) {
            try {
                // Put user code to initialize the page here	
                //LoadData();	
                if (!IsPostBack) {
                    //this.Page.ValSummary.Visible = false;
                    BindForm();
                    //DataBind();
                }
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.imgBtnDelete.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnDelete_Click);
            this.imgBtnSave.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnSave_Click);
            //this.btnNext.Click += new ImageClickEventHandler(btnNext_Click);
            //this.btnBack.Click += new ImageClickEventHandler(btnBack_Click);
        }

        #endregion

        //		public int Promo_CouponID
        //		{
        public int Promo_CouponID {
            get {
                try {
                    if (this.ViewState[Promo_Coupon_ID] == null) {
                        this.ViewState[Promo_Coupon_ID] = Convert.ToInt32(this.Request[Promo_Coupon_ID].ToString());
                    }
                    return Convert.ToInt32(this.ViewState[Promo_Coupon_ID].ToString());
                }
                catch {
                    return 0;
                }
            }
            set {
                ViewState[Promo_Coupon_ID] = value;
            }
        }

        private int VendorID {
            get {
                if (this.Request.QueryString["v"] != null)
                    return Convert.ToInt32(this.Request.QueryString["v"].ToString());
                else
                    return 0;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            //this.ViewState[Promo_Coupon_DATA] = dtsPromo_Coupon;
        }

        private void imgBtnDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
        }

        #region junk
        //void btnBack_Click(object sender, ImageClickEventArgs e)
        //{
        //    CommonUtility c = new CommonUtility();
        //    string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.CouponStep_1);
        //    Response.Redirect(url);
        //}
        //void btnNext_Click(object sender, ImageClickEventArgs e)
        //{
        //    CommonUtility c = new CommonUtility();
        //    string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.CouponStep_3);
        //    url += "&v="  + HttpUtility.HtmlEncode(this.VendorID.ToString());
        //    url += "&fm=" + HttpUtility.HtmlEncode(this.ctrlPromo_CouponHeaderForm.FMID);
        //    url += "&fmn="+ HttpUtility.HtmlEncode(this.ctrlPromo_CouponHeaderForm.FMName);
        //    url += "&n="  + HttpUtility.HtmlEncode(this.ctrlPromo_CouponHeaderForm.National.ToString());
        //    url += "&t="  + HttpUtility.HtmlEncode(this.ctrlPromo_CouponHeaderForm.PromoTextID.ToString());
        //    url += "&tn=" + HttpUtility.HtmlEncode(this.ctrlPromo_CouponHeaderForm.PromoTextName);
        //    url += "&i="  + HttpUtility.HtmlEncode(this.ctrlPromo_CouponHeaderForm.PromoImageID.ToString());
        //    url += "&in=" + HttpUtility.HtmlEncode(this.ctrlPromo_CouponHeaderForm.PromoImageName);
        //    url += "&ls=" + HttpUtility.HtmlEncode(this.ctrlPromo_CouponHeaderForm.LabelingStartDate);
        //    url += "&le=" + HttpUtility.HtmlEncode(this.ctrlPromo_CouponHeaderForm.LabelingStartDate);
        //    url += "&e="  + HttpUtility.HtmlEncode(this.ctrlPromo_CouponHeaderForm.ExpirationDate);

        //    //temp ** set session variable with description value;
        //    Session["PromoDesc"] = this.ctrlPromo_CouponHeaderForm.Description;

        //    //redirect
        //    Response.Redirect(url);
        //}
        #endregion

        protected void SetFormParameter() {
            /*
            if (Request[LOGO_ID] != null)
            {
                LogoID = Convert.ToInt32(Request[LOGO_ID].ToString());
            }
            else
            {
                LogoID = 0;
            }
            ViewState[LOGO_ID] = LogoID;	
            */
        }

        public override void BindForm() {
            //if (this.VendorID != 0)
            //{
            //    ctrlPromo_CouponHeaderForm.VendorID = this.VendorID;
            //    trEditButton.Visible = false;
            //}
            //else
            //{
            //    trEditButton.Visible = true;
            //    trNavigationButton.Visible = false;
            //}
            ctrlPromo_CouponHeaderForm.Promo_CouponID = this.Promo_CouponID;
            ctrlPromo_CouponHeaderForm.DataBind();
        }

        private void LoadData() {
            //dtblCoupon = promoSys.SelectOne(this.Promo_CouponID);

            #region trash
            //if (!IsPostBack)
            //{
            //    SetFormParameter();
            //    QSPForm.Business.PromoCouponSystem prdSys = new QSPForm.Business.PromoCouponSystem();
            //    //ToDo insert row when c_User_ID=0
            //    if(this.Promo_CouponID == 0)
            //    {
            //        dtsPromo_Coupon = prdSys.InitializePromoCoupon(this.Page.UserID);
            //    }
            //    else
            //    {
            //        dtsPromo_Coupon = prdSys.SelectAllDetail(this.Promo_CouponID);
            //    }

            //    //this.ViewState[LOGO_ID] = LogoID;
            //    this.ViewState[Promo_Coupon_DATA] = dtsPromo_Coupon;
            //}
            //else
            //{
            //    //LogoID = Convert.ToInt32(this.ViewState[LOGO_ID]);
            //    dtsPromo_Coupon = (dataDef)this.ViewState[Promo_Coupon_DATA];
            //}

            //ctrlPromo_CouponHeaderForm.Promo_CouponID = Promo_CouponID;
            //ctrlPromo_CouponHeaderForm.DataSource = dtsPromo_Coupon;//.PromoCoupon.Rows[0];
            #endregion
        }

        private void SetValue() {
        }

        private void imgBtnSave_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //try
            //{
            //    Boolean blnValid = true;
            //    blnValid = ctrlPromo_CouponHeaderForm.IsValid();
            //    if (!blnValid)
            //    {
            //        return;
            //    }

            //    blnValid = ctrlPromo_CouponHeaderForm.UpdateDataSource();
            //    if (!blnValid)
            //    {						
            //        return;
            //    }

            //    QSPForm.Business.PromoCouponSystem prmSys = new QSPForm.Business.PromoCouponSystem();
            //    blnValid = prmSys.UpdateAllDetail(dtsPromo_Coupon,this.Page.UserID);

            //    if( (blnValid) && (Promo_CouponID == 0))
            //    {
            //        //ctrlPromo_CouponHeaderForm.CreateImageAndPreview();
            //        Promo_CouponID = Convert.ToInt32(this.dtsPromo_Coupon.PromoCoupon.Rows[0][PromoCouponTable.FLD_PKID].ToString());
            //    }

            //    if(blnValid)
            //    {	
            //        string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.Promo_CouponDetailInfo, BasePromo_CouponDetail.Promo_Coupon_ID, Promo_CouponID.ToString());
            //        Response.Redirect(url,false);
            //    }
            //}
            //catch(Exception ex)
            //{
            //    this.Page.SetPageMessage("Error while saving Promo_Coupon : " + ex.Message);
            //}
        }
    }
}