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
using dataDef = QSPForm.Common.DataDef.LogoData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web {
    public partial class Logo : BaseWebForm {
        public const string LOGO_ID = "Logo_id";
        private const string LOGO_DATA = "Logo_data";
        protected dataDef dtsLogo;

        protected void Page_Load(object sender, EventArgs e) {
            AdjustDisplay();
            try {
                // Put user code to initialize the page here	
                LoadData();
                if (!IsPostBack) {
                    //this.Page.ValSummary.Visible = false;
                    BindForm();
                }
            }
            catch (Exception ex) {
                this.SetPageError(ex);
            }

        }

        private void AdjustDisplay() {
            if (this.Mode == PageMode.Edit) {
                this.Header.InstructionText = "<br />Complete the logo information below. To upload an image, it must be saved on your hard drive. Click Browse button, choose file, highlight image description and click Open button. The image name will populate the text box. Then click Upload button and Save button to store the image in Order Express. The logo will be available in the Logo List within a few minutes.<br /><br />";
                this.Header.SectionText = "Logo";
                this.Header.PageText = "Logo Detail";
                this.Header.IconImageVisiblilty = false;


                this.ctrlToolBar.DisplayMode = QSP.OrderExpress.Web.UserControls.ToolBar.DISPLAY_EDIT;
                this.ctrlToolBar.DeleteButton.Visible = false; //delete is forbiden
                this.ctrlLogo.Mode = PageMode.Edit;
                this.ctrlLogo.AdjustMode();
                this.ctrlToolBar.SaveButton.Visible = true;
            }
            else {
                this.Header.InstructionText = "";
                this.Header.SectionText = "Logo";
                this.Header.PageText = "Logo Detail";
                this.Header.IconImageVisiblilty = false;

                this.ctrlToolBar.DisplayMode = QSP.OrderExpress.Web.UserControls.ToolBar.DISPLAY_READ;
                this.ctrlLogo.Mode = PageMode.ReadOnly;
                this.ctrlLogo.Title = "Logo Information";
                this.ctrlLogo.AdjustMode();
                this.ctrlToolBar.EditButton.Visible = true;
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
            //Deleting a logo is not allow
            //this.imgBtnDelete.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnDelete_Click);

            //this.imgBtnSave.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnSave_Click);
            this.ctrlToolBar.SaveClick += new EventHandler(ctrlToolBar_SaveClick);
            this.ctrlToolBar.EditClick += new EventHandler(ctrlToolBar_EditClick);

        }

        void ctrlToolBar_EditClick(object sender, EventArgs e) {
            this.Mode = PageMode.Edit;
            AdjustDisplay();
        }

        void ctrlToolBar_SaveClick(object sender, EventArgs e) {
            try {
                Boolean blnValid = true;
                blnValid = ctrlLogo.IsValid();
                if (!blnValid) {
                    return;
                }

                blnValid = ctrlLogo.UpdateDataSource();
                if (!blnValid) {
                    return;
                }

                QSPForm.Business.LogoSystem prmSys = new QSPForm.Business.LogoSystem();
                blnValid = prmSys.UpdateAllDetail(dtsLogo);

                if ((blnValid) && (LogoID == 0)) {
                    ctrlLogo.CreateImageAndPreview();
                    LogoID = Convert.ToInt32(this.dtsLogo.Logo.Rows[0][LogoTable.FLD_PKID].ToString());
                }

                if (blnValid) {
                    //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.LogoDetailInfo, LogoDetailInfo.LOGO_ID, LogoID.ToString());
                    Response.Redirect("Logo.aspx?logo_id=" + LogoID, true);
                }


            }
            catch (Exception ex) {
                this.SetPageMessage("Error while saving Logo : " + ex.Message);
            }
        }


        #endregion

        public int LogoID {
            get {
                try {
                    if (this.ViewState[LOGO_ID] == null) {
                        this.ViewState[LOGO_ID] = Convert.ToInt32(this.Request[LOGO_ID].ToString());
                    }
                    return Convert.ToInt32(this.ViewState[LOGO_ID].ToString());
                }
                catch {
                    return 0;
                }
            }
            set {
                ViewState[LOGO_ID] = value;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            this.ViewState[LOGO_DATA] = dtsLogo;
        }

        public override void BindForm() {
            ctrlLogo.DataBind();
        }

        //protected override void LoadData()
        public override void LoadData() {
            if (!IsPostBack) {
                QSPForm.Business.LogoSystem prdSys = new QSPForm.Business.LogoSystem();
                //ToDo insert row when c_User_ID=0
                if (this.LogoID == 0) {
                    dtsLogo = prdSys.InitializeLogo(this.UserID);
                }
                else {
                    dtsLogo = prdSys.SelectAllDetail(this.LogoID);
                }

                //this.ViewState[LOGO_ID] = LogoID;
                this.ViewState[LOGO_DATA] = dtsLogo;
            }
            else {
                //LogoID = Convert.ToInt32(this.ViewState[LOGO_ID]);
                dtsLogo = (dataDef)this.ViewState[LOGO_DATA];
            }

            ctrlLogo.LogoID = LogoID;
            ctrlLogo.DataSource = dtsLogo;
        }
    } 
}