using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using QSPForm.Common.DataDef;
using dataRef = QSPForm.Common.DataDef.PromoCouponData;
using tableDataRef = QSPForm.Common.DataDef.PromoCouponTable;
using QSP.OrderExpress.Web;
using System.Web.UI;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>User Information - read only</summary>
    public partial class CouponStep_Detail : BasePromo_CouponDetail {
        private QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                LoadStep();
            }
        }

        protected override void OnInit(EventArgs e) {
            //LoadStep();
            base.OnInit(e);
        }

        protected void btnBack_Click(object sender, ImageClickEventArgs e) {
            PreviousStep();
        }

        protected void btnNext_Click(object sender, ImageClickEventArgs e) {
            NextStep();
        }

        public int VendorID {
            get { return Convert.ToInt32(Request.QueryString["v"]); }
        }

        public int Step {
            get {
                int step = 0;
                if (ViewState["Step"] != null) {
                    step = Convert.ToInt32(this.ViewState["Step"].ToString());
                }
                else {
                    ViewState["Step"] = 2;
                    return 2;
                }
                return step;
            }
            set {
                this.ViewState["Step"] = value;
            }
        }

        public DataSet DataSource {
            get {
                try {
                    if (this.ViewState["DataSource"] == null) {
                        QSPForm.Common.DataDef.PromoCouponTable tbl = new QSPForm.Common.DataDef.PromoCouponTable();
                        DataRow r = tbl.NewRow();
                        r[QSPForm.Common.DataDef.PromoCouponTable.FLD_VENDOR_ID] = this.VendorID;
                        tbl.Rows.Add(r);
                        DataSet dts = new DataSet();
                        dts.Tables.Add(tbl);
                        this.ViewState["DataSource"] = dts;
                        return dts;
                    }
                    else {
                        return (DataSet)this.ViewState["DataSource"];
                    }

                }
                catch { return null; }
            }
            set {
                this.ViewState["DataSource"] = value;
            }
        }

        private void PreviousStep() {
            try {
                Step = Step - 1;
                LoadStep();
                //this.phContainer.Controls[0].DataBind();
            }
            catch (Exception e) {
                Step = Step + 1;
                this.Page.SetPageError(e);
            }
        }

        private void NextStep() {
            try {
                Step = Step + 1;
                if (Step == 3)
                    UpdateDataSource();
                LoadStep();
                //this.phContainer.Controls[0].DataBind();
            }
            catch (Exception e) {
                Step = Step - 1;
                this.Page.SetPageError(e);
            }
        }

        private void LoadStep() {
            if (this.Step == 1) {
                //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.CouponStep_1);
                string url = "~/CouponStep_Selection.aspx";
                Response.Redirect(url, false);
            }
            else if ((this.Step == 2) || (this.Step == 0)) {
                LoadHeaderForm();
            }
            else if (this.Step == 3) {
                LoadDetailForm();
            }
            else if (this.Step >= 4) {
                if (Save()) {
                    //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.CouponStep_4);
                    string url = "~/CouponStep_Confirmation.aspx";
                    Response.Redirect(url, false);
                }
            }
            else {
                throw new Exception("enable to load the right control");
            }
        }

        private void LoadHeaderForm() {
            try {
                ctrl_couponHeaderForm.DataSource = this.DataSource;
                ctrl_couponHeaderForm.DataBind();
                tr_step2.Visible = true;
                tr_step3.Visible = false;
                btnBack.AlternateText = "Vendor Selection";
                btnNext.ImageUrl = "images/btnNext.gif";
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
        }

        private void LoadDetailForm() {
            try {
                ctrlCouponStep_Validation.DataSource = this.DataSource;
                ctrlCouponStep_Validation.DataBind();
                tr_step2.Visible = false;
                tr_step3.Visible = true;
                btnNext.ImageUrl = "images/btnConfirm.gif";
                btnBack.AlternateText = "Coupon Information";
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
        }

        public void UpdateDataSource() {
            //DataSet dts = this.DataSource;
            //QSP.OrderExpress.Web.BasePromo_CouponDetail ctrl = (BasePromo_CouponDetail)this.phContainer.Controls[0];
            //DataRow update = ctrl.UpdatedDataSource.Tables[0].Rows[0];
            //DataRow update = ctrl_couponHeaderForm.UpdatedDataSource.Tables[0].Rows[0];
            DataRow r = DataSource.Tables[0].Rows[0];//dts.Tables[0].Rows[0];

            comSys.UpdateRow(r, PromoCouponTable.FLD_FM_ID, ctrl_couponHeaderForm.FMID);
            comSys.UpdateRow(r, PromoCouponTable.FLD_FM_NAME, ctrl_couponHeaderForm.FMName);
            comSys.UpdateRow(r, PromoCouponTable.FLD_DESCRIPTION, ctrl_couponHeaderForm.Description);
            comSys.UpdateRow(r, PromoCouponTable.FLD_PROMO_LOGO_ID, ctrl_couponHeaderForm.PromoImageID.ToString());
            comSys.UpdateRow(r, PromoCouponTable.FLD_PROMO_LOGO_NAME, ctrl_couponHeaderForm.PromoImageName);
            comSys.UpdateRow(r, PromoCouponTable.FLD_PROMO_TEXT_ID, ctrl_couponHeaderForm.PromoTextID.ToString());
            comSys.UpdateRow(r, PromoCouponTable.FLD_PROMO_TEXT_NAME, ctrl_couponHeaderForm.PromoTextName);
            comSys.UpdateRow(r, PromoCouponTable.FLD_LABELING_START_DATE, ctrl_couponHeaderForm.LabelingStartDate);
            comSys.UpdateRow(r, PromoCouponTable.FLD_LABELING_END_DATE, ctrl_couponHeaderForm.LabelingEndDate);
            comSys.UpdateRow(r, PromoCouponTable.FLD_EXPIRATION_DATE, ctrl_couponHeaderForm.ExpirationDate);
            comSys.UpdateRow(r, PromoCouponTable.FLD_VENDOR_ID, ctrl_couponHeaderForm.VendorID.ToString());
            comSys.UpdateRow(r, PromoCouponTable.FLD_VENDOR_NAME, ctrl_couponHeaderForm.VendorName);

            //DataSource = dts;

            //comSys.UpdateRow(r,QSPForm.Common.DataDef.PromoCouponTable.FLD_EXPIRATION_DATE,);
        }

        private bool Save() {
            try {
                QSPForm.Business.PromoCouponSystem promoSys = new QSPForm.Business.PromoCouponSystem();
                this.DataSource.Tables[0].Rows[0][PromoCouponTable.FLD_CREATE_USER_ID] = this.Page.UserID;
                int x = promoSys.Insert(this.DataSource.Tables[0]);
                //if (x > 1)
                //    return true;
                //else
                //    return false;
                return true;
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
                return false;
            }
        }
    } 
}