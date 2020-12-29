using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Runtime.CompilerServices;
using System.Data.Linq;
using System.Configuration;
using SWCorporate.SAP.Shared;
using GA.BDC.Data;
using GA.BDC.Data.DataLayer;
using GA.BDC.Console.TaskExecutor.Properties;
using GA.BDC.Integration.MessageFormatters;


namespace GA.BDC.Console.TaskExecutor.BusinessLogic
{
    // ReSharper disable once InconsistentNaming
    public static class FRIDocPublisher
    {

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void PublishFROrdersIDoc()
        {
            var exceptions = new List<Exception>();
            using (var eFundraisingProdDataContext = new eFundraisingProdDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.eFundraisingProdConnectionString"].ConnectionString))
            {
                foreach (var sale in Orders.GetFROrdersToProcess(eFundraisingProdDataContext, Settings.Default.SaleConfirmed, Settings.Default.SaleIdCutOver, Settings.Default.ProductClassIdOmit, Settings.Default.MAXOrdersInBatch, Settings.Default.InHouseSaleStatus, Settings.Default.SaleInProcessSAP))
                {


                    try
                    {
                        IDocPublisher.FormatAndDeliverIDoc(new FROrderIDocMessageFormatter(sale, Settings.Default.AccountTypeBilling, Settings.Default.AccountTypeShipping));
                        var payments = Payments.GetFRPayments(sale.sales_id);
                        sale.ext_sales_status_id = payments.Count > 0 ? Settings.Default.SaleInSAPWoutPayment : Settings.Default.SaleInSAPNoPayment;
                        eFundraisingProdDataContext.SubmitChanges();
                    }
                    catch (ChangeConflictException changeConflictException)
                    {
                        eFundraisingProdDataContext.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);
                        exceptions.Add(changeConflictException);
                    }
                    catch (Exception exception)
                    {
                        exceptions.Add(new Exception(string.Concat("Exception during order publishing Sale ID: ", sale.sales_id), exception));
                    }
                }
            }
            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void PublishFRPaymentsIDoc()
        {
            var exceptions = new List<Exception>();
            using (var eFundraisingProdDataContext = new eFundraisingProdDataContext(Settings.Default.eFundraisingProdConnectionString))
            {
                foreach (var payment in Payments.GetFRPaymentsToProcess(eFundraisingProdDataContext, Settings.Default.SaleInProcessSAP))
                {
                    try
                    {

                        //add safety net where you check if payment is before '2017-02-01' throw an exception
                        var minimumDate = new DateTime(2017, 1, 1);
                        var datenow = DateTime.Now.ToShortDateString();
                        if (payment.payment_entry_date < minimumDate)
                        {
                            var paymentType = Payments.GetFRPaymentMethod(payment.payment_method_id);
                            var exception = new Exception($"Incorrect Payment Detected FR. Payment Date Before: {minimumDate.ToShortDateString()}");
                            exception.Data.Add("Order ID", payment.sales_id);
                            exception.Data.Add("Payment Trans date", payment.payment_entry_date);
                            exception.Data.Add("Payment Method", paymentType.description);
                            exception.Data.Add("Payment Amount", payment.payment_amount);
                            exception.Data.Add("Payment Coming From FR", datenow);

                            SWCorporate.SystemEx.InstrumentationProvider.SendExceptionNotification(exception, null);
                            throw exception;
                        }

                        
                        IDocPublisher.FormatAndDeliverIDoc(new FRPaymentIDocMessageFormatter(payment));
                        var sale = Orders.GetSale(eFundraisingProdDataContext, payment.sales_id);
                        if (sale == null)
                        {
                            throw new Exception("Cannot find sale status for " + Settings.Default.SaleInSAPWithPay + ", cannot update sale");
                        }
                        
                        eFundraisingProdDataContext.sp_publishFRpaymentsidoc(sale.sales_id, payment.payment_no, Settings.Default.PaymentSentToSAP, Settings.Default.SaleInSAPWithPay);
                    }
                    catch (ChangeConflictException changeConflictException)
                    {
                        eFundraisingProdDataContext.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);
                        exceptions.Add(changeConflictException);
                    }
                    catch (Exception exception)
                    {
                        exceptions.Add(new Exception("Exception during order publishing Sale ID: " + payment.sales_id, exception));
                    }
                }
            }
            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}

