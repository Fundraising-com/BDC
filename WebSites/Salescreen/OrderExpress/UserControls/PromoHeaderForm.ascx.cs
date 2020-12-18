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
using dataRef = QSPForm.Common.DataDef.PromoData;
using tableDataRef = QSPForm.Common.DataDef.PromoTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>User Information - read only</summary>
    public partial class PromoHeaderForm : BaseWebFormControl {
        #region Item Declarations

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCurrentSubdivision;
        protected System.Web.UI.WebControls.ListBox lbxSubdivision;
        protected System.Web.UI.WebControls.ListBox lbxCurrentSubdivision;

        private CommonUtility clsUtil = new CommonUtility();
        private QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
        //private PromoData dtsPromo;
        //private PromoTable _promoTable;
        //private PromoSubdivisionTable _promoSubdivisionTable;
        private Promo_ImageTable _promoImg;
        #endregion Item Declarations

        protected void Page_Load(object sender, System.EventArgs e) {
            //			AddJavascript();
            if (!IsPostBack) {
                AdjustUI();
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

        //public PromoData DataSource
        //{
        //    get{return dtsPromo;}
        //    set{
        //        dtsPromo = value;
        //        this._promoTable = dtsPromo.Promo;
        //        this._promoSubdivisionTable = dtsPromo.PromoSubdivision;
        //    }
        //}

        public Promo_ImageTable DataSource {
            get { return _promoImg; }
            set { _promoImg = value; }
        }
        public string Title {
            set { this.lblTitle.Text = value; }
        }

        private bool IsNewPromo {
            get {
                try { return Convert.ToBoolean(this.ViewState["IsNewPromo"].ToString()); }
                catch { throw new Exception("Error in Promo : Promo is not DataBinded"); }
            }
            set { this.ViewState["IsNewPromo"] = value; }
        }

        public int PromoID {
            get {
                try { return Convert.ToInt32(this.lblID.Text); }
                catch { return 0; }
            }
            set { this.lblID.Text = value.ToString(); }
        }
        /*
		public string Name
		{
			get{return this.txtName.Text;}
			set{this.txtName.Text = value;}
		}
		public string Description
		{
			get{return this.txtDescription.Text;}
			set{this.txtDescription.Text = value;}
		}
		public bool National
		{
			get{return this.chkNational.Checked;}
			set{this.chkNational.Checked = value;}
		}
		public string LabelingStartDate
		{
			get{return this.txtStartDate.Text;}
			set{this.txtStartDate.Text = value;}
		}
		public string LabelingEndDate
		{
			get{return this.txtEndDate.Text;}
			set{this.txtEndDate.Text = value;}
		}
//		public string FMID
//		{
//			get
//			{	// return right value depending of the ROLE
//				if(this.Page.Role >= QSPForm.Business.AuthSystem.ROLE_ADMINISTRATOR)
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
//				this.lblFMInfo.Text = value;
//				this.txtFMID.Text = value;
//			}
//		}
		public string FMID
			  {
				  get{ return txtFMID.Text; }
				  set{ txtFMID.Text = value; }
			  }
		public string FMName
		{
			set{ txtFMName.Text = value;}
		}
		public bool Enabled
		{
			get{return this.chkEnabled.Checked;}
			set{this.chkEnabled.Checked = value;}
		}
		private string HiddenCurrentSubdivision
		{
			get{return this.hidCurrentSubdivision.Value;}
			set{this.hidCurrentSubdivision.Value = value;}
		}
		public ArrayList SelectedSubdivision
		{
			get
			{
				try
				{
					ArrayList al = new ArrayList();
					string [] subdivision = this.ctrlSubdivisionSelector.GetSelectedSubdivision();//this.HiddenCurrentSubdivision.Split(',');
					for(int i=0;i<subdivision.Length;i++)
					{
						if(subdivision[i].ToString().Trim() != String.Empty)
						{
							al.Add(subdivision[i].ToString());
						}
					}
					return al;
				}
				catch
				{
					return null;
				}
			}
		}

//		private string Path
//		{
//			get
//			{
//				try{ return this.ViewState["Path"].ToString();}
//				catch{return String.Empty;}
//			}
//			set{this.ViewState["Path"]=value;}
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
        */
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
                if (this.DataSource.Rows[0][PromoTable.FLD_PKID].ToString() != "0") {
                    SetValue();
                    this.IsNewPromo = false;
                }
                else {
                    this.IsNewPromo = true;
                }
            }
            catch (Exception ex) {
                this.Page.SetPageMessage("Invalid Promo Data: " + ex.Message);
            }
        }

        protected override void LoadData() {
            /*
			int pkID = Convert.ToInt32(this.DataSource.Promo.Rows[0][tableDataRef.FLD_PKID].ToString());
			QSPForm.Business.PromoSubdivisionSystem regSys = new QSPForm.Business.PromoSubdivisionSystem();
			
			DataTable currentSubdivision = regSys.SelectAllByPromoID(pkID);
			ArrayList IdCollection = new ArrayList();


			QSPForm.Business.SubdivisionCollection subCol = new QSPForm.Business.SubdivisionCollection();
			QSPForm.Business.Subdivision sub;// = new QSPForm.Business.Subdivision();
			foreach(DataRow row in currentSubdivision.Rows)
			{
				sub = new QSPForm.Business.Subdivision();
				sub.SubdivisionCode = row[QSPForm.Common.DataDef.LogoSubdivisionTable.FLD_SUBDIVISION_CODE].ToString();
				sub.SubdivisionName1 = row[QSPForm.Common.DataDef.LogoSubdivisionTable.FLD_SUBDIVISION_NAME_1].ToString();
				subCol.Add(sub);
			}

			this.ctrlSubdivisionSelector.SelectedSubdivision = subCol;
			this.ctrlSubdivisionSelector.DataBind();
			*/
        }

        private void SetValue() {
            this.PromoID = Convert.ToInt32(_promoImg.Rows[0][tableDataRef.FLD_PKID].ToString());
            //this.Description = _promoTable.Rows[0][tableDataRef.FLD_DESCRIPTION].ToString();
            //this.Name = _promoTable.Rows[0][tableDataRef.FLD_PROMO_NAME].ToString();
            //this.Enabled = Convert.ToBoolean(_promoTable.Rows[0][tableDataRef.FLD_ENABLED].ToString());
            //string[] fminfo = _promoTable.Rows[0][tableDataRef.FLD_FM_ID].ToString().Split('-');
            //if(fminfo.Length >= 2)
            //{
            //	this.FMID = fminfo[0].Trim();
            //	this.FMName = fminfo[1].Trim();
            //}
            //this.National = Convert.ToBoolean(_promoTable.Rows[0][tableDataRef.FLD_NATIONAL].ToString());
            //this.FileExtension =  _promoTable.Rows[0][tableDataRef.FLD_FILE_EXTENSION].ToString();
            //this.txtStartDate.Text = FormatToDate(_promoTable.Rows[0][tableDataRef.FLD_START_DATE].ToString());
            //this.txtEndDate.Text = FormatToDate(_promoTable.Rows[0][tableDataRef.FLD_END_DATE].ToString());
            //this.FMID = _promoTable.Rows[0][tableDataRef.FLD_FM_ID].ToString();
            //this.PreviewPath = _promoTable.Rows[0][tableDataRef.FLD_PREVIEW_PATH].ToString();
            //this.TempPath = _promoTable.Rows[0][tableDataRef.FLD_PREVIEW_PATH].ToString();
        }

        private string FormatToDate(string s) {
            string[] d = s.Split(' ');
            return d[0];
        }

        public bool UpdateDataSource() {
            //    bool IsSuccess = false;

            DataRow row = this._promoImg.Rows[0];
            //    QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();

            //    comSys.UpdateRow(row,PromoTable.FLD_PROMO_NAME,this.Name);
            //    comSys.UpdateRow(row,PromoTable.FLD_DESCRIPTION,this.Description);
            //    comSys.UpdateRow(row,PromoTable.FLD_START_DATE,this.LabelingStartDate);
            //    comSys.UpdateRow(row,PromoTable.FLD_END_DATE,this.LabelingEndDate);
            //    comSys.UpdateRow(row,PromoTable.FLD_ENABLED,this.Enabled.ToString());

            if (row.RowState == DataRowState.Added) {
                //CreateImageAndPreview();

                row[PromoTable.FLD_CREATE_USER_ID] = Page.UserID;
                if ((this.TempFileName != String.Empty)) {
                    row[LogoTable.FLD_FILE_EXTENSION] = this.FileExtension;
                }
                else {
                    throw new Exception("You must select a image first");
                }
            }
            //    else
            //    {
            //        row[PromoTable.FLD_UPDATE_USER_ID] = Page.UserID;
            //    }	

            //    if(this.National)
            //    {
            //        //remove fsm
            //        comSys.UpdateRow(row,PromoTable.FLD_FSM_ID,"");

            //        //remove all subdivision
            //        foreach(DataRow dtRow in dtsPromo.PromoSubdivision.Rows)
            //        {
            //            dtRow[PromoSubdivisionTable.FLD_UPDATE_USER_ID] = Page.UserID;
            //            dtRow.Delete();
            //        }
            //    }
            //    else
            //    {
            //        comSys.UpdateRow(row,PromoTable.FLD_FSM_ID,"");

            //        //find removed subdivision
            //        foreach(DataRow dtRow in dtsPromo.PromoSubdivision.Rows)
            //        {
            //            if(! this.SelectedSubdivision.Contains(dtRow[PromoSubdivisionTable.FLD_PKID].ToString()))
            //            {
            //                dtRow[PromoSubdivisionTable.FLD_UPDATE_USER_ID] = Page.UserID;
            //                dtRow.Delete();
            //            }
            //        }

            //        if(this.SelectedSubdivision.Count > 0)
            //        {

            //            //find new subdivision
            //            for(int i=0;i<this.SelectedSubdivision.Count;i++)
            //            {
            //                DataView dv = new DataView(dtsPromo.PromoSubdivision);
            //                dv.Sort = PromoSubdivisionTable.FLD_SUBDIVISION_CODE;

            //                int iIndex = dv.Find(this.SelectedSubdivision[i].ToString());
            //                if(iIndex == -1)
            //                {
            //                    DataRow r = dtsPromo.PromoSubdivision.NewRow();
            //                    r[PromoSubdivisionTable.FLD_CREATE_USER_ID] = Page.UserID;
            //                    r[PromoSubdivisionTable.FLD_PROMO_ID] = dtsPromo.Promo.Rows[0][PromoTable.FLD_PKID].ToString();
            //                    r[PromoSubdivisionTable.FLD_SUBDIVISION_CODE] = this.SelectedSubdivision[i].ToString();
            //                    dtsPromo.PromoSubdivision.Rows.Add(r);
            //                }
            //            }
            //        }
            //        else
            //        {
            //            comSys.UpdateRow(row,PromoTable.FLD_FSM_ID,this.FMID);
            //        }
            //    }

            //    IsSuccess = true;

            //    return IsSuccess;
            return true;
        }

        private void Upload() {
            try {
                if (ValidateFile(ctrlUpload.PostedFile)) {
                    //verify if a file has been set in the temp
                    if (System.IO.File.Exists(this.TempPath + this.TempFileName)) {
                        FileInfo f = new FileInfo(this.TempPath + this.TempFileName);
                        f.Delete();
                    }
                    //verify if a preview has been set in the temp
                    if (System.IO.File.Exists(this.TempPath + this.TempPreviewFileName)) {
                        FileInfo f = new FileInfo(this.TempPath + this.TempPreviewFileName);
                        f.Delete();
                    }

                    string[] file = ctrlUpload.PostedFile.FileName.Split('.');
                    this.FileExtension = file[file.Length - 1];
                    this.TempFileName = this.Session.SessionID + "-" + String.Format("{0:dd-MM-yyyy-HHmmss}", DateTime.Now) + "." + FileExtension;
                    this.TempPreviewFileName = this.Session.SessionID + "-" + String.Format("{0:dd-MM-yyyy-HHmmss}", DateTime.Now) + "." + DefaultFileExtension;

                    ctrlUpload.PostedFile.SaveAs(this.TempPath + this.TempFileName);
                    ctrlUpload.Dispose();
                    ctrlUpload = null;
                    GC.Collect();

                    //CREATE TEMP PREVIEW
                    Bitmap bmp = new Bitmap(this.TempPath + this.TempFileName);
                    bmp.SetResolution(72, 72);
                    bmp.Save(this.TempPath + this.TempPreviewFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    bmp.Dispose();
                    GC.Collect();

                    AdjustUI();
                }
                else {
                    this.Page.SetPageMessage("Error while uploading image : please select JPEG or TIF image");
                }
            }
            catch (Exception ex) {
                string message;
                if (ex.Message.Contains("GDI"))
                    message = " This picture is used by an other process";
                else
                    message = ex.Message;
                this.Page.SetPageMessage("Error while uploading image : " + message);
            }
        }

        private void AdjustUI() {
            //    /*
            //        * Use the style instead of visible because 
            //        * controls that are not visible are not rendered
            //    */

            //    //Show upload tool while it's new promo
            //    if((this.IsNewPromo))
            //    {
            //        trUpload.Style["display"] = "block";
            if (this.TempPreviewFileName != String.Empty) {
                this.imgDetail.ImageUrl = QSPForm.Common.QSPFormConfiguration.PromoLogoTempFolder + this.TempPreviewFileName;
            }
            //    }
            //    else
            //    {
            //        this.imgDetail.ImageUrl =QSPForm.Common.QSPFormConfiguration.PromoImagePreviewPath + this.PromoID + "." + this.DefaultFileExtension;
            //    }

            //    // Adjust FM Selector if User is Admin or IT
            //    if(this.Page.Role >= QSPForm.Business.AuthSystem.ROLE_ADMINISTRATOR)
            //    {
            //        trFmInfo.Style["display"] = "none";
            //        trFmEdit.Style["display"] = "block";
            //    }

            //    // Adjust for National
            //    if(this.National)
            //    {
            //        trSubdivision.Style["display"] = "none";
            //        trFM.Style["display"] = "none";
            //    }

            //    if(this.PromoID == 0)
            //    {
            this.lblID.Text = "New ID";
            //    }
            //    //			else
            //    //			{
            //    //				this.trFmInfo.Style["display"] = "block";
            //    //				this.trFmEdit.Style["display"] = "none";
            //    //			}
        }

        //private void AddJavascript()
        //{
        //    clsUtil.SetJScriptForOpenCalendar(hypLnkStartDate,txtStartDate);
        //    clsUtil.SetJScriptForOpenCalendar(hypLnkEndDate,txtEndDate);
        //    clsUtil.SetJScriptForOpenSelector(imgBtnSelectFM,txtFMID,txtFMName,QSPForm.Business.AppItem.FMSelector,0,0);
        //    this.chkNational.Attributes["onClick"] = "ShowHideSubdivision();";
        //}

        private bool ValidateFile(HttpPostedFile pFile) {
            bool isValid = true;
            ArrayList enableMimeType = new ArrayList();
            string[] mime = QSPForm.Common.QSPFormConfiguration.PromoImageExtensionFile.Split(',');
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
                Bitmap bmp = new Bitmap(this.TempPath + this.TempFileName);
                bmp.Save(Server.MapPath(QSPForm.Common.QSPFormConfiguration.PromoImagePath) +
                    this.DataSource.Rows[0][QSPForm.Common.DataDef.PromoTable.FLD_PKID] +
                    "." + this.FileExtension);

                //create duplicate at 72 dpi
                bmp.SetResolution(72, 72);
                bmp.Save(Server.MapPath(QSPForm.Common.QSPFormConfiguration.PromoImagePreviewPath) +
                    this.DataSource.Rows[0][QSPForm.Common.DataDef.PromoTable.FLD_PKID] +
                    "." + this.DefaultFileExtension, System.Drawing.Imaging.ImageFormat.Jpeg);

                bmp.Dispose();
                GC.Collect();

                FileInfo f = new FileInfo(this.TempPath + this.TempFileName);
                f.Delete();
            }
            else {
                throw new Exception("Error while saving image");
            }
        }
    }
}