using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
//using DAL;
//using Business;

namespace QSPFulfillment.Reports
{
	///<summary>EstimatedSalesView</summary>
	public partial class EstimatedSalesView : QSPFulfillment.AcctMgt.AcctMgtPage
	{
		#region auto-generated code
		///<summary>Required method for Designer support</summary>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		///<summary>Required method for Designer support</summary>
		private void InitializeComponent()
		{
			this.pbSearch.Click +=new EventHandler(pbSearch_Click);
			this.dgEstimatedSalesView.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgEstimatedSalesView_PageIndexChanged);
			this.dgEstimatedSalesView.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgEstimatedSalesView_SortCommand);
			this.dgEstimatedSalesView.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgEstimatedSalesView_ItemDataBound);
		}
		#endregion auto-generated code

		#region Item Declarations
		protected double f_EstimatedAmountTotal = 0;
		protected double f_ActualAmountTotal = 0;
		protected double f_VarianceAmountTotal = 0;
		protected double f_em_total  = 0;
		protected double f_ac_total  = 0;
		protected double f_var_total = 0;

		DataTable dt_esv = new DataTable();

		//protected System.Web.UI.WebControls.Label msg;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label17;

		//protected System.Web.UI.WebControls.DropDownList f_program_id;
		//protected System.Web.UI.WebControls.DropDownList f_fm;
		//protected System.Web.UI.WebControls.DropDownList f_status;
		//protected System.Web.UI.HtmlControls.HtmlTableRow trFMSearcRow2;

		

		protected QSPFulfillment.CommonWeb.UC.DateEntry	f_to_date;
		protected QSPFulfillment.CommonWeb.UC.DateEntry	f_from_date;
		protected QSPFulfillment.CommonWeb.UC.ProgramsDDL ucPrograms;
		protected QSPFulfillment.CommonWeb.UC.FieldManagerDDL ucFMddl;
		protected QSPFulfillment.OrderMgt.UC.OrderStatus ucOrderStatus;
		#endregion Item Declarations

		protected void Page_Load(object s, System.EventArgs e)
		{

            if (!IsPostBack)
			{
				populate_list_items();
				f_to_date.Date = Convert.ToDateTime("06/30/2005");
				f_from_date.Date = Convert.ToDateTime("07/01/2004");

				this.ValidationSummary1.HeaderText = QSPFulfillment.DataAccess.Common.Message.VALMSG_HEADER_TEXT_VAR_0;

				if(aUserProfile.HasRole("HomeOffice"))
				{
					trFMSearchRow1.Visible = true;
				}
				else if(aUserProfile.IsFM)
				{
					trFMSearchRow1.Visible = false;
				}
				else //permissions error
				{
					trFMSearchRow1.Visible = false;
				}
			}
		}


		#region DataGrid Binding
		private void dg_bind(string p_sortby)
		{
			// pass the form field values to the business object to fetch the data set//

			#region Populate the business object from form values

			Business.EstimatedSalesView b_esv	= new Business.EstimatedSalesView();

			if (this.ucPrograms.SelectedValue != -5)
			{
					b_esv.p_program_id	= this.ucPrograms.SelectedValue;
			}

			if (this.ucOrderStatus.SelectedValue != -5)
			{
				b_esv.p_status_instance	= this.ucOrderStatus.SelectedValue;
			}

			if(aUserProfile.IsFM)
			{
				b_esv.p_fm_id  = aUserProfile.FMID;
			}
			else
			{
				b_esv.p_fm_id  = this.ucFMddl.SelectedValue;
			}
			b_esv.p_from_date		= f_from_date.Value;
			b_esv.p_to_date			= f_to_date.Value;
			#endregion Populate the business object from form values

			dt_esv = b_esv.fetch_batch_info();

			// compute report level totals - populate the total fields
			get_report_totals();
			// end computing reports totals
            
			DataView dv_esv     = new DataView();
			dv_esv = dt_esv.DefaultView; //ds_esv.Tables["Table"].DefaultView;
			dv_esv.ApplyDefaultSort = true;

			if (p_sortby == null)
			{p_sortby = "DateReceived";}

			dv_esv.Sort = p_sortby;

			// limit the datagrid with with user defined number of rows per page

			//string test  = f_rows_per_page.SelectedValue.GetType().ToString();


			if ( f_rows_per_page.SelectedValue != "ALL" )
			{
				dgEstimatedSalesView.PageSize = Convert.ToInt32(f_rows_per_page.SelectedValue);
			}
			else
			{
				dgEstimatedSalesView.PageSize  = dt_esv.Rows.Count;
			}

			// end limiting rows per page
			dgEstimatedSalesView.DataSource = dv_esv;
			dgEstimatedSalesView.DataBind();
			dgEstimatedSalesView.Visible = true; // it may be invisible because of an error previously
			lblRecordInfo.Text = Convert.ToString(dt_esv.Rows.Count)+ " records found. Currently displaying "+f_rows_per_page.SelectedValue+ " records per page." ;
			this.lblShow.Visible = true;
			this.f_rows_per_page.Visible = true;
		}
		#endregion

		private void get_report_totals ()
		{

			int v_recs = dt_esv.Rows.Count;
			string em_value,ac_value = "" ;

			for (int i=0; i <= v_recs -1; ++i)
			{
				em_value  = dt_esv.Rows[i]["EnterredAmount"].ToString();
				ac_value  = dt_esv.Rows[i]["CalculatedAmount"].ToString();
				f_em_total += Convert.ToDouble(em_value); //estimated report total
				f_ac_total += Convert.ToDouble(ac_value); //actual amount report total
			}

			f_var_total = f_em_total - f_ac_total; // variance  report total

		}


		private void pbSearch_Click(object sender, System.EventArgs e)
		{
			lblRecordInfo.Text = "";
			string fromDateStr = "";
			string toDateStr   = "";

			TextBox TB1 = (TextBox) f_from_date.FindControl("tb_DATE");
			if(TB1 != null) { fromDateStr = "" + TB1.Text; }
			TextBox TB2 = (TextBox) f_to_date.FindControl("tb_DATE");
			if(TB2 != null) { toDateStr = "" + TB2.Text; }

			//if (f_from_date. == "" || f_to_date.Text =="")
			if (fromDateStr == "" || toDateStr == "")
			{
				this.CurrentMessageManager.Add("Please enter From Date/To Date ");
				dgEstimatedSalesView.Visible = false;
				this.SetPageError();
			}
			//else if ( Convert.ToDateTime(f_from_date.Text) > Convert.ToDateTime(f_to_date.Text)  )
			else if ( f_from_date.Date > f_to_date.Date  )
			{
			  this.CurrentMessageManager.Add("From Date can not be greater than To Date");
			  dgEstimatedSalesView.Visible = false;
			  this.SetPageError();
			}
			else
			{
				lblRecordInfo.Text = "";
				dg_bind("DateReceived") ;
			}
		}

