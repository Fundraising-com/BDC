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
    public partial class OrganizationSearch : System.Web.UI.UserControl
    {
        private OrganizationSearchParameters Parameters;
        private List<ListItem> BeginWithList;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Parameters = new OrganizationSearchParameters();

            this.LoadBeginWithList();

            if (!Page.IsPostBack)
            {
                this.LoadSearchFieldValues();
                this.LoadOrganizationTypeValues();
                this.LoadStateValues();
                this.LoadFSMHierarchyValues();
                this.SetFormVisibility();
            }
        }

        private void SetFormVisibility()
        {
            LoggedUser loggedUser = (LoggedUser)Session["LoggedUser"];

            if (loggedUser.UserTypeId == (int)UserTypeEnum.User
                || loggedUser.UserTypeId == (int)UserTypeEnum.FieldSaleManager)
            {
                this.trFSMFilter.Visible = false;
                this.trFSMHierarchy.Visible = true;
            }
            else
            {
                this.trFSMFilter.Visible = true;
                this.trFSMHierarchy.Visible = false;
            }
        }
        private void LoadSearchFieldValues()
        {
            this.ddlField.Items.Clear();
            this.ddlField.Items.Add(new ListItem("Organization QSP Id", ((int)OrganizationSearchFieldEnum.QSPOrganizationId).ToString()));
            this.ddlField.Items.Add(new ListItem("Organization Name", ((int)OrganizationSearchFieldEnum.Name).ToString()));
            this.ddlField.Items.Add(new ListItem("Organization City", ((int)OrganizationSearchFieldEnum.City).ToString()));
            this.ddlField.Items.Add(new ListItem("Organization Zip code", ((int)OrganizationSearchFieldEnum.ZipCode).ToString()));

            this.SetDefaultSearchField(OrganizationSearchFieldEnum.Name);
        }
        private void LoadOrganizationTypeValues()
        {
            OrganizationSystem organizationSystem = new OrganizationSystem();
            List<OrganizationType> organizationTypeList = organizationSystem.GetOrganizationTypes();

            this.ddlType.Items.Clear();
            this.ddlType.Items.Add(new ListItem("Any", "0"));
            foreach (OrganizationType organizationType in organizationTypeList)
            {
                this.ddlType.Items.Add(new ListItem(organizationType.OrganizationTypeName, organizationType.OrganizationTypeId.ToString()));
            }
        }
        private void LoadStateValues()
        {
            SubdivisionSystem subdivisionSystem = new SubdivisionSystem();
            List<QSP.OrderExpress.Business.Entity.Subdivision> subdivisionList = subdivisionSystem.GetSubdivisionCodesByCountryCode("US");

            this.ddlState.Items.Clear();
            this.ddlState.Items.Add(new ListItem("Any", ""));
            foreach (QSP.OrderExpress.Business.Entity.Subdivision subdivision in subdivisionList)
            {
                this.ddlState.Items.Add(new ListItem(string.Format("{0} - {1}", subdivision.SubdivisionCode, subdivision.SubdivisionName1), subdivision.SubdivisionCode));
            }
        }
        private void LoadFSMHierarchyValues()
        {
            this.ddlFSMHierarchy.Items.Clear();
            this.ddlFSMHierarchy.Items.Add(new ListItem("Own accounts", "1"));
            this.ddlFSMHierarchy.Items.Add(new ListItem("Direct reports", "2"));
            this.ddlFSMHierarchy.Items.Add(new ListItem("Own accounts and direct reports", "3"));
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

            this.Parameters = new OrganizationSearchParameters();

            this.Parameters.LoggedUserId = loggedUser.UserId;
            this.Parameters.LoggedUserType = (UserTypeEnum)loggedUser.UserTypeId;

            this.Parameters.IsPagingEnabled = true;
            this.Parameters.ItemsPerPage = settings.GridPageSize;
            this.Parameters.RequestedPage = 1;
            this.Parameters.SortField = settings.OrganizationSearchDefaultSort;

            this.Parameters.SearchField = (OrganizationSearchFieldEnum)Convert.ToInt32(this.ddlField.SelectedValue);
            this.Parameters.SearchValue = this.txtContaining.Text;

            if (this.ddlType.SelectedIndex > 0)
            {
                this.Parameters.OrganizationTypeId = Convert.ToInt32(this.ddlType.SelectedValue);
            }
            else
            {
                this.Parameters.OrganizationTypeId = null;
            }
            this.Parameters.SubdivisionCode = this.ddlState.SelectedValue;

            if (loggedUser.UserTypeId == (int)UserTypeEnum.User
                || loggedUser.UserTypeId == (int)UserTypeEnum.FieldSaleManager)
            {
                this.Parameters.FSMId = loggedUser.FMId;
                this.Parameters.FSMName = "";                
                this.Parameters.SearchFSMOption = (SearchFSMHierarchyOptionEnum)(Convert.ToInt32(this.ddlFSMHierarchy.SelectedValue));
            }
            else
            {
                this.Parameters.FSMId = this.txtFSMId.Text;
                this.Parameters.FSMName = this.txtFSMName.Text;
                this.Parameters.SearchFSMOption = SearchFSMHierarchyOptionEnum.All;
            }
        }

        public void SetDefaultSearchField(OrganizationSearchFieldEnum field)
        {
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
            this.Parameters.SearchValue = value;
            this.txtContaining.Text = value;
        }
        public void SetDefaultType(int organizationTypeId)
        {
            this.Parameters.OrganizationTypeId = organizationTypeId;

            foreach (ListItem item in this.ddlType.Items)
            {
                if (item.Value == organizationTypeId.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }
        }
        public void SetDefaultState(string subdivisionCode)
        {
            this.Parameters.SubdivisionCode = subdivisionCode;

            foreach (ListItem item in this.ddlState.Items)
            {
                if (item.Value == subdivisionCode)
                {
                    item.Selected = true;
                    break;
                }
            }
        }
        public void SetDefaultFSMId(string fsmId)
        {
            this.Parameters.FSMId = fsmId;
            this.txtFSMId.Text = fsmId;
        }
        public void SetDefaultFSMHierarchy(SearchFSMHierarchyOptionEnum option)
        {
            this.Parameters.SearchFSMOption = option;

            foreach (ListItem item in this.ddlFSMHierarchy.Items)
            {
                if (item.Value == ((int)option).ToString())
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
            this.Parameters.SearchField = OrganizationSearchFieldEnum.NameBeginingWith;
            this.Parameters.SearchValue = beginWith;

            OnSearchClick(new OrganizationSearchEventArgs(this.Parameters));
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.ReadFormData();

            OnSearchClick(new OrganizationSearchEventArgs(this.Parameters));
        }

        public event EventHandler SearchClick;
        protected void OnSearchClick(OrganizationSearchEventArgs e)
        {
            if (SearchClick != null)
            {
                SearchClick(this, e);
            }
        }
    }

    public class OrganizationSearchEventArgs : EventArgs
    {
        public OrganizationSearchEventArgs() {}
        public OrganizationSearchEventArgs(OrganizationSearchParameters parameters) 
        {
            this.Parameters = parameters;
        }
        public OrganizationSearchParameters Parameters { get; set; }
    }
}