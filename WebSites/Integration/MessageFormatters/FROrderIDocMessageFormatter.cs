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
    public class FROrderIDocMessageFormatter : IDocMessageFormatter<ZGA_BDC_ORDER>
    {

        private readonly sale _sale;
        private readonly string _accountBilling;
        private readonly string _accountShipping;

        public FROrderIDocMessageFormatter(sale sale, string accBilling, string accShipping): base()
        {
                 
            this._sale = sale;
            this._accountBilling = accBilling;
            this._accountShipping = accShipping;
                                         
        }

        protected override IDoc FormatMessage()
        {
            ZGA_BDC_ORDER orderMessage = new ZGA_BDC_ORDER(true);
            IDoc iDoc = this.CreateIDoc(orderMessage, 1);

            client client = Orders.GetClient(_sale.client_id, _sale.client_sequence_code);
            if (client != null )
            {
               client_address clientBillingAddress = Orders.GetClientBillingAddress(client.client_id, client.client_sequence_code);
               if (clientBillingAddress != null)
               {
                  Country country = Orders.GetCountry(clientBillingAddress.country_code);
                  if (country != null)
                  {
                     State stBilling = Orders.GetState(clientBillingAddress.country_code, clientBillingAddress.state_code);
                     if (stBilling != null && !string.IsNullOrEmpty(stBilling.SAP_State_code))
                     {

                        var lead = Orders.GetLeadById(this._sale.lead_id ?? -1);
                        FROrderSharedFormatting.FormatFROrderHeaderSegment(this._sale, Orders.GetAdjustmentsPerSale(this._sale.sales_id, Helper.ReasonSAPAdjustment), country, client, ref orderMessage.ZgaBdcOrderHdrSeg, lead);
                        iDoc.AddSegment(orderMessage.ZgaBdcOrderHdrSeg);

                        ZGA_BDC_ORDER_ACCT_SEG billingAccountSegment = new ZGA_BDC_ORDER_ACCT_SEG(true);
                        FROrderSharedFormatting.FormatOrderAccountSegment(client, clientBillingAddress, this._accountBilling, false, stBilling,  ref billingAccountSegment);
                        orderMessage.ZgaBdcOrderAcctSegList.Add(billingAccountSegment);

                        client_address clientShippingAddress = Orders.GetClientShippingAddress(client.client_id, client.client_sequence_code);
                        ZGA_BDC_ORDER_ACCT_SEG shippingAccountSegment = new ZGA_BDC_ORDER_ACCT_SEG(true);
                        if (clientShippingAddress == null)
                        {
                           FROrderSharedFormatting.FormatOrderAccountSegment(client, clientBillingAddress, this._accountShipping,true, stBilling,  ref shippingAccountSegment);
                        }
                        else
                        {
                           Country countryShipping = Orders.GetCountry(clientShippingAddress.country_code);
                           if (countryShipping != null)
                           {
                              State stShipping = Orders.GetState(clientShippingAddress.country_code, clientShippingAddress.state_code);
                              if (stShipping != null && !string.IsNullOrEmpty(stShipping.SAP_State_code))
                              {
                                 FROrderSharedFormatting.FormatOrderAccountSegment(client, clientShippingAddress, this._accountShipping, true, stShipping, ref shippingAccountSegment);
                              }
                              else
                              {
                                 throw new Exception("Cannot retreive Shipping State from sales".ToString() + Environment.NewLine + this._sale.ToString() + Environment.NewLine + client.ToString() +
                          Environment.NewLine + clientShippingAddress.ToString());
                              }
                           }
                           else
                           {
                              throw new Exception("Cannot retreive Shipping Country from sales".ToString() + Environment.NewLine + this._sale.ToString() + Environment.NewLine + client.ToString() +
                       Environment.NewLine + clientShippingAddress.ToString());
                           }
                        }
                        orderMessage.ZgaBdcOrderAcctSegList.Add(shippingAccountSegment);
                        foreach (ZGA_BDC_ORDER_ACCT_SEG orderAccountSegment in orderMessage.ZgaBdcOrderAcctSegList)
                        {
                           iDoc.AddSegment(orderAccountSegment);
                        }


                        List<sales_item> saleItems = Orders.GetSaleItems(_sale.sales_id);
                        if (saleItems != null && saleItems.Count > 0)
                        {

                           foreach (sales_item si in saleItems)
                           {
                              if (si.product_class_id == Helper.ProductClassOmit || (si.quantity_sold <= 0 && si.quantity_free <=0))
                              {
                                 continue;
                              }
                              scratch_book scratchBook = Orders.GetScratchBook(si.scratch_book_id);
                              // product_class productClass = Orders.GetProductClass((int)si.product_class_id);
                              if (scratchBook != null)
                              {
                                 if (scratchBook.SAPMaterialNo == null || scratchBook.SAPMaterialNo <= 0)
                                 {
                                    throw new Exception("Cannot retrieve SAP # for scratchbook: ".ToString() + Environment.NewLine + scratchBook.ToString() + Environment.NewLine + si.ToString());
                                 }
                                 if (scratchBook.InHouse??false)
                                 {
                                    continue;
                                 }
                                 ZGA_BDC_ORDER_ITEM_SEG orderItemSegment = new ZGA_BDC_ORDER_ITEM_SEG(true);
                                 FROrderSharedFormatting.FormatFROrderOrderItemSegment(scratchBook, si, false, ref orderItemSegment);
                                 orderMessage.ZgaBdcOrderItemSegList.Add(orderItemSegment);

                                 if (si.quantity_free > 0)
                                 {
                                    ZGA_BDC_ORDER_ITEM_SEG orderItemSegmentFree = new ZGA_BDC_ORDER_ITEM_SEG(true);
                                    FROrderSharedFormatting.FormatFROrderOrderItemSegment(scratchBook, si, true, ref orderItemSegmentFree);
                                    orderMessage.ZgaBdcOrderItemSegList.Add(orderItemSegmentFree);
                                 }
                              }
                              else
                              {
                                 throw new Exception("Cannot retrieve scratch_bookid from sales item ".ToString() + Environment.NewLine + this._sale.ToString() + Environment.NewLine + si.ToString());
                              }
                           }

                           foreach (ZGA_BDC_ORDER_ITEM_SEG orderItemSegment in orderMessage.ZgaBdcOrderItemSegList)
                           {
                              iDoc.AddSegment(orderItemSegment);
                           }
                           return iDoc;
                        }
                        else
                        {
                           throw new Exception("Cannot retreive sale_item from sales".ToString() + Environment.NewLine + this._sale.ToString());
                        }
                     }
                     else
                     {
                        throw new Exception("Cannot retreive Billing State from sales".ToString() + Environment.NewLine + this._sale.ToString() + Environment.NewLine + client.ToString() +
                           Environment.NewLine + clientBillingAddress.ToString());
                     }
                  }
                  else
                  {
                     throw new Exception("Cannot retreive Billing Country from sales".ToString() + Environment.NewLine + this._sale.ToString() + Environment.NewLine + client.ToString() + 
                        Environment.NewLine + clientBillingAddress.ToString());
                  }
               }
               else
               {
                  throw new Exception("Cannot retreive client_address from sales".ToString() + Environment.NewLine + this._sale.ToString() + Environment.NewLine + client.ToString());
               }
            }
            else
            {
               throw new Exception("Cannot retreive client from sales".ToString() + Environment.NewLine + this._sale.ToString());
            }
        }
    }
}
