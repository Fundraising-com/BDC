using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Configuration;
using DAL;

namespace QSPFulfillment.Finance
{
	public partial class InvoiceList : QSPFulfillment.CommonWeb.QSPPage
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
		}
		#endregion auto-generated code
		
		#region Constructor
		public InvoiceList()
		{
			aInvoiceDataAccess = new InvoiceListDataAccess();
		}
		#endregion Constructor
		
		#region Item Declarations
		protected System.Web.UI.WebControls.RegularExpressionValidator RegExp9;
		protected System.Web.UI.WebControls.Label lblGL;
		protected InvoiceListDataAccess aInvoiceDataAccess;

		protected DataSet ds  = new DataSet();
		protected DataView dv = new DataView();
		protected DataGrid GLDG;
		protected Button btnAdj;
		protected HyperLink lnk_AccountType;
		protected HyperLink lnk_AccountTypePymt;

		//Error Message
		protected Label lblAdjustment;
		//Number of Records to show
		//Title
		//Show text
		//Page Number
		//Range of Records
		protected Label lblProductRecords;


		protected string strSearchField = "";

		protected DateTime dtStartParam, dtEndParam, dtStartDate;
		protected string strStartDate, strEndDate;

		//last week
		protected string strDayLW;
		protected string strMonthLW;
		protected System.Web.UI.WebControls.DropDownList ddlInvStatus;
		protected string strYearLW;
		protected int invIdValue;
		protected int invActValue;
		protected int invOrderValue;
		protected int invCampaignValue;
		protected int invAdjValue;

		protected int nAdjustmentID;
		protected int nPaymentID;		
        #endregion Item Declarations

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(Request.QueryString["Search"] != null)
			{
				strSearchField = Request.QueryString["Search"];
			}

			if (!IsPostBack)
			{
				Search.Text = strSearchField; //Request.QueryString["Search"];
	
				// go back 60 days
				TimeSpan ts = new System.TimeSpan(-60, 0, 0, 0);
				DateTime aToDate = DateTime.Now.Add(ts);
				strStartDate = aToDate.ToString("dd-MMM-yyyy").Trim();
				strEndDate   = DateTime.Now.ToString("dd-MMM-yyyy").Trim();

				//Textboxes
				FromDate.Text = strStartDate;
				ToDate.Text   = strEndDate;

				//Bind the invoice datagrid
				InvoiceListDG.SelectedIndex = 0;
				BindInvoiceGrid(strSearchField, strStartDate, strEndDate);
			}
		}


		#region INVOICE
		public void BindInvoiceGrid(string strSearchField, string strFromDate, string strToDate)
		{
            string AccountName="";
            int AccountID=0;
            int OrderID=0;
            int InvoiceID=0;
            int CampaignID=0;

            if(strSearchField.Length > 0)
			{
				if(ddlStatus.SelectedItem.Value.Trim()=="Name")
				{
                    AccountName = strSearchField;
				}
				else if(ddlStatus.SelectedItem.Value.Trim()=="AccountID")
				{
                    AccountID = Convert.ToInt32(strSearchField);
				}
				else if(ddlStatus.SelectedItem.Value.Trim()=="OrderID")
				{
                    OrderID = Convert.ToInt32(strSearchField);
				}
				else if(ddlStatus.SelectedItem.Value.Trim()=="InvoiceID")
				{
                    InvoiceID = Convert.ToInt32(strSearchField);
				}
				else if(ddlStatus.SelectedItem.Value.Trim()=="CampaignID")
				{
                    CampaignID = Convert.ToInt32(strSearchField);
				}

			}

			//Get all invoices by date range
			ds = aInvoiceDataAccess.GetAllInvoicesByDate(strFromDate, strToDate, AccountName, AccountID, OrderID, InvoiceID, CampaignID);

			dv = ds.Tables[0].DefaultView;

            int totalInvoices = ds.Tables[0].Rows.Count;
			ShowInvoiceStats(totalInvoices);

            Session["InvSortField"] = "Invoice_ID, Invoice_Date";

			dv.Sort = (string)Session["InvSortField"];

			// bind to the Data
			InvoiceListDG.DataSource = dv;
			InvoiceListDG.DataBind();
		}

		public void ShowInvoiceStats(int nCount)
		{
			if (nCount == 0){lblInvoice.Text = "No Records Found."; }
			else
			{
				int nStartOfSet = (nCount > 0) ? (InvoiceListDG.CurrentPageIndex*InvoiceListDG.PageSize+1) : 0;
				int nEndPage = (InvoiceListDG.CurrentPageIndex+1)*(InvoiceListDG.PageSize);
				int nEndOfSet = (nEndPage > nCount) ? nCount : nEndPage;
				lblInvoice.Text = String.Format("Records: {0}-{1} of {2}", nStartOfSet, nEndOfSet ,nCount);
				InvoiceListDG.PagerStyle.Visible = (nCount <= InvoiceListDG.PageSize) ? false : true;
			}
		}

		public void InvoiceListDG_Page(Object sender, DataGridPageChangedEventArgs e)
		{
			if( InvoiceListDG.SelectedIndex != -1 )
			{
				//undo the selection
				InvoiceListDG.SelectedIndex = -1;
			}
			//used by built-in pager.  CurrentPageIndex already set
			InvoiceListDG.CurrentPageIndex = e.NewPageIndex;
			strSearchField = (string)Search.Text.Trim();
			strStartDate    = (string)FromDate.Text.Trim();
			strEndDate      = (string)ToDate.Text.Trim();
			BindInvoiceGrid(strSearchField, strStartDate, strEndDate);
			//Hide the detail grids
			HideAdjustmentDetails();
			HidePaymentDetails();
			HideProductDetails();
		}

		public void InvoiceListDG_Sort(Object sender, DataGridSortCommandEventArgs e)
		{
			Session["InvSortField"] = (string)e.SortExpression;
			strSearchField = (string)Search.Text.Trim();
			strStartDate    = (string)FromDate.Text.Trim();
			strEndDate      = (string)ToDate.Text.Trim();
			BindInvoiceGrid(strSearchField, strStartDate, strEndDate);
			//Hide the detail grids
			HideAdjustmentDetails();
			HidePaymentDetails();
			HideProductDetails();
		}

		public void SearchButtonClick(Object sender, EventArgs e)
		{
			InvoiceListDG.CurrentPageIndex = 0;
			InvoiceListDG.SelectedIndex = 0;

			strSearchField  = (string)Search.Text.Trim();
			strStartDate    = (string)FromDate.Text.Trim();
			strEndDate      = (string)ToDate.Text.Trim();

			//Bind
			BindInvoiceGrid(strSearchField, strStartDate, strEndDate);
			//Hide the detail grids
			HideAdjustmentDetails();
			HidePaymentDetails();
			HideProductDetails();
		}

		public void InvoiceListDG_Select(Object sender, DataGridCommandEventArgs e)
		{
            HiddenField OrderID = (HiddenField)e.Item.FindControl("InvoiceListDG_OrderId");
            Session["invOrderValue"] = Convert.ToInt32(OrderID == null? "0": (string)OrderID.Value);
			if (e.CommandName == "Adjustment")
			{
				//Set the SelectedIndex property to select an item in the DataGrid
				InvoiceListDG.SelectedIndex=e.Item.ItemIndex;               
				//show the details for ADJUSTMENT
				ShowAdjustmentDetails();
				//hide the other detail grids
				HidePaymentDetails();
				HideProductDetails();
				BindAdjustmentDetails();
			}
			else if(e.CommandName == "Payment")
			{
				//Set the SelectedIndex property to select an item in the DataGrid
				InvoiceListDG.SelectedIndex=e.Item.ItemIndex;                
                
				//show the details for PAYMENT
				ShowPaymentDetails();
				//hide the other detail grids
				HideAdjustmentDetails();
				HideProductDetails();
				BindPaymentDetails();
			}
			else if(e.CommandName == "Product")
			{
				//Set the SelectedIndex property to select an item in the DataGrid
				InvoiceListDG.SelectedIndex=e.Item.ItemIndex;
				//show the details for PRODUCT
				ShowProductDetails();
				//hide the other detail grids
				HideAdjustmentDetails();
				HidePaymentDetails();
				BindProductDetails();
			}
		}
		#endregion INVOICE

		#region ADJUSTMENT
			public void BindAdjustmentDetails()
			{
				//get the filter value from the master Grid's DataKeys collection
				if( InvoiceListDG.SelectedIndex != -1)
				{
                    int nID = Convert.ToInt32(InvoiceListDG.DataKeys[InvoiceListDG.SelectedIndex] is DBNull ? 0 : InvoiceListDG.DataKeys[InvoiceListDG.SelectedIndex]);
                    int nOrderId = (int)Session["invOrderValue"];
                    ds = aInvoiceDataAccess.GetAllInvoiceAdjustmentDetails(nID, nOrderId);

					dv = ds.Tables[0].DefaultView;
					if (Session["AdjSortField"] == null) Session["AdjSortField"] = "ADJUSTMENT_EFFECTIVE_DATE DESC";
					dv.Sort = (string)Session["AdjSortField"];

					//Show the total number of records in the set
					int totalAdjustments = ds.Tables[0].Rows.Count;
					ShowAdjustmentStats(totalAdjustments);

					InvoiceListAdjustmentDG.DataSource = dv;
					InvoiceListAdjustmentDG.DataBind();
				}
			}

			public void ShowAdjustmentStats(int nCount)
			{
				if (nCount == 0){lblAdjustmentRecords.Text = "No Records Found."; }
				else
				{
					int nStartOfSet = (nCount > 0) ? (InvoiceListAdjustmentDG.CurrentPageIndex*InvoiceListAdjustmentDG.PageSize+1) : 0;
					int nEndPage = (InvoiceListAdjustmentDG.CurrentPageIndex+1)*(InvoiceListAdjustmentDG.PageSize);
					int nEndOfSet = (nEndPage > nCount) ? nCount : nEndPage;
					lblAdjustmentRecords.Text = String.Format("Records: {0}-{1} of {2}", nStartOfSet, nEndOfSet ,nCount);
					InvoiceListAdjustmentDG.PagerStyle.Visible = (nCount <= InvoiceListAdjustmentDG.PageSize) ? false : true;
				}
			}

			public void InvoiceListAdjustmentDG_Page(Object sender, DataGridPageChangedEventArgs e)
			{
				//used by built-in pager.  CurrentPageIndex already set
				InvoiceListAdjustmentDG.CurrentPageIndex = e.NewPageIndex;
				lblAdjPageNumber.Text = "Page Number: " + (e.NewPageIndex+1);
				BindAdjustmentDetails();
			}

			public void doAddAdjustment(Object sender, DataGridCommandEventArgs e)
			{
				Label AccountID							= (Label)e.Item.FindControl("add_AccountID");
				Label OrderID							= (Label)e.Item.FindControl("add_OrderID");
				TextBox InternalComment					= (TextBox)e.Item.FindControl("add_InternalComment");
				TextBox Amount							= (TextBox)e.Item.FindControl("add_AdjustmentAmount");
				Label CampaignID						= (Label)e.Item.FindControl("add_CampaignID");
				HtmlInputHidden AdjustmentType			= (HtmlInputHidden)e.Item.FindControl("add_AdjustmentValue");
				//HtmlInputHidden AdjustmentAccountType	= (HtmlInputHidden)e.Item.FindControl("add_AccountValue");
				// get the Session UserID
				int nChangedBy = Convert.ToInt32(Session["Instance"]);

				if ( e.CommandName == "Add" )
				{
					if (Amount.Text.ToString().Trim().Length != 0)
					{
						try
						{	nAdjustmentID =
							aInvoiceDataAccess.AddInvoiceAdjustment(
								Convert.ToInt32(AccountID.Text.Trim()),
								Convert.ToInt32(OrderID.Text.Trim()),
								InternalComment.Text.ToString().Trim(),
								Convert.ToDecimal(Amount.Text.Trim()),
								Convert.ToInt32(CampaignID.Text.Trim()),
								Convert.ToInt32(AdjustmentType.Value.Trim()),
								//Convert.ToInt32(AdjustmentAccountType.Value.Trim()),
								Convert.ToInt32(nChangedBy));
						}
						catch (Exception exc)
						{
							// handle exception...
							Response.Write("Message: " + exc.Message+ "<br>");
						}
					}
					else
					{
						lblAdjustment.Text = "No Adjustment Amount entered";
					}
				}

				//string sUrl = "GLTransaction.aspx?InvoiceID=" + Convert.ToInt32(InvoiceListDG.DataKeys[InvoiceListDG.SelectedIndex]) + "&AdjustmentID="+nAdjustmentID +"&Amount=" + Convert.ToDecimal(Amount.Text.Trim());
				//string  sScript = "<script language =javascript> ";
				//		sScript += "window.open('" + sUrl + "',null,'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=100%,height=300,left=100,top=100');";
				//		sScript += "</script> ";
				//Response.Write(sScript);
				BindAdjustmentDetails();
			}

			public DataTable GetAdjustmentType()
			{
				DataSet ds = null;
				//get the filter value from the master Grid's DataKeys collection
				if( InvoiceListDG.SelectedIndex != -1 )
				{
					ds = aInvoiceDataAccess.GetInvoiceAdjustmentTypes();
				}
				return ds.Tables[0];
			}

			public void InvoiceListAdjustmentDG_Sort(Object sender, DataGridSortCommandEventArgs e)
			{
				Session["AdjSortField"] = (string)e.SortExpression;
				BindAdjustmentDetails();
			}

			public void Adjustment_PageIndexChanged(Object sender, EventArgs e)
			{
				if(ddlNumInvAdj.SelectedItem.Value.Trim() != "All")
				{
					InvoiceListAdjustmentDG.PageSize = Int32.Parse(ddlNumInvAdj.SelectedItem.Value.Trim());
				}
				else
				{
					InvoiceListAdjustmentDG.PageSize = 1000;
				}
				InvoiceListAdjustmentDG.CurrentPageIndex = 0;
				lblAdjPageNumber.Text = "Page Number: 1";
				BindAdjustmentDetails();
			}

			public void ShowAdjustmentDetails()
			{
				lblAdjPageNumber.Visible		= true;
				lblAdjustmentRecords.Visible	= true;
				lblShowAdj.Visible				= true;
				lblAdjTitle.Visible				= true;
				lblShowAdj.Text					= "Show";
				lblAdjTitle.Text				= "Adjustments";
				ddlNumInvAdj.Visible			= true;
				InvoiceListAdjustmentDG.Visible	= true;
			}

			public void HideAdjustmentDetails()
			{
				lblAdjPageNumber.Visible		= false;
				lblAdjustmentRecords.Visible	= false;
				lblShowAdj.Visible				= false;
				lblAdjTitle.Visible				= false;
				ddlNumInvAdj.Visible			= false;
				InvoiceListAdjustmentDG.Visible	= false;
			}

			public void InvoiceListAdjustmentDG_ItemDataBound(Object sender, DataGridItemEventArgs e)
			{
				if(e.Item.ItemType == ListItemType.Item)
				{
					Label lblAdjustmentID	= (Label)e.Item.FindControl("AdjustmentID");
					HyperLink h1			= (HyperLink)e.Item.Cells[0].FindControl("Hyperlink1");
					if(lblAdjustmentID.Text.Trim() == "")
					{
						// Don't show blank row
						h1.Visible = false;
					}
				}
				if ( e.Item.ItemType == ListItemType.Footer )
				{
					//The rendered name of the Account Type TextBox
					/*
					string sAcctTextBoxName  = e.Item.Cells[1].FindControl("add_AccountText").ClientID;
					string sAcctTextBoxValue = e.Item.Cells[1].FindControl("add_AccountValue").ClientID;
					HyperLink hl		= (HyperLink)e.Item.Cells[1].FindControl("lnk_AccountType");
					hl.NavigateUrl= "javascript:ddl_window=window.open('AcctTypeDropDownList.aspx?CtrlName=" + sAcctTextBoxName + "&CtrlValue=" + sAcctTextBoxValue+ "','ddl_window','width=295,height=50,left=300,top=250');ddl_window.focus();" ;
					*/

					//The rendered name of the Adjustment Type TextBox
					string sAdjTextBoxName  = e.Item.Cells[2].FindControl("add_AdjustmentText").ClientID;
					string sAdjTextBoxValue = e.Item.Cells[2].FindControl("add_AdjustmentValue").ClientID;
					HyperLink hl2		= (HyperLink)e.Item.Cells[2].FindControl("lnk_AdjustmentType");
					hl2.NavigateUrl= "javascript:ddl_window=window.open('AdjTypeDropDownList.aspx?CtrlName=" + sAdjTextBoxName + "&CtrlValue=" + sAdjTextBoxValue+ "','ddl_window','width=295,height=50,left=300,top=250');ddl_window.focus();" ;
				}
			}
		#endregion ADJUSTMENT

		#region PAYMENT
			public void BindPaymentDetails()
			{
				lblMsg.Text = "";
				//get the filter value from the master Grid's DataKeys collection
				if( InvoiceListDG.SelectedIndex != -1)
				{
                    int nID = Convert.ToInt32(InvoiceListDG.DataKeys[InvoiceListDG.SelectedIndex] is DBNull ? 0 : InvoiceListDG.DataKeys[InvoiceListDG.SelectedIndex]);
                    int nOrderId = (int)Session["invOrderValue"];
                    ds = aInvoiceDataAccess.GetPaymentDetails(nID, nOrderId);

					dv = ds.Tables[0].DefaultView;
					if (Session["PaymentSortField"] == null) Session["PaymentSortField"] = "PAYMENT_EFFECTIVE_DATE DESC";
					dv.Sort = (string)Session["PaymentSortField"];

					//Show the total number of records in the set
					int totalPayments = ds.Tables[0].Rows.Count;
					ShowPaymentStats(totalPayments);

					InvoiceListPaymentDG.DataSource = dv;
					InvoiceListPaymentDG.DataBind();
				}
			}

			public void ShowPaymentStats(int nCount)
			{
				if (nCount == 0){lblPaymentRecords.Text = "No Records Found."; }
				else
				{
					int nStartOfSet = (nCount > 0) ? (InvoiceListPaymentDG.CurrentPageIndex*InvoiceListPaymentDG.PageSize+1) : 0;
					int nEndPage = (InvoiceListPaymentDG.CurrentPageIndex+1)*(InvoiceListPaymentDG.PageSize);
					int nEndOfSet = (nEndPage > nCount) ? nCount : nEndPage;
					lblPaymentRecords.Text = String.Format("Records: {0}-{1} of {2}", nStartOfSet, nEndOfSet ,nCount);
					InvoiceListPaymentDG.PagerStyle.Visible = (nCount <= InvoiceListPaymentDG.PageSize) ? false : true;
				}
			}

			public void InvoiceListPaymentDG_Page(Object sender, DataGridPageChangedEventArgs e)
			{
				//used by built-in pager.  CurrentPageIndex already set
				InvoiceListPaymentDG.CurrentPageIndex = e.NewPageIndex;
				lblPaymentPageNumber.Text = "Page Number: " + (e.NewPageIndex+1);
				BindPaymentDetails();
			}

			public void doAddPayment(Object sender, DataGridCommandEventArgs e)
			{
				lblMsg.Text = "";
				bool bOk = true;
				int nID								= Convert.ToInt32(InvoiceListDG.DataKeys[InvoiceListDG.SelectedIndex] is DBNull ? 0 :InvoiceListDG.DataKeys[InvoiceListDG.SelectedIndex]);
				Label AccountID						= (Label)e.Item.FindControl("add_PaymentAccountID");
				Label OrderID						= (Label)e.Item.FindControl("add_PaymentOrderID");
				Label CampaignID					= (Label)e.Item.FindControl("add_PaymentCampaignID");
				//HtmlInputHidden PaymentAccountType	= (HtmlInputHidden)e.Item.FindControl("add_AccountValuePymt");
				HtmlInputHidden PaymentMethod		= (HtmlInputHidden)e.Item.FindControl("add_PaymentMethodValue");
				TextBox CheckNumber					= (TextBox)e.Item.FindControl("add_PaymentCheckNumber");
				TextBox CheckDate					= (TextBox)e.Item.FindControl("add_PaymentCheckDate");
				TextBox CheckPayer					= (TextBox)e.Item.FindControl("add_PaymentCheckPayer");
				TextBox CreditCardOwner				= (TextBox)e.Item.FindControl("add_CreditCardOwner");
				TextBox CreditCardAuthNumber		= (TextBox)e.Item.FindControl("add_CreditCardAuthNumber");
				TextBox Amount						= (TextBox)e.Item.FindControl("add_PaymentAmt");
				// get the Session UserID
				int nChangedBy = Convert.ToInt32(Session["Instance"]);

				if ( e.CommandName == "Add" )
				{
					if(CheckDate.Text.ToString().Trim().Length > 0)
					{
						bOk = ValidateCheckDate(CheckDate.Text);
					}

					if (CheckNumber.Text.ToString().Trim().Length == 0)
					{
						CheckNumber.Text = "-1";
					}
					if (CheckDate.Text.ToString().Trim().Length == 0)
					{
						CheckDate.Text = "1/1/1995";
					}
					if (CheckPayer.Text.ToString().Trim().Length == 0)
					{
						CheckPayer.Text = "";
					}
					if (CreditCardOwner.Text.ToString().Trim().Length == 0)
					{
						CreditCardOwner.Text = "";
					}
					if (CreditCardAuthNumber.Text.ToString().Trim().Length == 0)
					{
						CreditCardAuthNumber.Text = "";
					}

					if (Amount.Text.ToString().Trim().Length != 0 && bOk == true)
					{
						try
						{
							nPaymentID =
								aInvoiceDataAccess.AddInvoicePayment(
								Convert.ToInt32(AccountID.Text.Trim()),
								Convert.ToInt32(OrderID.Text.Trim()),
								Convert.ToInt32(CampaignID.Text.Trim()),
								//Convert.ToInt32(PaymentAccountType.Value.Trim()),
								Convert.ToInt32(PaymentMethod.Value.Trim()),
                        CheckNumber.Text.Substring(0, CheckNumber.Text.Length > 49 ? 49 : CheckNumber.Text.Length),
								Convert.ToDateTime(CheckDate.Text.Trim()),
								CheckPayer.Text.ToString(),
								CreditCardOwner.Text.ToString(),
								CreditCardAuthNumber.Text.ToString(),
								Convert.ToDecimal(Amount.Text.Trim()),
								Convert.ToInt32(nChangedBy));
						}
						catch (Exception exc)
						{
							// handle exception...
							Response.Write("Message: " + exc.Message+ "<br>");
						}

                        string sUrl = "GLPaymentTransaction.aspx?InvoiceID=" + Convert.ToInt32(InvoiceListDG.DataKeys[InvoiceListDG.SelectedIndex] is DBNull ? 0 : InvoiceListDG.DataKeys[InvoiceListDG.SelectedIndex]) + "&OrderID=" + Convert.ToInt32(OrderID.Text.Trim()) + "&PaymentID=" + nPaymentID + "&Amount=" + Convert.ToDecimal(Amount.Text.Trim());
						string  sScript = "<script language =javascript> ";
						sScript += "window.open('" + sUrl + "',null,'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=100%,height=300,left=100,top=100');";
						sScript += "</script> ";
						Response.Write(sScript);

						BindPaymentDetails();
					}

				}


			}

			public bool ValidateCheckDate(string strCheckDate)
			{
				bool bSuccess = false;

				if (IsDate(strCheckDate) == false)
				{
					return bSuccess;
				}
				else
				{
					//go back 180 days
					TimeSpan ts = new System.TimeSpan(-180, 0, 0, 0);
					DateTime aDateOld = DateTime.Now.Add(ts);
					string strThen = aDateOld.ToString("dd-MMM-yyyy").Trim();
					string strNow = DateTime.Now.ToString("dd-MMM-yyyy").Trim();

					//Compare the dates
					if (DateTime.Compare(Convert.ToDateTime(strThen), Convert.ToDateTime(strCheckDate)) > 0)
					{
						lblMsg.Text= "Cheque date more than 180 days old.";
						return bSuccess;
					}
					if (DateTime.Compare(Convert.ToDateTime(strCheckDate), Convert.ToDateTime(strNow))  > 0)
					{
						lblMsg.Text= "Cheque date cannot be greater than today's date.";
						return bSuccess;
					}
					bSuccess = true;
					return bSuccess;
				}
			}

			public bool IsDate(string strDate)
			{
				DateTime aDate;
				bool bSuccess = true;
				try
				{
					aDate = DateTime.Parse(strDate);
				}
				catch
				{
					bSuccess = false;
				}
				return bSuccess;
			}

			public DataTable GetPaymentMethod()
			{
				DataSet ds = null;
				//get the filter value from the master Grid's DataKeys collection
				if( InvoiceListDG.SelectedIndex != -1 )
				{
					ds = aInvoiceDataAccess.GetInvoicePaymentMethods();
				}
				return ds.Tables[0];
			}
			public void InvoiceListPaymentDG_Sort(Object sender, DataGridSortCommandEventArgs e)
			{
				Session["PaymentSortField"] = (string)e.SortExpression;
				BindPaymentDetails();
			}
			public void Payment_PageIndexChanged(Object sender, EventArgs e)
			{
				if(ddlNumInvPay.SelectedItem.Value.Trim() != "All")
				{
					InvoiceListPaymentDG.PageSize = Int32.Parse(ddlNumInvPay.SelectedItem.Value.Trim());
				}
				else
				{
					InvoiceListPaymentDG.PageSize = 1000;
				}
				InvoiceListPaymentDG.CurrentPageIndex = 0;
				lblPaymentPageNumber.Text = "Page Number: 1";
				BindPaymentDetails();
			}

			public void ShowPaymentDetails()
			{
				lblPaymentPageNumber.Visible	= true;
				lblPaymentRecords.Visible		= true;
				lblShowPayment.Visible			= true;
				lblPayTitle.Visible				= true;
				lblShowPayment.Text				= "Show";
				lblPayTitle.Text				= "Payments";
				ddlNumInvPay.Visible			= true;
				InvoiceListPaymentDG.Visible	= true;
			}

			public void HidePaymentDetails()
			{
				lblPaymentPageNumber.Visible	= false;
				lblPaymentRecords.Visible		= false;
				lblShowPayment.Visible			= false;
				lblPayTitle.Visible				= false;
				ddlNumInvPay.Visible			= false;
				InvoiceListPaymentDG.Visible	= false;
			}
			public void InvoiceListPaymentDG_ItemDataBound(Object sender, DataGridItemEventArgs e)
			{
				if(e.Item.ItemType == ListItemType.Item)
				{
					Label lblPaymentID	= (Label)e.Item.FindControl("PaymentID");
					HyperLink h1		= (HyperLink)e.Item.Cells[0].FindControl("Hyperlink2");
					if(lblPaymentID.Text.Trim() == "")
					{
						// Don't show blank row
						h1.Visible = false;
					}
				}
				if ( e.Item.ItemType == ListItemType.Footer )
				{
					/*//The rendered name of the Account Type TextBox
					string sAcctTextBoxName  = e.Item.Cells[1].FindControl("add_AccountTextPymt").ClientID;
					string sAcctTextBoxValue = e.Item.Cells[1].FindControl("add_AccountValuePymt").ClientID;
					HyperLink hl		= (HyperLink)e.Item.Cells[1].FindControl("lnk_AccountTypePymt");
					hl.NavigateUrl= "javascript:ddl_window=window.open('AcctTypeDropDownList.aspx?CtrlName=" + sAcctTextBoxName + "&CtrlValue=" + sAcctTextBoxValue+ "','ddl_window','width=295,height=50,left=300,top=250');ddl_window.focus();" ;
					*/
					//The rendered name of the Payment Method TextBox
					string sPaymentTextBoxName  = e.Item.Cells[2].FindControl("add_PaymentMethodText").ClientID;
					string sPaymentTextBoxValue = e.Item.Cells[2].FindControl("add_PaymentMethodValue").ClientID;
					HyperLink hl2		= (HyperLink)e.Item.Cells[2].FindControl("lnk_PaymentMethod");
					hl2.NavigateUrl= "javascript:ddl_window=window.open('PaymentMethodDropDownList.aspx?CtrlName=" + sPaymentTextBoxName + "&CtrlValue=" + sPaymentTextBoxValue + "','ddl_window','width=375,height=50,left=300,top=250');ddl_window.focus();" ;
				}
			}
		#endregion PAYMENT

		#region PRODUCT
		public void BindProductDetails()
		{
			//get the filter value from the master Grid's DataKeys collection
			if( InvoiceListDG.SelectedIndex != -1)
			{
				int nID  = Convert.ToInt32(InvoiceListDG.DataKeys[InvoiceListDG.SelectedIndex]);
                int nOrderId = (int)Session["invOrderValue"];
				ds = aInvoiceDataAccess.GetAllInvoiceProductDetails(nID, nOrderId);

				dv = ds.Tables[0].DefaultView;
				if (Session["ProdSortField"] == null) Session["ProdSortField"] = "ProductName";
				dv.Sort = (string)Session["ProdSortField"];

				//Show the total number of records in the set
				int totalProducts = ds.Tables[0].Rows.Count;
				ShowProductStats(totalProducts);

				ProductDetailsDG.DataSource = dv;
				ProductDetailsDG.DataBind();
			}
		}

		public void ProductDetailsDG_Page(Object sender, DataGridPageChangedEventArgs e)
		{
			//used by built-in pager.  CurrentPageIndex already set
			ProductDetailsDG.CurrentPageIndex = e.NewPageIndex;
			BindProductDetails();
		}

		public void ProductDetailsDG_Sort(Object sender, DataGridSortCommandEventArgs e)
		{
			Session["ProdSortField"] = (string)e.SortExpression;
			BindProductDetails();
		}

		public void Product_PageIndexChanged(Object sender, EventArgs e)
		{
			if(ddlNumInvProduct.SelectedItem.Value.Trim() != "All")
			{
				ProductDetailsDG.PageSize = Int32.Parse(ddlNumInvProduct.SelectedItem.Value.Trim());
			}
			else
			{
				ProductDetailsDG.PageSize = 1000;
			}
			ProductDetailsDG.CurrentPageIndex = 0;
			lblProductPageNumber.Text = "Page Number: 1";
			BindProductDetails();
		}

		public void ShowProductStats(int nCount)
		{
			if (nCount == 0){lblProduct.Text = "No Records Found."; }
			else
			{
				int nStartOfSet = (nCount > 0) ? (ProductDetailsDG.CurrentPageIndex*ProductDetailsDG.PageSize+1) : 0;
				int nEndPage = (ProductDetailsDG.CurrentPageIndex+1)*(ProductDetailsDG.PageSize);
				int nEndOfSet = (nEndPage > nCount) ? nCount : nEndPage;
				lblProduct.Text = String.Format("Records: {0}-{1} of {2}", nStartOfSet, nEndOfSet ,nCount);
				ProductDetailsDG.PagerStyle.Visible = (nCount <= ProductDetailsDG.PageSize) ? false : true;
			}
		}
		public void ShowProductDetails()
		{
			lblProductPageNumber.Visible	= true;
			lblProduct.Visible				= true;
			lblShowProduct.Visible			= true;
			lblProductsTitle.Visible		= true;
			lblShowProduct.Text				= "Show";
			lblProductsTitle.Text			= "Invoice Details";
			ddlNumInvProduct.Visible		= true;
			ProductDetailsDG.Visible		= true;
		}

		public void HideProductDetails()
		{
			lblProductPageNumber.Visible	= false;
			lblProduct.Visible				= false;
			lblShowProduct.Visible			= false;
			lblProductsTitle.Visible		= false;
			ddlNumInvProduct.Visible		= false;
			ProductDetailsDG.Visible		= false;
		}
		#endregion PRODUCT

		#region FUNCTIONS
		public DataTable GetAccountType()
		{
			DataSet ds = null;
			//get the filter value from the master Grid's DataKeys collection
			if( InvoiceListDG.SelectedIndex != -1 )
			{
				ds = aInvoiceDataAccess.GetInvoiceAccountTypes();
			}
			return ds.Tables[0];
		}

		public int GetInvID(int x)
		{
			invIdValue = x;
			return x;
		}

		public int GetAdjID(int x)
		{
			invAdjValue = x;
			return x;
		}

		public int GetActID(int x)
		{
			invActValue = x;
			return x;
		}

		public int GetOrderID(int x)
		{
			invOrderValue = x;
			return x;
		}

		public int GetCampaignID(int x)
		{
			invCampaignValue = x;
			return x;
		}

		public void add_ChequeDate_DayRender(Object source, DayRenderEventArgs e)
		{
			if (e.Day.Date.ToString("d") == DateTime.Now.ToString("d"))
			{
				e.Cell.BackColor = System.Drawing.Color.LightGray;
			}

		}

		public void add_EffectiveDate_DayRender(Object source, DayRenderEventArgs e)
		{
			if (e.Day.Date.ToString("d") == DateTime.Now.ToString("d"))
			{
				e.Cell.BackColor = System.Drawing.Color.LightGray;
			}

		}

		#endregion

		public void InvoiceListDG_SelectedIndexChanged(object sender, System.EventArgs e)
		{

		}
	}
}
