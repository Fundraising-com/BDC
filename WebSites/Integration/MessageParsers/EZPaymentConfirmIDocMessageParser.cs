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
    internal sealed class EZPaymentConfirmIDocMessageParser : IDocMessageParser<ZGA_EZFUND_PMT_CONF>
    {
        private ZGA_EZFUND_PMT_CONF _paymentConfirmIDoc;


        public EZPaymentConfirmIDocMessageParser(byte[] input) : base(input)
        {
            this._paymentConfirmIDoc = new ZGA_EZFUND_PMT_CONF(true);
        }

        protected override void ProcessSegment(byte[] segment)
        {
            this._paymentConfirmIDoc.ZgaEZFUNDPmtConfSeg = this.DeserializeSegment<ZGA_EZFUND_PMT_CONF_SEG>(segment);

        }

        protected override void ReconcileAndIntegrate()
        {
            int orderRefID;
            AR_TRNS_TBL p = new AR_TRNS_TBL();



            if (!string.IsNullOrEmpty(this._paymentConfirmIDoc.ZgaEZFUNDPmtConfSeg.Orderrefid))
            {
                try
                {
                    orderRefID = Convert.ToInt32(this._paymentConfirmIDoc.ZgaEZFUNDPmtConfSeg.Orderrefid);
                }
                catch (Exception ex)
                {
                    throw new Exception("Cannot parse Orfer Refernce ID from ZGA_BDC_PMT_CONF_SEG".ToString() + Environment.NewLine +
                       this._paymentConfirmIDoc.ZgaEZFUNDPmtConfSeg.Orderrefid + Environment.NewLine, ex.InnerException);
                }
                ORDR_INVOIC_TO_PROCESS_TBL sale = EZOrders.GetSale(orderRefID);
                if (sale != null)
                {
                    var EzPayment = EZPayments.GetEZPayment(sale.ORDR_ID);

                    if (EzPayment == null)
                    {
                        EZPaymentConfirmSharedParser.ParsePaymentConfirmation(sale, this._paymentConfirmIDoc.ZgaEZFUNDPmtConfSeg, Helper.PaymentSentFromSAP, ref p);
                        p.PAYMENT_STATUS_ID = Helper.PaymentSentFromSAP;
                        EZPayments.InsertPayment(p);
                    }
                    else
                    {

                        int ExtPaymentID;
                        try
                        {
                            ExtPaymentID = Convert.ToInt32(this._paymentConfirmIDoc.ZgaEZFUNDPmtConfSeg.Sqlid);
                        }
                        catch (Exception)
                        {
                            try
                            {
                                ExtPaymentID = Convert.ToInt32(Helper.RemoveNonNumeric(this._paymentConfirmIDoc.ZgaEZFUNDPmtConfSeg.Sqlid));
                            }
                            catch (Exception exc)
                            {
                                throw new Exception("Failed to retrive ExtPaymentID from ZGA_EZFUND_PMT_CONF_SEG" + Environment.NewLine + this._paymentConfirmIDoc.ZgaEZFUNDPmtConfSeg.Sqlid.ToString(), exc.InnerException);
                            }


                        }

                        if (EzPayment.PMT_METH_TYPE_CDE == "CCRD")
                        {
                            EZPaymentConfirmSharedParser.ParsePaymentConfirmation(sale, this._paymentConfirmIDoc.ZgaEZFUNDPmtConfSeg, Helper.PaymentSentFromSAP, ref p);
                            p.PAYMENT_STATUS_ID = Helper.PaymentConfirmedBySAP;
                            EZPayments.UpdatePayment(p);
                        }
                        else if (EzPayment.EXT_PAYMENT_ID == ExtPaymentID)
                        {
                            EZPaymentConfirmSharedParser.ParsePaymentConfirmation(sale, this._paymentConfirmIDoc.ZgaEZFUNDPmtConfSeg, Helper.PaymentSentFromSAP, ref p);
                            p.PAYMENT_STATUS_ID = Helper.PaymentConfirmedBySAP;
                            EZPayments.UpdatePayment(p);
                        }
                        else
                        {
                            EZPaymentConfirmSharedParser.ParsePaymentConfirmation(sale, this._paymentConfirmIDoc.ZgaEZFUNDPmtConfSeg, Helper.PaymentSentFromSAP, ref p);
                            p.PAYMENT_STATUS_ID = Helper.PaymentSentFromSAP;
                            EZPayments.InsertPayment(p);
                        }
                    }


                    return;


                }
                else
                {
                    throw new Exception("Cannot find sale id from ZGA_EZFUND_PMT_CONF_SEG ".ToString() + orderRefID.ToString() + " cannot update payment".ToString());
                }

            }
            else
            {
                throw new Exception("Cannot retrieve Order Reference ID from ZGA_EZFUND_PMT_CONF_SEG".ToString() + Environment.NewLine + this._paymentConfirmIDoc.ZgaEZFUNDPmtConfSeg.Orderrefid);
            }
        }


    }
}

