using System;
using System.Collections.Generic;
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
    internal sealed class EZOrderConfirmIDocMessageParser : IDocMessageParser<ZGA_EZFUND_ORDER_CONF>
    {
        private ZGA_EZFUND_ORDER_CONF _orderConfirmIDoc;


        public EZOrderConfirmIDocMessageParser(byte[] input) : base(input)
        {
            this._orderConfirmIDoc = new ZGA_EZFUND_ORDER_CONF(true);
        }

        protected override void ProcessSegment(byte[] segment)
        {
            this._orderConfirmIDoc.ZgaEZFUNDOrderConfSeg = this.DeserializeSegment<ZGA_EZFUND_ORDER_CONF_SEG>(segment);
        }

        protected override void ReconcileAndIntegrate()
        {
            int orderRefID;


            if (!string.IsNullOrEmpty(this._orderConfirmIDoc.ZgaEZFUNDOrderConfSeg.Orderrefid))
            {
                try
                {
                    orderRefID = Convert.ToInt32(this._orderConfirmIDoc.ZgaEZFUNDOrderConfSeg.Orderrefid);
                }
                catch (Exception ex)
                {
                    throw new Exception("Cannot parse Order Reference ID from ZGA_EZFUND_ORDER_CONF_SEG".ToString() + Environment.NewLine + this._orderConfirmIDoc.ZgaEZFUNDOrderConfSeg.Orderrefid, ex.InnerException);
                }
                ORDR_INVOIC_TO_PROCESS_TBL sale = EZOrders.GetSale(orderRefID);
                
                if (sale != null)
                {
                    if (this._orderConfirmIDoc.ZgaEZFUNDOrderConfSeg.Actshipdate != null)
                    {
                        sale.PDCT_SHIP_DTE = this._orderConfirmIDoc.ZgaEZFUNDOrderConfSeg.Actshipdate;
                    }

                    if (this._orderConfirmIDoc.ZgaEZFUNDOrderConfSeg.Orderrefid != null)
                    {
                        try
                        {
                            sale.ext_order_id = Convert.ToInt32(this._orderConfirmIDoc.ZgaEZFUNDOrderConfSeg.Orderno);
                        }
                        catch (Exception)
                        {

                        }
                    }



                    //if (!string.IsNullOrEmpty(this._orderConfirmIDoc.ZgaEZFUNDOrderConfSeg.Billtoacct))
                    //{
                    //    sale.ext_billing_account_id = this._orderConfirmIDoc.ZgaEZFUNDOrderConfSeg.Billtoacct;
                    //}

                    //if (!string.IsNullOrEmpty(this._orderConfirmIDoc.ZgaEZFUNDOrderConfSeg.Shiptoacct))
                    //{
                    //    sale.ext_shipping_account_id = this._orderConfirmIDoc.ZgaEZFUNDOrderConfSeg.Shiptoacct;
                    //}


                    if (!string.IsNullOrEmpty(this._orderConfirmIDoc.ZgaEZFUNDOrderConfSeg.Trackno))
                    {
                        sale.PDCT_SHIP_TRACK_NBR = this._orderConfirmIDoc.ZgaEZFUNDOrderConfSeg.Trackno;
                    }

                    if (!string.IsNullOrEmpty(this._orderConfirmIDoc.ZgaEZFUNDOrderConfSeg.Scac))
                    {
                        carrier c = Orders.GetCarrierBySCAC(Helper.ConvertSCAC(this._orderConfirmIDoc.ZgaEZFUNDOrderConfSeg.Scac));
                        if (c != null)
                        {
                            sale.PDCT_SHIP_VIA_CDE = c.description;
                        }
                    }
                    EZOrders.ConfirmSale(sale, Helper.ShippedSale, Helper.SaleInSAPWithPay);
                }

                else
                {
                    throw new Exception("Cannot find sale id from ZGA_EZFUND_ORDER_CONF_SEG: ".ToString() + orderRefID.ToString() + " cannot update sale".ToString());
                }
            }
            else
            {
                throw new Exception("Cannot retrieve Order Reference ID from ZGA_EZFUND_ORDER_CONF_SEG".ToString() + Environment.NewLine + this._orderConfirmIDoc.ZgaEZFUNDOrderConfSeg.Orderrefid);
            }
        }
    }
}
