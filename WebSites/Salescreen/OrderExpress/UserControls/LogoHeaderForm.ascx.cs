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
using dataRef = QSPForm.Common.DataDef.LogoData;
using tableDataRef = QSPForm.Common.DataDef.LogoTable;
using System.Drawing.Imaging;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>User Information - read only</summary>
    public partial class LogoHeaderForm : BaseWebFormControl {
        public const int PERSONAL_IMAGES_CATEGORY_ID = 28;

        #region Item Declarations



        protected System.Web.UI.WebControls.CompareValidator compVal_StartDate;
        protected System.Web.UI.WebControls.CompareValidator Comparevalidator1;
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
        private LogoData dtsLogo;
        private LogoTable _promoTable;
        private LogoSubdivisionTable _promoSubdivisionTable;

        #endregion Item Declarations

        protected void Page_Load(object sender, System.EventArgs e) {

            /* Region are temporary removed 
             
             * ShowHideSubdivision() should not be called 
             * trSubdivision should stay visible = false;
             
             */

            AddJavascript();
            if (!IsPostBack) {
                AdjustUI();

            }

            trSubdivision.Visible = false;
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

        public LogoData DataSource {
            get { return dtsLogo; }
            set {
                dtsLogo = value;
                this._promoTable = dtsLogo.Logo;
                this._promoSubdivisionTable = dtsLogo.LogoSubdivision;
            }
        }
        public string Title {
            set { this.lblTitle.Text = value; }
        }

        private bool IsNewLogo {
            get {
                try { return Convert.ToBoolean(this.ViewState["IsNewLogo"].ToString()); }
                catch { throw new Exception("Error in Logo : Logo is not DataBinded"); }
            }
            set { this.ViewState["IsNewLogo"] = value; }
        }

        public int LogoID {
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
        public string Description {
            get { return this.txtDescription.Text; }
            set { this.txtDescription.Text = value; }
        }
        public bool National {
            get { return this.chkNational.Checked; }
            set { this.chkNational.Checked = value; }
        }
        //		public string FMID
        //		{
        //			get
        //			{	// return right value depending of the ROLE
        //				if( (this.Page.Role >= QSPForm.Business.AuthSystem.ROLE_ADMINISTRATOR) && (this.LogoID == 0))
        //				{
        //					return txtFMID.Text;
        //				}
        //				else
        //				{
        //					return this.lblFMInfo.Text;
        //				}
        //			}
        //			set
        //			{
        //				if(this.LogoID != 0)
        //				{
        //					this.lblFMInfo.Text = value;
        //				}
        //			}
        //		}

        public string FMID {
            get {
                // New tweak in ASP .NET 2.0 since posted values
                // are no longer processed for ReadOnly textboxes
                if (Request[txtFMID.UniqueID] != null) {
                    txtFMID.Text = Request[txtFMID.UniqueID];
                }

                return txtFMID.Text;
            }
            set { txtFMID.Text = value; }
        }
        public string FMName {
            set { txtFMName.Text = value; }
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

        //		private string FilePath
        //		{
        //			get
        //			{
        //				try{ return this.ViewState["FilePath"].ToString();}
        //				catch{return String.Empty;}
        //			}
        //			set{this.ViewState["FilePath"]=value;}
        //		}
        //		
        //		private string PreviewPath
        //		{
        //			get
        //			{
        //				try{ return this.ViewState["PreviewPath"].ToString();}
        //				catch{return String.Empty;}
        //			}
        //			set{this.ViewState["PreviewPath"]=value;}
        //		}
        //
        private string TempPath {
            get {
                return Server.MapPath(QSPForm.Common.QSPFormConfiguration.PromoLogoTempFolder);
            }
            //set{this.ViewState["TempPath"]=value;}
        }

        public string TempFileName {
            get {
                try { return this.ViewState["FileName"].ToString(); }
                catch { return String.Empty; }
            }
            set { this.ViewState["FileName"] = value; }
        }

        public string TempPreviewFileName {
            get {
                try { return this.ViewState["PreviewFileName"].ToString(); }
                catch { return String.Empty; }
            }
            set { this.ViewState["PreviewFileName"] = value; }
        }

        public string FileExtension {
            get {
                try { return this.ViewState["FileExtension"].ToString(); }
                catch { return String.Empty; }
            }
            set { this.ViewState["FileExtension"] = value; }
        }

        public string DefaultFileExtension {
            get { return QSPForm.Common.QSPFormConfiguration.ImagePreviewFileExtension; }
        }
        #endregion Property

        protected void btnUpload_Click(object sender, System.EventArgs e) {
            Upload();
        }

        public override void DataBind() {
            try {
                LoadData();
                if (this.DataSource.Logo.Rows[0][LogoTable.FLD_PKID].ToString() != "0") {
                    SetValue();
                    this.IsNewLogo = false;
                }
                else {
                    this.IsNewLogo = true;
                }
            }
            catch (Exception ex) {
                this.Page.SetPageMessage("Invalid Logo Data: " + ex.Message);
            }
        }

        protected override void LoadData() {
            int pkID = Convert.ToInt32(this.DataSource.Logo.Rows[0][tableDataRef.FLD_PKID].ToString());
            QSPForm.Business.LogoSubdivisionSystem regSys = new QSPForm.Business.LogoSubdivisionSystem();

            DataTable currentSubdivision = regSys.SelectAllByLogoID(pkID);
            ArrayList IdCollection = new ArrayList();

            QSPForm.Business.SubdivisionCollection subCol = new QSPForm.Business.SubdivisionCollection();
            QSPForm.Business.Subdivision sub;// = new QSPForm.Business.Subdivision();
            foreach (DataRow row in currentSubdivision.Rows) {
                sub = new QSPForm.Business.Subdivision();
                sub.SubdivisionCode = row[QSPForm.Common.DataDef.LogoSubdivisionTable.FLD_SUBDIVISION_CODE].ToString();
                sub.SubdivisionName1 = row[QSPForm.Common.DataDef.LogoSubdivisionTable.FLD_SUBDIVISION_NAME_1].ToString();
                subCol.Add(sub);
            }

            this.ctrlSubdivisionSelector.SelectedSubdivision = subCol;
            this.ctrlSubdivisionSelector.DataBind();

            clsUtil.SetImageCategoryDropDownList(ddlImageCategory, true);
        }

        private void SetValue() {
            this.LogoID = Convert.ToInt32(_promoTable.Rows[0][tableDataRef.FLD_PKID].ToString());
            this.Description = _promoTable.Rows[0][tableDataRef.FLD_DESCRIPTION].ToString();
            this.Name = _promoTable.Rows[0][tableDataRef.FLD_LOGO_NAME].ToString();
            this.Enabled = Convert.ToBoolean(_promoTable.Rows[0][tableDataRef.FLD_ENABLED].ToString());
            string[] fminfo = _promoTable.Rows[0][tableDataRef.FLD_FM_ID].ToString().Split('-');
            if (fminfo.Length >= 2) {
                this.FMID = fminfo[0].Trim();
                this.FMName = fminfo[1].Trim();
            }
            this.National = Convert.ToBoolean(_promoTable.Rows[0][tableDataRef.FLD_NATIONAL].ToString());
            this.FileExtension = _promoTable.Rows[0][tableDataRef.FLD_FILE_EXTENSION].ToString();
            this.ddlImageCategory.SelectedValue = _promoTable.Rows[0][tableDataRef.FLD_CATEGORY].ToString();
            //this.TempFileName = LogoID + "." + FileExtension;
            //this.PreviewPath = _promoTable.Rows[0][tableDataRef.FLD_PREVIEW_PATH].ToString();
            //this.TempPath = _promoTable.Rows[0][tableDataRef.FLD_PREVIEW_PATH].ToString();
        }

        private string FormatToDate(string s) {
            string[] d = s.Split(' ');
            return d[0];
        }

        public bool UpdateDataSource() {
            bool IsSuccess = false;

            DataRow row = this.dtsLogo.Logo.Rows[0];
            //QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();

            comSys.UpdateRow(row, LogoTable.FLD_LOGO_NAME, this.Name);
            comSys.UpdateRow(row, LogoTable.FLD_DESCRIPTION, this.Description);
            comSys.UpdateRow(row, LogoTable.FLD_ENABLED, this.Enabled.ToString());
            comSys.UpdateRow(row, LogoTable.FLD_CATEGORY, ddlImageCategory.SelectedValue.ToString());

            if (row.RowState == DataRowState.Added) {
                row[LogoTable.FLD_CREATE_USER_ID] = Page.UserID;

                if ((this.TempFileName != String.Empty)) {
                    row[LogoTable.FLD_FILE_EXTENSION] = this.FileExtension;
                }
                else {
                    throw new Exception("You must select a image first");
                }
            }
            else {
                row[LogoTable.FLD_UPDATE_USER_ID] = Page.UserID;
            }

            if (this.National) {
                //remove fsm
                comSys.UpdateRow(row, LogoTable.FLD_FSM_ID, "");

                //remove all subdivision
                foreach (DataRow dtRow in dtsLogo.LogoSubdivision.Rows) {
                    dtRow[LogoSubdivisionTable.FLD_UPDATE_USER_ID] = Page.UserID;
                    dtRow.Delete();
                }
            }
            else {

                comSys.UpdateRow(row, LogoTable.FLD_FSM_ID, this.FMID);

                //find removed subdivision
                foreach (DataRow dtRow in dtsLogo.LogoSubdivision.Rows) {
                    if (!this.SelectedSubdivision.Contains(dtRow[LogoSubdivisionTable.FLD_PKID].ToString())) {
                        dtRow[LogoSubdivisionTable.FLD_UPDATE_USER_ID] = Page.UserID;
                        dtRow.Delete();
                    }
                }

                if (this.SelectedSubdivision.Count > 0) {
                    //find new subdivision
                    for (int i = 0; i < this.SelectedSubdivision.Count; i++) {
                        DataView dv = new DataView(dtsLogo.LogoSubdivision);
                        dv.Sort = LogoSubdivisionTable.FLD_SUBDIVISION_CODE;

                        int iIndex = dv.Find(this.SelectedSubdivision[i].ToString().Trim());
                        //Be sure to add only new subdivision
                        if (iIndex == -1) {
                            DataRow r = dtsLogo.LogoSubdivision.NewRow();
                            r[LogoSubdivisionTable.FLD_CREATE_USER_ID] = Page.UserID;
                            r[LogoSubdivisionTable.FLD_LOGO_ID] = dtsLogo.Logo.Rows[0][LogoTable.FLD_PKID].ToString();
                            r[LogoSubdivisionTable.FLD_SUBDIVISION_CODE] = this.SelectedSubdivision[i].ToString().Trim();
                            dtsLogo.LogoSubdivision.Rows.Add(r);
                        }
                    }
                }
                /*else
                {
                    comSys.UpdateRow(row,LogoTable.FLD_FSM_ID,this.FMID);
                }*/
            }

            IsSuccess = true;

            return IsSuccess;
        }

        private void Upload() {
            try {
                if (ValidateFile(ctrlUpload.PostedFile)) {
                    //verify if a file has been set in the temp
                    if (System.IO.File.Exists(this.TempPath + this.TempFileName)) {
                        FileInfo f = new FileInfo(this.TempPath + this.TempFileName);
                        f.Delete();
                    }
                    if (System.IO.File.Exists(this.TempPath + this.TempPreviewFileName)) {
                        FileInfo f = new FileInfo(this.TempPath + this.TempPreviewFileName);
                        f.Delete();
                    }

                    string[] file = ctrlUpload.PostedFile.FileName.Split('.');
                    this.FileExtension = file[file.Length - 1];
                    this.TempFileName = String.Format("{0:dd-MM-yyyy-HHmmss}", DateTime.Now) + "-" + this.Session.SessionID + "." + FileExtension;
                    this.TempPreviewFileName = String.Format("{0:dd-MM-yyyy-HHmmss}", DateTime.Now) + "-" + this.Session.SessionID + "." + DefaultFileExtension;

                    ctrlUpload.PostedFile.SaveAs(this.TempPath + this.TempFileName);
                    ctrlUpload.Dispose();
                    ctrlUpload = null;
                    GC.Collect();

                    /*
					Bitmap bmp =  new Bitmap(this.TempPath+this.TempFileName);
					bmp.SetResolution(72,72);
					bmp.Save(this.TempPath+this.TempPreviewFileName,System.Drawing.Imaging.ImageFormat.Jpeg);
                    //(this.TempPath + this.TempPreviewFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    bmp.Dispose();
                    */
                    string origin = this.TempPath + this.TempFileName;
                    string destination = this.TempPath + this.TempPreviewFileName;
                    SavePreview(origin, destination);

                    AdjustUI();
                }
                else {
                    this.Page.SetPageMessage("Error while uploading image : please select JPEG or TIF image");
                }
            }
            catch (Exception ex) {
                string message;
                if (ex.Message.Contains("GDI"))
                    message = " This picture is used by an other process or is not in the right format    " + ex.Source + ex.StackTrace + ex.ToString();
                else
                    message = ex.Message;
                this.Page.SetPageMessage("Error while uploading image : " + message);
            }
        }

        private void AdjustUI() {
            /*
                * Use the style instead of visible because 
                * controls that are not visible are not rendered
            */

            //Show upload tool while it's new promo
            if ((this.IsNewLogo)) {
                trUpload.Style["display"] = "block";
                if (this.TempPreviewFileName != String.Empty) {
                    this.imgDetail.ImageUrl = QSPForm.Common.QSPFormConfiguration.PromoLogoTempFolder + this.TempPreviewFileName;
                }
            }
            else {
                this.imgDetail.ImageUrl = QSPForm.Common.QSPFormConfiguration.LogoImagePreviewPath + this.LogoID + "." + QSPForm.Common.QSPFormConfiguration.ImagePreviewFileExtension;    //this.FileExtension;
            }

            // Adjust FM Selector if User is Admin or IT
            if (this.Page.Role >= QSPForm.Business.AuthSystem.ROLE_FIELD_SUPPORT) {
                trFmInfo.Style["display"] = "none";
                trFmEdit.Style["display"] = "block";

                chkNational.Enabled = true;

                if (this.IsNewLogo) {
                    chkNational.Checked = true;
                }
            }
            else {
                trFmInfo.Style["display"] = "none";
                trFmEdit.Style["display"] = "none";

                chkNational.Enabled = false;

                if (this.IsNewLogo) {
                    chkNational.Checked = false;
                    trNational.Visible = false;
                    ddlImageCategory.SelectedIndex = ddlImageCategory.Items.IndexOf(ddlImageCategory.Items.FindByValue(PERSONAL_IMAGES_CATEGORY_ID.ToString()));
                    FMID = Page.FMID;
                }
            }

            // Adjust for National
            if (this.National) {
                trSubdivision.Style["display"] = "none";
                trFM.Style["display"] = "none";
            }

            if (this.LogoID == 0) {
                this.lblID.Text = "New ID";
            }
            //			else
            //			{
            //				this.trFmInfo.Style["display"] = "block";
            //				this.trFmEdit.Style["display"] = "none";
            //			}
        }

        private void AddJavascript() {
            //clsUtil.SetJScriptForOpenSelector(imgBtnSelectFM,txtFMID,txtFMName,QSPForm.Business.AppItem.FMSelector,0,0);
            clsUtil.SetJScriptForOpenSelector(imgBtnSelectFM, txtFMID, txtFMName, "FMSelector.aspx", "LogoDetail", 0, 0, null);
            //this.chkNational.Attributes["onClick"] = "ShowHideSubdivision();";
            //RNK         
            //  this.chkNational.Attributes["onClick"] = "ShowHideFMSelector();";
        }

        private bool ValidateFile(HttpPostedFile pFile) {
            bool isValid = true;
            ArrayList enableMimeType = new ArrayList();
            string[] mime = QSPForm.Common.QSPFormConfiguration.LogoImageExtensionFile.Split(',');
            for (int i = 0; i < mime.Length; i++) enableMimeType.Add(mime[i]);

            if (pFile == null)
                isValid = false;
            if (!(pFile.ContentLength > 0))
                isValid = false;
            if (!enableMimeType.Contains(pFile.ContentType))
                isValid = false;

            return isValid;
        }

        public void CreateImageAndPreview() {
            if (System.IO.File.Exists(this.TempPath + this.TempFileName)) {
                //create an image with the upload
                /*
				Bitmap bmp =  new Bitmap(this.TempPath+this.TempFileName);
				bmp.Save( Server.MapPath(QSPForm.Common.QSPFormConfiguration.LogoImagePath)+ 
							this.DataSource.Logo.Rows[0][QSPForm.Common.DataDef.LogoTable.FLD_PKID] + 
							"." + this.FileExtension );
                 */
                File.Copy(this.TempPath + this.TempFileName, Server.MapPath(QSPForm.Common.QSPFormConfiguration.LogoImagePath).ToString() +
                                   this.DataSource.Logo.Rows[0][QSPForm.Common.DataDef.LogoTable.FLD_PKID] +
                                   "." + this.FileExtension);

                //create duplicate at 72 dpi
                /*
                Bitmap bmp = new Bitmap(this.TempPath + this.TempFileName);
				bmp.SetResolution(72,72);
				bmp.Save(Server.MapPath(QSPForm.Common.QSPFormConfiguration.LogoImagePreviewPath)+ 
							this.DataSource.Logo.Rows[0][QSPForm.Common.DataDef.LogoTable.FLD_PKID] + 
					"." + this.DefaultFileExtension,System.Drawing.Imaging.ImageFormat.Jpeg);

				bmp.Dispose();
				*/
                string origin = this.TempPath + this.TempFileName;
                string destination = Server.MapPath(QSPForm.Common.QSPFormConfiguration.LogoImagePreviewPath) +
                                        this.DataSource.Logo.Rows[0][QSPForm.Common.DataDef.LogoTable.FLD_PKID] +
                                        "." + this.DefaultFileExtension;
                SavePreview(origin, destination);

                FileInfo f = new FileInfo(this.TempPath + this.TempFileName);
                f.Delete();
            }
            else {
                throw new Exception("Error while saving image");
            }
        }

        protected void chkNational_CheckedChanged(object sender, EventArgs e) {
            if (chkNational.Checked == true)
                trFM.Style["display"] = "none";
            else
                trFM.Style["display"] = "block";
        }

        private void SavePreview(string Origin, string Destination) {
            //Bitmap OrigBit = new Bitmap(MyFile.InputStream); //Temporary Bitmap object to intstantiate original image
            //StreamReader sr = new StreamReader(Origin);
            //FileInfo f = new FileInfo(Origin);
            FileStream fs = new FileStream(Origin, FileMode.Open);

            Bitmap OrigBit = new Bitmap(fs);
            Bitmap NewBit = new Bitmap(OrigBit.Width, OrigBit.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb); //New Bitmap object set to same size as original but with new PixelFormat (this will fix the CMYK)

            Graphics MyGraph = Graphics.FromImage(NewBit); //We need a new graphics object as the middle man to draw the original image onto the new image
            MyGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            MyGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            MyGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            Rectangle MyRect = new Rectangle(0, 0, OrigBit.Width, OrigBit.Height); //Create a Rectangle with same size as original image
            MyGraph.DrawImage(OrigBit, MyRect); //Draw your original image onto the graphic over the same size rectangle. I know it seems like you're missing a step here but you actually just transferred the original image onto the new one via the graphics objects.
            fs.Close();
            fs.Dispose();

            NewBit.Save(Destination, ImageFormat.Jpeg);

            MyGraph.Dispose();
            NewBit.Dispose();
            OrigBit.Dispose();

            GC.Collect();
        }
    }
}