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
    public partial class OrganizationSearchResults : System.Web.UI.UserControl
    {
        private OrganizationSearchParameters Parameters;

        protected void Page_Load(object sender, EventArgs e)
        {
            Properties.Settings settings = new Properties.Settings();
            this.GridView1.PageSize = settings.GridPageSize;

            this.LoadParameters();
        }
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (!e.ExecutingSelectCount)
            {
                this.LoadParameters();

                e.InputParameters["parameter"] = this.Parameters;
            }
        }
        protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue is int)
            {
                this.lblTotalRows.Text = String.Format("{0} total rows found", ((int)e.ReturnValue).ToString());
            }
        }
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortColumn = (string)ViewState["SortColumn"];
            if (sortColumn == e.SortExpression)
            {
                // Same column, switch direction
                string sortDirection = (string)ViewState["SortDirection"];
                if (sortDirection == "asc")
                {
                    ViewState["SortDirection"] = "desc";
                }
                else
                {
                    ViewState["SortDirection"] = "asc";
                }
            }
            else
            {
                // Different column, default to asc direction
                ViewState["SortColumn"] = e.SortExpression;
                ViewState["SortDirection"] = "asc";
            }

            if (ViewState["SortColumn"] != null && ViewState["SortDirection"] != null)
            {
                string sortField = string.Format("{0} {1}", ViewState["SortColumn"], ViewState["SortDirection"]);
                this.Parameters.SortField = sortField;
            }

            //this.LoadParameters();
            this.GridView1.DataBind();

            e.Cancel = true;
        }

        private void LoadParameters()
        {
            if (this.Parameters == null)
            {
                if (ViewState["Parameters"] == null)
                {
                    Properties.Settings settings = new Properties.Settings();
                    LoggedUser loggedUser = (LoggedUser)Session["LoggedUser"];

                    this.Parameters = new OrganizationSearchParameters();

                    this.Parameters.LoggedUserId = loggedUser.UserId;
                    this.Parameters.LoggedUserType = (UserTypeEnum)loggedUser.UserTypeId;

                    if (loggedUser.UserTypeId == (int)UserTypeEnum.User
                        || loggedUser.UserTypeId == (int)UserTypeEnum.FieldSaleManager)
                    {
                        this.Parameters.FSMId = loggedUser.FMId;
                        this.Parameters.FSMName = "";
                        this.Parameters.SearchFSMOption = SearchFSMHierarchyOptionEnum.Own;
                    }
                    else
                    {
                        this.Parameters.FSMId = "";
                        this.Parameters.FSMName = "";
                        this.Parameters.SearchFSMOption = SearchFSMHierarchyOptionEnum.All;
                    }
                    
                    this.Parameters.SearchField = OrganizationSearchFieldEnum.Any;
                    this.Parameters.SearchValue = "";
                    this.Parameters.OrganizationTypeId = null; 
                    this.Parameters.SubdivisionCode = "";
                    
                    this.Parameters.IsPagingEnabled = true;
                    this.Parameters.ItemsPerPage = settings.GridPageSize;
                    this.Parameters.RequestedPage = 1;
                    this.Parameters.SortField = settings.OrganizationSearchDefaultSort;

                    ViewState["SortColumn"] = "OrganizationId";
                    ViewState["SortDirection"] = "desc";
                }
                else 
                {
                    this.Parameters = (OrganizationSearchParameters)ViewState["Parameters"];

                    if (ViewState["SortColumn"] != null && ViewState["SortDirection"] != null)
                    {
                        string sortField = string.Format("{0} {1}", ViewState["SortColumn"], ViewState["SortDirection"]);
                        this.Parameters.SortField = sortField;
                    }
                }
            }
        }
        private void LoadGrid()
        {
            this.GridView1.PageIndex = 0;
            this.GridView1.DataBind();
        }

        public void SetSearchParameters(OrganizationSearchParameters parameters)
        {
            this.Parameters = parameters;
            ViewState["Parameters"] = parameters;
        }
        public void DoSearch()
        {
            this.LoadParameters();
            this.LoadGrid();
        }
    }

    public class OrganizationGridHelper
    {
        public IQueryable GetData(OrganizationSearchParameters parameter, int startRowIndex, int maximumRows, string sort)
        {
            parameter.RequestedPage = (startRowIndex / maximumRows) + 1;
            parameter.ItemsPerPage = maximumRows;

            OrganizationSystem organizationSystem = new OrganizationSystem();
            List<OrganizationSearchItem> results = organizationSystem.Search(parameter);

            return results.AsQueryable();
        }
        public int GetCount(OrganizationSearchParameters parameter)
        {
            OrganizationSystem organizationSystem = new OrganizationSystem();
            int count = organizationSystem.SearchTotalRowCount(parameter);

            return count;
        }
    }

}