//		public string get_programs(int p_campaign)
//		{
//			EstimatedSalesDataAccess eod	=	new EstimatedSalesDataAccess();
//			return  eod.FetchPrograms(p_campaign);
//		}


		private void dgEstimatedSalesView_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			dgEstimatedSalesView.CurrentPageIndex = e.NewPageIndex;
			dg_bind("DateReceived");
		}

		private void dgEstimatedSalesView_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			dg_bind(e.SortExpression);
		}

		
		private void populate_list_items()
		{
			//--------------program-------------------//
			this.ucPrograms.Bind();

			//----------field manager-----------------//
			this.ucFMddl.Bind(1); //mode = 1

			//------status----------------------------//
			this.ucOrderStatus.Bind();
		}


		private void calc_page_total(string Estimated,string Actual,string Variance)
		{
			try
			{
				f_EstimatedAmountTotal	+= Double.Parse(Estimated);
				f_ActualAmountTotal		+= Double.Parse(Actual);
				f_VarianceAmountTotal	+= Double.Parse(Variance);
			}
			catch
			{
				//A value was null
			}
		}


		private void dgEstimatedSalesView_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				Label elab = (Label)e.Item.Cells[7].Controls[1];
				Label alab = (Label)e.Item.Cells[8].Controls[1];
				Label vlab = (Label)e.Item.Cells[9].Controls[1];

				calc_page_total( elab.Text, alab.Text, vlab.Text);

				((Label)e.Item.Cells[7].Controls[1]).Text = string.Format("{0:n}", Convert.ToDouble(elab.Text));
				((Label)e.Item.Cells[8].Controls[1]).Text = string.Format("{0:n}", Convert.ToDouble(alab.Text));
				((Label)e.Item.Cells[9].Controls[1]).Text = string.Format("{0:n}", Convert.ToDouble(vlab.Text));
			}
				/*
			else if(e.Item.ItemType == ListItemType.Footer )
			{
				//e.Item.Cells[9] .Text  = "Page Total: ";
				//e.Item.Cells[10].Text = string.Format("{0:c}", f_EstimatedAmountTotal);
				//e.Item.Cells[11].Text = string.Format("{0:c}", f_ActualAmountTotal);
				//e.Item.Cells[12].Text = string.Format("{0:c}", f_VarianceAmountTotal);

			}
			*/

		}


	}
}
