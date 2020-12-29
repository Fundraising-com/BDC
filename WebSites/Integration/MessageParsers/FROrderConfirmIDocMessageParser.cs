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
   internal sealed class FROrderConfirmIDocMessageParser:IDocMessageParser<ZGA_BDC_ORDER_CONF>
   {
      private ZGA_BDC_ORDER_CONF _orderConfirmIDoc;


      public FROrderConfirmIDocMessageParser(byte[] input):base(input)
      {
         this._orderConfirmIDoc = new ZGA_BDC_ORDER_CONF(true);
      }

      protected override void ProcessSegment(byte[] segment)
      {
         this._orderConfirmIDoc.ZgaBdcOrderConfSeg = this.DeserializeSegment<ZGA_BDC_ORDER_CONF_SEG>(segment);
      }

      protected override void ReconcileAndIntegrate()
      {
         int orderRefID;


         if (!string.IsNullOrEmpty(this._orderConfirmIDoc.ZgaBdcOrderConfSeg.Orderrefid))
         {
            try
            {
               orderRefID = Convert.ToInt32(this._orderConfirmIDoc.ZgaBdcOrderConfSeg.Orderrefid);
            }
            catch (Exception ex)
            {
               throw new Exception("Cannot parse Order Reference ID from ZGA_BDC_ORDER_CONF_SEG".ToString() + Environment.NewLine + this._orderConfirmIDoc.ZgaBdcOrderConfSeg.Orderrefid, ex.InnerException);
            }
            sale sale = Orders.GetSale(orderRefID);
            if (sale != null)
            {
               if (this._orderConfirmIDoc.ZgaBdcOrderConfSeg.Actshipdate != null)
               {

                  sale.actual_ship_date = this._orderConfirmIDoc.ZgaBdcOrderConfSeg.Actshipdate;
               }

               if (this._orderConfirmIDoc.ZgaBdcOrderConfSeg.Orderno != null)
               {
                  try
                  {
                     sale.ext_order_id = Convert.ToInt32(this._orderConfirmIDoc.ZgaBdcOrderConfSeg.Orderno);
                  }
                  catch (Exception)
                  {

                  }
               }

               

               if (!string.IsNullOrEmpty(this._orderConfirmIDoc.ZgaBdcOrderConfSeg.Billtoacct))
               {
                  sale.ext_billing_account_id = this._orderConfirmIDoc.ZgaBdcOrderConfSeg.Billtoacct;
               }

               if (!string.IsNullOrEmpty(this._orderConfirmIDoc.ZgaBdcOrderConfSeg.Shiptoacct))
               {
                  sale.ext_shipping_account_id = this._orderConfirmIDoc.ZgaBdcOrderConfSeg.Shiptoacct;
               }


               if (!string.IsNullOrEmpty(this._orderConfirmIDoc.ZgaBdcOrderConfSeg.Trackno))
               {
                  sale.waybill_no = this._orderConfirmIDoc.ZgaBdcOrderConfSeg.Trackno;
               }

               if (!string.IsNullOrEmpty(this._orderConfirmIDoc.ZgaBdcOrderConfSeg.Scac))
               {
                  carrier c = Orders.GetCarrierBySCAC(Helper.ConvertSCAC(this._orderConfirmIDoc.ZgaBdcOrderConfSeg.Scac));
                  if (c != null)
                  {
                     sale.carrier_id = c.carrier_id;
                  }
               }
               Orders.ConfirmSale(sale, Helper.ShippedSale, Helper.SaleInSAPWithPay, Helper.SalesScreenShippedSale );
            }
            
            else
            {
               throw new Exception("Cannot find sale id from ZGA_BDC_ORDER_CONF_SEG: ".ToString() + orderRefID.ToString() + " cannot update sale".ToString());
            }
         }
         else
         {
            throw new Exception("Cannot retrieve Order Reference ID from ZGA_BDC_ORDER_CONF_SEG".ToString() + Environment.NewLine + this._orderConfirmIDoc.ZgaBdcOrderConfSeg.Orderrefid);
         }
      }
   }
}
