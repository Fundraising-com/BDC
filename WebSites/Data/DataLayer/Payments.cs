 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Globalization;
using System.Transactions;
using System.Configuration;



namespace GA.BDC.Data.DataLayer
{
   public class Payments
   {

      public static List<payment> GetFRPaymentsToProcess(eFundraisingProdDataContext dc,  int saleInProcessSAP)
      {
    
            return (from p in dc.payments 
                    join s in dc.sales on p.sales_id equals s.sales_id 
                    where s.ext_sales_status_id != saleInProcessSAP 
                    && s.ext_sales_status_id != null 
                    && p.payment_status_id == null 
                    && s.ext_order_id > 0 
                    && s.payment_method_id != 1
                    && s.payment_method_id != 16
                    orderby p.sales_id descending 
                    select p).ToList<payment>();
      }

      public static payment_method GetFRPaymentMethod(int paymentMethodId)
      {
         using (eFundraisingProdDataContext dc = new eFundraisingProdDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.eFundraisingProdConnectionString"].ConnectionString))
         {
            return (from pm in dc.payment_methods where pm.payment_method_id == paymentMethodId select pm).FirstOrDefault();

         }
      }

      public static payment GetFRPayment(int saleId, int paymentNo)
      {
         using (eFundraisingProdDataContext dc = new eFundraisingProdDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.eFundraisingProdConnectionString"].ConnectionString))
         {
            return (from p in dc.payments where p.sales_id == saleId && p.payment_no == paymentNo select p).FirstOrDefault();

         }
      }


        public static List<payment> GetFRPayments(int saleId)
        {
            using (eFundraisingProdDataContext dc = new eFundraisingProdDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.eFundraisingProdConnectionString"].ConnectionString))
            {
                return (from p in dc.payments where p.sales_id == saleId select p).ToList<payment>();
            }
        }


      public static Payment_status GetPaymentStatusByDesc(string input)
      {
         using (eFundraisingProdDataContext dc = new eFundraisingProdDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.eFundraisingProdConnectionString"].ConnectionString))
         {
            return (from ps in dc.Payment_status where ps.Description.ToLower() == input.ToLower() select ps).FirstOrDefault();
         }
      }

      public static void UpdatePayment(payment input)
      {
         using (eFundraisingProdDataContext dc = new eFundraisingProdDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.eFundraisingProdConnectionString"].ConnectionString))
         {
            payment _payment = (from p in dc.payments where p.sales_id == input.sales_id && p.payment_no == input.payment_no select p).FirstOrDefault();
            if (_payment != null)
            {
               _payment.ext_payment_id = input.ext_payment_id;
               _payment.create_date = input.create_date;
               _payment.payment_status_id = input.payment_status_id;
            }

            try
            {
               dc.SubmitChanges();

            }
            catch (ChangeConflictException)
            {
               dc.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);
            }
         }
      }

      public static payment_method GetPaymentMethodByDesc(string desc)
      {


         using (eFundraisingProdDataContext dc = new eFundraisingProdDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.eFundraisingProdConnectionString"].ConnectionString))
         {
            return (from pm in dc.payment_methods where pm.description.ToLower().Trim() == desc.ToLower().Trim() select pm).FirstOrDefault();

         }
      

      }




      public static void InsertPayment(payment input)
      {
         using (eFundraisingProdDataContext dc = new eFundraisingProdDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.eFundraisingProdConnectionString"].ConnectionString))
         {
            using (var transaction = new TransactionScope())
            {
               int maxNO;
               if (dc.payments.Any(p => p.sales_id == input.sales_id))
               {
                  maxNO = (from p in dc.payments where p.sales_id == input.sales_id select p.payment_no).Max() +1;
               }
               else
               {
                  maxNO = 1;
               }
               input.payment_no = (int)maxNO;
               dc.payments.InsertOnSubmit(input);
               try
               {
               dc.SubmitChanges();
               }
               catch (ChangeConflictException)
               {
                  dc.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);
               }
               transaction.Complete();
            }
         }
      }

      public static void InsertAdjustment(Adjustment input)
      {
         using (eFundraisingProdDataContext dc = new eFundraisingProdDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.eFundraisingProdConnectionString"].ConnectionString))
         {
         
            dc.Adjustments.InsertOnSubmit(input);
            try
            {
               dc.SubmitChanges();
            }
            catch (ChangeConflictException)
            {
               dc.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);
            }             
            
         }
      }


      public static void UpdateAdjustment(Adjustment input)
      {
         using (eFundraisingProdDataContext dc = new eFundraisingProdDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.eFundraisingProdConnectionString"].ConnectionString))
         {
            Adjustment _adj = (from a in dc.Adjustments where a.Sales_ID == input.Sales_ID && a.Adjustment_No == input.Adjustment_No select a).FirstOrDefault();
            if (_adj != null)
            {
               if (input.Ext_Adjustment_Id > 0)
               {
                  _adj.Ext_Adjustment_Id = input.Ext_Adjustment_Id;
               }

               _adj.Reason_ID = input.Reason_ID;
               _adj.Adjustment_On_Sale_Amount = input.Adjustment_On_Sale_Amount;
               _adj.Adjustment_Amount = input.Adjustment_Amount;
               _adj.Create_Date = input.Create_Date;
            }

            try
            {
               dc.SubmitChanges();

            }
            catch (ChangeConflictException)
            {
               dc.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);
            }

         }
      }

      public static double? GetProfit(int saleId, int scrathBookId)
      {
         using (eFundraisingProdDataContext dc = new eFundraisingProdDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.eFundraisingProdConnectionString"].ConnectionString))
         {
            return dc.fct_sale_item_profit(saleId, scrathBookId);
         }
      }

     
   }
}
