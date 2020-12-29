using System;
using System.Data;

namespace GA.BDC.Shared.Data.Repositories
{
   public interface IReportsRepository
   {
      DataTable ExecuteSpider(int? saleid, int? leadId, int? promotionId, int? scratchbookId, int? partnerId, string stateCode, int? consultantId, string zipCode, string country, string dayPhone, string eveningPhone, string email, int? organizationTypeId, decimal? totalAmount, int? productClassDescriptionId, DateTime? actualShipDateStart, DateTime? actualShipDateEnd, DateTime? fundraiserDateStart, DateTime? fundraiserDateEnd, DateTime? saleConfirmationDateStart, DateTime? saleConfirmationDateEnd, DateTime? leadEntryDateStart, DateTime? leadEntryDateEnd, int? groupTypeId);
      DataTable ExecuteRepeatedBusiness(DateTime frameOneDateInit, DateTime frameOneDateEnd, DateTime frameTwoDateInit, DateTime frameTwoDateEnd, bool showFCs, string countryCode, string stateCode);
      DataTable ExecuteRepeatedBusinessDetail(DateTime frameOneDateInit, DateTime frameOneDateEnd, DateTime frameTwoDateInit, DateTime frameTwoDateEnd, bool showFCs, string countryCode, string stateCode);
	   DataTable ExecuteGrossProfit(DateTime frameOneDateInit, DateTime frameOneDateEnd, DateTime frameTwoDateInit, DateTime frameTwoDateEnd);
        DataTable ExecuteReportSalesToProcess( DateTime? saleConfirmationDateStart, DateTime? saleConfirmationDateEnd);
        DataTable ExecuteReportProductList(int? productClassDescriptionId);
        DataTable ExecuteReportCustomerList(DateTime? saleConfirmationDateStart, DateTime? saleConfirmationDateEnd);

        DataTable ExecuteReportTraditionalConfirmedSalesByProductClass(DateTime? start_date, DateTime? end_date);

    }
}
