using GA.BDC.Shared.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GA.BDC.Shared.Entities;
using GA.BDC.Data.EzFund.EZMain.Tables;
using GA.BDC.Data.EzFund.EZMain.Mappers;
using Dapper.Contrib.Extensions;
using Dapper;

namespace GA.BDC.Data.EzFund.EZMain.Repositories
{
    public class SalesRepository : ISalesRepository
    {
        private readonly DataProvider _dataProvider;

        public SalesRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void Delete(Sale model)
        {
            throw new NotImplementedException();
        }

        public IList<Sale> GetAll()
        {
            throw new NotImplementedException();
        }

        public IList<Sale> GetByClientId(int clientId)
        {
            throw new NotImplementedException();
        }

	    public IList<Sale> GetByForSSClientId(int clientId)
	    {
		    throw new NotImplementedException();
	    }

	    public Sale GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Sale> GetByPromotionCodeUsed(int promotionCodeId)
        {
            throw new NotImplementedException();
        }

        public IList<Sale> GetPaidSales(DateTime limitDate)
        {
            throw new NotImplementedException();
        }

        public IList<Sale> GetRequiredFollowUpNotification(DateTime d)
        {
            throw new NotImplementedException();
        }

        public int Save(EzFundSale sale)
        {
            /*Insert the Sale in ORDR_INVOIC_TBL*/
            var creditCard = _dataProvider.Database.SqlQuery<byte[]>("SELECT EncryptByPassPhrase('j7@3bv!009', '" + sale.CreditCard.Number + "');").First();
            var saleToBePersisted = SaleMapper.Dehydrate(sale);
            var saleId = _dataProvider.Database.Connection.Insert(saleToBePersisted, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return (int)saleId;
        }

        public void SaveVendorAndItems(IList<EzFundSaleVendor> vendors, int saleId)
        {
            /*Insert each vendor row*/
            foreach (var vendor in vendors) {
                var vendorEntry = SaleMapper.DehydrateVendor(vendor, saleId);
                var orderSubId = (int)_dataProvider.Database.Connection.Insert(vendorEntry, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
                /*Insert each item related to the current vendor*/
                foreach (var item in vendor.Items) {
                    var itemEntry = SaleMapper.DehydrateItem(item, orderSubId);
                    _dataProvider.Database.Connection.Insert(itemEntry, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
                }
            }
        }
        
        public void Update(EzFundSale model)
        {
           

            const string sql = @"UPDATE ORDR_INVOIC_TBL SET LAST_STAT_CDE = @saleStatus WHERE ORDR_ID = @orderId;";
            var rows = _dataProvider.Database.Connection.Execute(sql, new { saleStatus = model.Status.ToString(), orderId = model.OrderId }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            //return (rows > 0);
            
        }

        

        public int Save(Sale model)
        {
            throw new NotImplementedException();
        }

        public EzFundSale GetEzFundSaleByOrderId(int orderId)
        {
            const string sql = @"SELECT TOP 1 * FROM ORDR_INVOIC_TBL (NOLOCK) WHERE ORDR_ID = @orderId
                SELECT oit.* FROM ORDR_ITEM_TBL oit (NOLOCK) join ORDR_VEND_TBL ovt(NOLOCK) on ovt.ORDR_SUB_ID = oit.ORDR_SUB_ID WHERE ovt.ORDR_ID = @orderId;";
				var multi = _dataProvider.Database.Connection.QueryMultiple(sql, new { orderId },
            _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            var invoiceTable = multi.Read<ordr_invoic_tbl>().Single();
            var itemOrdered = multi.Read<ordr_item_tbl>().ToList();
            if (invoiceTable == null) return null;
            var result = SaleMapper.Hydrate(invoiceTable);
            result.SubProducts = itemOrdered.Select(p => SaleMapper.HydrateItem(p, invoiceTable.ORDR_ID)).ToList();
           

            return result;
        }

		public bool UpdateFundPaymentReferenceByOrderId(int orderId, String reference)
		{
			const string sql = @"UPDATE ORDR_INVOIC_TBL SET PMT_REF_NBR = @refNum WHERE ORDR_ID = @orderId;";
			var rows = _dataProvider.Database.Connection.Execute(sql, new { refNum = reference, orderId= orderId }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
			return (rows>0);
		}

        public IList<Sale> GetSaleShippingDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Sale model)
        {
            throw new NotImplementedException();
        }
    }
}
