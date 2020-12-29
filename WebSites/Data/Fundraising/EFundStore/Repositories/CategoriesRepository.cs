
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
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly DataProvider _dataProvider;

        public CategoriesRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public Category GetById(int id)
        {
           var package = _dataProvider.Database.Connection.Query<package>("select TOP 1 package_id, parent_package_id, name, profit_percentage, [enabled], create_date, [order], shipping_fee_id, url, meta_description, meta_keywords, [description], link_group_key, description2, meta_title from package (NOLOCK) where package_id = @id", new { id },
              _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).Single();
            IList<shipping_fee_detail> shippingFeeDetails;
            if (package.shipping_fee_id != null)
            {
               shippingFeeDetails = _dataProvider.Database.Connection.Query<shipping_fee_detail>("SELECT SFD.id, SFD.shipping_fee_id, SFD.minimum_quantity, SFD.maximum_quantity, SFD.fee FROM shipping_fee SF (NOLOCK) JOIN shipping_fee_detail SFD (NOLOCK) ON SF.id = SFD.shipping_fee_id WHERE SF.id = @shipping_fee_id",
                  new { package.shipping_fee_id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
            }
            else
            {
               shippingFeeDetails = _dataProvider.Database.Connection.Query<shipping_fee_detail>("SELECT SFD.id, SFD.shipping_fee_id, SFD.minimum_quantity, SFD.maximum_quantity, SFD.fee FROM shipping_fee SF (NOLOCK) JOIN shipping_fee_detail SFD (NOLOCK) ON SF.id = SFD.shipping_fee_id WHERE SF.is_default = 1",
                  new { package.shipping_fee_id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
            }

            var category = CategoryMapper.Hydrate(package, shippingFeeDetails);
            return category;
        }

       public IList<Category> GetAll()
       {
          throw new System.NotImplementedException();
       }

       public int Save(Category model)
       {
          throw new System.NotImplementedException();
       }

       public void Update(Category model)
       {
          throw new System.NotImplementedException();
       }

       public void Delete(Category model)
       {
          throw new System.NotImplementedException();
       }

       public IList<Category> GetExcludedCategories(int partnerId)
        {
          var packagesIds =
                _dataProvider.package_exclusions.Where(p => p.partner_id == partnerId)
                    .ToList()
                    .Select(p => p.package_id);
          return packagesIds.Select(GetById).ToList();
        }

        public IList<Category> GetByPartner(int country, int partnerId)
        {
            var parentPackageId = country == 1 ? int.Parse(ConfigurationManager.AppSettings["Canada.Root.PackageId"]) : int.Parse(ConfigurationManager.AppSettings["US.Root.PackageId"]);
            var exclusions = _dataProvider.package_exclusions.Where(p => p.partner_id == partnerId).ToList();
            var packagesIds = _dataProvider.packages.Where(p => p.parent_package_id == parentPackageId && p.enabled).Select(p => p.package_id).ToList();
            var cleanPackageIds = packagesIds.Where(id => exclusions.All(p => p.package_id != id)).ToList();
           return cleanPackageIds.Select(GetById).ToList();
        }

        public IList<Category> GetByParent(int parentId)
        {
           var ids =
              _dataProvider.Database.Connection.Query<int>("SELECT package_id FROM package (NOLOCK) WHERE enabled = 1 AND parent_package_id = @parentId", new { parentId },
                 _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
           return ids.Select(GetById).ToList();
        }

        

        public IList<Category> GetCategoryByUrl(int country, string url)
        {
            var parentPackageId = country == 1 ? int.Parse(ConfigurationManager.AppSettings["Canada.Root.PackageId"]) : int.Parse(ConfigurationManager.AppSettings["US.Root.PackageId"]);
            var ids =
               _dataProvider.Database.Connection.Query<int>("SELECT package_id FROM package (NOLOCK) WHERE enabled = 1 AND parent_package_id = @parentPackageId AND url=@url", new { parentPackageId, url },
                  _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
           return ids.Select(GetById).ToList();
        }

        public IList<Category> GetByParentCode(string code)
        {
            throw new NotImplementedException();
        }
        
        public IList<Category> GetByCleanUrl(string parentUrl)
        {
            throw new NotImplementedException();
        }

        public Category GetByCleanUrl(string url, string parentUrl)
        {
            throw new NotImplementedException();
        }

        public IList<Category> GetAllSubCategories()
        {
            throw new NotImplementedException();
        }

        public IList<Category> GetAllCategories()
        {
            throw new NotImplementedException();
        }

        public IList<Category> GetCategoryByUrl(string url)
        {
            throw new NotImplementedException();
        }

        public Category GetByProductId(int productId)
        {
            throw new NotImplementedException();
        }

        public Category GetParentById(int parentId)
        {
            throw new NotImplementedException();
        }

        public IList<Category> GetAllCategoriesSiteMap()
        {
            throw new NotImplementedException();
        }
    }
}
