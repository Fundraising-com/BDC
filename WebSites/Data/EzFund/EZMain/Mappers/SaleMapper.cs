using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GA.BDC.Shared.Entities;
using GA.BDC.Data.EzFund.EZMain.Tables;

namespace GA.BDC.Data.EzFund.EZMain.Mappers
{
    public static class SaleMapper
    {
        public static ordr_invoic_tbl Dehydrate(EzFundSale sale)
        {
            var creditCardExpirationDate = new DateTime();
            var fakeCCNumber = string.Empty;

            if (sale.InternalPaymentMethod != EzFundInternalPaymentMethod.PO)
            {
                if (sale.CreditCard != null)
                {
                    creditCardExpirationDate = DateTime.Parse("20" + sale.CreditCard.ExpirationDate.Substring(2, 2) + "-" + sale.CreditCard.ExpirationDate.Substring(0, 2) + "-01");

                    if (sale.InternalPaymentMethod == EzFundInternalPaymentMethod.VISA)
                    {
                        fakeCCNumber = "4111111111111111";
                    }
                    else if (sale.InternalPaymentMethod == EzFundInternalPaymentMethod.MASTERCARD)
                    {
                        fakeCCNumber = "5424000000000015";
                    }
                    else if (sale.InternalPaymentMethod == EzFundInternalPaymentMethod.AMEX)
                    {
                        fakeCCNumber = "370000000000002";
                    }
                    else if (sale.InternalPaymentMethod == EzFundInternalPaymentMethod.DISCOVER)
                    {
                        fakeCCNumber = "6011000000000012";
                    }

                }
            }
            else {
                creditCardExpirationDate = DateTime.Today;
           }

            var result = new ordr_invoic_tbl
            {
                ORDR_TYPE_CDE = "DIRSLS",
                ORG_ID = sale.OrganizationId,
                CPGN_ID = 0,
                PRIM_INVOIC_FLG = true,
                PO_REQD_FLG = true,
                STAX_AMT = null,
                SHIP_CHRG_CALC_AMT = sale.ShippingCalculatedAmount, // (decimal)0.0,
                SHIP_CHRG_OVRD_AMT = null,
                TOTL_CHRG_AMT = (decimal)sale.TotalAmount,
                PTYP_CDE = sale.InternalPaymentMethod.ToString(), //check
                PMT_REF_NBR = fakeCCNumber.ToString(), 
                //PMT_REF_NBR = sale.SaleAuthorizationNumber, //This is the third party payment authorization number
                PMT_CCRD_EXPIRE_DTE = creditCardExpirationDate,//check
                PMT_APVL_FLG = null,
                BILL_ADDR_1_TXT = sale.Client.Addresses[0].Address1,
                BILL_ADDR_2_TXT = sale.Client.Addresses[0].Address2,
                BILL_ADDR_3_TXT = null,
                BILL_CITY_NME = sale.Client.Addresses[0].City,
                BILL_ST_CDE = sale.Client.Addresses[0].Region.Code,
                BILL_ZIP_CDE = sale.Client.Addresses[0].PostCode,
                CTCT_NME = sale.Client.FirstName + " " + sale.Client.LastName,
                CTCT_PH_1_NBR = sale.Client.Phone,
                CTCT_EML_TXT = sale.Client.Email,
                ALT_CTCT_NME = null,
                ALT_CTCT_PH_1_NBR = null,
                ALT_CTCT_PH_2_NBR = null,
                ALT_CTCT_EML_TXT = null,
                PDCT_SHIP_ADDR_1_TXT = sale.Client.Addresses[1].Address1,
                PDCT_SHIP_ADDR_2_TXT = sale.Client.Addresses[1].Address2,
                PDCT_SHIP_ADDR_3_TXT = null,
                PDCT_SHIP_CITY_NME = sale.Client.Addresses[1].City,
                PDCT_SHIP_ST_CDE = sale.Client.Addresses[1].Region.Code,
                PDCT_SHIP_ZIP_CDE = sale.Client.Addresses[1].PostCode,
                PDCT_SHIP_DTE = DateTime.Now,
                PDCT_SHIP_VIA_CDE = null,
                PDCT_SHIP_TRACK_NBR = null,
                PDCT_SHIP_ADDL_TXT = null,
                PRZP_SHIP_ADDL_TXT = null,
                PRZP_SHIP_ADDR_1_TXT = null,
                PRZP_SHIP_ADDR_2_TXT = null,
                PRZP_SHIP_ADDR_3_TXT = null,
                PRZP_SHIP_CITY_NME = null,
                PRZP_SHIP_DTE = null,
                PRZP_SHIP_ST_CDE = null,
                PRZP_SHIP_TRACK_NBR = null,
                PRZP_SHIP_VIA_CDE = null,
                PRZP_SHIP_ZIP_CDE = null,
                DLVY_CMNT_TXT = sale.DeliveryComments,
                SLSP_CDE = "EZ_WEB", //check the name
                AR_POST_DTE = null,
                SENT_TO_ACTG_FLG = false,
                SENT_TO_ORG_FLG = false,
                CREA_DTE = DateTime.Now,
                CREA_PRSN_CDE = "EZ_WEB", //check the name
                LAST_MODF_DTE = DateTime.Now,
                LAST_MODF_PRSN_CDE = "EZ_WEB", //check the name
                LAST_STAT_DTE = DateTime.Now,
                LAST_STAT_CDE = sale.Status.ToString(),
                ORDR_SRC_LKUP_TBL_ID = null,
                ORDR_SRC_CDE = "EZ-WEB",
                ORDR_REF = sale.ReferralCode

               

            };
            return result;
        }
        public static ordr_vend_tbl DehydrateVendor(EzFundSaleVendor vendor, int orderId) {
            var vendorOrder = new ordr_vend_tbl
            {
                ORDR_ID = orderId,
                PGM_CDE = "DIRSLS",
                SRC_GRP =  "PDCT", // check
                SRC_CDE = "DIRSLS",
                OFRM_ASSY_CDE = "DIRSLS",
                OFRM_CDE = vendor.OFRMCode,
                VEND_CDE = vendor.VendorCode,
                WHSE_CDE = vendor.WarehouseCode,
                PDCT_CDE = vendor.ProductCode,
                EZF_PO_NBR = null,
                PO_DTE = null, // it may change during the process
                PO_CONFO_NBR = null, // it may change during the process
                PO_CONFO_DTE = null, // it may change during the process
                BILL_RCVD_DTE = null, // it may change during the process
                SENT_TO_VEND_FLG = false,
                SHOW_REBT_FLG = false,
                DLVY_CMNT_TXT = null, // it may change during the process
                LAST_MODF_DTE = DateTime.Now,
                LAST_MODF_PRSN_CDE = "EZ_WEB",
                LAST_STAT_DTE = DateTime.Now,
                LAST_STAT_CDE = vendor.Status.ToString()  // it may change during the process
            };
            return vendorOrder;
        }
        public static ordr_item_tbl DehydrateItem(SubProduct subProduct, int ordrSubId)
        {
            var item = new ordr_item_tbl
            {
                ORDR_SUB_ID = ordrSubId,
                ITEM_CDE = subProduct.ItemCode,
                ITEM_RAW_QTY = subProduct.StackedQuantity,
                ITEM_EXTRA_QTY = 0,
                ITEM_REBT_QTY = 0,
                ITEM_RAW_UOM_TXT = subProduct.SizeName, // confirm this parameter
                ITEM_PO_QTY = subProduct.StackedQuantity,
                ITEM_PO_UOM_TXT = subProduct.SizeName, // confirm this parameter
                ITEM_INVOIC_QTY = subProduct.StackedQuantity,
                ITEM_INVOIC_UOM_TXT = subProduct.SizeName, // confirm this parameter
                ITEM_INVOIC_UNIT_AMT = (decimal)subProduct.Price,
                ITEM_INVOIC_EXT_AMT = (decimal)(subProduct.Price * subProduct.StackedQuantity),
                ITEM_SLS_UNIT_AMT = (decimal)0.0,
                ITEM_SLS_EXT_AMT = (decimal)0.0,
                ITEM_CMNT_TXT = null //check if needed
            };
            return item;          
        }

