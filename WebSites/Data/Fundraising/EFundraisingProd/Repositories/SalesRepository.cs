using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Configuration;
using GA.BDC.Data.Fundraising.EFundraisingProd.Mappers;
using GA.BDC.Data.Fundraising.EFundraisingProd.Tables;
using GA.BDC.Data.Fundraising.EFundStore.Repositories;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundraisingProd.Repositories
{
    public class SalesRepository : ISalesRepository
    {
        private readonly DataProvider _dataProvider;
        private readonly EFundStore.Tables.DataProvider _efundStoreDataProvider;
        public SalesRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            _efundStoreDataProvider = new EFundStore.Tables.DataProvider();
            _efundStoreDataProvider.Database.BeginTransaction();
        }

        public IList<Sale> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Save(Sale sale)
        {
            var creditCard = _dataProvider.Database.SqlQuery<byte[]>("SELECT EncryptByPassPhrase('j7@3bv!009', '" + sale.CreditCard.Number + "');").First();
            sale.Id = _dataProvider.sales.Max(p => p.sales_id) + 1;
            var saleToBePersisted = SaleMapper.Dehydrate(sale);
            saleToBePersisted.credit_card_no = creditCard;

            _dataProvider.sales.Add(saleToBePersisted);
            _dataProvider.SaveChanges();

            foreach (var saleItem in sale.Items)
            {
                saleItem.SaleId = sale.Id;
            }
            var saleItemsToBePersisted = SaleMapper.DehydrateItem(sale.Items);
            foreach (var saleItem in saleItemsToBePersisted)
            {
                _dataProvider.sales_item.Add(saleItem);
                _dataProvider.SaveChanges();
            }
            var saleComment = new Comment
            {
                Comments = sale.Client.Comments,
                Entry_Date = DateTime.Now,
                Sales_ID = saleToBePersisted.sales_id,
                Comments_ID = _dataProvider.Comments.Max(p => p.Comments_ID) + 1
            };
            _dataProvider.Comments.Add(saleComment);
            _dataProvider.SaveChanges();

            foreach (var appliedTax in sale.Taxes)
            {
                appliedTax.SaleId = sale.Id;
                _dataProvider.Applicable_Tax.Add(AppliedTaxMapper.Dehydrate(appliedTax));
                _dataProvider.SaveChanges();
            }
            if (sale.PromotionCode != null)
            {
                var adjustment = new Adjustment
                {
                    Create_Date = DateTime.Now,
                    Sales_ID = sale.Id,
                    Adjustment_No = 1,
                    Reason_ID = 35, //Reason: Promotion Code
                    Adjustment_Date = DateTime.Now,
                    Adjustment_Amount = (decimal)sale.PromotionCode.DiscountedAmount,
                    Ext_Adjustment_Id = sale.PromotionCodeId
                };
                _dataProvider.Adjustments.Add(adjustment);
                _dataProvider.SaveChanges();
            }
            return saleToBePersisted.sales_id;
        }

        public Sale GetById(int id)
        {
            const string sql = @"SELECT TOP 1 sales_id, consultant_id, carrier_id, shipping_option_id, payment_term_id, client_sequence_code, client_id, sales_status_id, payment_method_id, po_status_id, production_status_id, sponsor_consultant_id, ar_consultant_id, ar_status_id, lead_id, billing_company_id, upfront_payment_method_id, confirmer_id, collection_status_id, confirmation_method_id, credit_approval_method_id, po_number, expiry_date, sales_date, shipping_fees, shipping_fees_discount, payment_due_date, confirmed_date, scheduled_delivery_date, scheduled_ship_date, actual_ship_date, waybill_no, comment, coupon_sheet_assigned, total_amount, invoice_date, cancellation_date, is_ordered, po_received_on, is_delivered, local_sponsor_found, box_return_date, reship_date, upfront_payment_required, upfront_payment_due_date, sponsor_required, actual_delivery_date, accounting_comments, ssn_number, ssn_address, ssn_city, ssn_state_code, ssn_country_code, ssn_zip_code, is_validated, promised_due_date, general_flag, fuelsurcharge, is_packed_by_participant, carrier_tracking_id, qsp_order_id, ext_order_id, credit_card_no, wfc_invoice_number, cvv2, po_consultant_commission, ext_sales_status_id, ext_shipping_account_id, ext_billing_account_id, promotion_code_id FROM sale S (NOLOCK) WHERE S.sales_id = @id;
SELECT Sales_ID, Adjustment_No, Reason_ID, Adjustment_Date, Adjustment_Amount, Comment, Adjustment_On_Shipping, Adjustment_On_Taxes, Adjustment_On_Sale_Amount, Create_Date, Create_User_ID, Ext_Adjustment_Id, charge_id, msrepl_tran_version FROM Adjustment A (NOLOCK) WHERE A.Sales_ID = @id;
SELECT sales_id, sales_item_no, scratch_book_id, service_type_id, product_class_id, group_name, quantity_sold, unit_price_sold, quantity_free, suggested_coupons, sales_amount, paid_amount, adjusted_amount, discount_amount, sales_commission_amount, sponsor_commission_amount, nb_units_sold, manual_product_description, profit_margin, participant_id FROM sales_item SI (NOLOCK) WHERE SI.sales_id = @id;
SELECT Sales_ID, Tax_Code, Tax_Amount FROM Applicable_Tax AT (NOLOCK) WHERE AT.Sales_ID = @id;";
            var multi = _dataProvider.Database.Connection.QueryMultiple(sql, new { id },
               _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            var sale = multi.Read<sale>().First();
            var adjustments = multi.Read<Adjustment>().ToList();
            var result = SaleMapper.Hydrate(sale, adjustments);
            var saleItems = multi.Read<sales_item>().ToList();
            result.Items = SaleMapper.HydrateItems(saleItems);
            var applicableTaxes = multi.Read<Applicable_Tax>();
            foreach (var applicableTax in applicableTaxes)
            {
                result.Taxes.Add(AppliedTaxMapper.Hydrate(applicableTax));
            }
            if (result.PromotionCodeId > 0)
            {
                var promotionCode = _dataProvider.Database.Connection.Query<Promotion_Code>("SELECT TOP 1 * FROM Promotion_Code (NOLOCK) WHERE Promotion_Code_ID = @id", new { id = result.PromotionCodeId }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).First();
                var productIds = _dataProvider.Database.Connection.Query<int>("SELECT scratch_book_id FROM Promotion_Code_Product (NOLOCK) WHERE Promotion_Code_ID = @id", new { id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
                var productRepository = new ProductRepository(_efundStoreDataProvider);
                result.PromotionCode = PromotionCodeMapper.Hydrate(promotionCode);
                result.PromotionCode.Products = productIds.Select(p => productRepository.GetByScratchbookId(p)).ToList();
                result.PromotionCode.DiscountedAmount = (double)_dataProvider.Adjustments.First(p => p.Sales_ID == result.Id && p.Reason_ID == 35).Adjustment_Amount;
            }
            return result;
        }

        public IList<Sale> GetSaleShippingDetails()
        {
            var ids =
               _dataProvider.Database.Connection.Query<int>(
                  "SELECT sales_id FROM sale S (NOLOCK) WHERE confirmed_date > '2018-12-01' and (waybill_no <> NULL or waybill_no <> '') and is_delivered = 0;", 
                  _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return ids.Select(GetById).ToList();
        }


        public IList<Sale> GetByClientId(int clientId)
        {
            var ids =
               _dataProvider.Database.Connection.Query<int>(
                  "SELECT sales_id FROM sale S (NOLOCK) WHERE S.client_id = @clientId AND S.client_sequence_code IN ('OF','IF');", new { clientId },
                  _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return ids.Select(GetById).ToList();
        }

        public IList<Sale> GetByForSSClientId(int clientId)
        {
            var ids =
               _dataProvider.Database.Connection.Query<int>(
                  "SELECT sales_id FROM sale S (NOLOCK) WHERE S.client_id = @clientId AND S.client_sequence_code IN ('OF','IF','UI');", new { clientId },
                  _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return ids.Select(GetById).ToList();
        }

        public IList<Sale> GetRequiredFollowUpNotification(DateTime dateToUse)
        {
            const string sql = "SELECT s.sales_id, s.client_id " +
                               " FROM dbo.sale as s (NOLOCK) " +
                               " LEFT JOIN efundstore.dbo.notification as noti (NOLOCK) ON s.client_id = noti.external_id " +
                               " AND noti.type = @notificationType " +
                               " WHERE noti.id IS NULL " +
                               " AND s.sales_date > @limit";

            var ids = (_dataProvider.Database.Connection.Query<int>(sql, new { limit = dateToUse, notificationType = NotificationType.OrderFollowUp },
                 _dataProvider.Database.CurrentTransaction.UnderlyingTransaction)).ToList();

            return ids.Select(GetById).ToList();


        }

        public IList<Sale> GetPaidSales(DateTime limitDate)
        {
            const string sql = "SELECT s.sales_id " +
                               " FROM dbo.sale as s (NOLOCK) " +
                               " JOIN dbo.consultant as C (NOLOCK) ON s.consultant_id = C.consultant_id" +
                               " LEFT JOIN efundstore.dbo.notification as noti (NOLOCK) ON s.client_id = noti.external_id " +
                               " AND noti.type = @notificationType " +
                               " WHERE noti.id IS NULL " +
                               " AND s.payment_method_id = @paymentMethod" +
                               " AND C.division_id = 2" +
                               " AND s.ar_status_id = @arStatus" +
                               " AND s.sales_date > @limit";
            var ids = (_dataProvider.Database.Connection.Query<int>(sql, new { limit = limitDate, notificationType = NotificationType.SalePaid, paymentMethod = InternalPaymentMethod.PurchaseOrder, arStatus = ARStatus.Paid },
                  _dataProvider.Database.CurrentTransaction.UnderlyingTransaction)).ToList();

            return ids.Select(GetById).ToList();
        }

        public IList<Sale> GetByPromotionCodeUsed(int promotionCodeId)
        {
            const string sql = "SELECT sales_id FROM sale S (NOLOCK) WHERE S.promotion_code_id = @promotionCodeId;";
            var ids = _dataProvider.Database.Connection.Query<int>(sql, new { promotionCodeId }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return ids.Select(GetById).ToList();
        }

        public void Update(Sale sale)
        {

            if (sale.Confirmed == null || sale.Confirmed == DateTime.MinValue)
            {
                sale.Confirmed = null;
            }

            var saleToBeUpdated = _dataProvider.sales.Find(sale.Id);
            saleToBeUpdated.sales_status_id = (int)sale.Status;
            saleToBeUpdated.ar_status_id = (int)sale.ARStatus;
            saleToBeUpdated.confirmed_date = sale.Confirmed;
            
            saleToBeUpdated.scheduled_delivery_date = sale.ScheduledDelivery;
            _dataProvider.SaveChanges();
        }

        //UpdateSaleIsDelivered(int saleId);

        public void Delete(Sale model)
      {
         throw new NotImplementedException();
      }

        public void SaveVendorAndItems(IList<EzFundSaleVendor> vendors, int saleId)
        {
            throw new NotImplementedException();
        }


        public void Update(EzFundSale model)
        {


            throw new NotImplementedException();

        }


        public int Save(EzFundSale sale)
        {
            throw new NotImplementedException();
        }

        public EzFundSale GetEzFundSaleByOrderId(int orderId)
        {
            throw new NotImplementedException();
        }

		public bool UpdateFundPaymentReferenceByOrderId(int reference, string authNumber)
		{
			throw new NotImplementedException();
		}

        public Sale GetSaleItemsByScratchbookID(int id)
        {
            throw new NotImplementedException();
        }
    }
}
