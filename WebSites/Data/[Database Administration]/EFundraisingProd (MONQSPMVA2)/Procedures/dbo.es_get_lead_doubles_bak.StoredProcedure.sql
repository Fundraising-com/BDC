USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[es_get_lead_doubles_bak]    Script Date: 02/14/2014 13:08:37 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[es_get_lead_doubles_bak]
		@street as varchar(100)
		, @day_phone as varchar(20)
		, @evening_phone as varchar(20)
		, @email as varchar (50)
as

declare @lead_id as int
declare @consultant_id as int
declare @consultant_active as tinyint
declare @first_name as varchar(50) 
declare @last_name as varchar(50)
declare @organization as varchar(100)
declare @street_address as varchar(100)
declare @city as varchar(50)
declare @dayPhone as varchar(20)
declare @Participant_count as int
declare @partner_id as int	--mel


select  
	@consultant_id = l.consultant_id
	, @lead_id = l.lead_id
	, @partner_id = p.partner_id
	, @consultant_active = c.is_active
	, @first_name  = first_name
	, @last_name = last_name
	, @organization = organization
	, @street_address = street_address
	, @city = city
	, @dayPhone = day_phone
	, @participant_count = participant_count
from 
	lead l
	inner join consultant c
	on l.consultant_id = c.consultant_id
	inner join promotion p
	on p.promotion_id = l.promotion_id
where 
	(evening_phone like  cast(@evening_phone as varchar(20)) + '%'   and street_address = @street)
or 	(evening_phone like  cast(@evening_phone as varchar(20)) + '%'   and email = @email)
or 	(day_phone like  cast(@day_phone as varchar(20)) + '%'   and street_address = @street)
or 	(day_phone like  cast(@day_phone as varchar(20)) + '%'  and email = @email)
order by 
lead_entry_date desc 

if @lead_id is not null and @consultant_active = 0
begin
	update lead 
	set consultant_id = 0
	where lead_id = @lead_id
end

if @lead_id is not null 
begin
select @consultant_id as consultant_id
	, @lead_id  as lead_id
	, @partner_id as partner_id
	, @consultant_active as consultant_active
	, @first_name  as first_name
	, @last_name as last_name
	, @organization as organization
	, @street_address as street_address
	, @city as city
	, @dayPhone as day_phone
	, @participant_count as participant_count
end
else
begin
select  
	l.consultant_id
	,l.lead_id
	, p.partner_id
	, c.is_active
	,first_name
	,last_name
	,organization
	,street_address
	,city
	,day_phone
	,participant_count
from 
	lead l
	inner join consultant c
	on l.consultant_id = c.consultant_id
	inner join promotion p
	on p.promotion_id = l.promotion_id
where 1= 2
end
GO
