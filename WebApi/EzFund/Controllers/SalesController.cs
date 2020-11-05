using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GA.BDC.Data.EzFund.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;
using GA.BDC.Shared.Helpers;
using System.Text.RegularExpressions;
using GA.BDC.Data.EzFund.EZMain.Tables;

namespace GA.BDC.WebApi.EzFund.Controllers
{

	/*
	 * Update the sales reference number after credit card has been charged
	 */
	/*[HttpPost]
	public IHttpActionResult Post(EzFundSale model)
	{
		/* Fix phone number*/
		/*var phone = StringHelper.CanonicalString(model.Client.Phone);
		model.Client.Phone = PhoneHelper.Clean(phone);

*/
	 /*
     * EzFund EzOps Sales architecture
     * 1.- One invoice created in ORDR_INVOIC_TBL per sale
     * 2.-  Insert in ORDR_VEND_TBL one row by the following rule:
     *  * Group by Product Code and Warehouse Code
     * 3.- Insert items in ORDR_ITEM_TBL, where ORDR_SUB_ID is going to be the ORDR_SUB_ID in ORDR_VEND_TBL
     * 
     * When assigning warehouse to the Smencils we should choose EZFGAO-WH if available
     */
	 public class SalesController : ApiController
    {
		private static int MIN_ACCEPTED_PERCENTAGE = 80;
		private static int MIN_ACCEPTED_POINTS = 20;
		[HttpOptions]
        public IHttpActionResult Options()
        {
            return Ok();
        }

        [HttpPost]
		public IHttpActionResult Post(EzFundSale model)
		{
			/* Fix phone number*/
			var phone = StringHelper.CanonicalString(model.Client.Phone);
			model.Client.Phone = PhoneHelper.Clean(phone);

            /* TODO REDO - NEED TO FIX ORG INSERT DUPLICATION PROBLEM */
            /*Find or Create an Organization to which assign the Sale*/
            var organizationID = GetOrganizationId(model.Client);

            if (organizationID > 0)
            {
                model.OrganizationId = organizationID;
            }
            else
            {
                /*We need to create a new Organization with the Data Received
				 * and assign it to the sale
				 */
                using (var eZFundProdUnitOfWork = new UnitOfWork(Database.EZMain))
                {
                    var organizationRepository = eZFundProdUnitOfWork.CreateRepository<IOrganizationRepository>();
                    model.OrganizationId = organizationRepository.CreateNewOrganization(model);
                    eZFundProdUnitOfWork.Commit();
                }
            }
            /* We need this info to send a notification with the possible organizations */
            model.MatchingOrganizations = null;

            /*Organize Sale Items*/
            IList<EzFundSaleVendor> vendorList = OrganizeVendorItems(model);
			using (var eZFundProdUnitOfWork = new UnitOfWork(Database.EZMain))
			{
				/* 
				 * Inserting order data in tables:
				 * 
				 */
				var saleRepository = eZFundProdUnitOfWork.CreateRepository<ISalesRepository>();
				var saleId = saleRepository.Save(model);
				saleRepository.SaveVendorAndItems(vendorList, saleId);
				/*
				 * Begining a new WorkFlow
				*/
				var workflowRepository = eZFundProdUnitOfWork.CreateRepository<IWorkflowRepository>();
				var process = new WorkflowProcess
				{
					ProcessTemplateId = 170,
					OrganizationId = model.OrganizationId,
					ParentProcessId = null,
					CampaignId = 0,
					OrderId = saleId,
					StatusCode = EzFundSaleStatus.OPEN.ToString(),
					StatusDate = DateTime.Now,
					CreatorCode = "EZ-WEB",
					CreationDate = DateTime.Now,
					CompletionFlag = false,
					LastModificationDate = DateTime.Now
				};
				var processId = workflowRepository.SaveWorkflowProcess(process);
				var activity = new WorkflowActivity
				{
					ProcessId = processId,
					ActivityTemplateId = 240,
					PriorityCode = 5,
					StatusCode = EzFundSaleStatus.OPEN.ToString(),
					StatusDate = DateTime.Now,
					CreationDate = DateTime.Now,
					StartDate = DateTime.Now,
					ActorGroupCode = "EZ-WEB",
					CompletionFlag = false,
					LastModificationDate = DateTime.Now,
					LastModificationPersonCode = "EZ-WEB"
				};
				var activityId = workflowRepository.SaveWorkflowActivity(activity);
				eZFundProdUnitOfWork.Commit();
				model.OrderId = saleId;
				model.ProcessId = processId;
			}
			return Ok(model);
		}

