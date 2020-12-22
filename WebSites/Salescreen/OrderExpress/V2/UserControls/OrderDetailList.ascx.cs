using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using QSPForm.Business;

using QSP.OrderExpress.Business;
using QSP.OrderExpress.Business.Entity;
using QSP.OrderExpress.Common.Data;
using QSP.OrderExpress.Common.Enum;
using QSP.OrderExpress.Common.Search;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.V2.UserControls
{
    public partial class OrderDetailList : System.Web.UI.UserControl
    {
        private List<OrderDetailData> Parameters;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.LoadParameters();
        }

        private void LoadParameters()
        {
            if (this.Parameters == null)
            {
                if (ViewState["Parameters"] == null)
                {
                    this.Parameters = new List<OrderDetailData>();
                }
                else
                {
                    this.Parameters = (List<OrderDetailData>)ViewState["Parameters"];
                }
            }
        }
        private void LoadGrid()
        {
            this.GridView1.DataSource = this.Parameters;
            this.GridView1.DataBind();

            if (this.Parameters.Count > 0)
            {
                this.lblTotalRows.Visible = true;
                this.lblTotalRows.Text = string.Format("{0} total rows found", this.Parameters.Count);
            }
            else
            {
                this.lblTotalRows.Visible = false;
            }
        }

        public void SetValueForList(List<OrderDetailData> parameters)
        {
            this.Parameters = parameters;
            ViewState["Parameters"] = parameters;
        }
        public void SetValuesToForm()
        {
            this.LoadParameters();
            this.LoadGrid();
        }

        protected decimal GetProCodeTotal()
        {
            decimal result = 0;

            foreach (OrderDetailData orderDetailData in this.Parameters)
            {
                result += orderDetailData.OrderedProCodeCases;
            }

            return result;
        }
        protected decimal GetCaseTotal()
        {
            decimal result = 0;

            foreach (OrderDetailData orderDetailData in this.Parameters)
            {
                result += orderDetailData.OrderedCases;
            }

            return result;
        }
        protected decimal GetAmountTotal()
        {
            decimal result = 0;

            foreach (OrderDetailData orderDetailData in this.Parameters)
            {
                result += orderDetailData.Total;
            }

            return result;
        }
    }
}