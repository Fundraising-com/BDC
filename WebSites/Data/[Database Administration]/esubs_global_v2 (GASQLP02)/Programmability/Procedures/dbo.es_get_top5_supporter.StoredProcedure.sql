USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_top5_supporter]    Script Date: 02/14/2014 13:06:31 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/* retourne un top 5 des meilleurs supporters pour une campagne 
   fblais 2005-10-03
*/

CREATE   procedure [dbo].[es_get_top5_supporter]
	@event_id int
as
select
	top 5
	m.first_name + ' ' +m.last_name as [supp_name]
	,sum(orderTotal) as amount
	,min(m.create_date) as create_date
from
	member_hierarchy mh
	inner join member m
	on m.member_id = mh.member_id
	inner join event_participation ep
	on ep.member_hierarchy_id=mh.member_hierarchy_id
	inner join qspstore.dbo.TotalsPerSaleTable tps
	on tps.suppid = ep.event_participation_id
where
	(mh.creation_channel_id in(12,14,29) or tps.suppid is not null) -- on prend tous les gens qui ont des subs et les supporters invited
	and ep.event_id = @event_id
group by 
	first_name
	,Last_name
order by 2 desc
GO
