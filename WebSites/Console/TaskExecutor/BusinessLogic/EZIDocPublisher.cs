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
    public static class EZIDocPublisher
    {

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void PublishEZOrdersIDoc()
        {
            var exceptions = new List<Exception>();
            using (var EZMainCloudDataContext = new EZMainCloudDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.EZMainConnectionString"].ConnectionString))
            {
                foreach (var sale in EZOrders.GetEZOrdersToProcess(EZMainCloudDataContext, Settings.Default.EZSaleConfirmed, Settings.Default.EZSaleIdCutOver, Settings.Default.ProductClassIdOmit, Settings.Default.MAXOrdersInBatch, Settings.Default.InHouseSaleStatus, Settings.Default.SaleInProcessSAP))
                {
                    try
                    {
                        IDocPublisher.FormatAndDeliverIDoc(new EZOrderIDocMessageFormatter(sale, Settings.Default.AccountTypeBilling, Settings.Default.AccountTypeShipping));
                        var payments = EZPayments.GetEZPayments(sale.ORDR_ID);
                        sale.ext_sales_status_id = payments.Count > 0 ? Settings.Default.SaleInSAPWoutPayment : Settings.Default.SaleInSAPNoPayment;
                        EZMainCloudDataContext.SubmitChanges();
                    }
                    catch (ChangeConflictException changeConflictException)
                    {
                        EZMainCloudDataContext.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);
                        exceptions.Add(changeConflictException);
                    }
                    catch (Exception exception)
                    {
                        exceptions.Add(new Exception(string.Concat("Exception during order publishing Sale ID: ", sale.ORDR_ID), exception));
                    }
                }
            }
            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void PublishEZPaymentsIDoc()
        {
            var exceptions = new List<Exception>();
            using (var EZMainCloudDataContext = new EZMainCloudDataContext(Settings.Default.EZMainConnectionString))
            {
                foreach (var payment in EZPayments.GetEZPaymentsToProcess(EZMainCloudDataContext, Settings.Default.SaleInProcessSAP))
                {
                    try
                    {
                        //add safety net where you check if payment is before '2017-02-01' throw an exception
                        var minimumDate = new DateTime(2017, 2, 1);
                        var datenow = DateTime.Now.ToShortDateString();
                        if (payment.TRNS_DTE < minimumDate)
                        {
                            var exception = new Exception($"Incorrect Payment Detected EZFUND. Payment Date Before: {minimumDate.ToShortDateString()}");
                            exception.Data.Add("Order ID", payment.ORDR_ID);
                            exception.Data.Add("Payment Trans date", payment.TRNS_DTE);
                            exception.Data.Add("Payment Method", payment.PMT_METH_TYPE_CDE);
                            exception.Data.Add("Payment Amount", payment.TRNS_AMT);
                            exception.Data.Add("Payment Type:", payment.TRNS_TYPE_CDE);
                            exception.Data.Add("Payment Coming From Ezfund", datenow);

                            SWCorporate.SystemEx.InstrumentationProvider.SendExceptionNotification(exception, null);
                            throw exception;
                        }
                        
                        //log using SWCorporate.SystemEx.InstrumentationProvider.SendExceptionNotification(exception, null);
                        IDocPublisher.FormatAndDeliverIDoc(new EZPaymentIDocMessageFormatter(payment));
                        var sale = EZOrders.GetSale(EZMainCloudDataContext, payment.ORDR_ID);
                        if (sale == null)
                        {
                            throw new Exception("Cannot find sale status for " + Settings.Default.SaleInSAPWithPay + ", cannot update sale");
                        }

                        EZMainCloudDataContext.sp_publishEZpaymentsidoc(sale.ORDR_ID,Settings.Default.PaymentSentToSAP, Settings.Default.SaleInSAPWithPay);
                    }
                    catch (ChangeConflictException changeConflictException)
                    {
                        EZMainCloudDataContext.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);
                        exceptions.Add(changeConflictException);
                    }
                    catch (Exception exception)
                    {
                        exceptions.Add(new Exception("Exception during order publishing Sale ID: " + payment.ORDR_ID, exception));
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

