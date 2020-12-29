using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using GA.BDC.Data;
//using GA.BDC.Console.PAPFeedback.TaskExecutor.Properties;


namespace GA.BDC.PAP.Data.GpfRpcObjects
{
   [Serializable]
   internal class GpfRpcFormRequest
   {
      public GpfRpcFormRequest(pap_get_sales_to_be_processedResult sale, string userId, string commId, string commTypeId)
      {
         C = "Pap_Merchants_Transaction_TransactionsForm".ToString();
         M = "add".ToString();
        
        fields = new List<String[]> {                 
                                       new String[]{ "name","value"},
                                       new String[]{"Id",""},
                                       new String[]{"rstatus","P"},
                                       new String[]{"dateinserted",DateTime.Now.AddHours(-3).ToString("u")},
                                       new String[]{"totalcost",sale.total_amount.ToString()},
                                       new String[]{"channel",""},
                                       new String[]{"fixedcost",""},
                                       new String[]{"multiTier","N"},
                                       new String[]{"commtypeid",commTypeId}, // primary key
                                       new String[]{"bannerid",""},
                                       new String[]{"payoutstatus","U"},
                                       new String[]{"countrycode",""},
                                       new String[]{"userid",userId}, // primary key
                                       new String[]{"campaignid",commId},
                                       new String[]{"parenttransid",""},
                                       new String[]{"commission",""},
                                       new String[]{"tier","1"},
                                       new String[]{"commissionTag","Commissions are computed automatically"},
                                       new String[]{"orderid",sale.sales_id.ToString()},
                                       new String[]{"productid",sale.product_category_code},
                                       new String[]{"data1",sale.lead_id.ToString()},
                                       new String[]{"data2",""},
                                       new String[]{"data3",""},
                                       new String[]{"data4",""},
                                       new String[]{"data5",""},
                                       new String[]{"trackmethod","U"},
                                       new String[]{"refererurl",""},
                                       new String[]{"ip",""},
                                       new String[]{"firstclicktime",""},
                                       new String[]{"firstclickreferer",""},
                                       new String[]{"firstclickip",""},
                                       new String[]{"firstclickdata1",""},
                                       new String[]{"firstclickdata2",""},
                                       new String[]{"lastclicktime",""},
                                       new String[]{"lastclickreferer",""},
                                       new String[]{"lastclickip",""},
                                       new String[]{"lastclickdata1",""},
                                       new String[]{"lastclickdata2",""},
                                       new String[]{"systemnote",""},
                                       new String[]{"Landed order inserted by GA.BDC.Console.PAPFeedback"}};
      }

      public GpfRpcFormRequest(pap_get_sales_to_be_processedResult sale, string userId, string commId, string commTypeId, double commissionAmount)
      {
         C = "Pap_Merchants_Transaction_TransactionsForm".ToString();
         M = "add".ToString();

         fields = new List<String[]> {                 
                                       new String[]{ "name","value"},
                                       new String[]{"Id",""},
                                       new String[]{"rstatus","P"},
                                       new String[]{"dateinserted",DateTime.Now.AddHours(-3).ToString("u")},
                                       new String[]{"totalcost",sale.total_amount.ToString()},
                                       new String[]{"channel",""},
                                       new String[]{"fixedcost",""},
                                       new String[]{"multiTier","N"},
                                       new String[]{"commtypeid",commTypeId}, // primary key
                                       new String[]{"bannerid",""},
                                       new String[]{"payoutstatus","U"},
                                       new String[]{"countrycode",""},
                                       new String[]{"userid",userId}, // primary key
                                       new String[]{"campaignid",commId},
                                       new String[]{"parenttransid",""},
                                       new String[]{"commission",commissionAmount.ToString()},
                                       new String[]{"tier","1"},
                                       new String[]{"commissionTag",""},
                                       new String[]{"orderid",sale.sales_id.ToString()},
                                       new String[]{"productid",sale.product_category_code},
                                       new String[]{"data1",sale.lead_id.ToString()},
                                       new String[]{"data2",""},
                                       new String[]{"data3",""},
                                       new String[]{"data4",""},
                                       new String[]{"data5",""},
                                       new String[]{"trackmethod","U"},
                                       new String[]{"refererurl",""},
                                       new String[]{"ip",""},
                                       new String[]{"firstclicktime",""},
                                       new String[]{"firstclickreferer",""},
                                       new String[]{"firstclickip",""},
                                       new String[]{"firstclickdata1",""},
                                       new String[]{"firstclickdata2",""},
                                       new String[]{"lastclicktime",""},
                                       new String[]{"lastclickreferer",""},
                                       new String[]{"lastclickip",""},
                                       new String[]{"lastclickdata1",""},
                                       new String[]{"lastclickdata2",""},
                                       new String[]{"systemnote",""},
                                       new String[]{"merchantnote","Landed order inserted by GA.BDC.Console.PAPFeedback"}};
      }

