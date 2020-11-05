using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;
using System.Configuration;

namespace GA.BDC.WebApi.Fundraising.Core.Controllers
{
   public class SalesController : ApiController
   {
      [HttpOptions]
      public IHttpActionResult Options()
      {
         return Ok();
      }

      [HttpPost]
      public IHttpActionResult Post(Sale model)
      {
         var promotionCodeDiscount = 0.0;
         var singlePromotionCodeDiscount = 0.0;
         var isPromotionCodeUsed = false;
            var shippingCost = model.ShippingFee;
         using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
         {
            if (model.Id > 0)
            {
               return Ok(new { clientId = model.Client.Id });
            }
            if (model.PromotionCode != null)
            {
               if (!validatePromotionCode(efundraisingProdUnitOfWork, model))
               {
                  return BadRequest("The Promotion Code is not valid.");
               }
            }
            var clientRepository = efundraisingProdUnitOfWork.CreateRepository<IClientsRepository>();
            using (var efundstoreUnitOfWork = new UnitOfWork(Database.EFundStore))
            {
               var productRepository = efundstoreUnitOfWork.CreateRepository<IProductRepository>();
               var salesRepository = efundraisingProdUnitOfWork.CreateRepository<ISalesRepository>();
               var clientId = clientRepository.Save(model.Client);
               model.Client.Id = clientId;
               foreach (var item in model.Items)
               {
                  item.Product.ProductClass = productRepository.GetProductClass(item.Product.ScratchBookId);
               }
               var groupedSaleItems = (from p in model.Items
                                       group p by p.Product.ProductClass.Key
                                          into saleItems
                                          select new { ProductClassId = saleItems.Key, SaleItems = saleItems });
                    //foreach (var groupedSaleItem in groupedSaleItems)
                    // {
                    var groupedSaleItem = groupedSaleItems;
                    var clonedSale = new Sale
                  {
                     Id = model.Id,
                     InternalPaymentMethod = model.InternalPaymentMethod,
                     PaymentMethod = model.PaymentMethod,
                     UserId = model.UserId,
                     CreditCard = model.CreditCard,
                     Items = model.Items.ToList(),
                     Client = model.Client,
                     Status = SaleStatus.OnHold,
                     ARStatus = ARStatus.NotPaid,
                     ConsultantId = model.ConsultantId,
                     //ShippingFee = model.ShippingFee
                  };
                  //var shippingFees = 0.0;
                  var billingRegion = clonedSale.Client.Addresses[0].Region;
                  var appliedTaxes = new List<AppliedTax>();
                  foreach (var saleItem in clonedSale.Items)
                  {
                     //var shippingFee = saleItem.Product.Category.ShippingFees.FirstOrDefault(p => p.MinimumQuantity <= saleItem.Quantity && p.MaximumQuantity >= saleItem.Quantity);
                     if (model.PromotionCode != null && model.PromotionCode.ScopeType == PromotionCodeScopeType.Products && model.PromotionCode.Products.Any(p => p.ScratchBookId == saleItem.Product.ScratchBookId) && !isPromotionCodeUsed)
                     {
                        if (model.PromotionCode.DiscountType == PromotionCodeDiscountType.Amount)
                        {
                           promotionCodeDiscount = model.PromotionCode.AmountDiscount ?? 0;
                           singlePromotionCodeDiscount = model.PromotionCode.AmountDiscount ?? 0;
                           isPromotionCodeUsed = true;
                        }
                        if (model.PromotionCode.DiscountType == PromotionCodeDiscountType.Percentage)
                        {
                           promotionCodeDiscount = saleItem.Quantity * saleItem.Product.CalculatedPrice * (model.PromotionCode.PercentageDiscount ?? 0 / 100.0);
                           singlePromotionCodeDiscount = saleItem.Quantity * saleItem.Product.CalculatedPrice * (model.PromotionCode.PercentageDiscount ?? 0 / 100.0);
                           isPromotionCodeUsed = true;
                        }
                        if (model.PromotionCode.DiscountType == PromotionCodeDiscountType.FreeShipping)
                        {
                                promotionCodeDiscount += Convert.ToDouble(shippingCost);// shippingFee?.Fee ?? 0;
                           singlePromotionCodeDiscount = Convert.ToDouble(shippingCost); //shippingFee?.Fee ?? 0;
                            }
                        model.PromotionCode.DiscountedAmount = singlePromotionCodeDiscount;
                        clonedSale.PromotionCode = model.PromotionCode;
                        clonedSale.PromotionCodeId = model.PromotionCodeId;
                     }
                     
                     //if (shippingFee != null)
                     //{
                     //   shippingFees += shippingFee.Fee;
                     //}
                     saleItem.Product.CalculatedPrice = saleItem.Product.Profits.Any()
                         ? saleItem.Product.Profits.First(
                             profit => profit.Min <= saleItem.Quantity && profit.Max >= saleItem.Quantity).Price
                         : saleItem.Product.Price;
                     if (saleItem.Product.RequireTaxes)
                     {
                        foreach (var stateTax in saleItem.Product.Taxes.Where(p => p.StateCode == billingRegion.Code))
                        {
                           if (appliedTaxes.Any(p => p.TaxCode == stateTax.TaxCode))
                           {
                              var appliedTax = appliedTaxes.First(p => p.TaxCode == stateTax.TaxCode);
                              appliedTax.Amount += ((saleItem.Product.CalculatedPrice * saleItem.Quantity) + shippingCost) * (stateTax.Rate / 100.0);
                           }
                           else
                           {
                              appliedTaxes.Add(new AppliedTax { TaxCode = stateTax.TaxCode, SaleId = clonedSale.Id, Amount = ((saleItem.Product.CalculatedPrice * saleItem.Quantity) + shippingCost) * (stateTax.Rate / 100.0) });
                           }
                        }
                     }
                  }
                  clonedSale.Taxes = appliedTaxes;
                  if (model.PromotionCode != null && model.PromotionCode.ScopeType == PromotionCodeScopeType.ShoppingCart)
                  {
                     if (model.PromotionCode.DiscountType == PromotionCodeDiscountType.Amount)
                     {
                        var subTotal = model.Items.Sum(p => p.Quantity * p.Product.CalculatedPrice);
                        var clonedSaleSubTotal = clonedSale.Items.Sum(p => p.Quantity * p.Product.CalculatedPrice);
                        promotionCodeDiscount = clonedSaleSubTotal / subTotal * model.PromotionCode.AmountDiscount ?? 0;
                     }
                     if (model.PromotionCode.DiscountType == PromotionCodeDiscountType.Percentage)
                     {
                        promotionCodeDiscount = clonedSale.Items.Sum(p => p.Quantity * p.Product.CalculatedPrice) * (model.PromotionCode.PercentageDiscount ?? 0 / 100.0);
                     }
                     model.PromotionCode.DiscountedAmount = promotionCodeDiscount;
                     clonedSale.PromotionCode = model.PromotionCode;
                     clonedSale.PromotionCodeId = model.PromotionCodeId;
                  }
                  clonedSale.TotalAmount = clonedSale.Items.Sum(p => p.Quantity * p.Product.CalculatedPrice) + shippingCost + clonedSale.Taxes.Sum(p => p.Amount) - promotionCodeDiscount;
                  clonedSale.ShippingFee = shippingCost;
                  salesRepository.Save(clonedSale);

                   // }
                   
                    efundstoreUnitOfWork.Commit();
               efundraisingProdUnitOfWork.Commit();
               return Ok(new { clientId });
            }
         }
      }

