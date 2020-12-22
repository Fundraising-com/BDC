using System;
using System.Web;
using QSPForm.Common.DataDef;
using System.Data;
using System.Web.UI.WebControls;
using System.Security.Principal;
using System.Web.Security;
using System.Web.SessionState;

namespace QSP.OrderExpress.Web.Code {
    /// <summary>Base page for Web Form pages in QSPForm_Web</summary>
    /// <remarks>
    ///		Inherit from BasePage
    ///		We used this class to manage common functionnality
    ///		for DataGrid by example
    ///	</remarks>
    public class BaseWebFormGrid : BaseWebForm {
        private QSP.WebControl.DataGridControl.SortedDataGrid dtgMain;
        private DataView dvMain;
        protected System.Web.UI.WebControls.Label lblCurrentIndex;
        protected System.Web.UI.WebControls.Label lblTotal;
        public event System.Web.UI.WebControls.DataGridSortCommandEventHandler SortCommand;
        public event System.Web.UI.WebControls.DataGridCommandEventHandler DeleteCommand;
        public event System.Web.UI.WebControls.DataGridCommandEventHandler UpdateCommand;
        public event System.Web.UI.WebControls.DataGridCommandEventHandler CancelCommand;
        public event System.Web.UI.WebControls.DataGridCommandEventHandler EditCommand;
        public event System.Web.UI.WebControls.DataGridItemEventHandler ItemCreated;
        public event System.Web.UI.WebControls.DataGridPageChangedEventHandler PageIndexChanged;
        public event System.EventHandler AddNew;
        protected BaseSearchModule QSPFormSearchModule;
        protected BaseToolBar QSPFormToolBar;
        private int newID = 0;
        private string defaultSort = "";

        public string DefaultSort {
            get {
                return defaultSort;
            }
            set {
                defaultSort = value;
            }
        }

        public DataView DataSource {
            get {
                return dvMain;
            }
            set {
                dvMain = value;
            }
        }

        public QSP.WebControl.DataGridControl.SortedDataGrid MainDataGrid {
            get {
                return dtgMain;
            }
            set {
                dtgMain = value;
            }
        }

        public int NewID {
            get {
                return newID;
            }
            set {
                newID = value;
            }
        }

        //Virtual method to override in child class
        protected virtual void BindGrid() {
            try {
                LoadDataSourceGrid();
                //Prepare the DataSource of DropDownList when 
                //databind the grid 
                if (this.dtgMain.EditItemIndex != -1) {
                    LoadDataForDropDownInDG();
                }
                this.dtgMain.DataBind();
            }
            catch (Exception ex) {
                SetPageError(ex);
            }
        }

        protected virtual void LoadDataForDropDownInDG() {
        }

        protected virtual void FillFilter() {
        }

        protected virtual int GetIDFromGrid(DataGridItem e) {
            return 0;
        }

        protected virtual bool DeleteRowGrid(int iIndex) {
            return true;
        }

        protected virtual bool UpdateRowGrid() {
            return true;
        }

        protected virtual void LoadDataSourceGrid() {
        }

        protected override void OnLoad(EventArgs e) {


            if (!IsPostBack) {
                //Initt Value for Sort and filter
                this.dtgMain.SetDefaultSort(DefaultSort, "", 0);
                if (QSPFormSearchModule != null) {
                    QSPFormSearchModule.DataBind();
                }
                FillFilter();
                BindGrid();
            }

            base.OnLoad(e);
        }

