using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;


using GA.BDC.Data;
using GA.BDC.Data.DataLayer;
using GA.BDC.Integration.MessageStructures;
using log4net;

namespace GA.BDC.Integration.MessageFormatters
{
    internal static class EZOrderSharedFormatting
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(EZOrderSharedFormatting));
        public static void FormatEZOrderHeaderSegment(ORDR_INVOIC_TO_PROCESS_TBL sale, ref ZGA_EZFUND_ORDER_HDR_SEG EZOrderHeaderSegment)
        {

            EZOrderHeaderSegment.Bdcrepid = sale.SLSP_CDE;
            EZOrderHeaderSegment.Fieldsalesrepid = Helper.SAP_BLANK.Trim();
            
            ORG_CTCT_TBL contact =EZOrders.GetClientID(sale.ORG_ID);

            List<ORDR_DISC_TBL> adjustments = EZOrders.GetAdjustmentsPerSale(sale.ORDR_ID); 
           

            EZOrderHeaderSegment.Leadid = contact.CTCT_ID.ToString().Trim();
            EZOrderHeaderSegment.Sqlid = EZOrderHeaderSegment.Orderrefid = sale.ORDR_ID.ToString().Trim();
            EZOrderHeaderSegment.Reqdelondate = sale.PDCT_SHIP_DTE;
            EZOrderHeaderSegment.Shipcond = Helper.BULK_ORDER;
            EZOrderHeaderSegment.Students = Helper.SAP_BLANK;
            EZOrderHeaderSegment.Signdate = sale.CREA_DTE;
            EZOrderHeaderSegment.Startdate = null;
            EZOrderHeaderSegment.Shippingfee = sale.SHIP_CHRG_CALC_AMT;
            EZOrderHeaderSegment.Shipinst = Helper.SAP_BLANK;
            EZOrderHeaderSegment.Currency = "USD";
            // add logic to distinguish sale from the salesscreen vs online store

            if (sale.ORDR_SRC_CDE != null)
                { sale.ORDR_SRC_CDE = sale.ORDR_SRC_CDE.Trim(); }

            if (sale.ORDR_SRC_CDE == "EZ-WEB" || sale.ORDR_SRC_CDE == "EZ-CALL" || sale.ORDR_SRC_CDE == "EZ-MAIL" || sale.ORDR_SRC_CDE == "EZ-FAX" || sale.ORDR_SRC_CDE == "EZ-EMAL")
            {
                EZOrderHeaderSegment.Origin = 'E';
            }
            else if (sale.ORDR_SRC_CDE == "HCM-WEB" || sale.ORDR_SRC_CDE == "HCM-CALL" || sale.ORDR_SRC_CDE == "HCM-FAX" || sale.ORDR_SRC_CDE == "HCM-MAIL" || sale.ORDR_SRC_CDE == "HCM-EMAL")
            {
                EZOrderHeaderSegment.Origin = 'H';
            }
            else
            {
                EZOrderHeaderSegment.Origin = 'E';
            }




            // add logic for discounts and surcharges
            EZOrderHeaderSegment.Surcharge = Decimal.Zero;
            EZOrderHeaderSegment.Discount = Decimal.Zero;
            if (adjustments != null && adjustments.Count > 0)
            {

                foreach (var adj in adjustments)
                {
                    if (adj.CHRG_FLG == true)
                    {
                        EZOrderHeaderSegment.Surcharge = EZOrderHeaderSegment.Surcharge + adj.DISC_AMT;
                     }
                    if (adj.CHRG_FLG == false)
                    {
                        EZOrderHeaderSegment.Discount = EZOrderHeaderSegment.Discount + adj.DISC_AMT;
                    }
                }
                //EZOrderHeaderSegment.Surcharge = adjustments.Where(a => a.CHRG_FLG == true).Select(a => a.DISC_AMT).FirstOrDefault();
                //EZOrderHeaderSegment.Discount = adjustments.Where(a => a.CHRG_FLG == false).Select(a => a.DISC_AMT).FirstOrDefault();
            }
            else
            {
                EZOrderHeaderSegment.Discount = Decimal.Zero;
                EZOrderHeaderSegment.Surcharge = Decimal.Zero;
            }

            

        }

        public static void FormatOrderAccountSegment(ORDR_INVOIC_TO_PROCESS_TBL sale, string type, bool isShipping, ref ZGA_EZFUND_ORDER_ACCT_SEG orderAccountSegment)
        {
            ORG_MSTR_TBL orgInformation = EZOrders.GetOrgInfo(sale.ORG_ID);

            orderAccountSegment.Type = type;
            orderAccountSegment.Name1 = sale.CTCT_NME; 
            orderAccountSegment.Name2 = string.IsNullOrEmpty(orgInformation.ORG_NME) ? sale.CTCT_NME : orgInformation.ORG_NME;
            orderAccountSegment.Street1 = sale.BILL_ADDR_1_TXT;
            orderAccountSegment.Street2 = string.IsNullOrEmpty(sale.BILL_ADDR_2_TXT) ? Helper.SAP_BLANK : sale.BILL_ADDR_2_TXT;
            orderAccountSegment.City = sale.BILL_CITY_NME;
            orderAccountSegment.Region = string.IsNullOrEmpty(sale.BILL_ST_CDE) ? Helper.SAP_BLANK : sale.BILL_ST_CDE.Trim();
            orderAccountSegment.Postcode = string.IsNullOrEmpty(sale.BILL_ZIP_CDE) ? Helper.SAP_BLANK : sale.BILL_ZIP_CDE.Trim();
            orderAccountSegment.Pobox = Helper.SAP_BLANK;
            orderAccountSegment.Poboxpostcode = Helper.SAP_BLANK;
            orderAccountSegment.Country = "US";
            orderAccountSegment.Email = string.IsNullOrEmpty(sale.CTCT_EML_TXT) ? Helper.SAP_BLANK : sale.CTCT_EML_TXT.Trim();
            orderAccountSegment.Dayphone = string.IsNullOrEmpty(sale.CTCT_PH_1_NBR) ? Helper.SAP_BLANK : Helper.FormatPhone(sale.CTCT_PH_1_NBR.Trim());
            orderAccountSegment.Dayphoneext = Helper.SAP_BLANK;
            orderAccountSegment.Evephone = string.IsNullOrEmpty(sale.CTCT_PH_2_NBR) ? Helper.SAP_BLANK : Helper.FormatPhone(sale.CTCT_PH_2_NBR.Trim());
            orderAccountSegment.Mobilephone =Helper.SAP_BLANK;
            orderAccountSegment.Fax =Helper.SAP_BLANK;
            orderAccountSegment.Faxext = Helper.SAP_BLANK;

        }

        public static void FormatOrderShippingSegment(ORDR_INVOIC_TO_PROCESS_TBL sale, string type, bool isShipping, ref ZGA_EZFUND_ORDER_ACCT_SEG orderAccountSegment)
        {
            ORG_MSTR_TBL orgInformation = EZOrders.GetOrgInfo(sale.ORG_ID);
            
            orderAccountSegment.Type = type;
            orderAccountSegment.Name1 = string.IsNullOrEmpty(orgInformation.ORG_NME) ? sale.CTCT_NME : orgInformation.ORG_NME;
            orderAccountSegment.Name2 = sale.CTCT_NME;
            orderAccountSegment.Street1 = sale.PDCT_SHIP_ADDR_1_TXT;
            orderAccountSegment.Street2 = string.IsNullOrEmpty(sale.PDCT_SHIP_ADDR_2_TXT) ? Helper.SAP_BLANK : sale.PDCT_SHIP_ADDR_2_TXT;
            orderAccountSegment.City = sale.PDCT_SHIP_CITY_NME;
            orderAccountSegment.Region = string.IsNullOrEmpty(sale.PDCT_SHIP_ST_CDE) ? Helper.SAP_BLANK : sale.PDCT_SHIP_ST_CDE.Trim();
            orderAccountSegment.Postcode = string.IsNullOrEmpty(sale.PDCT_SHIP_ZIP_CDE) ? Helper.SAP_BLANK : sale.PDCT_SHIP_ZIP_CDE.Trim();
            orderAccountSegment.Pobox = Helper.SAP_BLANK;
            orderAccountSegment.Poboxpostcode = Helper.SAP_BLANK;
            orderAccountSegment.Country = "US";
            orderAccountSegment.Email = string.IsNullOrEmpty(sale.CTCT_EML_TXT) ? Helper.SAP_BLANK : sale.CTCT_EML_TXT.Trim();
            orderAccountSegment.Dayphone = string.IsNullOrEmpty(sale.CTCT_PH_1_NBR) ? Helper.SAP_BLANK : Helper.FormatPhone(sale.CTCT_PH_1_NBR.Trim());
            orderAccountSegment.Dayphoneext = Helper.SAP_BLANK;
            orderAccountSegment.Evephone = string.IsNullOrEmpty(sale.CTCT_PH_2_NBR) ? Helper.SAP_BLANK : Helper.FormatPhone(sale.CTCT_PH_2_NBR.Trim());
            orderAccountSegment.Mobilephone = Helper.SAP_BLANK;
            orderAccountSegment.Fax = Helper.SAP_BLANK;
            orderAccountSegment.Faxext = Helper.SAP_BLANK;

        }



        public static void FormatEZOrderOrderItemSegment(ITEM_LKUP_TBL scratchBook, ORDR_ITEM_TBL salesItem, bool freeOfCharge, ref ZGA_EZFUND_ORDER_ITEM_SEG orderItemSegment)
        {

            ORDR_VEND_TBL orderVendorInfo = EZOrders.GetOrderVendorInfo(salesItem.ORDR_SUB_ID);
           

            if (scratchBook != null && scratchBook.SAPMaterialNo != null)
            {
                orderItemSegment.Materialno = scratchBook.SAPMaterialNo.ToString().Trim();
            }
            else
            {
                orderItemSegment.Materialno = Helper.SAP_BLANK;
            }
            //if (freeOfCharge)
            //{
            //    orderItemSegment.Profitpct = Helper.SAP_BLANK;
            //}


            orderItemSegment.Profitpct = Helper.SAP_BLANK;
            orderItemSegment.Pricecode = Helper.IsTaxExluded(true);
            orderItemSegment.Quantity = salesItem.ITEM_PO_QTY;
            orderItemSegment.Unitprice = salesItem.ITEM_INVOIC_EXT_AMT;
            orderItemSegment.Uom = null;
            orderItemSegment.Free = freeOfCharge;
            orderItemSegment.Shipsource = string.IsNullOrEmpty(orderVendorInfo.VEND_CDE) ? Helper.SAP_BLANK :  Helper.GetShippingSource(orderVendorInfo.VEND_CDE);
        }
       

    }
}
