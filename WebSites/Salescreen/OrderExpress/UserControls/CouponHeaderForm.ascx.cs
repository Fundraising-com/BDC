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
using QSPForm.Common;
using dataRef = QSPForm.Common.DataDef.PromoCouponData;
using tableDataRef = QSPForm.Common.DataDef.PromoCouponTable;
using QSP.OrderExpress.Web;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    public partial class CouponHeaderForm : BasePromo_CouponDetail//System.Web.UI.UserControl
{
        private CommonUtility clsUtil = new CommonUtility();
        private QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
        private PromoCouponData dtsPromo_Coupon;
        private PromoCouponTable _promoTable;
        private PromoCouponSubdivisionTable _promoSubdivisionTable;

        protected void Page_Load(object sender, EventArgs e) {
            AddJavascript();
            AdjustUI();

            TrVendorEdit.Visible = false;
            TrVendorInfo.Visible = false;
            trReceived.Visible = false;

            if (IsNewPromo_Coupon) {
                Comparevalidator1.Enabled = false;
                Requiredfieldvalidator1.Enabled = false;
            }
        }

        #region auto-generated code
        ///<summary>Required method for Designer support</summary>
        override protected void OnInit(EventArgs e) {
            InitializeComponent();
            base.OnInit(e);
        }

        ///<summary>Required method for Designer support</summary>
        private void InitializeComponent() {
        }
        #endregion auto-generated code

        #region Property

        //public new PromoCouponData DataSource
        //{
        //    get{return dtsPromo_Coupon;}
        //    set
        //    {
        //        dtsPromo_Coupon = value;
        //        this._promoTable = dtsPromo_Coupon.PromoCoupon;
        //        this._promoSubdivisionTable = dtsPromo_Coupon.PromoCouponSubdivision;
        //    }
        //}

        public string Title {
            set { this.lblTitle.Text = value; }
        }

        private bool IsNewPromo_Coupon {
            get {
                bool new_promo = true;
                if (this.ViewState["IsNewPromo_Coupon"] != null)
                    new_promo = Convert.ToBoolean(this.ViewState["IsNewPromo_Coupon"].ToString());
                return new_promo;

                //try{ return Convert.ToBoolean(this.ViewState["IsNewPromo_Coupon"].ToString());}
                //catch{throw new Exception("Error in Promo_Coupon : Promo_Coupon is not DataBinded");}
            }
            set { this.ViewState["IsNewPromo_Coupon"] = value; }
        }

        /*
        private bool IsNewPromo_Coupon
        {
            get
            {
                return true;
            }
            set{}
        }
        */

        public int Promo_CouponID {
            get {
                try { return Convert.ToInt32(this.lblID.Text); }
                catch { return 0; }
            }
            set { this.lblID.Text = value.ToString(); }
        }

        public bool National {
            get { return this.chkNational.Checked; }
            set { this.chkNational.Checked = value; }
        }

        public string FMID {
            get {
                if (this.Page.Role >= QSPForm.Business.AuthSystem.ROLE_FIELD_SUPPORT)
                    return txtFMID.Text;
                else
                    return this.Page.FMID;
            }
            set { txtFMID.Text = value; }
        }

        public string FMName {
            get { return txtFMName.Text; }
            set { txtFMName.Text = value; }
        }

        //public string VendorID
        //{
        //    get{ return txtVendorID.Text; }
        //    set{ txtVendorID.Text = value; }
        //}

        public int VendorID {
            get {
                if (this.ViewState["VendorID"] != null)
                    return Convert.ToInt32(this.ViewState["VendorID"].ToString());
                else
                    return 0;
            }
            set {
                this.ViewState["VendorID"] = value;
            }
        }

        public string VendorName {
            get { return txtVendorName.Text; }
            set { txtVendorName.Text = value; }
        }

        public bool Received {
            get { return this.chkReceived.Checked; }
            set { this.chkReceived.Checked = value; }
        }

        //private string HiddenCurrentSubdivision
        //{
        //    get{return this.hidCurrentSubdivision.Value;}
        //    set{this.hidCurrentSubdivision.Value = value;}
        //}

        public ArrayList SelectedSubdivision {
            get {
                try {
                    ArrayList al = new ArrayList();
                    //					string [] subdivision = this.ctrlSubdivisionSelector.GetSelectedSubdivision();//this.HiddenCurrentSubdivision.Split(',');
                    //					for(int i=0;i<subdivision.Length;i++)
                    //					{
                    //						if(subdivision[i].ToString().Trim() != String.Empty)
                    //						{
                    //							al.Add(subdivision[i].ToString());
                    //						}
                    //					}
                    return al;
                }
                catch {
                    return null;
                }
            }
        }

        public int PromoTextID {
            get { return ctrlPromo_logo_text_selector.PromoTextID; }
            set { ctrlPromo_logo_text_selector.PromoTextID = value; }
        }

        public string PromoTextName {
            get { return ctrlPromo_logo_text_selector.PromoTextName; }
            set { ctrlPromo_logo_text_selector.PromoTextName = value; }
        }

        public int PromoImageID {
            get { return ctrlPromo_logo_text_selector.PromoImageID; }
            set { ctrlPromo_logo_text_selector.PromoImageID = value; }
        }

        public string PromoImageName {
            get { return ctrlPromo_logo_text_selector.PromoImageName; }
            set { ctrlPromo_logo_text_selector.PromoImageName = value; }
        }

        public string LabelingStartDate {
            get { return txtLabelingSD.Text; }
            set { txtLabelingSD.Text = value; }
        }

        public string LabelingEndDate {
            get { return txtLabelingED.Text; }
            set { txtLabelingED.Text = value; }
        }

        public string ExpirationDate {
            get { return txtExpirationDate.Text; }
            set { txtExpirationDate.Text = value; }
        }

        public string Description {
            get { return txtDescription.Text; }
            set { txtDescription.Text = value; }
        }

        override public DataSet UpdatedDataSource {
            get {
                if (UpdateDataSource()) {
                    return DataSource;
                }
                else {
                    throw new Exception("Invalid process");
                }
            }
        }

        private DataRow Coupon {
            get { return this.DataSource.Tables[0].Rows[0]; }
        }
        #endregion Property

        public override void DataBind() {
            try {
                LoadData();
                if (this.DataSource.Tables[0].Rows[0][PromoCouponTable.FLD_PKID].ToString() != "0")
                //if (this.DataSource[PromoCouponTable.FLD_PKID].ToString() != "0")
				{
                    this.IsNewPromo_Coupon = false;
                }
                else {
                    this.IsNewPromo_Coupon = true;
                }
                SetValue();
            }
            catch (Exception ex) {
                this.Page.SetPageMessage("Invalid Promo_Coupon Data: " + ex.Message);
            }
        }

        protected override void LoadData() {
            /*			
            int pkID = Convert.ToInt32(this.DataSource.Promo_Coupon.Rows[0][tableDataRef.FLD_PKID].ToString());
            QSPForm.Business.Promo_CouponSubdivisionSystem regSys = new QSPForm.Business.Promo_CouponSubdivisionSystem();
		
            DataTable currentSubdivision = regSys.SelectAllByPromo_CouponID(pkID);
            ArrayList IdCollection = new ArrayList();

            QSPForm.Business.SubdivisionCollection subCol = new QSPForm.Business.SubdivisionCollection();
            QSPForm.Business.Subdivision sub;// = new QSPForm.Business.Subdivision();
            foreach(DataRow row in currentSubdivision.Rows)
            {
                sub = new QSPForm.Business.Subdivision();
                sub.SubdivisionCode = row[QSPForm.Common.DataDef.Promo_CouponSubdivisionTable.FLD_SUBDIVISION_CODE].ToString();
                sub.SubdivisionName1 = row[QSPForm.Common.DataDef.Promo_CouponSubdivisionTable.FLD_SUBDIVISION_NAME_1].ToString();
                subCol.Add(sub);
            }

            this.ctrlSubdivisionSelector.SelectedSubdivision = subCol;
            this.ctrlSubdivisionSelector.DataBind();
            */
        }

        private void SetValue() {
            this.Promo_CouponID = Convert.ToInt32(this.Coupon[tableDataRef.FLD_PKID].ToString());
            if (this.Coupon[tableDataRef.FLD_NATIONAL].ToString() != String.Empty)
                this.National = Convert.ToBoolean(this.Coupon[tableDataRef.FLD_NATIONAL].ToString());
            this.VendorID = Convert.ToInt32(this.Coupon[tableDataRef.FLD_VENDOR_ID].ToString());
            this.VendorName = this.Coupon[tableDataRef.FLD_VENDOR_NAME].ToString();
            if (this.Coupon[tableDataRef.FLD_FM_ID].ToString() != String.Empty)
                this.FMID = this.Coupon[tableDataRef.FLD_FM_ID].ToString();
            if (this.Coupon[tableDataRef.FLD_FM_NAME].ToString() != String.Empty)
                this.FMName = this.Coupon[tableDataRef.FLD_FM_NAME].ToString();
            this.LabelingStartDate = this.Coupon[tableDataRef.FLD_LABELING_START_DATE].ToString();
            this.LabelingEndDate = this.Coupon[tableDataRef.FLD_LABELING_END_DATE].ToString();
            this.ExpirationDate = this.Coupon[tableDataRef.FLD_EXPIRATION_DATE].ToString();
            this.Description = this.Coupon[tableDataRef.FLD_DESCRIPTION].ToString();

            //set ctrl value
            if (this.Coupon[tableDataRef.FLD_PROMO_LOGO_ID].ToString() != String.Empty)
                this.PromoImageID = Convert.ToInt32(this.Coupon[tableDataRef.FLD_PROMO_LOGO_ID].ToString());
            this.PromoImageName = this.Coupon[tableDataRef.FLD_PROMO_LOGO_NAME].ToString();
            if (this.Coupon[tableDataRef.FLD_PROMO_TEXT_ID].ToString() != String.Empty)
                this.PromoTextID = Convert.ToInt32(this.Coupon[tableDataRef.FLD_PROMO_TEXT_ID].ToString());
            this.PromoTextName = this.Coupon[tableDataRef.FLD_PROMO_TEXT_NAME].ToString();
            this.ctrlPromo_logo_text_selector.DataBind();
        }

        private string FormatToDate(string s) {
            string[] d = s.Split(' ');
            return d[0];
        }

        public bool UpdateDataSource() {

            bool IsSuccess = false;
            DataRow r = this.DataSource.Tables[0].Rows[0];
            comSys.UpdateRow(r, PromoCouponTable.FLD_FM_ID, this.FMID);
            comSys.UpdateRow(r, PromoCouponTable.FLD_FM_NAME, this.FMName);
            comSys.UpdateRow(r, PromoCouponTable.FLD_DESCRIPTION, this.Description);
            comSys.UpdateRow(r, PromoCouponTable.FLD_PROMO_LOGO_ID, this.ctrlPromo_logo_text_selector.PromoImageID.ToString());
            comSys.UpdateRow(r, PromoCouponTable.FLD_PROMO_LOGO_NAME, this.ctrlPromo_logo_text_selector.PromoImageName.ToString());
            comSys.UpdateRow(r, PromoCouponTable.FLD_PROMO_TEXT_ID, this.ctrlPromo_logo_text_selector.PromoTextID.ToString());
            comSys.UpdateRow(r, PromoCouponTable.FLD_PROMO_TEXT_NAME, this.ctrlPromo_logo_text_selector.PromoTextName.ToString());
            comSys.UpdateRow(r, PromoCouponTable.FLD_LABELING_START_DATE, this.LabelingStartDate);
            comSys.UpdateRow(r, PromoCouponTable.FLD_LABELING_END_DATE, this.LabelingEndDate);
            comSys.UpdateRow(r, PromoCouponTable.FLD_EXPIRATION_DATE, this.ExpirationDate);

            #region junk
            /*
			DataRow row = this.dtsPromo_Coupon.Promo_Coupon.Rows[0];
			//QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();

			comSys.UpdateRow(row,Promo_CouponTable.FLD_Promo_Coupon_NAME,this.Name);
			comSys.UpdateRow(row,Promo_CouponTable.FLD_Promo_Coupon_CODE,this.Code);
			comSys.UpdateRow(row,Promo_CouponTable.FLD_DESCRIPTION,this.Description);
			comSys.UpdateRow(row,Promo_CouponTable.FLD_ENABLED,this.Enabled.ToString());
			comSys.UpdateRow(row,Promo_CouponTable.FLD_VENDOR_ID,this.VendorID);

			if (row.RowState == DataRowState.Added)
			{	
				row[Promo_CouponTable.FLD_CREATE_USER_ID] = Page.UserID;
			}
			else
			{
				row[Promo_CouponTable.FLD_UPDATE_USER_ID] = Page.UserID;
			}	

			if(this.National)
			{
				//remove fsm
				comSys.UpdateRow(row,Promo_CouponTable.FLD_FSM_ID,"");

				//remove all subdivision
				foreach(DataRow dtRow in dtsPromo_Coupon.Promo_CouponSubdivision.Rows)
				{
					dtRow[Promo_CouponSubdivisionTable.FLD_UPDATE_USER_ID] = Page.UserID;
					dtRow.Delete();
				}
			}
			else
			{
				
				comSys.UpdateRow(row,Promo_CouponTable.FLD_FSM_ID,"");
			
				//find removed subdivision
				foreach(DataRow dtRow in dtsPromo_Coupon.Promo_CouponSubdivision.Rows)
				{
					if(! this.SelectedSubdivision.Contains(dtRow[Promo_CouponSubdivisionTable.FLD_PKID].ToString()))
					{
						dtRow[Promo_CouponSubdivisionTable.FLD_UPDATE_USER_ID] = Page.UserID;
						dtRow.Delete();
					}
				}

				if(this.SelectedSubdivision.Count > 0)
				{
					//find new subdivision
					for(int i=0;i<this.SelectedSubdivision.Count;i++)
					{
						DataView dv = new DataView(dtsPromo_Coupon.Promo_CouponSubdivision);
						dv.Sort = Promo_CouponSubdivisionTable.FLD_SUBDIVISION_CODE;

						int iIndex = dv.Find(this.SelectedSubdivision[i].ToString().Trim());
						//Be sure to add only new subdivision
						if(iIndex == -1)
						{
							DataRow r = dtsPromo_Coupon.Promo_CouponSubdivision.NewRow();
							r[Promo_CouponSubdivisionTable.FLD_CREATE_USER_ID] = Page.UserID;
							r[Promo_CouponSubdivisionTable.FLD_Promo_Coupon_ID] = dtsPromo_Coupon.Promo_Coupon.Rows[0][Promo_CouponTable.FLD_PKID].ToString();
							r[Promo_CouponSubdivisionTable.FLD_SUBDIVISION_CODE] = this.SelectedSubdivision[i].ToString().Trim();
							dtsPromo_Coupon.Promo_CouponSubdivision.Rows.Add(r);
						}
					}
				}
				else
				{
					comSys.UpdateRow(row,Promo_CouponTable.FLD_FSM_ID,this.FMID);
				}
			}			
			*/
            #endregion junk
            IsSuccess = true;

            return IsSuccess;
        }

        private void AdjustUI() {
            /*
                * Use the style instead of visible because 
                * controls that are not visible are not rendered
            */

            // Adjust FM Selector if User is Admin or IT
            //show FM Name and ID if needed
            if (this.Page.Role >= QSPForm.Business.AuthSystem.ROLE_FIELD_SUPPORT) {
                trFmInfo.Style["display"] = "none";
                trFmEdit.Style["display"] = "block";
                TrVendorInfo.Style["display"] = "none";
                TrVendorEdit.Style["display"] = "block";

                lblFMInfo.Text = this.Page.FMID + "-" + this.Page.User.Identity.Name;
            }

            // Adjust for National
            if (this.National) {
                trFmInfo.Style["display"] = "none";
                trFmEdit.Style["display"] = "none";
            }

            if (this.Promo_CouponID == 0) {
                this.lblID.Text = "New Contract";
            }
        }

        private void AddJavascript() {
            txtFMName.Attributes.Add("onfocus", "javascript:window.focus();");
            txtFMID.Attributes.Add("onfocus", "javascript:window.focus();");

            clsUtil.SetJScriptForOpenCalendar(hypLnkStartDate, txtLabelingSD);
            clsUtil.SetJScriptForOpenCalendar(hypLnkEndDate, txtLabelingED);
            clsUtil.SetJScriptForOpenCalendar(hypLnkExpirationDate, txtExpirationDate);
            //clsUtil.SetJScriptForOpenSelector(imgBtnSelectFM,txtFMID,txtFMName,QSPForm.Business.AppItem.FMSelector,0,0);
            //clsUtil.SetJScriptForOpenSelector(imgBtnSelectVendor,txtVendorID,txtVendorName,QSPForm.Business.AppItem.VendorSelector,0,0);

            clsUtil.SetJScriptForOpenSelector(imgBtnSelectFM, txtFMID, txtFMName, "FMSelector.aspx", "FMDetail", 0, 0, null);
            clsUtil.SetJScriptForOpenSelector(imgBtnSelectVendor, txtVendorID, txtVendorName, "VendorSelector.aspx", "VendorDetail", 0, 0, null);

            this.chkNational.Attributes["onClick"] = "ShowHideSubdivision();";

            txtLabelingSD.Attributes.Add("onpropertychange", "javascript:QuickFill();");
        }
    } 
}