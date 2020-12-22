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
    public partial class OrderSearch : System.Web.UI.UserControl
    {
        private OrderSearchParameters Parameters;
        private List<ListItem> BeginWithList;

        protected void Page_Load(object sender, EventArgs e)
        {
            int year = DateTime.Now.Year;

            if (this.Parameters == null)
            {
                this.Parameters = new OrderSearchParameters();
            }

            this.LoadBeginWithList();

            if (!Page.IsPostBack)
            {
                this.LoadSearchFieldValues();
                this.LoadProgramTypeValues();
                this.LoadSourceValues();
                this.LoadFormValues();
                this.LoadOrderTypeValues();
                this.LoadStateValues();
                this.LoadStatusCategoryValues();
                this.LoadFSMHierarchyValues();

                this.SetFormVisibility();

                if ((DateTime.Now.Month <= 6))
                    year = DateTime.Now.Year - 1;

                tbStartDate_CalendarExtender.SelectedDate = new DateTime(year, 4, 1);
            }
            else
            {
                tbStartDate_CalendarExtender.SelectedDate = Convert.ToDateTime(tbStartDate.Text.Trim());

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
            this.ddlField.Items.Add(new ListItem("Order QSP Id", ((int)ProgramAgreementSearchFieldEnum.QSPProgramAgreementId).ToString()));
            this.ddlField.Items.Add(new ListItem("Order EDS Id", ((int)ProgramAgreementSearchFieldEnum.EDSProgramAgreementId).ToString()));
            this.ddlField.Items.Add(new ListItem("Account QSP Id", ((int)ProgramAgreementSearchFieldEnum.QSPAccountId).ToString()));
            this.ddlField.Items.Add(new ListItem("Account EDS Id", ((int)ProgramAgreementSearchFieldEnum.EDSAccountId).ToString()));
            this.ddlField.Items.Add(new ListItem("Account Name", ((int)ProgramAgreementSearchFieldEnum.Name).ToString()));
            this.ddlField.Items.Add(new ListItem("Account City", ((int)ProgramAgreementSearchFieldEnum.City).ToString()));
            this.ddlField.Items.Add(new ListItem("Account Zip code", ((int)ProgramAgreementSearchFieldEnum.ZipCode).ToString()));

            this.SetDefaultSearchField(OrderSearchFieldEnum.Name);
        }
        private void LoadProgramTypeValues()
        {
            ProgramSystem programSystem = new ProgramSystem();
            List<ProgramType> programTypeList = programSystem.GetProgramTypes();

            this.ddlProgramType.Items.Clear();
            this.ddlProgramType.Items.Add(new ListItem("Any", "0"));
            foreach (ProgramType programType in programTypeList)
            {
                this.ddlProgramType.Items.Add(new ListItem(programType.ProgramTypeName, programType.ProgramTypeId.ToString()));
            }

            Properties.Settings settings = new Properties.Settings();
            this.SetDefaultProgramType(settings.DefaultProgramType);
        }
        private void LoadSourceValues()
        {
            OrderSystem orderSystem = new OrderSystem();
            List<Source> sourceList = orderSystem.GetSources();

            this.ddlSource.Items.Clear();
            this.ddlSource.Items.Add(new ListItem("Any", "0"));
            foreach (Source source in sourceList)
            {
                this.ddlSource.Items.Add(new ListItem(source.SourceName, source.SourceId.ToString()));
            }
        }
        private void LoadFormValues()
        {
            FormSystem formSystem = new FormSystem();
            List<Form> formList = formSystem.GetEnabledOrderForms();

            this.ddlForm.Items.Clear();
            this.ddlForm.Items.Add(new ListItem("Any", "0"));
            foreach (Form form in formList)
            {
                this.ddlForm.Items.Add(new ListItem(form.FormName, form.FormId.ToString()));
            }
        }
        private void LoadOrderTypeValues()
        {
            OrderSystem orderSystem = new OrderSystem();
            List<OrderType> orderTypeList = orderSystem.GetOrderTypes();

            this.ddlOrderType.Items.Clear();
            this.ddlOrderType.Items.Add(new ListItem("Any", "0"));
            foreach (OrderType orderType in orderTypeList)
            {
                this.ddlOrderType.Items.Add(new ListItem(orderType.OrderTypeName, orderType.OrderTypeId.ToString()));
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
        private void LoadStatusCategoryValues()
        {
            StatusSystem statusSystem = new StatusSystem();
            List<StatusCategory> statusCategoryList = statusSystem.GetStatusCategories();

            this.ddlStatusCategory.Items.Clear();
            this.ddlStatusCategory.Items.Add(new ListItem("Any", "0"));
            foreach (StatusCategory statusCategory in statusCategoryList)
            {
                this.ddlStatusCategory.Items.Add(new ListItem(statusCategory.StatusCategoryName, statusCategory.StatusCategoryId.ToString()));
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

            if (this.Parameters == null)
            {
                this.Parameters = new OrderSearchParameters();
            }

            this.Parameters.LoggedUserId = loggedUser.UserId;
            this.Parameters.LoggedUserType = (UserTypeEnum)loggedUser.UserTypeId;

            this.Parameters.IsPagingEnabled = true;
            this.Parameters.ItemsPerPage = settings.GridPageSize;
            this.Parameters.RequestedPage = 1;
            this.Parameters.SortField = settings.AccountSearchDefaultSort;

            this.Parameters.SearchField = (OrderSearchFieldEnum)Convert.ToInt32(this.ddlField.SelectedValue);
            this.Parameters.SearchValue = this.txtContaining.Text;



            if (this.ddlProgramType.SelectedIndex > 0)
            {
                this.Parameters.ProgramTypeId = Convert.ToInt32(this.ddlProgramType.SelectedValue);
            }
            else
            {
                this.Parameters.ProgramTypeId = null;
            }
            if (this.ddlSource.SelectedIndex > 0)
            {
                this.Parameters.SourceId = Convert.ToInt32(this.ddlSource.SelectedValue);
            }
            else
            {
                this.Parameters.SourceId = null;
            }
            if (this.ddlForm.SelectedIndex > 0)
            {
                this.Parameters.FormId = Convert.ToInt32(this.ddlForm.SelectedValue);
            }
            else
            {
                this.Parameters.FormId = null;
            }
            if (this.ddlOrderType.SelectedIndex > 0)
            {
                this.Parameters.OrderTypeId = Convert.ToInt32(this.ddlOrderType.SelectedValue);
            }
            else
            {
                this.Parameters.OrderTypeId = null;
            }
            this.Parameters.SubdivisionCode = this.ddlState.SelectedValue;
            if (this.ddlStatusCategory.SelectedIndex > 0)
            {
                this.Parameters.StatusCategoryId = Convert.ToInt32(this.ddlStatusCategory.SelectedValue);
            }
            else
            {
                this.Parameters.StatusCategoryId = null;
            }

            DateTime startDate;
            bool isStartDateAvailable = DateTime.TryParse(this.tbStartDate.Text, out startDate);
            if (isStartDateAvailable)
            {
                this.Parameters.StartDate = startDate;
            }
            else
            {
                this.Parameters.StartDate = null;
            }

            DateTime endDate;
            bool isEndDateAvailable = DateTime.TryParse(this.tbEndDate.Text, out endDate);
            if (isEndDateAvailable)
            {
                this.Parameters.EndDate = endDate;
            }
            else
            {
                this.Parameters.EndDate = null;
            }

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

        public void SetDefaultSearchField(OrderSearchFieldEnum field)
        {
            if (this.Parameters == null)
            {
                this.Parameters = new OrderSearchParameters();
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
                this.Parameters = new OrderSearchParameters();
            }

            this.Parameters.SearchValue = value;
            this.txtContaining.Text = value;
        }
        public void SetDefaultProgramType(int programTypeId)
        {
            if (this.Parameters == null)
            {
                this.Parameters = new OrderSearchParameters();
            }

            this.Parameters.ProgramTypeId = programTypeId;

            foreach (ListItem item in this.ddlProgramType.Items)
            {
                if (item.Value == programTypeId.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }
        }
        public void SetDefaultSource(int sourceId)
        {
            if (this.Parameters == null)
            {
                this.Parameters = new OrderSearchParameters();
            }

            this.Parameters.SourceId = sourceId;

            foreach (ListItem item in this.ddlSource.Items)
            {
                if (item.Value == sourceId.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }
        }
        public void SetDefaultForm(int formId)
        {
            if (this.Parameters == null)
            {
                this.Parameters = new OrderSearchParameters();
            }

            this.Parameters.FormId = formId;

            foreach (ListItem item in this.ddlForm.Items)
            {
                if (item.Value == formId.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }
        }
        public void SetDefaultState(string subdivisionCode)
        {
            if (this.Parameters == null)
            {
                this.Parameters = new OrderSearchParameters();
            }

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
        public void SetDefaultStatusCategory(int statusCategoryId)
        {
            if (this.Parameters == null)
            {
                this.Parameters = new OrderSearchParameters();
            }

            this.Parameters.StatusCategoryId = statusCategoryId;

            foreach (ListItem item in this.ddlStatusCategory.Items)
            {
                if (item.Value == statusCategoryId.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }
        }
        public void SetDefaultFSMId(string fsmId)
        {
            if (this.Parameters == null)
            {
                this.Parameters = new OrderSearchParameters();
            }

            this.Parameters.FSMId = fsmId;
            this.txtFSMId.Text = fsmId;
        }
        public void SetDefaultFSMHierarchy(SearchFSMHierarchyOptionEnum option)
        {
            if (this.Parameters == null)
            {
                this.Parameters = new OrderSearchParameters();
            }

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
            this.Parameters.SearchField = OrderSearchFieldEnum.NameBeginingWith;
            this.Parameters.SearchValue = beginWith;

            OnSearchClick(new OrderSearchEventArgs(this.Parameters));
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.ReadFormData();

            OnSearchClick(new OrderSearchEventArgs(this.Parameters));
        }

        public event EventHandler SearchClick;
        protected void OnSearchClick(OrderSearchEventArgs e)
        {
            if (SearchClick != null)
            {
                SearchClick(this, e);
            }
        }
    }

    public class OrderSearchEventArgs : EventArgs
    {
        public OrderSearchEventArgs() { }
        public OrderSearchEventArgs(OrderSearchParameters parameters)
        {
            this.Parameters = parameters;
        }
        public OrderSearchParameters Parameters { get; set; }
    }
}