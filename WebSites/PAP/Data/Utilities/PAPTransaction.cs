using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GA.BDC.Data;
using GA.BDC.Data.DataLayer;
using log4net;
using Newtonsoft.Json;
using GA.BDC.PAP.Data.SearchFilters;
using GA.BDC.PAP.Data.GpfRpcObjects;


namespace GA.BDC.PAP.Data.Utilities
{
    // ReSharper disable once InconsistentNaming
    public class PAPTransaction : PAPCommnicationBase
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(PAPTransaction));
        /// <summary>
        /// Returns the PAP Affiliate Id for the Affiliate name received
        /// </summary>
        /// <param name="afilliateName"></param>
        /// <returns></returns>
        private string SearchAffiliateId(string afilliateName)
        {
            var affiliateFilter = new AffiliateFilter(afilliateName.Trim());
            var respond = getGpfRpcSearchRespond(JsonWrapper.JsonMultiRequestWrapper(JsonConvert.SerializeObject(new GpfRpcSearchRequest(affiliateFilter)), PAPSession.PAPSessionGUID));
            return respond.GetFieldValue(affiliateFilter.FilterResult);
        }
        /// <summary>
        /// Returns the PAP Campaign Id for the Campaign name received
        /// </summary>
        /// <param name="campaignName"></param>
        /// <returns></returns>
        public string SearchCampaignIdByName(string campaignName)
        {
            var campaignFilter = new CampaignFilter(campaignName.Trim());
            var respond = getGpfRpcSearchRespond(JsonWrapper.JsonMultiRequestWrapper(JsonConvert.SerializeObject(new GpfRpcSearchRequest(campaignFilter)), PAPSession.PAPSessionGUID));
            return respond.GetFieldValue(campaignFilter.FilterResult);
        }
        /// <summary>
        /// Returns the Comission Id by the Campaign Id received
        /// </summary>
        /// <param name="campaignId"></param>
        /// <returns></returns>
        private string SearchCommissionIdByCampaignId(string campaignId)
        {
            var commissionFilter = new CommissionFilter(campaignId);
            var respond = getGpfRpcFormRespond(JsonWrapper.JsonMultiRequestWrapper(JsonConvert.SerializeObject(new GpfRpcServer(commissionFilter)), PAPSession.PAPSessionGUID));
            return respond.GetFieldValue(commissionFilter.FilterResult, commissionFilter.FilterType);
        }

        private string searchActivation(string campaignId)
        {
            ActivationFilter af = new ActivationFilter(campaignId);
            GpfRpcFormRespond respond = getGpfRpcFormRespond(JsonWrapper.JsonMultiRequestWrapper(JsonConvert.SerializeObject(new GpfRpcServer(af)), PAPSession.PAPSessionGUID));
            return respond.GetFieldValue(af.FilterResult, af.FilterType);
        }

        private string searchKickoff(string campaignId)
        {
            KickoffFilter cf = new KickoffFilter(campaignId);
            GpfRpcFormRespond respond = getGpfRpcFormRespond(JsonWrapper.JsonMultiRequestWrapper(JsonConvert.SerializeObject(new GpfRpcServer(cf)), PAPSession.PAPSessionGUID));
            return respond.GetFieldValue(cf.FilterResult, cf.FilterType);
        }
        /// <summary>
        /// Returns the PAP Banner Id for the local Banner Id received
        /// </summary>
        /// <param name="bannerId"></param>
        /// <returns></returns>
        private string SearchBannerId(string bannerId)
        {
            var bannerFilter = new BannerFilter(bannerId);
            var respond = getGpfRpcFormRespond(JsonWrapper.JsonMultiRequestWrapper(JsonConvert.SerializeObject(new GpfRpcSearchRequest(bannerFilter)), PAPSession.PAPSessionGUID));
            return respond.GetFieldValue(bannerFilter.FilterResult, bannerFilter.FilterType);
        }

        private GpfRpcSearchRespond getGpfRpcSearchRespond(string input)
        {
            JsonSerializer serializer = new JsonSerializer();
            Newtonsoft.Json.Linq.JArray result = JsonConvert.DeserializeObject((new PAPCommunication()).CallPapAPI(input, serverURL, serverMethodPost)) as Newtonsoft.Json.Linq.JArray;
            return new GpfRpcSearchRespond(result);
        }

        private GpfRpcFormRespond getGpfRpcFormRespond(string input)
        {
            var callPapApi = (new PAPCommunication()).CallPapAPI(input, serverURL, serverMethodPost);
            var result = JsonConvert.DeserializeObject(callPapApi) as Newtonsoft.Json.Linq.JArray;
            return new GpfRpcFormRespond(result);
        }
        /// <summary>
        /// Calculates the comission for the Campaign and the Affiliate
        /// </summary>
        /// <param name="campaignId"></param>
        /// <param name="userId"></param>
        /// <param name="commId"></param>
        /// <param name="totalCost"></param>
        /// <returns></returns>
        public double CalculateCommission(string campaignId, string userId, string commId, double totalCost)
        {
            var respond = getGpfRpcFormRespond(JsonWrapper.JsonMultiRequestWrapper(JsonConvert.SerializeObject(new GpfRpcCommissionCalculationRequest(campaignId, userId, commId, totalCost)), PAPSession.PAPSessionGUID));
            var comissionReceived = respond.GetRowValue((new CommissionAmountFilter()).FilterResult);
            try
            {
                return Convert.ToDouble(comissionReceived);
            }
            catch (Exception exception)
            {
                Log.Warn(string.Format("Exception while trying to calculate a comission. Campaign Id: {0}. Affiliate Id: {1}. Comission Id: {2}. Total Cost: {3}. Comission Returned: {4}.", campaignId, userId, commId, totalCost, comissionReceived), exception);
                return 0.0;
            }
        }
        /// <summary>
        /// Sends a Sale to PAP
        /// </summary>
        /// <param name="eFundraisingProdDataContext"></param>
        /// <param name="sale"></param>
        /// <returns></returns>
        public string PostSaleAPI(eFundraisingProdDataContext eFundraisingProdDataContext, pap_get_sales_to_be_processed_v2Result sale)
        {
            var productClass = Orders.GetProductClassBySaleId(eFundraisingProdDataContext, sale.sales_id);
            var productDescription = productClass != null ? productClass.description : string.Empty;
            var campaignId = string.Empty;
            if (sale.promotion_id == 0)
            {
                campaignId = SearchCampaignIdByName("Online Store Orders"); //MGP Sales
            } 
            else if (sale.client_sequence_code == "OF")
            {
                campaignId = SearchCampaignIdByName("Store Orders Fastfund"); //Store.fundraising.com sales
            }
            else
            {
                campaignId = string.IsNullOrEmpty(sale.product_category_desc) ? SearchCampaignIdByName("Default Campaign") : SearchCampaignIdByName(sale.product_category_desc);
            }
            var affiliateId = SearchAffiliateId(sale.a_aid);
            var comissionId = SearchCommissionIdByCampaignId(campaignId);
            var commissionAmount = CalculateCommission(campaignId, affiliateId, comissionId, ((double?)sale.total_amount) ?? 0.0);
            var bannerId = String.IsNullOrWhiteSpace(sale.a_bid) ? String.Empty : SearchBannerId(sale.a_bid);
            Log.DebugFormat("Product class: {0}. Product description: {1}. Campaign Id: {2}. User Id: {3}. Comission Id: {4}. Comission Ammount: {5}. Banner Id: {6}.", productClass, productDescription, campaignId, affiliateId, comissionId, commissionAmount, bannerId);
            var respond = getGpfRpcFormRespond(JsonWrapper.JsonMultiRequestWrapper(JsonConvert.SerializeObject(new GpfRpcFormRequest(sale, affiliateId, campaignId, comissionId, commissionAmount, productDescription, bannerId)), PAPSession.PAPSessionGUID));
            if (respond == null)
            {
                return string.Empty;
            }
            var transactionFilter = new TransactionFilter();
            var transactionId = respond.GetRowValue(transactionFilter.FilterResult, transactionFilter.FilterValue);
            return transactionId;
        }

        public string PostKitRequest(int leadId, string partnerName)
        {
            const string campaignId = "8ac7f560";
            const string commId = "924fd9aa";
            var userId = SearchAffiliateId(partnerName);
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception(string.Format("Affiliate Id not found for Lead Id {0} and Partner AAid {1}", leadId, partnerName));
            }
            var commissionAmount = CalculateCommission(campaignId, userId, commId, 0);
            var request = new GpfRpcFormRequest(userId, campaignId, commId, commissionAmount, 0, leadId);
            var respond = getGpfRpcFormRespond(JsonWrapper.JsonMultiRequestWrapper(JsonConvert.SerializeObject(request), PAPSession.PAPSessionGUID));
            if (respond == null) return string.Empty;
            var transactionFilter = new TransactionFilter();
            return respond.GetRowValue(transactionFilter.FilterResult, transactionFilter.FilterValue);
        }
        

        public string PostSaleAPI(eFundraisingProdDataContext eFundraisingProdDataContext, partner_attribute_value pav, es_get_valid_orders_items_by_partner_id_and_date_rangeResult sale, pap_product_category ppc)
        {
            var campaignId = SearchCampaignIdByName("Online Store Orders");
            var userId = SearchAffiliateId(pav.value);
            var commId = SearchCommissionIdByCampaignId(campaignId);
            var totalAmount = (double?) sale.total_amount ?? 0.0;
            var commissionAmount = CalculateCommission(campaignId, userId, commId, totalAmount);
            var respond = getGpfRpcFormRespond(JsonWrapper.JsonMultiRequestWrapper(JsonConvert.SerializeObject(new GpfRpcFormRequest(sale, userId, campaignId, commId, commissionAmount, sale.product_type_desc)), PAPSession.PAPSessionGUID));
            if (respond == null) return string.Empty;
            var transactionFilter = new TransactionFilter();
            return respond.GetRowValue(transactionFilter.FilterResult, transactionFilter.FilterValue);
        }

        public string PostActivationAPI(eFundraisingProdDataContext dc, partner_attribute_value pav, es_get_valid_orders_items_by_partner_idResult sale, string activationCampaign, string status)
        {
            var campaignId = SearchCampaignIdByName(activationCampaign);
            var userId = SearchAffiliateId(pav.value);
            var commId = searchActivation(campaignId);
            var commissionAmount = CalculateCommission(campaignId, userId, commId, (double?)(sale.sub_total - sale.tax - sale.freight) ?? 0.0);
            var respond = getGpfRpcFormRespond(JsonWrapper.JsonMultiRequestWrapper(JsonConvert.SerializeObject(new GpfRpcFormRequest(sale, userId, campaignId, commId, commissionAmount, sale.product_type_desc, status)), PAPSession.PAPSessionGUID));
            if (respond != null)
            {
                var transactionFilter = new TransactionFilter();
                return respond.GetRowValue(transactionFilter.FilterResult, transactionFilter.FilterValue);
            }
            
                return string.Empty;
            
        }

        public string PostActivationAPI(eFundraisingProdDataContext dc, partner_attribute_value pav, es_get_valid_orders_items_by_partner_id_and_date_rangeResult sale, string activationCampaign, string status)
        {
            var campaignId = SearchCampaignIdByName(activationCampaign);
            var userId = SearchAffiliateId(pav.value);
            var commId = searchActivation(campaignId);
            var commissionAmount = CalculateCommission(campaignId, userId, commId, (double?)(sale.sub_total - sale.tax - sale.freight) ?? 0.0);
            var respond = getGpfRpcFormRespond(JsonWrapper.JsonMultiRequestWrapper(JsonConvert.SerializeObject(new GpfRpcFormRequest(sale, userId, campaignId, commId, commissionAmount, sale.product_type_desc, status)), PAPSession.PAPSessionGUID));
            if (respond != null)
            {
                var transactionFilter = new TransactionFilter();
                return respond.GetRowValue(transactionFilter.FilterResult, transactionFilter.FilterValue);
            }
            
                return string.Empty;
            
        }

        public string PostKickoffAPI(eFundraisingProdDataContext eFundraisingProdDataContext, partner_attribute_value partnerAttributeValue, es_get_kickoff_by_partner_idResult kickoff, string kickoffCampaign, string status)
        {
            var campaignId = SearchCampaignIdByName(kickoffCampaign);
            var userId = SearchAffiliateId(partnerAttributeValue.value);
            var comissionId = searchActivation(campaignId);
            var commissionAmount = CalculateCommission(campaignId, userId, comissionId, 0.0);
            var respond = getGpfRpcFormRespond(JsonWrapper.JsonMultiRequestWrapper(JsonConvert.SerializeObject(new GpfRpcFormRequest(kickoff, userId, campaignId, comissionId, commissionAmount, kickoffCampaign, status)), PAPSession.PAPSessionGUID));
            if (respond != null)
            {
                var transactionFilter = new TransactionFilter();
                return respond.GetRowValue(transactionFilter.FilterResult, transactionFilter.FilterValue);
            }
            return string.Empty;
        }

        public string PostGroupAutoCreateAPI(eFundraisingProdDataContext eFundraisingProdDataContext, partner_attribute_value partnerAttributeValue, es_get_auto_created_groups_by_partner_idResult group, string autoCreateCampaign, string status)
        {
            var campaignId = SearchCampaignIdByName(autoCreateCampaign);
            var userId = SearchAffiliateId(partnerAttributeValue.value);
            var comissionId = searchActivation(campaignId);
            var commissionAmount = CalculateCommission(campaignId, userId, comissionId, 0.0);
            var respond = getGpfRpcFormRespond(JsonWrapper.JsonMultiRequestWrapper(JsonConvert.SerializeObject(new GpfRpcFormRequest(group, userId, campaignId, comissionId, commissionAmount, autoCreateCampaign, status)), PAPSession.PAPSessionGUID));
            if (respond != null)
            {
                var transactionFilter = new TransactionFilter();
                return respond.GetRowValue(transactionFilter.FilterResult, transactionFilter.FilterValue);
            }
            return string.Empty;
        }


        public string PostSaleAPI(eFundraisingProdDataContext eFundraisingProdDataContext, pap_get_sales_to_be_processed_v2Result sale, string userId)
        {
            var pc = Orders.GetProductClassBySaleId(eFundraisingProdDataContext, sale.sales_id);
            var productClass = pc != null ? pc.description : string.Empty;
            var campaignId = SearchCampaignIdByName(sale.product_category_desc);
            //string userId = searchAffiliate(sale.a_aid);
            var commId = SearchCommissionIdByCampaignId(campaignId);
            var commissionAmount = CalculateCommission(campaignId, userId, commId, ((double?)sale.total_amount) ?? 0.0);
            var bannerId = String.IsNullOrWhiteSpace(sale.a_bid) ? String.Empty : SearchBannerId(sale.a_bid);
            var respond = getGpfRpcFormRespond(JsonWrapper.JsonMultiRequestWrapper(JsonConvert.SerializeObject(new GpfRpcFormRequest(sale, userId, campaignId, commId, commissionAmount, productClass, bannerId)), PAPSession.PAPSessionGUID));
            if (respond != null)
            {
                var transactionFilter = new TransactionFilter();
                return respond.GetRowValue(transactionFilter.FilterResult, transactionFilter.FilterValue);
            }
            
                return string.Empty;
            

        }


        public string PostSaleWeb(pap_get_sales_to_be_processedResult sale, string status)
        {
            var st = new SaleTrack(sale) {S = status};
            var s = st.GetWebSerializedObject();
            var output = (new PAPCommunication()).CallPapAPI(string.Empty, s, serverMethodPost);
            return Regex.Matches(output, @"'(?<val>.*?)'").Cast<Match>().Select(match => match.Groups["val"].Value).FirstOrDefault();
        }

        public List<string[]> GetAllPendingTransactions(string statusType)
        {
            var pendingTransactions =
                JsonWrapper.JsonMultiRequestWrapper(
                    JsonConvert.SerializeObject(new GpfRpcSearchRequest(new TransactionTypeFilter(statusType))),
                    PAPSession.PAPSessionGUID);
            var respond = getGpfRpcFormRespond(pendingTransactions);

            return respond != null ? respond.Columns : null;

        }

        public void UpdateTransaction(string Id, int orderId, string statusType, string mechantNote)
        {
            var respond = getGpfRpcFormRespond(JsonWrapper.JsonMultiRequestWrapper(JsonConvert.SerializeObject(new GpfRpcTransactionChangeRequest(Id, orderId.ToString(), statusType, mechantNote)), PAPSession.PAPSessionGUID));
        }
        /// <summary>
        /// Returns the date when the affiliate was inserted in PAP
        /// </summary>
        /// <param name="affiliateName">Affiliate name</param>
        /// <returns>Inserted Date</returns>
        public string GetAffiliateDateInserted(string affiliateName)
        {
            var affiliateFilter = new AffiliateFilter(affiliateName.Trim());
            var respond = getGpfRpcSearchRespond(JsonWrapper.JsonMultiRequestWrapper(JsonConvert.SerializeObject(new GpfRpcSearchRequest(affiliateFilter)), PAPSession.PAPSessionGUID));
            return respond.GetFieldValue(new DateInsertedFilter().FilterResult);
        }
    }
}