		private IList<EzFundSaleVendor> OrganizeVendorItems(EzFundSale sale) {
            var vendorsList = new List<EzFundSaleVendor>();
            var itemsDictionary = new Dictionary<string, Dictionary<string,SubProduct>>();
            /**/
            foreach (var item in sale.Items)
            {
				/*
				* We are only going to use the first vendor and warehouse
				* as they have been already being ordered using their "SEQ_NBR" (Sequence Number)
				*/
				foreach (var subProduct in item.Product.SubProducts)
				{
					if (subProduct.SelectedQuantity > 0)
					{
						//lets generate the unique key WarehouseCode+ProductCode
						var key = subProduct.Warehouse?.FirstOrDefault()?.WarehouseCode ?? "" + subProduct.ProductCode;
						if (!itemsDictionary.ContainsKey(key))
						{
							/* 
							 * no key yet, create key entry 
							 * instantiate SubProduct List
							*/
							itemsDictionary.Add(key, new Dictionary<string, SubProduct>());
						}
						// Search for a previous item in the dictionary
						if (itemsDictionary[key].ContainsKey(subProduct.ItemCode))
						{
							//If already there, sum the quantity
							(itemsDictionary[key])[subProduct.ItemCode].StackedQuantity += subProduct.SelectedQuantity;
						}
						else
						{
							// Add current SubProduct to SubProduct List
							subProduct.StackedQuantity = subProduct.SelectedQuantity;
							itemsDictionary[key].Add(subProduct.ItemCode, subProduct);
						}
					}
				}

				}
				foreach (KeyValuePair<string, Dictionary<string, SubProduct>> entry in itemsDictionary)
            {
                var t = entry.Value.Values.ToList();
                var dataItem = entry.Value.First().Value; //using the first item to fill the shared info
                vendorsList.Add(new EzFundSaleVendor
                {
                    OFRMCode = dataItem.ProductCode,
                    VendorCode = dataItem.Vendor?.FirstOrDefault()?.VendorCode??"",
                    WarehouseCode = dataItem.Warehouse?.FirstOrDefault()?.WarehouseCode??"",
                    ProductCode = dataItem.ProductCode,
                    Status = EzFundSaleStatus.OPEN,
                    Items = entry.Value.Values.ToList()//entry.Value.Select(i => i.Value).ToList()
                });
            }
            return vendorsList;
        }

		private int GetOrganizationId(Client client)
		{
			var organizations = new Dictionary<int, int>();

			using (var eZFundProdUnitOfWork = new UnitOfWork(Database.EZMain))
			{
				var organizationRepository = eZFundProdUnitOfWork.CreateRepository<IOrganizationRepository>();
				var organizationIds = organizationRepository.GetOrganizationsByClientDataNew(client);
				//So far we got all the Organizations which ZipCode && State or ZipCode && State && City match to those provided by the user
				foreach (var org in organizationIds)
				{
					/* Get each organization and add point to each according to
					 * Same ZipCode
					 * Same State
					 * Same City
					 * Any Contact Phone match
					 * Same Contact Name
					 * Address matching any contact address
					 * Organization Name
					 */
					var points = 0;

					var currentOrganization = organizationRepository.GetById(org);

					//City name match: We see if any of the cities in the contact addesses matches with the one the client provided
					var found = false;
					for (var i = 0; i < currentOrganization.ContactAddresses.Count && !found; i++)
					{
						if (client.Addresses.Where(cs => string.Equals(cs.City, currentOrganization.ContactAddresses[i].CityName, StringComparison.OrdinalIgnoreCase)).Count() > 0)
						{
							points += 20;
							found = true;
						}
					}
					//Phone number match: We see if any of the Contact phone numbers matches with the one the client provided
					if (StringHelper.CanonicalString(currentOrganization.Phone1).Equals(StringHelper.CanonicalString(client.Phone)) ||
						 StringHelper.CanonicalString(currentOrganization.Phone2).Equals(StringHelper.CanonicalString(client.Phone)) ||
						 StringHelper.CanonicalString(currentOrganization.Phone3).Equals(StringHelper.CanonicalString(client.Phone)) ||
						 currentOrganization.Contacts.Where(c =>
						 StringHelper.CanonicalString(c.Phone1).Equals(StringHelper.CanonicalString(client.Phone)) ||
						 StringHelper.CanonicalString(c.Phone2).Equals(StringHelper.CanonicalString(client.Phone)) ||
						 StringHelper.CanonicalString(c.Phone3).Equals(StringHelper.CanonicalString(client.Phone))).Count() > 0)
					{
						points += 20;
					}
					//Contact name match: We see if any of the Contact names matches with the one the client provided
					if (currentOrganization.Contacts.Where(c => StringHelper.CanonicalString(c.ContactName).Equals(StringHelper.CanonicalString(string.Concat(client.FirstName, client.LastName)), StringComparison.OrdinalIgnoreCase)).Count() > 0 ||
						 currentOrganization.Contacts.Where(c => StringHelper.CanonicalString(c.ContactName).Equals(StringHelper.CanonicalString(client.Addresses[1].AttentionOf), StringComparison.OrdinalIgnoreCase)).Count() > 0)
					{
						points += 20;
					}

					//Contact email match: We see if any of the Contact emails matches with the one the client provided
					if (currentOrganization.Contacts.Where(c => StringHelper.RemoveWhiteSpaces(c.Email).Equals(StringHelper.RemoveWhiteSpaces(client.Email), StringComparison.OrdinalIgnoreCase)).Count() > 0)
					{
						points += 10;
					}

					//Address match: We see if any of the address in the contact addesses matches with the one the client provided
					found = false;
					for (var i = 0; i < currentOrganization.ContactAddresses.Count && !found; i++)
					{
						var canonicalBillingAddress = StringHelper.CanonicalString(client.Addresses[0].Address1).ToLower();
						var canonicalShippingAddress = StringHelper.CanonicalString(client.Addresses[1].Address1).ToLower();
						var canonicalCurrentAddress = StringHelper.CanonicalString(currentOrganization.ContactAddresses[i].Address1).ToLower();
						var maxLenBilling = Math.Max(canonicalCurrentAddress.Length, canonicalBillingAddress.Length);
						var maxLenShipping = Math.Max(canonicalCurrentAddress.Length, canonicalShippingAddress.Length);
						if ((((double)(maxLenBilling - LevenshteinDistance.Compute(canonicalCurrentAddress, canonicalBillingAddress)) / maxLenBilling) * 100) >= MIN_ACCEPTED_PERCENTAGE ||
							 (((double)(maxLenShipping - LevenshteinDistance.Compute(canonicalCurrentAddress, canonicalShippingAddress)) / maxLenShipping) * 100) >= MIN_ACCEPTED_PERCENTAGE)
						{
							points += 10;
							found = true;
						}
					}
					//Organization match: We see if the current organization matches with the one the client provided
					var canonicalCurrentOrganization = StringHelper.CanonicalString(currentOrganization.OrganizationCanonicalName).ToLower();
					var canonicalClientOrganization = StringHelper.CanonicalString(client.Organization).ToLower();
					var maxLen = Math.Max(canonicalCurrentOrganization.Length, canonicalClientOrganization.Length);
					points += (int)(((double)(maxLen - LevenshteinDistance.Compute(canonicalCurrentOrganization, canonicalClientOrganization)) / maxLen) * 10);

					//We finally add the Organization to the Organization's Dictionary
					organizations.Add(currentOrganization.OrganizationId, points);
				}
			}
			var bestMatchOrg = organizations.OrderByDescending(org => org.Value).FirstOrDefault();
			return bestMatchOrg.Value > MIN_ACCEPTED_POINTS ? bestMatchOrg.Key : 0;
		}

