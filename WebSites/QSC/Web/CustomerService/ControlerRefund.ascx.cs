namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.DataAccess.Common.ActionObject;
    using System.Collections.Generic;


	/// <summary>
	///	Summary description for ControlerAddress.
	/// </summary>
	public partial class ControlerRefund :  CustomerServiceControl
	{
        bool showCheckBoxes;

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			
			base.OnInit(e);

            this.dtlRefund.ItemDataBound += new DataListItemEventHandler(dtlRefund_ItemDataBound);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion
		
		public void DataBindRefunds()
		{
			try
			{
				DataSource = new DataTable("Refund");
                this.Page.BusPayment.SelectRefundsByCOD(DataSource, this.Page.OrderInfo.CustomerOrderHeaderInstance, this.Page.OrderInfo.TransID);
				
                this.dtlRefund.DataSource = DataSource;
                this.dtlRefund.DataBind();
                base.DataBind();

			}
			catch(ExceptionFulf ex)
			{
				this.Page.SetPageError(ex);
			}
		}

        public void DataBindRefundsAvailableToCancel()
        {
            try
            {
                DataSource = new DataTable("Refund");
                DataSource = this.Page.BusPayment.SelectRefundsAvailableToCancelByCOD(this.Page.OrderInfo.CustomerOrderHeaderInstance, this.Page.OrderInfo.TransID);

                this.dtlRefund.DataSource = DataSource;
                this.dtlRefund.DataBind();
                base.DataBind();

            }
            catch (ExceptionFulf ex)
            {
                this.Page.SetPageError(ex);
            }
        }
		
        public List<int> GetSelectedRefundIDs()
        {
            List<int> list = new List<int>();

            foreach (DataListItem dli in this.dtlRefund.Items)
            {
                CheckBox checkBox = (CheckBox)dli.FindControl("cbxSelectRefund");
                if (checkBox.Checked)
                {
                    Label label = (Label)dli.FindControl("RefundID");
                    list.Add(Convert.ToInt32(label.Text));
                }
            }

            return list;
        }

        void dtlRefund_ItemDataBound(Object sender, DataListItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item ||
             e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox checkBox = (CheckBox)e.Item.FindControl("cbxSelectRefund");
                checkBox.Visible = ShowCheckBoxes;
                checkBox.Enabled = ShowCheckBoxes;
            }
        }

        public bool ShowCheckBoxes
        {
            get
            {
                return showCheckBoxes;
            }
            set
            {
                showCheckBoxes = value;
            }
        }
    }
}
