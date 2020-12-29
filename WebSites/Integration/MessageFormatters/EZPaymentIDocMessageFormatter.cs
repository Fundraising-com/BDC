using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;


using SWCorporate.SAP.Shared;

using GA.BDC.Data;
using GA.BDC.Data.DataLayer;
using GA.BDC.Integration.MessageStructures;

[assembly: InternalsVisibleTo("GA.BDC.Console.TaskExecutor, PublicKey=" +
"0024000004800000940000000602000000240000525341310004000001000100b5fcef761974c0" +
"3c45e2b88ccad328f0cb8b2724945800c688d311751cc33c921a680ab4af6b8825a1bd684771e9" +
"9c0a16bcc5eb6e197d04d86c5a1e17c1da64a5b6bc8c500111ba04f756242430aad3131336e94c" +
"09acef49d01b8e0dfd83ab40a24e769d5e5e06124b06ba3f6c26e3b9fe1733ab1d09acb6da9ad4" +
"bb17cebc")]

namespace GA.BDC.Integration.MessageFormatters
{
    internal sealed class EZPaymentIDocMessageFormatter : IDocMessageFormatter<ZGA_EZFUND_ORDER>
    {
        private readonly AR_TRNS_TBL _payment;

        public EZPaymentIDocMessageFormatter(AR_TRNS_TBL payment) : base()
        {

            this._payment = payment;

        }

        protected override IDoc FormatMessage()
        {
            ZGA_EZFUND_PAYMENT paymentMessage = new ZGA_EZFUND_PAYMENT(true);
            IDoc iDoc = this.CreateIDoc(paymentMessage, 1);
            if (this._payment.TRNS_TYPE_CDE == "PMT")
            {
                AR_TRNS_TBL paymentMethod = EZPayments.GetEZPaymentMethod(this._payment.PMT_METH_TYPE_CDE);
                if (paymentMethod != null)
                {
                    
                    ORDR_INVOIC_TO_PROCESS_TBL sale = EZOrders.GetSale(this._payment.ORDR_ID);
                    if (sale != null)
                    {
                        ORG_CTCT_TBL client = EZClients.GetClient(this._payment.ORG_ID);
                        if (client != null)
                        {
                            EZPaymentSharedFormatting.FormatEZPaymentHeaderSegment(this._payment, paymentMethod, sale, "US", ref paymentMessage.ZgaEZFUNDPaymentSeg, client);
                            iDoc.AddSegment(paymentMessage.ZgaEZFUNDPaymentSeg);
                        }
                        else
                        {
                            throw new Exception("Cannot retrieve client ctct id from sales".ToString() + Environment.NewLine + this._payment.ToString() + Environment.NewLine + sale.ToString());
                        }
                    }
                    else
                    {
                        throw new Exception("Cannot retrieve client_address from sales".ToString() + Environment.NewLine + this._payment.ToString() + Environment.NewLine + sale.ToString());

                    }
                }
                else
                {
                    throw new Exception("Cannot retrieve sale from payment".ToString() + Environment.NewLine + this._payment.ToString());
                }


            }
            return iDoc;
        }



    }
}