        public static EzFundSale Hydrate(ordr_invoic_tbl sale)
        {
            var result = new EzFundSale
            {
                OrderId = sale.ORDR_ID,
                ReferralCode = sale.ORDR_REF,
                OrderTypeCode = sale.ORDR_TYPE_CDE,
                OrganizationId = sale.ORG_ID,
                ShippingCalculatedAmount = sale.SHIP_CHRG_CALC_AMT ?? 0,
                ShippingOverAmount = sale.SHIP_CHRG_OVRD_AMT ?? 0,
                TotalAmount = sale.TOTL_CHRG_AMT ?? 0,
                InternalPaymentMethod = (EzFundInternalPaymentMethod)Enum.Parse(typeof(EzFundInternalPaymentMethod), sale.PTYP_CDE), 
                SaleAuthorizationNumber = sale.PMT_REF_NBR,
                Client = new Client {
                    Phone = sale.CTCT_PH_1_NBR,
                    Email = sale.CTCT_EML_TXT,
                    FirstName = sale.CTCT_NME.Substring(0, sale.CTCT_NME.IndexOf(" ")),
                    LastName = sale.CTCT_NME.Substring(sale.CTCT_NME.IndexOf(" "), sale.CTCT_NME.Length - sale.CTCT_NME.IndexOf(" ")),
                },
                DeliveryComments = sale.DLVY_CMNT_TXT,
                Status = (EzFundSaleStatus)Enum.Parse(typeof(EzFundSaleStatus), sale.LAST_STAT_CDE)                
            };
            /*Billing Address*/
            result.Client.Addresses.Add(
                new ClientAddress
                {
                    Address1 = sale.BILL_ADDR_1_TXT,
                    Address2 = sale.BILL_ADDR_2_TXT,
                    City = sale.BILL_CITY_NME,
                    Region = new Region {
                        Code = sale.BILL_ST_CDE
                    }, 
                    PostCode = sale.BILL_ZIP_CDE,
                    Type = sale.PDCT_SHIP_ADDL_TXT
                });
            /*Shiping Address*/
            result.Client.Addresses.Add(
                new ClientAddress
                {
                    Address1 = sale.PDCT_SHIP_ADDR_1_TXT,
                    Address2 = sale.PDCT_SHIP_ADDR_2_TXT,
                    City = sale.PDCT_SHIP_CITY_NME,
                    Region = new Region
                    {
                        Code = sale.PDCT_SHIP_ST_CDE
                    },
                    PostCode = sale.PDCT_SHIP_ZIP_CDE,
                });
            return result;
        }