      public GpfRpcFormRequest(pap_get_sales_to_be_processedResult sale, string userId, string commId, string commTypeId, double commissionAmount, string productDescription)
      {
         C = "Pap_Merchants_Transaction_TransactionsForm".ToString();
         M = "add".ToString();

         fields = new List<String[]> {                 
                                       new String[]{ "name","value"},
                                       new String[]{"Id",""},
                                       new String[]{"rstatus","P"},
                                       new String[]{"dateinserted",DateTime.Now.AddHours(-3).ToString("u")},
                                       new String[]{"totalcost",sale.total_amount.ToString()},
                                       new String[]{"channel",""},
                                       new String[]{"fixedcost",""},
                                       new String[]{"multiTier","N"},
                                       new String[]{"commtypeid",commTypeId}, // primary key
                                       new String[]{"bannerid",""},
                                       new String[]{"payoutstatus","U"},
                                       new String[]{"countrycode",""},
                                       new String[]{"userid",userId}, // primary key
                                       new String[]{"campaignid",commId},
                                       new String[]{"parenttransid",""},
                                       new String[]{"commission",commissionAmount.ToString()},
                                       new String[]{"tier","1"},
                                       new String[]{"commissionTag",""},
                                       new String[]{"orderid",sale.sales_id.ToString()},
                                       new String[]{"productid",productDescription},
                                       new String[]{"data1",sale.lead_id.ToString()},
                                       new String[]{"data2",""},
                                       new String[]{"data3",""},
                                       new String[]{"data4",""},
                                       new String[]{"data5",""},
                                       new String[]{"trackmethod","U"},
                                       new String[]{"refererurl",""},
                                       new String[]{"ip",""},
                                       new String[]{"firstclicktime",""},
                                       new String[]{"firstclickreferer",""},
                                       new String[]{"firstclickip",""},
                                       new String[]{"firstclickdata1",""},
                                       new String[]{"firstclickdata2",""},
                                       new String[]{"lastclicktime",""},
                                       new String[]{"lastclickreferer",""},
                                       new String[]{"lastclickip",""},
                                       new String[]{"lastclickdata1",""},
                                       new String[]{"lastclickdata2",""},
                                       new String[]{"systemnote",""},
                                       new String[]{"merchantnote","Landed order inserted by GA.BDC.Console.PAPFeedback"}};
      }

