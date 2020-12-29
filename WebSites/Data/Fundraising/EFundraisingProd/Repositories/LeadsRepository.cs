using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using GA.BDC.Data.Fundraising.EFundraisingProd.Mappers;
using GA.BDC.Data.Fundraising.EFundraisingProd.Tables;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Entities;
using GA.BDC.Shared.Data.Repositories;
using System.Data.Entity.Validation;

namespace GA.BDC.Data.Fundraising.EFundraisingProd.Repositories
{
    public class LeadsRepository : ILeadRepository
    {

        private readonly DataProvider _dataProvider;
        public LeadsRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }
        public IList<Lead> GetAllByDateRange(DateTime start, DateTime end)
        {
            var inital = new DateTime(start.Year, start.Month, start.Day);
            var last = new DateTime(end.Year, end.Month, end.Day, 23, 59, 59);
            var ids = _dataProvider.Database.Connection.Query<int>("SELECT lead_id FROM lead L (NOLOCK) WHERE L.lead_entry_date BETWEEN @inital AND @last AND (L.fk_kit_type_id IN (42,43,44));",
                  new { inital, last }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return ids.Select(GetById).ToList();
        }

        public IList<Lead> GetAllByEmail(int id, string email)
        {

            var ids = _dataProvider.Database.Connection.Query<int>("SELECT lead_id FROM lead (NOLOCK) WHERE email = @email AND lead_id <> @id", new { email, id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return ids.Select(GetById).ToList();
        }

        public IList<Lead> GetByEmail(string email)
        {

            var ids = _dataProvider.Database.Connection.Query<int>("SELECT lead_id FROM lead (NOLOCK) WHERE email = @email", new {email}, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return ids.Select(GetById).ToList();
        }

        public IList<Lead> GetAllByPhone(int id, string phone)
        {
            var ids = _dataProvider.Database.Connection.Query<int>("SELECT lead_id FROM lead (NOLOCK) WHERE (day_phone = @phone OR evening_phone = @phone) AND lead_id <> @id", new { phone, id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return ids.Select(GetById).ToList();
        }

        public void CreateUnassigmentLog(Lead lead)
        {
            var id = _dataProvider.Database.Connection.ExecuteScalar<int>("SELECT MAX(UnAssignLogin_Id) + 1 FROM UnAssignLogin (NOLOCK)", null, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            var unassignLogin = new UnAssignLogin { Consultant_Id = lead.ConsultantId, Lead_Id = lead.Id, UnAssignLogin_Id = id, User_Name = "0", Unassignment_TimeStamp = DateTime.Now };
            _dataProvider.UnAssignLogins.Add(unassignLogin);
            _dataProvider.SaveChanges();
        }

        public void CreateLeadActivity(Lead lead, LeadActivityType activityType, bool isCompleted, string comments)
        {
            var id = _dataProvider.Database.Connection.ExecuteScalar<int>("SELECT MAX(lead_activity_id) + 1 FROM Lead_Activity (NOLOCK)", null, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            var leadActivity = new lead_activity
            {
                lead_activity_id = id,
                lead_activity_type_id = (int)activityType,
                lead_activity_date = DateTime.Now,
                comments = comments,
                lead_id = lead.Id
            };
            if (isCompleted)
            {
                leadActivity.completed_date = DateTime.Now;
            }
            _dataProvider.lead_activity.Add(leadActivity);
            _dataProvider.SaveChanges();
        }

        public void CreateLeadComment(Lead lead, string comments)
        {
            var id = _dataProvider.Database.Connection.ExecuteScalar<int>("SELECT MAX(Comments_Id) + 1 FROM Comments (NOLOCK)", null, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            var toBePersisted = new Comment
            {
                Comments_ID = id,
                Lead_ID = lead.Id,
                Consultant_ID = lead.ConsultantId,
                Department_ID = 4,
                Entry_Date = DateTime.Now,
                Comments = comments
            };
            _dataProvider.Comments.Add(toBePersisted);
            _dataProvider.SaveChanges();
        }

        public void CreateLeadVisit(Lead lead)
        {
            var id = _dataProvider.Database.Connection.ExecuteScalar<int>("SELECT MAX(Lead_Visit_ID) + 1 FROM Lead_Visit (NOLOCK)", null, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            var toBePersisted = new Lead_Visit
            {
                Lead_Visit_ID = id,
                Lead_ID = lead.Id,
                Channel_Code = lead.ChannelCode,
                Promotion_ID = lead.PromotionId,
                Visit_Date = DateTime.Now,
            };
            _dataProvider.Lead_Visit.Add(toBePersisted);
            _dataProvider.SaveChanges();
        }


        public void Delete(Lead model)
        {
            throw new NotImplementedException();
        }

        // To force a Merge
        public Lead GetById(int id)
        {
            var leadFound = _dataProvider.Database.Connection.Query<lead>("SELECT TOP 1 lead_id, lead_status_id, lead_qualification_type_id, lead_priority_id, temp_lead_id, division_id, promotion_id, channel_code, consultant_id, group_type_id, organization_type_id, hear_id, fk_kit_type_id, old_lead_id, assigner_id, referee_id, title_id, campaign_reason_id, web_site_id, promotion_code_id, activity_closing_reason_id, ext_consultant_id, salutation, first_name, last_name, organization, street_address, city, state_code, country_code, zip_code, day_phone, day_time_call, evening_phone, evening_time_call, fax, email, lead_entry_date, member_count, participant_count, fund_raising_goal, decision_date, decision_maker, committee_meeting_required, committee_meeting_date, fund_raiser_start_date, onemaillist, faxkit, emailkit, comments, kit_to_send, kit_sent, kit_sent_date, lead_assignment_date, interests, has_been_contacted, day_phone_ext, evening_phone_ext, other_phone, group_web_site, nb_queries, submit_date, cookie_content, first_contact_date, day_phone_is_good, evening_phone_is_good, account_number, valid_email, other_closing_activity_reason, transfered_date, matching_code, phone_number_tracking_id, customer_status_id, vif, address_zone_id, is_postal_address_validated, client_status_id, activation_date, fundraisers_per_year, wfc_customer_number, other_phone_is_good, sent_to_publitrac, sent_to_pap, initial_phone_number_entered FROM lead (NOLOCK) WHERE lead_id = @id", new { id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).First();
            var result = LeadMapper.Hydrate(leadFound);
            return result;
        }

        public IList<Lead> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Save(Lead lead)
        {
            try
            {
                var id = _dataProvider.Database.Connection.ExecuteScalar<int>("SELECT MAX(Lead_Id) + 1 FROM Lead (NOLOCK)", null, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
                lead.Id = id;
                var toBePersisted = LeadMapper.Dehydrate(lead);
                _dataProvider.leads.Add(toBePersisted);
                _dataProvider.SaveChanges();
                return toBePersisted.lead_id;
            }
            catch (DbEntityValidationException dbEntityValidationException)
            {
                foreach (var validationError in dbEntityValidationException.EntityValidationErrors.SelectMany(p => p.ValidationErrors))
                {
                    dbEntityValidationException.Data.Add($"EntityValidationError - {validationError.PropertyName}", validationError.ErrorMessage);
                }
                throw dbEntityValidationException;
            }
            catch (Exception exception)
            {

                throw exception;
            }

        }

        void IRepository<Lead>.Update(Lead model)
        {
            try
            {
                var toBeUpdated = _dataProvider.leads.Find(model.Id);
                toBeUpdated.first_name = model.FirstName;
                toBeUpdated.last_name = model.LastName;
                toBeUpdated.day_phone = model.Phone;
                toBeUpdated.evening_phone = model.Phone;
                toBeUpdated.email = model.Email;
                toBeUpdated.group_web_site = model.Website;
                toBeUpdated.organization = model.Group;
                toBeUpdated.street_address = model.Address != null ? model.Address.Address1 : string.Empty;
                toBeUpdated.city = model.Address != null ? model.Address.City : string.Empty;
                toBeUpdated.zip_code = model.Address != null ? model.Address.PostCode : string.Empty;
                toBeUpdated.state_code = model.Address != null && model.Address.Region != null ? model.Address.Region.Code : "N/A";
                toBeUpdated.country_code = model.Address != null && model.Address.Country != null ? model.Address.Country.Code : "N/A";
                toBeUpdated.initial_phone_number_entered = model.InitialPhoneNumberEntered;
                toBeUpdated.sent_to_pap = toBeUpdated.sent_to_pap;
                //toBeUpdated.member_count = model.NumberOfMembers;
                //toBeUpdated.interests = toBeUpdated.interests != null ? toBeUpdated.interests + model.Interest : null;
                //toBeUpdated.group_type_id = toBeUpdated.group_type_id == 99 ? Convert.ToByte(model.GroupType) == 0 ? Convert.ToByte(99) : Convert.ToByte(model.GroupType);
                //toBeUpdated.fund_raising_goal = toBeUpdated.fund_raising_goal != 0 ? toBeUpdated.fund_raising_goal : Convert.ToInt32(model.AmountToRaise);
                toBeUpdated.comments = toBeUpdated.comments + " - " + model.Comments;

                if (toBeUpdated.member_count == 0 || toBeUpdated.member_count == null)
                {
                    toBeUpdated.member_count = model.NumberOfMembers;
                }

                if (Convert.ToInt32(model.AmountToRaise) > 0)

                {
                    if (model.AmountToRaise == null)
                    {
                        toBeUpdated.fund_raising_goal = 0;
                    }
                    else
                    {
                        toBeUpdated.fund_raising_goal = Convert.ToInt32(model.AmountToRaise);
                    }

                  
                }

                if (toBeUpdated.interests == null || toBeUpdated.interests == string.Empty)
                {
                    toBeUpdated.interests = toBeUpdated.interests + model.Interest;
                }

                if (model.GroupType != 0)
                {
                    toBeUpdated.group_type_id = Convert.ToByte(model.GroupType);
                }


                _dataProvider.SaveChanges();
            }
            catch (DbEntityValidationException dbEntityValidationException)
            {
                foreach (var validationError in dbEntityValidationException.EntityValidationErrors.SelectMany(p => p.ValidationErrors))
                {
                    dbEntityValidationException.Data.Add($"EntityValidationError - {validationError.PropertyName}", validationError.ErrorMessage);
                }
                throw dbEntityValidationException;
            } catch(Exception exception)
            {
                throw exception;
            }
        }



        public Lead GetProspectById(int externalId)
        {
            throw new NotImplementedException();
        }
    }
}
