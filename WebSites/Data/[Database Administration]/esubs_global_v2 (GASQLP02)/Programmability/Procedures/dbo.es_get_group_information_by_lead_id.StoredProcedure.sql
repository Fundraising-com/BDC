USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_group_information_by_lead_id]    Script Date: 02/14/2014 13:05:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- EXEC [dbo].[es_get_group_information_by_lead_id] 1139492

CREATE  PROCEDURE [dbo].[es_get_group_information_by_lead_id]
	@lead_id int
AS
BEGIN
	declare @partner_id INT
	declare @has_collection_site BIT
	declare @event_participation_id INT
	
	select @event_participation_id = ep.event_participation_id
		from [group] g WITH (NOLOCK)
		inner join event_participation ep WITH (NOLOCK) on ep.member_hierarchy_id = g.sponsor_id
		where g.lead_id = @lead_id

	if @event_participation_id is not null 
	begin 
		-- Information sur le group deja existant
		select ep.event_participation_id
			, g.group_id
			, g.sponsor_id
			, g.partner_id
			, g.lead_id
			, NULL as name
			, NULL as group_name
			, NULL as address_1
			, NULL as city
			, NULL as subdivision_code
			, NULL as country_code
			, NULL as zip_code
			, NULL as day_phone
			, NULL as email
			, NULL as expected_membership
			, NULL as group_url
		from [group] g WITH (NOLOCK)
			inner join event_participation ep WITH (NOLOCK) on ep.member_hierarchy_id = g.sponsor_id
		where g.lead_id = @lead_id
	end
	else
	begin
		-- Get the partner information if the lead doesn't have a collection site
		select @partner_id = pa.partner_id
			, @has_collection_site = pa.has_collection_site
		from efundraisingprod..lead l WITH (NOLOCK)
			inner join efundweb..promotion p WITH (NOLOCK) on p.promotion_id = l.promotion_id
			inner join efundweb..partner pa WITH (NOLOCK) on pa.partner_id = p.partner_id 
		where l.lead_id = @lead_id
	
		if @has_collection_site = 0
			set @partner_id = 0
			
		select top 1
			NULL as event_participation_id
			, NULL as group_id
			, NULL as sponsor_id
			, @partner_id as partner_id
			, l.lead_id
			, RTRIM(LTRIM(l.first_name)) + ' ' + RTRIM(LTRIM(l.last_name)) as name
			, CASE ISNULL(l.organization, '')
				WHEN '' THEN	client.organization
				ELSE			l.organization
			  END AS group_name
			, l.street_address as address_1
			, l.city
			, l.state_code as subdivision_code
			, l.country_code
			, l.zip_code
			, l.day_phone
			, l.email
			, l.participant_count as expected_membership
			, l.group_web_site as group_url
		from efundraisingprod..lead l WITH (NOLOCK)
		left join eFundraisingProd..client as client WITH (NOLOCK)
			on client.lead_id = l.lead_id
		where l.lead_id = @lead_id
	end

END
GO
