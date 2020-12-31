using System;
using Business.ReportService;
using Common.TableDef;

namespace Business.Reports
{
	/// <summary>
	/// Summary description for CampaignConfirmationAgreementReport.
	/// </summary>
	public class PrintInvoiceReport : Report
	{
		private const string REPORT_NAME = "PrintInvoice";

		private bool isFrench = false;

		public PrintInvoiceReport() : base() { }

		protected override string ReportName
		{
			get
			{
				string reportName = REPORT_NAME;

				/*if(IsFrench) 
				{
					reportName += "French";
				}*/

				return reportName;
			}
		}

		public bool IsFrench 
		{
			get 
			{
				return isFrench;
			}
			set 
			{
				isFrench = value;
			}
		}

		public ParameterFieldReference InvoiceIDParameter 
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
			ReportParameters[0].ParameterName = "InvoiceID";
		}

		public override byte[] Generate(System.Data.DataRow row)
		{
			InvoiceDataSet.INVOICERow invoiceRow = row as InvoiceDataSet.INVOICERow;

			this.IsFrench = (invoiceRow != null && invoiceRow.Lang == "FR");

			return base.Generate (row);
		}

	}
}