      public GpfRpcFormRequest(string userId, string campaignId, string commTypeId, double commissionAmount, double totalAmount, int leadId)
      {
          C = "Pap_Merchants_Transaction_TransactionsForm".ToString();
          M = "add".ToString();

          fields = new List<String[]> {                 
                                       new String[]{ "name","value"},
                                       new String[]{"Id",""},
                                       new String[]{"rstatus","A"},
                                       new String[]{"dateinserted",DateTime.Now.AddHours(-3).ToString("u")},
                                       new String[]{"totalcost",totalAmount.ToString(CultureInfo.InvariantCulture)},
                                       new String[]{"channel",""},
                                       new String[]{"fixedcost",""},
                                       new String[]{"multiTier","N"},
                                       new String[]{"commtypeid",commTypeId}, // primary key
                                       new String[]{"bannerid",""},
                                       new String[]{"payoutstatus","U"},
                                       new String[]{"countrycode",""},
                                       new String[]{"userid",userId}, // primary key
                                       new String[]{"campaignid",campaignId},
                                       new String[]{"parenttransid",""},
                                       new String[]{"commission",commissionAmount.ToString(CultureInfo.InvariantCulture)},
                                       new String[]{"tier","1"},
                                       new String[]{"commissionTag",""},
                                       new String[]{"orderid",leadId.ToString()},
                                       new String[]{"productid","REQUEST"},
                                       new String[]{"data1",leadId.ToString()},
                                       new String[]{"data2",""},
                                       new String[]{"data3",""},
                                       new String[]{"data4",""},
                                       new String[]{"data5",""},
                                       new String[]{"trackmethod","U"},
                                       new String[]{"refererurl",""},
                                       new String[]{"ip",""},
                                       new String[]{"firstclicktime",""},
                                       new String[]{"firstclickreferer",""},
                                       new String[]{"firstclickip",""},
                                       new String[]{"firstclickdata1",""},
                                       new String[]{"firstclickdata2",""},
                                       new String[]{"lastclicktime",""},
                                       new String[]{"lastclickreferer",""},
                                       new String[]{"lastclickip",""},
                                       new String[]{"lastclickdata1",""},
                                       new String[]{"lastclickdata2",""},
                                       new String[]{"systemnote",""},
                                       new String[]{"merchantnote","Kit Request"}};
      }
      public GpfRpcFormRequest(pap_get_sales_to_be_processed_v2Result sale, string userId, string commId, string commTypeId, double commissionAmount, string productDescription, string bannerId)
      {
         C = "Pap_Merchants_Transaction_TransactionsForm";
         M = "add";

         fields = new List<String[]> {                 
                                       new []{ "name","value"},
                                       new []{"Id",""},
                                       new []{"rstatus","P"},
                                       new []{"dateinserted",DateTime.Now.AddHours(-3).ToString("u")},
                                       new []{"totalcost",sale.total_amount.ToString()},
                                       new []{"channel",""},
                                       new []{"fixedcost",""},
                                       new []{"multiTier","N"},
                                       new []{"commtypeid",commTypeId}, // primary key
                                       new []{"bannerid",bannerId},
                                       new []{"payoutstatus","U"},
                                       new []{"countrycode",""},
                                       new []{"userid",userId}, // primary key
                                       new []{"campaignid",commId},
                                       new []{"parenttransid",""},
                                       new []{"commission",commissionAmount.ToString()},
                                       new []{"tier","1"},
                                       new []{"commissionTag",""},
                                       new []{"orderid",sale.sales_id.ToString()},
                                       new []{"productid",productDescription},
                                       new []{"data1",sale.lead_id.ToString()},
                                       new []{"data2",""},
                                       new []{"data3",""},
                                       new []{"data4",""},
                                       new []{"data5",""},
                                       new []{"trackmethod","U"},
                                       new []{"refererurl",""},
                                       new []{"ip",""},
                                       new []{"firstclicktime",""},
                                       new []{"firstclickreferer",""},
                                       new []{"firstclickip",""},
                                       new []{"firstclickdata1",""},
                                       new []{"firstclickdata2",""},
                                       new []{"lastclicktime",""},
                                       new []{"lastclickreferer",""},
                                       new []{"lastclickip",""},
                                       new []{"lastclickdata1",""},
                                       new []{"lastclickdata2",""},
                                       new []{"systemnote",""},
                                       new []{"merchantnote","Landed order inserted by GA.BDC.Console.PAPFeedback"}};
      }

      public GpfRpcFormRequest(es_get_valid_orders_items_by_partner_idResult sale, string userId, string commId, string commTypeId, double commissionAmount, string productDescription)
      {
         C = "Pap_Merchants_Transaction_TransactionsForm".ToString();
         M = "add".ToString();

         fields = new List<String[]> {                 
                                       new String[]{ "name","value"},
                                       new String[]{"Id",""},
                                       new String[]{"rstatus","P"},
                                       new String[]{"dateinserted",DateTime.Now.AddHours(-3).ToString("u")},
                                       new String[]{"totalcost",sale.sub_total.ToString()},
                                       new String[]{"channel",""},
                                       new String[]{"fixedcost",""},
                                       new String[]{"multiTier","N"},
                                       new String[]{"commtypeid",commTypeId}, // primary key
                                       new String[]{"bannerid",""},
                                       new String[]{"payoutstatus","U"},
                                       new String[]{"countrycode",""},
                                       new String[]{"userid",userId}, // primary key
                                       new String[]{"campaignid",commId},
                                       new String[]{"parenttransid",""},
                                       new String[]{"commission",commissionAmount.ToString()},
                                       new String[]{"tier","1"},
                                       new String[]{"commissionTag",""},
                                       new String[]{"orderid",sale.order_item_id.ToString()},
                                       new String[]{"productid",productDescription},
                                       new String[]{"data1",sale.order_id.ToString()},
                                       new String[]{"data2",sale.product_id.ToString()},
                                       new String[]{"data3",sale.event_id.ToString()},
                                       new String[]{"data4",sale.product_type_id.ToString()},
                                       new String[]{"data5",sale.store_id.ToString()},
                                       new String[]{"trackmethod","U"},
                                       new String[]{"refererurl",""},
                                       new String[]{"ip",""},
                                       new String[]{"firstclicktime",""},
                                       new String[]{"firstclickreferer",""},
                                       new String[]{"firstclickip",""},
                                       new String[]{"firstclickdata1",""},
                                       new String[]{"firstclickdata2",""},
                                       new String[]{"lastclicktime",""},
                                       new String[]{"lastclickreferer",""},
                                       new String[]{"lastclickip",""},
                                       new String[]{"lastclickdata1",""},
                                       new String[]{"lastclickdata2",""},
                                       new String[]{"systemnote",""},
                                       new String[]{"merchantnote","Online Order inserted by GA.BDC.Console.PAPFeedback"}};
      }

