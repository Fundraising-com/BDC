using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using QSP.OrderExpress.Web;
using dataDef = QSPForm.Common.DataDef.AccountTransferAccountTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    public partial class FMAccountTransferControl : BaseWebUserControl {
        private CommonUtility clsUtil = new CommonUtility();
        protected dataDef dTblOrganization = new dataDef();
        protected string deletedIds = String.Empty;
        protected string flagpoleIds = String.Empty;
        protected string ChkdItems = String.Empty;
        protected string ChkBxIndex = String.Empty;
        protected bool BxChkd = false;
        protected bool flagpoleBxChkd = false;
        protected ArrayList CheckedItems;
        protected ArrayList FlagPoleCheckedItems;
        protected string[] Results;
        protected string[] FlagPoleResults;
        protected string SortField = String.Empty;

        protected void Page_Load(object sender, EventArgs e) {
            txtFMName.Attributes.Add("onfocus", "javascript:window.focus();");
            txtFMID.Attributes.Add("onfocus", "javascript:window.focus();");
            clsUtil.SetJScriptForOpenSelector(imgBtnSelectFM, txtFMID, txtFMName, "FMSelector.aspx", "FMSelector", 0, 0, null);
            txtFMName1.Attributes.Add("onfocus", "javascript:window.focus();");
            txtFMID1.Attributes.Add("onfocus", "javascript:window.focus();");
            clsUtil.SetJScriptForOpenSelector(imgBtnSelectFM1, txtFMID1, txtFMName1, "FMSelector.aspx", "FMSelector", 0, 0, null);
            txtFMName2.Attributes.Add("onfocus", "javascript:window.focus();");
            txtFMID2.Attributes.Add("onfocus", "javascript:window.focus();");
            clsUtil.SetJScriptForOpenSelector(imgBtnSelectFM2, txtFMID2, txtFMName2, "FMSelector.aspx", "FMSelector", 0, 0, null);
        }

        void BindGrid(string SortField) {
            QSPForm.Business.FMAccountTransferSystem FMorgSys = new QSPForm.Business.FMAccountTransferSystem();
            dTblOrganization = FMorgSys.SelectAllAccountByFMID(txtFMID.Text);

            //Assign sort expression to Session   
            Session["SortOrder"] = SortField;
            DataView Source = new DataView();
            Source.Table = dTblOrganization;
            Source.Sort = SortField;
            MasterGridView.DataSource = Source;
            MasterGridView.DataBind();
            lblTotal.Text = "Number of Account(s) : " + Source.Count.ToString();
        }

        protected void FlagPoleID_CheckedChanged(object sender, EventArgs e) {
            CheckBox checkbox = (CheckBox)sender;
            GridViewRow row = (GridViewRow)checkbox.NamingContainer;
            TableCell cell = row.Cells[0];

            string flagpoleid = row.Cells[0].Text;

            foreach (GridViewRow item in MasterGridView.Rows) {
                if (item.Cells[0].Text == flagpoleid) {
                    CheckBox Accountchk = (CheckBox)item.FindControl("AccountCheckBox");

                    if (checkbox.Checked == true) {
                        if (Accountchk != null) {
                            Accountchk.Checked = true;
                        }
                    }
                    else {
                        if (Accountchk != null) {
                            Accountchk.Checked = false;
                        }
                    }
                }
            }
        }

        protected void AccountID_CheckedChanged(object sender, EventArgs e) {
        }

        protected void MasterGridView_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            //Get CheckBoxValues before paging occurs
            GetCheckBoxValues();
            MasterGridView.PageIndex = e.NewPageIndex;
            BindGrid(Session["SortOrder"].ToString());
            //Populate current GridView page with the current page items from Session after databind            
            RePopulateCheckBoxes();
            lblCurrentIndex.Text = "Page&nbsp;" + (MasterGridView.PageIndex + 1) + "&nbsp;of&nbsp;" + MasterGridView.PageCount + "&nbsp;";
        }

        protected void GetCheckBoxValues() {
            CheckedItems = new ArrayList();
            FlagPoleCheckedItems = new ArrayList();

            foreach (GridViewRow dgItems in MasterGridView.Rows) {
                ChkBxIndex = MasterGridView.DataKeys[dgItems.RowIndex].Value.ToString();

                CheckBox checkBox = (CheckBox)dgItems.FindControl("AccountCheckBox");
                CheckBox flagpoleCheckBox = (CheckBox)dgItems.FindControl("FlagPoleID");

                if (Session["CheckedItems"] != null) {
                    CheckedItems = (ArrayList)Session["CheckedItems"];
                }

                if (Session["FlagPoleCheckedItems"] != null) {
                    FlagPoleCheckedItems = (ArrayList)Session["FlagPoleCheckedItems"];
                }

                if (checkBox.Checked == true) {
                    BxChkd = true;

                    if (!CheckedItems.Contains(ChkBxIndex)) {
                        CheckedItems.Add(ChkBxIndex.ToString());
                    }
                }
                else {
                    //Remove value from Session when unchecked    
                    CheckedItems.Remove(ChkBxIndex.ToString());
                }

                if (flagpoleCheckBox.Checked == true) {
                    flagpoleBxChkd = true;

                    if (!FlagPoleCheckedItems.Contains(ChkBxIndex)) {
                        FlagPoleCheckedItems.Add(ChkBxIndex.ToString());
                    }
                }
                else {
                    //Remove value from Session when unchecked    
                    FlagPoleCheckedItems.Remove(ChkBxIndex.ToString());
                }
            }
            //Update Session with the list of checked items   
            Session["CheckedItems"] = CheckedItems;

            //Update Session with the list of flagpole checked items   
            Session["FlagPoleCheckedItems"] = FlagPoleCheckedItems;
        }

        protected void RePopulateCheckBoxes() {
            CheckedItems = new ArrayList();
            CheckedItems = (ArrayList)Session["CheckedItems"];

            FlagPoleCheckedItems = new ArrayList();
            FlagPoleCheckedItems = (ArrayList)Session["FlagPoleCheckedItems"];

            if (CheckedItems != null) {
                foreach (GridViewRow dgItems in MasterGridView.Rows) {
                    ChkBxIndex = MasterGridView.DataKeys[dgItems.RowIndex].Value.ToString();

                    //Repopulate GridView with items found in Session   
                    if (CheckedItems.Contains(ChkBxIndex)) {
                        CheckBox checkBox = (CheckBox)dgItems.FindControl("AccountCheckBox");
                        checkBox.Checked = true;
                    }
                }
            }

            if (FlagPoleCheckedItems != null) {
                foreach (GridViewRow dgItems in MasterGridView.Rows) {
                    ChkBxIndex = MasterGridView.DataKeys[dgItems.RowIndex].Value.ToString();

                    //Repopulate GridView with items found in Session   
                    if (FlagPoleCheckedItems.Contains(ChkBxIndex)) {
                        CheckBox checkBox = (CheckBox)dgItems.FindControl("FlagPoleID");
                        checkBox.Checked = true;
                    }
                }
            }

            //Copy ArrayList to a new array       
            Results = (string[])CheckedItems.ToArray(typeof(string)); // ToArray(GetType(String));

            ////Concatenate ArrayList with comma to properly send for deletion     
            deletedIds = String.Join(",", Results);

            //Copy ArrayList to a new array       
            FlagPoleResults = (string[])FlagPoleCheckedItems.ToArray(typeof(string)); // ToArray(GetType(String));

            ////Concatenate ArrayList with comma to properly send for deletion     
            flagpoleIds = String.Join(",", FlagPoleResults);
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e) {
            if (txtFMID.Text.Trim() != String.Empty) {
                ErrorMessage.Text = String.Empty;

                if (Session["CheckedItems"] != null) {
                    //Clear all Session values    
                    Session.Remove("CheckedItems");
                }
                if (Session["FlagPoleCheckedItems"] != null) {
                    //Clear all Session values    
                    Session.Remove("FlagPoleCheckedItems");
                }
                if (Session["SortOrder"] != null) {
                    Session.Remove("SortOrder");
                }

                MasterGridView.PageIndex = 0;

                //Set up default column sorting 
                if (Session["SortOrder"] == null) {
                    BindGrid("flagpoleinstance asc");
                }
                else {
                    BindGrid(Session["SortOrder"].ToString());
                }
            }
            else {
                ErrorMessage.Text = "Please Select the FM";
            }
        }

        protected void imgSelect1_Click(object sender, ImageClickEventArgs e) {
            if (Page.IsValid) {
                QSPForm.Business.FMAccountTransferSystem FMAccountSys = new QSPForm.Business.FMAccountTransferSystem();
                GetCheckBoxValues();

                if (BxChkd == true) {
                    RePopulateCheckBoxes();
                    bool result = FMAccountSys.UpdateAccountsBYFMID(deletedIds, txtFMID.Text, txtFMID1.Text, txtFMID2.Text, Convert.ToDateTime(txtDate.Text), txtReason.Text, this.Page.UserID);
                    //Clear all Session values    
                    Session.Remove("CheckedItems");
                    //Reset GridView to top                
                    MasterGridView.PageIndex = 0;
                    BindGrid(Session["SortOrder"].ToString());
                }
            }
        }

        protected void imgSelect2_Click(object sender, ImageClickEventArgs e) {
            if (Page.IsValid) {
                QSPForm.Business.CommonSystem FMterritorySys = new QSPForm.Business.CommonSystem();
                bool result = FMterritorySys.UpdateTerritoryBYFMID(txtFMID.Text, txtFMID1.Text, txtFMID2.Text, Convert.ToDateTime(txtDate.Text), txtReason.Text, this.Page.UserID);

                BindGrid(Session["SortOrder"].ToString());
            }
        }

        protected void MasterGridView_Sorting(object sender, GridViewSortEventArgs e) {
            //To retain checkbox on sorting       
            GetCheckBoxValues();
            MasterGridView.PageIndex = 0;
            BindGrid(SortOrder(e.SortExpression).ToString());  //Rebind our GridView
            //To retain checkbox on sorting
            RePopulateCheckBoxes();
        }

        protected string SortOrder(string Field) {
            string sortorder;
            string so = Session["SortOrder"].ToString();

            if (Field == so) {
                sortorder = Field.Replace("asc", "desc");
            }
            else if (Field != so) {
                sortorder = Field.Replace("desc", "asc");
            }
            else {
                sortorder = Field.Replace("asc", "desc");
            }

            Session["SortOrder"] = sortorder;
            return sortorder;
        }
    } 
}