using System;
using System.Data;
using Business.Reports;
using Common;
using Common.TableDef;
using DAL;
using dataSetRef = Common.TableDef.InvoiceDataSet;
using dataAccessRef = DAL.InvoiceData;





namespace Business.Objects
{
    /// <summary>
    /// Summary description for MagNetSalesBatchReport.
    /// </summary>
    public class InvoiceBatchReport : BatchReportsObject
    {
        dataAccessRef dataAccess = new dataAccessRef();
        dataSetRef dtsDataSet = new dataSetRef();

        private string accountName = String.Empty;
        private int invoiceID = 0;
        private int accountID = 0;
        private int campaignID = 0;
        private int? fiscalYear;
        private bool showOnlyAccountsInOwing = false;
        private bool showNonPrinted = false;
        private string isPrinted = String.Empty;
        private string fmID = String.Empty;


        public InvoiceBatchReport(bool IncludeOEFUReport) : base(IncludeOEFUReport) { }     

        protected override DataSet baseDataSet
        {
            get
            {
                return dtsDataSet;
            }
        }

        public dataSetRef dataSet
        {
            get
            {
                return dtsDataSet;
            }
        }

        internal override string DefaultTableName
        {
            get
            {
                return dtsDataSet.INVOICE.TableName;
            }
        }

        protected override DBTableOperation DataAccessReference
        {
            get
            {
                return dataAccess;
            }
        }

        protected override string FileNameExpression
        {
            get
            {
                return dtsDataSet.INVOICE.Invoice_IDColumn.ColumnName;
            }
        }

        protected override void GetDataList()
        {
            Search(accountName, invoiceID, accountID, campaignID, fiscalYear, isPrinted, showOnlyAccountsInOwing, showNonPrinted);
        }

        protected override void InitializeReports()
        {
            if (includeOEFUReport)
            {
                Reports = new Business.Reports.Report[2];

                Reports[0] = new Business.Reports.PrintInvoiceReport();
                Reports[1] = new Business.Reports.OEFReport();
            }
            else
            {
                Reports = new Business.Reports.Report[1];

                Reports[0] = new Business.Reports.PrintInvoiceReport();
            }


            MapReportParameters();
        }

        private void MapReportParameters()
        {
            MapPrintInvoiceReportParameters();
            if (includeOEFUReport)
            {
                MapOEFReportParameters();
            }
        }

        private void MapPrintInvoiceReportParameters()
        {
            PrintInvoiceReport report = (PrintInvoiceReport)Reports[0];

            report.InvoiceIDParameter.FieldAlias = dtsDataSet.INVOICE.Invoice_IDColumn.ColumnName;

        }

        private void MapOEFReportParameters()
        {

            OEFReport oReport = (OEFReport)Reports[1];

            oReport.OrderIDParameter.FieldAlias = dtsDataSet.INVOICE.Order_IDColumn.ColumnName;
        }


        public virtual byte[] Generate(string accountName, int invoiceID, int? fiscalYear, string isPrinted, string fmId, bool showOnlyAccountsInOwing, bool showNonPrinted)
        {
            this.accountName = accountName;
            this.invoiceID = invoiceID;
            this.fiscalYear = fiscalYear;
            this.isPrinted = isPrinted;
            this.fmID = fmId;
            this.showOnlyAccountsInOwing = showOnlyAccountsInOwing;
            this.showNonPrinted = showNonPrinted;

            return base.Generate();
        }



        public void Search(string accountName, int invoiceID, int accountID, int campaignID, int? fiscalYear, string isPrinted, bool showOnlyAccountsInOwing, bool showNonPrinted)
        {
            if (isPrinted.ToLower() == "all")
            {
                isPrinted = "";
            }

            try
            {
                dataAccess.SelectAllToPrint(dtsDataSet, DefaultTableName, accountName, invoiceID, accountID, campaignID, fiscalYear, isPrinted, this.fmID, showOnlyAccountsInOwing, showNonPrinted);

                //if (showInvoiceType == "Owing")
                //{
                //    // Show only invoices in owing (filter those in 0)
                //    InvoiceDataSet.INVOICEDataTable table = (InvoiceDataSet.INVOICEDataTable)dtsDataSet.INVOICE.Copy();

                //    foreach (InvoiceDataSet.INVOICERow row in table)
                //    {
                //        if (row.Invoice_Amount <= 0)
                //        {
                //            dtsDataSet.INVOICE.RemoveINVOICERow(row);
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                ManageError(ex);
            }
        }
    }
}
