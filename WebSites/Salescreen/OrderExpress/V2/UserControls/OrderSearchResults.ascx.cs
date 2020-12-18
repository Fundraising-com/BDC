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
    public partial class OrderSearchResults : System.Web.UI.UserControl
    {
        private OrderSearchParameters Parameters;
        private int count = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Properties.Settings settings = new Properties.Settings();
            this.GridView1.PageSize = settings.GridPageSize;

            this.LoadParameters();

            LoggedUser loggedUser = (LoggedUser)Session["LoggedUser"];
            if (IsPostBack || loggedUser.UserTypeId != (int)UserTypeEnum.SuperUser)
            {
                GridView1.DataSourceID = "ObjectDataSource1";
            }
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
                this.count = (int)e.ReturnValue;
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

                    this.Parameters = new OrderSearchParameters();

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

                    this.Parameters.SearchField = OrderSearchFieldEnum.Any;
                    this.Parameters.SearchValue = "";
                    this.Parameters.ProgramTypeId = settings.DefaultProgramType;
                    this.Parameters.SourceId = settings.DefaultSource;
                    this.Parameters.FormId = null;
                    this.Parameters.StatusCategoryId = null;
                    this.Parameters.SubdivisionCode = "";

                    this.Parameters.IsPagingEnabled = true;
                    this.Parameters.ItemsPerPage = settings.GridPageSize;
                    this.Parameters.RequestedPage = 1;
                    this.Parameters.SortField = settings.AccountSearchDefaultSort;

                    int year = DateTime.Now.Year;
                    if ((DateTime.Now.Month <= 6))
                        year = DateTime.Now.Year - 1;
                    this.Parameters.StartDate = new DateTime(year, 4, 1);

                    ViewState["SortColumn"] = "OrderId";
                    ViewState["SortDirection"] = "desc";
                }
                else
                {
                    this.Parameters = (OrderSearchParameters)ViewState["Parameters"];

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

        public void SetColumnsForOrderSearch()
        {
            this.GridView1.Columns[17].Visible = true;
            this.GridView1.Columns[16].Visible = true;
            this.GridView1.Columns[15].Visible = true;
            this.GridView1.Columns[14].Visible = true;
            this.GridView1.Columns[13].Visible = true;
            this.GridView1.Columns[12].Visible = true;
            this.GridView1.Columns[11].Visible = false;
            this.GridView1.Columns[10].Visible = true;
            this.GridView1.Columns[9].Visible = true;
            this.GridView1.Columns[8].Visible = true;
            this.GridView1.Columns[7].Visible = true;
            this.GridView1.Columns[6].Visible = true;
            this.GridView1.Columns[5].Visible = true;
            this.GridView1.Columns[4].Visible = true;
            this.GridView1.Columns[3].Visible = true;
            this.GridView1.Columns[2].Visible = true;
            this.GridView1.Columns[1].Visible = true;
            this.GridView1.Columns[0].Visible = true;
        }
        public void SetColumnsForAccountView()
        {
            this.GridView1.Columns[17].Visible = true;
            this.GridView1.Columns[16].Visible = false;
            this.GridView1.Columns[15].Visible = false;
            this.GridView1.Columns[14].Visible = false;
            this.GridView1.Columns[13].Visible = false;
            this.GridView1.Columns[12].Visible = true;
            this.GridView1.Columns[11].Visible = false;
            this.GridView1.Columns[10].Visible = true;
            this.GridView1.Columns[9].Visible = true;
            this.GridView1.Columns[8].Visible = false;
            this.GridView1.Columns[7].Visible = false;
            this.GridView1.Columns[6].Visible = false;
            this.GridView1.Columns[5].Visible = true;
            this.GridView1.Columns[4].Visible = true;
            this.GridView1.Columns[3].Visible = true;
            this.GridView1.Columns[2].Visible = true;
            this.GridView1.Columns[1].Visible = true;
            this.GridView1.Columns[0].Visible = true;
        }
        public void SetColumnsForProgramAgreementView()
        {
            this.GridView1.Columns[17].Visible = true;
            this.GridView1.Columns[16].Visible = false;
            this.GridView1.Columns[15].Visible = false;
            this.GridView1.Columns[14].Visible = false;
            this.GridView1.Columns[13].Visible = false;
            this.GridView1.Columns[12].Visible = true;
            this.GridView1.Columns[11].Visible = false;
            this.GridView1.Columns[10].Visible = true;
            this.GridView1.Columns[9].Visible = true;
            this.GridView1.Columns[8].Visible = false;
            this.GridView1.Columns[7].Visible = false;
            this.GridView1.Columns[6].Visible = false;
            this.GridView1.Columns[5].Visible = true;
            this.GridView1.Columns[4].Visible = true;
            this.GridView1.Columns[3].Visible = true;
            this.GridView1.Columns[2].Visible = true;
            this.GridView1.Columns[1].Visible = true;
            this.GridView1.Columns[0].Visible = true;
        }
        public void SetSearchParameters(OrderSearchParameters parameters)
        {
            this.Parameters = parameters;
            ViewState["Parameters"] = parameters;
        }
        public void DoSearch()
        {
            this.LoadParameters();
            this.LoadGrid();
        }
        public int GetCount()
        {
            return this.count;
        }
    }

    public class OrderGridHelper
    {
        public IQueryable GetData(OrderSearchParameters parameter, int startRowIndex, int maximumRows, string sort)
        {
            parameter.RequestedPage = (startRowIndex / maximumRows) + 1;
            parameter.ItemsPerPage = maximumRows;

            OrderSystem orderSystem = new OrderSystem();
            List<OrderSearchItem> results = orderSystem.Search(parameter);

            return results.AsQueryable();
        }
        public int GetCount(OrderSearchParameters parameter)
        {
            OrderSystem orderSystem = new OrderSystem();
            int count = orderSystem.SearchTotalRowCount(parameter);

            return count;
        }
    }
}