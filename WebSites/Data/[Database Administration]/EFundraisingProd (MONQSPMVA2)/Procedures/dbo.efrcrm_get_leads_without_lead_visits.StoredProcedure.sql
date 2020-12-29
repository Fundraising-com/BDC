USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_leads_without_lead_visits]    Script Date: 02/14/2014 13:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec dbo.efrcrm_get_leads_wihout_lead_visits

CREATE    procedure [dbo].[efrcrm_get_leads_without_lead_visits]
as
select 
	l.lead_id
	, l.lead_status_id
	, l.lead_qualification_type_id
	, l.lead_priority_id
	, l.temp_lead_id
	, l.division_id
	, l.promotion_id
	, l.channel_code
	, l.consultant_id
	, l.group_type_id
	, l.organization_type_id
	, l.hear_id
	, l.fk_kit_type_id
	, l.old_lead_id
	, l.assigner_id
	, l.referee_id
	, l.title_id
	, l.campaign_reason_id
	, l.web_site_id
	, l.promotion_code_id
	, l.activity_closing_reason_id
	, l.ext_consultant_id
	, l.salutation
	, l.first_name
	, l.last_name
	, l.organization
	, l.street_address
	, l.city
	, l.state_code
	, l.country_code
	, l.zip_code
	, l.day_phone
	, l.day_time_call
	, l.evening_phone
	, l.evening_time_call
	, l.fax
	, l.email
	, l.lead_entry_date
	, l.member_count
	, l.participant_count
	, l.fund_raising_goal
	, l.decision_date
	, l.decision_maker
	, l.committee_meeting_required
	, l.committee_meeting_date
	, l.fund_raiser_start_date
	, l.onemaillist
	, l.faxkit
	, l.emailkit
	, l.comments
	, l.kit_to_send
	, l.kit_sent
	, l.kit_sent_date
	, l.lead_assignment_date
	, l.interests
	, l.has_been_contacted
	, l.day_phone_ext
	, l.evening_phone_ext
	, l.other_phone
	, l.group_web_site
	, l.nb_queries
	, l.submit_date
	, l.cookie_content
	, l.first_contact_date
	, l.day_phone_is_good
	, l.evening_phone_is_good
	, l.account_number
	, l.valid_email
	, l.other_closing_activity_reason
	, l.transfered_date
	, l.matching_code
	, l.phone_number_tracking_id
	, l.customer_status_id
	, l.address_zone_id 
	, l.fundraisers_per_year
from lead l 
left join lead_visit lv on l.lead_id = lv.lead_id
where lv.lead_id is null 
--and l.lead_entry_date > '2007-06-12 12:00:00'
GO