      public GpfRpcFormRequest(es_get_valid_orders_items_by_partner_id_and_date_rangeResult sale, string userId, string commId, string commTypeId, double commissionAmount, string productDescription)
      {
         C = "Pap_Merchants_Transaction_TransactionsForm".ToString();
         M = "add".ToString();

         fields = new List<String[]> {
                                       new String[]{ "name","value"},
                                       new String[]{"Id",""},
                                       new String[]{"rstatus","P"},
                                       new String[]{"dateinserted",DateTime.Now.AddHours(-3).ToString("u")},
                                       new String[]{"totalcost",sale.sub_total.ToString()},
                                       new String[]{"channel",""},
                                       new String[]{"fixedcost",""},
                                       new String[]{"multiTier","N"},
                                       new String[]{"commtypeid",commTypeId}, // primary key
                                       new String[]{"bannerid",""},
                                       new String[]{"payoutstatus","U"},
                                       new String[]{"countrycode",""},
                                       new String[]{"userid",userId}, // primary key
                                       new String[]{"campaignid",commId},
                                       new String[]{"parenttransid",""},
                                       new String[]{"commission",commissionAmount.ToString()},
                                       new String[]{"tier","1"},
                                       new String[]{"commissionTag",""},
                                       new String[]{"orderid",sale.order_item_id.ToString()},
                                       new String[]{"productid",productDescription},
                                       new String[]{"data1",sale.order_id.ToString()},
                                       new String[]{"data2",sale.product_id.ToString()},
                                       new String[]{"data3",sale.event_id.ToString()},
                                       new String[]{"data4",sale.product_type_id.ToString()},
                                       new String[]{"data5",sale.store_id.ToString()},
                                       new String[]{"trackmethod","U"},
                                       new String[]{"refererurl",""},
                                       new String[]{"ip",""},
                                       new String[]{"firstclicktime",""},
                                       new String[]{"firstclickreferer",""},
                                       new String[]{"firstclickip",""},
                                       new String[]{"firstclickdata1",""},
                                       new String[]{"firstclickdata2",""},
                                       new String[]{"lastclicktime",""},
                                       new String[]{"lastclickreferer",""},
                                       new String[]{"lastclickip",""},
                                       new String[]{"lastclickdata1",""},
                                       new String[]{"lastclickdata2",""},
                                       new String[]{"systemnote",""},
                                       new String[]{"merchantnote","Online Order inserted by GA.BDC.Console.PAPFeedback"}};
      }


