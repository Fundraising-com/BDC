using GA.BDC.Shared.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using GA.BDC.Shared.Entities;
using GA.BDC.Data.EzFund.EZMain.Tables;
using GA.BDC.Data.EzFund.EZMain.Mappers;
using Dapper;

namespace GA.BDC.Data.EzFund.EZMain.Repositories
{
    public class ProductsRepository : IProductRepository
    {
        private readonly DataProvider _dataProvider;

        public ProductsRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }
        public void Delete(Product model)
        {
            throw new NotImplementedException();
        }

        public IList<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public IList<Product> GetAllByCountry(int country)
        {
            throw new NotImplementedException();
        }

        public IList<Product> GetAllFeatured()
        {
            throw new NotImplementedException();
        }

        public IList<Product> GetByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public IList<Product> GetByCleanUrl(string rootCategoryUrl, string categoryUrl)
        {
            throw new NotImplementedException();
        }

        public Product GetByCleanUrl(string rootCategoryUrl, string categoryUrl, string url)
        {
            // We try to get the program first
            const string sqlProgram = "SELECT * FROM SITE_PGM_TBL (NOLOCK) WHERE PGM_GRP_CDE = @rootCategoryUrl and CLEAN_URL = @url";
            var program = _dataProvider.Database.Connection.QuerySingleOrDefault<site_pgm_tbl>(sqlProgram,
                 new { rootCategoryUrl, url }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            if (program != null) { 
                return ProgramMapper.Hydrate(program);
            }
            const string sqlProduct = "SELECT ORDR_ITEM_NBR FROM SITE_PDCT_TBL (NOLOCK) WHERE PDCT_CTGY_CDE = @categoryUrl AND SRC_GRP = @rootCategoryUrl AND CLEAN_URL = @url";
            var id = _dataProvider.Database.Connection.QuerySingleOrDefault<int>(sqlProduct,
                 new { categoryUrl, rootCategoryUrl, url }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return GetById(id);
        }


       


        //Fetches the info from a program code
        public Product GetByCode(string code)
        {
            const string sql = @"SELECT TOP 1 * FROM SITE_PGM_TBL AS PGM (NOLOCK) WHERE PGM_CDE = @code";
            var row = _dataProvider.Database.Connection.QueryFirst<site_pgm_tbl>(sql, new { code }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return ProgramMapper.Hydrate(row);
        }

        public Product GetById(int id)
        {
            const string sql = @"SELECT TOP 1 * FROM product (NOLOCK) WHERE product_id = @id";
            var product = _dataProvider.Database.Connection.QuerySingleOrDefault<product>(sql, new { id },
              _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            const string sqlSubProducts = @"SELECT * FROM ITEM_LKUP_TBL (NOLOCK) WHERE PARENT_ID = @id AND NTRN_END_DTE >= CAST(CURRENT_TIMESTAMP AS DATE) AND NTRN_STRT_DTE <= CAST(CURRENT_TIMESTAMP AS DATE) ORDER BY ITEM_SEQ_NBR ASC";
            var subProducts = _dataProvider.Database.Connection.Query<item_lkup_tbl>(sqlSubProducts, new { id = product.product_id },
              _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();

            IList<shipping_fee_detail> shippingFeeDetails;
            if (product.shipping_fee_id != null)
            {
                shippingFeeDetails = _dataProvider.Database.Connection.Query<shipping_fee_detail>("SELECT SFD.id, SFD.shipping_fee_id, SFD.minimum_quantity, SFD.maximum_quantity, SFD.fee FROM shipping_fee SF (NOLOCK) JOIN shipping_fee_detail SFD (NOLOCK) ON SF.id = SFD.shipping_fee_id WHERE SF.id = @shipping_fee_id",
                   new { product.shipping_fee_id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
            }
            else
            {
                shippingFeeDetails = _dataProvider.Database.Connection.Query<shipping_fee_detail>("SELECT SFD.id, SFD.shipping_fee_id, SFD.minimum_quantity, SFD.maximum_quantity, SFD.fee FROM shipping_fee SF (NOLOCK) JOIN shipping_fee_detail SFD (NOLOCK) ON SF.id = SFD.shipping_fee_id WHERE SF.is_default = 1",
                   new { product.shipping_fee_id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
            }


            var result = ProductMapper.Hydrate(product, subProducts, shippingFeeDetails);
			   //Adding PriceRanges / Warehouses / Vendors to each SubProduct
				foreach (var subProduct in result.SubProducts) {
				const string sqlSubProductPriceRange = @"SELECT * FROM price_range (NOLOCK) WHERE item_code = @itemCode ORDER BY minimum_qty ASC";
				var priceRange = _dataProvider.Database.Connection.Query<price_range>(sqlSubProductPriceRange, new { itemCode = subProduct.ItemCode },
					_dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
				subProduct.Profit = PriceRangeMapper.Hydrate(priceRange);

				const string sqlSubProductWarehouse = @"SELECT * FROM ITEM_WHSE_MAP_TBL (NOLOCK) WHERE ITEM_CDE = @itemCode ORDER BY WHSE_ITEM_SEQ_NBR ASC, WHSE_CDE ASC";
            var warehouse = _dataProvider.Database.Connection.Query<item_whse_map_tbl>(sqlSubProductWarehouse, new { itemCode = subProduct.ItemCode},
					_dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
            subProduct.Warehouse = ItemWarehouseMapper.Hydrate(warehouse);
            const string sqlSubProductVendor = @"SELECT * FROM ITEM_VEND_MAP_TBL (NOLOCK) WHERE ITEM_CDE = @itemCode ORDER BY ITEM_SEQ_NBR ASC, VEND_CDE ASC";
            var vendor = _dataProvider.Database.Connection.Query<item_vend_map_tbl>(sqlSubProductVendor, new { itemCode = subProduct.ItemCode },
               _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
            subProduct.Vendor = ItemVendorMapper.Hydrate(vendor);

            }
            return result;
        }

        public Product GetSimpleById(int id) {
            const string sql = @"SELECT TOP 1 * FROM product (NOLOCK) WHERE product_id = @id";
            var product = _dataProvider.Database.Connection.QuerySingleOrDefault<product>(sql, new { id },
              _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return ProductMapper.Hydrate(product, null, null);
        }

        public SubProduct GetSubProductByCode(string code)
        {
            const string sql = @"SELECT TOP 1 * FROM ITEM_LKUP_TBL (NOLOCK) WHERE ITEM_CDE = @code";
            var subProduct = _dataProvider.Database.Connection.QuerySingleOrDefault<item_lkup_tbl>(sql, new { code },
              _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return ProductMapper.HydrateSubProduct(subProduct);
        }


        public Product GetById(int id, bool showUnapprovedReviews)
        {
            throw new NotImplementedException();
        }

        public Product GetByScratchbookId(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetByUrl(string url)
        {
            const string sqlProduct = "SELECT product_id FROM product (NOLOCK) WHERE url = @url";
            var id = _dataProvider.Database.Connection.QuerySingleOrDefault<int>(sqlProduct,
                 new { url }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return GetById(id);
        }

        public IList<Product> GetFiltered(double price, double profit, int[] packageIds)
        {
            throw new NotImplementedException();
        }

        public KeyValuePair<int, string> GetProductClass(int scratchBookId)
        {
            throw new NotImplementedException();
        }

        public IList<Product> GetRelatedProducts(int id, int maxResults, bool isRandom)
        {
            const string sqlGet = "SELECT pc.product_id FROM product_category pc join product p (NOLOCK) ON p.product_id = pc.product_id WHERE pc.category_id = @id AND p.enabled = 1 ORDER BY pc.display_order";
            const string sqlGetRandom = "SELECT pc.product_id FROM product_category pc join product p (NOLOCK) ON p.product_id = pc.product_id WHERE category_id = @id AND p.enabled = 1 ORDER BY NEWID(); ";
            var ids = _dataProvider.Database.Connection.Query<int>(isRandom ? sqlGetRandom : sqlGet,
                 new { id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            if (maxResults > 0)
            {
                ids = ids.Take(maxResults);
            }
            return ids.Select(GetById).ToList();
        }

        public IList<Product> GetRelatedProducts(string code, int maxResults, bool isRandom, bool canBePurchased)
        {
            if (canBePurchased)
            {
                const string sqlGet = "SELECT ORDR_ITEM_NBR FROM SITE_PDCT_TBL (NOLOCK) WHERE PDCT_CTGY_CDE = @code AND XTRN_END_DTE >= CAST(CURRENT_TIMESTAMP AS DATE) AND XTRN_STRT_DTE <= CAST(CURRENT_TIMESTAMP AS DATE);";
                const string sqlGetRandom = "SELECT ORDR_ITEM_NBR FROM SITE_PDCT_TBL (NOLOCK) WHERE PDCT_CTGY_CDE = @code AND XTRN_END_DTE >= CAST(CURRENT_TIMESTAMP AS DATE) AND XTRN_STRT_DTE <= CAST(CURRENT_TIMESTAMP AS DATE) ORDER BY NEWID();";
                var ids = _dataProvider.Database.Connection.Query<int>(isRandom ? sqlGetRandom : sqlGet,
                     new { code }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
                if (maxResults > 0)
                {
                    ids = ids.Take(maxResults);
                }
                return ids.Select(GetById).ToList();
            }
            else
            {
                const string sqlGet = "SELECT PGM_CDE FROM SITE_PGM_TBL (NOLOCK) WHERE PGM_GRP_CDE = @code AND XTRN_END_DTE >= CAST(CURRENT_TIMESTAMP AS DATE) AND XTRN_STRT_DTE <= CAST(CURRENT_TIMESTAMP AS DATE);";
                const string sqlGetRandom = "SELECT PGM_CDE FROM SITE_PGM_TBL (NOLOCK) WHERE PGM_GRP_CDE = @code AND XTRN_END_DTE >= CAST(CURRENT_TIMESTAMP AS DATE) AND XTRN_STRT_DTE <= CAST(CURRENT_TIMESTAMP AS DATE) ORDER BY NEWID()"; 
                var ids = _dataProvider.Database.Connection.Query<string>(isRandom ? sqlGetRandom : sqlGet,
                     new { code }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
                if (maxResults > 0)
                {
                    ids = ids.Take(maxResults);
                }
                return ids.Select(GetByCode).ToList();
            }
        }
        public int Save(Product model)
        {
            throw new NotImplementedException();
        }

        public void Update(Product model)
        {
            throw new NotImplementedException();
        }

        public Product GetByScratchbookIdSalesScreen(int id)
        {
            throw new NotImplementedException();
        }
    }
}