      private bool validatePromotionCode(IUnitOfWork efundraisingProdUnitOfWork, Sale model)
      {
         if (!model.PromotionCode.IsEnabled)
         {
            return false;
         }
         if (model.PromotionCode.Country.Code != model.Items[0].Product.CountryCode)
         {
            return false;
         }
         if (model.PromotionCode.LimitType == PromotionCodeLimitType.Date && DateTime.Now > model.PromotionCode.DateLimit)
         {
            return false;
         }
         if (model.PromotionCode.LimitType == PromotionCodeLimitType.Quantity)
         {
            var salesRepository = efundraisingProdUnitOfWork.CreateRepository<ISalesRepository>();
            var sales = salesRepository.GetByPromotionCodeUsed(model.PromotionCode.Id);
            if (sales.Count >= model.PromotionCode.QuantityLimit)
            {
               return false;
            }
         }
         if (model.PromotionCode.ScopeType == PromotionCodeScopeType.Products && model.Items.Select(p => p.Product).All(a => model.PromotionCode.Products.All(p => p.ScratchBookId != a.ScratchBookId)))
         {
            return false;
         }

         if (model.PromotionCode.MinimumRequirementType == PromotionCodeMinimumRequirementType.Quantity)
         {
            if (model.PromotionCode.ScopeType == PromotionCodeScopeType.Products)
            {
               if (model.Items.Any(saleItem => model.PromotionCode.Products.Any(p => p.ScratchBookId == saleItem.ScratchBookId) && saleItem.Quantity < model.PromotionCode.MinimumQuantity))
               {
                  return false;
               }
            }
            else if (model.PromotionCode.ScopeType == PromotionCodeScopeType.ShoppingCart)
            {
               if (model.Items.Sum(p => p.Quantity) < model.PromotionCode.MinimumQuantity)
               {
                  return false;
               }
            }

         }

         if (model.PromotionCode.MinimumRequirementType == PromotionCodeMinimumRequirementType.Amount)
         {
            if (model.PromotionCode.ScopeType == PromotionCodeScopeType.Products)
            {
               if (model.Items.Any(saleItem => model.PromotionCode.Products.Any(p => p.ScratchBookId == saleItem.ScratchBookId) && (saleItem.Quantity * saleItem.Product.CalculatedPrice) < model.PromotionCode.MinimumAmount))
               {
                  return false;
               }
            }
            else if (model.PromotionCode.ScopeType == PromotionCodeScopeType.ShoppingCart)
            {
               if (model.PromotionCode.MinimumAmount > model.Items.Sum(p => p.Quantity * p.Product.CalculatedPrice))
               {
                  return false;
               }
            }
         }

         return true;
      }
      [HttpGet]
      public IHttpActionResult Get(int id)
      {
         using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
         {

            var clientRepository = efundraisingProdUnitOfWork.CreateRepository<IClientsRepository>();
            using (var efundstoreUnitOfWork = new UnitOfWork(Database.EFundStore))
            {
               var productRepository = efundstoreUnitOfWork.CreateRepository<IProductRepository>();
               var categoriesRepository = efundstoreUnitOfWork.CreateRepository<ICategoriesRepository>();
               var salesRepository = efundraisingProdUnitOfWork.CreateRepository<ISalesRepository>();

               var sale = salesRepository.GetById(id);
               sale.Client = clientRepository.GetById(sale.ClientId);
               foreach (var item in sale.Items)
               {
                  item.Product = productRepository.GetByScratchbookId(item.ScratchBookId);
                  item.Product.Category = categoriesRepository.GetById(item.Product.CategoryId);
                  item.Product.Category.Parent = categoriesRepository.GetById(item.Product.Category.ParentId);
               }
               return Ok(sale);
            }
         }
      }