        public static EzFundSaleVendor HydrateVendor(ordr_vend_tbl vendor) {
            var ezVendor = new EzFundSaleVendor
            {
                SubOrderId = vendor.ORDR_SUB_ID,
                OFRMCode = vendor.OFRM_CDE,
                VendorCode = vendor.VEND_CDE,
                WarehouseCode = vendor.WHSE_CDE,
                ProductCode = vendor.PDCT_CDE,
                Status  = (EzFundSaleStatus)Enum.Parse(typeof(EzFundSaleStatus), vendor.LAST_STAT_CDE)
            };
            return ezVendor;
        }

        public static SubProduct HydrateItem(ordr_item_tbl item, int parentId) {
            var ezItem = new SubProduct
            {
                ItemCode = item.ITEM_CDE,
                ParentId = parentId,
                SelectedQuantity = item.ITEM_INVOIC_QTY??0,
                SizeName = item.ITEM_RAW_UOM_TXT,
                Price = (double)(item.ITEM_INVOIC_UNIT_AMT??0),
            };
            return ezItem;
        }

        public static ordr_item_tbl HydrateItem(ordr_item_tbl items)
        {
            var item = new ordr_item_tbl
            {
                ORDR_SUB_ID = items.ORDR_SUB_ID,
                ITEM_CDE = items.ITEM_CDE,
                ITEM_RAW_QTY = items.ITEM_RAW_QTY,
                ITEM_EXTRA_QTY = 0,
                ITEM_REBT_QTY = 0,
                ITEM_RAW_UOM_TXT = items.ITEM_RAW_UOM_TXT, // confirm this parameter
                ITEM_PO_QTY = items.ITEM_PO_QTY,
                ITEM_PO_UOM_TXT = items.ITEM_PO_UOM_TXT, // confirm this parameter
                ITEM_INVOIC_QTY = items.ITEM_INVOIC_QTY,
                ITEM_INVOIC_UOM_TXT = items.ITEM_INVOIC_UOM_TXT, // confirm this parameter
                ITEM_INVOIC_UNIT_AMT = (decimal)items.ITEM_INVOIC_UNIT_AMT,
                ITEM_INVOIC_EXT_AMT = (decimal)(items.ITEM_INVOIC_UNIT_AMT * items.ITEM_RAW_QTY),
                ITEM_SLS_UNIT_AMT = (decimal)0.0,
                ITEM_SLS_EXT_AMT = (decimal)0.0,
                ITEM_CMNT_TXT = null //check if needed
            };
            return item;
        }
    }
}