			[HttpGet]
        public IHttpActionResult Get(int id)
        {
            using (var eZFundProdUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var salesRepository = eZFundProdUnitOfWork.CreateRepository<ISalesRepository>();
                var ezSale = salesRepository.GetEzFundSaleByOrderId(id);
                var productsRepository = eZFundProdUnitOfWork.CreateRepository<IProductRepository>();
                var productsDictionary = new Dictionary<int, Product>();
                foreach (var subProduct in ezSale.SubProducts) {
                    if (!productsDictionary.ContainsKey(subProduct.ParentId))
                    {
                        /*We don't have the entry, then we create it*/
                        productsDictionary.Add(subProduct.ParentId, productsRepository.GetById(subProduct.ParentId));
                    }
                    /*We already have the product, just adjust the subproduct info*/
                    var tmpSubProduct = productsDictionary[subProduct.ParentId].SubProducts.FirstOrDefault(x => x.ProductCode == subProduct.ProductCode);
                    if (tmpSubProduct != null) tmpSubProduct.SelectedQuantity = tmpSubProduct.ProductQuantity = subProduct.SelectedQuantity;
                }

                foreach (var product in productsDictionary)
                {
                    ezSale.Items.Add(new EzFundSaleItem
                    {
                        Product = product.Value
                    });
                }
                return Ok(ezSale);
            }
        }

        [HttpGet]
        public IHttpActionResult GetByClientId(int clientId)
        {
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Update(EzFundSale model)
        {

            using (var eZFundProdUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var salesRepository = eZFundProdUnitOfWork.CreateRepository<ISalesRepository>();
                salesRepository.Update(model);
                eZFundProdUnitOfWork.Commit();
            }
                return Ok();
          
        }
        /// <summary>
        /// Returns a collection of Sales that meet the business rule to require a Follow Up Notification
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllRequiredFollowUps(bool requiresAFollowUp)
        {
            return Ok();
        }
        /// <summary>
        /// Returns all Paid sales that belong to a FC and are Purchase order
        /// </summary>
        /// <param name="isPaid"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllPaid(bool isPaid)
        {
            return Ok();
        }
    }
}
