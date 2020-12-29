USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[efr_get_lead]    Script Date: 02/14/2014 13:01:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--select * from efr_get_lead
--create unique clustered index ix_view_lead_activity_id on efr_get_lead(lead_activity_id)
--create index ix_view_lead_id on efr_get_lead(lead_id)

CREATE view [dbo].[efr_get_lead] --with schemabinding 
as
SELECT     l.consultant_id
	, l.lead_id
	, c.name AS Consultant
	, c.email_address AS consultant_email
	, l.lead_entry_date
	, la.lead_activity_type_id
	, la.lead_activity_id
	, la.lead_activity_date
	, la.completed_date
	, l.lead_assignment_date
	, ls.Description AS Status
	, l.salutation
	, l.first_name
	, l.last_name
	, l.organization
	, l.street_address
	, l.city
	, l.state_code
	, l.zip_code
	, l.day_phone
	, l.day_time_call
	, l.evening_phone
	, l.fax
	, l.email
	, l.group_type_id
	, l.participant_count
	, l.fund_raising_goal
	, l.decision_maker
	, l.fund_raiser_start_date
	--, l.comments
	, l.country_code
	, l.has_been_contacted
	, l.lead_priority_id
	, p.promotion_type_code
	, pt.Description AS PromoType
	, p.description AS Promotion
	, l.fk_kit_type_id AS Kit_Type_ID
	, kt.Description AS KitType
	, l.day_phone_ext
	, l.evening_phone_ext
	, s.Time_Zone_Difference
	, l.promotion_id
	, l.kit_sent_date
	, p.partner_id
	, l.title_id
	, l.campaign_reason_id
	, l.organization_type_id
	, l.web_site_id
	, l.group_web_site
	, l.other_phone
	, w.Web_Site_Name
	, l.interests
	, l.day_phone_is_good
	, l.evening_phone_is_good
	, l.account_number
	, l.activity_closing_reason_id
	, l.ext_consultant_id
	, l.customer_status_id
FROM	dbo.lead l with(nolock)
INNER JOIN dbo.lead_activity la with(nolock) ON l.lead_id = la.lead_id 
INNER JOIN dbo.Lead_Activity_Type lat with(nolock)  ON la.lead_activity_type_id = lat.Lead_Activity_Type_Id 
INNER JOIN dbo.Lead_Status ls with(nolock) ON l.lead_status_id = ls.Lead_Status_ID 
INNER JOIN dbo.consultant c with(nolock) ON l.consultant_id = c.consultant_id 
INNER JOIN dbo.promotion p with(nolock) ON l.promotion_id = p.promotion_id 
INNER JOIN dbo.Promotion_Type pt with(nolock) ON p.promotion_type_code = pt.Promotion_Type_Code 
INNER JOIN dbo.Kit_Type kt with(nolock) ON l.fk_kit_type_id = kt.Kit_Type_ID 
INNER JOIN dbo.State s with(nolock) ON l.state_code = s.State_Code 
INNER JOIN dbo.Web_Site w with(nolock) ON l.web_site_id = w.Web_Site_Id
WHERE c.is_active = 1
AND c.department_id IN (7, 4, 18, 17)
and l.activity_closing_reason_id in (1, 2)


/*INNER JOIN (select lead_id
	, la.lead_activity_type_id
	, la.lead_activity_id
	, la.lead_activity_date
	, la.completed_date
	from dbo.lead_activity la with(nolock) 
	INNER JOIN dbo.Lead_Activity_Type lat with(nolock) 
	ON la.lead_activity_type_id = lat.Lead_Activity_Type_Id ) as la
	ON l.lead_id = la.lead_id */
GO