        protected override void OnPreRender(EventArgs e) {
            if (dtgMain != null) {
                //SetPage text
                if (lblCurrentIndex != null) {
                    this.dtgMain.SetPageIndexText(this.lblCurrentIndex);
                }
                //				if (QSPFormSearchModule != null)
                //					QSPFormSearchModule.Enabled = (dtgMain.EditItemIndex == -1);
                //				dtgMain.Columns[0].Visible = (dtgMain.EditItemIndex == -1);
            }

            base.OnPreRender(e);
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            try {
                if (dtgMain != null) {
                    this.dtgMain.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dtgMain_ItemCreated);
                    this.dtgMain.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgMain_PageIndexChanged);
                    this.dtgMain.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgMain_CancelCommand);
                    this.dtgMain.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgMain_EditCommand);
                    this.dtgMain.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgMain_UpdateCommand);
                    this.dtgMain.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgMain_DeleteCommand);
                    this.dtgMain.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dtgMain_SortCommand);
                }
                if (QSPFormSearchModule != null) {
                    this.QSPFormSearchModule.OnSearch += new SearchModuleEventHandler(this.QSPFormSearchModule_ONClick);
                }
                //				if (QSPFormToolBar != null)
                //				{
                //					this.QSPFormToolBar.AddNewClick += new EventHandler(QSPFormToolBar_AddNewClick);
                //				}
            }
            catch {
            }
        }
        #endregion

        protected override void OnDataBinding(EventArgs e) {
            base.OnDataBinding(e);
        }

        protected override void OnMenuChange(System.ComponentModel.CancelEventArgs e) {
            try {
                bool blnValid = false;

                if (dtgMain != null) {
                    if (IsFormChange) {
                        Page.Validate();
                        if (Page.IsValid) {
                            blnValid = UpdateRowGrid();
                            //to don't reupdate in the inherit class
                            IsFormChange = false;
                        }
                        e.Cancel = !blnValid;
                    }

                }
            }
            catch (Exception ex) {
                this.SetPageError(ex);
                e.Cancel = true;
            }

            base.OnMenuChange(e);
        }

        private void QSPFormToolBar_AddNewClick(object source, EventArgs e) {
            this.OnAddNew(e);
        }

        protected virtual void OnAddNew(EventArgs e) {
            if (AddNew != null) {
                // Invokes the delegates. 
                AddNew(this, e);
            }
        }


        private void dtgMain_ItemCreated(object source, DataGridItemEventArgs e) {
            this.OnItemCreated(e);
        }

        protected virtual void OnItemCreated(DataGridItemEventArgs e) {
            //In this case we invoke the delegate before main operation
            if (ItemCreated != null) {
                // Invokes the delegates. 
                ItemCreated(this, e);
            }
            if ((e.Item.ItemType == ListItemType.EditItem) || (e.Item.ItemType == ListItemType.Item)
                    || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                ImageButton imgBtnDelete = (ImageButton)e.Item.FindControl("imgBtnDelete");
                if (imgBtnDelete != null)
                    imgBtnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this row?');");

                ImageButton imgBtnUpdate = (ImageButton)e.Item.FindControl("imgBtnUpdate");
                if ((imgBtnUpdate != null) && (LabelMessage != null))
                    imgBtnUpdate.Attributes.Add("onclick", "document.all." + LabelMessage.ClientID + ".innerHTML = '';");

            }
            this.dtgMain.CreateSortHeader(e);

        }

        private void dtgMain_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e) {
            this.OnCancelCommand(e);
        }

        protected virtual void OnCancelCommand(DataGridCommandEventArgs e) {
            if (CancelCommand != null) {
                // Invokes the delegates. 
                CancelCommand(this, e);
            }
            this.dtgMain.EditItemIndex = -1;
            IsFormChange = false;
            BindGrid();
        }

        private void dtgMain_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e) {
            this.OnEditCommand(e);
        }

        protected virtual void OnEditCommand(DataGridCommandEventArgs e) {
            if (EditCommand != null) {
                // Invokes the delegates. 
                EditCommand(this, e);
            }
            this.dtgMain.EditItemIndex = e.Item.ItemIndex;
            BindGrid();
        }

        private void dtgMain_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e) {
            this.OnUpdateCommand(e);
        }

        protected virtual void OnUpdateCommand(DataGridCommandEventArgs e) {
            try {
                if (UpdateCommand != null) {
                    // Invokes the delegates. 
                    UpdateCommand(this, e);
                }
                bool IsSuccess = false;

                Page.Validate();
                if (Page.IsValid) {
                    IsSuccess = UpdateRowGrid();

                    if (IsSuccess) {
                        dtgMain.EditItemIndex = -1;
                        IsFormChange = false;
                        string sID = dvMain.Table.Rows[0][dtgMain.DataKeyField].ToString();
                        LoadDataSourceGrid();
                        this.dtgMain.EnsureVisibility(GetIndexOf(Convert.ToInt32(sID), dtgMain.DataKeyField));
                        dtgMain.DataBind();
                    }
                }
            }
            catch (Exception ex) {
                SetPageError(ex);
            }
        }

        private void dtgMain_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e) {
            this.OnDeleteCommand(e);
        }

        protected virtual void OnDeleteCommand(DataGridCommandEventArgs e) {
            try {
                if (DeleteCommand != null) {
                    // Invokes the delegates. 
                    DeleteCommand(this, e);
                }

                bool IsSuccess = false;
                Page.Validate();
                if (Page.IsValid) {
                    IsSuccess = DeleteRowGrid(e.Item.ItemIndex);

                    if (IsSuccess) {
                        if (dtgMain.CurrentPageIndex != 0) {
                            //If there is the only element move to the
                            //previous page
                            if (dtgMain.Items.Count == 1) {
                                dtgMain.CurrentPageIndex = dtgMain.CurrentPageIndex - 1;
                            }
                        }
                        BindGrid();
                    }
                }
            }
            catch (QSPForm.Common.QSPFormValidationException ex) {
                SetPageError(ex);
            }
        }

        private void dtgMain_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e) {
            this.OnPageIndexChanged(e);
        }

        protected virtual void OnPageIndexChanged(DataGridPageChangedEventArgs e) {
            if (PageIndexChanged != null) {
                // Invokes the delegates. 
                PageIndexChanged(this, e);
            }
            dtgMain.CurrentPageIndex = e.NewPageIndex;
            dtgMain.EditItemIndex = -1;
            IsFormChange = false;
            BindGrid();

        }

        private void dtgMain_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e) {
            this.OnSortCommand(e);
        }

        protected virtual void OnSortCommand(DataGridSortCommandEventArgs e) {
            if (SortCommand != null) {
                // Invokes the delegates. 
                SortCommand(this, e);
            }
            //Sort expression to be manage				
            String sSortExp = "";
            sSortExp = e.SortExpression.ToString();

            //First thing to do
            //Is it for the Primary or the Secondary Sort
            String sPreviousSort;
            if (this.dtgMain.SortExpression != "") {
                sPreviousSort = this.dtgMain.SortExpression;

            }
            else {
                sPreviousSort = DefaultSort;
            }

            String[] sArrSort;
            String suffixSort = "ASC";
            sArrSort = sSortExp.Split(',');
            String sSortToCompare = sPreviousSort.Replace(" ASC", "").Replace(" DESC", "");

            if (sSortToCompare.IndexOf(sSortExp) >= 0) {
                //The same sort have been call twice
                //Set to the opposite direction (ASC or DESC)
                if (sPreviousSort.IndexOf("DESC") > 0) {
                    suffixSort = "ASC";
                }
                else {
                    suffixSort = "DESC";
                }
            }
            else {
                suffixSort = "ASC";
            }
            if (sArrSort.Length > 0) {
                sSortExp = "";
                for (int iCount = 0; iCount < sArrSort.Length; iCount++) {
                    if (iCount > 0) {
                        sSortExp = sSortExp + ",";
                    }
                    sSortExp = sSortExp + sArrSort[iCount] + " " + suffixSort;
                }
            }

            dtgMain.SortExpression = sSortExp;
            dtgMain.EditItemIndex = -1;
            IsFormChange = false;
            BindGrid();
        }

        private void QSPFormSearchModule_ONClick(object sender, SearchModuleClickedArgs e) {
            dtgMain.CurrentPageIndex = 0;
            dtgMain.EditItemIndex = -1;
            this.dtgMain.FilterExpression = e.FilterExpression;
            this.dtgMain.SearchMode = e.SearchMode;
            this.dtgMain.Criteria = e.Criteria;
            BindGrid();
        }

        public int GetIndexOf(int value, string Column) {

            int iCount = 0;
            bool IsFound = false;

            foreach (DataRowView dvrow in dvMain) {
                if (Convert.ToInt32(dvrow[Column]) == value) {
                    IsFound = true;
                    break;
                }
                else {
                    iCount++;
                }
            }
            if (IsFound) {
                return iCount;
            }

            return -1;
        }
    }
}