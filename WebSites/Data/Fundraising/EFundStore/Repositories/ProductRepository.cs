using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Dapper;
using GA.BDC.Data.Fundraising.EFundStore.Mappers;
using GA.BDC.Data.Fundraising.EFundStore.Tables;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundStore.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataProvider _dataProvider;
        private readonly EFundraisingProd.Tables.DataProvider _efundraisingProdDataProvider;

        public ProductRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            _efundraisingProdDataProvider = new EFundraisingProd.Tables.DataProvider();
            _efundraisingProdDataProvider.Database.BeginTransaction();
        }

        public IList<Product> GetAllFeatured()
        {
            var rootId = int.Parse(ConfigurationManager.AppSettings["US.Root.PackageId"]);
            var ids =
               _dataProvider.Database.Connection.Query<int>(
                  "select p.product_id from product p (NOLOCK) join product_package pp (NOLOCK) on p.product_id = pp.product_id join package pa (NOLOCK) on pp.package_id = pa.package_id join package paa (NOLOCK) on pa.parent_package_id = paa.package_id where p.is_featured = 1 and p.enabled =1 and paa.parent_package_id = @rootId",
                  new { rootId }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
            return ids.Select(id => GetById(id)).ToList();
        }

        public IList<Product> GetByCategory(int categoryId)
        {
            var ids =
               _dataProvider.Database.Connection.Query<int>("SELECT P.product_id FROM product_package PP (NOLOCK) JOIN product P (NOLOCK) ON PP.product_id = P.product_id WHERE P.enabled = 1 AND PP.package_id = @categoryId", new { categoryId },
                  _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
            return ids.Select(id => GetById(id)).ToList();
        }

        public Product GetById(int id, bool showUnapprovedReviews)
        {
            try
            {
                var sql = @"SELECT TOP 1 P.product_id, P.parent_product_id, P.scratch_book_id, P.name, P.raising_potential, P.product_code, P.enabled, P.create_date, P.is_inner, P.is_featured FROM product P (NOLOCK) WHERE P.product_id = @id
SELECT TOP 1 PP.package_id, PP.product_id, PP.display_order, PP.display, PP.create_date FROM product_package PP (NOLOCK) WHERE PP.product_id = @id;
SELECT TOP 1 product_id, culture_code, name, short_desc, long_desc, page_name, image_name, template_id, extra_desc, page_title, image_alt_text, display_order, enabled, configuration, create_date, url, description, flavors, packaging, extra_information, base_price, is_store_purchasable, retail_price, minimum_quantity, meta_description, meta_keywords, display_order_featured, meta_title, canonical_url FROM product_desc PD (NOLOCK) WHERE PD.product_id = @id AND PD.culture_code = 'en-US';

SELECT TOP 1 PPI.product_id, PPI.country_code, PPI.effective_date, PPI.product_class_id, PPI.unit_price FROM product_price_info PPI (NOLOCK) WHERE PPI.product_id = @id;
SELECT PP.id, PP.product_id, PP.price, PP.min_qty, PP.max_qty FROM product_profit PP WHERE PP.product_id = @id;
SELECT TOP 1 C.id, C.country_code AS country_code, C.currency_code AS currency_code from currency C (NOLOCK) JOIN product_price_info PPI (NOLOCK) ON PPI.country_code = C.country_code WHERE PPI.product_id = @id;
SELECT PIS.id, PIS.product_id, PIS.url, PIS.alternative_text, PIS.created, PIS.enabled, PIS.sort, PIS.is_cover FROM product_image PIS (NOLOCK) WHERE PIS.product_id = @id AND PIS.enabled = 1;
SELECT * FROM advertiser_products (NOLOCK) WHERE product_id = @id AND enabled = 1;
SELECT * FROM review (NOLOCK) WHERE ProductId = @id AND IsApproved = @showUnapprovedReviews";

                var multi = _dataProvider.Database.Connection.QueryMultiple(sql, new { id, showUnapprovedReviews = !showUnapprovedReviews },
                   _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
                var dataBaseProduct = multi.Read<product>().Single();
                var parentPackage = multi.Read<product_package>().First();
                var productDescriptions = multi.Read<product_desc>().ToList();
                //var productDescription = multi.Read<product_desc>().First();
                var productDescription = productDescriptions.Any() ? productDescriptions.First() : null;
                var productPrices = multi.Read<product_price_info>().ToList();
                var profits = multi.Read<product_profit>().ToList();
                var productPrice = productPrices.Any() ? productPrices.First() : new product_price_info { country_code = "US", unit_price = 1 };
                var currencies = multi.Read<currency>().ToList();
                var currency = currencies.Any() ? currencies.First() : new currency { currency_code = "USD", country_code = "US" };

                var result = ProductMapper.Hydrate(dataBaseProduct, productDescription, parentPackage.package_id, productPrice, profits, currency);
                result.CalculatedPrice = profits.Any() ? profits.Max(p => p.price) : result.Price;
                var productImages = multi.Read<product_image>().ToList();

                var adversitingOverrides = multi.Read<advertiser_products>().ToList();
                if (adversitingOverrides.Any())
                {
                    result.AdvertisingOverride = AdvertisingMapper.Hydrate(adversitingOverrides.First());
                }



                var reviews = multi.Read<review>().ToList();
                result.Reviews = reviews.Select(p => ReviewMapper.Hydrate(p)).ToList();
                foreach (var r in result.Reviews)
                {
                    r.ProductName = result.Name;
                }
                result.Image = ProductMapper.HydrateImages(productImages.Take(1).ToList()).First();
                result.Image.IsCover = true;
                var requireTaxes = false;
                result.Taxes = GetTaxes(result.ScratchBookId, out requireTaxes);
                result.RequireTaxes = requireTaxes;
                return result;
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Exception while trying to read Product ID {0} from the Database.", id), exception);
            }

        }

        public IList<Product> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public int Save(Product model)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Product model)
        {
            const string sqlCreateReview = "INSERT INTO review (Name, Comments, Rating, ProductId, SaleId, IsApproved, Created, Email) VALUES (@name, @comments, @rating, @productId, @saleId, @isApproved, @created, @email);";
            #region Reviews

            foreach (var newReview in model.Reviews.Where(p => p.Id == 0))
            {
                const string preExistentSales = "SELECT S.sales_id FROM sale S (NOLOCK) JOIN lead L (NOLOCK) ON S.lead_id = L.lead_id AND L.email = @email";
                newReview.SaleId = _efundraisingProdDataProvider.Database.Connection.QueryFirstOrDefault<int>(preExistentSales, new { email = newReview.Email }, _efundraisingProdDataProvider.Database.CurrentTransaction.UnderlyingTransaction);
                _dataProvider.Database.Connection.Execute(sqlCreateReview, new { name = newReview.Name, comments = newReview.Comments, rating = newReview.Rating, productId = model.Id, saleId = newReview.SaleId == 0 ? (int?)null : newReview.SaleId, isApproved = false, created = DateTime.Now, email = newReview.Email },
                   _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            }
            #endregion

        }

        public void Delete(Product model)
        {
            throw new System.NotImplementedException();
        }

        public IList<Product> GetAllByCountry(int country)
        {
            var rootId = country == 1 ? int.Parse(ConfigurationManager.AppSettings["Canada.Root.PackageId"]) : int.Parse(ConfigurationManager.AppSettings["US.Root.PackageId"]);
            var ids =
               _dataProvider.Database.Connection.Query<int>(
                  "select p.product_id from product p (NOLOCK) join product_package pp (NOLOCK) on p.product_id = pp.product_id join package pa (NOLOCK) on pp.package_id = pa.package_id join package paa (NOLOCK) on pa.parent_package_id = paa.package_id where p.is_featured = 1 and p.enabled =1 and paa.parent_package_id = @rootId", new { rootId }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
            return ids.Select(id => GetById(id)).ToList();
        }

        public IList<Product> GetFiltered(double price, double profit, int[] packageIds)
        {
            var result = new List<Product>();
            var productsIds = (from pr in _dataProvider.products
                               from prPrice in _dataProvider.product_price_info
                               from prpa in _dataProvider.product_package
                               from pa in _dataProvider.packages
                               where pr.enabled
                                     && pr.product_id == prPrice.product_id
                                     && (price == 0 || (double)prPrice.unit_price <= price)
                                     && prpa.product_id == pr.product_id
                                     && pa.package_id == prpa.package_id
                                     && packageIds.Contains((int)pa.parent_package_id)
                               select pr.product_id).ToList();
            foreach (var id in productsIds)
            {
                var product = GetById(id);
                result.Add(product);
            }
            return profit == 0 ? result : result.Where(p => p.ProfitPercentage == 0 || profit >= (p.ProfitPercentage * 100.0)).ToList();
        }

        public Product GetByScratchbookId(int id)
        {
            try
            {
                var usRootId = int.Parse(ConfigurationManager.AppSettings["US.Root.PackageId"]);
                var canadaRootId = int.Parse(ConfigurationManager.AppSettings["Canada.Root.PackageId"]);
                var productId =
                   _dataProvider.Database.Connection.Query<int>(
                      "SELECT TOP 1 P.product_id FROM product P (NOLOCK) JOIN product_package PP (NOLOCK) ON P.product_id = PP.product_id JOIN package PA (NOLOCK) ON PP.package_id = PA.package_id JOIN package PAA (NOLOCK) ON PA.parent_package_id = PAA.package_id WHERE (PAA.parent_package_id = @usRootId OR PAA.parent_package_id = @canadaRootId) AND P.scratch_book_id = @id",
                      new { usRootId, canadaRootId, id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).First();
                return GetById(productId);
            }
            catch (Exception exception)
            {
                throw new ArgumentException(string.Format("Product with Scratchbook Id {0} doesn't exist", id), exception);
            }

        }

        public Product GetByScratchbookIdSalesScreen(int id)
        {
            var productId = int.MinValue;
            var ssrootId = int.Parse(ConfigurationManager.AppSettings["SalesScreen.Root.PackageId"]);

            try
            {
                try
                {
                    productId =
                      _dataProvider.Database.Connection.Query<int>(
                         "SELECT TOP 1 P.product_id FROM product P (NOLOCK) JOIN product_package PP (NOLOCK) ON P.product_id = PP.product_id JOIN package PA (NOLOCK) ON PP.package_id = PA.package_id JOIN package PAA (NOLOCK) ON PA.parent_package_id = PAA.package_id WHERE PAA.parent_package_id = @ssrootId AND P.scratch_book_id = @id",
                         new { ssrootId, id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).First();
                }
                catch
                {
                    productId =
                     _dataProvider.Database.Connection.Query<int>(
                        "SELECT TOP 1 P.product_id FROM product P (NOLOCK) JOIN product_package PP (NOLOCK) ON P.product_id = PP.product_id JOIN package PA (NOLOCK) ON PP.package_id = PA.package_id WHERE PA.parent_package_id = @ssrootId AND P.scratch_book_id = @id",
                        new { ssrootId, id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).First();
                }
                return GetById(productId);


            }

            catch (Exception exception)
            {
                throw new ArgumentException(string.Format("Product with Scratchbook Id {0} doesn't exist", id), exception);
            }

        }


        public KeyValuePair<int, string> GetProductClass(int scratchBookId)
        {
            var productClass =
                  _efundraisingProdDataProvider.Database.Connection.Query<EFundraisingProd.Tables.product_class>(
                     "SELECT TOP 1 PC.product_class_id, PC.division_id, PC.accounting_class_id, PC.description, ISNULL(PC.product_code,'') AS product_code, ISNULL(PC.display_name,'') AS display_name, PC.is_displayable, PC.minimum_order_qty, ISNULL(PC.tax_exempt,0) AS tax_exempt FROM scratch_book SC (NOLOCK) JOIN product_class PC (NOLOCK) ON SC.product_class_id = PC.product_class_id WHERE SC.scratch_book_id = @scratchBookId;",
                     new { scratchBookId },
                     _efundraisingProdDataProvider.Database.CurrentTransaction.UnderlyingTransaction).First();

            return new KeyValuePair<int, string>(productClass.product_class_id, productClass.display_name);
        }
        /// <summary>
        /// Returns the Flag that indicates if the Product Class requires taxes
        /// </summary>
        /// <param name="scratchBookId"></param>
        /// <returns></returns>
        private IList<StateTax> GetTaxes(int scratchBookId, out bool requireTaxes)
        {
            var result = new List<StateTax>();
            requireTaxes = false;
            if (scratchBookId > 0)
            {
                var productClasses =
                   _efundraisingProdDataProvider.Database.Connection.Query<EFundraisingProd.Tables.product_class>(
                      "SELECT PC.product_class_id, PC.division_id, PC.accounting_class_id, PC.description, ISNULL(PC.product_code,'') AS product_code, ISNULL(PC.display_name,'') AS display_name, PC.is_displayable, PC.minimum_order_qty, ISNULL(PC.tax_exempt,0) AS tax_exempt FROM scratch_book SC (NOLOCK) JOIN product_class PC (NOLOCK) ON SC.product_class_id = PC.product_class_id WHERE SC.scratch_book_id = @scratchBookId;",
                      new { scratchBookId },
                      _efundraisingProdDataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
                requireTaxes = productClasses.Any() && (!productClasses.First().tax_exempt ?? false);
                if (requireTaxes)
                {
                    result.AddRange(_efundraisingProdDataProvider.State_Tax.ToList().Select(stateTax => new StateTax { EffectiveDate = stateTax.Effective_Date, Rate = (double)stateTax.Tax_Rate, TaxCode = stateTax.Tax_Code, StateCode = stateTax.State_Code }));
                }
            }
            return result;
        }

        public Product GetById(int id)
        {
            return GetById(id, false);
        }

        public Product GetByCode(string code)
        {
            throw new NotImplementedException();
        }

        public Product GetByCleanUrl(string rootCategoryUrl, string categoryUrl, string url)
        {
            throw new NotImplementedException();
        }

        public IList<Product> GetByCleanUrl(string rootCategoryUrl, string categoryUrl)
        {
            throw new NotImplementedException();
        }

        public IList<Product> GetRelatedProducts(string code, int maxResults, bool isRandom, bool canBePurchased)
        {
            throw new NotImplementedException();
        }

        public Product GetProgramByCleanUrl(string programCode, string url)
        {
            throw new NotImplementedException();
        }

        public IList<Product> GetRelatedProducts(int id, int maxResults, bool isRandom)
        {
            throw new NotImplementedException();
        }

        public Product GetByUrl(string url)
        {
            throw new NotImplementedException();
        }

        public Product GetSimpleById(int id)
        {
            throw new NotImplementedException();
        }

        public SubProduct GetSubProductByCode(string code)
        {
            throw new NotImplementedException();
        }
    }
}
