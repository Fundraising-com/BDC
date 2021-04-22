using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data.Repositories;
using PayPal.Api;
using Payment = PayPal.Api.Payment;
using GA.BDC.Shared.Data;
using GA.BDC.WebApi.EzFund.Helpers;

namespace GA.BDC.WebApi.EzFund.Controllers
{

    public class PaypalController : ApiController
    {
        [HttpOptions]
        public IHttpActionResult Options()
        {
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetRedirectUrlByClientId(int clientId)
        {

            using (var eZFundProdUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var salesRepository = eZFundProdUnitOfWork.CreateRepository<ISalesRepository>();
                var productsRepository = eZFundProdUnitOfWork.CreateRepository<IProductRepository>();
                var ezSale = salesRepository.GetEzFundSaleByOrderId(clientId);
                //var productsDictionary = new Dictionary<int, Product>();

                //foreach (var subProduct in ezSale.SubProducts)
                //{
                //    if (!productsDictionary.ContainsKey(subProduct.ParentId))
                //    {
                //        /*We don't have the entry, then we create it*/
                //        productsDictionary.Add(subProduct.ParentId, productsRepository.GetById(subProduct.ParentId));
                //    }
                //    /*We already have the product, just adjust the subproduct info*/
                //    var tmpSubProduct = productsDictionary[subProduct.ParentId].SubProducts.FirstOrDefault(x => x.ProductCode == subProduct.ProductCode);
                //    if (tmpSubProduct != null) tmpSubProduct.SelectedQuantity = tmpSubProduct.ProductQuantity = subProduct.SelectedQuantity;
                //}
                //foreach (var product in productsDictionary)
                //{
                //    ezSale.Items.Add(new EzFundSaleItem
                //    {
                //        Product = product.Value
                //    });
                //}

                var redirectUrl = string.Format(ConfigurationManager.AppSettings["ezfund.host"], "/shopping-cart/checkout");
                var apiContext = PaypalConfiguration.GetAPIContext();



                //var payer = new Payer { payment_method = "paypal" };
                //var redirUrls = new RedirectUrls
                //{
                //    cancel_url =
                //          string.Concat(ConfigurationManager.AppSettings["ezfund.host"], "/shopping-cart/checkout"),
                //    return_url =
                //          string.Concat(ConfigurationManager.AppSettings["ezfund.host"], "/shopping-cart/paypal-return")
                //};
                //var details = new Details
                //{
                //    tax = "0",
                //    shipping = sales.Sum(p => p.ShippingFee).ToString("F2"),
                //    subtotal = (sales.Sum(p => p.TotalAmount) - sales.Sum(p => p.ShippingFee)).ToString("F2")
                //};
                //var transactionList = new List<Transaction>
                //{
                //  new Transaction
                //  {
                //     description = "Fundraising.com Sale",
                //     invoice_number = sales[0].ClientId.ToString("F0"),
                //     amount = amount,
                //     item_list = itemList,
                //     custom = sales[0].ClientId.ToString("F0")

                //  }
                //};
                //var payment = new Payment
                //{
                //    intent = "sale",
                //    payer = payer,
                //    transactions = transactionList,
                //    redirect_urls = redirUrls
                //};
                //var createdPayment = payment.Create(apiContext);
                //var links = createdPayment.links.GetEnumerator();
                //while (links.MoveNext())
                //{
                //    var link = links.Current;
                //    if (link != null && link.rel.ToLower().Trim().Equals("approval_url"))
                //    {
                //        redirectUrl = link.href;
                //    }
                //}
                //return Ok(new { RedirectUrl = redirectUrl });
            }




                using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
            {
                using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
                {
                    var productRepository = efundStoreUnitOfWork.CreateRepository<IProductRepository>();
                    var salesRepository = efundraisingProdUnitOfWork.CreateRepository<ISalesRepository>();

                    var redirectUrl = string.Format(ConfigurationManager.AppSettings["fundraising.host"],
                       "/shopping-cart/checkout");
                    var sales = salesRepository.GetByClientId(clientId);
                    foreach (var item in sales.SelectMany(p => p.Items))
                    {
                        item.Product = productRepository.GetByScratchbookId(item.ScratchBookId);
                    }
                    var apiContext = PaypalConfiguration.GetAPIContext();

                    var itemList = new ItemList
                    {
                        items =
                          new List<Item>(
                             sales.SelectMany(x => x.Items)
                                .Select(
                                   p =>
                                      new Item
                                      {
                                          name = p.Product.Name,
                                          currency = "USD",
                                          price = p.UnitPrice.ToString("F2"),
                                          quantity = p.Quantity.ToString("F0"),
                                          sku = p.Product.Id.ToString("F0")
                                      }))
                    };
                    var payer = new Payer { payment_method = "paypal" };

                    var redirUrls = new RedirectUrls
                    {
                        cancel_url =
                          string.Concat(ConfigurationManager.AppSettings["fundraising.host"], "/shopping-cart/checkout"),
                        return_url =
                          string.Concat(ConfigurationManager.AppSettings["fundraising.host"], "/shopping-cart/paypal-return")
                    };
                    var details = new Details
                    {
                        tax = "0",
                        shipping = sales.Sum(p => p.ShippingFee).ToString("F2"),
                        subtotal = (sales.Sum(p => p.TotalAmount) - sales.Sum(p => p.ShippingFee)).ToString("F2")
                    };
                    var amount = new Amount
                    {
                        currency = "USD",
                        total = sales.Sum(p => p.TotalAmount).ToString("F2"),
                        details = details
                    };
                    var transactionList = new List<Transaction>
               {
                  new Transaction
                  {
                     description = "Fundraising.com Sale",
                     invoice_number = sales[0].ClientId.ToString("F0"),
                     amount = amount,
                     item_list = itemList,
                     custom = sales[0].ClientId.ToString("F0")

                  }
               };
                    var payment = new Payment
                    {
                        intent = "sale",
                        payer = payer,
                        transactions = transactionList,
                        redirect_urls = redirUrls
                    };
                    var createdPayment = payment.Create(apiContext);
                    var links = createdPayment.links.GetEnumerator();
                    while (links.MoveNext())
                    {
                        var link = links.Current;
                        if (link != null && link.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            redirectUrl = link.href;
                        }
                    }
                    return Ok(new { RedirectUrl = redirectUrl });
                }
            }
        }
        
        [HttpPost]
        public IHttpActionResult Post(PaypalPaymentRequest paypalPaymentRequest)
        {
            var apiContext = PaypalConfiguration.GetAPIContext();
            var paymentExecution = new PaymentExecution { payer_id = paypalPaymentRequest.PayerId };
            var payment = new Payment { id = paypalPaymentRequest.PaymentId };
            var executedPayment = payment.Execute(apiContext, paymentExecution);
            var paymentFound = Payment.Get(apiContext, paypalPaymentRequest.PaymentId);
            return Ok(new { ClientId = paymentFound.transactions[0].invoice_number, AuthorizationCode = executedPayment.id.Substring(0, 10) });
        }
    }
}
