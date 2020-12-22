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
using dataRef = QSPForm.Common.DataDef.Promo_TextData;
using tableDataRef = QSPForm.Common.DataDef.Promo_TextTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>User Information - read only</summary>
    public partial class Promo_TextHeaderForm : BaseWebFormControl {
        #region Item Declarations

        protected System.Web.UI.WebControls.CompareValidator compVal_StartDate;
        protected System.Web.UI.WebControls.CompareValidator compVal_EndDate;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCurrentSubdivision;

        protected System.Web.UI.HtmlControls.HtmlTableRow trStartDate;
        protected System.Web.UI.HtmlControls.HtmlTableRow trEndDate;
        protected System.Web.UI.HtmlControls.HtmlTableRow trRegion;

        protected System.Web.UI.WebControls.HyperLink hypLnkStartDate;
        protected System.Web.UI.WebControls.HyperLink hypLnkEndDate;

        protected System.Web.UI.WebControls.ListBox lbxSubdivision;
        protected System.Web.UI.WebControls.ListBox lbxCurrentSubdivision;

        protected System.Web.UI.WebControls.RequiredFieldValidator reqFldVal_StartDate;
        protected System.Web.UI.WebControls.RequiredFieldValidator reqFldVal_EndDate;
        protected System.Web.UI.WebControls.TextBox txtStartDate;
        protected System.Web.UI.WebControls.TextBox txtEndDate;

        private CommonUtility clsUtil = new CommonUtility();
        private QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
        private Promo_TextData dtsPromo_Text;
        private Promo_TextTable _promoTable;
        private Promo_TextSubdivisionTable _promoSubdivisionTable;

        #endregion Item Declarations

        protected void Page_Load(object sender, System.EventArgs e) {
            /*
             * Region are temporary removed
             * 
             * keep trSubdivision visible = false;
             * verify that ShowHideSubdivision() javascript is never called
             */
            AddJavascript();
            AdjustUI();
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

        public Promo_TextData DataSource {
            get { return dtsPromo_Text; }
            set {
                dtsPromo_Text = value;
                this._promoTable = dtsPromo_Text.Promo_Text;
                this._promoSubdivisionTable = dtsPromo_Text.Promo_TextSubdivision;
            }
        }

        public string Title {
            set { this.lblTitle.Text = value; }
        }

        private bool IsNewPromo_Text {
            get {
                try { return Convert.ToBoolean(this.ViewState["IsNewPromo_Text"].ToString()); }
                catch { throw new Exception("Error in Promo_Text : Promo_Text is not DataBinded"); }
            }
            set { this.ViewState["IsNewPromo_Text"] = value; }
        }

        public int Promo_TextID {
            get {
                try { return Convert.ToInt32(this.lblID.Text); }
                catch { return 0; }
            }
            set { this.lblID.Text = value.ToString(); }
        }

        public string Name {
            get { return this.txtName.Text; }
            set { this.txtName.Text = value; }
        }

        public string Code {
            get { return this.txtCode.Text; }
            set { this.txtCode.Text = value; }
        }

        public string Description {
            get { return this.txtDescription.Text; }
            set { this.txtDescription.Text = value; }
        }

        public bool National {
            get { return this.chkNational.Checked; }
            set { this.chkNational.Checked = value; }
        }

        public string FMID {
            get { return txtFMID.Text; }
            set { txtFMID.Text = value; }
        }

        public string FMName {
            set { txtFMName.Text = value; }
        }

        public string VendorID {
            get { return txtVendorID.Text; }
            set { txtVendorID.Text = value; }
        }

        public string VendorName {
            set { txtVendorName.Text = value; }
        }

        public bool Enabled {
            get { return this.chkEnabled.Checked; }
            set { this.chkEnabled.Checked = value; }
        }

        private string HiddenCurrentSubdivision {
            get { return this.hidCurrentSubdivision.Value; }
            set { this.hidCurrentSubdivision.Value = value; }
        }

        public ArrayList SelectedSubdivision {
            get {
                try {
                    ArrayList al = new ArrayList();
                    string[] subdivision = this.ctrlSubdivisionSelector.GetSelectedSubdivision();//this.HiddenCurrentSubdivision.Split(',');
                    for (int i = 0; i < subdivision.Length; i++) {
                        if (subdivision[i].ToString().Trim() != String.Empty) {
                            al.Add(subdivision[i].ToString());
                        }
                    }
                    return al;
                }
                catch {
                    return null;
                }
            }
        }

        public string PromoText {
            get { return txtText.Text; }
            set { txtText.Text = value; }
        }

        #endregion Property

        public override void DataBind() {
            try {
                LoadData();
                if (this.DataSource.Promo_Text.Rows[0][Promo_TextTable.FLD_PKID].ToString() != "0") {
                    SetValue();
                    this.IsNewPromo_Text = false;
                }
                else {
                    this.IsNewPromo_Text = true;
                }
            }
            catch (Exception ex) {
                this.Page.SetPageMessage("Invalid Promo_Text Data: " + ex.Message);
            }
        }

        protected override void LoadData() {
            int pkID = Convert.ToInt32(this.DataSource.Promo_Text.Rows[0][tableDataRef.FLD_PKID].ToString());
            QSPForm.Business.Promo_TextSubdivisionSystem regSys = new QSPForm.Business.Promo_TextSubdivisionSystem();

            DataTable currentSubdivision = regSys.SelectAllByPromo_TextID(pkID);
            ArrayList IdCollection = new ArrayList();

            QSPForm.Business.SubdivisionCollection subCol = new QSPForm.Business.SubdivisionCollection();
            QSPForm.Business.Subdivision sub;// = new QSPForm.Business.Subdivision();
            foreach (DataRow row in currentSubdivision.Rows) {
                sub = new QSPForm.Business.Subdivision();
                sub.SubdivisionCode = row[QSPForm.Common.DataDef.Promo_TextSubdivisionTable.FLD_SUBDIVISION_CODE].ToString();
                sub.SubdivisionName1 = row[QSPForm.Common.DataDef.Promo_TextSubdivisionTable.FLD_SUBDIVISION_NAME_1].ToString();
                subCol.Add(sub);
            }

            this.ctrlSubdivisionSelector.SelectedSubdivision = subCol;
            this.ctrlSubdivisionSelector.DataBind();
        }

        private void SetValue() {
            this.Promo_TextID = Convert.ToInt32(_promoTable.Rows[0][tableDataRef.FLD_PKID].ToString());
            this.Description = _promoTable.Rows[0][tableDataRef.FLD_DESCRIPTION].ToString();
            this.Name = _promoTable.Rows[0][tableDataRef.FLD_PROMO_TEXT_NAME].ToString();
            this.Code = _promoTable.Rows[0][tableDataRef.FLD_PROMO_TEXT_CODE].ToString();
            this.PromoText = _promoTable.Rows[0][tableDataRef.FLD_TEXT].ToString();
            this.Enabled = Convert.ToBoolean(_promoTable.Rows[0][tableDataRef.FLD_ENABLED].ToString());
            string[] fminfo = _promoTable.Rows[0][tableDataRef.FLD_FM_ID].ToString().Split('-');
            if (fminfo.Length >= 2) {
                this.FMID = fminfo[0].Trim();
                this.FMName = fminfo[1].Trim();
            }
            this.National = Convert.ToBoolean(_promoTable.Rows[0][tableDataRef.FLD_NATIONAL].ToString());

            string[] vendorinfo = _promoTable.Rows[0][tableDataRef.FLD_VENDOR_ID].ToString().Split('-');
            if (vendorinfo.Length >= 2) {
                this.VendorID = vendorinfo[0].Trim();
                this.VendorName = vendorinfo[1].Trim();
            }
        }

        private string FormatToDate(string s) {
            string[] d = s.Split(' ');
            return d[0];
        }

        public bool UpdateDataSource() {
            bool IsSuccess = false;

            DataRow row = this.dtsPromo_Text.Promo_Text.Rows[0];
            //QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();

            comSys.UpdateRow(row, Promo_TextTable.FLD_PROMO_TEXT_NAME, this.Name);
            comSys.UpdateRow(row, Promo_TextTable.FLD_PROMO_TEXT_CODE, this.Code);
            comSys.UpdateRow(row, Promo_TextTable.FLD_DESCRIPTION, this.Description);
            comSys.UpdateRow(row, Promo_TextTable.FLD_ENABLED, this.Enabled.ToString());
            comSys.UpdateRow(row, Promo_TextTable.FLD_VENDOR_ID, this.VendorID);
            comSys.UpdateRow(row, Promo_TextTable.FLD_TEXT, this.PromoText);

            if (row.RowState == DataRowState.Added) {
                row[Promo_TextTable.FLD_CREATE_USER_ID] = Page.UserID;
            }
            else {
                row[Promo_TextTable.FLD_UPDATE_USER_ID] = Page.UserID;
            }

            if (this.National) {
                //remove fsm
                comSys.UpdateRow(row, Promo_TextTable.FLD_FSM_ID, "");

                //remove all subdivision
                foreach (DataRow dtRow in dtsPromo_Text.Promo_TextSubdivision.Rows) {
                    dtRow[Promo_TextSubdivisionTable.FLD_UPDATE_USER_ID] = Page.UserID;
                    dtRow.Delete();
                }
            }
            else {
                comSys.UpdateRow(row, Promo_TextTable.FLD_FSM_ID, "");

                //find removed subdivision
                foreach (DataRow dtRow in dtsPromo_Text.Promo_TextSubdivision.Rows) {
                    if (!this.SelectedSubdivision.Contains(dtRow[Promo_TextSubdivisionTable.FLD_PKID].ToString())) {
                        dtRow[Promo_TextSubdivisionTable.FLD_UPDATE_USER_ID] = Page.UserID;
                        dtRow.Delete();
                    }
                }

                if (this.SelectedSubdivision.Count > 0) {
                    //find new subdivision
                    for (int i = 0; i < this.SelectedSubdivision.Count; i++) {
                        DataView dv = new DataView(dtsPromo_Text.Promo_TextSubdivision);
                        dv.Sort = Promo_TextSubdivisionTable.FLD_SUBDIVISION_CODE;

                        int iIndex = dv.Find(this.SelectedSubdivision[i].ToString().Trim());
                        //Be sure to add only new subdivision
                        if (iIndex == -1) {
                            DataRow r = dtsPromo_Text.Promo_TextSubdivision.NewRow();
                            r[Promo_TextSubdivisionTable.FLD_CREATE_USER_ID] = Page.UserID;
                            r[Promo_TextSubdivisionTable.FLD_PROMO_TEXT_ID] = dtsPromo_Text.Promo_Text.Rows[0][Promo_TextTable.FLD_PKID].ToString();
                            r[Promo_TextSubdivisionTable.FLD_SUBDIVISION_CODE] = this.SelectedSubdivision[i].ToString().Trim();
                            dtsPromo_Text.Promo_TextSubdivision.Rows.Add(r);
                        }
                    }
                }
                else {
                    comSys.UpdateRow(row, Promo_TextTable.FLD_FSM_ID, this.FMID);
                }
            }

            IsSuccess = true;

            return IsSuccess;
        }

        private void AdjustUI() {
            /*
             * Region are temporary removed
             * 
             * keep trSubdivision visible = false;
             * verify that ShowHideSubdivision() javascript is never called
             */

            // Adjust FM Selector if User is Admin or IT
            if (this.Page.Role >= QSPForm.Business.AuthSystem.ROLE_ADMINISTRATOR) {
                trFmInfo.Style["display"] = "none";
                trFmEdit.Style["display"] = "block";
                TrVendorInfo.Style["display"] = "none";
                TrVendorEdit.Style["display"] = "block";
            }

            // Adjust for National
            if (this.National) {
                //trSubdivision.Style["display"] = "none";
                trFM.Style["display"] = "none";
            }

            if (this.Promo_TextID == 0) {
                this.lblID.Text = "New ID";
            }

            trSubdivision.Visible = false;
        }

        private void AddJavascript() {
            //clsUtil.SetJScriptForOpenSelector(imgBtnSelectFM,txtFMID,txtFMName,QSPForm.Business.AppItem.FMSelector,0,0);
            //clsUtil.SetJScriptForOpenSelector(imgBtnSelectVendor,txtVendorID,txtVendorName,QSPForm.Business.AppItem.VendorSelector,0,0);
            clsUtil.SetJScriptForOpenSelector(imgBtnSelectFM, txtFMID, txtFMName, "FMSelector.aspx", "PromoDetail", 0, 0, null);
            clsUtil.SetJScriptForOpenSelector(imgBtnSelectVendor, txtVendorID, txtVendorName, "VendorSelector.aspx", "VendorDetail", 0, 0, null);

            //this.chkNational.Attributes["onClick"] = "ShowHideSubdivision();";
            this.chkNational.Attributes["onClick"] = "ShowHideFMSelector();";
        }
    }
}