using System;
using Business.ReportService;
using Common.TableDef;

namespace Business.Reports
{
	/// <summary>
	/// Summary description for CampaignConfirmationAgreementReport.
	/// </summary>
	public class OEFReport : Report
	{
		private const string REPORT_NAME = "OrderEntryFollowupReport";
		
		public OEFReport() : base() { }

		protected override string ReportName
		{
			get
			{
				string reportName = REPORT_NAME;

				return reportName;
			}
		}
		

		public ParameterFieldReference OrderIDParameter 
		{
			get 
			{
				return ReportParameters[0];
			}
		}


		protected override void InitializeReportParameters()
		{
			ReportParameters = new ParameterFieldReference[1];

			ReportParameters[0] = new ParameterFieldReference();
			ReportParameters[0].ParameterName = "OrderID";

		}

		public override byte[] Generate(System.Data.DataRow row)
		{
			InvoiceDataSet.INVOICERow invoiceRow = row as InvoiceDataSet.INVOICERow;
			
			if (invoiceRow.Type =="Mag Only")
			{ 
				return base.Generate (row);
			}
			else
			{
				return null;
			}
		}
		
	}
}
