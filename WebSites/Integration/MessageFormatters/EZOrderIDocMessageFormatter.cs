using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using SWCorporate.SAP.Shared;

using GA.BDC.Data;
using GA.BDC.Data.DataLayer;
using GA.BDC.Integration.MessageStructures;

namespace GA.BDC.Integration.MessageFormatters
{
    public class EZOrderIDocMessageFormatter : IDocMessageFormatter<ZGA_EZFUND_ORDER>
    {

        private readonly ORDR_INVOIC_TO_PROCESS_TBL _sale;
        private readonly string _accountBilling;
        private readonly string _accountShipping;

        public EZOrderIDocMessageFormatter(ORDR_INVOIC_TO_PROCESS_TBL sale, string accBilling, string accShipping) : base()
        {

            this._sale = sale;
            this._accountBilling = accBilling;
            this._accountShipping = accShipping;

        }

        protected override IDoc FormatMessage()
        {
            ZGA_EZFUND_ORDER orderMessage = new ZGA_EZFUND_ORDER(true);
            IDoc iDoc = this.CreateIDoc(orderMessage, 1);

            ORDR_INVOIC_TO_PROCESS_TBL sale = EZOrders.GetClientOrder(_sale.ORDR_ID);

            if (sale != null)
            {
                //var lead = Orders.GetLeadById(this._sale.lead_id ?? -1);
                EZOrderSharedFormatting.FormatEZOrderHeaderSegment(sale, ref orderMessage.ZgaEZFUNDOrderHdrSeg);
                iDoc.AddSegment(orderMessage.ZgaEZFUNDOrderHdrSeg);

                ZGA_EZFUND_ORDER_ACCT_SEG billingAccountSegment = new ZGA_EZFUND_ORDER_ACCT_SEG(true);
                EZOrderSharedFormatting.FormatOrderAccountSegment(sale, this._accountBilling, false, ref billingAccountSegment);
                orderMessage.ZgaEZFUNDOrderAcctSegList.Add(billingAccountSegment);

                ZGA_EZFUND_ORDER_ACCT_SEG shippingAccountSegment = new ZGA_EZFUND_ORDER_ACCT_SEG(true);
                EZOrderSharedFormatting.FormatOrderShippingSegment(sale, this._accountShipping, true, ref shippingAccountSegment);
                orderMessage.ZgaEZFUNDOrderAcctSegList.Add(shippingAccountSegment);

                foreach (ZGA_EZFUND_ORDER_ACCT_SEG orderAccountSegment in orderMessage.ZgaEZFUNDOrderAcctSegList)
                {
                    iDoc.AddSegment(orderAccountSegment);
                }


                List<ORDR_ITEM_TBL> saleItems = EZOrders.GetSaleItems(sale.ORDR_ID);
                if (saleItems != null && saleItems.Count > 0)
                {
                    
                    foreach (ORDR_ITEM_TBL si in saleItems)
                    {

                        ITEM_LKUP_TBL scratchBook = EZOrders.GetScratchBook(si.ITEM_CDE);
                        // product_class productClass = Orders.GetProductClass((int)si.product_class_id);
                        if (scratchBook != null)
                        {
                            if (scratchBook.SAPMaterialNo == null || scratchBook.SAPMaterialNo <= 0)
                            {
                                throw new Exception("Cannot retrieve SAP # for scratchbook: ".ToString() + Environment.NewLine + scratchBook.ITEM_CDE.ToString() + Environment.NewLine + si.ToString());
                               
                            }
                            //if (scratchBook.InHouse ?? false)
                            //{
                            //    continue;
                            //}
                            ZGA_EZFUND_ORDER_ITEM_SEG orderItemSegment = new ZGA_EZFUND_ORDER_ITEM_SEG(true);
                            EZOrderSharedFormatting.FormatEZOrderOrderItemSegment(scratchBook, si, false, ref orderItemSegment);
                            orderMessage.ZgaEZFUNDOrderItemSegList.Add(orderItemSegment);

                            //if (si.ITEM_EXTRA_QTY > 0)
                            //{
                            //    ZGA_EZFUND_ORDER_ITEM_SEG orderItemSegmentFree = new ZGA_EZFUND_ORDER_ITEM_SEG(true);
                            //    EZOrderSharedFormatting.FormatEZOrderOrderItemSegment(scratchBook, si, true, ref orderItemSegmentFree);
                            //    orderMessage.ZgaEZFUNDOrderItemSegList.Add(orderItemSegmentFree);
                            //}
                        }
                        else
                        {
                            throw new Exception("Cannot retrieve scratch_bookid from sales item ".ToString() + Environment.NewLine + this._sale.ORDR_ID.ToString() + Environment.NewLine + si.ITEM_CDE.ToString());
                        }
                    }

                    foreach (ZGA_EZFUND_ORDER_ITEM_SEG orderItemSegment in orderMessage.ZgaEZFUNDOrderItemSegList)
                    {
                        iDoc.AddSegment(orderItemSegment);
                    }
                   
                }
               

            }
            return iDoc;
        }
    }
}
