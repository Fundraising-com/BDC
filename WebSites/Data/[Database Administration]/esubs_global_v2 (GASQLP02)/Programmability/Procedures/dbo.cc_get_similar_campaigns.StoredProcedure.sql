USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_get_similar_campaigns]    Script Date: 02/14/2014 13:04:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--dbo.cc_get_similar_campaigns 770079
-- Modified by Dat 2006-Jul-31
CREATE
	PROCEDURE [dbo].[cc_get_similar_campaigns]
	@event_id INT
AS
	declare @address as varchar(100)
	declare @eventID as int
	declare @groupID as int
	declare @groupName as varchar (200)
	declare @eventName as varchar (200)
	declare @firstName as varchar(50)
	declare @lastName as varchar(50)
	declare @dayPhone as varchar(20)
	declare @state as varchar(2)
	declare @extOrgID as varchar(128)
	declare @leadID as int 
	declare @partnerName as varchar(20)
	declare @IsActive as bit
	declare @memberHierarchyID as int

--un select * car les champs demandés vont changer et que beaucoup de champs sont requis.  à utiliser seulement avec le customer service tools car pas utilisé souvent.




SELECT
	@eventID = e.event_id
	,@groupID = g.group_id
	,@eventName= e.event_name
	,@groupName = group_name
	,@firstName  = first_name
	,@lastName = last_name
	,@memberHierarchyID= ep.member_hierarchy_id
	--,m.day_phone
	,@address = pa.address_1
	,@state = pa.subdivision_code 
	,@extOrgID = g.external_group_id
	,@leadID = g.lead_id
	,@partnerName = p.partner_name
	,@IsActive = e.active
FROM
	event e 
	INNER JOIN event_group eg
		ON e.event_id = eg.event_id 
	INNER JOIN event_type et 
		ON e.event_type_id = et.event_type_id
	inner join [group] g
		on g.group_id = eg.group_id
	INNER JOIN member_hierarchy mh
		on mh.member_hierarchy_id = g.sponsor_id
	INNER JOIN member m
		on mh.member_id = m.member_id
	LEFT OUTER JOIN payment_info pi 
		on g.group_id = pi.group_id and pi.active = 1
	INNER JOIN dbo.postal_address pa 
		ON pa.postal_address_id = pi.postal_address_id
	inner join event_participation ep 
		on ep.event_id = eg.event_id and ep.participation_channel_id = 3
	INNER JOIN dbo.partner p 
		ON g.partner_id = p.partner_id
WHERE
	e.event_id = @event_id 




SELECT 
	@eventID as event_id
	,@groupID as group_id
	,@eventName as event_name
	,@groupName as group_name
	, @memberHierarchyID as member_hierarchy_id
	,@firstName + ' ' + @lastName as [name]
	,@address as address_1
	,right(@state,2) as state_code
	,@extOrgID as external_group_id
	,@leadID as lead_id
	,@partnerName as partner_name
	,@IsActive as active


IF @firstName IS NULL
SET @firstName = ''

IF @lastName IS NULL
SET @lastName = ''

IF @leadID IS NULL
SET @leadID = -2147483648

IF @extorgid IS NULL
SET @extorgid = ''

IF @address IS NULL
SET @address = ''

SELECT distinct
	e.event_id
	,g.group_id
	,e.event_name
	,g.group_name
	,ep.member_hierarchy_id  
	,m.first_name + ' ' + m.last_name as [name]
	--,m.day_phone
	,pa.address_1
	,right(pa.subdivision_code,2) as state_code 
	,g.external_group_id
	,g.lead_id
	,p.partner_name
	,e.active
	, ep.event_participation_id
FROM
	event e 
	INNER JOIN event_group eg
		ON e.event_id = eg.event_id
	inner join [group] g
		on g.group_id = eg.group_id
	INNER JOIN member_hierarchy mh
		on mh.member_hierarchy_id = g.sponsor_id
	INNER JOIN member m
		on mh.member_id = m.member_id 
	LEFT OUTER JOIN payment_info pi 
		on g.group_id = pi.group_id and pi.active = 1
	INNER JOIN dbo.postal_address pa 
		ON pa.postal_address_id = pi.postal_address_id
	inner join event_participation ep 
		on ep.event_id = eg.event_id and ep.participation_channel_id = 3
	INNER JOIN dbo.partner p 
		ON g.partner_id = p.partner_id
WHERE
	( 
		(
		m.[first_name] is not null and m.[last_name] is not null  
		and m.[first_name] = @firstName and m.[last_name] = @lastName
		)	
		OR (pa.address_1 is not null and pa.address_1 like @address)
		OR g.group_name like @groupName
		OR (g.lead_id is not null and g.lead_id = @leadID) 
		--OR (g.external_group_id IS NOT NULL and g.external_group_id =@extorgid)
	)
AND e.event_id <> @event_id
AND e.active = 1
GO
