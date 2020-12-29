using System;
using System.Data;
using GA.BDC.Data.Fundraising.EFundraisingProd.Tables;
using GA.BDC.Shared.Data.Repositories;
using Dapper;

namespace GA.BDC.Data.Fundraising.EFundraisingProd.StoredProcedures
{
   public class ReportsRepository : IReportsRepository
   {
      private readonly DataProvider _dataProvider;
      public ReportsRepository(DataProvider dataProvider)
      {
         _dataProvider = dataProvider;
      }

        public DataTable ExecuteRepeatedBusiness(DateTime frameOneDateInit, DateTime frameOneDateEnd, DateTime frameTwoDateInit, DateTime frameTwoDateEnd, bool showFCs, string countryCode, string regionCode)
        {
            var dataReader = _dataProvider.Database.Connection.ExecuteReader("exec report_repeated_business @frameOneDateInit, @frameOneDateEnd, @frameTwoDateInit, @frameTwoDateEnd, @showFCs, @countryCode, @regionCode",
               new
               {
                   frameOneDateInit, frameOneDateEnd, frameTwoDateInit, frameTwoDateEnd, showFCs, countryCode,regionCode
               },
               _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            var result = new DataTable();
            result.Load(dataReader);
            return result;
        }

      public DataTable ExecuteRepeatedBusinessDetail(DateTime frameOneDateInit, DateTime frameOneDateEnd, DateTime frameTwoDateInit, DateTime frameTwoDateEnd, bool showFCs, string countryCode, string regionCode)
      {
         var dataReader = _dataProvider.Database.Connection.ExecuteReader("exec report_repeated_business_detail @frameOneDateInit, @frameOneDateEnd, @frameTwoDateInit, @frameTwoDateEnd, @showFCs, @countryCode, @regionCode",
               new
               {
                  frameOneDateInit,
                  frameOneDateEnd,
                  frameTwoDateInit,
                  frameTwoDateEnd,
                  showFCs,
                  countryCode,
                  regionCode
               },
               _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         var result = new DataTable();
         result.Load(dataReader);
         return result;
      }

	   public DataTable ExecuteGrossProfit(DateTime frameOneDateInit, DateTime frameOneDateEnd, DateTime frameTwoDateInit, DateTime frameTwoDateEnd)
	   {
		   var dataReader = _dataProvider.Database.Connection.ExecuteReader("exec report_gross_profit @frameOneDateInit, @frameOneDateEnd, @frameTwoDateInit, @frameTwoDateEnd",
			   new
			   {
				   frameOneDateInit, frameOneDateEnd, frameTwoDateInit, frameTwoDateEnd
			   },
			   _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
		   var result = new DataTable();
		   result.Load(dataReader);
		   return result;
	   }

	   public DataTable ExecuteSpider(int? saleid, int? leadId, int? promotionId, int? scratchbookId, int? partnerId, string stateCode,
         int? consultantId, string zipCode, string country, string dayPhone, string eveningPhone, string email,
         int? organizationTypeId, decimal? totalAmount, int? productClassDescriptionId, DateTime? actualShipDateStart,
         DateTime? actualShipDateEnd, DateTime? fundraiserDateStart, DateTime? fundraiserDateEnd,
         DateTime? saleConfirmationDateStart, DateTime? saleConfirmationDateEnd, DateTime? leadEntryDateStart,
         DateTime? leadEntryDateEnd, int? groupTypeId)
      {
         var dataReader = _dataProvider.Database.Connection.ExecuteReader("exec report_spider_leads_sales @saleId, @leadId, @promotionId, @scratchbookId, @partnerId, @stateCode, @consultantId, @zipCode, @country, @dayPhone, @eveningPhone, @email, @organizationTypeId, @totalAmount, @productClassDescriptionId, @actualShipDateStart, @actualShipDateEnd, @fundraiserDateStart, @fundraiserDateEnd, @saleConfirmationDateStart, @saleConfirmationDateEnd, @leadEntryDateStart, @leadEntryDateEnd, @groupTypeId", 
            new
            {
               saleid, leadId, promotionId, scratchbookId, partnerId, stateCode, consultantId, zipCode, country, dayPhone, eveningPhone, email,
               organizationTypeId, totalAmount, productClassDescriptionId, actualShipDateStart, actualShipDateEnd, fundraiserDateStart, fundraiserDateEnd,
               saleConfirmationDateStart, saleConfirmationDateEnd, leadEntryDateStart, leadEntryDateEnd, groupTypeId               
            },
            _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         var result = new DataTable();
         result.Load(dataReader);
         return result;
      }


        public DataTable ExecuteReportSalesToProcess(DateTime? saleConfirmationDateStart, DateTime? saleConfirmationDateEnd)
        {
            var dataReader = _dataProvider.Database.Connection.ExecuteReader("exec report_sales_to_process  @saleConfirmationDateStart, @saleConfirmationDateEnd",
               new
               {
                   saleConfirmationDateStart,
                   saleConfirmationDateEnd,
                  
               },
               _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            var result = new DataTable();
            result.Load(dataReader);
            return result;
        }

        public DataTable ExecuteReportProductList(int? productClassDescriptionId)
        {
            var dataReader = _dataProvider.Database.Connection.ExecuteReader("exec report_Product_List  @productClassDescriptionId",
               new
               {
                   productClassDescriptionId

               },
               _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            var result = new DataTable();
            result.Load(dataReader);
            return result;
        }


        public DataTable ExecuteReportCustomerList(DateTime? saleConfirmationDateStart, DateTime? saleConfirmationDateEnd)
        {
            var dataReader = _dataProvider.Database.Connection.ExecuteReader("exec report_customers_to_process @saleConfirmationDateStart, @saleConfirmationDateEnd",
               new
               {
                   saleConfirmationDateStart,
                   saleConfirmationDateEnd,

               },
               _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            var result = new DataTable();
            result.Load(dataReader);
            return result;
        }


        public DataTable ExecuteReportTraditionalConfirmedSalesByProductClass(DateTime? start_date, DateTime? @end_date)
        {
            var dataReader = _dataProvider.Database.Connection.ExecuteReader("exec efr_rpt_partner_confirmed_report @start_date, @end_date",
               new
               {
                   start_date,
                   end_date,

               },
               _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            var result = new DataTable();
            result.Load(dataReader);
            return result;
        }


    }
}
