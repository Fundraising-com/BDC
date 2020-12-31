using System;
using Business.Objects;
using Common;



namespace QSPFulfillment.Finance
{
	public partial class StatementPrintList :  FinancePage
	{
		
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			NullReferenceException exception;

			try 
			{
				if(PrintChecked) 
				{
					if(GroupStatementBatchReport != null) 
					{
						GenerateChecked();
					} 
					else 
					{
						exception = new NullReferenceException();
						exception.Source = "StatementReport";
						ApplicationError.ManageError(exception);
						AddScriptClose();
					}
				} 
				else 
				{
					if(FromDate != new DateTime(1995, 1, 1) && ToDate != new DateTime(1995, 1, 1)&& CampaignID!=0 ) 
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

		private int AccountID 
		{
			get 
			{
				int AccountID = 0;

				try 
				{
					AccountID = Convert.ToInt32(Request.QueryString["AccountID"]);
				} 
				catch { }

				return AccountID;
			}
		}
		
		private int CampaignID 
		{
			get 
			{
				int CampaignID = 0;

				try 
				{
					CampaignID = Convert.ToInt32(Request.QueryString["CampaignID"]);
				} 
				catch { }

				return CampaignID;
			}
		}

		private int Over100
		{
			get 
			{
				int Over100 = 0;

				try 
				{
					Over100 = 0;
				} 
				catch { }

				return Over100;
			}
		}


		private DateTime FromDate 
		{
			get 
			{
				DateTime fromDate = new DateTime(1995, 1, 1);

				try 
				{
					fromDate = Convert.ToDateTime(Request.QueryString["FromDate"]);
				} 
				catch { }

				return fromDate;
			}
		}

		private DateTime ToDate 
		{
			get 
			{
				DateTime toDate = new DateTime(1995, 1, 1);

				try 
				{
					toDate = Convert.ToDateTime(Request.QueryString["ToDate"]);
				} 
				catch { }

				return toDate;
			}
		}

		
		private StatementBatchReport GroupStatementBatchReport 
		{
			get 
			{
				return (StatementBatchReport) Session["GroupStatementBatchReport"];
			}
			set 
			{
				Session["GroupStatementBatchReport"] = value;
			}
		}

		private void AddScriptClose() 
		{
			this.RegisterStartupScript("AddScriptClose", "<script language=\"javascript\"> self.close(); </script>");
		}

		private void GenerateList() 
		{
			StatementBatchReport report = new StatementBatchReport();
			//string a = QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID;
			
			ShowResults(report.Generate(AccountID,CampaignID, FromDate, ToDate, Over100));
		}

		private void GenerateChecked() 
		{
			ShowResults(GroupStatementBatchReport.Generate(false));

			Session.Remove("GroupStatementBatchReport");
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
