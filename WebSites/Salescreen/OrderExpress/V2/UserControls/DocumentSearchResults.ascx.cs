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
    public partial class DocumentSearchResults : System.Web.UI.UserControl
    {
        private DocumentSearchParameters Parameters;
        private int count = 0;

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

                    this.Parameters = new DocumentSearchParameters();

                    this.Parameters.LoggedUserId = loggedUser.UserId;
                    this.Parameters.LoggedUserType = (UserTypeEnum)loggedUser.UserTypeId;

                    this.Parameters.SearchField = DocumentSearchFieldEnum.Any;
                    this.Parameters.SearchValue = "";
                    this.Parameters.DocumentStatusId = null;
                    this.Parameters.DocumentTypeId = null;

                    this.Parameters.IsPagingEnabled = true;
                    this.Parameters.ItemsPerPage = settings.GridPageSize;
                    this.Parameters.RequestedPage = 1;
                    this.Parameters.SortField = settings.DocumentSearchDefaultSort;

                    ViewState["SortColumn"] = "QSPDocumentId";
                    ViewState["SortDirection"] = "desc";
                }
                else
                {
                    this.Parameters = (DocumentSearchParameters)ViewState["Parameters"];

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

        public void SetColumnsForDocumentSearch()
        {
            this.GridView1.Columns[11].Visible = true;
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
            this.GridView1.Columns[11].Visible = true;
            this.GridView1.Columns[10].Visible = true;
            this.GridView1.Columns[9].Visible = true;
            this.GridView1.Columns[8].Visible = true;
            this.GridView1.Columns[7].Visible = true;
            this.GridView1.Columns[6].Visible = true;
            this.GridView1.Columns[5].Visible = false;
            this.GridView1.Columns[4].Visible = false;
            this.GridView1.Columns[3].Visible = false;
            this.GridView1.Columns[2].Visible = true;
            this.GridView1.Columns[1].Visible = true;
            this.GridView1.Columns[0].Visible = false;
        }
        public void SetSearchParameters(DocumentSearchParameters parameters)
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

    public class DocumentGridHelper
    {
        public IQueryable GetData(DocumentSearchParameters parameter, int startRowIndex, int maximumRows, string sort)
        {
            parameter.RequestedPage = (startRowIndex / maximumRows) + 1;
            parameter.ItemsPerPage = maximumRows;

            DocumentSystem documentSystem = new DocumentSystem();
            List<DocumentSearchItem> results = documentSystem.Search(parameter);

            return results.AsQueryable();
        }
        public int GetCount(DocumentSearchParameters parameter)
        {
            DocumentSystem documentSystem = new DocumentSystem();
            int count = documentSystem.SearchTotalRowCount(parameter);

            return count;
        }
    }

}