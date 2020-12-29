USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_find_event]    Script Date: 02/14/2014 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	exec [es_find_event] 'efund', NULL, 'US', NULL, 0
	EXEC [dbo].[es_find_event]
		@keyword = 'OOPS',
		@city = NULL,
		@country_code = 'CA',
		@subdivision_code = NULL,
		@partner_id = 741
*/
CREATE   PROCEDURE [dbo].[es_find_event]
	@keyword varchar(255)
	, @city varchar(255) = NULL
	, @country_code nvarchar(2) = NULL
	, @subdivision_code nvarchar(7) = NULL
	, @partner_id int = NULL
AS
BEGIN

IF @partner_id = 741 OR @partner_id = 0
BEGIN
	SET @partner_id = NULL;
END

--set @start = 0
select e.event_id
, e.event_status_id 
, e.culture_code 
, e.event_name 
, e.start_date 
, e.end_date 
, e.active 
, e.comments 
, e.create_date 
, e.redirect 
, e.group_type_id 
, e.[profit_group_id]
, e.[profit_calculated]
, e.[processing_fee]
, e.event_type_id
, e.donation
,-1 as sponsor_event_participation_id 
, es.event_status_name 
, et.event_type_name 
, eg.group_id 
, g.partner_id 
, (case when pa.country_code is null then  right(e.culture_code, 2)  else pa.country_code end) as country_code 
, pa.subdivision_code
, pa.address_1 as address
, pa.city 
, COALESCE(eta.total_amount, 0) as total_amount
, COALESCE(eta.total_supporters, 0) as total_supporters
, COALESCE(eta.total_donation_amount, 0) as total_donation_amount
, COALESCE(eta.total_donars, 0) as total_donars
, COALESCE(eta.total_profit, 0) as total_profit
from event as e 
inner join event_group as eg with(nolock)
	on eg.event_id = e.event_id 
inner join event_status es with(nolock)
    on es.event_status_id = e.event_status_id 
inner join event_type et with(nolock)
	on e.event_type_id = et.event_type_id 
inner join [group] as g with(nolock)
	on g.group_id = eg.group_id 
inner join payment_info pi with(nolock)
	on [pi].group_id = g.group_id and [pi].event_id = e.event_id and [pi].active = 1 
left join postal_address as pa with(nolock)
	on pa.postal_address_id = [pi].postal_address_id 
left join event_total_amount eta with (nolock)
	on e.event_id = eta.event_id
WHERE e.active = 1 
AND e.displayable = 1 
AND (pa.city = @city or @city is null)
AND (pa.country_code = @country_code or @country_code is null or (pa.country_code is null and right(e.culture_code, 2)=@country_code))
AND (pa.subdivision_code = @subdivision_code or @subdivision_code is null)
AND (g.partner_id = CAST(@partner_id as varchar(10)) or @partner_id is null)
AND (event_name like '%' + REPLACE(LTRIM(RTRIM(@keyword)), ' ', '% ') + '%' or @keyword is null)
AND len(e.event_name) > 0    
order by e.event_name
END
GO
