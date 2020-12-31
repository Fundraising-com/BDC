using System;
using Business.Objects;
using Common;



namespace QSPFulfillment.Finance
{
	///<summary>InvoiceListPrint</summary>
	public partial class InvoicePrintWholeList :  FinancePage
	{
		
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			NullReferenceException exception;

			try 
			{
				if(PrintChecked) 
				{
					if(InvoiceReport != null) 
					{
						GenerateChecked();
					} 
					else 
					{
						exception = new NullReferenceException();
						exception.Source = "InvoiceReport";
						ApplicationError.ManageError(exception);
						AddScriptClose();
					}
				} 
				else 
				{
					if(IsPrinted != String.Empty) 
					{
						GenerateList();
					} 
					else 
					{
						exception = new NullReferenceException();
						exception.Source = "List";
						ApplicationError.ManageError(exception);
						AddScriptClose();
					}
				}
			} 
			catch(MessageException ex)
			{
				this.SetPageError(ex);
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		private bool PrintChecked 
		{
			get 
			{
				bool printChecked = false;

				try 
				{
					printChecked = Convert.ToBoolean(Request.QueryString["PrintChecked"]);
				} 
				catch { }

				return printChecked;
			}
		}

		private string AccountName 
		{
			get 
			{
				string accountName = String.Empty;

				if(Request.QueryString["AccountName"] != null) 
				{
					accountName = Request.QueryString["AccountName"].ToString();
				}

				return accountName;
			}
		}
		private int InvoiceID 
		{
			get 
			{
				int invoiceID = 0;

				try 
				{
					invoiceID = Convert.ToInt32(Request.QueryString["InvoiceID"]);
				} 
				catch { }

				return invoiceID;
			}
		}
		private int? FiscalYear
		{
			get 
			{
                int? fiscalYear = null;

                if (Request.QueryString["FiscalYear"] != null)
                {
                    int result;
                    bool isParseSuccessful = Int32.TryParse(Request.QueryString["FiscalYear"], out result);

                    if (isParseSuccessful)
                    {
                        fiscalYear = result;
                    }
                }

                return fiscalYear;
			}
		}
		private string IsPrinted
		{
			get 
			{
				string isPrinted = String.Empty;

				if (Request.QueryString["IsPrinted"] != null) 
				{
					isPrinted = Request.QueryString["IsPrinted"].ToString();
				}

				return isPrinted;
			}
		}
        private bool ShowOnlyAccountsInOwing
        {
            get
            {
                bool result = false;
                string showInvoiceType = String.Empty;

                if (Request.QueryString["ShowInvoiceType"] != null)
                {
                    showInvoiceType = Request.QueryString["ShowInvoiceType"].ToString();

                    if (showInvoiceType == "Owing")
                    {
                        result = true;
                    }
                }

                return result;
            }
        }
        private bool IncludeOEFUReport
        {
            get
            {
                bool IncludeOEFUReport = false;

                try
                {
                    IncludeOEFUReport = Convert.ToBoolean(Request.QueryString["IsIncludeOEFUReport"]);
                }
                catch { }

                return IncludeOEFUReport;
            }
        }
        private bool ShowNonPrinted
        {
           get
           {
              bool ShowNonPrinted = false;

              try
              {
                 ShowNonPrinted = Convert.ToBoolean(Request.QueryString["ShowNonPrinted"]);
              }
              catch { }

              return ShowNonPrinted;
           }
        }
		private InvoiceBatchReport InvoiceReport 
		{
			get 
			{
				return (InvoiceBatchReport) Session["InvoiceReport"];
			}
			set 
			{
				Session["InvoiceReport"] = value;
			}
		}

		private void AddScriptClose() 
		{
			this.RegisterStartupScript("AddScriptClose", "<script language=\"javascript\"> self.close(); </script>");
		}

		private void GenerateList() 
		{
			string fmId = QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID;

            InvoiceBatchReport report = new InvoiceBatchReport(IncludeOEFUReport);
            ShowResults(report.Generate(AccountName, InvoiceID, FiscalYear, IsPrinted, fmId, ShowOnlyAccountsInOwing, ShowNonPrinted));
		}

		private void GenerateChecked() 
		{
			ShowResults(InvoiceReport.Generate(false));

			Session.Remove("InvoiceReport");
		}

		private void ShowResults(byte[] results) 
		{
			Response.ClearContent();
			Response.AppendHeader("content-length", results.Length.ToString());
			Response.ContentType = "application/pdf";
			Response.BinaryWrite(results);
			Response.Flush();
			Response.Close();
		}

				
	}
}