      public GpfRpcFormRequest(es_get_kickoff_by_partner_idResult kickoff, string userId, string commId, string commTypeId, double commissionAmount, string productDescription, string status)
      {
         C = "Pap_Merchants_Transaction_TransactionsForm".ToString();
         M = "add".ToString();

         fields = new List<String[]> {                 
                                       new String[]{ "name","value"},
                                       new String[]{"Id",""},
                                       new String[]{"rstatus", status},
                                       new String[]{"dateinserted",DateTime.Now.AddHours(-3).ToString("u")},
                                       new String[]{"totalcost","0.0"},
                                       new String[]{"channel",""},
                                       new String[]{"fixedcost",""},
                                       new String[]{"multiTier","N"},
                                       new String[]{"commtypeid",commTypeId}, // primary key
                                       new String[]{"bannerid",""},
                                       new String[]{"payoutstatus","U"},
                                       new String[]{"countrycode",""},
                                       new String[]{"userid",userId}, // primary key
                                       new String[]{"campaignid",commId},
                                       new String[]{"parenttransid",""},
                                       new String[]{"commission",commissionAmount.ToString()},
                                       new String[]{"tier","1"},
                                       new String[]{"commissionTag",""},
                                       new String[]{"orderid",kickoff.event_participation_id.ToString()},
                                       new String[]{"productid","Event Kickoff"},
                                       new String[]{"data1",kickoff.event_id.ToString()},
                                       new String[]{"data2",kickoff.event_name.Length > 50? kickoff.event_name.Substring(0, 50):kickoff.event_name },
                                       new String[]{"trackmethod","U"},
                                       new String[]{"refererurl",""},
                                       new String[]{"ip",""},
                                       new String[]{"firstclicktime",""},
                                       new String[]{"firstclickreferer",""},
                                       new String[]{"firstclickip",""},
                                       new String[]{"firstclickdata1",""},
                                       new String[]{"firstclickdata2",""},
                                       new String[]{"lastclicktime",""},
                                       new String[]{"lastclickreferer",""},
                                       new String[]{"lastclickip",""},
                                       new String[]{"lastclickdata1",""},
                                       new String[]{"lastclickdata2",""},
                                       new String[]{"systemnote",""},
                                       new String[]{"merchantnote","Online KickOff inserted by GA.BDC.Console.PAPFeedback"}};
      }

      public GpfRpcFormRequest(es_get_auto_created_groups_by_partner_idResult autoCreate, string userId, string commId, string commTypeId, double commissionAmount, string productDescription, string status)
      {
         C = "Pap_Merchants_Transaction_TransactionsForm".ToString();
         M = "add".ToString();

         fields = new List<String[]> {                 
                                       new String[]{ "name","value"},
                                       new String[]{"Id",""},
                                       new String[]{"rstatus", status},
                                       new String[]{"dateinserted",DateTime.Now.AddHours(-3).ToString("u")},
                                       new String[]{"totalcost","0.0"},
                                       new String[]{"channel",""},
                                       new String[]{"fixedcost",""},
                                       new String[]{"multiTier","N"},
                                       new String[]{"commtypeid",commTypeId}, // primary key
                                       new String[]{"bannerid",""},
                                       new String[]{"payoutstatus","U"},
                                       new String[]{"countrycode",""},
                                       new String[]{"userid",userId}, // primary key
                                       new String[]{"campaignid",commId},
                                       new String[]{"parenttransid",""},
                                       new String[]{"commission",commissionAmount.ToString()},
                                       new String[]{"tier","1"},
                                       new String[]{"commissionTag",""},
                                       new String[]{"orderid", autoCreate.group_id.ToString()},
                                       new String[]{"productid","Auto Create Event"},
                                       new String[]{"data1",autoCreate.create_date.ToString()},
                                       new String[]{"data2",autoCreate.group_name.Length > 50? autoCreate.group_name.Substring(0, 50):autoCreate.group_name },
                                       new String[]{"trackmethod","U"},
                                       new String[]{"refererurl",""},
                                       new String[]{"ip",""},
                                       new String[]{"firstclicktime",""},
                                       new String[]{"firstclickreferer",""},
                                       new String[]{"firstclickip",""},
                                       new String[]{"firstclickdata1",""},
                                       new String[]{"firstclickdata2",""},
                                       new String[]{"lastclicktime",""},
                                       new String[]{"lastclickreferer",""},
                                       new String[]{"lastclickip",""},
                                       new String[]{"lastclickdata1",""},
                                       new String[]{"lastclickdata2",""},
                                       new String[]{"systemnote",""},
                                       new String[]{"merchantnote","Group Auto Created event inserted by GA.BDC.Console.PAPFeedback"}};
      }

