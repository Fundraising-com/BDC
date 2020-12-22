using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using QSPForm.Business;

using QSP.OrderExpress.Business;
using QSP.OrderExpress.Business.Entity;
using QSP.OrderExpress.Common.Enum;
using QSP.OrderExpress.Common.Search;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.V2.UserControls
{
    public partial class DocumentSearch : System.Web.UI.UserControl
    {
        private DocumentSearchParameters Parameters;
        private List<ListItem> BeginWithList;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Parameters == null)
            {
                this.Parameters = new DocumentSearchParameters();
            }

            this.LoadBeginWithList();

            if (!Page.IsPostBack)
            {
                this.LoadSearchFieldValues();
                this.LoadDocumentTypeValues();
                this.LoadDocumentStatusValues();
                this.SetFormVisibility();
            }
        }

        private void SetFormVisibility()
        {
            LoggedUser loggedUser = (LoggedUser)Session["LoggedUser"];
        }
        private void LoadSearchFieldValues()
        {
            this.ddlField.Items.Clear();
            this.ddlField.Items.Add(new ListItem("Account QSP Id", ((int)DocumentSearchFieldEnum.QSPAccountId).ToString()));
            this.ddlField.Items.Add(new ListItem("Account EDS Id", ((int)DocumentSearchFieldEnum.EDSAccountId).ToString()));
            this.ddlField.Items.Add(new ListItem("Account Name", ((int)DocumentSearchFieldEnum.AccountName).ToString()));
            this.ddlField.Items.Add(new ListItem("Document QSP Id", ((int)DocumentSearchFieldEnum.QSPDocumentId).ToString()));

            this.SetDefaultSearchField(DocumentSearchFieldEnum.AccountName);
        }
        private void LoadDocumentTypeValues()
        {
            DocumentSystem documentSystem = new DocumentSystem();
            List<DocumentType> documentTypeList = documentSystem.GetDocumentTypes();

            this.ddlDocumentType.Items.Clear();
            this.ddlDocumentType.Items.Add(new ListItem("Any", "0"));
            foreach (DocumentType documentType in documentTypeList)
            {
                this.ddlDocumentType.Items.Add(new ListItem(documentType.DocumentTypeName, documentType.DocumentTypeId.ToString()));
            }
        }
        private void LoadDocumentStatusValues()
        {
            DocumentSystem documentSystem = new DocumentSystem();
            Dictionary<int, string> documentStatuses = documentSystem.GetDocumentStatuses();
            
            this.ddlStatus.Items.Clear();
            this.ddlStatus.Items.Add(new ListItem("Any", ""));
            foreach (KeyValuePair<int, string> pair in documentStatuses)
            {
                this.ddlStatus.Items.Add(new ListItem(pair.Value, pair.Key.ToString()));
            }
        }
        private void LoadBeginWithList()
        {
            this.BeginWithList = new List<ListItem>();

            this.BeginWithList.Add(new ListItem("#"));
            this.BeginWithList.Add(new ListItem("A"));
            this.BeginWithList.Add(new ListItem("B"));
            this.BeginWithList.Add(new ListItem("C"));
            this.BeginWithList.Add(new ListItem("D"));
            this.BeginWithList.Add(new ListItem("E"));
            this.BeginWithList.Add(new ListItem("F"));
            this.BeginWithList.Add(new ListItem("G"));
            this.BeginWithList.Add(new ListItem("H"));
            this.BeginWithList.Add(new ListItem("I"));
            this.BeginWithList.Add(new ListItem("J"));
            this.BeginWithList.Add(new ListItem("K"));
            this.BeginWithList.Add(new ListItem("L"));
            this.BeginWithList.Add(new ListItem("M"));
            this.BeginWithList.Add(new ListItem("N"));
            this.BeginWithList.Add(new ListItem("O"));
            this.BeginWithList.Add(new ListItem("P"));
            this.BeginWithList.Add(new ListItem("Q"));
            this.BeginWithList.Add(new ListItem("R"));
            this.BeginWithList.Add(new ListItem("S"));
            this.BeginWithList.Add(new ListItem("T"));
            this.BeginWithList.Add(new ListItem("U"));
            this.BeginWithList.Add(new ListItem("V"));
            this.BeginWithList.Add(new ListItem("W"));
            this.BeginWithList.Add(new ListItem("X"));
            this.BeginWithList.Add(new ListItem("Y"));
            this.BeginWithList.Add(new ListItem("Z"));
            this.BeginWithList.Add(new ListItem("All"));

            this.BeginWithRepeater.DataSource = this.BeginWithList;
            this.BeginWithRepeater.DataBind();
        }
        private void ReadFormData()
        {
            Properties.Settings settings = new Properties.Settings();
            LoggedUser loggedUser = (LoggedUser)Session["LoggedUser"];

            if (this.Parameters == null)
            {
                this.Parameters = new DocumentSearchParameters();
            }

            this.Parameters.LoggedUserId = loggedUser.UserId;
            this.Parameters.LoggedUserType = (UserTypeEnum)loggedUser.UserTypeId;

            this.Parameters.IsPagingEnabled = true;
            this.Parameters.ItemsPerPage = settings.GridPageSize;
            this.Parameters.RequestedPage = 1;
            this.Parameters.SortField = settings.DocumentSearchDefaultSort;

            this.Parameters.SearchField = (DocumentSearchFieldEnum)Convert.ToInt32(this.ddlField.SelectedValue);
            this.Parameters.SearchValue = this.txtContaining.Text;

            if (this.ddlDocumentType.SelectedIndex > 0)
            {
                this.Parameters.DocumentTypeId = Convert.ToInt32(this.ddlDocumentType.SelectedValue);
            }
            else
            {
                this.Parameters.DocumentTypeId = null;
            }
            
            if (this.ddlStatus.SelectedIndex > 0)
            {
                this.Parameters.DocumentStatusId = Convert.ToInt32(this.ddlStatus.SelectedValue);
            }
            else
            {
                this.Parameters.DocumentStatusId = null;
            }
        }

        public void SetDefaultSearchField(DocumentSearchFieldEnum field)
        {
            if (this.Parameters == null)
            {
                this.Parameters = new DocumentSearchParameters();
            }

            this.Parameters.SearchField = field;

            foreach (ListItem item in this.ddlField.Items)
            {
                if (item.Value == ((int)field).ToString())
                {
                    item.Selected = true;
                    break;
                }
            }
        }
        public void SetDefaultSearchValue(string value)
        {
            if (this.Parameters == null)
            {
                this.Parameters = new DocumentSearchParameters();
            }

            this.Parameters.SearchValue = value;
            this.txtContaining.Text = value;
        }
        public void SetDefaultDocumentType(int documentTypeId)
        {
            if (this.Parameters == null)
            {
                this.Parameters = new DocumentSearchParameters();
            }

            this.Parameters.DocumentTypeId = documentTypeId;

            foreach (ListItem item in this.ddlDocumentType.Items)
            {
                if (item.Value == documentTypeId.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }
        }
        public void SetDefaultDocumentStatus(int documentStatusId)
        {
            if (this.Parameters == null)
            {
                this.Parameters = new DocumentSearchParameters();
            }

            this.Parameters.DocumentTypeId = documentStatusId;

            foreach (ListItem item in this.ddlStatus.Items)
            {
                if (item.Value == documentStatusId.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }
        }

        protected void BeginWithRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string beginWith = this.BeginWithList[e.Item.ItemIndex].Text;

            this.ReadFormData();
            this.Parameters.SearchField = DocumentSearchFieldEnum.AccountNameBeginingWith;
            this.Parameters.SearchValue = beginWith;

            OnSearchClick(new DocumentSearchEventArgs(this.Parameters));
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.ReadFormData();

            OnSearchClick(new DocumentSearchEventArgs(this.Parameters));
        }

        public event EventHandler SearchClick;
        protected void OnSearchClick(DocumentSearchEventArgs e)
        {
            if (SearchClick != null)
            {
                SearchClick(this, e);
            }
        }
    }

    public class DocumentSearchEventArgs : EventArgs
    {
        public DocumentSearchEventArgs() { }
        public DocumentSearchEventArgs(DocumentSearchParameters parameters)
        {
            this.Parameters = parameters;
        }
        public DocumentSearchParameters Parameters { get; set; }
    }

}