USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_3_top_sellers]    Script Date: 02/14/2014 13:05:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- exec es_get_3_top_sellers 7
-- sp_who1 
CREATE   procedure [dbo].[es_get_3_top_sellers] 
	@prize_id int 
AS

-- get the time range for that prize

declare @start_date datetime
declare @end_date datetime

select top 1 @end_date = expiration_date, @start_date = create_date
from prize_item
where prize_id = @prize_id
and expiration_date > getdate()

 

select top 3
	ep.event_id    
	, m.first_name + ' ' +m.last_name as [supp_name]    
	, isnull(sum(tps.quantity),0) + isnull(sum(child.quantity),0) as quantity
	, isnull(sum(tps.price),0) + isnull(sum(child.price),0) as amount
	, min(m.create_date) as create_date
    , m.email_address
from event_participation ep with (nolock)
	inner join member_hierarchy mh with (nolock)
		on mh.member_hierarchy_id = ep.member_hierarchy_id
	inner join (
		select mh.parent_member_hierarchy_id
			  ,sum(tps.quantity) as quantity
			  , sum(tps.price) as price
			  , ep.event_id
		from member_hierarchy mh with (nolock)
			inner join event_participation ep with (nolock)
				on ep.member_hierarchy_id = mh.member_hierarchy_id
			left join (
				select supp_ID
				 , sum(quantity) as quantity
				 , sum(sub_total) as price
			from [es_get_valid_orders_items] ()
			group by supp_ID
			) tps
				on tps.supp_ID = ep.event_participation_id
		group by mh.parent_member_hierarchy_id, ep.event_id
	) child 
		on child.parent_member_hierarchy_id = mh.member_hierarchy_id and child.event_id = ep.event_id
	left join (
			select supp_ID
				 , sum(quantity) as quantity
				 , sum(sub_total) as price
			from [es_get_valid_orders_items] ()
			group by supp_ID
		) tps
		on tps.supp_ID = ep.event_participation_id
	inner join member m --with (index(0))
		on mh.member_id = m.member_id
where mh.parent_member_hierarchy_id is not null
group by ep.event_id, m.email_address, m.first_name , m.last_name
order by 3 desc

/*
select
	top 3
	m.first_name + ' ' +m.last_name as [supp_name]
	, ep.event_id
	, m.email_address
	, sum(quantity) as quantity
	, sum(price) as amount
	, min(m.create_date) as create_date
from
	member_hierarchy mh
	inner join member m
	on m.member_id = mh.member_id
	inner join event_participation ep
	on ep.member_hierarchy_id=mh.member_hierarchy_id
	inner join #tps tps
	on tps.suppid = ep.event_participation_id
	
where
	(mh.creation_channel_id in(12,14,29) or tps.suppid is not null) -- on prend tous les gens qui ont des subs et les supporters invited
	
	--and ep.event_id = @event_id
group by 
	first_name
	,Last_name
	,email_address
	, ep.event_id
order by 4 desc
*/

/*
select * from prize
select * from dbo.prize_item
*/
GO
