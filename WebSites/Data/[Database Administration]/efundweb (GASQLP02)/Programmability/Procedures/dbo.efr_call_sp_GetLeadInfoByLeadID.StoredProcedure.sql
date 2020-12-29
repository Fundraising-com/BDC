USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[efr_call_sp_GetLeadInfoByLeadID]    Script Date: 02/14/2014 13:04:31 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[efr_call_sp_GetLeadInfoByLeadID](@intLeadID AS INT)
AS

SELECT lead_id,
		l.promotion_id, 
		consultant_id, 
		group_type_id,
		organization_type_id, 
		title_id, 
		first_name,
		last_name, 
		organization, 
		street_address,
		city, 
		state_code, 
		country_code,
		zip_code,
		day_phone,
		day_time_call,	
		evening_phone,
		fax,
		email,
		lead_entry_date,
		member_count,
		participant_count,
		fund_raising_goal
		decision_maker,
		comments,
		day_phone_ext,
		evening_phone_ext,
		p.partner_id
	FROM Lead l
	INNER JOIN Promotion p
		ON p.Promotion_id = l.promotion_id
	WHERE Lead_id = @intLeadID



	SELECT 
		part.partner_id
		,part.partner_group_type_id
		,part.partner_name
		,part.partner_path
		,part.eSubs_url
		, part.eStore_url
		,part.free_kit_url 
		,part.phone_number
		,part.url
		,part.guid
		,part.prize_eligible
		,part.has_collection_site
	FROM Partner part
		inner join promotion p
		on p.partner_id = part.partner_id
		inner join lead l 
		on l.promotion_id = p.promotion_id
	WHERE 
		l.lead_id = @intLeadID
GO
