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
//using DAL;
//using Business;
//using System.Data.OleDb;
//using System.Xml;

namespace QSPFulfillment.Finance
{
	///<summary>payDeposit</summary>
	public partial class payDeposit : QSPFulfillment.CommonWeb.QSPPage
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
			this.dgDeposits.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.ItemsGrid_Command);
			this.dgDeposits.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgDeposits_PageIndexChanged);
			this.dgDeposits.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgDeposits_SortCommand);
			this.dgDeposits.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgDeposits_ItemDataBound);
			this.dgPayments.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgPayments_PageIndexChanged);
			this.dgPayments.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgPayments_SortCommand);

		}
		#endregion auto-generated code

		#region Item Declarations
		#endregion Item Declarations

		public string dgDepositsSortfield = "BANK_DEPOSIT_ID";
		public string dgPaymentsSortfield = "PAYMENT_ID";
		public string searchByFieldType ;

		DataView dvDeposits = new DataView();
		DataView dvPayments = new DataView();
		//Business.CodeHeader DepositStatusCodeHeader = Business.CodeHeader.BankDepositStatus;

		private double PageTotal = 0;
		public int Cnt;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				populate_list_items();
				dg_Deposit_bind();
			}
		}

		public string getDepositStatusDesc(int StatusCode)
		{
			DataSet ds = new DataSet();
			DAL.CodeDetailDataAccess StatData =	new DAL.CodeDetailDataAccess();
			ds = StatData.GetCodeDescSelectone(StatusCode); 
			return ds.Tables[0].Rows[0]["Description"].ToString();
		}

		private void populate_list_items()
		{
			int DepositStatusCodeHeader =  (int) Business.CodeHeader.BankDepositStatus;
			DataSet Statusds = new DataSet();
			DAL.CodeDetailDataAccess StatusData =	new DAL.CodeDetailDataAccess();
			Statusds = StatusData.GetCodeDesc(DepositStatusCodeHeader); 
			ddlDepositStatus.DataSource= Statusds;
			ddlDepositStatus.DataBind();
			ddlDepositStatus.Items.Insert(0, new ListItem("All", String.Empty));

		}

		protected void ddlSearchBy_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (ddlSearchBy.SelectedValue == "DEPOSIT_DATE")
			{
				SearchboxDate.Visible = true;
				SearchboxDate.Text="";
				LblSearchDate2.Visible =true;
				LblSearchFrom.Visible = true;
				LblSearch.Visible = false;
			}
			else
			{
				SearchboxDate.Visible = false;
				SearchboxDate.Text="";
				LblSearchDate2.Visible =false;
				LblSearchFrom.Visible = false;
				LblSearch.Visible = true;
			}

		}

		private void dg_Deposit_bind()

		{
			double DepositTotal=0;

			DataSet Depositds = new DataSet();
			Business.BankDeposit BD = new Business.BankDeposit();

			if (ddlDepositStatus.SelectedValue == "")
			  {
				BD.DepositStatusId = null;
			  }
			else
			  {
				BD.DepositStatusId = ddlDepositStatus.SelectedValue;
			  }

			if ((Searchbox.Text != "") || (SearchboxDate.Text !=""))
			{   // validate the textbox value

				switch (Convert.ToString (ddlSearchBy.SelectedValue))
				{
					case "BANK_DEPOSIT_ID":
					
						BD.Bank_Deposit_Id= Searchbox.Text;
						break;

					case "BANK_ACCOUNT_NUMBER":
						BD.DepositAccountNo = Searchbox.Text;
						break;

					case "DEPOSIT_AMOUNT":
						BD.DepositAmount = Searchbox.Text;
						break;

					case"ITEM_COUNT" :
						BD.ItemDeposited = Searchbox.Text;
						break;

					case "DEPOSIT_DATE" :
						if ((BD.Validate("System.DateTime",Searchbox.Text)))
						{
							BD.DepositDate = Searchbox.Text;
							// entered search value is in correct format check the second value
							if ((BD.Validate("System.DateTime",SearchboxDate.Text)))
							{
								BD.DepositDateTo = SearchboxDate.Text;
							}
							else
							{
								// make query retreive no record by setting end date
								BD.DepositDateTo = "12/31/1901";
							}
						}
						else
						{
							//the entered string is not a date
							BD.DepositDate= "01/01/2222";
						}
   					 break;
				}
			}
			//If no search Creteria define

					Depositds = BD.GetDepositDataSet();
					dvDeposits = Depositds.Tables["Table"].DefaultView;
					dvDeposits.Sort = dgDepositsSortfield;
					dgDeposits.DataSource = dvDeposits;
					for (Cnt=0;  Cnt <= (dvDeposits.Count-1) ; ++Cnt)
					{
						DepositTotal += Convert.ToDouble(Depositds.Tables[0].Rows[Cnt]["Deposit_Amount"]);
					}
					lblReportTotalDeposit.Text =  string.Format("{0:C}",(DepositTotal));
					lblPgCnt.Text = Convert.ToString(dvDeposits.Count);
			
					try
					{
						dgDeposits.DataBind();
					}
					catch( Exception e)
					{
						// If Numbers of records filtered are less than the current page index reset Page index
						if (dgDeposits.CurrentPageIndex> (dvDeposits.Count/dgDeposits.PageSize)  )
						{
								dgDeposits.CurrentPageIndex = 0;
								dgDeposits.DataBind();
						}
						string a =e.Message;
					}
		}

		private void dgDeposits_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			// Hide child payment records because they will not related
			// as a result of page index change
			HidePayments();
			dgDeposits.CurrentPageIndex = e.NewPageIndex;
			dg_Deposit_bind();
		}

		private void dgDeposits_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			dgDepositsSortfield = e.SortExpression;
			dg_Deposit_bind();
		}

		protected void ddlDepositStatus_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			HidePayments();
			lblMessage.Visible = false;
			dg_Deposit_bind();
		}

		protected void pbSearch_Click(object sender, System.EventArgs e)
		{
			HidePayments();
			lblMessage.Visible = false;
			dg_Deposit_bind();
		}


		private void dg_Payment_bind ()
		{
			double PaymentTotal = 0;
			DataSet Paymentds = new DataSet();
			Business.Payments P = new Business.Payments();
			P.BankDepositId = lblBDID.Text;

			if ((SearchPaymentBox.Text != "") || (SearchboxPDate.Text !=""))
			{   // validate the textbox value

				switch (Convert.ToString (ddlSearchPaymentBy.SelectedValue))
				{
					case "PAYMENT_ID":
						P.PaymentId = SearchPaymentBox.Text;
						break;

					case "CHEQUE_NUMBER":
						P.ChequeNumber = SearchPaymentBox.Text;
						break;

					case "ORDER_ID":
						P.OrderId = SearchPaymentBox.Text;
						break;

					case "CAMPAIGN_ID":
						P.CampaignId = SearchPaymentBox.Text;
						break;

					case "PAYMENT_AMOUNT":
						P.PaymentAmount = SearchPaymentBox.Text;
						break;

					case "CHEQUE_DATE" :
						if ((P.Validate("System.DateTime",SearchPaymentBox.Text)))
						{
							P.ChequeDate = SearchPaymentBox.Text;
							// entered search value is in correct format check the second value
							if ((P.Validate("System.DateTime",SearchboxPDate.Text)))
							{
								P.ChequeDateTo = SearchboxPDate.Text;
							}
							else
							{
								// make query retreive no record by setting end date
								P.ChequeDateTo = "12/31/1901";
							}
						}
						else
						{
							//the entered string is not a date
							P.ChequeDate= "01/01/2222";
						}
						break;

				}
			}
			//If no search Creteria define

			Paymentds = P.GetPaymentDataSet();
			dvPayments = Paymentds.Tables["Table"].DefaultView;
			dgPayments.DataSource = dvPayments;

			dvPayments.Sort =dgPaymentsSortfield;

			for (Cnt=0;  Cnt <= (dvPayments.Count-1) ; ++Cnt)
			{
				PaymentTotal += Convert.ToDouble(Paymentds.Tables[0].Rows[Cnt]["Payment_Amount"]);
			}
			lblReportTotalPayment.Text =  string.Format("{0:C}",(PaymentTotal));
            lblPaymentCount.Text = Convert.ToString(dvPayments.Count);
			try
			{
				dgPayments.DataBind();
			}
			catch( Exception e)
			{
				// same as Deposit grid
				if (dgPayments.CurrentPageIndex> (dvPayments.Count/dgPayments.PageSize)  )
				{
					dgPayments.CurrentPageIndex = 0;
					dgPayments.DataBind();
				}
				string a =e.Message;
			}

		}

		private void dgPayments_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			dgPaymentsSortfield = e.SortExpression;
			dg_Payment_bind();
		}


		private void dgPayments_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			dgPayments.CurrentPageIndex = e.NewPageIndex;
			dg_Payment_bind();
		}

		private void ShowPaymentsData(DataGridCommandEventArgs e)
		{
			dgDeposits.SelectedIndex =    Convert.ToInt32(e.Item.ItemIndex);
			lblBDID.Text = Convert.ToString(dgDeposits.DataKeys[dgDeposits.SelectedIndex]);
			ShowPaymentsHeader(e);
			dg_Payment_bind();
		}


		void ItemsGrid_Command(Object sender, DataGridCommandEventArgs e)
		{
			if (e.CommandName == "ShowPayments")
			{
				if (ddlPageSize.SelectedValue == "10")
				{
					ShowPaymentsData(e);
				}
				else
				{
					//Message please restric number of records to 10 to view detail
				}
			}
			else
			{
       			// Do nothing.
			}
		}


		private void ShowPaymentsHeader(DataGridCommandEventArgs e)
		{
      	// If Payments Details are not visible make them visible
      		lblPayments.Visible = true;
			dgPayments.Visible  = true;
			lblSearchPaymentBy.Visible = true;

			if (Convert.ToString(ddlSearchPaymentBy.SelectedValue) == "CHEQUE_DATE")
			{
				lblSearchPayment.Visible = false;
				lblSearchPFrom.Visible = true;
				SearchboxPDate.Visible = true;
				lblSearchPDate2.Visible = true;
			}
			else
			{
				lblSearchPFrom.Visible = false;
				lblSearchPayment.Visible = true;
			}
			ddlSearchPaymentBy.Visible = true;
			pbSearchPayment.Visible  = true;
			SearchPaymentBox.Visible = true;
			SearchPaymentBox.Text = "";
			pbHidePayments.Visible = true;
		}


		private void HidePayments()
		{
		// If Payments Details are already visible hide them
			lblPayments.Visible = false;
			dgPayments.Visible = false;
			lblSearchPayment.Visible   = false;
			lblSearchPaymentBy.Visible = false;
			ddlSearchPaymentBy.Visible = false;
			pbSearchPayment.Visible  = false;
			SearchPaymentBox.Visible = false;
			pbHidePayments.Visible = false;

			SearchboxPDate.Visible = false;
			SearchboxPDate.Text="";
			lblSearchPDate2.Visible =false;
			lblSearchPFrom.Visible = false;

		}


		protected void pbHidePayments_Click(object sender, System.EventArgs e)
		{
			lblMessage.Visible = false;
			lblMessage.Text = "";
    		HidePayments();
	    }


		protected void pbSearchPayment_Click(object sender, System.EventArgs e)
		{
			dg_Payment_bind ();
		}

		protected void pbAddNewDeposit_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("BankDepositAdd.aspx");
		}


		protected void ddlSearchPaymentBy_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (ddlSearchPaymentBy.SelectedValue == "CHEQUE_DATE")
			{
				SearchboxPDate.Visible = true;
				SearchboxPDate.Text="";
				lblSearchPDate2.Visible =true;
				lblSearchPFrom.Visible = true;
				lblSearchPayment.Visible = false;
			}
			else
			{
				SearchboxPDate.Visible = false;
				SearchboxPDate.Text="";
				lblSearchPDate2.Visible =false;
				lblSearchPFrom.Visible = false;
				lblSearchPayment.Visible = true;
			}
		}


		private void dgDeposits_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			//if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			//{
			//	Label elab  = (Label) e.Item.Cells[5].Controls[1] ;
			//	CalcPageTotal( elab.Text);

			//	((Label)e.Item.Cells[5].Controls[1]).Text = string.Format("{0:n}", Convert.ToDouble(elab.Text));

			//}

		}

		private void CalcPageTotal(string _price)
		{
			try
			{
				PageTotal += Double.Parse(_price);
			}
			catch
			{
				//A value was null
			}
		}

		protected void ddlPageSize_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			switch (ddlPageSize.SelectedValue)
			{
				case "All":
					HidePayments();
					dgDeposits.PageSize = dgDeposits.PageCount * dgDeposits.PageSize;
					break;
				case "10":
					dgDeposits.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
					break;
				case "25":
					HidePayments();
					dgDeposits.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
					break;
				case "50":
					HidePayments();
					dgDeposits.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
					break;
			}
			dg_Deposit_bind();

		}







	}
}

