using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.BusinessTaskTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for CustomerInfo.
    /// </summary>
    public partial class BusinessTaskForm : BaseWebUserControl {
        protected dataDef dtBusinessTask = new dataDef();
        private int c_ParentID = 0;
        private int c_BaseParentID = 0;
        protected System.Web.UI.WebControls.ValidationSummary ValSum;
        private QSPForm.Business.BusinessTaskSystem bizSys = new QSPForm.Business.BusinessTaskSystem();
        protected DataTable tblTask = new DataTable();
        protected RoleTable tblRole = new RoleTable();
        protected System.Web.UI.HtmlControls.HtmlTable tblAddButton;
        bool c_IsReadOnly = false;
        bool c_HideButton = false;
        protected DataView DVBizTask;
        protected DataView DVParentBizTask;
        protected DataView DVRole;
        protected dataDef dtParentBusinessTask;
        private CommonUtility clsUtil = new CommonUtility();

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here					
            this.hidFormID.Value = this.ParentID.ToString();
            AddJavascript();
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
            this.dtLstBizTask.ItemCreated += new System.Web.UI.WebControls.DataListItemEventHandler(this.dtLstBizTask_ItemCreated);
            this.dtLstBizTask.DeleteCommand += new System.Web.UI.WebControls.DataListCommandEventHandler(this.dtLstBizTask_DeleteCommand);
            this.dtLstBizTask.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.dtLstBizTask_ItemDataBound);
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
                FillDataListJavascript();
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
            //dtBusinessTask = bizSys.SelectAllByEntityID(c_ParentType, c_ParentID);
        }

        public void BindForm() {
            FillDataTableForDropDownList();
            DVBizTask = new DataView(dtBusinessTask);
            dtLstBizTask.DataBind();
        }

        public int Count {
            get {
                return this.dtLstBizTask.Items.Count;

            }
        }

        public BusinessTaskTable DataSource {
            get {
                return dtBusinessTask;

            }
            set {
                dtBusinessTask = value;
            }
        }

        public RepeatDirection RepeatDirection {
            get {
                return dtLstBizTask.RepeatDirection;
            }
            set {
                dtLstBizTask.RepeatDirection = value;
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

        public int BaseParentID {
            get {
                return c_BaseParentID;
            }
            set {
                c_BaseParentID = value;
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

        protected int getSelectedIndex(DataTable dt, String sValue) {
            int iIndex = -1;
            int iCount = 0;
            try {
                if (sValue != "") {
                    foreach (DataRow row in dt.Rows) {
                        if (row[0].ToString() == sValue) {
                            iIndex = iCount;
                            break;
                        }
                        iCount++;
                    }
                    //because of the null value
                    return iIndex;
                }
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
            return iIndex;
        }

        protected int getSelectedIndex(DataView dv, String sValue) {
            int iIndex = -1;
            int iCount = 0;
            try {
                foreach (DataRowView drwv in dv) {
                    if (drwv[0].ToString() == sValue) {
                        iIndex = iCount;
                        break;
                    }
                    iCount++;
                }
                //because of the null value
                return iIndex;
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
            return iIndex;
        }

        private void FillDataTableForDropDownList() {
            try {
                QSPForm.Business.TaskSystem taskSys = new QSPForm.Business.TaskSystem();
                //Task				
                tblTask = taskSys.SelectAll();
                dtParentBusinessTask = (dataDef)dtBusinessTask.Copy();
                DataRow newRow = dtParentBusinessTask.NewRow();
                newRow[dataDef.FLD_NAME] = "<None>";
                dtParentBusinessTask.Rows.InsertAt(newRow, 0);
                DVParentBizTask = new DataView(dtParentBusinessTask);
                QSPForm.Business.AuthSystem authSys = new QSPForm.Business.AuthSystem();
                //Role 				
                tblRole = authSys.SelectAllRole();
                DVRole = new DataView(tblRole);
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        protected DataView FilterDataViewForParentBizTask(DataView dv, string sBizTaskID) {
            try {
                //To Filter the DropDownList of ParentBiz task
                //To don't include hitself
                dv.RowFilter = dataDef.FLD_PKID + " <> " + sBizTaskID;
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }

            return dv;
        }

        public bool UpdateDataSource() {
            bool blnValid = false;

            try {
                int iCounter = 0;
                //'We save everything that is possible				
                for (iCounter = 0; iCounter <= dtLstBizTask.Items.Count - 1; iCounter++) {
                    DataListItem dlstItem;
                    dlstItem = dtLstBizTask.Items[iCounter];
                    DataView dv = new DataView(dtBusinessTask);
                    int ID = Convert.ToInt32(dtLstBizTask.DataKeys[iCounter]);
                    dv.Sort = dataDef.FLD_PKID;
                    int iIndex = dv.Find(ID);
                    if (iIndex != -1) {
                        DataRow row = dv[iIndex].Row;
                        if (row.RowState != DataRowState.Deleted) {

                            //'Table Mapping                      
                            row[dataDef.FLD_FORM_ID] = c_ParentID;
                            row[dataDef.FLD_NAME] = ((TextBox)dlstItem.FindControl("txtName")).Text;
                            row[dataDef.FLD_TASK_ID] = ((DropDownList)dlstItem.FindControl("ddlTask")).SelectedValue;
                            row[dataDef.FLD_TASK_NAME] = ((DropDownList)dlstItem.FindControl("ddlTask")).SelectedItem.Text;
                            row[dataDef.FLD_MESSAGE] = ((TextBox)dlstItem.FindControl("txtMessage")).Text;
                            row[dataDef.FLD_EXPRESSION] = ((TextBox)dlstItem.FindControl("txtExpression")).Text;
                            row[dataDef.FLD_DESCRIPTION] = ((TextBox)dlstItem.FindControl("txtDescription")).Text;
                            row[dataDef.FLD_ASSIGNMENT_TYPE_ID] = ((DropDownList)dlstItem.FindControl("ddlAssignmentType")).SelectedValue;
                            //Assignment Type
                            int AssignType = Convert.ToInt32(row[dataDef.FLD_ASSIGNMENT_TYPE_ID]);
                            if (AssignType == QSPForm.Common.BizTask_AssignmentType.SPECIFIC_USER) {
                                TextBox txtCtl = ((TextBox)dlstItem.FindControl("txtAssignedUserID"));
                                if (txtCtl.Text.Trim().Length > 0)
                                    row[dataDef.FLD_ASSIGNED_USER_ID] = ((TextBox)dlstItem.FindControl("txtAssignedUserID")).Text;
                                else
                                    row[dataDef.FLD_ASSIGNED_USER_ID] = System.DBNull.Value;
                                row[dataDef.FLD_ASSIGNED_ROLE_ID] = System.DBNull.Value;
                            }
                            else if (AssignType == QSPForm.Common.BizTask_AssignmentType.CURRENT_USER) {
                                row[dataDef.FLD_ASSIGNED_USER_ID] = System.DBNull.Value;
                                row[dataDef.FLD_ASSIGNED_ROLE_ID] = System.DBNull.Value;
                            }
                            else if (AssignType == QSPForm.Common.BizTask_AssignmentType.SPECIFIC_ROLE) {
                                row[dataDef.FLD_ASSIGNED_ROLE_ID] = ((DropDownList)dlstItem.FindControl("ddlRole")).SelectedValue;
                                row[dataDef.FLD_ASSIGNED_USER_ID] = System.DBNull.Value;
                            }

                            DropDownList ddl = ((DropDownList)dlstItem.FindControl("ddlBusinessTask"));
                            if (ddl.SelectedIndex > 0)
                                row[dataDef.FLD_PARENT_ID] = ddl.SelectedValue;
                            else
                                row[dataDef.FLD_PARENT_ID] = System.DBNull.Value;

                            if (row.RowState == DataRowState.Added)
                                row[dataDef.FLD_CREATE_USER_ID] = Page.UserID;
                            else
                                row[dataDef.FLD_UPDATE_USER_ID] = Page.UserID;
                        }
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

        private void dtLstBizTask_DeleteCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e) {
            Delete(e.Item.ItemIndex);
        }

        public void Delete(int iItemIndex) {
            UpdateDataSource();
            DataView dv = new DataView(dtBusinessTask);
            int ID = Convert.ToInt32(dtLstBizTask.DataKeys[iItemIndex]);
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
            dtBusinessTask.Rows.Add(dtBusinessTask.NewRow());
            BindForm();
        }

        private void dtLstBizTask_ItemCreated(object sender, System.Web.UI.WebControls.DataListItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                ImageButton imgBtnDelete = (ImageButton)e.Item.FindControl("imgBtnDelete");
                if (imgBtnDelete != null) {
                    imgBtnDelete.Visible = (!c_HideButton);
                }
            }
        }

        private void dtLstBizTask_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e) {
            CommonUtility clsUtil = new CommonUtility();
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {

                ImageButton imgBtnSelectUser = (ImageButton)e.Item.FindControl("imgBtnSelectUser");
                if (imgBtnSelectUser != null) {
                    TextBox txtAssignedUserID = (TextBox)e.Item.FindControl("txtAssignedUserID");

                    TextBox txtAssignedUserName = (TextBox)e.Item.FindControl("txtAssignedUserName");

                    //  clsUtil.SetJScriptForOpenSelector(imgBtnSelectUser,txtAssignedUserID,txtAssignedUserName,QSPForm.Business.AppItem.UserSelector,0,0);

                    clsUtil.SetJScriptForOpenSelector(imgBtnSelectUser, txtAssignedUserID, txtAssignedUserName, "UserSelector.aspx", "UserSelector", 0, 0, null);
                }
            }
        }

        private void FillDataListJavascript() {
            ImageButton exBuilder;
            TextBox txtEx;
            foreach (DataListItem e in this.dtLstBizTask.Items) {
                if ((e.ItemType == ListItemType.Item) || (e.ItemType == ListItemType.AlternatingItem)) {
                    /* Expression builder */
                    exBuilder = (ImageButton)e.FindControl("imgBtnExBuilder");
                    txtEx = (TextBox)e.FindControl("txtExpression");
                    clsUtil.SetJScriptForOpenSelector(exBuilder, txtEx, QSPForm.Business.AppItem.ExpressionBuilder, 0, 0, "&formID=" + this.ParentID);

                    // clsUtil.SetJScriptForOpenSelector(exBuilder, txtEx, txtEx, "ExpressionBuilder.aspx", 0, 0, "&formID=" + this.ParentID);

                    //DropDown Assignement type Management
                    DropDownList ddlAssignmentType = (DropDownList)e.FindControl("ddlAssignmentType");
                    HtmlTableRow UserSelectorRow = (HtmlTableRow)e.FindControl("UserSelectorRow");
                    HtmlTableRow RoleSelectorRow = (HtmlTableRow)e.FindControl("RoleSelectorRow");
                    string JScript = "ToggleAssigneTo('" + ddlAssignmentType.ClientID + "', '" + UserSelectorRow.ClientID + "', '" + RoleSelectorRow.ClientID + "')";
                    ddlAssignmentType.Attributes.Add("OnChange", JScript);
                    if (ddlAssignmentType.SelectedValue == "3") {
                        UserSelectorRow.Style["display"] = "none";
                        RoleSelectorRow.Style["display"] = "block";
                    }
                    else {
                        UserSelectorRow.Style["display"] = "block";
                        RoleSelectorRow.Style["display"] = "none";
                    }
                }
            }
        }

        private void AddJavascript() {
            System.Text.StringBuilder JScript = new System.Text.StringBuilder();
            JScript.Append("<script>");
            JScript.Append("function ToggleAssigneTo(AssignSelectorRefCtrl,UserSelectorRowRefCtrl,RoleSelectorRowRefCtrl) {\n");
            JScript.Append("  var AssignSelector = document.getElementById(AssignSelectorRefCtrl);\n");
            JScript.Append("  var UserSelectorRow = document.getElementById(UserSelectorRowRefCtrl);\n");
            JScript.Append("  var RoleSelectorRow = document.getElementById(RoleSelectorRowRefCtrl);\n");
            JScript.Append("  var newvalue = AssignSelector.options[AssignSelector.selectedIndex].value;");
            JScript.Append("   if (newvalue == '3') {\n");
            JScript.Append("		RoleSelectorRow.style.display = 'block';\n");
            JScript.Append("		UserSelectorRow.style.display = 'none';\n");
            JScript.Append("    } else {\n");
            JScript.Append("		RoleSelectorRow.style.display = 'none';\n");
            JScript.Append("		UserSelectorRow.style.display = 'block';\n");
            JScript.Append("	}\n");
            JScript.Append("}");
            JScript.Append("</script>");
            this.Page.RegisterClientScriptBlock("ToggleAssigneTo", JScript.ToString());
        }
    }
}