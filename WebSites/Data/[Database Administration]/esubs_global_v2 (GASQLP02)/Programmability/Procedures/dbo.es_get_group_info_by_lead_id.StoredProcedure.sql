USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_group_info_by_lead_id]    Script Date: 02/14/2014 13:05:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
	
*/
CREATE PROCEDURE [dbo].[es_get_group_info_by_lead_id]
	@lead_id int
AS
BEGIN
SELECT 
	case when promotion.partner_id =154 then 0 else promotion.partner_id end as partner_id  --les lead de Go Fundraising doivent être sous efundraising
	, lead_id
	, Organization as group_name
	, participant_count as expected_membership
	-- Address info
	, RTRIM(LTRIM(first_name)) + ' ' + RTRIM(LTRIM(last_name)) as name
	, street_address as address_1
	, city
	, state_code as subdivision_code
	, zip_code
	, country_code
	, day_phone
FROM eFundraisingProd..lead as lead
	inner join eFundraisingProd..promotion as promotion
		on lead.promotion_id = promotion.promotion_id
WHERE lead_id = @lead_id
END
GO