      [HttpGet]
      public IHttpActionResult GetByClientId(int clientId)
      {
         using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
         {

            var clientRepository = efundraisingProdUnitOfWork.CreateRepository<IClientsRepository>();
            using (var efundstoreUnitOfWork = new UnitOfWork(Database.EFundStore))
            {
               var productRepository = efundstoreUnitOfWork.CreateRepository<IProductRepository>();
               var categoriesRepository = efundstoreUnitOfWork.CreateRepository<ICategoriesRepository>();
               var salesRepository = efundraisingProdUnitOfWork.CreateRepository<ISalesRepository>();
               var sales = salesRepository.GetByClientId(clientId);
               foreach (var sale in sales)
               {
                  sale.Client = clientRepository.GetById(clientId);

                  foreach (var item in sale.Items)
                  {
                     item.Product = productRepository.GetByScratchbookId(item.ScratchBookId);
                     item.Product.Category = categoriesRepository.GetById(item.Product.CategoryId);
                     item.Product.Category.Parent = categoriesRepository.GetById(item.Product.Category.ParentId);
                  }
               }
               return Ok(sales);
            }
         }
      }

      [HttpPut]
      public IHttpActionResult Update(Sale model)
      {
         using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
         {
            var salesRepository = efundraisingProdUnitOfWork.CreateRepository<ISalesRepository>();
            salesRepository.Update(model);
            efundraisingProdUnitOfWork.Commit();
            return Ok();
         }
      }


       

        /// <summary>
        /// Returns a collection of Sales that meet the business rule to require a Follow Up Notification
        /// </summary>
        /// <returns></returns>
        [HttpGet]
      public IHttpActionResult GetAllRequiredFollowUps(bool requiresAFollowUp)
      {
          var days = Convert.ToInt32(ConfigurationManager.AppSettings["DaysInThePast"]);
          var daysInThePast = DateTime.Now.AddDays(-days);

          using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
          {
              var salesRepository = efundraisingProdUnitOfWork.CreateRepository<ISalesRepository>();
              var salesToNotify = salesRepository.GetRequiredFollowUpNotification(daysInThePast);
              return Ok(salesToNotify);  
          }
      }
      /// <summary>
      /// Returns all Paid sales that belong to a FC and are Purchase order
      /// </summary>
      /// <param name="isPaid"></param>
      /// <returns></returns>
      [HttpGet]
      public IHttpActionResult GetAllPaid(bool isPaid)
      {
         var days = Convert.ToInt32(ConfigurationManager.AppSettings["DaysInThePast"]);
         var daysInThePast = DateTime.Now.AddDays(-days);

         using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
         {
            var salesRepository = efundraisingProdUnitOfWork.CreateRepository<ISalesRepository>();
            var salesToNotify = salesRepository.GetPaidSales(daysInThePast);
            return Ok(salesToNotify);
         }

      }
   }
}
