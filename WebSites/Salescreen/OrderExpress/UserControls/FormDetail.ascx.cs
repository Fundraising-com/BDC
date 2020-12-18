//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\Stub_FormDetail_ascx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'FormDetail.ascx' was also modified to refer to the new class name.
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
using dataDef = QSPForm.Common.DataDef.FormData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    /// Summary description for FormDetail.
    /// </summary>
    public partial class FormDetail : BaseFormDetail {
        private int c_FormID;
        private const string FORM_DATA = "FormData";
        protected dataDef dtsForm;
        protected int EntityTypeID = 0;

        protected void Page_Load(object sender, System.EventArgs e) {
            try {
                // Put user code to initialize the page here	
                if (!IsPostBack) {
                    if (Request[FORM_ID] != null) {
                        c_FormID = Convert.ToInt32(Request[FORM_ID].ToString());
                    }
                    else {
                        c_FormID = 0;
                    }
                    imgBtnDelete.Attributes.Add("onclick", "return confirm('Are you sure that you want to delete this campaign ?');");
                }

                if (ViewState[FORM_ID] != null) {
                    c_FormID = Int32.Parse(ViewState[FORM_ID].ToString());
                }
                ViewState[FORM_ID] = c_FormID;

                //FormDetailForm.FormID  = c_FormID;
                //BusinessRuleList.ParentID  = c_FormID;
                //BusinessExceptionList.ParentID  = c_FormID;
                //BusinessTaskList.ParentID  = c_FormID;
                //VersionList.FormID = c_FormID;
                //FormSectionListCtrl.ParentID = c_FormID;
                //FormDeliveryMethodListCtrl.ParentID = c_FormID;
                //FormOrderTypeListCtrl.ParentID = c_FormID;
                //FormProfitRateListCtrl.ParentID = c_FormID;

                if (!IsPostBack) {
                    LoadDataSet();

                    AssignForm();
                    BindForm();
                    ////Tab 1 Form
                    //FormDetailForm.DataSource = dtsForm.Form;
                    //FormDetailForm.BindForm();
                    ////Tab 2 Biz rule	
                    //BusinessRuleList.DataSource = dtsForm.BusinessRule;		
                    //BusinessRuleList.EntityTypeID = FormDetailForm.EntityTypeID;
                    //BusinessRuleList.BindForm();
                    ////Tab 3 Biz Exception	
                    //BusinessExceptionList.DataSource = dtsForm.BusinessException;				
                    //BusinessExceptionList.BindForm();
                    ////Tab 4 Biz Task	
                    //BusinessTaskList.DataSource = dtsForm.BusinessTask;		
                    //BusinessTaskList.BaseParentID = FormDetailForm.BaseFormID;
                    //BusinessTaskList.BindForm();
                    ////Tab 5 Form Section	
                    //FormSectionListCtrl.DataSource = dtsForm.FormSection;
                    //FormSectionListCtrl.BindForm();
                    ////Tab 6 Delivery Method 
                    //FormDeliveryMethodListCtrl.DataSource = dtsForm.FormDeliveryMethod;
                    //FormDeliveryMethodListCtrl.BindForm();
                    ////Tab 6 Order type
                    //FormOrderTypeListCtrl.DataSource = dtsForm.FormOrderType;
                    //FormOrderTypeListCtrl.BindForm();
                    ////Tab 6 Profit Rate
                    //FormProfitRateListCtrl.DataSource = dtsForm.FormProfitRate;
                    //FormProfitRateListCtrl.BindForm();

                }
                else {
                    //For each postback, the page (the higher in the hierarchy)
                    //is in charge to set all children's datasource 
                    dtsForm = (FormData)this.ViewState[FORM_DATA];
                    AssignForm();
                    //FormDetailForm.DataSource = dtsForm.Form;
                    //BusinessRuleList.EntityTypeID = FormDetailForm.EntityTypeID;
                    //BusinessRuleList.DataSource = dtsForm.BusinessRule;
                    //BusinessExceptionList.DataSource = dtsForm.BusinessException;
                    //BusinessTaskList.BaseParentID = FormDetailForm.BaseFormID;
                    //BusinessTaskList.DataSource = dtsForm.BusinessTask;	
                    //FormSectionListCtrl.DataSource = dtsForm.FormSection;
                    //FormDeliveryMethodListCtrl.DataSource = dtsForm.FormDeliveryMethod;
                    //FormOrderTypeListCtrl.DataSource = dtsForm.FormOrderType;
                    //FormProfitRateListCtrl.DataSource = dtsForm.FormProfitRate;
                }
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
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
        }
        #endregion

        //		public int FormID
        //		{
        override public int FormID {
            get {
                return c_FormID;
            }
            set {
                c_FormID = value;
                ViewState[FORM_ID] = c_FormID;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            bool IsEnabled = false;
            if (dtsForm.Form.Rows.Count > 0) {
                DataRow frmRow = dtsForm.Form.Rows[0];
                lblFormTitle.Text = "Form :  " + frmRow[FormTable.FLD_FORM_CODE].ToString() + " - " + dtsForm.Form.Rows[0][FormTable.FLD_FORM_NAME].ToString();

                if (!frmRow.IsNull(FormTable.FLD_ENABLED))
                    IsEnabled = Convert.ToBoolean(frmRow[FormTable.FLD_ENABLED]);
                if (!frmRow.IsNull(FormTable.FLD_IMAGE_URL)) {
                    imgForm.ImageUrl = "~/" + frmRow[FormTable.FLD_IMAGE_URL].ToString();
                }
                else
                    imgForm.Visible = false;
            }
            this.ViewState[FORM_DATA] = dtsForm;
            imgBtnSave.Visible = IsEnabled;
        }

        private void LoadDataSet() {
            if (this.c_FormID > 0) {
                QSPForm.Business.FormSystem bizSys = new QSPForm.Business.FormSystem();
                dtsForm = bizSys.SelectAllDetail(c_FormID);
            }
            else {
                //Insert a new row
                dtsForm = new FormData();
                DataRow newRow = dtsForm.Form.NewRow();
                newRow[FormTable.FLD_FORM_NAME] = "New Form";
                dtsForm.Form.Rows.Add(newRow);
            }
        }

        private void imgBtnDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
        }

        private void imgBtnSave_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            try {
                Boolean blnValid = true;
                blnValid = FormDetailForm.IsValid();
                if (!blnValid) {
                    TbStrp_Form.SelectedIndex = 0;
                    return;
                }
                blnValid = BusinessRuleList.IsValid();
                if (!blnValid) {
                    TbStrp_Form.SelectedIndex = 1;
                    return;
                }
                blnValid = BusinessExceptionList.IsValid();
                if (!blnValid) {
                    TbStrp_Form.SelectedIndex = 2;
                    return;
                }
                blnValid = BusinessTaskList.IsValid();
                if (!blnValid) {
                    TbStrp_Form.SelectedIndex = 3;
                    return;
                }
                blnValid = FormSectionListCtrl.IsValid();
                if (!blnValid) {
                    TbStrp_Form.SelectedIndex = 4;
                    return;
                }

                blnValid = FormDetailForm.UpdateDataSource();
                if (!blnValid) {
                    return;
                }
                blnValid = BusinessRuleList.UpdateDataSource();
                if (!blnValid) {
                    return;
                }
                blnValid = BusinessExceptionList.UpdateDataSource();
                if (!blnValid) {
                    return;
                }
                blnValid = BusinessTaskList.UpdateDataSource();
                if (!blnValid) {
                    return;
                }
                blnValid = FormSectionListCtrl.UpdateDataSource();
                if (!blnValid) {
                    return;
                }

                blnValid = FormDeliveryMethodListCtrl.UpdateDataSource();
                if (!blnValid) {
                    return;
                }

                blnValid = FormOrderTypeListCtrl.UpdateDataSource();
                if (!blnValid) {
                    return;
                }

                blnValid = FormProfitRateListCtrl.UpdateDataSource();
                if (!blnValid) {
                    return;
                }

                QSPForm.Business.FormSystem formSys = new QSPForm.Business.FormSystem();
                blnValid = formSys.UpdateAllDetail(dtsForm, this.Page.UserID);
                if (blnValid) {
                    //Get the NewID
                    DataRow frmRow = dtsForm.Form.Rows[0];
                    int iFormID = Convert.ToInt32(frmRow[FormTable.FLD_PKID]);
                    //Reload completely the Form
                    //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.Form_Detail, FormDetail.FORM_ID, iFormID.ToString());
                    string url = "~/FormDetail.aspx?" + FormDetail.FORM_ID + "=" + iFormID.ToString();
                    Response.Redirect(url, false);
                }
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        protected void imgBtnCreateNewVersion_Click(object sender, ImageClickEventArgs e) {
            this.c_FormID = 0;
            QSPForm.Business.FormSystem frmSys = new QSPForm.Business.FormSystem();

            dtsForm = frmSys.InitializeNewVersion(dtsForm, this.Page.UserID);
            AssignForm();
            BindForm();
        }

        protected void imgBtnCopyAsNewForm_Click(object sender, ImageClickEventArgs e) {
            this.c_FormID = 0;
            QSPForm.Business.FormSystem frmSys = new QSPForm.Business.FormSystem();

            dtsForm = frmSys.InitializeFormByModel(dtsForm, this.Page.UserID);
            AssignForm();
            BindForm();
        }

        private void AssignForm() {
            FormDetailForm.FormID = c_FormID;
            BusinessRuleList.ParentID = c_FormID;
            BusinessExceptionList.ParentID = c_FormID;
            BusinessTaskList.ParentID = c_FormID;
            VersionList.FormID = c_FormID;
            FormSectionListCtrl.ParentID = c_FormID;
            FormDeliveryMethodListCtrl.ParentID = c_FormID;
            FormOrderTypeListCtrl.ParentID = c_FormID;
            FormProfitRateListCtrl.ParentID = c_FormID;

            //Tab 1 Form
            FormDetailForm.DataSource = dtsForm.Form;
            FormDetailForm.BindForm();
            //Tab 2 Biz rule	
            BusinessRuleList.DataSource = dtsForm.BusinessRule;
            BusinessRuleList.EntityTypeID = FormDetailForm.EntityTypeID;
            BusinessRuleList.BindForm();
            //Tab 3 Biz Exception	
            BusinessExceptionList.DataSource = dtsForm.BusinessException;
            BusinessExceptionList.BindForm();
            //Tab 4 Biz Task	
            BusinessTaskList.DataSource = dtsForm.BusinessTask;
            BusinessTaskList.BaseParentID = FormDetailForm.BaseFormID;
            BusinessTaskList.BindForm();
            //Tab 5 Form Section	
            FormSectionListCtrl.DataSource = dtsForm.FormSection;
            FormSectionListCtrl.BindForm();
            //Tab 6 Delivery Method 
            FormDeliveryMethodListCtrl.DataSource = dtsForm.FormDeliveryMethod;
            FormDeliveryMethodListCtrl.BindForm();
            //Tab 6 Order type
            FormOrderTypeListCtrl.DataSource = dtsForm.FormOrderType;
            FormOrderTypeListCtrl.BindForm();
            //Tab 6 Profit Rate
            FormProfitRateListCtrl.DataSource = dtsForm.FormProfitRate;
            FormProfitRateListCtrl.BindForm();
        }

        public override void BindForm() {
            //Tab 1 Form
            FormDetailForm.BindForm();
            //Tab 2 Biz rule	
            BusinessRuleList.BindForm();
            //Tab 3 Biz Exception	
            BusinessExceptionList.BindForm();
            //Tab 4 Biz Task	
            BusinessTaskList.BindForm();
            //Tab 5 Form Section	
            FormSectionListCtrl.BindForm();
            //Tab 6 Delivery Method 
            FormDeliveryMethodListCtrl.BindForm();
            //Tab 6 Order type
            FormOrderTypeListCtrl.BindForm();
            //Tab 6 Profit Rate
            FormProfitRateListCtrl.BindForm();
        }
    }
}