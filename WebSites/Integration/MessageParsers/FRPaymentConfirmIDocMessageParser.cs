using System;
using System.Collections.Generic;
using System.Transactions;
using System.Linq;
using System.Text;

using SWCorporate.SAP.Shared;
using SWCorporate.SystemEx;

/*using GA.BDC.Console.TaskExecutor.Integration.MessageStructures;
using GA.BDC.Console.TaskExecutor.Data;*/
using GA.BDC.Data;
using GA.BDC.Data.DataLayer;
using GA.BDC.Integration.MessageStructures;

namespace GA.BDC.Integration.MessageParsers
{
   internal sealed class FRPaymentConfirmIDocMessageParser:IDocMessageParser<ZGA_BDC_PMT_CONF>
   {
      private ZGA_BDC_PMT_CONF _paymentConfirmIDoc;
      

      public FRPaymentConfirmIDocMessageParser(byte[] input) : base(input)
      {
         this._paymentConfirmIDoc = new ZGA_BDC_PMT_CONF(true);
      }

      protected override void ProcessSegment(byte[] segment)
      {
         this._paymentConfirmIDoc.ZgaBdcPmtConfSeg = this.DeserializeSegment<ZGA_BDC_PMT_CONF_SEG>(segment);
        
      }

      protected override void ReconcileAndIntegrate()
      {
         int orderRefID;
         payment p = new payment();

         if (!string.IsNullOrEmpty(this._paymentConfirmIDoc.ZgaBdcPmtConfSeg.Orderrefid))
         {
            try
            {
               orderRefID = Convert.ToInt32(this._paymentConfirmIDoc.ZgaBdcPmtConfSeg.Orderrefid);
            }
            catch(Exception ex)
            {
               throw new Exception("Cannot parse Orfer Refernce ID from ZGA_BDC_PMT_CONF_SEG".ToString() + Environment.NewLine +
                  this._paymentConfirmIDoc.ZgaBdcPmtConfSeg.Orderrefid + Environment.NewLine, ex.InnerException);
            }
            sale sale = Orders.GetSale(orderRefID);
            if (sale != null)
            {
               payment_method payMethod = Payments.GetPaymentMethodByDesc(Helper.GetSAPPaymentType(this._paymentConfirmIDoc.ZgaBdcPmtConfSeg.Pmttype));
               if (payMethod != null)
               {
                  if (payMethod.ext_payment_type_id == Helper.ExternalAjustment)
                  {
                     // insert adjustment instead of payment 
                     Adjustment a = new Adjustment();
                     //using (var transaction = new TransactionScope())
                     //{
                     if (FRPaymentConfirmSharedParser.ParseAdjustmentConfirmation(sale, this._paymentConfirmIDoc.ZgaBdcPmtConfSeg, ref a))
                     {Payments.InsertAdjustment(a)
                        ;
                     }
                     else
                     {
                        Payments.UpdateAdjustment(a);
                     }
                     //}
                     return;
                  }
                  
               }
               string[] paymentNumber = this._paymentConfirmIDoc.ZgaBdcPmtConfSeg.Sqlid.Split('-');
               if (paymentNumber.Length == 2 && this._paymentConfirmIDoc.ZgaBdcPmtConfSeg.Orderrefid.Trim() == paymentNumber[0].Trim())
               {
                  int paymentNo;
                  try
                  {
                     paymentNo = Convert.ToInt32(paymentNumber[1]);
                  }
                  catch (Exception)
                  {
                     FRPaymentConfirmSharedParser.ParsePaymentConfirmation(sale, this._paymentConfirmIDoc.ZgaBdcPmtConfSeg, Helper.PaymentSentFromSAP, ref p);
                     p.payment_status_id = Helper.PaymentSentFromSAP;
                     Payments.InsertPayment(p);
                     return;
                  }

                  payment _payment = Payments.GetFRPayment(sale.sales_id, paymentNo);
                  if (_payment != null)
                  {
                     _payment.payment_status_id = Helper.PaymentConfirmedBySAP;
                      Payments.UpdatePayment(_payment);
                   }
                  else
                  {
                     FRPaymentConfirmSharedParser.ParsePaymentConfirmation(sale, this._paymentConfirmIDoc.ZgaBdcPmtConfSeg, Helper.PaymentSentFromSAP, ref p);
                     Payments.InsertPayment(p);
                     return;
                  }
               }
               else
               {
                  FRPaymentConfirmSharedParser.ParsePaymentConfirmation(sale, this._paymentConfirmIDoc.ZgaBdcPmtConfSeg, Helper.PaymentSentFromSAP, ref p);
                  Payments.InsertPayment(p);
                  return;
               }
                           
              
            }
            else
            {
               throw new Exception("Cannot find sale id from ZGA_BDC_PMT_CONF_SEG ".ToString() + orderRefID.ToString() + " cannot update payment".ToString());
            }

         }
         else
         {
            throw new Exception("Cannot retrieve Order Reference ID from ZGA_BDC_PMT_CONF_SEG".ToString() + Environment.NewLine + this._paymentConfirmIDoc.ZgaBdcPmtConfSeg.Orderrefid);
         }
      }


   }
}
