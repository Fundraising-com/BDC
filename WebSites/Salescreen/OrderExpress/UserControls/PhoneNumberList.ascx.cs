using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.PhoneNumberEntityTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for Phone Number List.
    /// </summary>
    public partial class PhoneNumberList : BaseWebUserControl {
        protected dataDef dtPhoneNumber = new dataDef();
        private int c_ParentID;
        private int c_PhoneID;
        private int c_ParentType;
        protected System.Web.UI.WebControls.Button btnAddNew;
        protected System.Data.DataTable tblTypePhoneNumber = new DataTable();
        private QSPForm.Business.PhoneNumberSystem phoneSys = new QSPForm.Business.PhoneNumberSystem();

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here					
            lblMessage.Text = "";
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
            this.dtgPhoneNumber.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgPhoneNumber_DeleteCommand);
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);

        }
        #endregion

        protected void Page_DataBinding(object sender, System.EventArgs e) {
            try {
                //retreive data detail item for db
                //Init DataList

                this.dtgPhoneNumber.EditItemIndex = -1;
                BindGrid();
            }
            catch (Exception ex) {
                lblMessage.Text = ex.Message;
            }
        }

        public int PhoneID {
            get {
                return c_PhoneID;
            }
            set {
                c_PhoneID = value;
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

        public int ParentType {
            //Identify on wich we have to do our operation
            //0= Nothing (direct to the postal address table)
            //1= Organization
            //2= Account
            //3= Campaign
            //4= Contact
            get {
                return c_ParentType;
            }
            set {
                c_ParentType = value;
            }
        }

        public PhoneNumberEntityTable DataSource {
            get {
                return dtPhoneNumber;

            }
            set {
                dtPhoneNumber = value;
            }
        }

        public void LoadDataSource() {

            // Create a new dataset to hold the records returned from the call to FillDataSet.
            // A temporary dataset is used because filling the existing dataset would
            // require the databindings to be rebound.
            //dtPhoneNumber = phoneSys.SelectAllByEntityID(c_ParentType, c_ParentID);
        }

        private void BindGrid() {
            FillDataTableForDropDownList();
            this.dtgPhoneNumber.DataBind();
        }

        private void imgBtnAddNew_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            UpdateDataSource();
            DataRow NewRow = dtPhoneNumber.NewRow();
            dtPhoneNumber.Rows.InsertAt(NewRow, 0);
            BindGrid();
        }

        private void FillDataTableForDropDownList() {
            try {
                QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
                //Type Address				
                tblTypePhoneNumber = comSys.SelectAllPhoneNumberType();
            }
            catch (Exception ex) {
                lblMessage.Text = ex.Message;
            }
        }

        protected int getSelectedIndex(DataTable dt, String sValue) {
            int iIndex = -1;
            try {
                if (sValue != "") {
                    int iCounter = 0;
                    foreach (DataRow row in dt.Rows) {
                        if (sValue == row[0].ToString()) {
                            iIndex = iCounter;
                            break;
                        }
                        iCounter++;
                    }
                }
            }
            catch (Exception ex) {
                lblMessage.Text = ex.Message;
            }
            return iIndex;
        }

        private void dtgPhoneNumber_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e) {
            UpdateDataSource();
            int pkID = Convert.ToInt32(dtgPhoneNumber.DataKeys[e.Item.ItemIndex]);
            DataRow row = dtPhoneNumber.Rows.Find(pkID);
            //Don't need to update a row recently added who doesn't exist
            //really in the database
            if (row.RowState != DataRowState.Added)
                row[dataDef.FLD_UPDATE_USER_ID] = Page.UserID;
            row.Delete();
            BindGrid();
        }

        public bool UpdateDataSource() {
            bool blnValid = false;

            try {
                CommonUtility clsUtil = new CommonUtility();
                int iCounter = 0;
                //'We save everything that is possible
                //Invalid 	
                for (iCounter = 0; iCounter <= dtgPhoneNumber.Items.Count - 1; iCounter++) {
                    DataGridItem dgItem;
                    dgItem = dtgPhoneNumber.Items[iCounter];
                    int pkID = Convert.ToInt32(dtgPhoneNumber.DataKeys[iCounter]);
                    DataRow row = dtPhoneNumber.Rows.Find(pkID);

                    //'Table Mapping                     
                    clsUtil.UpdateRow(row, dataDef.FLD_ENTITY_ID, c_ParentID.ToString());
                    clsUtil.UpdateRow(row, dataDef.FLD_ENTITY_TYPE_ID, c_ParentType.ToString());
                    clsUtil.UpdateRow(row, dataDef.FLD_TYPE, ((DropDownList)dgItem.FindControl("ddlType")).SelectedValue);
                    clsUtil.UpdateRow(row, dataDef.FLD_TYPE_NAME, ((DropDownList)dgItem.FindControl("ddlType")).SelectedItem.Text);
                    clsUtil.UpdateRow(row, dataDef.FLD_PHONE_NUMBER, ((TextBox)dgItem.FindControl("txtPhoneNumber")).Text);

                    if (row.RowState != DataRowState.Unchanged) {
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

        private bool Update(DataGridItem dgItem) {
            bool IsSuccess = false;

            c_PhoneID = Convert.ToInt32(dtgPhoneNumber.DataKeys[dgItem.ItemIndex]);

            // get edited row values in grid
            DataRow row;
            if (c_PhoneID <= 0) {
                row = dtPhoneNumber.NewRow();
                row[dataDef.FLD_ENTITY_ID] = c_ParentID;
                row[dataDef.FLD_ENTITY_TYPE_ID] = c_ParentType;
                row[dataDef.FLD_TYPE] = ((DropDownList)dgItem.FindControl("ddlType")).SelectedValue;
                row[dataDef.FLD_PHONE_NUMBER] = ((TextBox)dgItem.FindControl("txtPhoneNumber")).Text;

                row[dataDef.FLD_CREATE_USER_ID] = Page.UserID;
                dtPhoneNumber.Rows.Add(row);
                IsSuccess = phoneSys.Insert(dtPhoneNumber);
            }
            else {
                dtPhoneNumber = phoneSys.SelectOne(c_ParentType, c_PhoneID);
                if (dtPhoneNumber.Rows.Count > 0) {
                    row = dtPhoneNumber.Rows[0];
                    row[dataDef.FLD_ENTITY_ID] = c_ParentID;
                    row[dataDef.FLD_ENTITY_TYPE_ID] = c_ParentType;
                    row[dataDef.FLD_TYPE] = ((DropDownList)dgItem.FindControl("ddlType")).SelectedValue;
                    row[dataDef.FLD_PHONE_NUMBER] = ((TextBox)dgItem.FindControl("txtPhoneNumber")).Text;

                    row[dataDef.FLD_UPDATE_USER_ID] = Page.UserID;
                    IsSuccess = phoneSys.Update(dtPhoneNumber);
                }
                else {
                    //If we can't find it is probably because someone delete it before
                    IsSuccess = true;
                }
            }

            return IsSuccess;
        }
    }
}