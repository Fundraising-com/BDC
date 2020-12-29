using System;
using GA.BDC.Data.Fundraising.EFundraisingProd.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundraisingProd.Mappers
{
   public static class LeadMapper
   {
      public static Lead Hydrate(lead lead)
      {
            var result = new Lead
            {
                Address = new Address { Address1 = lead.street_address, City = lead.city, Country = new Country { Name = lead.country_code, Code = lead.country_code }, Region = new Region { Code = lead.state_code }, PostCode = lead.zip_code },
                Id = lead.lead_id,
                Email = lead.email,
                FirstName = lead.first_name,
                Group = lead.organization,
                LastName = lead.last_name,
                NumberOfMembers = lead.participant_count ?? 0,
                Phone = lead.day_phone,
                PromotionId = lead.promotion_id,
                Website = lead.group_web_site,
                ChannelCode = lead.channel_code,
                ConsultantId = lead.consultant_id,
                RepresentativeId = lead.ext_consultant_id ?? 0,
                Comments = lead.comments,
                IsSuscribed = lead.onemaillist,
                InitialPhoneNumberEntered = lead.initial_phone_number_entered == null ? lead.initial_phone_number_entered : false,
                OrigProsDte = lead.fund_raiser_start_date




            };
         return result;
      }

      public static lead Dehydrate(Lead lead)
      {
         return new lead
         {
            lead_id = lead.Id,
            first_name = lead.FirstName,
            last_name = lead.LastName,
            email = lead.Email,
            organization = lead.Group,
            participant_count = lead.NumberOfMembers,
            day_phone = lead.Phone,
            initial_phone_number_entered = true,
            evening_phone = lead.Phone,
            promotion_id = lead.PromotionId,
            group_web_site = lead.Website,
            channel_code = lead.ChannelCode,
            consultant_id = lead.ConsultantId,
            ext_consultant_id = lead.RepresentativeId,
            comments = lead.Comments,
            onemaillist = true,
            lead_entry_date = DateTime.Now,
            street_address = lead.Address != null ? lead.Address.Address1 : string.Empty,
            state_code = lead.Address != null && lead.Address.Region != null ? lead.Address.Region.Code : "N/A",
            country_code = lead.Address != null && lead.Address.Country != null ? lead.Address.Country.Code : string.Empty,
            zip_code = lead.Address != null ? lead.Address.PostCode : string.Empty,
            city = lead.Address != null ? lead.Address.City : string.Empty,
            lead_priority_id = 1,
            lead_status_id = 1,
            group_type_id = 99,
            organization_type_id = 99,
            division_id = 1,
            hear_id = 6,
            fk_kit_type_id = lead.KitType,
            title_id = 99,
            campaign_reason_id = 99,
            web_site_id = 2,
            activity_closing_reason_id = 1,
            committee_meeting_required = false,
            faxkit = true,
            emailkit = false,
            kit_to_send = false,
            day_phone_is_good = true,
            evening_phone_is_good = true,
            valid_email = true,
            fund_raiser_start_date = lead.OrigProsDte != null ? lead.OrigProsDte : DateTime.Now,
            has_been_contacted = false,
            day_phone_ext = "0",
            evening_phone_ext = "0",
            fund_raising_goal = 0,
            sent_to_pap = false

         };
      }
    }
}
