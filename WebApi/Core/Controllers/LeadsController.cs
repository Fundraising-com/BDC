using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;
using GA.BDC.Shared.Helpers;

namespace GA.BDC.WebApi.Fundraising.Core.Controllers
{
    public class LeadsController : ApiController
    {

        [HttpOptions]
        public IHttpActionResult Options()
        {
            return Ok();
        }

        /// <summary>
        /// Searchs between the list of Leads and compare one by one against the source lead, at the end returns the lead that macthes the best
        /// </summary>
        /// <param name="sourceLead">Source Lead</param>
        /// <param name="leadsToCompare">Leads to be Compared against the Source Lead</param>
        /// <returns>Lead with the best match against the Source Lead</returns>
        private Lead FindBestMatch(Lead sourceLead, IList<Lead> leadsToCompare)
        {
            Lead bestLead = null;
            var bestRank = 0;
            foreach (var currentLead in leadsToCompare)
            {
                var currentRank = 0;
                if (String.Equals(sourceLead.FirstName, currentLead.FirstName, StringComparison.InvariantCultureIgnoreCase))
                {
                    currentRank++;
                }
                if (String.Equals(sourceLead.LastName, currentLead.LastName, StringComparison.InvariantCultureIgnoreCase))
                {
                    currentRank++;
                }

                if (!string.IsNullOrEmpty(currentLead.Phone) && String.Equals(sourceLead.Phone, currentLead.Phone, StringComparison.InvariantCultureIgnoreCase))
                {
                    currentRank++;
                }
                if (sourceLead.Address != null && currentLead.Address != null && String.Equals(sourceLead.Address.PostCode, currentLead.Address.PostCode, StringComparison.InvariantCultureIgnoreCase))
                {
                    currentRank++;
                }
                if (sourceLead.Address != null && currentLead.Address != null && String.Equals(sourceLead.Address.Address1, currentLead.Address.Address1, StringComparison.InvariantCultureIgnoreCase))
                {
                    currentRank++;
                }
                if (String.Equals(sourceLead.Email, currentLead.Email, StringComparison.InvariantCultureIgnoreCase))
                {
                    currentRank++;
                }

                if (currentRank >= bestRank)
                {
                    bestLead = currentLead;
                    bestRank = currentRank;
                }
            }
            return bestLead;
        }
        /// <summary>
        /// Merge the main info of both Leads instances
        /// </summary>
        /// <param name="source">Lead info coming from Website</param>
        /// <param name="target">Lead in DB already</param>
        private void MergeLeads(Lead source, Lead target)
        {
            //target.FirstName = source.FirstName;
            //target.LastName = source.LastName;
            //target.Email = source.Email;
            //target.Group = source.Group;
            //target.NumberOfMembers = source.NumberOfMembers;
            //target.Phone = source.Phone;
            //target.Website = source.Website;
            //target.Address = source.Address;

            target.FirstName = string.IsNullOrEmpty(source.FirstName) || source.FirstName == "-" ? target.FirstName : source.FirstName;
            target.LastName = string.IsNullOrEmpty(source.LastName) || source.LastName == "-" ? target.LastName : source.LastName;
            target.Email = string.IsNullOrEmpty(source.Email) || source.Email == "-" ? target.Email : source.Email;
            target.Group = string.IsNullOrEmpty(source.Group) || source.Group == "-" ? target.Group : source.Group;
            target.NumberOfMembers = source.NumberOfMembers;
            target.Phone = string.IsNullOrEmpty(source.Phone) || source.Phone == "-" || source.Phone == "0000000000" ? target.Phone : source.Phone;
            target.Website = string.IsNullOrEmpty(source.Website) || source.Website == "-" ? target.Website : source.Website;
            target.Address.Address1 = string.IsNullOrEmpty(source.Address.Address1) || source.Address.Address1 == "-" ? target.Address.Address1 : source.Address.Address1;
            target.Address.City = string.IsNullOrEmpty(source.Address.City) || source.Address.City == "-" ? target.Address.City : source.Address.City;
            target.Address.PostCode = string.IsNullOrEmpty(source.Address.PostCode) || source.Address.PostCode == "-" ? target.Address.PostCode : source.Address.PostCode;

        }
        [HttpPost]
        public IHttpActionResult Post(Lead model)
        {
            model.Phone = PhoneHelper.Clean(model.Phone);
            //  var addReportLeadActivity = false;
            using (var eFundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
            {
                var fundpassRepository = eFundraisingProdUnitOfWork.CreateRepository<IFundPassCouponRepositoryRepository>();
                var leadRepository = eFundraisingProdUnitOfWork.CreateRepository<ILeadRepository>();
                var consultantRepository = eFundraisingProdUnitOfWork.CreateRepository<IConsultantRepository>();
                //var leadsWithSameEmail = leadRepository.GetAllByEmail(model.Id, model.Email);
                var leadsWithSameEmail = leadRepository.GetByEmail(model.Email);
                var isDuplicated = (model.Phone != "0000000000" && leadsWithSameEmail.Any(p => p.Phone == model.Phone)) || (model.Phone == "0000000000" && leadsWithSameEmail.Any());
                var leadsWithSamePhone = model.Phone != "0000000000"
                   ? leadRepository.GetAllByPhone(model.Id, model.Phone)
                   : new List<Lead>();
                if (isDuplicated)
                {
                    var bestDuplicatedLead = FindBestMatch(model, leadsWithSameEmail);
                    bestDuplicatedLead.Consultant = consultantRepository.GetById(bestDuplicatedLead.ConsultantId);
                    if (bestDuplicatedLead.Consultant != null && bestDuplicatedLead.Consultant.IsActive)
                    {
                        var differences = string.Concat("The following infos changed since last visit : ",
                           model.FindDifferences(bestDuplicatedLead));
                        leadRepository.CreateLeadActivity(bestDuplicatedLead, LeadActivityType.FirstCall, false,
                           differences);
                    }
                    else
                    {
                        leadRepository.CreateUnassigmentLog(bestDuplicatedLead);
                        bestDuplicatedLead.ConsultantId = 0;
                    }
                    MergeLeads(model, bestDuplicatedLead);
                    leadRepository.Update(bestDuplicatedLead);
                    model = bestDuplicatedLead;
                }
                else
                {
                    if (model.RepresentativeId > 0)
                    {
                        using (var fastFundraisingUnitOfWork = new UnitOfWork(Database.FastFundraising))
                        {
                            var representativeRepository =
                               fastFundraisingUnitOfWork.CreateRepository<IRepresentativeRepository>();
                            var representative = representativeRepository.GetById(model.RepresentativeId);
                            model.RepresentativeId = representative.ExternalId;
                            model.ConsultantId = representative.ExternalId;
                        }
                        //addReportLeadActivity = true;


                    }
                    model.Id = leadRepository.Save(model);
                    leadRepository.CreateLeadComment(model, "Lead created using the new FR.com Lead Worlflow");
                    //check if any coupons exist
                    var couponsStillRemaining = fundpassRepository.GetAllRemaining();
                    var remaining = couponsStillRemaining.Count();
                    if (remaining > 0)
                    {
                        fundpassRepository.Update(model.Id);
                    }



                    //if (addReportLeadActivity) {
                    //    var activityCommentRep = "Lead entered through rep portal";
                    //    leadRepository.CreateLeadActivity(model, LeadActivityType.FirstCall, false, activityCommentRep);
                    //}
                    //else
                    //{
                    //    var activityCommentRep = "Lead created using the new FR.com Lead Worlflow";
                    //    leadRepository.CreateLeadActivity(model, LeadActivityType.FirstCall, false, activityCommentRep);
                    //}

                }
                leadRepository.CreateLeadVisit(model);


                eFundraisingProdUnitOfWork.Commit();
                model.IsDuplicated = isDuplicated;
                model.IsPotentiallyDuplicated = !isDuplicated &&
                                                (leadsWithSameEmail.Count > 2 || leadsWithSamePhone.Count > 1);
            }
            return Ok(model);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            using (var eFundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
            {
                var leadRepository = eFundraisingProdUnitOfWork.CreateRepository<ILeadRepository>();
                var lead = leadRepository.GetById(id);
                using (var efrCommonUnitOfWork = new UnitOfWork(Database.EFRCommon))
                {
                    var promotionRepository = efrCommonUnitOfWork.CreateRepository<IPromotionRepository>();
                    var partnerRepository = efrCommonUnitOfWork.CreateRepository<IPartnerRepository>();
                    var promotion = promotionRepository.GetById(lead.PromotionId);
                    var partner = partnerRepository.GetById(promotion.PartnerId);
                    lead.PartnerId = partner.Id;
                    lead.PartnerGuid = partner.Guid.ToString();

                    var leadsWithSameEmail = leadRepository.GetAllByEmail(lead.Id, lead.Email);
                    lead.IsDuplicated = leadsWithSameEmail.Any(p => p.Phone == lead.Phone);
                    lead.Phone = PhoneHelper.Clean(lead.Phone);
                    var leadsWithSamePhone = lead.Phone != "0000000000" ? leadRepository.GetAllByPhone(lead.Id, lead.Phone) : new List<Lead>();
                    lead.IsPotentiallyDuplicated = !lead.IsDuplicated && (leadsWithSameEmail.Count > 2 || leadsWithSamePhone.Count > 1);
                    return Ok(lead);
                }
            }
        }
        /// <summary>
        /// Returns a list of Leads created between the given dates
        /// </summary>
        /// <param name="start">Start Date</param>
        /// <param name="end">End Date</param>
        /// <returns>List of Leads found</returns>
        [HttpGet]
        public IHttpActionResult GetAll(DateTime start, DateTime end)
        {
            var result = new List<Lead>();
            using (var eFundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
            {
                var leadRepository = eFundraisingProdUnitOfWork.CreateRepository<ILeadRepository>();
                var leads = leadRepository.GetAllByDateRange(start, end);
                using (var efrCommonUnitOfWork = new UnitOfWork(Database.EFRCommon))
                {
                    foreach (var lead in leads)
                    {
                        var promotionRepository = efrCommonUnitOfWork.CreateRepository<IPromotionRepository>();
                        var partnerRepository = efrCommonUnitOfWork.CreateRepository<IPartnerRepository>();
                        var promotion = promotionRepository.GetById(lead.PromotionId);
                        var partner = partnerRepository.GetById(promotion.PartnerId);
                        lead.PartnerId = partner.Id;
                        lead.PartnerGuid = partner.Guid.ToString();

                        var leadsWithSameEmail = leadRepository.GetAllByEmail(lead.Id, lead.Email);
                        lead.IsDuplicated = leadsWithSameEmail.Any(p => p.Phone == lead.Phone);
                        lead.Phone = PhoneHelper.Clean(lead.Phone);
                        var leadsWithSamePhone = lead.Phone != "0000000000" ? leadRepository.GetAllByPhone(lead.Id, lead.Phone) : new List<Lead>();
                        lead.IsPotentiallyDuplicated = !lead.IsDuplicated && (leadsWithSameEmail.Count > 2 || leadsWithSamePhone.Count > 1);
                        result.Add(lead);
                    }
                }
            }
            return Ok(result);
        }

        [HttpPut]
        public IHttpActionResult Put(Lead lead)
        {
            try
            {
                using (var eFundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
                {
                    var leadRepository = eFundraisingProdUnitOfWork.CreateRepository<ILeadRepository>();

                    leadRepository.Update(lead);
                    lead = leadRepository.GetById(lead.Id);
                    eFundraisingProdUnitOfWork.Commit();
                    
                    return Ok(lead);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }
}
