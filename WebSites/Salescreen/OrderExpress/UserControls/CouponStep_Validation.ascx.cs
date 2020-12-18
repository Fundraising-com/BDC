using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using QSPForm.Common;
using QSP.OrderExpress.Web;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.PromoCouponTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    public partial class CouponStep_Validation : BasePromo_CouponDetail {
        private QSPForm.Business.Promo_TextSystem textSys;
        private QSPForm.Common.DataDef.Promo_TextTable dtblText;
        private const string ERR_01 = "An error occurs, please try again later";

        protected void Page_Load(object sender, EventArgs e) {
            //if(!IsPostBack)
            //{
            //    DataBind();
            //}
        }
        #region Property
        private int VendorID {
            //get{return Convert.ToInt32(GetQueryStringInfo("v"));}
            get { return Convert.ToInt32(this.DataSource.Tables[0].Rows[0][dataDef.FLD_VENDOR_ID].ToString()); }
        }
        private int FMID {
            //get{return Convert.ToInt32(GetQueryStringInfo("fm"));}
            get {
                int fm = 0;
                if (this.DataSource.Tables[0].Rows[0][dataDef.FLD_FM_ID].ToString() != string.Empty)
                    fm = Convert.ToInt32(this.DataSource.Tables[0].Rows[0][dataDef.FLD_FM_ID].ToString());
                return fm;
            }
        }
        private string FMName {
            //get { return GetQueryStringInfo("fmn"); }
            get { return this.DataSource.Tables[0].Rows[0][dataDef.FLD_FM_NAME].ToString(); }
        }
        private bool National {
            //get{return Convert.ToBoolean(GetQueryStringInfo("n"));}
            get { return Convert.ToBoolean(this.DataSource.Tables[0].Rows[0][dataDef.FLD_NATIONAL].ToString()); }
        }
        private int PromoTextID {
            get { return Convert.ToInt32(this.DataSource.Tables[0].Rows[0][dataDef.FLD_PROMO_TEXT_ID].ToString()); }
        }
        private string PromoTextName {
            //get{return GetQueryStringInfo("tn");}
            get { return this.DataSource.Tables[0].Rows[0][dataDef.FLD_PROMO_TEXT_NAME].ToString(); }
        }
        private int PromoImageID {
            //get{return Convert.ToInt32(GetQueryStringInfo("i"));}
            get { return Convert.ToInt32(this.DataSource.Tables[0].Rows[0][dataDef.FLD_PROMO_LOGO_ID].ToString()); }
        }
        private string PromoImageName {
            //get{return GetQueryStringInfo("in");}
            get { return this.DataSource.Tables[0].Rows[0][dataDef.FLD_PROMO_LOGO_NAME].ToString(); }
        }
        private string LabelingStartDate {
            //get{return GetQueryStringInfo("ls");}
            get { return this.DataSource.Tables[0].Rows[0][dataDef.FLD_LABELING_START_DATE].ToString(); }
        }
        private string LabelingEndDate {
            //get{return GetQueryStringInfo("le");}
            get { return this.DataSource.Tables[0].Rows[0][dataDef.FLD_LABELING_END_DATE].ToString(); }
        }
        private string ExpirationDate {
            get { return this.DataSource.Tables[0].Rows[0][dataDef.FLD_EXPIRATION_DATE].ToString(); }
        }
        private string Description {
            //get{return this.ViewState["PromoDesc"].ToString();}
            get { return this.DataSource.Tables[0].Rows[0][dataDef.FLD_DESCRIPTION].ToString(); }
        }
        #endregion

        private string GetQueryStringInfo(string var, bool isInt) {
            try {
                string x;
                if (Request.QueryString[var] != null) {
                    return HttpUtility.HtmlDecode(Request.QueryString[var].ToString());
                }
                else {
                    throw new Exception();
                }
            }
            catch {
                if (isInt)
                    return "0";
                else
                    return String.Empty;
            }
        }

        private string GetQueryStringInfo(string var) {
            return GetQueryStringInfo(var, false);
        }

        public override void DataBind() {
            try {
                LoadData();
                SetValue();
                SetGUI();

                ctrlVendorInfo.DataBind();
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
        }

        private void LoadData() {
            textSys = new QSPForm.Business.Promo_TextSystem();
            dtblText = textSys.SelectOne(this.PromoTextID);
        }

        private void SetValue() {
            ctrlVendorInfo.VendorID = this.VendorID;
            lblPromotion.Text = dtblText.Rows[0][QSPForm.Common.DataDef.Promo_TextTable.FLD_DESCRIPTION].ToString();
            imgPromo.ImageUrl = QSPFormConfiguration.Promo_LogoImagePreviewPath + this.PromoImageID + "." + QSPFormConfiguration.ImagePreviewFileExtension;
            //if (Session["PromoDesc"] != null)
            //{
            //    this.ViewState["PromoDesc"] = Session["PromoDesc"];
            //    Session.Remove("PromoDesc");
            //}
        }

        private void SetGUI() {
            lblImage.Text = this.PromoImageName;
            lblOffer.Text = this.PromoTextName;
            lblStartDate.Text = this.LabelingStartDate;
            lblEndDate.Text = this.LabelingEndDate;
            lblExpirationDate.Text = this.ExpirationDate;
            lblDescription.Text = this.Description;

            if (this.FMID == 0)
                trFMID.Visible = false;
            else
                lblFMID.Text = this.FMID + " - " + this.FMName;
        }

        protected void btnBack_Click(object sender, ImageClickEventArgs e) {
            CommonUtility c = new CommonUtility();
            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.CouponStep_2);
            string url = "~/CouponStep_Detail.aspx?";
            Response.Redirect(url + "&v=" + this.VendorID.ToString());
        }

        protected void btnConfirm_Click(object sender, ImageClickEventArgs e) {
            Save();
        }

        private void Save() {
            try {
                CommonUtility clsUtil = new CommonUtility();
                QSPForm.Business.PromoCouponSystem promoSys = new QSPForm.Business.PromoCouponSystem();
                QSPForm.Common.DataDef.PromoCouponTable dtblPromo = new QSPForm.Common.DataDef.PromoCouponTable();
                DataRow row = dtblPromo.NewRow();

                clsUtil.UpdateRow(row, PromoCouponTable.FLD_DESCRIPTION, this.Description);
                clsUtil.UpdateRow(row, PromoCouponTable.FLD_PROMO_LOGO_ID, this.PromoImageID.ToString());
                clsUtil.UpdateRow(row, PromoCouponTable.FLD_PROMO_TEXT_ID, this.PromoTextID.ToString());
                clsUtil.UpdateRow(row, PromoCouponTable.FLD_VENDOR_ID, this.VendorID.ToString());
                clsUtil.UpdateRow(row, PromoCouponTable.FLD_FM_ID, this.FMID.ToString());
                clsUtil.UpdateRow(row, PromoCouponTable.FLD_LABELING_START_DATE, this.LabelingStartDate);
                clsUtil.UpdateRow(row, PromoCouponTable.FLD_LABELING_END_DATE, this.LabelingEndDate);
                clsUtil.UpdateRow(row, PromoCouponTable.FLD_EXPIRATION_DATE, this.ExpirationDate);
                clsUtil.UpdateRow(row, PromoCouponTable.FLD_CREATE_USER_ID, this.Page.UserID.ToString());

                dtblPromo.Rows.Add(row);

                if (promoSys.Insert(dtblPromo)) {
                    //redirect confirmation
                    string url = clsUtil.GetPageUrl(QSPForm.Business.AppItem.CouponStep_4);
                    Response.Redirect(url);
                }
                else {
                    throw new Exception(ERR_01);
                }
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
        }
    } 
}