      public GpfRpcFormRequest(es_get_valid_orders_items_by_partner_idResult sale, string userId, string commId, string commTypeId, double commissionAmount, string productDescription, string status)
      {
         C = "Pap_Merchants_Transaction_TransactionsForm".ToString();
         M = "add".ToString();

         fields = new List<String[]> {                 
                                       new String[]{ "name","value"},
                                       new String[]{"Id",""},
                                       new String[]{"rstatus",status},
                                       new String[]{"dateinserted",DateTime.Now.AddHours(-3).ToString("u")},
                                       new String[]{"totalcost",sale.sub_total.ToString()},
                                       new String[]{"channel",""},
                                       new String[]{"fixedcost",""},
                                       new String[]{"multiTier","N"},
                                       new String[]{"commtypeid",commTypeId}, // primary key
                                       new String[]{"bannerid",""},
                                       new String[]{"payoutstatus","U"},
                                       new String[]{"countrycode",""},
                                       new String[]{"userid",userId}, // primary key
                                       new String[]{"campaignid",commId},
                                       new String[]{"parenttransid",""},
                                       new String[]{"commission",commissionAmount.ToString()},
                                       new String[]{"tier","1"},
                                       new String[]{"commissionTag",""},
                                       new String[]{"orderid",sale.order_item_id.ToString()},
                                       new String[]{"productid",productDescription},
                                       new String[]{"data1",sale.order_id.ToString()},
                                       new String[]{"data2",sale.product_id.ToString()},
                                       new String[]{"data3",sale.event_id.ToString()},
                                       new String[]{"data4",sale.product_type_id.ToString()},
                                       new String[]{"data5",sale.store_id.ToString()},
                                       new String[]{"trackmethod","U"},
                                       new String[]{"refererurl",""},
                                       new String[]{"ip",""},
                                       new String[]{"firstclicktime",""},
                                       new String[]{"firstclickreferer",""},
                                       new String[]{"firstclickip",""},
                                       new String[]{"firstclickdata1",""},
                                       new String[]{"firstclickdata2",""},
                                       new String[]{"lastclicktime",""},
                                       new String[]{"lastclickreferer",""},
                                       new String[]{"lastclickip",""},
                                       new String[]{"lastclickdata1",""},
                                       new String[]{"lastclickdata2",""},
                                       new String[]{"systemnote",""},
                                       new String[]{"merchantnote","Online Order inserted by GA.BDC.Console.PAPFeedback"}};
      }

      public GpfRpcFormRequest(es_get_valid_orders_items_by_partner_id_and_date_rangeResult sale, string userId, string commId, string commTypeId, double commissionAmount, string productDescription, string status)
      {
         C = "Pap_Merchants_Transaction_TransactionsForm".ToString();
         M = "add".ToString();

         fields = new List<String[]> {                 
                                       new String[]{ "name","value"},
                                       new String[]{"Id",""},
                                       new String[]{"rstatus",status},
                                       new String[]{"dateinserted",DateTime.Now.AddHours(-3).ToString("u")},
                                       new String[]{"totalcost",sale.sub_total.ToString()},
                                       new String[]{"channel",""},
                                       new String[]{"fixedcost",""},
                                       new String[]{"multiTier","N"},
                                       new String[]{"commtypeid",commTypeId}, // primary key
                                       new String[]{"bannerid",""},
                                       new String[]{"payoutstatus","U"},
                                       new String[]{"countrycode",""},
                                       new String[]{"userid",userId}, // primary key
                                       new String[]{"campaignid",commId},
                                       new String[]{"parenttransid",""},
                                       new String[]{"commission",commissionAmount.ToString()},
                                       new String[]{"tier","1"},
                                       new String[]{"commissionTag",""},
                                       new String[]{"orderid",sale.order_item_id.ToString()},
                                       new String[]{"productid",productDescription},
                                       new String[]{"data1",sale.order_id.ToString()},
                                       new String[]{"data2",sale.product_id.ToString()},
                                       new String[]{"data3",sale.event_id.ToString()},
                                       new String[]{"data4",sale.product_type_id.ToString()},
                                       new String[]{"data5",sale.store_id.ToString()},
                                       new String[]{"trackmethod","U"},
                                       new String[]{"refererurl",""},
                                       new String[]{"ip",""},
                                       new String[]{"firstclicktime",""},
                                       new String[]{"firstclickreferer",""},
                                       new String[]{"firstclickip",""},
                                       new String[]{"firstclickdata1",""},
                                       new String[]{"firstclickdata2",""},
                                       new String[]{"lastclicktime",""},
                                       new String[]{"lastclickreferer",""},
                                       new String[]{"lastclickip",""},
                                       new String[]{"lastclickdata1",""},
                                       new String[]{"lastclickdata2",""},
                                       new String[]{"systemnote",""},
                                       new String[]{"merchantnote","Online Order inserted by GA.BDC.Console.PAPFeedback"}};
      }

      public GpfRpcFormRequest(string transactionId)
      {
         C = "Pap_Merchants_Transaction_TransactionsForm".ToString();
         M = "load".ToString();

         fields = new List<String[]> {                 
                                       new String[]{ "name","value"},
                                       new String[]{"Id", transactionId}
            };
      }
      
      public string C { get; set; }
      public string M { get; set; }
      public List<String[]> fields { get; set; }
   }
}
