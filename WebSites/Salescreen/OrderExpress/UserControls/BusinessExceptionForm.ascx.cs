using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.BusinessExceptionTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for CustomerInfo.
    /// </summary>
    public partial class BusinessExceptionForm : BaseWebUserControl {
        protected dataDef dtBusinessException = new dataDef();
        private int c_ParentID = 0;
        protected System.Web.UI.WebControls.ValidationSummary ValSum;
        private QSPForm.Business.BusinessExceptionSystem bizSys = new QSPForm.Business.BusinessExceptionSystem();
        protected DataTable tblExceptionType = new DataTable();
        protected DataTable tblEntityType = new DataTable();
        protected DataTable tblAppItem = new DataTable();
        protected System.Web.UI.HtmlControls.HtmlTable tblAddButton;
        protected DataTable tblFormSectionType = new DataTable();
        protected DataView DVFormSectionType;
        bool c_IsReadOnly = false;
        bool c_HideButton = false;
        protected DataView DVBizException;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here								
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.imgBtnAddNew.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnAddNew_Click);
            this.dtLstBizException.ItemCreated += new System.Web.UI.WebControls.DataListItemEventHandler(this.dtLstBizException_ItemCreated);
            this.dtLstBizException.DeleteCommand += new System.Web.UI.WebControls.DataListCommandEventHandler(this.dtLstBizException_DeleteCommand);
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);
        }
        #endregion

        protected void Page_DataBinding(object sender, System.EventArgs e) {
            try {
                //retreive data detail item for db
                //Init DataList								
                BindForm();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            try {
                imgBtnAddNew.Visible = !c_HideButton;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public void LoadDataSet() {
            // Create a new dataset to hold the records returned from the call to FillDataSet.
            // A temporary dataset is used because filling the existing dataset would
            // require the databindings to be rebound.

            // Attempt to fill the temporary dataset.
            //dtBusinessException = bizSys.SelectAllByEntityID(c_ParentType, c_ParentID);
        }

        public void BindForm() {
            FillDataTableForDropDownList();
            DVBizException = new DataView(dtBusinessException);
            dtLstBizException.DataBind();
        }

        public int Count {
            get {
                return this.dtLstBizException.Items.Count;
            }
        }

        public BusinessExceptionTable DataSource {
            get {
                return dtBusinessException;

            }
            set {
                dtBusinessException = value;
            }
        }

        public RepeatDirection RepeatDirection {
            get {
                return dtLstBizException.RepeatDirection;
            }
            set {
                dtLstBizException.RepeatDirection = value;
            }
        }

        public int ParentID {
            get {
                return c_ParentID;
            }
            set {
                c_ParentID = value;
            }
        }

        public bool IsReadOnly {
            get {
                return c_IsReadOnly;
            }
            set {
                c_IsReadOnly = value;
            }
        }

        public bool HideButton {
            get {
                return c_HideButton;
            }
            set {
                c_HideButton = value;
            }
        }

        private void FillDataTableForDropDownList() {
            try {
                QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
                //Exception Type				
                tblExceptionType = comSys.SelectAllExceptionType();

                QSPForm.Business.ContentManagerSystem cmSys = new QSPForm.Business.ContentManagerSystem();
                //Menu Item
                QSPForm.Common.DataDef.AppItemData dts = cmSys.SelectAllMenuItemByEntityTypeID(QSPForm.Common.EntityType.TYPE_ORDER_BILLING);
                tblAppItem = dts.AppItem;
                DataRow row = tblAppItem.NewRow();
                row[QSPForm.Common.DataDef.AppItemTable.FLD_NAME] = "Not Specified";
                tblAppItem.Rows.InsertAt(row, 0);

                //Entity Type
                tblEntityType = comSys.SelectAllEntityType();

                //Section Type
                tblFormSectionType = comSys.SelectAllFormSectionType();
                DataRow newRow = tblFormSectionType.NewRow();
                newRow[0] = 0;
                newRow[1] = "---SELECT---";
                tblFormSectionType.Rows.InsertAt(newRow, 0);
                DVFormSectionType = new DataView(tblFormSectionType);
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        public bool UpdateDataSource() {
            bool blnValid = false;

            try {
                DataView DVBiz = new DataView(dtBusinessException);
                DVBiz.Sort = dataDef.FLD_PKID;
                int iCounter = 0;
                //'We save everything that is possible				
                for (iCounter = 0; iCounter <= dtLstBizException.Items.Count - 1; iCounter++) {
                    DataListItem dlstItem;
                    dlstItem = dtLstBizException.Items[iCounter];
                    int ID = Convert.ToInt32(dtLstBizException.DataKeys[iCounter]);
                    int iIndex = DVBiz.Find(ID);
                    if (iIndex > -1) {
                        DataRow row = DVBiz[iIndex].Row;

                        //'Table Mapping                      
                        row[dataDef.FLD_FORM_ID] = c_ParentID;
                        row[dataDef.FLD_NAME] = ((TextBox)dlstItem.FindControl("txtName")).Text;
                        row[dataDef.FLD_EXCEPTION_TYPE_ID] = ((DropDownList)dlstItem.FindControl("ddlExceptionType")).SelectedValue;
                        DropDownList ddlAppItem = ((DropDownList)dlstItem.FindControl("ddlAppItem"));
                        //Application Item (Step 1, 2, ...)
                        if (ddlAppItem.SelectedIndex > 0)
                            row[dataDef.FLD_APP_ITEM_ID] = ddlAppItem.SelectedValue;
                        else
                            row[dataDef.FLD_APP_ITEM_ID] = System.DBNull.Value;

                        row[dataDef.FLD_EXPRESSION] = ((TextBox)dlstItem.FindControl("txtExpression")).Text;
                        row[dataDef.FLD_FEES_VALUE_EXPRESSION] = ((TextBox)dlstItem.FindControl("txtFeesValueExpression")).Text;
                        row[dataDef.FLD_WARNING_MESSAGE] = ((TextBox)dlstItem.FindControl("txtWarningMessage")).Text;
                        row[dataDef.FLD_MESSAGE] = ((TextBox)dlstItem.FindControl("txtMessage")).Text;
                        //Entity Type
                        DropDownList ddlEntityType = ((DropDownList)dlstItem.FindControl("ddlEntityType"));
                        if (ddlEntityType.SelectedIndex > 0)
                            row[dataDef.FLD_ENTITY_TYPE_ID] = ddlEntityType.SelectedValue;
                        else
                            row[dataDef.FLD_ENTITY_TYPE_ID] = System.DBNull.Value;
                        //Form Section Type
                        CommonUtility clsUtil = new CommonUtility();
                        DropDownList ddl = ((DropDownList)dlstItem.FindControl("ddlFormSectionType"));
                        string sValue = "";
                        if (ddl.SelectedIndex > 0)
                            sValue = ddl.SelectedValue;
                        else
                            sValue = "";
                        clsUtil.UpdateRow(row, dataDef.FLD_FORM_SECTION_TYPE_ID, sValue);

                        //Form Section Number
                        TextBox txt;
                        txt = ((TextBox)dlstItem.FindControl("txtSectionNumber"));
                        sValue = txt.Text;
                        clsUtil.UpdateRow(row, dataDef.FLD_FORM_SECTION_NUMBER, sValue);

                        if (row.RowState == DataRowState.Added)
                            row[dataDef.FLD_CREATE_USER_ID] = Page.UserID;
                        else
                            row[dataDef.FLD_UPDATE_USER_ID] = Page.UserID;
                    }
                }

                blnValid = true;
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }

            return blnValid;
        }

        private void imgBtnAddNew_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            AddNew();
        }

        private void dtLstBizException_DeleteCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e) {
            Delete(e.Item.ItemIndex);
        }

        public void Delete(int iItemIndex) {
            UpdateDataSource();
            DataView dv = new DataView(dtBusinessException);
            int ID = Convert.ToInt32(dtLstBizException.DataKeys[iItemIndex]);
            dv.Sort = dataDef.FLD_PKID;
            int iIndex = dv.Find(ID);
            if (iIndex != -1) {
                DataRow row = dv[iIndex].Row;
                if (row.RowState != DataRowState.Deleted) {
                    if (row.RowState != DataRowState.Added)
                        row[dataDef.FLD_UPDATE_USER_ID] = Page.UserID;

                    row.Delete();
                }
            }
            BindForm();
        }

        public void AddNew() {
            UpdateDataSource();
            dtBusinessException.Rows.Add(dtBusinessException.NewRow());
            BindForm();
        }

        private void dtLstBizException_ItemCreated(object sender, System.Web.UI.WebControls.DataListItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {

                ImageButton imgBtnDelete = (ImageButton)e.Item.FindControl("imgBtnDelete");
                if (imgBtnDelete != null) {
                    imgBtnDelete.Visible = (!c_HideButton);

                }
            }
        }
    }
}